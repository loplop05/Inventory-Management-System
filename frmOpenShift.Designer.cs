namespace InventoryManagementSystem
{
    partial class frmOpenShift
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblCashierName = new System.Windows.Forms.Label();
            this._lblDate = new System.Windows.Forms.Label();
            this._lblStartingCash = new System.Windows.Forms.Label();
            this._txtStartingCash = new System.Windows.Forms.TextBox();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnOpen = new System.Windows.Forms.Button();
            this._btnSkip = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.ColumnCount = 1;
            this._mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Controls.Add(this._lblTitle, 0, 0);
            this._mainPanel.Controls.Add(this._lblCashierName, 0, 1);
            this._mainPanel.Controls.Add(this._lblDate, 0, 2);
            this._mainPanel.Controls.Add(this._lblStartingCash, 0, 3);
            this._mainPanel.Controls.Add(this._txtStartingCash, 0, 4);
            this._mainPanel.Controls.Add(this._buttonsPanel, 0, 5);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.RowCount = 6;
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Size = new System.Drawing.Size(400, 300);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblTitle.Location = new System.Drawing.Point(3, 0);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(0, 31);
            this._lblTitle.TabIndex = 0;
            // 
            // _lblCashierName
            // 
            this._lblCashierName.AutoSize = true;
            this._lblCashierName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblCashierName.Location = new System.Drawing.Point(3, 50);
            this._lblCashierName.Name = "_lblCashierName";
            this._lblCashierName.Size = new System.Drawing.Size(0, 20);
            this._lblCashierName.TabIndex = 1;
            // 
            // _lblDate
            // 
            this._lblDate.AutoSize = true;
            this._lblDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblDate.Location = new System.Drawing.Point(3, 80);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(0, 20);
            this._lblDate.TabIndex = 2;
            // 
            // _lblStartingCash
            // 
            this._lblStartingCash.AutoSize = true;
            this._lblStartingCash.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblStartingCash.Location = new System.Drawing.Point(3, 110);
            this._lblStartingCash.Name = "_lblStartingCash";
            this._lblStartingCash.Size = new System.Drawing.Size(0, 20);
            this._lblStartingCash.TabIndex = 3;
            // 
            // _txtStartingCash
            // 
            this._txtStartingCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtStartingCash.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._txtStartingCash.Location = new System.Drawing.Point(3, 140);
            this._txtStartingCash.Name = "_txtStartingCash";
            this._txtStartingCash.Size = new System.Drawing.Size(394, 25);
            this._txtStartingCash.TabIndex = 4;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._btnOpen);
            this._buttonsPanel.Controls.Add(this._btnSkip);
            this._buttonsPanel.Controls.Add(this._btnCancel);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 193);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(394, 104);
            this._buttonsPanel.TabIndex = 5;
            // 
            // _btnOpen
            // 
            this._btnOpen.Location = new System.Drawing.Point(275, 3);
            this._btnOpen.Name = "_btnOpen";
            this._btnOpen.Size = new System.Drawing.Size(100, 35);
            this._btnOpen.TabIndex = 0;
            this._btnOpen.Text = "Open Shift";
            this._btnOpen.UseVisualStyleBackColor = true;
            // 
            // _btnSkip
            // 
            this._btnSkip.Location = new System.Drawing.Point(169, 3);
            this._btnSkip.Name = "_btnSkip";
            this._btnSkip.Size = new System.Drawing.Size(100, 35);
            this._btnSkip.TabIndex = 1;
            this._btnSkip.Text = "Skip";
            this._btnSkip.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(63, 3);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 2;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmOpenShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenShift";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Shift";
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel _mainPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblCashierName;
        private System.Windows.Forms.Label _lblDate;
        private System.Windows.Forms.Label _lblStartingCash;
        private System.Windows.Forms.TextBox _txtStartingCash;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _btnOpen;
        private System.Windows.Forms.Button _btnSkip;
        private System.Windows.Forms.Button _btnCancel;
    }
}
