using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsAssociationData
    {
        public static DataTable GetSuggestionsForProduct(int productId, int topN, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT TOP (@topN) 
                            a.ConsequentProductID, 
                            p.ProductName, 
                            a.Confidence, 
                            a.Lift
                        FROM ProductAssociations a
                        INNER JOIN Products p ON p.ProductID = a.ConsequentProductID
                        WHERE a.AntecedentProductID = @ProductID
                        ORDER BY a.Lift DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@topN", topN);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public static DataTable GetAllAssociations(int topN, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT TOP (@topN) 
                            a.AntecedentProductID,
                            p1.ProductName AS AntecedentProduct,
                            a.ConsequentProductID,
                            p2.ProductName AS ConsequentProduct,
                            a.Support,
                            a.Confidence,
                            a.Lift
                        FROM ProductAssociations a
                        INNER JOIN Products p1 ON p1.ProductID = a.AntecedentProductID
                        INNER JOIN Products p2 ON p2.ProductID = a.ConsequentProductID
                        ORDER BY a.Lift DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@topN", topN);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}
