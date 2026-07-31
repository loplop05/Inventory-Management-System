using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmPrintReceipt : Form
    {
        private int _currentOrderID = -1;
        private DataTable _currentOrderDetails = null;
        private DataTable _currentOrderItems = null;

        public frmPrintReceipt()
        {
            InitializeComponent();
        }

        private void frmPrintReceipt_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ClearDisplay();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Print Receipt", clsFormTheme.Icons.Print);
            clsFormTheme.ApplyTextBoxStyle(_txtOrderID);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySuccessButtonStyle(_btnPrint, clsFormTheme.Icons.Print);

            // Share buttons - uncomment after adding controls to Designer
            // clsFormTheme.ApplySecondaryButtonStyle(_btnShareWhatsApp, clsFormTheme.Icons.Share);
            // clsFormTheme.ApplySecondaryButtonStyle(_btnShareEmail, clsFormTheme.Icons.Email);
            // clsFormTheme.ApplySecondaryButtonStyle(_btnCopy, clsFormTheme.Icons.Copy);

            KeyDown += frmPrintReceipt_KeyDown;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Print Receipt");
        }

        private void ClearDisplay()
        {
            _currentOrderID = -1;
            _currentOrderDetails = null;
            _currentOrderItems = null;
            _txtOrderID.Text = "";
            _lblReceiptPreview.Text = "Enter an Order ID to view and print the receipt.";
            _btnPrint.Enabled = false;
        }

        public TextBox OrderIDTextBox
        {
            get { return _txtOrderID; }
        }

        public void SearchOrder()
        {
            btnSearch_Click(null, null);
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
            DisplayReceipt();
        }

        private void DisplayReceipt()
        {
            if (_currentOrderDetails == null || _currentOrderDetails.Rows.Count == 0)
                return;

            DataRow order = _currentOrderDetails.Rows[0];

            // Build receipt text
            System.Text.StringBuilder receipt = new System.Text.StringBuilder();

            receipt.AppendLine("========================================");
            receipt.AppendLine("        INVENTORY MANAGEMENT SYSTEM");
            receipt.AppendLine("========================================");
            receipt.AppendLine();

            DateTime orderDate = Convert.ToDateTime(order["OrderDate"]);
            receipt.AppendLine($"Order ID: {_currentOrderID}");
            receipt.AppendLine($"Date: {orderDate:yyyy-MM-dd HH:mm}");
            receipt.AppendLine();

            // Customer info
            if (order["CustomerID"] != DBNull.Value && order["CustomerID"] != null)
            {
                string customerName = order["CustomerName"] != DBNull.Value ? order["CustomerName"].ToString() : "Unknown";
                string phoneNumber = order["PhoneNumber"] != DBNull.Value ? order["PhoneNumber"].ToString() : "N/A";
                receipt.AppendLine($"Customer: {customerName}");
                receipt.AppendLine($"Phone: {phoneNumber}");
            }
            else
            {
                receipt.AppendLine("Customer: Walk-in");
            }
            receipt.AppendLine();

            // Payment info
            string paymentMethod = order["PaymentMethod"] != DBNull.Value ? order["PaymentMethod"].ToString() : "Cash";
            string paymentDetails = order["PaymentDetails"] != DBNull.Value ? order["PaymentDetails"].ToString() : "";
            receipt.AppendLine($"Payment: {paymentMethod}");
            if (!string.IsNullOrEmpty(paymentDetails))
                receipt.AppendLine($"Card: {paymentDetails}");
            receipt.AppendLine();
            receipt.AppendLine("----------------------------------------");

            // Order items
            if (_currentOrderItems != null && _currentOrderItems.Rows.Count > 0)
            {
                receipt.AppendLine("ITEMS:");
                receipt.AppendLine();

                foreach (DataRow item in _currentOrderItems.Rows)
                {
                    string productName = item["ProductName"].ToString();
                    int quantity = Convert.ToInt32(item["Quantity"]);
                    decimal unitPrice = Convert.ToDecimal(item["UnitPrice"]);
                    decimal itemSubtotal = Convert.ToDecimal(item["Subtotal"]);

                    receipt.AppendLine($"{productName}");
                    receipt.AppendLine($"  Qty: {quantity} x {unitPrice:C2} = {itemSubtotal:C2}");
                }
            }
            receipt.AppendLine();
            receipt.AppendLine("----------------------------------------");

            // Totals
            decimal subtotal = Convert.ToDecimal(order["Subtotal"]);
            decimal taxAmount = Convert.ToDecimal(order["TaxAmount"]);
            decimal totalAmount = Convert.ToDecimal(order["TotalAmount"]);

            receipt.AppendLine($"Subtotal: {subtotal:C2}");
            receipt.AppendLine($"Tax (7%): {taxAmount:C2}");
            receipt.AppendLine($"TOTAL: {totalAmount:C2}");
            receipt.AppendLine();
            receipt.AppendLine("========================================");
            receipt.AppendLine("          Thank you for shopping!");
            receipt.AppendLine("========================================");

            _lblReceiptPreview.Text = receipt.ToString();
            _btnPrint.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
                return;

            DialogResult result = MessageBox.Show(
                "Do you want to print this receipt?",
                "Print Receipt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                PrintReceipt();
            }
        }

        private void PrintReceipt()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) =>
            {
                Font font = new Font("Consolas", 10);
                Font boldFont = new Font("Consolas", 10, FontStyle.Bold);
                float lineHeight = font.GetHeight(e.Graphics);
                float yPos = 20;
                float leftMargin = 20;
                float pageWidth = e.PageBounds.Width - 2 * leftMargin;

                string receiptText = _lblReceiptPreview.Text;
                string[] lines = receiptText.Split('\n');

                foreach (string line in lines)
                {
                    if (yPos > e.PageBounds.Height - 50)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    // Check if line contains header/footer (use bold font)
                    if (line.Contains("=") || line.Contains("-") || 
                        line.Contains("INVENTORY MANAGEMENT SYSTEM") ||
                        line.Contains("Thank you") ||
                        line.Contains("TOTAL"))
                    {
                        e.Graphics.DrawString(line, boldFont, Brushes.Black, leftMargin, yPos);
                    }
                    else
                    {
                        e.Graphics.DrawString(line, font, Brushes.Black, leftMargin, yPos);
                    }
                    yPos += lineHeight;
                }
                e.HasMorePages = false;
            };

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDoc
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                MessageBox.Show("Receipt printed successfully.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmPrintReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        // Share button handlers - uncomment after adding controls to Designer
        /*
        private void btnShareWhatsApp_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1 || _currentOrderDetails == null || _currentOrderItems == null)
            {
                MessageBox.Show("Please load a receipt first.", "Share", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var receiptData = BuildReceiptData();
            string customerPhone = _currentOrderDetails.Rows[0]["PhoneNumber"] != DBNull.Value 
                ? _currentOrderDetails.Rows[0]["PhoneNumber"].ToString() 
                : null;

            clsReceiptSharing.ShareViaWhatsApp(receiptData, customerPhone);
        }

        private void btnShareEmail_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1 || _currentOrderDetails == null || _currentOrderItems == null)
            {
                MessageBox.Show("Please load a receipt first.", "Share", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var receiptData = BuildReceiptData();
            clsReceiptSharing.ShareViaEmail(receiptData);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1 || _currentOrderDetails == null || _currentOrderItems == null)
            {
                MessageBox.Show("Please load a receipt first.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var receiptData = BuildReceiptData();
            if (clsReceiptSharing.CopyToClipboard(receiptData))
            {
                MessageBox.Show("Receipt copied to clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to copy receipt to clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsReceiptSharing.ReceiptData BuildReceiptData()
        {
            var receipt = new clsReceiptSharing.ReceiptData
            {
                OrderID = _currentOrderID,
                OrderDate = Convert.ToDateTime(_currentOrderDetails.Rows[0]["OrderDate"]),
                CustomerName = _currentOrderDetails.Rows[0]["CustomerName"] != DBNull.Value 
                    ? _currentOrderDetails.Rows[0]["CustomerName"].ToString() 
                    : "",
                CustomerPhone = _currentOrderDetails.Rows[0]["PhoneNumber"] != DBNull.Value 
                    ? _currentOrderDetails.Rows[0]["PhoneNumber"].ToString() 
                    : "",
                Subtotal = Convert.ToDecimal(_currentOrderDetails.Rows[0]["Subtotal"]),
                Discount = 0, // Would need to calculate from coupon if applicable
                Tax = Convert.ToDecimal(_currentOrderDetails.Rows[0]["TaxAmount"]),
                Total = Convert.ToDecimal(_currentOrderDetails.Rows[0]["TotalAmount"]),
                PaymentMethod = _currentOrderDetails.Rows[0]["PaymentMethod"].ToString(),
                Items = new System.Collections.Generic.List<clsReceiptSharing.ReceiptItem>()
            };

            foreach (DataRow row in _currentOrderItems.Rows)
            {
                receipt.Items.Add(new clsReceiptSharing.ReceiptItem
                {
                    ProductName = row["ProductName"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Subtotal = Convert.ToDecimal(row["Subtotal"])
                });
            }

            return receipt;
        }
        */
    }
}
