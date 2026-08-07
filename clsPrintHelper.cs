using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized print helper for improved printing functionality.
    /// Provides professional receipt/invoice templates with company branding.
    /// </summary>
    public static class clsPrintHelper
    {
        // ─── Company Information ────────────────────────────────────────────────

        public class CompanyInfo
        {
            public string Name { get; set; } = "Inventory Management System";
            public string Address { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Email { get; set; } = "";
            public string Website { get; set; } = "";
            public string LogoPath { get; set; } = "";
        }

        private static CompanyInfo _companyInfo = new CompanyInfo();

        public static CompanyInfo Company
        {
            get => _companyInfo;
            set => _companyInfo = value ?? new CompanyInfo();
        }

        // ─── Print Document Helpers ─────────────────────────────────────────────

        /// <summary>
        /// Prints a receipt with professional formatting.
        /// </summary>
        public static void PrintReceipt(DataTable orderDetails, DataTable orderItems, string customerName = "")
        {
            var printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) =>
            {
                DrawReceipt(e.Graphics, e.PageBounds, orderDetails, orderItems, customerName);
            };

            var printDialog = new PrintDialog
            {
                Document = printDoc,
                AllowSelection = false,
                AllowSomePages = false
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        /// <summary>
        /// Draws a receipt on the graphics context.
        /// </summary>
        private static void DrawReceipt(Graphics g, Rectangle bounds, DataTable orderDetails, DataTable orderItems, string customerName)
        {
            using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            using (var headerFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 9))
            using (var smallFont = new Font("Segoe UI", 8))
            using (var boldBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (var normalBrush = new SolidBrush(Color.FromArgb(71, 85, 105)))
            using (var linePen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                float y = 20;
                float x = 20;
                float width = bounds.Width - 40;

                // ─── Company Header ─────────────────────────────────────────────
                g.DrawString(_companyInfo.Name, titleFont, boldBrush, x, y);
                y += titleFont.GetHeight() + 5;

                if (!string.IsNullOrWhiteSpace(_companyInfo.Address))
                {
                    g.DrawString(_companyInfo.Address, bodyFont, normalBrush, x, y);
                    y += bodyFont.GetHeight() + 2;
                }

                if (!string.IsNullOrWhiteSpace(_companyInfo.Phone))
                {
                    g.DrawString($"Tel: {_companyInfo.Phone}", bodyFont, normalBrush, x, y);
                    y += bodyFont.GetHeight() + 2;
                }

                if (!string.IsNullOrWhiteSpace(_companyInfo.Email))
                {
                    g.DrawString($"Email: {_companyInfo.Email}", smallFont, normalBrush, x, y);
                    y += smallFont.GetHeight() + 10;
                }

                // ─── Divider ───────────────────────────────────────────────────
                g.DrawLine(linePen, x, y, x + width, y);
                y += 15;

                // ─── Order Details ───────────────────────────────────────────────
                if (orderDetails != null && orderDetails.Rows.Count > 0)
                {
                    var order = orderDetails.Rows[0];
                    g.DrawString($"Order #{order["OrderID"]}", headerFont, boldBrush, x, y);
                    y += headerFont.GetHeight() + 5;

                    g.DrawString($"Date: {Convert.ToDateTime(order["OrderDate"]):dd/MM/yyyy HH:mm}", bodyFont, normalBrush, x, y);
                    y += bodyFont.GetHeight() + 5;

                    if (!string.IsNullOrWhiteSpace(customerName))
                    {
                        g.DrawString($"Customer: {customerName}", bodyFont, normalBrush, x, y);
                        y += bodyFont.GetHeight() + 10;
                    }
                }

                // ─── Divider ───────────────────────────────────────────────────
                g.DrawLine(linePen, x, y, x + width, y);
                y += 15;

                // ─── Items Header ───────────────────────────────────────────────
                g.DrawString("Item", headerFont, boldBrush, x, y);
                g.DrawString("Qty", headerFont, boldBrush, x + width * 0.5f, y);
                g.DrawString("Price", headerFont, boldBrush, x + width * 0.7f, y);
                g.DrawString("Total", headerFont, boldBrush, x + width * 0.85f, y);
                y += headerFont.GetHeight() + 10;

                // ─── Items ───────────────────────────────────────────────────────
                if (orderItems != null)
                {
                    foreach (DataRow item in orderItems.Rows)
                    {
                        string name = item["ProductName"].ToString();
                        int qty = Convert.ToInt32(item["Quantity"]);
                        decimal price = Convert.ToDecimal(item["UnitPrice"]);
                        decimal total = Convert.ToDecimal(item["Subtotal"]);

                        // Truncate long names
                        if (name.Length > 25) name = name.Substring(0, 22) + "...";

                        g.DrawString(name, bodyFont, normalBrush, x, y);
                        g.DrawString(qty.ToString(), bodyFont, normalBrush, x + width * 0.5f, y);
                        g.DrawString(price.ToString("C"), bodyFont, normalBrush, x + width * 0.7f, y);
                        g.DrawString(total.ToString("C"), bodyFont, normalBrush, x + width * 0.85f, y);
                        y += bodyFont.GetHeight() + 5;
                    }
                }

                // ─── Divider ───────────────────────────────────────────────────
                y += 5;
                g.DrawLine(linePen, x, y, x + width, y);
                y += 15;

                // ─── Totals ────────────────────────────────────────────────────
                if (orderDetails != null && orderDetails.Rows.Count > 0)
                {
                    var order = orderDetails.Rows[0];
                    decimal subtotal = Convert.ToDecimal(order["Subtotal"]);
                    decimal tax = Convert.ToDecimal(order["Tax"]);
                    decimal total = Convert.ToDecimal(order["TotalAmount"]);

                    g.DrawString($"Subtotal:", bodyFont, normalBrush, x + width * 0.6f, y);
                    g.DrawString(subtotal.ToString("C"), headerFont, boldBrush, x + width * 0.85f, y);
                    y += bodyFont.GetHeight() + 5;

                    g.DrawString($"Tax ({(tax / subtotal * 100):F1}%):", bodyFont, normalBrush, x + width * 0.6f, y);
                    g.DrawString(tax.ToString("C"), bodyFont, normalBrush, x + width * 0.85f, y);
                    y += bodyFont.GetHeight() + 5;

                    y += 5;
                    g.DrawString($"TOTAL:", headerFont, boldBrush, x + width * 0.6f, y);
                    g.DrawString(total.ToString("C"), titleFont, boldBrush, x + width * 0.85f, y);
                    y += titleFont.GetHeight() + 15;
                }

                // ─── Footer ─────────────────────────────────────────────────────
                g.DrawLine(linePen, x, y, x + width, y);
                y += 15;

                g.DrawString("Thank you for your business!", bodyFont, normalBrush, x + width / 2 - g.MeasureString("Thank you for your business!", bodyFont).Width / 2, y);
                y += bodyFont.GetHeight() + 5;

                if (!string.IsNullOrWhiteSpace(_companyInfo.Website))
                {
                    g.DrawString(_companyInfo.Website, smallFont, normalBrush, x + width / 2 - g.MeasureString(_companyInfo.Website, smallFont).Width / 2, y);
                }
            }
        }

        /// <summary>
        /// Prints a report with professional formatting.
        /// </summary>
        public static void PrintReport(DataTable data, string title, string[] columnsToPrint = null)
        {
            var printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) =>
            {
                DrawReport(e.Graphics, e.PageBounds, data, title, columnsToPrint);
            };

            var printDialog = new PrintDialog
            {
                Document = printDoc,
                AllowSelection = false,
                AllowSomePages = false
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        /// <summary>
        /// Draws a report on the graphics context.
        /// </summary>
        private static void DrawReport(Graphics g, Rectangle bounds, DataTable data, string title, string[] columnsToPrint)
        {
            using (var titleFont = new Font("Segoe UI", 16, FontStyle.Bold))
            using (var headerFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 9))
            using (var boldBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (var normalBrush = new SolidBrush(Color.FromArgb(71, 85, 105)))
            using (var headerBrush = new SolidBrush(Color.White))
            using (var headerBackBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (var linePen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                float y = 20;
                float x = 20;
                float width = bounds.Width - 40;

                // ─── Company Header ─────────────────────────────────────────────
                g.DrawString(_companyInfo.Name, bodyFont, normalBrush, x, y);
                y += bodyFont.GetHeight() + 5;

                g.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), bodyFont, normalBrush, x, y);
                y += bodyFont.GetHeight() + 20;

                // ─── Title ───────────────────────────────────────────────────────
                g.DrawString(title, titleFont, boldBrush, x, y);
                y += titleFont.GetHeight() + 20;

                if (data == null || data.Rows.Count == 0)
                {
                    g.DrawString("No data to display.", bodyFont, normalBrush, x, y);
                    return;
                }

                // ─── Determine columns to print ───────────────────────────────────
                var columns = columnsToPrint;
                if (columns == null)
                {
                    columns = new string[data.Columns.Count];
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        columns[i] = data.Columns[i].ColumnName;
                    }
                }

                float columnWidth = width / columns.Length;

                // ─── Header Row ─────────────────────────────────────────────────
                float headerHeight = 30;
                var headerRect = new RectangleF(x, y, width, headerHeight);
                g.FillRectangle(headerBackBrush, headerRect);

                for (int i = 0; i < columns.Length; i++)
                {
                    string colName = columns[i];
                    g.DrawString(colName, headerFont, headerBrush, x + i * columnWidth + 5, y + 8);
                }
                y += headerHeight;

                // ─── Data Rows ───────────────────────────────────────────────────
                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        string colName = columns[i];
                        string value = row[colName]?.ToString() ?? "";
                        
                        // Truncate long values
                        if (value.Length > 15) value = value.Substring(0, 12) + "...";

                        g.DrawString(value, bodyFont, normalBrush, x + i * columnWidth + 5, y + 5);
                    }
                    y += 20;

                    // Row separator
                    g.DrawLine(linePen, x, y, x + width, y);
                    y += 2;
                }

                // ─── Footer ─────────────────────────────────────────────────────
                y += 10;
                g.DrawLine(linePen, x, y, x + width, y);
                y += 15;

                g.DrawString($"Total Records: {data.Rows.Count}", bodyFont, normalBrush, x, y);
            }
        }

        /// <summary>
        /// Exports data to CSV format.
        /// </summary>
        public static string ExportToCsv(DataTable data)
        {
            if (data == null) return string.Empty;

            var csv = new StringBuilder();

            // Header row
            for (int i = 0; i < data.Columns.Count; i++)
            {
                csv.Append($"\"{data.Columns[i].ColumnName}\"");
                if (i < data.Columns.Count - 1) csv.Append(",");
            }
            csv.AppendLine();

            // Data rows
            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    string value = row[i]?.ToString() ?? "";
                    // Escape quotes and commas
                    value = value.Replace("\"", "\"\"");
                    csv.Append($"\"{value}\"");
                    if (i < data.Columns.Count - 1) csv.Append(",");
                }
                csv.AppendLine();
            }

            return csv.ToString();
        }

        /// <summary>
        /// Exports data to CSV file.
        /// </summary>
        public static bool ExportToCsvFile(DataTable data, string filePath)
        {
            try
            {
                string csv = ExportToCsv(data);
                System.IO.File.WriteAllText(filePath, csv);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
