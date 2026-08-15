using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAddEditCustomer : Form
    {
        private int? _customerID;
        private bool _isEditMode;
        private bool _isQuickMode; // For POS - hides Notes field

        public int? CustomerID { get; private set; }
        public string CustomerName { get; private set; }

        public frmAddEditCustomer()
        {
            InitializeComponent();
            _isEditMode = false;
            _isQuickMode = false;
            Text = "Add Customer";
        }

        public frmAddEditCustomer(bool quickMode)
        {
            InitializeComponent();
            _isEditMode = false;
            _isQuickMode = quickMode;
            Text = "Add Customer";
            if (_isQuickMode)
            {
                _lblNotes.Visible = false;
                _txtNotes.Visible = false;
                ClientSize = new System.Drawing.Size(400, 350);
                _btnSave.Location = new System.Drawing.Point(20, 270);
                _btnCancel.Location = new System.Drawing.Point(260, 270);
            }
        }

        public frmAddEditCustomer(int customerID, string phoneNumber, string customerName)
        {
            InitializeComponent();
            _customerID = customerID;
            _isEditMode = true;
            _isQuickMode = false;
            Text = "Edit Customer";

            _txtPhoneNumber.Text = phoneNumber;
            _txtCustomerName.Text = customerName;
        }

        private void frmAddEditCustomer_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtPhoneNumber);
            clsFormTheme.ApplyTextBoxStyle(_txtCustomerName);
            clsFormTheme.ApplyTextBoxStyle(_txtEmail);
            clsFormTheme.ApplyTextBoxStyle(_txtAddress);
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
            _lblEmail.Text = clsLanguageManager.GetString("Email");
            _lblAddress.Text = clsLanguageManager.GetString("Address");
            _lblNotes.Text = clsLanguageManager.GetString("Notes");
            _btnSave.Text = clsLanguageManager.GetString("Save");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = _isEditMode ? clsLanguageManager.GetString("Edit Customer") : clsLanguageManager.GetString("Add Customer");
        }

        private void _btnSave_Click(object sender, EventArgs ev)
        {
            string phoneNumber = _txtPhoneNumber.Text.Trim();
            string customerName = _txtCustomerName.Text.Trim();
            string email = _txtEmail.Text.Trim();
            string address = _txtAddress.Text.Trim();
            string notes = _isQuickMode ? "" : _txtNotes.Text.Trim();

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
                    CustomerID = newCustomerID;
                    CustomerName = customerName;
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
