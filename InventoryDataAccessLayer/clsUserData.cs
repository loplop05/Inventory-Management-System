using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace InventoryDataAccessLayer
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }

    public static class clsUserData
    {
        public static bool EnsureUserTableAndSeedAdmin(out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if Users table exists
                    string checkTableQuery = @"
                        SELECT COUNT(*)
                        FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_NAME = 'Users'";

                    bool tableExists = false;
                    using (SqlCommand checkCommand = new SqlCommand(checkTableQuery, connection))
                    {
                        tableExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                    }

                    if (!tableExists)
                    {
                        // Create Users table
                        string createTableQuery = @"
                            CREATE TABLE Users (
                                UserID INT PRIMARY KEY IDENTITY(1,1),
                                Username NVARCHAR(50) UNIQUE NOT NULL,
                                PasswordHash NVARCHAR(256) NOT NULL,
                                PasswordSalt NVARCHAR(128) NOT NULL,
                                DisplayName NVARCHAR(100) NOT NULL,
                                Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Cashier')),
                                IsActive BIT NOT NULL DEFAULT 1,
                                CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
                            )";

                        using (SqlCommand createCommand = new SqlCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        // Seed default admin account
                        string adminUsername = "admin";
                        string adminPassword = "admin123"; // Default password, should be changed on first login
                        string adminDisplayName = "System Administrator";

                        if (!AddUser(adminUsername, adminPassword, adminDisplayName, "Admin", out errorMessage))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static UserInfo AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT UserID, Username, PasswordHash, PasswordSalt, DisplayName, Role, IsActive
                        FROM Users
                        WHERE Username = @Username AND IsActive = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["PasswordHash"].ToString();
                                string storedSalt = reader["PasswordSalt"].ToString();

                                // Verify password
                                if (VerifyPassword(password, storedHash, storedSalt))
                                {
                                    return new UserInfo
                                    {
                                        UserID = Convert.ToInt32(reader["UserID"]),
                                        Username = reader["Username"].ToString(),
                                        DisplayName = reader["DisplayName"].ToString(),
                                        Role = reader["Role"].ToString(),
                                        IsActive = Convert.ToBoolean(reader["IsActive"])
                                    };
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsUserData.GetUserByUsername", ex);
                }
            }

            return null;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                    SELECT UserID, Username, DisplayName, Role, IsActive, CreatedDate
                    FROM Users
                    ORDER BY DisplayName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsErrorLog.LogException("clsUserData.GetAllUsers", ex);
                    }
                }
            }

            return dt;
        }

        public static bool AddUser(string username, string password, string displayName, string role, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(displayName))
            {
                errorMessage = "Username, password, and display name are required.";
                return false;
            }

            if (role != "Admin" && role != "Cashier")
            {
                errorMessage = "Role must be either 'Admin' or 'Cashier'.";
                return false;
            }

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Ensure Users table exists
                    EnsureUserTableAndSeedAdmin(out string setupError);
                    if (!string.IsNullOrWhiteSpace(setupError))
                    {
                        errorMessage = "Failed to setup user table: " + setupError;
                        return false;
                    }

                    // Check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                        {
                            errorMessage = "Username already exists.";
                            return false;
                        }
                    }

                    // Generate salt and hash
                    string salt = GenerateSalt();
                    string hash = HashPassword(password, salt);

                    string insertQuery = @"
                        INSERT INTO Users (Username, PasswordHash, PasswordSalt, DisplayName, Role, IsActive, CreatedDate)
                        VALUES (@Username, @PasswordHash, @PasswordSalt, @DisplayName, @Role, 1, GETDATE())";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@PasswordHash", hash);
                        insertCommand.Parameters.AddWithValue("@PasswordSalt", salt);
                        insertCommand.Parameters.AddWithValue("@DisplayName", displayName);
                        insertCommand.Parameters.AddWithValue("@Role", role);

                        insertCommand.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error: " + ex.Message;
                    return false;
                }
            }
        }

        public static bool UpdateUser(int userID, string displayName, string role, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(displayName))
            {
                errorMessage = "Display name is required.";
                return false;
            }

            if (role != "Admin" && role != "Cashier")
            {
                errorMessage = "Role must be either 'Admin' or 'Cashier'.";
                return false;
            }

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE Users
                        SET DisplayName = @DisplayName, Role = @Role
                        WHERE UserID = @UserID";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@DisplayName", displayName);
                        updateCommand.Parameters.AddWithValue("@Role", role);
                        updateCommand.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            errorMessage = "User not found.";
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static bool DeactivateUser(int userID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Prevent deactivating the last admin
                    string adminCountQuery = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin' AND IsActive = 1 AND UserID != @UserID";
                    using (SqlCommand adminCountCommand = new SqlCommand(adminCountQuery, connection))
                    {
                        adminCountCommand.Parameters.AddWithValue("@UserID", userID);
                        int adminCount = Convert.ToInt32(adminCountCommand.ExecuteScalar());

                        if (adminCount == 0)
                        {
                            errorMessage = "Cannot deactivate the last active admin user.";
                            return false;
                        }
                    }

                    string updateQuery = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            errorMessage = "User not found.";
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static bool ChangePassword(int userID, string newPassword, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                errorMessage = "Password must be at least 6 characters long.";
                return false;
            }

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Generate new salt and hash
                    string salt = GenerateSalt();
                    string hash = HashPassword(newPassword, salt);

                    string updateQuery = @"
                        UPDATE Users
                        SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt
                        WHERE UserID = @UserID";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@PasswordHash", hash);
                        updateCommand.Parameters.AddWithValue("@PasswordSalt", salt);
                        updateCommand.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            errorMessage = "User not found.";
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        private static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            string computedHash = HashPassword(password, storedSalt);
            return computedHash == storedHash;
        }
    }
}
