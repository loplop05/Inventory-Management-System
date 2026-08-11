namespace InventoryManagementSystem
{
    partial class frmCloseShift
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
            this._infoPanel = new System.Windows.Forms.TableLayoutPanel();
            this._lblStartingCash = new System.Windows.Forms.Label();
            this._lblStartingCashValue = new System.Windows.Forms.Label();
            this._lblExpectedCash = new System.Windows.Forms.Label();
            this._lblExpectedCashValue = new System.Windows.Forms.Label();
            this._lblCountedCash = new System.Windows.Forms.Label();
            this._txtCountedCash = new System.Windows.Forms.TextBox();
            this._lblCashDifference = new System.Windows.Forms.Label();
            this._lblCashDifferenceValue = new System.Windows.Forms.Label();
            this._lblNotes = new System.Windows.Forms.Label();
            this._txtNotes = new System.Windows.Forms.TextBox();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnClose = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this._infoPanel.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.ColumnCount = 1;
            this._mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Controls.Add(this._lblTitle, 0, 0);
            this._mainPanel.Controls.Add(this._infoPanel, 0, 1);
            this._mainPanel.Controls.Add(this._buttonsPanel, 0, 2);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.RowCount = 3;
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainPanel.Size = new System.Drawing.Size(500, 450);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._lblTitle.Location = new System.Drawing.Point(3, 0);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(0, 37);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _infoPanel
            // 
            this._infoPanel.ColumnCount = 2;
            this._infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this._infoPanel.Controls.Add(this._lblStartingCash, 0, 0);
            this._infoPanel.Controls.Add(this._lblStartingCashValue, 1, 0);
            this._infoPanel.Controls.Add(this._lblExpectedCash, 0, 1);
            this._infoPanel.Controls.Add(this._lblExpectedCashValue, 1, 1);
            this._infoPanel.Controls.Add(this._lblCountedCash, 0, 2);
            this._infoPanel.Controls.Add(this._txtCountedCash, 1, 2);
            this._infoPanel.Controls.Add(this._lblCashDifference, 0, 3);
            this._infoPanel.Controls.Add(this._lblCashDifferenceValue, 1, 3);
            this._infoPanel.Controls.Add(this._lblNotes, 0, 4);
            this._infoPanel.Controls.Add(this._txtNotes, 1, 4);
            this._infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoPanel.Location = new System.Drawing.Point(3, 53);
            this._infoPanel.Name = "_infoPanel";
            this._infoPanel.RowCount = 5;
            this._infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._infoPanel.Size = new System.Drawing.Size(444, 294);
            this._infoPanel.TabIndex = 1;
            // 
            // _lblStartingCash
            // 
            this._lblStartingCash.AutoSize = true;
            this._lblStartingCash.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblStartingCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._lblStartingCash.Location = new System.Drawing.Point(3, 3);
            this._lblStartingCash.Name = "_lblStartingCash";
            this._lblStartingCash.Size = new System.Drawing.Size(0, 21);
            this._lblStartingCash.TabIndex = 0;
            // 
            // _lblStartingCashValue
            // 
            this._lblStartingCashValue.AutoSize = true;
            this._lblStartingCashValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblStartingCashValue.Location = new System.Drawing.Point(180, 3);
            this._lblStartingCashValue.Name = "_lblStartingCashValue";
            this._lblStartingCashValue.Size = new System.Drawing.Size(0, 20);
            this._lblStartingCashValue.TabIndex = 1;
            // 
            // _lblExpectedCash
            // 
            this._lblExpectedCash.AutoSize = true;
            this._lblExpectedCash.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblExpectedCash.Location = new System.Drawing.Point(3, 33);
            this._lblExpectedCash.Name = "_lblExpectedCash";
            this._lblExpectedCash.Size = new System.Drawing.Size(0, 20);
            this._lblExpectedCash.TabIndex = 2;
            // 
            // _lblExpectedCashValue
            // 
            this._lblExpectedCashValue.AutoSize = true;
            this._lblExpectedCashValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblExpectedCashValue.Location = new System.Drawing.Point(180, 33);
            this._lblExpectedCashValue.Name = "_lblExpectedCashValue";
            this._lblExpectedCashValue.Size = new System.Drawing.Size(0, 20);
            this._lblExpectedCashValue.TabIndex = 3;
            // 
            // _lblCountedCash
            // 
            this._lblCountedCash.AutoSize = true;
            this._lblCountedCash.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblCountedCash.Location = new System.Drawing.Point(3, 63);
            this._lblCountedCash.Name = "_lblCountedCash";
            this._lblCountedCash.Size = new System.Drawing.Size(0, 20);
            this._lblCountedCash.TabIndex = 4;
            // 
            // _txtCountedCash
            // 
            this._txtCountedCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtCountedCash.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._txtCountedCash.Location = new System.Drawing.Point(180, 63);
            this._txtCountedCash.Name = "_txtCountedCash";
            this._txtCountedCash.Size = new System.Drawing.Size(261, 25);
            this._txtCountedCash.TabIndex = 5;
            // 
            // _lblCashDifference
            // 
            this._lblCashDifference.AutoSize = true;
            this._lblCashDifference.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblCashDifference.Location = new System.Drawing.Point(3, 98);
            this._lblCashDifference.Name = "_lblCashDifference";
            this._lblCashDifference.Size = new System.Drawing.Size(0, 20);
            this._lblCashDifference.TabIndex = 6;
            // 
            // _lblCashDifferenceValue
            // 
            this._lblCashDifferenceValue.AutoSize = true;
            this._lblCashDifferenceValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblCashDifferenceValue.Location = new System.Drawing.Point(180, 98);
            this._lblCashDifferenceValue.Name = "_lblCashDifferenceValue";
            this._lblCashDifferenceValue.Size = new System.Drawing.Size(0, 20);
            this._lblCashDifferenceValue.TabIndex = 7;
            // 
            // _lblNotes
            // 
            this._lblNotes.AutoSize = true;
            this._lblNotes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblNotes.Location = new System.Drawing.Point(3, 128);
            this._lblNotes.Name = "_lblNotes";
            this._lblNotes.Size = new System.Drawing.Size(0, 20);
            this._lblNotes.TabIndex = 8;
            // 
            // _txtNotes
            // 
            this._txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtNotes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._txtNotes.Location = new System.Drawing.Point(180, 128);
            this._txtNotes.Multiline = true;
            this._txtNotes.Name = "_txtNotes";
            this._txtNotes.Size = new System.Drawing.Size(261, 163);
            this._txtNotes.TabIndex = 9;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._btnClose);
            this._buttonsPanel.Controls.Add(this._btnCancel);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 350);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(444, 47);
            this._buttonsPanel.TabIndex = 2;
            // 
            // _btnClose
            // 
            this._btnClose.Location = new System.Drawing.Point(324, 3);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(100, 35);
            this._btnClose.TabIndex = 0;
            this._btnClose.Text = "Close Shift";
            this._btnClose.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(218, 3);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmCloseShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCloseShift";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Close Shift";
            this.Load += new System.EventHandler(this.frmCloseShift_Load);
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this._infoPanel.ResumeLayout(false);
            this._infoPanel.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel _mainPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.TableLayoutPanel _infoPanel;
        private System.Windows.Forms.Label _lblStartingCash;
        private System.Windows.Forms.Label _lblStartingCashValue;
        private System.Windows.Forms.Label _lblExpectedCash;
        private System.Windows.Forms.Label _lblExpectedCashValue;
        private System.Windows.Forms.Label _lblCountedCash;
        private System.Windows.Forms.TextBox _txtCountedCash;
        private System.Windows.Forms.Label _lblCashDifference;
        private System.Windows.Forms.Label _lblCashDifferenceValue;
        private System.Windows.Forms.Label _lblNotes;
        private System.Windows.Forms.TextBox _txtNotes;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Button _btnCancel;
    }
}
