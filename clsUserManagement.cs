using System;
using System.Collections.Generic;

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
        /// Authenticates a user with username and password.
        /// For demo purposes, uses hardcoded credentials.
        /// </summary>
        public static User Authenticate(string username, string password)
        {
            // Demo credentials - in production, use database authentication
            var users = new List<User>
            {
                new User { UserID = 1, Username = "admin", DisplayName = "Administrator", Role = UserRole.Admin, IsActive = true },
                new User { UserID = 2, Username = "cashier", DisplayName = "Cashier", Role = UserRole.Cashier, IsActive = true }
            };

            foreach (var user in users)
            {
                if (string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase) && 
                    user.IsActive && 
                    password == "1234") // Demo password
                {
                    return user;
                }
            }

            return null;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
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
