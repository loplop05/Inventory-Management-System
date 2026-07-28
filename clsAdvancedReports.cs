using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Advanced reporting system with extended reporting capabilities.
    /// Provides monthly/yearly reports, category analysis, supplier performance, and more.
    /// </summary>
    public static class clsAdvancedReports
    {
        // ─── Report Types ────────────────────────────────────────────────────────

        public enum ReportType
        {
            DailySales,
            WeeklySales,
            MonthlySales,
            YearlySales,
            CategoryPerformance,
            SupplierPerformance,
            ProductPerformance,
            ProfitMargin,
            CustomerAnalysis,
            StockMovement
        }

        public enum DateRange
        {
            Today,
            Yesterday,
            Last7Days,
            Last30Days,
            ThisMonth,
            LastMonth,
            ThisYear,
            LastYear,
            Custom
        }

        // ─── Report Data Structures ─────────────────────────────────────────────

        public class ReportData
        {
            public ReportType Type { get; set; }
            public DateRange Range { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DataTable Data { get; set; }
            public Dictionary<string, object> Summary { get; set; } = new Dictionary<string, object>();
            public string Title { get; set; }
        }

        // ─── Report Generation ─────────────────────────────────────────────────

        /// <summary>
        /// Generates a report based on type and date range.
        /// </summary>
        public static ReportData GenerateReport(ReportType type, DateRange range, DateTime? customStart = null, DateTime? customEnd = null)
        {
            var (startDate, endDate) = GetDateRange(range, customStart, customEnd);
            var report =new ReportData
            {
                Type = type,
                Range = range,
                StartDate = startDate,
                EndDate = endDate,
                Title = GetReportTitle(type, range)
            };

            try
            {
                switch (type)
                {
                    case ReportType.DailySales:
                    case ReportType.WeeklySales:
                    case ReportType.MonthlySales:
                    case ReportType.YearlySales:
                        report.Data = GenerateSalesReport(startDate, endDate, type);
                        break;

                    case ReportType.CategoryPerformance:
                        report.Data = GenerateCategoryReport(startDate, endDate);
                        break;

                    case ReportType.SupplierPerformance:
                        report.Data = GenerateSupplierReport(startDate, endDate);
                        break;

                    case ReportType.ProductPerformance:
                        report.Data = GenerateProductReport(startDate, endDate);
                        break;

                    case ReportType.ProfitMargin:
                        report.Data = GenerateProfitMarginReport(startDate, endDate);
                        break;

                    case ReportType.CustomerAnalysis:
                        report.Data = GenerateCustomerReport(startDate, endDate);
                        break;

                    case ReportType.StockMovement:
                        report.Data = GenerateStockMovementReport(startDate, endDate);
                        break;
                }

                CalculateSummary(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return report;
        }

        private static (DateTime, DateTime) GetDateRange(DateRange range, DateTime? customStart, DateTime? customEnd)
        {
            DateTime now = DateTime.Now;
            DateTime start, end;

            switch (range)
            {
                case DateRange.Today:
                    start = now.Date;
                    end = now.Date.AddDays(1).AddTicks(-1);
                    break;

                case DateRange.Yesterday:
                    start = now.Date.AddDays(-1);
                    end = now.Date.AddTicks(-1);
                    break;

                case DateRange.Last7Days:
                    start = now.Date.AddDays(-7);
                    end = now.Date.AddTicks(-1);
                    break;

                case DateRange.Last30Days:
                    start = now.Date.AddDays(-30);
                    end = now.Date.AddTicks(-1);
                    break;

                case DateRange.ThisMonth:
                    start = new DateTime(now.Year, now.Month, 1);
                    end = start.AddMonths(1).AddTicks(-1);
                    break;

                case DateRange.LastMonth:
                    start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    end = new DateTime(now.Year, now.Month, 1).AddTicks(-1);
                    break;

                case DateRange.ThisYear:
                    start = new DateTime(now.Year, 1, 1);
                    end = new DateTime(now.Year, 12, 31);
                    break;

                case DateRange.LastYear:
                    start = new DateTime(now.Year - 1, 1, 1);
                    end = new DateTime(now.Year - 1, 12, 31);
                    break;

                case DateRange.Custom:
                    start = customStart ?? now.Date;
                    end = customEnd ?? now.Date.AddDays(1).AddTicks(-1);
                    break;

                default:
                    start = now.Date;
                    end = now.Date.AddTicks(-1);
                    break;
            }

            return (start, end);
        }

        private static string GetReportTitle(ReportType type, DateRange range)
        {
            string rangeText = range.ToString().Replace("Last", "").Replace("This", "");
            string typeText = type.ToString().Replace("Sales", " Sales Report").Replace("Performance", " Performance");
            return $"{rangeText} {typeText}";
        }

        private static DataTable GenerateSalesReport(DateTime start, DateTime end, ReportType type)
        {
            // This would call the business layer to get sales data
            // For now, return a sample structure
            DataTable table = new DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("OrderCount", typeof(int));
            table.Columns.Add("TotalSales", typeof(decimal));
            table.Columns.Add("AverageOrderValue", typeof(decimal));

            // Sample data - replace with actual business layer call
            var data = clsReport.GetDailySales(start);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                table.Rows.Add(start, Convert.ToInt32(row["OrderCount"]), 
                    Convert.ToDecimal(row["TotalSales"]), 
                    Convert.ToDecimal(row["TotalSales"]) / Math.Max(1, Convert.ToInt32(row["OrderCount"])));
            }

            return table;
        }

        private static DataTable GenerateCategoryReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("OrderCount", typeof(int));
            table.Columns.Add("TotalSales", typeof(decimal));
            table.Columns.Add("Percentage", typeof(decimal));

            // Sample data
            table.Rows.Add("Electronics", 45, 4500.00m, 35.0m);
            table.Rows.Add("Clothing", 30, 3000.00m, 23.0m);
            table.Rows.Add("Food", 25, 2500.00m, 19.0m);
            table.Rows.Add("Other", 30, 3000.00m, 23.0m);

            return table;
        }

        private static DataTable GenerateSupplierReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Supplier", typeof(string));
            table.Columns.Add("OrderCount", typeof(int));
            table.Columns.Add("TotalSales", typeof(decimal));
            table.Columns.Add("ProductCount", typeof(int));
            table.Columns.Add("Rating", typeof(decimal));

            // Sample data
            table.Rows.Add("ABC Supplies", 50, 5000.00m, 15, 4.5m);
            table.Rows.Add("XYZ Distributors", 40, 4000.00m, 12, 4.2m);
            table.Rows.Add("Global Traders", 30, 3000.00m, 10, 4.0m);

            return table;
        }

        private static DataTable GenerateProductReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("QuantitySold", typeof(int));
            table.Columns.Add("Revenue", typeof(decimal));
            table.Columns.Add("Profit", typeof(decimal));

            // Sample data
            table.Rows.Add("Laptop", "Electronics", 20, 20000.00m, 5000.00m);
            table.Rows.Add("T-Shirt", "Clothing", 50, 2500.00m, 1000.00m);
            table.Rows.Add("Coffee", "Food", 100, 1000.00m, 400.00m);

            return table;
        }

        private static DataTable GenerateProfitMarginReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Revenue", typeof(decimal));
            table.Columns.Add("Cost", typeof(decimal));
            table.Columns.Add("Profit", typeof(decimal));
            table.Columns.Add("Margin", typeof(decimal));

            // Sample data
            table.Rows.Add("Electronics", 4500.00m, 3000.00m, 1500.00m, 33.3m);
            table.Rows.Add("Clothing", 3000.00m, 2000.00m, 1000.00m, 33.3m);
            table.Rows.Add("Food", 2500.00m, 2000.00m, 500.00m, 20.0m);

            return table;
        }

        private static DataTable GenerateCustomerReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("OrderCount", typeof(int));
            table.Columns.Add("TotalSpent", typeof(decimal));
            table.Columns.Add("LastOrderDate", typeof(DateTime));

            // Sample data
            table.Rows.Add("John Doe", "555-1234", 10, 1000.00m, DateTime.Now.AddDays(-5));
            table.Rows.Add("Jane Smith", "555-5678", 8, 800.00m, DateTime.Now.AddDays(-10));
            table.Rows.Add("Bob Johnson", "555-9012", 5, 500.00m, DateTime.Now.AddDays(-15));

            return table;
        }

        private static DataTable GenerateStockMovementReport(DateTime start, DateTime end)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("OpeningStock", typeof(int));
            table.Columns.Add("StockIn", typeof(int));
            table.Columns.Add("StockOut", typeof(int));
            table.Columns.Add("ClosingStock", typeof(int));

            // Sample data
            table.Rows.Add("Laptop", 50, 20, 30, 40);
            table.Rows.Add("T-Shirt", 100, 50, 60, 90);
            table.Rows.Add("Coffee", 200, 100, 150, 150);

            return table;
        }

        private static void CalculateSummary(ReportData report)
        {
            if (report.Data == null || report.Data.Rows.Count == 0) return;

            switch (report.Type)
            {
                case ReportType.DailySales:
                case ReportType.WeeklySales:
                case ReportType.MonthlySales:
                case ReportType.YearlySales:
                    decimal totalSales = 0;
                    int totalOrders = 0;
                    foreach (DataRow row in report.Data.Rows)
                    {
                        totalSales += Convert.ToDecimal(row["TotalSales"]);
                        totalOrders += Convert.ToInt32(row["OrderCount"]);
                    }
                    report.Summary["TotalSales"] = totalSales;
                    report.Summary["TotalOrders"] = totalOrders;
                    report.Summary["AverageOrderValue"] = totalOrders > 0 ? totalSales / totalOrders : 0;
                    break;

                case ReportType.CategoryPerformance:
                    decimal catTotal = 0;
                    foreach (DataRow row in report.Data.Rows)
                    {
                        catTotal += Convert.ToDecimal(row["TotalSales"]);
                    }
                    report.Summary["TotalSales"] = catTotal;
                    report.Summary["TopCategory"] = report.Data.Rows[0]["Category"];
                    break;

                case ReportType.ProfitMargin:
                    decimal totalRevenue = 0;
                    decimal totalProfit = 0;
                    foreach (DataRow row in report.Data.Rows)
                    {
                        totalRevenue += Convert.ToDecimal(row["Revenue"]);
                        totalProfit += Convert.ToDecimal(row["Profit"]);
                    }
                    report.Summary["TotalRevenue"] = totalRevenue;
                    report.Summary["TotalProfit"] = totalProfit;
                    report.Summary["AverageMargin"] = totalRevenue > 0 ? (totalProfit / totalRevenue) * 100 : 0;
                    break;
            }
        }

        // ─── Report Display ───────────────────────────────────────────────────

        /// <summary>
        /// Shows a report viewer form for the specified report.
        /// </summary>
        public static void ShowReport(ReportData report)
        {
            var reportForm = new Form
            {
                Text = report.Title,
                Size = new Size(900, 600),
                StartPosition = FormStartPosition.CenterParent,
                MinimumSize = new Size(800, 500)
            };

            ApplyTheme(reportForm);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // Summary panel
            var summaryPanel = new Panel { Dock = DockStyle.Fill };
            var summaryLabel = new Label
            {
                Text = BuildSummaryText(report),
                Font = new Font("Segoe UI", 10F),
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            summaryPanel.Controls.Add(summaryLabel);

            // Data grid
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = report.Data,
                AutoGenerateColumns = true,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            clsFormTheme.ApplyGridStyle(grid);

            // Button panel
            var buttonPanel = new Panel { Dock = DockStyle.Fill };
            
            var btnExport = new Button
            {
                Text = "Export CSV",
                Size = new Size(120, 30),
                Location = new Point(550, 10)
            };
            clsFormTheme.ApplySuccessButtonStyle(btnExport, clsFormTheme.Icons.Export);
            btnExport.Click += (s, e) =>
            {
                string csv = clsPrintHelper.ExportToCsv(report.Data);
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    DefaultExt = "csv",
                    FileName = $"{report.Title.Replace(" ", "_")}.csv"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dialog.FileName, csv);
                    MessageBox.Show("Report exported successfully.", "Export", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            var btnPrint = new Button
            {
                Text = "Print",
                Size = new Size(100, 30),
                Location = new Point(680, 10)
            };
            clsFormTheme.ApplySecondaryButtonStyle(btnPrint, clsFormTheme.Icons.Print);
            btnPrint.Click += (s, e) => clsPrintHelper.PrintReport(report.Data, report.Title);

            var btnClose = new Button
            {
                Text = "Close",
                Size = new Size(100, 30),
                Location = new Point(790, 10)
            };
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            btnClose.Click += (s, e) => reportForm.Close();

            buttonPanel.Controls.Add(btnExport);
            buttonPanel.Controls.Add(btnPrint);
            buttonPanel.Controls.Add(btnClose);

            mainPanel.Controls.Add(summaryPanel, 0, 0);
            mainPanel.Controls.Add(grid, 0, 1);
            mainPanel.Controls.Add(buttonPanel, 0, 2);

            reportForm.Controls.Add(mainPanel);
            reportForm.Show();
        }

        private static string BuildSummaryText(ReportData report)
        {
            var summary = new System.Text.StringBuilder();
            summary.AppendLine($"Report: {report.Title}");
            summary.AppendLine($"Period: {report.StartDate:dd/MM/yyyy} - {report.EndDate:dd/MM/yyyy}");
            summary.AppendLine();

            foreach (var kvp in report.Summary)
            {
                if (kvp.Value is decimal d)
                    summary.AppendLine($"{kvp.Key}: {d:C}");
                else if (kvp.Value is int i)
                    summary.AppendLine($"{kvp.Key}: {i}");
                else
                    summary.AppendLine($"{kvp.Key}: {kvp.Value}");
            }

            return summary.ToString();
        }

        private static void ApplyTheme(Form form)
        {
            clsFormTheme.ApplyFormStyle(form);
            clsFormTheme.CreateHeaderPanel(form, "Advanced Reports", clsFormTheme.Icons.Reports);
        }

        // ─── Report Launcher ───────────────────────────────────────────────────

        /// <summary>
        /// Shows a report selection dialog.
        /// </summary>
        public static void ShowReportLauncher()
        {
            var launcherForm = new Form
            {
                Text = "Advanced Reports",
                Size = new Size(500, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            ApplyTheme(launcherForm);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(20)
            };

            // Report type selection
            mainPanel.Controls.Add(new Label { Text = "Report Type:", Anchor = AnchorStyles.Left }, 0, 0);
            var cboReportType = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cboReportType.Items.AddRange(Enum.GetNames(typeof(ReportType)));
            cboReportType.SelectedIndex = 0;
            mainPanel.Controls.Add(cboReportType, 1, 0);

            // Date range selection
            mainPanel.Controls.Add(new Label { Text = "Date Range:", Anchor = AnchorStyles.Left }, 0, 1);
            var cboDateRange = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cboDateRange.Items.AddRange(Enum.GetNames(typeof(DateRange)));
            cboDateRange.SelectedIndex = 4; // ThisMonth
            mainPanel.Controls.Add(cboDateRange, 1, 1);

            // Custom date range (hidden by default)
            var dtpStart = new DateTimePicker { Dock = DockStyle.Fill, Visible = false };
            var dtpEnd = new DateTimePicker { Dock = DockStyle.Fill, Visible = false };
            mainPanel.Controls.Add(new Label { Text = "Start Date:", Anchor = AnchorStyles.Left, Visible = false }, 0, 2);
            mainPanel.Controls.Add(dtpStart, 1, 2);
            mainPanel.Controls.Add(new Label { Text = "End Date:", Anchor = AnchorStyles.Left, Visible = false }, 0, 3);
            mainPanel.Controls.Add(dtpEnd, 1, 3);

            // Show/hide custom date range based on selection
            cboDateRange.SelectedIndexChanged += (s, e) =>
            {
                bool showCustom = cboDateRange.SelectedItem.ToString() == "Custom";
                dtpStart.Visible = showCustom;
                dtpEnd.Visible = showCustom;
                mainPanel.GetControlFromPosition(0, 2).Visible = showCustom;
                mainPanel.GetControlFromPosition(0, 3).Visible = showCustom;
            };

            // Generate button
            var btnGenerate = new Button
            {
                Text = "Generate Report",
                Height = 40,
                Margin = new Padding(0, 10, 0, 0)
            };
            clsFormTheme.ApplyPrimaryButtonStyle(btnGenerate, clsFormTheme.Icons.Chart);
            btnGenerate.Click += (s, e) =>
            {
                var type = (ReportType)Enum.Parse(typeof(ReportType), cboReportType.SelectedItem.ToString());
                var range = (DateRange)Enum.Parse(typeof(DateRange), cboDateRange.SelectedItem.ToString());
                
                DateTime? customStart = dtpStart.Visible ? dtpStart.Value : (DateTime?)null;
                DateTime? customEnd = dtpEnd.Visible ? dtpEnd.Value : (DateTime?)null;

                var report = GenerateReport(type, range, customStart, customEnd);
                ShowReport(report);
            };

            mainPanel.Controls.Add(btnGenerate, 0, 4);
            mainPanel.SetColumnSpan(btnGenerate, 2);

            launcherForm.Controls.Add(mainPanel);
            launcherForm.ShowDialog();
        }
    }
}
