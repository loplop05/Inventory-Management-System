using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmRefund : Form
    {
        private readonly int _orderID;
        private DataTable _orderDetails;
        private DataTable _orderItems;
        private List<RefundItem> _selectedRefundItems = new List<RefundItem>();

        public frmRefund(int orderID)
        {
            InitializeComponent();
            _orderID = orderID;
        }

        private void frmRefund_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadOrderData();
            LoadOrderItems();
            CalculateRefundAmount();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(txtRefundReason);
            clsFormTheme.ApplyComboBoxStyle(cboRefundMethod);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProcessRefund, clsFormTheme.Icons.Refresh);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            clsFormTheme.ApplyGridStyle(gridOrderItems);

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
            Text = clsLanguageManager.GetString("Process Refund");
            btnProcessRefund.Text = clsLanguageManager.GetString("Process Refund");
            btnClose.Text = clsLanguageManager.GetString("Close");
            lblRefundType.Text = clsLanguageManager.GetString("Refund Type");
            lblRefundMethod.Text = clsLanguageManager.GetString("Refund Method");
            lblRefundReason.Text = clsLanguageManager.GetString("Reason");
            lblOrderItems.Text = clsLanguageManager.GetString("Order Items");
            lblRefundAmount.Text = clsLanguageManager.GetString("Refund Amount");
        }

        private void LoadOrderData()
        {
            _orderDetails = clsCustomer.GetOrderDetails(_orderID);
            if (_orderDetails == null || _orderDetails.Rows.Count == 0)
            {
                clsFormTheme.ShowError(this, "Order not found.", "Refund");
                Close();
                return;
            }

            DataRow order = _orderDetails.Rows[0];
            DateTime orderDate = Convert.ToDateTime(order["OrderDate"]);
            decimal totalAmount = Convert.ToDecimal(order["TotalAmount"]);
            bool isVoided = order["IsVoided"] != DBNull.Value && Convert.ToBoolean(order["IsVoided"]);

            // Check if order is voided
            if (isVoided)
            {
                clsFormTheme.ShowWarning(this, "Cannot refund a voided order.", "Refund");
                btnProcessRefund.Enabled = false;
            }

            // Check if order is already refunded
            if (order["RefundID"] != DBNull.Value)
            {
                clsFormTheme.ShowWarning(this, "Order has already been refunded.", "Refund");
                btnProcessRefund.Enabled = false;
            }

            // Check return policy (30 days)
            TimeSpan daysSincePurchase = DateTime.Now - orderDate;
            if (daysSincePurchase.TotalDays > 30)
            {
                clsFormTheme.ShowWarning(this,
                    $"This order is {daysSincePurchase.Days} days old. Return policy allows refunds within 30 days of purchase.",
                    "Return Policy");
            }

            lblOrderInfo.Text = $"Order #{_orderID} - {orderDate:yyyy-MM-dd} - Total: {totalAmount:C2}";
            lblOrderTotal.Text = totalAmount.ToString("C2");

            // Populate refund method combo
            cboRefundMethod.Items.Clear();
            cboRefundMethod.Items.AddRange(new object[] { "Cash", "Card", "Store Credit" });
            cboRefundMethod.SelectedIndex = 0;
        }

        private void LoadOrderItems()
        {
            _orderItems = clsCustomer.GetOrderItems(_orderID);
            if (_orderItems != null)
            {
                gridOrderItems.DataSource = _orderItems;
                
                // Add checkbox column for selecting items to refund
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Refund",
                    Width = 50
                };
                gridOrderItems.Columns.Insert(0, chkColumn);

                // Add quantity column for partial refund
                DataGridViewTextBoxColumn qtyColumn = new DataGridViewTextBoxColumn
                {
                    Name = "RefundQty",
                    HeaderText = "Qty",
                    Width = 50,
                    MaxInputLength = 3
                };
                gridOrderItems.Columns.Insert(1, qtyColumn);

                // Initialize refund quantities
                foreach (DataGridViewRow row in gridOrderItems.Rows)
                {
                    row.Cells["RefundQty"].Value = row.Cells["Quantity"].Value;
                }
            }
        }

        private void CalculateRefundAmount()
        {
            decimal refundAmount = 0;
            _selectedRefundItems.Clear();

            foreach (DataGridViewRow row in gridOrderItems.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                {
                    int productID = Convert.ToInt32(row.Cells["ProductID"].Value);
                    string productName = row.Cells["ProductName"].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells["RefundQty"].Value);
                    decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    int maxQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                    // Validate quantity
                    if (quantity <= 0 || quantity > maxQuantity)
                    {
                        row.Cells["RefundQty"].Value = maxQuantity;
                        quantity = maxQuantity;
                    }

                    decimal itemRefundAmount = quantity * unitPrice;
                    refundAmount += itemRefundAmount;

                    _selectedRefundItems.Add(new RefundItem
                    {
                        ProductID = productID,
                        ProductName = productName,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        RefundAmount = itemRefundAmount
                    });
                }
            }

            lblRefundAmount.Text = refundAmount.ToString("C2");

            // Update refund type
            if (refundAmount == 0)
            {
                lblRefundType.Text = "None";
            }
            else
            {
                decimal orderTotal = Convert.ToDecimal(_orderDetails.Rows[0]["TotalAmount"]);
                if (refundAmount >= orderTotal)
                {
                    lblRefundType.Text = "Full Refund";
                }
                else
                {
                    lblRefundType.Text = "Partial Refund";
                }
            }
        }

        private void gridOrderItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == gridOrderItems.Columns["Select"].Index || 
                                     e.ColumnIndex == gridOrderItems.Columns["RefundQty"].Index))
            {
                CalculateRefundAmount();
            }
        }

        private void gridOrderItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridOrderItems.IsCurrentCellDirty)
            {
                gridOrderItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnProcessRefund_Click(object sender, EventArgs e)
        {
            if (_selectedRefundItems.Count == 0)
            {
                clsFormTheme.ShowWarning(this, "Please select items to refund.", "Refund");
                return;
            }

            string refundReason = txtRefundReason.Text.Trim();
            if (string.IsNullOrWhiteSpace(refundReason))
            {
                clsFormTheme.ShowWarning(this, "Please enter a refund reason.", "Refund");
                txtRefundReason.Focus();
                return;
            }

            string refundMethod = cboRefundMethod.SelectedItem?.ToString();
            int processedBy = clsUserManagement.CurrentUser?.UserID ?? 0;

            decimal refundAmount = _selectedRefundItems.Sum(item => item.RefundAmount);
            decimal orderTotal = Convert.ToDecimal(_orderDetails.Rows[0]["TotalAmount"]);

            int refundID;
            string errorMessage;

            bool success;
            if (refundAmount >= orderTotal)
            {
                // Full refund
                success = clsRefund.ProcessFullRefund(_orderID, refundReason, refundMethod, processedBy, out refundID, out errorMessage);
            }
            else
            {
                // Partial refund
                var refundItems = _selectedRefundItems.Select(item => new clsRefundData.RefundItemInfo
                {
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    RefundAmount = item.RefundAmount
                }).ToList();

                success = clsRefund.ProcessPartialRefund(_orderID, refundAmount, refundItems, refundReason, refundMethod, processedBy, out refundID, out errorMessage);
            }

            if (success)
            {
                clsFormTheme.ShowSuccess(this, $"Refund processed successfully. Refund ID: {refundID}", "Refund");
                clsAuditLog.LogAction("Refund Processed", $"OrderID: {_orderID}, RefundID: {refundID}, Amount: {refundAmount:C2}", "Sales");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                clsFormTheme.ShowError(this, $"Failed to process refund: {errorMessage}", "Refund");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class RefundItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal RefundAmount { get; set; }
        }
    }
}
