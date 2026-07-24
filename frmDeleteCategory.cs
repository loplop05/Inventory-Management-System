using InventoryBusinessLayer;
using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmDeleteCategory : Form
    {
        public frmDeleteCategory()
        {
            InitializeComponent();
        }

      

        private void txtCategoryID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDelete.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCategoryID.Text.Trim(), out int categoryID))
            {
                MessageBox.Show(
                    "Please enter a valid Category ID.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryID.Focus();
                return;
            }

            clsCategory category = clsCategory.FindCategory(categoryID);

            if (category == null)
            {
                MessageBox.Show(
                    "Category not found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{category.CategoryName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            if (clsCategory.DeleteCategory(categoryID))
            {
                MessageBox.Show(
                    "Category deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Failed to delete category.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}