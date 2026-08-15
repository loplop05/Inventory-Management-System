using System;
using System.Windows.Forms;
using System.IO;
using InventoryDataAccessLayer;
using InventoryManagementSystem;

namespace InventoryManagementSystem
{
    public partial class frmBackupRestore : Form
    {
        public frmBackupRestore()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(txtBackupPath);
            clsFormTheme.ApplyTextBoxStyle(txtRestorePath);
            clsFormTheme.ApplyPrimaryButtonStyle(btnBackup);
            clsFormTheme.ApplyPrimaryButtonStyle(btnRestore);
            clsFormTheme.ApplySecondaryButtonStyle(btnBrowseBackup);
            clsFormTheme.ApplySecondaryButtonStyle(btnBrowseRestore);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            // Set default backup path
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                $"InventoryBackup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
            txtBackupPath.Text = defaultPath;
            txtRestorePath.Text = "";
        }

        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                sfd.FileName = $"InventoryBackup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                sfd.Title = "Save Backup File";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath.Text = sfd.FileName;
                }
            }
        }

        private void btnBrowseRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                ofd.Title = "Select Backup File to Restore";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRestorePath.Text = ofd.FileName;
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBackupPath.Text))
            {
                clsNotify.Warn("Please specify a backup path.");
                return;
            }

            try
            {
                string directory = Path.GetDirectoryName(txtBackupPath.Text);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string errorMessage;
                // Use the backup name (filename without extension) for the backup
                string backupName = Path.GetFileNameWithoutExtension(txtBackupPath.Text);
                
                if (InventoryDataAccessLayer.clsDatabaseBackup.CreateBackup(out errorMessage, backupName))
                {
                    clsNotify.Success("Backup completed successfully!");
                    clsAuditLog.LogAction("Database Backup", 
                        $"Created backup", 
                        "System");
                }
                else
                {
                    clsNotify.Error($"Backup failed: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmBackupRestore.btnBackup_Click", ex);
                clsNotify.Error("Backup failed: " + ex.Message);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRestorePath.Text))
            {
                clsNotify.Warn("Please select a backup file to restore.");
                return;
            }

            if (!File.Exists(txtRestorePath.Text))
            {
                clsNotify.Error("The selected backup file does not exist.");
                return;
            }

            if (!clsNotify.Confirm("Are you sure you want to restore the database? This will replace all existing data and cannot be undone.", "Confirm Restore"))
            {
                return;
            }

            try
            {
                string errorMessage;
                if (InventoryDataAccessLayer.clsDatabaseBackup.RestoreBackup(txtRestorePath.Text, out errorMessage))
                {
                    clsNotify.Success("Restore completed successfully! The application will now restart.");
                    clsAuditLog.LogAction("Database Restore", 
                        $"Restored backup from {txtRestorePath.Text}", 
                        "System");
                    
                    Application.Restart();
                }
                else
                {
                    clsNotify.Error($"Restore failed: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmBackupRestore.btnRestore_Click", ex);
                clsNotify.Error("Restore failed: " + ex.Message);
            }
        }
    }
}
