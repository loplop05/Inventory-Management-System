using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsShift
    {
        public static int OpenShift(int userID, decimal startingCash, out string errorMessage)
        {
            errorMessage = "";

            // Validate starting cash
            if (startingCash < 0)
            {
                errorMessage = "Starting cash cannot be negative.";
                return -1;
            }

            // Check if user already has an open shift
            DataTable existingShift = clsShiftData.GetOpenShiftForUser(userID);
            if (existingShift != null && existingShift.Rows.Count > 0)
            {
                errorMessage = "You already have an open shift. Close it before opening a new one.";
                return -1;
            }

            // Open the shift
            int shiftID = clsShiftData.OpenShift(userID, startingCash, out errorMessage);

            if (shiftID > 0)
            {
                // Shift opened successfully - logging handled by presentation layer
            }

            return shiftID;
        }

        public static bool CloseShift(int shiftID, decimal countedCash, string notes, out string errorMessage)
        {
            errorMessage = "";

            // Validate counted cash
            if (countedCash < 0)
            {
                errorMessage = "Counted cash cannot be negative.";
                return false;
            }

            // Close the shift
            bool success = clsShiftData.CloseShift(shiftID, countedCash, notes, out errorMessage);

            if (success)
            {
                // Shift closed successfully - logging handled by presentation layer
            }

            return success;
        }

        public static DataTable GetOpenShiftForUser(int userID)
        {
            return clsShiftData.GetOpenShiftForUser(userID);
        }

        public static decimal GetCashSalesTotal(int shiftID)
        {
            return clsShiftData.GetCashSalesTotal(shiftID);
        }

        public static decimal GetStartingCash(int shiftID)
        {
            return clsShiftData.GetStartingCash(shiftID);
        }

        public static decimal GetExpectedCash(int shiftID)
        {
            decimal startingCash = clsShiftData.GetStartingCash(shiftID);
            decimal cashSales = clsShiftData.GetCashSalesTotal(shiftID);
            return startingCash + cashSales;
        }

        public static DataTable GetShiftHistory(DateTime? from, DateTime? to, int? userID)
        {
            return clsShiftData.GetShiftHistory(from, to, userID);
        }

        public static bool HasOpenShift(int userID)
        {
            DataTable shift = clsShiftData.GetOpenShiftForUser(userID);
            return shift != null && shift.Rows.Count > 0;
        }
    }
}
