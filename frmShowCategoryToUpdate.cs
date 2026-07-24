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

        public frmShowCategoryToUpdate(clsCategory category)
        {
            InitializeComponent();
            _Category = category;

        }

        private void frmShowCategoryToUpdate_Load(object sender, EventArgs e)
        {
            lblCategoryID.Text = _Category.CategoryID.ToString();

            lblCategory.Text = _Category.CategoryName;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _Category.CategoryName = txtBoxNewCategory.Text.Trim();



            switch (_Category.Validate())
            {
                case clsCategory.enValidateCategory.InvalidName:

                    MessageBox.Show("Invalid Category Name.");
                    return;

                case clsCategory.enValidateCategory.NameAlreadyExists:

                    MessageBox.Show("Category already exists.");
                    return;
            }

            if (_Category.Save())
            {
                MessageBox.Show("Category Updated Successfully.");

                DialogResult = DialogResult.OK;
                Close();
                
            }
            else
            {
                MessageBox.Show("Update Failed.");
            }
        }










    }
    }

