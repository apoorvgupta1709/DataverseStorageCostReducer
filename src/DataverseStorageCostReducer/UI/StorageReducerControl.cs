using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using DataverseStorageCostReducer.AppCode.Helpers;
using DataverseStorageCostReducer.AppCode.Models;
using DataverseStorageCostReducer.AppCode.Services;

namespace DataverseStorageCostReducer.UI
{
    public partial class StorageReducerControl : PluginControlBase, IXrmToolBoxPluginControl
    {
        private CostAnalysisResult? _currentResult;
        private bool _isCanceling = false;

        public StorageReducerControl()
        {
            InitializeComponent();
        }

        private void StorageReducerControl_Load(object sender, EventArgs e)
        {
            // Default archive path to user's Documents folder
            txtArchivePath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dataverse_Storage_Archive");
            SetupDataGridViewStyles();
        }

        private void SetupDataGridViewStyles()
        {
            Color darkBg = Color.FromArgb(30, 41, 59);
            Color altBg = Color.FromArgb(23, 32, 51);
            Color headerBg = Color.FromArgb(51, 65, 85);
            Color textWhite = Color.FromArgb(241, 245, 249);
            Color gridLine = Color.FromArgb(51, 65, 85);

            foreach (var dgv in new[] { dgvTables, dgvAttachments, dgvAudit, dgvAsync })
            {
                dgv.EnableHeadersVisualStyles = false;
                dgv.BackgroundColor = darkBg;
                dgv.DefaultCellStyle.BackColor = darkBg;
                dgv.DefaultCellStyle.ForeColor = textWhite;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = altBg;
                dgv.GridColor = gridLine;
                dgv.BorderStyle = BorderStyle.None;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

                dgv.ColumnHeadersDefaultCellStyle.BackColor = headerBg;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(226, 232, 240);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 32;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to a Dataverse environment first using XrmToolBox.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isCanceling = false;
            btnScan.Enabled = false;
            btnCancel.Enabled = true;
            progressBar.Visible = true;
            progressBar.Value = 0;
            lblStatus.Text = "Starting Dataverse Storage & Cost Audit...";

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Analyzing Dataverse Storage Consumption & Cost Drivers...",
                Work = (worker, args) =>
                {
                    var engine = new StorageAnalysisEngine(Service);
                    var result = engine.RunComprehensiveAudit(
                        dbRatePerGb: 40m,
                        fileRatePerGb: 10m,
                        logRatePerGb: 10m,
                        progressCallback: (percent, msg) =>
                        {
                            worker.ReportProgress(percent, msg);
                        },
                        cancelCheck: () => _isCanceling);

                    if (ConnectionDetail != null)
                    {
                        result.OrganizationName = ConnectionDetail.OrganizationFriendlyName ?? ConnectionDetail.Organization ?? "Dataverse Org";
                        result.OrganizationUrl = ConnectionDetail.WebApplicationUrl ?? string.Empty;
                    }

                    args.Result = result;
                },
                ProgressChanged = (args) =>
                {
                    progressBar.Value = Math.Min(100, Math.Max(0, args.ProgressPercentage));
                    if (args.UserState is string statusMsg)
                    {
                        lblStatus.Text = statusMsg;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    btnScan.Enabled = true;
                    btnCancel.Enabled = false;
                    progressBar.Visible = false;

                    if (args.Error != null)
                    {
                        lblStatus.Text = "Error during audit: " + args.Error.Message;
                        MessageBox.Show("Audit failed: " + args.Error.Message, "Audit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (args.Result is CostAnalysisResult result)
                    {
                        _currentResult = result;
                        RenderDashboardResults(result);
                        lblStatus.Text = $"Audit complete. Scanned {result.Tables.Count} tables. Found {result.Top80PercentDrivers.Count} top 80% cost drivers.";
                    }
                }
            });
        }

        private void RenderDashboardResults(CostAnalysisResult result)
        {
            // 1. Top Metrics Cards
            lblCardStorageVal.Text = $"{result.OverallTotalGB:F2} GB";
            lblCardBillVal.Text = $"${result.TotalMonthlyCost:N2}";
            lblCardSavingsVal.Text = $"${result.PotentialMonthlySavingsUsd:N2}";
            lblCardDriversVal.Text = $"{result.Top80PercentDrivers.Count} Tables";

            // 2. Populate Table Breakdown Grid
            ApplyTableFilter();

            // 3. Populate Attachments Grid
            dgvAttachments.DataSource = null;
            dgvAttachments.DataSource = result.AttachmentGroups.Select(g => new
            {
                FileType = g.ExtensionOrMime,
                FilesCount = g.Count.ToString("N0"),
                SizeMB = $"{g.TotalSizeMB:F2} MB",
                SizeGB = $"{g.TotalSizeGB:F3} GB",
                MonthlyCost = $"${g.EstimatedMonthlyCostUsd:N2}"
            }).ToList();

            // 4. Populate Audit Partitions Grid
            dgvAudit.DataSource = null;
            dgvAudit.DataSource = result.AuditPartitions.Select(p => new
            {
                Partition = p.PartitionNumber,
                Start = p.StartDate.ToString("yyyy-MM-dd"),
                End = p.EndDate.ToString("yyyy-MM-dd"),
                SizeMB = $"{p.SizeMB:F2} MB",
                MonthlyCost = $"${p.EstimatedMonthlyCostUsd:N2}",
                Deletable = p.CanBeDeleted ? "YES (Oldest)" : "Locked"
            }).ToList();

            // 5. Populate Async Operations Grid
            dgvAsync.DataSource = null;
            dgvAsync.DataSource = result.AsyncJobBreakdowns.Select(j => new
            {
                Operation = j.OperationType,
                Status = j.Status,
                JobCount = j.Count.ToString("N0"),
                EstimatedSizeMB = $"{j.EstimatedSizeMB:F2} MB",
                MonthlyCost = $"${j.EstimatedMonthlyCostUsd:N2}",
                Purgeable = j.IsRecommendedForPurge ? "YES (Safe to Purge)" : "No (Active/Waiting)"
            }).ToList();
        }

        private void ApplyTableFilter()
        {
            if (_currentResult == null) return;

            string search = txtSearchTable.Text.Trim();
            bool paretoOnly = chkParetoOnly.Checked;

            var filtered = _currentResult.Tables.AsEnumerable();

            if (paretoOnly)
            {
                filtered = filtered.Where(t => t.IsTop80PercentDriver);
            }

            if (!string.IsNullOrEmpty(search))
            {
                filtered = filtered.Where(t =>
                    t.DisplayName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.LogicalName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dgvTables.DataSource = null;
            dgvTables.DataSource = filtered.OrderByDescending(t => t.MonthlyCostUsd).Select(t => new
            {
                Name = t.DisplayName,
                LogicalName = t.LogicalName,
                Category = t.Category.ToString(),
                Records = t.RecordCount.ToString("N0"),
                SizeMB = $"{t.EstimatedTotalMB:F2} MB",
                SizeGB = $"{t.EstimatedTotalGB:F3} GB",
                MonthlyCost = $"${t.MonthlyCostUsd:N2}",
                AnnualCost = $"${t.AnnualCostUsd:N2}",
                ShareOfCost = $"{t.PercentageOfTotalStorage:F2}%",
                Driver80 = t.IsTop80PercentDriver ? "★ TOP 80%" : ""
            }).ToList();
        }

        private void txtSearchTable_TextChanged(object sender, EventArgs e) => ApplyTableFilter();
        private void chkParetoOnly_CheckedChanged(object sender, EventArgs e) => ApplyTableFilter();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isCanceling = true;
            lblStatus.Text = "Canceling operation...";
        }

        private void btnExportHtml_Click(object sender, EventArgs e)
        {
            if (_currentResult == null)
            {
                MessageBox.Show("Please run a Storage Audit first before exporting an Executive Report.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "HTML Report (*.html)|*.html";
                sfd.FileName = $"Dataverse_Storage_Cost_Report_{DateTime.UtcNow:yyyyMMdd}.html";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    CsvExporter.ExportExecutiveHtmlReport(_currentResult, sfd.FileName);
                    if (MessageBox.Show("Report exported successfully! Would you like to view it now?", "Export Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_currentResult == null)
            {
                MessageBox.Show("Please run a Storage Audit first before exporting CSV data.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"Dataverse_Storage_Breakdown_{DateTime.UtcNow:yyyyMMdd}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    CsvExporter.ExportTableBreakdownToCsv(_currentResult, sfd.FileName);
                    MessageBox.Show("CSV data exported successfully to:\n" + sfd.FileName, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnPurgeWizard_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPurge;
        }

        private void btnBrowseArchive_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Destination Folder for Archived Attachments";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtArchivePath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnDeleteOldestPartition_Click(object sender, EventArgs e)
        {
            if (Service == null) return;
            if (_currentResult?.AuditPartitions == null || _currentResult.AuditPartitions.Count == 0)
            {
                MessageBox.Show("No audit partitions found. Please run the Storage Audit first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var oldest = _currentResult.AuditPartitions.FirstOrDefault(p => p.CanBeDeleted);
            if (oldest == null)
            {
                MessageBox.Show("There is only 1 partition or no deletable partition available. Microsoft Dataverse requires retaining at least the active audit partition.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var prompt = MessageBox.Show(
                $"Are you sure you want to delete the oldest audit partition ending on {oldest.EndDate:yyyy-MM-dd}?\n\n" +
                $"Estimated Size: {oldest.SizeMB:F2} MB\n" +
                $"Estimated Monthly Savings: ${oldest.EstimatedMonthlyCostUsd:N2}\n\n" +
                "THIS ACTION CANNOT BE UNDONE.",
                "Confirm Audit Partition Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (prompt != DialogResult.Yes) return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Deleting audit partition up to {oldest.EndDate:yyyy-MM-dd}...",
                Work = (worker, args) =>
                {
                    var analyzer = new AuditLogAnalyzer(Service);
                    analyzer.DeleteOldestAuditPartition(oldest.EndDate);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show("Failed to delete partition: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Audit partition deleted successfully! Re-running audit to update metrics.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnScan_Click(sender, e);
                    }
                }
            });
        }

        private void btnExecutePurge_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to Dataverse first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var config = new PurgeConfig
            {
                IsDryRun = chkDryRun.Checked,
                ArchiveBeforePurge = chkArchiveBeforePurge.Checked,
                ArchiveDestinationPath = txtArchivePath.Text,
                OlderThanDays = (int)numOlderThanDays.Value,
                PurgeEmailAttachments = chkPurgeEmailAttachments.Checked,
                PurgeNotesAttachments = chkPurgeNotes.Checked,
                PurgeCompletedAsyncWorkflows = chkPurgeCompletedWorkflows.Checked,
                PurgeFailedAsyncJobs = chkPurgeFailedAsync.Checked,
                PurgeCanceledAsyncJobs = chkPurgeCanceledAsync.Checked
            };

            if (!config.PurgeEmailAttachments && !config.PurgeNotesAttachments &&
                !config.PurgeCompletedAsyncWorkflows && !config.PurgeFailedAsyncJobs && !config.PurgeCanceledAsyncJobs)
            {
                MessageBox.Show("Please select at least one purge criteria checkbox.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!config.IsDryRun)
            {
                var confirm = MessageBox.Show(
                    "You are executing a LIVE PURGE.\n\n" +
                    $"Records older than {config.OlderThanDays} days matching your criteria will be permanently deleted from Dataverse.\n\n" +
                    (config.ArchiveBeforePurge ? $"Attachments will first be saved locally to:\n{config.ArchiveDestinationPath}\n\n" : "WARNING: Archiving is disabled! Files will not be backed up!\n\n") +
                    "Are you sure you want to proceed?",
                    "Confirm Live Purge",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;
            }

            txtPurgeLog.Clear();
            AppendLog($"--- [{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC] Starting Storage Purge (DryRun = {config.IsDryRun}) ---");
            btnExecutePurge.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = config.IsDryRun ? "Simulating storage purge (Dry Run)..." : "Archiving and purging Dataverse records...",
                Work = (worker, args) =>
                {
                    var engine = new ArchivalPurgeEngine(Service);
                    var purgeResult = engine.ExecutePurge(
                        config,
                        progressCallback: (pct, msg) => worker.ReportProgress(pct, msg),
                        cancelCheck: () => _isCanceling);

                    args.Result = purgeResult;
                },
                ProgressChanged = (args) =>
                {
                    if (args.UserState is string msg)
                    {
                        AppendLog(msg);
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    btnExecutePurge.Enabled = true;

                    if (args.Error != null)
                    {
                        AppendLog($"ERROR: {args.Error.Message}");
                        MessageBox.Show("Purge failed: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (args.Result is PurgeResult res)
                    {
                        AppendLog("--------------------------------------------------");
                        AppendLog($"Execution Completed: {res.EndTime:yyyy-MM-dd HH:mm:ss} UTC");
                        AppendLog($"Mode: {(res.IsDryRun ? "DRY RUN (Simulation)" : "LIVE PURGE")}");
                        AppendLog($"Records Evaluated: {res.TotalRecordsEvaluated:N0}");
                        AppendLog($"Records Purged: {res.TotalRecordsPurged:N0}");
                        AppendLog($"Records Archived: {res.TotalRecordsArchived:N0}");
                        AppendLog($"Space Freed: {res.TotalMBFreed:F2} MB ({res.TotalGBFreed:F3} GB)");
                        AppendLog($"Est. Monthly Savings: ${res.EstimatedMonthlySavingsUsd:N2}/mo");

                        if (res.Errors.Count > 0)
                        {
                            AppendLog($"Warnings / Errors encountered ({res.Errors.Count}):");
                            foreach (var err in res.Errors.Take(10)) AppendLog($"  • {err}");
                        }

                        string title = res.IsDryRun ? "Dry Run Completed" : "Purge Completed";
                        MessageBox.Show(
                            $"Process complete!\n\nRecords Purged: {res.TotalRecordsPurged:N0}\nSpace Freed: {res.TotalMBFreed:F2} MB\nMonthly Savings: ${res.EstimatedMonthlySavingsUsd:N2}",
                            title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            });
        }

        private void AppendLog(string message)
        {
            if (txtPurgeLog.InvokeRequired)
            {
                txtPurgeLog.Invoke(new Action(() => AppendLog(message)));
                return;
            }

            txtPurgeLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            txtPurgeLog.SelectionStart = txtPurgeLog.Text.Length;
            txtPurgeLog.ScrollToCaret();
        }
    }
}
