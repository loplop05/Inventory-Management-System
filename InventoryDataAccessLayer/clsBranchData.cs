using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsBranchData
    {
        public class BranchInfo
        {
            public int BranchID { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public int? UpdatedBy { get; set; }
        }

        public static bool AddBranch(BranchInfo branch, out int branchID, out string errorMessage)
        {
            branchID = -1;
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Branches (BranchName, BranchCode, Address, Phone, IsActive, CreatedDate, CreatedBy)
                        VALUES (@BranchName, @BranchCode, @Address, @Phone, @IsActive, GETDATE(), @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchName", branch.BranchName);
                        command.Parameters.AddWithValue("@BranchCode", branch.BranchCode);
                        command.Parameters.AddWithValue("@Address", branch.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", branch.Phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", branch.IsActive);
                        command.Parameters.AddWithValue("@CreatedBy", branch.CreatedBy ?? (object)DBNull.Value);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            branchID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create branch record.";
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

        public static bool UpdateBranch(BranchInfo branch, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Branches 
                        SET BranchName = @BranchName, 
                            BranchCode = @BranchCode, 
                            Address = @Address, 
                            Phone = @Phone, 
                            IsActive = @IsActive, 
                            UpdatedDate = GETDATE(), 
                            UpdatedBy = @UpdatedBy
                        WHERE BranchID = @BranchID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", branch.BranchID);
                        command.Parameters.AddWithValue("@BranchName", branch.BranchName);
                        command.Parameters.AddWithValue("@BranchCode", branch.BranchCode);
                        command.Parameters.AddWithValue("@Address", branch.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", branch.Phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", branch.IsActive);
                        command.Parameters.AddWithValue("@UpdatedBy", branch.UpdatedBy ?? (object)DBNull.Value);

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

        public static bool DeleteBranch(int branchID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    // Check if branch has registers
                    string checkQuery = "SELECT COUNT(*) FROM Registers WHERE BranchID = @BranchID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@BranchID", branchID);
                        int registerCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (registerCount > 0)
                        {
                            errorMessage = "Cannot delete branch with existing registers.";
                            return false;
                        }
                    }

                    string query = "DELETE FROM Branches WHERE BranchID = @BranchID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", branchID);
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

        public static BranchInfo GetBranchByID(int branchID, out string errorMessage)
        {
            errorMessage = "";
            BranchInfo branch = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Branches WHERE BranchID = @BranchID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", branchID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                branch = new BranchInfo
                                {
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                                    BranchCode = reader.GetString(reader.GetOrdinal("BranchCode")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy"))
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

            return branch;
        }

        public static List<BranchInfo> GetAllBranches(out string errorMessage)
        {
            errorMessage = "";
            List<BranchInfo> branches = new List<BranchInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Branches ORDER BY BranchName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                branches.Add(new BranchInfo
                                {
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                                    BranchCode = reader.GetString(reader.GetOrdinal("BranchCode")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy"))
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

            return branches;
        }

        public static List<BranchInfo> GetActiveBranches(out string errorMessage)
        {
            errorMessage = "";
            List<BranchInfo> branches = new List<BranchInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Branches WHERE IsActive = 1 ORDER BY BranchName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                branches.Add(new BranchInfo
                                {
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                                    BranchCode = reader.GetString(reader.GetOrdinal("BranchCode")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy"))
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

            return branches;
        }

        public static bool BranchExists(string branchCode, out string errorMessage)
        {
            errorMessage = "";
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Branches WHERE BranchCode = @BranchCode";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchCode", branchCode);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        exists = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return exists;
        }
    }
}
