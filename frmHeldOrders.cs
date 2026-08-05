using System;
using System.Data;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmHeldOrders : Form
    {
        public clsHeldOrderData.HeldOrderInfo SelectedHeldOrder { get; private set; }

        public frmHeldOrders()
        {
            InitializeComponent();
        }

        private void frmHeldOrders_Load(object sender, EventArgs e)
        {
            // Ensure HeldOrders tables exist in database
            string migrationError;
            if (!clsDatabaseMigration.EnsureHeldOrdersTablesExist(out migrationError))
            {
                MessageBox.Show("Failed to initialize held orders tables: " + migrationError, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Held Orders", clsFormTheme.Icons.Save);

            clsFormTheme.ApplyGridStyle(gridHeldOrders);
            clsFormTheme.ApplyPrimaryButtonStyle(btnRetrieve, clsFormTheme.Icons.Add);
            clsFormTheme.ApplyDangerButtonStyle(btnDelete, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh, clsFormTheme.Icons.Refresh);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);

            LoadHeldOrders();
            clsLanguageManager.ApplyLanguage(this);
        }

        private void LoadHeldOrders()
        {
            string errorMessage;
            DataTable heldOrders = clsHeldOrder.GetAllHeldOrders(out errorMessage);

            if (heldOrders == null)
            {
                MessageBox.Show("Failed to load held orders: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridHeldOrders.DataSource = heldOrders;
            gridHeldOrders.AutoGenerateColumns = false;

            // Configure columns
            gridHeldOrders.Columns.Clear();

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HeldOrderID",
                DataPropertyName = "HeldOrderID",
                HeaderText = "ID",
                Width = 60
            });

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                DataPropertyName = "CreatedDate",
                HeaderText = "Date",
                Width = 150
            });

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 150
            });

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ItemCount",
                DataPropertyName = "ItemCount",
                HeaderText = "Items",
                Width = 60
            });

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                DataPropertyName = "TotalAmount",
                HeaderText = "Total",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            gridHeldOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Notes",
                DataPropertyName = "Notes",
                HeaderText = "Notes",
                Width = 200
            });

            gridHeldOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridHeldOrders.ReadOnly = true;
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (gridHeldOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select a held order to retrieve.", "Retrieve", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int heldOrderID = Convert.ToInt32(gridHeldOrders.CurrentRow.Cells["HeldOrderID"].Value);

            string errorMessage;
            SelectedHeldOrder = clsHeldOrder.GetHeldOrder(heldOrderID, out errorMessage);

            if (SelectedHeldOrder != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Failed to retrieve held order: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridHeldOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select a held order to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int heldOrderID = Convert.ToInt32(gridHeldOrders.CurrentRow.Cells["HeldOrderID"].Value);

            var result = MessageBox.Show("Are you sure you want to delete this held order?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string errorMessage;
                if (clsHeldOrder.DeleteHeldOrder(heldOrderID, out errorMessage))
                {
                    MessageBox.Show("Held order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHeldOrders();
                }
                else
                {
                    MessageBox.Show("Failed to delete held order: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadHeldOrders();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void gridHeldOrders_DoubleClick(object sender, EventArgs e)
        {
            btnRetrieve_Click(null, null);
        }
    }
}
