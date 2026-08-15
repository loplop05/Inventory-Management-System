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
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Size = new System.Drawing.Size(450, 380);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._lblTitle.Location = new System.Drawing.Point(3, 0);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(0, 46);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblCashierName
            // 
            this._lblCashierName.AutoSize = true;
            this._lblCashierName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblCashierName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._lblCashierName.Location = new System.Drawing.Point(3, 60);
            this._lblCashierName.Name = "_lblCashierName";
            this._lblCashierName.Size = new System.Drawing.Size(0, 28);
            this._lblCashierName.TabIndex = 1;
            // 
            // _lblDate
            // 
            this._lblDate.AutoSize = true;
            this._lblDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._lblDate.Location = new System.Drawing.Point(3, 95);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(0, 28);
            this._lblDate.TabIndex = 2;
            // 
            // _lblStartingCash
            // 
            this._lblStartingCash.AutoSize = true;
            this._lblStartingCash.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblStartingCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._lblStartingCash.Location = new System.Drawing.Point(3, 130);
            this._lblStartingCash.Name = "_lblStartingCash";
            this._lblStartingCash.Size = new System.Drawing.Size(0, 28);
            this._lblStartingCash.TabIndex = 3;
            // 
            // _txtStartingCash
            // 
            this._txtStartingCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtStartingCash.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._txtStartingCash.Location = new System.Drawing.Point(3, 168);
            this._txtStartingCash.Name = "_txtStartingCash";
            this._txtStartingCash.Size = new System.Drawing.Size(444, 34);
            this._txtStartingCash.TabIndex = 4;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._btnOpen);
            this._buttonsPanel.Controls.Add(this._btnSkip);
            this._buttonsPanel.Controls.Add(this._btnCancel);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 218);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(444, 159);
            this._buttonsPanel.TabIndex = 5;
            // 
            // _btnOpen
            // 
            this._btnOpen.Location = new System.Drawing.Point(321, 3);
            this._btnOpen.Name = "_btnOpen";
            this._btnOpen.Size = new System.Drawing.Size(120, 45);
            this._btnOpen.TabIndex = 0;
            this._btnOpen.Text = "Open Shift";
            this._btnOpen.UseVisualStyleBackColor = true;
            // 
            // _btnSkip
            // 
            this._btnSkip.Location = new System.Drawing.Point(195, 3);
            this._btnSkip.Name = "_btnSkip";
            this._btnSkip.Size = new System.Drawing.Size(120, 45);
            this._btnSkip.TabIndex = 1;
            this._btnSkip.Text = "Skip";
            this._btnSkip.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(69, 3);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(120, 45);
            this._btnCancel.TabIndex = 2;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmOpenShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenShift";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Shift";
            this.Load += new System.EventHandler(this.frmOpenShift_Load);
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
