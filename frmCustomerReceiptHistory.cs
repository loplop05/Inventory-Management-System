using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCustomerReceiptHistory : Form
    {
        private int? _currentCustomerID = null;

        public frmCustomerReceiptHistory()
        {
            InitializeComponent();
        }

        private void frmCustomerReceiptHistory_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ClearDisplay();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Customer Receipt History", clsFormTheme.Icons.User);
            clsFormTheme.ApplyTextBoxStyle(txtPhoneNumber);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySuccessButtonStyle(btnSelect, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);
            clsFormTheme.ApplyGridStyle(gridOrders);

            btnSearch.Text = "Search";
            btnSearch.Font = new Font(clsFormTheme.MainFontName, 10F);

            btnSelect.Text = "Select";
            btnSelect.Font = new Font(clsFormTheme.MainFontName, 10F);

            btnCancel.Text = "Cancel";
            btnCancel.Font = new Font(clsFormTheme.MainFontName, 10F);

            KeyDown += frmCustomerReceiptHistory_KeyDown;
        }

        private void ClearDisplay()
        {
            SelectedOrderID = null;
            _currentCustomerID = null;
            txtPhoneNumber.Text = "";
            lblCustomerName.Text = "";
            gridOrders.DataSource = null;
            btnSelect.Enabled = false;
            lblInstructions.Text = "Enter customer phone number to view order history";
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please enter a phone number.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return;
            }

            string phoneNumber = txtPhoneNumber.Text.Trim();
            DataTable customer = clsCustomer.GetCustomerByPhone(phoneNumber);

            if (customer == null || customer.Rows.Count == 0)
            {
                MessageBox.Show("Customer not found with this phone number.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCustomerName.Text = "";
                gridOrders.DataSource = null;
                btnSelect.Enabled = false;
                return;
            }

            _currentCustomerID = Convert.ToInt32(customer.Rows[0]["CustomerID"]);
            string customerName = customer.Rows[0]["CustomerName"].ToString();
            lblCustomerName.Text = "Customer: " + customerName;

            LoadCustomerOrders(_currentCustomerID.Value);
        }

        private void LoadCustomerOrders(int customerID)
        {
            DataTable orders = clsCustomer.GetCustomerOrders(customerID);

            if (orders == null || orders.Rows.Count == 0)
            {
                gridOrders.DataSource = null;
                lblInstructions.Text = "No orders found for this customer";
                btnSelect.Enabled = false;
                return;
            }

            gridOrders.AutoGenerateColumns = false;
            gridOrders.DataSource = orders;
            lblInstructions.Text = "Double-click an order or select and click Select to view details";
            btnSelect.Enabled = true;
        }

        private void gridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridOrders.CurrentRow != null)
            {
                SelectOrder();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectOrder();
        }

        private void SelectOrder()
        {
            if (gridOrders.CurrentRow == null)
                return;

            SelectedOrderID = Convert.ToInt32(gridOrders.CurrentRow.Cells["colOrderID"].Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmCustomerReceiptHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, e);
            }
        }
    }
}
