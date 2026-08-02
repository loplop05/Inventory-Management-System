using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsRegisterData
    {
        public class RegisterInfo
        {
            public int RegisterID { get; set; }
            public int BranchID { get; set; }
            public string RegisterName { get; set; }
            public string RegisterCode { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public int? UpdatedBy { get; set; }
            public string BranchName { get; set; } // Joined from Branches table
        }

        public static bool AddRegister(RegisterInfo register, out int registerID, out string errorMessage)
        {
            registerID = -1;
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Registers (BranchID, RegisterName, RegisterCode, IsActive, CreatedDate, CreatedBy)
                        VALUES (@BranchID, @RegisterName, @RegisterCode, @IsActive, GETDATE(), @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", register.BranchID);
                        command.Parameters.AddWithValue("@RegisterName", register.RegisterName);
                        command.Parameters.AddWithValue("@RegisterCode", register.RegisterCode);
                        command.Parameters.AddWithValue("@IsActive", register.IsActive);
                        command.Parameters.AddWithValue("@CreatedBy", register.CreatedBy ?? (object)DBNull.Value);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            registerID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create register record.";
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

        public static bool UpdateRegister(RegisterInfo register, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Registers 
                        SET BranchID = @BranchID, 
                            RegisterName = @RegisterName, 
                            RegisterCode = @RegisterCode, 
                            IsActive = @IsActive, 
                            UpdatedDate = GETDATE(), 
                            UpdatedBy = @UpdatedBy
                        WHERE RegisterID = @RegisterID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegisterID", register.RegisterID);
                        command.Parameters.AddWithValue("@BranchID", register.BranchID);
                        command.Parameters.AddWithValue("@RegisterName", register.RegisterName);
                        command.Parameters.AddWithValue("@RegisterCode", register.RegisterCode);
                        command.Parameters.AddWithValue("@IsActive", register.IsActive);
                        command.Parameters.AddWithValue("@UpdatedBy", register.UpdatedBy ?? (object)DBNull.Value);

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

        public static bool DeleteRegister(int registerID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    // Check if register has orders
                    string checkQuery = "SELECT COUNT(*) FROM Orders WHERE RegisterID = @RegisterID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@RegisterID", registerID);
                        int orderCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (orderCount > 0)
                        {
                            errorMessage = "Cannot delete register with existing orders.";
                            return false;
                        }
                    }

                    string query = "DELETE FROM Registers WHERE RegisterID = @RegisterID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegisterID", registerID);
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

        public static RegisterInfo GetRegisterByID(int registerID, out string errorMessage)
        {
            errorMessage = "";
            RegisterInfo register = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, b.BranchName 
                        FROM Registers r 
                        INNER JOIN Branches b ON r.BranchID = b.BranchID 
                        WHERE r.RegisterID = @RegisterID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegisterID", registerID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                register = new RegisterInfo
                                {
                                    RegisterID = reader.GetInt32(reader.GetOrdinal("RegisterID")),
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    RegisterName = reader.GetString(reader.GetOrdinal("RegisterName")),
                                    RegisterCode = reader.GetString(reader.GetOrdinal("RegisterCode")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName"))
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

            return register;
        }

        public static List<RegisterInfo> GetAllRegisters(out string errorMessage)
        {
            errorMessage = "";
            List<RegisterInfo> registers = new List<RegisterInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, b.BranchName 
                        FROM Registers r 
                        INNER JOIN Branches b ON r.BranchID = b.BranchID 
                        ORDER BY b.BranchName, r.RegisterName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registers.Add(new RegisterInfo
                                {
                                    RegisterID = reader.GetInt32(reader.GetOrdinal("RegisterID")),
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    RegisterName = reader.GetString(reader.GetOrdinal("RegisterName")),
                                    RegisterCode = reader.GetString(reader.GetOrdinal("RegisterCode")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName"))
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

            return registers;
        }

        public static List<RegisterInfo> GetRegistersByBranch(int branchID, out string errorMessage)
        {
            errorMessage = "";
            List<RegisterInfo> registers = new List<RegisterInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, b.BranchName 
                        FROM Registers r 
                        INNER JOIN Branches b ON r.BranchID = b.BranchID 
                        WHERE r.BranchID = @BranchID 
                        ORDER BY r.RegisterName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BranchID", branchID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registers.Add(new RegisterInfo
                                {
                                    RegisterID = reader.GetInt32(reader.GetOrdinal("RegisterID")),
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    RegisterName = reader.GetString(reader.GetOrdinal("RegisterName")),
                                    RegisterCode = reader.GetString(reader.GetOrdinal("RegisterCode")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName"))
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

            return registers;
        }

        public static List<RegisterInfo> GetActiveRegisters(out string errorMessage)
        {
            errorMessage = "";
            List<RegisterInfo> registers = new List<RegisterInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT r.*, b.BranchName 
                        FROM Registers r 
                        INNER JOIN Branches b ON r.BranchID = b.BranchID 
                        WHERE r.IsActive = 1 AND b.IsActive = 1 
                        ORDER BY b.BranchName, r.RegisterName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registers.Add(new RegisterInfo
                                {
                                    RegisterID = reader.GetInt32(reader.GetOrdinal("RegisterID")),
                                    BranchID = reader.GetInt32(reader.GetOrdinal("BranchID")),
                                    RegisterName = reader.GetString(reader.GetOrdinal("RegisterName")),
                                    RegisterCode = reader.GetString(reader.GetOrdinal("RegisterCode")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    BranchName = reader.GetString(reader.GetOrdinal("BranchName"))
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

            return registers;
        }

        public static bool RegisterExists(string registerCode, out string errorMessage)
        {
            errorMessage = "";
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Registers WHERE RegisterCode = @RegisterCode";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegisterCode", registerCode);
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
