using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsCouponData
    {
        public class CouponInfo
        {
            public int CouponID { get; set; }
            public string CouponCode { get; set; }
            public string CouponType { get; set; }
            public decimal DiscountValue { get; set; }
            public decimal MinPurchaseAmount { get; set; }
            public decimal? MaxDiscountAmount { get; set; }
            public DateTime ValidFrom { get; set; }
            public DateTime ValidUntil { get; set; }
            public int? UsageLimit { get; set; }
            public int UsedCount { get; set; }
            public bool IsActive { get; set; }
            public string ApplicableCategories { get; set; }
            public string ApplicableProducts { get; set; }
            public DateTime CreatedDate { get; set; }
            public int? CreatedBy { get; set; }
        }

        public static bool AddCoupon(CouponInfo coupon, out int couponID, out string errorMessage)
        {
            couponID = -1;
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Coupons (CouponCode, CouponType, DiscountValue, MinPurchaseAmount, MaxDiscountAmount, 
                                              ValidFrom, ValidUntil, UsageLimit, UsedCount, IsActive, 
                                              ApplicableCategories, ApplicableProducts, CreatedDate, CreatedBy)
                        VALUES (@CouponCode, @CouponType, @DiscountValue, @MinPurchaseAmount, @MaxDiscountAmount, 
                                @ValidFrom, @ValidUntil, @UsageLimit, 0, @IsActive, 
                                @ApplicableCategories, @ApplicableProducts, GETDATE(), @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponCode", coupon.CouponCode);
                        command.Parameters.AddWithValue("@CouponType", coupon.CouponType);
                        command.Parameters.AddWithValue("@DiscountValue", coupon.DiscountValue);
                        command.Parameters.AddWithValue("@MinPurchaseAmount", coupon.MinPurchaseAmount);
                        command.Parameters.AddWithValue("@MaxDiscountAmount", coupon.MaxDiscountAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ValidFrom", coupon.ValidFrom);
                        command.Parameters.AddWithValue("@ValidUntil", coupon.ValidUntil);
                        command.Parameters.AddWithValue("@UsageLimit", coupon.UsageLimit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", coupon.IsActive);
                        command.Parameters.AddWithValue("@ApplicableCategories", coupon.ApplicableCategories ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ApplicableProducts", coupon.ApplicableProducts ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", coupon.CreatedBy ?? (object)DBNull.Value);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            couponID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create coupon record.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool UpdateCoupon(CouponInfo coupon, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Coupons 
                        SET CouponType = @CouponType,
                            DiscountValue = @DiscountValue,
                            MinPurchaseAmount = @MinPurchaseAmount,
                            MaxDiscountAmount = @MaxDiscountAmount,
                            ValidFrom = @ValidFrom,
                            ValidUntil = @ValidUntil,
                            UsageLimit = @UsageLimit,
                            IsActive = @IsActive,
                            ApplicableCategories = @ApplicableCategories,
                            ApplicableProducts = @ApplicableProducts
                        WHERE CouponID = @CouponID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponID", coupon.CouponID);
                        command.Parameters.AddWithValue("@CouponType", coupon.CouponType);
                        command.Parameters.AddWithValue("@DiscountValue", coupon.DiscountValue);
                        command.Parameters.AddWithValue("@MinPurchaseAmount", coupon.MinPurchaseAmount);
                        command.Parameters.AddWithValue("@MaxDiscountAmount", coupon.MaxDiscountAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ValidFrom", coupon.ValidFrom);
                        command.Parameters.AddWithValue("@ValidUntil", coupon.ValidUntil);
                        command.Parameters.AddWithValue("@UsageLimit", coupon.UsageLimit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", coupon.IsActive);
                        command.Parameters.AddWithValue("@ApplicableCategories", coupon.ApplicableCategories ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ApplicableProducts", coupon.ApplicableProducts ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool DeleteCoupon(int couponID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Coupons WHERE CouponID = @CouponID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponID", couponID);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static CouponInfo GetCouponByID(int couponID, out string errorMessage)
        {
            errorMessage = "";
            CouponInfo coupon = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Coupons WHERE CouponID = @CouponID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponID", couponID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                coupon = new CouponInfo
                                {
                                    CouponID = reader.GetInt32(reader.GetOrdinal("CouponID")),
                                    CouponCode = reader.GetString(reader.GetOrdinal("CouponCode")),
                                    CouponType = reader.GetString(reader.GetOrdinal("CouponType")),
                                    DiscountValue = reader.GetDecimal(reader.GetOrdinal("DiscountValue")),
                                    MinPurchaseAmount = reader.GetDecimal(reader.GetOrdinal("MinPurchaseAmount")),
                                    MaxDiscountAmount = reader.IsDBNull(reader.GetOrdinal("MaxDiscountAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("MaxDiscountAmount")),
                                    ValidFrom = reader.GetDateTime(reader.GetOrdinal("ValidFrom")),
                                    ValidUntil = reader.GetDateTime(reader.GetOrdinal("ValidUntil")),
                                    UsageLimit = reader.IsDBNull(reader.GetOrdinal("UsageLimit")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UsageLimit")),
                                    UsedCount = reader.GetInt32(reader.GetOrdinal("UsedCount")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    ApplicableCategories = reader.IsDBNull(reader.GetOrdinal("ApplicableCategories")) ? null : reader.GetString(reader.GetOrdinal("ApplicableCategories")),
                                    ApplicableProducts = reader.IsDBNull(reader.GetOrdinal("ApplicableProducts")) ? null : reader.GetString(reader.GetOrdinal("ApplicableProducts")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return coupon;
        }

        public static CouponInfo GetCouponByCode(string couponCode, out string errorMessage)
        {
            errorMessage = "";
            CouponInfo coupon = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Coupons WHERE CouponCode = @CouponCode";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponCode", couponCode);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                coupon = new CouponInfo
                                {
                                    CouponID = reader.GetInt32(reader.GetOrdinal("CouponID")),
                                    CouponCode = reader.GetString(reader.GetOrdinal("CouponCode")),
                                    CouponType = reader.GetString(reader.GetOrdinal("CouponType")),
                                    DiscountValue = reader.GetDecimal(reader.GetOrdinal("DiscountValue")),
                                    MinPurchaseAmount = reader.GetDecimal(reader.GetOrdinal("MinPurchaseAmount")),
                                    MaxDiscountAmount = reader.IsDBNull(reader.GetOrdinal("MaxDiscountAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("MaxDiscountAmount")),
                                    ValidFrom = reader.GetDateTime(reader.GetOrdinal("ValidFrom")),
                                    ValidUntil = reader.GetDateTime(reader.GetOrdinal("ValidUntil")),
                                    UsageLimit = reader.IsDBNull(reader.GetOrdinal("UsageLimit")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UsageLimit")),
                                    UsedCount = reader.GetInt32(reader.GetOrdinal("UsedCount")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    ApplicableCategories = reader.IsDBNull(reader.GetOrdinal("ApplicableCategories")) ? null : reader.GetString(reader.GetOrdinal("ApplicableCategories")),
                                    ApplicableProducts = reader.IsDBNull(reader.GetOrdinal("ApplicableProducts")) ? null : reader.GetString(reader.GetOrdinal("ApplicableProducts")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return coupon;
        }

        public static List<CouponInfo> GetAllCoupons(out string errorMessage)
        {
            errorMessage = "";
            List<CouponInfo> coupons = new List<CouponInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Coupons ORDER BY CreatedDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                coupons.Add(new CouponInfo
                                {
                                    CouponID = reader.GetInt32(reader.GetOrdinal("CouponID")),
                                    CouponCode = reader.GetString(reader.GetOrdinal("CouponCode")),
                                    CouponType = reader.GetString(reader.GetOrdinal("CouponType")),
                                    DiscountValue = reader.GetDecimal(reader.GetOrdinal("DiscountValue")),
                                    MinPurchaseAmount = reader.GetDecimal(reader.GetOrdinal("MinPurchaseAmount")),
                                    MaxDiscountAmount = reader.IsDBNull(reader.GetOrdinal("MaxDiscountAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("MaxDiscountAmount")),
                                    ValidFrom = reader.GetDateTime(reader.GetOrdinal("ValidFrom")),
                                    ValidUntil = reader.GetDateTime(reader.GetOrdinal("ValidUntil")),
                                    UsageLimit = reader.IsDBNull(reader.GetOrdinal("UsageLimit")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UsageLimit")),
                                    UsedCount = reader.GetInt32(reader.GetOrdinal("UsedCount")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    ApplicableCategories = reader.IsDBNull(reader.GetOrdinal("ApplicableCategories")) ? null : reader.GetString(reader.GetOrdinal("ApplicableCategories")),
                                    ApplicableProducts = reader.IsDBNull(reader.GetOrdinal("ApplicableProducts")) ? null : reader.GetString(reader.GetOrdinal("ApplicableProducts")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return coupons;
        }

        public static bool IncrementCouponUsage(int couponID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Coupons SET UsedCount = UsedCount + 1 WHERE CouponID = @CouponID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponID", couponID);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool CouponExists(string couponCode, out string errorMessage)
        {
            errorMessage = "";
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Coupons WHERE CouponCode = @CouponCode";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CouponCode", couponCode);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        exists = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return exists;
        }
    }
}
