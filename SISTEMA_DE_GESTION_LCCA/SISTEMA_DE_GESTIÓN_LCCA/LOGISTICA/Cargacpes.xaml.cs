using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para Cargacpes.xaml
    /// </summary>
    public partial class Cargacpes : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        private DataSet dtsTablas = new DataSet();
        public Cargacpes()
        {
            InitializeComponent();
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
        private void btnuploanddata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Listado> _lista = new List<Listado>();

                int flag = 0;
                connection.Close();
                connection.Open();
                string cadena = "delete from tbcpesrepetidos";
                SqlCommand comando = new SqlCommand(cadena, connection);
                comando.ExecuteNonQuery();
                using (SqlBulkCopy s = new SqlBulkCopy(connection))
                {

                    DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];

                    //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS

                    s.ColumnMappings.Add("SERIAL", "SERIAL");


                    //definimos la tabla a cargar
                    s.DestinationTableName = "tbcpesrepetidos";

                    s.BulkCopyTimeout = 2500;
                    s.WriteToServer(data);
                }
                SqlCommand command2 = new SqlCommand("Select * from tbcpesrepetidos", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string cpe = Convert.ToString(row["SERIAL"]);

                    SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @SERIAL", connection); //CONSULTA SQL
                    command_buscar.Parameters.AddWithValue("@SERIAL", cpe);
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        flag += 1;
                    }
                    reader_buscar.Close();
                }
                if (flag == 0)
                {
                    cargacpe();
                }
                else
                {
                    MessageBox.Show(flag + " seriales de los que intenta ingresar ya estan registrados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algó paso, por favor verifica de nuevo" + ex.Message);
            }
        }
        public void cargacpe()
        {
            connection.Close();
            connection.Open();
            using (SqlBulkCopy s = new SqlBulkCopy(connection))
            {

                DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];
                //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS
                s.ColumnMappings.Add("CODIGO", "CODIGO");
                s.ColumnMappings.Add("DESCRIPCIÓN", "DESCRIPCIÓN");
                s.ColumnMappings.Add("SERIAL", "SERIAL");
                s.ColumnMappings.Add("ESTADO", "ESTADO");
                s.ColumnMappings.Add("LUGAR_ASIGNACIÓN", "LUGAR_ASIGNACIÓN");
                s.ColumnMappings.Add("ID_LUGAR", "ID_LUGAR");

                //definimos la tabla a cargar
                s.DestinationTableName = "tbbodega_cpesppal";

                s.BulkCopyTimeout = 2500;
                s.WriteToServer(data);

                MessageBox.Show("Datos ingresados, cierre y actualicé modulo CPES");
                this.Close();

            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
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
