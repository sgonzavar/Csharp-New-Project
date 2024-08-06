using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows;
using Application = System.Windows.Application;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para BancoPruebas.xaml
    /// </summary>
    public partial class BancoPruebas : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        List<lisdesc> lsdes = new List<lisdesc>();
        DataTable dt = new DataTable();
        string estado = "";
        public BancoPruebas()
        {
            InitializeComponent();
            series();
            listado(lsdes);
            charts.Visibility = Visibility.Visible;
            progress.Visibility = Visibility.Hidden;
        }
        public void series()
        {
            string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
            int pruebas = 0;
            int noengancha = 0;
            int noenciende = 0;
            int hdmisulf = 0;
            int otro = 0;
            int valorado = 0;
            int novalorado = 0;

            SqlCommand command2 = new SqlCommand("Select * from tbbodega_cpesppal WHERE FECHA_REVISIÓN = '" + dateTime + "'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            foreach (DataRow row in dataTable2.Rows)
            {
                if (row["NOTAS"].ToString() == "PRUEBAS OK")
                {
                    pruebas++;
                }
                else if (row["NOTAS"].ToString() == "NO ENGANCHA")
                {
                    noengancha++;
                }
                else if (row["NOTAS"].ToString() == "HDMI SULFATADO")
                {
                    hdmisulf++;
                }
                else if (row["NOTAS"].ToString() == "NO ENCIENDE")
                {
                    noenciende++;
                }
                else if (row["NOTAS"].ToString() == "OTRO")
                {
                    otro++;
                }
                if (row["LOTE"].ToString() == "VALORADO")
                {
                    valorado++;
                }
                if (row["LOTE"].ToString() == "NO VALORADO")
                {
                    novalorado++;
                }
            }

            Chart_Copy.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "VALORADO",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(valorado) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "NO VALORADO",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(novalorado) },
                    DataLabels = true
                },

            };


            Chart.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Pruebas OK",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pruebas) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "No engancha",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(noengancha) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "No enciende",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(noenciende) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "HDMI sulfatado",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(hdmisulf) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Otro",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(otro) },
                    DataLabels = true
                }
            };

            DataContext = this;
        }
        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {

            string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
            connection.Close();
            connection.Open();
            SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
            command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(txtserial.Text));
            SqlDataReader reader_buscar12 = command_buscar12.ExecuteReader();
            if (reader_buscar12.Read())
            {
                if (reader_buscar12["ESTADO"].ToString() == "ALMACENADO")
                {
                    reader_buscar12.Close();
                    if (estado == "")
                    {
                        MessageBox.Show("Por favor ingrese resultado de las pruebas", "Null");
                    }
                    else if (estado == "PRUEBAS OK")
                    {

                        String query2 = "UPDATE tbbodega_cpesppal SET ESTADO = 'DISPONIBLE' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update2 = new SqlCommand(query2, connection);
                        command_update2.Parameters.AddWithValue("ITEM", "DISPONIBLE");
                        command_update2.ExecuteNonQuery();

                        String query = "UPDATE tbbodega_cpesppal SET NOTAS = '" + estado + "' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update = new SqlCommand(query, connection);
                        command_update.Parameters.AddWithValue("ITEM", estado);
                        command_update.ExecuteNonQuery();

                        String query1 = "UPDATE tbbodega_cpesppal SET FECHA_REVISIÓN = '" + dateTime + "' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update1 = new SqlCommand(query1, connection);
                        command_update1.Parameters.AddWithValue("ITEM", estado);
                        command_update1.ExecuteNonQuery();
                        txtserial.Clear();
                        MessageBox.Show("Registro exitoso");
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        String query2 = "UPDATE tbbodega_cpesppal SET ESTADO = 'GARANTÍA' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update2 = new SqlCommand(query2, connection);
                        command_update2.Parameters.AddWithValue("ITEM", "DISPONIBLE");
                        command_update2.ExecuteNonQuery();

                        String query = "UPDATE tbbodega_cpesppal SET NOTAS = '" + estado + "' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update = new SqlCommand(query, connection);
                        command_update.Parameters.AddWithValue("ITEM", estado);
                        command_update.ExecuteNonQuery();

                        String query1 = "UPDATE tbbodega_cpesppal SET FECHA_REVISIÓN = '" + dateTime + "' Where SERIAL ='" + txtserial.Text + "'";
                        SqlCommand command_update1 = new SqlCommand(query1, connection);
                        command_update1.Parameters.AddWithValue("ITEM", estado);
                        command_update1.ExecuteNonQuery();
                        txtserial.Clear();
                        MessageBox.Show("Registro exitoso");
                    }
                }
                else
                {
                    MessageBox.Show("El CPE no esta en estado disponible para revisión", "FAILED");
                    reader_buscar12.Close();
                    txtserial.Clear();
                }
                series();
                rdhdmisulfa.IsChecked = false;
                rdnoenciende.IsChecked = false;
                rdnoengancha.IsChecked = false;
                rdotro.IsChecked = false;
                rdpruebasok.IsChecked = false;

            }
            else
            {
                rdhdmisulfa.IsChecked = false;
                rdnoenciende.IsChecked = false;
                rdnoengancha.IsChecked = false;
                rdotro.IsChecked = false;
                rdpruebasok.IsChecked = false;
                reader_buscar12.Close();
                MessageBox.Show("El m CPE no existe en la bodega", "FAILED");
                txtserial.Clear();
            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Logistica logistica = new Logistica();
            logistica.Show();
            this.Close();
        }
        private void btnexportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                connection.Close();
                connection.Open();
                SqlCommand command1 = new SqlCommand("Select * from tbbodega_cpesppal", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command1;
                DataTable dataTable = new DataTable();
                adapter.Fill(dt);

                Thread thread = new Thread(new ThreadStart(() =>
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        charts.Visibility = Visibility.Hidden;
                        progress.Visibility = Visibility.Visible;
                        btnbuscar.IsEnabled = false;
                    });



                    if (dt == null || dt.Columns.Count == 0)
                        throw new Exception("ExportToExcel: Null or empty input table!\n");
                    // load Excel, and create a new workbook
                    var excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Workbooks.Add();

                    // single worksheet
                    Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelApp.ActiveSheet;
                    // column headings

                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        workSheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                    }
                    // rows
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        // to do: format datetime values before printing
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            workSheet.Cells[i + 2, j + 1] = "'" + dt.Rows[i][j];
                        }
                    }
                    MessageBox.Show("Documento exportardo, recuerde guardarlo");
                    excelApp.Visible = true;
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        charts.Visibility = Visibility.Visible;
                        progress.Visibility = Visibility.Hidden;
                        btnbuscar.IsEnabled = true;
                        btnexportar.IsEnabled = true;
                    });
                }));
                thread.Start();
            }
            catch (Exception)
            {

            }
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Chart_OnDataClick(object sender, ChartPoint chartPoint)
        {

        }
        private void PieChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void listado(List<lisdesc> lsdes)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                SqlCommand command2 = new SqlCommand("Select DESCRIPCIÓN, ESTADO, NOTAS from tbbodega_cpesppal", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    var resultado = (from i in lsdes
                                     where i.descripcion == Convert.ToString(row["DESCRIPCIÓN"])
                                     select i).ToList();
                    if (resultado.Count == 0)
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            lisdesc ls = new lisdesc();
                            ls.descripcion = Convert.ToString(row["DESCRIPCIÓN"]);
                            ls.cantidad += 1;
                            lsdes.Add(ls);
                            lsdescripcion.Items.Add(Convert.ToString(row["DESCRIPCIÓN"]));
                        });
                    }
                }
            }));
            thread.Start();
        }
        private void lsdescripcion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int pruebas = 0;
            int noengancha = 0;
            int noenciende = 0;
            int hdmisulf = 0;
            int otro = 0;
            int valorado = 0;
            int novalorado = 0;
            SqlCommand command2 = new SqlCommand("Select * from tbbodega_cpesppal WHERE DESCRIPCIÓN = '" + lsdescripcion.SelectedItem.ToString() + "'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            foreach (DataRow row in dataTable2.Rows)
            {
                if (row["NOTAS"].ToString() == "PRUEBAS OK")
                {
                    pruebas++;
                }
                else if (row["NOTAS"].ToString() == "NO ENGANCHA")
                {
                    noengancha++;
                }
                else if (row["NOTAS"].ToString() == "HDMI SULFATADO")
                {
                    hdmisulf++;
                }
                else if (row["NOTAS"].ToString() == "NO ENCIENDE")
                {
                    noenciende++;
                }
                else if (row["NOTAS"].ToString() == "OTRO")
                {
                    otro++;
                }
                if (row["LOTE"].ToString() == "VALORADO")
                {
                    valorado++;
                }
                if (row["LOTE"].ToString() == "NO VALORADO")
                {
                    novalorado++;
                }
            }
            chartmodelos.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Pruebas OK",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pruebas) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "No engancha",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(noengancha) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "No enciende",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(noenciende) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "HDMI sulfatado",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(hdmisulf) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Otro",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(otro) },
                    DataLabels = true
                }
            };
        }
        private void rdpruebasok_Checked(object sender, RoutedEventArgs e)
        {
            estado = "PRUEBAS OK";
        }
        private void rdnoengancha_Checked(object sender, RoutedEventArgs e)
        {
            estado = "NO ENGANCHA";
        }
        private void rdhdmisulfa_Checked(object sender, RoutedEventArgs e)
        {
            estado = "HDMI SULFATADO";
        }
        private void rdnoenciende_Checked(object sender, RoutedEventArgs e)
        {
            estado = "NO ENCIENDE";
        }
        private void rdotro_Checked(object sender, RoutedEventArgs e)
        {
            estado = "OTRO";
        }
    }
}


