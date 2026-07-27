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

            _btnRefresh.Text = clsFormTheme.Icons.Refresh + "  Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.IconFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh);

            _btnExportCsv.Text = clsFormTheme.Icons.Export + "  Export CSV";
            _btnExportCsv.Font = new Font(clsFormTheme.IconFontName, 11F);
            clsFormTheme.ApplySuccessButtonStyle(_btnExportCsv);

            _btnClose.Text = clsFormTheme.Icons.Exit + "  Close";
            _btnClose.Font = new Font(clsFormTheme.IconFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose);

            KeyDown += frmDailyReport_KeyDown;
            this.AutoScroll = true;
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
                _lblOrders.Text = "Orders" + Environment.NewLine + Convert.ToInt32(row["OrderCount"]);
                _lblSubtotal.Text = "Subtotal" + Environment.NewLine + Convert.ToDecimal(row["Subtotal"]).ToString("C2");
                _lblTax.Text = "Tax" + Environment.NewLine + Convert.ToDecimal(row["TaxAmount"]).ToString("C2");
                _lblRevenue.Text = "Revenue" + Environment.NewLine + Convert.ToDecimal(row["TotalRevenue"]).ToString("C2");
            }

            _ordersTable = clsPOS.GetTodayOrders();
            _topProductsTable = clsPOS.GetTodayTopSellingProducts();

            _gridOrders.DataSource = _ordersTable;
            _gridTopProducts.DataSource = _topProductsTable;
            _btnExportCsv.Enabled = _ordersTable.Rows.Count > 0 || _topProductsTable.Rows.Count > 0;

            FormatCurrencyColumn(_gridOrders, "Subtotal");
            FormatCurrencyColumn(_gridOrders, "TaxAmount");
            FormatCurrencyColumn(_gridOrders, "TotalAmount");
            FormatCurrencyColumn(_gridTopProducts, "Revenue");
        }

        private void FormatCurrencyColumn(DataGridView grid, string columnName)
        {
            if (grid.Columns.Contains(columnName))
                grid.Columns[columnName].DefaultCellStyle.Format = "C2";
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

        private string BuildCsvReport()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("End-of-Day Report");
            builder.AppendLine("Date," + EscapeCsv(DateTime.Today.ToString("yyyy-MM-dd")));
            builder.AppendLine();

            builder.AppendLine("Summary");
            if (_summaryTable.Rows.Count > 0)
            {
                DataRow row = _summaryTable.Rows[0];
                builder.AppendLine("Orders," + EscapeCsv(Convert.ToInt32(row["OrderCount"]).ToString()));
                builder.AppendLine("Subtotal," + EscapeCsv(Convert.ToDecimal(row["Subtotal"]).ToString("0.00")));
                builder.AppendLine("Tax," + EscapeCsv(Convert.ToDecimal(row["TaxAmount"]).ToString("0.00")));
                builder.AppendLine("Revenue," + EscapeCsv(Convert.ToDecimal(row["TotalRevenue"]).ToString("0.00")));
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
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1200, 1300);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            ExportReportCsv();
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

        private void frmDailyReport_Scroll(object sender, ScrollEventArgs e)
        {
           //  this.AutoScroll = true;
        }
    }
}
