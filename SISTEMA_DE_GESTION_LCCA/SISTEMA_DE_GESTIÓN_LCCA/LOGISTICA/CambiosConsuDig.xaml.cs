using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para CambiosConsuDig.xaml
    /// </summary>
    public partial class CambiosConsuDig : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        string[] items = new string[300];
        string cedula = ConsumosDigi.cedula;
        string nombre = ConsumosDigi.nombre;
        string id = ConsumosDigi.id_con;
        public CambiosConsuDig()
        {
            InitializeComponent();
            txtcuenta.IsEnabled = false;
            txtot.IsEnabled = false;
            cbxitem.IsEnabled = false;
            txtitemdes.IsEnabled = false;
            txtnodo.IsEnabled = false;
            txttipoot.IsEnabled = false;
            btnok.IsEnabled = false;
            txtcantidad.IsEnabled = false;
            pkfecchaact.IsEnabled = false;
            cbxitem1.IsEnabled = false;
            txtitemdes1.IsEnabled = false;
            btnbodetec.IsEnabled = false;
            MATERIALES.Visibility = Visibility.Hidden;
            CPES.Visibility = Visibility.Hidden;
            inicio();
        }
        private void inicio()
        {
            if (ConsumosDigi.elemento == "MATERIAL")
            {
                MATERIALES.Visibility = Visibility.Visible;
            }
            else
            {
                CPES.Visibility = Visibility.Visible;
                txtcantidad.Text = "1";
                cbmate.Visibility = Visibility.Hidden;
            }
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from tbbodega_ppal", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                int cont = 0;
                foreach (DataRow row in dataTable2.Rows)
                {
                    cbxitem.Items.Add(Convert.ToString(row["ITEM"]));
                    items[cont] = Convert.ToString(row["MATERIAL"]);
                    cont++;
                }
                SqlCommand command_buscar = new SqlCommand("Select * from tbregistroconsumosterreno Where ID = @id", connection); //CONSULTA SQL
                command_buscar.Parameters.AddWithValue("@id", id);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    DateTime n = Convert.ToDateTime(reader_buscar["FECHA_ACTIVIDAD"]);
                    txtcuenta.Text = Convert.ToString(reader_buscar["CUENTA"]);
                    txtot.Text = Convert.ToString(reader_buscar["OT"]);
                    cbxitem.Text = Convert.ToString(reader_buscar["ITEM"]);
                    txtitemdes.Text = Convert.ToString(reader_buscar["DESCRIPCIÓN"]);
                    cbxitem1.Text = Convert.ToString(reader_buscar["ITEM"]);
                    txtitemdes1.Text = Convert.ToString(reader_buscar["DESCRIPCIÓN"]);
                    txtcantidad.Text = Convert.ToString(reader_buscar["CANTIDAD"]);
                    txtnodo.Text = Convert.ToString(reader_buscar["NODO"]);
                    txttipoot.Text = Convert.ToString(reader_buscar["TIPO_OT"]);
                    pkfecchaact.Text = n.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                if (rdagregar.IsChecked == true)
                {
                    string query = "INSERT tbregistroconsumosterreno (CÉDULA, NOMBRE, CUENTA, OT, NODO, TIPO_OT, ITEM, DESCRIPCIÓN, CANTIDAD, FECHA_ACTIVIDAD, ELEMENTO, ESTADO_REGISTRO, TOMADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad,  @elemento, @estado, @tomado)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cédula", cedula);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@cuenta_consumo", txtcuenta.Text);
                    command.Parameters.AddWithValue("@ot", txtot.Text);
                    command.Parameters.AddWithValue("@nodo", txtnodo.Text);
                    command.Parameters.AddWithValue("@tipo_trabajo", txttipoot.Text.Trim());
                    command.Parameters.AddWithValue("@item", cbxitem.Text);
                    command.Parameters.AddWithValue("@material", txtitemdes.Text);
                    command.Parameters.AddWithValue("@cantidad", txtcantidad.Text);
                    command.Parameters.AddWithValue("@fecha_actividad", pkfecchaact.Text);
                    command.Parameters.AddWithValue("@elemento", ConsumosDigi.elemento);
                    command.Parameters.AddWithValue("@estado", "POR VALIDAR");
                    command.Parameters.AddWithValue("@tomado", ClaseCompartida.nombre);
                    command.ExecuteNonQuery();
                }
                else if (rdmodificar.IsChecked == true)
                {
                    if (Convert.ToInt32(txtcantidad.Text) >= 1)
                    {

                        string queryup = "UPDATE tbregistroconsumosterreno SET CUENTA = '" + txtcuenta.Text + "', OT = '" + txtot.Text + "', NODO = '" + txtnodo.Text + "', FECHA_ACTIVIDAD = '" + pkfecchaact.Text + "' Where OT ='" + ConsumosDigi.ot + "'";
                        SqlCommand command_updateup = new SqlCommand(queryup, connection);
                        command_updateup.Parameters.AddWithValue("CUENTA", txtcuenta.Text);
                        command_updateup.Parameters.AddWithValue("OT", txtot.Text);
                        command_updateup.Parameters.AddWithValue("NODO", txtnodo.Text);
                        command_updateup.Parameters.AddWithValue("FECHA_ACTIVIDAD", pkfecchaact.Text);
                        command_updateup.ExecuteNonQuery();

                        string queryup1 = "UPDATE tbregistroconsumosterreno SET ITEM = '" + cbxitem.Text + "', DESCRIPCIÓN = '" + txtitemdes.Text + "', CANTIDAD = '" + txtcantidad.Text + "' Where ID ='" + ConsumosDigi.id_con + "'";
                        SqlCommand command_updateup1 = new SqlCommand(queryup1, connection);
                        command_updateup1.Parameters.AddWithValue("ITEM", txtcuenta.Text);
                        command_updateup1.Parameters.AddWithValue("DESCRIPCIÓN", txtitemdes.Text);
                        command_updateup1.Parameters.AddWithValue("CANTIDAD", txtcantidad.Text);
                        command_updateup1.ExecuteNonQuery();

                        if (rdonlyelemt.IsChecked == true)
                        {
                            string queryup2 = "UPDATE tbregistroconsumosterreno SET TIPO_OT = '" + txttipoot.Text + "' Where ID ='" + ConsumosDigi.id_con + "'";
                            SqlCommand command_updateup2 = new SqlCommand(queryup2, connection);
                            command_updateup2.Parameters.AddWithValue("TIPO_OT", txttipoot.Text);
                            command_updateup2.ExecuteNonQuery();
                        }
                        else
                        {
                            string queryup2 = "UPDATE tbregistroconsumosterreno SET TIPO_OT = '" + txttipoot.Text + "' Where OT ='" + ConsumosDigi.ot + "'";
                            SqlCommand command_updateup2 = new SqlCommand(queryup2, connection);
                            command_updateup2.Parameters.AddWithValue("TIPO_OT", txttipoot.Text);
                            command_updateup2.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string dl = "DELETE FROM tbregistroconsumosterreno WHERE ID =" + id;
                        SqlCommand command = new SqlCommand(dl, connection);
                        command.ExecuteNonQuery();
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cbcuenta.IsChecked == true)
            {
                txtcuenta.IsEnabled = true;
            }
            else
            {
                txtcuenta.IsEnabled = false;
            }
        }
        private void cbot_Checked(object sender, RoutedEventArgs e)
        {
            if (cbot.IsChecked == true)
            {
                txtot.IsEnabled = true;
            }
            else
            {
                txtot.IsEnabled = false;
            }
        }
        private void cbitem_Checked(object sender, RoutedEventArgs e)
        {
            if (cbitem.IsChecked == true || cbitem1.IsChecked == true)
            {
                cbxitem.IsEnabled = true;
                btnbodetec.IsEnabled = true;
            }
            else
            {
                cbxitem.IsEnabled = false;
                cbxitem1.IsEnabled = false;
                btnbodetec.IsEnabled = false;
            }
        }
        private void cbmate_Checked(object sender, RoutedEventArgs e)
        {
            if (cbmate.IsChecked == true)
            {
                txtcantidad.IsEnabled = true;
            }
            else
            {
                txtcantidad.IsEnabled = false;
            }
        }
        private void cbnodo_Checked(object sender, RoutedEventArgs e)
        {
            if (cbnodo.IsChecked == true)
            {
                txtnodo.IsEnabled = true;
            }
            else
            {
                txtnodo.IsEnabled = false;
            }
        }
        private void cbtipo_Checked(object sender, RoutedEventArgs e)
        {
            if (cbtipo.IsChecked == true)
            {
                txttipoot.IsEnabled = true;
            }
            else
            {
                txttipoot.IsEnabled = false;
            }
        }
        private void rdagregar_Checked(object sender, RoutedEventArgs e)
        {
            btnok.IsEnabled = true;
        }
        private void rdmodificar_Checked(object sender, RoutedEventArgs e)
        {
            btnok.IsEnabled = true;
        }
        private void cbxitem_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            txtitemdes.Text = items[cbxitem.SelectedIndex];
        }
        private void cbfecha_Checked(object sender, RoutedEventArgs e)
        {
            if (cbfecha.IsChecked == true)
            {
                pkfecchaact.IsEnabled = true;
            }
            else
            {
                pkfecchaact.IsEnabled = false;
            }
        }
        private void btnbodetec_Click(object sender, RoutedEventArgs e)
        {
            flagt.flagtec = 1;
            flagt.ced = cedula;
            BodegaTec bt = new BodegaTec();
            bt.ShowDialog();

            if (flagt.flagtec == 0)
            {
                cbxitem.Text = flagt.serial;
                txtitemdes.Text = flagt.descripcion;
                cbxitem1.Text = flagt.serial;
                txtitemdes1.Text = flagt.descripcion;
            }
        }
    }
}
