using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsLoyalty
    {
        // Loyalty tier thresholds
        private const decimal BronzeThreshold = 0;
        private const decimal SilverThreshold = 1000;
        private const decimal GoldThreshold = 5000;
        private const decimal PlatinumThreshold = 10000;

        // Point earning rate: 1 point per $1 spent
        private const decimal PointsPerDollar = 1;

        // Point redemption rate: 100 points = $1 discount
        private const decimal DiscountPer100Points = 1;

        public static string CalculateTier(decimal totalSpent)
        {
            if (totalSpent >= PlatinumThreshold)
                return "Platinum";
            else if (totalSpent >= GoldThreshold)
                return "Gold";
            else if (totalSpent >= SilverThreshold)
                return "Silver";
            else
                return "Bronze";
        }

        public static int CalculatePointsEarned(decimal amountSpent)
        {
            return (int)(amountSpent * PointsPerDollar);
        }

        public static decimal CalculateDiscountFromPoints(int points)
        {
            return (points / 100) * DiscountPer100Points;
        }

        public static int CalculatePointsFromDiscount(decimal discountAmount)
        {
            return (int)((discountAmount / DiscountPer100Points) * 100);
        }

        public static bool UpdateCustomerLoyalty(int customerID, decimal amountSpent, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Get current customer data
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return false;
                }

                DataRow customerRow = customer.Rows[0];

                // Calculate new values
                int currentPoints = customerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customerRow["LoyaltyPoints"]) : 0;
                decimal currentTotalSpent = customerRow["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(customerRow["TotalSpent"]) : 0;

                int pointsEarned = CalculatePointsEarned(amountSpent);
                int newPoints = currentPoints + pointsEarned;
                decimal newTotalSpent = currentTotalSpent + amountSpent;
                string newTier = CalculateTier(newTotalSpent);

                // Update customer
                return clsCustomer.UpdateCustomerLoyalty(customerID, newPoints, newTotalSpent, newTier, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool RedeemPoints(int customerID, int pointsToRedeem, out decimal discountAmount, out string errorMessage)
        {
            discountAmount = 0;
            errorMessage = "";

            try
            {
                // Get current customer data
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return false;
                }

                DataRow customerRow = customer.Rows[0];
                int currentPoints = customerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customerRow["LoyaltyPoints"]) : 0;

                // Validate points
                if (pointsToRedeem > currentPoints)
                {
                    errorMessage = "Insufficient loyalty points.";
                    return false;
                }

                if (pointsToRedeem < 100)
                {
                    errorMessage = "Minimum redemption is 100 points.";
                    return false;
                }

                // Calculate discount
                discountAmount = CalculateDiscountFromPoints(pointsToRedeem);
                int newPoints = currentPoints - pointsToRedeem;

                // Update customer points
                return clsCustomer.UpdateCustomerPoints(customerID, newPoints, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static DataTable GetCustomerLoyaltyInfo(int customerID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return null;
                }

                // Add calculated columns
                customer.Columns.Add("PointsEarned", typeof(int));
                customer.Columns.Add("DiscountAvailable", typeof(decimal));
                customer.Columns.Add("NextTier", typeof(string));
                customer.Columns.Add("AmountToNextTier", typeof(decimal));

                DataRow row = customer.Rows[0];
                int currentPoints = row["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(row["LoyaltyPoints"]) : 0;
                decimal totalSpent = row["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(row["TotalSpent"]) : 0;
                string currentTier = row["Tier"] != DBNull.Value ? row["Tier"].ToString() : "Bronze";

                row["PointsEarned"] = currentPoints;
                row["DiscountAvailable"] = CalculateDiscountFromPoints(currentPoints);

                // Calculate next tier
                string nextTier = "";
                decimal amountToNextTier = 0;

                if (currentTier == "Bronze")
                {
                    nextTier = "Silver";
                    amountToNextTier = SilverThreshold - totalSpent;
                }
                else if (currentTier == "Silver")
                {
                    nextTier = "Gold";
                    amountToNextTier = GoldThreshold - totalSpent;
                }
                else if (currentTier == "Gold")
                {
                    nextTier = "Platinum";
                    amountToNextTier = PlatinumThreshold - totalSpent;
                }
                else
                {
                    nextTier = "Max";
                    amountToNextTier = 0;
                }

                row["NextTier"] = nextTier;
                row["AmountToNextTier"] = amountToNextTier > 0 ? amountToNextTier : 0;

                return customer;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public static bool DeductLoyaltyPointsOnVoid(int customerID, int pointsToDeduct, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Get current customer data
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return false;
                }

                DataRow customerRow = customer.Rows[0];
                int currentPoints = customerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customerRow["LoyaltyPoints"]) : 0;
                decimal currentTotalSpent = customerRow["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(customerRow["TotalSpent"]) : 0;

                // Calculate new values
                int newPoints = Math.Max(0, currentPoints - pointsToDeduct);
                decimal newTotalSpent = Math.Max(0, currentTotalSpent - (pointsToDeduct / PointsPerDollar));
                string newTier = CalculateTier(newTotalSpent);

                // Update customer
                return clsCustomer.UpdateCustomerLoyalty(customerID, newPoints, newTotalSpent, newTier, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static DataTable GetCustomerCountByTier(out string errorMessage)
        {
            return clsCustomer.GetCustomerCountByTier(out errorMessage);
        }

        public static DataTable GetTopLoyaltyMembers(int topN, out string errorMessage)
        {
            return clsCustomer.GetTopLoyaltyMembers(topN, out errorMessage);
        }
    }
}
