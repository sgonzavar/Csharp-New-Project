using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Formacion.xaml
    /// </summary>
    public partial class Formacion : Window
    {

        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        public Formacion()
        {
            InitializeComponent();
            loandinfo();
            lbpun.Visibility = Visibility.Hidden;
            puntaje.Visibility = Visibility.Hidden;
        }
        public void loandinfo()
        {
            lsppal.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'NUEVO INGRESO'", connection);
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
                MessageBox.Show("Sin ingresos nuevos");
            }
            btnllego.Content = "LLEGÓ A FORMACIÓN";
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            loandinfo();
            listas.Visibility = Visibility.Visible;
            lbpun.Visibility = Visibility.Hidden;
            puntaje.Visibility = Visibility.Hidden;
            btnllego.Content = "LLEGÓ A FORMACIÓN";
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
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'EN FORMACIÓN'", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            foreach (DataRow row in dataTable22.Rows)
            {
                lsppal.Items.Add(Convert.ToString(row["NOMBRE"]));
            }
            puntaje.Visibility = Visibility.Visible;
            lbpun.Visibility = Visibility.Visible;
            btnllego.Content = "CERTIFICADO";
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
                connection.Close();
                connection.Open();
                string name = lsppal.SelectedItem.ToString();
                if (btnllego.Content == "LLEGÓ A FORMACIÓN")
                {
                    string update1 = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'EN FORMACIÓN' WHERE NOMBRE = '" + name + "'";
                    SqlCommand command_updateup = new SqlCommand(update1, connection);
                    command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "EN FORMACIÓN");
                    command_updateup.ExecuteReader();

                    connection.Close();
                    connection.Open();

                    string update2 = "UPDATE EMPLEADOS_PPAL SET F_INICIO_FORMACION = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' WHERE NOMBRE = '" + name + "'";
                    SqlCommand command_updateup2 = new SqlCommand(update2, connection);
                    command_updateup2.Parameters.AddWithValue("F_INICIO_FORMACION", DateTime.Now.ToString("dd/MM/yyyy"));
                    command_updateup2.ExecuteReader();
                    loandinfo();
                    MessageBox.Show("Colaborador listo para recibir formación, en caso de ausentismos por favor comunicar al area encargada");
                }
                else
                {
                    if (puntaje.Text != null)
                    {
                        if (Convert.ToInt32(puntaje.Text) > 89)
                        {
                            string update1 = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'CERTIFICADO' WHERE NOMBRE = '" + name + "'";
                            SqlCommand command_updateup = new SqlCommand(update1, connection);
                            command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "CERTIFICADO");
                            command_updateup.ExecuteReader();

                            connection.Close();
                            connection.Open();

                            string update2 = "UPDATE EMPLEADOS_PPAL SET F_CERTIFICACION = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' WHERE NOMBRE = '" + name + "'";
                            SqlCommand command_updateup2 = new SqlCommand(update2, connection);
                            command_updateup2.Parameters.AddWithValue("F_CERTIFICACION", DateTime.Now.ToString("dd/MM/yyyy"));
                            command_updateup2.ExecuteReader();

                            connection.Close();
                            connection.Open();

                            string update3 = "UPDATE EMPLEADOS_PPAL SET PUNTAJE_CERTIFICACION = '" + puntaje.Text.ToString() + "' WHERE NOMBRE = '" + name + "'";
                            SqlCommand command_updateup3 = new SqlCommand(update3, connection);
                            command_updateup3.Parameters.AddWithValue("PUNTAJE_CERTIFICACION", puntaje.Text.ToString());
                            command_updateup3.ExecuteReader();
                            puntaje.Clear();
                            enformacion();
                            MessageBox.Show("Colaborador listo para curso de alturas");
                        }
                        else
                        {
                            MessageBox.Show("El puntaje no es suficiente para la certificaciín, el colaborador no puede pasar a la siguiente etapa");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor indique puntaje");
                    }
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
