using InventoryBusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmShowProductToUpdate : Form
    {
        private clsProduct _Product;
        private ErrorProvider _errorProvider;
        private bool _isUpdating = false;

        public frmShowProductToUpdate(clsProduct product)
        {
            InitializeComponent();
            _Product = product;

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Edit Product", clsFormTheme.Icons.Update);
            btnUpdate.Text = "Save Changes";
            btnUpdate.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdate, clsFormTheme.Icons.Save);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewProductName);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewPrice);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewQuantity);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewBarcode);

            lblProductName.BackColor = Color.Transparent;
            lblProductName.ForeColor = clsFormTheme.HeaderColor;
            lblProductID.BackColor = Color.Transparent;
            lblProductID.ForeColor = clsFormTheme.TextSecondary;

            btnUpdate.Enabled = false;
            AcceptButton = btnUpdate;
            txtBoxNewProductName.TextChanged += txtBoxNewProductName_TextChanged;
            txtBoxNewPrice.TextChanged += txtBoxNewPrice_TextChanged;
            txtBoxNewQuantity.TextChanged += txtBoxNewQuantity_TextChanged;
            txtBoxNewBarcode.TextChanged += txtBoxNewBarcode_TextChanged;
            cmbNewCategory.SelectedIndexChanged += cmbNewCategory_SelectedIndexChanged;
            cmbNewSupplier.SelectedIndexChanged += cmbNewSupplier_SelectedIndexChanged;
            txtBoxNewProductName.KeyDown += txtBoxNewProductName_KeyDown;
            txtBoxNewPrice.KeyDown += txtBoxNewPrice_KeyDown;
            txtBoxNewQuantity.KeyDown += txtBoxNewQuantity_KeyDown;
            txtBoxNewBarcode.KeyDown += txtBoxNewBarcode_KeyDown;
            cmbNewCategory.KeyDown += cmbNewCategory_KeyDown;
            cmbNewSupplier.KeyDown += cmbNewSupplier_KeyDown;
            KeyDown += frmShowProductToUpdate_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Edit Product");
            btnUpdate.Text = clsLanguageManager.GetString("Save Changes");
        }

        private void frmShowProductToUpdate_Load(object sender, EventArgs e)
        {
            lblProductID.Text = _Product.ProductID.ToString();
            lblProductName.Text = _Product.ProductName;
            txtBoxNewProductName.Text = _Product.ProductName;
            txtBoxNewPrice.Text = _Product.Price.ToString();
            txtBoxNewQuantity.Text = _Product.Quantity.ToString();
            txtBoxNewBarcode.Text = _Product.Barcode;

            LoadCategories();
            LoadSuppliers();

            cmbNewCategory.SelectedValue = _Product.CategoryID;
            cmbNewSupplier.SelectedValue = _Product.SupplierID;

            ValidateAllInputs(); // Initial validation to set button state
            txtBoxNewProductName.Focus();
        }

        private async void LoadCategories()
        {
            try
            {
                DataTable categories = await Task.Run(() => clsCategory.GetAllCategories());
                cmbNewCategory.DataSource = categories;
                cmbNewCategory.DisplayMember = "CategoryName";
                cmbNewCategory.ValueMember = "CategoryID";
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading categories: " + ex.Message, "Error");
            }
        }

        private async void LoadSuppliers()
        {
            try
            {
                DataTable suppliers = await Task.Run(() => clsSupplier.GetAllSuppliers());
                cmbNewSupplier.DataSource = suppliers;
                cmbNewSupplier.DisplayMember = "SupplierName";
                cmbNewSupplier.ValueMember = "SupplierID";
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading suppliers: " + ex.Message, "Error");
            }
        }

        private bool IsProductNameValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxNewProductName.Text);
        }

        private bool IsPriceValid()
        {
            return decimal.TryParse(txtBoxNewPrice.Text, out decimal price) && price >= 0;
        }

        private bool IsQuantityValid()
        {
            return int.TryParse(txtBoxNewQuantity.Text, out int quantity) && quantity >= 0;
        }

        private bool IsBarcodeValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxNewBarcode.Text);
        }

        private bool IsCategorySelected()
        {
            return cmbNewCategory.SelectedValue != null;
        }

        private bool IsSupplierSelected()
        {
            return cmbNewSupplier.SelectedValue != null;
        }

        private bool ValidateAllInputs()
        {
            bool isValid = true;

            if (!IsProductNameValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewProductName, _errorProvider, "Product name cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewProductName, _errorProvider);
            }

            if (!IsPriceValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewPrice, _errorProvider, "Please enter a valid price (non-negative).");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewPrice, _errorProvider);
            }

            if (!IsQuantityValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewQuantity, _errorProvider, "Please enter a valid quantity (non-negative).");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewQuantity, _errorProvider);
            }

            if (!IsBarcodeValid())
            {
                clsFormTheme.ShowInputError(txtBoxNewBarcode, _errorProvider, "Barcode cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxNewBarcode, _errorProvider);
            }

            if (!IsCategorySelected())
            {
                _errorProvider.SetError(cmbNewCategory, "Please select a category.");
                isValid = false;
            }
            else
            {
                _errorProvider.SetError(cmbNewCategory, "");
            }

            if (!IsSupplierSelected())
            {
                _errorProvider.SetError(cmbNewSupplier, "Please select a supplier.");
                isValid = false;
            }
            else
            {
                _errorProvider.SetError(cmbNewSupplier, "");
            }

            btnUpdate.Enabled = isValid && !_isUpdating;
            return isValid;
        }

        private void SetUpdatingState(bool isUpdating)
        {
            _isUpdating = isUpdating;
            UseWaitCursor = isUpdating;
            txtBoxNewProductName.Enabled = !isUpdating;
            txtBoxNewPrice.Enabled = !isUpdating;
            txtBoxNewQuantity.Enabled = !isUpdating;
            txtBoxNewBarcode.Enabled = !isUpdating;
            cmbNewCategory.Enabled = !isUpdating;
            cmbNewSupplier.Enabled = !isUpdating;
            btnUpdate.Enabled = !isUpdating && ValidateAllInputs();

            clsFormTheme.SetButtonBusy(
                btnUpdate,
                isUpdating,
                "Update",
                "Updating...");
        }

        private void txtBoxNewProductName_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxNewPrice_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxNewQuantity_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxNewBarcode_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void cmbNewCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void cmbNewSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateAllInputs())
                return;

            _Product.ProductName = txtBoxNewProductName.Text.Trim();
            _Product.Price = decimal.Parse(txtBoxNewPrice.Text);
            _Product.Quantity = int.Parse(txtBoxNewQuantity.Text);
            _Product.Barcode = txtBoxNewBarcode.Text.Trim();

            // Add null checks before casting SelectedValue
            if (cmbNewCategory.SelectedValue == null || cmbNewSupplier.SelectedValue == null)
            {
                clsFormTheme.ShowInputError(cmbNewCategory, _errorProvider, "Please select both category and supplier.");
                return;
            }
            _Product.CategoryID = (int)cmbNewCategory.SelectedValue;
            _Product.SupplierID = (int)cmbNewSupplier.SelectedValue;

            SetUpdatingState(true);

            try
            {
                clsProduct.enValidateProduct result = await Task.Run(() => _Product.Validate());

                switch (result)
                {
                    case clsProduct.enValidateProduct.NameAlreadyExists:
                        clsFormTheme.ShowInputError(txtBoxNewProductName, _errorProvider, "Product name already exists.");
                        return;
                    case clsProduct.enValidateProduct.BarcodeAlreadyExists:
                        clsFormTheme.ShowInputError(txtBoxNewBarcode, _errorProvider, "Barcode already exists.");
                        return;
                    case clsProduct.enValidateProduct.InvalidCategory:
                        _errorProvider.SetError(cmbNewCategory, "Invalid category selected.");
                        return;
                    case clsProduct.enValidateProduct.InvalidSupplier:
                        _errorProvider.SetError(cmbNewSupplier, "Invalid supplier selected.");
                        return;
                    case clsProduct.enValidateProduct.InvalidName:
                        clsFormTheme.ShowInputError(txtBoxNewProductName, _errorProvider, "Product name cannot be empty.");
                        return;
                    case clsProduct.enValidateProduct.InvalidPrice:
                        clsFormTheme.ShowInputError(txtBoxNewPrice, _errorProvider, "Invalid price.");
                        return;
                    case clsProduct.enValidateProduct.InvalidQuantity:
                        clsFormTheme.ShowInputError(txtBoxNewQuantity, _errorProvider, "Invalid quantity.");
                        return;
                    case clsProduct.enValidateProduct.InvalidBarcode:
                        clsFormTheme.ShowInputError(txtBoxNewBarcode, _errorProvider, "Invalid barcode.");
                        return;
                }

                bool isSaved = await Task.Run(() => _Product.Save());

                if (isSaved)
                {
                    clsFormTheme.ShowSuccess(this,
                        "Product updated successfully.",
                        "Success");

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this,
                        "Failed to update the product.",
                        "Error");
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
                    SetUpdatingState(false);
            }
        }

        private void txtBoxNewProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxNewPrice.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxNewPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxNewQuantity.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxNewQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxNewBarcode.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxNewBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbNewCategory.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbNewCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbNewSupplier.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbNewSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmShowProductToUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
