using InventoryBusinessLayer;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmDeleteCategory : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isDeleting = false;

        public frmDeleteCategory()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Delete Category", clsFormTheme.Icons.Delete);
            clsFormTheme.ApplyDangerButtonStyle(btnDelete, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplyTextBoxStyle(txtCategoryID);

            btnDelete.Enabled = false;
            AcceptButton = btnDelete;
            txtCategoryID.TextChanged += txtCategoryID_TextChanged;
            KeyDown += frmDeleteCategory_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Delete Category");
        }

        private bool IsCategoryIDValid()
        {
            return int.TryParse(txtCategoryID.Text.Trim(), out int categoryID) &&
                   categoryID > 0;
        }

        private bool ValidateCategoryID()
        {
            if (!int.TryParse(txtCategoryID.Text.Trim(), out int categoryID) || categoryID <= 0)
            {
                clsFormTheme.ShowInputError(
                    txtCategoryID,
                    _errorProvider,
                    "Please enter a valid category ID.");

                return false;
            }

            clsFormTheme.ClearInputError(txtCategoryID, _errorProvider);
            return true;
        }

        private void SetDeletingState(bool isDeleting)
        {
            _isDeleting = isDeleting;
            UseWaitCursor = isDeleting;
            txtCategoryID.Enabled = !isDeleting;
            btnDelete.Enabled = !isDeleting && IsCategoryIDValid();

            clsFormTheme.SetButtonBusy(
                btnDelete,
                isDeleting,
                "Delete",
                "Working...");
        }

        private void txtCategoryID_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateCategoryID();
            btnDelete.Enabled = isValid && !_isDeleting;
        }

        private void txtCategoryID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDelete.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryID())
                return;

            int categoryID = Convert.ToInt32(txtCategoryID.Text.Trim());

            SetDeletingState(true);

            try
            {
                clsCategory category = await Task.Run(() => clsCategory.FindCategory(categoryID));

                if (category == null)
                {
                    clsFormTheme.ShowInputError(
                        txtCategoryID,
                        _errorProvider,
                        "Category not found.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{category.CategoryName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                bool isDeleted = await Task.Run(() => clsCategory.DeleteCategory(categoryID));

                if (isDeleted)
                {
                    MessageBox.Show(
                        "Category deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to delete the category. It may be used by a product.",
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

        private void frmDeleteCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
