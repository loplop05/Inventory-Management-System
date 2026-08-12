using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsForecastData
    {
        public static DataTable GetForecastsForProduct(int productId, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT * FROM ProductForecasts 
                        WHERE ProductID = @ProductID 
                        ORDER BY ForecastDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);

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

        public static DataTable GetForecastSummary(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName,
                            pf.PredictedQty,
                            pf.LowerBound,
                            pf.UpperBound,
                            pf.ForecastDate,
                            pf.ModelType,
                            pf.GeneratedDate
                        FROM ProductForecasts pf
                        INNER JOIN Products p ON pf.ProductID = p.ProductID
                        WHERE pf.ForecastDate >= CAST(GETDATE() AS DATE)
                        ORDER BY p.ProductName, pf.ForecastDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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

        public static DataTable GetNext7DayForecastSummary(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName,
                            SUM(pf.PredictedQty) AS Next7DayQty,
                            MIN(pf.LowerBound) AS MinLowerBound,
                            MAX(pf.UpperBound) AS MaxUpperBound,
                            MAX(pf.GeneratedDate) AS GeneratedDate
                        FROM ProductForecasts pf
                        INNER JOIN Products p ON pf.ProductID = p.ProductID
                        WHERE pf.ForecastDate >= CAST(GETDATE() AS DATE) 
                            AND pf.ForecastDate < DATEADD(day, 7, CAST(GETDATE() AS DATE))
                        GROUP BY p.ProductID, p.ProductName
                        ORDER BY p.ProductName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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
