using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Provides icon+label button rendering functionality.
    /// Extracted from frmPOS.cs for reuse across the application.
    /// </summary>
    public static class IconButtonRenderer
    {
        private class IconButtonInfo
        {
            public string Icon;
            public string Label;
            public float IconFontSize;
            public float TextFontSize;
            public FontStyle TextStyle;
        }

        private static readonly Dictionary<Button, IconButtonInfo> _iconButtons =
            new Dictionary<Button, IconButtonInfo>();

        /// <summary>
        /// Sets up a button to display an icon and label using custom painting.
        /// </summary>
        /// <param name="btn">The button to configure.</param>
        /// <param name="icon">The icon glyph to display.</param>
        /// <param name="label">The text label to display.</param>
        /// <param name="iconFontSize">Font size for the icon. Default is 12F.</param>
        /// <param name="textFontSize">Font size for the text. Default is 10F.</param>
        /// <param name="textStyle">Font style for the text. Default is Bold.</param>
        public static void SetIconButtonText(Button btn, string icon, string label,
            float iconFontSize = 12F, float textFontSize = 10F, FontStyle textStyle = FontStyle.Bold)
        {
            btn.Text = "";
            _iconButtons[btn] = new IconButtonInfo
            {
                Icon = icon,
                Label = label,
                IconFontSize = iconFontSize,
                TextFontSize = textFontSize,
                TextStyle = textStyle
            };
            btn.Paint -= IconButton_Paint; // avoid double subscribe if reused
            btn.Paint += IconButton_Paint;
            btn.Disposed += Button_Disposed;
        }

        private static void IconButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            IconButtonInfo info;
            if (btn == null || !_iconButtons.TryGetValue(btn, out info))
                return;

            using (Font iconFont = new Font(clsFormTheme.IconFontName, info.IconFontSize))
            using (Font textFont = new Font(clsFormTheme.MainFontName, info.TextFontSize, info.TextStyle))
            using (SolidBrush brush = new SolidBrush(btn.Enabled ? btn.ForeColor : clsFormTheme.TextMuted))
            {
                SizeF iconSize = e.Graphics.MeasureString(info.Icon, iconFont);
                SizeF textSize = e.Graphics.MeasureString(info.Label, textFont);

                const float gap = 6f;
                float totalWidth = iconSize.Width + gap + textSize.Width;
                float startX = (btn.ClientSize.Width - totalWidth) / 2f;
                float centerY = btn.ClientSize.Height / 2f;

                var iconRect = new RectangleF(startX, centerY - iconSize.Height / 2f, iconSize.Width, iconSize.Height);
                var textRect = new RectangleF(startX + iconSize.Width + gap, centerY - textSize.Height / 2f, textSize.Width, textSize.Height);

                e.Graphics.DrawString(info.Icon, iconFont, brush, iconRect);
                e.Graphics.DrawString(info.Label, textFont, brush, textRect);
            }
        }

        private static void Button_Disposed(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && _iconButtons.ContainsKey(btn))
            {
                _iconButtons.Remove(btn);
            }
        }

        /// <summary>
        /// Cleans up disposed buttons from the internal tracking dictionary.
        /// Call this periodically to prevent memory leaks.
        /// </summary>
        public static void PurgeDisposedButtons()
        {
            foreach (Button key in _iconButtons.Keys.Where(b => b.IsDisposed).ToList())
            {
                _iconButtons.Remove(key);
            }
        }
    }
}
