using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAddEditCustomer : Form
    {
        private int? _customerID;
        private bool _isEditMode;

        public frmAddEditCustomer()
        {
            InitializeComponent();
            _isEditMode = false;
            Text = "Add Customer";
        }

        public frmAddEditCustomer(int customerID, string phoneNumber, string customerName)
        {
            InitializeComponent();
            _customerID = customerID;
            _isEditMode = true;
            Text = "Edit Customer";
            
            _txtPhoneNumber.Text = phoneNumber;
            _txtCustomerName.Text = customerName;
        }

        private void frmAddEditCustomer_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtPhoneNumber);
            clsFormTheme.ApplyTextBoxStyle(_txtCustomerName);
            clsFormTheme.ApplyTextBoxStyle(_txtNotes);
            
            _btnSave.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSave, clsFormTheme.Icons.Save);
            
            _btnCancel.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel, clsFormTheme.Icons.Cancel);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            _btnSave.Click += _btnSave_Click;
            _btnCancel.Click += (s, ev) => Close();
        }

        private void ApplyLocalization()
        {
            _lblPhoneNumber.Text = clsLanguageManager.GetString("Phone Number");
            _lblCustomerName.Text = clsLanguageManager.GetString("Customer Name");
            _lblNotes.Text = clsLanguageManager.GetString("Notes");
            _btnSave.Text = clsLanguageManager.GetString("Save");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = _isEditMode ? clsLanguageManager.GetString("Edit Customer") : clsLanguageManager.GetString("Add Customer");
        }

        private void _btnSave_Click(object sender, EventArgs ev)
        {
            string phoneNumber = _txtPhoneNumber.Text.Trim();
            string customerName = _txtCustomerName.Text.Trim();
            string notes = _txtNotes.Text.Trim();

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                clsFormTheme.ShowWarning(this, "Phone number is required.", "Validation Error");
                _txtPhoneNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(customerName))
            {
                clsFormTheme.ShowWarning(this, "Customer name is required.", "Validation Error");
                _txtCustomerName.Focus();
                return;
            }

            string errorMessage;

            if (_isEditMode)
            {
                if (clsCustomer.UpdateCustomer(_customerID.Value, phoneNumber, customerName, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "Customer updated successfully.", "Success");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to update customer: " + errorMessage, "Error");
                }
            }
            else
            {
                int newCustomerID;
                if (clsCustomer.AddCustomer(phoneNumber, customerName, out newCustomerID, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "Customer added successfully.", "Success");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to add customer: " + errorMessage, "Error");
                }
            }
        }
    }
}
