using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace InventoryDataAccessLayer
{
    public static class clsHeldOrderData
    {
        public class HeldOrderInfo
        {
            public int HeldOrderID { get; set; }
            public int? UserID { get; set; }
            public int? CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string PaymentMethod { get; set; }
            public string PaymentDetails { get; set; }
            public string CouponCode { get; set; }
            public string ManualDiscountType { get; set; }
            public decimal? ManualDiscountValue { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Notes { get; set; }
            public List<HeldOrderItemInfo> Items { get; set; } = new List<HeldOrderItemInfo>();
        }

        public class HeldOrderItemInfo
        {
            public int HeldOrderItemID { get; set; }
            public int HeldOrderID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Subtotal { get; set; }
        }

        public static int SaveHeldOrder(HeldOrderInfo heldOrder, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Insert held order
                    string insertOrderQuery = @"
                        INSERT INTO HeldOrders (UserID, CustomerID, CustomerName, CustomerPhone, PaymentMethod, PaymentDetails, 
                                                CouponCode, ManualDiscountType, ManualDiscountValue, Notes)
                        VALUES (@UserID, @CustomerID, @CustomerName, @CustomerPhone, @PaymentMethod, @PaymentDetails,
                                @CouponCode, @ManualDiscountType, @ManualDiscountValue, @Notes);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand command = new SqlCommand(insertOrderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", (object)heldOrder.UserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerID", (object)heldOrder.CustomerID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerName", (object)heldOrder.CustomerName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerPhone", (object)heldOrder.CustomerPhone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentMethod", (object)heldOrder.PaymentMethod ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentDetails", (object)heldOrder.PaymentDetails ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CouponCode", (object)heldOrder.CouponCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ManualDiscountType", (object)heldOrder.ManualDiscountType ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ManualDiscountValue", (object)heldOrder.ManualDiscountValue ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)heldOrder.Notes ?? DBNull.Value);

                        heldOrder.HeldOrderID = (int)command.ExecuteScalar();
                    }

                    // Insert held order items
                    foreach (var item in heldOrder.Items)
                    {
                        string insertItemQuery = @"
                            INSERT INTO HeldOrderItems (HeldOrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
                            VALUES (@HeldOrderID, @ProductID, @ProductName, @Quantity, @UnitPrice, @Subtotal);";

                        using (SqlCommand command = new SqlCommand(insertItemQuery, connection))
                        {
                            command.Parameters.AddWithValue("@HeldOrderID", heldOrder.HeldOrderID);
                            command.Parameters.AddWithValue("@ProductID", item.ProductID);
                            command.Parameters.AddWithValue("@ProductName", item.ProductName);
                            command.Parameters.AddWithValue("@Quantity", item.Quantity);
                            command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            command.Parameters.AddWithValue("@Subtotal", item.Subtotal);

                            command.ExecuteNonQuery();
                        }
                    }

                    return heldOrder.HeldOrderID;
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error: " + ex.Message;
                    return -1;
                }
            }
        }

        public static HeldOrderInfo GetHeldOrder(int heldOrderID, out string errorMessage)
        {
            errorMessage = "";
            HeldOrderInfo heldOrder = null;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Get held order
                    string orderQuery = @"
                        SELECT HeldOrderID, UserID, CustomerID, CustomerName, CustomerPhone, PaymentMethod, PaymentDetails,
                               CouponCode, ManualDiscountType, ManualDiscountValue, CreatedDate, Notes
                        FROM HeldOrders
                        WHERE HeldOrderID = @HeldOrderID;";

                    using (SqlCommand command = new SqlCommand(orderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HeldOrderID", heldOrderID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                heldOrder = new HeldOrderInfo
                                {
                                    HeldOrderID = reader.GetInt32(reader.GetOrdinal("HeldOrderID")),
                                    UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserID")),
                                    CustomerID = reader.IsDBNull(reader.GetOrdinal("CustomerID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName")) ? null : reader.GetString(reader.GetOrdinal("CustomerName")),
                                    CustomerPhone = reader.IsDBNull(reader.GetOrdinal("CustomerPhone")) ? null : reader.GetString(reader.GetOrdinal("CustomerPhone")),
                                    PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod")) ? null : reader.GetString(reader.GetOrdinal("PaymentMethod")),
                                    PaymentDetails = reader.IsDBNull(reader.GetOrdinal("PaymentDetails")) ? null : reader.GetString(reader.GetOrdinal("PaymentDetails")),
                                    CouponCode = reader.IsDBNull(reader.GetOrdinal("CouponCode")) ? null : reader.GetString(reader.GetOrdinal("CouponCode")),
                                    ManualDiscountType = reader.IsDBNull(reader.GetOrdinal("ManualDiscountType")) ? null : reader.GetString(reader.GetOrdinal("ManualDiscountType")),
                                    ManualDiscountValue = reader.IsDBNull(reader.GetOrdinal("ManualDiscountValue")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ManualDiscountValue")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"))
                                };
                            }
                        }
                    }

                    if (heldOrder == null)
                    {
                        errorMessage = "Held order not found.";
                        return null;
                    }

                    // Get held order items
                    string itemsQuery = @"
                        SELECT HeldOrderItemID, HeldOrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal
                        FROM HeldOrderItems
                        WHERE HeldOrderID = @HeldOrderID;";

                    using (SqlCommand command = new SqlCommand(itemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HeldOrderID", heldOrderID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                heldOrder.Items.Add(new HeldOrderItemInfo
                                {
                                    HeldOrderItemID = reader.GetInt32(reader.GetOrdinal("HeldOrderItemID")),
                                    HeldOrderID = reader.GetInt32(reader.GetOrdinal("HeldOrderID")),
                                    ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
                                });
                            }
                        }
                    }

                    return heldOrder;
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error: " + ex.Message;
                    return null;
                }
            }
        }

        public static DataTable GetAllHeldOrders(out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT h.HeldOrderID, h.UserID, h.CustomerID, h.CustomerName, h.CustomerPhone,
                               h.PaymentMethod, h.CouponCode, h.ManualDiscountType, h.ManualDiscountValue,
                               h.CreatedDate, h.Notes, COUNT(i.HeldOrderItemID) AS ItemCount,
                               SUM(i.Subtotal) AS TotalAmount
                        FROM HeldOrders h
                        LEFT JOIN HeldOrderItems i ON h.HeldOrderID = i.HeldOrderID
                        GROUP BY h.HeldOrderID, h.UserID, h.CustomerID, h.CustomerName, h.CustomerPhone,
                                 h.PaymentMethod, h.CouponCode, h.ManualDiscountType, h.ManualDiscountValue,
                                 h.CreatedDate, h.Notes
                        ORDER BY h.CreatedDate DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error: " + ex.Message;
                    return null;
                }
            }
        }

        public static bool DeleteHeldOrder(int heldOrderID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM HeldOrders WHERE HeldOrderID = @HeldOrderID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HeldOrderID", heldOrderID);
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error: " + ex.Message;
                    return false;
                }
            }
        }
    }
}
