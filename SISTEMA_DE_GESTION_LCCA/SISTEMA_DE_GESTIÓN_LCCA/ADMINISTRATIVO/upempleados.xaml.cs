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

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para upempleados.xaml
    /// </summary>
    public partial class upempleados : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        private DataSet dtsTablas = new DataSet();
        public upempleados()
        {
            InitializeComponent();
            btnuploanddata.IsEnabled = false;
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
                string btn = Convert.ToString(MessageBox.Show("¿Esta seguro que desea subir esta información?", "LoandData", (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.YesNoCancel));
                if (btn == "Yes")
                {
                    connection.Close();
                    connection.Open();

                    using (SqlBulkCopy s = new SqlBulkCopy(connection))
                    {

                        DataTable data = dtsTablas.Tables[cbhojas.SelectedIndex];
                        //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS                      
                        s.ColumnMappings.Add("IDENTIFICACION", "IDENTIFICACION");
                        s.ColumnMappings.Add("NOMBRE", "NOMBRE");
                        s.ColumnMappings.Add("ESTADO_EMPLEADO", "ESTADO_EMPLEADO");
                        s.ColumnMappings.Add("FECHA_INGRESO", "FECHA_INGRESO");
                        s.ColumnMappings.Add("PUESTO", "PUESTO");
                        s.ColumnMappings.Add("ESTADO_PROYECTO", "ESTADO_PROYECTO");
                        s.ColumnMappings.Add("DIRECCION_HAB", "DIRECCION_HAB");
                        s.ColumnMappings.Add("TELEFONO1", "TELEFONO1");
                        s.ColumnMappings.Add("PRIMER_APELLIDO", "PRIMER_APELLIDO");
                        s.ColumnMappings.Add("SEGUNDO_APELLIDO", "SEGUNDO_APELLIDO");
                        s.ColumnMappings.Add("NOMBRE_PILA", "NOMBRE_PILA");
                        s.ColumnMappings.Add("E_MAIL", "E_MAIL");
                        s.ColumnMappings.Add("U_TALLA_CAMISA", "U_TALLA_CAMISA");
                        s.ColumnMappings.Add("U_TALLA_PANTALON", "U_TALLA_PANTALON");
                        s.ColumnMappings.Add("U_TALLA_ZAPATOS", "U_TALLA_ZAPATOS");
                        s.ColumnMappings.Add("U_TALLA_CHALECO", "U_TALLA_CHALECO");
                        s.ColumnMappings.Add("U_TALLA_DELANTAL", "U_TALLA_DELANTAL");

                        //definimos la tabla a cargar
                        s.DestinationTableName = "EMPLEADOS_PPAL";

                        s.BulkCopyTimeout = 2500;
                        s.WriteToServer(data);

                        MessageBox.Show("Datos actualizados");
                        InitializeComponent();
                    }
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                MessageBox.Show("Por favor verifique el archivo, no puede tener espacios en identificación");
            }
        }
        private void cbhojas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnhome_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
