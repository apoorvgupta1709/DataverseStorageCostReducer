using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class AsyncJobAnalyzer
    {
        private readonly IOrganizationService _service;

        public AsyncJobAnalyzer(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public (AsyncJobSummary Summary, List<AsyncJobBreakdown> Breakdowns) AnalyzeAsyncJobs(Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke("Analyzing System Jobs (AsyncOperationBase) bloat...");

            var breakdowns = new List<AsyncJobBreakdown>();
            var summary = new AsyncJobSummary();

            // FetchXML to aggregate async operations by operationtype and statuscode
            string fetch = @"
                <fetch aggregate='true'>
                  <entity name='asyncoperation'>
                    <attribute name='asyncoperationid' aggregate='count' alias='JobCount' />
                    <attribute name='operationtype' groupby='true' alias='OpType' />
                    <attribute name='statuscode' groupby='true' alias='StatusCode' />
                  </entity>
                </fetch>";

            try
            {
                var response = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var entity in response.Entities)
                {
                    var countVal = (AliasedValue)entity["JobCount"];
                    var opTypeVal = (AliasedValue)entity["OpType"];
                    var statusVal = (AliasedValue)entity["StatusCode"];

                    int count = Convert.ToInt32(countVal.Value);
                    int opTypeInt = opTypeVal?.Value is OptionSetValue osv ? osv.Value : 0;
                    int statusInt = statusVal?.Value is OptionSetValue ssv ? ssv.Value : 0;

                    string opTypeName = GetOperationTypeName(opTypeInt);
                    string statusName = GetStatusName(statusInt);

                    // Estimate ~2.5 KB per system job record
                    double estSizeMb = (count * 2500.0) / (1024.0 * 1024.0);
                    decimal monthlyCost = Math.Round((decimal)(estSizeMb / 1024.0) * 40m, 2);

                    bool isPurgeable = statusInt == (int)AsyncJobStatusCode.Succeeded ||
                                       statusInt == (int)AsyncJobStatusCode.Canceled ||
                                       statusInt == (int)AsyncJobStatusCode.Failed;

                    breakdowns.Add(new AsyncJobBreakdown
                    {
                        OperationType = opTypeName,
                        Status = statusName,
                        Count = count,
                        EstimatedSizeMB = estSizeMb,
                        EstimatedMonthlyCostUsd = monthlyCost,
                        IsRecommendedForPurge = isPurgeable
                    });

                    summary.TotalJobsCount += count;
                    switch (statusInt)
                    {
                        case 30: // Succeeded
                            summary.SucceededJobsCount += count;
                            break;
                        case 31: // Failed
                            summary.FailedJobsCount += count;
                            break;
                        case 32: // Canceled
                            summary.CanceledJobsCount += count;
                            break;
                        default:
                            summary.WaitingOrInProgressCount += count;
                            break;
                    }
                }

                summary.TotalEstimatedSizeMB = (summary.TotalJobsCount * 2500.0) / (1024.0 * 1024.0);
                summary.TotalMonthlyCostUsd = Math.Round((decimal)summary.TotalEstimatedSizeGB * 40m, 2);

                long purgeableCount = summary.SucceededJobsCount + summary.FailedJobsCount + summary.CanceledJobsCount;
                double purgeableMb = (purgeableCount * 2500.0) / (1024.0 * 1024.0);
                summary.PotentialMonthlySavingsUsd = Math.Round((decimal)(purgeableMb / 1024.0) * 40m, 2);
            }
            catch (Exception ex)
            {
                statusCallback?.Invoke($"Could not aggregate async operations: {ex.Message}");
            }

            return (summary, breakdowns.OrderByDescending(b => b.Count).ToList());
        }

        private static string GetOperationTypeName(int opType)
        {
            return opType switch
            {
                1 => "System Event",
                10 => "Workflow Execution",
                14 => "Bulk Delete Operation",
                21 => "Import File Data",
                43 => "Rollup Field Calculation",
                90 => "Activity Feeds",
                _ => $"Operation ({opType})"
            };
        }

        private static string GetStatusName(int statusCode)
        {
            return statusCode switch
            {
                0 => "Waiting For Resources",
                10 => "Waiting",
                20 => "In Progress",
                21 => "Pausing",
                22 => "Canceling",
                30 => "Succeeded",
                31 => "Failed",
                32 => "Canceled",
                _ => $"Status ({statusCode})"
            };
        }
    }
}
