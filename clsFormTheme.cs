using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public static class clsFormTheme
    {
        // ─── Modern Professional Palette ────────────────────────────────────────
        // Background tones
        public static readonly Color FormBackColor        = Color.FromArgb(245, 247, 250);  // Cool off-white
        public static readonly Color FormBackColorAlt     = Color.FromArgb(235, 239, 245);  // Slightly deeper

        // Brand / accent
        public static readonly Color PrimaryColor         = Color.FromArgb(37,  99,  235);  // Vivid Royal Blue
        public static readonly Color PrimaryHoverColor    = Color.FromArgb(29,  78,  216);  // Darker blue for hover
        public static readonly Color PrimaryLightColor    = Color.FromArgb(219, 234, 254);  // Light blue tint

        // Secondary / neutral
        public static readonly Color SecondaryColor       = Color.FromArgb(71,  85,  105);  // Slate 600
        public static readonly Color SecondaryHoverColor  = Color.FromArgb(51,  65,  85);   // Slate 700

        // Semantic
        public static readonly Color SuccessColor         = Color.FromArgb(5,   150, 105);  // Emerald 600
        public static readonly Color SuccessHoverColor    = Color.FromArgb(4,   120, 87);   // Emerald 700
        public static readonly Color DangerColor          = Color.FromArgb(220, 38,  38);   // Red 600
        public static readonly Color DangerHoverColor     = Color.FromArgb(185, 28,  28);   // Red 700
        public static readonly Color WarningColor         = Color.FromArgb(217, 119, 6);    // Amber 600
        public static readonly Color InfoColor            = Color.FromArgb(6,   182, 212);  // Cyan 500

        // Surface / structural
        public static readonly Color HeaderColor          = Color.FromArgb(15,  23,  42);   // Slate 900 (near-black)
        public static readonly Color HeaderGradientEnd    = Color.FromArgb(30,  58,  138);  // Blue 900
        public static readonly Color CardColor            = Color.White;
        public static readonly Color CardBorderColor      = Color.FromArgb(226, 232, 240);  // Slate 200
        public static readonly Color RowAltColor          = Color.FromArgb(248, 250, 252);  // Slate 50
        public static readonly Color SelectionBackColor   = Color.FromArgb(219, 234, 254);  // Blue 100
        public static readonly Color SelectionForeColor   = Color.FromArgb(30,  58,  138);  // Blue 900

        // Text
        public static readonly Color TextPrimary          = Color.FromArgb(15,  23,  42);   // Slate 900
        public static readonly Color TextSecondary        = Color.FromArgb(100, 116, 139);  // Slate 500
        public static readonly Color TextMuted            = Color.FromArgb(148, 163, 184);  // Slate 400

        // ─── Typography ─────────────────────────────────────────────────────────
        public static readonly string MainFontName  = "Segoe UI";
        public static readonly string IconFontName  = "Segoe MDL2 Assets";

        // ─── Shared Fonts ────────────────────────────────────────────────────────
        public static readonly Font BodyFont        = new Font("Segoe UI", 10F);
        public static readonly Font BoldFont        = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font HeaderFont      = new Font("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font SubtitleFont    = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font SmallFont       = new Font("Segoe UI", 9F);

        // ─── Icon Codes (Segoe MDL2 Assets) ─────────────────────────────────────
        public static class Icons
        {
            // Navigation
            public const string Home        = "\uE80F";  // Home
            public const string Back        = "\uE72B";  // Back
            public const string Exit        = "\uE8FB";  // ChromeClose (X)

            // Modules
            public const string Categories  = "\uE8FD";  // Tag
            public const string Suppliers   = "\uE716";  // People
            public const string Products    = "\uE7B8";  // Shop
            public const string POS         = "\uE7BF";  // ShoppingCart
            public const string Reports     = "\uE9D2";  // ReportDocument

            // CRUD
            public const string Add         = "\uE710";  // Add
            public const string Delete      = "\uE74D";  // Delete
            public const string Update      = "\uE70F";  // Edit
            public const string Refresh     = "\uE72C";  // Refresh
            public const string Search      = "\uE721";  // Search
            public const string Save        = "\uE74E";  // Save
            public const string Cancel      = "\uE711";  // Cancel

            // Status / misc
            public const string Success     = "\uE73E";  // Accept (checkmark)
            public const string Check       = "\uE73E";  // Accept (checkmark)
            public const string Warning     = "\uE7BA";  // Warning
            public const string Info        = "\uE946";  // Info
            public const string Export      = "\uEDE1";  // Download
            public const string Print       = "\uE749";  // Print
            public const string Filter      = "\uE71C";  // Filter
            public const string Settings    = "\uE713";  // Settings
            public const string Stock       = "\uE8B7";  // AllApps (inventory)
            public const string Money       = "\uE8A4";  // Money
            public const string Calendar    = "\uE787";  // Calendar
            public const string Chart       = "\uE9D9";  // BarChart
            public const string User        = "\uE77B";  // Contact
            public const string Exchange    = "\uE77C";  // Switch
            public const string Plus        = "\uE710";  // Add
        }

        // ════════════════════════════════════════════════════════════════════════
        //  FORM STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>Applies the standard form background, font, and gradient.</summary>
        public static void ApplyFormStyle(Form form)
        {
            form.BackColor        = FormBackColor;
            form.StartPosition    = FormStartPosition.CenterScreen;
            form.Font             = new Font(MainFontName, 10F);
            form.KeyPreview       = true;

            // Subtle diagonal gradient background
            form.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    form.ClientRectangle,
                    FormBackColor,
                    FormBackColorAlt,
                    LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(brush, form.ClientRectangle);
                }
            };
        }

        // ════════════════════════════════════════════════════════════════════════
        //  LABEL / STATIC TEXT STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>Applies standard body text style to a Label (Segoe UI 10pt, TextPrimary).</summary>
        public static void ApplyLabelStyle(Label label)
        {
            label.Font      = BodyFont;
            label.ForeColor = TextPrimary;
            label.BackColor = Color.Transparent;
        }

        /// <summary>Applies a bold header style to a Label (Segoe UI 13pt Bold, HeaderColor).</summary>
        public static void ApplyHeaderLabelStyle(Label label)
        {
            label.Font      = HeaderFont;
            label.ForeColor = HeaderColor;
            label.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Styles a Label as a dashboard stat card — white background, border,
        /// bold Segoe UI text, centered. Used for summary panels.
        /// </summary>
        public static void StyleStatCard(Label label, Color accentColor)
        {
            label.BackColor   = CardColor;
            label.ForeColor   = accentColor;
            label.Font        = SubtitleFont;
            label.TextAlign   = System.Drawing.ContentAlignment.MiddleCenter;
            label.BorderStyle = System.Windows.Forms.BorderStyle.None;

            label.Paint += (s, e) =>
            {
                var lbl = (Label)s;
                // Subtle card border
                using (Pen pen = new Pen(CardBorderColor, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, lbl.Width - 1, lbl.Height - 1);
                // Top accent bar
                using (SolidBrush bar = new SolidBrush(accentColor))
                    e.Graphics.FillRectangle(bar, new Rectangle(0, 0, lbl.Width, 3));
            };
        }

        // ════════════════════════════════════════════════════════════════════════
        //  COMBOBOX STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>Applies Segoe UI font and theme colors to a ComboBox.</summary>
        public static void ApplyComboBoxStyle(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.Font      = BodyFont;
            comboBox.ForeColor = TextPrimary;
            comboBox.BackColor = Color.White;
        }

        /// <summary>
        /// Creates a gradient header panel docked to the top of the form,
        /// with a small icon glyph and a title label.
        /// </summary>
        public static void CreateHeaderPanel(Form form, string title, string iconGlyph = null)
        {
            Panel header = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 64,
                BackColor = HeaderColor   // fallback; gradient is painted below
            };

            // Gradient paint
            header.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    header.ClientRectangle,
                    HeaderColor,
                    HeaderGradientEnd,
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, header.ClientRectangle);
                }

                // Bottom accent line
                using (Pen pen = new Pen(Color.FromArgb(59, 130, 246), 2))
                    e.Graphics.DrawLine(pen, 0, header.Height - 2, header.Width, header.Height - 2);
            };

            int textLeft = 20;

            // Optional icon badge
            if (!string.IsNullOrEmpty(iconGlyph))
            {
                Label iconBadge = new Label
                {
                    Text      = iconGlyph,
                    Font      = new Font(IconFontName, 16F),
                    ForeColor = Color.FromArgb(147, 197, 253),  // Blue 300
                    Location  = new Point(16, 14),
                    AutoSize  = true
                };
                header.Controls.Add(iconBadge);
                textLeft = 52;
            }

            Label lblTitle = new Label
            {
                Text      = title,
                ForeColor = Color.White,
                Font      = new Font(MainFontName, 16F, FontStyle.Bold),
                Location  = new Point(textLeft, 18),
                AutoSize  = true
            };

            header.Controls.Add(lblTitle);
            form.Controls.Add(header);
            header.BringToFront();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  CARD / PANEL DRAWING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>Draws a white card with a soft border and subtle drop-shadow.</summary>
        public static void DrawCard(Graphics g, Rectangle rect, int radius = 6)
        {
            // Shadow
            using (SolidBrush shadow = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                g.FillRectangle(shadow, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height));

            // Card fill
            using (SolidBrush fill = new SolidBrush(CardColor))
                g.FillRectangle(fill, rect);

            // Border
            using (Pen border = new Pen(CardBorderColor, 1))
                g.DrawRectangle(border, rect);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  BUTTON STYLING
        // ════════════════════════════════════════════════════════════════════════

        public static void ApplyPrimaryButtonStyle(Button button, string icon = null)
            => ApplyButtonStyle(button, PrimaryColor, PrimaryHoverColor, icon);

        public static void ApplySecondaryButtonStyle(Button button, string icon = null)
            => ApplyButtonStyle(button, SecondaryColor, SecondaryHoverColor, icon);

        public static void ApplyDangerButtonStyle(Button button, string icon = null)
            => ApplyButtonStyle(button, DangerColor, DangerHoverColor, icon);

        public static void ApplySuccessButtonStyle(Button button, string icon = null)
            => ApplyButtonStyle(button, SuccessColor, SuccessHoverColor, icon);

        public static void ApplyWarningButtonStyle(Button button, string icon = null)
            => ApplyButtonStyle(button, WarningColor, Color.FromArgb(180, 100, 0), icon);

        private static void ApplyButtonStyle(Button button, Color backColor, Color hoverColor, string icon = null)
        {
            button.BackColor                           = backColor;
            button.ForeColor                           = Color.White;
            button.Font                                = new Font(MainFontName, 10F, FontStyle.Bold);
            button.FlatStyle                           = FlatStyle.Flat;
            button.FlatAppearance.BorderSize           = 0;
            button.FlatAppearance.MouseOverBackColor   = hoverColor;
            button.FlatAppearance.MouseDownBackColor   = ControlPaint.Dark(backColor, 0.15f);
            button.Cursor                              = Cursors.Hand;
            button.UseVisualStyleBackColor             = false;
            button.Padding                             = new Padding(10, 0, 10, 0);
            button.TextImageRelation                   = TextImageRelation.ImageBeforeText;
        }

        /// <summary>
        /// Toggles a button between its normal label and a "busy" label,
        /// disabling it while the operation is in progress.
        /// </summary>
        public static void SetButtonBusy(Button button, bool isBusy, string normalText, string busyText)
        {
            button.Enabled = !isBusy;
            button.Text    = isBusy ? busyText : normalText;

            if (isBusy)
            {
                button.BackColor = Color.FromArgb(148, 163, 184);  // Slate 400 — muted while loading
                button.FlatAppearance.MouseOverBackColor = button.BackColor;
            }
            else
            {
                // Restore to the original primary color
                button.BackColor = PrimaryColor;
                button.FlatAppearance.MouseOverBackColor = PrimaryHoverColor;
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  TEXTBOX STYLING
        // ════════════════════════════════════════════════════════════════════════

        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor   = Color.White;
            textBox.ForeColor   = TextPrimary;
            textBox.Font        = new Font(MainFontName, 10F);
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        // ════════════════════════════════════════════════════════════════════════
        //  VALIDATION HELPERS
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Highlights a TextBox in red and sets an error provider message.
        /// </summary>
        public static void ShowInputError(TextBox textBox, ErrorProvider errorProvider, string message)
        {
            textBox.BackColor = Color.FromArgb(254, 226, 226);  // Red 100
            textBox.ForeColor = DangerColor;
            errorProvider.SetError(textBox, message);
        }

        /// <summary>
        /// Clears the error state from a TextBox and resets its appearance.
        /// </summary>
        public static void ClearInputError(TextBox textBox, ErrorProvider errorProvider)
        {
            textBox.BackColor = Color.White;
            textBox.ForeColor = TextPrimary;
            errorProvider.SetError(textBox, string.Empty);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  DATAGRIDVIEW STYLING
        // ════════════════════════════════════════════════════════════════════════

        public static void ApplyGridStyle(DataGridView grid)
        {
            grid.BackgroundColor  = CardColor;
            grid.BorderStyle      = BorderStyle.None;
            grid.CellBorderStyle  = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor        = CardBorderColor;
            grid.EnableHeadersVisualStyles = false;

            // Column headers
            grid.ColumnHeadersDefaultCellStyle.BackColor  = HeaderColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font       = new Font(MainFontName, 10F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Padding    = new Padding(10, 0, 10, 0);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grid.ColumnHeadersHeight      = 44;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row defaults
            grid.DefaultCellStyle.Font                = new Font(MainFontName, 10F);
            grid.DefaultCellStyle.ForeColor           = TextPrimary;
            grid.DefaultCellStyle.BackColor           = CardColor;
            grid.DefaultCellStyle.SelectionBackColor  = SelectionBackColor;
            grid.DefaultCellStyle.SelectionForeColor  = SelectionForeColor;
            grid.DefaultCellStyle.Padding             = new Padding(8, 4, 8, 4);

            // Alternating rows
            grid.AlternatingRowsDefaultCellStyle.BackColor           = RowAltColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor  = SelectionBackColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor  = SelectionForeColor;

            // Row height
            grid.RowTemplate.Height = 36;

            // Misc
            grid.AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect          = false;
            grid.RowHeadersVisible    = false;
            grid.AllowUserToResizeRows = false;
            grid.AllowUserToAddRows   = false;
        }

        // ════════════════════════════════════════════════════════════════════════
        //  POS PRODUCT TILE
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Applies a polished card style to a POS product tile panel,
        /// with a colored top accent bar and hover effect.
        /// </summary>
        public static void StyleProductTile(Panel tile, bool inStock)
        {
            tile.BackColor   = CardColor;
            tile.BorderStyle = BorderStyle.None;
            tile.Cursor      = inStock ? Cursors.Hand : Cursors.Default;

            // Rounded border + top accent via Paint
            tile.Paint += (s, e) =>
            {
                Graphics g    = e.Graphics;
                Rectangle r   = new Rectangle(0, 0, tile.Width - 1, tile.Height - 1);

                // Card fill
                using (SolidBrush fill = new SolidBrush(CardColor))
                    g.FillRectangle(fill, r);

                // Top accent bar
                Color accentColor = inStock ? PrimaryColor : Color.FromArgb(203, 213, 225);
                using (SolidBrush accent = new SolidBrush(accentColor))
                    g.FillRectangle(accent, new Rectangle(0, 0, tile.Width, 4));

                // Border
                using (Pen border = new Pen(CardBorderColor, 1))
                    g.DrawRectangle(border, r);
            };

            // Hover highlight
            tile.MouseEnter += (s, e) =>
            {
                if (inStock) tile.BackColor = Color.FromArgb(239, 246, 255);  // Blue 50
            };
            tile.MouseLeave += (s, e) => tile.BackColor = CardColor;
        }
    }
}
