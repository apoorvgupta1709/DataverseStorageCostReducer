using System;
using System.Collections.Generic;

namespace DataverseStorageCostReducer.AppCode.Models
{
    public class CostAnalysisResult
    {
        public DateTime ScanTimestamp { get; set; } = DateTime.UtcNow;
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationUrl { get; set; } = string.Empty;

        // Pricing Rates ($ per GB per Month)
        public decimal DatabasePricePerGb { get; set; } = 40m;
        public decimal FilePricePerGb { get; set; } = 10m;
        public decimal LogPricePerGb { get; set; } = 10m;

        // Storage Totals
        public double DatabaseTotalGB { get; set; }
        public double FileTotalGB { get; set; }
        public double LogTotalGB { get; set; }
        public double OverallTotalGB => DatabaseTotalGB + FileTotalGB + LogTotalGB;

        // Monthly Run Rates
        public decimal DatabaseMonthlyCost => (decimal)DatabaseTotalGB * DatabasePricePerGb;
        public decimal FileMonthlyCost => (decimal)FileTotalGB * FilePricePerGb;
        public decimal LogMonthlyCost => (decimal)LogTotalGB * LogPricePerGb;
        public decimal TotalMonthlyCost => DatabaseMonthlyCost + FileMonthlyCost + LogMonthlyCost;
        public decimal TotalAnnualRunRate => TotalMonthlyCost * 12m;

        // Estimated Potential Savings
        public double PotentialReclaimableGB { get; set; }
        public decimal PotentialMonthlySavingsUsd { get; set; }
        public decimal PotentialAnnualSavingsUsd => PotentialMonthlySavingsUsd * 12m;

        // Collections
        public List<TableStorageItem> Tables { get; set; } = new List<TableStorageItem>();
        public List<TableStorageItem> Top80PercentDrivers { get; set; } = new List<TableStorageItem>();
        public List<AttachmentSummaryGroup> AttachmentGroups { get; set; } = new List<AttachmentSummaryGroup>();
        public List<AuditPartitionItem> AuditPartitions { get; set; } = new List<AuditPartitionItem>();
        public List<AsyncJobBreakdown> AsyncJobBreakdowns { get; set; } = new List<AsyncJobBreakdown>();
        public AuditLogSummary AuditSummary { get; set; } = new AuditLogSummary();
        public AsyncJobSummary AsyncSummary { get; set; } = new AsyncJobSummary();
    }
}
