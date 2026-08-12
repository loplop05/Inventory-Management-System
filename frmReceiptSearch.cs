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
            clsFormTheme.ApplyTextBoxStyle(_txtOrderID);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySecondaryButtonStyle(_btnViewByPhone, clsFormTheme.Icons.User);
            clsFormTheme.ApplySuccessButtonStyle(_btnExchange, clsFormTheme.Icons.Exchange);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose, clsFormTheme.Icons.Close);
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

            clsSearchHelper.SetupAutoComplete(_txtOrderID, "ReceiptSearch");

            KeyDown += frmReceiptSearch_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Receipt Search");
            _lblHeaderTitle.Text = clsLanguageManager.GetString("Receipt Search");
            _btnSearch.Text = clsLanguageManager.GetString("Search");
            _btnViewByPhone.Text = clsLanguageManager.GetString("By Phone");
            _btnExchange.Text = clsLanguageManager.GetString("Exchange");
            _btnClose.Text = clsLanguageManager.GetString("Close");
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
            string input = _txtOrderID.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                clsFormTheme.ShowWarning(this, "Please enter an Order ID or Customer Phone Number.", "Search");
                _txtOrderID.Focus();
                return;
            }

            clsSearchHelper.UpdateAutoComplete(_txtOrderID, "ReceiptSearch", input);

            if (int.TryParse(input, out int orderID))
            {
                DataTable dt = clsCustomer.GetOrderDetails(orderID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    LoadOrderDetails(orderID);
                    return;
                }
            }

            // Search by Phone
            DataTable customerDt = clsCustomer.GetCustomerByPhone(input);
            if (customerDt != null && customerDt.Rows.Count > 0)
            {
                int custID = Convert.ToInt32(customerDt.Rows[0]["CustomerID"]);
                DataTable orders = clsCustomer.GetCustomerOrders(custID);
                if (orders != null && orders.Rows.Count > 0)
                {
                    int latestOrderID = Convert.ToInt32(orders.Rows[0]["OrderID"]);
                    LoadOrderDetails(latestOrderID);
                    return;
                }
            }

            clsFormTheme.ShowInfo(this, "No matching order or customer found.", "Search");
            ClearDisplay();
        }

        private void LoadOrderDetails(int orderID)
        {
            _currentOrderDetails = clsCustomer.GetOrderDetails(orderID);
            _currentOrderItems = clsCustomer.GetOrderItems(orderID);

            if (_currentOrderDetails == null || _currentOrderDetails.Rows.Count == 0)
            {
                clsFormTheme.ShowInfo(this, "Order not found.", "Search");
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
                clsFormTheme.ShowInfo(this, "No items found for this order.", "Order Items");
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
