using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized keyboard shortcut management for the application.
    /// Provides common shortcuts and helper methods for form-specific shortcuts.
    /// </summary>
    public static class clsKeyboardShortcuts
    {
        // ─── Common Shortcuts ────────────────────────────────────────────────
        
        /// <summary>Escape key - Close form/Cancel operation</summary>
        public const Keys ESCAPE = Keys.Escape;
        
        /// <summary>F5 key - Refresh data</summary>
        public const Keys REFRESH = Keys.F5;
        
        /// <summary>F2 key - Edit/Update</summary>
        public const Keys EDIT = Keys.F2;
        
        /// <summary>F3 key - Search</summary>
        public const Keys SEARCH = Keys.F3;
        
        /// <summary>F4 key - Add/New</summary>
        public const Keys ADD = Keys.F4;
        
        /// <summary>Delete key - Delete item</summary>
        public const Keys DELETE = Keys.Delete;
        
        /// <summary>Enter key - Confirm/Submit</summary>
        public const Keys CONFIRM = Keys.Enter;
        
        /// <summary>Ctrl+S - Save</summary>
        public const Keys SAVE = Keys.Control | Keys.S;
        
        /// <summary>Ctrl+F - Find/Search</summary>
        public const Keys FIND = Keys.Control | Keys.F;
        
        /// <summary>Ctrl+N - New item</summary>
        public const Keys NEW = Keys.Control | Keys.N;
        
        /// <summary>Ctrl+P - Print</summary>
        public const Keys PRINT = Keys.Control | Keys.P;
        
        /// <summary>Ctrl+E - Export</summary>
        public const Keys EXPORT = Keys.Control | Keys.E;
        
        /// <summary>Ctrl+R - Refresh</summary>
        public const Keys CTRL_REFRESH = Keys.Control | Keys.R;

        // ─── Helper Methods ────────────────────────────────────────────────────

        /// <summary>
        /// Sets up common keyboard shortcuts for a form.
        /// Call this in the form's constructor or Load event.
        /// </summary>
        /// <param name="form">The form to add shortcuts to</param>
        /// <param name="onEscape">Action to perform on Escape (close/cancel)</param>
        /// <param name="onRefresh">Action to perform on F5 (refresh)</param>
        /// <param name="onSearch">Action to perform on F3 or Ctrl+F (search)</param>
        /// <param name="onAdd">Action to perform on F4 or Ctrl+N (add new)</param>
        public static void SetupCommonShortcuts(
            Form form,
            Action onEscape = null,
            Action onRefresh = null,
            Action onSearch = null,
            Action onAdd = null)
        {
            if (form == null) return;

            form.KeyPreview = true;
            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape && onEscape != null)
                {
                    onEscape();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.F5 && onRefresh != null)
                {
                    onRefresh();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if ((e.KeyCode == Keys.F3 || (e.Control && e.KeyCode == Keys.F)) && onSearch != null)
                {
                    onSearch();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if ((e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.N)) && onAdd != null)
                {
                    onAdd();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        /// <summary>
        /// Gets a tooltip text for a keyboard shortcut.
        /// Useful for displaying shortcuts in tooltips or help text.
        /// </summary>
        public static string GetShortcutText(Keys shortcut)
        {
            if (shortcut == Keys.Escape) return "Esc";
            if (shortcut == Keys.F5) return "F5";
            if (shortcut == Keys.F2) return "F2";
            if (shortcut == Keys.F3) return "F3";
            if (shortcut == Keys.F4) return "F4";
            if (shortcut == Keys.Delete) return "Del";
            if (shortcut == Keys.Enter) return "Enter";
            
            // Handle modifier keys
            string text = "";
            if ((shortcut & Keys.Control) == Keys.Control) text += "Ctrl+";
            if ((shortcut & Keys.Alt) == Keys.Alt) text += "Alt+";
            if ((shortcut & Keys.Shift) == Keys.Shift) text += "Shift+";
            
            Keys key = shortcut & Keys.KeyCode;
            text += key.ToString();
            
            return text;
        }

        /// <summary>
        /// Creates a help text string showing all available shortcuts for a form.
        /// </summary>
        public static string GetHelpText()
        {
            return @"Common Keyboard Shortcuts:
─────────────────────────────
Esc        - Close form / Cancel
F5         - Refresh data
F2         - Edit selected item
F3         - Search / Find
F4         - Add new item
Del        - Delete selected item
Enter      - Confirm / Submit
Ctrl+S     - Save
Ctrl+F     - Find
Ctrl+N     - New item
Ctrl+P     - Print
Ctrl+E     - Export
Ctrl+R     - Refresh";
    }
    }
}
