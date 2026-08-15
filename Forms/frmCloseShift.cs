using System;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCloseShift : Form
    {
        private int _shiftID;
        private bool _isClosing = false;

        public frmCloseShift(int shiftID)
        {
            InitializeComponent();
            _shiftID = shiftID;
        }

        private void frmCloseShift_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtCountedCash);
            clsFormTheme.ApplyTextBoxStyle(_txtNotes);
            
            _btnClose.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnClose, clsFormTheme.Icons.Check);
            
            _btnCancel.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel, clsFormTheme.Icons.Cancel);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            LoadShiftInfo();
            
            _btnClose.Click += _btnClose_Click;
            _btnCancel.Click += (s, ev) => Close();
            _txtCountedCash.TextChanged += _txtCountedCash_TextChanged;
        }

        private void ApplyLocalization()
        {
            _lblTitle.Text = clsLanguageManager.GetString("Close Shift");
            _lblStartingCash.Text = clsLanguageManager.GetString("Starting Cash");
            _lblExpectedCash.Text = clsLanguageManager.GetString("Expected Cash");
            _lblCountedCash.Text = clsLanguageManager.GetString("Counted Cash");
            _lblCashDifference.Text = clsLanguageManager.GetString("Cash Difference");
            _lblNotes.Text = clsLanguageManager.GetString("Notes");
            _btnClose.Text = clsLanguageManager.GetString("Close Shift");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = clsLanguageManager.GetString("Close Shift");
        }

        private void LoadShiftInfo()
        {
            try
            {
                decimal startingCash = clsShift.GetStartingCash(_shiftID);
                decimal cashSales = clsShift.GetCashSalesTotal(_shiftID);
                decimal expectedCash = startingCash + cashSales;
                
                _lblStartingCashValue.Text = "$" + startingCash.ToString("F2");
                _lblExpectedCashValue.Text = "$" + expectedCash.ToString("F2");
                _txtCountedCash.Text = expectedCash.ToString("F2");
                
                UpdateCashDifference(expectedCash);
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading shift info: " + ex.Message, "Error");
            }
        }

        private void _txtCountedCash_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(_lblExpectedCashValue.Text.Replace("$", ""), out decimal expectedCash))
            {
                UpdateCashDifference(expectedCash);
            }
        }

        private void UpdateCashDifference(decimal expectedCash)
        {
            if (decimal.TryParse(_txtCountedCash.Text, out decimal countedCash))
            {
                decimal difference = countedCash - expectedCash;
                _lblCashDifferenceValue.Text = "$" + difference.ToString("F2");
                
                if (difference == 0)
                {
                    _lblCashDifferenceValue.ForeColor = clsFormTheme.SuccessColor;
                }
                else
                {
                    _lblCashDifferenceValue.ForeColor = clsFormTheme.WarningColor;
                }
            }
            else
            {
                _lblCashDifferenceValue.Text = "---";
                _lblCashDifferenceValue.ForeColor = clsFormTheme.TextMuted;
            }
        }

        private bool ValidateInput()
        {
            if (!decimal.TryParse(_txtCountedCash.Text, out decimal countedCash))
            {
                clsFormTheme.ShowWarning(this, "Please enter a valid counted cash amount.", "Validation Error");
                _txtCountedCash.Focus();
                return false;
            }
            
            if (countedCash < 0)
            {
                clsFormTheme.ShowWarning(this, "Counted cash cannot be negative.", "Validation Error");
                _txtCountedCash.Focus();
                return false;
            }
            
            return true;
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            if (_isClosing)
                return;
            
            if (!ValidateInput())
                return;
            
            decimal countedCash = decimal.Parse(_txtCountedCash.Text);
            string notes = _txtNotes.Text.Trim();
            _isClosing = true;
            _btnClose.Enabled = false;
            
            try
            {
                string errorMessage;
                bool success = clsShift.CloseShift(_shiftID, countedCash, notes, out errorMessage);
                
                if (success)
                {
                    clsFormTheme.ShowSuccess(this, "Shift closed successfully.", "Success");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, errorMessage, "Error");
                    _btnClose.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error closing shift: " + ex.Message, "Error");
                _btnClose.Enabled = true;
            }
            finally
            {
                _isClosing = false;
            }
        }
    }
}
