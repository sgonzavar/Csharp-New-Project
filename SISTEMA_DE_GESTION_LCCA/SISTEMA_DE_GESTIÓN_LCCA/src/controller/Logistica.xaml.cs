using Microsoft.Toolkit.Uwp.Notifications;
using SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Logistica.xaml
    /// </summary>
    public partial class Logistica : Window
    {
        BancoPruebas bpr = new BancoPruebas();
        SqlConnection connection = new SqlConnection(conexion.cadena);
        int[] id = new int[300];
        int posicionls = 0;

        public Logistica()
        {
            InitializeComponent();
            lbwlcome.Content = "BIENVENIDO " + ClaseCompartida.nombre;
        }
        public void cargapermanente()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    for (int i = 0; i < 2;)
                    {
                        SqlCommand command2 = new SqlCommand("Select * from tbMovimientosTerreno WHERE ESTADO_GRF = 'PENDIENTE'", connection);
                        SqlDataAdapter adapter2 = new SqlDataAdapter();
                        adapter2.SelectCommand = command2;
                        DataTable dataTable2 = new DataTable();
                        adapter2.Fill(dataTable2);
                        foreach (DataRow row in dataTable2.Rows)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                if (lsseriales.Items.Contains(row["SERIAL"].ToString()))
                                {

                                }
                                else
                                {
                                    lsseriales.Items.Add(row["SERIAL"].ToString());
                                    try
                                    {
                                        new ToastContentBuilder()
                                                            .AddArgument("action", "viewConversation")
                                                            .AddArgument("conversationId", 98130)
                                                            .AddText("NUEVO MOVIMIENTO EN TERRENO")
                                                            .AddText("Por favor verifique el listado de movimientos, hay reportes nuevos.")
                                                            .Show();
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            });
                        }
                        Thread.Sleep(60000);
                        for (int j = 0; j < lsseriales.Items.Count; j++)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                connection.Close();
                                connection.Open();
                                lsseriales.SelectedIndex = j;
                                SqlCommand command_buscar3 = new SqlCommand("Select * from tbMovimientosTerreno Where SERIAL = @item", connection);
                                command_buscar3.Parameters.AddWithValue("@item", lsseriales.SelectedItem.ToString());
                                SqlDataReader reader_buscar3 = command_buscar3.ExecuteReader();
                                if (reader_buscar3.Read())
                                {
                                    if (reader_buscar3["ESTADO_GRF"].ToString() == "REALIZADO")
                                    {
                                        lsseriales.Items.RemoveAt(j);
                                    }
                                }
                                reader_buscar3.Close();
                            });
                        }
                        Thread.Sleep(10);

                    }
                }
                catch (Exception ex)
                {
                    string m = ex.Message;
                    cargapermanente();
                }
            }));
            thread.Start();
        }
        public class WpConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return Int32.Parse(value.ToString()) - 200;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private void btnexcelenciamod_Click(object sender, RoutedEventArgs e)
        {
            BodegaTec bd = new BodegaTec();
            bd.Show();
        }
        private void btnmetasmod_Click(object sender, RoutedEventArgs e)
        {
            Consumos con = new Consumos();
            con.Show();
        }
        private void btncall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BodegaCpes cpe = new BodegaCpes();
                cpe.Show();
            }
            catch (Exception)
            {
                BodegaCpes cpe = new BodegaCpes();
                cpe.Show();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.bancopruebas == "TRUE")
            {
                try
                {
                    bpr.Show();
                }
                catch (Exception)
                {
                    BancoPruebas bpr = new BancoPruebas();
                    bpr.Show();
                }
            }
            else
            {
                MessageBox.Show("Sin permisos para ingresar");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConsumosDigi nd = new ConsumosDigi();
            nd.Show();
        }

        private void btnexcelenciamod_Copy_Click(object sender, RoutedEventArgs e)
        {
            IngresoMaterial im = new IngresoMaterial();
            im.Show();
        }

        private void btn_Copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("En el momento estamos actualizando el modulo de movimientos en terreno, ya que presentaba deficiencia para su buen funcionamiento. En el momento OP debe notificar por WhatsApp los movimientos que hagan por medio de LinApp");
            //Movimientosterreno mt = new Movimientosterreno();
            //mt.Show();
        }
    }
}
