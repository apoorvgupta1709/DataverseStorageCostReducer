using System;

namespace DataverseStorageCostReducer.AppCode.Models
{
    public class AsyncJobBreakdown
    {
        public string OperationType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public long Count { get; set; }
        public double EstimatedSizeMB { get; set; }
        public decimal EstimatedMonthlyCostUsd { get; set; }
        public bool IsRecommendedForPurge { get; set; }
    }

    public class AsyncJobSummary
    {
        public long TotalJobsCount { get; set; }
        public long SucceededJobsCount { get; set; }
        public long FailedJobsCount { get; set; }
        public long CanceledJobsCount { get; set; }
        public long WaitingOrInProgressCount { get; set; }
        public double TotalEstimatedSizeMB { get; set; }
        public double TotalEstimatedSizeGB => TotalEstimatedSizeMB / 1024.0;
        public decimal TotalMonthlyCostUsd { get; set; }
        public decimal PotentialMonthlySavingsUsd { get; set; }
    }
}
