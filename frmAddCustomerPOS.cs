using System;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmAddCustomerPOS : Form
    {
        public int? CustomerID { get; private set; }
        public string CustomerName { get; private set; }

        public frmAddCustomerPOS()
        {
            InitializeComponent();
        }

        private void frmAddCustomerPOS_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            //clsFormTheme.CreateHeaderPanel(this, "Add Customer", clsFormTheme.Icons.User);

            clsFormTheme.ApplyTextBoxStyle(txtName);
            clsFormTheme.ApplyTextBoxStyle(txtPhone);
            clsFormTheme.ApplyTextBoxStyle(txtEmail);
            clsFormTheme.ApplyTextBoxStyle(txtAddress);

            clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            txtName.Focus();
            clsLanguageManager.ApplyLanguage(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                clsFormTheme.ShowWarning(this, "Customer name is required.", "Validation");
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                clsFormTheme.ShowWarning(this, "Phone number is required.", "Validation");
                txtPhone.Focus();
                return;
            }

            string errorMessage;
            int customerID;

            bool success = clsCustomer.AddCustomer(phone, name, out customerID, out errorMessage);

            if (success && customerID > 0)
            {
                CustomerID = customerID;
                CustomerName = name;
                clsFormTheme.ShowSuccess(this, "Customer added successfully.", "Success");
                DialogResult = DialogResult.OK;
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to add customer: " + errorMessage, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPhone.Focus();
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmail.Focus();
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtAddress.Focus();
                e.Handled = true;
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
