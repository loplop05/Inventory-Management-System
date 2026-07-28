using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized data validation helper class.
    /// Provides common validation methods for user input.
    /// </summary>
    public static class clsDataValidation
    {
        // ─── Common Validation Rules ───────────────────────────────────────────

        /// <summary>Validates that a string is not null or whitespace.</summary>
        public static bool IsRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>Validates that a string meets minimum length requirement.</summary>
        public static bool HasMinimumLength(string value, int minLength)
        {
            return value != null && value.Length >= minLength;
        }

        /// <summary>Validates that a string does not exceed maximum length.</summary>
        public static bool HasMaximumLength(string value, int maxLength)
        {
            return value != null && value.Length <= maxLength;
        }

        /// <summary>Validates that a string is within length bounds.</summary>
        public static bool HasValidLength(string value, int minLength, int maxLength)
        {
            return value != null && value.Length >= minLength && value.Length <= maxLength;
        }

        /// <summary>Validates that a string contains only letters and spaces.</summary>
        public static bool IsAlphaOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z\s]+$");
        }

        /// <summary>Validates that a string contains only alphanumeric characters.</summary>
        public static bool IsAlphanumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z0-9\s]+$");
        }

        /// <summary>Validates that a string is a valid email address.</summary>
        public static bool IsValidEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return addr.Address == value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Validates that a string is a valid phone number (digits, spaces, dashes, parentheses).</summary>
        public static bool IsValidPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[\d\s\-\(\)\+]+$");
        }

        /// <summary>Validates that a string represents a positive number.</summary>
        public static bool IsPositiveNumber(string value)
        {
            return decimal.TryParse(value, out decimal result) && result > 0;
        }

        /// <summary>Validates that a string represents a non-negative number.</summary>
        public static bool IsNonNegativeNumber(string value)
        {
            return decimal.TryParse(value, out decimal result) && result >= 0;
        }

        /// <summary>Validates that a string represents a number within a range.</summary>
        public static bool IsNumberInRange(string value, decimal min, decimal max)
        {
            return decimal.TryParse(value, out decimal result) && result >= min && result <= max;
        }

        /// <summary>Validates that a string is a valid integer.</summary>
        public static bool IsValidInteger(string value)
        {
            return int.TryParse(value, out _);
        }

        /// <summary>Validates that a string is a valid positive integer.</summary>
        public static bool IsValidPositiveInteger(string value)
        {
            return int.TryParse(value, out int result) && result > 0;
        }

        /// <summary>Validates that a string contains only digits.</summary>
        public static bool IsDigitsOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^\d+$");
        }

        /// <summary>Validates that a value is not greater than a maximum.</summary>
        public static bool IsNotGreaterThan(decimal value, decimal max)
        {
            return value <= max;
        }

        /// <summary>Validates that a value is not less than a minimum.</summary>
        public static bool IsNotLessThan(decimal value, decimal min)
        {
            return value >= min;
        }

        // ─── Business-Specific Validation ───────────────────────────────────────

        /// <summary>Validates product name (letters, numbers, spaces, common symbols).</summary>
        public static bool IsValidProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z0-9\s\-\.\,\(\)]+$");
        }

        /// <summary>Validates barcode (alphanumeric, typically 8-13 digits).</summary>
        public static bool IsValidBarcode(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z0-9]+$") && value.Length >= 8 && value.Length <= 13;
        }

        /// <summary>Validates category name (letters, numbers, spaces).</summary>
        public static bool IsValidCategoryName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z0-9\s]+$");
        }

        /// <summary>Validates supplier name (letters, numbers, spaces, common symbols).</summary>
        public static bool IsValidSupplierName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, @"^[a-zA-Z0-9\s\-\.\,\(\)]+$");
        }

        // ─── UI Integration Helpers ─────────────────────────────────────────────

        /// <summary>
        /// Validates a TextBox and shows error if invalid.
        /// Returns true if valid, false otherwise.
        /// </summary>
        public static bool ValidateTextBox(
            TextBox textBox,
            ErrorProvider errorProvider,
            Func<string, bool> validationRule,
            string errorMessage)
        {
            if (validationRule(textBox.Text))
            {
                clsFormTheme.ClearInputError(textBox, errorProvider);
                return true;
            }
            else
            {
                clsFormTheme.ShowInputError(textBox, errorProvider, errorMessage);
                return false;
            }
        }

        /// <summary>
        /// Validates multiple TextBoxes and returns true if all are valid.
        /// </summary>
        public static bool ValidateMultiple(
            params (TextBox textBox, ErrorProvider errorProvider, Func<string, bool> rule, string error)[] validations)
        {
            bool allValid = true;
            foreach (var (textBox, errorProvider, rule, error) in validations)
            {
                if (!ValidateTextBox(textBox, errorProvider, rule, error))
                {
                    allValid = false;
                }
            }
            return allValid;
        }

        /// <summary>
        /// Clears all validation errors from a form's ErrorProvider.
        /// </summary>
        public static void ClearAllErrors(Control parent, ErrorProvider errorProvider)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    clsFormTheme.ClearInputError((TextBox)control, errorProvider);
                }
                else if (control.HasChildren)
                {
                    ClearAllErrors(control, errorProvider);
                }
            }
        }

        // ─── Error Messages ─────────────────────────────────────────────────────

        public static class ErrorMessages
        {
            public const string Required = "This field is required.";
            public const string TooShort = "This field is too short.";
            public const string TooLong = "This field is too long.";
            public const string InvalidEmail = "Please enter a valid email address.";
            public const string InvalidPhone = "Please enter a valid phone number.";
            public const string InvalidNumber = "Please enter a valid number.";
            public const string InvalidPositiveNumber = "Please enter a positive number.";
            public const string InvalidInteger = "Please enter a valid integer.";
            public const string InvalidPositiveInteger = "Please enter a positive integer.";
            public const string InvalidProductName = "Product name can only contain letters, numbers, and common symbols.";
            public const string InvalidBarcode = "Barcode must be 8-13 alphanumeric characters.";
            public const string InvalidCategoryName = "Category name can only contain letters, numbers, and spaces.";
            public const string InvalidSupplierName = "Supplier name can only contain letters, numbers, and common symbols.";
            public const string ExceedsMax = "Value exceeds maximum allowed.";
            public const string BelowMin = "Value is below minimum allowed.";
        }
    }
}
