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
            clsFormTheme.ApplyTextBoxStyle(txtExchangeQuantity);
            clsFormTheme.ApplyTextBoxStyle(txtExchangeReason);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProcessExchange, clsFormTheme.Icons.Exchange);
            clsFormTheme.ApplyDangerButtonStyle(btnRemoveExchange, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplySuccessButtonStyle(btnConfirmExchange, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            clsFormTheme.ApplyGridStyle(gridOriginalItems);
            clsFormTheme.ApplyGridStyle(gridNewItems);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: null,
                onSearch: null,
                onAdd: null
            );

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Product Exchange");
            btnProcessExchange.Text = clsLanguageManager.GetString("Add");
            btnRemoveExchange.Text = clsLanguageManager.GetString("Remove");
            btnConfirmExchange.Text = clsLanguageManager.GetString("Confirm Exchange");
            btnClose.Text = clsLanguageManager.GetString("Close");
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
                clsFormTheme.ShowWarning(this,
                    $"This order is {daysSincePurchase.Days} days old. Exchange policy allows exchanges within {ExchangeDaysLimit} days of purchase.",
                    "Exchange Policy");
                btnProcessExchange.Enabled = false;
                btnConfirmExchange.Enabled = false;
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
                clsFormTheme.ShowWarning(this, "Please select an item to exchange.", "Exchange");
                return;
            }

            int quantity;
            if (!int.TryParse(txtExchangeQuantity.Text, out quantity) || quantity <= 0)
            {
                clsFormTheme.ShowWarning(this, "Please enter a valid quantity.", "Exchange");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtExchangeReason.Text))
            {
                clsFormTheme.ShowWarning(this, "Please provide a reason for the exchange.", "Exchange");
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
                clsFormTheme.ShowWarning(this, $"Cannot exchange more than the original quantity ({originalQuantity}).", "Exchange");
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
            decimal refundAmount = _newTotal;
            lblPriceDifference.Text = $"Refund / Credit amount: {refundAmount:C2}";
            lblPriceDifference.ForeColor = clsFormTheme.CurrentSuccessColor;
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

        private void btnConfirmExchange_Click(object sender, EventArgs e)
        {
            if (_exchangeItems.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please add at least one item to exchange before confirming.", "Exchange");
                return;
            }

            var returnedItems = _exchangeItems.Select(item => new InventoryDataAccessLayer.clsPOSData.ExchangeItemInfo
            {
                ProductID = item.ProductID,
                ProductName = item.ProductName,
                ReturnedQuantity = item.ExchangeQuantity,
                UnitPrice = item.UnitPrice,
                Reason = item.Reason
            }).ToList();

            string errorMessage;
            bool success = clsPOS.ProcessExchange(_originalOrderID, returnedItems, null, out errorMessage);

            if (success)
            {
                decimal refundAmount = _exchangeItems.Sum(i => i.ExchangeQuantity * i.UnitPrice);
                clsFormTheme.ShowSuccess(this, $"Exchange processed successfully!\n\nTotal credit/refund: {refundAmount:C2}\nInventory stock has been restocked.", "Exchange Complete");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to process exchange: " + errorMessage, "Error");
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
