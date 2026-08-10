using System;

namespace InventoryBusinessLayer
{
    public static class clsLoyaltyProgram
    {
        private const int PointsPerDollar = 1;

        public static bool EarnPointsForOrder(int customerID, int orderID, decimal orderTotal, out string errorMessage)
        {
            errorMessage = "";
            
            if (customerID <= 0)
            {
                errorMessage = "Invalid customer ID.";
                return false;
            }

            if (orderTotal <= 0)
            {
                errorMessage = "Order total must be positive to earn points.";
                return true; // Not an error, just no points earned
            }

            // Update customer loyalty with purchase amount - this will add points and total spent
            bool updated = clsCustomer.UpdateCustomerLoyalty(customerID, orderTotal, out errorMessage);
            
            if (!updated)
                return false;
            
            return true;
        }

        public static string CalculateTier(int points)
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
                    return 0.05m; // 5% discount
                case "Silver":
                    return 0.02m; // 2% discount
                default:
                    return 0m; // No discount for Bronze
            }
        }
    }
}
