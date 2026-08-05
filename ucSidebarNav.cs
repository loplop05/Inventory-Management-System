using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class ucSidebarNav : UserControl
    {
        public event Action<string> NavigationRequested;

        private Dictionary<string, Panel> _navPanels = new Dictionary<string, Panel>();
        private string _activeScreen = "";

        public ucSidebarNav()
        {
            InitializeComponent();
            LoadBranding();
            CreateNavItems();
        }

        private void LoadBranding()
        {
            try
            {
                _lblTitle.Text = ConfigurationManager.AppSettings["AppName"] ?? "ElectroPOS Pro";
                _lblSubtitle.Text = ConfigurationManager.AppSettings["StoreName"] ?? "Main Store";
            }
            catch
            {
                _lblTitle.Text = "ElectroPOS Pro";
                _lblSubtitle.Text = "Main Store";
            }
        }

        private void CreateNavItems()
        {
            var navItems = new[]
            {
                new { Key = "Dashboard", Label = "Dashboard", Icon = clsFormTheme.Icons.Chart },
                new { Key = "POS", Label = "Point of Sale", Icon = clsFormTheme.Icons.POS },
                new { Key = "Inventory", Label = "Inventory", Icon = clsFormTheme.Icons.Products },
                new { Key = "Orders", Label = "Orders", Icon = clsFormTheme.Icons.Money },
                new { Key = "Reports", Label = "Reports", Icon = clsFormTheme.Icons.Reports }
            };

            int yOffset = 10;
            foreach (var item in navItems)
            {
                Panel navPanel = CreateNavItem(item.Key, item.Label, item.Icon);
                navPanel.Location = new Point(10, yOffset);
                _navItemsPanel.Controls.Add(navPanel);
                _navPanels[item.Key] = navPanel;
                yOffset += 50;
            }

            // Create Support item at bottom
            Panel supportPanel = CreateNavItem("Support", "Support", clsFormTheme.Icons.Info);
            supportPanel.Dock = DockStyle.Fill;
            _supportPanel.Controls.Add(supportPanel);
            _navPanels["Support"] = supportPanel;

            // Add divider above support
            Panel divider = new Panel
            {
                BackColor = Color.FromArgb(51, 65, 85),
                Height = 1,
                Dock = DockStyle.Top
            };
            _supportPanel.Controls.Add(divider);
            divider.BringToFront();
        }

        private Panel CreateNavItem(string key, string label, string icon)
        {
            Panel panel = new Panel
            {
                Height = 40,
                Width = _navItemsPanel.Width - 20,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font(clsFormTheme.IconFontName, 14F),
                ForeColor = clsFormTheme.TextSecondary,
                Location = new Point(12, 8),
                AutoSize = true
            };

            Label lblText = new Label
            {
                Text = label,
                Font = new Font(clsFormTheme.MainFontName, 10F),
                ForeColor = clsFormTheme.TextPrimary,
                Location = new Point(40, 12),
                AutoSize = true
            };

            panel.Controls.Add(lblIcon);
            panel.Controls.Add(lblText);

            panel.Click += (s, e) => OnNavItemClick(key);
            panel.MouseEnter += (s, e) => OnNavItemMouseEnter(panel, key);
            panel.MouseLeave += (s, e) => OnNavItemMouseLeave(panel, key);

            return panel;
        }

        private void OnNavItemClick(string key)
        {
            SetActive(key);
            NavigationRequested?.Invoke(key);
        }

        private void OnNavItemMouseEnter(Panel panel, string key)
        {
            if (key != _activeScreen)
            {
                panel.BackColor = clsFormTheme.FormBackColorAlt;
            }
        }

        private void OnNavItemMouseLeave(Panel panel, string key)
        {
            if (key != _activeScreen)
            {
                panel.BackColor = Color.Transparent;
            }
        }

        public void SetActive(string screenKey)
        {
            _activeScreen = screenKey;

            foreach (var kvp in _navPanels)
            {
                Panel panel = kvp.Value;
                bool isActive = kvp.Key == screenKey;

                if (isActive)
                {
                    panel.BackColor = clsFormTheme.PrimaryColor;
                    foreach (Control ctrl in panel.Controls)
                    {
                        if (ctrl is Label lbl)
                        {
                            lbl.ForeColor = Color.White;
                        }
                    }
                }
                else
                {
                    panel.BackColor = Color.Transparent;
                    foreach (Control ctrl in panel.Controls)
                    {
                        if (ctrl is Label lbl)
                        {
                            lbl.ForeColor = ctrl.Text.Length == 1 ? clsFormTheme.TextSecondary : clsFormTheme.TextPrimary;
                        }
                    }
                }
            }
        }
    }
}
