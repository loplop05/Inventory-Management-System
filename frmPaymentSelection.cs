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
            _txtCardLastFour.MaxLength = 4;
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
            // Show card input dialog
            using (var cardInput = new frmInputBox("Enter last 4 digits of card:", "Card Payment"))
            {
                if (cardInput.ShowDialog(this) != DialogResult.OK)
                    return;

                if (string.IsNullOrWhiteSpace(cardInput.InputValue))
                {
                    clsFormTheme.ShowError(this, "Please enter the last 4 digits of the card.", "Missing Card Info");
                    return;
                }

                if (cardInput.InputValue.Length != 4)
                {
                    clsFormTheme.ShowError(this, "Card last 4 digits must be exactly 4 characters.", "Invalid Card Info");
                    return;
                }

                SelectedPaymentMethod = "Visa";
                PaymentDetails = "****" + cardInput.InputValue;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void _btnSplit_Click(object sender, EventArgs e)
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
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
