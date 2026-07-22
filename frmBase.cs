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
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }

      
            protected void LoadGrid(DataGridView dgv, object data)
            {
                dgv.DataSource = data;
            }
       




        private void frmBase_Load(object sender, EventArgs e)
        {

        }
    }
}
