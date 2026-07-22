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
    public partial class frmAddCategory : frmBase
    {
        public frmAddCategory()
        {
            InitializeComponent();
        }

        public static bool ContainsNumbersAndSpecial(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;


            return input.Any(ch => char.IsDigit(ch) ||
                                  (!char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)));
        }



        private void txtBoxCategoryName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsCategory category = new clsCategory();

            category.CategoryName = txtBoxCategoryName.Text.Trim();


            clsCategory.enValidateCategory result = category.Validate();


            switch (result)
            {
                case clsCategory.enValidateCategory.NameAlreadyExists:

                    MessageBox.Show(
                        "Category already exists",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;


                case clsCategory.enValidateCategory.InvalidName:

                    MessageBox.Show(
                        "Invalid category name",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;


                case clsCategory.enValidateCategory.Success:

                    if (category.Save())
                    {
                        MessageBox.Show(
                            "Category Added Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);


                        txtBoxCategoryName.Clear();
                        txtBoxCategoryName.Focus();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed To Add Category");
                    }

                    break;
            }
        }

        private void txtBoxCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}


