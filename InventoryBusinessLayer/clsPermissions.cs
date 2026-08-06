using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsPermissions
    {
        // Permission constants
        public const string ViewDashboard = "ViewDashboard";
        public const string ManageProducts = "ManageProducts";
        public const string ManageCategories = "ManageCategories";
        public const string ManageSuppliers = "ManageSuppliers";
        public const string ManageCustomers = "ManageCustomers";
        public const string ManageUsers = "ManageUsers";
        public const string ViewReports = "ViewReports";
        public const string ProcessSales = "ProcessSales";
        public const string ManageCoupons = "ManageCoupons";
        public const string ViewAuditLogs = "ViewAuditLogs";
        public const string AdjustLoyalty = "AdjustLoyalty";
        public const string DeleteOrders = "DeleteOrders";
        public const string ManageInventory = "ManageInventory";

        private static Dictionary<int, List<string>> _userPermissionCache = new Dictionary<int, List<string>>();
        private static DateTime _cacheExpiry = DateTime.MinValue;

        public static bool HasPermission(int userID, string permission)
        {
            try
            {
                // Refresh cache if expired (5 minutes)
                if (DateTime.Now > _cacheExpiry)
                {
                    RefreshPermissionCache();
                }

                if (_userPermissionCache.ContainsKey(userID))
                {
                    return _userPermissionCache[userID].Contains(permission);
                }

                return false;
            }
            catch
            {
                // On error, deny access for safety
                return false;
            }
        }

        public static bool HasPermission(string permission)
        {
            // Note: This overload requires the current user to be set via a session/context mechanism
            // For now, this method should not be used - use HasPermission(int userID, string permission) instead
            return false;
        }

        public static List<string> GetUserPermissions(int userID)
        {
            try
            {
                string errorMessage;
                DataTable permissions = clsPermissionsData.GetUserPermissions(userID, out errorMessage);

                if (permissions != null && permissions.Rows.Count > 0)
                {
                    return permissions.AsEnumerable()
                        .Select(row => row["PermissionName"].ToString())
                        .ToList();
                }

                return new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static List<string> GetAllPermissions()
        {
            try
            {
                string errorMessage;
                DataTable permissions = clsPermissionsData.GetAllPermissions(out errorMessage);

                if (permissions != null && permissions.Rows.Count > 0)
                {
                    return permissions.AsEnumerable()
                        .Select(row => row["PermissionName"].ToString())
                        .ToList();
                }

                return new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static DataTable GetAllPermissionsDataTable(out string errorMessage)
        {
            return clsPermissionsData.GetAllPermissions(out errorMessage);
        }

        public static bool AssignPermission(int userID, string permission, out string errorMessage)
        {
            try
            {
                bool result = clsPermissionsData.AssignPermission(userID, permission, out errorMessage);
                
                if (result)
                {
                    // Clear cache to force refresh
                    _cacheExpiry = DateTime.MinValue;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool RevokePermission(int userID, string permission, out string errorMessage)
        {
            try
            {
                bool result = clsPermissionsData.RevokePermission(userID, permission, out errorMessage);
                
                if (result)
                {
                    // Clear cache to force refresh
                    _cacheExpiry = DateTime.MinValue;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool SetUserPermissions(int userID, List<string> permissions, out string errorMessage)
        {
            try
            {
                bool result = clsPermissionsData.SetUserPermissions(userID, permissions, out errorMessage);
                
                if (result)
                {
                    // Clear cache to force refresh
                    _cacheExpiry = DateTime.MinValue;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private static void RefreshPermissionCache()
        {
            try
            {
                _userPermissionCache.Clear();
                
                string errorMessage;
                DataTable allUserPermissions = clsPermissionsData.GetAllUserPermissions(out errorMessage);
                
                if (allUserPermissions != null && allUserPermissions.Rows.Count > 0)
                {
                    foreach (DataRow row in allUserPermissions.Rows)
                    {
                        int userID = Convert.ToInt32(row["UserID"]);
                        string permission = row["PermissionName"].ToString();
                        
                        if (!_userPermissionCache.ContainsKey(userID))
                        {
                            _userPermissionCache[userID] = new List<string>();
                        }
                        
                        _userPermissionCache[userID].Add(permission);
                    }
                }
                
                _cacheExpiry = DateTime.Now.AddMinutes(5);
            }
            catch
            {
                // On cache refresh error, set expiry to retry soon
                _cacheExpiry = DateTime.Now.AddSeconds(30);
            }
        }

        public static DataTable GetRolePermissions(int roleID, out string errorMessage)
        {
            return clsPermissionsData.GetRolePermissions(roleID, out errorMessage);
        }

        public static bool SetRolePermissions(int roleID, List<string> permissions, out string errorMessage)
        {
            try
            {
                bool result = clsPermissionsData.SetRolePermissions(roleID, permissions, out errorMessage);
                
                if (result)
                {
                    // Clear cache to force refresh
                    _cacheExpiry = DateTime.MinValue;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
