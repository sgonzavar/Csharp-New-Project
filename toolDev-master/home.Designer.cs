
namespace exampleo01
{
    partial class home
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnScheTeach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchDate = new System.Windows.Forms.Button();
            this.btnSearchTeach = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnScheTeach
            // 
            this.btnScheTeach.Image = global::exampleo01.Properties.Resources.user;
            this.btnScheTeach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScheTeach.Location = new System.Drawing.Point(51, 136);
            this.btnScheTeach.Name = "btnScheTeach";
            this.btnScheTeach.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnScheTeach.Size = new System.Drawing.Size(306, 64);
            this.btnScheTeach.TabIndex = 0;
            this.btnScheTeach.Text = "Agendar cita docente";
            this.btnScheTeach.UseVisualStyleBackColor = true;
            this.btnScheTeach.Click += new System.EventHandler(this.btnScheTeach_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(51, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 76);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asesorías";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSearchDate
            // 
            this.btnSearchDate.Image = global::exampleo01.Properties.Resources.clock;
            this.btnSearchDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchDate.Location = new System.Drawing.Point(51, 206);
            this.btnSearchDate.Name = "btnSearchDate";
            this.btnSearchDate.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSearchDate.Size = new System.Drawing.Size(306, 64);
            this.btnSearchDate.TabIndex = 2;
            this.btnSearchDate.Text = "Consultar agenda fecha";
            this.btnSearchDate.UseVisualStyleBackColor = true;
            this.btnSearchDate.Click += new System.EventHandler(this.btnSearchDate_Click);
            // 
            // btnSearchTeach
            // 
            this.btnSearchTeach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSearchTeach.Image = global::exampleo01.Properties.Resources.loupe;
            this.btnSearchTeach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchTeach.Location = new System.Drawing.Point(51, 276);
            this.btnSearchTeach.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchTeach.Name = "btnSearchTeach";
            this.btnSearchTeach.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSearchTeach.Size = new System.Drawing.Size(306, 64);
            this.btnSearchTeach.TabIndex = 3;
            this.btnSearchTeach.Text = "Consultar agenda docente";
            this.btnSearchTeach.UseVisualStyleBackColor = true;
            this.btnSearchTeach.Click += new System.EventHandler(this.btnSearchTeach_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::exampleo01.Properties.Resources.exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(51, 346);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnExit.Size = new System.Drawing.Size(306, 64);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(409, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSearchTeach);
            this.Controls.Add(this.btnSearchDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnScheTeach);
            this.Name = "home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.home_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScheTeach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchDate;
        private System.Windows.Forms.Button btnSearchTeach;
        private System.Windows.Forms.Button btnExit;
    }
}

