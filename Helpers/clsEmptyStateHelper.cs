using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Helper class for displaying consistent empty-state messages in DataGridView controls.
    /// </summary>
    public static class clsEmptyStateHelper
    {
        /// <summary>
        /// Sets up a DataGridView to display an empty state message when no data is present.
        /// </summary>
        /// <param name="grid">The DataGridView to configure</param>
        /// <param name="emptyMessage">The message to display when empty</param>
        public static void SetupEmptyState(DataGridView grid, string emptyMessage = "No data available")
        {
            if (grid == null) return;

            // Store the empty message in the Tag for use in Paint event
            grid.Tag = emptyMessage;
            grid.Paint += Grid_Paint;
        }

        private static void Grid_Paint(object sender, PaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null) return;

            // Only show empty state if there are no rows
            if (grid.RowCount == 0 || (grid.RowCount == 1 && grid.NewRowIndex == 0))
            {
                string message = grid.Tag?.ToString() ?? "No data available";
                
                using (Font font = new Font("Segoe UI", 12F, FontStyle.Italic))
                using (Brush brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                {
                    SizeF textSize = e.Graphics.MeasureString(message, font);
                    float x = (grid.Width - textSize.Width) / 2;
                    float y = (grid.Height - textSize.Height) / 2;
                    
                    e.Graphics.DrawString(message, font, brush, x, y);
                }
            }
        }

        /// <summary>
        /// Clears the empty state configuration from a DataGridView.
        /// </summary>
        /// <param name="grid">The DataGridView to clear</param>
        public static void ClearEmptyState(DataGridView grid)
        {
            if (grid == null) return;
            grid.Paint -= Grid_Paint;
            grid.Tag = null;
        }

        /// <summary>
        /// Updates the empty state message for a DataGridView.
        /// </summary>
        /// <param name="grid">The DataGridView to update</param>
        /// <param name="emptyMessage">The new empty message</param>
        public static void UpdateEmptyMessage(DataGridView grid, string emptyMessage)
        {
            if (grid == null) return;
            grid.Tag = emptyMessage;
            grid.Invalidate();
        }

        /// <summary>
        /// Standard empty messages for common scenarios.
        /// </summary>
        public static class Messages
        {
            public const string NoProducts = "No products found";
            public const string NoCustomers = "No customers found";
            public const string NoCategories = "No categories found";
            public const string NoSuppliers = "No suppliers found";
            public const string NoOrders = "No orders found";
            public const string NoResults = "No results match your search";
            public const string NoData = "No data available";
            public const string NoLowStock = "No low stock items";
            public const string NoRecentActivity = "No recent activity";
            public const string NoAuditLogs = "No audit logs found";
        }
    }
}
