using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para BodegaCpes.xaml
    /// </summary>
    public partial class BodegaCpes : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        DataTable dt = new DataTable();
        public BodegaCpes()
        {
            InitializeComponent();
            txtserial.Focus();
            info.Visibility = Visibility.Visible;
            progress.Visibility = Visibility.Hidden;
            txtserial.Text = Consumos.serialcpe;
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            connection.Open();
            SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @SERIAL", connection); //CONSULTA SQL
            command_buscar.Parameters.AddWithValue("@SERIAL", txtserial.Text);
            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
            if (reader_buscar.Read())
            {

                txtcodigo.Content = Convert.ToString(reader_buscar["CODIGO"]);
                txtdescripcion.Content = Convert.ToString(reader_buscar["DESCRIPCIÓN"]);
                txtestado.Content = Convert.ToString(reader_buscar["ESTADO"]);
                txtidlugar.Content = Convert.ToString(reader_buscar["ID_LUGAR"]);
                txtlugarasignacion.Content = Convert.ToString(reader_buscar["LUGAR_ASIGNACIÓN"]);
                txtserial2.Content = Convert.ToString(reader_buscar["SERIAL"]);

                reader_buscar.Close();

                SqlCommand command_buscar1 = new SqlCommand("Select * from tbtrasavilidadcpe Where SERIAL = @SERIAL", connection); //CONSULTA SQL
                command_buscar1.Parameters.AddWithValue("@SERIAL", txtserial.Text);
                SqlDataReader reader_buscar1 = command_buscar1.ExecuteReader();
                if (reader_buscar1.Read())
                {
                    reader_buscar1.Close();
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = command_buscar1;
                    DataTable dataTable1 = new DataTable();
                    adapter1.Fill(dataTable1);
                    gvBodegaPpalCPS.ItemsSource = dataTable1.DefaultView;
                }
                else
                {
                    reader_buscar1.Close();
                    MessageBox.Show("Sin trasabilidad");
                }
            }
            else
            {
                MessageBox.Show("No existe");
                reader_buscar.Close();
            }
        }
        private void btnexportar_Click(object sender, RoutedEventArgs e)
        {
            exportatotal();
        }
        public void exportatotal()
        {
            info.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Visible;
            connection.Close();
            connection.Open();
            //MUESTRA DATOS EN GREADVIEW
            SqlCommand command1 = new SqlCommand("Select * from tbbodega_cpesppal", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command1;
            adapter.Fill(dt);
            gvBodegaPpalCPS.ItemsSource = dt.DefaultView;
            btnbuscar.IsEnabled = false;
            AutoClosingMessageBox.Show("En un momento el documento se exportará", "", 1000);
            try
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
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
                    double cont = dt.Rows.Count;
                    double barr = 100 / cont;
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        // to do: format datetime values before printing
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            workSheet.Cells[i + 2, j + 1] = "'" + dt.Rows[i][j];
                        }
                        this.barraprogress.Dispatcher.BeginInvoke((ThreadStart)delegate { this.barraprogress.Value += barr; });
                    }
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        MessageBox.Show("Documento exportardo, recuerde guardarlo");
                        excelApp.Visible = true;
                        info.Visibility = Visibility.Visible;
                        progress.Visibility = Visibility.Hidden;
                        btnbuscar.IsEnabled = true;
                    });
                }));
                thread.Start();
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    info.Visibility = Visibility.Visible;
                    progress.Visibility = Visibility.Hidden;
                    MessageBox.Show(Convert.ToString(ex));
                    btnbuscar.IsEnabled = false;
                });
            }
        }
        private void btnnewcodigo_Click(object sender, RoutedEventArgs e)
        {
            Cargacpes ccpe = new Cargacpes();
            ccpe.Show();
        }

        private void btntrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serial = Microsoft.VisualBasic.Interaction.InputBox("Ingrese serial del CPE a reintegrar ", "Registro SERIAL", "", 200, 0);
                string tabla = "";
                connection.Close();
                connection.Open();
                SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial", connection);
                command_buscar.Parameters.AddWithValue("@serial", serial);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    string cedula = Convert.ToString(reader_buscar["ID_LUGAR"]);
                    string nombre = Convert.ToString(reader_buscar["LUGAR_ASIGNACIÓN"]);
                    serial = Convert.ToString(reader_buscar["SERIAL"]);
                    tabla = "tabla" + cedula;
                    string descripción = "";
                    reader_buscar.Close();
                    SqlCommand command_buscar1 = new SqlCommand("Select * from " + tabla + " Where ITEM = @item", connection);
                    command_buscar1.Parameters.AddWithValue("@item", Convert.ToString(serial));
                    SqlDataReader reader_buscar1 = command_buscar1.ExecuteReader();
                    if (reader_buscar1.Read())
                    {
                        string estado = Convert.ToString(reader_buscar1["ESTADO"]);
                        if (true) // AQUI OPCION ESTADO = REINTEGRAR
                        {
                            reader_buscar1.Close();
                            string cadena = "DELETE from " + tabla + " Where ITEM = @itemD";
                            SqlCommand comando = new SqlCommand(cadena, connection);
                            comando.Parameters.AddWithValue("@itemD", serial);
                            comando.ExecuteNonQuery();

                            string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'ALMACENADO', LUGAR_ASIGNACIÓN = 'BODEGA PPAL', ID_LUGAR = 'REINTEGRO'  Where SERIAL = '" + serial + "'";
                            SqlCommand command_update = new SqlCommand(query, connection);
                            command_update.Parameters.AddWithValue("@CANTIDAD", serial);
                            command_update.ExecuteNonQuery();
                            connection.Close();
                            connection.Open();
                            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
                            SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
                            command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(serial));
                            SqlDataReader reader_buscar12 = command_buscar12.ExecuteReader();
                            if (reader_buscar12.Read())
                            {
                                descripción = Convert.ToString(reader_buscar12["DESCRIPCIÓN"]);
                                string codigo = Convert.ToString(reader_buscar12["CODIGO"]);
                                reader_buscar12.Close();
                                string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                SqlCommand sqlCommand1 = new SqlCommand(trasavilidad, connection);
                                sqlCommand1.Parameters.AddWithValue("@codigo", codigo);
                                sqlCommand1.Parameters.AddWithValue("@descripcion", descripción);
                                sqlCommand1.Parameters.AddWithValue("@serial", serial);
                                sqlCommand1.Parameters.AddWithValue("@estado", "ALMACENADO");
                                sqlCommand1.Parameters.AddWithValue("@lugar", "REINTEGRO");
                                sqlCommand1.Parameters.AddWithValue("@idlugar", "REINTEGRO");
                                sqlCommand1.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                sqlCommand1.ExecuteNonQuery();
                            }
                            reader_buscar12.Close();
                            var fecha_m_a = DateTime.Now.ToString("MM/yyyy");
                            string historicoreinte = "insert HistoricoReintegros (SERIAL, DESCRIPCION, ID_TECNICO, NOMBRE, FECHA_REINTEGRO, LOGISTICO, MES_AÑO) VALUES(@serial, @descripcion, @id_tec, @nombre, @fecha, @logistico, @mes_año)";
                            SqlCommand sqlCommand = new SqlCommand(historicoreinte, connection);
                            sqlCommand.Parameters.AddWithValue("@serial", serial);
                            sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                            sqlCommand.Parameters.AddWithValue("@id_tec", cedula);
                            sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                            sqlCommand.Parameters.AddWithValue("@fecha", fecha);
                            sqlCommand.Parameters.AddWithValue("@logistico", ClaseCompartida.nombre);
                            sqlCommand.Parameters.AddWithValue("@mes_año", fecha_m_a);
                            sqlCommand.ExecuteNonQuery();

                            MessageBox.Show("CPE reintegrado a Bodega principal");
                        }
                        else
                        {
                            MessageBox.Show("NO SE PUEDE REINTEGRAR. \nEl técnico aun no ha reintegrado", "REGISTRO");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CPE no encontrado en ningun tecnico");
                    }
                }
                else
                {
                    MessageBox.Show("Serial no existe en sistema");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algó paso, por favor intenta de nuevo\n" + ex.ToString());
            }
        }

        private void btnexcelenciamod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serial = Microsoft.VisualBasic.Interaction.InputBox("Ingrese serial del CPE a reintegrar ", "Registro SERIAL", "", 200, 0);
                string tabla = "";
                connection.Close();
                connection.Open();
                SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial", connection);
                command_buscar.Parameters.AddWithValue("@serial", serial);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    string cedula = Convert.ToString(reader_buscar["ID_LUGAR"]);
                    string nombre = Convert.ToString(reader_buscar["LUGAR_ASIGNACIÓN"]);
                    serial = Convert.ToString(reader_buscar["SERIAL"]);
                    tabla = "tabla" + cedula;
                    string descripción = "";
                    reader_buscar.Close();
                    SqlCommand command_buscar1 = new SqlCommand("Select * from " + tabla + " Where ITEM = @item", connection);
                    command_buscar1.Parameters.AddWithValue("@item", Convert.ToString(serial));
                    SqlDataReader reader_buscar1 = command_buscar1.ExecuteReader();
                    if (reader_buscar1.Read())
                    {
                        string estado = Convert.ToString(reader_buscar1["ESTADO"]);
                        if (true) // AQUI OPCION ESTADO = REINTEGRAR
                        {
                            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
                            reader_buscar1.Close();
                            string cadena = "DELETE from " + tabla + " Where ITEM = @itemD";
                            SqlCommand comando = new SqlCommand(cadena, connection);
                            comando.Parameters.AddWithValue("@itemD", serial);
                            comando.ExecuteNonQuery();

                            string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'GARANTÍA', LUGAR_ASIGNACIÓN = 'BODEGA PPAL', ID_LUGAR = 'REINTEGRO | " + ClaseCompartida.nombre + "', NOTAS = 'OTRO', FECHA_REVISIÓN = '" + fecha + "'  Where SERIAL = '" + serial + "'";
                            SqlCommand command_update = new SqlCommand(query, connection);
                            command_update.Parameters.AddWithValue("@CANTIDAD", serial);
                            command_update.ExecuteNonQuery();
                            connection.Close();
                            connection.Open();

                            SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
                            command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(serial));
                            SqlDataReader reader_buscar12 = command_buscar12.ExecuteReader();
                            if (reader_buscar12.Read())
                            {
                                descripción = Convert.ToString(reader_buscar12["DESCRIPCIÓN"]);
                                string codigo = Convert.ToString(reader_buscar12["CODIGO"]);
                                reader_buscar12.Close();
                                string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                SqlCommand sqlCommand1 = new SqlCommand(trasavilidad, connection);
                                sqlCommand1.Parameters.AddWithValue("@codigo", codigo);
                                sqlCommand1.Parameters.AddWithValue("@descripcion", descripción);
                                sqlCommand1.Parameters.AddWithValue("@serial", serial);
                                sqlCommand1.Parameters.AddWithValue("@estado", "ALMACENADO");
                                sqlCommand1.Parameters.AddWithValue("@lugar", "REINTEGRO");
                                sqlCommand1.Parameters.AddWithValue("@idlugar", "REINTEGRO");
                                sqlCommand1.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                sqlCommand1.ExecuteNonQuery();
                            }
                            reader_buscar12.Close();
                            var fecha_m_a = DateTime.Now.ToString("MM/yyyy");
                            string historicoreinte = "insert HistoricoReintegros (SERIAL, DESCRIPCION, ID_TECNICO, NOMBRE, FECHA_REINTEGRO, LOGISTICO, MES_AÑO) VALUES(@serial, @descripcion, @id_tec, @nombre, @fecha, @logistico, @mes_año)";
                            SqlCommand sqlCommand = new SqlCommand(historicoreinte, connection);
                            sqlCommand.Parameters.AddWithValue("@serial", serial);
                            sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                            sqlCommand.Parameters.AddWithValue("@id_tec", cedula);
                            sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                            sqlCommand.Parameters.AddWithValue("@fecha", fecha);
                            sqlCommand.Parameters.AddWithValue("@logistico", ClaseCompartida.nombre);
                            sqlCommand.Parameters.AddWithValue("@mes_año", fecha_m_a);
                            sqlCommand.ExecuteNonQuery();

                            MessageBox.Show("CPE enviado a garantía");
                        }
                        else
                        {
                            MessageBox.Show("NO SE PUEDE REINTEGRAR. \nEl técnico aun no ha reintegrado", "REGISTRO");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CPE no encontrado en ningun tecnico");
                    }
                }
                else
                {
                    MessageBox.Show("Serial no existe en sistema");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Equipo no asignado");
            }
        }
    }
}
