using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsCategoryData
    {

        public static int AddNewCategory(string CategoryName)
        {
            int CategoryID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                INSERT INTO Categories(CategoryName)
                VALUES(@CategoryName);

                SELECT SCOPE_IDENTITY();";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);

                    try
                    {
                        conn.Open();

                        object result = cmd.ExecuteScalar();

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
            bool IsUpdated = false;


            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                UPDATE Categories
                SET CategoryName = @CategoryName
                WHERE CategoryID = @CategoryID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);


                    try
                    {
                        conn.Open();

                        IsUpdated = cmd.ExecuteNonQuery() > 0;
                    }
                    catch
                    {
                        IsUpdated = false;
                    }
                }
            }


            return IsUpdated;
        }



        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();


            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                SELECT CategoryID, CategoryName
                FROM Categories";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }


            return dt;
        }



        public static bool DeleteCategory(int CategoryID)
        {
            bool IsDeleted = false;


            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                DELETE FROM Categories
                WHERE CategoryID = @CategoryID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);


                    try
                    {
                        conn.Open();

                        IsDeleted = cmd.ExecuteNonQuery() > 0;
                    }
                    catch
                    {
                        IsDeleted = false;
                    }
                }
            }


            return IsDeleted;
        }
    }
}