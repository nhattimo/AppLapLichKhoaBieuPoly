namespace AppLapLichThoiKhoaBieuPoly.UC
{
    partial class UC_Department
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Department));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDelete = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnAddDepartments = new Guna.UI2.WinForms.Guna2GradientButton();
            this.DataGridViewDepartments = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnNew = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtDepartmentName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDepartments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.BorderColor = System.Drawing.Color.BurlyWood;
            this.btnDelete.BorderRadius = 10;
            this.btnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDelete.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageSize = new System.Drawing.Size(25, 25);
            this.btnDelete.Location = new System.Drawing.Point(1487, 104);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(184, 57);
            this.btnDelete.TabIndex = 280;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseWaitCursor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddDepartments
            // 
            this.btnAddDepartments.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnAddDepartments.BorderRadius = 10;
            this.btnAddDepartments.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddDepartments.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddDepartments.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddDepartments.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddDepartments.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddDepartments.FillColor = System.Drawing.Color.Lime;
            this.btnAddDepartments.FillColor2 = System.Drawing.Color.Lime;
            this.btnAddDepartments.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddDepartments.ForeColor = System.Drawing.Color.Black;
            this.btnAddDepartments.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDepartments.Image")));
            this.btnAddDepartments.ImageSize = new System.Drawing.Size(25, 25);
            this.btnAddDepartments.Location = new System.Drawing.Point(1369, 108);
            this.btnAddDepartments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddDepartments.Name = "btnAddDepartments";
            this.btnAddDepartments.Size = new System.Drawing.Size(112, 53);
            this.btnAddDepartments.TabIndex = 279;
            this.btnAddDepartments.Text = "Add";
            this.btnAddDepartments.UseWaitCursor = true;
            this.btnAddDepartments.Click += new System.EventHandler(this.btnAddProfessors_Click);
            // 
            // DataGridViewDepartments
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.DataGridViewDepartments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewDepartments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DataGridViewDepartments.ColumnHeadersHeight = 4;
            this.DataGridViewDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewDepartments.DefaultCellStyle = dataGridViewCellStyle9;
            this.DataGridViewDepartments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewDepartments.Location = new System.Drawing.Point(89, 213);
            this.DataGridViewDepartments.Name = "DataGridViewDepartments";
            this.DataGridViewDepartments.RowHeadersVisible = false;
            this.DataGridViewDepartments.RowHeadersWidth = 51;
            this.DataGridViewDepartments.RowTemplate.Height = 24;
            this.DataGridViewDepartments.Size = new System.Drawing.Size(1582, 611);
            this.DataGridViewDepartments.TabIndex = 278;
            this.DataGridViewDepartments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewDepartments.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.DataGridViewDepartments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.DataGridViewDepartments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.DataGridViewDepartments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.DataGridViewDepartments.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewDepartments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.DataGridViewDepartments.ThemeStyle.HeaderStyle.Height = 4;
            this.DataGridViewDepartments.ThemeStyle.ReadOnly = false;
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.Height = 24;
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewDepartments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridViewDepartments.UseWaitCursor = true;
            this.DataGridViewDepartments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewClasses_CellClick);
            // 
            // btnNew
            // 
            this.btnNew.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnNew.BorderRadius = 10;
            this.btnNew.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNew.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNew.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNew.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNew.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNew.FillColor = System.Drawing.Color.Silver;
            this.btnNew.FillColor2 = System.Drawing.Color.Silver;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.Black;
            this.btnNew.ImageSize = new System.Drawing.Size(25, 25);
            this.btnNew.Location = new System.Drawing.Point(1369, 108);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(112, 53);
            this.btnNew.TabIndex = 277;
            this.btnNew.Text = "New";
            this.btnNew.UseWaitCursor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.BackColor = System.Drawing.Color.Transparent;
            this.txtDepartmentName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDepartmentName.BorderRadius = 5;
            this.txtDepartmentName.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.txtDepartmentName.DefaultText = "";
            this.txtDepartmentName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDepartmentName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDepartmentName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDepartmentName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDepartmentName.FillColor = System.Drawing.Color.BlanchedAlmond;
            this.txtDepartmentName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDepartmentName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDepartmentName.ForeColor = System.Drawing.Color.Black;
            this.txtDepartmentName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDepartmentName.Location = new System.Drawing.Point(218, 104);
            this.txtDepartmentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.PasswordChar = '\0';
            this.txtDepartmentName.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtDepartmentName.PlaceholderText = "VD: Abcd";
            this.txtDepartmentName.SelectedText = "";
            this.txtDepartmentName.Size = new System.Drawing.Size(444, 53);
            this.txtDepartmentName.TabIndex = 276;
            this.txtDepartmentName.UseWaitCursor = true;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(218, 77);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(124, 20);
            this.guna2HtmlLabel1.TabIndex = 281;
            this.guna2HtmlLabel1.Text = "Name Department";
            this.guna2HtmlLabel1.UseWaitCursor = true;
            // 
            // UC_Department
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddDepartments);
            this.Controls.Add(this.DataGridViewDepartments);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtDepartmentName);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Name = "UC_Department";
            this.Size = new System.Drawing.Size(1760, 900);
            this.Load += new System.EventHandler(this.UC_Department_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDepartments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton btnDelete;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddDepartments;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewDepartments;
        private Guna.UI2.WinForms.Guna2GradientButton btnNew;
        private Guna.UI2.WinForms.Guna2TextBox txtDepartmentName;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}
