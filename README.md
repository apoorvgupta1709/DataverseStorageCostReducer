# 💰 Dataverse Storage & Cost Reducer (XrmToolBox Plugin)

> **Cut your Microsoft Dataverse cloud bill by 20% to 60%** by auditing storage bloat, identifying top cost drivers, and safely offloading or purging legacy attachments, audit partitions, and system jobs.

---

## 🎯 The Problem
Microsoft Dataverse database capacity is one of the most expensive enterprise storage tiers in the cloud:
* **Database Storage:** ~$40 / GB / month
* **File Storage:** ~$10 / GB / month
* **Audit & Log Storage:** ~$10 / GB / month

Enterprises routinely receive surprise overage invoices of **$2,000 to $20,000+ per month** due to:
1. **Unchecked Email & Note Attachments (`activitymimeattachment`, `annotation`)**: Decades of huge PDFs, scan files, and zip attachments that no one accesses.
2. **Audit Log Bloat (`audit`)**: Unmanaged audit partitions spanning years with millions of field modification records.
3. **AsyncOperationBase Table Bloat (`asyncoperation`)**: Millions of succeeded or canceled workflow instances that had *"Delete completed workflow jobs"* disabled.
4. **Pareto 80/20 Rule**: In almost every organization, **20% of the tables generate 80% of the entire storage bill**.

---

## 🚀 Key Features

### 1. 📊 Executive Storage & Cost Dashboard
- Real-time estimation of **Monthly Run Rate ($)** and **Annual Run Rate ($)**.
- Category breakdown: **Database**, **File**, and **Log** tiers.
- **Potential Monthly & Annual Savings ($)** calculator.

### 2. 🎯 Pareto 80/20 Cost Driver Hunter
- Automatically sorts and flags the exact tables driving 80% of your Microsoft storage invoice.
- Interactive filter to isolate top drivers with one click.
- Full search and sorting across record counts, total MB/GB, and percentage of overall spend.

### 3. 📎 Email & Note Attachment Profiler
- Groups attachments by file extension (`.pdf`, `.zip`, `.png`, `.docx`, etc.).
- Calculates total byte volume, counts, and monthly run-rate per file type.
- Identifies large attachments (>1MB, >5MB, >10MB) and orphaned attachments.

### 4. 📜 Audit Partition Manager
- Calls native Dataverse `RetrieveAuditPartitionListRequest`.
- Visualizes start dates, end dates, and partition sizes.
- Supports safe **One-Click Deletion of Oldest Audit Partition** via `DeleteAuditPartitionRequest`.

### 5. ⚙️ System Job (AsyncOperation) Optimizer
- Aggregates system jobs by operation type (Workflows, System Events, Bulk Delete) and status (Succeeded, Failed, Canceled).
- Highlights purgeable completed workflows that are needlessly consuming database storage.

### 6. 🛡️ Safe Archival & Purge Studio
- **Dry Run Simulation Mode:** Test-run your purge criteria to see exactly how many records and bytes will be freed before committing any changes.
- **Local / Azure Archival:** Automatically downloads full attachment binary payloads and saves them to disk/archive before deletion.
- **Throttled Batch Deletion:** Deletes records in configurable batches (via `ExecuteMultipleRequest`) to prevent timeouts or service protection throttling.

### 7. 📄 One-Click Reporting
- **Executive HTML Audit Report:** Polished dark-themed report ready to present directly to IT Directors and CIOs.
- **CSV Data Export:** Full tabular export for Excel financial analysis and compliance documentation.

---

## 🏗️ Project Architecture

```
DataverseStorageCostReducer/
├── DataverseStorageReducer.sln
├── README.md
├── docs/
│   └── ARCHITECTURE.md
└── src/
    └── DataverseStorageCostReducer/
        ├── DataverseStorageCostReducer.csproj
        ├── PluginInfo.cs                           # XrmToolBox MEF registration
        ├── Properties/
        │   └── AssemblyInfo.cs
        ├── AppCode/
        │   ├── Models/
        │   │   ├── StorageCategory.cs              # Storage tier enums
        │   │   ├── TableStorageItem.cs             # Table metrics & Pareto flags
        │   │   ├── AttachmentItem.cs               # File & attachment model
        │   │   ├── AuditLogSummary.cs              # Audit partitions model
        │   │   ├── AsyncJobSummary.cs              # System jobs bloat model
        │   │   ├── CostAnalysisResult.cs           # Audit aggregate result
        │   │   └── PurgeConfig.cs & PurgeResult.cs # Purge configuration & outputs
        │   ├── Helpers/
        │   │   ├── CostCalculator.cs               # 80/20 Pareto & pricing engine
        │   │   └── CsvExporter.cs                  # Executive HTML & CSV reports
        │   └── Services/
        │       ├── DataverseMetadataService.cs     # Metadata queries
        │       ├── AttachmentAnalyzer.cs           # Attachment distribution engine
        │       ├── AuditLogAnalyzer.cs             # Audit partition manager
        │       ├── AsyncJobAnalyzer.cs             # AsyncOperationBase analyzer
        │       ├── ArchivalPurgeEngine.cs          # Archiving & safe batch purge
        │       └── StorageAnalysisEngine.cs        # Orchestration engine
        └── UI/
            ├── StorageReducerControl.cs            # Main WinForms plugin controller
            └── StorageReducerControl.Designer.cs   # Modern UI layout
```

---

## 🛠️ How to Build and Deploy to XrmToolBox

### Prerequisites
* Windows 10/11
* .NET Framework 4.8 Developer Pack or .NET SDK
* Visual Studio 2019/2022 or `msbuild` / `dotnet build`
* [XrmToolBox](https://www.xrmtoolbox.com/) installed

### Build Steps
1. Clone or open the folder `DataverseStorageCostReducer`.
2. Open `DataverseStorageReducer.sln` in Visual Studio (or run `dotnet build -c Release`).
3. Build the solution. The output DLL `DataverseStorageCostReducer.dll` will be generated in `bin\Release\`.
4. Copy the compiled assembly and dependencies into your XrmToolBox `Plugins` folder:
   ```
   %AppData%\MscrmTools\XrmToolBox\Plugins\
   ```
5. Launch XrmToolBox, connect to your Dataverse environment, and search for **"Dataverse Storage & Cost Reducer"**.

---

## 💼 Enterprise ROI & Business Case

| Metric | Typical Enterprise Environment | Estimated Savings |
|---|---|---|
| Current Storage Usage | 150 GB Database ($6,000/mo) | — |
| Unmanaged System Jobs | 35 GB of succeeded workflows | **Save $1,400 / month** |
| Legacy Attachments (>3 yrs old) | 50 GB archived to Blob/disk | **Save $500 / month** |
| Old Audit Partitions | 20 GB of expired audit logs | **Save $200 / month** |
| **Total Monthly Savings** | — | **$2,100 / month ($25,200 / year)** |
| **Tool Investment** | $299 – $499 one-time Pro license | **1-day ROI for IT Director** |
