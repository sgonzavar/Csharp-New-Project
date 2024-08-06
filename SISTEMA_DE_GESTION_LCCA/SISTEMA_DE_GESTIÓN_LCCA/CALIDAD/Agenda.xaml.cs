using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SISTEMA_DE_GESTIÓN_LCCA.CALIDAD
{
    /// <summary>
    /// Lógica de interacción para Agenda.xaml
    /// </summary>
    public partial class Agenda : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        public static string id_visita;
        public Agenda()
        {
            InitializeComponent();
            cvestado.Visibility = Visibility.Collapsed;
            cvfiltro.Visibility = Visibility.Collapsed;
            dgviewfile.Visibility = Visibility.Collapsed;
            lsidvisita.Visibility = Visibility.Collapsed;
            cdvisitafiltro.Visibility = Visibility.Collapsed;
            pickfecha.Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand command22 = new SqlCommand("Select ID_VISITA, ID_LLAMADA, SUPERVISOR, ID_TECNICO, TECNICO, HORA_NOTIFICACION, FECHA_AGENDA, VEZ_VISITA, ESTADO_VISITA from Agendamiento WHERE ESTADO_VISITA = '" + cbxestado.Text + "'", connection);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command22;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                dgviewfile.ItemsSource = dataTable22.DefaultView;
            }
            catch (Exception)
            {
                MessageBox.Show("Algo paso, por favor intente de nuevo.");
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lsidvisita.Items.Clear();
            boton();
        }
        public void boton()
        {
            lsidvisita.Visibility = Visibility.Visible;
            lsidvisita.Items.Clear();
            if (rborden.IsChecked == true)
            {
                connection.Close();
                connection.Open();

                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE NUMERO_ORDEN = '" + txtfiltro.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["VISITA"]);

                    if (visita == "TRUE      ")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }

                }
            }
            else if (rbcuenta.IsChecked == true)
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE CUENTA = '" + txtfiltro.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["VISITA"]);

                    if (visita == "TRUE      ")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }

                }
            }
            else if (rbidllamada.IsChecked == true)
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE ID_LLAMADA = '" + txtfiltro.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["VISITA"]);

                    if (visita == "TRUE      ")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }

                }
            }
            else if (rbidvisita.IsChecked == true)
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE ID_VISITA = '" + txtfiltro.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["VISITA"]);

                    if (visita == "TRUE      ")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }

                }
            }
            else if (rbfecha.IsChecked == true)
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE FECHA_EVENTO = '" + pickfecha.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["VISITA"]);

                    if (visita == "TRUE      ")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }
                    pickfecha.Visibility = Visibility.Visible;
                }
            }
            else
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Agendamiento WHERE FECHA_AGENDA = '" + pickfecha.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string visita = Convert.ToString(row["ESTADO_VISITA"]);

                    if (visita == "CERRADO")
                    {
                        lsidvisita.Items.Add(Convert.ToString(row["ID_VISITA"]));
                    }
                    pickfecha.Visibility = Visibility.Visible;
                }
            }
        }
        private void lsidvisita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                string id_visita = lsidvisita.SelectedValue.ToString();

                SqlCommand command_buscar = new SqlCommand("Select * from Llamadas WHERE ID_VISITA = @id", connection); //CONSULTA SQL            
                command_buscar.Parameters.AddWithValue("@id", id_visita);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    if (Convert.ToString(reader_buscar["VISITA"]) == "TRUE      ")
                    {
                        lbnamecliente.Content = Convert.ToString(reader_buscar["NOMBRE_CLIENTE"]);
                        lbdireccion.Content = Convert.ToString(reader_buscar["DIRECCIÓN"]);
                        lbtelcliente.Content = Convert.ToString(reader_buscar["NUM_CONTACTO"]);
                        txtnotasllamada.Text = Convert.ToString(reader_buscar["NOTA"]);
                        lbidorden.Content = Convert.ToString(reader_buscar["NUMERO_ORDEN"]);
                        Clipboard.SetText(Convert.ToString(reader_buscar["NUMERO_ORDEN"]));
                        reader_buscar.Close();

                        SqlCommand command_buscar2 = new SqlCommand("Select * from Agendamiento WHERE ID_VISITA = @id", connection); //CONSULTA SQL            
                        command_buscar2.Parameters.AddWithValue("@id", id_visita);
                        SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                        if (reader_buscar2.Read())
                        {
                            lbidvisita.Content = Convert.ToString(reader_buscar2["ID_VISITA"]);
                            lbestado.Content = Convert.ToString(reader_buscar2["ESTADO_VISITA"]);
                            lbsupervisor.Content = Convert.ToString(reader_buscar2["SUPERVISOR"]);
                            lbfecha.Content = Convert.ToString(reader_buscar2["FECHA_AGENDA"]);
                            lbfranja.Content = Convert.ToString(reader_buscar2["FRANJA_VISITA"]);
                            lbtecnico.Content = Convert.ToString(reader_buscar2["TECNICO"]);

                            if (lbestado.Content.ToString() == "AGENDADO")
                            {
                                txtnotascierre.Text = "AUN EN AGENDAMIENTO";
                                reader_buscar2.Close();
                            }
                            else
                            {
                                txtnotascierre.Text = Convert.ToString(reader_buscar2["NOTAS_CIERRE"]);
                                reader_buscar2.Close();
                                SqlCommand command_buscar5 = new SqlCommand("Select * from Agendamiento Where ID_VISITA = @item2", connection); //CONSULTA SQL            
                                command_buscar5.Parameters.AddWithValue("@item2", id_visita);
                                SqlDataReader reader_buscar5 = command_buscar5.ExecuteReader();
                                if (reader_buscar5.Read())
                                {
                                    try
                                    {
                                        byte[] datos = new byte[0];
                                        datos = (byte[])reader_buscar5["FOTO_ARREGLO"];
                                        BitmapImage bi3 = new BitmapImage();
                                        bi3.BeginInit();
                                        MemoryStream ms = new MemoryStream(datos);

                                        bi3.StreamSource = ms;
                                        bi3.EndInit();

                                        imgevento.Stretch = Stretch.Uniform;
                                        imgevento.Source = bi3;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(Convert.ToString(ex));
                                    }
                                    try
                                    {
                                        byte[] datos = new byte[0];
                                        datos = (byte[])reader_buscar5["FOTO_PLACA"];
                                        BitmapImage bi3 = new BitmapImage();
                                        bi3.BeginInit();
                                        MemoryStream ms = new MemoryStream(datos);

                                        bi3.StreamSource = ms;
                                        bi3.EndInit();

                                        imgplaca.Stretch = Stretch.Uniform;
                                        imgplaca.Source = bi3;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(Convert.ToString(ex));
                                    }
                                }

                            }
                        }

                        cdvisitafiltro.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception)
            {
                lbidorden.Content = "";
                cdvisitafiltro.Visibility = Visibility.Hidden;
                lsidvisita.Visibility = Visibility.Hidden;
                boton();
            }

        }
        private void rbfecha_Checked(object sender, RoutedEventArgs e)
        {
            pickfecha.Visibility = Visibility.Visible;
            pickfecha.Text = "";
        }
        private void rborden_Checked(object sender, RoutedEventArgs e)
        {
            pickfecha.Visibility = Visibility.Hidden;
        }
        private void rbcuenta_Checked(object sender, RoutedEventArgs e)
        {
            pickfecha.Visibility = Visibility.Hidden;
        }
        private void rbidllamada_Checked(object sender, RoutedEventArgs e)
        {
            pickfecha.Visibility = Visibility.Hidden;
        }
        private void rbidvisita_Checked(object sender, RoutedEventArgs e)
        {
            pickfecha.Visibility = Visibility.Hidden;
        }
        private void rbsearchagenda_Checked(object sender, RoutedEventArgs e)
        {

            lsidvisita.Visibility = Visibility.Hidden;
            cdvisitafiltro.Visibility = Visibility.Hidden;
            cvestado.Visibility = Visibility.Visible;
            cvfiltro.Visibility = Visibility.Hidden;
            dgviewfile.Visibility = Visibility.Visible;
            pickfecha.Visibility = Visibility.Hidden;
        }
        private void rbsearchfiltro_Checked(object sender, RoutedEventArgs e)
        {
            lsidvisita.Visibility = Visibility.Hidden;
            cdvisitafiltro.Visibility = Visibility.Hidden;
            cvestado.Visibility = Visibility.Hidden;
            cvfiltro.Visibility = Visibility.Visible;
            dgviewfile.Visibility = Visibility.Hidden;

        }
        private void btnhome_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnhome_Copy1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnsalir1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnhome_Copy1_Click_1(object sender, RoutedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgviewfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgviewfile_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Convert.ToString((dgviewfile.CurrentItem as DataRowView).Row.ItemArray[8].ToString()) == "AGENDADO")
            {
                id_visita = Convert.ToString((dgviewfile.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                Reagendamiento rd = new Reagendamiento();
                rd.ShowDialog();


                try
                {
                    connection.Close();
                    connection.Open();
                    SqlCommand command22 = new SqlCommand("Select ID_VISITA, ID_LLAMADA, SUPERVISOR, ID_TECNICO, TECNICO, HORA_NOTIFICACION, FECHA_AGENDA, VEZ_VISITA, ESTADO_VISITA from Agendamiento WHERE ESTADO_VISITA = '" + cbxestado.Text + "'", connection);
                    SqlDataAdapter adapter22 = new SqlDataAdapter();
                    adapter22.SelectCommand = command22;
                    DataTable dataTable22 = new DataTable();
                    adapter22.Fill(dataTable22);
                    dgviewfile.ItemsSource = dataTable22.DefaultView;
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo paso, por favor intente de nuevo.");
                }
            }
            else
            {
                MessageBox.Show("La visita no se puede reagendar, no esta en estado 'AGENDADO'");
            }
        }
    }
}

