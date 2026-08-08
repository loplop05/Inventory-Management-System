using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Login", clsFormTheme.Icons.User);
            clsFormTheme.ApplyTextBoxStyle(txtUsername);
            clsFormTheme.ApplyTextBoxStyle(txtPassword);
            clsFormTheme.ApplyPrimaryButtonStyle(btnLogin, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(btnExit, clsFormTheme.Icons.Exit);

            txtPassword.UseSystemPasswordChar = true;
            txtPassword.MaxLength = 20;
            txtUsername.MaxLength = 50;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            ApplyLocalization();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                clsFormTheme.ShowWarning(this, "Please enter username.", "Login");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                clsFormTheme.ShowWarning(this, "Please enter password.", "Login");
                txtPassword.Focus();
                return;
            }

            var user = clsUserManagement.Authenticate(username, password);

            if (user != null)
            {
                clsUserManagement.CurrentUser = user;
                clsAuditLog.LogAction("User Login", $"User {user.DisplayName} logged in as {user.Role}", "System");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Invalid username or password.", "Login");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Login");
            lblUsername.Text = clsLanguageManager.GetString("Username") + ":";
            lblPassword.Text = clsLanguageManager.GetString("Password") + ":";
            btnLogin.Text = clsLanguageManager.GetString("Login");
            btnExit.Text = clsLanguageManager.GetString("Exit");
        }
    }
}
