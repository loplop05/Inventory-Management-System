using System;
using System.Windows.Forms;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmChangePassword : Form
    {
        private int _userID;
        private string _displayName;

        public frmChangePassword(int userID, string displayName)
        {
            InitializeComponent();
            _userID = userID;
            _displayName = displayName;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Change Password", clsFormTheme.Icons.Refresh);

            clsFormTheme.ApplyTextBoxStyle(txtNewPassword);
            clsFormTheme.ApplyTextBoxStyle(txtConfirmPassword);

            clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            lblUserInfo.Text = $"Changing password for: {_displayName}";

            clsLanguageManager.ApplyLanguage(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Password is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            string errorMessage;
            if (clsUserData.ChangePassword(_userID, newPassword, out errorMessage))
            {
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to change password: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
