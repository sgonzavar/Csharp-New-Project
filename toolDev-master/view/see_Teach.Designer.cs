
namespace exampleo01.view
{
    partial class see_Teach
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
            this.cmbTeach = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvTeach = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeach)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(38, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione Un Docente :";
            // 
            // cmbTeach
            // 
            this.cmbTeach.FormattingEnabled = true;
            this.cmbTeach.Location = new System.Drawing.Point(245, 52);
            this.cmbTeach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTeach.Name = "cmbTeach";
            this.cmbTeach.Size = new System.Drawing.Size(129, 23);
            this.cmbTeach.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(391, 50);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 20);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvTeach
            // 
            this.dgvTeach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeach.Location = new System.Drawing.Point(38, 103);
            this.dgvTeach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvTeach.Name = "dgvTeach";
            this.dgvTeach.RowHeadersWidth = 62;
            this.dgvTeach.RowTemplate.Height = 33;
            this.dgvTeach.Size = new System.Drawing.Size(441, 176);
            this.dgvTeach.TabIndex = 3;
            // 
            // see_Teach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(526, 321);
            this.Controls.Add(this.dgvTeach);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbTeach);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "see_Teach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "see_Teach";
            this.Load += new System.EventHandler(this.see_Teach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTeach;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTeach;
    }
}