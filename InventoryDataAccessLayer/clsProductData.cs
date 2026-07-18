using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsProductData
    {
        public static int AddNewProduct(
            string ProductName,
            int CategoryID,
            int SupplierID,
            decimal Price,
            int Quantity,
            string Barcode,
            string ImagePath,
            DateTime CreatedDate)
        {
            int ProductID = -1;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Products
                                (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)

                                VALUES

                                (@ProductName,@CategoryID,@SupplierID,@Price,@Quantity,@Barcode,@ImagePath,@CreatedDate);

                                SELECT SCOPE_IDENTITY();";


                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@ProductName", ProductName);
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.AddWithValue("@SupplierID", SupplierID);
                command.Parameters.AddWithValue("@Price", Price);
                command.Parameters.AddWithValue("@Quantity", Quantity);
                command.Parameters.AddWithValue("@Barcode", Barcode);
                command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null)
                    ProductID = Convert.ToInt32(result);
            }


            return ProductID;
        }



        public static bool UpdateProduct(
            int ProductID,
            string ProductName,
            int CategoryID,
            int SupplierID,
            decimal Price,
            int Quantity,
            string Barcode,
            string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Products

                                 SET ProductName=@ProductName,
                                     CategoryID=@CategoryID,
                                     SupplierID=@SupplierID,
                                     Price=@Price,
                                     Quantity=@Quantity,
                                     Barcode=@Barcode,
                                     ImagePath=@ImagePath

                                 WHERE ProductID=@ProductID";


                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@ProductID", ProductID);
                command.Parameters.AddWithValue("@ProductName", ProductName);
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.AddWithValue("@SupplierID", SupplierID);
                command.Parameters.AddWithValue("@Price", Price);
                command.Parameters.AddWithValue("@Quantity", Quantity);
                command.Parameters.AddWithValue("@Barcode", Barcode);
                command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);


                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }



        public static bool DeleteProduct(int ProductID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID=@ProductID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ProductID", ProductID);

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }



        public static DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                SELECT 
                    Products.ProductID,
                    Products.ProductName,
                    Categories.CategoryName,
                    Suppliers.SupplierName,
                    Products.Price,
                    Products.Quantity,
                    Products.Barcode,
                    Products.ImagePath,
                    Products.CreatedDate

                FROM Products

                INNER JOIN Categories
                ON Products.CategoryID = Categories.CategoryID

                INNER JOIN Suppliers
                ON Products.SupplierID = Suppliers.SupplierID";


                SqlCommand command = new SqlCommand(query, connection);


                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                    dt.Load(reader);
            }


            return dt;
        }
    }
}