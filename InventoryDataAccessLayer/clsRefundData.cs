using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsRefundData
    {
        public class RefundInfo
        {
            public int RefundID { get; set; }
            public int OrderID { get; set; }
            public DateTime RefundDate { get; set; }
            public decimal RefundAmount { get; set; }
            public string RefundReason { get; set; }
            public string RefundType { get; set; }
            public string RefundMethod { get; set; }
            public int ProcessedBy { get; set; }
            public bool IsVoided { get; set; }
            public DateTime? VoidDate { get; set; }
            public int? VoidedBy { get; set; }
            public string VoidReason { get; set; }
            public DateTime CreatedDate { get; set; }
            public string ProcessedByUserName { get; set; }
            public string VoidedByUserName { get; set; }
        }

        public class RefundItemInfo
        {
            public int RefundItemID { get; set; }
            public int RefundID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal RefundAmount { get; set; }
        }

        public static bool AddRefund(RefundInfo refund, out int refundID, out string errorMessage)
        {
            refundID = -1;
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Refunds (OrderID, RefundDate, RefundAmount, RefundReason, RefundType, RefundMethod, ProcessedBy, CreatedDate)
                        VALUES (@OrderID, GETDATE(), @RefundAmount, @RefundReason, @RefundType, @RefundMethod, @ProcessedBy, GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", refund.OrderID);
                        command.Parameters.AddWithValue("@RefundAmount", refund.RefundAmount);
                        command.Parameters.AddWithValue("@RefundReason", refund.RefundReason ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RefundType", refund.RefundType ?? "Full");
                        command.Parameters.AddWithValue("@RefundMethod", refund.RefundMethod ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProcessedBy", refund.ProcessedBy);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            refundID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create refund record.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool AddRefundItem(RefundItemInfo refundItem, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO RefundItems (RefundID, ProductID, ProductName, Quantity, UnitPrice, RefundAmount)
                        VALUES (@RefundID, @ProductID, @ProductName, @Quantity, @UnitPrice, @RefundAmount);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RefundID", refundItem.RefundID);
                        command.Parameters.AddWithValue("@ProductID", refundItem.ProductID);
                        command.Parameters.AddWithValue("@ProductName", refundItem.ProductName);
                        command.Parameters.AddWithValue("@Quantity", refundItem.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", refundItem.UnitPrice);
                        command.Parameters.AddWithValue("@RefundAmount", refundItem.RefundAmount);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool UpdateOrderRefundID(int orderID, int refundID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Orders SET RefundID = @RefundID WHERE OrderID = @OrderID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);
                        command.Parameters.AddWithValue("@RefundID", refundID);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool VoidRefund(int refundID, int voidedBy, string voidReason, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Refunds 
                        SET IsVoided = 1, VoidDate = GETDATE(), VoidedBy = @VoidedBy, VoidReason = @VoidReason
                        WHERE RefundID = @RefundID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RefundID", refundID);
                        command.Parameters.AddWithValue("@VoidedBy", voidedBy);
                        command.Parameters.AddWithValue("@VoidReason", voidReason ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static RefundInfo GetRefundByID(int refundID, out string errorMessage)
        {
            errorMessage = "";
            RefundInfo refund = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, 
                               u1.UserName as ProcessedByUserName,
                               u2.UserName as VoidedByUserName
                        FROM Refunds r
                        LEFT JOIN Users u1 ON r.ProcessedBy = u1.UserID
                        LEFT JOIN Users u2 ON r.VoidedBy = u2.UserID
                        WHERE r.RefundID = @RefundID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RefundID", refundID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                refund = new RefundInfo
                                {
                                    RefundID = reader.GetInt32(reader.GetOrdinal("RefundID")),
                                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                    RefundDate = reader.GetDateTime(reader.GetOrdinal("RefundDate")),
                                    RefundAmount = reader.GetDecimal(reader.GetOrdinal("RefundAmount")),
                                    RefundReason = reader.IsDBNull(reader.GetOrdinal("RefundReason")) ? null : reader.GetString(reader.GetOrdinal("RefundReason")),
                                    RefundType = reader.GetString(reader.GetOrdinal("RefundType")),
                                    RefundMethod = reader.IsDBNull(reader.GetOrdinal("RefundMethod")) ? null : reader.GetString(reader.GetOrdinal("RefundMethod")),
                                    ProcessedBy = reader.GetInt32(reader.GetOrdinal("ProcessedBy")),
                                    IsVoided = reader.GetBoolean(reader.GetOrdinal("IsVoided")),
                                    VoidDate = reader.IsDBNull(reader.GetOrdinal("VoidDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("VoidDate")),
                                    VoidedBy = reader.IsDBNull(reader.GetOrdinal("VoidedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VoidedBy")),
                                    VoidReason = reader.IsDBNull(reader.GetOrdinal("VoidReason")) ? null : reader.GetString(reader.GetOrdinal("VoidReason")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ProcessedByUserName = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserName")),
                                    VoidedByUserName = reader.IsDBNull(reader.GetOrdinal("VoidedByUserName")) ? null : reader.GetString(reader.GetOrdinal("VoidedByUserName"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return refund;
        }

        public static List<RefundItemInfo> GetRefundItems(int refundID, out string errorMessage)
        {
            errorMessage = "";
            List<RefundItemInfo> items = new List<RefundItemInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM RefundItems WHERE RefundID = @RefundID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RefundID", refundID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new RefundItemInfo
                                {
                                    RefundItemID = reader.GetInt32(reader.GetOrdinal("RefundItemID")),
                                    RefundID = reader.GetInt32(reader.GetOrdinal("RefundID")),
                                    ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                    RefundAmount = reader.GetDecimal(reader.GetOrdinal("RefundAmount"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return items;
        }

        public static List<RefundInfo> GetRefundsByOrder(int orderID, out string errorMessage)
        {
            errorMessage = "";
            List<RefundInfo> refunds = new List<RefundInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, 
                               u1.UserName as ProcessedByUserName,
                               u2.UserName as VoidedByUserName
                        FROM Refunds r
                        LEFT JOIN Users u1 ON r.ProcessedBy = u1.UserID
                        LEFT JOIN Users u2 ON r.VoidedBy = u2.UserID
                        WHERE r.OrderID = @OrderID
                        ORDER BY r.RefundDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                refunds.Add(new RefundInfo
                                {
                                    RefundID = reader.GetInt32(reader.GetOrdinal("RefundID")),
                                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                    RefundDate = reader.GetDateTime(reader.GetOrdinal("RefundDate")),
                                    RefundAmount = reader.GetDecimal(reader.GetOrdinal("RefundAmount")),
                                    RefundReason = reader.IsDBNull(reader.GetOrdinal("RefundReason")) ? null : reader.GetString(reader.GetOrdinal("RefundReason")),
                                    RefundType = reader.GetString(reader.GetOrdinal("RefundType")),
                                    RefundMethod = reader.IsDBNull(reader.GetOrdinal("RefundMethod")) ? null : reader.GetString(reader.GetOrdinal("RefundMethod")),
                                    ProcessedBy = reader.GetInt32(reader.GetOrdinal("ProcessedBy")),
                                    IsVoided = reader.GetBoolean(reader.GetOrdinal("IsVoided")),
                                    VoidDate = reader.IsDBNull(reader.GetOrdinal("VoidDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("VoidDate")),
                                    VoidedBy = reader.IsDBNull(reader.GetOrdinal("VoidedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VoidedBy")),
                                    VoidReason = reader.IsDBNull(reader.GetOrdinal("VoidReason")) ? null : reader.GetString(reader.GetOrdinal("VoidReason")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ProcessedByUserName = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserName")),
                                    VoidedByUserName = reader.IsDBNull(reader.GetOrdinal("VoidedByUserName")) ? null : reader.GetString(reader.GetOrdinal("VoidedByUserName"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return refunds;
        }

        public static List<RefundInfo> GetAllRefunds(out string errorMessage)
        {
            errorMessage = "";
            List<RefundInfo> refunds = new List<RefundInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, 
                               u1.UserName as ProcessedByUserName,
                               u2.UserName as VoidedByUserName
                        FROM Refunds r
                        LEFT JOIN Users u1 ON r.ProcessedBy = u1.UserID
                        LEFT JOIN Users u2 ON r.VoidedBy = u2.UserID
                        ORDER BY r.RefundDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                refunds.Add(new RefundInfo
                                {
                                    RefundID = reader.GetInt32(reader.GetOrdinal("RefundID")),
                                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                    RefundDate = reader.GetDateTime(reader.GetOrdinal("RefundDate")),
                                    RefundAmount = reader.GetDecimal(reader.GetOrdinal("RefundAmount")),
                                    RefundReason = reader.IsDBNull(reader.GetOrdinal("RefundReason")) ? null : reader.GetString(reader.GetOrdinal("RefundReason")),
                                    RefundType = reader.GetString(reader.GetOrdinal("RefundType")),
                                    RefundMethod = reader.IsDBNull(reader.GetOrdinal("RefundMethod")) ? null : reader.GetString(reader.GetOrdinal("RefundMethod")),
                                    ProcessedBy = reader.GetInt32(reader.GetOrdinal("ProcessedBy")),
                                    IsVoided = reader.GetBoolean(reader.GetOrdinal("IsVoided")),
                                    VoidDate = reader.IsDBNull(reader.GetOrdinal("VoidDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("VoidDate")),
                                    VoidedBy = reader.IsDBNull(reader.GetOrdinal("VoidedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VoidedBy")),
                                    VoidReason = reader.IsDBNull(reader.GetOrdinal("VoidReason")) ? null : reader.GetString(reader.GetOrdinal("VoidReason")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ProcessedByUserName = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserName")),
                                    VoidedByUserName = reader.IsDBNull(reader.GetOrdinal("VoidedByUserName")) ? null : reader.GetString(reader.GetOrdinal("VoidedByUserName"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return refunds;
        }
    }
}
