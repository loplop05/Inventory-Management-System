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

            frmShowCategoryToUpdate frm = new frmShowCategoryToUpdate();

            
            if(int.TryParse(txtUpdateCategoryid.Text.Trim(),out int CategoryID))
            {





            }







        }




        private void txtUpdateCategoryid_KeyDown(object sender, KeyEventArgs e)
        {


            if(e.KeyCode == Keys.Enter)
            {
                
            }





        }
    }
}
