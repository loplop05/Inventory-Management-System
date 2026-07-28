using InventoryBusinessLayer;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmDeleteSupplier : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isDeleting = false;

        public frmDeleteSupplier()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Delete Supplier", clsFormTheme.Icons.Delete);
            clsFormTheme.ApplyDangerButtonStyle(btnDelete);
            clsFormTheme.ApplyTextBoxStyle(txtSupplierID);

            btnDelete.Enabled = false;
            AcceptButton = btnDelete;
            txtSupplierID.TextChanged += txtSupplierID_TextChanged;
            KeyDown += frmDeleteSupplier_KeyDown;
        }

        private bool IsSupplierIDValid()
        {
            return int.TryParse(txtSupplierID.Text.Trim(), out int supplierID) &&
                   supplierID > 0;
        }

        private bool ValidateSupplierID()
        {
            if (!int.TryParse(txtSupplierID.Text.Trim(), out int supplierID) || supplierID <= 0)
            {
                clsFormTheme.ShowInputError(
                    txtSupplierID,
                    _errorProvider,
                    "Please enter a valid Supplier ID.");

                return false;
            }

            clsFormTheme.ClearInputError(txtSupplierID, _errorProvider);
            return true;
        }

        private void SetDeletingState(bool isDeleting)
        {
            _isDeleting = isDeleting;
            UseWaitCursor = isDeleting;
            txtSupplierID.Enabled = !isDeleting;
            btnDelete.Enabled = !isDeleting && IsSupplierIDValid();

            clsFormTheme.SetButtonBusy(
                btnDelete,
                isDeleting,
                "Delete",
                "Working...");
        }

        private void txtSupplierID_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateSupplierID();
            btnDelete.Enabled = isValid && !_isDeleting;
        }

        private void txtSupplierID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDelete.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateSupplierID())
                return;

            int supplierID = Convert.ToInt32(txtSupplierID.Text.Trim());

            SetDeletingState(true);

            try
            {
                clsSupplier supplier = await Task.Run(() => clsSupplier.FindSupplier(supplierID));

                if (supplier == null)
                {
                    clsFormTheme.ShowInputError(
                        txtSupplierID,
                        _errorProvider,
                        "Supplier not found.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{supplier.SupplierName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                bool isDeleted = await Task.Run(() => clsSupplier.DeleteSupplier(supplierID));

                if (isDeleted)
                {
                    MessageBox.Show(
                        "Supplier deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to delete the supplier. It may be used by a product.",
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
                    SetDeletingState(false);
            }
        }

        private void frmDeleteSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
