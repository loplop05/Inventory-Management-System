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
    public partial class frmUpdateCategory : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSearching = false;

        public frmUpdateCategory()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Update Category", clsFormTheme.Icons.Update);
            btnSearch.Text = "Find Category";
            btnSearch.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplyTextBoxStyle(txtUpdateCategoryid);

            btnSearch.Enabled = false;
            AcceptButton = btnSearch;
            txtUpdateCategoryid.TextChanged += txtUpdateCategoryid_TextChanged;
            KeyDown += frmUpdateCategory_KeyDown;
        }

        private bool IsCategoryIDValid()
        {
            return int.TryParse(txtUpdateCategoryid.Text.Trim(), out int categoryID) &&
                   categoryID > 0;
        }

        private bool ValidateCategoryID()
        {
            if (!int.TryParse(txtUpdateCategoryid.Text.Trim(), out int categoryID) || categoryID <= 0)
            {
                clsFormTheme.ShowInputError(
                    txtUpdateCategoryid,
                    _errorProvider,
                    "Please enter a valid category ID.");

                return false;
            }

            clsFormTheme.ClearInputError(txtUpdateCategoryid, _errorProvider);
            return true;
        }

        private void SetSearchingState(bool isSearching)
        {
            _isSearching = isSearching;
            UseWaitCursor = isSearching;
            txtUpdateCategoryid.Enabled = !isSearching;
            btnSearch.Enabled = !isSearching && IsCategoryIDValid();

            clsFormTheme.SetButtonBusy(
                btnSearch,
                isSearching,
                "Search",
                "Searching...");
        }

        private void txtUpdateCategoryid_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateCategoryID();
            btnSearch.Enabled = isValid && !_isSearching;
        }

        private async Task UpdateCategory()
        {
            if (!ValidateCategoryID())
                return;

            int categoryID = Convert.ToInt32(txtUpdateCategoryid.Text.Trim());

            SetSearchingState(true);

            try
            {
                clsCategory category = await Task.Run(() => clsCategory.FindCategory(categoryID));

                if (category == null)
                {
                    clsFormTheme.ShowInputError(
                        txtUpdateCategoryid,
                        _errorProvider,
                        "Category not found.");
                    return;
                }

                frmShowCategoryToUpdate frm = new frmShowCategoryToUpdate(category);

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
            await UpdateCategory();
        }

        private async void txtUpdateCategoryid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await UpdateCategory();
                e.SuppressKeyPress = true;
            }
        }

        private void frmUpdateCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
