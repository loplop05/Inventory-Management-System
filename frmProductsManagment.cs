using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmProductsManagment : Form
    {
        private DataTable _productsTable = new DataTable();
        private const int PageSize = 10;
        private int _currentPage = 1;
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmProductsManagment()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddProduct);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteProduct);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdateProduct);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh);
            clsFormTheme.ApplySecondaryButtonStyle(btnBackToPrevPage);
            clsFormTheme.ApplySecondaryButtonStyle(btnPreviousPage);
            clsFormTheme.ApplySecondaryButtonStyle(btnNextPage);
            clsFormTheme.ApplyTextBoxStyle(txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVProducts);

            _toolTip.SetToolTip(txtSearch, "Search by product ID, name, barcode, category, or supplier.");
            _toolTip.SetToolTip(btnRefresh, "Refresh the product list (F5).");

            lblEmptyState.Visible = false;
            KeyDown += frmProductsManagment_KeyDown;
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _productsTable = await Task.Run(() => clsProduct.GetAllProducts());
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                _productsTable = new DataTable();
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
            DataGVProducts.Enabled = !isLoading;
            txtSearch.Enabled = !isLoading;
            btnAddProduct.Enabled = !isLoading;
            btnDeleteProduct.Enabled = !isLoading;
            btnUpdateProduct.Enabled = !isLoading;
            btnRefresh.Enabled = !isLoading;

            if (isLoading)
            {
                lblEmptyState.Text = "Loading products...";
                lblEmptyState.Visible = true;
                btnPreviousPage.Enabled = false;
                btnNextPage.Enabled = false;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredProducts()
        {
            DataTable filteredTable = _productsTable.Clone();
            string searchText = txtSearch.Text.Trim();

            foreach (DataRow row in _productsTable.Rows)
            {
                string productID = row["ProductID"].ToString();
                string productName = row["ProductName"].ToString();
                string barcode = row["Barcode"].ToString();
                // Assuming CategoryName and SupplierName are available in the DataTable from GetAllProducts
                // If not, this would require joining with Category and Supplier tables in DAL
                string categoryName = row["CategoryID"].ToString(); // Placeholder, ideally would be CategoryName
                string supplierName = row["SupplierID"].ToString(); // Placeholder, ideally would be SupplierName

                if (string.IsNullOrWhiteSpace(searchText) ||
                    productID.Contains(searchText) ||
                    productName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    barcode.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    categoryName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    supplierName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
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

            DataTable filteredTable = GetFilteredProducts();
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

            DataGVProducts.DataSource = pageTable;

            foreach (DataGridViewColumn column in DataGVProducts.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            bool hasRows = pageTable.Rows.Count > 0;
            lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                lblEmptyState.Text = string.IsNullOrWhiteSpace(txtSearch.Text)
                    ? "No products found. Add your first product."
                    : "No products match your search.";
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

        private async void frmProductsManagment_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmAddProduct frm = new frmAddProduct();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            frmDeleteProduct frm = new frmDeleteProduct();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            frmUpdateProduct frm = new frmUpdateProduct();

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
            DataTable filteredTable = GetFilteredProducts();
            int totalPages = Math.Max(1, (int)Math.Ceiling(filteredTable.Rows.Count / (double)PageSize));

            if (_currentPage < totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
            }
        }

        private async void frmProductsManagment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await RefreshGridDataAsync();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                btnAddProduct.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
