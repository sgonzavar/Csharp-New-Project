namespace appUser
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNote1 = new System.Windows.Forms.TextBox();
            this.txtNote2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNote3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNote4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.pnlShow = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblaverage = new System.Windows.Forms.Label();
            this.pnlShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Note 1:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtNote1
            // 
            this.txtNote1.Location = new System.Drawing.Point(146, 73);
            this.txtNote1.Name = "txtNote1";
            this.txtNote1.Size = new System.Drawing.Size(75, 20);
            this.txtNote1.TabIndex = 2;
            // 
            // txtNote2
            // 
            this.txtNote2.Location = new System.Drawing.Point(146, 99);
            this.txtNote2.Name = "txtNote2";
            this.txtNote2.Size = new System.Drawing.Size(75, 20);
            this.txtNote2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Note 2:";
            // 
            // txtNote3
            // 
            this.txtNote3.Location = new System.Drawing.Point(146, 125);
            this.txtNote3.Name = "txtNote3";
            this.txtNote3.Size = new System.Drawing.Size(75, 20);
            this.txtNote3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Note 3:";
            // 
            // txtNote4
            // 
            this.txtNote4.Location = new System.Drawing.Point(146, 151);
            this.txtNote4.Name = "txtNote4";
            this.txtNote4.Size = new System.Drawing.Size(75, 20);
            this.txtNote4.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Note 4:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(76, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 46);
            this.label5.TabIndex = 9;
            this.label5.Text = "AppUser";
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClean.Location = new System.Drawing.Point(49, 216);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(101, 35);
            this.btnClean.TabIndex = 10;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStart.Location = new System.Drawing.Point(180, 216);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 35);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(36, 295);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 13;
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEnd.Location = new System.Drawing.Point(206, 403);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(90, 35);
            this.btnEnd.TabIndex = 16;
            this.btnEnd.Text = "End";
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.button3_Click);
            // 
            // pnlShow
            // 
            this.pnlShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlShow.Controls.Add(this.lblaverage);
            this.pnlShow.Controls.Add(this.label6);
            this.pnlShow.Location = new System.Drawing.Point(21, 271);
            this.pnlShow.Name = "pnlShow";
            this.pnlShow.Size = new System.Drawing.Size(286, 115);
            this.pnlShow.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Average : ";
            // 
            // lblaverage
            // 
            this.lblaverage.Location = new System.Drawing.Point(140, 22);
            this.lblaverage.Name = "lblaverage";
            this.lblaverage.Size = new System.Drawing.Size(100, 23);
            this.lblaverage.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 471);
            this.Controls.Add(this.pnlShow);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNote4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNote3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNote2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNote1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlShow.ResumeLayout(false);
            this.pnlShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNote1;
        private System.Windows.Forms.TextBox txtNote2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNote3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNote4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Panel pnlShow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblaverage;
    }
}

