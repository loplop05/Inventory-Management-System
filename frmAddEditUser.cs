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
            clsFormTheme.CreateHeaderPanel(this, _userID.HasValue ? "Edit User" : "Add User", clsFormTheme.Icons.User);

            clsFormTheme.ApplyTextBoxStyle(txtUsername);
            clsFormTheme.ApplyTextBoxStyle(txtDisplayName);
            clsFormTheme.ApplyTextBoxStyle(txtPassword);
            clsFormTheme.ApplyTextBoxStyle(txtConfirmPassword);

            clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            cmbRole.Items.AddRange(new object[] { "Admin", "Cashier" });
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
                MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Display name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDisplayName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Role is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return;
            }

            if (role != "Admin" && role != "Cashier")
            {
                MessageBox.Show("Role must be either 'Admin' or 'Cashier'.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string errorMessage;

            if (_userID.HasValue)
            {
                // Update existing user
                if (clsUserData.UpdateUser(_userID.Value, displayName, role, out errorMessage))
                {
                    MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Failed to update user: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Add new user
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Password is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return;
                }

                if (clsUserData.AddUser(username, password, displayName, role, out errorMessage))
                {
                    MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Failed to add user: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
