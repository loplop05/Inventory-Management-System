using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCategoriesManagment : Form
    {
        private DataTable _categoriesTable = new DataTable();
        private const int PageSize = 10;
        private int _currentPage = 1;
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmCategoriesManagment()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Categories", clsFormTheme.Icons.Categories);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await RefreshGridDataAsync(),
                onSearch: () => _txtSearch.Focus(),
                onAdd: () => btnAddCategory_Click(null, null)
            );

            btnAddCategory.Text = "Add";
            btnAddCategory.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddCategory, clsFormTheme.Icons.Add);

            btnDeleteCategory.Text = "Delete";
            btnDeleteCategory.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteCategory, clsFormTheme.Icons.Delete);

            btnUpdateCategory.Text = "Update";
            btnUpdateCategory.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdateCategory, clsFormTheme.Icons.Update);

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(_btnPreviousPage);

            _btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(_btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVCategories);

            _toolTip.SetToolTip(_txtSearch, "Search by category ID or name.");
            _toolTip.SetToolTip(_btnRefresh, "Refresh the category list (F5).");

            clsSearchHelper.SetupAutoComplete(_txtSearch, "CategoriesSearch");

            _lblEmptyState.Visible = false;
            KeyDown += frmCategoriesManagment_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Categories Management");
            btnAddCategory.Text = clsLanguageManager.GetString("Add");
            btnDeleteCategory.Text = clsLanguageManager.GetString("Delete");
            btnUpdateCategory.Text = clsLanguageManager.GetString("Update");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnPreviousPage.Text = clsLanguageManager.GetString("Previous");
            _btnNextPage.Text = clsLanguageManager.GetString("Next");
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _categoriesTable = await Task.Run(() => clsCategory.GetAllCategories());
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                _categoriesTable = new DataTable();
                DisplayCurrentPage();

                clsFormTheme.ShowError(this, ex.Message, "Error");
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
            DataGVCategories.Enabled = !isLoading;
            _txtSearch.Enabled = !isLoading;
            btnAddCategory.Enabled = !isLoading;
            btnDeleteCategory.Enabled = !isLoading;
            btnUpdateCategory.Enabled = !isLoading;
            _btnRefresh.Enabled = !isLoading;

            if (isLoading)
            {
                _lblEmptyState.Text = "Loading categories...";
                _lblEmptyState.Visible = true;
                _btnPreviousPage.Enabled = false;
                _btnNextPage.Enabled = false;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredCategories()
        {
            string searchText = _txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
                return _categoriesTable;

            DataView view = clsSearchHelper.QuickSearch(_categoriesTable, searchText, "CategoryID", "CategoryName");
            return view.ToTable();
        }

        private void DisplayCurrentPage()
        {
            if (_isLoading)
                return;

            DataTable filteredTable = GetFilteredCategories();
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

            DataGVCategories.DataSource = pageTable;

            foreach (DataGridViewColumn column in DataGVCategories.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            bool hasRows = pageTable.Rows.Count > 0;
            _lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                _lblEmptyState.Text = string.IsNullOrWhiteSpace(_txtSearch.Text)
                    ? "No categories found. Add your first category."
                    : "No categories match your search.";
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

        private async void frmCategoriesManagment_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
            
            // Action-level permission check: Delete is Admin-only
            if (!clsUserManagement.IsAdmin)
            {
                btnDeleteCategory.Visible = false;
            }
        }

        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmAddCategory frm = new frmAddCategory();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            frmDeleteCategory frm = new frmDeleteCategory();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            frmUpdateCategory frm = new frmUpdateCategory();

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
            DataTable filteredTable = GetFilteredCategories();
            int totalPages = Math.Max(1, (int)Math.Ceiling(filteredTable.Rows.Count / (double)PageSize));

            if (_currentPage < totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
            }
        }

        private void FrmCategoriesManagment_Paint(object sender, PaintEventArgs e)
        {
            // Draw a card around the grid area
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(DataGVCategories.Left - 10, DataGVCategories.Top - 10, DataGVCategories.Width + 20, DataGVCategories.Height + 20));
        }

        private async void frmCategoriesManagment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await RefreshGridDataAsync();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                btnAddCategory.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
