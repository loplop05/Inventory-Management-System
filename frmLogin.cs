using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace InventoryManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtUsername);
            clsFormTheme.ApplyTextBoxStyle(_txtPassword);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnLogin, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(_btnExit, clsFormTheme.Icons.Exit);

            _txtPassword.UseSystemPasswordChar = true;
            _txtPassword.MaxLength = 20;
            _txtUsername.MaxLength = 50;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _txtUsername.Focus();
            ApplyLocalization();
            LoadSavedCredentials();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = _txtUsername.Text.Trim();
            string password = _txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                clsFormTheme.ShowWarning(this, "Please enter username.", "Login");
                _txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                clsFormTheme.ShowWarning(this, "Please enter password.", "Login");
                _txtPassword.Focus();
                return;
            }

            var user = clsUserManagement.Authenticate(username, password);

            if (user != null)
            {
                clsUserManagement.CurrentUser = user;
                clsAuditLog.LogAction("User Login", $"User {user.DisplayName} logged in as {user.Role}", "System");
                
                if (_chkRememberMe.Checked)
                {
                    SaveCredentials(username, password);
                }
                else
                {
                    ClearSavedCredentials();
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Invalid username or password.", "Login");
                _txtPassword.Clear();
                _txtPassword.Focus();
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
                _txtPassword.Focus();
            }
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Login");
            _lblTitle.Text = clsLanguageManager.GetString("Login");
            _lblSubtitle.Text = "Inventory Management System";
            _lblUsername.Text = clsLanguageManager.GetString("Username") + ":";
            _lblPassword.Text = clsLanguageManager.GetString("Password") + ":";
            _btnLogin.Text = clsLanguageManager.GetString("Login");
            _btnExit.Text = clsLanguageManager.GetString("Exit");
        }

        private void SaveCredentials(string username, string password)
        {
            try
            {
                MessageBox.Show($"Attempting to save: Username={username}, Password length={password.Length}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\InventoryManagementSystem"))
                {
                    if (key == null)
                    {
                        MessageBox.Show("Failed to create registry key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    key.SetValue("Username", username);
                    key.SetValue("Password", password);
                    
                    MessageBox.Show("Credentials saved to registry successfully!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving credentials: {ex.Message}\n\nStack trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSavedCredentials()
        {
            try
            {
                MessageBox.Show("Attempting to load saved credentials...", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\InventoryManagementSystem"))
                {
                    if (key == null)
                    {
                        MessageBox.Show("Registry key not found - no saved credentials exist", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                    string username = key.GetValue("Username") as string;
                    string password = key.GetValue("Password") as string;
                    
                    MessageBox.Show($"Loaded from registry: Username={username}, Password length={password?.Length ?? 0}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        _txtUsername.Text = username;
                        _txtPassword.Text = password;
                        _chkRememberMe.Checked = true;
                        
                        MessageBox.Show("Credentials loaded into form fields!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Saved credentials are empty", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading credentials: {ex.Message}\n\nStack trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSavedCredentials()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\InventoryManagementSystem", true))
                {
                    if (key != null)
                    {
                        key.DeleteValue("Username", false);
                        key.DeleteValue("Password", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing credentials: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
