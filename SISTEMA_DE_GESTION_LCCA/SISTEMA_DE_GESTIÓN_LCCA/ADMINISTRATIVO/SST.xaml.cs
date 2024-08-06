using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para SST.xaml
    /// </summary>
    public partial class SST : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        public SST()
        {
            InitializeComponent();
            loandinfo();
        }
        public void loandinfo()
        {
            lsppal.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'CERTIFICADO'", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            foreach (DataRow row in dataTable22.Rows)
            {
                lsppal.Items.Add(Convert.ToString(row["NOMBRE"]));
            }
            if (lsppal.Items.Count >= 1)
            {
                lsppal.SelectedIndex = 0;
            }
            else
            {
                btnllego.IsEnabled = false;
                MessageBox.Show("Sin personal para programar certificación de alturas");
            }
            btnllego.Visibility = Visibility.Visible;
            btnllego.Content = "PROGRAMAR ALTURAS";
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            loandinfo();
            listas.Visibility = Visibility.Visible;
            btnllego.Content = "PROGRAMAR ALTURAS";
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void EN_FORMACIÓN_Click(object sender, RoutedEventArgs e)
        {
            enformacion();
        }
        public void enformacion()
        {
            lsppal.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'EN ALTURAS'", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            foreach (DataRow row in dataTable22.Rows)
            {
                lsppal.Items.Add(Convert.ToString(row["NOMBRE"]));
            }

            btnllego.Visibility = Visibility.Hidden;
            if (lsppal.Items.Count >= 1)
            {
                lsppal.SelectedIndex = 0;
            }
        }
        private void lsppal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string name = lsppal.SelectedItem.ToString();
                connection.Close();
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE NOMBRE = '" + name + "'", connection);
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                if (dataReader1.Read())
                {
                    lbcelular.Content = Convert.ToString(dataReader1["TELEFONO1"]);
                    lbdireccion.Content = Convert.ToString(dataReader1["DIRECCION_HAB"]);
                    lbemail.Content = Convert.ToString(dataReader1["E_MAIL"]);
                    lbfecha.Content = Convert.ToString(dataReader1["FECHA_INGRESO"]);
                    lbid.Content = Convert.ToString(dataReader1["IDENTIFICACION"]);
                    lbsuper.Content = Convert.ToString(dataReader1["SUPERVISOR"]);
                    lbpuesto.Content = Convert.ToString(dataReader1["PUESTO"]);
                }
                dataReader1.Close();
            }
            catch (Exception)
            {

            }
        }
        private void btnllego_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (f_inicio.SelectedDate != null || f_final.SelectedDate != null)
                {
                    DateTime fechaUno = Convert.ToDateTime(f_inicio.Text);
                    DateTime fechaDos = Convert.ToDateTime(f_final.Text);
                    TimeSpan difFechas = fechaDos - fechaUno;

                    DateTime fechaUno1 = Convert.ToDateTime(f_inicio.Text);
                    DateTime fechaDos1 = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                    TimeSpan difFechas1 = fechaDos1 - fechaUno1;

                    if (difFechas.Days >= 1 && difFechas.Days < 5 && difFechas1.Days <= 0)
                    {
                        connection.Close();
                        connection.Open();
                        string name = lsppal.SelectedItem.ToString();

                        string update1 = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'EN ALTURAS' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup = new SqlCommand(update1, connection);
                        command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "EN FORMACIÓN");
                        command_updateup.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update2 = "UPDATE EMPLEADOS_PPAL SET F_INICIO_ALTURAS = '" + f_inicio.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup2 = new SqlCommand(update2, connection);
                        command_updateup2.Parameters.AddWithValue("F_INICIO_FORMACION", f_inicio.Text);
                        command_updateup2.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update3 = "UPDATE EMPLEADOS_PPAL SET F_FIN_ALTURAS = '" + f_final.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup3 = new SqlCommand(update3, connection);
                        command_updateup3.Parameters.AddWithValue("F_INICIO_FORMACION", f_inicio.Text);
                        command_updateup3.ExecuteReader();

                        loandinfo();
                        MessageBox.Show("Colaborador listo para recibir realizar curso de alturas, en caso de ausentismos por favor comunicar al area encargada");
                    }
                    else
                    {
                        MessageBox.Show("No es posible programar, revise las fechas");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar fechas");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
