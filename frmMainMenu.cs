using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmMainMenu : Form
    {
        private ToolTip _toolTip = new ToolTip();

        // ── Grid layout constants ───────────────────────────────────────────
        private const int CardWidth = 264;
        private const int CardHeight = 150;
        private const int ColGap = 133;   // horizontal gap between cards
        private const int RowGap = 38;    // vertical gap between rows
        private const int GridLeft = 240;   // left margin of the grid
        private const int GridTop = 140;   // top margin of the grid

        private int Col(int index) => GridLeft + index * (CardWidth + ColGap);
        private int Row(int index) => GridTop + index * (CardHeight + RowGap);

        public frmMainMenu()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            label1.Visible = false;

            clsFormTheme.CreateHeaderPanel(this, "Inventory Management System", clsFormTheme.Icons.Stock);

            // ── Row 0: Categories | Suppliers ──────────────────────────────
            btnCategories.Text = clsFormTheme.Icons.Categories + "\nCategories";
            btnCategories.Font = new Font(clsFormTheme.IconFontName, 20F);
            btnCategories.Location = new Point(Col(0), Row(0));
            btnCategories.Size = new Size(CardWidth, CardHeight);
            clsFormTheme.ApplyPrimaryButtonStyle(btnCategories);
            _toolTip.SetToolTip(btnCategories, "Manage product categories");

            btnSuppliers.Text = clsFormTheme.Icons.Suppliers + "\nSuppliers";
            btnSuppliers.Font = new Font(clsFormTheme.IconFontName, 20F);
            btnSuppliers.Location = new Point(Col(2), Row(0));
            btnSuppliers.Size = new Size(CardWidth, CardHeight);
            clsFormTheme.ApplyPrimaryButtonStyle(btnSuppliers);
            _toolTip.SetToolTip(btnSuppliers, "Manage suppliers");

            // ── Row 1: POS | Products | Daily Report ───────────────────────
            btnProducts.Text = clsFormTheme.Icons.Products + "\nProducts";
            btnProducts.Font = new Font(clsFormTheme.IconFontName, 20F);
            btnProducts.Location = new Point(Col(1), Row(1));   // ← center column
            btnProducts.Size = new Size(CardWidth, CardHeight);
            clsFormTheme.ApplyPrimaryButtonStyle(btnProducts);
            _toolTip.SetToolTip(btnProducts, "Manage products and inventory");

            // ── Exit button ─────────────────────────────────────────────────
            button4.Text = clsFormTheme.Icons.Exit + "  Exit";
            button4.Font = new Font(clsFormTheme.IconFontName, 12F);
            button4.Height = 36;
            clsFormTheme.ApplyDangerButtonStyle(button4);
            _toolTip.SetToolTip(button4, "Exit application (Esc)");

            btnSuppliers.Enabled = true;
            btnProducts.Enabled = true;
            AddPOSMenuButtons();

            KeyDown += frmMainMenu_KeyDown;
            Paint += FrmMainMenu_Paint;
        }
         
        

        private void FrmMainMenu_Paint(object sender, PaintEventArgs e)
        {
            // Draw a card behind every top-level nav button, using its REAL bounds
            foreach (Control c in Controls)
            {
                if (c is Button b && (b == btnCategories || b == btnSuppliers ||
                                       b == btnProducts || b.Name == "btnPOS" ||
                                       b.Name == "btnDailyReport"))
                {
                    clsFormTheme.DrawCard(e.Graphics, b.Bounds);
                }
            }
        }

        private void AddPOSMenuButtons()
        {
            Button btnPOS = new Button
            {
                Name = "btnPOS",
                Text = clsFormTheme.Icons.POS + "\nPoint of Sale",
                Font = new Font(clsFormTheme.IconFontName, 20F),
                Location = new Point(Col(0), Row(1)),
                Size = new Size(CardWidth, CardHeight)
            };
            btnPOS.Click += btnPOS_Click;
            clsFormTheme.ApplySuccessButtonStyle(btnPOS);
            _toolTip.SetToolTip(btnPOS, "Open Point of Sale (POS)");

            Button btnDailyReport = new Button
            {
                Name = "btnDailyReport",
                Text = clsFormTheme.Icons.Reports + "\nDaily Report",
                Font = new Font(clsFormTheme.IconFontName, 20F),
                Location = new Point(Col(2), Row(1)),
                Size = new Size(CardWidth, CardHeight)
            };
            btnDailyReport.Click += btnDailyReport_Click;
            clsFormTheme.ApplySecondaryButtonStyle(btnDailyReport);
            _toolTip.SetToolTip(btnDailyReport, "View today's sales report");

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
