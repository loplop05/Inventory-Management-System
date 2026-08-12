using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public class clsSegmentData
    {
        public static DataTable GetSegmentSummary(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT 
                            SegmentLabel,
                            COUNT(*) AS CustomerCount,
                            AVG(MonetaryScore) AS AvgMonetary,
                            AVG(FrequencyScore) AS AvgFrequency,
                            AVG(RecencyScore) AS AvgRecency
                        FROM CustomerSegments
                        GROUP BY SegmentLabel
                        ORDER BY CustomerCount DESC";

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

        public static DataTable GetSegmentsForCustomer(int customerId, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT 
                            cs.SegmentLabel,
                            cs.RecencyScore,
                            cs.FrequencyScore,
                            cs.MonetaryScore,
                            cs.ClusterID,
                            cs.GeneratedDate
                        FROM CustomerSegments cs
                        WHERE cs.CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerId);

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

        public static DataTable GetAllSegments(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"
                        SELECT 
                            cs.CustomerID,
                            c.CustomerName,
                            cs.SegmentLabel,
                            cs.RecencyScore,
                            cs.FrequencyScore,
                            cs.MonetaryScore,
                            cs.ClusterID,
                            cs.GeneratedDate
                        FROM CustomerSegments cs
                        INNER JOIN Customers c ON c.CustomerID = cs.CustomerID
                        ORDER BY cs.SegmentLabel, cs.MonetaryScore DESC";

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
