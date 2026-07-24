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
    public partial class frmMainMenu : Form
    {
        private ToolTip _toolTip = new ToolTip();

        public frmMainMenu()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyPrimaryButtonStyle(btnCategories);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSuppliers);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProducts);
            clsFormTheme.ApplyDangerButtonStyle(button4);

            // Remove 'Coming Soon' labels and enable buttons
            btnSuppliers.Text = "Suppliers";
            btnProducts.Text = "Products";
            btnSuppliers.Enabled = true;
            btnProducts.Enabled = true;

            _toolTip.RemoveAll(); // Clear tooltips as buttons are now active

            KeyDown += frmMainMenu_KeyDown;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            frmCategoriesManagment frm = new frmCategoriesManagment();
            frm.Show();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliersManagment frm = new frmSuppliersManagment();
            frm.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            
            frmProductsManagment frm = new frmProductsManagment();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
