using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace ViewPaymed
{
    public partial class Form1 : Form
    {
        #region "Metodos personalizados"

        public void Clear()
        {
            this.txtName.Text = string.Empty;
            this.txtPayment.Text = string.Empty;
            this.txtWorkDays.Text = string.Empty;
            this.lblNameOfEmpl.Text = string.Empty;
            this.lblTotalPaymed.Text = string.Empty;
        }

        #endregion


        #region "Variables Globales"

        private string strName;
        private int intWorkDays;
        private double dblPayment;
        private double dblTotalPaymed;
        clsDatos data = new clsDatos();

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            this.txtName.Focus();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                strName = Convert.ToString(txtName.Text);
                intWorkDays = Convert.ToInt32(txtWorkDays.Text);
                dblPayment = Convert.ToDouble(txtPayment.Text);

                //envio de informacion
                data.setName = strName;
                data.setPayment = dblPayment;
                data.setWorkDays = intWorkDays;

                if (!data.calculate())
                {
                    MessageBox.Show(" Hubo un error, " + data.getError);
                    data = null;
                    return;
                }

                //recuperacion de informacion

                lblNameOfEmpl.Text = data.getName.ToString();
                lblTotalPaymed.Text = data.getTotalPaymed.ToString();
                data = null;


            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error, " + ex.Message);
            }

        }
    }
}
