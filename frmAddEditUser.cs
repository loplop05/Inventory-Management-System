using System;
using System.Windows.Forms;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAddEditUser : Form
    {
        private int? _userID;
        private string _existingUsername;

        public frmAddEditUser()
        {
            InitializeComponent();
            _userID = null;
            _existingUsername = null;
        }

        public frmAddEditUser(int userID, string username, string displayName, string role)
        {
            InitializeComponent();
            _userID = userID;
            _existingUsername = username;
            Text = "Edit User";
            txtUsername.Text = username;
            txtUsername.ReadOnly = true; // Username cannot be changed
            txtDisplayName.Text = displayName;
            cmbRole.Text = role;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);

            clsFormTheme.ApplyTextBoxStyle(txtUsername);
            clsFormTheme.ApplyTextBoxStyle(txtDisplayName);
            clsFormTheme.ApplyTextBoxStyle(txtPassword);
            clsFormTheme.ApplyTextBoxStyle(txtConfirmPassword);

            clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            cmbRole.Items.AddRange(new object[] { "Admin", "Manager", "Cashier" });
            if (cmbRole.Items.Count > 0)
                cmbRole.SelectedIndex = 0;

            if (!_userID.HasValue)
            {
                // New user - password fields required
                tableLayoutPanel.RowStyles[3].Height = 40;
                tableLayoutPanel.RowStyles[4].Height = 40;
                lblPassword.Visible = true;
                lblConfirmPassword.Visible = true;
                txtPassword.Visible = true;
                txtConfirmPassword.Visible = true;
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;
            }
            else
            {
                // Edit user - password fields hidden (use Change Password button instead)
                tableLayoutPanel.RowStyles[3].Height = 0;
                tableLayoutPanel.RowStyles[4].Height = 0;
                lblPassword.Visible = false;
                lblConfirmPassword.Visible = false;
                txtPassword.Visible = false;
                txtConfirmPassword.Visible = false;
            }

            clsLanguageManager.ApplyLanguage(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string role = cmbRole.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                clsFormTheme.ShowWarning(this, "Username is required.", "Validation");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                clsFormTheme.ShowWarning(this, "Display name is required.", "Validation");
                txtDisplayName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                clsFormTheme.ShowWarning(this, "Role is required.", "Validation");
                cmbRole.Focus();
                return;
            }

            if (role != "Admin" && role != "Cashier")
            {
                clsFormTheme.ShowWarning(this, "Role must be either 'Admin' or 'Cashier'.", "Validation");
                return;
            }

            string errorMessage;

            if (_userID.HasValue)
            {
                // Update existing user
                if (clsUserData.UpdateUser(_userID.Value, displayName, role, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "User updated successfully.", "Success");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to update user: " + errorMessage, "Error");
                }
            }
            else
            {
                // Add new user
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;

                if (string.IsNullOrWhiteSpace(password))
                {
                    clsFormTheme.ShowWarning(this, "Password is required.", "Validation");
                    txtPassword.Focus();
                    return;
                }

                if (password.Length < 6)
                {
                    clsFormTheme.ShowWarning(this, "Password must be at least 6 characters long.", "Validation");
                    txtPassword.Focus();
                    return;
                }

                if (password != confirmPassword)
                {
                    clsFormTheme.ShowWarning(this, "Passwords do not match.", "Validation");
                    txtConfirmPassword.Focus();
                    return;
                }

                if (clsUserData.AddUser(username, password, displayName, role, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "User added successfully.", "Success");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to add user: " + errorMessage, "Error");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
