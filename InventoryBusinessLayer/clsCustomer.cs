using System;
using System.Data;
using System.Linq;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsCustomer
    {
        public static bool AddCustomer(string phoneNumber, string customerName, out int customerID, out string errorMessage)
        {
            // Validate phone number format (Jordan format: +962 or starting with 07)
            if (!IsValidPhoneNumber(phoneNumber))
            {
                customerID = -1;
                errorMessage = "Invalid phone number format. Use +962XXXXXXXXX or 07XXXXXXXXX format.";
                return false;
            }

            // Validate customer name
            if (string.IsNullOrWhiteSpace(customerName))
            {
                customerID = -1;
                errorMessage = "Customer name cannot be empty.";
                return false;
            }

            if (customerName.Length > 100)
            {
                customerID = -1;
                errorMessage = "Customer name cannot exceed 100 characters.";
                return false;
            }

            return clsCustomerData.AddCustomer(phoneNumber, customerName.Trim(), out customerID, out errorMessage);
        }

        public static bool CustomerExistsByPhone(string phoneNumber)
        {
            return clsCustomerData.CustomerExistsByPhone(phoneNumber);
        }

        public static DataTable GetCustomerByPhone(string phoneNumber)
        {
            return clsCustomerData.GetCustomerByPhone(phoneNumber);
        }

        public static DataTable GetCustomerByID(int customerID)
        {
            return clsCustomerData.GetCustomerByID(customerID);
        }

        public static DataTable GetAllCustomers()
        {
            return clsCustomerData.GetAllCustomers();
        }

        public static bool UpdateCustomer(int customerID, string phoneNumber, string customerName, out string errorMessage)
        {
            // Validate phone number format
            if (!IsValidPhoneNumber(phoneNumber))
            {
                errorMessage = "Invalid phone number format. Use +962XXXXXXXXX or 07XXXXXXXXX format.";
                return false;
            }

            // Validate customer name
            if (string.IsNullOrWhiteSpace(customerName))
            {
                errorMessage = "Customer name cannot be empty.";
                return false;
            }

            if (customerName.Length > 100)
            {
                errorMessage = "Customer name cannot exceed 100 characters.";
                return false;
            }

            return clsCustomerData.UpdateCustomer(customerID, phoneNumber, customerName.Trim(), out errorMessage);
        }

        public static bool DeleteCustomer(int customerID, out string errorMessage)
        {
            return clsCustomerData.DeleteCustomer(customerID, out errorMessage);
        }

        public static bool UpdateLastPurchaseDate(int customerID, out string errorMessage)
        {
            return clsCustomerData.UpdateLastPurchaseDate(customerID, out errorMessage);
        }

        public static DataTable GetCustomerOrders(int customerID)
        {
            return clsCustomerData.GetCustomerOrders(customerID);
        }

        public static DataTable GetOrderDetails(int orderID)
        {
            return clsCustomerData.GetOrderDetails(orderID);
        }

        public static DataTable GetOrderItems(int orderID)
        {
            return clsCustomerData.GetOrderItems(orderID);
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            phoneNumber = phoneNumber.Trim();

            // Check for Jordan phone format: +962XXXXXXXXX or 07XXXXXXXXX
            if (phoneNumber.StartsWith("+962"))
            {
                // +962 followed by 9 digits
                return phoneNumber.Length == 13 && phoneNumber.Substring(3).All(char.IsDigit);
            }
            else if (phoneNumber.StartsWith("07"))
            {
                // 07 followed by 8 digits
                return phoneNumber.Length == 10 && phoneNumber.Substring(2).All(char.IsDigit);
            }

            return false;
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            phoneNumber = phoneNumber.Trim();

            // If starts with 07, convert to +962 format
            if (phoneNumber.StartsWith("07") && phoneNumber.Length == 10)
            {
                return "+962" + phoneNumber.Substring(2);
            }

            return phoneNumber;
        }
    }
}
