using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public enum ToastIcon
    {
        Success,
        Info,
        Warning,
        Error
    }

    public partial class frmToastNotification : Form
    {
        private Timer _dismissTimer;
        private const int DefaultDismissTime = 2500; // 2.5 seconds

        public frmToastNotification(string message, string title = "Success", int dismissTimeMs = DefaultDismissTime, ToastIcon icon = ToastIcon.Success)
        {
            InitializeComponent();
            
            Text = title;
            lblMessage.Text = message;
            
            // Style based on icon type
            SetupIcon(icon);
            
            // Hide buttons for toast (auto-dismiss only)
            btnOK.Visible = false;
            btnCancel.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            
            // Make it look like a toast - smaller, no title bar
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            
            // Apply themed styling
            ApplyToastStyle();
            
            // Setup auto-dismiss timer
            _dismissTimer = new Timer();
            _dismissTimer.Interval = dismissTimeMs;
            _dismissTimer.Tick += (s, e) =>
            {
                _dismissTimer.Stop();
                Close();
            };
        }

        private void SetupIcon(ToastIcon icon)
        {
            switch (icon)
            {
                case ToastIcon.Success:
                    lblIcon.Text = "✓";
                    lblIcon.ForeColor = clsFormTheme.SuccessColor;
                    break;
                case ToastIcon.Info:
                    lblIcon.Text = "ℹ";
                    lblIcon.ForeColor = clsFormTheme.InfoColor;
                    break;
                case ToastIcon.Warning:
                    lblIcon.Text = "⚠";
                    lblIcon.ForeColor = clsFormTheme.WarningColor;
                    break;
                case ToastIcon.Error:
                    lblIcon.Text = "✕";
                    lblIcon.ForeColor = clsFormTheme.DangerColor;
                    break;
            }
        }

        private void ApplyToastStyle()
        {
            BackColor = clsFormTheme.CardColor;
            ForeColor = clsFormTheme.TextPrimary;
            
            // Rounded corners effect
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
            
            // Add subtle shadow effect via padding
            Padding = new Padding(1);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public void ShowToast(Form owner)
        {
            if (owner != null)
            {
                // Position at bottom-right of owner form
                Point ownerLocation = owner.Location;
                Size ownerSize = owner.Size;
                
                int x = ownerLocation.X + ownerSize.Width - Width - 20;
                int y = ownerLocation.Y + ownerSize.Height - Height - 20;
                
                Location = new Point(x, y);
            }
            else
            {
                // Center on screen if no owner
                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                Location = new Point(screen.Right - Width - 20, screen.Bottom - Height - 20);
            }
            
            Show();
            _dismissTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw border
            using (Pen borderPen = new Pen(clsFormTheme.CardBorderColor, 1))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            // Click to dismiss immediately
            _dismissTimer.Stop();
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dismissTimer != null)
                {
                    _dismissTimer.Stop();
                    _dismissTimer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
