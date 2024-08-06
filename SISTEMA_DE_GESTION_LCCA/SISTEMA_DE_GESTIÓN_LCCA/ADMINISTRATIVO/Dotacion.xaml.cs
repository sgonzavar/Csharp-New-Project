using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Dotacion.xaml
    /// </summary>
    public partial class Dotacion : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);

        public Dotacion()
        {
            InitializeComponent();
            loandinfo();
        }
        public void loandinfo()
        {
            lsppal.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = 'ENTREGA DOTACIÓN'", connection);
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
                MessageBox.Show("Sin personal para entrega de dotación");
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
                    lbcamisa.Content = Convert.ToString(dataReader1["U_TALLA_CAMISA"]);
                    lbpantalon.Content = Convert.ToString(dataReader1["U_TALLA_PANTALON"]);
                    lbzapatos.Content = Convert.ToString(dataReader1["U_TALLA_ZAPATOS"]);
                    lbchaleco.Content = Convert.ToString(dataReader1["U_TALLA_CHALECO"]);
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

                string update1 = "UPDATE EMPLEADOS_PPAL SET ESTADO_PROYECTO = 'CON DOTACIÓN' WHERE NOMBRE = '" + name + "'";
                SqlCommand command_updateup = new SqlCommand(update1, connection);
                command_updateup.Parameters.AddWithValue("ESTADO_PROYECTO", "CON DOTACIÓN");
                command_updateup.ExecuteReader();

                connection.Close();
                connection.Open();

                string update2 = "UPDATE EMPLEADOS_PPAL SET F_ENTREGA_DOTACIÓN = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' WHERE NOMBRE = '" + name + "'";
                SqlCommand command_updateup2 = new SqlCommand(update2, connection);
                command_updateup2.Parameters.AddWithValue("F_INICIO_FORMACION", DateTime.Now.ToString("dd/MM/yyyy"));
                command_updateup2.ExecuteReader();
                MessageBox.Show("Colaborador listo para recibir ingresar a operación, por favor notificar al coordinador de proyecto");
                loandinfo();

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
