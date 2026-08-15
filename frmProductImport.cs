using System;
using System.Windows.Forms;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmProductImport : Form
    {
        public frmProductImport()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(txtFilePath);
            clsFormTheme.ApplyPrimaryButtonStyle(btnImport);
            clsFormTheme.ApplySecondaryButtonStyle(btnDownloadTemplate);
            clsFormTheme.ApplySecondaryButtonStyle(btnBrowse);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose);
        }

        private void frmProductImport_Load(object sender, EventArgs e)
        {
            txtFilePath.Text = "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                ofd.Title = "Select CSV File";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.FileName;
                }
            }
        }

        private void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = "ProductImportTemplate.csv";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
            string errorMessage;
            if (InventoryDataAccessLayer.clsProductImport.GenerateTemplate(sfd.FileName, out errorMessage))
                    {
                        clsNotify.Success("Template downloaded successfully!");
                        txtFilePath.Text = sfd.FileName;
                    }
                    else
                    {
                        clsNotify.Error(errorMessage);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                clsNotify.Warn("Please select a CSV file first.");
                return;
            }

            if (!System.IO.File.Exists(txtFilePath.Text))
            {
                clsNotify.Error("The selected file does not exist.");
                return;
            }

            string errorMessage;
            var result = InventoryDataAccessLayer.clsProductImport.ImportFromCSV(txtFilePath.Text, true, out errorMessage);

            if (result.Success)
            {
                clsNotify.Success($"Import completed successfully! {result.SuccessfulImports} products imported.");
                clsAuditLog.LogAction("Product Import", 
                    $"Successfully imported {result.SuccessfulImports} products from CSV", 
                    "Inventory");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                string message = $"Import completed with errors.\n\n" +
                    $"Total rows: {result.TotalRows}\n" +
                    $"Successful: {result.SuccessfulImports}\n" +
                    $"Failed: {result.FailedImports}\n\n";
                
                if (result.Errors.Count > 0)
                {
                    message += "Errors:\n" + string.Join("\n", result.Errors.Take(5));
                    if (result.Errors.Count > 5)
                        message += $"\n... and {result.Errors.Count - 5} more errors";
                }
                
                if (result.Warnings.Count > 0)
                {
                    message += "\n\nWarnings:\n" + string.Join("\n", result.Warnings.Take(5));
                    if (result.Warnings.Count > 5)
                        message += $"\n... and {result.Warnings.Count - 5} more warnings";
                }

                clsNotify.Warn(message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
