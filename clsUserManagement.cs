using System;
using System.Collections.Generic;
using InventoryDataAccessLayer;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    /// <summary>
    /// User role and access control management.
    /// Supports Cashier (POS only) and Admin (full access) roles.
    /// </summary>
    public static class clsUserManagement
    {
        public enum UserRole
        {
            Cashier,  // POS only
            Manager,  // Day-to-day operations, reporting, limited admin
            Admin     // Full access
        }

        public class User
        {
            public int UserID { get; set; }
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public UserRole Role { get; set; }
            public bool IsActive { get; set; }
        }

        private static User _currentUser = null;

        /// <summary>
        /// Gets or sets the currently logged-in user.
        /// </summary>
        public static User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        /// <summary>
        /// Checks if the current user has admin privileges.
        /// </summary>
        public static bool IsAdmin => _currentUser?.Role == UserRole.Admin;

        /// <summary>
        /// Checks if the current user is a cashier.
        /// </summary>
        public static bool IsCashier => _currentUser?.Role == UserRole.Cashier;

        /// <summary>
        /// Checks if the current user is a manager.
        /// </summary>
        public static bool IsManager => _currentUser?.Role == UserRole.Manager;

        /// <summary>
        /// Authenticates a user with username and password.
        /// Uses database authentication with salted hash.
        /// </summary>
        public static User Authenticate(string username, string password)
        {
            // Ensure user table exists and has admin seeded
            string errorMessage;
            clsUserData.EnsureUserTableAndSeedAdmin(out errorMessage);

            // Authenticate via data layer
            var userInfo = clsUserData.AuthenticateUser(username, password);
            
            if (userInfo == null)
                return null;

            // Convert UserInfo to User
            return new User
            {
                UserID = userInfo.UserID,
                Username = userInfo.Username,
                DisplayName = userInfo.DisplayName,
                Role = (UserRole)Enum.Parse(typeof(UserRole), userInfo.Role),
                IsActive = userInfo.IsActive
            };
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
        }

        /// <summary>
        /// Checks if the current user has a specific permission.
        /// Admins always have full access.
        /// </summary>
        public static bool HasPermission(string permission)
        {
            if (_currentUser == null) return false;
            if (_currentUser.Role == UserRole.Admin) return true; // Admin always full access
            return clsPermissions.HasPermission(_currentUser.UserID, permission);
        }

        /// <summary>
        /// Checks if the current user can access a specific feature.
        /// </summary>
        public static bool CanAccessFeature(string feature)
        {
            if (_currentUser == null) return false;

            // Admins can access everything
            if (_currentUser.Role == UserRole.Admin) return true;

            // Cashiers can only access POS
            if (_currentUser.Role == UserRole.Cashier)
            {
                return feature == "POS";
            }

            return false;
        }
    }
}
