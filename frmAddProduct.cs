using InventoryBusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmAddProduct : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSaving = false;
        private string _selectedImagePath = null;

        public frmAddProduct()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);

            // Style buttons
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Save);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel);

            // Style text boxes
            clsFormTheme.ApplyTextBoxStyle(txtBoxProductName);
            clsFormTheme.ApplyTextBoxStyle(txtBoxPrice);
            clsFormTheme.ApplyTextBoxStyle(txtBoxQuantity);
            clsFormTheme.ApplyTextBoxStyle(txtBoxBarcode);

            // Style combo boxes
            clsFormTheme.ApplyComboBoxStyle(cmbCategory);
            clsFormTheme.ApplyComboBoxStyle(cmbSupplier);

            // Style browse button
            clsFormTheme.ApplySecondaryButtonStyle(_btnBrowseImage);
            UpdateImageButtonText();

            btnAdd.Enabled = false;
            AcceptButton = btnAdd;
            KeyDown += frmAddProduct_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Add New Product");
            _lblPageTitle.Text = clsLanguageManager.GetString("Add Product");
            btnAdd.Text = clsLanguageManager.GetString("Save Product");
            btnCancel.Text = clsLanguageManager.GetString("Cancel");
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();
            ValidateAllInputs(); // Initial validation to set button state
        }

        private async void LoadCategories()
        {
            try
            {
                DataTable categories = await Task.Run(() => clsCategory.GetAllCategories());
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                cmbCategory.SelectedIndex = -1;
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
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
                cmbSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading suppliers: " + ex.Message, "Error");
            }
        }

        private bool IsProductNameValid()
        {
            return clsDataValidation.ValidateTextBox(
                txtBoxProductName,
                _errorProvider,
                clsDataValidation.IsValidProductName,
                clsDataValidation.ErrorMessages.InvalidProductName
            );
        }

        private bool IsPriceValid()
        {
            return clsDataValidation.ValidateTextBox(
                txtBoxPrice,
                _errorProvider,
                clsDataValidation.IsPositiveNumber,
                clsDataValidation.ErrorMessages.InvalidPositiveNumber
            );
        }

        private bool IsQuantityValid()
        {
            return clsDataValidation.ValidateTextBox(
                txtBoxQuantity,
                _errorProvider,
                clsDataValidation.IsValidPositiveInteger,
                clsDataValidation.ErrorMessages.InvalidPositiveInteger
            );
        }

        private bool IsBarcodeValid()
        {
            return clsDataValidation.ValidateTextBox(
                txtBoxBarcode,
                _errorProvider,
                clsDataValidation.IsValidBarcode,
                clsDataValidation.ErrorMessages.InvalidBarcode
            );
        }

        private bool IsCategorySelected()
        {
            return cmbCategory.SelectedValue != null;
        }

        private bool IsSupplierSelected()
        {
            return cmbSupplier.SelectedValue != null;
        }

        private bool ValidateAllInputs()
        {
            bool isValid = true;

            isValid &= IsProductNameValid();
            isValid &= IsPriceValid();
            isValid &= IsQuantityValid();
            isValid &= IsBarcodeValid();

            if (!IsCategorySelected())
            {
                _errorProvider.SetError(cmbCategory, "Please select a category.");
                isValid = false;
            }
            else
            {
                _errorProvider.SetError(cmbCategory, "");
            }

            if (!IsSupplierSelected())
            {
                _errorProvider.SetError(cmbSupplier, "Please select a supplier.");
                isValid = false;
            }
            else
            {
                _errorProvider.SetError(cmbSupplier, "");
            }

            btnAdd.Enabled = isValid && !_isSaving;
            return isValid;
        }

        private void SetSavingState(bool isSaving)
        {
            _isSaving = isSaving;
            UseWaitCursor = isSaving;
            txtBoxProductName.Enabled = !isSaving;
            txtBoxPrice.Enabled = !isSaving;
            txtBoxQuantity.Enabled = !isSaving;
            txtBoxBarcode.Enabled = !isSaving;
            cmbCategory.Enabled = !isSaving;
            cmbSupplier.Enabled = !isSaving;
            btnAdd.Enabled = !isSaving && ValidateAllInputs();

            clsFormTheme.SetButtonBusy(
                btnAdd,
                isSaving,
                "Save Product",
                "Saving...");
        }

        private void txtBoxProductName_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxPrice_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void txtBoxBarcode_TextChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllInputs();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateAllInputs())
                return;

            clsProduct product = new clsProduct();
            product.ProductName = txtBoxProductName.Text.Trim();
            product.Price = decimal.Parse(txtBoxPrice.Text);
            product.Quantity = int.Parse(txtBoxQuantity.Text);
            product.Barcode = txtBoxBarcode.Text.Trim();
            product.CategoryID = (int)cmbCategory.SelectedValue;
            product.SupplierID = (int)cmbSupplier.SelectedValue;
            product.ImagePath = _selectedImagePath;

            SetSavingState(true);

            try
            {
                clsProduct.enValidateProduct result = await Task.Run(() => product.Validate());

                switch (result)
                {
                    case clsProduct.enValidateProduct.NameAlreadyExists:
                        clsFormTheme.ShowInputError(txtBoxProductName, _errorProvider, "Product name already exists.");
                        return;
                    case clsProduct.enValidateProduct.BarcodeAlreadyExists:
                        clsFormTheme.ShowInputError(txtBoxBarcode, _errorProvider, "Barcode already exists.");
                        return;
                    case clsProduct.enValidateProduct.InvalidCategory:
                        _errorProvider.SetError(cmbCategory, "Invalid category selected.");
                        return;
                    case clsProduct.enValidateProduct.InvalidSupplier:
                        _errorProvider.SetError(cmbSupplier, "Invalid supplier selected.");
                        return;
                    case clsProduct.enValidateProduct.InvalidName:
                        clsFormTheme.ShowInputError(txtBoxProductName, _errorProvider, "Product name cannot be empty.");
                        return;
                    case clsProduct.enValidateProduct.InvalidPrice:
                        clsFormTheme.ShowInputError(txtBoxPrice, _errorProvider, "Invalid price.");
                        return;
                    case clsProduct.enValidateProduct.InvalidQuantity:
                        clsFormTheme.ShowInputError(txtBoxQuantity, _errorProvider, "Invalid quantity.");
                        return;
                    case clsProduct.enValidateProduct.InvalidBarcode:
                        clsFormTheme.ShowInputError(txtBoxBarcode, _errorProvider, "Invalid barcode.");
                        return;
                }

                bool isSaved = await Task.Run(() => product.Save());

                if (isSaved)
                {
                    clsFormTheme.ShowSuccess(this, "Product added successfully.", "Success");

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to add the product.", "Error");
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, ex.Message, "Error");
            }
            finally
            {
                if (!IsDisposed)
                    SetSavingState(false);
            }
        }

        private void txtBoxProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxPrice.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxQuantity.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxBarcode.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCategory.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSupplier.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmAddProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void UpdateImageButtonText()
        {
            if (_picPreview.Image != null)
            {
                _btnBrowseImage.Text = "Change Image";
            }
            else
            {
                _btnBrowseImage.Text = "Choose Image";
            }
        }

        private void _btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
                dlg.Title = "Select Product Image";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Validate file size before copying
                    FileInfo fileInfo = new FileInfo(dlg.FileName);
                    if (fileInfo.Length > 5 * 1024 * 1024) // 5 MB
                    {
                        clsFormTheme.ShowError(this, "Image file is too large. Maximum size is 5 MB.", "File Too Large");
                        return;
                    }

                    try
                    {
                        string destFolder = Path.Combine(Application.StartupPath, "ProductImages");
                        Directory.CreateDirectory(destFolder);
                        string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dlg.FileName).ToLowerInvariant();
                        string destPath = Path.Combine(destFolder, fileName);
                        File.Copy(dlg.FileName, destPath, overwrite: true);
                        _selectedImagePath = Path.Combine("ProductImages", fileName);
                        _picPreview.Load(destPath);
                        UpdateImageButtonText();
                    }
                    catch (Exception ex)
                    {
                        clsFormTheme.ShowError(this, "Failed to load image: " + ex.Message, "Error");
                    }
                }
            }
        }
    }
}
