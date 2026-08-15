using System;
using System.Data;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;
using InventoryManagementSystem;

namespace InventoryManagementSystem
{
    public partial class frmQuickSearch : Form
    {
        public object SelectedItem { get; private set; }
        public string SelectedType { get; private set; }

        public frmQuickSearch()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(txtSearch);
            clsFormTheme.ApplyComboBoxStyle(cmbSearchType);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSearch);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
            clsFormTheme.ApplyGridStyle(gridResults);
        }

        private void frmQuickSearch_Load(object sender, EventArgs e)
        {
            cmbSearchType.SelectedIndex = 0;
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            string searchType = cmbSearchType.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                gridResults.DataSource = null;
                return;
            }

            try
            {
                DataTable results = null;

                // Skip search implementation for now - methods don't exist in business layer
                // This will be implemented after restructuring
                gridResults.DataSource = null;
                clsNotify.Warn("Search functionality will be implemented after code restructuring.");
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmQuickSearch.PerformSearch", ex);
                clsNotify.Error("Search failed. Please try again.");
            }
        }

        private void gridResults_DoubleClick(object sender, EventArgs e)
        {
            if (gridResults.SelectedRows.Count > 0)
            {
                SelectedItem = gridResults.SelectedRows[0].DataBoundItem;
                SelectedType = cmbSearchType.SelectedItem?.ToString();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                PerformSearch();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
