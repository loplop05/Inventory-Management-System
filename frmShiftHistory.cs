using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmShiftHistory : Form
    {
        public frmShiftHistory()
        {
            InitializeComponent();
        }

        private void frmShiftHistory_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            
            _btnRefresh.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            _btnFilter.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            _btnClose.Font = new System.Drawing.Font(clsFormTheme.MainFontName, 10F, System.Drawing.FontStyle.Bold);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            LoadShiftHistory();
            
            _btnRefresh.Click += _btnRefresh_Click;
            _btnFilter.Click += _btnFilter_Click;
            _btnClose.Click += (s, ev) => Close();
            _txtSearch.TextChanged += _txtSearch_TextChanged;
        }

        private void ApplyLocalization()
        {
            _lblTitle.Text = clsLanguageManager.GetString("Shift History");
            _lblSearch.Text = clsLanguageManager.GetString("Search");
            _btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            _btnFilter.Text = clsLanguageManager.GetString("Filter");
            _btnClose.Text = clsLanguageManager.GetString("Close");
            Text = clsLanguageManager.GetString("Shift History");
        }

        private void LoadShiftHistory(DateTime? fromDate = null, DateTime? toDate = null, string search = null)
        {
            try
            {
                DataTable shifts = clsShift.GetShiftHistory(fromDate, toDate, null);
                
                if (shifts != null && shifts.Rows.Count > 0)
                {
                    // Add CashSales column if not present
                    if (!shifts.Columns.Contains("CashSales"))
                    {
                        shifts.Columns.Add("CashSales", typeof(decimal));
                        foreach (DataRow row in shifts.Rows)
                        {
                            if (row["ExpectedCash"] != DBNull.Value && row["StartingCash"] != DBNull.Value)
                            {
                                row["CashSales"] = Convert.ToDecimal(row["ExpectedCash"]) - Convert.ToDecimal(row["StartingCash"]);
                            }
                        }
                    }
                    
                    // Apply search filter if provided
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        var filteredRows = shifts.Select($"Username LIKE '%{search}%' OR Status LIKE '%{search}%'");
                        shifts = filteredRows.Length > 0 ? filteredRows.CopyToDataTable() : shifts.Clone();
                    }
                    
                    _dgvShifts.DataSource = shifts;
                    
                    // Format columns
                    foreach (DataGridViewColumn col in _dgvShifts.Columns)
                    {
                        if (col.Name.Contains("Cash") || col.Name.Contains("Sales") || col.Name.Contains("Difference"))
                        {
                            col.DefaultCellStyle.Format = "F2";
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                    
                    // Format date columns
                    if (_dgvShifts.Columns.Contains("OpenedAt"))
                    {
                        _dgvShifts.Columns["OpenedAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                        _dgvShifts.Columns["OpenedAt"].HeaderText = "Opened";
                    }
                    if (_dgvShifts.Columns.Contains("ClosedAt"))
                    {
                        _dgvShifts.Columns["ClosedAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                        _dgvShifts.Columns["ClosedAt"].HeaderText = "Closed";
                    }
                    
                    // Rename columns for display
                    if (_dgvShifts.Columns.Contains("ShiftID"))
                        _dgvShifts.Columns["ShiftID"].HeaderText = "ID";
                    if (_dgvShifts.Columns.Contains("Username"))
                        _dgvShifts.Columns["Username"].HeaderText = "Cashier";
                    if (_dgvShifts.Columns.Contains("StartingCash"))
                        _dgvShifts.Columns["StartingCash"].HeaderText = "Starting Cash";
                    if (_dgvShifts.Columns.Contains("CashSales"))
                        _dgvShifts.Columns["CashSales"].HeaderText = "Cash Sales";
                    if (_dgvShifts.Columns.Contains("ExpectedCash"))
                        _dgvShifts.Columns["ExpectedCash"].HeaderText = "Expected Cash";
                    if (_dgvShifts.Columns.Contains("CountedCash"))
                        _dgvShifts.Columns["CountedCash"].HeaderText = "Counted Cash";
                    if (_dgvShifts.Columns.Contains("CashDifference"))
                        _dgvShifts.Columns["CashDifference"].HeaderText = "Difference";
                    if (_dgvShifts.Columns.Contains("Status"))
                        _dgvShifts.Columns["Status"].HeaderText = "Status";
                    if (_dgvShifts.Columns.Contains("Notes"))
                        _dgvShifts.Columns["Notes"].HeaderText = "Notes";
                    
                    // Auto-size columns
                    _dgvShifts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    
                    _lblRecordCount.Text = shifts.Rows.Count + " records";
                }
                else
                {
                    _dgvShifts.DataSource = null;
                    _lblRecordCount.Text = "No records found";
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading shift history: " + ex.Message, "Error");
                _dgvShifts.DataSource = null;
                _lblRecordCount.Text = "Error loading data";
            }
        }

        private void _btnRefresh_Click(object sender, EventArgs e)
        {
            LoadShiftHistory();
        }

        private void _btnFilter_Click(object sender, EventArgs e)
        {
            using (var filterForm = new Form())
            {
                filterForm.Text = "Filter Shifts";
                filterForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                filterForm.MaximizeBox = false;
                filterForm.MinimizeBox = false;
                filterForm.StartPosition = FormStartPosition.CenterParent;
                filterForm.Size = new Size(350, 200);
                
                var layout = new TableLayoutPanel();
                layout.Dock = DockStyle.Fill;
                layout.Padding = new Padding(20);
                layout.ColumnCount = 2;
                layout.RowCount = 4;
                
                var lblFrom = new Label { Text = "From Date:", AutoSize = true };
                var dtpFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-30) };
                var lblTo = new Label { Text = "To Date:", AutoSize = true };
                var dtpTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
                var btnApply = new Button { Text = "Apply", Width = 100, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Width = 100, DialogResult = DialogResult.Cancel };
                
                layout.Controls.Add(lblFrom, 0, 0);
                layout.Controls.Add(dtpFrom, 1, 0);
                layout.Controls.Add(lblTo, 0, 1);
                layout.Controls.Add(dtpTo, 1, 1);
                layout.Controls.Add(btnApply, 0, 2);
                layout.Controls.Add(btnCancel, 1, 2);
                
                filterForm.Controls.Add(layout);
                
                if (filterForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadShiftHistory(dtpFrom.Value.Date, dtpTo.Value.Date.AddDays(1).AddTicks(-1));
                }
            }
        }

        private void _txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadShiftHistory(search: _txtSearch.Text);
        }
    }
}
