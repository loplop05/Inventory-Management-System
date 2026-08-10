namespace InventoryManagementSystem
{
    partial class frmSplitPayment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._lblTotalAmount = new System.Windows.Forms.Label();
            this._lblCashAmount = new System.Windows.Forms.Label();
            this._txtCashAmount = new System.Windows.Forms.TextBox();
            this._lblCardAmount = new System.Windows.Forms.Label();
            this._txtCardAmount = new System.Windows.Forms.TextBox();
            this._lblCardType = new System.Windows.Forms.Label();
            this._cmbCardType = new System.Windows.Forms.ComboBox();
            this._lblCardLastFour = new System.Windows.Forms.Label();
            this._txtCardLastFour = new System.Windows.Forms.TextBox();
            this._lblSplitTotal = new System.Windows.Forms.Label();
            this._btnConfirm = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnFullCash = new System.Windows.Forms.Button();
            this._btnFullCard = new System.Windows.Forms.Button();
            this._btn5050 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lblTotalAmount
            // 
            this._lblTotalAmount.AutoSize = true;
            this._lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblTotalAmount.Location = new System.Drawing.Point(20, 20);
            this._lblTotalAmount.Name = "_lblTotalAmount";
            this._lblTotalAmount.Size = new System.Drawing.Size(120, 21);
            this._lblTotalAmount.TabIndex = 0;
            this._lblTotalAmount.Text = "Total Amount: $0.00";
            // 
            // _lblCashAmount
            // 
            this._lblCashAmount.AutoSize = true;
            this._lblCashAmount.Location = new System.Drawing.Point(20, 60);
            this._lblCashAmount.Name = "_lblCashAmount";
            this._lblCashAmount.Size = new System.Drawing.Size(75, 13);
            this._lblCashAmount.TabIndex = 1;
            this._lblCashAmount.Text = "Cash Amount:";
            // 
            // _txtCashAmount
            // 
            this._txtCashAmount.Location = new System.Drawing.Point(20, 80);
            this._txtCashAmount.Name = "_txtCashAmount";
            this._txtCashAmount.Size = new System.Drawing.Size(150, 20);
            this._txtCashAmount.TabIndex = 2;
            this._txtCashAmount.Text = "$0.00";
            // 
            // _lblCardAmount
            // 
            this._lblCardAmount.AutoSize = true;
            this._lblCardAmount.Location = new System.Drawing.Point(200, 60);
            this._lblCardAmount.Name = "_lblCardAmount";
            this._lblCardAmount.Size = new System.Drawing.Size(72, 13);
            this._lblCardAmount.TabIndex = 3;
            this._lblCardAmount.Text = "Card Amount:";
            // 
            // _txtCardAmount
            // 
            this._txtCardAmount.Location = new System.Drawing.Point(200, 80);
            this._txtCardAmount.Name = "_txtCardAmount";
            this._txtCardAmount.Size = new System.Drawing.Size(150, 20);
            this._txtCardAmount.TabIndex = 4;
            this._txtCardAmount.Text = "$0.00";
            // 
            // _lblCardType
            // 
            this._lblCardType.AutoSize = true;
            this._lblCardType.Location = new System.Drawing.Point(200, 110);
            this._lblCardType.Name = "_lblCardType";
            this._lblCardType.Size = new System.Drawing.Size(56, 13);
            this._lblCardType.TabIndex = 5;
            this._lblCardType.Text = "Card Type:";
            // 
            // _cmbCardType
            // 
            this._cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbCardType.Location = new System.Drawing.Point(200, 130);
            this._cmbCardType.Name = "_cmbCardType";
            this._cmbCardType.Size = new System.Drawing.Size(150, 21);
            this._cmbCardType.TabIndex = 6;
            // 
            // _lblCardLastFour
            // 
            this._lblCardLastFour.AutoSize = true;
            this._lblCardLastFour.Location = new System.Drawing.Point(200, 160);
            this._lblCardLastFour.Name = "_lblCardLastFour";
            this._lblCardLastFour.Size = new System.Drawing.Size(82, 13);
            this._lblCardLastFour.TabIndex = 7;
            this._lblCardLastFour.Text = "Last 4 Digits:";
            // 
            // _txtCardLastFour
            // 
            this._txtCardLastFour.Location = new System.Drawing.Point(200, 180);
            this._txtCardLastFour.MaxLength = 4;
            this._txtCardLastFour.Name = "_txtCardLastFour";
            this._txtCardLastFour.Size = new System.Drawing.Size(80, 20);
            this._txtCardLastFour.TabIndex = 8;
            // 
            // _lblSplitTotal
            // 
            this._lblSplitTotal.AutoSize = true;
            this._lblSplitTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSplitTotal.Location = new System.Drawing.Point(20, 220);
            this._lblSplitTotal.Name = "_lblSplitTotal";
            this._lblSplitTotal.Size = new System.Drawing.Size(110, 21);
            this._lblSplitTotal.TabIndex = 9;
            this._lblSplitTotal.Text = "Split Total: $0.00";
            // 
            // _btnConfirm
            // 
            this._btnConfirm.Location = new System.Drawing.Point(20, 260);
            this._btnConfirm.Name = "_btnConfirm";
            this._btnConfirm.Size = new System.Drawing.Size(100, 30);
            this._btnConfirm.TabIndex = 10;
            this._btnConfirm.Text = "Confirm";
            this._btnConfirm.UseVisualStyleBackColor = true;
            this._btnConfirm.Click += new System.EventHandler(this._btnConfirm_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(130, 260);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 30);
            this._btnCancel.TabIndex = 11;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _btnFullCash
            // 
            this._btnFullCash.Location = new System.Drawing.Point(20, 300);
            this._btnFullCash.Name = "_btnFullCash";
            this._btnFullCash.Size = new System.Drawing.Size(80, 25);
            this._btnFullCash.TabIndex = 12;
            this._btnFullCash.Text = "Full Cash";
            this._btnFullCash.UseVisualStyleBackColor = true;
            this._btnFullCash.Click += new System.EventHandler(this._btnFullCash_Click);
            // 
            // _btnFullCard
            // 
            this._btnFullCard.Location = new System.Drawing.Point(110, 300);
            this._btnFullCard.Name = "_btnFullCard";
            this._btnFullCard.Size = new System.Drawing.Size(80, 25);
            this._btnFullCard.TabIndex = 13;
            this._btnFullCard.Text = "Full Card";
            this._btnFullCard.UseVisualStyleBackColor = true;
            this._btnFullCard.Click += new System.EventHandler(this._btnFullCard_Click);
            // 
            // _btn5050
            // 
            this._btn5050.Location = new System.Drawing.Point(200, 300);
            this._btn5050.Name = "_btn5050";
            this._btn5050.Size = new System.Drawing.Size(80, 25);
            this._btn5050.TabIndex = 14;
            this._btn5050.Text = "50/50";
            this._btn5050.UseVisualStyleBackColor = true;
            this._btn5050.Click += new System.EventHandler(this._btn5050_Click);
            // 
            // frmSplitPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 340);
            this.Controls.Add(this._btn5050);
            this.Controls.Add(this._btnFullCard);
            this.Controls.Add(this._btnFullCash);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnConfirm);
            this.Controls.Add(this._lblSplitTotal);
            this.Controls.Add(this._txtCardLastFour);
            this.Controls.Add(this._lblCardLastFour);
            this.Controls.Add(this._cmbCardType);
            this.Controls.Add(this._lblCardType);
            this.Controls.Add(this._txtCardAmount);
            this.Controls.Add(this._lblCardAmount);
            this.Controls.Add(this._txtCashAmount);
            this.Controls.Add(this._lblCashAmount);
            this.Controls.Add(this._lblTotalAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSplitPayment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Split Payment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label _lblTotalAmount;
        private System.Windows.Forms.Label _lblCashAmount;
        private System.Windows.Forms.TextBox _txtCashAmount;
        private System.Windows.Forms.Label _lblCardAmount;
        private System.Windows.Forms.TextBox _txtCardAmount;
        private System.Windows.Forms.Label _lblCardType;
        private System.Windows.Forms.ComboBox _cmbCardType;
        private System.Windows.Forms.Label _lblCardLastFour;
        private System.Windows.Forms.TextBox _txtCardLastFour;
        private System.Windows.Forms.Label _lblSplitTotal;
        private System.Windows.Forms.Button _btnConfirm;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnFullCash;
        private System.Windows.Forms.Button _btnFullCard;
        private System.Windows.Forms.Button _btn5050;
    }
}
