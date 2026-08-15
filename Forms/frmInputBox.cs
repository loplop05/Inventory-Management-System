using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmInputBox : Form
    {
        public string InputValue { get; private set; }

        public frmInputBox(string prompt, string title)
        {
            InitializeComponent();
            _lblPrompt.Text = prompt;
            this.Text = title;
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            InputValue = _txtInput.Text;
            DialogResult = DialogResult.OK;
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtInput);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnOK);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel);
            _txtInput.Focus();
        }
    }
}
