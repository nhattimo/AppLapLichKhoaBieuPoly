namespace AppLapLichThoiKhoaBieuPoly
{
    partial class FormCreateATimetable
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
            this.txtLoad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLoad
            // 
            this.txtLoad.AutoSize = true;
            this.txtLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLoad.Location = new System.Drawing.Point(331, 269);
            this.txtLoad.Name = "txtLoad";
            this.txtLoad.Size = new System.Drawing.Size(339, 25);
            this.txtLoad.TabIndex = 0;
            this.txtLoad.Text = "Data processing ..., please wait a little.";
            // 
            // FormCreateATimetable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 563);
            this.Controls.Add(this.txtLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreateATimetable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateATimetable";
            this.Load += new System.EventHandler(this.CreateATimetable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtLoad;
    }
}