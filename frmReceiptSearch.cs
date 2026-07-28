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
            clsFormTheme.CreateHeaderPanel(this, "Receipt Search", clsFormTheme.Icons.Search);
            clsFormTheme.ApplyTextBoxStyle(_txtOrderID);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySecondaryButtonStyle(_btnViewByPhone, clsFormTheme.Icons.User);
            clsFormTheme.ApplySuccessButtonStyle(_btnExchange, clsFormTheme.Icons.Exchange);
            clsFormTheme.ApplyGridStyle(_gridOrderItems);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: null,
                onSearch: () => _txtOrderID.Focus(),
                onAdd: null
            );

            _btnSearch.Text = "Search";
            _btnSearch.Font = new Font(clsFormTheme.MainFontName, 10F);

            _btnViewByPhone.Text = "By Phone";
            _btnViewByPhone.Font = new Font(clsFormTheme.MainFontName, 10F);

            _btnExchange.Text = "Exchange";
            _btnExchange.Font = new Font(clsFormTheme.MainFontName, 10F);

            KeyDown += frmReceiptSearch_KeyDown;
        }

        private void ClearDisplay()
        {
            _currentOrderID = -1;
            _currentOrderDetails = null;
            _currentOrderItems = null;

            _txtOrderID.Text = "";
            _lblOrderInfo.Text = "";
            _lblCustomerName.Text = "";
            _lblCustomerPhone.Text = "";
            _lblPaymentInfo.Text = "";
            _gridOrderItems.DataSource = null;
            _btnExchange.Enabled = false;
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
            if (string.IsNullOrWhiteSpace(_txtOrderID.Text))
            {
                MessageBox.Show("Please enter an Order ID.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtOrderID.Focus();
                return;
            }

            int orderID;
            if (!int.TryParse(_txtOrderID.Text.Trim(), out orderID))
            {
                MessageBox.Show("Invalid Order ID format.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtOrderID.Focus();
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

            _lblOrderInfo.Text = $"Order #{_currentOrderID} | Date: {orderDate:yyyy-MM-dd HH:mm} | Subtotal: {subtotal:C2} | Tax: {taxAmount:C2} | Total: {totalAmount:C2}";

            // Display customer info
            if (order["CustomerID"] != DBNull.Value && order["CustomerID"] != null)
            {
                _lblCustomerName.Text = "Customer: " + (order["CustomerName"] != DBNull.Value ? order["CustomerName"].ToString() : "Unknown");
                _lblCustomerPhone.Text = "Phone: " + (order["PhoneNumber"] != DBNull.Value ? order["PhoneNumber"].ToString() : "N/A");
            }
            else
            {
                _lblCustomerName.Text = "Customer: Walk-in";
                _lblCustomerPhone.Text = "";
            }

            // Display payment info
            string paymentMethod = order["PaymentMethod"] != DBNull.Value ? order["PaymentMethod"].ToString() : "Cash";
            string paymentDetails = order["PaymentDetails"] != DBNull.Value ? order["PaymentDetails"].ToString() : "";
            _lblPaymentInfo.Text = "Payment: " + paymentMethod + (string.IsNullOrEmpty(paymentDetails) ? "" : " (" + paymentDetails + ")");

            // Display order items
            if (_currentOrderItems != null && _currentOrderItems.Rows.Count > 0)
            {
                _gridOrderItems.AutoGenerateColumns = false;
                _gridOrderItems.DataSource = _currentOrderItems;
                _btnExchange.Enabled = true;
            }
            else
            {
                _gridOrderItems.DataSource = null;
                MessageBox.Show("No items found for this order.", "Order Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _btnExchange.Enabled = false;
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
                        _txtOrderID.Text = historyForm.SelectedOrderID.Value.ToString();
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
