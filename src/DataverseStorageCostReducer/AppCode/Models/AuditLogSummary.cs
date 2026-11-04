using System;

namespace DataverseStorageCostReducer.AppCode.Models
{
    public class AuditPartitionItem
    {
        public int PartitionNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long SizeBytes { get; set; }
        public double SizeMB => SizeBytes / (1024.0 * 1024.0);
        public double SizeGB => SizeBytes / (1024.0 * 1024.0 * 1024.0);
        public decimal EstimatedMonthlyCostUsd { get; set; }
        public bool CanBeDeleted { get; set; }
    }

    public class AuditEntityBreakdown
    {
        public string EntityLogicalName { get; set; } = string.Empty;
        public string EntityDisplayName { get; set; } = string.Empty;
        public long AuditEventCount { get; set; }
        public double EstimatedSizeMB { get; set; }
        public decimal EstimatedMonthlyCostUsd { get; set; }
    }

    public class AuditLogSummary
    {
        public long TotalAuditRecordCount { get; set; }
        public double TotalAuditSizeMB { get; set; }
        public double TotalAuditSizeGB => TotalAuditSizeMB / 1024.0;
        public decimal TotalMonthlyCostUsd { get; set; }
        public DateTime? OldestAuditDate { get; set; }
        public DateTime? NewestAuditDate { get; set; }
    }
}
