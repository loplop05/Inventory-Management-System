using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsCategoryData
    {

        public static bool DoesCategoryExist(string CategoryName)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT TOP 1 1 
                                 FROM Categories 
                                 WHERE CategoryName = @CategoryName";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", CategoryName);


                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        isFound = result != null;
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }


            return isFound;
        }



        public static bool DoesCategoryExist(int CategoryID)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT TOP 1 1 
                                 FROM Categories 
                                 WHERE CategoryID = @CategoryID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);


                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        isFound = result != null;
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }


            return isFound;
        }



        public static bool GetCategoryByID(int CategoryID, ref string CategoryName)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT CategoryName 
                                 FROM Categories 
                                 WHERE CategoryID = @CategoryID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);


                    try
                    {
                        connection.Open();


                        SqlDataReader reader = command.ExecuteReader();


                        if (reader.Read())
                        {
                            isFound = true;

                            CategoryName = reader["CategoryName"].ToString();
                        }


                        reader.Close();
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }


            return isFound;
        }



        public static int AddNewCategory(string CategoryName)
        {
            int CategoryID = -1;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string query = @"
                INSERT INTO Categories(CategoryName)
                VALUES(@CategoryName);

                SELECT SCOPE_IDENTITY();";


                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@CategoryName", CategoryName);


                    try
                    {
                        connection.Open();


                        object result = command.ExecuteScalar();


                        if (result != null)
                        {
                            CategoryID = Convert.ToInt32(result);
                        }

                    }
                    catch
                    {
                        CategoryID = -1;
                    }
                }
            }


            return CategoryID;
        }



        public static bool UpdateCategory(int CategoryID, string CategoryName)
        {
            int RowsAffected = 0;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string query = @"
                UPDATE Categories
                SET CategoryName = @CategoryName
                WHERE CategoryID = @CategoryID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@CategoryID", CategoryID);

                    command.Parameters.AddWithValue("@CategoryName", CategoryName);


                    try
                    {
                        connection.Open();

                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }


            return RowsAffected > 0;
        }



        public static bool DeleteCategory(int CategoryID)
        {
            int RowsAffected = 0;


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string query = @"
                DELETE FROM Categories
                WHERE CategoryID = @CategoryID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@CategoryID", CategoryID);


                    try
                    {
                        connection.Open();

                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }


            return RowsAffected > 0;
        }



        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string query = @"
                SELECT CategoryID, CategoryName
                FROM Categories";


                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        connection.Open();


                        SqlDataReader reader = command.ExecuteReader();


                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }


                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsCategoryData.GetAllCategories", ex);
                    }
                }
            }


            return dt;
        }

    }
}