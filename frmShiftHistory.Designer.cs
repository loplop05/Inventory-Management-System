namespace InventoryManagementSystem
{
    partial class frmShiftHistory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblSearch = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._dgvShifts = new System.Windows.Forms.DataGridView();
            this._lblRecordCount = new System.Windows.Forms.Label();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._btnFilter = new System.Windows.Forms.Button();
            this._btnClose = new System.Windows.Forms.Button();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this._dgvShifts)).BeginInit();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._lblTitle.Location = new System.Drawing.Point(20, 20);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(0, 32);
            this._lblTitle.TabIndex = 0;
            // 
            // _lblSearch
            // 
            this._lblSearch.AutoSize = true;
            this._lblSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblSearch.Location = new System.Drawing.Point(20, 65);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(0, 20);
            this._lblSearch.TabIndex = 1;
            // 
            // _txtSearch
            // 
            this._txtSearch.Location = new System.Drawing.Point(80, 62);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(200, 25);
            this._txtSearch.TabIndex = 2;
            // 
            // _dgvShifts
            // 
            this._dgvShifts.AllowUserToAddRows = false;
            this._dgvShifts.AllowUserToDeleteRows = false;
            this._dgvShifts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this._dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvShifts.Location = new System.Drawing.Point(20, 95);
            this._dgvShifts.Name = "_dgvShifts";
            this._dgvShifts.ReadOnly = true;
            this._dgvShifts.RowHeadersVisible = false;
            this._dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvShifts.Size = new System.Drawing.Size(1160, 450);
            this._dgvShifts.TabIndex = 3;
            // 
            // _lblRecordCount
            // 
            this._lblRecordCount.AutoSize = true;
            this._lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblRecordCount.ForeColor = System.Drawing.Color.Gray;
            this._lblRecordCount.Location = new System.Drawing.Point(20, 555);
            this._lblRecordCount.Name = "_lblRecordCount";
            this._lblRecordCount.Size = new System.Drawing.Size(0, 19);
            this._lblRecordCount.TabIndex = 4;
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Location = new System.Drawing.Point(3, 3);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(100, 35);
            this._btnRefresh.TabIndex = 0;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            // 
            // _btnFilter
            // 
            this._btnFilter.Location = new System.Drawing.Point(109, 3);
            this._btnFilter.Name = "_btnFilter";
            this._btnFilter.Size = new System.Drawing.Size(100, 35);
            this._btnFilter.TabIndex = 1;
            this._btnFilter.Text = "Filter";
            this._btnFilter.UseVisualStyleBackColor = true;
            // 
            // _btnClose
            // 
            this._btnClose.Location = new System.Drawing.Point(215, 3);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(100, 35);
            this._btnClose.TabIndex = 2;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._btnRefresh);
            this._buttonsPanel.Controls.Add(this._btnFilter);
            this._buttonsPanel.Controls.Add(this._btnClose);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 590);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(1200, 50);
            this._buttonsPanel.TabIndex = 5;
            // 
            // frmShiftHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1200, 640);
            this.Controls.Add(this._buttonsPanel);
            this.Controls.Add(this._lblRecordCount);
            this.Controls.Add(this._dgvShifts);
            this.Controls.Add(this._txtSearch);
            this.Controls.Add(this._lblSearch);
            this.Controls.Add(this._lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "frmShiftHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shift History";
            this.Load += new System.EventHandler(this.frmShiftHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvShifts)).EndInit();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblSearch;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.DataGridView _dgvShifts;
        private System.Windows.Forms.Label _lblRecordCount;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button _btnFilter;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
    }
}
