namespace QLTV.GUI
{
    partial class frmSearch
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTimSach = new System.Windows.Forms.TextBox();
            this.btnTimSach = new System.Windows.Forms.Button();
            this.rdbTenSach = new System.Windows.Forms.RadioButton();
            this.rdbMaSach = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTmDG = new System.Windows.Forms.TextBox();
            this.btnTimDG = new System.Windows.Forms.Button();
            this.rdbTenDG = new System.Windows.Forms.RadioButton();
            this.rdbMaDG = new System.Windows.Forms.RadioButton();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTimSach);
            this.groupBox2.Controls.Add(this.btnTimSach);
            this.groupBox2.Controls.Add(this.rdbTenSach);
            this.groupBox2.Controls.Add(this.rdbMaSach);
            this.groupBox2.Location = new System.Drawing.Point(21, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(822, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm kiếm Sách";
            // 
            // txtTimSach
            // 
            this.txtTimSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimSach.Location = new System.Drawing.Point(234, 43);
            this.txtTimSach.Name = "txtTimSach";
            this.txtTimSach.Size = new System.Drawing.Size(351, 26);
            this.txtTimSach.TabIndex = 3;
            // 
            // btnTimSach
            // 
            this.btnTimSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimSach.Location = new System.Drawing.Point(644, 43);
            this.btnTimSach.Name = "btnTimSach";
            this.btnTimSach.Size = new System.Drawing.Size(84, 26);
            this.btnTimSach.TabIndex = 2;
            this.btnTimSach.Text = "Tìm";
            this.btnTimSach.UseVisualStyleBackColor = true;
            // 
            // rdbTenSach
            // 
            this.rdbTenSach.AutoSize = true;
            this.rdbTenSach.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTenSach.Location = new System.Drawing.Point(69, 61);
            this.rdbTenSach.Name = "rdbTenSach";
            this.rdbTenSach.Size = new System.Drawing.Size(102, 25);
            this.rdbTenSach.TabIndex = 1;
            this.rdbTenSach.TabStop = true;
            this.rdbTenSach.Text = "Tên sách";
            this.rdbTenSach.UseVisualStyleBackColor = true;
            // 
            // rdbMaSach
            // 
            this.rdbMaSach.AutoSize = true;
            this.rdbMaSach.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMaSach.Location = new System.Drawing.Point(73, 19);
            this.rdbMaSach.Name = "rdbMaSach";
            this.rdbMaSach.Size = new System.Drawing.Size(97, 25);
            this.rdbMaSach.TabIndex = 0;
            this.rdbMaSach.TabStop = true;
            this.rdbMaSach.Text = "Mã sách";
            this.rdbMaSach.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTmDG);
            this.groupBox1.Controls.Add(this.btnTimDG);
            this.groupBox1.Controls.Add(this.rdbTenDG);
            this.groupBox1.Controls.Add(this.rdbMaDG);
            this.groupBox1.Location = new System.Drawing.Point(21, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(822, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm Đọc giả";
            // 
            // txtTmDG
            // 
            this.txtTmDG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTmDG.Location = new System.Drawing.Point(234, 43);
            this.txtTmDG.Name = "txtTmDG";
            this.txtTmDG.Size = new System.Drawing.Size(351, 26);
            this.txtTmDG.TabIndex = 3;
            // 
            // btnTimDG
            // 
            this.btnTimDG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimDG.Location = new System.Drawing.Point(644, 43);
            this.btnTimDG.Name = "btnTimDG";
            this.btnTimDG.Size = new System.Drawing.Size(84, 26);
            this.btnTimDG.TabIndex = 2;
            this.btnTimDG.Text = "Tìm";
            this.btnTimDG.UseVisualStyleBackColor = true;
            // 
            // rdbTenDG
            // 
            this.rdbTenDG.AutoSize = true;
            this.rdbTenDG.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTenDG.Location = new System.Drawing.Point(69, 61);
            this.rdbTenDG.Name = "rdbTenDG";
            this.rdbTenDG.Size = new System.Drawing.Size(128, 25);
            this.rdbTenDG.TabIndex = 1;
            this.rdbTenDG.TabStop = true;
            this.rdbTenDG.Text = "Tên đọc giả";
            this.rdbTenDG.UseVisualStyleBackColor = true;
            // 
            // rdbMaDG
            // 
            this.rdbMaDG.AutoSize = true;
            this.rdbMaDG.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMaDG.Location = new System.Drawing.Point(73, 19);
            this.rdbMaDG.Name = "rdbMaDG";
            this.rdbMaDG.Size = new System.Drawing.Size(123, 25);
            this.rdbMaDG.TabIndex = 0;
            this.rdbMaDG.TabStop = true;
            this.rdbMaDG.Text = "Mã đọc giả";
            this.rdbMaDG.UseVisualStyleBackColor = true;
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Location = new System.Drawing.Point(21, 245);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.Size = new System.Drawing.Size(822, 243);
            this.dgvTable.TabIndex = 5;
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 500);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmSearch";
            this.Text = "Tìm kiếm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTimSach;
        private System.Windows.Forms.Button btnTimSach;
        private System.Windows.Forms.RadioButton rdbTenSach;
        private System.Windows.Forms.RadioButton rdbMaSach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTmDG;
        private System.Windows.Forms.Button btnTimDG;
        private System.Windows.Forms.RadioButton rdbTenDG;
        private System.Windows.Forms.RadioButton rdbMaDG;
        private System.Windows.Forms.DataGridView dgvTable;
    }
}