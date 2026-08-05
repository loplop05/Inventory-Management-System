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
        private int _selectedProductID = -1;

        public int SelectedProductID
        {
            get { return _selectedProductID; }
            set
            {
                _selectedProductID = value;
                if (_selectedProductID > 0)
                {
                    txtUpdateProductID.Text = _selectedProductID.ToString();
                }
            }
        }

        public frmUpdateProduct()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);

            // Style buttons
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel);

            // Style text box
            clsFormTheme.ApplyTextBoxStyle(txtUpdateProductID);

            btnSearch.Enabled = false;
            AcceptButton = btnSearch;
            txtUpdateProductID.TextChanged += txtUpdateProductID_TextChanged;
            KeyDown += frmUpdateProduct_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Update Product");
            _lblPageTitle.Text = clsLanguageManager.GetString("Update Product");
            btnSearch.Text = clsLanguageManager.GetString("Find Product");
            btnCancel.Text = clsLanguageManager.GetString("Cancel");
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
                clsFormTheme.ShowError(this,
                    ex.Message,
                    "Error");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmUpdateProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
