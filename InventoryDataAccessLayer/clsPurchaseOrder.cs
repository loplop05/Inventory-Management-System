using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// Purchase order workflow for managing stock replenishment.
    /// Supports creating, tracking, and completing purchase orders.
    /// </summary>
    public static class clsPurchaseOrder
    {
        /// <summary>
        /// Status of a purchase order.
        /// </summary>
        public enum OrderStatus
        {
            Draft = 0,
            Submitted = 1,
            Approved = 2,
            Ordered = 3,
            Received = 4,
            Cancelled = 5
        }

        /// <summary>
        /// Information about a purchase order item.
        /// </summary>
        public class PurchaseOrderItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitCost { get; set; }
            public decimal TotalCost => Quantity * UnitCost;
        }

        /// <summary>
        /// Creates a new purchase order.
        /// </summary>
        /// <param name="supplierID">The supplier ID.</param>
        /// <param name="items">List of items to order.</param>
        /// <param name="notes">Optional notes for the order.</param>
        /// <param name="createdBy">User who created the order.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>The purchase order ID if successful, -1 otherwise.</returns>
        public static int CreatePurchaseOrder(int supplierID, PurchaseOrderItem[] items, string notes, string createdBy, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (items == null || items.Length == 0)
            {
                errorMessage = "Purchase order must contain at least one item.";
                return -1;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Calculate total cost
                        decimal totalCost = 0;
                        foreach (var item in items)
                        {
                            totalCost += item.TotalCost;
                        }

                        // Insert purchase order
                        string insertOrderQuery = @"
                            INSERT INTO PurchaseOrders (SupplierID, TotalCost, Status, Notes, CreatedBy, CreatedDate, ExpectedDate)
                            VALUES (@SupplierID, @TotalCost, @Status, @Notes, @CreatedBy, GETDATE(), DATEADD(DAY, 7, GETDATE()));
                            SELECT SCOPE_IDENTITY();
                        ";

                        int purchaseOrderID;
                        using (SqlCommand command = new SqlCommand(insertOrderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SupplierID", supplierID);
                            command.Parameters.AddWithValue("@TotalCost", totalCost);
                            command.Parameters.AddWithValue("@Status", (int)OrderStatus.Draft);
                            command.Parameters.AddWithValue("@Notes", notes ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CreatedBy", createdBy ?? (object)DBNull.Value);
                            
                            purchaseOrderID = Convert.ToInt32(command.ExecuteScalar());
                        }

                        // Insert purchase order items
                        string insertItemQuery = @"
                            INSERT INTO PurchaseOrderItems (PurchaseOrderID, ProductID, Quantity, UnitCost, TotalCost)
                            VALUES (@PurchaseOrderID, @ProductID, @Quantity, @UnitCost, @TotalCost);
                        ";

                        foreach (var item in items)
                        {
                            using (SqlCommand command = new SqlCommand(insertItemQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                                command.Parameters.AddWithValue("@ProductID", item.ProductID);
                                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                                command.Parameters.AddWithValue("@UnitCost", item.UnitCost);
                                command.Parameters.AddWithValue("@TotalCost", item.TotalCost);
                                
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        // Log to audit log
                        clsAuditLog.LogAction("Purchase Order Created", 
                            $"PurchaseOrderID: {purchaseOrderID}, SupplierID: {supplierID}, Items: {items.Length}, Total: {totalCost:C2}", 
                            "Inventory");

                        errorMessage = $"Purchase order created successfully. ID: {purchaseOrderID}";
                        return purchaseOrderID;
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
                errorMessage = "Failed to create purchase order: " + ex.Message;
                clsErrorLog.LogException("clsPurchaseOrder.CreatePurchaseOrder", ex);
                return -1;
            }
        }

        /// <summary>
        /// Updates the status of a purchase order.
        /// </summary>
        /// <param name="purchaseOrderID">The purchase order ID.</param>
        /// <param name="status">The new status.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if update succeeded, false otherwise.</returns>
        public static bool UpdateOrderStatus(int purchaseOrderID, OrderStatus status, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE PurchaseOrders
                        SET Status = @Status,
                            ModifiedDate = GETDATE()
                        WHERE PurchaseOrderID = @PurchaseOrderID;
                    ";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Status", (int)status);
                        command.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        
                        if (rowsAffected == 0)
                        {
                            errorMessage = "Purchase order not found.";
                            return false;
                        }
                    }

                    // Log to audit log
                    clsAuditLog.LogAction("Purchase Order Status Updated", 
                        $"PurchaseOrderID: {purchaseOrderID}, New Status: {status}", 
                        "Inventory");

                    errorMessage = "Purchase order status updated successfully.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to update purchase order status: " + ex.Message;
                clsErrorLog.LogException("clsPurchaseOrder.UpdateOrderStatus", ex);
                return false;
            }
        }

        /// <summary>
        /// Receives a purchase order and updates stock levels.
        /// </summary>
        /// <param name="purchaseOrderID">The purchase order ID.</param>
        /// <param name="receivedBy">User who received the order.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if receipt succeeded, false otherwise.</returns>
        public static bool ReceivePurchaseOrder(int purchaseOrderID, string receivedBy, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Get purchase order items
                        DataTable items = GetOrderItems(purchaseOrderID, connection, transaction);
                        
                        if (items == null || items.Rows.Count == 0)
                        {
                            errorMessage = "Purchase order has no items.";
                            transaction.Rollback();
                            return false;
                        }

                        // Update stock for each item
                        foreach (DataRow row in items.Rows)
                        {
                            int productID = Convert.ToInt32(row["ProductID"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);

                            string updateStockQuery = @"
                                UPDATE Products
                                SET Quantity = Quantity + @Quantity
                                WHERE ProductID = @ProductID;
                            ";

                            using (SqlCommand command = new SqlCommand(updateStockQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@ProductID", productID);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Update order status to Received
                        string updateStatusQuery = @"
                            UPDATE PurchaseOrders
                            SET Status = @Status,
                                ReceivedDate = GETDATE(),
                                ReceivedBy = @ReceivedBy
                            WHERE PurchaseOrderID = @PurchaseOrderID;
                        ";

                        using (SqlCommand command = new SqlCommand(updateStatusQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Status", (int)OrderStatus.Received);
                            command.Parameters.AddWithValue("@ReceivedBy", receivedBy ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // Log to audit log
                        clsAuditLog.LogAction("Purchase Order Received", 
                            $"PurchaseOrderID: {purchaseOrderID}, Items: {items.Rows.Count}", 
                            "Inventory");

                        errorMessage = "Purchase order received successfully. Stock updated.";
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
                errorMessage = "Failed to receive purchase order: " + ex.Message;
                clsErrorLog.LogException("clsPurchaseOrder.ReceivePurchaseOrder", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets all purchase orders.
        /// </summary>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>DataTable containing purchase orders.</returns>
        public static DataTable GetAllPurchaseOrders(out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            po.PurchaseOrderID,
                            po.SupplierID,
                            s.SupplierName,
                            po.TotalCost,
                            po.Status,
                            po.Notes,
                            po.CreatedBy,
                            po.CreatedDate,
                            po.ExpectedDate,
                            po.ReceivedDate,
                            po.ReceivedBy
                        FROM PurchaseOrders po
                        LEFT JOIN Suppliers s ON po.SupplierID = s.SupplierID
                        ORDER BY po.CreatedDate DESC;
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to get purchase orders: " + ex.Message;
                clsErrorLog.LogException("clsPurchaseOrder.GetAllPurchaseOrders", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets items for a specific purchase order.
        /// </summary>
        /// <param name="purchaseOrderID">The purchase order ID.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>DataTable containing order items.</returns>
        public static DataTable GetOrderItems(int purchaseOrderID, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            poi.PurchaseOrderItemID,
                            poi.PurchaseOrderID,
                            poi.ProductID,
                            p.ProductName,
                            poi.Quantity,
                            poi.UnitCost,
                            poi.TotalCost
                        FROM PurchaseOrderItems poi
                        INNER JOIN Products p ON poi.ProductID = p.ProductID
                        WHERE poi.PurchaseOrderID = @PurchaseOrderID;
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to get order items: " + ex.Message;
                clsErrorLog.LogException("clsPurchaseOrder.GetOrderItems", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets items for a purchase order (internal use with transaction).
        /// </summary>
        private static DataTable GetOrderItems(int purchaseOrderID, SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
                SELECT ProductID, Quantity, UnitCost, TotalCost
                FROM PurchaseOrderItems
                WHERE PurchaseOrderID = @PurchaseOrderID;
            ";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                
                return table;
            }
        }

        /// <summary>
        /// Gets a friendly name for an order status.
        /// </summary>
        public static string GetStatusName(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Draft:
                    return "Draft";
                case OrderStatus.Submitted:
                    return "Submitted";
                case OrderStatus.Approved:
                    return "Approved";
                case OrderStatus.Ordered:
                    return "Ordered";
                case OrderStatus.Received:
                    return "Received";
                case OrderStatus.Cancelled:
                    return "Cancelled";
                default:
                    return "Unknown";
            }
        }
    }
}
