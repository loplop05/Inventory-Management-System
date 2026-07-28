using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmMainMenu : Form
    {
        private ToolTip _toolTip = new ToolTip();

        public frmMainMenu()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Inventory Management System", clsFormTheme.Icons.Home);

            // ── Categories button ──────────────────────────────────────────────
            btnCategories.Text = "Categories";
            btnCategories.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCategories.BackColor = Color.FromArgb(59, 130, 246); // Blue
            btnCategories.ForeColor = Color.White;
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnCategories, "Manage product categories");

            // ── Suppliers button ───────────────────────────────────────────────
            btnSuppliers.Text = "Suppliers";
            btnSuppliers.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSuppliers.BackColor = Color.FromArgb(16, 185, 129); // Green
            btnSuppliers.ForeColor = Color.White;
            btnSuppliers.FlatStyle = FlatStyle.Flat;
            btnSuppliers.FlatAppearance.BorderSize = 0;
            btnSuppliers.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnSuppliers, "Manage suppliers");

            // ── Products button ────────────────────────────────────────────────
            btnProducts.Text = "Products";
            btnProducts.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnProducts.BackColor = Color.FromArgb(245, 158, 11); // Orange
            btnProducts.ForeColor = Color.White;
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnProducts, "Manage products and inventory");

            // ── Receipt Search button ────────────────────────────────────────────
            btnReceiptSearch.Text = "Receipt Search";
            btnReceiptSearch.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnReceiptSearch.BackColor = Color.FromArgb(139, 92, 246); // Purple
            btnReceiptSearch.ForeColor = Color.White;
            btnReceiptSearch.FlatStyle = FlatStyle.Flat;
            btnReceiptSearch.FlatAppearance.BorderSize = 0;
            btnReceiptSearch.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnReceiptSearch, "Search receipts and manage exchanges");

            // ── Print Receipt button ─────────────────────────────────────────────
            btnPrintReceipt.Text = "Print Receipt";
            btnPrintReceipt.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPrintReceipt.BackColor = Color.FromArgb(236, 72, 153); // Pink
            btnPrintReceipt.ForeColor = Color.White;
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.FlatAppearance.BorderSize = 0;
            btnPrintReceipt.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnPrintReceipt, "Print order receipts");

            btnSuppliers.Enabled = true;
            btnProducts.Enabled  = true;
            AddPOSMenuButtons();

            KeyDown += frmMainMenu_KeyDown;
        }

        private void AddPOSMenuButtons()
        {
            // ── POS button ─────────────────────────────────────────────────────
            Button btnPOS = new Button
            {
                Name     = "btnPOS",
                Text     = clsFormTheme.Icons.POS + "\nPoint of Sale",
                Font     = new Font(clsFormTheme.IconFontName, 20F),
                Dock     = DockStyle.Fill
            };
            btnPOS.Click += btnPOS_Click;
            clsFormTheme.ApplySuccessButtonStyle(btnPOS);
            _toolTip.SetToolTip(btnPOS, "Open Point of Sale (POS)");

            // ── Daily Report button ────────────────────────────────────────────
            Button btnDailyReport = new Button
            {
                Name     = "btnDailyReport",
                Text     = clsFormTheme.Icons.Reports + "\nDaily Report",
                Font     = new Font(clsFormTheme.IconFontName, 20F),
                Dock     = DockStyle.Fill
            };
            btnDailyReport.Click += btnDailyReport_Click;
            clsFormTheme.ApplySecondaryButtonStyle(btnDailyReport);
            _toolTip.SetToolTip(btnDailyReport, "View today's sales report");

            // Add to buttons panel (3rd column, rows 0 and 1)
            _buttonsPanel.Controls.Add(btnPOS, 2, 0);
            _buttonsPanel.Controls.Add(btnDailyReport, 2, 1);
        }

        // ── Event handlers ─────────────────────────────────────────────────────

        private void btnCategories_Click(object sender, EventArgs e)
        {
            frmCategoriesManagment frm = new frmCategoriesManagment();
            frm.Show();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliersManagment frm = new frmSuppliersManagment();
            frm.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            frmProductsManagment frm = new frmProductsManagment();
            frm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void btnDailyReport_Click(object sender, EventArgs e)
        {
            frmDailyReport frm = new frmDailyReport();
            frm.ShowDialog();
        }

        private void btnReceiptSearch_Click(object sender, EventArgs e)
        {
            frmReceiptSearch frm = new frmReceiptSearch();
            frm.ShowDialog();
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            frmPrintReceipt frm = new frmPrintReceipt();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmMainMenu_Load(object sender, EventArgs e) { }

        private void _buttonsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
