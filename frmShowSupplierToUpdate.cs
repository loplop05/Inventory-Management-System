using InventoryBusinessLayer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmShowSupplierToUpdate : Form
    {
        private clsSupplier _Supplier;
        private ErrorProvider _errorProvider;
        private bool _isUpdating = false;

        public frmShowSupplierToUpdate(clsSupplier supplier)
        {
            InitializeComponent();
            _Supplier = supplier;

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Edit Supplier", clsFormTheme.Icons.Update);
            btnUpdate.Text = clsFormTheme.Icons.Save + "  Save Changes";
            btnUpdate.Font = new Font(clsFormTheme.IconFontName, 11F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdate);
            btnUpdate.Height = 36;
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewSupplierName);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewPhone);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewEmail);
            txtBoxNewSupplierName.Font = new Font(clsFormTheme.MainFontName, 10F);
            txtBoxNewPhone.Font = new Font(clsFormTheme.MainFontName, 10F);
            txtBoxNewEmail.Font = new Font(clsFormTheme.MainFontName, 10F);

            lblSupplierName.BackColor = Color.Transparent;
            lblSupplierName.ForeColor = clsFormTheme.HeaderColor;
            lblSupplierID.BackColor = Color.Transparent;
            lblSupplierID.ForeColor = clsFormTheme.TextSecondary;

            btnUpdate.Enabled = false;
            AcceptButton = btnUpdate;
            CancelButton = null;
            txtBoxNewSupplierName.TextChanged += txtBoxNewSupplierName_TextChanged;
            txtBoxNewPhone.TextChanged += txtBoxNewPhone_TextChanged;
            txtBoxNewEmail.TextChanged += txtBoxNewEmail_TextChanged;
            txtBoxNewSupplierName.KeyDown += txtBoxNewSupplierName_KeyDown;
            txtBoxNewPhone.KeyDown += txtBoxNewPhone_KeyDown;
            txtBoxNewEmail.KeyDown += txtBoxNewEmail_KeyDown;
            KeyDown += frmShowSupplierToUpdate_KeyDown;
        }

        private void frmShowSupplierToUpdate_Load(object sender, EventArgs e)
        {
            lblSupplierID.Text = _Supplier.SupplierID.ToString();
            lblSupplierName.Text = _Supplier.SupplierName;
            txtBoxNewSupplierName.Text = _Supplier.SupplierName;
            txtBoxNewPhone.Text = _Supplier.Phone;
            txtBoxNewEmail.Text = _Supplier.Email;
            txtBoxNewSupplierName.Focus();
        }

        private bool IsSupplierNameValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxNewSupplierName.Text);
        }

        private bool IsPhoneValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxNewPhone.Text) && txtBoxNewPhone.Text.StartsWith("+962");
        }

        private bool IsEmailValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxNewEmail.Text) && txtBoxNewEmail.Text.Contains("@");
        }

        private bool ValidateAllInputs()
        {
            bool isValid = true;

            if (!IsSupplierNameValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewSupplierName, _errorProvider, "Supplier name cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewSupplierName, _errorProvider);
            }

            if (!IsPhoneValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewPhone, _errorProvider, "Phone must start with +962.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewPhone, _errorProvider);
            }

            if (!IsEmailValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewEmail, _errorProvider, "Invalid email format.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewEmail, _errorProvider);
            }

            return isValid;
        }

        private void SetUpdatingState(bool isUpdating)
        {
            _isUpdating = isUpdating;
            UseWaitCursor = isUpdating;
            txtBoxNewSupplierName.Enabled = !isUpdating;
            txtBoxNewPhone.Enabled = !isUpdating;
            txtBoxNewEmail.Enabled = !isUpdating;
            btnUpdate.Enabled = !isUpdating && ValidateAllInputs();

            clsFormTheme.SetButtonBusy(
                btnUpdate,
                isUpdating,
                "Update",
                "Updating...");
        }

        private void txtBoxNewSupplierName_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnUpdate.Enabled = ValidateAllInputs() && !_isUpdating;
        }

        private void txtBoxNewPhone_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnUpdate.Enabled = ValidateAllInputs() && !_isUpdating;
        }

        private void txtBoxNewEmail_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
            btnUpdate.Enabled = ValidateAllInputs() && !_isUpdating;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateAllInputs())
                return;

            _Supplier.SupplierName = txtBoxNewSupplierName.Text.Trim();
            _Supplier.Phone = txtBoxNewPhone.Text.Trim();
            _Supplier.Email = txtBoxNewEmail.Text.Trim();

            SetUpdatingState(true);

            try
            {
                clsSupplier.enValidateSupplier result = await Task.Run(() => _Supplier.Validate());

                switch (result)
                {
                    case clsSupplier.enValidateSupplier.NameIsEmpty:
                        clsFormTheme.ShowInputError(txtBoxNewSupplierName, _errorProvider, "Supplier name cannot be empty.");
                        return;
                    case clsSupplier.enValidateSupplier.InvalidPhone:
                        clsFormTheme.ShowInputError(txtBoxNewPhone, _errorProvider, "Phone must start with +962.");
                        return;
                    case clsSupplier.enValidateSupplier.InvalidEmail:
                        clsFormTheme.ShowInputError(txtBoxNewEmail, _errorProvider, "Invalid email format.");
                        return;
                }

                bool isSaved = await Task.Run(() => _Supplier.Save());

                if (isSaved)
                {
                    MessageBox.Show(
                        "Supplier updated successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to update the supplier.",
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
                    SetUpdatingState(false);
            }
        }

        private void txtBoxNewSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxNewPhone.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxNewPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxNewEmail.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxNewEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmShowSupplierToUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
