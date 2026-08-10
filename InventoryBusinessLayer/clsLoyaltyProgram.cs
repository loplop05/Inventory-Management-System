using System;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsLoyaltyProgram
    {
        public const int PointsPerDollar = 1;

        public static bool EarnPointsForOrder(int customerID, int orderID, decimal orderTotal, out string errorMessage)
        {
            errorMessage = "";
            
            if (customerID <= 0)
            {
                errorMessage = "Invalid customer ID";
                return false;
            }

            if (orderID <= 0)
            {
                errorMessage = "Invalid order ID";
                return false;
            }

            if (orderTotal <= 0)
            {
                // Zero or negative orders earn 0 points - not an error, just no action
                return true;
            }

            int pointsEarned = (int)Math.Floor(orderTotal * PointsPerDollar);
            
            if (pointsEarned <= 0)
            {
                return true;
            }

            try
            {
                // Get current points
                int currentPoints = clsCustomer.GetLoyaltyPoints(customerID);
                int newPoints = currentPoints + pointsEarned;
                
                // Update customer points with history
                bool updated = clsCustomer.UpdateCustomerPoints(customerID, newPoints, "Purchase", "Purchase", orderID, out errorMessage);
                
                if (!updated)
                {
                    return false;
                }

                // Recalculate tier
                RecalculateTier(customerID);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Error earning points: " + ex.Message;
                return false;
            }
        }

        public static void RecalculateTier(int customerID)
        {
            try
            {
                int currentPoints = clsCustomer.GetLoyaltyPoints(customerID);
                string currentTier = clsCustomer.GetLoyaltyTier(customerID);
                string newTier = CalculateTier(currentPoints);

                if (currentTier != newTier)
                {
                    clsCustomer.UpdateLoyaltyTier(customerID, newTier);
                    
                    // Log tier change
                    clsAuditLog.LogAction("TierChanged", 
                        $"CustomerID={customerID}, OldTier={currentTier}, NewTier={newTier}, Points={currentPoints}", 
                        "Loyalty");
                }
            }
            catch
            {
                // Don't throw - tier recalculation should never block other operations
            }
        }

        private static string CalculateTier(int points)
        {
            if (points >= 2000)
                return "Gold";
            else if (points >= 500)
                return "Silver";
            else
                return "Bronze";
        }

        public static decimal GetTierDiscount(string tier)
        {
            switch (tier)
            {
                case "Gold":
                    return 0.05m; // 5%
                case "Silver":
                    return 0.03m; // 3%
                default:
                    return 0m; // Bronze - 0%
            }
        }
    }
}
