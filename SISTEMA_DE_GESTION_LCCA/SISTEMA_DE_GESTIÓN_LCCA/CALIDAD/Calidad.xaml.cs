using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Calidad.xaml
    /// </summary>
    public partial class Calidad : Window
    {
        private DataSet dtsTablas = new DataSet();
        SqlConnection connection = new SqlConnection(conexion.cadena);
        string nametec = "";
        int cedulatec = 0;
        string _mes = "";
        string periodopun = "";
        public Calidad()
        {
            InitializeComponent();
            loadfom();
            lbloanddata.Visibility = Visibility.Hidden;
        }
        private void loadfom()
        {
            loandmes();
            loandranking();
            targets.Visibility = Visibility.Hidden;
            restapuntos.Visibility = Visibility.Hidden;
            btnloadexc.IsEnabled = false;
            lbperiodo.Content = "";
            lbpuntos.Content = "";
            btnrestapun.IsEnabled = false;
            lateralizdata.Visibility = Visibility.Hidden;
            dgviewfile.Visibility = Visibility.Hidden;
            btnuploanddata.Visibility = Visibility.Hidden;
            cbxmes_Copy.Visibility = Visibility.Hidden;
        }
        private void loandmes()
        {
            cbxtec.IsEnabled = false;
            cbxmes.Items.Clear();
            cbxmes.Items.Add("ENERO");
            cbxmes.Items.Add("FEBRERO");
            cbxmes.Items.Add("MARZO");
            cbxmes.Items.Add("ABRIL");
            cbxmes.Items.Add("MAYO");
            cbxmes.Items.Add("JUNIO");
            cbxmes.Items.Add("JULIO");
            cbxmes.Items.Add("AGOSTO");
            cbxmes.Items.Add("SEPTIEMBRE");
            cbxmes.Items.Add("OCTUBRE");
            cbxmes.Items.Add("NOVIEMBRE");
            cbxmes.Items.Add("DICIEMBRE");

            cbxmes_Copy.Items.Clear();
            cbxmes_Copy.Items.Add("ENERO");
            cbxmes_Copy.Items.Add("FEBRERO");
            cbxmes_Copy.Items.Add("MARZO");
            cbxmes_Copy.Items.Add("ABRIL");
            cbxmes_Copy.Items.Add("MAYO");
            cbxmes_Copy.Items.Add("JUNIO");
            cbxmes_Copy.Items.Add("JULIO");
            cbxmes_Copy.Items.Add("AGOSTO");
            cbxmes_Copy.Items.Add("SEPTIEMBRE");
            cbxmes_Copy.Items.Add("OCTUBRE");
            cbxmes_Copy.Items.Add("NOVIEMBRE");
            cbxmes_Copy.Items.Add("DICIEMBRE");
        }
        private void cbxmes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                cbxtec.Items.Clear();
                connection.Close();
                connection.Open();
                string select = cbxmes.SelectedValue.ToString();
                SqlCommand command2 = new SqlCommand("Select NOMBRE from excelencia" + select + "", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    cbxtec.Items.Add(Convert.ToString(row["NOMBRE"]));
                    lsreinsidencia.Items.Add(Convert.ToString(row["NOMBRE"]));
                }

                cbxtec.Items.IsLiveSorting = true;
                _mes = select;
                cbxtec.IsEnabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show("No existe histórico", "Filed");
                cantargets.Visibility = Visibility.Visible;
                cbxtec.IsEnabled = false;
                targets.Visibility = Visibility.Hidden;
                lbinfonull.Visibility = Visibility.Visible;
                btnloadexc.IsEnabled = false;
                btnrestapun.IsEnabled = false;
                restapuntos.Visibility = Visibility.Hidden;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * from excelencia" + _mes + " WHERE CEDULA = '" + cedulatec + "'", connection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    try
                    {
                        lbcorte1.Content = Convert.ToString(dataReader["SEMANA1"]);
                        string nota1 = Convert.ToString(dataReader["NOTA1"]);
                        string q7_1 = Convert.ToString(dataReader["Q7_1"]);
                        string efectividad_1 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["EFECTIVIDAD1"]));
                        string chd1 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["CDH1"]));
                        string completads_1 = Convert.ToString(dataReader["COMPLETADOS1"]);
                        //string n = String.Format("{0:P2}.", Convert.ToInt32(efectividad_1));

                        Nota1.Content = "Nota: ................................................... " + nota1;
                        Q71.Content = "Q7: ...................................................... " + q7_1;
                        Efectividad1.Content = "Efectividad: ....................................... " + efectividad_1;
                        Chd1.Content = "CDH: ................................................ " + chd1;
                        Completada1.Content = "Completadas: ................................ " + completads_1;
                        tg1.Visibility = Visibility.Visible;
                    }
                    catch (Exception)
                    {
                        tg1.Visibility = Visibility.Hidden;

                    }//1
                    try
                    {
                        lbcorte2.Content = Convert.ToString(dataReader["SEMANA2"]);
                        string nota2 = Convert.ToString(dataReader["NOTA2"]);
                        string q7_2 = Convert.ToString(dataReader["Q7_2"]);
                        string efectividad_2 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["EFECTIVIDAD2"]));
                        string chd2 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["CDH2"]));
                        string completads_2 = Convert.ToString(dataReader["COMPLETADOS2"]);

                        Nota2.Content = "Nota: ...................................................... " + nota2;
                        Q72.Content = "Q7: ......................................................... " + q7_2;
                        Efectividad2.Content = "Efectividad: .......................................... " + efectividad_2;
                        Chd2.Content = "CDH: ................................................... " + chd2;
                        Completada2.Content = "Completadas: ................................... " + completads_2;
                        tg2.Visibility = Visibility.Visible;
                    }
                    catch (Exception)
                    {
                        tg2.Visibility = Visibility.Hidden;

                    }//2
                    try
                    {

                        lbcorte3.Content = Convert.ToString(dataReader["SEMANA3"]);
                        string nota3 = Convert.ToString(dataReader["NOTA3"]);
                        string q7_3 = Convert.ToString(dataReader["Q7_3"]);
                        string efectividad_3 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["EFECTIVIDAD3"]));
                        string chd3 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["CDH3"]));
                        string completads_3 = Convert.ToString(dataReader["COMPLETADOS3"]);

                        Nota3.Content = "Nota: ...................................................... " + nota3;
                        Q73.Content = "Q7: ......................................................... " + q7_3;
                        Efectividad3.Content = "Efectividad: .......................................... " + efectividad_3;
                        Chd3.Content = "CDH: ................................................... " + chd3;
                        Completada3.Content = "Completadas: ................................... " + completads_3;
                        tg3.Visibility = Visibility.Visible;
                    }
                    catch (Exception)
                    {
                        tg3.Visibility = Visibility.Hidden;

                    }//3
                    try
                    {
                        lbcorte4.Content = Convert.ToString(dataReader["SEMANA4"]);
                        string nota4 = Convert.ToString(dataReader["NOTA4"]);
                        string q7_4 = Convert.ToString(dataReader["Q7_4"]);
                        string efectividad_4 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["EFECTIVIDAD4"]));
                        string chd4 = String.Format("{0:P2}.", Convert.ToDecimal(dataReader["CDH4"]));
                        string completads_4 = Convert.ToString(dataReader["COMPLETADOS4"]);

                        Nota4.Content = "Nota: ...................................................... " + nota4;
                        Q74.Content = "Q7: ......................................................... " + q7_4;
                        Efectividad4.Content = "Efectividad: .......................................... " + efectividad_4;
                        Chd4.Content = "CDH: ................................................... " + chd4;
                        Completada4.Content = "Completadas: ................................... " + completads_4;
                        tg4.Visibility = Visibility.Visible;
                    }
                    catch (Exception)
                    {
                        tg4.Visibility = Visibility.Hidden;
                    }//4                    
                    loandpuntos();
                }
                targets.Visibility = Visibility.Visible;
                lbinfonull.Visibility = Visibility.Hidden;
                btnrestapun.IsEnabled = true;
                restapuntos.Visibility = Visibility.Hidden;
                cantargets.Visibility = Visibility.Visible;
                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro excelencia" + ex, "Failed");
                lbinfonull.Visibility = Visibility.Visible;
                targets.Visibility = Visibility.Hidden;
                btnrestapun.IsEnabled = false;
            }
        }
        private void loandranking()
        {
            connection.Close();
            connection.Open();
            for (int i = 0; i < 4; i++)
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * from excelenciarankig where PUESTO = " + i + "", connection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    switch (i)
                    {
                        case 1:
                            Ranking1.Text = Convert.ToString(dataReader["NOMBRE"]);
                            break;
                        case 2:
                            Ranking2.Text = Convert.ToString(dataReader["NOMBRE"]);
                            break;
                        case 3:
                            Ranking3.Text = Convert.ToString(dataReader["NOMBRE"]);
                            break;
                    }
                }
                dataReader.Close();
            }
        }
        private void cbxtec_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxtec.SelectedItem != null)
                {
                    connection.Close();
                    connection.Open();
                    string nombre = cbxtec.SelectedItem.ToString();
                    SqlCommand command_buscar = new SqlCommand("Select * from excelencia" + _mes + " WHERE NOMBRE = @id", connection); //CONSULTA SQL            
                    command_buscar.Parameters.AddWithValue("@id", nombre);
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        cedulatec = Convert.ToInt32(reader_buscar["CEDULA"]);
                    }
                    nametec = cbxtec.SelectedItem.ToString();
                    btnloadexc.IsEnabled = true;
                    restapuntos.Visibility = Visibility.Hidden;
                    btnrestapun.IsEnabled = false;
                    cantargets.Visibility = Visibility.Hidden;
                    lbinfonull.Visibility = Visibility.Visible;
                    btnrestapun.Content = "VER DETALLES";
                }
                else
                {
                    btnloadexc.IsEnabled = false;
                    restapuntos.Visibility = Visibility.Hidden;
                    btnrestapun.IsEnabled = false;
                    cantargets.Visibility = Visibility.Hidden;
                    btnrestapun.Content = "VER DETALLES";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(Convert.ToString(ex));
            }

        }
        private void loandpuntos()
        {

            decimal puntos_suma = 0;
            connection.Close();
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * from puntos" + _mes + " WHERE CEDULA = '" + cedulatec + "'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = sqlCommand;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            foreach (DataRow row in dataTable2.Rows)
            {
                if (Convert.ToString(row["SUMA"]) == "TRUE")
                {
                    puntos_suma += Convert.ToDecimal(row["PUNTOS"]);
                }
            }
            lbpuntos.Content = puntos_suma.ToString("#.##");
            loandperiodo();
            lbperiodo.Content = periodopun;
        }
        private void loandperiodo()
        {
            switch (_mes)
            {
                case "ENERO":
                    periodopun = "15 DICIEMBRE AL 14 DE ENERO";
                    break;
                case "FEBRERO":
                    periodopun = "15 ENERO AL 14 DE FEBRERO";
                    break;
                case "MARZO":
                    periodopun = "15 FEBRERO AL 14 DE MARZO";
                    break;
                case "ABRIL":
                    periodopun = "15 MARZO AL 14 DE ABRIL";
                    break;
                case "MAYO":
                    periodopun = "15 ABRIL AL 14 DE MAYO";
                    break;
                case "JUNIO":
                    periodopun = "15 MAYO AL 14 DE JUNIO";
                    break;
                case "JULIO":
                    periodopun = "15 JUNIO AL 14 DE JULIO";
                    break;
                case "AGOSTO":
                    periodopun = "15 JULIO AL 14 DE AGOSTO";
                    break;
                case "SEPTIEMBRE":
                    periodopun = "15 AGOSTO AL 14 DE SEPTIEMBRE";
                    break;
                case "OCTUBRE":
                    periodopun = "15 SEPTIEMBRE AL 14 DE OCTUBRE";
                    break;
                case "NOVIEMBRE":
                    periodopun = "15 OCTUBRE AL 14 DE NOVIEMBRE";
                    break;
                case "DICIEMBRE":
                    periodopun = "15 NOVIEMBRE AL 14 DE DICIEMBRE";
                    break;
            }
        }
        private void btnrestapun_Click(object sender, RoutedEventArgs e)
        {
            switch (btnrestapun.Content)
            {
                case "VOLVER":
                    {
                        try
                        {
                            lsreinsidencia.SelectedItem = -1;
                            connection.Close();
                            connection.Open();
                            SqlCommand sqlCommand = new SqlCommand("SELECT * from excelencia" + _mes + " WHERE CEDULA = '" + cedulatec + "'", connection);
                            SqlDataReader dataReader = sqlCommand.ExecuteReader();
                            if (dataReader.Read())
                            {

                                lbcorte1.Content = Convert.ToString(dataReader["SEMANA1"]);
                                string nota1 = Convert.ToString(dataReader["NOTA1"]);
                                string q7_1 = Convert.ToString(dataReader["Q7_1"]);
                                string efectividad_1 = Convert.ToString(dataReader["EFECTIVIDAD1"]);
                                string chd1 = Convert.ToString(dataReader["CDH1"]);
                                string completads_1 = Convert.ToString(dataReader["COMPLETADOS1"]);

                                Nota1.Content = "Nota: ......................................................... " + nota1;
                                Q71.Content = "Q7: ............................................................ " + q7_1;
                                Efectividad1.Content = "Efectividad: ............................................. " + efectividad_1;
                                Chd1.Content = "CDH: ...................................................... " + chd1;
                                Completada1.Content = "Completadas: ...................................... " + completads_1;


                                lbcorte2.Content = Convert.ToString(dataReader["SEMANA2"]);
                                string nota2 = Convert.ToString(dataReader["NOTA2"]);
                                string q7_2 = Convert.ToString(dataReader["Q7_2"]);
                                string efectividad_2 = Convert.ToString(dataReader["EFECTIVIDAD2"]);
                                string chd2 = Convert.ToString(dataReader["CDH2"]);
                                string completads_2 = Convert.ToString(dataReader["COMPLETADOS2"]);

                                Nota2.Content = "Nota: ......................................................... " + nota2;
                                Q72.Content = "Q7: ............................................................ " + q7_2;
                                Efectividad2.Content = "Efectividad: ............................................. " + efectividad_2;
                                Chd2.Content = "CDH: ...................................................... " + chd2;
                                Completada2.Content = "Completadas: ...................................... " + completads_2;

                                lbcorte3.Content = Convert.ToString(dataReader["SEMANA3"]);
                                string nota3 = Convert.ToString(dataReader["NOTA3"]);
                                string q7_3 = Convert.ToString(dataReader["Q7_3"]);
                                string efectividad_3 = Convert.ToString(dataReader["EFECTIVIDAD3"]);
                                string chd3 = Convert.ToString(dataReader["CDH3"]);
                                string completads_3 = Convert.ToString(dataReader["COMPLETADOS3"]);

                                Nota3.Content = "Nota: ......................................................... " + nota3;
                                Q73.Content = "Q7: ............................................................ " + q7_3;
                                Efectividad3.Content = "Efectividad: ............................................. " + efectividad_3;
                                Chd3.Content = "CDH: ...................................................... " + chd3;
                                Completada3.Content = "Completadas: ...................................... " + completads_3;


                                lbcorte4.Content = Convert.ToString(dataReader["SEMANA4"]);
                                string nota4 = Convert.ToString(dataReader["NOTA4"]);
                                string q7_4 = Convert.ToString(dataReader["Q7_4"]);
                                string efectividad_4 = Convert.ToString(dataReader["EFECTIVIDAD4"]);
                                string chd4 = Convert.ToString(dataReader["CDH4"]);
                                string completads_4 = Convert.ToString(dataReader["COMPLETADOS4"]);

                                Nota4.Content = "Nota: ......................................................... " + nota4;
                                Q74.Content = "Q7: ............................................................ " + q7_4;
                                Efectividad4.Content = "Efectividad: ............................................. " + efectividad_4;
                                Chd4.Content = "CDH: ...................................................... " + chd4;
                                Completada4.Content = "Completadas: ...................................... " + completads_4;
                                loandpuntos();
                            }
                            dataReader.Close();
                            targets.Visibility = Visibility.Visible;
                            lbinfonull.Visibility = Visibility.Hidden;
                            btnrestapun.IsEnabled = true;
                            restapuntos.Visibility = Visibility.Hidden;
                            cantargets.Visibility = Visibility.Visible;
                            btnrestapun.Content = "VER DETALLES";
                        }
                        catch (Exception es)
                        {
                            MessageBox.Show("No se encontro excelencia" + es, "Failed");
                            lbinfonull.Visibility = Visibility.Visible;
                            targets.Visibility = Visibility.Hidden;
                            btnrestapun.IsEnabled = false;
                            btnrestapun.Content = "VER DETALLES";
                        }

                        break;
                    }

                default:
                    cantargets.Visibility = Visibility.Hidden;
                    loandreinsi();
                    restapuntos.Visibility = Visibility.Visible;
                    btnrestapun.Content = "VOLVER";
                    break;
            }

        }
        private void loandreinsi()
        {
            try
            {
                lbpuntos_restados.IsEnabled = false;
                if (cbxmes.SelectedItem != null)
                {
                    lsreinsidencia.Items.Clear();
                    connection.Close();
                    connection.Open();
                    decimal puntos = 0;
                    string select = cbxmes.SelectedValue.ToString();
                    SqlCommand command2 = new SqlCommand("Select * from puntos" + select + "", connection);
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = command2;
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);
                    foreach (DataRow row in dataTable2.Rows)
                    {
                        string resta = Convert.ToString(row["RESTA"]);
                        int cedula = Convert.ToInt32(row["CEDULA"]);
                        if (resta == "TRUE" && cedula == cedulatec)
                        {
                            lsreinsidencia.Items.Add(Convert.ToString(row["ID_ORDEN"]));
                            puntos += Convert.ToDecimal(row["PUNTOS"]);
                        }
                    }
                    puntosreinsitotal.Content = "Total puntos restados: " + puntos.ToString("#.##");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void lsreinsidencia_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            try
            {
                SqlCommand command_buscar = new SqlCommand("Select * from puntos" + _mes + " WHERE ID_ORDEN = '" + lsreinsidencia.SelectedItem.ToString() + "'", connection); //CONSULTA SQL            
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    lbnotaspr.Text = "Notas y/o motivo: " + Convert.ToString(reader_buscar["NOTA_RESTA"]);
                    lbpuntos_restados.Text = "Puntos restados: " + Convert.ToDecimal(reader_buscar["PUNTOS"]).ToString("#.##");
                    reader_buscar.Close();
                }
                else
                {
                    lbpuntos_restados.Text = "";
                    lbnotaspr.Text = "";
                    MessageBox.Show("No se pudo obtener información", "Failed");
                    reader_buscar.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            funcionbtnloanddata();
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
                    MessageBox.Show("Archivo en uso, primero ciere para continuar");
                }
            }

        }
        private void btbviwfile_Click(object sender, RoutedEventArgs e)
        {
            dgviewfile.Visibility = Visibility.Visible;
            dgviewfile.ItemsSource = dtsTablas.Tables[cbhojas.SelectedIndex].DefaultView;
            dgviewfile.SelectedItem = null;
            dgviewfile.UnselectAll();
            dgviewfile.IsReadOnly = true;
            cbxmes_Copy.IsEnabled = true;
        }
        private void funcionbtnloanddata()
        {
            switch (lbloanddata.Content)
            {
                case "Subir data":
                    {
                        cantargets.Visibility = Visibility.Hidden;
                        restapuntos.Visibility = Visibility.Hidden;
                        lateraliztec.Visibility = Visibility.Hidden;
                        lateralizdata.Visibility = Visibility.Visible;
                        gridpuntos.Visibility = Visibility.Hidden;
                        btnuploanddata.Visibility = Visibility.Visible;
                        btnuploanddata.IsEnabled = false;
                        cbxmes_Copy.Visibility = Visibility.Visible;
                        cbxmes_Copy.IsEditable = false;
                        btnloandfile.IsEnabled = false;

                        Image myImage3 = new Image();
                        BitmapImage bi3 = new BitmapImage();
                        bi3.BeginInit();
                        bi3.UriSource = new Uri("https://i.ibb.co/tcxq6F0/iconout.png");
                        bi3.EndInit();
                        imgbtnlda.Stretch = Stretch.UniformToFill;
                        imgbtnlda.ImageSource = bi3;
                        btbviwfile.IsEnabled = false;
                        cbhojas.IsEnabled = false;

                        Image myImage1 = new Image();
                        BitmapImage bi1 = new BitmapImage();
                        bi1.BeginInit();
                        bi1.UriSource = new Uri("https://i.ibb.co/Fg98w3z/iconexcelnull.png");
                        bi1.EndInit();
                        imgexcel.Stretch = Stretch.Uniform;
                        imgexcel.Source = bi1;

                        lbloanddata.Content = "CANCELAR";
                        break;
                    }
                default:
                    {
                        loadfom();
                        lateraliztec.Visibility = Visibility.Visible;
                        Image myImage3 = new Image();
                        BitmapImage bi3 = new BitmapImage();
                        bi3.BeginInit();
                        bi3.UriSource = new Uri("https://i.ibb.co/Rp7J4H7/icondatacarga.png");
                        bi3.EndInit();
                        imgbtnlda.Stretch = Stretch.UniformToFill;
                        imgbtnlda.ImageSource = bi3;
                        gridpuntos.Visibility = Visibility.Visible;
                        btnuploanddata.Visibility = Visibility.Hidden;
                        lbloanddata.Content = "Subir data";
                        break;
                    }
            }
        }
        private void btnuploanddata_Click(object sender, RoutedEventArgs e)
        {
            if (rbloandpuntos.IsChecked == true)
            {
                string btn = Convert.ToString(MessageBox.Show("¿Esta seguro de subir informacion del mes de " + cbxmes_Copy.SelectedItem.ToString() + "?", "LoandData", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNoCancel));
                if (btn == "Yes")
                {

                    connection.Close();
                    connection.Open();
                    using (SqlBulkCopy s = new SqlBulkCopy(connection))
                    {

                        DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];
                        //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS
                        s.ColumnMappings.Add("CEDULA", "CEDULA");
                        s.ColumnMappings.Add("NOMBRE", "NOMBRE");
                        s.ColumnMappings.Add("PERIODO", "PERIODO");
                        s.ColumnMappings.Add("ID_ORDEN", "ID_ORDEN");
                        s.ColumnMappings.Add("PUNTOS", "PUNTOS");
                        s.ColumnMappings.Add("SUMA", "SUMA");
                        s.ColumnMappings.Add("NOTA_SUMA", "NOTA_SUMA");
                        s.ColumnMappings.Add("RESTA", "RESTA");
                        s.ColumnMappings.Add("NOTA_RESTA", "NOTA_RESTA");

                        //definimos la tabla a cargar
                        s.DestinationTableName = "puntos" + cbxmes_Copy.SelectedItem.ToString();

                        s.BulkCopyTimeout = 2500;
                        s.WriteToServer(data);

                        MessageBox.Show("Datos actualizados, sera redirigido a la vista principal");
                    }
                }
            }
            else if (rbloandexcelencia.IsChecked == true)
            {
                try
                {
                    string btn = Convert.ToString(MessageBox.Show("¿Esta seguro de subir informacion del mes de " + cbxmes_Copy.SelectedItem.ToString() + "?", "LoandData", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNoCancel));
                    if (btn == "Yes")
                    {
                        connection.Close();
                        connection.Open();

                        using (SqlBulkCopy s = new SqlBulkCopy(connection))
                        {

                            DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];
                            //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS                      
                            s.ColumnMappings.Add("CEDULA", "CEDULA");
                            s.ColumnMappings.Add("NOMBRE", "NOMBRE");
                            s.ColumnMappings.Add("SEMANA1", "SEMANA1");
                            s.ColumnMappings.Add("NOTA1", "NOTA1");
                            s.ColumnMappings.Add("Q7_1", "Q7_1");
                            s.ColumnMappings.Add("EFECTIVIDAD1", "EFECTIVIDAD1");
                            s.ColumnMappings.Add("CDH1", "CDH1");
                            s.ColumnMappings.Add("COMPLETADOS1", "COMPLETADOS1");
                            s.ColumnMappings.Add("CONSEJO1", "CONSEJO1");

                            s.ColumnMappings.Add("SEMANA2", "SEMANA2");
                            s.ColumnMappings.Add("NOTA2", "NOTA2");
                            s.ColumnMappings.Add("Q7_2", "Q7_2");
                            s.ColumnMappings.Add("EFECTIVIDAD2", "EFECTIVIDAD2");
                            s.ColumnMappings.Add("CDH2", "CDH2");
                            s.ColumnMappings.Add("COMPLETADOS2", "COMPLETADOS2");
                            s.ColumnMappings.Add("CONSEJO2", "CONSEJO2");

                            s.ColumnMappings.Add("SEMANA3", "SEMANA3");
                            s.ColumnMappings.Add("NOTA3", "NOTA3");
                            s.ColumnMappings.Add("Q7_3", "Q7_3");
                            s.ColumnMappings.Add("EFECTIVIDAD3", "EFECTIVIDAD3");
                            s.ColumnMappings.Add("CDH3", "CDH3");
                            s.ColumnMappings.Add("COMPLETADOS3", "COMPLETADOS3");
                            s.ColumnMappings.Add("CONSEJO3", "CONSEJO3");

                            s.ColumnMappings.Add("SEMANA4", "SEMANA4");
                            s.ColumnMappings.Add("NOTA4", "NOTA4");
                            s.ColumnMappings.Add("Q7_4", "Q7_4");
                            s.ColumnMappings.Add("EFECTIVIDAD4", "EFECTIVIDAD4");
                            s.ColumnMappings.Add("CDH4", "CDH4");
                            s.ColumnMappings.Add("COMPLETADOS4", "COMPLETADOS4");
                            s.ColumnMappings.Add("CONSEJO4", "CONSEJO4");

                            string cadena = "DELETE from excelencia" + cbxmes_Copy.SelectedItem.ToString() + "";
                            SqlCommand comando = new SqlCommand(cadena, connection);
                            comando.ExecuteNonQuery();

                            //definimos la tabla a cargar
                            s.DestinationTableName = "excelencia" + cbxmes_Copy.SelectedItem.ToString();

                            s.BulkCopyTimeout = 2500;
                            s.WriteToServer(data);

                            MessageBox.Show("Datos actualizados, sera redirigido a la vista principal");
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No fue posible subir documento" + ex, "Failed");
                }
            }
        }
        private void cbxmes_Copy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnuploanddata.IsEnabled = true;
        }
        private void rbloandexcelencia_Checked(object sender, RoutedEventArgs e)
        {
            string mg = Convert.ToString(MessageBox.Show("Si esta subiendo segundo, tercero o cuarto corte debe tener las plantillas anteriores ¿Lo tiene?\nSi: continuar\nNo:Descargar anterior?", "Para recordar", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNo));
            if (mg == "Yes")
            {
                btnloandfile.IsEnabled = true;
            }
            else if (mg == "No")
            {
                Selectfileexcelencia popup = new Selectfileexcelencia();
                rbloandexcelencia.IsChecked = false;
                popup.ShowDialog();
            }

        }
        private void cbhojas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
        private void rbloandpuntos_Checked(object sender, RoutedEventArgs e)
        {
            btnloandfile.IsEnabled = true;
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Operación operación = new Operación();
            operación.Show();
            this.Close();
        }
    }
}
