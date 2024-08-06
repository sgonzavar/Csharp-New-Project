using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace SISTEMA_DE_GESTIÓN_LCCA.CALIDAD
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            chart1.Titles.Add("TIPO DE LLAMADAS");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Series serie = new Series();
            chart1.Series.Clear();
            serie.Points.Clear();
            int visita = 0;
            double[] d_tipollamada = { 0, 0, 0 };

            string fecha = dateTimePicker1.Text;
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from Llamadas WHERE FECHA_REGISTRO = '" + fecha + "'", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            foreach (DataRow row in dataTable22.Rows)
            {

                if (Convert.ToString(row["TIPO_LLAMADA"]) == "ENTRANTE")
                {
                    d_tipollamada[0] += 1;
                }
                else if (Convert.ToString(row["TIPO_LLAMADA"]) == "SALIENTE")
                {
                    d_tipollamada[1] += 1;
                }
                else if (Convert.ToString(row["TIPO_LLAMADA"]) == "SIN CONTESTAR")
                {
                    d_tipollamada[2] += 1;
                }
                if (Convert.ToString(row["VISITA"]) == "TRUE      ")
                {
                    visita += 1;
                }
            }
            string[] tipollamada = { "ENTRANTE", "SALIENTE", "SIN CONTESTAR" };

            for (int i = 0; i < tipollamada.Length; i++)
            {
                serie = chart1.Series.Add(tipollamada[i]);

                serie.Label = d_tipollamada[i].ToString();

                serie.Points.Add(d_tipollamada[i]);
            }

            double total = d_tipollamada[0] + d_tipollamada[1] + d_tipollamada[2];
            lbcanentra.Text = Convert.ToString(d_tipollamada[0]);
            lbcansaliente.Text = Convert.ToString(d_tipollamada[1]);
            lbcansinres.Text = Convert.ToString(d_tipollamada[2]);
            lbcantotal.Text = Convert.ToString(total);

            double l1 = (d_tipollamada[0] / total) * 100;
            double l2 = (d_tipollamada[1] / total) * 100;
            double l3 = (d_tipollamada[2] / total) * 100;


            lbporentrante.Text = l1.ToString("0.#") + "%";
            lbporsaliente.Text = l2.ToString("0.#") + "%";
            lbporsinres.Text = l3.ToString("0.#") + "%";
            lbportotal.Text = "100%";

            lbvisitas.Text = "De " + total + " llamadas, se generan " + visita + " garantias";
            ;
        }
        public void visitas()
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
