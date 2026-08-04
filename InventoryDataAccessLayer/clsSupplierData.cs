using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsSupplierData
    {
        public static int AddNewSupplier(string SupplierName, string Phone, string Email)
        {
            int SupplierID;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                using (SqlCommand getMaxCommand = new SqlCommand(
                    "SELECT ISNULL(MAX(SupplierID), 0) + 1 FROM Suppliers", connection))
                {
                    SupplierID = (int)getMaxCommand.ExecuteScalar();
                }

                string query = @"INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email)
                         VALUES (@SupplierID, @SupplierName, @Phone, @Email);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    command.Parameters.AddWithValue("@SupplierName", SupplierName);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(Phone) ? DBNull.Value : (object)Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? DBNull.Value : (object)Email);

                    command.ExecuteNonQuery();
                }
            }

            return SupplierID;
        }

        public static bool UpdateSupplier(int SupplierID, string SupplierName, string Phone, string Email)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"UPDATE Suppliers 
                                 SET SupplierName = @SupplierName, Phone = @Phone, Email = @Email
                                 WHERE SupplierID = @SupplierID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    command.Parameters.AddWithValue("@SupplierName", SupplierName);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(Phone) ? DBNull.Value : (object)Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? DBNull.Value : (object)Email);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.UpdateSupplier", ex);
                        return false;
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool DeleteSupplier(int SupplierID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.DeleteSupplier", ex);
                        return false;
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool GetSupplierByID(int SupplierID, ref string SupplierName, ref string Phone, ref string Email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT SupplierName, Phone, Email FROM Suppliers WHERE SupplierID = @SupplierID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                SupplierName = reader["SupplierName"].ToString();
                                Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.GetSupplierByID", ex);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static DataTable GetAllSuppliers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT SupplierID, SupplierName, Phone, Email FROM Suppliers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.GetAllSuppliers", ex);
                    }
                }
            }
            return dt;
        }

        public static bool DoesSupplierExist(int SupplierID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Suppliers WHERE SupplierID = @SupplierID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.DoesSupplierExist", ex);
                    }
                }
            }
            return isFound;
        }

        public static bool DoesSupplierExist(string SupplierName)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Suppliers WHERE SupplierName = @SupplierName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierName", SupplierName);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.DoesSupplierExist", ex);
                    }
                }
            }
            return isFound;
        }

        public static bool DoesSupplierExistExcept(string SupplierName, int SupplierID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Suppliers WHERE SupplierName = @SupplierName AND SupplierID <> @SupplierID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierName", SupplierName);
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsSupplierData.DoesSupplierExist", ex);
                    }
                }
            }
            return isFound;
        }
    }
}
