using InventoryBusinessLayer;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmUpdateSupplier : Form
    {
        private ErrorProvider _errorProvider;
        private bool _isSearching = false;

        public frmUpdateSupplier()
        {
            InitializeComponent();

            _errorProvider = new ErrorProvider();
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Update Supplier", clsFormTheme.Icons.Update);
            btnSearch.Text = clsFormTheme.Icons.Search + "  Find Supplier";
            btnSearch.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch);
            clsFormTheme.ApplyTextBoxStyle(txtUpdateSupplierID);

            btnSearch.Enabled = false;
            AcceptButton = btnSearch;
            txtUpdateSupplierID.TextChanged += txtUpdateSupplierID_TextChanged;
            KeyDown += frmUpdateSupplier_KeyDown;
        }

        private bool IsSupplierIDValid()
        {
            return int.TryParse(txtUpdateSupplierID.Text.Trim(), out int supplierID) &&
                   supplierID > 0;
        }

        private bool ValidateSupplierID()
        {
            if (!int.TryParse(txtUpdateSupplierID.Text.Trim(), out int supplierID) || supplierID <= 0)
            {
                clsFormTheme.ShowInputError(
                    txtUpdateSupplierID,
                    _errorProvider,
                    "Please enter a valid Supplier ID.");

                return false;
            }

            clsFormTheme.ClearInputError(txtUpdateSupplierID, _errorProvider);
            return true;
        }

        private void SetSearchingState(bool isSearching)
        {
            _isSearching = isSearching;
            UseWaitCursor = isSearching;
            txtUpdateSupplierID.Enabled = !isSearching;
            btnSearch.Enabled = !isSearching && IsSupplierIDValid();

            clsFormTheme.SetButtonBusy(
                btnSearch,
                isSearching,
                "Search",
                "Searching...");
        }

        private void txtUpdateSupplierID_TextChanged(object sender, EventArgs e)
        {
            bool isValid = ValidateSupplierID();
            btnSearch.Enabled = isValid && !_isSearching;
        }

        private async Task UpdateSupplier()
        {
            if (!ValidateSupplierID())
                return;

            int supplierID = Convert.ToInt32(txtUpdateSupplierID.Text.Trim());

            SetSearchingState(true);

            try
            {
                clsSupplier supplier = await Task.Run(() => clsSupplier.FindSupplier(supplierID));

                if (supplier == null)
                {
                    clsFormTheme.ShowInputError(
                        txtUpdateSupplierID,
                        _errorProvider,
                        "Supplier not found.");
                    return;
                }

                frmShowSupplierToUpdate frm = new frmShowSupplierToUpdate(supplier);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    SetSearchingState(false);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await UpdateSupplier();
        }

        private async void txtUpdateSupplierID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await UpdateSupplier();
                e.SuppressKeyPress = true;
            }
        }

        private void frmUpdateSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
