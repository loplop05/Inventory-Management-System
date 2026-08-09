using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmAuditLog : Form
    {
        public frmAuditLog()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);

            clsFormTheme.ApplyComboBoxStyle(cmbModule);
            clsFormTheme.ApplyTextBoxStyle(txtSearch);
            clsFormTheme.ApplyPrimaryButtonStyle(btnExport);
            clsFormTheme.ApplySecondaryButtonStyle(btnRefresh);
            clsFormTheme.ApplyDangerButtonStyle(btnClear);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
            clsFormTheme.ApplyGridStyle(dgvAuditLogs);

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void frmAuditLog_Load(object sender, EventArgs e)
        {
            cmbModule.Items.Clear();
            cmbModule.Items.AddRange(new object[] { "All", "System", "Products", "Categories", "Suppliers", "POS", "Coupons" });
            cmbModule.SelectedIndex = 0;

            ApplyLocalization();
            LoadAuditLogs();
        }

        private void frmAuditLog_Activated(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Audit Logs");
            _lblHeaderTitle.Text = clsLanguageManager.GetString("Audit Logs");
            lblModule.Text = clsLanguageManager.GetString("Module") + ":";
            lblSearch.Text = clsLanguageManager.GetString("Search") + ":";
            btnExport.Text = clsLanguageManager.GetString("Export CSV");
            btnRefresh.Text = clsLanguageManager.GetString("Refresh");
            btnClear.Text = clsLanguageManager.GetString("Clear Logs");
            btnClose.Text = clsLanguageManager.GetString("Close");
        }

        private void LoadAuditLogs()
        {
            string selectedModule = cmbModule.SelectedItem?.ToString();
            string keyword = txtSearch.Text;

            List<AuditEntry> logs = clsAuditLog.GetLogs(selectedModule, keyword);
            dgvAuditLogs.DataSource = null;
            dgvAuditLogs.DataSource = logs;

            if (dgvAuditLogs.Columns.Count > 0)
            {
                if (dgvAuditLogs.Columns["Id"] != null) dgvAuditLogs.Columns["Id"].Width = 80;
                if (dgvAuditLogs.Columns["Timestamp"] != null) dgvAuditLogs.Columns["Timestamp"].Width = 140;
                if (dgvAuditLogs.Columns["Module"] != null) dgvAuditLogs.Columns["Module"].Width = 100;
                if (dgvAuditLogs.Columns["Action"] != null) dgvAuditLogs.Columns["Action"].Width = 150;
                if (dgvAuditLogs.Columns["User"] != null) dgvAuditLogs.Columns["User"].Width = 100;
            }
        }

        private void cmbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = $"AuditLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string moduleFilter = cmbModule.SelectedItem?.ToString();
                    var logs = clsAuditLog.GetLogs(moduleFilter, txtSearch.Text);
                    clsAuditLog.ExportToCSV(logs, sfd.FileName);
                    clsNotify.Success("Audit logs exported successfully!");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (clsNotify.Confirm("Are you sure you want to clear all audit logs?", "Clear Logs"))
            {
                clsAuditLog.ClearLogs();
                LoadAuditLogs();
                clsNotify.Success("Audit logs cleared.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
