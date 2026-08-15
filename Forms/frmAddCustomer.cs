using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAddCustomer : Form
    {
        public int? CustomerID { get; private set; }
        public string CustomerName { get; private set; }
        public string PhoneNumber { get; set; }

        private bool _isSaving = false;

        public frmAddCustomer()
        {
            InitializeComponent();
        }

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ClearValidation();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(txtPhoneNumber);
            clsFormTheme.ApplyTextBoxStyle(txtCustomerName);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            btnAdd.Text = "Add Customer";
            btnAdd.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);

            btnCancel.Text = "Cancel";
            btnCancel.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F);

            KeyDown += frmAddCustomer_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Add New Customer");
            btnAdd.Text = clsLanguageManager.GetString("Add Customer");
            btnCancel.Text = clsLanguageManager.GetString("Cancel");
        }

        private void ClearValidation()
        {
            errorProvider.Clear();
        }

        private bool ValidateInput()
        {
            ClearValidation();

            bool isValid = true;

            // Validate phone number
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                errorProvider.SetError(txtPhoneNumber, "Phone number is required");
                isValid = false;
            }
            else if (!IsValidPhoneNumberFormat(txtPhoneNumber.Text.Trim()))
            {
                errorProvider.SetError(txtPhoneNumber, "Invalid phone number format. Use +962XXXXXXXXX or 07XXXXXXXXX");
                isValid = false;
            }
            else if (clsCustomer.CustomerExistsByPhone(txtPhoneNumber.Text.Trim()))
            {
                errorProvider.SetError(txtPhoneNumber, "A customer with this phone number already exists");
                isValid = false;
            }

            // Validate customer name
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, "Customer name is required");
                isValid = false;
            }
            else if (txtCustomerName.Text.Trim().Length > 100)
            {
                errorProvider.SetError(txtCustomerName, "Customer name cannot exceed 100 characters");
                isValid = false;
            }

            return isValid;
        }

        private bool IsValidPhoneNumberFormat(string phoneNumber)
        {
            phoneNumber = phoneNumber.Trim();

            // Check for Jordan phone format: +962XXXXXXXXX or 07XXXXXXXXX
            if (phoneNumber.StartsWith("+962"))
            {
                if (phoneNumber.Length != 13)
                    return false;
                for (int i = 3; i < phoneNumber.Length; i++)
                {
                    if (!char.IsDigit(phoneNumber[i]))
                        return false;
                }
                return true;
            }
            else if (phoneNumber.StartsWith("07"))
            {
                if (phoneNumber.Length != 10)
                    return false;
                for (int i = 2; i < phoneNumber.Length; i++)
                {
                    if (!char.IsDigit(phoneNumber[i]))
                        return false;
                }
                return true;
            }

            return false;
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtPhoneNumber, "");
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtCustomerName, "");
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustomerName.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_isSaving)
                return;

            if (!ValidateInput())
            {
                clsFormTheme.ShowWarning(this, "Please fix the validation errors before proceeding.", "Validation Error");
                return;
            }

            _isSaving = true;
            btnAdd.Enabled = false;
            btnCancel.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string errorMessage;
            int customerID;

            bool success = clsCustomer.AddCustomer(
                txtPhoneNumber.Text.Trim(),
                txtCustomerName.Text.Trim(),
                out customerID,
                out errorMessage
            );

            _isSaving = false;
            btnAdd.Enabled = true;
            btnCancel.Enabled = true;
            this.Cursor = Cursors.Default;

            if (success)
            {
                CustomerID = customerID;
                CustomerName = txtCustomerName.Text.Trim();
                PhoneNumber = txtPhoneNumber.Text.Trim();

                clsFormTheme.ShowSuccess(this, "Customer added successfully!", "Success");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to add customer: " + errorMessage, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmAddCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, e);
            }
        }
    }
}
