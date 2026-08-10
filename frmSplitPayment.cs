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

        private bool _isAutoFilling = false;

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
            _lblTotalAmount.Text = "Total: " + TotalAmount.ToString("C2");
            _txtCashAmount.Text = "0.00";
            _txtCardAmount.Text = TotalAmount.ToString("F2");
            _cmbCardType.Items.AddRange(new object[] { "Visa", "MasterCard", "American Express", "Discover" });
            _cmbCardType.SelectedIndex = 0;

            // Wire events - use Leave for auto-calc to avoid cursor issues
            _txtCashAmount.Leave += CalculateSplit;
            _txtCardAmount.Leave += CalculateSplit;
        }

        private void CalculateSplit(object sender, EventArgs e)
        {
            try
            {
                if (_isAutoFilling)
                    return;

                // Parse current values safely - don't auto-format during typing
                decimal cash = 0;
                decimal card = 0;

                if (!string.IsNullOrWhiteSpace(_txtCashAmount.Text))
                {
                    string cashText = _txtCashAmount.Text.Replace("$", "").Replace(",", "").Trim();
                    decimal.TryParse(cashText, out cash);
                }

                if (!string.IsNullOrWhiteSpace(_txtCardAmount.Text))
                {
                    string cardText = _txtCardAmount.Text.Replace("$", "").Replace(",", "").Trim();
                    decimal.TryParse(cardText, out card);
                }

                // Ensure values are not negative
                cash = Math.Max(0, cash);
                card = Math.Max(0, card);

                // Auto-calculate the other field
                if (sender == _txtCashAmount)
                {
                    // Cash changed - auto-fill card
                    if (cash <= TotalAmount)
                    {
                        decimal autoCard = TotalAmount - cash;
                        _isAutoFilling = true;
                        _txtCardAmount.Text = autoCard.ToString("F2");
                        _isAutoFilling = false;
                        card = autoCard;
                    }
                }
                else if (sender == _txtCardAmount)
                {
                    // Card changed - auto-fill cash
                    if (card <= TotalAmount)
                    {
                        decimal autoCash = TotalAmount - card;
                        _isAutoFilling = true;
                        _txtCashAmount.Text = autoCash.ToString("F2");
                        _isAutoFilling = false;
                        cash = autoCash;
                    }
                }

                decimal total = cash + card;
                _lblSplitTotal.Text = "Split: " + total.ToString("C2");

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
            catch
            {
                // On any error, disable confirm and show red
                _lblSplitTotal.ForeColor = Color.Red;
                _btnConfirm.Enabled = false;
                _lblSplitTotal.Text = "Error";
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
            try
            {
                CashAmount = ParseAmount(_txtCashAmount.Text);
                CardAmount = ParseAmount(_txtCardAmount.Text);
                CardType = _cmbCardType.SelectedItem?.ToString() ?? "Visa";
                CardLastFour = _txtCardLastFour.Text.Trim();

                // Validate total matches
                decimal total = CashAmount + CardAmount;
                if (Math.Abs(total - TotalAmount) > 0.01m)
                {
                    clsFormTheme.ShowError(this, $"Split total ({total:C2}) does not match order total ({TotalAmount:C2})", "Invalid Split");
                    return;
                }

                // Validate card info if card amount > 0
                if (CardAmount > 0)
                {
                    if (string.IsNullOrWhiteSpace(CardLastFour))
                    {
                        clsFormTheme.ShowError(this, "Please enter the last 4 digits of the card.", "Missing Card Info");
                        _txtCardLastFour.Focus();
                        return;
                    }

                    if (CardLastFour.Length != 4)
                    {
                        clsFormTheme.ShowError(this, "Card last 4 digits must be exactly 4 characters.", "Invalid Card Info");
                        _txtCardLastFour.Focus();
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error processing payment: " + ex.Message, "Error");
            }
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
