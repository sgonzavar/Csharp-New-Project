using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Operacion.xaml
    /// </summary>
    public partial class Operacion : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        public Operacion()
        {
            InitializeComponent();
            loandinfo();
        }
        public void loandinfo()
        {
            lsppal.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'CON DOTACIÓN'", connection);
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
                MessageBox.Show("Sin personal para programar inicio de operación");
                this.Close();
            }
            btnllego.Visibility = Visibility.Visible;
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            loandinfo();
            listas.Visibility = Visibility.Visible;
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

                if (cbxcarpeta.SelectedItem != null && cbxsuper.SelectedItem != null && cbxtecno.SelectedItem != null && txtplaca.Text != null && txtplaca.Text != "")
                {
                    if (txtplaca.Text.Length == 6)
                    {
                        connection.Close();
                        connection.Open();
                        string name = lsppal.SelectedItem.ToString();

                        string update1 = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'EN OPERACIÓN' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup = new SqlCommand(update1, connection);
                        command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "EN OPERACIÓN");
                        command_updateup.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update2 = "UPDATE EMPLEADOS_PPAL SET TECNOLOGIA = '" + cbxtecno.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup2 = new SqlCommand(update2, connection);
                        command_updateup2.Parameters.AddWithValue("F_INICIO_FORMACION", cbxtecno.Text);
                        command_updateup2.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update3 = "UPDATE EMPLEADOS_PPAL SET CARPETA = '" + cbxcarpeta.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup3 = new SqlCommand(update3, connection);
                        command_updateup3.Parameters.AddWithValue("F_INICIO_FORMACION", cbxcarpeta.Text);
                        command_updateup3.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update4 = "UPDATE EMPLEADOS_PPAL SET PLACA_MOTO = '" + txtplaca.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup4 = new SqlCommand(update4, connection);
                        command_updateup4.Parameters.AddWithValue("PLACA_MOTO", txtplaca.Text);
                        command_updateup4.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update5 = "UPDATE EMPLEADOS_PPAL SET SUPERVISOR = '" + cbxsuper.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup5 = new SqlCommand(update5, connection);
                        command_updateup5.Parameters.AddWithValue("SUPERVISOR", cbxsuper.Text);
                        command_updateup5.ExecuteReader();

                        connection.Close();
                        connection.Open();

                        string update6 = "UPDATE EMPLEADOS_PPAL SET F_INICIO_PERACION = '" + pfinicio.Text + "' WHERE NOMBRE = '" + name + "'";
                        SqlCommand command_updateup6 = new SqlCommand(update6, connection);
                        command_updateup6.Parameters.AddWithValue("F_INICIO_PERACION", pfinicio.Text);
                        command_updateup6.ExecuteReader();

                        txtplaca.Clear();
                        cbxcarpeta.SelectedItem = null;
                        cbxsuper.SelectedItem = null;
                        cbxtecno.Text = null;
                        pfinicio.SelectedDate = null;
                        MessageBox.Show("Colaborador listo para entrar a operación, en caso de ausentismos por favor comunicar al area encargada");
                        loandinfo();

                    }
                    else
                    {
                        MessageBox.Show("Placa no valida");
                    }
                }
                else
                {
                    MessageBox.Show("Ningún campo puede estar sin información");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

