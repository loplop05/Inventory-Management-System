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

            btnAddCategory.Text = clsFormTheme.Icons.Add + "  Add";
            btnAddCategory.Font = new Font(clsFormTheme.IconFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddCategory);

            btnDeleteCategory.Text = clsFormTheme.Icons.Delete + "  Delete";
            btnDeleteCategory.Font = new Font(clsFormTheme.IconFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteCategory);

            btnUpdateCategory.Text = clsFormTheme.Icons.Update + "  Update";
            btnUpdateCategory.Font = new Font(clsFormTheme.IconFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdateCategory);

            btnRefresh.Text = clsFormTheme.Icons.Refresh + "  Refresh";
            btnRefresh.Font = new Font(clsFormTheme.IconFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh);

            btnBackToPrevPage.Text = clsFormTheme.Icons.Back + "  Back";
            btnBackToPrevPage.Font = new Font(clsFormTheme.IconFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(btnBackToPrevPage);

            btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(btnPreviousPage);

            btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVCategories);
            Paint += FrmCategoriesManagment_Paint;

            _toolTip.SetToolTip(txtSearch, "Search by category ID or name.");
            _toolTip.SetToolTip(btnRefresh, "Refresh the category list (F5).");

            lblEmptyState.Visible = false;
            KeyDown += frmCategoriesManagment_KeyDown;
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
            DataGVCategories.Enabled = !isLoading;
            txtSearch.Enabled = !isLoading;
            btnAddCategory.Enabled = !isLoading;
            btnDeleteCategory.Enabled = !isLoading;
            btnUpdateCategory.Enabled = !isLoading;
            btnRefresh.Enabled = !isLoading;

            if (isLoading)
            {
                lblEmptyState.Text = "Loading categories...";
                lblEmptyState.Visible = true;
                btnPreviousPage.Enabled = false;
                btnNextPage.Enabled = false;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredCategories()
        {
            DataTable filteredTable = _categoriesTable.Clone();
            string searchText = txtSearch.Text.Trim();

            foreach (DataRow row in _categoriesTable.Rows)
            {
                string categoryID = row["CategoryID"].ToString();
                string categoryName = row["CategoryName"].ToString();

                if (string.IsNullOrWhiteSpace(searchText) ||
                    categoryID.Contains(searchText) ||
                    categoryName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
            }

            return filteredTable;
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
            lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                lblEmptyState.Text = string.IsNullOrWhiteSpace(txtSearch.Text)
                    ? "No categories found. Add your first category."
                    : "No categories match your search.";
            }

            lblPageInfo.Text = rowCount == 0
                ? "No results"
                : $"Page {_currentPage} of {totalPages}";

            btnPreviousPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < totalPages;
        }

        private void btnBackToPrevPage_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void frmCategoriesManagment_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
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
