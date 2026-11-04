namespace DataverseStorageCostReducer.UI
{
    partial class StorageReducerControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportHtml = new System.Windows.Forms.ToolStripButton();
            this.btnExportCsv = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPurgeWizard = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.pnlCardSavings = new System.Windows.Forms.Panel();
            this.lblCardSavingsVal = new System.Windows.Forms.Label();
            this.lblCardSavingsTitle = new System.Windows.Forms.Label();
            this.pnlCardBill = new System.Windows.Forms.Panel();
            this.lblCardBillVal = new System.Windows.Forms.Label();
            this.lblCardBillTitle = new System.Windows.Forms.Label();
            this.pnlCardDrivers = new System.Windows.Forms.Panel();
            this.lblCardDriversVal = new System.Windows.Forms.Label();
            this.lblCardDriversTitle = new System.Windows.Forms.Label();
            this.pnlCardStorage = new System.Windows.Forms.Panel();
            this.lblCardStorageVal = new System.Windows.Forms.Label();
            this.lblCardStorageTitle = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabTables = new System.Windows.Forms.TabPage();
            this.pnlTableFilter = new System.Windows.Forms.Panel();
            this.chkParetoOnly = new System.Windows.Forms.CheckBox();
            this.txtSearchTable = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.tabAttachments = new System.Windows.Forms.TabPage();
            this.dgvAttachments = new System.Windows.Forms.DataGridView();
            this.tabAudit = new System.Windows.Forms.TabPage();
            this.pnlAuditActions = new System.Windows.Forms.Panel();
            this.btnDeleteOldestPartition = new System.Windows.Forms.Button();
            this.lblAuditNote = new System.Windows.Forms.Label();
            this.dgvAudit = new System.Windows.Forms.DataGridView();
            this.tabAsync = new System.Windows.Forms.TabPage();
            this.dgvAsync = new System.Windows.Forms.DataGridView();
            this.tabPurge = new System.Windows.Forms.TabPage();
            this.pnlPurgeContainer = new System.Windows.Forms.Panel();
            this.btnExecutePurge = new System.Windows.Forms.Button();
            this.txtPurgeLog = new System.Windows.Forms.TextBox();
            this.chkDryRun = new System.Windows.Forms.CheckBox();
            this.chkArchiveBeforePurge = new System.Windows.Forms.CheckBox();
            this.btnBrowseArchive = new System.Windows.Forms.Button();
            this.txtArchivePath = new System.Windows.Forms.TextBox();
            this.lblArchivePath = new System.Windows.Forms.Label();
            this.grpPurgeRules = new System.Windows.Forms.GroupBox();
            this.chkPurgeCanceledAsync = new System.Windows.Forms.CheckBox();
            this.chkPurgeFailedAsync = new System.Windows.Forms.CheckBox();
            this.chkPurgeCompletedWorkflows = new System.Windows.Forms.CheckBox();
            this.chkPurgeNotes = new System.Windows.Forms.CheckBox();
            this.chkPurgeEmailAttachments = new System.Windows.Forms.CheckBox();
            this.numOlderThanDays = new System.Windows.Forms.NumericUpDown();
            this.lblDays = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.pnlCardSavings.SuspendLayout();
            this.pnlCardBill.SuspendLayout();
            this.pnlCardDrivers.SuspendLayout();
            this.pnlCardStorage.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabTables.SuspendLayout();
            this.pnlTableFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.tabAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachments)).BeginInit();
            this.tabAudit.SuspendLayout();
            this.pnlAuditActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.tabAsync.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsync)).BeginInit();
            this.tabPurge.SuspendLayout();
            this.pnlPurgeContainer.SuspendLayout();
            this.grpPurgeRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOlderThanDays)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnScan,
            this.toolStripSeparator1,
            this.btnExportHtml,
            this.btnExportCsv,
            this.toolStripSeparator2,
            this.btnPurgeWizard,
            this.btnCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.toolStrip1.Size = new System.Drawing.Size(1100, 39);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Image = null;
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(155, 28);
            this.btnScan.Text = "▶  Run Storage Audit";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnExportHtml
            // 
            this.btnExportHtml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnExportHtml.Name = "btnExportHtml";
            this.btnExportHtml.Size = new System.Drawing.Size(175, 28);
            this.btnExportHtml.Text = "📊  Executive Report (HTML)";
            this.btnExportHtml.Click += new System.EventHandler(this.btnExportHtml_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(140, 28);
            this.btnExportCsv.Text = "📁  Export CSV Data";
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // btnPurgeWizard
            // 
            this.btnPurgeWizard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.btnPurgeWizard.Name = "btnPurgeWizard";
            this.btnPurgeWizard.Size = new System.Drawing.Size(165, 28);
            this.btnPurgeWizard.Text = "🧹  Purge & Archive Studio";
            this.btnPurgeWizard.Click += new System.EventHandler(this.btnPurgeWizard_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancel.Enabled = false;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 28);
            this.btnCancel.Text = "⏹ Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlCards
            // 
            this.pnlCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlCards.Controls.Add(this.pnlCardSavings);
            this.pnlCards.Controls.Add(this.pnlCardBill);
            this.pnlCards.Controls.Add(this.pnlCardDrivers);
            this.pnlCards.Controls.Add(this.pnlCardStorage);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(0, 39);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCards.Size = new System.Drawing.Size(1100, 95);
            this.pnlCards.TabIndex = 1;
            // 
            // pnlCardSavings
            // 
            this.pnlCardSavings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlCardSavings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardSavings.Controls.Add(this.lblCardSavingsVal);
            this.pnlCardSavings.Controls.Add(this.lblCardSavingsTitle);
            this.pnlCardSavings.Location = new System.Drawing.Point(544, 12);
            this.pnlCardSavings.Name = "pnlCardSavings";
            this.pnlCardSavings.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCardSavings.Size = new System.Drawing.Size(250, 70);
            this.pnlCardSavings.TabIndex = 3;
            // 
            // lblCardSavingsVal
            // 
            this.lblCardSavingsVal.AutoSize = true;
            this.lblCardSavingsVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCardSavingsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblCardSavingsVal.Location = new System.Drawing.Point(8, 28);
            this.lblCardSavingsVal.Name = "lblCardSavingsVal";
            this.lblCardSavingsVal.Size = new System.Drawing.Size(89, 37);
            this.lblCardSavingsVal.TabIndex = 1;
            this.lblCardSavingsVal.Text = "$0.00";
            // 
            // lblCardSavingsTitle
            // 
            this.lblCardSavingsTitle.AutoSize = true;
            this.lblCardSavingsTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardSavingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardSavingsTitle.Location = new System.Drawing.Point(8, 8);
            this.lblCardSavingsTitle.Name = "lblCardSavingsTitle";
            this.lblCardSavingsTitle.Size = new System.Drawing.Size(193, 19);
            this.lblCardSavingsTitle.TabIndex = 0;
            this.lblCardSavingsTitle.Text = "POTENTIAL MO. SAVINGS";
            // 
            // pnlCardBill
            // 
            this.pnlCardBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlCardBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardBill.Controls.Add(this.lblCardBillVal);
            this.pnlCardBill.Controls.Add(this.lblCardBillTitle);
            this.pnlCardBill.Location = new System.Drawing.Point(278, 12);
            this.pnlCardBill.Name = "pnlCardBill";
            this.pnlCardBill.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCardBill.Size = new System.Drawing.Size(250, 70);
            this.pnlCardBill.TabIndex = 2;
            // 
            // lblCardBillVal
            // 
            this.lblCardBillVal.AutoSize = true;
            this.lblCardBillVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCardBillVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.lblCardBillVal.Location = new System.Drawing.Point(8, 28);
            this.lblCardBillVal.Name = "lblCardBillVal";
            this.lblCardBillVal.Size = new System.Drawing.Size(89, 37);
            this.lblCardBillVal.TabIndex = 1;
            this.lblCardBillVal.Text = "$0.00";
            // 
            // lblCardBillTitle
            // 
            this.lblCardBillTitle.AutoSize = true;
            this.lblCardBillTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardBillTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardBillTitle.Location = new System.Drawing.Point(8, 8);
            this.lblCardBillTitle.Name = "lblCardBillTitle";
            this.lblCardBillTitle.Size = new System.Drawing.Size(183, 19);
            this.lblCardBillTitle.TabIndex = 0;
            this.lblCardBillTitle.Text = "EST. MONTHLY RUN-RATE";
            // 
            // pnlCardDrivers
            // 
            this.pnlCardDrivers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlCardDrivers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardDrivers.Controls.Add(this.lblCardDriversVal);
            this.pnlCardDrivers.Controls.Add(this.lblCardDriversTitle);
            this.pnlCardDrivers.Location = new System.Drawing.Point(810, 12);
            this.pnlCardDrivers.Name = "pnlCardDrivers";
            this.pnlCardDrivers.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCardDrivers.Size = new System.Drawing.Size(250, 70);
            this.pnlCardDrivers.TabIndex = 4;
            // 
            // lblCardDriversVal
            // 
            this.lblCardDriversVal.AutoSize = true;
            this.lblCardDriversVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCardDriversVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.lblCardDriversVal.Location = new System.Drawing.Point(8, 28);
            this.lblCardDriversVal.Name = "lblCardDriversVal";
            this.lblCardDriversVal.Size = new System.Drawing.Size(32, 37);
            this.lblCardDriversVal.TabIndex = 1;
            this.lblCardDriversVal.Text = "0";
            // 
            // lblCardDriversTitle
            // 
            this.lblCardDriversTitle.AutoSize = true;
            this.lblCardDriversTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardDriversTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardDriversTitle.Location = new System.Drawing.Point(8, 8);
            this.lblCardDriversTitle.Name = "lblCardDriversTitle";
            this.lblCardDriversTitle.Size = new System.Drawing.Size(189, 19);
            this.lblCardDriversTitle.TabIndex = 0;
            this.lblCardDriversTitle.Text = "TOP 80% COST DRIVERS";
            // 
            // pnlCardStorage
            // 
            this.pnlCardStorage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlCardStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardStorage.Controls.Add(this.lblCardStorageVal);
            this.pnlCardStorage.Controls.Add(this.lblCardStorageTitle);
            this.pnlCardStorage.Location = new System.Drawing.Point(12, 12);
            this.pnlCardStorage.Name = "pnlCardStorage";
            this.pnlCardStorage.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCardStorage.Size = new System.Drawing.Size(250, 70);
            this.pnlCardStorage.TabIndex = 1;
            // 
            // lblCardStorageVal
            // 
            this.lblCardStorageVal.AutoSize = true;
            this.lblCardStorageVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCardStorageVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.lblCardStorageVal.Location = new System.Drawing.Point(8, 28);
            this.lblCardStorageVal.Name = "lblCardStorageVal";
            this.lblCardStorageVal.Size = new System.Drawing.Size(113, 37);
            this.lblCardStorageVal.TabIndex = 1;
            this.lblCardStorageVal.Text = "0.00 GB";
            // 
            // lblCardStorageTitle
            // 
            this.lblCardStorageTitle.AutoSize = true;
            this.lblCardStorageTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardStorageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardStorageTitle.Location = new System.Drawing.Point(8, 8);
            this.lblCardStorageTitle.Name = "lblCardStorageTitle";
            this.lblCardStorageTitle.Size = new System.Drawing.Size(176, 19);
            this.lblCardStorageTitle.TabIndex = 0;
            this.lblCardStorageTitle.Text = "TOTAL STORAGE USAGE";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabTables);
            this.tabControlMain.Controls.Add(this.tabAttachments);
            this.tabControlMain.Controls.Add(this.tabAudit);
            this.tabControlMain.Controls.Add(this.tabAsync);
            this.tabControlMain.Controls.Add(this.tabPurge);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControlMain.Location = new System.Drawing.Point(0, 134);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1100, 516);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabTables
            // 
            this.tabTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabTables.Controls.Add(this.dgvTables);
            this.tabTables.Controls.Add(this.pnlTableFilter);
            this.tabTables.Location = new System.Drawing.Point(4, 29);
            this.tabTables.Name = "tabTables";
            this.tabTables.Padding = new System.Windows.Forms.Padding(8);
            this.tabTables.Size = new System.Drawing.Size(1092, 483);
            this.tabTables.TabIndex = 0;
            this.tabTables.Text = "  Tables & Pareto Drivers  ";
            // 
            // pnlTableFilter
            // 
            this.pnlTableFilter.Controls.Add(this.chkParetoOnly);
            this.pnlTableFilter.Controls.Add(this.txtSearchTable);
            this.pnlTableFilter.Controls.Add(this.lblSearch);
            this.pnlTableFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableFilter.Location = new System.Drawing.Point(8, 8);
            this.pnlTableFilter.Name = "pnlTableFilter";
            this.pnlTableFilter.Size = new System.Drawing.Size(1076, 40);
            this.pnlTableFilter.TabIndex = 0;
            // 
            // chkParetoOnly
            // 
            this.chkParetoOnly.AutoSize = true;
            this.chkParetoOnly.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkParetoOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.chkParetoOnly.Location = new System.Drawing.Point(360, 9);
            this.chkParetoOnly.Name = "chkParetoOnly";
            this.chkParetoOnly.Size = new System.Drawing.Size(262, 24);
            this.chkParetoOnly.TabIndex = 2;
            this.chkParetoOnly.Text = "Show Only Top 80% Cost Drivers";
            this.chkParetoOnly.UseVisualStyleBackColor = true;
            this.chkParetoOnly.CheckedChanged += new System.EventHandler(this.chkParetoOnly_CheckedChanged);
            // 
            // txtSearchTable
            // 
            this.txtSearchTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtSearchTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchTable.ForeColor = System.Drawing.Color.White;
            this.txtSearchTable.Location = new System.Drawing.Point(100, 7);
            this.txtSearchTable.Name = "txtSearchTable";
            this.txtSearchTable.Size = new System.Drawing.Size(240, 27);
            this.txtSearchTable.TabIndex = 1;
            this.txtSearchTable.TextChanged += new System.EventHandler(this.txtSearchTable_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSearch.Location = new System.Drawing.Point(3, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Filter Tables :";
            // 
            // dgvTables
            // 
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTables.Location = new System.Drawing.Point(8, 48);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.RowHeadersVisible = false;
            this.dgvTables.RowHeadersWidth = 51;
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(1076, 427);
            this.dgvTables.TabIndex = 1;
            // 
            // tabAttachments
            // 
            this.tabAttachments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabAttachments.Controls.Add(this.dgvAttachments);
            this.tabAttachments.Location = new System.Drawing.Point(4, 29);
            this.tabAttachments.Name = "tabAttachments";
            this.tabAttachments.Padding = new System.Windows.Forms.Padding(8);
            this.tabAttachments.Size = new System.Drawing.Size(1092, 483);
            this.tabAttachments.TabIndex = 1;
            this.tabAttachments.Text = "  Files & Attachments  ";
            // 
            // dgvAttachments
            // 
            this.dgvAttachments.AllowUserToAddRows = false;
            this.dgvAttachments.AllowUserToDeleteRows = false;
            this.dgvAttachments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttachments.Location = new System.Drawing.Point(8, 8);
            this.dgvAttachments.Name = "dgvAttachments";
            this.dgvAttachments.ReadOnly = true;
            this.dgvAttachments.RowHeadersVisible = false;
            this.dgvAttachments.RowHeadersWidth = 51;
            this.dgvAttachments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttachments.Size = new System.Drawing.Size(1076, 467);
            this.dgvAttachments.TabIndex = 0;
            // 
            // tabAudit
            // 
            this.tabAudit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabAudit.Controls.Add(this.dgvAudit);
            this.tabAudit.Controls.Add(this.pnlAuditActions);
            this.tabAudit.Location = new System.Drawing.Point(4, 29);
            this.tabAudit.Name = "tabAudit";
            this.tabAudit.Padding = new System.Windows.Forms.Padding(8);
            this.tabAudit.Size = new System.Drawing.Size(1092, 483);
            this.tabAudit.TabIndex = 2;
            this.tabAudit.Text = "  Audit Partitions  ";
            // 
            // pnlAuditActions
            // 
            this.pnlAuditActions.Controls.Add(this.btnDeleteOldestPartition);
            this.pnlAuditActions.Controls.Add(this.lblAuditNote);
            this.pnlAuditActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAuditActions.Location = new System.Drawing.Point(8, 8);
            this.pnlAuditActions.Name = "pnlAuditActions";
            this.pnlAuditActions.Size = new System.Drawing.Size(1076, 45);
            this.pnlAuditActions.TabIndex = 0;
            // 
            // btnDeleteOldestPartition
            // 
            this.btnDeleteOldestPartition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnDeleteOldestPartition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOldestPartition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteOldestPartition.ForeColor = System.Drawing.Color.White;
            this.btnDeleteOldestPartition.Location = new System.Drawing.Point(3, 5);
            this.btnDeleteOldestPartition.Name = "btnDeleteOldestPartition";
            this.btnDeleteOldestPartition.Size = new System.Drawing.Size(230, 32);
            this.btnDeleteOldestPartition.TabIndex = 0;
            this.btnDeleteOldestPartition.Text = "🗑 Delete Oldest Audit Partition";
            this.btnDeleteOldestPartition.UseVisualStyleBackColor = false;
            this.btnDeleteOldestPartition.Click += new System.EventHandler(this.btnDeleteOldestPartition_Click);
            // 
            // lblAuditNote
            // 
            this.lblAuditNote.AutoSize = true;
            this.lblAuditNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAuditNote.Location = new System.Drawing.Point(245, 11);
            this.lblAuditNote.Name = "lblAuditNote";
            this.lblAuditNote.Size = new System.Drawing.Size(650, 20);
            this.lblAuditNote.TabIndex = 1;
            this.lblAuditNote.Text = "Note: Dataverse audit architecture only allows deleting audit partitions starting" +
    " from the oldest partition.";
            // 
            // dgvAudit
            // 
            this.dgvAudit.AllowUserToAddRows = false;
            this.dgvAudit.AllowUserToDeleteRows = false;
            this.dgvAudit.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.Location = new System.Drawing.Point(8, 53);
            this.dgvAudit.Name = "dgvAudit";
            this.dgvAudit.ReadOnly = true;
            this.dgvAudit.RowHeadersVisible = false;
            this.dgvAudit.RowHeadersWidth = 51;
            this.dgvAudit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAudit.Size = new System.Drawing.Size(1076, 422);
            this.dgvAudit.TabIndex = 1;
            // 
            // tabAsync
            // 
            this.tabAsync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabAsync.Controls.Add(this.dgvAsync);
            this.tabAsync.Location = new System.Drawing.Point(4, 29);
            this.tabAsync.Name = "tabAsync";
            this.tabAsync.Padding = new System.Windows.Forms.Padding(8);
            this.tabAsync.Size = new System.Drawing.Size(1092, 483);
            this.tabAsync.TabIndex = 3;
            this.tabAsync.Text = "  System Jobs (AsyncOperationBase)  ";
            // 
            // dgvAsync
            // 
            this.dgvAsync.AllowUserToAddRows = false;
            this.dgvAsync.AllowUserToDeleteRows = false;
            this.dgvAsync.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvAsync.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAsync.Location = new System.Drawing.Point(8, 8);
            this.dgvAsync.Name = "dgvAsync";
            this.dgvAsync.ReadOnly = true;
            this.dgvAsync.RowHeadersVisible = false;
            this.dgvAsync.RowHeadersWidth = 51;
            this.dgvAsync.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsync.Size = new System.Drawing.Size(1076, 467);
            this.dgvAsync.TabIndex = 0;
            // 
            // tabPurge
            // 
            this.tabPurge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabPurge.Controls.Add(this.pnlPurgeContainer);
            this.tabPurge.Location = new System.Drawing.Point(4, 29);
            this.tabPurge.Name = "tabPurge";
            this.tabPurge.Padding = new System.Windows.Forms.Padding(12);
            this.tabPurge.Size = new System.Drawing.Size(1092, 483);
            this.tabPurge.TabIndex = 4;
            this.tabPurge.Text = "  🧹 Purge & Archival Studio  ";
            // 
            // pnlPurgeContainer
            // 
            this.pnlPurgeContainer.Controls.Add(this.btnExecutePurge);
            this.pnlPurgeContainer.Controls.Add(this.txtPurgeLog);
            this.pnlPurgeContainer.Controls.Add(this.chkDryRun);
            this.pnlPurgeContainer.Controls.Add(this.chkArchiveBeforePurge);
            this.pnlPurgeContainer.Controls.Add(this.btnBrowseArchive);
            this.pnlPurgeContainer.Controls.Add(this.txtArchivePath);
            this.pnlPurgeContainer.Controls.Add(this.lblArchivePath);
            this.pnlPurgeContainer.Controls.Add(this.grpPurgeRules);
            this.pnlPurgeContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPurgeContainer.Location = new System.Drawing.Point(12, 12);
            this.pnlPurgeContainer.Name = "pnlPurgeContainer";
            this.pnlPurgeContainer.Size = new System.Drawing.Size(1068, 459);
            this.pnlPurgeContainer.TabIndex = 0;
            // 
            // btnExecutePurge
            // 
            this.btnExecutePurge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnExecutePurge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecutePurge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExecutePurge.ForeColor = System.Drawing.Color.White;
            this.btnExecutePurge.Location = new System.Drawing.Point(12, 280);
            this.btnExecutePurge.Name = "btnExecutePurge";
            this.btnExecutePurge.Size = new System.Drawing.Size(280, 42);
            this.btnExecutePurge.TabIndex = 7;
            this.btnExecutePurge.Text = "🚀  Execute Purge / Dry Run";
            this.btnExecutePurge.UseVisualStyleBackColor = false;
            this.btnExecutePurge.Click += new System.EventHandler(this.btnExecutePurge_Click);
            // 
            // txtPurgeLog
            // 
            this.txtPurgeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurgeLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtPurgeLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurgeLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtPurgeLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtPurgeLog.Location = new System.Drawing.Point(310, 15);
            this.txtPurgeLog.Multiline = true;
            this.txtPurgeLog.Name = "txtPurgeLog";
            this.txtPurgeLog.ReadOnly = true;
            this.txtPurgeLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPurgeLog.Size = new System.Drawing.Size(746, 428);
            this.txtPurgeLog.TabIndex = 6;
            // 
            // chkDryRun
            // 
            this.chkDryRun.AutoSize = true;
            this.chkDryRun.Checked = true;
            this.chkDryRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDryRun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDryRun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.chkDryRun.Location = new System.Drawing.Point(12, 240);
            this.chkDryRun.Name = "chkDryRun";
            this.chkDryRun.Size = new System.Drawing.Size(286, 24);
            this.chkDryRun.TabIndex = 5;
            this.chkDryRun.Text = "Dry Run Mode (Simulate, Do Not Delete)";
            this.chkDryRun.UseVisualStyleBackColor = true;
            // 
            // chkArchiveBeforePurge
            // 
            this.chkArchiveBeforePurge.AutoSize = true;
            this.chkArchiveBeforePurge.Checked = true;
            this.chkArchiveBeforePurge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkArchiveBeforePurge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkArchiveBeforePurge.Location = new System.Drawing.Point(12, 210);
            this.chkArchiveBeforePurge.Name = "chkArchiveBeforePurge";
            this.chkArchiveBeforePurge.Size = new System.Drawing.Size(257, 24);
            this.chkArchiveBeforePurge.TabIndex = 4;
            this.chkArchiveBeforePurge.Text = "Archive Attachments before Purge";
            this.chkArchiveBeforePurge.UseVisualStyleBackColor = true;
            // 
            // btnBrowseArchive
            // 
            this.btnBrowseArchive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBrowseArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseArchive.ForeColor = System.Drawing.Color.White;
            this.btnBrowseArchive.Location = new System.Drawing.Point(245, 172);
            this.btnBrowseArchive.Name = "btnBrowseArchive";
            this.btnBrowseArchive.Size = new System.Drawing.Size(50, 27);
            this.btnBrowseArchive.TabIndex = 3;
            this.btnBrowseArchive.Text = "...";
            this.btnBrowseArchive.UseVisualStyleBackColor = false;
            this.btnBrowseArchive.Click += new System.EventHandler(this.btnBrowseArchive_Click);
            // 
            // txtArchivePath
            // 
            this.txtArchivePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtArchivePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArchivePath.ForeColor = System.Drawing.Color.White;
            this.txtArchivePath.Location = new System.Drawing.Point(12, 172);
            this.txtArchivePath.Name = "txtArchivePath";
            this.txtArchivePath.Size = new System.Drawing.Size(225, 27);
            this.txtArchivePath.TabIndex = 2;
            // 
            // lblArchivePath
            // 
            this.lblArchivePath.AutoSize = true;
            this.lblArchivePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblArchivePath.Location = new System.Drawing.Point(8, 149);
            this.lblArchivePath.Name = "lblArchivePath";
            this.lblArchivePath.Size = new System.Drawing.Size(184, 20);
            this.lblArchivePath.TabIndex = 1;
            this.lblArchivePath.Text = "Archive Destination Path :";
            // 
            // grpPurgeRules
            // 
            this.grpPurgeRules.Controls.Add(this.chkPurgeCanceledAsync);
            this.grpPurgeRules.Controls.Add(this.chkPurgeFailedAsync);
            this.grpPurgeRules.Controls.Add(this.chkPurgeCompletedWorkflows);
            this.grpPurgeRules.Controls.Add(this.chkPurgeNotes);
            this.grpPurgeRules.Controls.Add(this.chkPurgeEmailAttachments);
            this.grpPurgeRules.Controls.Add(this.numOlderThanDays);
            this.grpPurgeRules.Controls.Add(this.lblDays);
            this.grpPurgeRules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.grpPurgeRules.Location = new System.Drawing.Point(8, 8);
            this.grpPurgeRules.Name = "grpPurgeRules";
            this.grpPurgeRules.Size = new System.Drawing.Size(287, 135);
            this.grpPurgeRules.TabIndex = 0;
            this.grpPurgeRules.TabStop = false;
            this.grpPurgeRules.Text = "Purge Criteria";
            // 
            // chkPurgeCanceledAsync
            // 
            this.chkPurgeCanceledAsync.AutoSize = true;
            this.chkPurgeCanceledAsync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkPurgeCanceledAsync.Location = new System.Drawing.Point(145, 102);
            this.chkPurgeCanceledAsync.Name = "chkPurgeCanceledAsync";
            this.chkPurgeCanceledAsync.Size = new System.Drawing.Size(131, 24);
            this.chkPurgeCanceledAsync.TabIndex = 6;
            this.chkPurgeCanceledAsync.Text = "Canceled Jobs";
            this.chkPurgeCanceledAsync.UseVisualStyleBackColor = true;
            // 
            // chkPurgeFailedAsync
            // 
            this.chkPurgeFailedAsync.AutoSize = true;
            this.chkPurgeFailedAsync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkPurgeFailedAsync.Location = new System.Drawing.Point(145, 76);
            this.chkPurgeFailedAsync.Name = "chkPurgeFailedAsync";
            this.chkPurgeFailedAsync.Size = new System.Drawing.Size(109, 24);
            this.chkPurgeFailedAsync.TabIndex = 5;
            this.chkPurgeFailedAsync.Text = "Failed Jobs";
            this.chkPurgeFailedAsync.UseVisualStyleBackColor = true;
            // 
            // chkPurgeCompletedWorkflows
            // 
            this.chkPurgeCompletedWorkflows.AutoSize = true;
            this.chkPurgeCompletedWorkflows.Checked = true;
            this.chkPurgeCompletedWorkflows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPurgeCompletedWorkflows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkPurgeCompletedWorkflows.Location = new System.Drawing.Point(145, 50);
            this.chkPurgeCompletedWorkflows.Name = "chkPurgeCompletedWorkflows";
            this.chkPurgeCompletedWorkflows.Size = new System.Drawing.Size(133, 24);
            this.chkPurgeCompletedWorkflows.TabIndex = 4;
            this.chkPurgeCompletedWorkflows.Text = "Succeeded WFs";
            this.chkPurgeCompletedWorkflows.UseVisualStyleBackColor = true;
            // 
            // chkPurgeNotes
            // 
            this.chkPurgeNotes.AutoSize = true;
            this.chkPurgeNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkPurgeNotes.Location = new System.Drawing.Point(10, 80);
            this.chkPurgeNotes.Name = "chkPurgeNotes";
            this.chkPurgeNotes.Size = new System.Drawing.Size(117, 24);
            this.chkPurgeNotes.TabIndex = 3;
            this.chkPurgeNotes.Text = "Note Attach.";
            this.chkPurgeNotes.UseVisualStyleBackColor = true;
            // 
            // chkPurgeEmailAttachments
            // 
            this.chkPurgeEmailAttachments.AutoSize = true;
            this.chkPurgeEmailAttachments.Checked = true;
            this.chkPurgeEmailAttachments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPurgeEmailAttachments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkPurgeEmailAttachments.Location = new System.Drawing.Point(10, 54);
            this.chkPurgeEmailAttachments.Name = "chkPurgeEmailAttachments";
            this.chkPurgeEmailAttachments.Size = new System.Drawing.Size(120, 24);
            this.chkPurgeEmailAttachments.TabIndex = 2;
            this.chkPurgeEmailAttachments.Text = "Email Attach.";
            this.chkPurgeEmailAttachments.UseVisualStyleBackColor = true;
            // 
            // numOlderThanDays
            // 
            this.numOlderThanDays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.numOlderThanDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numOlderThanDays.ForeColor = System.Drawing.Color.White;
            this.numOlderThanDays.Location = new System.Drawing.Point(150, 20);
            this.numOlderThanDays.Maximum = new decimal(new int[] {
            3650,
            0,
            0,
            0});
            this.numOlderThanDays.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numOlderThanDays.Name = "numOlderThanDays";
            this.numOlderThanDays.Size = new System.Drawing.Size(75, 27);
            this.numOlderThanDays.TabIndex = 1;
            this.numOlderThanDays.Value = new decimal(new int[] {
            365,
            0,
            0,
            0});
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDays.Location = new System.Drawing.Point(6, 23);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(131, 20);
            this.lblDays.TabIndex = 0;
            this.lblDays.Text = "Older Than (Days):";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1100, 26);
            this.statusStrip1.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(943, 20);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready. Connect to Dataverse and click \'Run Storage Audit\' to begin.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(140, 18);
            this.progressBar.Visible = false;
            // 
            // StorageReducerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "StorageReducerControl";
            this.Size = new System.Drawing.Size(1100, 676);
            this.Load += new System.EventHandler(this.StorageReducerControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlCards.ResumeLayout(false);
            this.pnlCardSavings.ResumeLayout(false);
            this.pnlCardSavings.PerformLayout();
            this.pnlCardBill.ResumeLayout(false);
            this.pnlCardBill.PerformLayout();
            this.pnlCardDrivers.ResumeLayout(false);
            this.pnlCardDrivers.PerformLayout();
            this.pnlCardStorage.ResumeLayout(false);
            this.pnlCardStorage.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabTables.ResumeLayout(false);
            this.pnlTableFilter.ResumeLayout(false);
            this.pnlTableFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.tabAttachments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachments)).EndInit();
            this.tabAudit.ResumeLayout(false);
            this.pnlAuditActions.ResumeLayout(false);
            this.pnlAuditActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.tabAsync.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsync)).EndInit();
            this.tabPurge.ResumeLayout(false);
            this.pnlPurgeContainer.ResumeLayout(false);
            this.pnlPurgeContainer.PerformLayout();
            this.grpPurgeRules.ResumeLayout(false);
            this.grpPurgeRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOlderThanDays)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnScan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportHtml;
        private System.Windows.Forms.ToolStripButton btnExportCsv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPurgeWizard;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel pnlCardStorage;
        private System.Windows.Forms.Label lblCardStorageVal;
        private System.Windows.Forms.Label lblCardStorageTitle;
        private System.Windows.Forms.Panel pnlCardBill;
        private System.Windows.Forms.Label lblCardBillVal;
        private System.Windows.Forms.Label lblCardBillTitle;
        private System.Windows.Forms.Panel pnlCardSavings;
        private System.Windows.Forms.Label lblCardSavingsVal;
        private System.Windows.Forms.Label lblCardSavingsTitle;
        private System.Windows.Forms.Panel pnlCardDrivers;
        private System.Windows.Forms.Label lblCardDriversVal;
        private System.Windows.Forms.Label lblCardDriversTitle;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabTables;
        private System.Windows.Forms.Panel pnlTableFilter;
        private System.Windows.Forms.TextBox txtSearchTable;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.CheckBox chkParetoOnly;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.TabPage tabAttachments;
        private System.Windows.Forms.DataGridView dgvAttachments;
        private System.Windows.Forms.TabPage tabAudit;
        private System.Windows.Forms.Panel pnlAuditActions;
        private System.Windows.Forms.Button btnDeleteOldestPartition;
        private System.Windows.Forms.Label lblAuditNote;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.TabPage tabAsync;
        private System.Windows.Forms.DataGridView dgvAsync;
        private System.Windows.Forms.TabPage tabPurge;
        private System.Windows.Forms.Panel pnlPurgeContainer;
        private System.Windows.Forms.GroupBox grpPurgeRules;
        private System.Windows.Forms.NumericUpDown numOlderThanDays;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.CheckBox chkPurgeEmailAttachments;
        private System.Windows.Forms.CheckBox chkPurgeNotes;
        private System.Windows.Forms.CheckBox chkPurgeCompletedWorkflows;
        private System.Windows.Forms.CheckBox chkPurgeFailedAsync;
        private System.Windows.Forms.CheckBox chkPurgeCanceledAsync;
        private System.Windows.Forms.Label lblArchivePath;
        private System.Windows.Forms.TextBox txtArchivePath;
        private System.Windows.Forms.Button btnBrowseArchive;
        private System.Windows.Forms.CheckBox chkArchiveBeforePurge;
        private System.Windows.Forms.CheckBox chkDryRun;
        private System.Windows.Forms.Button btnExecutePurge;
        private System.Windows.Forms.TextBox txtPurgeLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}
