using System;
using System.Data;
using System.Data.SqlClient;


namespace InventoryDataAccessLayer
{
    public class clsSupplierData
    {


        public static int AddNewSupplier(
            string SupplierName,
            string Phone,
            string Email)
        {

            int SupplierID = -1;



            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString))
            {


                string query = @"
                INSERT INTO Suppliers
                (
                    SupplierName,
                    Phone,
                    Email
                )

                VALUES
                (
                    @SupplierName,
                    @Phone,
                    @Email
                );

                SELECT SCOPE_IDENTITY();";



                SqlCommand command =
                    new SqlCommand(query, connection);



                command.Parameters.AddWithValue(
                    "@SupplierName",
                    SupplierName);



                command.Parameters.AddWithValue(
                    "@Phone",
                    Phone);



                command.Parameters.AddWithValue(
                    "@Email",
                    Email);




                connection.Open();



                object result =
                    command.ExecuteScalar();



                if (result != null)
                {
                    SupplierID =
                        Convert.ToInt32(result);
                }


            }



            return SupplierID;


        }







        public static bool UpdateSupplier(
            int SupplierID,
            string SupplierName,
            string Phone,
            string Email)
        {


            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString))
            {


                string query = @"
                UPDATE Suppliers

                SET
                    SupplierName=@SupplierName,
                    Phone=@Phone,
                    Email=@Email

                WHERE SupplierID=@SupplierID";




                SqlCommand command =
                    new SqlCommand(query, connection);



                command.Parameters.AddWithValue(
                    "@SupplierID",
                    SupplierID);



                command.Parameters.AddWithValue(
                    "@SupplierName",
                    SupplierName);



                command.Parameters.AddWithValue(
                    "@Phone",
                    Phone);



                command.Parameters.AddWithValue(
                    "@Email",
                    Email);




                connection.Open();



                return command.ExecuteNonQuery() > 0;


            }


        }







        public static bool DeleteSupplier(int SupplierID)
        {


            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString))
            {


                string query =
                "DELETE FROM Suppliers WHERE SupplierID=@SupplierID";



                SqlCommand command =
                    new SqlCommand(query, connection);



                command.Parameters.AddWithValue(
                    "@SupplierID",
                    SupplierID);



                connection.Open();



                return command.ExecuteNonQuery() > 0;


            }



        }








        public static bool GetSupplierByID(
            int SupplierID,
            ref string SupplierName,
            ref string Phone,
            ref string Email)
        {


            bool IsFound = false;



            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString))
            {



                string query = @"
                SELECT 
                    SupplierName,
                    Phone,
                    Email

                FROM Suppliers

                WHERE SupplierID=@SupplierID";




                SqlCommand command =
                    new SqlCommand(query, connection);



                command.Parameters.AddWithValue(
                    "@SupplierID",
                    SupplierID);



                connection.Open();



                SqlDataReader reader =
                    command.ExecuteReader();



                if (reader.Read())
                {


                    IsFound = true;



                    SupplierName =
                        reader["SupplierName"].ToString();



                    Phone =
                        reader["Phone"].ToString();



                    Email =
                        reader["Email"].ToString();


                }



                reader.Close();


            }



            return IsFound;


        }








        public static DataTable GetAllSuppliers()
        {


            DataTable dt = new DataTable();



            using (SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString))
            {


                string query =
                "SELECT * FROM Suppliers";



                SqlCommand command =
                    new SqlCommand(query, connection);



                connection.Open();



                SqlDataReader reader =
                    command.ExecuteReader();



                if (reader.HasRows)
                    dt.Load(reader);



            }




            return dt;


        }




    }
}