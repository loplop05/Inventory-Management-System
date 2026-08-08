using System;
using System.Windows.Forms;
using System.Drawing;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized help system for user guidance and tooltips.
    /// Provides context-sensitive help, tooltips, and user documentation.
    /// </summary>
    public static class clsHelpSystem
    {
        private static ToolTip _globalToolTip = new ToolTip
        {
            AutoPopDelay = 5000,
            InitialDelay = 500,
            ReshowDelay = 100,
            ShowAlways = true
        };

        // ─── Help Topics ────────────────────────────────────────────────────────

        public static class Topics
        {
            public const string MainMenu = "MainMenu";
            public const string Products = "Products";
            public const string Categories = "Categories";
            public const string Suppliers = "Suppliers";
            public const string POS = "POS";
            public const string Receipts = "Receipts";
            public const string Reports = "Reports";
            public const string AddProduct = "AddProduct";
            public const string UpdateProduct = "UpdateProduct";
            public const string DeleteProduct = "DeleteProduct";
            public const string AddCategory = "AddCategory";
            public const string AddSupplier = "AddSupplier";
            public const string Search = "Search";
            public const string KeyboardShortcuts = "KeyboardShortcuts";
        }

        // ─── Help Content ───────────────────────────────────────────────────────

        private static readonly System.Collections.Generic.Dictionary<string, string> _helpContent =
            new System.Collections.Generic.Dictionary<string, string>
            {
                {
                    Topics.MainMenu,
                    @"Main Menu Help
═══════════════════════════════════════════════════════════
The Main Menu provides quick access to all system modules:

• Categories - Manage product categories
• Suppliers - Manage supplier information
• Products - Manage product inventory
• POS - Point of Sale for processing orders
• Receipt Search - Search and view order receipts
• Print Receipt - Print specific order receipts

Keyboard Shortcuts:
• Esc - Close application
• F1 - Show this help"
                },
                {
                    Topics.Products,
                    @"Products Management Help
═══════════════════════════════════════════════════════════
Manage your product inventory from this screen:

Actions:
• Add - Create a new product
• Delete - Remove a product (requires confirmation)
• Update - Edit product details
• Stock Report - View stock valuation report

Navigation:
• Use search box to filter products
• Use Prev/Next buttons to navigate pages
• Click Refresh to reload data

Keyboard Shortcuts:
• F5 - Refresh data
• F3 - Focus search box
• F4 - Add new product
• Esc - Close form"
                },
                {
                    Topics.Categories,
                    @"Categories Management Help
═══════════════════════════════════════════════════════════
Manage product categories from this screen:

Actions:
• Add - Create a new category
• Delete - Remove a category (requires confirmation)
• Update - Edit category details

Navigation:
• Use search box to filter categories
• Use Prev/Next buttons to navigate pages
• Click Refresh to reload data

Keyboard Shortcuts:
• F5 - Refresh data
• F3 - Focus search box
• F4 - Add new category
• Esc - Close form"
                },
                {
                    Topics.Suppliers,
                    @"Suppliers Management Help
═══════════════════════════════════════════════════════════
Manage supplier information from this screen:

Actions:
• Add - Register a new supplier
• Delete - Remove a supplier (requires confirmation)
• Update - Edit supplier details

Navigation:
• Use search box to filter suppliers
• Use Prev/Next buttons to navigate pages
• Click Refresh to reload data

Keyboard Shortcuts:
• F5 - Refresh data
• F3 - Focus search box
• F4 - Add new supplier
• Esc - Close form"
                },
                {
                    Topics.POS,
                    @"Point of Sale Help
═══════════════════════════════════════════════════════════
Process customer orders from this screen:

Workflow:
1. Search for products using the search box
2. Click products to add them to the receipt
3. Enter customer phone number (optional)
4. Select payment method (Cash/Visa)
5. Click Complete Order to finalize

Features:
• Products are organized by category tabs
• Low stock items are disabled
• Real-time receipt calculation
• Customer history lookup

Keyboard Shortcuts:
• F5 - Refresh products
• Esc - Close form
• Enter - Complete order"
                },
                {
                    Topics.Receipts,
                    @"Receipt Search Help
═══════════════════════════════════════════════════════════
Search and view order receipts:

Actions:
• Search - Find receipt by Order ID
• By Phone - Find receipts by customer phone
• Exchange - Process product exchange

Navigation:
• Enter Order ID to search specific receipt
• Enter phone number to view customer history
• Click on receipt items to view details

Keyboard Shortcuts:
• F3 - Focus search box
• Esc - Close form"
                },
                {
                    Topics.Reports,
                    @"Reports Help
═══════════════════════════════════════════════════════════
View business performance reports:

Available Reports:
• Daily Sales Report - Today's sales and top products
• Stock Valuation Report - Current inventory value

Features:
• Export reports to CSV
• Print reports
• Refresh data

Keyboard Shortcuts:
• F5 - Refresh report
• Ctrl+E - Export to CSV
• Ctrl+P - Print report
• Esc - Close form"
                },
                {
                    Topics.AddProduct,
                    @"Add Product Help
═══════════════════════════════════════════════════════════
Add a new product to inventory:

Required Fields:
• Product Name - Name of the product
• Price - Selling price (must be positive)
• Quantity - Stock quantity (must be positive)
• Barcode - Product barcode (8-13 alphanumeric)
• Category - Select from dropdown
• Supplier - Select from dropdown

Validation:
• Product name can contain letters, numbers, and common symbols
• Barcode must be 8-13 alphanumeric characters
• Price and quantity must be positive numbers

Keyboard Shortcuts:
• Enter - Save product
• Esc - Cancel and close"
                },
                {
                    Topics.KeyboardShortcuts,
                    clsKeyboardShortcuts.GetHelpText()
                }
            };

        // ─── Tooltip Management ────────────────────────────────────────────────

        /// <summary>
        /// Sets a tooltip for a control with auto-hide.
        /// </summary>
        public static void SetToolTip(Control control, string text)
        {
            if (control == null || string.IsNullOrWhiteSpace(text)) return;
            _globalToolTip.SetToolTip(control, text);
        }

        /// <summary>
        /// Sets a tooltip with a title (shown in bold).
        /// </summary>
        public static void SetToolTip(Control control, string title, string text)
        {
            if (control == null) return;
            string tooltip = string.IsNullOrWhiteSpace(title) ? text : $"{title}\n{text}";
            _globalToolTip.SetToolTip(control, tooltip);
        }

        /// <summary>
        /// Removes tooltip from a control.
        /// </summary>
        public static void RemoveToolTip(Control control)
        {
            if (control == null) return;
            _globalToolTip.SetToolTip(control, string.Empty);
        }

        // ─── Help Display ─────────────────────────────────────────────────────

        /// <summary>
        /// Shows help for a specific topic in a message box.
        /// </summary>
        public static void ShowHelp(string topic)
        {
            if (_helpContent.ContainsKey(topic))
            {
                clsFormTheme.ShowInfo(null, _helpContent[topic], "Help - " + topic);
            }
            else
            {
                clsFormTheme.ShowWarning(null, "Help topic not found.", "Help");
            }
        }

        /// <summary>
        /// Shows help in a custom form (better formatting).
        /// </summary>
        public static void ShowHelpForm(string topic)
        {
            if (!_helpContent.ContainsKey(topic))
            {
                clsFormTheme.ShowWarning(null, "Help topic not found.", "Help");
                return;
            }

            var helpForm = new Form
            {
                Text = "Help - " + topic,
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Text = _helpContent[topic],
                Font = new Font("Consolas", 10F),
                BackColor = Color.FromArgb(245, 247, 250),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10)
            };

            var closeButton = new Button
            {
                Text = "Close",
                Dock = DockStyle.Bottom,
                Height = 40,
                FlatStyle = FlatStyle.Flat
            };
            clsFormTheme.ApplySecondaryButtonStyle(closeButton, clsFormTheme.Icons.Exit);
            closeButton.Click += (s, e) => helpForm.Close();

            helpForm.Controls.Add(textBox);
            helpForm.Controls.Add(closeButton);

            helpForm.ShowDialog();
        }

        /// <summary>
        /// Gets help text for a topic.
        /// </summary>
        public static string GetHelpText(string topic)
        {
            return _helpContent.ContainsKey(topic) ? _helpContent[topic] : "Help topic not found.";
        }

        // ─── Context-Sensitive Help ───────────────────────────────────────────

        /// <summary>
        /// Sets up F1 help for a form.
        /// </summary>
        public static void SetupHelp(Form form, string topic)
        {
            if (form == null) return;

            form.KeyPreview = true;
            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.F1)
                {
                    ShowHelpForm(topic);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        /// <summary>
        /// Adds a help button to a form that shows help for a topic.
        /// </summary>
        public static Button AddHelpButton(Form form, string topic, Point location)
        {
            if (form == null) return null;

            var helpButton = new Button
            {
                Text = "?",
                Size = new Size(30, 30),
                Location = location,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = clsFormTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            helpButton.FlatAppearance.BorderSize = 0;

            helpButton.Click += (s, e) => ShowHelpForm(topic);
            SetToolTip(helpButton, "Press F1 or click for help");

            form.Controls.Add(helpButton);
            return helpButton;
        }

        // ─── Common Tooltips ───────────────────────────────────────────────────

        /// <summary>
        /// Sets common tooltips for standard form elements.
        /// </summary>
        public static void SetCommonTooltips(
            TextBox searchBox = null,
            Button addButton = null,
            Button deleteButton = null,
            Button updateButton = null,
            Button refreshButton = null)
        {
            if (searchBox != null)
                SetToolTip(searchBox, "Search", "Type to filter results. Press F3 to focus.");

            if (addButton != null)
                SetToolTip(addButton, "Add New", "Create a new record. Press F4.");

            if (deleteButton != null)
                SetToolTip(deleteButton, "Delete", "Remove selected record. Press Delete.");

            if (updateButton != null)
                SetToolTip(updateButton, "Update", "Edit selected record. Press F2.");

            if (refreshButton != null)
                SetToolTip(refreshButton, "Refresh", "Reload data. Press F5.");
        }
    }
}
