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
    public partial class frmAddCategory : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSaving = false;

        public frmAddCategory()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Add New Category", clsFormTheme.Icons.Add);
            btnAdd.Text = "Save Category";
            btnAdd.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Save);
            clsFormTheme.ApplyTextBoxStyle(txtBoxCategoryName);

            btnAdd.Enabled = false;
            AcceptButton = btnAdd;
            KeyDown += frmAddCategory_KeyDown;
        }

        public static bool ContainsNumbersAndSpecial(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return input.Any(ch => char.IsDigit(ch) ||
                                  (!char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)));
        }

        private bool IsCategoryNameValid()
        {
            string categoryName = txtBoxCategoryName.Text.Trim();

            return !string.IsNullOrWhiteSpace(categoryName) &&
                   !ContainsNumbersAndSpecial(categoryName);
        }

        private bool ValidateCategoryName()
        {
            if (string.IsNullOrWhiteSpace(txtBoxCategoryName.Text))
            {
                clsFormTheme.ShowInputError(
                    txtBoxCategoryName,
                    _errorProvider,
                    "Please enter a category name.");

                return false;
            }

            if (ContainsNumbersAndSpecial(txtBoxCategoryName.Text.Trim()))
            {
                clsFormTheme.ShowInputError(
                    txtBoxCategoryName,
                    _errorProvider,
                    "Use letters and spaces only.");

                return false;
            }

            clsFormTheme.ClearInputError(txtBoxCategoryName, _errorProvider);
            return true;
        }

        private void SetSavingState(bool isSaving)
        {
            _isSaving = isSaving;
            UseWaitCursor = isSaving;
            txtBoxCategoryName.Enabled = !isSaving;
            btnAdd.Enabled = !isSaving && IsCategoryNameValid();

            clsFormTheme.SetButtonBusy(
                btnAdd,
                isSaving,
                "Add",
                "Adding...");
        }

        private void txtBoxCategoryName_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateCategoryName();
            btnAdd.Enabled = isValid && !_isSaving;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryName())
                return;

            clsCategory category = new clsCategory();
            category.CategoryName = txtBoxCategoryName.Text.Trim();

            SetSavingState(true);

            try
            {
                clsCategory.enValidateCategory result =
                    await Task.Run(() => category.Validate());

                switch (result)
                {
                    case clsCategory.enValidateCategory.NameAlreadyExists:
                        clsFormTheme.ShowInputError(
                            txtBoxCategoryName,
                            _errorProvider,
                            "This category already exists.");
                        return;

                    case clsCategory.enValidateCategory.InvalidName:
                        clsFormTheme.ShowInputError(
                            txtBoxCategoryName,
                            _errorProvider,
                            "Use letters and spaces only.");
                        return;

                    case clsCategory.enValidateCategory.NotFound:
                        MessageBox.Show(
                            "The category could not be found.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                }

                bool isSaved = await Task.Run(() => category.Save());

                if (isSaved)
                {
                    MessageBox.Show(
                        "Category added successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtBoxCategoryName.Clear();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to add the category.",
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

        private void txtBoxCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmAddCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
