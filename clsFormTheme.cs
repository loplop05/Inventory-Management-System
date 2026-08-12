using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public static class clsFormTheme
    {
        // ─── Theme Mode ─────────────────────────────────────────────────────────
        public static bool IsDarkMode { get; private set; } = false;

        /// <summary>
        /// Formats a decimal amount as currency using the current culture.
        /// </summary>
        public static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Toggles between light and dark mode.
        /// </summary>
        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            SaveThemePreference();
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Sets the theme mode explicitly.
        /// </summary>
        public static void SetTheme(bool darkMode)
        {
            IsDarkMode = darkMode;
            SaveThemePreference();
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        public static event EventHandler ThemeChanged;

        private static void SaveThemePreference()
        {
            try
            {
                Properties.Settings.Default.DarkMode = IsDarkMode;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Settings may not be configured yet
            }
        }

        private static void LoadThemePreference()
        {
            try
            {
                IsDarkMode = Properties.Settings.Default.DarkMode;
            }
            catch
            {
                IsDarkMode = false; // Default to light mode
            }
        }

        static clsFormTheme()
        {
            LoadThemePreference();
        }

        // ─── Light Mode Palette ───────────────────────────────────────────────────
        // Background tones
        public static readonly Color LightFormBackColor        = Color.FromArgb(245, 247, 250);  // Cool off-white
        public static readonly Color LightFormBackColorAlt     = Color.FromArgb(235, 239, 245);  // Slightly deeper

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
        public static readonly Color SuccessLightColor    = Color.FromArgb(209, 250, 229);  // Emerald 100
        public static readonly Color DangerColor          = Color.FromArgb(220, 38,  38);   // Red 600
        public static readonly Color DangerHoverColor     = Color.FromArgb(185, 28,  28);   // Red 700
        public static readonly Color DangerLightColor     = Color.FromArgb(254, 226, 226);  // Red 100
        public static readonly Color WarningColor         = Color.FromArgb(217, 119, 6);    // Amber 600
        public static readonly Color WarningLightColor    = Color.FromArgb(254, 243, 199);  // Amber 100
        public static readonly Color InfoColor            = Color.FromArgb(6,   182, 212);  // Cyan 500

        // Surface / structural
        public static readonly Color LightHeaderColor          = Color.FromArgb(15,  23,  42);   // Slate 900 (near-black)
        public static readonly Color LightHeaderGradientEnd    = Color.FromArgb(30,  58,  138);  // Blue 900
        public static readonly Color LightCardColor            = Color.White;
        public static readonly Color LightCardBorderColor      = Color.FromArgb(226, 232, 240);  // Slate 200
        public static readonly Color LightRowAltColor          = Color.FromArgb(248, 250, 252);  // Slate 50
        public static readonly Color LightSelectionBackColor   = Color.FromArgb(219, 234, 254);  // Blue 100
        public static readonly Color LightSelectionForeColor   = Color.FromArgb(30,  58,  138);  // Blue 900

        // Text
        public static readonly Color LightTextPrimary          = Color.FromArgb(15,  23,  42);   // Slate 900
        public static readonly Color LightTextSecondary        = Color.FromArgb(100, 116, 139);  // Slate 500
        public static readonly Color LightTextMuted            = Color.FromArgb(148, 163, 184);  // Slate 400

        // ─── Dark Mode Palette ────────────────────────────────────────────────────
        // Background tones
        public static readonly Color DarkFormBackColor        = Color.FromArgb(30,  41,  59);   // Slate 900
        public static readonly Color DarkFormBackColorAlt     = Color.FromArgb(51,  65,  85);   // Slate 700

        // Brand / accent (same as light mode for consistency)
        public static readonly Color DarkPrimaryColor         = Color.FromArgb(96,  165, 250);  // Lighter blue for dark mode
        public static readonly Color DarkPrimaryHoverColor    = Color.FromArgb(59,  130,  246);  // Blue 500
        public static readonly Color DarkPrimaryLightColor    = Color.FromArgb(37,  99,  235);  // Blue 600

        // Secondary / neutral
        public static readonly Color DarkSecondaryColor       = Color.FromArgb(148, 163, 184);  // Slate 400
        public static readonly Color DarkSecondaryHoverColor  = Color.FromArgb(203, 213, 225);  // Slate 300

        // Semantic (adjusted for dark mode)
        public static readonly Color DarkSuccessColor         = Color.FromArgb(74,  222, 128);  // Emerald 400
        public static readonly Color DarkSuccessHoverColor    = Color.FromArgb(52,  211, 153);  // Emerald 500
        public static readonly Color DarkSuccessLightColor    = Color.FromArgb(6,   95,  70);   // Emerald 900
        public static readonly Color DarkDangerColor          = Color.FromArgb(248, 113, 113);  // Red 400
        public static readonly Color DarkDangerHoverColor     = Color.FromArgb(239, 68,  68);   // Red 500
        public static readonly Color DarkDangerLightColor     = Color.FromArgb(127, 29,  29);   // Red 900
        public static readonly Color DarkWarningColor         = Color.FromArgb(251, 191, 36);  // Amber 400
        public static readonly Color DarkWarningLightColor    = Color.FromArgb(120, 53,  15);   // Amber 900
        public static readonly Color DarkInfoColor            = Color.FromArgb(34,  211, 238);  // Cyan 400

        // Surface / structural
        public static readonly Color DarkHeaderColor          = Color.FromArgb(15,  23,  42);   // Slate 950
        public static readonly Color DarkHeaderGradientEnd    = Color.FromArgb(30,  58,  138);  // Blue 900
        public static readonly Color DarkCardColor            = Color.FromArgb(51,  65,  85);   // Slate 700
        public static readonly Color DarkCardBorderColor      = Color.FromArgb(71,  85,  105);  // Slate 600
        public static readonly Color DarkRowAltColor          = Color.FromArgb(71,  85,  105);  // Slate 600
        public static readonly Color DarkSelectionBackColor   = Color.FromArgb(37,  99,  235);  // Blue 600
        public static readonly Color DarkSelectionForeColor   = Color.FromArgb(255, 255, 255);  // White

        // Text
        public static readonly Color DarkTextPrimary          = Color.FromArgb(248, 250, 252);  // Slate 50
        public static readonly Color DarkTextSecondary        = Color.FromArgb(203, 213, 225);  // Slate 300
        public static readonly Color DarkTextMuted            = Color.FromArgb(148, 163, 184);  // Slate 400

        // ─── Current Theme Colors (computed based on mode) ─────────────────────────
        public static Color FormBackColor => IsDarkMode ? DarkFormBackColor : LightFormBackColor;
        public static Color FormBackColorAlt => IsDarkMode ? DarkFormBackColorAlt : LightFormBackColorAlt;
        public static Color HeaderColor => IsDarkMode ? DarkHeaderColor : LightHeaderColor;
        public static Color HeaderGradientEnd => IsDarkMode ? DarkHeaderGradientEnd : LightHeaderGradientEnd;
        public static Color CardColor => IsDarkMode ? DarkCardColor : LightCardColor;
        public static Color CardBorderColor => IsDarkMode ? DarkCardBorderColor : LightCardBorderColor;
        public static Color RowAltColor => IsDarkMode ? DarkRowAltColor : LightRowAltColor;
        public static Color SelectionBackColor => IsDarkMode ? DarkSelectionBackColor : LightSelectionBackColor;
        public static Color SelectionForeColor => IsDarkMode ? DarkSelectionForeColor : LightSelectionForeColor;
        public static Color TextPrimary => IsDarkMode ? DarkTextPrimary : LightTextPrimary;
        public static Color TextSecondary => IsDarkMode ? DarkTextSecondary : LightTextSecondary;
        public static Color TextMuted => IsDarkMode ? DarkTextMuted : LightTextMuted;
        public static Color CurrentPrimaryColor => IsDarkMode ? DarkPrimaryColor : PrimaryColor;
        public static Color CurrentPrimaryHoverColor => IsDarkMode ? DarkPrimaryHoverColor : PrimaryHoverColor;
        public static Color CurrentPrimaryLightColor => IsDarkMode ? DarkPrimaryLightColor : PrimaryLightColor;
        public static Color CurrentSecondaryColor => IsDarkMode ? DarkSecondaryColor : SecondaryColor;
        public static Color CurrentSecondaryHoverColor => IsDarkMode ? DarkSecondaryHoverColor : SecondaryHoverColor;
        public static Color CurrentSuccessColor => IsDarkMode ? DarkSuccessColor : SuccessColor;
        public static Color CurrentSuccessHoverColor => IsDarkMode ? DarkSuccessHoverColor : SuccessHoverColor;
        public static Color CurrentSuccessLightColor => IsDarkMode ? DarkSuccessLightColor : SuccessLightColor;
        public static Color CurrentDangerColor => IsDarkMode ? DarkDangerColor : DangerColor;
        public static Color CurrentDangerHoverColor => IsDarkMode ? DarkDangerHoverColor : DangerHoverColor;
        public static Color CurrentDangerLightColor => IsDarkMode ? DarkDangerLightColor : DangerLightColor;
        public static Color CurrentWarningColor => IsDarkMode ? DarkWarningColor : WarningColor;
        public static Color CurrentWarningLightColor => IsDarkMode ? DarkWarningLightColor : WarningLightColor;
        public static Color CurrentInfoColor => IsDarkMode ? DarkInfoColor : InfoColor;

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
            public const string Info        = "\uE946";  // Info (also used for Help)
            public const string Export      = "\uEDE1";  // Download
            public const string Print       = "\uE749";  // Print
            public const string Filter      = "\uE71C";  // Filter
            public const string Settings    = "\uE713";  // Settings
            public const string Stock       = "\uE8B7";  // AllApps (inventory)
            public const string Money       = "\uE8A4";  // Money
            public const string Calendar    = "\uE787";  // Calendar
            public const string Chart       = "\uE9D9";  // BarChart
            public const string User        = "\uE77B";  // Contact (for User Management)
            public const string Customer    = "\uE8D7";  // ContactCard (for Customer Management)
            public const string Coupon      = "\uE8EC";  // Tag (for Coupon Manager)
            public const string AuditLog    = "\uE81C";  // Clock/History (for Audit Logs)
            public const string History     = "\uE81C";  // Clock/History (for Shift History)
            public const string Close       = "\uE8FB";  // ChromeClose (X)
            public const string Exchange    = "\uE77C";  // Switch
            public const string Plus        = "\uE710";  // Add
            public const string Share       = "\uE72D";  // Share
            public const string Email       = "\uE715";  // Mail
            public const string Copy        = "\uE8C8";  // Copy
            public const string Moon        = "\uE708";  // Bulb (for dark mode)
            public const string Sun         = "\uE706";  // Lightbulb (for light mode)
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
            // Reserve room for the header so docked content starts below it. The header
            // itself is anchored (not docked) so it always sits flush with the top of the
            // form, no matter when it is created relative to the form content.
            form.Padding = new Padding(form.Padding.Left, 64, form.Padding.Right, form.Padding.Bottom);

            Panel header = new Panel
            {
                Location  = new Point(0, 0),
                Width     = form.ClientSize.Width,
                Height    = 64,
                Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
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
                Text      = clsLanguageManager.GetString(title),
                ForeColor = Color.White,
                Font      = new Font(MainFontName, 16F, FontStyle.Bold),
                Location  = new Point(textLeft, 18),
                AutoSize  = true
            };

            // Language Toggle Switcher Button
            Button btnLangToggle = new Button
            {
                Text = (clsLanguageManager.CurrentLanguage == AppLanguage.Arabic) ? "🌐 العربية (AR)" : "🌐 English (EN)",
                Font = new Font(MainFontName, 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(59, 130, 246),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 32),
                Location = new Point(form.ClientSize.Width - 146, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnLangToggle.FlatAppearance.BorderSize = 0;
            btnLangToggle.Click += (s, e) =>
            {
                AppLanguage lang = clsLanguageManager.ToggleLanguage();
                btnLangToggle.Text = (lang == AppLanguage.Arabic) ? "🌐 العربية (AR)" : "🌐 English (EN)";
                lblTitle.Text = clsLanguageManager.GetString(title);
                clsLanguageManager.ApplyLanguage(form);
            };

            clsLanguageManager.LanguageChanged += (s, e) =>
            {
                if (!form.IsDisposed)
                {
                    btnLangToggle.Text = (clsLanguageManager.CurrentLanguage == AppLanguage.Arabic) ? "🌐 العربية (AR)" : "🌐 English (EN)";
                    lblTitle.Text = clsLanguageManager.GetString(title);
                }
            };

            header.Controls.Add(lblTitle);
            header.Controls.Add(btnLangToggle);
            form.Controls.Add(header);
            header.BringToFront();
        }

        /// <summary>
        /// Creates an app header panel with search box and icon row.
        /// This is an overload of CreateHeaderPanel for the new design system.
        /// </summary>
        public static void CreateAppHeaderPanel(Form form, string searchPlaceholder, Action<string> onSearch)
        {
            // Reserve room for the header
            form.Padding = new Padding(form.Padding.Left, 70, form.Padding.Right, form.Padding.Bottom);

            Panel header = new Panel
            {
                Location  = new Point(0, 0),
                Width     = form.ClientSize.Width,
                Height    = 70,
                Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = HeaderColor
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
            };

            // App name/logo on left
            Label lblAppName = new Label
            {
                Text = ConfigurationManager.AppSettings["AppName"] ?? "ElectroPOS Pro",
                ForeColor = Color.White,
                Font = new Font(MainFontName, 16F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            header.Controls.Add(lblAppName);

            // Centered search box
            TextBox txtSearch = new TextBox
            {
                Width = 400,
                Height = 36,
                Location = new Point((header.ClientSize.Width - 400) / 2, 17),
                Font = new Font(MainFontName, 10F),
                Text = searchPlaceholder,
                ForeColor = TextMuted,
                BackColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == searchPlaceholder)
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = TextPrimary;
                }
            };
            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = searchPlaceholder;
                    txtSearch.ForeColor = TextMuted;
                }
            };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != searchPlaceholder)
                {
                    onSearch?.Invoke(txtSearch.Text);
                }
            };
            header.Controls.Add(txtSearch);

            // Right-aligned icon row
            int iconRight = header.ClientSize.Width - 20;
            int iconSpacing = 40;

            // Notification bell
            Label lblNotification = new Label
            {
                Text = Icons.Info,
                Font = new Font(IconFontName, 16F),
                ForeColor = Color.White,
                Location = new Point(iconRight - iconSpacing * 2, 22),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            header.Controls.Add(lblNotification);

            // Settings gear
            Label lblSettings = new Label
            {
                Text = Icons.Settings,
                Font = new Font(IconFontName, 16F),
                ForeColor = Color.White,
                Location = new Point(iconRight - iconSpacing, 22),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            header.Controls.Add(lblSettings);

            // Avatar placeholder
            Panel avatarPanel = new Panel
            {
                Width = 36,
                Height = 36,
                Location = new Point(iconRight + 10, 17),
                BackColor = PrimaryColor,
                Cursor = Cursors.Hand
            };
            avatarPanel.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                using (Font font = new Font(MainFontName, 12F, FontStyle.Bold))
                {
                    e.Graphics.DrawString("U", font, brush, 10, 8);
                }
            };
            header.Controls.Add(avatarPanel);

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

            // Store icon for custom rendering
            if (!string.IsNullOrEmpty(icon))
            {
                button.Tag = icon;
                button.Paint -= ButtonIcon_Paint; // avoid double subscribe
                button.Paint += ButtonIcon_Paint;
            }
        }

        private static void ButtonIcon_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null || string.IsNullOrEmpty(btn.Tag.ToString()))
                return;

            string icon = btn.Tag.ToString();
            string text = btn.Text;

            using (Font iconFont = new Font(IconFontName, 12F))
            using (Font textFont = new Font(MainFontName, 10F, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(btn.Enabled ? btn.ForeColor : TextMuted))
            {
                SizeF iconSize = e.Graphics.MeasureString(icon, iconFont);
                SizeF textSize = e.Graphics.MeasureString(text, textFont);

                const float gap = 6f;
                float totalWidth = iconSize.Width + gap + textSize.Width;
                float startX = (btn.ClientSize.Width - totalWidth) / 2f;
                float centerY = btn.ClientSize.Height / 2f;

                var iconRect = new RectangleF(startX, centerY - iconSize.Height / 2f, iconSize.Width, iconSize.Height);
                var textRect = new RectangleF(startX + iconSize.Width + gap, centerY - textSize.Height / 2f, textSize.Width, textSize.Height);

                // Clear the default text rendering
                e.Graphics.Clear(btn.BackColor);

                // Draw icon
                e.Graphics.DrawString(icon, iconFont, brush, iconRect);

                // Draw text
                e.Graphics.DrawString(text, textFont, brush, textRect);
            }
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
        /// Highlights any input control in red and sets an error provider message.
        /// </summary>
        public static void ShowInputError(Control control, ErrorProvider errorProvider, string message)
        {
            control.BackColor = Color.FromArgb(254, 226, 226);  // Red 100
            control.ForeColor = DangerColor;
            errorProvider.SetError(control, message);
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

        /// <summary>
        /// Applies grid styling with dark header (navy background, white text).
        /// This is a variant of ApplyGridStyle with overridden header colors.
        /// </summary>
        public static void ApplyDarkHeaderGridStyle(DataGridView grid)
        {
            // Apply base grid styling first
            ApplyGridStyle(grid);

            // Override header colors to dark navy
            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        // ════════════════════════════════════════════════════════════════════════
        //  PILL STYLING (Shared Helper)
        // ════════════════════════════════════════════════════════════════════════

        private static void ApplyPillRegion(Control control)
        {
            int radius = control.Height / 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  STOCK PILL STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Styles a Label as a stock status pill with rounded corners and color coding.
        /// </summary>
        public static void ApplyStockPill(Label lbl, int quantity, bool showAsText = false)
        {
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font(MainFontName, 9F, FontStyle.Bold);
            lbl.Padding = new Padding(12, 0, 12, 0); // Horizontal padding ≈ 6px per side

            Color bgColor, fgColor;
            string text;

            if (quantity > 5)
            {
                bgColor = CurrentSuccessLightColor;
                fgColor = CurrentSuccessColor;
                text = quantity.ToString();
            }
            else if (quantity >= 1)
            {
                bgColor = CurrentWarningLightColor;
                fgColor = CurrentWarningColor;
                text = quantity.ToString();
            }
            else // quantity == 0
            {
                bgColor = CurrentDangerLightColor;
                fgColor = CurrentDangerColor;
                text = showAsText ? "Out of stock" : "0";
            }

            lbl.BackColor = bgColor;
            lbl.ForeColor = fgColor;
            lbl.Text = text;

            // Apply rounded pill region
            lbl.SizeChanged += (s, e) => ApplyPillRegion(lbl);
            ApplyPillRegion(lbl);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  STATUS PILL STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Styles a Label as a status pill with rounded corners and color coding.
        /// Status values: "Sale", "Stock", "Refund", "Alert"
        /// </summary>
        public static void ApplyStatusPill(Label lbl, string status)
        {
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font(MainFontName, 9F, FontStyle.Bold);
            lbl.Padding = new Padding(12, 0, 12, 0);

            Color bgColor, fgColor;

            switch (status.ToLower())
            {
                case "sale":
                    bgColor = CurrentSuccessLightColor;
                    fgColor = CurrentSuccessColor;
                    break;
                case "stock":
                    bgColor = CurrentPrimaryLightColor;
                    fgColor = CurrentPrimaryColor;
                    break;
                case "refund":
                    bgColor = CurrentDangerLightColor;
                    fgColor = CurrentDangerColor;
                    break;
                case "alert":
                    bgColor = CurrentWarningLightColor;
                    fgColor = CurrentWarningColor;
                    break;
                default:
                    bgColor = CurrentInfoColor;
                    fgColor = TextPrimary;
                    break;
            }

            lbl.BackColor = bgColor;
            lbl.ForeColor = fgColor;
            lbl.Text = status;

            // Apply rounded pill region
            lbl.SizeChanged += (s, e) => ApplyPillRegion(lbl);
            ApplyPillRegion(lbl);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  PILL TOGGLE BUTTON STYLING
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Styles a Button as a pill toggle with rounded corners.
        /// </summary>
        public static void ApplyPillToggleStyle(Button btn, bool selected)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font(MainFontName, 10F, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Cursor = Cursors.Hand;

            if (selected)
            {
                btn.BackColor = HeaderColor;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = HeaderColor;
            }
            else
            {
                btn.BackColor = IsDarkMode ? DarkFormBackColor : Color.White;
                btn.ForeColor = TextPrimary;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = CardBorderColor;
                btn.FlatAppearance.MouseOverBackColor = FormBackColorAlt;
            }

            // Apply rounded pill region
            btn.SizeChanged += (s, e) => ApplyPillRegion(btn);
            ApplyPillRegion(btn);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  POS PRODUCT TILE
        // ════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Applies a polished card style to a POS product tile panel,
        /// with a colored top accent bar and hover effect.
        /// </summary>
        public static void StyleProductTile(Panel tile, bool inStock, bool lowStock = false)
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

                // Top accent bar - three states: success (in stock), warning (low stock), danger (out of stock)
                Color accentColor;
                if (!inStock)
                    accentColor = DangerColor;
                else if (lowStock)
                    accentColor = WarningColor;
                else
                    accentColor = PrimaryColor;

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
                tile.Invalidate();
            };
            tile.MouseLeave += (s, e) => tile.Invalidate();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  THEMED MESSAGE BOX
        // ════════════════════════════════════════════════════════════════════════

        public static void ShowInfo(Form owner, string message, string title = "Information")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Info, ThemedMessageBoxButtons.OK))
            {
                dlg.ShowDialog(owner);
            }
        }

        public static void ShowSuccess(Form owner, string message, string title = "Success")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Success, ThemedMessageBoxButtons.OK))
            {
                dlg.ShowDialog(owner);
            }
        }

        public static void ShowWarning(Form owner, string message, string title = "Warning")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Warning, ThemedMessageBoxButtons.OK))
            {
                dlg.ShowDialog(owner);
            }
        }

        public static void ShowError(Form owner, string message, string title = "Error")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Error, ThemedMessageBoxButtons.OK))
            {
                dlg.ShowDialog(owner);
            }
        }

        public static DialogResult ShowConfirm(Form owner, string message, string title = "Confirm")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Warning, ThemedMessageBoxButtons.OKCancel))
            {
                return dlg.ShowDialog(owner);
            }
        }

        public static DialogResult ShowYesNo(Form owner, string message, string title = "Confirm")
        {
            using (var dlg = new frmThemedMessageBox(message, title, ThemedMessageBoxIcon.Warning, ThemedMessageBoxButtons.YesNo))
            {
                return dlg.ShowDialog(owner);
            }
        }

        //  TOAST NOTIFICATIONS
        // ════════════════════════════════════════════════════════════════════════

        public static void ShowToastSuccess(Form owner, string message, string title = "Success", int dismissTimeMs = 2500)
        {
            var toast = new frmToastNotification(message, title, dismissTimeMs, ToastIcon.Success);
            toast.ShowToast(owner);
        }

        public static void ShowToastInfo(Form owner, string message, string title = "Information", int dismissTimeMs = 2500)
        {
            var toast = new frmToastNotification(message, title, dismissTimeMs, ToastIcon.Info);
            toast.ShowToast(owner);
        }

        public static void ShowToastWarning(Form owner, string message, string title = "Warning", int dismissTimeMs = 3000)
        {
            var toast = new frmToastNotification(message, title, dismissTimeMs, ToastIcon.Warning);
            toast.ShowToast(owner);
        }

        public static void ShowToastError(Form owner, string message, string title = "Error", int dismissTimeMs = 3000)
        {
            var toast = new frmToastNotification(message, title, dismissTimeMs, ToastIcon.Error);
            toast.ShowToast(owner);
        }

        public static void ShowToastWithUndo(Form owner, string message, string title = "Item Removed", int dismissTimeMs = 5000, Action undoAction = null)
        {
            var toast = new frmToastNotification(message, title, dismissTimeMs, ToastIcon.Info, undoAction);
            toast.ShowToast(owner);
        }
    }
}
