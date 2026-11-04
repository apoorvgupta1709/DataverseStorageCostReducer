using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Services
{
    public class DataverseMetadataService
    {
        private readonly IOrganizationService _service;

        public DataverseMetadataService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<TableStorageItem> RetrieveAllTablesMetadata(Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke("Retrieving entity definitions from Dataverse metadata...");

            var request = new RetrieveAllEntitiesRequest
            {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };

            var response = (RetrieveAllEntitiesResponse)_service.Execute(request);
            var results = new List<TableStorageItem>();

            foreach (var md in response.EntityMetadata)
            {
                // Exclude virtual entities or unqueryable entities if applicable
                if (md.IsPrivate == true) continue;

                var item = new TableStorageItem
                {
                    LogicalName = md.LogicalName,
                    DisplayName = md.DisplayName?.UserLocalizedLabel?.Label 
                                  ?? md.SchemaName 
                                  ?? md.LogicalName,
                    IsCustomEntity = md.IsCustomEntity == true,
                    PrimaryIdAttribute = md.PrimaryIdAttribute ?? string.Empty,
                    PrimaryNameAttribute = md.PrimaryNameAttribute ?? string.Empty,
                    Category = DetermineStorageCategory(md.LogicalName),
                    EstimatedAvgRowSizeBytes = EstimateAverageRowSizeBytes(md.LogicalName)
                };

                results.Add(item);
            }

            return results.OrderBy(r => r.DisplayName).ToList();
        }

        private static StorageCategory DetermineStorageCategory(string logicalName)
        {
            if (string.Equals(logicalName, "activitymimeattachment", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(logicalName, "annotation", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(logicalName, "webresource", StringComparison.OrdinalIgnoreCase))
            {
                return StorageCategory.File;
            }

            if (string.Equals(logicalName, "audit", StringComparison.OrdinalIgnoreCase))
            {
                return StorageCategory.Log;
            }

            return StorageCategory.Database;
        }

        private static double EstimateAverageRowSizeBytes(string logicalName)
        {
            // Typical baseline averages in Dataverse relational storage including index overhead
            return logicalName.ToLowerInvariant() switch
            {
                "asyncoperation" => 2048.0,      // System jobs have execution data & error payloads (~2 KB)
                "audit" => 512.0,                // Audit entries are relatively lightweight (~512 bytes)
                "email" => 4096.0,               // Email description (HTML body) (~4 KB)
                "contact" => 1500.0,             // Standard business entity with ~100 fields (~1.5 KB)
                "account" => 2000.0,             // Core customer record (~2 KB)
                "opportunity" => 1800.0,
                "incident" => 2500.0,            // Case tickets (~2.5 KB)
                "activitypointer" => 1200.0,
                _ => 1200.0                      // General baseline table row estimate
            };
        }
    }
}
