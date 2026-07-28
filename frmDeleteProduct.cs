using InventoryBusinessLayer;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmDeleteProduct : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isDeleting = false;

        public frmDeleteProduct()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Delete Product", clsFormTheme.Icons.Delete);
            clsFormTheme.ApplyDangerButtonStyle(btnDelete, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplyTextBoxStyle(txtProductID);
            
            btnDelete.Enabled = false;
            AcceptButton = btnDelete;
            txtProductID.TextChanged += txtProductID_TextChanged;
            KeyDown += frmDeleteProduct_KeyDown;
        }

        private bool IsProductIDValid()
        {
            return int.TryParse(txtProductID.Text.Trim(), out int productID) &&
                   productID > 0;
        }

        private bool ValidateProductID()
        {
            if (!int.TryParse(txtProductID.Text.Trim(), out int productID) || productID <= 0)
            {
                clsFormTheme.ShowInputError(
                    txtProductID,
                    _errorProvider,
                    "Please enter a valid Product ID.");
            
                return false;
            }

            clsFormTheme.ClearInputError(txtProductID, _errorProvider);
            return true;
        }

        private void SetDeletingState(bool isDeleting)
        {
            _isDeleting = isDeleting;
            UseWaitCursor = isDeleting;
            txtProductID.Enabled = !isDeleting;
            btnDelete.Enabled = !isDeleting && IsProductIDValid();

            clsFormTheme.SetButtonBusy(
                btnDelete,
                isDeleting,
                "Delete",
                "Working...");
        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateProductID();
            btnDelete.Enabled = isValid && !_isDeleting;
        }

        private void txtProductID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDelete.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateProductID())
                return;

            int productID = Convert.ToInt32(txtProductID.Text.Trim());

            SetDeletingState(true);

            try
            {
                clsProduct product = await Task.Run(() => clsProduct.FindProduct(productID));

                if (product == null)
                {
                    clsFormTheme.ShowInputError(
                        txtProductID,
                        _errorProvider,
                        "Product not found.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{product.ProductName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                bool isDeleted = await Task.Run(() => clsProduct.DeleteProduct(productID));

                if (isDeleted)
                {
                    MessageBox.Show(
                        "Product deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to delete the product.",
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

        private void frmDeleteProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
