using ExcelDataReader;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Cuentascobro.xaml
    /// </summary>
    public partial class Cuentascobro : Window
    {
        SqlConnection connection = new SqlConnection("Data Source = 190.145.38.162; Initial Catalog = BDcuentascobro;User ID=analista;Password=claro4n4l1sta");
        private DataSet dtsTablas = new DataSet();
        string proyecto = "";
        double valorp = 0;
        public Cuentascobro()
        {
            InitializeComponent();
            btnok.IsEnabled = false;
            footer_Copy.IsEnabled = false;
            cbxproyecto.IsEnabled = false;
            txtperiodo.IsEnabled = false;
            dgviewfile.Visibility = Visibility.Hidden;
            barr.Visibility = Visibility.Hidden;
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
                    // btnuploanddata.IsEnabled = false;
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
            cbxproyecto.IsEnabled = true;
            footer_Copy.IsEnabled = true;

            try
            {
                connection.Close();
                connection.Open();

                string cadena = "delete from tablappal";
                SqlCommand comando = new SqlCommand(cadena, connection);
                comando.ExecuteNonQuery();


                using (SqlBulkCopy s = new SqlBulkCopy(connection))
                {

                    DataTable dt = dtsTablas.Tables[cbhojas.SelectedIndex];

                    //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS
                    s.ColumnMappings.Add(Convert.ToString("ID"), "ID");
                    s.ColumnMappings.Add(Convert.ToString("CEDULA"), "CEDULA");
                    s.ColumnMappings.Add("NOMBRE", "NOMBRE");
                    s.ColumnMappings.Add("DIRECCIÓN", "DIRECCIÓN");
                    s.ColumnMappings.Add("NUMERO_CUENTA", "NUMERO_CUENTA");
                    s.ColumnMappings.Add("CIUDAD", "CIUDAD");
                    s.ColumnMappings.Add("CELULAR", "CELULAR");
                    s.ColumnMappings.Add("CENTROCOS", "CENTROCOS");
                    s.ColumnMappings.Add("1", "1"); s.ColumnMappings.Add("2", "2"); s.ColumnMappings.Add("3", "3"); s.ColumnMappings.Add("4", "4"); s.ColumnMappings.Add("5", "5");
                    s.ColumnMappings.Add("6", "6"); s.ColumnMappings.Add("7", "7"); s.ColumnMappings.Add("8", "8"); s.ColumnMappings.Add("9", "9"); s.ColumnMappings.Add("10", "10");
                    s.ColumnMappings.Add("11", "11"); s.ColumnMappings.Add("12", "12"); s.ColumnMappings.Add("13", "13"); s.ColumnMappings.Add("14", "14"); s.ColumnMappings.Add("15", "15");
                    s.ColumnMappings.Add("16", "16"); s.ColumnMappings.Add("17", "17"); s.ColumnMappings.Add("18", "18"); s.ColumnMappings.Add("19", "19"); s.ColumnMappings.Add("20", "20");
                    s.ColumnMappings.Add("21", "21"); s.ColumnMappings.Add("22", "22"); s.ColumnMappings.Add("23", "23"); s.ColumnMappings.Add("24", "24"); s.ColumnMappings.Add("25", "25");
                    s.ColumnMappings.Add("26", "26"); s.ColumnMappings.Add("27", "27"); s.ColumnMappings.Add("28", "28"); s.ColumnMappings.Add("29", "29"); s.ColumnMappings.Add("30", "30");


                    //definimos la tabla a cargar
                    s.DestinationTableName = "tablappal";
                    s.BulkCopyTimeout = 100000;
                    s.WriteToServer(dt);
                    MessageBox.Show("Datos ingresados, proceda a generar PDF's");
                    footer_Copy.IsEnabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor verifique excel");
                footer_Copy.IsEnabled = false;
            }


        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                barr.Visibility = Visibility.Visible;
                dgviewfile.Visibility = Visibility.Hidden;

                if (cbxproyecto.Text == "BANCA")
                {
                    proyecto = "preciobanca";
                    valorp = 12567;
                }
                else if (cbxproyecto.Text == "CLARO")
                {
                    proyecto = "precioclaro";
                    valorp = 14818.6666666667;
                }

                string a = txtperiodo.Text;
                if (a.Length > 8)
                {
                    string fecha = DateTime.Now.Date.ToString("dd/MM/yyyy");

                    int limite = 500;
                    int id = 0;
                    int idbarr = 0;
                    string periodo = txtperiodo.Text;
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        for (int i = 0; i < limite + 1; i++)
                        {
                            this.barraprogress.Dispatcher.BeginInvoke((ThreadStart)delegate { this.barraprogress.Value = i; });
                            this.barraprogress.Dispatcher.BeginInvoke((ThreadStart)delegate { lbcuentanum.Content = "CUENTA #" + (idbarr + 1); });

                            SqlCommand comando = new SqlCommand("Select * from tablappal Where ID = @id", connection);
                            comando.Parameters.AddWithValue("@id", id);
                            connection.Close();
                            connection.Open();
                            SqlDataReader reader = comando.ExecuteReader();
                            if (reader.Read())
                            {
                                idbarr++;
                                //obtengo la BD
                                string id1 = reader["ID"].ToString();
                                string nombre = reader["NOMBRE"].ToString();
                                string cedula = reader["CEDULA"].ToString();
                                string direccion = reader["DIRECCIÓN"].ToString();
                                string cuenta = reader["NUMERO_CUENTA"].ToString();
                                string celular = reader["CELULAR"].ToString();
                                string centrocosto = reader["CENTROCOS"].ToString();
                                string ciudad = reader["CIUDAD"].ToString();
                                //Hace cuentas de dias

                                int dia = 1;
                                int multiplo = 0;
                                double valor = 0;
                                for (int l = 0; l < 30; l++)
                                {
                                    string suma = reader["" + dia + ""].ToString();
                                    if (suma == "0")
                                    {
                                        valor += 0;
                                    }
                                    else
                                    {
                                        int s = Convert.ToInt32(suma);
                                        valor += valorp * s;
                                        multiplo += s;
                                    }

                                    dia++;
                                }
                                string valor_t = String.Format("{0:n}", valor);
                                reader.Close();

                                //SqlCommand comando1 = new SqlCommand("Select * from " + proyecto + " Where ID = @id1", connection);
                                //comando1.Parameters.AddWithValue("@id1", multiplo);
                                //SqlDataReader reader1 = comando1.ExecuteReader();
                                //if (reader1.Read())
                                //{
                                //    valorletra = reader1["LETRA"].ToString();
                                //}
                                double RETENCION = (valor * 4) / 100;
                                double SINRETE = valor - RETENCION;
                                string valorletra = Convert.ToDecimal(SINRETE).NumeroALetras();
                                string plantillacarp = @"C:\ExportPDF_Cuentas_Cobro\";
                                string plantillaPDF = @"cuenta_cobro.pdf";
                                string nuevoPDF = @"" + cuenta + ".pdf";    //nombre del nuevo PDF
                                string doc = plantillacarp + plantillaPDF;
                                string ubicacion = plantillacarp + nuevoPDF; // ubicación del archivo
                                using (var exsistencia = new FileStream(doc, FileMode.Open))
                                using (var archivo = new FileStream(ubicacion, FileMode.Create))
                                {
                                    var pdfplantilla_leer = new PdfReader(exsistencia);

                                    var pdfstamper = new PdfStamper(pdfplantilla_leer, archivo);

                                    AcroFields fields = pdfstamper.AcroFields;

                                    fields.SetField("FECHA", fecha);
                                    fields.SetField("NUMERO COBRO", cuenta);
                                    fields.SetField("A NOMBRE DE", nombre);
                                    fields.SetField("CEDULA 1", cedula);
                                    fields.SetField("VALOR_NUM", valor_t);
                                    fields.SetField("VALOR_LETRA", valorletra);
                                    fields.SetField("Texto1", periodo);
                                    fields.SetField("NOMBRE2", nombre);
                                    fields.SetField("DIRECCION", direccion);
                                    fields.SetField("CIUDAD", ciudad);
                                    fields.SetField("CELULAR", celular);
                                    fields.SetField("CENTROCOS", centrocosto);
                                    fields.SetField("RETENCION", String.Format("{0:n}", RETENCION));
                                    fields.SetField("SINRETE", String.Format("{0:n}", SINRETE));
                                    fields.SetField("VALOR_PAGAR", String.Format("{0:n}", SINRETE));


                                    pdfstamper.FormFlattening = true;

                                    pdfstamper.Close();
                                    pdfplantilla_leer.Close();
                                }
                            }
                            id++;

                            connection.Close();

                        }
                        MessageBox.Show("CUENTAS DE COBRO GENERADAS\n" + idbarr + " CUENTAS GENERADAS");
                        System.Diagnostics.Process.Start(@"C:\ExportPDF_Cuentas_Cobro\");
                        connection.Close();

                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            Menuingresos me = new Menuingresos();
                            me.Show();
                            this.Close();
                        });
                    }));
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Por favor revise el período a facturar");
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Por favor verifique la plantilla y ubicación de las cuentas a generar" + EX);
            }
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnok.IsEnabled = true;
            txtperiodo.IsEnabled = true;
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Menuingresos me = new Menuingresos();
            me.Show();
            this.Close();
        }
        private void btnhome_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cbhojas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
