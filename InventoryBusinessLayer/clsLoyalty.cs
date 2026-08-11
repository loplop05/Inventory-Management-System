using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    /// <summary>
    /// DEPRECATED: This class is legacy code and should not be used.
    /// Use clsLoyaltyProgram.cs instead for all loyalty operations.
    /// clsLoyaltyProgram.cs provides a simpler, cleaner implementation based on LoyaltyPoints (not TotalSpent)
    /// and only includes Bronze, Silver, Gold tiers (no Platinum).
    /// 
    /// This class is kept for backward compatibility only and will be removed in a future version.
    /// </summary>
    [Obsolete("Use clsLoyaltyProgram instead. This class is deprecated.", false)]
    public static class clsLoyalty
    {
        // Loyalty tier thresholds
        private const decimal BronzeThreshold = 0;
        private const decimal SilverThreshold = 1000;
        private const decimal GoldThreshold = 5000;
        private const decimal PlatinumThreshold = 10000;

        // Point earning rate: 1 point per $1 spent (base rate)
        private const decimal PointsPerDollar = 1;

        // Tier-based point multipliers
        private const decimal BronzeMultiplier = 1.0m;   // 1x points
        private const decimal SilverMultiplier = 1.25m;  // 1.25x points
        private const decimal GoldMultiplier = 1.5m;     // 1.5x points
        private const decimal PlatinumMultiplier = 2.0m; // 2x points

        // Point redemption rate: 100 points = $1 discount (base rate)
        private const decimal DiscountPer100Points = 1;

        // Tier-specific redemption rates (points needed for $1 discount)
        private const int BronzeRedemptionRate = 100;   // 100 points = $1
        private const int SilverRedemptionRate = 95;    // 95 points = $1 (5% better)
        private const int GoldRedemptionRate = 90;      // 90 points = $1 (10% better)
        private const int PlatinumRedemptionRate = 85;   // 85 points = $1 (15% better)

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

        public static int CalculatePointsEarned(decimal amountSpent, string tier)
        {
            decimal multiplier = GetTierMultiplier(tier);
            return (int)(amountSpent * PointsPerDollar * multiplier);
        }

        public static int CalculatePointsEarned(decimal amountSpent)
        {
            return (int)(amountSpent * PointsPerDollar);
        }

        private static decimal GetTierMultiplier(string tier)
        {
            switch (tier)
            {
                case "Platinum": return PlatinumMultiplier;
                case "Gold": return GoldMultiplier;
                case "Silver": return SilverMultiplier;
                default: return BronzeMultiplier;
            }
        }

        private static int GetTierRedemptionRate(string tier)
        {
            switch (tier)
            {
                case "Platinum": return PlatinumRedemptionRate;
                case "Gold": return GoldRedemptionRate;
                case "Silver": return SilverRedemptionRate;
                default: return BronzeRedemptionRate;
            }
        }

        public static decimal CalculateDiscountFromPoints(int points, string tier)
        {
            int redemptionRate = GetTierRedemptionRate(tier);
            return (points / redemptionRate) * 1m; // $1 per redemptionRate points
        }

        public static decimal CalculateDiscountFromPoints(int points)
        {
            return (points / 100) * DiscountPer100Points;
        }

        public static int CalculatePointsFromDiscount(decimal discountAmount, string tier)
        {
            int redemptionRate = GetTierRedemptionRate(tier);
            return (int)((discountAmount / 1m) * redemptionRate);
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
                string currentTier = customerRow["Tier"] != DBNull.Value ? customerRow["Tier"].ToString() : "Bronze";

                // Calculate points with tier multiplier
                int pointsEarned = CalculatePointsEarned(amountSpent, currentTier);
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
                string currentTier = customerRow["Tier"] != DBNull.Value ? customerRow["Tier"].ToString() : "Bronze";

                // Validate points
                if (pointsToRedeem > currentPoints)
                {
                    errorMessage = "Insufficient loyalty points.";
                    return false;
                }

                int redemptionRate = GetTierRedemptionRate(currentTier);
                if (pointsToRedeem < redemptionRate)
                {
                    errorMessage = $"Minimum redemption is {redemptionRate} points for {currentTier} tier.";
                    return false;
                }

                // Calculate discount with tier-specific rate
                discountAmount = CalculateDiscountFromPoints(pointsToRedeem, currentTier);
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

        // Promotion bonus points
        public static bool ApplyPromotionBonus(int customerID, string promotionCode, out int bonusPoints, out string errorMessage)
        {
            bonusPoints = 0;
            errorMessage = "";

            try
            {
                // Get promotion bonus amount
                int promotionBonus = GetPromotionBonus(promotionCode);
                if (promotionBonus == 0)
                {
                    errorMessage = "Invalid or expired promotion code.";
                    return false;
                }

                // Get current customer data
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return false;
                }

                DataRow customerRow = customer.Rows[0];
                int currentPoints = customerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customerRow["LoyaltyPoints"]) : 0;
                string currentTier = customerRow["Tier"] != DBNull.Value ? customerRow["Tier"].ToString() : "Bronze";

                // Apply tier multiplier to promotion bonus
                decimal multiplier = GetTierMultiplier(currentTier);
                bonusPoints = (int)(promotionBonus * multiplier);
                int newPoints = currentPoints + bonusPoints;

                // Update customer points
                return clsCustomer.UpdateCustomerPoints(customerID, newPoints, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private static int GetPromotionBonus(string promotionCode)
        {
            // In a real system, this would check a database of active promotions
            // For now, we'll implement a simple hardcoded system
            switch (promotionCode.ToUpper())
            {
                case "WELCOME10": return 10;   // Welcome bonus
                case "DOUBLE50": return 50;   // Double points promotion
                case "BONUS100": return 100;  // Special bonus
                case "PLATINUM200": return 200; // Platinum promotion
                default: return 0;
            }
        }

        // Point expiration settings
        private const int PointExpirationDays = 365; // Points expire after 1 year
        private const int ExpirationWarningDays = 30; // Warn 30 days before expiration

        public static bool AddBonusPoints(int customerID, int bonusPoints, string reason, out string errorMessage)
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
                string currentTier = customerRow["Tier"] != DBNull.Value ? customerRow["Tier"].ToString() : "Bronze";

                // Apply tier multiplier to bonus points
                decimal multiplier = GetTierMultiplier(currentTier);
                int adjustedBonus = (int)(bonusPoints * multiplier);
                int newPoints = currentPoints + adjustedBonus;

                // Update customer points
                bool success = clsCustomer.UpdateCustomerPoints(customerID, newPoints, out errorMessage);
                
                if (success)
                {
                    // Track point history with expiration
                    TrackPointsHistory(customerID, adjustedBonus, currentPoints, newPoints, reason, true);
                    errorMessage = $"Added {adjustedBonus} bonus points (multiplier: {multiplier}x). Reason: {reason}";
                }
                
                return success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private static void TrackPointsHistory(int customerID, int pointsChange, int pointsBefore, int pointsAfter, string reason, bool hasExpiration)
        {
            try
            {
                DateTime? expirationDate = null;
                if (hasExpiration && pointsChange > 0)
                {
                    expirationDate = DateTime.Now.AddDays(PointExpirationDays);
                }

                // This would call a data layer method to insert into LoyaltyPointsHistory
                // For now, we'll add a placeholder comment
                // clsLoyaltyData.TrackPointsHistory(customerID, pointsChange, pointsBefore, pointsAfter, reason, expirationDate);
            }
            catch
            {
                // Silently fail - history tracking is optional
            }
        }

        public static int GetExpiringPoints(int customerID, int daysThreshold)
        {
            try
            {
                // This would query the LoyaltyPointsHistory table
                // For now, return 0 as placeholder
                // return clsLoyaltyData.GetExpiringPoints(customerID, daysThreshold);
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        // Birthday bonus settings
        private const int BirthdayBonusPoints = 50;
        private const int BirthdayBonusWindowDays = 7; // Bonus valid 7 days before/after birthday

        public static bool ExpireOldPoints(out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // This would find and expire points older than PointExpirationDays
                // For now, return true as placeholder
                // return clsLoyaltyData.ExpireOldPoints(PointExpirationDays, out errorMessage);
                errorMessage = "Point expiration not yet implemented in data layer.";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool CheckAndApplyBirthdayBonus(int customerID, out int bonusPoints, out string errorMessage)
        {
            bonusPoints = 0;
            errorMessage = "";

            try
            {
                // Get customer data including birthday
                var customer = clsCustomer.GetCustomerByID(customerID);
                if (customer == null || customer.Rows.Count == 0)
                {
                    errorMessage = "Customer not found.";
                    return false;
                }

                DataRow customerRow = customer.Rows[0];
                
                // Check if birthday is set
                if (customerRow["Birthday"] == DBNull.Value)
                {
                    errorMessage = "Customer birthday not set.";
                    return false;
                }

                DateTime birthday = Convert.ToDateTime(customerRow["Birthday"]);
                DateTime today = DateTime.Today;
                
                // Get customer tier for multiplier
                string currentTier = customerRow["Tier"] != DBNull.Value ? customerRow["Tier"].ToString() : "Bronze";
                decimal multiplier = GetTierMultiplier(currentTier);

                // Check if today is within birthday bonus window
                DateTime birthdayThisYear = new DateTime(today.Year, birthday.Month, birthday.Day);
                DateTime birthdayWindowStart = birthdayThisYear.AddDays(-BirthdayBonusWindowDays);
                DateTime birthdayWindowEnd = birthdayThisYear.AddDays(BirthdayBonusWindowDays);

                if (today >= birthdayWindowStart && today <= birthdayWindowEnd)
                {
                    // Check if birthday bonus already applied this year
                    bool alreadyApplied = CheckBirthdayBonusApplied(customerID, today.Year);
                    
                    if (!alreadyApplied)
                    {
                        bonusPoints = (int)(BirthdayBonusPoints * multiplier);
                        int currentPoints = customerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customerRow["LoyaltyPoints"]) : 0;
                        int newPoints = currentPoints + bonusPoints;

                        bool success = clsCustomer.UpdateCustomerPoints(customerID, newPoints, out errorMessage);
                        
                        if (success)
                        {
                            TrackPointsHistory(customerID, bonusPoints, currentPoints, newPoints, "Birthday Bonus", true);
                            errorMessage = $"Happy Birthday! Added {bonusPoints} bonus points.";
                        }
                        
                        return success;
                    }
                    else
                    {
                        errorMessage = "Birthday bonus already applied this year.";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Not within birthday bonus window.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        // Referral bonus settings
        private const int ReferrerBonusPoints = 100; // Points for referrer
        private const int RefereeBonusPoints = 50;   // Points for new customer

        private static bool CheckBirthdayBonusApplied(int customerID, int year)
        {
            try
            {
                // This would check LoyaltyPointsHistory for birthday bonus in the given year
                // For now, return false as placeholder
                // return clsLoyaltyData.CheckBirthdayBonusApplied(customerID, year);
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ApplyReferralBonus(int referrerID, int refereeID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Get referrer data
                var referrer = clsCustomer.GetCustomerByID(referrerID);
                if (referrer == null || referrer.Rows.Count == 0)
                {
                    errorMessage = "Referrer not found.";
                    return false;
                }

                // Get referee data
                var referee = clsCustomer.GetCustomerByID(refereeID);
                if (referee == null || referee.Rows.Count == 0)
                {
                    errorMessage = "Referee not found.";
                    return false;
                }

                DataRow referrerRow = referrer.Rows[0];
                DataRow refereeRow = referee.Rows[0];

                // Check if referee already has a referrer set
                if (refereeRow["ReferredBy"] != DBNull.Value)
                {
                    errorMessage = "Referee already has a referrer.";
                    return false;
                }

                // Get tiers for multipliers
                string referrerTier = referrerRow["Tier"] != DBNull.Value ? referrerRow["Tier"].ToString() : "Bronze";
                string refereeTier = refereeRow["Tier"] != DBNull.Value ? refereeRow["Tier"].ToString() : "Bronze";

                decimal referrerMultiplier = GetTierMultiplier(referrerTier);
                decimal refereeMultiplier = GetTierMultiplier(refereeTier);

                // Calculate bonuses
                int referrerBonus = (int)(ReferrerBonusPoints * referrerMultiplier);
                int refereeBonus = (int)(RefereeBonusPoints * refereeMultiplier);

                // Update referrer points
                int referrerCurrentPoints = referrerRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(referrerRow["LoyaltyPoints"]) : 0;
                int referrerNewPoints = referrerCurrentPoints + referrerBonus;
                bool referrerSuccess = clsCustomer.UpdateCustomerPoints(referrerID, referrerNewPoints, out errorMessage);

                if (!referrerSuccess)
                    return false;

                // Update referee points
                int refereeCurrentPoints = refereeRow["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(refereeRow["LoyaltyPoints"]) : 0;
                int refereeNewPoints = refereeCurrentPoints + refereeBonus;
                bool refereeSuccess = clsCustomer.UpdateCustomerPoints(refereeID, refereeNewPoints, out errorMessage);

                if (!refereeSuccess)
                    return false;

                // Set referee's ReferredBy field
                bool updateReferrerSuccess = clsCustomer.UpdateCustomerReferrer(refereeID, referrerID, out errorMessage);

                if (updateReferrerSuccess)
                {
                    // Track both bonuses
                    TrackPointsHistory(referrerID, referrerBonus, referrerCurrentPoints, referrerNewPoints, "Referral Bonus", true);
                    TrackPointsHistory(refereeID, refereeBonus, refereeCurrentPoints, refereeNewPoints, "Referral Welcome Bonus", true);
                    errorMessage = $"Referral bonus applied: {referrerBonus} points to referrer, {refereeBonus} points to referee.";
                }

                return updateReferrerSuccess;
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
