using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InventoryManagementSystem
{
    public partial class frmReorderSuggestions : Form
    {
        public frmReorderSuggestions()
        {
            InitializeComponent();
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyPrimaryButtonStyle(btnExport, clsFormTheme.Icons.Export);
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            clsFormTheme.ApplyGridStyle(dgvReorders);
            clsFormTheme.ApplyComboBoxStyle(cmbThreshold);

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void frmReorderSuggestions_Load(object sender, EventArgs e)
        {
            cmbThreshold.Items.AddRange(new object[] { 5, 10, 15, 20, 25, 30 });
            cmbThreshold.SelectedIndex = 1; // Default to 10
            LoadReorderSuggestions();
            ApplyLocalization();
        }

        private void LoadReorderSuggestions()
        {
            int threshold = Convert.ToInt32(cmbThreshold.SelectedItem);
            var suggestions = clsReorderSuggestions.GetReorderSuggestions(threshold);

            dgvReorders.DataSource = suggestions;
            dgvReorders.AutoGenerateColumns = false;

            if (dgvReorders.Columns.Count == 0)
            {
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductName",
                    HeaderText = "Product",
                    FillWeight = 150
                });
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CategoryName",
                    HeaderText = "Category",
                    FillWeight = 100
                });
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SupplierName",
                    HeaderText = "Supplier",
                    FillWeight = 120
                });
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CurrentStock",
                    HeaderText = "Current Stock",
                    FillWeight = 80
                });
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SuggestedOrderQty",
                    HeaderText = "Order Qty",
                    FillWeight = 70
                });
                dgvReorders.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SuggestedOrderCost",
                    HeaderText = "Cost",
                    FillWeight = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });
            }

            decimal totalCost = clsReorderSuggestions.CalculateTotalReorderCost(suggestions);
            lblTotalCost.Text = "Total Suggested Order Cost: " + totalCost.ToString("C2");
            lblItemCount.Text = suggestions.Count + " item(s) need reordering";
        }

        private void cmbThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReorderSuggestions();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = $"ReorderSuggestions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(sfd.FileName);
                    clsNotify.Success("Reorder suggestions exported successfully!");
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            int threshold = Convert.ToInt32(cmbThreshold.SelectedItem);
            var suggestions = clsReorderSuggestions.GetReorderSuggestions(threshold);
            var bySupplier = clsReorderSuggestions.GetReorderSuggestionsBySupplier(threshold);

            using (var writer = new System.IO.StreamWriter(filePath, false, new System.Text.UTF8Encoding(true)))
            {
                writer.WriteLine("REORDER SUGGESTIONS BY SUPPLIER");
                writer.WriteLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                writer.WriteLine($"Threshold: {threshold} units");
                writer.WriteLine();

                foreach (var supplier in bySupplier)
                {
                    writer.WriteLine($"SUPPLIER: {supplier.Key}");
                    writer.WriteLine("Product,Category,Current Stock,Order Qty,Unit Cost,Total Cost");
                    
                    foreach (var item in supplier.Value)
                    {
                        writer.WriteLine($"\"{item.ProductName}\",\"{item.CategoryName}\",{item.CurrentStock},{item.SuggestedOrderQty},{item.UnitPrice:F2},{item.SuggestedOrderCost:F2}");
                    }
                    
                    decimal supplierTotal = supplier.Value.Sum(s => s.SuggestedOrderCost);
                    writer.WriteLine($"Supplier Total: {supplierTotal:F2}");
                    writer.WriteLine();
                }

                decimal grandTotal = clsReorderSuggestions.CalculateTotalReorderCost(suggestions);
                writer.WriteLine($"GRAND TOTAL: {grandTotal:F2}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Reorder Suggestions");
            lblThreshold.Text = clsLanguageManager.GetString("Reorder Threshold") + ":";
            btnExport.Text = clsLanguageManager.GetString("Export CSV");
            btnClose.Text = clsLanguageManager.GetString("Close");
        }
    }
}
