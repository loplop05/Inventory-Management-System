using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmSuppliersManagment : Form
    {
        private DataTable _suppliersTable = new DataTable();
        private const int PageSize = 10;
        private int _currentPage = 1;
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmSuppliersManagment()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Suppliers", clsFormTheme.Icons.Suppliers);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await RefreshGridDataAsync(),
                onSearch: () => _txtSearch.Focus(),
                onAdd: () => btnAddSupplier_Click(null, null)
            );

            btnAddSupplier.Text = "Add";
            btnAddSupplier.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddSupplier, clsFormTheme.Icons.Add);

            btnDeleteSupplier.Text = "Delete";
            btnDeleteSupplier.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteSupplier, clsFormTheme.Icons.Delete);

            btnUpdateSupplier.Text = "Update";
            btnUpdateSupplier.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdateSupplier, clsFormTheme.Icons.Update);

            btnViewPerformance.Text = "Performance";
            btnViewPerformance.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(btnViewPerformance, clsFormTheme.Icons.Chart);

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(_btnPreviousPage);

            _btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(_btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVSuppliers);

            _toolTip.SetToolTip(_txtSearch, "Search by supplier ID, name, phone, or email.");
            _toolTip.SetToolTip(_btnRefresh, "Refresh the supplier list (F5).");

            clsSearchHelper.SetupAutoComplete(_txtSearch, "SuppliersSearch");

            _lblEmptyState.Visible = false;
            KeyDown += frmSuppliersManagment_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Suppliers Management");
            btnAddSupplier.Text = clsLanguageManager.GetString("Add");
            btnDeleteSupplier.Text = clsLanguageManager.GetString("Delete");
            btnUpdateSupplier.Text = clsLanguageManager.GetString("Update");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnPreviousPage.Text = clsLanguageManager.GetString("Previous");
            _btnNextPage.Text = clsLanguageManager.GetString("Next");
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _suppliersTable = await Task.Run(() => clsSupplier.GetAllSuppliers());
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                _suppliersTable = new DataTable();
                DisplayCurrentPage();

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
            DataGVSuppliers.Enabled = !isLoading;
            _txtSearch.Enabled = !isLoading;
            btnAddSupplier.Enabled = !isLoading;
            btnDeleteSupplier.Enabled = !isLoading;
            btnUpdateSupplier.Enabled = !isLoading;
            _btnRefresh.Enabled = !isLoading;

            if (isLoading)
            {
                _lblEmptyState.Text = "Loading suppliers...";
                _lblEmptyState.Visible = true;
                _btnPreviousPage.Enabled = false;
                _btnNextPage.Enabled = false;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredSuppliers()
        {
            string searchText = _txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
                return _suppliersTable;

            DataView view = clsSearchHelper.QuickSearch(_suppliersTable, searchText, "SupplierID", "SupplierName", "Phone", "Email");
            return view.ToTable();
        }

        private void DisplayCurrentPage()
        {
            if (_isLoading)
                return;

            DataTable filteredTable = GetFilteredSuppliers();
            int rowCount = filteredTable.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(rowCount / (double)PageSize));

            if (_currentPage > totalPages)
                _currentPage = totalPages;

            DataTable pageTable = filteredTable.Clone();
            int startIndex = (_currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, rowCount);

            for (int index = startIndex; index < endIndex; index++)
            {
                pageTable.ImportRow(filteredTable.Rows[index]);
            }

            DataGVSuppliers.DataSource = pageTable;

            foreach (DataGridViewColumn column in DataGVSuppliers.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            bool hasRows = pageTable.Rows.Count > 0;
            _lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                _lblEmptyState.Text = string.IsNullOrWhiteSpace(_txtSearch.Text)
                    ? "No suppliers found. Add your first supplier."
                    : "No suppliers match your search.";
            }

            _lblPageInfo.Text = rowCount == 0
                ? "No results"
                : $"Page {_currentPage} of {totalPages}";

            _btnPreviousPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < totalPages;
        }

        private void btnBackToPrevPage_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void frmSuppliersManagment_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnAddSupplier_Click(object sender, EventArgs e)
        {
            frmAddSupplier frm = new frmAddSupplier();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            frmDeleteSupplier frm = new frmDeleteSupplier();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            frmUpdateSupplier frm = new frmUpdateSupplier();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private void btnViewPerformance_Click(object sender, EventArgs e)
        {
            if (DataGVSuppliers.CurrentRow == null)
            {
                clsFormTheme.ShowWarning(this, "Please select a supplier first.", "Supplier Performance");
                return;
            }

            int supplierID = Convert.ToInt32(DataGVSuppliers.CurrentRow.Cells["SupplierID"].Value);
            string supplierName = DataGVSuppliers.CurrentRow.Cells["SupplierName"].Value.ToString();

            // Get performance data for the last 30 days
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);

            DataTable performanceData = clsReport.GetSupplierPerformance(startDate, endDate);

            if (performanceData == null || performanceData.Rows.Count == 0)
            {
                clsFormTheme.ShowInfo(this, $"No performance data available for {supplierName} in the last 30 days.", "Supplier Performance");
                return;
            }

            // Filter for selected supplier
            DataRow[] supplierRows = performanceData.Select($"SupplierID = {supplierID}");
            
            if (supplierRows.Length == 0)
            {
                clsFormTheme.ShowInfo(this, $"No sales data found for {supplierName} in the last 30 days.", "Supplier Performance");
                return;
            }

            // Show performance dialog
            using (Form perfForm = new Form
            {
                Text = $"Supplier Performance - {supplierName}",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            })
            {
                clsFormTheme.ApplyFormStyle(perfForm);
                clsFormTheme.CreateHeaderPanel(perfForm, "Supplier Performance", clsFormTheme.Icons.Chart);

                var mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    Padding = new Padding(20)
                };
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    DataSource = supplierRows.CopyToDataTable()
                };

                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductName",
                    HeaderText = "Product",
                    Width = 200
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "QuantitySold",
                    HeaderText = "Qty Sold",
                    Width = 80
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Revenue",
                    HeaderText = "Revenue",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OrderCount",
                    HeaderText = "Orders",
                    Width = 80
                });

                clsFormTheme.ApplyGridStyle(grid);

                var btnClose = new Button
                {
                    Text = "Close",
                    Width = 100,
                    Height = 35,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };
                clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
                btnClose.Click += (s, args) => perfForm.Close();

                var btnPanel = new Panel { Dock = DockStyle.Fill };
                btnPanel.Controls.Add(btnClose);
                btnClose.Location = new Point(540, 5);

                mainPanel.Controls.Add(grid, 0, 0);
                mainPanel.Controls.Add(btnPanel, 0, 1);

                perfForm.Controls.Add(mainPanel);
                perfForm.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            DisplayCurrentPage();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                DisplayCurrentPage();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            DataTable filteredTable = GetFilteredSuppliers();
            int totalPages = Math.Max(1, (int)Math.Ceiling(filteredTable.Rows.Count / (double)PageSize));

            if (_currentPage < totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
            }
        }

        private void FrmSuppliersManagment_Paint(object sender, PaintEventArgs e)
        {
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(DataGVSuppliers.Left - 10, DataGVSuppliers.Top - 10, DataGVSuppliers.Width + 20, DataGVSuppliers.Height + 20));
        }

        private async void frmSuppliersManagment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await RefreshGridDataAsync();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                btnAddSupplier.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
