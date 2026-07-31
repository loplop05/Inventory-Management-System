using System;
using System.Diagnostics;
using System.Text;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Receipt sharing functionality for WhatsApp and Email.
    /// Generates shareable receipt text and opens sharing platforms.
    /// </summary>
    public static class clsReceiptSharing
    {
        public class ReceiptData
        {
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Discount { get; set; }
            public decimal Tax { get; set; }
            public decimal Total { get; set; }
            public string PaymentMethod { get; set; }
            public System.Collections.Generic.List<ReceiptItem> Items { get; set; }
        }

        public class ReceiptItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Subtotal { get; set; }
        }

        /// <summary>
        /// Generates a formatted receipt text for sharing.
        /// </summary>
        public static string GenerateReceiptText(ReceiptData receipt)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("══════════════════════════════════════");
            sb.AppendLine("       INVENTORY MANAGEMENT SYSTEM");
            sb.AppendLine("              RECEIPT");
            sb.AppendLine("══════════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine($"Order #: {receipt.OrderID}");
            sb.AppendLine($"Date: {receipt.OrderDate:yyyy-MM-dd HH:mm}");
            
            if (!string.IsNullOrEmpty(receipt.CustomerName))
            {
                sb.AppendLine($"Customer: {receipt.CustomerName}");
                if (!string.IsNullOrEmpty(receipt.CustomerPhone))
                    sb.AppendLine($"Phone: {receipt.CustomerPhone}");
            }
            
            sb.AppendLine($"Payment: {receipt.PaymentMethod}");
            sb.AppendLine();
            sb.AppendLine("─────────────────────────────────────");
            sb.AppendLine("ITEMS:");
            sb.AppendLine("─────────────────────────────────────");
            
            foreach (var item in receipt.Items)
            {
                sb.AppendLine($"{item.ProductName}");
                sb.AppendLine($"  {item.Quantity} x {item.UnitPrice:C2} = {item.Subtotal:C2}");
            }
            
            sb.AppendLine("─────────────────────────────────────");
            sb.AppendLine($"Subtotal: {receipt.Subtotal:C2}");
            
            if (receipt.Discount > 0)
                sb.AppendLine($"Discount: -{receipt.Discount:C2}");
            
            sb.AppendLine($"Tax (7%): {receipt.Tax:C2}");
            sb.AppendLine($"─────────────────────────────────────");
            sb.AppendLine($"TOTAL: {receipt.Total:C2}");
            sb.AppendLine("══════════════════════════════════════");
            sb.AppendLine("Thank you for your purchase!");
            sb.AppendLine("══════════════════════════════════════");
            
            return sb.ToString();
        }

        /// <summary>
        /// Shares receipt via WhatsApp (opens WhatsApp web with pre-filled message).
        /// </summary>
        public static bool ShareViaWhatsApp(ReceiptData receipt, string phoneNumber = null)
        {
            try
            {
                string receiptText = GenerateReceiptText(receipt);
                string encodedText = Uri.EscapeDataString(receiptText);
                
                // Use customer phone if provided, otherwise open WhatsApp web
                string targetPhone = string.IsNullOrEmpty(phoneNumber) ? "" : phoneNumber.Replace("+", "").Replace(" ", "");
                
                string url;
                if (!string.IsNullOrEmpty(targetPhone))
                {
                    // Direct to specific number
                    url = $"https://wa.me/{targetPhone}?text={encodedText}";
                }
                else
                {
                    // WhatsApp web with message
                    url = $"https://web.whatsapp.com/send?text={encodedText}";
                }
                
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Failed to open WhatsApp: " + ex.Message, "Error", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Shares receipt via Email (opens default email client).
        /// </summary>
        public static bool ShareViaEmail(ReceiptData receipt, string emailAddress = null)
        {
            try
            {
                string receiptText = GenerateReceiptText(receipt);
                string subject = Uri.EscapeDataString($"Receipt #{receipt.OrderID} - Inventory Management System");
                string body = Uri.EscapeDataString(receiptText);
                
                string mailto = $"mailto:{emailAddress}?subject={subject}&body={body}";
                
                Process.Start(new ProcessStartInfo
                {
                    FileName = mailto,
                    UseShellExecute = true
                });
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Failed to open email client: " + ex.Message, "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Copies receipt text to clipboard.
        /// </summary>
        public static bool CopyToClipboard(ReceiptData receipt)
        {
            try
            {
                string receiptText = GenerateReceiptText(receipt);
                System.Windows.Forms.Clipboard.SetText(receiptText);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
