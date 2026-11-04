namespace DataverseStorageCostReducer.AppCode.Models
{
    public class TableStorageItem
    {
        public string LogicalName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public StorageCategory Category { get; set; } = StorageCategory.Database;
        public long RecordCount { get; set; }
        public double EstimatedAvgRowSizeBytes { get; set; }
        public double EstimatedTotalSizeBytes => RecordCount * EstimatedAvgRowSizeBytes;
        public double EstimatedTotalMB => EstimatedTotalSizeBytes / (1024.0 * 1024.0);
        public double EstimatedTotalGB => EstimatedTotalSizeBytes / (1024.0 * 1024.0 * 1024.0);
        
        /// <summary>
        /// Monthly cost calculated based on Microsoft Dataverse pricing.
        /// </summary>
        public decimal MonthlyCostUsd { get; set; }

        /// <summary>
        /// Annual cost run-rate ($).
        /// </summary>
        public decimal AnnualCostUsd => MonthlyCostUsd * 12m;

        /// <summary>
        /// Percentage of total storage across the environment.
        /// </summary>
        public double PercentageOfTotalStorage { get; set; }

        /// <summary>
        /// Indicates if this table falls into the top 80% cost driver group (Pareto principle).
        /// </summary>
        public bool IsTop80PercentDriver { get; set; }

        /// <summary>
        /// Whether the table is standard or custom.
        /// </summary>
        public bool IsCustomEntity { get; set; }

        /// <summary>
        /// Primary id attribute name.
        /// </summary>
        public string PrimaryIdAttribute { get; set; } = string.Empty;

        /// <summary>
        /// Primary name attribute.
        /// </summary>
        public string PrimaryNameAttribute { get; set; } = string.Empty;
    }
}
