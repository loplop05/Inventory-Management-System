using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAdjustPoints : Form
    {
        private int _customerID;

        public frmAdjustPoints(int customerID)
        {
            InitializeComponent();
            _customerID = customerID;
        }

        private void frmAdjustPoints_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtAdjustment);
            clsFormTheme.ApplyTextBoxStyle(_txtReason);
            
            _btnAdd.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAdd, clsFormTheme.Icons.Add);
            
            _btnDeduct.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplyDangerButtonStyle(_btnDeduct, clsFormTheme.Icons.Delete);
            
            _btnCancel.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel, clsFormTheme.Icons.Cancel);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            LoadCurrentPoints();
            
            _btnAdd.Click += _btnAdd_Click;
            _btnDeduct.Click += _btnDeduct_Click;
            _btnCancel.Click += (s, ev) => Close();
        }

        private void ApplyLocalization()
        {
            _lblAdjustment.Text = clsLanguageManager.GetString("Points to Adjust");
            _lblReason.Text = clsLanguageManager.GetString("Reason");
            _btnAdd.Text = clsLanguageManager.GetString("Add Points");
            _btnDeduct.Text = clsLanguageManager.GetString("Deduct Points");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = clsLanguageManager.GetString("Adjust Loyalty Points");
        }

        private void LoadCurrentPoints()
        {
            try
            {
                int currentPoints = clsCustomer.GetLoyaltyPoints(_customerID);
                _lblCurrentPoints.Text = "Current Points: " + currentPoints.ToString();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading current points: " + ex.Message, "Error");
            }
        }

        private void _btnAdd_Click(object sender, EventArgs ev)
        {
            if (!ValidateInput())
                return;
            
            int pointsToAdd = int.Parse(_txtAdjustment.Text);
            string reason = _txtReason.Text.Trim();
            
            string errorMessage;
            int currentPoints = clsCustomer.GetLoyaltyPoints(_customerID);
            int newPoints = currentPoints + pointsToAdd;
            
            if (clsCustomer.UpdateCustomerPoints(_customerID, newPoints, out errorMessage))
            {
                clsFormTheme.ShowSuccess(this, $"Added {pointsToAdd} points successfully.", "Success");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to add points: " + errorMessage, "Error");
            }
        }

        private void _btnDeduct_Click(object sender, EventArgs ev)
        {
            if (!ValidateInput())
                return;
            
            int pointsToDeduct = int.Parse(_txtAdjustment.Text);
            string reason = _txtReason.Text.Trim();
            
            int currentPoints = clsCustomer.GetLoyaltyPoints(_customerID);
            
            if (pointsToDeduct > currentPoints)
            {
                clsFormTheme.ShowWarning(this, "Cannot deduct more points than the customer has.", "Validation Error");
                return;
            }
            
            string errorMessage;
            int newPoints = currentPoints - pointsToDeduct;
            
            if (clsCustomer.UpdateCustomerPoints(_customerID, newPoints, out errorMessage))
            {
                clsFormTheme.ShowSuccess(this, $"Deducted {pointsToDeduct} points successfully.", "Success");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to deduct points: " + errorMessage, "Error");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_txtAdjustment.Text))
            {
                clsFormTheme.ShowWarning(this, "Please enter the number of points to adjust.", "Validation Error");
                _txtAdjustment.Focus();
                return false;
            }
            
            if (!int.TryParse(_txtAdjustment.Text, out int points) || points <= 0)
            {
                clsFormTheme.ShowWarning(this, "Please enter a valid positive number of points.", "Validation Error");
                _txtAdjustment.Focus();
                return false;
            }
            
            return true;
        }
    }
}
