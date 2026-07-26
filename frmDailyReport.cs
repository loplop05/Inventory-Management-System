using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public class frmDailyReport : Form
    {
        private Label _lblOrders;
        private Label _lblSubtotal;
        private Label _lblTax;
        private Label _lblRevenue;
        private DataGridView _gridOrders;
        private DataGridView _gridTopProducts;
        private Button _btnRefresh;
        private Button _btnClose;

        public frmDailyReport()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyGridStyle(_gridOrders);
            clsFormTheme.ApplyGridStyle(_gridTopProducts);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnClose);

            KeyDown += frmDailyReport_KeyDown;
        }

        private void InitializeComponent()
        {
            Text = "End-of-Day Report";
            Width = 1040;
            Height = 700;
            MinimumSize = new Size(900, 600);
            Font = new Font("Segoe UI", 10F);

            TableLayoutPanel root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill;
            root.ColumnCount = 1;
            root.RowCount = 3;
            root.Padding = new Padding(16);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

            TableLayoutPanel summary = new TableLayoutPanel();
            summary.Dock = DockStyle.Fill;
            summary.ColumnCount = 4;
            summary.RowCount = 1;
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            _lblOrders = CreateSummaryLabel("Orders", "0");
            _lblSubtotal = CreateSummaryLabel("Subtotal", "$0.00");
            _lblTax = CreateSummaryLabel("Tax", "$0.00");
            _lblRevenue = CreateSummaryLabel("Revenue", "$0.00");

            summary.Controls.Add(_lblOrders, 0, 0);
            summary.Controls.Add(_lblSubtotal, 1, 0);
            summary.Controls.Add(_lblTax, 2, 0);
            summary.Controls.Add(_lblRevenue, 3, 0);

            SplitContainer split = new SplitContainer();
            split.Dock = DockStyle.Fill;
            split.Orientation = Orientation.Horizontal;
            split.SplitterDistance = 270;

            Panel ordersPanel = CreateSectionPanel("Today's Orders", out _gridOrders);
            Panel topProductsPanel = CreateSectionPanel("Top-Selling Products", out _gridTopProducts);

            split.Panel1.Controls.Add(ordersPanel);
            split.Panel2.Controls.Add(topProductsPanel);

            FlowLayoutPanel buttons = new FlowLayoutPanel();
            buttons.Dock = DockStyle.Fill;
            buttons.FlowDirection = FlowDirection.RightToLeft;

            _btnClose = new Button();
            _btnClose.Text = "Close";
            _btnClose.Size = new Size(100, 34);
            _btnClose.Click += btnClose_Click;

            _btnRefresh = new Button();
            _btnRefresh.Text = "Refresh";
            _btnRefresh.Size = new Size(100, 34);
            _btnRefresh.Click += btnRefresh_Click;

            buttons.Controls.Add(_btnClose);
            buttons.Controls.Add(_btnRefresh);

            root.Controls.Add(summary, 0, 0);
            root.Controls.Add(split, 0, 1);
            root.Controls.Add(buttons, 0, 2);

            Controls.Add(root);
            Load += frmDailyReport_Load;
        }

        private Label CreateSummaryLabel(string caption, string value)
        {
            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.Margin = new Padding(6);
            label.BackColor = Color.White;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label.ForeColor = clsFormTheme.HeaderColor;
            label.Text = caption + Environment.NewLine + value;
            return label;
        }

        private Panel CreateSectionPanel(string title, out DataGridView grid)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.White;
            panel.Padding = new Padding(12);

            Label label = new Label();
            label.Text = title;
            label.Dock = DockStyle.Top;
            label.Height = 34;
            label.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label.ForeColor = clsFormTheme.HeaderColor;

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;

            panel.Controls.Add(grid);
            panel.Controls.Add(label);

            return panel;
        }

        private void LoadReport()
        {
            string errorMessage;
            if (!clsPOS.EnsurePosSetupAndSampleData(out errorMessage))
            {
                MessageBox.Show("Report setup failed: " + errorMessage, "Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable summary = clsPOS.GetTodayOrderSummary();
            if (summary.Rows.Count > 0)
            {
                DataRow row = summary.Rows[0];
                _lblOrders.Text = "Orders" + Environment.NewLine + Convert.ToInt32(row["OrderCount"]);
                _lblSubtotal.Text = "Subtotal" + Environment.NewLine + Convert.ToDecimal(row["Subtotal"]).ToString("C2");
                _lblTax.Text = "Tax" + Environment.NewLine + Convert.ToDecimal(row["TaxAmount"]).ToString("C2");
                _lblRevenue.Text = "Revenue" + Environment.NewLine + Convert.ToDecimal(row["TotalRevenue"]).ToString("C2");
            }

            _gridOrders.DataSource = clsPOS.GetTodayOrders();
            _gridTopProducts.DataSource = clsPOS.GetTodayTopSellingProducts();

            FormatCurrencyColumn(_gridOrders, "Subtotal");
            FormatCurrencyColumn(_gridOrders, "TaxAmount");
            FormatCurrencyColumn(_gridOrders, "TotalAmount");
            FormatCurrencyColumn(_gridTopProducts, "Revenue");
        }

        private void FormatCurrencyColumn(DataGridView grid, string columnName)
        {
            if (grid.Columns.Contains(columnName))
                grid.Columns[columnName].DefaultCellStyle.Format = "C2";
        }

        private void frmDailyReport_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDailyReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadReport();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
