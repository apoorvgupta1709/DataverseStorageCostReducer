using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class ArchivalPurgeEngine
    {
        private readonly IOrganizationService _service;

        public ArchivalPurgeEngine(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public PurgeResult ExecutePurge(
            PurgeConfig config,
            Action<int, string>? progressCallback = null,
            Func<bool>? cancelCheck = null)
        {
            var result = new PurgeResult
            {
                IsDryRun = config.IsDryRun,
                StartTime = DateTime.UtcNow
            };

            try
            {
                // Create archive directory if archiving is enabled and not a dry run
                string? archiveDir = null;
                if (!config.IsDryRun && config.ArchiveBeforePurge)
                {
                    archiveDir = Path.Combine(
                        string.IsNullOrWhiteSpace(config.ArchiveDestinationPath) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : config.ArchiveDestinationPath,
                        $"Dataverse_Archive_{DateTime.UtcNow:yyyyMMdd_HHmmss}");
                    Directory.CreateDirectory(archiveDir);
                }

                // 1. Purge Email Attachments
                if (config.PurgeEmailAttachments)
                {
                    PurgeAttachments(
                        entityName: "activitymimeattachment",
                        idAttr: "activitymimeattachmentid",
                        fileSizeAttr: "filesize",
                        bodyAttr: "body",
                        fileNameAttr: "filename",
                        config: config,
                        archiveDir: archiveDir,
                        result: result,
                        progressCallback: progressCallback,
                        cancelCheck: cancelCheck);
                }

                // 2. Purge Note Attachments
                if (config.PurgeNotesAttachments && (cancelCheck == null || !cancelCheck()))
                {
                    PurgeAttachments(
                        entityName: "annotation",
                        idAttr: "annotationid",
                        fileSizeAttr: "filesize",
                        bodyAttr: "documentbody",
                        fileNameAttr: "filename",
                        config: config,
                        archiveDir: archiveDir,
                        result: result,
                        progressCallback: progressCallback,
                        cancelCheck: cancelCheck);
                }

                // 3. Purge Async Operations (Completed / Failed / Canceled Workflows)
                if ((config.PurgeCompletedAsyncWorkflows || config.PurgeFailedAsyncJobs || config.PurgeCanceledAsyncJobs) &&
                    (cancelCheck == null || !cancelCheck()))
                {
                    PurgeAsyncJobs(config, result, progressCallback, cancelCheck);
                }

                // Calculate savings
                result.EstimatedMonthlySavingsUsd = Math.Round((decimal)result.TotalGBFreed * 10m, 2);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Fatal error in purge execution: {ex.Message}");
            }
            finally
            {
                result.EndTime = DateTime.UtcNow;
            }

            return result;
        }

        private void PurgeAttachments(
            string entityName,
            string idAttr,
            string fileSizeAttr,
            string bodyAttr,
            string fileNameAttr,
            PurgeConfig config,
            string? archiveDir,
            PurgeResult result,
            Action<int, string>? progressCallback,
            Func<bool>? cancelCheck)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-config.OlderThanDays);
            int batchSize = Math.Min(config.BatchSize, 100);

            string query = $@"
                <fetch top='{config.MaxRecordsToPurge}'>
                  <entity name='{entityName}'>
                    <attribute name='{idAttr}' />
                    <attribute name='{fileNameAttr}' />
                    <attribute name='{fileSizeAttr}' />
                    {(!config.IsDryRun && config.ArchiveBeforePurge ? $"<attribute name='{bodyAttr}' />" : "")}
                    <filter type='and'>
                      <condition attribute='createdon' operator='le' value='{cutoffDate:yyyy-MM-dd}' />
                      <condition attribute='{fileSizeAttr}' operator='ge' value='{config.AttachmentMinSizeBytes}' />
                    </filter>
                  </entity>
                </fetch>";

            var response = _service.RetrieveMultiple(new FetchExpression(query));
            progressCallback?.Invoke(10, $"Found {response.Entities.Count} {entityName} matching criteria.");

            int count = 0;
            var deleteBatch = new ExecuteMultipleRequest
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                }
            };

            foreach (var record in response.Entities)
            {
                if (cancelCheck != null && cancelCheck()) break;

                result.TotalRecordsEvaluated++;
                long size = record.Contains(fileSizeAttr) ? Convert.ToInt64(record[fileSizeAttr]) : 0;
                string filename = record.GetAttributeValue<string>(fileNameAttr) ?? "file.bin";

                result.TotalBytesFreed += size;

                if (config.IsDryRun)
                {
                    result.TotalRecordsPurged++;
                    continue;
                }

                // Archiving
                if (config.ArchiveBeforePurge && archiveDir != null && record.Contains(bodyAttr))
                {
                    try
                    {
                        string bodyBase64 = record.GetAttributeValue<string>(bodyAttr);
                        if (!string.IsNullOrEmpty(bodyBase64))
                        {
                            byte[] bytes = Convert.FromBase64String(bodyBase64);
                            string safeFileName = $"{record.Id}_{SanitizeFileName(filename)}";
                            string targetPath = Path.Combine(archiveDir, safeFileName);
                            File.WriteAllBytes(targetPath, bytes);
                            result.ArchivedFilePaths.Add(targetPath);
                            result.TotalRecordsArchived++;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add($"Failed to archive {filename} ({record.Id}): {ex.Message}");
                    }
                }

                // Add delete request to batch
                deleteBatch.Requests.Add(new DeleteRequest { Target = new EntityReference(entityName, record.Id) });

                if (deleteBatch.Requests.Count >= batchSize)
                {
                    ExecuteDeleteBatch(deleteBatch, result);
                    deleteBatch.Requests.Clear();
                    count += batchSize;
                    int percent = Math.Min(90, (count * 100) / Math.Max(1, response.Entities.Count));
                    progressCallback?.Invoke(percent, $"Processed {count}/{response.Entities.Count} {entityName}...");
                }
            }

            if (deleteBatch.Requests.Count > 0 && !config.IsDryRun)
            {
                ExecuteDeleteBatch(deleteBatch, result);
            }
        }

        private void PurgeAsyncJobs(
            PurgeConfig config,
            PurgeResult result,
            Action<int, string>? progressCallback,
            Func<bool>? cancelCheck)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-config.OlderThanDays);
            var statusConditions = new List<string>();

            if (config.PurgeCompletedAsyncWorkflows) statusConditions.Add("<value>30</value>"); // Succeeded
            if (config.PurgeFailedAsyncJobs) statusConditions.Add("<value>31</value>");          // Failed
            if (config.PurgeCanceledAsyncJobs) statusConditions.Add("<value>32</value>");        // Canceled

            if (statusConditions.Count == 0) return;

            string query = $@"
                <fetch top='{config.MaxRecordsToPurge}'>
                  <entity name='asyncoperation'>
                    <attribute name='asyncoperationid' />
                    <attribute name='name' />
                    <filter type='and'>
                      <condition attribute='completedon' operator='le' value='{cutoffDate:yyyy-MM-dd}' />
                      <condition attribute='statuscode' operator='in'>
                        {string.Join("", statusConditions)}
                      </condition>
                    </filter>
                  </entity>
                </fetch>";

            var response = _service.RetrieveMultiple(new FetchExpression(query));
            progressCallback?.Invoke(50, $"Found {response.Entities.Count} system jobs matching purge rules.");

            var deleteBatch = new ExecuteMultipleRequest
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings { ContinueOnError = true, ReturnResponses = false }
            };

            foreach (var record in response.Entities)
            {
                if (cancelCheck != null && cancelCheck()) break;

                result.TotalRecordsEvaluated++;
                // Approx 2.5 KB per record
                result.TotalBytesFreed += 2500;

                if (config.IsDryRun)
                {
                    result.TotalRecordsPurged++;
                    continue;
                }

                deleteBatch.Requests.Add(new DeleteRequest { Target = new EntityReference("asyncoperation", record.Id) });
                if (deleteBatch.Requests.Count >= config.BatchSize)
                {
                    ExecuteDeleteBatch(deleteBatch, result);
                    deleteBatch.Requests.Clear();
                }
            }

            if (deleteBatch.Requests.Count > 0 && !config.IsDryRun)
            {
                ExecuteDeleteBatch(deleteBatch, result);
            }
        }

        private void ExecuteDeleteBatch(ExecuteMultipleRequest batch, PurgeResult result)
        {
            try
            {
                var response = (ExecuteMultipleResponse)_service.Execute(batch);
                foreach (var item in response.Responses)
                {
                    if (item.Fault != null)
                    {
                        result.Errors.Add($"Delete failed: {item.Fault.Message}");
                    }
                    else
                    {
                        result.TotalRecordsPurged++;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Batch execution error: {ex.Message}");
            }
        }

        private static string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }
    }
}
