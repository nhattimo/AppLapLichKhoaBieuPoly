namespace AppLapLichThoiKhoaBieuPoly.UC.UC_Item
{
    partial class UC_Item_Courses_In_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Item_Courses_In_List));
            this.PictureBoxProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.txtNameCourses = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxProduct
            // 
            this.PictureBoxProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.PictureBoxProduct.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PictureBoxProduct.ErrorImage")));
            this.PictureBoxProduct.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxProduct.Image")));
            this.PictureBoxProduct.ImageLocation = "";
            this.PictureBoxProduct.ImageRotate = 0F;
            this.PictureBoxProduct.Location = new System.Drawing.Point(17, 0);
            this.PictureBoxProduct.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxProduct.Name = "PictureBoxProduct";
            this.PictureBoxProduct.Size = new System.Drawing.Size(79, 92);
            this.PictureBoxProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxProduct.TabIndex = 18;
            this.PictureBoxProduct.TabStop = false;
            // 
            // txtNameCourses
            // 
            this.txtNameCourses.BackColor = System.Drawing.Color.Transparent;
            this.txtNameCourses.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNameCourses.Location = new System.Drawing.Point(150, 30);
            this.txtNameCourses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameCourses.Name = "txtNameCourses";
            this.txtNameCourses.Size = new System.Drawing.Size(133, 33);
            this.txtNameCourses.TabIndex = 17;
            this.txtNameCourses.Text = "Lập trình C#";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // UC_Item_Courses_In_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PictureBoxProduct);
            this.Controls.Add(this.txtNameCourses);
            this.Name = "UC_Item_Courses_In_List";
            this.Size = new System.Drawing.Size(416, 90);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox PictureBoxProduct;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtNameCourses;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
