using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para IngresoMaterial.xaml
    /// </summary>
    public partial class IngresoMaterial : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        DataTable dt = new DataTable();
        public IngresoMaterial()
        {
            InitializeComponent();
            iniciar();
        }
        public void iniciar()
        {
            try
            {
                txtubicacion.IsEnabled = false;
                txtmaterial.IsEnabled = false;
                cbxlote.IsEnabled = false;
                connection.Close();
                connection.Open();
                //MUESTRA DATOS EN GREADVIEW
                SqlCommand command1 = new SqlCommand("Select * from tbbodega_ppal WHERE CANTIDAD > 0", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command1;
                adapter.Fill(dt);
                gvBodegaPpal.ItemsSource = dt.DefaultView;
                connection.Close();
                MessageBox.Show("Base de datos actualizada");
            }
            catch (Exception)
            {

                MessageBox.Show("No es posible acceder a la base de datos");
            }
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnexportar_Click(object sender, RoutedEventArgs e)
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void btnnewcodigo_Click(object sender, RoutedEventArgs e)
        {
            if (ClaseCompartida.usuario == "alejandra.rojas" || ClaseCompartida.usuario == "alexander.peralta")
            {
                Newcodigo form1 = new Newcodigo();
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene persimos para crear códigos");
            }
        }
        private void btntrl_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            connection.Open();
            //MUESTRA DATOS EN GREADVIEW
            SqlCommand command1 = new SqlCommand("Select * from tbbodega_trl", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command1;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gvBodegaPpal.ItemsSource = dataTable.DefaultView;
            //FIN
            connection.Close();
            MessageBox.Show("Base de datos actualizada");
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtcantidad.Clear();
            txtitem.Clear();

            txtmaterial.Clear();
            txttls.Clear();
            txtubicacion.Clear();
        }
        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BUSCAR EL ITEM PARA COMPARARLO EN EL SIGUIENTE IF
                connection.Close();
                connection.Open();
                string busqueda_item = "";
                SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item", connection); //CONSULTA SQL
                command_buscar.Parameters.AddWithValue("@item", txtitem.Text);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    busqueda_item = Convert.ToString(reader_buscar["ITEM"]); //Almacena ese itemuna variable no definida               

                }
                //COMPRUEBA QUE ITEM NO EXISTA EN TABLA, SI EXISTE SUMA CANTIDAD SI NO LO CREA.
                if (txtitem.Text == busqueda_item)
                {
                    txtmaterial.Text = Convert.ToString(reader_buscar["MATERIAL"]);
                    txtubicacion.Text = Convert.ToString(reader_buscar["UBICACIÓN"]);
                    txtcantidad.Text = Convert.ToString(reader_buscar["CANTIDAD"]);
                    cbxlote.Text = Convert.ToString(reader_buscar["LOTE"]);
                    txttls.Text = Convert.ToString(reader_buscar["TLS"]);
                }
                else
                {
                    MessageBox.Show("ITEM no existe");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo fallo, por favor intente de nuevo. No se realizo ningun cambio." + Convert.ToString(ex));
            }
        }
        private void txtitem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void gvBodegaPpal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void gvBodegaPpal_CurrentCellChanged(object sender, EventArgs e)
        {

        }
        private void btnguardar_Click(object sender, RoutedEventArgs e)
        {


            //try
            //{
            connection.Open();
            //BUSCAR EL ITEM PARA COMPARARLO EN EL SIGUIENTE IF

            string busqueda_item = "";
            SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item", connection);
            command_buscar.Parameters.AddWithValue("@item", Convert.ToString(txtitem.Text));
            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
            if (reader_buscar.Read())
            {
                busqueda_item = Convert.ToString(reader_buscar["ITEM"]); //Almacena ese item EN una variable                    
            }
            //SUMA CANTIDAD EN CASO DE EXISTENCIA
            if (txtitem.Text == busqueda_item)
            {
                cbxlote.Text = Convert.ToString(reader_buscar["LOTE"]);
                txtmaterial.Text = Convert.ToString(reader_buscar["MATERIAL"]);
                txtubicacion.Text = Convert.ToString(reader_buscar["UBICACIÓN"]);
                //cbxlote.Text = Convert.ToString(reader_buscar["LOTE"]);
                //txttls.Text = Convert.ToString(reader_buscar["TLS"]);
                int cant_anterior = Convert.ToInt32(reader_buscar["CANTIDAD"]);
                int suma_can_nueva = cant_anterior + int.Parse(txtcantidad.Text);

                string ubicacion = Convert.ToString(reader_buscar["UBICACIÓN"]);
                if (ubicacion == "")
                {

                    ubicacion = Microsoft.VisualBasic.Interaction.InputBox("Ingrese ubicación del ITEM ", "Registro ubicación", "", 100, 0);
                    reader_buscar.Close();
                    string queryA = "UPDATE tbbodega_ppal SET UBICACIÓN = '" + ubicacion + "' Where ITEM = '" + txtitem.Text + "'";
                    SqlCommand command_updateA = new SqlCommand(queryA, connection);
                    command_updateA.Parameters.AddWithValue("@CANTIDAD", Convert.ToString(txtitem.Text));
                    command_updateA.ExecuteNonQuery();
                    txtubicacion.Text = ubicacion;

                    string query = "UPDATE tbbodega_ppal SET CANTIDAD = '" + suma_can_nueva + "' Where ITEM = '" + txtitem.Text + "'";
                    SqlCommand command_update = new SqlCommand(query, connection);
                    command_update.Parameters.AddWithValue("@CANTIDAD", Convert.ToString(txtitem.Text));
                    command_update.ExecuteNonQuery();

                    string querytrl = "INSERT tbbodega_trl(ITEM, MATERIAL, CANTIDAD, UBICACIÓN, LOTE, TRL, FECHA_DE_INGRESO ) VALUES(@item, @material, @cantidad, @ubicación, @lote, @trl, @fecha_de_ingreso)";
                    SqlCommand command = new SqlCommand(querytrl, connection);
                    command.Parameters.AddWithValue("@item", txtitem.Text);
                    command.Parameters.AddWithValue("@material", txtmaterial.Text);
                    command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtcantidad.Text));
                    command.Parameters.AddWithValue("@ubicación", txtubicacion.Text);
                    command.Parameters.AddWithValue("@lote", cbxlote.Text);
                    command.Parameters.AddWithValue("@trl", txttls.Text);
                    command.Parameters.AddWithValue("@fecha_de_ingreso", fechatime.Text + " | " + ClaseCompartida.usuario);
                    command.ExecuteNonQuery();

                    connection.Close();

                    //MUESTRA DATOS EN GREADVIEW
                    SqlCommand command1 = new SqlCommand("Select * from tbbodega_ppal", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command1;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvBodegaPpal.ItemsSource = dataTable.DefaultView;
                    //FIN

                    txtcantidad.Clear();
                    txtitem.Clear();
                    txtubicacion.Clear();
                    txtmaterial.Clear();


                    MessageBox.Show("El elemento existe, se le han sumado " + txtcantidad.Text + " cantidades");


                }
                else
                {
                    reader_buscar.Close();
                    string query = "UPDATE tbbodega_ppal SET CANTIDAD = '" + suma_can_nueva + "' Where ITEM = '" + txtitem.Text + "'";
                    SqlCommand command_update = new SqlCommand(query, connection);
                    command_update.Parameters.AddWithValue("@CANTIDAD", Convert.ToString(txtitem.Text));
                    command_update.ExecuteNonQuery();


                    string querytrl = "INSERT tbbodega_trl(ITEM, MATERIAL, CANTIDAD, UBICACIÓN, LOTE, TRL, FECHA_DE_INGRESO ) VALUES(@item, @material, @cantidad, @ubicación, @lote, @trl, @fecha_de_ingreso)";
                    SqlCommand command = new SqlCommand(querytrl, connection);
                    command.Parameters.AddWithValue("@item", txtitem.Text);
                    command.Parameters.AddWithValue("@material", txtmaterial.Text);
                    command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtcantidad.Text));
                    command.Parameters.AddWithValue("@ubicación", txtubicacion.Text);
                    command.Parameters.AddWithValue("@lote", cbxlote.Text);
                    command.Parameters.AddWithValue("@trl", txttls.Text);
                    command.Parameters.AddWithValue("@fecha_de_ingreso", fechatime.Text + " | " + ClaseCompartida.usuario);
                    command.ExecuteNonQuery();

                    connection.Close();
                    //MUESTRA DATOS EN GREADVIEW
                    SqlCommand command1 = new SqlCommand("Select * from tbbodega_ppal", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command1;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvBodegaPpal.ItemsSource = dataTable.DefaultView;
                    //FIN

                    txtcantidad.Clear();
                    txtitem.Clear();
                    txtubicacion.Clear();
                    txtmaterial.Clear();
                    MessageBox.Show("El elemento existe, se le han sumado " + txtcantidad.Text + " cantidades");
                }

            }
            //CREA ITEM NUEVO POR NO EXISTIR
            else
            {
                MessageBox.Show("Código no registrado, por favor valide");
            }
            connection.Close();
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
