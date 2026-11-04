using System;
using System.IO;
using System.Linq;
using System.Text;
using DataverseStorageCostReducer.AppCode.Models;

namespace DataverseStorageCostReducer.AppCode.Helpers
{
    public static class CsvExporter
    {
        public static string ExportTableBreakdownToCsv(CostAnalysisResult result, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("DisplayName,LogicalName,Category,RecordCount,EstimatedTotalMB,EstimatedTotalGB,MonthlyCostUSD,AnnualCostUSD,PercentOfTotal,IsTop80PercentDriver,IsCustom");

            foreach (var table in result.Tables.OrderByDescending(t => t.MonthlyCostUsd))
            {
                sb.AppendLine($"\"{EscapeCsv(table.DisplayName)}\"," +
                              $"\"{EscapeCsv(table.LogicalName)}\"," +
                              $"{table.Category}," +
                              $"{table.RecordCount}," +
                              $"{table.EstimatedTotalMB:F2}," +
                              $"{table.EstimatedTotalGB:F3}," +
                              $"{table.MonthlyCostUsd:F2}," +
                              $"{table.AnnualCostUsd:F2}," +
                              $"{table.PercentageOfTotalStorage:F2}%," +
                              $"{(table.IsTop80PercentDriver ? "YES" : "NO")}," +
                              $"{(table.IsCustomEntity ? "YES" : "NO")}");
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            return filePath;
        }

        public static string ExportExecutiveHtmlReport(CostAnalysisResult result, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='en'>");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset='utf-8'>");
            sb.AppendLine("  <title>Dataverse Storage & Cost Audit Executive Report</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background: #0f172a; color: #f8fafc; margin: 0; padding: 24px; }");
            sb.AppendLine("    .container { max-width: 1200px; margin: 0 auto; }");
            sb.AppendLine("    .header { margin-bottom: 24px; border-bottom: 1px solid #334155; padding-bottom: 16px; }");
            sb.AppendLine("    h1 { margin: 0 0 8px; font-size: 26px; color: #38bdf8; }");
            sb.AppendLine("    .meta { color: #94a3b8; font-size: 14px; }");
            sb.AppendLine("    .cards { display: grid; grid-template-columns: repeat(auto-fit, minmax(220px, 1fr)); gap: 16px; margin-bottom: 28px; }");
            sb.AppendLine("    .card { background: #1e293b; border: 1px solid #334155; border-radius: 8px; padding: 18px; }");
            sb.AppendLine("    .card-title { font-size: 12px; text-transform: uppercase; color: #94a3b8; margin-bottom: 6px; font-weight: 600; }");
            sb.AppendLine("    .card-value { font-size: 24px; font-weight: 700; color: #f1f5f9; }");
            sb.AppendLine("    .card-value.highlight { color: #34d399; }");
            sb.AppendLine("    .card-value.warn { color: #f87171; }");
            sb.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 16px; background: #1e293b; border-radius: 8px; overflow: hidden; }");
            sb.AppendLine("    th { background: #334155; text-align: left; padding: 12px; font-size: 13px; color: #e2e8f0; font-weight: 600; }");
            sb.AppendLine("    td { padding: 10px 12px; font-size: 13px; border-bottom: 1px solid #1e293b; }");
            sb.AppendLine("    tr:nth-child(even) { background: #172033; }");
            sb.AppendLine("    .badge { display: inline-block; padding: 2px 8px; border-radius: 9999px; font-size: 11px; font-weight: 600; }");
            sb.AppendLine("    .badge-pareto { background: #7f1d1d; color: #fca5a5; }");
            sb.AppendLine("    .badge-db { background: #1e3a8a; color: #93c5fd; }");
            sb.AppendLine("    .badge-file { background: #064e3b; color: #6ee7b7; }");
            sb.AppendLine("    .badge-log { background: #581c87; color: #d8b4fe; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class='container'>");
            sb.AppendLine("    <div class='header'>");
            sb.AppendLine("      <h1>Dataverse Storage & Cost Audit Report</h1>");
            sb.AppendLine($"     <div class='meta'>Organization: <strong>{EscapeHtml(result.OrganizationName)}</strong> ({EscapeHtml(result.OrganizationUrl)}) | Generated: {result.ScanTimestamp:yyyy-MM-dd HH:mm:ss} UTC</div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class='cards'>");
            sb.AppendLine($"     <div class='card'><div class='card-title'>Total Dataverse Storage</div><div class='card-value'>{result.OverallTotalGB:F2} GB</div></div>");
            sb.AppendLine($"     <div class='card'><div class='card-title'>Est. Monthly Bill</div><div class='card-value warn'>${result.TotalMonthlyCost:N2}</div></div>");
            sb.AppendLine($"     <div class='card'><div class='card-title'>Est. Annual Bill</div><div class='card-value warn'>${result.TotalAnnualRunRate:N2}</div></div>");
            sb.AppendLine($"     <div class='card'><div class='card-title'>Potential Monthly Savings</div><div class='card-value highlight'>${result.PotentialMonthlySavingsUsd:N2}</div></div>");
            sb.AppendLine($"     <div class='card'><div class='card-title'>Potential Annual Savings</div><div class='card-value highlight'>${result.PotentialAnnualSavingsUsd:N2}</div></div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <h2>Top 80% Cost Drivers (Pareto Analysis)</h2>");
            sb.AppendLine("    <p style='color:#94a3b8; font-size:14px;'>These tables account for 80% of your total Microsoft Dataverse database and file storage spend.</p>");
            sb.AppendLine("    <table>");
            sb.AppendLine("      <thead><tr><th>Table Display Name</th><th>Logical Name</th><th>Category</th><th>Records</th><th>Size (GB)</th><th>Monthly Spend</th><th>% of Total</th><th>Status</th></tr></thead>");
            sb.AppendLine("      <tbody>");

            foreach (var t in result.Tables.Where(t => t.IsTop80PercentDriver).OrderByDescending(t => t.MonthlyCostUsd))
            {
                string catBadge = t.Category switch
                {
                    StorageCategory.Database => "<span class='badge badge-db'>Database</span>",
                    StorageCategory.File => "<span class='badge badge-file'>File</span>",
                    StorageCategory.Log => "<span class='badge badge-log'>Log</span>",
                    _ => t.Category.ToString()
                };

                sb.AppendLine("      <tr>");
                sb.AppendLine($"       <td><strong>{EscapeHtml(t.DisplayName)}</strong></td>");
                sb.AppendLine($"       <td><code>{EscapeHtml(t.LogicalName)}</code></td>");
                sb.AppendLine($"       <td>{catBadge}</td>");
                sb.AppendLine($"       <td>{t.RecordCount:N0}</td>");
                sb.AppendLine($"       <td>{t.EstimatedTotalGB:F3} GB</td>");
                sb.AppendLine($"       <td style='font-weight:700; color:#f87171;'>${t.MonthlyCostUsd:N2}</td>");
                sb.AppendLine($"       <td>{t.PercentageOfTotalStorage:F2}%</td>");
                sb.AppendLine("       <td><span class='badge badge-pareto'>Top 80% Driver</span></td>");
                sb.AppendLine("      </tr>");
            }

            sb.AppendLine("      </tbody>");
            sb.AppendLine("    </table>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            return filePath;
        }

        private static string EscapeCsv(string val) => val?.Replace("\"", "\"\"") ?? string.Empty;
        private static string EscapeHtml(string val) => System.Net.WebUtility.HtmlEncode(val ?? string.Empty);
    }
}
