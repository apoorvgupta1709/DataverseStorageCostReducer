using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class AttachmentAnalyzer
    {
        private readonly IOrganizationService _service;

        public AttachmentAnalyzer(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<AttachmentSummaryGroup> AnalyzeAttachmentDistribution(Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke("Analyzing Email and Note attachments distribution...");

            var groups = new Dictionary<string, AttachmentSummaryGroup>(StringComparer.OrdinalIgnoreCase);

            // 1. Analyze Email Attachments (activitymimeattachment)
            AnalyzeEntityAttachments(
                entityName: "activitymimeattachment",
                fileSizeAttr: "filesize",
                fileNameAttr: "filename",
                mimeTypeAttr: "mimetype",
                source: AttachmentSource.EmailAttachment,
                groups: groups);

            // 2. Analyze Note Attachments (annotation)
            AnalyzeEntityAttachments(
                entityName: "annotation",
                fileSizeAttr: "filesize",
                fileNameAttr: "filename",
                mimeTypeAttr: "mimetype",
                source: AttachmentSource.NoteAnnotation,
                groups: groups,
                additionalCondition: "<condition attribute='isdocument' operator='eq' value='1' />");

            // Calculate costs ($10/GB file storage)
            foreach (var group in groups.Values)
            {
                group.EstimatedMonthlyCostUsd = Math.Round((decimal)group.TotalSizeGB * 10m, 2);
            }

            return groups.Values.OrderByDescending(g => g.TotalSizeBytes).ToList();
        }

        public List<AttachmentItem> RetrieveLargeOrOldAttachments(
            int olderThanDays,
            long minSizeBytes,
            int maxRecords = 500,
            Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke($"Querying attachments older than {olderThanDays} days and larger than {minSizeBytes / (1024 * 1024)} MB...");

            var results = new List<AttachmentItem>();
            var cutoffDate = DateTime.UtcNow.AddDays(-olderThanDays);

            // 1. Fetch Email Attachments
            string emailFetch = $@"
                <fetch top='{maxRecords}'>
                  <entity name='activitymimeattachment'>
                    <attribute name='activitymimeattachmentid' />
                    <attribute name='filename' />
                    <attribute name='mimetype' />
                    <attribute name='filesize' />
                    <attribute name='createdon' />
                    <attribute name='objectid' />
                    <attribute name='objecttypecode' />
                    <filter type='and'>
                      <condition attribute='createdon' operator='le' value='{cutoffDate:yyyy-MM-dd}' />
                      <condition attribute='filesize' operator='ge' value='{minSizeBytes}' />
                    </filter>
                    <order attribute='filesize' descending='true' />
                  </entity>
                </fetch>";

            var emailRecords = _service.RetrieveMultiple(new FetchExpression(emailFetch));
            foreach (var r in emailRecords.Entities)
            {
                var regardingRef = r.GetAttributeValue<EntityReference>("objectid");
                results.Add(new AttachmentItem
                {
                    Id = r.Id,
                    Source = AttachmentSource.EmailAttachment,
                    FileName = r.GetAttributeValue<string>("filename") ?? "Untitled_Attachment",
                    MimeType = r.GetAttributeValue<string>("mimetype") ?? "application/octet-stream",
                    FileSizeBytes = r.Contains("filesize") ? Convert.ToInt64(r["filesize"]) : 0,
                    CreatedOn = r.GetAttributeValue<DateTime>("createdon"),
                    RegardingEntityLogicalName = regardingRef?.LogicalName ?? "email",
                    RegardingId = regardingRef?.Id ?? Guid.Empty,
                    RegardingName = regardingRef?.Name ?? string.Empty
                });
            }

            // 2. Fetch Note Attachments
            string noteFetch = $@"
                <fetch top='{maxRecords}'>
                  <entity name='annotation'>
                    <attribute name='annotationid' />
                    <attribute name='filename' />
                    <attribute name='mimetype' />
                    <attribute name='filesize' />
                    <attribute name='createdon' />
                    <attribute name='objectid' />
                    <filter type='and'>
                      <condition attribute='isdocument' operator='eq' value='1' />
                      <condition attribute='createdon' operator='le' value='{cutoffDate:yyyy-MM-dd}' />
                      <condition attribute='filesize' operator='ge' value='{minSizeBytes}' />
                    </filter>
                    <order attribute='filesize' descending='true' />
                  </entity>
                </fetch>";

            var noteRecords = _service.RetrieveMultiple(new FetchExpression(noteFetch));
            foreach (var r in noteRecords.Entities)
            {
                var regardingRef = r.GetAttributeValue<EntityReference>("objectid");
                results.Add(new AttachmentItem
                {
                    Id = r.Id,
                    Source = AttachmentSource.NoteAnnotation,
                    FileName = r.GetAttributeValue<string>("filename") ?? "Untitled_Note",
                    MimeType = r.GetAttributeValue<string>("mimetype") ?? "application/octet-stream",
                    FileSizeBytes = r.Contains("filesize") ? Convert.ToInt64(r["filesize"]) : 0,
                    CreatedOn = r.GetAttributeValue<DateTime>("createdon"),
                    RegardingEntityLogicalName = regardingRef?.LogicalName ?? string.Empty,
                    RegardingId = regardingRef?.Id ?? Guid.Empty,
                    RegardingName = regardingRef?.Name ?? string.Empty
                });
            }

            return results.OrderByDescending(a => a.FileSizeBytes).ToList();
        }

        private void AnalyzeEntityAttachments(
            string entityName,
            string fileSizeAttr,
            string fileNameAttr,
            string mimeTypeAttr,
            AttachmentSource source,
            Dictionary<string, AttachmentSummaryGroup> groups,
            string additionalCondition = "")
        {
            try
            {
                // Fetch top 5,000 attachments sample to compute realistic file extension distribution
                string fetch = $@"
                    <fetch top='5000'>
                      <entity name='{entityName}'>
                        <attribute name='{fileSizeAttr}' />
                        <attribute name='{fileNameAttr}' />
                        <attribute name='{mimeTypeAttr}' />
                        <filter type='and'>
                          <condition attribute='{fileSizeAttr}' operator='not-null' />
                          {additionalCondition}
                        </filter>
                      </entity>
                    </fetch>";

                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    long size = r.Contains(fileSizeAttr) ? Convert.ToInt64(r[fileSizeAttr]) : 0;
                    string fileName = r.GetAttributeValue<string>(fileNameAttr) ?? string.Empty;
                    string ext = !string.IsNullOrWhiteSpace(fileName) && System.IO.Path.HasExtension(fileName)
                        ? System.IO.Path.GetExtension(fileName).ToLowerInvariant()
                        : (r.GetAttributeValue<string>(mimeTypeAttr) ?? "unknown");

                    if (!groups.TryGetValue(ext, out var group))
                    {
                        group = new AttachmentSummaryGroup
                        {
                            ExtensionOrMime = ext
                        };
                        groups[ext] = group;
                    }

                    group.Count++;
                    group.TotalSizeBytes += size;
                }
            }
            catch
            {
                // Graceful fallback if security permissions or table access is restricted
            }
        }
    }
}
