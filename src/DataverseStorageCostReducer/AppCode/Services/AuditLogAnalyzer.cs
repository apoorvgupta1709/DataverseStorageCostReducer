using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class AuditLogAnalyzer
    {
        private readonly IOrganizationService _service;

        public AuditLogAnalyzer(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<AuditPartitionItem> RetrieveAuditPartitions(Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke("Querying Dataverse audit partitions list...");

            var results = new List<AuditPartitionItem>();

            try
            {
                var request = new RetrieveAuditPartitionListRequest();
                var response = (RetrieveAuditPartitionListResponse)_service.Execute(request);

                if (response.AuditPartitionDetailCollection != null)
                {
                    // Audit partitions must be deleted sequentially starting from the oldest.
                    // Only the partition with the oldest EndDate can be deleted.
                    var sortedList = response.AuditPartitionDetailCollection
                        .OrderBy(p => p.EndDate)
                        .ToList();

                    for (int i = 0; i < sortedList.Count; i++)
                    {
                        var p = sortedList[i];
                        long sizeBytes = p.Size;

                        results.Add(new AuditPartitionItem
                        {
                            PartitionNumber = p.PartitionNumber,
                            StartDate = p.StartDate ?? DateTime.MinValue,
                            EndDate = p.EndDate ?? DateTime.MinValue,
                            SizeBytes = sizeBytes,
                            EstimatedMonthlyCostUsd = Math.Round((decimal)(sizeBytes / (1024.0 * 1024.0 * 1024.0)) * 10m, 2),
                            // Dataverse rule: Only the oldest audit partition can be deleted first
                            CanBeDeleted = (i == 0 && sortedList.Count > 1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                statusCallback?.Invoke($"Note: Could not retrieve audit partitions ({ex.Message}).");
            }

            return results;
        }

        public AuditLogSummary GetAuditSummary(List<AuditPartitionItem> partitions)
        {
            var summary = new AuditLogSummary();

            if (partitions != null && partitions.Count > 0)
            {
                summary.TotalAuditSizeMB = partitions.Sum(p => p.SizeMB);
                summary.TotalMonthlyCostUsd = partitions.Sum(p => p.EstimatedMonthlyCostUsd);
                summary.OldestAuditDate = partitions.Min(p => p.StartDate);
                summary.NewestAuditDate = partitions.Max(p => p.EndDate);
            }

            return summary;
        }

        public bool DeleteOldestAuditPartition(DateTime endDate, Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke($"Deleting audit partition up to {endDate:yyyy-MM-dd}...");

            try
            {
                var request = new DeleteAuditDataRequest
                {
                    EndDate = endDate
                };

                _service.Execute(request);
                statusCallback?.Invoke($"Audit partition up to {endDate:yyyy-MM-dd} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                statusCallback?.Invoke($"Failed to delete audit partition: {ex.Message}");
                throw;
            }
        }
    }
}
