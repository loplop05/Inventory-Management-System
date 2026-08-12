using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmPrintReceipt : Form
    {
        private int _currentOrderID = -1;
        private DataTable _currentOrderDetails = null;
        private DataTable _currentOrderItems = null;
        private int _currentPrintLine = 0;
        private string[] _receiptLines = null;
        private int _currentPageNumber = 0;

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
            clsFormTheme.ApplyTextBoxStyle(_txtOrderID);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSearch, clsFormTheme.Icons.Search);
            clsFormTheme.ApplySuccessButtonStyle(_btnPrint, clsFormTheme.Icons.Print);
            clsFormTheme.ApplyDangerButtonStyle(_btnVoid, clsFormTheme.Icons.Delete);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: null,
                onSearch: () => _txtOrderID.Focus(),
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
            Text = clsLanguageManager.GetString("Print Receipt");
            _btnClose.Text = clsLanguageManager.GetString("Close");
        }

        private void ClearDisplay()
        {
            _currentOrderID = -1;
            _currentOrderDetails = null;
            _currentOrderItems = null;
            _txtOrderID.Text = "";
            _lblReceiptPreview.Text = "Enter an Order ID to view and print the receipt.";
            _btnPrint.Enabled = false;
            _btnVoid.Enabled = false;
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
                clsFormTheme.ShowWarning(this, "Please enter an Order ID.", "Search");
                _txtOrderID.Focus();
                return;
            }

            int orderID;
            if (!int.TryParse(_txtOrderID.Text.Trim(), out orderID))
            {
                clsFormTheme.ShowWarning(this, "Invalid Order ID format.", "Search");
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
                clsFormTheme.ShowInfo(this, "Order not found.", "Search");
                ClearDisplay();
                return;
            }

            _currentOrderID = orderID;
            DisplayReceipt();

            // Enable Void button if order is not already voided
            bool isVoided = false;
            if (_currentOrderDetails.Columns.Contains("IsVoided"))
            {
                isVoided = _currentOrderDetails.Rows[0]["IsVoided"] != DBNull.Value && Convert.ToBoolean(_currentOrderDetails.Rows[0]["IsVoided"]);
            }
            _btnVoid.Enabled = !isVoided;
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
            receipt.AppendLine($"Tax ({(clsDataAccessSettings.TaxRate * 100):F0}%): {taxAmount:C2}");
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

            DialogResult result = clsFormTheme.ShowYesNo(this,
                "Do you want to print this receipt?",
                "Print Receipt");

            if (result == DialogResult.Yes)
            {
                PrintReceipt();
            }
        }

        private void PrintReceipt()
        {
            // Initialize printing state
            _currentPrintLine = 0;
            _receiptLines = _lblReceiptPreview.Text.Split('\n');
            _currentPageNumber = 0;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDoc
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                clsFormTheme.ShowSuccess(this, "Receipt printed successfully.", "Print");
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 10);
            Font boldFont = new Font("Consolas", 10, FontStyle.Bold);
            float lineHeight = font.GetHeight(e.Graphics);
            float yPos = e.MarginBounds.Top;
            float leftMargin = e.MarginBounds.Left;
            float pageWidth = e.MarginBounds.Width;

            // Print page header on each page (except first page which has the main header)
            if (_currentPageNumber > 0)
            {
                e.Graphics.DrawString($"--- Receipt Continued (Page {_currentPageNumber + 1}) ---", boldFont, Brushes.Black, leftMargin, yPos);
                yPos += lineHeight * 2;
            }

            // Print lines until page is full or all lines are printed
            while (_currentPrintLine < _receiptLines.Length)
            {
                string line = _receiptLines[_currentPrintLine];

                // Check if we need a new page
                if (yPos > e.MarginBounds.Bottom - lineHeight * 3)
                {
                    _currentPageNumber++;
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
                _currentPrintLine++;
            }

            // All lines printed
            e.HasMorePages = false;
            _currentPrintLine = 0;
            _receiptLines = null;
            _currentPageNumber = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
            {
                clsFormTheme.ShowWarning(this, "Please load a receipt first.", "Void");
                return;
            }

            DialogResult result = clsFormTheme.ShowYesNo(this,
                $"Are you sure you want to void Order {_currentOrderID}?\n\nThis will:\n" +
                "- Reverse all stock changes\n" +
                "- Deduct any loyalty points awarded\n" +
                "- Mark the order as voided\n\n" +
                "This action cannot be undone.",
                "Confirm Void");

            if (result != DialogResult.Yes)
                return;

            // Simple reason input using a form
            using (Form reasonForm = new Form
            {
                Text = "Void Reason",
                Size = new Size(400, 200),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            })
            {
                clsFormTheme.ApplyFormStyle(reasonForm);
                
                Label lbl = new Label
                {
                    Text = "Please enter a reason for voiding this order:",
                    Location = new Point(20, 20),
                    Width = 340
                };
                clsFormTheme.ApplyLabelStyle(lbl);
                
                TextBox txtReason = new TextBox
                {
                    Location = new Point(20, 50),
                    Width = 340,
                    Multiline = true,
                    Height = 60
                };
                clsFormTheme.ApplyTextBoxStyle(txtReason);
                
                Button btnOK = new Button
                {
                    Text = "Void",
                    DialogResult = DialogResult.OK,
                    Location = new Point(200, 120),
                    Width = 80
                };
                clsFormTheme.ApplyDangerButtonStyle(btnOK, clsFormTheme.Icons.Delete);
                
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(290, 120),
                    Width = 80
                };
                clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);
                
                reasonForm.Controls.Add(lbl);
                reasonForm.Controls.Add(txtReason);
                reasonForm.Controls.Add(btnOK);
                reasonForm.Controls.Add(btnCancel);
                reasonForm.AcceptButton = btnOK;
                reasonForm.CancelButton = btnCancel;
                
                if (reasonForm.ShowDialog() == DialogResult.OK && string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    clsFormTheme.ShowWarning(this, "A reason is required to void an order.", "Void");
                    return;
                }
                
                string reason = txtReason.Text;
                if (string.IsNullOrWhiteSpace(reason))
                    return;

                string errorMessage;
                if (clsPOS.VoidOrder(_currentOrderID, reason, Environment.UserName, out errorMessage))
                {
                    // Log the void action
                    clsAuditLog.LogAction("Order Voided", $"Order {_currentOrderID} voided. Reason: {reason}", "POS");
                    
                    clsFormTheme.ShowSuccess(this, $"Order {_currentOrderID} has been voided successfully.", "Void");
                    ClearDisplay();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to void order: " + errorMessage, "Void");
                }
            }
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
                clsFormTheme.ShowWarning(this, "Please load a receipt first.", "Share");
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
                clsFormTheme.ShowWarning(this, "Please load a receipt first.", "Share");
                return;
            }

            var receiptData = BuildReceiptData();
            clsReceiptSharing.ShareViaEmail(receiptData);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1 || _currentOrderDetails == null || _currentOrderItems == null)
            {
                clsFormTheme.ShowWarning(this, "Please load a receipt first.", "Copy");
                return;
            }

            var receiptData = BuildReceiptData();
            if (clsReceiptSharing.CopyToClipboard(receiptData))
            {
                clsFormTheme.ShowSuccess(this, "Receipt copied to clipboard.", "Copy");
            }
            else
            {
                clsFormTheme.ShowError(this, "Failed to copy receipt to clipboard.", "Copy");
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
