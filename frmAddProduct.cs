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
    public partial class frmAddProduct : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSaving = false;

        public frmAddProduct()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Add New Product", clsFormTheme.Icons.Add);
            btnAdd.Text = "Save Product";
            btnAdd.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Save);
            clsFormTheme.ApplyTextBoxStyle(txtBoxProductName);
            clsFormTheme.ApplyTextBoxStyle(txtBoxPrice);
            clsFormTheme.ApplyTextBoxStyle(txtBoxQuantity);
            clsFormTheme.ApplyTextBoxStyle(txtBoxBarcode);

            btnAdd.Enabled = false;
            AcceptButton = btnAdd;
            KeyDown += frmAddProduct_KeyDown;
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
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsProductNameValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxProductName.Text);
        }

        private bool IsPriceValid()
        {
            return decimal.TryParse(txtBoxPrice.Text, out decimal price) && price >= 0;
        }

        private bool IsQuantityValid()
        {
            return int.TryParse(txtBoxQuantity.Text, out int quantity) && quantity >= 0;
        }

        private bool IsBarcodeValid()
        {
            return !string.IsNullOrWhiteSpace(txtBoxBarcode.Text);
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

            if (!IsProductNameValid())
            {
                clsFormTheme.ShowInputError(txtBoxProductName, _errorProvider, "Product name cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxProductName, _errorProvider);
            }

            if (!IsPriceValid())
            {
                clsFormTheme.ShowInputError(txtBoxPrice, _errorProvider, "Please enter a valid price (non-negative).");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxPrice, _errorProvider);
            }

            if (!IsQuantityValid())
            {
                clsFormTheme.ShowInputError(txtBoxQuantity, _errorProvider, "Please enter a valid quantity (non-negative).");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxQuantity, _errorProvider);
            }

            if (!IsBarcodeValid())
            {
                clsFormTheme.ShowInputError(txtBoxBarcode, _errorProvider, "Barcode cannot be empty.");
                isValid = false;
            }
            else
            {
                clsFormTheme.ClearInputError(txtBoxBarcode, _errorProvider);
            }

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
                "Add",
                "Adding...");
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
                    MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmAddProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
