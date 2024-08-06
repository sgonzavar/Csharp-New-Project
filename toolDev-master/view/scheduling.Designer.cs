
namespace exampleo01
{
    partial class scheduling
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameStudent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbHours = new System.Windows.Forms.ComboBox();
            this.txtTheme = new System.Windows.Forms.TextBox();
            this.btnScheduling = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSche = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTeach = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSche)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(102, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agendamiento";
            // 
            // txtNameStudent
            // 
            this.txtNameStudent.Location = new System.Drawing.Point(118, 73);
            this.txtNameStudent.Margin = new System.Windows.Forms.Padding(2);
            this.txtNameStudent.Name = "txtNameStudent";
            this.txtNameStudent.PlaceholderText = "Nombre de estudiante";
            this.txtNameStudent.Size = new System.Drawing.Size(231, 23);
            this.txtNameStudent.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(64, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha :";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(118, 101);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(231, 23);
            this.dtpDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(69, 134);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hora :";
            // 
            // cmbHours
            // 
            this.cmbHours.FormattingEnabled = true;
            this.cmbHours.Location = new System.Drawing.Point(118, 129);
            this.cmbHours.Margin = new System.Windows.Forms.Padding(2);
            this.cmbHours.Name = "cmbHours";
            this.cmbHours.Size = new System.Drawing.Size(112, 23);
            this.cmbHours.TabIndex = 6;
            // 
            // txtTheme
            // 
            this.txtTheme.Location = new System.Drawing.Point(50, 185);
            this.txtTheme.Margin = new System.Windows.Forms.Padding(2);
            this.txtTheme.Multiline = true;
            this.txtTheme.Name = "txtTheme";
            this.txtTheme.PlaceholderText = "Tema";
            this.txtTheme.Size = new System.Drawing.Size(299, 69);
            this.txtTheme.TabIndex = 8;
            // 
            // btnScheduling
            // 
            this.btnScheduling.Image = global::exampleo01.Properties.Resources.download;
            this.btnScheduling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScheduling.Location = new System.Drawing.Point(198, 362);
            this.btnScheduling.Margin = new System.Windows.Forms.Padding(2);
            this.btnScheduling.Name = "btnScheduling";
            this.btnScheduling.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnScheduling.Size = new System.Drawing.Size(150, 42);
            this.btnScheduling.TabIndex = 9;
            this.btnScheduling.Text = "Agendar";
            this.btnScheduling.UseVisualStyleBackColor = true;
            this.btnScheduling.Click += new System.EventHandler(this.btnScheduling_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(50, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nombre :";
            // 
            // dgvSche
            // 
            this.dgvSche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSche.Location = new System.Drawing.Point(50, 265);
            this.dgvSche.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSche.Name = "dgvSche";
            this.dgvSche.RowHeadersWidth = 62;
            this.dgvSche.RowTemplate.Height = 33;
            this.dgvSche.Size = new System.Drawing.Size(298, 84);
            this.dgvSche.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(48, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Profesor :";
            // 
            // cmbTeach
            // 
            this.cmbTeach.FormattingEnabled = true;
            this.cmbTeach.Location = new System.Drawing.Point(118, 157);
            this.cmbTeach.Name = "cmbTeach";
            this.cmbTeach.Size = new System.Drawing.Size(153, 23);
            this.cmbTeach.TabIndex = 13;
            // 
            // scheduling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(395, 440);
            this.Controls.Add(this.cmbTeach);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvSche);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnScheduling);
            this.Controls.Add(this.txtTheme);
            this.Controls.Add(this.cmbHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNameStudent);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "scheduling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "scheduling";
            this.Load += new System.EventHandler(this.scheduling_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSche)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameStudent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbHours;
        private System.Windows.Forms.TextBox txtTheme;
        private System.Windows.Forms.Button btnScheduling;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvSche;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTeach;
    }
}