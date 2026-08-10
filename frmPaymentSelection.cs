using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmPaymentSelection : Form
    {
        public decimal TotalAmount { get; set; }
        public string SelectedPaymentMethod { get; private set; }
        public string PaymentDetails { get; private set; }

        public frmPaymentSelection(decimal totalAmount)
        {
            InitializeComponent();
            TotalAmount = totalAmount;
            InitializeForm();
        }

        private void InitializeForm()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Select Payment Method", clsFormTheme.Icons.Money);

            // Style controls
            clsFormTheme.ApplyTextBoxStyle(_txtCardLastFour);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnCash);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnCard);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSplit);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel);

            // Set initial values
            _lblTotalAmount.Text = "Total Amount: " + TotalAmount.ToString("C2");
            _rbCash.Checked = true;
            _txtCardLastFour.MaxLength = 4;
            _txtCardLastFour.Enabled = false;

            // Wire events
            _rbCash.CheckedChanged += PaymentMethodChanged;
            _rbCard.CheckedChanged += PaymentMethodChanged;
            _rbSplit.CheckedChanged += PaymentMethodChanged;
        }

        private void PaymentMethodChanged(object sender, EventArgs e)
        {
            _txtCardLastFour.Enabled = _rbCard.Checked || _rbSplit.Checked;
            _btnSplit.Enabled = _rbSplit.Checked;
        }

        private void _btnCash_Click(object sender, EventArgs e)
        {
            SelectedPaymentMethod = "Cash";
            PaymentDetails = null;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _btnCard_Click(object sender, EventArgs e)
        {
            if (_rbCard.Checked || _rbSplit.Checked)
            {
                if (string.IsNullOrWhiteSpace(_txtCardLastFour.Text))
                {
                    clsFormTheme.ShowError(this, "Please enter the last 4 digits of the card.", "Missing Card Info");
                    return;
                }

                if (_txtCardLastFour.Text.Length != 4)
                {
                    clsFormTheme.ShowError(this, "Card last 4 digits must be exactly 4 characters.", "Invalid Card Info");
                    return;
                }
            }

            if (_rbCard.Checked)
            {
                SelectedPaymentMethod = "Visa";
                PaymentDetails = "****" + _txtCardLastFour.Text;
            }
            else if (_rbSplit.Checked)
            {
                // Show split payment dialog
                using (var splitDialog = new frmSplitPayment(TotalAmount))
                {
                    if (splitDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        SelectedPaymentMethod = "Split";
                        PaymentDetails = $"Cash:{splitDialog.CashAmount:0.00}|Card:{splitDialog.CardAmount:0.00}|{splitDialog.CardType}:****{splitDialog.CardLastFour}";
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    return;
                }
            }
            else
            {
                SelectedPaymentMethod = "Cash";
                PaymentDetails = null;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void _btnSplit_Click(object sender, EventArgs e)
        {
            using (var splitDialog = new frmSplitPayment(TotalAmount))
            {
                if (splitDialog.ShowDialog(this) == DialogResult.OK)
                {
                    SelectedPaymentMethod = "Split";
                    PaymentDetails = $"Cash:{splitDialog.CashAmount:0.00}|Card:{splitDialog.CardAmount:0.00}|{splitDialog.CardType}:****{splitDialog.CardLastFour}";
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
