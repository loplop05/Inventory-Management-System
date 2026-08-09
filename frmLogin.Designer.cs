namespace InventoryManagementSystem
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._loginCard = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblUsername = new System.Windows.Forms.Label();
            this._txtUsername = new System.Windows.Forms.TextBox();
            this._lblPassword = new System.Windows.Forms.Label();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._btnLogin = new System.Windows.Forms.Button();
            this._btnExit = new System.Windows.Forms.Button();
            this._loginCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // _loginCard
            // 
            this._loginCard.BackColor = System.Drawing.Color.White;
            this._loginCard.Controls.Add(this._lblTitle);
            this._loginCard.Controls.Add(this._lblUsername);
            this._loginCard.Controls.Add(this._txtUsername);
            this._loginCard.Controls.Add(this._lblPassword);
            this._loginCard.Controls.Add(this._txtPassword);
            this._loginCard.Controls.Add(this._btnLogin);
            this._loginCard.Controls.Add(this._btnExit);
            this._loginCard.Location = new System.Drawing.Point(0, 0);
            this._loginCard.Name = "_loginCard";
            this._loginCard.Size = new System.Drawing.Size(380, 320);
            this._loginCard.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this._lblTitle.Location = new System.Drawing.Point(140, 20);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(100, 32);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Login";
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblUsername
            // 
            this._lblUsername.AutoSize = true;
            this._lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblUsername.Location = new System.Drawing.Point(20, 80);
            this._lblUsername.Name = "_lblUsername";
            this._lblUsername.Size = new System.Drawing.Size(70, 19);
            this._lblUsername.TabIndex = 1;
            this._lblUsername.Text = "Username:";
            // 
            // _txtUsername
            // 
            this._txtUsername.Location = new System.Drawing.Point(20, 105);
            this._txtUsername.Name = "_txtUsername";
            this._txtUsername.Size = new System.Drawing.Size(340, 23);
            this._txtUsername.TabIndex = 2;
            this._txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // _lblPassword
            // 
            this._lblPassword.AutoSize = true;
            this._lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblPassword.Location = new System.Drawing.Point(20, 140);
            this._lblPassword.Name = "_lblPassword";
            this._lblPassword.Size = new System.Drawing.Size(65, 19);
            this._lblPassword.TabIndex = 3;
            this._lblPassword.Text = "Password:";
            // 
            // _txtPassword
            // 
            this._txtPassword.Location = new System.Drawing.Point(20, 165);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.Size = new System.Drawing.Size(340, 23);
            this._txtPassword.TabIndex = 4;
            this._txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // _btnLogin
            // 
            this._btnLogin.Location = new System.Drawing.Point(20, 220);
            this._btnLogin.Name = "_btnLogin";
            this._btnLogin.Size = new System.Drawing.Size(160, 35);
            this._btnLogin.TabIndex = 5;
            this._btnLogin.Text = "Login";
            this._btnLogin.UseVisualStyleBackColor = true;
            this._btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // _btnExit
            // 
            this._btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnExit.Location = new System.Drawing.Point(200, 220);
            this._btnExit.Name = "_btnExit";
            this._btnExit.Size = new System.Drawing.Size(160, 35);
            this._btnExit.TabIndex = 6;
            this._btnExit.Text = "Exit";
            this._btnExit.UseVisualStyleBackColor = true;
            this._btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this._loginCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this._loginCard.ResumeLayout(false);
            this._loginCard.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel _loginCard;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblUsername;
        private System.Windows.Forms.TextBox _txtUsername;
        private System.Windows.Forms.Label _lblPassword;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Button _btnLogin;
        private System.Windows.Forms.Button _btnExit;
    }
}
