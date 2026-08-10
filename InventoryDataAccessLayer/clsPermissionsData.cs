using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsPermissionsData
    {
        public static DataTable GetUserPermissions(int userID, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT p.PermissionID, p.PermissionName, p.Description
                        FROM Permissions p
                        INNER JOIN RolePermissions rp ON p.PermissionID = rp.PermissionID
                        INNER JOIN Users u ON u.Role = rp.RoleName
                        WHERE u.UserID = @UserID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
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

        public static DataTable GetAllPermissions(out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    string query = "SELECT PermissionID, PermissionName, Description FROM Permissions ORDER BY PermissionName";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
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

        public static DataTable GetAllUserPermissions(out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT u.UserID, p.PermissionName
                        FROM Users u
                        INNER JOIN RolePermissions rp ON u.Role = rp.RoleName
                        INNER JOIN Permissions p ON rp.PermissionID = p.PermissionID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
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

        public static bool AssignPermission(int userID, string permission, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    // Get user's role
                    string roleQuery = "SELECT Role FROM Users WHERE UserID = @UserID";
                    string roleName;
                    
                    using (SqlCommand command = new SqlCommand(roleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        object result = command.ExecuteScalar();
                        
                        if (result == null || result == DBNull.Value)
                        {
                            errorMessage = "User not found";
                            return false;
                        }
                        
                        roleName = result.ToString();
                    }
                    
                    // Get permission ID
                    string permQuery = "SELECT PermissionID FROM Permissions WHERE PermissionName = @PermissionName";
                    int permissionID;
                    
                    using (SqlCommand command = new SqlCommand(permQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PermissionName", permission);
                        object result = command.ExecuteScalar();
                        
                        if (result == null || result == DBNull.Value)
                        {
                            errorMessage = "Permission not found";
                            return false;
                        }
                        
                        permissionID = Convert.ToInt32(result);
                    }
                    
                    // Check if already assigned
                    string checkQuery = "SELECT COUNT(*) FROM RolePermissions WHERE RoleName = @RoleName AND PermissionID = @PermissionID";
                    
                    using (SqlCommand command = new SqlCommand(checkQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        command.Parameters.AddWithValue("@PermissionID", permissionID);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        
                        if (count > 0)
                        {
                            errorMessage = "Permission already assigned to this role";
                            return false;
                        }
                    }
                    
                    // Assign permission to role
                    string insertQuery = "INSERT INTO RolePermissions (RoleName, PermissionID) VALUES (@RoleName, @PermissionID)";
                    
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        command.Parameters.AddWithValue("@PermissionID", permissionID);
                        command.ExecuteNonQuery();
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool RevokePermission(int userID, string permission, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    // Get user's role
                    string roleQuery = "SELECT Role FROM Users WHERE UserID = @UserID";
                    string roleName;
                    
                    using (SqlCommand command = new SqlCommand(roleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        object result = command.ExecuteScalar();
                        
                        if (result == null || result == DBNull.Value)
                        {
                            errorMessage = "User not found";
                            return false;
                        }
                        
                        roleName = result.ToString();
                    }
                    
                    // Get permission ID
                    string permQuery = "SELECT PermissionID FROM Permissions WHERE PermissionName = @PermissionName";
                    int permissionID;
                    
                    using (SqlCommand command = new SqlCommand(permQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PermissionName", permission);
                        object result = command.ExecuteScalar();
                        
                        if (result == null || result == DBNull.Value)
                        {
                            errorMessage = "Permission not found";
                            return false;
                        }
                        
                        permissionID = Convert.ToInt32(result);
                    }
                    
                    // Revoke permission
                    string deleteQuery = "DELETE FROM RolePermissions WHERE RoleName = @RoleName AND PermissionID = @PermissionID";
                    
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        command.Parameters.AddWithValue("@PermissionID", permissionID);
                        command.ExecuteNonQuery();
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool SetUserPermissions(int userID, List<string> permissions, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    
                    // Get user's role
                    string roleQuery = "SELECT Role FROM Users WHERE UserID = @UserID";
                    string roleName;
                    
                    using (SqlCommand command = new SqlCommand(roleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        object result = command.ExecuteScalar();
                        
                        if (result == null || result == DBNull.Value)
                        {
                            errorMessage = "User not found";
                            return false;
                        }
                        
                        roleName = result.ToString();
                    }
                    
                    // Delete existing permissions for this role
                    string deleteQuery = "DELETE FROM RolePermissions WHERE RoleName = @RoleName";
                    
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        command.ExecuteNonQuery();
                    }
                    
                    // Add new permissions
                    foreach (string permission in permissions)
                    {
                        string permQuery = "SELECT PermissionID FROM Permissions WHERE PermissionName = @PermissionName";
                        
                        using (SqlCommand command = new SqlCommand(permQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PermissionName", permission);
                            object result = command.ExecuteScalar();
                            
                            if (result != null && result != DBNull.Value)
                            {
                                int permissionID = Convert.ToInt32(result);
                                string insertQuery = "INSERT INTO RolePermissions (RoleName, PermissionID) VALUES (@RoleName, @PermissionID)";
                                
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                                {
                                    insertCmd.Parameters.AddWithValue("@RoleName", roleName);
                                    insertCmd.Parameters.AddWithValue("@PermissionID", permissionID);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static DataTable GetRolePermissions(int roleID, out string errorMessage)
        {
            // This method is deprecated - use GetUserPermissions instead
            // Kept for backward compatibility
            errorMessage = "This method is deprecated. Use GetUserPermissions instead.";
            return null;
        }

        public static bool SetRolePermissions(int roleID, List<string> permissions, out string errorMessage)
        {
            // This method is deprecated - use SetUserPermissions instead
            // Kept for backward compatibility
            errorMessage = "This method is deprecated. Use SetUserPermissions instead.";
            return false;
        }
    }
}
