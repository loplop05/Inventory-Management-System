using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCategoriesManagment : Form
    {
        public frmCategoriesManagment()
        {
            InitializeComponent();
        }

        private void RefreshGridData()
        {
            try
            {
                DataGVCategories.DataSource = clsCategory.GetAllCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnBackToPrevPage_Click(object sender, EventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Close();
           
           
        }


    

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmCategoriesManagment_Load(object sender, EventArgs e)
        {
            RefreshGridData();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmAddCategory frm = new frmAddCategory();
            frm.Show();
        }
    }
}
