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
            label1.Visible = false; // Hide the old label

            // Header with a stock/inventory icon
            clsFormTheme.CreateHeaderPanel(this, "Inventory Management System", clsFormTheme.Icons.Stock);

            // ── Categories button ──────────────────────────────────────────────
            btnCategories.Text = clsFormTheme.Icons.Categories + "\nCategories";
            btnCategories.Font = new Font(clsFormTheme.IconFontName, 20F);
            clsFormTheme.ApplyPrimaryButtonStyle(btnCategories);
            _toolTip.SetToolTip(btnCategories, "Manage product categories");

            // ── Suppliers button ───────────────────────────────────────────────
            btnSuppliers.Text = clsFormTheme.Icons.Suppliers + "\nSuppliers";
            btnSuppliers.Font = new Font(clsFormTheme.IconFontName, 20F);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSuppliers);
            _toolTip.SetToolTip(btnSuppliers, "Manage suppliers");

            // ── Products button ────────────────────────────────────────────────
            btnProducts.Text = clsFormTheme.Icons.Products + "\nProducts";
            btnProducts.Font = new Font(clsFormTheme.IconFontName, 20F);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProducts);
            _toolTip.SetToolTip(btnProducts, "Manage products and inventory");

            // ── Exit button ────────────────────────────────────────────────────
            button4.Text = clsFormTheme.Icons.Exit + "  Exit";
            button4.Font = new Font(clsFormTheme.IconFontName, 12F);
            clsFormTheme.ApplyDangerButtonStyle(button4);
            _toolTip.SetToolTip(button4, "Exit application (Esc)");

            btnSuppliers.Enabled = true;
            btnProducts.Enabled  = true;
            AddPOSMenuButtons();

            KeyDown += frmMainMenu_KeyDown;
            Paint   += FrmMainMenu_Paint;
        }

        private void FrmMainMenu_Paint(object sender, PaintEventArgs e)
        {
            // Draw styled cards behind the main navigation buttons
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(240, 140, 264, 150)); // Categories
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(657, 140, 264, 150)); // Suppliers
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(458, 328, 264, 150)); // Products
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(240, 328, 264, 150)); // POS
            clsFormTheme.DrawCard(e.Graphics, new Rectangle(657, 328, 264, 150)); // Daily Report
        }

        private void AddPOSMenuButtons()
        {
            // ── POS button ─────────────────────────────────────────────────────
            Button btnPOS = new Button
            {
                Name     = "btnPOS",
                Text     = clsFormTheme.Icons.POS + "\nPoint of Sale",
                Font     = new Font(clsFormTheme.IconFontName, 20F),
                Location = new Point(256, 349),
                Size     = new Size(232, 109)
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
                Location = new Point(673, 349),
                Size     = new Size(232, 109)
            };
            btnDailyReport.Click += btnDailyReport_Click;
            clsFormTheme.ApplySecondaryButtonStyle(btnDailyReport);
            _toolTip.SetToolTip(btnDailyReport, "View today's sales report");

            btnProducts.Location = new Point(474, 255);

            Controls.Add(btnPOS);
            Controls.Add(btnDailyReport);
        }

        // ── Event handlers ─────────────────────────────────────────────────────

        private void label1_Click(object sender, EventArgs e) { }

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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmMainMenu_Load(object sender, EventArgs e) { }
    }
}
