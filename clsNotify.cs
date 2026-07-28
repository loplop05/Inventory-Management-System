using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized notification and message display class.
    /// Provides consistent styling and behavior for user notifications.
    /// </summary>
    public static class clsNotify
    {
        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="title">Optional title for the error dialog. Defaults to "Error".</param>
        public static void Error(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a success message to the user.
        /// </summary>
        /// <param name="message">The success message to display.</param>
        /// <param name="title">Optional title for the success dialog. Defaults to "Success".</param>
        public static void Success(string message, string title = "Success")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays a warning message to the user.
        /// </summary>
        /// <param name="message">The warning message to display.</param>
        /// <param name="title">Optional title for the warning dialog. Defaults to "Warning".</param>
        public static void Warn(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Displays a confirmation dialog to the user.
        /// </summary>
        /// <param name="message">The confirmation message to display.</param>
        /// <param name="title">Optional title for the confirmation dialog. Defaults to "Confirm".</param>
        /// <returns>True if the user clicked Yes, false otherwise.</returns>
        public static bool Confirm(string message, string title = "Confirm")
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}
