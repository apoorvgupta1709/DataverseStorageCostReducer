using System;
using System.Collections.Generic;

namespace DataverseStorageCostReducer.AppCode.Models
{
    public class PurgeConfig
    {
        public bool IsDryRun { get; set; } = true;
        public bool ArchiveBeforePurge { get; set; } = true;
        public string ArchiveDestinationPath { get; set; } = string.Empty;

        // Cleanup Filters
        public int OlderThanDays { get; set; } = 365;
        public bool PurgeEmailAttachments { get; set; } = false;
        public bool PurgeNotesAttachments { get; set; } = false;
        public long AttachmentMinSizeBytes { get; set; } = 1024 * 1024; // 1MB default threshold
        public List<string> AttachmentExtensionsToPurge { get; set; } = new List<string>();

        public bool PurgeCompletedAsyncWorkflows { get; set; } = false;
        public bool PurgeFailedAsyncJobs { get; set; } = false;
        public bool PurgeCanceledAsyncJobs { get; set; } = false;

        public bool DeleteOldestAuditPartitions { get; set; } = false;
        public int AuditPartitionsToRetain { get; set; } = 3;

        // Batch execution limits
        public int BatchSize { get; set; } = 100;
        public int MaxRecordsToPurge { get; set; } = 5000;
    }

    public class PurgeResult
    {
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }
        public bool IsDryRun { get; set; }
        public int TotalRecordsEvaluated { get; set; }
        public int TotalRecordsPurged { get; set; }
        public int TotalRecordsArchived { get; set; }
        public long TotalBytesFreed { get; set; }
        public double TotalMBFreed => TotalBytesFreed / (1024.0 * 1024.0);
        public double TotalGBFreed => TotalBytesFreed / (1024.0 * 1024.0 * 1024.0);
        public decimal EstimatedMonthlySavingsUsd { get; set; }
        public List<string> ArchivedFilePaths { get; set; } = new List<string>();
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success => Errors.Count == 0;
    }
}
