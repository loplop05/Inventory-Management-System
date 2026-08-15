using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// Bulk product import from CSV files.
    /// Supports importing products with validation and error handling.
    /// </summary>
    public static class clsProductImport
    {
        /// <summary>
        /// Result of a product import operation.
        /// </summary>
        public class ImportResult
        {
            public int TotalRows { get; set; }
            public int SuccessfulImports { get; set; }
            public int FailedImports { get; set; }
            public List<string> Errors { get; set; } = new List<string>();
            public List<string> Warnings { get; set; } = new List<string>();
            public bool Success => FailedImports == 0;
        }

        /// <summary>
        /// Imports products from a CSV file.
        /// Expected CSV format: ProductName,CategoryID,SupplierID,Price,Quantity,Description,Barcode
        /// </summary>
        /// <param name="csvFilePath">Path to the CSV file.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <param name="skipFirstRow">Whether to skip the header row.</param>
        /// <returns>ImportResult containing details of the import operation.</returns>
        public static ImportResult ImportFromCSV(string csvFilePath, out string errorMessage, bool skipFirstRow = true)
        {
            errorMessage = string.Empty;
            var result = new ImportResult();

            if (!File.Exists(csvFilePath))
            {
                errorMessage = "CSV file not found: " + csvFilePath;
                result.Errors.Add(errorMessage);
                return result;
            }

            try
            {
                string[] lines = File.ReadAllLines(csvFilePath);
                result.TotalRows = lines.Length;

                int startIndex = skipFirstRow ? 1 : 0;
                
                if (startIndex >= lines.Length)
                {
                    errorMessage = "CSV file is empty or contains only headers.";
                    result.Errors.Add(errorMessage);
                    return result;
                }

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        for (int i = startIndex; i < lines.Length; i++)
                        {
                            string line = lines[i].Trim();
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            string[] fields = ParseCSVLine(line);

                            if (fields.Length < 5)
                            {
                                result.FailedImports++;
                                result.Errors.Add($"Row {i + 1}: Insufficient columns. Expected at least 5, got {fields.Length}");
                                continue;
                            }

                            // Parse fields
                            string productName = fields[0].Trim();
                            int categoryID;
                            int supplierID;
                            decimal price;
                            int quantity;
                            string description = fields.Length > 5 ? fields[5].Trim() : "";
                            string barcode = fields.Length > 6 ? fields[6].Trim() : "";

                            // Validate and parse
                            if (!int.TryParse(fields[1].Trim(), out categoryID))
                            {
                                result.FailedImports++;
                                result.Errors.Add($"Row {i + 1}: Invalid CategoryID '{fields[1]}'");
                                continue;
                            }

                            if (!int.TryParse(fields[2].Trim(), out supplierID))
                            {
                                result.FailedImports++;
                                result.Errors.Add($"Row {i + 1}: Invalid SupplierID '{fields[2]}'");
                                continue;
                            }

                            if (!decimal.TryParse(fields[3].Trim(), out price) || price < 0)
                            {
                                result.FailedImports++;
                                result.Errors.Add($"Row {i + 1}: Invalid Price '{fields[3]}'");
                                continue;
                            }

                            if (!int.TryParse(fields[4].Trim(), out quantity) || quantity < 0)
                            {
                                result.FailedImports++;
                                result.Errors.Add($"Row {i + 1}: Invalid Quantity '{fields[4]}'");
                                continue;
                            }

                            // Check if category exists
                            if (!CategoryExists(connection, transaction, categoryID))
                            {
                                result.FailedImports++;
                                result.Warnings.Add($"Row {i + 1}: CategoryID {categoryID} does not exist. Skipping product '{productName}'.");
                                continue;
                            }

                            // Check if supplier exists
                            if (!SupplierExists(connection, transaction, supplierID))
                            {
                                result.FailedImports++;
                                result.Warnings.Add($"Row {i + 1}: SupplierID {supplierID} does not exist. Skipping product '{productName}'.");
                                continue;
                            }

                            // Check for duplicate product name
                            if (ProductExists(connection, transaction, productName))
                            {
                                result.FailedImports++;
                                result.Warnings.Add($"Row {i + 1}: Product '{productName}' already exists. Skipping.");
                                continue;
                            }

                            // Insert product
                            string insertQuery = @"
                                INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Description, Barcode)
                                VALUES (@ProductName, @CategoryID, @SupplierID, @Price, @Quantity, @Description, @Barcode);
                            ";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ProductName", productName);
                                command.Parameters.AddWithValue("@CategoryID", categoryID);
                                command.Parameters.AddWithValue("@SupplierID", supplierID);
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Description", description ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Barcode", barcode ?? (object)DBNull.Value);
                                
                                command.ExecuteNonQuery();
                            }

                            result.SuccessfulImports++;
                        }

                        transaction.Commit();

                        errorMessage = $"Import completed. Success: {result.SuccessfulImports}, Failed: {result.FailedImports}";
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to import products: " + ex.Message;
                result.Errors.Add(errorMessage);
                clsErrorLog.LogException("clsProductImport.ImportFromCSV", ex);
                return result;
            }
        }

        /// <summary>
        /// Parses a CSV line handling quoted values.
        /// </summary>
        private static string[] ParseCSVLine(string line)
        {
            var result = new List<string>();
            var current = new System.Text.StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            result.Add(current.ToString());
            return result.ToArray();
        }

        /// <summary>
        /// Checks if a category exists.
        /// </summary>
        private static bool CategoryExists(SqlConnection connection, SqlTransaction transaction, int categoryID)
        {
            string query = "SELECT COUNT(*) FROM Categories WHERE CategoryID = @CategoryID";
            
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Checks if a supplier exists.
        /// </summary>
        private static bool SupplierExists(SqlConnection connection, SqlTransaction transaction, int supplierID)
        {
            string query = "SELECT COUNT(*) FROM Suppliers WHERE SupplierID = @SupplierID";
            
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@SupplierID", supplierID);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Checks if a product with the given name exists.
        /// </summary>
        private static bool ProductExists(SqlConnection connection, SqlTransaction transaction, string productName)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE ProductName = @ProductName";
            
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Generates a CSV template file for product import.
        /// </summary>
        /// <param name="filePath">Path where the template will be saved.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if template was created successfully.</returns>
        public static bool GenerateTemplate(string filePath, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                string header = "ProductName,CategoryID,SupplierID,Price,Quantity,Description,Barcode";
                string exampleLine = "Sample Product,1,1,19.99,100,Sample product description,1234567890123";
                
                File.WriteAllLines(filePath, new[] { header, exampleLine });
                
                errorMessage = "Template created successfully: " + filePath;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to create template: " + ex.Message;
                clsErrorLog.LogException("clsProductImport.GenerateTemplate", ex);
                return false;
            }
        }
    }
}
