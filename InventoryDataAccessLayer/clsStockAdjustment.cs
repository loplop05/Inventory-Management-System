using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// Manual stock adjustment with reason codes.
    /// Allows for tracking inventory changes with audit trail.
    /// </summary>
    public static class clsStockAdjustment
    {
        /// <summary>
        /// Reason codes for stock adjustments.
        /// </summary>
        public enum AdjustmentReason
        {
            Damaged = 1,
            Expired = 2,
            Lost = 3,
            Theft = 4,
            Restock = 5,
            Correction = 6,
            Return = 7,
            Other = 99
        }

        /// <summary>
        /// Performs a manual stock adjustment.
        /// </summary>
        /// <param name="productID">The product ID to adjust.</param>
        /// <param name="quantityChange">The change in quantity (positive to add, negative to remove).</param>
        /// <param name="reason">The reason for the adjustment.</param>
        /// <param name="notes">Additional notes about the adjustment.</param>
        /// <param name="userID">The user ID making the adjustment.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if adjustment succeeded, false otherwise.</returns>
        public static bool AdjustStock(int productID, int quantityChange, AdjustmentReason reason, string notes, string userID, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (quantityChange == 0)
            {
                errorMessage = "Quantity change cannot be zero.";
                return false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Get current stock
                        int currentStock = 0;
                        string getCurrentStockQuery = "SELECT Quantity FROM Products WHERE ProductID = @ProductID";
                        
                        using (SqlCommand command = new SqlCommand(getCurrentStockQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ProductID", productID);
                            object result = command.ExecuteScalar();
                            
                            if (result == null || result == DBNull.Value)
                            {
                                errorMessage = "Product not found.";
                                transaction.Rollback();
                                return false;
                            }
                            
                            currentStock = Convert.ToInt32(result);
                        }

                        // Check if adjustment would result in negative stock
                        int newStock = currentStock + quantityChange;
                        if (newStock < 0)
                        {
                            errorMessage = $"Adjustment would result in negative stock. Current: {currentStock}, Change: {quantityChange}";
                            transaction.Rollback();
                            return false;
                        }

                        // Update product stock
                        string updateStockQuery = @"
                            UPDATE Products 
                            SET Quantity = @NewStock
                            WHERE ProductID = @ProductID;
                        ";

                        using (SqlCommand command = new SqlCommand(updateStockQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@NewStock", newStock);
                            command.Parameters.AddWithValue("@ProductID", productID);
                            command.ExecuteNonQuery();
                        }

                        // Log the adjustment
                        string logAdjustmentQuery = @"
                            INSERT INTO StockAdjustments (ProductID, QuantityChange, PreviousStock, NewStock, Reason, Notes, AdjustedBy, AdjustmentDate)
                            VALUES (@ProductID, @QuantityChange, @PreviousStock, @NewStock, @Reason, @Notes, @AdjustedBy, GETDATE());
                        ";

                        using (SqlCommand command = new SqlCommand(logAdjustmentQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ProductID", productID);
                            command.Parameters.AddWithValue("@QuantityChange", quantityChange);
                            command.Parameters.AddWithValue("@PreviousStock", currentStock);
                            command.Parameters.AddWithValue("@NewStock", newStock);
                            command.Parameters.AddWithValue("@Reason", (int)reason);
                            command.Parameters.AddWithValue("@Notes", notes ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@AdjustedBy", userID ?? (object)DBNull.Value);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // Log to audit log
                        clsAuditLog.LogAction("Stock Adjustment", 
                            $"ProductID: {productID}, Change: {quantityChange}, Reason: {reason}, Notes: {notes}", 
                            "Inventory");

                        errorMessage = $"Stock adjusted successfully. Previous: {currentStock}, New: {newStock}";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to adjust stock: " + ex.Message;
                clsErrorLog.LogException("clsStockAdjustment.AdjustStock", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the adjustment history for a product.
        /// </summary>
        /// <param name="productID">The product ID.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>DataTable containing adjustment history.</returns>
        public static DataTable GetAdjustmentHistory(int productID, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            AdjustmentID,
                            ProductID,
                            QuantityChange,
                            PreviousStock,
                            NewStock,
                            Reason,
                            Notes,
                            AdjustedBy,
                            AdjustmentDate
                        FROM StockAdjustments
                        WHERE ProductID = @ProductID
                        ORDER BY AdjustmentDate DESC;
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to get adjustment history: " + ex.Message;
                clsErrorLog.LogException("clsStockAdjustment.GetAdjustmentHistory", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets all stock adjustments within a date range.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>DataTable containing all adjustments.</returns>
        public static DataTable GetAllAdjustments(DateTime startDate, DateTime endDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            sa.AdjustmentID,
                            sa.ProductID,
                            p.ProductName,
                            sa.QuantityChange,
                            sa.PreviousStock,
                            sa.NewStock,
                            sa.Reason,
                            sa.Notes,
                            sa.AdjustedBy,
                            sa.AdjustmentDate
                        FROM StockAdjustments sa
                        INNER JOIN Products p ON sa.ProductID = p.ProductID
                        WHERE sa.AdjustmentDate >= @StartDate AND sa.AdjustmentDate <= @EndDate
                        ORDER BY sa.AdjustmentDate DESC;
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to get adjustments: " + ex.Message;
                clsErrorLog.LogException("clsStockAdjustment.GetAllAdjustments", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a friendly name for an adjustment reason.
        /// </summary>
        public static string GetReasonName(AdjustmentReason reason)
        {
            switch (reason)
            {
                case AdjustmentReason.Damaged:
                    return "Damaged";
                case AdjustmentReason.Expired:
                    return "Expired";
                case AdjustmentReason.Lost:
                    return "Lost";
                case AdjustmentReason.Theft:
                    return "Theft";
                case AdjustmentReason.Restock:
                    return "Restock";
                case AdjustmentReason.Correction:
                    return "Correction";
                case AdjustmentReason.Return:
                    return "Return";
                case AdjustmentReason.Other:
                    return "Other";
                default:
                    return "Unknown";
            }
        }
    }
}
