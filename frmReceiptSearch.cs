using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmReceiptSearch : Form
    {
        private int _currentOrderID = -1;
        private DataTable _currentOrderDetails = null;
        private DataTable _currentOrderItems = null;

        public frmReceiptSearch()
        {
            InitializeComponent();
        }

        private void frmReceiptSearch_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ClearDisplay();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Search Receipt", clsFormTheme.Icons.Search);
            clsFormTheme.ApplyTextBoxStyle(txtOrderID);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch);
            clsFormTheme.ApplySecondaryButtonStyle(btnViewByPhone);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
            clsFormTheme.ApplySuccessButtonStyle(btnExchange);
            clsFormTheme.ApplyGridStyle(gridOrderItems);

            btnSearch.Text = clsFormTheme.Icons.Search + "  Search";
            btnSearch.Font = new Font(clsFormTheme.IconFontName, 10F);

            btnViewByPhone.Text = clsFormTheme.Icons.User + "  By Phone";
            btnViewByPhone.Font = new Font(clsFormTheme.IconFontName, 10F);

            btnExchange.Text = clsFormTheme.Icons.Exchange + "  Exchange";
            btnExchange.Font = new Font(clsFormTheme.IconFontName, 10F);

            btnClose.Text = clsFormTheme.Icons.Exit + "  Close";
            btnClose.Font = new Font(clsFormTheme.IconFontName, 10F);

            KeyDown += frmReceiptSearch_KeyDown;
        }

        private void ClearDisplay()
        {
            _currentOrderID = -1;
            _currentOrderDetails = null;
            _currentOrderItems = null;

            txtOrderID.Text = "";
            lblTitle.Text = "";
            lblOrderInfo.Text = "";
            lblCustomerName.Text = "";
            lblCustomerPhone.Text = "";
            lblPaymentInfo.Text = "";
            gridOrderItems.DataSource = null;
            btnExchange.Enabled = false;
        }

        private void txtOrderID_KeyDown(object sender, KeyEventArgs e)
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
            if (string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("Please enter an Order ID.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderID.Focus();
                return;
            }

            int orderID;
            if (!int.TryParse(txtOrderID.Text.Trim(), out orderID))
            {
                MessageBox.Show("Invalid Order ID format.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderID.Focus();
                return;
            }

            LoadOrderDetails(orderID);
        }

        private void LoadOrderDetails(int orderID)
        {
            _currentOrderDetails = clsCustomer.GetOrderDetails(orderID);
            _currentOrderItems = clsCustomer.GetOrderItems(orderID);

            if (_currentOrderDetails == null || _currentOrderDetails.Rows.Count == 0)
            {
                MessageBox.Show("Order not found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearDisplay();
                return;
            }

            _currentOrderID = orderID;
            DisplayOrderDetails();
        }

        private void DisplayOrderDetails()
        {
            if (_currentOrderDetails == null || _currentOrderDetails.Rows.Count == 0)
                return;

            DataRow order = _currentOrderDetails.Rows[0];

            // Display order info
            DateTime orderDate = Convert.ToDateTime(order["OrderDate"]);
            decimal subtotal = Convert.ToDecimal(order["Subtotal"]);
            decimal taxAmount = Convert.ToDecimal(order["TaxAmount"]);
            decimal totalAmount = Convert.ToDecimal(order["TotalAmount"]);

            lblTitle.Text = "Order #" + _currentOrderID;
            lblOrderInfo.Text = $"Date: {orderDate:yyyy-MM-dd HH:mm} | Subtotal: {subtotal:C2} | Tax: {taxAmount:C2} | Total: {totalAmount:C2}";

            // Display customer info
            if (order["CustomerID"] != DBNull.Value && order["CustomerID"] != null)
            {
                lblCustomerName.Text = "Customer: " + (order["CustomerName"] != DBNull.Value ? order["CustomerName"].ToString() : "Unknown");
                lblCustomerPhone.Text = "Phone: " + (order["PhoneNumber"] != DBNull.Value ? order["PhoneNumber"].ToString() : "N/A");
            }
            else
            {
                lblCustomerName.Text = "Customer: Walk-in";
                lblCustomerPhone.Text = "";
            }

            // Display payment info
            string paymentMethod = order["PaymentMethod"] != DBNull.Value ? order["PaymentMethod"].ToString() : "Cash";
            string paymentDetails = order["PaymentDetails"] != DBNull.Value ? order["PaymentDetails"].ToString() : "";
            lblPaymentInfo.Text = "Payment: " + paymentMethod + (string.IsNullOrEmpty(paymentDetails) ? "" : " (" + paymentDetails + ")");

            // Display order items
            if (_currentOrderItems != null && _currentOrderItems.Rows.Count > 0)
            {
                gridOrderItems.AutoGenerateColumns = false;
                gridOrderItems.DataSource = _currentOrderItems;
                btnExchange.Enabled = true;
            }
            else
            {
                gridOrderItems.DataSource = null;
                MessageBox.Show("No items found for this order.", "Order Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnExchange.Enabled = false;
            }
        }

        private void btnViewByPhone_Click(object sender, EventArgs e)
        {
            using (frmCustomerReceiptHistory historyForm = new frmCustomerReceiptHistory())
            {
                if (historyForm.ShowDialog(this) == DialogResult.OK)
                {
                    // If an order was selected from history, load it
                    if (historyForm.SelectedOrderID.HasValue)
                    {
                        txtOrderID.Text = historyForm.SelectedOrderID.Value.ToString();
                        LoadOrderDetails(historyForm.SelectedOrderID.Value);
                    }
                }
            }
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
                return;

            using (frmExchange exchangeForm = new frmExchange(_currentOrderID, _currentOrderDetails, _currentOrderItems))
            {
                exchangeForm.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmReceiptSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }
    }
}
