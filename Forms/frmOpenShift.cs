using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmOpenShift : Form
    {
        private bool _isOpening = false;

        public frmOpenShift()
        {
            InitializeComponent();
        }

        private void frmOpenShift_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtStartingCash);
            
            _btnOpen.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnOpen, clsFormTheme.Icons.Check);
            
            _btnSkip.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnSkip, clsFormTheme.Icons.Cancel);
            
            _btnCancel.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel, clsFormTheme.Icons.Close);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            // Display cashier info
            if (clsUserManagement.CurrentUser != null)
            {
                _lblCashierName.Text = "Cashier: " + clsUserManagement.CurrentUser.Username;
            }
            
            _lblDate.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
            
            // Hide skip button for non-admin/manager users
            if (!clsUserManagement.IsManager && !clsUserManagement.IsAdmin)
            {
                _btnSkip.Visible = false;
            }
            
            _btnOpen.Click += _btnOpen_Click;
            _btnSkip.Click += _btnSkip_Click;
            _btnCancel.Click += (s, ev) => Close();
            
            _txtStartingCash.TextChanged += _txtStartingCash_TextChanged;
        }

        private void ApplyLocalization()
        {
            _lblTitle.Text = clsLanguageManager.GetString("Open Shift");
            _lblStartingCash.Text = clsLanguageManager.GetString("Starting Cash");
            _btnOpen.Text = clsLanguageManager.GetString("Open Shift");
            _btnSkip.Text = clsLanguageManager.GetString("Skip");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = clsLanguageManager.GetString("Open Shift");
        }

        private void _txtStartingCash_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private bool ValidateInput()
        {
            bool isValid = decimal.TryParse(_txtStartingCash.Text, out decimal startingCash) && startingCash >= 0;
            _btnOpen.Enabled = isValid;
            return isValid;
        }

        private void _btnOpen_Click(object sender, EventArgs e)
        {
            if (_isOpening)
                return;
            
            if (!ValidateInput())
            {
                clsFormTheme.ShowWarning(this, "Please enter a valid non-negative starting cash amount.", "Validation Error");
                _txtStartingCash.Focus();
                return;
            }
            
            decimal startingCash = decimal.Parse(_txtStartingCash.Text);
            _isOpening = true;
            _btnOpen.Enabled = false;
            
            try
            {
                string errorMessage;
                int shiftID = clsShift.OpenShift(clsUserManagement.CurrentUser.UserID, startingCash, out errorMessage);
                
                if (shiftID > 0)
                {
                    clsFormTheme.ShowSuccess(this, $"Shift #{shiftID} opened successfully with ${startingCash:F2} starting cash.", "Success");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, errorMessage, "Error");
                    _btnOpen.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error opening shift: " + ex.Message, "Error");
                _btnOpen.Enabled = true;
            }
            finally
            {
                _isOpening = false;
            }
        }

        private void _btnSkip_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
