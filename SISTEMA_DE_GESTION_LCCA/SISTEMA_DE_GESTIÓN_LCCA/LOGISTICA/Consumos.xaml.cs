using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;
using DataTable = System.Data.DataTable;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para Consumos.xaml
    /// </summary>
    public partial class Consumos : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        string busqueda = "CÉDULA";
        DataTable dt = new DataTable();
        int flag = 0;
        string[] itemsname = new string[300];
        string[] itemscod = new string[300];
        string filtro = "";
        string tipofiltro = "";
        string fechadel = "";
        string fechaal = "";
        public static string serialcpe;

        public Consumos()
        {
            try
            {
                InitializeComponent();
                rdporfiltro.IsChecked = true;
                cnfiltrofecha.Visibility = Visibility.Hidden;
                cnbusqueda.Visibility = Visibility.Visible;
                busquedaitems.Visibility = Visibility.Visible;
                progress.Visibility = Visibility.Hidden;
                SqlCommand command2 = new SqlCommand("Select * from tbbodega_ppal", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                int cont = 0;
                foreach (DataRow row in dataTable2.Rows)
                {
                    collection.Add(Convert.ToString(row["ITEM"]));
                    itemsname[cont] = Convert.ToString(row["MATERIAL"]);
                    itemscod[cont] = Convert.ToString(row["ITEM"]);
                    cont++;
                }
                //CBX1
                cbx1.ItemsSource = dataTable2.DefaultView;
                cbx1.DisplayMemberPath = "ID";
                cbx1.DisplayMemberPath = "ITEM";
                reloand();
                cbx1.SelectedIndex = 1;
            }
            catch (Exception)
            {

            }
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        } //Este evento procede con la busqueda de consumos
        private void btnexportar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {

                    if (flag == 1)
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            busquedaitems.Visibility = Visibility.Hidden;
                            progress.Visibility = Visibility.Visible;
                            btnbuscartec1.IsEnabled = false;
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

                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            busquedaitems.Visibility = Visibility.Hidden;
                            progress.Visibility = Visibility.Visible;
                            btnbuscartec1.IsEnabled = false;
                            tipofiltro = cbxfiltrofehca.Text;
                            fechadel = datedel.Text;
                            fechaal = dateal.Text;
                        });
                        connection.Close();
                        connection.Open();
                        if (string.IsNullOrEmpty(fechadel) || string.IsNullOrEmpty(fechaal) || string.IsNullOrEmpty(tipofiltro))
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                MessageBox.Show("Verifique las reglas del filtro");
                            });
                        }
                        else
                        {

                            DateTime fechaUno = Convert.ToDateTime(fechaal);
                            DateTime fechaDos = Convert.ToDateTime(fechadel);
                            TimeSpan difFechas = fechaUno - fechaDos;
                            if (difFechas.Days >= 0)
                            {
                                SqlCommand command1 = new SqlCommand("select * from tbbodega_consumo where " + filtro + " >=  '" + fechadel + "' AND FECHA_ACTIVIDAD <= '" + fechaal + "'", connection);

                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command1;
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dt);




                                if (dt == null || dt.Columns.Count == 0)
                                    throw new Exception("ExportToExcel: Null or empty input table!\n");
                                // load Excel, and create a new workbook
                                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                                excelApp.Workbooks.Add();
                                // single worksheet
                                Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelApp.ActiveSheet;
                                // column headings
                                this.barraprogress.Dispatcher.BeginInvoke((ThreadStart)delegate { this.barraprogress.Value = 0; });
                                for (var i = 0; i < dt.Columns.Count; i++)
                                {
                                    workSheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                                }
                                double cont = dt.Rows.Count;
                                double barr = 100 / cont;
                                for (var i = 0; i < cont; i++)
                                {
                                    // to do: format datetime values before printing
                                    for (var j = 0; j < dt.Columns.Count; j++)
                                    {
                                        workSheet.Cells[i + 2, j + 1] = "'" + dt.Rows[i][j];
                                    }
                                    this.barraprogress.Dispatcher.BeginInvoke((ThreadStart)delegate { this.barraprogress.Value += barr; });
                                }
                                MessageBox.Show("Documento exportardo, recuerde guardarlo");

                                excelApp.Visible = true;
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    MessageBox.Show("Las fechas deben ser del pasado al presente, por favor verifique");
                                });
                            }
                        }
                    }
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        busquedaitems.Visibility = Visibility.Visible;
                        progress.Visibility = Visibility.Hidden;
                        btnbuscartec1.IsEnabled = true;
                        btnexportar.IsEnabled = true;
                    });
                }));
                thread.Start();
            }
            catch (Exception)
            {

            }
        }
        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            buscartec();
        }
        public void buscartec()
        {
            try
            {
                if (string.IsNullOrEmpty(txtnumdoc.Text))
                {
                    MessageBox.Show("Por favor introduzca cédula.");
                    reloand();
                }
                else
                {
                    connection.Close();
                    connection.Open();

                    // MUESTRA DATOS EN GREADVIEW
                    string cedula = Convert.ToString(txtnumdoc.Text);
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @id", connection); //CONSULTA SQL
                    command_buscar.Parameters.AddWithValue("@id", cedula);
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        lbname.Content = Convert.ToString(reader_buscar["MATERIAL"]);
                        ClaseTecnico.nombre = Convert.ToString(lbname.Content);
                        ClaseTecnico.cedula = Convert.ToString(txtnumdoc.Text);
                    }
                    reader_buscar.Close();
                    MessageBox.Show("Base de datos actualizada");
                    cbx1.SelectedIndex = 1;
                    cbx1.IsEnabled = false;
                    materiales.Visibility = Visibility.Hidden;
                    Mcpes.Visibility = Visibility.Hidden;
                    cbx1.SelectedIndex = 0;
                    txtcuenta.IsEnabled = true;
                    txtnodo.IsEnabled = true;
                    txtot.IsEnabled = true;
                    txttipot.IsEnabled = true;
                    btnregistrarot.IsEnabled = true;
                    fechaactividad.IsEnabled = true;
                    checkBox1.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario no registrado.");
                lbname.Content = "";
                lbname.Content = "";
                gvBodegaConsumible.ItemsSource = null;
                materiales.Visibility = Visibility.Hidden;
                Mcpes.Visibility = Visibility.Hidden;
                cbx1.IsEnabled = false;
                radioButton1.IsChecked = false;
                radioButton2.IsChecked = false;
                reloand();
            }
        }
        private void rbmaterial_Checked(object sender, RoutedEventArgs e)
        {
            materiales.Visibility = Visibility.Visible;
            Mcpes.Visibility = Visibility.Hidden;
            btnregistromat.IsDefault = true;
            if (radioButton1.IsChecked == true)
            {
                gvBodegaConsumible.Columns.Clear();
                cbx1.IsEnabled = true;
                txtcan1.IsEnabled = true;
                btnregistromat.IsEnabled = true;
                lb1.IsEnabled = true;
            }
            else
            {
                cbx1.IsEnabled = false;
                txtcan1.IsEnabled = false;
                btnregistromat.IsEnabled = false;
                lb1.IsEnabled = false;
            }
        }
        private void rbcpe_Checked(object sender, RoutedEventArgs e)
        {
            materiales.Visibility = Visibility.Hidden;
            Mcpes.Visibility = Visibility.Visible;
            btnasignarcpe.IsDefault = true;

            if (radioButton1.IsChecked == true)
            {
                txtserial.IsEnabled = false;
                btnasignarcpe.IsEnabled = false;
            }
            else
            {
                txtserial.IsEnabled = true;
                btnasignarcpe.IsEnabled = true;
            }
        }
        private void reloand()
        {
            materiales.Visibility = Visibility.Hidden;
            Mcpes.Visibility = Visibility.Hidden;
            btnasignarcpe.IsEnabled = false;
            btnregistromat.IsEnabled = false;
            txtserial.IsEnabled = false;
            txtcan1.IsEnabled = false;
            radioButton1.IsEnabled = false;
            radioButton2.IsEnabled = false;
            fechaactividad.IsEnabled = false;
            txtcuenta.IsEnabled = false;
            txtnodo.IsEnabled = false;
            txtot.IsEnabled = false;
            txttipot.IsEnabled = false;
            btnregistrarot.IsEnabled = false;
            btnnuevaot.IsEnabled = false;
            cbx1.IsEnabled = false;
            checkBox1.IsEnabled = false;
        }
        private void btnregistrarot_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtcuenta.Text) || string.IsNullOrEmpty(txtnodo.Text) || string.IsNullOrEmpty(txtot.Text) || string.IsNullOrEmpty(txttipot.Text))
            {
                MessageBox.Show("Ningun campo puede estar vacio");
            }
            else
            {
                radioButton1.IsEnabled = true;
                radioButton2.IsEnabled = true;
                btnregistrarot.IsEnabled = false;
                fechaactividad.IsEnabled = false;
                txtcuenta.IsEnabled = false;
                txtnodo.IsEnabled = false;
                txtot.IsEnabled = false;
                txttipot.IsEnabled = false;
                btnnuevaot.IsEnabled = true;
                btnbuscartec1.IsEnabled = false;
                fechaactividad.IsEnabled = false;
                checkBox1.IsEnabled = true;
            }
        }
        private void btnnuevaot_Click(object sender, RoutedEventArgs e)
        {
            gvBodegaConsumible.Columns.Clear();
            txtnumdoc.Clear();
            lbname.Content = "";
            btnbuscartec1.IsEnabled = true;
            txtcuenta.Clear();
            txtot.Clear();
            txtnodo.Clear();
            txttipot.Clear();
            radioButton1.IsChecked = false;
            radioButton2.IsChecked = false;
            radioButton1.IsEnabled = false;
            radioButton2.IsEnabled = false;
            btnnuevaot.IsEnabled = false;
            fechaactividad.IsEnabled = false;
            reloand();
        }
        private void btnregistromat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcan1.Text))
                {
                    MessageBox.Show("Ingrese cantidad");
                }
                else
                {
                    //BUSCAR EL ITEM  EN BODEGA TECNICO PARA COMPARARLO EN EL SIGUIENTE IF
                    connection.Close();
                    connection.Open();
                    string cedula = txtnumdoc.Text;
                    string busqueda_item = "";
                    int cantidad_item_tbtecnico = 0;
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @item", connection);
                    command_buscar.Parameters.AddWithValue("@item", Convert.ToString(cbx1.Text));
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        busqueda_item = Convert.ToString(reader_buscar["ITEM"]); //Almacena ese item EN una variable
                        cantidad_item_tbtecnico = Convert.ToInt32(reader_buscar["CANTIDAD"]);
                        reader_buscar.Close();
                        if (cbx1.Text == busqueda_item)
                        {
                            int cantidad_item_consoli = 0;
                            SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_consolidado Where ITEM = @item2", connection);
                            command_buscar2.Parameters.AddWithValue("@item2", cbx1.Text);
                            SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                            if (reader_buscar2.Read())
                            {
                                cantidad_item_consoli = Convert.ToInt32(reader_buscar2["CANTIDAD"]);

                                int nueva_can_consoli = cantidad_item_consoli - Convert.ToInt32(txtcan1.Text);
                                int nueva_can_tecni = cantidad_item_tbtecnico - Convert.ToInt32(txtcan1.Text);
                                if (nueva_can_consoli >= 0 && nueva_can_tecni >= 0)
                                {

                                    //Registra material en consumible
                                    reader_buscar2.Close();
                                    string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
                                    string query = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD, FECHA_REGISTRO, DIGITADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad, @fecha_registro, @digitador_por)";
                                    SqlCommand command = new SqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@cédula", txtnumdoc.Text);
                                    command.Parameters.AddWithValue("@nombre", lbname.Content);
                                    command.Parameters.AddWithValue("@cuenta_consumo", txtcuenta.Text);
                                    command.Parameters.AddWithValue("@ot", txtot.Text);
                                    command.Parameters.AddWithValue("@nodo", txtnodo.Text);
                                    command.Parameters.AddWithValue("@tipo_trabajo", txttipot.Text);
                                    command.Parameters.AddWithValue("@item", cbx1.Text);
                                    command.Parameters.AddWithValue("@material", lb1.Content);
                                    command.Parameters.AddWithValue("@cantidad", txtcan1.Text);
                                    command.Parameters.AddWithValue("@fecha_actividad", fechaactividad.Text);
                                    command.Parameters.AddWithValue("@fecha_registro", dateTime);
                                    command.Parameters.AddWithValue("@digitador_por", ClaseCompartida.nombre);
                                    command.ExecuteNonQuery();

                                    //Resta cantidad en BD_Consolidado                                    
                                    String query2 = "UPDATE tbbodega_consolidado SET CANTIDAD = CANTIDAD - "+Convert.ToInt32(txtcan1.Text)+" Where ITEM ='" + cbx1.Text + "'";
                                    SqlCommand command_update2 = new SqlCommand(query2, connection);
                                    command_update2.Parameters.AddWithValue("ITEM", "CANTIDAD - " + Convert.ToInt32(txtcan1.Text) + "");
                                    command_update2.ExecuteNonQuery();


                                    //Resta cantidad en bodega tecnico
                                    String queryup = "UPDATE tabla" + cedula + " SET CANTIDAD = CANTIDAD - " + Convert.ToInt32(txtcan1.Text) + " Where ITEM ='" + cbx1.Text + "'";
                                    SqlCommand command_updateup = new SqlCommand(queryup, connection);
                                    command_updateup.Parameters.AddWithValue("ITEM", "CANTIDAD - " + Convert.ToInt32(txtcan1.Text) + "");
                                    command_updateup.ExecuteNonQuery();


                                    string cadena = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                                    sqlCommand.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                    sqlCommand.Parameters.AddWithValue("@tecnico", lbname.Content);
                                    sqlCommand.Parameters.AddWithValue("@item", busqueda_item);
                                    sqlCommand.Parameters.AddWithValue("@material", lb1.Content);
                                    sqlCommand.Parameters.AddWithValue("@cantidad", txtcan1.Text);
                                    sqlCommand.Parameters.AddWithValue("@tipo_movimiento", "CONSUMO");
                                    sqlCommand.Parameters.AddWithValue("@fecha", fechaactividad.Text + " | " + ClaseCompartida.nombre);
                                    sqlCommand.ExecuteNonQuery();


                                    SqlCommand command_buscar1 = new SqlCommand("Select * from tbbodega_consumo Where OT = @item", connection);
                                    command_buscar1.Parameters.AddWithValue("@item", Convert.ToString(txtot.Text));
                                    SqlDataReader reader_buscar1 = command_buscar1.ExecuteReader();
                                    if (reader_buscar1.Read())
                                    {
                                        reader_buscar1.Close();
                                        SqlDataAdapter adapter = new SqlDataAdapter();
                                        adapter.SelectCommand = command_buscar1;
                                        DataTable dataTable = new DataTable();
                                        adapter.Fill(dataTable);
                                        gvBodegaConsumible.ItemsSource = dataTable.DefaultView;
                                    }
                                    cbx1.Focus();
                                    cbx1.Text = "";
                                    txtcan1.Clear();
                                    lb1.Content = "";
                                    MessageBox.Show("REGISTRO EXITOSO...");
                                }
                                else
                                {
                                    MessageBox.Show("Stock de técnico o consolidado insuficiente, por favor revise.");
                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("ITEM NO EXISTE");
                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void btnasignarcpe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtserial.Text = txtserial.Text.ToUpper();
                if (txtnumdoc.Text != txtserial.Text)
                {
                    connection.Close();
                    connection.Open();
                    string cedula = txtnumdoc.Text;
                    string busqueda_item = "";
                    // int cantidad_item_tbtecnico = 0;
                    string descripcion = "";
                    string estado = "";
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @item", connection);
                    command_buscar.Parameters.AddWithValue("@item", Convert.ToString(txtserial.Text));
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        estado = Convert.ToString(reader_buscar["ESTADO"]);
                        if (true)
                        {
                            busqueda_item = Convert.ToString(reader_buscar["ITEM"]);
                            descripcion = Convert.ToString(reader_buscar["MATERIAL"]);
                            reader_buscar.Close();
                            string tabla = "tabla" + cedula;
                            string serial = txtserial.Text;


                            SqlCommand command_buscar12 = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial1", connection);
                            command_buscar12.Parameters.AddWithValue("@serial1", Convert.ToString(txtserial.Text));
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
                                sqlCommand.Parameters.AddWithValue("@serial", txtserial.Text);
                                sqlCommand.Parameters.AddWithValue("@estado", "CONSUMIDO");
                                sqlCommand.Parameters.AddWithValue("@lugar", txtcuenta.Text);
                                sqlCommand.Parameters.AddWithValue("@idlugar", txtot.Text);
                                sqlCommand.Parameters.AddWithValue("@fecha", fechaactividad.Text + " | " + ClaseCompartida.nombre);
                                sqlCommand.ExecuteNonQuery();
                            }


                            string cadena = "DELETE from " + tabla + " Where ITEM = @itemD";
                            SqlCommand comando = new SqlCommand(cadena, connection);
                            comando.Parameters.AddWithValue("@itemD", serial);
                            comando.ExecuteNonQuery();

                            //CAMBIA ESTADO EN BDCPES
                            string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'CONSUMIDO', LUGAR_ASIGNACIÓN = '" + txtcuenta.Text + "' ,ID_LUGAR = '" + txtot.Text + "'  Where SERIAL = '" + busqueda_item + "'";
                            SqlCommand command_update = new SqlCommand(query, connection);
                            command_update.Parameters.AddWithValue("@CANTIDAD", busqueda_item);
                            command_update.ExecuteNonQuery();


                            string dateTime = DateTime.Now.ToString("dd/MM/yyyy");

                            string queryIN = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD, FECHA_REGISTRO, DIGITADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad, @fecha_registro, @digitador_por)";
                            SqlCommand command = new SqlCommand(queryIN, connection);
                            command.Parameters.AddWithValue("@cédula", txtnumdoc.Text);
                            command.Parameters.AddWithValue("@nombre", lbname.Content);
                            command.Parameters.AddWithValue("@cuenta_consumo", txtcuenta.Text);
                            command.Parameters.AddWithValue("@ot", txtot.Text);
                            command.Parameters.AddWithValue("@nodo", txtnodo.Text);
                            command.Parameters.AddWithValue("@tipo_trabajo", txttipot.Text);
                            command.Parameters.AddWithValue("@item", txtserial.Text);
                            command.Parameters.AddWithValue("@material", descripcion);
                            command.Parameters.AddWithValue("@cantidad", 1);
                            command.Parameters.AddWithValue("@fecha_actividad", fechaactividad.Text);
                            command.Parameters.AddWithValue("@fecha_registro", dateTime);
                            command.Parameters.AddWithValue("@digitador_por", ClaseCompartida.nombre);
                            command.ExecuteNonQuery();

                            SqlCommand command_buscar1 = new SqlCommand("Select * from tbbodega_consumo Where CUENTA_CONSUMO = @item", connection);
                            command_buscar1.Parameters.AddWithValue("@item", Convert.ToString(txtcuenta.Text));
                            SqlDataReader reader_buscar1 = command_buscar1.ExecuteReader();
                            if (reader_buscar1.Read())
                            {
                                reader_buscar1.Close();
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command_buscar1;
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                gvBodegaConsumible.ItemsSource = dataTable.DefaultView;
                            }
                            txtserial.Focus();
                            txtserial.Clear();
                            MessageBox.Show("REGISTRO EXITOSO...");
                        }
                        else
                        {
                            MessageBox.Show("NO SE PUEDE REGISTRAR. \nEl técnico aun no ha aceptado", "REGISTRO");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CPE no registrado en bodega del técnico.");
                    }
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Verifique el serial");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Logistica logistica = new Logistica();
            logistica.Show();
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbx1.Text != "")
                {
                    connection.Close();
                    connection.Open();
                    string cedula = txtnumdoc.Text;
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @item", connection); //CONSULTA SQL
                    command_buscar.Parameters.AddWithValue("@item", itemscod[cbx1.SelectedIndex].ToString());
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        lb1.Content = itemsname[cbx1.SelectedIndex];
                        btnregistromat.IsEnabled = true;
                    }
                    else
                    {
                        lb1.Content = "SIN ASIGNACIÓN A TÉCNICO";
                        btnregistromat.IsEnabled = false;
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                lb1.Content = "CÓDIGO INEXISTENTE";
                btnregistromat.IsEnabled = false;
            }
        }
        private void cbx1_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbx1.IsDropDownOpen = true;
        }
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox1.IsChecked == true)
            {
                txttipot.IsEnabled = true;
                btnasignarcpe.IsEnabled = false;
                btnregistromat.IsEnabled = false;
            }
            else
            {
                txttipot.IsEnabled = false;
                btnasignarcpe.IsEnabled = true;
                btnregistromat.IsEnabled = true;
            }
        }
        private void btnbodetec_Click(object sender, RoutedEventArgs e)
        {
            flagt.flagtec = 1;
            flagt.ced = txtnumdoc.Text;
            BodegaTec tec = new BodegaTec();
            tec.Show();
        }
        private void btnbodcpes_Click(object sender, RoutedEventArgs e)
        {
            serialcpe = txtserial.Text;
            BodegaCpes cpe = new BodegaCpes();
            cpe.Show();

        }
        private void txtserial_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void btnbuscarf_Click(object sender, RoutedEventArgs e)
        {
            if (rdot.IsChecked == true)
            {
                busqueda = "OT";
            }
            else if (rddoc.IsChecked == true)
            {
                busqueda = "CÉDULA";
            }
            else if (rdcuenta.IsChecked == true)
            {
                busqueda = "CUENTA_CONSUMO";
            }
            Thread.Sleep(5000);
            connection.Close();
            connection.Open();
            SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_consumo Where " + busqueda + " = @item", connection);
            command_buscar.Parameters.AddWithValue("@item", Convert.ToString(textBox1.Text));
            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
            if (reader_buscar.Read())
            {
                reader_buscar.Close();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command_buscar;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.Fill(dt);
                gvBodegaConsumible.ItemsSource = dataTable.DefaultView;
                flag = 1;
            }
            else
            {
                gvBodegaConsumible.Columns.Clear();
                flag = 0;
            }
        }
        private void txtnumdoc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void consumomas_Click(object sender, RoutedEventArgs e)
        {
            Consumomasivocpes conmas = new Consumomasivocpes();
            conmas.Show();
        }
        private void rdporfiltro_Checked(object sender, RoutedEventArgs e)
        {
            cnfiltrofecha.Visibility = Visibility.Hidden;
            cnbusqueda.Visibility = Visibility.Visible;
            flag = 1;
        }
        private void rdporfecha_Checked(object sender, RoutedEventArgs e)
        {
            cnfiltrofecha.Visibility = Visibility.Visible;
            cnbusqueda.Visibility = Visibility.Hidden;
            flag = 0;
        }
        private void cbxfiltrofehca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxfiltrofehca.SelectedIndex == 0)
            {
                filtro = "FECHA_ACTIVIDAD";
            }
            else if (cbxfiltrofehca.SelectedIndex == 1)
            {
                filtro = "FECHA_REGISTRO";
            }

        }
    }
}
