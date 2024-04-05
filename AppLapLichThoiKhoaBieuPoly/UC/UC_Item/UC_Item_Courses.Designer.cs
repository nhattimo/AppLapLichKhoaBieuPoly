namespace AppLapLichThoiKhoaBieuPoly.UC.UC_Item
{
    partial class UC_Item_Courses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Item_Courses));
            this.btnDeleteItem = new Guna.UI2.WinForms.Guna2GradientTileButton();
            this.txtNameCourses = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.PictureBoxProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteItem.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteItem.FillColor = System.Drawing.Color.Transparent;
            this.btnDeleteItem.FillColor2 = System.Drawing.Color.Transparent;
            this.btnDeleteItem.FocusedColor = System.Drawing.Color.IndianRed;
            this.btnDeleteItem.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnDeleteItem.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteItem.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.btnDeleteItem.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.btnDeleteItem.HoverState.FillColor2 = System.Drawing.Color.Transparent;
            this.btnDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteItem.Image")));
            this.btnDeleteItem.ImageSize = new System.Drawing.Size(35, 35);
            this.btnDeleteItem.Location = new System.Drawing.Point(356, 0);
            this.btnDeleteItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(62, 92);
            this.btnDeleteItem.TabIndex = 10;
            this.btnDeleteItem.UseTransparentBackground = true;
            // 
            // txtNameCourses
            // 
            this.txtNameCourses.BackColor = System.Drawing.Color.Transparent;
            this.txtNameCourses.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNameCourses.Location = new System.Drawing.Point(134, 30);
            this.txtNameCourses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameCourses.Name = "txtNameCourses";
            this.txtNameCourses.Size = new System.Drawing.Size(133, 33);
            this.txtNameCourses.TabIndex = 11;
            this.txtNameCourses.Text = "Lập trình C#";
            this.txtNameCourses.Click += new System.EventHandler(this.txtNameCourses_Click);
            // 
            // PictureBoxProduct
            // 
            this.PictureBoxProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.PictureBoxProduct.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PictureBoxProduct.ErrorImage")));
            this.PictureBoxProduct.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxProduct.Image")));
            this.PictureBoxProduct.ImageLocation = "";
            this.PictureBoxProduct.ImageRotate = 0F;
            this.PictureBoxProduct.Location = new System.Drawing.Point(12, 0);
            this.PictureBoxProduct.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxProduct.Name = "PictureBoxProduct";
            this.PictureBoxProduct.Size = new System.Drawing.Size(79, 92);
            this.PictureBoxProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxProduct.TabIndex = 16;
            this.PictureBoxProduct.TabStop = false;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // UC_Item_Courses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxProduct);
            this.Controls.Add(this.txtNameCourses);
            this.Controls.Add(this.btnDeleteItem);
            this.Name = "UC_Item_Courses";
            this.Size = new System.Drawing.Size(418, 92);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientTileButton btnDeleteItem;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtNameCourses;
        private Guna.UI2.WinForms.Guna2PictureBox PictureBoxProduct;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
