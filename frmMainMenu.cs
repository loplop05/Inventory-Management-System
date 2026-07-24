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
            clsFormTheme.ApplySecondaryButtonStyle(btnSuppliers);
            clsFormTheme.ApplySecondaryButtonStyle(btnProducts);
            clsFormTheme.ApplyDangerButtonStyle(button4);

            btnSuppliers.Text = "Suppliers (Coming Soon)";
            btnProducts.Text = "Products (Coming Soon)";
            btnSuppliers.Enabled = false;
            btnProducts.Enabled = false;

            _toolTip.SetToolTip(btnSuppliers, "Supplier screens have not been added yet.");
            _toolTip.SetToolTip(btnProducts, "Product screens have not been added yet.");

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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
