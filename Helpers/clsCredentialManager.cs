using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Secure credential storage using DPAPI (Data Protection API)
    /// Encrypts credentials before storing in Windows Registry
    /// </summary>
    public static class clsCredentialManager
    {
        private const string RegistryPath = @"Software\InventoryManagementSystem";
        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";

        /// <summary>
        /// Saves credentials securely using DPAPI encryption
        /// </summary>
        public static bool SaveCredentials(string username, string password)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    if (key == null)
                        return false;

                    // Store username in plaintext (not sensitive)
                    key.SetValue(UsernameKey, username);

                    // Encrypt password using DPAPI
                    byte[] encryptedPassword = ProtectedData.Protect(
                        Encoding.UTF8.GetBytes(password),
                        null,
                        DataProtectionScope.CurrentUser
                    );

                    key.SetValue(PasswordKey, Convert.ToBase64String(encryptedPassword));
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving credentials: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Loads and decrypts credentials
        /// </summary>
        public static bool LoadCredentials(out string username, out string password)
        {
            username = null;
            password = null;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    if (key == null)
                        return false;

                    username = key.GetValue(UsernameKey) as string;
                    string encryptedPassword = key.GetValue(PasswordKey) as string;

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(encryptedPassword))
                        return false;

                    // Decrypt password using DPAPI
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
                    byte[] decryptedBytes = ProtectedData.Unprotect(
                        encryptedBytes,
                        null,
                        DataProtectionScope.CurrentUser
                    );

                    password = Encoding.UTF8.GetString(decryptedBytes);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading credentials: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Clears saved credentials from registry
        /// </summary>
        public static bool ClearCredentials()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
                {
                    if (key == null)
                        return true; // Nothing to clear

                    key.DeleteValue(UsernameKey, false);
                    key.DeleteValue(PasswordKey, false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing credentials: {ex.Message}");
                return false;
            }
        }
    }
}
