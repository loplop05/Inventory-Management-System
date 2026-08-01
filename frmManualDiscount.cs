using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmManualDiscount : Form
    {
        public enum DiscountType
        {
            Percentage,
            FixedAmount
        }

        public DiscountType SelectedType { get; private set; }
        public decimal DiscountValue { get; private set; }

        public frmManualDiscount()
        {
            InitializeComponent();
        }

        private void frmManualDiscount_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Manual Discount", clsFormTheme.Icons.Money);

            clsFormTheme.ApplyTextBoxStyle(txtDiscountValue);
            clsFormTheme.ApplyPrimaryButtonStyle(btnApply, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

            rbPercentage.Checked = true;
            txtDiscountValue.Text = "0";
            txtDiscountValue.Focus();
            txtDiscountValue.SelectAll();

            clsLanguageManager.ApplyLanguage(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtDiscountValue.Text, out value) || value < 0)
            {
                MessageBox.Show("Please enter a valid positive number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscountValue.Focus();
                return;
            }

            if (rbPercentage.Checked && value > 100)
            {
                MessageBox.Show("Percentage discount cannot exceed 100%.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscountValue.Focus();
                return;
            }

            SelectedType = rbPercentage.Checked ? DiscountType.Percentage : DiscountType.FixedAmount;
            DiscountValue = value;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtDiscountValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnApply_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
