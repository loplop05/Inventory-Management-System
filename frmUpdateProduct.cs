using InventoryBusinessLayer;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmUpdateProduct : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSearching = false;

        public frmUpdateProduct()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Update Product", clsFormTheme.Icons.Update);
            btnSearch.Text = "Find Product";
            btnSearch.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplyTextBoxStyle(txtUpdateProductID);

            btnSearch.Enabled = false;
            AcceptButton = btnSearch;
            txtUpdateProductID.TextChanged += txtUpdateProductID_TextChanged;
            KeyDown += frmUpdateProduct_KeyDown;
        }

        private bool IsProductLookupValid()
        {
            return !string.IsNullOrWhiteSpace(txtUpdateProductID.Text);
        }

        private bool ValidateProductLookup()
        {
            if (!IsProductLookupValid())
            {
                clsFormTheme.ShowInputError(
                    txtUpdateProductID,
                    _errorProvider,
                    "Please enter a Product ID or barcode.");

                return false;
            }

            clsFormTheme.ClearInputError(txtUpdateProductID, _errorProvider);
            return true;
        }

        private void SetSearchingState(bool isSearching)
        {
            _isSearching = isSearching;
            UseWaitCursor = isSearching;
            txtUpdateProductID.Enabled = !isSearching;
            btnSearch.Enabled = !isSearching && IsProductLookupValid();

            clsFormTheme.SetButtonBusy(
                btnSearch,
                isSearching,
                "Search",
                "Searching...");
        }

        private void txtUpdateProductID_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateProductLookup();
            btnSearch.Enabled = isValid && !_isSearching;
        }

        private async Task UpdateProduct()
        {
            if (!ValidateProductLookup())
                return;

            string productLookup = txtUpdateProductID.Text.Trim();

            SetSearchingState(true);

            try
            {
                clsProduct product = await Task.Run(() =>
                {
                    clsProduct foundProduct = null;

                    if (int.TryParse(productLookup, out int productID) && productID > 0)
                    {
                        foundProduct = clsProduct.FindProduct(productID);
                    }

                    return foundProduct ?? clsProduct.FindProductByBarcode(productLookup);
                });

                if (product == null)
                {
                    clsFormTheme.ShowInputError(
                        txtUpdateProductID,
                        _errorProvider,
                        "Product not found by ID or barcode.");
                    return;
                }

                frmShowProductToUpdate frm = new frmShowProductToUpdate(product);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
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
                    SetSearchingState(false);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await UpdateProduct();
        }

        private async void txtUpdateProductID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await UpdateProduct();
                e.SuppressKeyPress = true;
            }
        }

        private void frmUpdateProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
