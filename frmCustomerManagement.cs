using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCustomerManagement : Form
    {
        private DataTable _customersTable = new DataTable();
        private const int PageSize = 10;
        private int _currentPage = 1;
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmCustomerManagement()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Customer Management", clsFormTheme.Icons.User);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await RefreshGridDataAsync(),
                onSearch: () => _txtSearch.Focus(),
                onAdd: () => btnAddCustomer_Click(null, null)
            );

            btnAddCustomer.Text = "Add";
            btnAddCustomer.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddCustomer, clsFormTheme.Icons.Add);

            btnEditCustomer.Text = "Edit";
            btnEditCustomer.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnEditCustomer, clsFormTheme.Icons.Update);

            btnDeleteCustomer.Text = "Delete";
            btnDeleteCustomer.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeleteCustomer, clsFormTheme.Icons.Delete);

            btnViewDetails.Text = "View Details";
            btnViewDetails.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(btnViewDetails);

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(_btnPreviousPage);

            _btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(_btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVCustomers);

            _toolTip.SetToolTip(_txtSearch, "Search by phone number or customer name.");
            _toolTip.SetToolTip(_btnRefresh, "Refresh the customer list (F5).");

            _lblEmptyState.Visible = false;
            KeyDown += frmCustomerManagement_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Customer Management");
            btnAddCustomer.Text = clsLanguageManager.GetString("Add");
            btnEditCustomer.Text = clsLanguageManager.GetString("Edit");
            btnDeleteCustomer.Text = clsLanguageManager.GetString("Delete");
            btnViewDetails.Text = clsLanguageManager.GetString("View Details");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnPreviousPage.Text = clsLanguageManager.GetString("Previous");
            _btnNextPage.Text = clsLanguageManager.GetString("Next");
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _customersTable = await Task.Run(() => clsCustomer.GetAllCustomers());
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading customers: " + ex.Message, "Error");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void DisplayCurrentPage()
        {
            if (_customersTable.Rows.Count == 0)
            {
                DataGVCustomers.DataSource = null;
                _lblEmptyState.Visible = true;
                _lblPageInfo.Text = "Page 0 of 0";
                _btnPreviousPage.Enabled = false;
                _btnNextPage.Enabled = false;
                return;
            }

            _lblEmptyState.Visible = false;

            var pagedData = _customersTable.AsEnumerable()
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .CopyToDataTable();

            DataGVCustomers.DataSource = pagedData;

            // Hide internal columns
            if (DataGVCustomers.Columns.Contains("CustomerID"))
                DataGVCustomers.Columns["CustomerID"].Visible = false;
            if (DataGVCustomers.Columns.Contains("CategoryID"))
                DataGVCustomers.Columns["CategoryID"].Visible = false;

            // Rename columns for display
            if (DataGVCustomers.Columns.Contains("PhoneNumber"))
                DataGVCustomers.Columns["PhoneNumber"].HeaderText = "Phone";
            if (DataGVCustomers.Columns.Contains("CustomerName"))
                DataGVCustomers.Columns["CustomerName"].HeaderText = "Name";
            if (DataGVCustomers.Columns.Contains("LoyaltyPoints"))
                DataGVCustomers.Columns["LoyaltyPoints"].HeaderText = "Points";
            if (DataGVCustomers.Columns.Contains("TotalSpent"))
                DataGVCustomers.Columns["TotalSpent"].HeaderText = "Total Spent";
            if (DataGVCustomers.Columns.Contains("Tier"))
                DataGVCustomers.Columns["Tier"].HeaderText = "Tier";
            if (DataGVCustomers.Columns.Contains("LastPurchaseDate"))
                DataGVCustomers.Columns["LastPurchaseDate"].HeaderText = "Last Purchase";

            int totalPages = (int)Math.Ceiling((double)_customersTable.Rows.Count / PageSize);
            _lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";

            _btnPreviousPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < totalPages;
        }

        private void SetLoadingState(bool isLoading)
        {
            _isLoading = isLoading;
            DataGVCustomers.Enabled = !isLoading;
            _btnRefresh.Enabled = !isLoading;
            btnAddCustomer.Enabled = !isLoading;
            btnEditCustomer.Enabled = !isLoading;
            btnDeleteCustomer.Enabled = !isLoading;
            btnViewDetails.Enabled = !isLoading;
            _txtSearch.Enabled = !isLoading;
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
        }

        private void frmCustomerManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _txtSearch.Focused)
            {
                SearchCustomers();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private async void _btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private void _txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtSearch.Text))
            {
                _customersTable = clsCustomer.GetAllCustomers();
                _currentPage = 1;
                DisplayCurrentPage();
            }
        }

        private void SearchCustomers()
        {
            string searchTerm = _txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                _customersTable = clsCustomer.GetAllCustomers();
            }
            else
            {
                var allCustomers = clsCustomer.GetAllCustomers();
                var filtered = allCustomers.AsEnumerable()
                    .Where(row =>
                        row["PhoneNumber"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        row["CustomerName"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .CopyToDataTable();
                _customersTable = filtered;
            }
            _currentPage = 1;
            DisplayCurrentPage();
        }

        private void _btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                DisplayCurrentPage();
            }
        }

        private void _btnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_customersTable.Rows.Count / PageSize);
            if (_currentPage < totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
            }
        }

        private async void frmCustomerManagement_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (var form = new frmAddEditCustomer())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await RefreshGridDataAsync();
                }
            }
        }

        private async void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (DataGVCustomers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a customer to edit.", "Edit Customer");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVCustomers.SelectedRows[0].DataBoundItem).Row;
            int customerID = Convert.ToInt32(selectedRow["CustomerID"]);
            string phoneNumber = selectedRow["PhoneNumber"].ToString();
            string customerName = selectedRow["CustomerName"].ToString();

            using (var form = new frmAddEditCustomer(customerID, phoneNumber, customerName))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await RefreshGridDataAsync();
                }
            }
        }

        private async void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (DataGVCustomers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a customer to delete.", "Delete Customer");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVCustomers.SelectedRows[0].DataBoundItem).Row;
            int customerID = Convert.ToInt32(selectedRow["CustomerID"]);
            string customerName = selectedRow["CustomerName"].ToString();

            var result = clsFormTheme.ShowYesNo(this, $"Are you sure you want to delete customer '{customerName}'?", 
                "Delete Customer");

            if (result == DialogResult.Yes)
            {
                string errorMessage;
                if (clsCustomer.DeleteCustomer(customerID, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "Customer deleted successfully.", "Success");
                    await RefreshGridDataAsync();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to delete customer: " + errorMessage, "Error");
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (DataGVCustomers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a customer to view details.", "View Details");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVCustomers.SelectedRows[0].DataBoundItem).Row;
            int customerID = Convert.ToInt32(selectedRow["CustomerID"]);

            using (var form = new frmCustomerDetails(customerID))
            {
                form.ShowDialog(this);
            }
        }
    }
}
