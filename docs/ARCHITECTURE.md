# Architecture & Technical Deep Dive: Dataverse Storage & Cost Reducer

## 1. Storage Consumption & Cost Formulas

Dataverse storage consumption is divided into three distinct capacity types, each with its own cloud pricing model:

$$\text{Monthly Cost} = (\text{DB GB} \times \$40) + (\text{File GB} \times \$10) + (\text{Log GB} \times \$10)$$

$$\text{Annual Run Rate} = \text{Monthly Cost} \times 12$$

### Pareto 80/20 Driver Algorithm
1. Retrieve all table storage records.
2. Calculate individual monthly costs:
   $$\text{Cost}_i = \text{GB}_i \times \text{Rate}_{\text{Category}_i}$$
3. Sort tables in descending order by $\text{Cost}_i$.
4. Calculate running sum of monthly costs:
   $$\text{CumulativeCost}_k = \sum_{i=1}^k \text{Cost}_i$$
5. Any table where $\frac{\text{CumulativeCost}_k}{\text{TotalCost}} \le 0.80$ is tagged with `IsTop80PercentDriver = true`.

---

## 2. Dataverse SDK Messages & APIs Used

| Area | SDK Message / Query | Description |
|---|---|---|
| **Metadata** | `RetrieveAllEntitiesRequest` | Extracts all entity metadata (`EntityFilters.Entity`), display names, primary keys, and flags |
| **Row Counts** | `FetchExpression` (FetchXML Aggregate) | Performs aggregate row counts using `<attribute aggregate='count' alias='total' />` |
| **Audit Logs** | `RetrieveAuditPartitionListRequest` | Retrieves all audit partitions, start dates, end dates, and size in bytes |
| **Audit Deletion** | `DeleteAuditPartitionRequest` | Deletes the oldest audit partition up to specified `EndDate` |
| **Attachments** | `FetchExpression` (`activitymimeattachment`, `annotation`) | Samples file extensions, mime types, and file sizes in bytes |
| **System Jobs** | `FetchExpression` (GroupBy Aggregate) | Aggregates `asyncoperation` by `operationtype` and `statuscode` |
| **Batch Purge** | `ExecuteMultipleRequest` | Packages up to 100 `DeleteRequest` items in atomic/fault-tolerant batches with `ContinueOnError = true` |

---

## 3. Safety & Resilience Guarantees

1. **Dry-Run Validation First:**
   * Before executing any deletions, the tool validates record counts, computes exact byte reductions, and previews the result without touching live Dataverse records.
2. **Mandatory Archiving Capability:**
   * Downloads raw Base64 payload (`body` in `activitymimeattachment`, `documentbody` in `annotation`) to local files or zipped storage before issuing `DeleteRequest`.
3. **Oldest-Partition Enforcement for Audit Logs:**
   * Dataverse architecture requires audit partitions to be deleted sequentially. The tool prevents arbitrary partition deletion and only allows deleting the oldest expired partition.
4. **Service Protection Limit Awareness:**
   * Uses throttled batches (default 100 records per `ExecuteMultipleRequest`) to avoid triggering HTTP 429 throttling limits.
5. **Non-blocking UI Threading:**
   * All long-running operations execute asynchronously via XrmToolBox's `WorkAsync(WorkAsyncInfo)` worker thread with cancellation support.
