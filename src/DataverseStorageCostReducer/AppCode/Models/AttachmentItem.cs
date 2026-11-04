using System;

namespace DataverseStorageCostReducer.AppCode.Models
{
    public class AttachmentItem
    {
        public Guid Id { get; set; }
        public AttachmentSource Source { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public double FileSizeMB => FileSizeBytes / (1024.0 * 1024.0);
        public DateTime CreatedOn { get; set; }
        public int AgeInDays => (int)(DateTime.UtcNow - CreatedOn).TotalDays;
        
        /// <summary>
        /// Logical name of related entity (e.g., email, account, contact).
        /// </summary>
        public string RegardingEntityLogicalName { get; set; } = string.Empty;
        public Guid RegardingId { get; set; }
        public string RegardingName { get; set; } = string.Empty;

        /// <summary>
        /// Whether the attachment is orphaned (parent record deleted or missing).
        /// </summary>
        public bool IsOrphaned { get; set; }

        public bool SelectedForPurge { get; set; }
    }

    public class AttachmentSummaryGroup
    {
        public string ExtensionOrMime { get; set; } = string.Empty;
        public int Count { get; set; }
        public long TotalSizeBytes { get; set; }
        public double TotalSizeMB => TotalSizeBytes / (1024.0 * 1024.0);
        public double TotalSizeGB => TotalSizeBytes / (1024.0 * 1024.0 * 1024.0);
        public decimal EstimatedMonthlyCostUsd { get; set; }
    }
}
