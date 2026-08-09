using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
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

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await LoadReportDataAsync(),
                onSearch: null,
                onAdd: null
            );

            btnExportCsv.Text = "Export CSV";
            btnExportCsv.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySuccessButtonStyle(btnExportCsv, clsFormTheme.Icons.Export);

            btnRefresh.Text = "Refresh";
            btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh, clsFormTheme.Icons.Refresh);

            btnClose.Text = "Close";
            btnClose.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);

            clsFormTheme.ApplyGridStyle(DataGVStockValuation);

            _toolTip.SetToolTip(btnExportCsv, "Export the report to a CSV file that can be opened in Excel.");
            _toolTip.SetToolTip(btnRefresh, "Refresh the stock valuation report (F5).");
            _toolTip.SetToolTip(btnClose, "Close this report (Esc).");

            lblEmptyState.Visible = false;
            KeyPreview = true;
            KeyDown += frmStockValuationReport_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Stock Valuation Report");
            _lblHeaderTitle.Text = clsLanguageManager.GetString("Stock Valuation Report");
            btnExportCsv.Text = clsLanguageManager.GetString("Export CSV");
            btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            btnClose.Text = clsLanguageManager.GetString("Close");
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

                clsFormTheme.ShowError(this,
                    ex.Message,
                    "Error");
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
            btnExportCsv.Enabled = !isLoading && _reportTable.Rows.Count > 0;
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
            btnExportCsv.Enabled = hasRows;
        }

        private string GetCsvValue(object value)
        {
            string text = value == DBNull.Value ? "" : Convert.ToString(value);
            return "\"" + text.Replace("\"", "\"\"") + "\"";
        }

        private void ExportReportToCsv()
        {
            if (_reportTable.Rows.Count == 0)
            {
                clsFormTheme.ShowInfo(this,
                    "There is no report data to export.",
                    "Export Report");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = "StockValuationReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    StringBuilder csv = new StringBuilder();

                    for (int columnIndex = 0; columnIndex < _reportTable.Columns.Count; columnIndex++)
                    {
                        csv.Append(GetCsvValue(_reportTable.Columns[columnIndex].ColumnName));

                        if (columnIndex < _reportTable.Columns.Count - 1)
                            csv.Append(",");
                    }

                    csv.AppendLine();

                    foreach (DataRow row in _reportTable.Rows)
                    {
                        for (int columnIndex = 0; columnIndex < _reportTable.Columns.Count; columnIndex++)
                        {
                            csv.Append(GetCsvValue(row[columnIndex]));

                            if (columnIndex < _reportTable.Columns.Count - 1)
                                csv.Append(",");
                        }

                        csv.AppendLine();
                    }

                    File.WriteAllText(saveFileDialog.FileName, csv.ToString(), Encoding.UTF8);

                    clsFormTheme.ShowSuccess(this, "Stock valuation report exported successfully.", "Export Report");
                }
                catch (Exception ex)
                {
                    clsFormTheme.ShowError(this, ex.Message, "Export Error");
                }
            }
        }

        private async void frmStockValuationReport_Load(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            ExportReportToCsv();
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
