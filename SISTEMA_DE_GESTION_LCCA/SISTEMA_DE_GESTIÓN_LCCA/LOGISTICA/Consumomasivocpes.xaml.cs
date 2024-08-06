using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para Consumomasivocpes.xaml
    /// </summary>
    public partial class Consumomasivocpes : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        private DataSet dtsTablas = new DataSet();
        public Consumomasivocpes()
        {
            InitializeComponent();
            progress.Visibility = Visibility.Hidden;
        }

        private void btnloandfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Worbook|*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                string namefile = openFileDialog.FileName;
                cbhojas.IsEnabled = true;
                cbhojas.Items.Clear();
                try
                {
                    FileStream fsSource = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(fsSource);
                    dtsTablas = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    foreach (DataTable tabla in dtsTablas.Tables)
                    {
                        cbhojas.Items.Add(tabla.TableName);
                    }
                    cbhojas.SelectedIndex = 0;
                    btbviwfile.IsEnabled = true;
                    Image myImage1 = new Image();
                    BitmapImage bi1 = new BitmapImage();
                    bi1.BeginInit();
                    bi1.UriSource = new Uri("https://i.ibb.co/XFMwVxJ/iconexcelon.png");
                    bi1.EndInit();
                    imgexcel.Stretch = Stretch.Uniform;
                    imgexcel.Source = bi1;
                    reader.Close();
                }
                catch (Exception)
                {
                    dgviewfile.Visibility = Visibility.Hidden;
                    Image myImage1 = new Image();
                    BitmapImage bi1 = new BitmapImage();
                    bi1.BeginInit();
                    bi1.UriSource = new Uri("https://i.ibb.co/Fg98w3z/iconexcelnull.png");
                    bi1.EndInit();
                    imgexcel.Stretch = Stretch.Uniform;
                    imgexcel.Source = bi1;
                    cbhojas.IsEnabled = false;
                    btbviwfile.IsEnabled = false;
                    btnuploanddata.IsEnabled = false;
                    MessageBox.Show("Archivo en uso, primero ciere para continuar");
                }
            }
        }

        private void btbviwfile_Click(object sender, RoutedEventArgs e)
        {
            btnuploanddata.IsEnabled = true;
            dgviewfile.Visibility = Visibility.Visible;
            dgviewfile.ItemsSource = dtsTablas.Tables[cbhojas.SelectedIndex].DefaultView;
            dgviewfile.SelectedItem = null;
            dgviewfile.UnselectAll();
            dgviewfile.IsReadOnly = true;
        }

        private void cbhojas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void btnuploanddata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        dgviewfile.Visibility = Visibility.Hidden;
                        progress.Visibility = Visibility.Visible;
                        btnuploanddata.IsEnabled = false;
                    });

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        List<Listado> _lista = new List<Listado>();
                        Listado lt = new Listado();
                        string estado = "inicio";
                        connection.Close();
                        connection.Open();
                        string cadena = "delete from tbconsumomasivo";
                        SqlCommand comando = new SqlCommand(cadena, connection);
                        comando.ExecuteNonQuery();
                        using (SqlBulkCopy s = new SqlBulkCopy(connection))
                        {
                            DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];
                            //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS                
                            s.ColumnMappings.Add("CÉDULA", "CÉDULA");
                            s.ColumnMappings.Add("NOMBRE", "NOMBRE");
                            s.ColumnMappings.Add("CUENTA_CONSUMO", "CUENTA_CONSUMO");
                            s.ColumnMappings.Add("OT", "OT");
                            s.ColumnMappings.Add("NODO", "NODO");
                            s.ColumnMappings.Add("TIPO_TRABAJO", "TIPO_TRABAJO");
                            s.ColumnMappings.Add("ITEM", "ITEM");
                            s.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            s.ColumnMappings.Add("CANTIDAD", "CANTIDAD");
                            s.ColumnMappings.Add("FECHA_ACTIVIDAD", "FECHA_ACTIVIDAD");
                            //definimos la tabla a cargar
                            s.DestinationTableName = "tbconsumomasivo";
                            s.BulkCopyTimeout = 2500;
                            s.WriteToServer(data);
                        }

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        excel.Application.Workbooks.Add(true);
                        int IndiceColumna = 0;
                        int IndeceFila = 0;


                        SqlCommand command2 = new SqlCommand("Select * from tbconsumomasivo", connection);
                        SqlDataAdapter adapter2 = new SqlDataAdapter();
                        adapter2.SelectCommand = command2;
                        DataTable dataTable2 = new DataTable();
                        adapter2.Fill(dataTable2);
                        foreach (DataRow row in dataTable2.Rows)
                        {
                            string cpe = Convert.ToString(row["ITEM"]);
                            string tablatec = "tabla" + Convert.ToString(row["CÉDULA"]);
                            string cuenta = Convert.ToString(row["CUENTA_CONSUMO"]);
                            string ot = Convert.ToString(row["OT"]);
                            string cedula = Convert.ToString(row["CÉDULA"]);
                            string nombre = Convert.ToString(row["NOMBRE"]);
                            string nodo = Convert.ToString(row["NODO"]);
                            string tipo_ot = Convert.ToString(row["TIPO_TRABAJO"]);
                            string material = Convert.ToString(row["MATERIAL"]);
                            string fecha = Convert.ToString(row["FECHA_ACTIVIDAD"]);

                            if (estado == "inicio")
                            {
                                if (tablatec != "tablaBODEGA")
                                {
                                    try
                                    {
                                        SqlCommand command_buscar = new SqlCommand("Select * from " + tablatec + " Where ITEM = @ITEM", connection); //CONSULTA SQL
                                        command_buscar.Parameters.AddWithValue("@ITEM", cpe);
                                        SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                                        if (reader_buscar.Read())
                                        {
                                            reader_buscar.Close();
                                            string cadena1 = "DELETE from " + tablatec + " Where ITEM = @itemD";
                                            SqlCommand comando1 = new SqlCommand(cadena1, connection);
                                            comando1.Parameters.AddWithValue("@itemD", cpe);
                                            comando1.ExecuteNonQuery();

                                            string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'CONSUMIDO', LUGAR_ASIGNACIÓN = '" + cuenta + "' ,ID_LUGAR = '" + ot + "'  Where SERIAL = '" + cpe + "'";
                                            SqlCommand command_update = new SqlCommand(query, connection);
                                            command_update.Parameters.AddWithValue("@CANTIDAD", cpe);
                                            command_update.ExecuteNonQuery();

                                            string queryIN = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad)";
                                            SqlCommand command = new SqlCommand(queryIN, connection);
                                            command.Parameters.AddWithValue("@cédula", cedula);
                                            command.Parameters.AddWithValue("@nombre", nombre);
                                            command.Parameters.AddWithValue("@cuenta_consumo", cuenta);
                                            command.Parameters.AddWithValue("@ot", ot);
                                            command.Parameters.AddWithValue("@nodo", nodo);
                                            command.Parameters.AddWithValue("@tipo_trabajo", tipo_ot);
                                            command.Parameters.AddWithValue("@item", cpe);
                                            command.Parameters.AddWithValue("@material", material);
                                            command.Parameters.AddWithValue("@cantidad", 1);
                                            command.Parameters.AddWithValue("@fecha_actividad", fecha + " | " + ClaseCompartida.nombre);
                                            command.ExecuteNonQuery();

                                            SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
                                            command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(cpe));
                                            SqlDataReader reader_buscar12 = command_buscar12.ExecuteReader();
                                            if (reader_buscar12.Read())
                                            {
                                                string descripción = Convert.ToString(reader_buscar12["DESCRIPCIÓN"]);
                                                string codigo = Convert.ToString(reader_buscar12["CODIGO"]);
                                                reader_buscar12.Close();
                                                string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                                SqlCommand sqlCommand = new SqlCommand(trasavilidad, connection);
                                                sqlCommand.Parameters.AddWithValue("@codigo", codigo);
                                                sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                                                sqlCommand.Parameters.AddWithValue("@serial", cpe);
                                                sqlCommand.Parameters.AddWithValue("@estado", "CONSUMIDO");
                                                sqlCommand.Parameters.AddWithValue("@lugar", cuenta);
                                                sqlCommand.Parameters.AddWithValue("@idlugar", ot);
                                                sqlCommand.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                                sqlCommand.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            reader_buscar.Close();
                                            lt.serial = cpe;
                                            _lista.Add(lt);
                                            IndeceFila++;
                                            IndiceColumna = 0;
                                            IndiceColumna++;
                                            excel.Cells[IndeceFila + 1, IndiceColumna] = "' " + cpe;

                                        }
                                    }
                                    catch (Exception)
                                    {

                                        lt.serial = cpe;
                                        _lista.Add(lt);
                                        IndeceFila++;
                                        IndiceColumna = 0;
                                        IndiceColumna++;
                                        excel.Cells[IndeceFila + 1, IndiceColumna] = "' " + cpe;
                                    }

                                }
                                else
                                {
                                    SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
                                    command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(cpe));
                                    SqlDataReader reader_buscar12 = command_buscar12.ExecuteReader();
                                    if (reader_buscar12.Read())
                                    {
                                        string descripción = Convert.ToString(reader_buscar12["DESCRIPCIÓN"]);
                                        string codigo = Convert.ToString(reader_buscar12["CODIGO"]);
                                        string estadocpe = Convert.ToString(reader_buscar12["ESTADO"]);
                                        reader_buscar12.Close();
                                        if (estadocpe == " ALMACENADO")
                                        {
                                            string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'CONSUMIDO', LUGAR_ASIGNACIÓN = '" + cuenta + "' ,ID_LUGAR = '" + ot + "'  Where SERIAL = '" + cpe + "'";
                                            SqlCommand command_update = new SqlCommand(query, connection);
                                            command_update.Parameters.AddWithValue("@CANTIDAD", cpe);
                                            command_update.ExecuteNonQuery();

                                            string queryIN = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad)";
                                            SqlCommand command = new SqlCommand(queryIN, connection);
                                            command.Parameters.AddWithValue("@cédula", cedula);
                                            command.Parameters.AddWithValue("@nombre", nombre);
                                            command.Parameters.AddWithValue("@cuenta_consumo", cuenta);
                                            command.Parameters.AddWithValue("@ot", ot);
                                            command.Parameters.AddWithValue("@nodo", nodo);
                                            command.Parameters.AddWithValue("@tipo_trabajo", tipo_ot);
                                            command.Parameters.AddWithValue("@item", cpe);
                                            command.Parameters.AddWithValue("@material", material);
                                            command.Parameters.AddWithValue("@cantidad", 1);
                                            command.Parameters.AddWithValue("@fecha_actividad", fecha + " | " + ClaseCompartida.nombre);
                                            command.ExecuteNonQuery();

                                            string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                            SqlCommand sqlCommand = new SqlCommand(trasavilidad, connection);
                                            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
                                            sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                                            sqlCommand.Parameters.AddWithValue("@serial", cpe);
                                            sqlCommand.Parameters.AddWithValue("@estado", "CONSUMIDO");
                                            sqlCommand.Parameters.AddWithValue("@lugar", cuenta);
                                            sqlCommand.Parameters.AddWithValue("@idlugar", ot);
                                            sqlCommand.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                            sqlCommand.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            lt.serial = cpe;
                                            _lista.Add(lt);
                                            IndeceFila++;
                                            IndiceColumna = 0;
                                            IndiceColumna++;
                                            excel.Cells[IndeceFila + 1, IndiceColumna] = "' " + cpe;
                                        }
                                    }
                                }
                            }

                        }
                        MessageBox.Show("Proceso finalizado, a continuación un listado del serial que no fue posible consumir, por favor revise el estado de estos");
                        excel.Visible = true;
                    });

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        dgviewfile.Visibility = Visibility.Visible;
                        progress.Visibility = Visibility.Hidden;
                        btnuploanddata.IsEnabled = true;
                    });

                }));
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.ToString());
            }
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Logistica lg = new Logistica();
            lg.Show();
            this.Close();
        }

        private void btnhome_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
