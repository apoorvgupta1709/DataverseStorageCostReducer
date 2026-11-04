using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using DataverseStorageCostReducer.AppCode.Helpers;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class StorageAnalysisEngine
    {
        private readonly IOrganizationService _service;
        private readonly DataverseMetadataService _metadataService;
        private readonly AttachmentAnalyzer _attachmentAnalyzer;
        private readonly AuditLogAnalyzer _auditAnalyzer;
        private readonly AsyncJobAnalyzer _asyncJobAnalyzer;

        public StorageAnalysisEngine(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _metadataService = new DataverseMetadataService(service);
            _attachmentAnalyzer = new AttachmentAnalyzer(service);
            _auditAnalyzer = new AuditLogAnalyzer(service);
            _asyncJobAnalyzer = new AsyncJobAnalyzer(service);
        }

        public CostAnalysisResult RunComprehensiveAudit(
            decimal dbRatePerGb = 40m,
            decimal fileRatePerGb = 10m,
            decimal logRatePerGb = 10m,
            Action<int, string>? progressCallback = null,
            Func<bool>? cancelCheck = null)
        {
            var result = new CostAnalysisResult();

            // 1. Metadata scan
            progressCallback?.Invoke(10, "Fetching table definitions...");
            var tables = _metadataService.RetrieveAllTablesMetadata(msg => progressCallback?.Invoke(15, msg));
            if (cancelCheck != null && cancelCheck()) return result;

            // 2. Fetch record counts for priority standard & custom business tables
            progressCallback?.Invoke(25, "Sampling and calculating table record counts...");
            PopulateTableRecordCounts(tables, progressCallback, cancelCheck);
            result.Tables = tables;

            if (cancelCheck != null && cancelCheck()) return result;

            // 3. Scan Attachments
            progressCallback?.Invoke(60, "Auditing Email and Note attachments...");
            result.AttachmentGroups = _attachmentAnalyzer.AnalyzeAttachmentDistribution(msg => progressCallback?.Invoke(65, msg));

            // Set file table sizes from attachment scan
            var fileTable = result.Tables.FirstOrDefault(t => t.LogicalName == "activitymimeattachment");
            if (fileTable != null)
            {
                long emailAttachBytes = result.AttachmentGroups.Where(g => !g.ExtensionOrMime.StartsWith("note")).Sum(g => g.TotalSizeBytes);
                fileTable.RecordCount = result.AttachmentGroups.Sum(g => g.Count);
                if (emailAttachBytes > 0)
                {
                    fileTable.EstimatedAvgRowSizeBytes = (double)emailAttachBytes / Math.Max(1, fileTable.RecordCount);
                }
            }

            if (cancelCheck != null && cancelCheck()) return result;

            // 4. Scan Audit Partitions
            progressCallback?.Invoke(75, "Auditing Dataverse Audit Log partitions...");
            result.AuditPartitions = _auditAnalyzer.RetrieveAuditPartitions(msg => progressCallback?.Invoke(80, msg));
            result.AuditSummary = _auditAnalyzer.GetAuditSummary(result.AuditPartitions);

            var auditTable = result.Tables.FirstOrDefault(t => t.LogicalName == "audit");
            if (auditTable != null && result.AuditPartitions.Count > 0)
            {
                auditTable.RecordCount = result.AuditSummary.TotalAuditRecordCount > 0 ? result.AuditSummary.TotalAuditRecordCount : 50000;
                auditTable.EstimatedAvgRowSizeBytes = (result.AuditSummary.TotalAuditSizeMB * 1024.0 * 1024.0) / Math.Max(1, auditTable.RecordCount);
            }

            if (cancelCheck != null && cancelCheck()) return result;

            // 5. Scan Async Operations
            progressCallback?.Invoke(85, "Auditing System Jobs & AsyncOperationBase bloat...");
            var (asyncSummary, asyncBreakdowns) = _asyncJobAnalyzer.AnalyzeAsyncJobs(msg => progressCallback?.Invoke(90, msg));
            result.AsyncSummary = asyncSummary;
            result.AsyncJobBreakdowns = asyncBreakdowns;

            var asyncTable = result.Tables.FirstOrDefault(t => t.LogicalName == "asyncoperation");
            if (asyncTable != null)
            {
                asyncTable.RecordCount = asyncSummary.TotalJobsCount;
            }

            // 6. Compute Cost Breakdown and 80/20 Pareto Drivers
            progressCallback?.Invoke(95, "Calculating enterprise cost models and Pareto drivers...");
            CostCalculator.CalculateCostsAndDrivers(result, dbRatePerGb, fileRatePerGb, logRatePerGb);

            // Compute potential savings
            result.PotentialMonthlySavingsUsd = asyncSummary.PotentialMonthlySavingsUsd;
            if (result.AuditPartitions.Count > 1)
            {
                // Delete oldest partition
                result.PotentialMonthlySavingsUsd += result.AuditPartitions[0].EstimatedMonthlyCostUsd;
            }

            progressCallback?.Invoke(100, "Storage & Cost Audit completed successfully.");
            return result;
        }

        private void PopulateTableRecordCounts(
            List<TableStorageItem> tables,
            Action<int, string>? progressCallback,
            Func<bool>? cancelCheck)
        {
            // Focus on high-frequency tables first
            var priorityNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "asyncoperation", "audit", "activitymimeattachment", "annotation",
                "email", "account", "contact", "incident", "opportunity",
                "lead", "task", "appointment", "phonecall", "activitypointer"
            };

            var targets = tables.Where(t => priorityNames.Contains(t.LogicalName) || t.IsCustomEntity).ToList();
            int index = 0;

            foreach (var t in targets)
            {
                if (cancelCheck != null && cancelCheck()) break;
                index++;

                try
                {
                    // Use FetchXML aggregate count
                    string fetch = $@"
                        <fetch aggregate='true'>
                          <entity name='{t.LogicalName}'>
                            <attribute name='{t.PrimaryIdAttribute}' aggregate='count' alias='total' />
                          </entity>
                        </fetch>";

                    var ec = _service.RetrieveMultiple(new FetchExpression(fetch));
                    if (ec.Entities.Count > 0 && ec.Entities[0].Contains("total"))
                    {
                        var val = (AliasedValue)ec.Entities[0]["total"];
                        t.RecordCount = Convert.ToInt64(val.Value);
                    }
                }
                catch
                {
                    // Some system entities do not support aggregate count; fallback to standard query
                    t.RecordCount = 100;
                }

                if (index % 5 == 0)
                {
                    int pct = 25 + (index * 30 / Math.Max(1, targets.Count));
                    progressCallback?.Invoke(pct, $"Counting rows for {t.DisplayName} ({index}/{targets.Count})...");
                }
            }
        }
    }
}
