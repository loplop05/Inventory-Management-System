using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsShiftData
    {
        public static int OpenShift(int userID, decimal startingCash, out string errorMessage)
        {
            errorMessage = "";
            int shiftID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Shifts (UserID, StartingCash, Status)
                        VALUES (@UserID, @StartingCash, 'Open');
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@StartingCash", startingCash);

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newShiftID))
                        {
                            shiftID = newShiftID;
                        }
                    }

                    return shiftID;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsShiftData.OpenShift", ex);
                    return -1;
                }
            }
        }

        public static bool CloseShift(int shiftID, decimal countedCash, string notes, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Get expected cash first
                    decimal expectedCash = GetCashSalesTotal(shiftID);
                    decimal startingCash = GetStartingCash(shiftID);
                    expectedCash = startingCash + expectedCash;
                    decimal cashDifference = countedCash - expectedCash;

                    string query = @"
                        UPDATE Shifts
                        SET ClosedAt = GETDATE(),
                            CountedCash = @CountedCash,
                            ExpectedCash = @ExpectedCash,
                            CashDifference = @CashDifference,
                            Status = 'Closed',
                            Notes = @Notes
                        WHERE ShiftID = @ShiftID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShiftID", shiftID);
                        command.Parameters.AddWithValue("@CountedCash", countedCash);
                        command.Parameters.AddWithValue("@ExpectedCash", expectedCash);
                        command.Parameters.AddWithValue("@CashDifference", cashDifference);
                        command.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsShiftData.CloseShift", ex);
                    return false;
                }
            }
        }

        public static DataTable GetOpenShiftForUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT ShiftID, UserID, OpenedAt, StartingCash, Status
                        FROM Shifts
                        WHERE UserID = @UserID AND Status = 'Open'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsShiftData.GetOpenShiftForUser", ex);
                    return null;
                }
            }
        }

        public static decimal GetCashSalesTotal(int shiftID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT ISNULL(SUM(TotalAmount), 0)
                        FROM Orders
                        WHERE ShiftID = @ShiftID AND PaymentMethod = 'Cash'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShiftID", shiftID);

                        object result = command.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal total))
                        {
                            return total;
                        }
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsShiftData.GetCashSalesTotal", ex);
                    return 0;
                }
            }
        }

        public static decimal GetStartingCash(int shiftID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT StartingCash FROM Shifts WHERE ShiftID = @ShiftID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShiftID", shiftID);

                        object result = command.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal startingCash))
                        {
                            return startingCash;
                        }
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsShiftData.GetStartingCash", ex);
                    return 0;
                }
            }
        }

        public static DataTable GetShiftHistory(DateTime? from, DateTime? to, int? userID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT s.ShiftID, s.UserID, u.Username, s.OpenedAt, s.ClosedAt, 
                               s.StartingCash, s.ExpectedCash, s.CountedCash, s.CashDifference, s.Status, s.Notes
                        FROM Shifts s
                        LEFT JOIN Users u ON s.UserID = u.UserID
                        WHERE 1=1";

                    if (from.HasValue)
                    {
                        query += " AND s.OpenedAt >= @FromDate";
                    }
                    if (to.HasValue)
                    {
                        query += " AND s.OpenedAt <= @ToDate";
                    }
                    if (userID.HasValue)
                    {
                        query += " AND s.UserID = @UserID";
                    }

                    query += " ORDER BY s.OpenedAt DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (from.HasValue)
                            command.Parameters.AddWithValue("@FromDate", from.Value);
                        if (to.HasValue)
                            command.Parameters.AddWithValue("@ToDate", to.Value);
                        if (userID.HasValue)
                            command.Parameters.AddWithValue("@UserID", userID.Value);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsShiftData.GetShiftHistory", ex);
                    return null;
                }
            }
        }
    }
}
