using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmStockValuationReport : Form
    {
        private DataTable _reportTable = new DataTable();
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmStockValuationReport()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
            clsFormTheme.ApplyGridStyle(DataGVStockValuation);

            _toolTip.SetToolTip(btnRefresh, "Refresh the stock valuation report (F5).");
            _toolTip.SetToolTip(btnClose, "Close this report (Esc).");

            lblEmptyState.Visible = false;
            KeyPreview = true;
            KeyDown += frmStockValuationReport_KeyDown;
        }

        private async Task LoadReportDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _reportTable = await Task.Run(() => clsReport.GetStockValuationReport());
            }
            catch (Exception ex)
            {
                _reportTable = new DataTable();

                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            _isLoading = isLoading;
            UseWaitCursor = isLoading;
            DataGVStockValuation.Enabled = !isLoading;
            btnRefresh.Enabled = !isLoading;
            btnClose.Enabled = !isLoading;

            if (isLoading)
            {
                lblEmptyState.Text = "Loading stock valuation report...";
                lblEmptyState.Visible = true;
            }
            else
            {
                DisplayReport();
            }
        }

        private decimal GetTotalStockValue()
        {
            decimal totalStockValue = 0;

            foreach (DataRow row in _reportTable.Rows)
            {
                if (row["StockValue"] != DBNull.Value)
                {
                    totalStockValue += Convert.ToDecimal(row["StockValue"]);
                }
            }

            return totalStockValue;
        }

        private void DisplayReport()
        {
            if (_isLoading)
                return;

            DataGVStockValuation.DataSource = _reportTable;

            foreach (DataGridViewColumn column in DataGVStockValuation.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            if (DataGVStockValuation.Columns.Contains("CategoryName"))
                DataGVStockValuation.Columns["CategoryName"].HeaderText = "Category";

            if (DataGVStockValuation.Columns.Contains("SupplierName"))
                DataGVStockValuation.Columns["SupplierName"].HeaderText = "Supplier";

            if (DataGVStockValuation.Columns.Contains("Price"))
                DataGVStockValuation.Columns["Price"].DefaultCellStyle.Format = "N2";

            if (DataGVStockValuation.Columns.Contains("StockValue"))
            {
                DataGVStockValuation.Columns["StockValue"].HeaderText = "Stock Value";
                DataGVStockValuation.Columns["StockValue"].DefaultCellStyle.Format = "N2";
            }

            bool hasRows = _reportTable.Rows.Count > 0;
            lblEmptyState.Visible = !hasRows;
            lblEmptyState.Text = "No products are available for stock valuation.";
            lblTotalStockValue.Text = hasRows
                ? "Total Stock Value: " + GetTotalStockValue().ToString("N2")
                : "Total Stock Value: 0.00";
        }

        private async void frmStockValuationReport_Load(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void frmStockValuationReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await LoadReportDataAsync();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
