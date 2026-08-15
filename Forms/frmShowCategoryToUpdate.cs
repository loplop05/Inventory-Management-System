using InventoryBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmShowCategoryToUpdate : Form
    {
        private clsCategory _Category;
        private ErrorProvider _errorProvider;
        private bool _isUpdating = false;

        public frmShowCategoryToUpdate(clsCategory category)
        {
            InitializeComponent();
            _Category = category;

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            btnUpdate.Text = "Save Changes";
            btnUpdate.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnUpdate, clsFormTheme.Icons.Save);
            clsFormTheme.ApplyTextBoxStyle(txtBoxNewCategory);

            lblCategory.BackColor = Color.Transparent;
            lblCategory.ForeColor = clsFormTheme.HeaderColor;
            lblCategoryID.BackColor = Color.Transparent;
            lblCategoryID.ForeColor = clsFormTheme.TextSecondary;

            btnUpdate.Enabled = false;
            AcceptButton = btnUpdate;
            txtBoxNewCategory.TextChanged += txtBoxNewCategory_TextChanged;
            txtBoxNewCategory.KeyDown += txtBoxNewCategory_KeyDown;
            KeyDown += frmShowCategoryToUpdate_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Edit Category");
            btnUpdate.Text = clsLanguageManager.GetString("Save Changes");
        }

        private void frmShowCategoryToUpdate_Load(object sender, EventArgs e)
        {
            lblCategoryID.Text = _Category.CategoryID.ToString();
            lblCategory.Text = _Category.CategoryName;
            txtBoxNewCategory.Focus();
        }

        private bool IsCategoryNameValid()
        {
            string categoryName = txtBoxNewCategory.Text.Trim();

            return !string.IsNullOrWhiteSpace(categoryName) &&
                   !categoryName.Any(ch => char.IsDigit(ch) ||
                       (!char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)));
        }

        private bool ValidateCategoryName()
        {
            if (string.IsNullOrWhiteSpace(txtBoxNewCategory.Text))
            {
                clsFormTheme.ShowInputError(
                    txtBoxNewCategory,
                    _errorProvider,
                    "Please enter a category name.");

                return false;
            }

            if (!IsCategoryNameValid())
            {
                clsFormTheme.ShowInputError(
                    txtBoxNewCategory,
                    _errorProvider,
                    "Use letters and spaces only.");

                return false;
            }

            clsFormTheme.ClearInputError(txtBoxNewCategory, _errorProvider);
            return true;
        }

        private void SetUpdatingState(bool isUpdating)
        {
            _isUpdating = isUpdating;
            UseWaitCursor = isUpdating;
            txtBoxNewCategory.Enabled = !isUpdating;
            btnUpdate.Enabled = !isUpdating && IsCategoryNameValid();

            clsFormTheme.SetButtonBusy(
                btnUpdate,
                isUpdating,
                "Update",
                "Updating...");
        }

        private void txtBoxNewCategory_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateCategoryName();
            btnUpdate.Enabled = isValid && !_isUpdating;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryName())
                return;

            _Category.CategoryName = txtBoxNewCategory.Text.Trim();
            SetUpdatingState(true);

            try
            {
                clsCategory.enValidateCategory result =
                    await Task.Run(() => _Category.Validate());

                switch (result)
                {
                    case clsCategory.enValidateCategory.InvalidName:
                        clsFormTheme.ShowInputError(
                            txtBoxNewCategory,
                            _errorProvider,
                            "Use letters and spaces only.");
                        return;

                    case clsCategory.enValidateCategory.NameAlreadyExists:
                        clsFormTheme.ShowInputError(
                            txtBoxNewCategory,
                            _errorProvider,
                            "This category already exists.");
                        return;

                    case clsCategory.enValidateCategory.NotFound:
                        clsFormTheme.ShowError(this,
                            "The category no longer exists.",
                            "Error");
                        return;
                }

                bool isSaved = await Task.Run(() => _Category.Save());

                if (isSaved)
                {
                    clsFormTheme.ShowSuccess(this,
                        "Category updated successfully.",
                        "Success");

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this,
                        "Failed to update the category.",
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

        private void txtBoxNewCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmShowCategoryToUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
