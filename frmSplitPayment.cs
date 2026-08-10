using System;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmSplitPayment : Form
    {
        public decimal TotalAmount { get; set; }
        public decimal CashAmount { get; private set; }
        public decimal CardAmount { get; private set; }
        public string CardType { get; private set; }
        public string CardLastFour { get; private set; }

        public frmSplitPayment(decimal totalAmount)
        {
            InitializeComponent();
            TotalAmount = totalAmount;
            InitializeForm();
        }

        private void InitializeForm()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Split Payment", clsFormTheme.Icons.Money);

            // Style controls
            clsFormTheme.ApplyTextBoxStyle(_txtCashAmount);
            clsFormTheme.ApplyTextBoxStyle(_txtCardAmount);
            clsFormTheme.ApplyComboBoxStyle(_cmbCardType);
            clsFormTheme.ApplyTextBoxStyle(_txtCardLastFour);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnConfirm);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel);

            // Set initial values
            _lblTotalAmount.Text = "Total Amount: " + TotalAmount.ToString("C2");
            _txtCashAmount.Text = "0.00";
            _txtCardAmount.Text = TotalAmount.ToString("C2");
            _cmbCardType.Items.AddRange(new object[] { "Visa", "MasterCard", "American Express", "Discover" });
            _cmbCardType.SelectedIndex = 0;

            // Wire events
            _txtCashAmount.TextChanged += CalculateSplit;
            _txtCardAmount.TextChanged += CalculateSplit;
        }

        private void CalculateSplit(object sender, EventArgs e)
        {
            decimal cash = ParseAmount(_txtCashAmount.Text);
            decimal card = ParseAmount(_txtCardAmount.Text);
            decimal total = cash + card;

            _lblSplitTotal.Text = "Split Total: " + total.ToString("C2");

            if (total != TotalAmount)
            {
                _lblSplitTotal.ForeColor = Color.Red;
                _btnConfirm.Enabled = false;
            }
            else
            {
                _lblSplitTotal.ForeColor = clsFormTheme.SuccessColor;
                _btnConfirm.Enabled = true;
            }
        }

        private decimal ParseAmount(string text)
        {
            if (decimal.TryParse(text.Replace("$", "").Replace(",", ""), out decimal amount))
                return amount;
            return 0;
        }

        private void _btnConfirm_Click(object sender, EventArgs e)
        {
            CashAmount = ParseAmount(_txtCashAmount.Text);
            CardAmount = ParseAmount(_txtCardAmount.Text);
            CardType = _cmbCardType.SelectedItem?.ToString();
            CardLastFour = _txtCardLastFour.Text.Trim();

            if (CardAmount > 0 && string.IsNullOrWhiteSpace(CardLastFour))
            {
                clsFormTheme.ShowError(this, "Please enter the last 4 digits of the card.", "Missing Card Info");
                return;
            }

            if (CardLastFour.Length != 4 && CardAmount > 0)
            {
                clsFormTheme.ShowError(this, "Card last 4 digits must be exactly 4 characters.", "Invalid Card Info");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _btnFullCash_Click(object sender, EventArgs e)
        {
            _txtCashAmount.Text = TotalAmount.ToString("C2");
            _txtCardAmount.Text = "0.00";
            CalculateSplit(null, null);
        }

        private void _btnFullCard_Click(object sender, EventArgs e)
        {
            _txtCashAmount.Text = "0.00";
            _txtCardAmount.Text = TotalAmount.ToString("C2");
            CalculateSplit(null, null);
        }

        private void _btn5050_Click(object sender, EventArgs e)
        {
            decimal half = TotalAmount / 2;
            _txtCashAmount.Text = half.ToString("C2");
            _txtCardAmount.Text = half.ToString("C2");
            CalculateSplit(null, null);
        }
    }
}
