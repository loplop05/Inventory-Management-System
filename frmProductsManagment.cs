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

            // Wire sidebar navigation
            _sidebar.NavigationRequested += OnSidebarNavigation;
            _sidebar.SetActive("Inventory");

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await RefreshGridDataAsync(),
                onSearch: () => _txtSearch.Focus(),
                onAdd: () => _btnAddProduct_Click(null, null)
            );

            // Style buttons
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddProduct, clsFormTheme.Icons.Add);
            clsFormTheme.ApplySecondaryButtonStyle(_btnUpdateProduct, clsFormTheme.Icons.Update);
            clsFormTheme.ApplyDangerButtonStyle(_btnDeleteProduct, clsFormTheme.Icons.Delete);

            // Style search box
            clsFormTheme.ApplyTextBoxStyle(_txtSearch);

            // Style category filter
            clsFormTheme.ApplyComboBoxStyle(_cmbCategoryFilter);

            // Apply dark header grid style
            clsFormTheme.ApplyDarkHeaderGridStyle(DataGVProducts);

            _toolTip.SetToolTip(_txtSearch, "Search by product ID, name, barcode, category, or supplier.");

            clsSearchHelper.SetupAutoComplete(_txtSearch, "ProductsSearch");

            _lblEmptyState.Visible = false;
            KeyDown += frmProductsManagment_KeyDown;

            // Load categories into filter
            LoadCategories();

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Products Management");
            _lblPageTitle.Text = clsLanguageManager.GetString("Inventory");
            _btnAddProduct.Text = clsLanguageManager.GetString("Add Product");
            _btnUpdateProduct.Text = clsLanguageManager.GetString("Update");
            _btnDeleteProduct.Text = clsLanguageManager.GetString("Delete");
        }

        private void LoadCategories()
        {
            try
            {
                _cmbCategoryFilter.Items.Clear();
                _cmbCategoryFilter.Items.Add("All Categories");
                _cmbCategoryFilter.SelectedIndex = 0;

                DataTable categories = clsCategory.GetAllCategories();
                if (categories != null)
                {
                    foreach (DataRow row in categories.Rows)
                    {
                        _cmbCategoryFilter.Items.Add(row["CategoryName"]);
                    }
                }
            }
            catch
            {
                // Ignore errors loading categories
            }
        }

        private void OnSidebarNavigation(string screenKey)
        {
            switch (screenKey)
            {
                case "Dashboard":
                    var dashboardForm = new frmDashboard();
                    dashboardForm.Show();
                    this.Close();
                    break;
                case "POS":
                    var posForm = new frmPOS();
                    posForm.Show();
                    this.Close();
                    break;
                case "Inventory":
                    // Already on Inventory
                    break;
                case "Orders":
                    var receiptForm = new frmReceiptSearch();
                    receiptForm.Show();
                    this.Close();
                    break;
                case "Reports":
                    var reportForm = new frmDailyReport();
                    reportForm.Show();
                    this.Close();
                    break;
                case "Support":
                    // Help system integration - to be implemented
                    break;
            }
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                string errorMessage;
                _productsTable = await Task.Run(() => 
                {
                    DataTable dt;
                    clsProduct.GetAllProducts(out dt, out errorMessage);
                    return dt;
                });
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                _productsTable = new DataTable();
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
            DataGVProducts.Enabled = !isLoading;
            _txtSearch.Enabled = !isLoading;
            _cmbCategoryFilter.Enabled = !isLoading;
            _btnAddProduct.Enabled = !isLoading;

            if (isLoading)
            {
                _lblEmptyState.Text = "Loading products...";
                _lblEmptyState.Visible = true;
            }
            else
            {
                DisplayCurrentPage();
            }
        }

        private DataTable GetFilteredProducts()
        {
            DataTable filtered = _productsTable.Copy();

            // Apply search filter
            string searchText = _txtSearch.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                DataView view = clsSearchHelper.QuickSearch(filtered, searchText, "ProductID", "ProductName", "Barcode", "CategoryName", "SupplierName");
                filtered = view.ToTable();
            }

            // Apply category filter
            if (_cmbCategoryFilter.SelectedIndex > 0)
            {
                string selectedCategory = _cmbCategoryFilter.SelectedItem.ToString();
                DataRow[] rows = filtered.Select("CategoryName = '" + selectedCategory + "'");
                if (rows.Length > 0)
                {
                    filtered = rows.CopyToDataTable();
                }
                else
                {
                    filtered = filtered.Clone();
                }
            }

            return filtered;
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
            DataGVProducts.AutoGenerateColumns = false;

            // Configure columns
            if (DataGVProducts.Columns.Count == 0)
            {
                DataGVProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductID",
                    HeaderText = "ID",
                    Name = "colID",
                    Width = 60
                });

                DataGVProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductName",
                    HeaderText = "Product",
                    Name = "colProduct",
                    Width = 200
                });

                DataGVProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CategoryName",
                    HeaderText = "Category",
                    Name = "colCategory",
                    Width = 120
                });

                DataGVProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Quantity",
                    HeaderText = "Stock",
                    Name = "colStock",
                    Width = 80
                });

                DataGVProducts.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Price",
                    HeaderText = "Price",
                    Name = "colPrice",
                    Width = 80
                });
            }

            // Apply stock pill styling to Stock column
            foreach (DataGridViewRow row in DataGVProducts.Rows)
            {
                if (row.Cells["colStock"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["colStock"].Value);
                    row.Cells["colStock"].Style.BackColor = quantity > 5 ? clsFormTheme.CurrentSuccessLightColor :
                                                    quantity >= 1 ? clsFormTheme.CurrentWarningLightColor :
                                                    clsFormTheme.CurrentDangerLightColor;
                    row.Cells["colStock"].Style.ForeColor = quantity > 5 ? clsFormTheme.CurrentSuccessColor :
                                                    quantity >= 1 ? clsFormTheme.CurrentWarningColor :
                                                    clsFormTheme.CurrentDangerColor;
                }
            }

            bool hasRows = pageTable.Rows.Count > 0;
            _lblEmptyState.Visible = !hasRows;

            if (!hasRows)
            {
                _lblEmptyState.Text = string.IsNullOrWhiteSpace(_txtSearch.Text)
                    ? "No products found. Add your first product."
                    : "No products match your search.";
            }
        }

        private void btnBackToPrevPage_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void frmProductsManagment_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
            
            // Action-level permission check: Delete is Admin-only
            if (!clsUserManagement.IsAdmin)
            {
                _btnDeleteProduct.Visible = false;
            }
        }

        private async void _btnAddProduct_Click(object sender, EventArgs e)
        {
            frmAddProduct frm = new frmAddProduct();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void _btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (DataGVProducts.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowInfo(this, "Please select a product to update.", "No Selection");
                return;
            }

            DataGridViewRow selectedRow = DataGVProducts.SelectedRows[0];
            int productId = Convert.ToInt32(selectedRow.Cells["colID"].Value);

            frmUpdateProduct frm = new frmUpdateProduct();
            frm.SelectedProductID = productId;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridDataAsync();
            }
        }

        private async void _btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (DataGVProducts.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowInfo(this, "Please select a product to delete.", "No Selection");
                return;
            }

            DataGridViewRow selectedRow = DataGVProducts.SelectedRows[0];
            int productId = Convert.ToInt32(selectedRow.Cells["colID"].Value);
            string productName = selectedRow.Cells["colProduct"].Value.ToString();

            DialogResult result = clsFormTheme.ShowConfirm(
                this,
                $"Are you sure you want to delete '{productName}'?",
                "Confirm Delete");

            if (result == DialogResult.Yes)
            {
                bool isDeleted = clsProduct.DeleteProduct(productId);
                if (isDeleted)
                {
                    clsFormTheme.ShowSuccess(this, "Product deleted successfully.", "Success");
                    await RefreshGridDataAsync();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to delete product.", "Error");
                }
            }
        }

        private void _txtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            DisplayCurrentPage();
        }

        private void _cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            DisplayCurrentPage();
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
                _btnAddProduct_Click(null, null);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

    }
}
