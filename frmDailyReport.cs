using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

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
                clsFormTheme.ShowError(this, errorMessage, "Report Setup Failed");
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

        private void _gridOrders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                _gridOrders.ClearSelection();
                _gridOrders.Rows[e.RowIndex].Selected = true;
                _contextMenuOrders.Show(Cursor.Position);
            }
        }

        private void _menuViewDetails_Click(object sender, EventArgs e)
        {
            if (_gridOrders.SelectedRows.Count == 0)
                return;

            int orderID = Convert.ToInt32(_gridOrders.SelectedRows[0].Cells["OrderID"].Value);
            ShowOrderDetails(orderID);
        }

        private void _menuPrintReceipt_Click(object sender, EventArgs e)
        {
            if (_gridOrders.SelectedRows.Count == 0)
                return;

            int orderID = Convert.ToInt32(_gridOrders.SelectedRows[0].Cells["OrderID"].Value);
            PrintOrderReceipt(orderID);
        }

        private void _menuRefund_Click(object sender, EventArgs e)
        {
            if (_gridOrders.SelectedRows.Count == 0)
                return;

            int orderID = Convert.ToInt32(_gridOrders.SelectedRows[0].Cells["OrderID"].Value);
            ProcessRefund(orderID);
        }

        private void _menuVoid_Click(object sender, EventArgs e)
        {
            if (_gridOrders.SelectedRows.Count == 0)
                return;

            int orderID = Convert.ToInt32(_gridOrders.SelectedRows[0].Cells["OrderID"].Value);
            VoidOrder(orderID);
        }

        private void ShowOrderDetails(int orderID)
        {
            var orderDetails = clsCustomer.GetOrderDetails(orderID);
            var orderItems = clsCustomer.GetOrderItems(orderID);

            if (orderDetails == null || orderDetails.Rows.Count == 0)
            {
                clsFormTheme.ShowError(this, "Order not found.", "Error");
                return;
            }

            string details = $"Order ID: {orderID}\n";
            details += $"Date: {orderDetails.Rows[0]["OrderDate"]}\n";
            details += $"Subtotal: {Convert.ToDecimal(orderDetails.Rows[0]["Subtotal"]):C2}\n";
            details += $"Tax: {Convert.ToDecimal(orderDetails.Rows[0]["TaxAmount"]):C2}\n";
            details += $"Total: {Convert.ToDecimal(orderDetails.Rows[0]["TotalAmount"]):C2}\n";
            details += $"Payment: {orderDetails.Rows[0]["PaymentMethod"]}\n";
            
            if (orderDetails.Rows[0]["PaymentDetails"] != DBNull.Value)
            {
                details += $"Payment Details: {orderDetails.Rows[0]["PaymentDetails"]}\n";
            }

            details += "\nItems:\n";
            foreach (DataRow item in orderItems.Rows)
            {
                details += $"- {item["ProductName"]} x{item["Quantity"]} @ {Convert.ToDecimal(item["UnitPrice"]):C2} = {Convert.ToDecimal(item["Subtotal"]):C2}\n";
            }

            MessageBox.Show(details, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintOrderReceipt(int orderID)
        {
            var orderDetails = clsCustomer.GetOrderDetails(orderID);
            var orderItems = clsCustomer.GetOrderItems(orderID);
            string customerName = orderDetails.Rows[0]["CustomerName"] != DBNull.Value 
                ? orderDetails.Rows[0]["CustomerName"].ToString() 
                : "Guest";

            clsPrintHelper.PrintReceipt(orderDetails, orderItems, customerName);
        }

        private void ProcessRefund(int orderID)
        {
            var orderDetails = clsCustomer.GetOrderDetails(orderID);
            if (orderDetails == null || orderDetails.Rows.Count == 0)
            {
                clsFormTheme.ShowError(this, "Order not found.", "Error");
                return;
            }

            // Check if already refunded
            if (orderDetails.Rows[0]["RefundID"] != DBNull.Value)
            {
                clsFormTheme.ShowWarning(this, "Order has already been refunded.", "Refund");
                return;
            }

            // Check if voided
            bool isVoided = orderDetails.Rows[0]["IsVoided"] != DBNull.Value && Convert.ToBoolean(orderDetails.Rows[0]["IsVoided"]);
            if (isVoided)
            {
                clsFormTheme.ShowWarning(this, "Cannot refund a voided order.", "Refund");
                return;
            }

            decimal totalAmount = Convert.ToDecimal(orderDetails.Rows[0]["TotalAmount"]);
            var result = MessageBox.Show($"Process full refund for Order #{orderID}?\n\nAmount: {totalAmount:C2}", 
                "Confirm Refund", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int refundID;
                string errorMessage;
                bool success = clsRefund.ProcessFullRefund(orderID, "Refund from Daily Report", "Cash", 
                    clsUserManagement.CurrentUser?.UserID ?? 0, out refundID, out errorMessage);

                if (success)
                {
                    clsFormTheme.ShowSuccess(this, "Refund processed successfully.", "Refund");
                    LoadReport(); // Refresh the report
                }
                else
                {
                    clsFormTheme.ShowError(this, errorMessage, "Refund Failed");
                }
            }
        }

        private void VoidOrder(int orderID)
        {
            var orderDetails = clsCustomer.GetOrderDetails(orderID);
            if (orderDetails == null || orderDetails.Rows.Count == 0)
            {
                clsFormTheme.ShowError(this, "Order not found.", "Error");
                return;
            }

            // Check if already voided
            bool isVoided = orderDetails.Rows[0]["IsVoided"] != DBNull.Value && Convert.ToBoolean(orderDetails.Rows[0]["IsVoided"]);
            if (isVoided)
            {
                clsFormTheme.ShowWarning(this, "Order is already voided.", "Void");
                return;
            }

            // Check if already refunded
            if (orderDetails.Rows[0]["RefundID"] != DBNull.Value)
            {
                clsFormTheme.ShowWarning(this, "Cannot void an order that has been refunded.", "Void");
                return;
            }

            decimal totalAmount = Convert.ToDecimal(orderDetails.Rows[0]["TotalAmount"]);
            var result = MessageBox.Show($"Void Order #{orderID}?\n\nThis will mark the order as voided.\nAmount: {totalAmount:C2}", 
                "Confirm Void", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string errorMessage;
                bool success = clsPOSData.VoidOrder(orderID, "Voided from Daily Report", 
                    clsUserManagement.CurrentUser?.Username ?? "System", out errorMessage);

                if (success)
                {
                    clsFormTheme.ShowSuccess(this, "Order voided successfully.", "Void");
                    LoadReport(); // Refresh the report
                }
                else
                {
                    clsFormTheme.ShowError(this, errorMessage, "Void Failed");
                }
            }
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
                    clsFormTheme.ShowSuccess(this, "Daily report exported successfully.", "Export");
                }
                catch (Exception ex)
                {
                    clsFormTheme.ShowError(this, ex.Message, "Export Failed");
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
                        clsFormTheme.ShowSuccess(this, "Daily report exported to HTML successfully.\n\nYou can open the HTML file in your browser and print to PDF from there.", "Export");
                    }
                    else
                    {
                        clsFormTheme.ShowError(this, errorMessage, "Export Failed");
                    }
                }
                catch (Exception ex)
                {
                    clsFormTheme.ShowError(this, ex.Message, "Export Failed");
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
