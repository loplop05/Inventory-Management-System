using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public static class clsReportExporter
    {
        public static bool ExportToHtml(DataTable table, string title, string filePath, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                StringBuilder html = new StringBuilder();
                
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html>");
                html.AppendLine("<head>");
                html.AppendLine("<meta charset='utf-8'>");
                html.AppendLine("<title>" + title + "</title>");
                html.AppendLine("<style>");
                html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
                html.AppendLine("h1 { color: #2c3e50; text-align: center; }");
                html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; }");
                html.AppendLine("th { background-color: #2c3e50; color: white; padding: 10px; text-align: left; }");
                html.AppendLine("td { border: 1px solid #ddd; padding: 8px; }");
                html.AppendLine("tr:nth-child(even) { background-color: #f8f9fa; }");
                html.AppendLine("</style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("<h1>" + title + "</h1>");
                html.AppendLine("<p>Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                
                html.AppendLine("<table>");
                
                // Headers
                html.AppendLine("<tr>");
                foreach (DataColumn column in table.Columns)
                {
                    html.AppendLine("<th>" + column.ColumnName + "</th>");
                }
                html.AppendLine("</tr>");
                
                // Data rows
                foreach (DataRow row in table.Rows)
                {
                    html.AppendLine("<tr>");
                    foreach (DataColumn column in table.Columns)
                    {
                        html.AppendLine("<td>" + row[column] + "</td>");
                    }
                    html.AppendLine("</tr>");
                }
                
                html.AppendLine("</table>");
                html.AppendLine("</body>");
                html.AppendLine("</html>");
                
                File.WriteAllText(filePath, html.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "HTML export failed: " + ex.Message;
                return false;
            }
        }

        public static bool ExportMultipleTablesToHtml(DataTable[] tables, string[] titles, string documentTitle, string filePath, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                StringBuilder html = new StringBuilder();
                
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html>");
                html.AppendLine("<head>");
                html.AppendLine("<meta charset='utf-8'>");
                html.AppendLine("<title>" + documentTitle + "</title>");
                html.AppendLine("<style>");
                html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
                html.AppendLine("h1 { color: #2c3e50; text-align: center; }");
                html.AppendLine("h2 { color: #34495e; margin-top: 30px; }");
                html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 10px; }");
                html.AppendLine("th { background-color: #2c3e50; color: white; padding: 10px; text-align: left; }");
                html.AppendLine("td { border: 1px solid #ddd; padding: 8px; }");
                html.AppendLine("tr:nth-child(even) { background-color: #f8f9fa; }");
                html.AppendLine("</style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("<h1>" + documentTitle + "</h1>");
                html.AppendLine("<p>Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                
                for (int t = 0; t < tables.Length; t++)
                {
                    html.AppendLine("<h2>" + titles[t] + "</h2>");
                    DataTable table = tables[t];
                    
                    html.AppendLine("<table>");
                    
                    // Headers
                    html.AppendLine("<tr>");
                    foreach (DataColumn column in table.Columns)
                    {
                        html.AppendLine("<th>" + column.ColumnName + "</th>");
                    }
                    html.AppendLine("</tr>");
                    
                    // Data rows
                    foreach (DataRow row in table.Rows)
                    {
                        html.AppendLine("<tr>");
                        foreach (DataColumn column in table.Columns)
                        {
                            html.AppendLine("<td>" + row[column] + "</td>");
                        }
                        html.AppendLine("</tr>");
                    }
                    
                    html.AppendLine("</table>");
                }
                
                html.AppendLine("</body>");
                html.AppendLine("</html>");
                
                File.WriteAllText(filePath, html.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "HTML export failed: " + ex.Message;
                return false;
            }
        }

        public static bool PrintToPdf(DataTable table, string title, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = title;
                
                int currentPage = 0;
                int rowsPerPage = 40;
                int currentRow = 0;
                
                printDoc.PrintPage += (sender, e) =>
                {
                    currentPage++;
                    
                    float yPos = e.MarginBounds.Top;
                    float leftMargin = e.MarginBounds.Left;
                    float pageWidth = e.MarginBounds.Width;
                    
                    using (Font titleFont = new Font("Arial", 16, FontStyle.Bold))
                    using (Font headerFont = new Font("Arial", 10, FontStyle.Bold))
                    using (Font normalFont = new Font("Arial", 9))
                    {
                        // Title
                        e.Graphics.DrawString(title, titleFont, Brushes.Black, 
                            leftMargin + (pageWidth - e.Graphics.MeasureString(title, titleFont).Width) / 2, yPos);
                        yPos += titleFont.GetHeight(e.Graphics) + 10;
                        
                        // Date
                        e.Graphics.DrawString("Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                            normalFont, Brushes.Gray, leftMargin, yPos);
                        yPos += normalFont.GetHeight(e.Graphics) + 20;
                        
                        // Headers
                        float columnWidth = pageWidth / table.Columns.Count;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            e.Graphics.DrawString(table.Columns[i].ColumnName, headerFont, Brushes.White, 
                                leftMargin + (i * columnWidth), yPos);
                        }
                        yPos += headerFont.GetHeight(e.Graphics) + 5;
                        
                        // Draw header background
                        e.Graphics.FillRectangle(Brushes.DarkBlue, leftMargin, yPos - headerFont.GetHeight(e.Graphics) - 5, 
                            pageWidth, headerFont.GetHeight(e.Graphics) + 5);
                        
                        // Data rows
                        int rowsOnPage = 0;
                        while (currentRow < table.Rows.Count && rowsOnPage < rowsPerPage)
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                e.Graphics.DrawString(table.Rows[currentRow][i].ToString(), normalFont, Brushes.Black, 
                                    leftMargin + (i * columnWidth), yPos);
                            }
                            yPos += normalFont.GetHeight(e.Graphics);
                            currentRow++;
                            rowsOnPage++;
                        }
                        
                        e.HasMorePages = currentRow < table.Rows.Count;
                    }
                };
                
                // Show print dialog
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;
                
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Print failed: " + ex.Message;
                return false;
            }
        }
    }
}
