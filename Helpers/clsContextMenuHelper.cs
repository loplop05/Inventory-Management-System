using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Helper class for creating and managing context menus on DataGridView controls.
    /// Provides standard Add, Update, Delete operations for record management.
    /// </summary>
    public static class clsContextMenuHelper
    {
        /// <summary>
        /// Creates and attaches a context menu to a DataGridView with Add, Update, and Delete options.
        /// </summary>
        /// <param name="grid">The DataGridView to attach thecontext menu to.</param>
        /// <param name="onAdd">Action to perform when Add is clicked.</param>
        /// <param name="onUpdate">Action to perform when Update is clicked.</param>
        /// <param name="onDelete">Action to perform when Delete is clicked.</param>
        /// <param name="enableAdd">Whether to show the Add option (default: true).</param>
        /// <param name="enableUpdate">Whether to show the Update option (default: true).</param>
        /// <param name="enableDelete">Whether to show the Delete option (default: true).</param>
        public static void AttachDataGridViewContextMenu(
            DataGridView grid,
            Action onAdd,
            Action onUpdate,
            Action onDelete,
            bool enableAdd = true,
            bool enableUpdate = true,
            bool enableDelete = true)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));

            var contextMenu = new ContextMenuStrip();

            // Add menu item
            if (enableAdd && onAdd != null)
            {
                var addMenuItem = new ToolStripMenuItem("Add New Record");
                addMenuItem.Click += (s, e) => onAdd();
                contextMenu.Items.Add(addMenuItem);
            }

            // Separator
            if (enableAdd && (enableUpdate || enableDelete))
            {
                contextMenu.Items.Add(new ToolStripSeparator());
            }

            // Update menu item
            if (enableUpdate && onUpdate != null)
            {
                var updateMenuItem = new ToolStripMenuItem("Update Selected Record");
                updateMenuItem.Click += (s, e) =>
                {
                    if (grid.SelectedRows.Count > 0)
                    {
                        onUpdate();
                    }
                    else
                    {
                        clsNotify.Warn("Please select a record to update.");
                    }
                };
                contextMenu.Items.Add(updateMenuItem);
            }

            // Delete menu item
            if (enableDelete && onDelete != null)
            {
                var deleteMenuItem = new ToolStripMenuItem("Delete Selected Record");
                deleteMenuItem.Click += (s, e) =>
                {
                    if (grid.SelectedRows.Count > 0)
                    {
                        onDelete();
                    }
                    else
                    {
                        clsNotify.Warn("Please select a record to delete.");
                    }
                };
                contextMenu.Items.Add(deleteMenuItem);
            }

            // Attach to grid
            grid.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Creates and attaches a context menu to a DataGridView with custom menu items.
        /// </summary>
        /// <param name="grid">The DataGridView to attach the context menu to.</param>
        /// <param name="menuItems">Array of custom menu items to add.</param>
        public static void AttachCustomDataGridViewContextMenu(
            DataGridView grid,
            params ToolStripMenuItem[] menuItems)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));

            var contextMenu = new ContextMenuStrip();

            foreach (var item in menuItems)
            {
                contextMenu.Items.Add(item);
            }

            // Attach to grid
            grid.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Creates a standard context menu item with click handler.
        /// </summary>
        /// <param name="text">Menu item text.</param>
        /// <param name="onClick">Click event handler.</param>
        /// <returns>Configured ToolStripMenuItem.</returns>
        public static ToolStripMenuItem CreateMenuItem(string text, EventHandler onClick)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += onClick;
            return item;
        }
    }
}
