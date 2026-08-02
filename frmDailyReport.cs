using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmDailyReport : Form
    {
        private DataTable _summaryTable = new DataTable();
        private DataTable _ordersTable = new DataTable();
        private DataTable _topProductsTable = new DataTable();

        public frmDailyReport()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Daily Sales Report", clsFormTheme.Icons.Reports);
            clsFormTheme.ApplyGridStyle(_gridOrders);
            clsFormTheme.ApplyGridStyle(_gridTopProducts);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: () => LoadReport(),
                onSearch: null,
                onAdd: null
            );

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnExportCsv.Text = "Export CSV";
            _btnExportCsv.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySuccessButtonStyle(_btnExportCsv, clsFormTheme.Icons.Export);

            _btnExportHtml.Text = "Export HTML";
            _btnExportHtml.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySuccessButtonStyle(_btnExportHtml, clsFormTheme.Icons.Export);

            _btnClose.Text = "Close";
            _btnClose.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose, clsFormTheme.Icons.Exit);

            KeyDown += frmDailyReport_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Daily Sales Report");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnExportCsv.Text = clsLanguageManager.GetString("Export CSV");
            _btnClose.Text = clsLanguageManager.GetString("Close");
        }

        private void LoadReport()
        {
            
            string errorMessage;
            if (!clsPOS.EnsurePosSetupAndSampleData(out errorMessage))
            {
                MessageBox.Show("Report setup failed: " + errorMessage, "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _summaryTable = clsPOS.GetTodayOrderSummary();
            if (_summaryTable.Rows.Count > 0)
            {
                DataRow row = _summaryTable.Rows[0];
                int orderCount = Convert.ToInt32(row["OrderCount"]);
                decimal dailySubtotal = Convert.ToDecimal(row["Subtotal"]);
                decimal taxAmount = Convert.ToDecimal(row["TaxAmount"]);
                decimal totalRevenue = Convert.ToDecimal(row["TotalRevenue"]);

                _lblOrders.Text = "Orders" + Environment.NewLine + orderCount.ToString();
                _lblSubtotal.Text = "Subtotal" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " " + dailySubtotal.ToString("0.00");
                _lblTax.Text = "Tax" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " " + taxAmount.ToString("0.00");
                _lblRevenue.Text = "Revenue" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " " + totalRevenue.ToString("0.00");

                // Apply colors to KPI cards
                _lblOrders.BackColor = Color.FromArgb(59, 130, 246); // Blue
                _lblOrders.ForeColor = Color.White;
                _lblSubtotal.BackColor = Color.FromArgb(16, 185, 129); // Green
                _lblSubtotal.ForeColor = Color.White;
                _lblTax.BackColor = Color.FromArgb(245, 158, 11); // Orange
                _lblTax.ForeColor = Color.White;
                _lblRevenue.BackColor = Color.FromArgb(139, 92, 246); // Purple
                _lblRevenue.ForeColor = Color.White;
            }
            else
            {
                _lblOrders.Text = "Orders" + Environment.NewLine + "0";
                _lblSubtotal.Text = "Subtotal" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " 0.00";
                _lblTax.Text = "Tax" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " 0.00";
                _lblRevenue.Text = "Revenue" + Environment.NewLine + clsLanguageManager.CurrencySymbol + " 0.00";

                // Default gray color when no data
                _lblOrders.BackColor = Color.FromArgb(148, 163, 184);
                _lblOrders.ForeColor = Color.White;
                _lblSubtotal.BackColor = Color.FromArgb(148, 163, 184);
                _lblSubtotal.ForeColor = Color.White;
                _lblTax.BackColor = Color.FromArgb(148, 163, 184);
                _lblTax.ForeColor = Color.White;
                _lblRevenue.BackColor = Color.FromArgb(148, 163, 184);
                _lblRevenue.ForeColor = Color.White;
            }

            _ordersTable = clsPOS.GetTodayOrders();
            _topProductsTable = clsPOS.GetTodayTopSellingProducts();

            _gridOrders.DataSource = _ordersTable;
            _gridTopProducts.DataSource = _topProductsTable;
            _btnExportCsv.Enabled = _ordersTable.Rows.Count > 0 || _topProductsTable.Rows.Count > 0;

            // Ensure PaymentMethod column exists and is properly positioned
            if (_gridOrders.Columns.Contains("PaymentMethod"))
            {
                _gridOrders.Columns["PaymentMethod"].DisplayIndex = 5; // Position after TotalAmount
            }

            FormatCurrencyColumn(_gridOrders, "Subtotal");
            FormatCurrencyColumn(_gridOrders, "TaxAmount");
            FormatCurrencyColumn(_gridOrders, "TotalAmount");
            FormatCurrencyColumn(_gridTopProducts, "Revenue");
        }

        private void FormatCurrencyColumn(DataGridView grid, string columnName)
        {
            if (grid.Columns.Contains(columnName))
                grid.Columns[columnName].DefaultCellStyle.Format = "0.00";
        }

        private void ExportReportCsv()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Daily Report";
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.FileName = "DailyReport-" + DateTime.Today.ToString("yyyy-MM-dd") + ".csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    File.WriteAllText(dialog.FileName, BuildCsvReport(), Encoding.UTF8);
                    MessageBox.Show("Daily report exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportReportHtml()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Daily Report to HTML";
                dialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
                dialog.FileName = "DailyReport-" + DateTime.Today.ToString("yyyy-MM-dd") + ".html";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    string errorMessage;
                    DataTable[] tables = { _summaryTable, _ordersTable, _topProductsTable };
                    string[] titles = { "Summary", "Today's Orders", "Top-Selling Products" };

                    if (clsReportExporter.ExportMultipleTablesToHtml(tables, titles, "Daily Sales Report", dialog.FileName, out errorMessage))
                    {
                        MessageBox.Show("Daily report exported to HTML successfully.\n\nYou can open the HTML file in your browser and print to PDF from there.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Export failed: " + errorMessage, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string BuildCsvReport()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("End-of-Day Close-out Report");
            builder.AppendLine("Date," + EscapeCsv(DateTime.Today.ToString("yyyy-MM-dd")));
            builder.AppendLine("Currency," + EscapeCsv(clsLanguageManager.CurrencyName + " (" + clsLanguageManager.CurrencySymbol + ")"));
            builder.AppendLine();

            builder.AppendLine("Summary");
            if (_summaryTable.Rows.Count > 0)
            {
                DataRow row = _summaryTable.Rows[0];
                builder.AppendLine("Orders," + EscapeCsv(Convert.ToInt32(row["OrderCount"]).ToString()));
                builder.AppendLine("Subtotal," + EscapeCsv(clsLanguageManager.CurrencySymbol + " " + Convert.ToDecimal(row["Subtotal"]).ToString("0.00")));
                builder.AppendLine("Tax," + EscapeCsv(clsLanguageManager.CurrencySymbol + " " + Convert.ToDecimal(row["TaxAmount"]).ToString("0.00")));
                builder.AppendLine("Revenue," + EscapeCsv(clsLanguageManager.CurrencySymbol + " " + Convert.ToDecimal(row["TotalRevenue"]).ToString("0.00")));
            }
            builder.AppendLine();

            AppendTable(builder, "Today's Orders", _ordersTable);
            builder.AppendLine();
            AppendTable(builder, "Top-Selling Products", _topProductsTable);

            return builder.ToString();
        }

        private void AppendTable(StringBuilder builder, string title, DataTable table)
        {
            builder.AppendLine(title);

            if (table == null || table.Columns.Count == 0)
            {
                builder.AppendLine("No data");
                return;
            }

            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
            {
                if (columnIndex > 0)
                    builder.Append(",");

                builder.Append(EscapeCsv(table.Columns[columnIndex].ColumnName));
            }

            builder.AppendLine();

            if (table.Rows.Count == 0)
            {
                builder.AppendLine("No rows");
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                {
                    if (columnIndex > 0)
                        builder.Append(",");

                    builder.Append(EscapeCsv(Convert.ToString(row[columnIndex])));
                }

                builder.AppendLine();
            }
        }

        private string EscapeCsv(string value)
        {
            if (value == null)
                return "";

            bool mustQuote = value.Contains(",") || value.Contains("\"") || value.Contains("\r") || value.Contains("\n");
            string escaped = value.Replace("\"", "\"\"");

            return mustQuote ? "\"" + escaped + "\"" : escaped;
        }

        private void frmDailyReport_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            ExportReportCsv();
        }

        private void btnExportHtml_Click(object sender, EventArgs e)
        {
            ExportReportHtml();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDailyReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadReport();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
