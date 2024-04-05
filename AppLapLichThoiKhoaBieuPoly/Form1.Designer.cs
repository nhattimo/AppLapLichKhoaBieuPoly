namespace AppLapLichThoiKhoaBieuPoly
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnNoAccount = new Guna.UI2.WinForms.Guna2Button();
            this.errorLoginFailed = new System.Windows.Forms.Label();
            this.btnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.errorPassword = new System.Windows.Forms.Label();
            this.errorAccount = new System.Windows.Forms.Label();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNameAccount = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.guna2Button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("guna2Button1.BackgroundImage")));
            this.guna2Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.guna2Button1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.Black;
            this.guna2Button1.Location = new System.Drawing.Point(820, 363);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.PressedColor = System.Drawing.Color.White;
            this.guna2Button1.Size = new System.Drawing.Size(41, 53);
            this.guna2Button1.TabIndex = 24;
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            this.guna2Button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.guna2Button1_MouseDown);
            this.guna2Button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.guna2Button1_MouseUp);
            // 
            // btnNoAccount
            // 
            this.btnNoAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.BorderColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.DisabledState.BorderColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.DisabledState.CustomBorderColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.DisabledState.FillColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNoAccount.FillColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.Font = new System.Drawing.Font("Constantia", 10.8F, System.Drawing.FontStyle.Underline);
            this.btnNoAccount.ForeColor = System.Drawing.Color.Black;
            this.btnNoAccount.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.HoverState.ForeColor = System.Drawing.Color.Gray;
            this.btnNoAccount.Location = new System.Drawing.Point(430, 530);
            this.btnNoAccount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNoAccount.Name = "btnNoAccount";
            this.btnNoAccount.PressedColor = System.Drawing.Color.Transparent;
            this.btnNoAccount.Size = new System.Drawing.Size(256, 41);
            this.btnNoAccount.TabIndex = 22;
            this.btnNoAccount.Text = "Bạn chưa có tài khoản!";
            this.btnNoAccount.Click += new System.EventHandler(this.btnNoAccount_Click);
            // 
            // errorLoginFailed
            // 
            this.errorLoginFailed.AutoSize = true;
            this.errorLoginFailed.BackColor = System.Drawing.Color.Transparent;
            this.errorLoginFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.errorLoginFailed.ForeColor = System.Drawing.Color.Red;
            this.errorLoginFailed.Location = new System.Drawing.Point(629, 448);
            this.errorLoginFailed.Name = "errorLoginFailed";
            this.errorLoginFailed.Size = new System.Drawing.Size(82, 18);
            this.errorLoginFailed.TabIndex = 21;
            this.errorLoginFailed.Text = "Login failed";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderRadius = 10;
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogin.FillColor = System.Drawing.Color.Lime;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Location = new System.Drawing.Point(464, 480);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.PressedColor = System.Drawing.Color.White;
            this.btnLogin.Size = new System.Drawing.Size(413, 46);
            this.btnLogin.TabIndex = 20;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // errorPassword
            // 
            this.errorPassword.AutoSize = true;
            this.errorPassword.BackColor = System.Drawing.Color.Transparent;
            this.errorPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.errorPassword.ForeColor = System.Drawing.Color.Red;
            this.errorPassword.Location = new System.Drawing.Point(461, 418);
            this.errorPassword.Name = "errorPassword";
            this.errorPassword.Size = new System.Drawing.Size(42, 18);
            this.errorPassword.TabIndex = 19;
            this.errorPassword.Text = "Error";
            // 
            // errorAccount
            // 
            this.errorAccount.AutoSize = true;
            this.errorAccount.BackColor = System.Drawing.Color.Transparent;
            this.errorAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.errorAccount.ForeColor = System.Drawing.Color.Red;
            this.errorAccount.Location = new System.Drawing.Point(461, 334);
            this.errorAccount.Name = "errorAccount";
            this.errorAccount.Size = new System.Drawing.Size(42, 18);
            this.errorAccount.TabIndex = 18;
            this.errorAccount.Text = "Error";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtPassword.BorderRadius = 10;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtPassword.IconLeft")));
            this.txtPassword.Location = new System.Drawing.Point(464, 363);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(413, 53);
            this.txtPassword.TabIndex = 17;
            // 
            // txtNameAccount
            // 
            this.txtNameAccount.BackColor = System.Drawing.Color.Transparent;
            this.txtNameAccount.BorderRadius = 10;
            this.txtNameAccount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNameAccount.DefaultText = "";
            this.txtNameAccount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNameAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNameAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNameAccount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNameAccount.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtNameAccount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNameAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNameAccount.ForeColor = System.Drawing.Color.Black;
            this.txtNameAccount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNameAccount.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtNameAccount.IconLeft")));
            this.txtNameAccount.Location = new System.Drawing.Point(464, 279);
            this.txtNameAccount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameAccount.Name = "txtNameAccount";
            this.txtNameAccount.PasswordChar = '\0';
            this.txtNameAccount.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtNameAccount.PlaceholderText = "Name account";
            this.txtNameAccount.SelectedText = "";
            this.txtNameAccount.Size = new System.Drawing.Size(413, 53);
            this.txtNameAccount.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Constantia", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.LightCyan;
            this.label3.Location = new System.Drawing.Point(565, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 53);
            this.label3.TabIndex = 15;
            this.label3.Text = "Welcome";
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Black;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1278, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(65, 50);
            this.guna2ControlBox1.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Constantia", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(549, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 73);
            this.label1.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Constantia", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(121, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1101, 73);
            this.label2.TabIndex = 27;
            this.label2.Text = "Application for schedule arrangement";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1342, 756);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.btnNoAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.errorLoginFailed);
            this.Controls.Add(this.txtNameAccount);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.errorPassword);
            this.Controls.Add(this.errorAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button btnNoAccount;
        private System.Windows.Forms.Label errorLoginFailed;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private System.Windows.Forms.Label errorPassword;
        private System.Windows.Forms.Label errorAccount;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtNameAccount;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

