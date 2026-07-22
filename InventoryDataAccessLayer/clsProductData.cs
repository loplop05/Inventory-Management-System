using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsProductData
    {
        
        public static int AddNewProduct(string ProductName, int CategoryID, int SupplierID, decimal Price, int Quantity, string Barcode, string ImagePath, DateTime CreatedDate)
        {
            int productID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
                                 VALUES (@ProductName, @CategoryID, @SupplierID, @Price, @Quantity, @Barcode, @ImagePath, @CreatedDate);
                                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? DBNull.Value : (object)ImagePath);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            productID = id;
                        }
                    }
                    catch { productID = -1; }
                }
            }
            return productID;
        }

       
        public static bool UpdateProduct(int ProductID, string ProductName, int CategoryID, int SupplierID, decimal Price, int Quantity, string Barcode, string ImagePath)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"UPDATE Products 
                                 SET ProductName = @ProductName, CategoryID = @CategoryID, SupplierID = @SupplierID, 
                                     Price = @Price, Quantity = @Quantity, Barcode = @Barcode, ImagePath = @ImagePath
                                 WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? DBNull.Value : (object)ImagePath);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch { return false; }
                }
            }
            return (rowsAffected > 0);
        }

      
        public static bool DeleteProduct(int ProductID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch { return false; }
                }
            }
            return (rowsAffected > 0);
        }

      
        public static DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT * FROM Products";

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
                    catch { }
                }
            }
            return dt;
        }

        
        public static bool DoesProductExist(int ProductID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); } catch { }
                }
            }
            return isFound;
        }

       
        public static bool DoesProductExistByName(string ProductName)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Products WHERE ProductName = @ProductName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); } catch { }
                }
            }
            return isFound;
        }

        
        public static bool DoesProductExistByBarcode(string Barcode)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Products WHERE Barcode = @Barcode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); } catch { }
                }
            }
            return isFound;
        }

       
        public static bool DoesProductExistByNameExcept(string ProductName, int ProductID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Products WHERE ProductName = @ProductName AND ProductID <> @ProductID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); } catch { }
                }
            }
            return isFound;
        }

      
        public static bool DoesProductExistByBarcodeExcept(string Barcode, int ProductID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT TOP 1 1 FROM Products WHERE Barcode = @Barcode AND ProductID <> @ProductID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    try { connection.Open(); isFound = (command.ExecuteScalar() != null); } catch { }
                }
            }
            return isFound;
        }
    }
}
