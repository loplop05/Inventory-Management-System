using System;
using System.Collections.Generic;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsCoupon
    {
        public static bool AddCoupon(clsCouponData.CouponInfo coupon, out int couponID, out string errorMessage)
        {
            couponID = -1;
            errorMessage = "";

            try
            {
                // Validate coupon code uniqueness
                if (clsCouponData.CouponExists(coupon.CouponCode, out string existsError))
                {
                    errorMessage = "Coupon code already exists.";
                    return false;
                }

                // Validate dates
                if (coupon.ValidFrom >= coupon.ValidUntil)
                {
                    errorMessage = "ValidFrom date must be before ValidUntil date.";
                    return false;
                }

                // Validate discount value
                if (coupon.DiscountValue <= 0)
                {
                    errorMessage = "Discount value must be greater than 0.";
                    return false;
                }

                // Validate coupon type
                if (coupon.CouponType == "Percentage" && coupon.DiscountValue > 100)
                {
                    errorMessage = "Percentage discount cannot exceed 100%.";
                    return false;
                }

                // Validate usage limit
                if (coupon.UsageLimit.HasValue && coupon.UsageLimit.Value <= 0)
                {
                    errorMessage = "Usage limit must be greater than 0.";
                    return false;
                }

                return clsCouponData.AddCoupon(coupon, out couponID, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool UpdateCoupon(clsCouponData.CouponInfo coupon, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Check if coupon exists
                var existing = clsCouponData.GetCouponByID(coupon.CouponID, out string getError);
                if (existing == null)
                {
                    errorMessage = "Coupon not found.";
                    return false;
                }

                // Validate coupon code uniqueness if changed
                if (existing.CouponCode != coupon.CouponCode)
                {
                    if (clsCouponData.CouponExists(coupon.CouponCode, out string existsError))
                    {
                        errorMessage = "Coupon code already exists.";
                        return false;
                    }
                }

                // Validate dates
                if (coupon.ValidFrom >= coupon.ValidUntil)
                {
                    errorMessage = "ValidFrom date must be before ValidUntil date.";
                    return false;
                }

                // Validate discount value
                if (coupon.DiscountValue <= 0)
                {
                    errorMessage = "Discount value must be greater than 0.";
                    return false;
                }

                // Validate coupon type
                if (coupon.CouponType == "Percentage" && coupon.DiscountValue > 100)
                {
                    errorMessage = "Percentage discount cannot exceed 100%.";
                    return false;
                }

                return clsCouponData.UpdateCoupon(coupon, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool DeleteCoupon(int couponID, out string errorMessage)
        {
            return clsCouponData.DeleteCoupon(couponID, out errorMessage);
        }

        public static clsCouponData.CouponInfo GetCouponByID(int couponID, out string errorMessage)
        {
            return clsCouponData.GetCouponByID(couponID, out errorMessage);
        }

        public static clsCouponData.CouponInfo GetCouponByCode(string couponCode, out string errorMessage)
        {
            return clsCouponData.GetCouponByCode(couponCode, out errorMessage);
        }

        public static List<clsCouponData.CouponInfo> GetAllCoupons(out string errorMessage)
        {
            return clsCouponData.GetAllCoupons(out errorMessage);
        }

        public static bool ValidateCoupon(string couponCode, decimal purchaseAmount, out decimal discountAmount, out string errorMessage)
        {
            discountAmount = 0;
            errorMessage = "";

            try
            {
                var coupon = clsCouponData.GetCouponByCode(couponCode, out string getError);
                if (coupon == null)
                {
                    errorMessage = "Invalid coupon code.";
                    return false;
                }

                // Check if coupon is active
                if (!coupon.IsActive)
                {
                    errorMessage = "Coupon is not active.";
                    return false;
                }

                // Check validity dates
                DateTime now = DateTime.Now;
                if (now < coupon.ValidFrom || now > coupon.ValidUntil)
                {
                    errorMessage = "Coupon is not valid at this time.";
                    return false;
                }

                // Check usage limit
                if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value)
                {
                    errorMessage = "Coupon usage limit has been reached.";
                    return false;
                }

                // Check minimum purchase amount
                if (purchaseAmount < coupon.MinPurchaseAmount)
                {
                    errorMessage = $"Minimum purchase amount of {coupon.MinPurchaseAmount:C} required.";
                    return false;
                }

                // Calculate discount based on type
                if (coupon.CouponType == "Percentage")
                {
                    discountAmount = purchaseAmount * (coupon.DiscountValue / 100);
                }
                else if (coupon.CouponType == "FixedAmount")
                {
                    discountAmount = coupon.DiscountValue;
                }
                else if (coupon.CouponType == "BOGO")
                {
                    // Buy One Get One - 50% off
                    discountAmount = purchaseAmount * 0.5m;
                }
                else
                {
                    errorMessage = "Invalid coupon type.";
                    return false;
                }

                // Apply max discount limit if set
                if (coupon.MaxDiscountAmount.HasValue && discountAmount > coupon.MaxDiscountAmount.Value)
                {
                    discountAmount = coupon.MaxDiscountAmount.Value;
                }

                // Ensure discount doesn't exceed purchase amount
                if (discountAmount > purchaseAmount)
                {
                    discountAmount = purchaseAmount;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool ApplyCoupon(int couponID, out string errorMessage)
        {
            return clsCouponData.IncrementCouponUsage(couponID, out errorMessage);
        }

        public static List<clsCouponData.CouponInfo> GetActiveCoupons(out string errorMessage)
        {
            errorMessage = "";
            List<clsCouponData.CouponInfo> activeCoupons = new List<clsCouponData.CouponInfo>();

            try
            {
                var allCoupons = clsCouponData.GetAllCoupons(out errorMessage);
                DateTime now = DateTime.Now;

                foreach (var coupon in allCoupons)
                {
                    if (coupon.IsActive && now >= coupon.ValidFrom && now <= coupon.ValidUntil)
                    {
                        // Check usage limit
                        if (!coupon.UsageLimit.HasValue || coupon.UsedCount < coupon.UsageLimit.Value)
                        {
                            activeCoupons.Add(coupon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return activeCoupons;
        }
    }
}
