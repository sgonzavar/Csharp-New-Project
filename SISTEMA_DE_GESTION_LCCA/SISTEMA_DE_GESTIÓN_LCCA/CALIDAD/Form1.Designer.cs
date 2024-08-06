
namespace SISTEMA_DE_GESTIÓN_LCCA.CALIDAD
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbcanentra = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbcansinres = new System.Windows.Forms.Label();
            this.lbcansaliente = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbporsaliente = new System.Windows.Forms.Label();
            this.lbporsinres = new System.Windows.Forms.Label();
            this.lbporentrante = new System.Windows.Forms.Label();
            this.lbcantotal = new System.Windows.Forms.Label();
            this.lbportotal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbvisitas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(15, 116);
            this.chart1.Margin = new System.Windows.Forms.Padding(6);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chart1.Size = new System.Drawing.Size(502, 273);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(61)))), ((int)(((byte)(56)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 67);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(649, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "VER ESTADISTICAS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(45, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "SELECCIONE DÍA:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(258, 22);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(375, 30);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "TIPO LLAMADA ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(204, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "CANTIDAD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(333, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(12, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "ENTRANTE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(12, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "SALIENTE:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(12, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "SIN RESPUESTA:";
            // 
            // lbcanentra
            // 
            this.lbcanentra.AutoSize = true;
            this.lbcanentra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcanentra.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbcanentra.Location = new System.Drawing.Point(204, 44);
            this.lbcanentra.Name = "lbcanentra";
            this.lbcanentra.Size = new System.Drawing.Size(23, 25);
            this.lbcanentra.TabIndex = 9;
            this.lbcanentra.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbportotal);
            this.panel2.Controls.Add(this.lbcantotal);
            this.panel2.Controls.Add(this.lbporsaliente);
            this.panel2.Controls.Add(this.lbporsinres);
            this.panel2.Controls.Add(this.lbporentrante);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lbcansaliente);
            this.panel2.Controls.Add(this.lbcansinres);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lbcanentra);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(526, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 168);
            this.panel2.TabIndex = 10;
            // 
            // lbcansinres
            // 
            this.lbcansinres.AutoSize = true;
            this.lbcansinres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcansinres.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbcansinres.Location = new System.Drawing.Point(204, 94);
            this.lbcansinres.Name = "lbcansinres";
            this.lbcansinres.Size = new System.Drawing.Size(23, 25);
            this.lbcansinres.TabIndex = 10;
            this.lbcansinres.Text = "0";
            // 
            // lbcansaliente
            // 
            this.lbcansaliente.AutoSize = true;
            this.lbcansaliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcansaliente.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbcansaliente.Location = new System.Drawing.Point(204, 69);
            this.lbcansaliente.Name = "lbcansaliente";
            this.lbcansaliente.Size = new System.Drawing.Size(23, 25);
            this.lbcansaliente.TabIndex = 11;
            this.lbcansaliente.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(12, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 25);
            this.label8.TabIndex = 12;
            this.label8.Text = "TOTAL:";
            // 
            // lbporsaliente
            // 
            this.lbporsaliente.AutoSize = true;
            this.lbporsaliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbporsaliente.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbporsaliente.Location = new System.Drawing.Point(338, 69);
            this.lbporsaliente.Name = "lbporsaliente";
            this.lbporsaliente.Size = new System.Drawing.Size(23, 25);
            this.lbporsaliente.TabIndex = 15;
            this.lbporsaliente.Text = "0";
            // 
            // lbporsinres
            // 
            this.lbporsinres.AutoSize = true;
            this.lbporsinres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbporsinres.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbporsinres.Location = new System.Drawing.Point(338, 94);
            this.lbporsinres.Name = "lbporsinres";
            this.lbporsinres.Size = new System.Drawing.Size(23, 25);
            this.lbporsinres.TabIndex = 14;
            this.lbporsinres.Text = "0";
            // 
            // lbporentrante
            // 
            this.lbporentrante.AutoSize = true;
            this.lbporentrante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbporentrante.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbporentrante.Location = new System.Drawing.Point(338, 44);
            this.lbporentrante.Name = "lbporentrante";
            this.lbporentrante.Size = new System.Drawing.Size(23, 25);
            this.lbporentrante.TabIndex = 13;
            this.lbporentrante.Text = "0";
            // 
            // lbcantotal
            // 
            this.lbcantotal.AutoSize = true;
            this.lbcantotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcantotal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbcantotal.Location = new System.Drawing.Point(204, 128);
            this.lbcantotal.Name = "lbcantotal";
            this.lbcantotal.Size = new System.Drawing.Size(23, 25);
            this.lbcantotal.TabIndex = 16;
            this.lbcantotal.Text = "0";
            // 
            // lbportotal
            // 
            this.lbportotal.AutoSize = true;
            this.lbportotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbportotal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbportotal.Location = new System.Drawing.Point(338, 128);
            this.lbportotal.Name = "lbportotal";
            this.lbportotal.Size = new System.Drawing.Size(23, 25);
            this.lbportotal.TabIndex = 17;
            this.lbportotal.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-29, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(540, 25);
            this.label9.TabIndex = 11;
            this.label9.Text = "________________________________________________";
            // 
            // lbvisitas
            // 
            this.lbvisitas.AutoSize = true;
            this.lbvisitas.Location = new System.Drawing.Point(526, 296);
            this.lbvisitas.Name = "lbvisitas";
            this.lbvisitas.Size = new System.Drawing.Size(0, 25);
            this.lbvisitas.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1002, 553);
            this.ControlBox = false;
            this.Controls.Add(this.lbvisitas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESTADISTICAS LLAMADAS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbcanentra;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbportotal;
        private System.Windows.Forms.Label lbcantotal;
        private System.Windows.Forms.Label lbporsaliente;
        private System.Windows.Forms.Label lbporsinres;
        private System.Windows.Forms.Label lbporentrante;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbcansaliente;
        private System.Windows.Forms.Label lbcansinres;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbvisitas;
    }
}