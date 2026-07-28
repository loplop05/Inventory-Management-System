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
            clsFormTheme.CreateHeaderPanel(this, "Products", clsFormTheme.Icons.Products);

            btnAddProduct.Text = "Add";
            btnAddProduct.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddProduct, clsFormTheme.Icons.Add);

            btnDeleteProduct.Text = "Delete";
            btnDeleteProduct.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteProduct, clsFormTheme.Icons.Delete);

            btnUpdateProduct.Text = "Update";
            btnUpdateProduct.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdateProduct, clsFormTheme.Icons.Update);

            btnStockValuationReport.Text = "Stock Report";
            btnStockValuationReport.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(btnStockValuationReport, clsFormTheme.Icons.Chart);

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(_btnPreviousPage);

            _btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(_btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVProducts);

            _toolTip.SetToolTip(_txtSearch, "Search by product ID, name, barcode, category, or supplier.");
            _toolTip.SetToolTip(_btnRefresh, "Refresh the product list (F5).");
            _toolTip.SetToolTip(btnStockValuationReport, "Open the stock valuation report (Ctrl+R).");

            _lblEmptyState.Visible = false;
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

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _txtSearch.Enabled = !isLoading;
            btnAddProduct.Enabled = !isLoading;
            btnDeleteProduct.Enabled = !isLoading;
            btnUpdateProduct.Enabled = !isLoading;
            btnStockValuationReport.Enabled = !isLoading;
            _btnRefresh.Enabled = !isLoading;

            if (isLoading)
            {
                _lblEmptyState.Text = "Loading products...";
                _lblEmptyState.Visible = true;
                _btnPreviousPage.Enabled = false;
                _btnNextPage.Enabled = false;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredProducts()
        {
            DataTable filteredTable = _productsTable.Clone();
            string searchText = _txtSearch.Text.Trim();

            foreach (DataRow row in _productsTable.Rows)
            {
                string productID = row["ProductID"].ToString();
                string productName = row["ProductName"].ToString();
                string barcode = row["Barcode"].ToString();
                string categoryName = row["CategoryName"].ToString();
                string supplierName = row["SupplierName"].ToString();

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

            if (DataGVProducts.Columns.Contains("CategoryID"))
                DataGVProducts.Columns["CategoryID"].Visible = false;

            if (DataGVProducts.Columns.Contains("SupplierID"))
                DataGVProducts.Columns["SupplierID"].Visible = false;

            if (DataGVProducts.Columns.Contains("CategoryName"))
                DataGVProducts.Columns["CategoryName"].HeaderText = "Category";

            if (DataGVProducts.Columns.Contains("SupplierName"))
                DataGVProducts.Columns["SupplierName"].HeaderText = "Supplier";

            foreach (DataGridViewColumn column in DataGVProducts.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            bool hasRows = pageTable.Rows.Count > 0;
            _lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                _lblEmptyState.Text = string.IsNullOrWhiteSpace(_txtSearch.Text)
                    ? "No products found. Add your first product."
                    : "No products match your search.";
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

        private void btnStockValuationReport_Click(object sender, EventArgs e)
        {
            frmStockValuationReport frm = new frmStockValuationReport();
            frm.ShowDialog();
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

        private void FrmProductsManagment_Paint(object sender, PaintEventArgs e)
        {
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(DataGVProducts.Left - 10, DataGVProducts.Top - 10, DataGVProducts.Width + 20, DataGVProducts.Height + 20));
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
            else if (e.Control && e.KeyCode == Keys.R)
            {
                btnStockValuationReport.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void DataGVProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
