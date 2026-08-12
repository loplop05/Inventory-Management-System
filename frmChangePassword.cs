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
                clsFormTheme.ShowWarning(this, "Password is required.", "Validation");
                txtNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                clsFormTheme.ShowWarning(this, "Password must be at least 6 characters long.", "Validation");
                txtNewPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                clsFormTheme.ShowWarning(this, "Passwords do not match.", "Validation");
                txtConfirmPassword.Focus();
                return;
            }

            string errorMessage;
            if (clsUserData.ChangePassword(_userID, newPassword, out errorMessage))
            {
                clsFormTheme.ShowSuccess(this, "Password changed successfully.", "Success");
                DialogResult = DialogResult.OK;
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to change password: " + errorMessage, "Error");
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
