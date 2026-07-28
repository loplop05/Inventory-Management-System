using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmExchange : Form
    {
        private readonly int _originalOrderID;
        private readonly DataTable _originalOrderDetails;
        private readonly DataTable _originalOrderItems;
        
        private readonly List<ExchangeItem> _exchangeItems = new List<ExchangeItem>();
        private decimal _originalTotal = 0;
        private decimal _newTotal = 0;

        // Exchange policy: 30 days from purchase date
        private const int ExchangeDaysLimit = 30;

        public frmExchange(int orderID, DataTable orderDetails, DataTable orderItems)
        {
            InitializeComponent();
            _originalOrderID = orderID;
            _originalOrderDetails = orderDetails;
            _originalOrderItems = orderItems;
        }

        private void frmExchange_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadOriginalOrder();
            LoadExchangePolicy();
            PopulateExchangeItems();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Product Exchange", clsFormTheme.Icons.Exchange);
            clsFormTheme.ApplyTextBoxStyle(txtExchangeQuantity);
            clsFormTheme.ApplyTextBoxStyle(txtExchangeReason);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProcessExchange, clsFormTheme.Icons.Exchange);
            clsFormTheme.ApplyDangerButtonStyle(btnRemoveExchange, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            clsFormTheme.ApplyGridStyle(gridOriginalItems);
            clsFormTheme.ApplyGridStyle(gridNewItems);

            btnProcessExchange.Text = "Add";
            btnProcessExchange.Font = new Font(clsFormTheme.MainFontName, 10F);

            btnRemoveExchange.Text = "Remove";
            btnRemoveExchange.Font = new Font(clsFormTheme.MainFontName, 10F);

            btnClose.Text = "Close";
            btnClose.Font = new Font(clsFormTheme.MainFontName, 10F);

            KeyDown += frmExchange_KeyDown;
        }

        private void LoadOriginalOrder()
        {
            if (_originalOrderDetails == null || _originalOrderDetails.Rows.Count == 0)
                return;

            DataRow order = _originalOrderDetails.Rows[0];
            DateTime orderDate = Convert.ToDateTime(order["OrderDate"]);
            decimal totalAmount = Convert.ToDecimal(order["TotalAmount"]);

            _originalTotal = totalAmount;

            // Check if order is within exchange period
            TimeSpan daysSincePurchase = DateTime.Now - orderDate;
            if (daysSincePurchase.TotalDays > ExchangeDaysLimit)
            {
                MessageBox.Show(
                    $"This order is {daysSincePurchase.Days} days old. Exchange policy allows exchanges within {ExchangeDaysLimit} days of purchase.",
                    "Exchange Policy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                btnProcessExchange.Enabled = false;
            }

            lblOriginalOrderInfo.Text = $"Original Order #{_originalOrderID} - {orderDate:yyyy-MM-dd} - Total: {totalAmount:C2}";

            gridOriginalItems.AutoGenerateColumns = false;
            gridOriginalItems.DataSource = _originalOrderItems;
        }

        private void LoadExchangePolicy()
        {
            lblExchangePolicy.Text = $"Exchange Policy: Items can be exchanged within {ExchangeDaysLimit} days of purchase. Price difference will be calculated.";
        }

        private void PopulateExchangeItems()
        {
            cmbExchangeItem.Items.Clear();

            if (_originalOrderItems == null)
                return;

            foreach (DataRow row in _originalOrderItems.Rows)
            {
                string productName = row["ProductName"].ToString();
                int quantity = Convert.ToInt32(row["Quantity"]);
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);

                cmbExchangeItem.Items.Add($"{productName} (Qty: {quantity}, Price: {unitPrice:C2})");
            }

            if (cmbExchangeItem.Items.Count > 0)
                cmbExchangeItem.SelectedIndex = 0;
        }

        private void btnProcessExchange_Click(object sender, EventArgs e)
        {
            if (cmbExchangeItem.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an item to exchange.", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity;
            if (!int.TryParse(txtExchangeQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtExchangeReason.Text))
            {
                MessageBox.Show("Please provide a reason for the exchange.", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected item details
            DataRow selectedItem = _originalOrderItems.Rows[cmbExchangeItem.SelectedIndex];
            int productID = Convert.ToInt32(selectedItem["ProductID"]);
            string productName = selectedItem["ProductName"].ToString();
            int originalQuantity = Convert.ToInt32(selectedItem["Quantity"]);
            decimal unitPrice = Convert.ToDecimal(selectedItem["UnitPrice"]);

            if (quantity > originalQuantity)
            {
                MessageBox.Show($"Cannot exchange more than the original quantity ({originalQuantity}).", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add to exchange list
            var exchangeItem = new ExchangeItem
            {
                ProductID = productID,
                ProductName = productName,
                OriginalQuantity = originalQuantity,
                ExchangeQuantity = quantity,
                UnitPrice = unitPrice,
                Reason = txtExchangeReason.Text.Trim()
            };

            _exchangeItems.Add(exchangeItem);

            // Update display
            UpdateExchangeDisplay();

            // Clear inputs
            txtExchangeQuantity.Text = "";
            txtExchangeReason.Text = "";
        }

        private void UpdateExchangeDisplay()
        {
            if (_exchangeItems.Count == 0)
            {
                gridNewItems.DataSource = null;
                lblNewOrderInfo.Text = "Exchange Items";
                lblPriceDifference.Text = "";
                return;
            }

            // Create table for exchange items
            DataTable exchangeTable = new DataTable();
            exchangeTable.Columns.Add("ProductName", typeof(string));
            exchangeTable.Columns.Add("Quantity", typeof(int));
            exchangeTable.Columns.Add("UnitPrice", typeof(decimal));

            _newTotal = 0;

            foreach (var item in _exchangeItems)
            {
                exchangeTable.Rows.Add(item.ProductName, item.ExchangeQuantity, item.UnitPrice);
                _newTotal += item.ExchangeQuantity * item.UnitPrice;
            }

            gridNewItems.AutoGenerateColumns = false;
            gridNewItems.DataSource = exchangeTable;

            lblNewOrderInfo.Text = "Exchange Items";

            // Calculate price difference
            decimal difference = _newTotal - _originalTotal;
            if (difference > 0)
            {
                lblPriceDifference.Text = $"Additional payment required: {difference:C2}";
                lblPriceDifference.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else if (difference < 0)
            {
                lblPriceDifference.Text = $"Refund amount: {Math.Abs(difference):C2}";
                lblPriceDifference.ForeColor = Color.FromArgb(40, 167, 69);
            }
            else
            {
                lblPriceDifference.Text = "No price difference";
                lblPriceDifference.ForeColor = Color.FromArgb(44, 62, 80);
            }
        }

        private void btnRemoveExchange_Click(object sender, EventArgs e)
        {
            if (gridNewItems.CurrentRow == null)
                return;

            int index = gridNewItems.CurrentRow.Index;
            if (index >= 0 && index < _exchangeItems.Count)
            {
                _exchangeItems.RemoveAt(index);
                UpdateExchangeDisplay();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExchange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private class ExchangeItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int OriginalQuantity { get; set; }
            public int ExchangeQuantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Reason { get; set; }
        }
    }
}
