using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmUserManagement : Form
    {
        private DataTable _usersTable = new DataTable();
        private const int PageSize = 10;
        private int _currentPage = 1;
        private bool _isLoading = false;
        private ToolTip _toolTip = new ToolTip();

        public frmUserManagement()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: async () => await RefreshGridDataAsync(),
                onSearch: () => _txtSearch.Focus(),
                onAdd: () => btnAddUser_Click(null, null)
            );

            btnAddUser.Text = "Add";
            btnAddUser.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAddUser, clsFormTheme.Icons.Add);

            btnEditUser.Text = "Edit";
            btnEditUser.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnEditUser, clsFormTheme.Icons.Update);

            btnDeactivateUser.Text = "Deactivate";
            btnDeactivateUser.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(btnDeactivateUser, clsFormTheme.Icons.Delete);

            btnChangePassword.Text = "Change Password";
            btnChangePassword.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(btnChangePassword, clsFormTheme.Icons.Refresh);

            btnManagePermissions.Text = "Permissions";
            btnManagePermissions.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnManagePermissions, clsFormTheme.Icons.User);

            _btnRefresh.Text = "Refresh";
            _btnRefresh.Font = new Font(clsFormTheme.MainFontName, 11F);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            _btnPreviousPage.Text = "\u2039  Prev";
            clsFormTheme.ApplySecondaryButtonStyle(_btnPreviousPage);

            _btnNextPage.Text = "Next  \u203A";
            clsFormTheme.ApplySecondaryButtonStyle(_btnNextPage);

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyGridStyle(DataGVUsers);

            _toolTip.SetToolTip(_txtSearch, "Search by username or display name.");
            _toolTip.SetToolTip(_btnRefresh, "Refresh the user list (F5).");

            _lblEmptyState.Visible = false;
            KeyDown += frmUserManagement_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("User Management");
            btnAddUser.Text = clsLanguageManager.GetString("Add");
            btnEditUser.Text = clsLanguageManager.GetString("Edit");
            btnDeactivateUser.Text = clsLanguageManager.GetString("Deactivate");
            btnChangePassword.Text = clsLanguageManager.GetString("Change Password");
            btnManagePermissions.Text = clsLanguageManager.GetString("Permissions");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnPreviousPage.Text = clsLanguageManager.GetString("Previous");
            _btnNextPage.Text = clsLanguageManager.GetString("Next");
        }

        private async Task RefreshGridDataAsync()
        {
            SetLoadingState(true);

            try
            {
                _usersTable = await Task.Run(() => clsUserData.GetAllUsers());
                _currentPage = 1;
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading users: " + ex.Message, "Error");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void DisplayCurrentPage()
        {
            if (_usersTable.Rows.Count == 0)
            {
                DataGVUsers.DataSource = null;
                _lblEmptyState.Visible = true;
                _lblPageInfo.Text = "Page 0 of 0";
                _btnPreviousPage.Enabled = false;
                _btnNextPage.Enabled = false;
                return;
            }

            _lblEmptyState.Visible = false;

            var pagedData = _usersTable.AsEnumerable()
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .CopyToDataTable();

            DataGVUsers.DataSource = pagedData;

            int totalPages = (int)Math.Ceiling((double)_usersTable.Rows.Count / PageSize);
            _lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";

            _btnPreviousPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < totalPages;
        }

        private void SetLoadingState(bool isLoading)
        {
            _isLoading = isLoading;
            DataGVUsers.Enabled = !isLoading;
            _btnRefresh.Enabled = !isLoading;
            btnAddUser.Enabled = !isLoading;
            btnEditUser.Enabled = !isLoading;
            btnDeactivateUser.Enabled = !isLoading;
            btnChangePassword.Enabled = !isLoading;
            _txtSearch.Enabled = !isLoading;
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
        }

        private void frmUserManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _txtSearch.Focused)
            {
                SearchUsers();
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
                _usersTable = clsUserData.GetAllUsers();
                _currentPage = 1;
                DisplayCurrentPage();
            }
        }

        private void SearchUsers()
        {
            string searchTerm = _txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                _usersTable = clsUserData.GetAllUsers();
            }
            else
            {
                var allUsers = clsUserData.GetAllUsers();
                var filtered = allUsers.AsEnumerable()
                    .Where(row => 
                        row["Username"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        row["DisplayName"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .CopyToDataTable();
                _usersTable = filtered;
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
            int totalPages = (int)Math.Ceiling((double)_usersTable.Rows.Count / PageSize);
            if (_currentPage < totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
            }
        }

        private async void frmUserManagement_Load(object sender, EventArgs e)
        {
            await RefreshGridDataAsync();
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var form = new frmAddEditUser())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await RefreshGridDataAsync();
                }
            }
        }

        private async void btnEditUser_Click(object sender, EventArgs e)
        {
            if (DataGVUsers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a user to edit.", "Edit User");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVUsers.SelectedRows[0].DataBoundItem).Row;
            int userID = Convert.ToInt32(selectedRow["UserID"]);
            string username = selectedRow["Username"].ToString();
            string displayName = selectedRow["DisplayName"].ToString();
            string role = selectedRow["Role"].ToString();

            using (var form = new frmAddEditUser(userID, username, displayName, role))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await RefreshGridDataAsync();
                }
            }
        }

        private async void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            if (DataGVUsers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a user to deactivate.", "Deactivate User");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVUsers.SelectedRows[0].DataBoundItem).Row;
            int userID = Convert.ToInt32(selectedRow["UserID"]);
            string displayName = selectedRow["DisplayName"].ToString();

            var result = clsFormTheme.ShowYesNo(this, $"Are you sure you want to deactivate user '{displayName}'?", 
                "Deactivate User");

            if (result == DialogResult.Yes)
            {
                string errorMessage;
                if (clsUserData.DeactivateUser(userID, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "User deactivated successfully.", "Success");
                    await RefreshGridDataAsync();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to deactivate user: " + errorMessage, "Error");
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (DataGVUsers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a user to change password for.", "Change Password");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVUsers.SelectedRows[0].DataBoundItem).Row;
            int userID = Convert.ToInt32(selectedRow["UserID"]);
            string displayName = selectedRow["DisplayName"].ToString();

            using (var form = new frmChangePassword(userID, displayName))
            {
                form.ShowDialog(this);
            }
        }

        private void btnManagePermissions_Click(object sender, EventArgs e)
        {
            if (DataGVUsers.SelectedRows.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select a user to manage permissions for.", "Manage Permissions");
                return;
            }

            DataRow selectedRow = ((DataRowView)DataGVUsers.SelectedRows[0].DataBoundItem).Row;
            int userID = Convert.ToInt32(selectedRow["UserID"]);
            string displayName = selectedRow["DisplayName"].ToString();
            int roleID = Convert.ToInt32(selectedRow["RoleID"]);

            using (var form = new frmManagePermissions(userID, displayName, roleID))
            {
                form.ShowDialog(this);
            }
        }
    }
}
