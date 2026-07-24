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
        public frmUpdateCategory()
        {
            InitializeComponent();
        }


        private void UpdateCategory()
        {


            
            if(!int.TryParse(txtUpdateCategoryid.Text.Trim(),out int CategoryID))
            {

                MessageBox.Show(
                    "Please enter a valid Category ID.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUpdateCategoryid.Focus();
                return;

            }

            clsCategory category = clsCategory.FindCategory(CategoryID);


            if (category == null)
            {
                MessageBox.Show(
                    "Category not found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            frmShowCategoryToUpdate frm = new frmShowCategoryToUpdate(category);

            frm.ShowDialog();








        }




        private void txtUpdateCategoryid_KeyDown(object sender, KeyEventArgs e)
        {


            if(e.KeyCode == Keys.Enter)
            {
                UpdateCategory();
            }





        }
    }
}
