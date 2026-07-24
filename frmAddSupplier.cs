using InventoryBusinessLayer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmAddSupplier : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSaving = false;

        public frmAddSupplier()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd);
            clsFormTheme.ApplyTextBoxStyle(txtBoxSupplierName);
            clsFormTheme.ApplyTextBoxStyle(txtBoxPhone);
            clsFormTheme.ApplyTextBoxStyle(txtBoxEmail);

            btnAdd.Enabled = false;
            AcceptButton = btnAdd;
            KeyDown += frmAddSupplier_KeyDown;
        }

        private bool IsSupplierNameValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxSupplierName.Text);
        }

        private bool IsPhoneValid()
        {
            // Basic validation: non-empty and starts with +962
            return !string.IsNullOrWhiteSpace(txtBoxPhone.Text) && txtBoxPhone.Text.StartsWith("+962");
        }

        private bool IsEmailValid()
        {
            // Basic validation: non-empty and contains @
            return !string.IsNullOrWhiteSpace(txtBoxEmail.Text) && txtBoxEmail.Text.Contains("@");
        }

        private bool ValidateAllInputs()
        {
            bool isValid = true;

            if (!IsSupplierNameValid())
            {
                clsFormTheme.ShowInputError(txtBoxSupplierName, _errorProvider, "Supplier name cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxSupplierName, _errorProvider);
            }

            if (!IsPhoneValid())
            {
                clsFormTheme.ShowInputError(txtBoxPhone, _errorProvider, "Phone must start with +962.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxPhone, _errorProvider);
            }

            if (!IsEmailValid())
            {
                clsFormTheme.ShowInputError(txtBoxEmail, _errorProvider, "Invalid email format.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxEmail, _errorProvider);
            }

            return isValid;
        }

        private void SetSavingState(bool isSaving)
        {
            _isSaving = isSaving;
            UseWaitCursor = isSaving;
            txtBoxSupplierName.Enabled = !isSaving;
            txtBoxPhone.Enabled = !isSaving;
            txtBoxEmail.Enabled = !isSaving;
            btnAdd.Enabled = !isSaving && ValidateAllInputs();

            clsFormTheme.SetButtonBusy(
                btnAdd,
                isSaving,
                "Add",
                "Adding...");
        }

        private void txtBoxSupplierName_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnAdd.Enabled = ValidateAllInputs() && !_isSaving;
        }

        private void txtBoxPhone_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnAdd.Enabled = ValidateAllInputs() && !_isSaving;
        }

        private void txtBoxEmail_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnAdd.Enabled = ValidateAllInputs() && !_isSaving;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateAllInputs())
                return;

            clsSupplier supplier = new clsSupplier();
            supplier.SupplierName = txtBoxSupplierName.Text.Trim();
            supplier.Phone = txtBoxPhone.Text.Trim();
            supplier.Email = txtBoxEmail.Text.Trim();

            SetSavingState(true);

            try
            {
                clsSupplier.enValidateSupplier result = await Task.Run(() => supplier.Validate());

                switch (result)
                {
                    case clsSupplier.enValidateSupplier.NameIsEmpty:
                        clsFormTheme.ShowInputError(txtBoxSupplierName, _errorProvider, "Supplier name cannot be empty.");
                        return;
                    case clsSupplier.enValidateSupplier.InvalidPhone:
                        clsFormTheme.ShowInputError(txtBoxPhone, _errorProvider, "Phone must start with +962.");
                        return;
                    case clsSupplier.enValidateSupplier.InvalidEmail:
                        clsFormTheme.ShowInputError(txtBoxEmail, _errorProvider, "Invalid email format.");
                        return;
                }

                bool isSaved = await Task.Run(() => supplier.Save());

                if (isSaved)
                {
                    MessageBox.Show(
                        "Supplier added successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtBoxSupplierName.Clear();
                    txtBoxPhone.Clear();
                    txtBoxEmail.Clear();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to add the supplier.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    SetSavingState(false);
            }
        }

        private void txtBoxSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxPhone.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxEmail.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmAddSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
