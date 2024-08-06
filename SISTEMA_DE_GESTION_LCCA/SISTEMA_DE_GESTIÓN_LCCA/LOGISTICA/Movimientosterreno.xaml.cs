//using Microsoft.Toolkit.Uwp.Notifications;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Threading;
//using System.Windows;
//using System.Windows.Controls;

//namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
//{
//    /// <summary>
//    /// Lógica de interacción para Movimientosterreno.xaml
//    /// </summary>
//    public partial class Movimientosterreno : Window
//    {
//        SqlConnection connection = new SqlConnection(conexion.cadena);
//        int[] id = new int[300];
//        int posicionls = 0;
//        public Movimientosterreno()
//        {
//            InitializeComponent();
//            connection.Close();
//            connection.Open();
//            cargapermanente();
//            actualización();
//        }
//        public void cargapermanente()
//        {
//            Thread thread = new Thread(new ThreadStart(() =>
//            {
//                try
//                {
//                    for (int i = 0; i < 2;)
//                    {
//                        SqlCommand command2 = new SqlCommand("Select * from tbMovimientosTerreno WHERE ESTADO_GRF = 'PENDIENTE'", connection);
//                        SqlDataAdapter adapter2 = new SqlDataAdapter();
//                        adapter2.SelectCommand = command2;
//                        DataTable dataTable2 = new DataTable();
//                        adapter2.Fill(dataTable2);
//                        foreach (DataRow row in dataTable2.Rows)
//                        {
//                            Application.Current.Dispatcher.Invoke((Action)delegate
//                            {
//                                if (lsseriales.Items.Contains(row["SERIAL"].ToString()))
//                                {

//                                }
//                                else
//                                {
//                                    lsseriales.Items.Add(row["SERIAL"].ToString());                                    
//                                }
//                            });
//                        }
//                        Thread.Sleep(70000);
//                        for (int j = 0; j < lsseriales.Items.Count; j++)
//                        {
//                            Application.Current.Dispatcher.Invoke((Action)delegate
//                         {
//                                connection.Close();
//                                connection.Open();
//                                lsseriales.SelectedIndex = j;
//                                SqlCommand command_buscar3 = new SqlCommand("Select * from tbMovimientosTerreno Where SERIAL = @item", connection);
//                                command_buscar3.Parameters.AddWithValue("@item", lsseriales.SelectedItem.ToString());
//                                SqlDataReader reader_buscar3 = command_buscar3.ExecuteReader();
//                                if (reader_buscar3.Read())
//                                {
//                                    if (reader_buscar3["ESTADO_GRF"].ToString() == "REALIZADO")
//                                    {
//                                        lsseriales.Items.RemoveAt(j);                                   
//                                    }
//                                }
//                                reader_buscar3.Close();
//                                });
//                        }
//                        Thread.Sleep(10);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    string m = ex.Message;
//                    cargapermanente();
//                }
//            }));
//            thread.Start();
//        }
//        private void btnhome_Click(object sender, RoutedEventArgs e)
//        {
//            this.Close();
//        }
//        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            try
//            {
//                Application.Current.Dispatcher.Invoke((Action)delegate
//                {                    
//                    SqlCommand command_buscar = new SqlCommand("Select * from tbMovimientosTerreno Where serial = '" + lsseriales.SelectedItem.ToString() + "'  AND estado_grf = 'PENDIENTE'", connection); //CONSULTA SQL
//                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
//                    if (reader_buscar.Read())
//                    {
//                        txtblock.Text = "El equipo lo tenia: " + reader_buscar["NOMBRE_LOTENIA"].ToString() + " " + reader_buscar["ID_LOTENIA"].ToString() + "\nAhora lo tiene " + reader_buscar["NOMBRE_LOTIENE"].ToString() + " " + reader_buscar["ID_LOTIENE"].ToString() +
//                            "\nEl equipo es: " + reader_buscar["DESCRIPCION"].ToString() +  " Serial: "+reader_buscar["SERIAL"].ToString();
//                    }
//                    reader_buscar.Close();
//                    posicionls = lsseriales.SelectedIndex;
//                });
//            }
//            catch (Exception)
//            {

//            }
//        }
//        private void btnuploanddata_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {                
//                string query1 = "UPDATE tbMovimientosTerreno SET estado_grf = 'REALIZADO' Where serial = '" + lsseriales.SelectedItem.ToString() + "'";
//                SqlCommand command_update1 = new SqlCommand(query1, connection);
//                command_update1.ExecuteNonQuery();
//                Application.Current.Dispatcher.Invoke((Action)delegate
//                {
//                    lsseriales.Items.RemoveAt(lsseriales.SelectedIndex);
//                    txtblock.Text = "";
//                });
//            }
//            catch (Exception ex)
//            {

//            }
//        }
//        public void actualización()
//        {
//        }
//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            txtblock.Clear();
//            this.Close();
//        }
//    }
//}
