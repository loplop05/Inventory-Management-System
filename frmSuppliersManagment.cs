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
            EventHandler onLanguageChanged = (s, e) => clsLanguageManager.ApplyLanguage(this);
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
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
