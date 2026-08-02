using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsStockTransferData
    {
        public class StockTransferInfo
        {
            public int TransferID { get; set; }
            public int FromBranchID { get; set; }
            public int ToBranchID { get; set; }
            public DateTime TransferDate { get; set; }
            public string TransferStatus { get; set; }
            public string Notes { get; set; }
            public int CreatedBy { get; set; }
            public int? ApprovedBy { get; set; }
            public DateTime? ApprovalDate { get; set; }
            public int? CompletedBy { get; set; }
            public DateTime? CompletionDate { get; set; }
            public string FromBranchName { get; set; }
            public string ToBranchName { get; set; }
            public string CreatedByUserName { get; set; }
            public string ApprovedByUserName { get; set; }
            public string CompletedByUserName { get; set; }
        }

        public class StockTransferItemInfo
        {
            public int TransferItemID { get; set; }
            public int TransferID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public static bool AddStockTransfer(StockTransferInfo transfer, out int transferID, out string errorMessage)
        {
            transferID = -1;
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO StockTransfers (FromBranchID, ToBranchID, TransferDate, TransferStatus, Notes, CreatedBy)
                        VALUES (@FromBranchID, @ToBranchID, GETDATE(), @TransferStatus, @Notes, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromBranchID", transfer.FromBranchID);
                        command.Parameters.AddWithValue("@ToBranchID", transfer.ToBranchID);
                        command.Parameters.AddWithValue("@TransferStatus", transfer.TransferStatus ?? "Pending");
                        command.Parameters.AddWithValue("@Notes", transfer.Notes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", transfer.CreatedBy);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            transferID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create stock transfer record.";
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

        public static bool AddStockTransferItem(StockTransferItemInfo transferItem, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO StockTransferItems (TransferID, ProductID, ProductName, Quantity, UnitPrice)
                        VALUES (@TransferID, @ProductID, @ProductName, @Quantity, @UnitPrice);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransferID", transferItem.TransferID);
                        command.Parameters.AddWithValue("@ProductID", transferItem.ProductID);
                        command.Parameters.AddWithValue("@ProductName", transferItem.ProductName);
                        command.Parameters.AddWithValue("@Quantity", transferItem.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", transferItem.UnitPrice);

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

        public static bool UpdateTransferStatus(int transferID, string status, int? approvedBy, int? completedBy, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE StockTransfers 
                        SET TransferStatus = @TransferStatus";

                    if (status == "Approved" && approvedBy.HasValue)
                    {
                        query += ", ApprovedBy = @ApprovedBy, ApprovalDate = GETDATE()";
                    }
                    else if (status == "Completed" && completedBy.HasValue)
                    {
                        query += ", CompletedBy = @CompletedBy, CompletionDate = GETDATE()";
                    }

                    query += " WHERE TransferID = @TransferID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransferID", transferID);
                        command.Parameters.AddWithValue("@TransferStatus", status);

                        if (status == "Approved" && approvedBy.HasValue)
                        {
                            command.Parameters.AddWithValue("@ApprovedBy", approvedBy.Value);
                        }
                        else if (status == "Completed" && completedBy.HasValue)
                        {
                            command.Parameters.AddWithValue("@CompletedBy", completedBy.Value);
                        }

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

        public static StockTransferInfo GetStockTransferByID(int transferID, out string errorMessage)
        {
            errorMessage = "";
            StockTransferInfo transfer = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT st.*, 
                               b1.BranchName as FromBranchName,
                               b2.BranchName as ToBranchName,
                               u1.UserName as CreatedByUserName,
                               u2.UserName as ApprovedByUserName,
                               u3.UserName as CompletedByUserName
                        FROM StockTransfers st
                        INNER JOIN Branches b1 ON st.FromBranchID = b1.BranchID
                        INNER JOIN Branches b2 ON st.ToBranchID = b2.BranchID
                        LEFT JOIN Users u1 ON st.CreatedBy = u1.UserID
                        LEFT JOIN Users u2 ON st.ApprovedBy = u2.UserID
                        LEFT JOIN Users u3 ON st.CompletedBy = u3.UserID
                        WHERE st.TransferID = @TransferID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransferID", transferID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transfer = new StockTransferInfo
                                {
                                    TransferID = reader.GetInt32(reader.GetOrdinal("TransferID")),
                                    FromBranchID = reader.GetInt32(reader.GetOrdinal("FromBranchID")),
                                    ToBranchID = reader.GetInt32(reader.GetOrdinal("ToBranchID")),
                                    TransferDate = reader.GetDateTime(reader.GetOrdinal("TransferDate")),
                                    TransferStatus = reader.GetString(reader.GetOrdinal("TransferStatus")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                                    ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                                    CompletedBy = reader.IsDBNull(reader.GetOrdinal("CompletedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CompletedBy")),
                                    CompletionDate = reader.IsDBNull(reader.GetOrdinal("CompletionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletionDate")),
                                    FromBranchName = reader.GetString(reader.GetOrdinal("FromBranchName")),
                                    ToBranchName = reader.GetString(reader.GetOrdinal("ToBranchName")),
                                    CreatedByUserName = reader.IsDBNull(reader.GetOrdinal("CreatedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CreatedByUserName")),
                                    ApprovedByUserName = reader.IsDBNull(reader.GetOrdinal("ApprovedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByUserName")),
                                    CompletedByUserName = reader.IsDBNull(reader.GetOrdinal("CompletedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CompletedByUserName"))
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

            return transfer;
        }

        public static List<StockTransferItemInfo> GetStockTransferItems(int transferID, out string errorMessage)
        {
            errorMessage = "";
            List<StockTransferItemInfo> items = new List<StockTransferItemInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM StockTransferItems WHERE TransferID = @TransferID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransferID", transferID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new StockTransferItemInfo
                                {
                                    TransferItemID = reader.GetInt32(reader.GetOrdinal("TransferItemID")),
                                    TransferID = reader.GetInt32(reader.GetOrdinal("TransferID")),
                                    ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
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

        public static List<StockTransferInfo> GetAllStockTransfers(out string errorMessage)
        {
            errorMessage = "";
            List<StockTransferInfo> transfers = new List<StockTransferInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT st.*, 
                               b1.BranchName as FromBranchName,
                               b2.BranchName as ToBranchName,
                               u1.UserName as CreatedByUserName,
                               u2.UserName as ApprovedByUserName,
                               u3.UserName as CompletedByUserName
                        FROM StockTransfers st
                        INNER JOIN Branches b1 ON st.FromBranchID = b1.BranchID
                        INNER JOIN Branches b2 ON st.ToBranchID = b2.BranchID
                        LEFT JOIN Users u1 ON st.CreatedBy = u1.UserID
                        LEFT JOIN Users u2 ON st.ApprovedBy = u2.UserID
                        LEFT JOIN Users u3 ON st.CompletedBy = u3.UserID
                        ORDER BY st.TransferDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                transfers.Add(new StockTransferInfo
                                {
                                    TransferID = reader.GetInt32(reader.GetOrdinal("TransferID")),
                                    FromBranchID = reader.GetInt32(reader.GetOrdinal("FromBranchID")),
                                    ToBranchID = reader.GetInt32(reader.GetOrdinal("ToBranchID")),
                                    TransferDate = reader.GetDateTime(reader.GetOrdinal("TransferDate")),
                                    TransferStatus = reader.GetString(reader.GetOrdinal("TransferStatus")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                                    ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                                    CompletedBy = reader.IsDBNull(reader.GetOrdinal("CompletedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CompletedBy")),
                                    CompletionDate = reader.IsDBNull(reader.GetOrdinal("CompletionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletionDate")),
                                    FromBranchName = reader.GetString(reader.GetOrdinal("FromBranchName")),
                                    ToBranchName = reader.GetString(reader.GetOrdinal("ToBranchName")),
                                    CreatedByUserName = reader.IsDBNull(reader.GetOrdinal("CreatedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CreatedByUserName")),
                                    ApprovedByUserName = reader.IsDBNull(reader.GetOrdinal("ApprovedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByUserName")),
                                    CompletedByUserName = reader.IsDBNull(reader.GetOrdinal("CompletedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CompletedByUserName"))
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

            return transfers;
        }

        public static List<StockTransferInfo> GetStockTransfersByBranch(int branchID, out string errorMessage)
        {
            errorMessage = "";
            List<StockTransferInfo> transfers = new List<StockTransferInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT st.*, 
                               b1.BranchName as FromBranchName,
                               b2.BranchName as ToBranchName,
                               u1.UserName as CreatedByUserName,
                               u2.UserName as ApprovedByUserName,
                               u3.UserName as CompletedByUserName
                        FROM StockTransfers st
                        INNER JOIN Branches b1 ON st.FromBranchID = b1.BranchID
                        INNER JOIN Branches b2 ON st.ToBranchID = b2.BranchID
                        LEFT JOIN Users u1 ON st.CreatedBy = u1.UserID
                        LEFT JOIN Users u2 ON st.ApprovedBy = u2.UserID
                        LEFT JOIN Users u3 ON st.CompletedBy = u3.UserID
                        WHERE st.FromBranchID = @BranchID OR st.ToBranchID = @BranchID
                        ORDER BY st.TransferDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", branchID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                transfers.Add(new StockTransferInfo
                                {
                                    TransferID = reader.GetInt32(reader.GetOrdinal("TransferID")),
                                    FromBranchID = reader.GetInt32(reader.GetOrdinal("FromBranchID")),
                                    ToBranchID = reader.GetInt32(reader.GetOrdinal("ToBranchID")),
                                    TransferDate = reader.GetDateTime(reader.GetOrdinal("TransferDate")),
                                    TransferStatus = reader.GetString(reader.GetOrdinal("TransferStatus")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                                    ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                                    CompletedBy = reader.IsDBNull(reader.GetOrdinal("CompletedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CompletedBy")),
                                    CompletionDate = reader.IsDBNull(reader.GetOrdinal("CompletionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletionDate")),
                                    FromBranchName = reader.GetString(reader.GetOrdinal("FromBranchName")),
                                    ToBranchName = reader.GetString(reader.GetOrdinal("ToBranchName")),
                                    CreatedByUserName = reader.IsDBNull(reader.GetOrdinal("CreatedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CreatedByUserName")),
                                    ApprovedByUserName = reader.IsDBNull(reader.GetOrdinal("ApprovedByUserName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByUserName")),
                                    CompletedByUserName = reader.IsDBNull(reader.GetOrdinal("CompletedByUserName")) ? null : reader.GetString(reader.GetOrdinal("CompletedByUserName"))
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

            return transfers;
        }
    }
}
