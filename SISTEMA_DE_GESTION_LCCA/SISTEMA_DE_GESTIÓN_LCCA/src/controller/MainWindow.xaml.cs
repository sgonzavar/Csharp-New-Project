using SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                btnLoginOff.Visibility = Visibility.Hidden; //no se ve
                button1.Visibility = Visibility.Hidden;
                btnoperacion.Visibility = Visibility.Hidden;
                btnlogistica.Visibility = Visibility.Hidden;
                frameproyectos.Visibility = Visibility.Hidden;
                frameadmin.Visibility = Visibility.Hidden;
                txtuser.Focus();
                entregadotacion();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        public void entregadotacion()
        {

            try
            {
                SqlConnection connections = new SqlConnection(conexion.cadenallamadas);
                connections.Close();
                connections.Open();
                SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'EN ALTURAS'", connections);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command22;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                foreach (DataRow row in dataTable22.Rows)
                {
                    try
                    {
                        string name = Convert.ToString(row["NOMBRE"]);
                        DateTime dateOne = Convert.ToDateTime(Convert.ToString(row["F_FIN_ALTURAS"]));
                        DateTime dateTwo = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        TimeSpan difFechas = fechaDos - fechaUno;


                        if (difFechas.Days >= 0)
                        {
                            connections.Close();
                            connections.Open();

                            string updateEmploy = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'ENTREGA DOTACIÓN' WHERE NOMBRE = '" + name + "'";
                            SqlCommand command_updateup = new SqlCommand(update1, connections);
                            command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "ENTREGA DOTACIÓN");
                            command_updateup.ExecuteReader();

                            connections.Close();
                            connections.Open();

                            fechaDos = fechaDos.AddDays(1);
                            string update2 = "UPDATE EMPLEADOS_PPAL SET F_ENTREGA_DOTACIÓN = '" + fechaDos + "' WHERE NOMBRE = '" + name + "'";
                            SqlCommand command_updateup2 = new SqlCommand(update2, connections);
                            command_updateup2.Parameters.AddWithValue("F_ENTREGA_DOTACIÓN", fechaDos);
                            command_updateup2.ExecuteReader();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }

        }
        private void btnloginon_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlConnection connection = new SqlConnection(conexion.cadena);
                connection.Close();
                connection.Open();
                SqlCommand command_buscar = new SqlCommand("Select * from tblogins Where NOMBRE = @version", connection);
                command_buscar.Parameters.AddWithValue("@version", "VERSION");
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    string versión = Convert.ToString(reader_buscar["NIVEL"]);
                    if (versión == "18")
                    {
                        reader_buscar.Close();
                        SqlCommand sqlCommand = new SqlCommand("SELECT * from tblogins WHERE USUARIOS= '" + txtuser.Text + "' AND PASS= '" + txtpass.Password + "'", connection);
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        if (dataReader.Read())
                        {
                            //btnloginoff.Content = "LÓGISTICA";
                            ClaseCompartida.nombre = Convert.ToString(dataReader["NOMBRE"]);
                            ClaseCompartida.usuario = Convert.ToString(dataReader["USUARIOS"]);
                            ClaseCompartida.nivel = Convert.ToString(dataReader["NIVEL"]);
                            ClaseCompartida.logistica = Convert.ToString(dataReader["LOGISTICA"]);
                            ClaseCompartida.calidad = Convert.ToString(dataReader["CALIDAD"]);
                            ClaseCompartida.administrativo = Convert.ToString(dataReader["ADMINISTRATIVO"]);
                            ClaseCompartida.formacion = Convert.ToString(dataReader["FORMACION"]);
                            ClaseCompartida.sst = Convert.ToString(dataReader["SST"]);
                            ClaseCompartida.dotacion = Convert.ToString(dataReader["DOTACION"]);
                            ClaseCompartida.operacion = Convert.ToString(dataReader["OPERACION"]);
                            ClaseCompartida.up = Convert.ToString(dataReader["UPEMPLEADOS"]);
                            ClaseCompartida.bancopruebas = Convert.ToString(dataReader["BANCOPRUEBAS"]);
                            ClaseCompartida.proyecto = Convert.ToString(dataReader["CLARO"]);
                            ClaseCompartida.consecutivos = Convert.ToString(dataReader["AMDINPPAL"]);
                            txtuser.Text = Convert.ToString(dataReader["NOMBRE"]);
                            txtuser.IsEnabled = false;
                            button1.IsEnabled = true;
                            txtpass.IsEnabled = false;
                            connection.Close();
                            dataReader.Close();
                            loginfm.Visibility = Visibility.Hidden;
                            btnloginon.Visibility = Visibility.Hidden;
                            frameproyectos.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            txtuser.Clear();
                            txtpass.Clear();
                            MessageBox.Show("Usuario o contraseña incorrectos");
                            connection.Close();
                            dataReader.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("POR FAVOR ACTUALICÉ A LA ÚLTIMA VERSIÓN");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible conectar con la BD" + ex);
            }
        }
        private void btnloginoff_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnoperacion_Click(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.calidad == "TRUE")
            {
                Operación OP = new Operación();
                OP.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este modulo.", "Permisos");
            }
        }
        private void Window_PreviewDragEnter(object sender, DragEventArgs e)
        {

        }
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.administrativo == "TRUE")
            {
                Menuingresos PS = new Menuingresos();
                PS.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sin permisos para ingresar");
            }
        }
        private void btnlogistica_Click(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.logistica == "TRUE")
            {
                //Movimientosterreno movimientosterreno = new Movimientosterreno();
                //movimientosterreno.Show();
                Logistica Ls = new Logistica();
                Ls.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sin permisos para ingresar");
            }
        }
        private void btnproyectoclaro_Click(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.proyecto == "TRUE")
            {
                frameproyectos.Visibility = Visibility.Hidden;
                btnloginoff.Visibility = Visibility.Visible;
                button1.Visibility = Visibility.Visible;
                btnoperacion.Visibility = Visibility.Visible;
                btnlogistica.Visibility = Visibility.Visible;
            }
        }
        private void bnadmin_Click(object sender, RoutedEventArgs e)
        {
            frameproyectos.Visibility = Visibility.Hidden;
            frameadmin.Visibility = Visibility.Visible;
        }

        private void btnconseadmin_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnconseproyec_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnconseproyec_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void btnconseproyec_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void btnconseadmin_Click_1(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.consecutivos == "ADMINISTRATIVO" || ClaseCompartida.consecutivos == "AMBOS")
            {
                Consecutivoasis nw = new Consecutivoasis();
                nw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sin permisos para ingresar");
            }
        }

        private void btnconseproyec_Click_3(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.consecutivos == "ASISTENTE" || ClaseCompartida.consecutivos == "AMBOS")
            {
                Consecutivo nsw = new Consecutivo();
                nsw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sin permisos para ingresar");
            }
        }
        
    }
}
