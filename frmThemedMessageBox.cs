using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public enum ThemedMessageBoxIcon
    {
        None,
        Info,
        Success,
        Warning,
        Error
    }

    public enum ThemedMessageBoxButtons
    {
        OK,
        OKCancel,
        YesNo
    }

    public partial class frmThemedMessageBox : Form
    {
        private ThemedMessageBoxIcon _icon;
        private ThemedMessageBoxButtons _buttons;
        private DialogResult _result = DialogResult.Cancel;

        public frmThemedMessageBox(string message, string title, ThemedMessageBoxIcon icon, ThemedMessageBoxButtons buttons)
        {
            InitializeComponent();
            _icon = icon;
            _buttons = buttons;

            Text = title;
            lblMessage.Text = message;
            SetupIcon();
            SetupButtons();
        }

        private void SetupIcon()
        {
            switch (_icon)
            {
                case ThemedMessageBoxIcon.Info:
                    lblIcon.Text = "ℹ";
                    lblIcon.ForeColor = clsFormTheme.InfoColor;
                    break;
                case ThemedMessageBoxIcon.Success:
                    lblIcon.Text = "✓";
                    lblIcon.ForeColor = clsFormTheme.SuccessColor;
                    break;
                case ThemedMessageBoxIcon.Warning:
                    lblIcon.Text = "⚠";
                    lblIcon.ForeColor = clsFormTheme.WarningColor;
                    break;
                case ThemedMessageBoxIcon.Error:
                    lblIcon.Text = "✕";
                    lblIcon.ForeColor = clsFormTheme.DangerColor;
                    break;
                default:
                    lblIcon.Text = "";
                    break;
            }
        }

        private void SetupButtons()
        {
            switch (_buttons)
            {
                case ThemedMessageBoxButtons.OK:
                    btnOK.Visible = true;
                    btnCancel.Visible = false;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                    break;
                case ThemedMessageBoxButtons.OKCancel:
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                    break;
                case ThemedMessageBoxButtons.YesNo:
                    btnOK.Visible = false;
                    btnCancel.Visible = false;
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    break;
            }

            ApplyButtonStyles();
        }

        private void ApplyButtonStyles()
        {
            if (btnOK.Visible)
                clsFormTheme.ApplyPrimaryButtonStyle(btnOK, clsFormTheme.Icons.Check);
            if (btnCancel.Visible)
                clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);
            if (btnYes.Visible)
                clsFormTheme.ApplySuccessButtonStyle(btnYes, clsFormTheme.Icons.Check);
            if (btnNo.Visible)
                clsFormTheme.ApplyDangerButtonStyle(btnNo, clsFormTheme.Icons.Cancel);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _result = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _result = DialogResult.Cancel;
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            _result = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            _result = DialogResult.No;
            Close();
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            if (owner != null)
            {
                StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            base.ShowDialog(owner);
            return _result;
        }
    }
}
