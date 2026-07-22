using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDataAccessLayer
{
    public class clsCategoryData
    {


        public static bool DoesCategoryExist(string CategoryName)
        {

            // select Exist = 1 where CategoryName = CategoryName  




            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = @"SELECT TOP 1 1 FROM Categories WHERE CategoryName = @CategoryName";

            SqlCommand command = new SqlCommand(query,connection);



            bool isFound = false;



            command.Parameters.AddWithValue(@"CategoryName", CategoryName);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
        
    

            return isFound;
}

        public static bool DoesCategoryExist(int CategoryID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT TOP 1 1 FROM Categories WHERE CategoryID = @CategoryID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }


        public static bool GetCategoryByID(int CategoryID ,ref string CaregoryName)
        {

            bool isFound = false;



            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = @"SELECT * FROM CATEGORIES WHERE CategoryID = @CategoryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            try
            {
                connection.Open();



                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    CategoryID = (int)reader["CategoryID"];

                    CaregoryName = (string)reader["CategoryName"];

                }
                else
                {
                    isFound = false;

                }
                reader.Close();
            }


            catch (Exception ex)
            {
                isFound = false;
            }

            finally
            {
                connection.Close();
            }

            return isFound;

        }
       


       public static int AddNewCategory(int CategoryID,string CategoryName)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"insert into Categories (CategoryID,CategoryName)
                            values (@CategoryID,@CategoryName) 
                             select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue(@"CategoryID", CategoryID);
            command.Parameters.AddWithValue(@"CategoryName", CategoryName);


            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CategoryID = insertedID;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }



            return CategoryID;

        }

       public static bool UpdateCategory(int CategoryID,string CategoryName)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE categories
                            set CategoryName = @CategoryName
                            where CategoryID = CategoryID";


            SqlCommand command = new SqlCommand(query, connection);


            int RowsAffected = 0;

            command.Parameters.AddWithValue(@"CategoryID", CategoryID);
            command.Parameters.AddWithValue(@"CategoryName", CategoryName);



            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }


       public static bool DeleteCategory(int CategoryID)
        {

            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = @"Delete Categories where CategoryID = @CategoryID";


            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue(@"CategoryID", CategoryID);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);


        }

        
       public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from Categories";

            SqlCommand command = new SqlCommand(query, connection);


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
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }



        


















    }
}
