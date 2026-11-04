using System;
using System.Collections.Generic;
using System.Linq;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Helpers
{
    public static class CostCalculator
    {
        public static void CalculateCostsAndDrivers(
            CostAnalysisResult result,
            decimal dbRatePerGb = 40m,
            decimal fileRatePerGb = 10m,
            decimal logRatePerGb = 10m)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            result.DatabasePricePerGb = dbRatePerGb;
            result.FilePricePerGb = fileRatePerGb;
            result.LogPricePerGb = logRatePerGb;

            // 1. Calculate each table's monthly cost based on its storage category
            foreach (var table in result.Tables)
            {
                decimal rate = table.Category switch
                {
                    StorageCategory.Database => dbRatePerGb,
                    StorageCategory.File => fileRatePerGb,
                    StorageCategory.Log => logRatePerGb,
                    _ => dbRatePerGb
                };

                table.MonthlyCostUsd = Math.Round((decimal)table.EstimatedTotalGB * rate, 2);
            }

            // 2. Compute category sums
            result.DatabaseTotalGB = result.Tables
                .Where(t => t.Category == StorageCategory.Database)
                .Sum(t => t.EstimatedTotalGB);

            result.FileTotalGB = result.Tables
                .Where(t => t.Category == StorageCategory.File)
                .Sum(t => t.EstimatedTotalGB);

            result.LogTotalGB = result.Tables
                .Where(t => t.Category == StorageCategory.Log)
                .Sum(t => t.EstimatedTotalGB);

            double overallGb = result.OverallTotalGB;

            // 3. Percentage of total storage for each table
            if (overallGb > 0)
            {
                foreach (var table in result.Tables)
                {
                    table.PercentageOfTotalStorage = Math.Round((table.EstimatedTotalGB / overallGb) * 100.0, 2);
                }
            }

            // 4. Pareto 80/20 Principle: Sort by Monthly Cost Descending
            var sortedTables = result.Tables.OrderByDescending(t => t.MonthlyCostUsd).ToList();
            decimal totalMonthlyCost = sortedTables.Sum(t => t.MonthlyCostUsd);
            decimal runningCost = 0m;

            result.Top80PercentDrivers.Clear();

            foreach (var table in sortedTables)
            {
                if (totalMonthlyCost > 0 && (runningCost / totalMonthlyCost) < 0.80m)
                {
                    table.IsTop80PercentDriver = true;
                    result.Top80PercentDrivers.Add(table);
                    runningCost += table.MonthlyCostUsd;
                }
                else
                {
                    table.IsTop80PercentDriver = false;
                }
            }

            // If no table hit 80% (e.g. only 1 or 2 small tables), include at least the top one
            if (result.Top80PercentDrivers.Count == 0 && sortedTables.Count > 0)
            {
                sortedTables[0].IsTop80PercentDriver = true;
                result.Top80PercentDrivers.Add(sortedTables[0]);
            }
        }
    }
}
