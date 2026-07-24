using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public static class clsFormTheme
    {
        public static readonly Color FormBackColor = Color.FromArgb(235, 242, 248);
        public static readonly Color PrimaryColor = Color.FromArgb(41, 128, 185);
        public static readonly Color SecondaryColor = Color.FromArgb(96, 125, 139);
        public static readonly Color DangerColor = Color.FromArgb(192, 57, 43);
        public static readonly Color HeaderColor = Color.FromArgb(44, 62, 80);

        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = FormBackColor;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Font = new Font("Microsoft Sans Serif", 10F);
            form.KeyPreview = true;
        }

        public static void ApplyPrimaryButtonStyle(Button button)
        {
            ApplyButtonStyle(button, PrimaryColor);
        }

        public static void ApplySecondaryButtonStyle(Button button)
        {
            ApplyButtonStyle(button, SecondaryColor);
        }

        public static void ApplyDangerButtonStyle(Button button)
        {
            ApplyButtonStyle(button, DangerColor);
        }

        private static void ApplyButtonStyle(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.Black;
            textBox.UseWaitCursor = false;
        }

        public static void ApplyGridStyle(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
        }

        public static void ShowInputError(TextBox textBox, ErrorProvider errorProvider, string message)
        {
            textBox.BackColor = Color.MistyRose;
            errorProvider.SetError(textBox, message);
        }

        public static void ClearInputError(TextBox textBox, ErrorProvider errorProvider)
        {
            textBox.BackColor = Color.White;
            errorProvider.SetError(textBox, "");
        }

        public static void SetButtonBusy(Button button, bool isBusy, string normalText, string busyText)
        {
            button.Text = isBusy ? busyText : normalText;
            button.UseWaitCursor = isBusy;
        }
    }
}
