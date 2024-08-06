using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsDatos;

namespace appUser
{
    public partial class Form1 : Form
    {

        #region "Metodos personalizados"

        public void Clear()
        {
            this.txtNote2.Text = "";
            this.txtNote1.Text = "";
            this.txtNote3.Text = "";
            this.txtNote4.Text = "";
            this.lblaverage.Text = string.Empty;
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNote1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            txtNote1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double dblNoteA, dblNoteB, dblNoteC, dblNoteD;

            try
            {
                //Envio de informacion
                dblNoteA = Convert.ToDouble(this.txtNote1.Text);
                dblNoteB = Convert.ToDouble(this.txtNote2.Text);
                dblNoteC = Convert.ToDouble(this.txtNote3.Text);
                dblNoteD = Convert.ToDouble(this.txtNote4.Text);

                // instancia de la clase
                Datos objDato = new Datos();

                //envio de info
                objDato.setNoteA = dblNoteA;
                objDato.setNoteB = dblNoteB;
                objDato.setNoteC = dblNoteC;
                objDato.setNoteD = dblNoteD;

                if (!objDato.calculate()) 
                {
                    MessageBox.Show(" Hubo un error, " + objDato.getError);
                    objDato = null;
                    return;
                }

                //recuperacion de informacion

                lblaverage.Text = objDato.getAverage.ToString();
                objDato = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
