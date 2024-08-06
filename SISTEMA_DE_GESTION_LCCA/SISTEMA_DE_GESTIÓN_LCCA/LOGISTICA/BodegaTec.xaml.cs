
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para BodegaTec.xaml
    /// </summary>
    public partial class BodegaTec : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        string movimientos = "";
        string[] items = new string[300];
        string[] itemscod = new string[300];
        private int flag;

        public BodegaTec()
        {
            try
            {
                InitializeComponent();
                materiales.Visibility = Visibility.Hidden;
                Mcpes.Visibility = Visibility.Hidden;
                btnasignar.IsEnabled = false;
                rbmaterial.IsEnabled = false;
                rbcpe.IsEnabled = false;
                cbx1.IsEnabled = false;
                gvtablatecnicos.IsEnabled = false;
                txtcan1.IsEnabled = false;
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
                    items[cont] = Convert.ToString(row["MATERIAL"]);
                    itemscod[cont] = Convert.ToString(row["ITEM"]);
                    cont++;
                }
                //CBX1
                cbx1_Copy.ItemsSource = dataTable2.DefaultView;
                cbx1_Copy.DisplayMemberPath = "ID";
                cbx1_Copy.DisplayMemberPath = "ITEM";
                cbx1.ItemsSource = dataTable2.DefaultView;
                cbx1.DisplayMemberPath = "ID";
                cbx1.DisplayMemberPath = "ITEM";
                cbx1.SelectedIndex = 1;
                btnbuscarelemento.IsEnabled = false;
                if (flagt.flagtec == 1)
                {
                    txtnumdoc.Text = flagt.ced;
                    buscartec();
                    rbmaterial.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void actualizamaterial()
        {
            materiales.Visibility = Visibility.Visible;
            Mcpes.Visibility = Visibility.Hidden;
            btnasignarcpe.IsDefault = false;
            btnasignar.IsDefault = true;

            string cedula = Convert.ToString(txtnumdoc.Text);
            SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " WHERE ITEM LIKE '%-%' AND CANTIDAD >= 1", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command1;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gvtablatecnicos.ItemsSource = dataTable.DefaultView;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            actualizamaterial();
            flag = 1;
        }
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            materiales.Visibility = Visibility.Hidden;
            Mcpes.Visibility = Visibility.Visible;
            btnasignarcpe.IsDefault = true;
            btnasignar.IsDefault = false;
            gvtablatecnicos.IsEnabled = true;
            string cedula = Convert.ToString(txtnumdoc.Text);
            SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " where ESTADO = 'ASIGNADO' OR ESTADO = 'REINTEGRAR' OR ESTADO = 'ACEPTADO'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command1;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gvtablatecnicos.ItemsSource = dataTable.DefaultView;

        }
        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            buscartec();
        }
        public void buscartec()
        {
            try
            {

                gvtablatecnicos.ItemsSource = null;
                if (string.IsNullOrEmpty(txtnumdoc.Text))
                {
                    MessageBox.Show("Por favor introduzca cédula.");
                }
                else
                {

                    connection.Close();
                    connection.Open();
                    string cedula = Convert.ToString(txtnumdoc.Text);
                    //int id string cedula = Convert.ToString(txtnumdoc.Text);= 0;
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @id", connection); //CONSULTA SQL
                    command_buscar.Parameters.AddWithValue("@id", cedula);
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        lbname.Content = Convert.ToString(reader_buscar["MATERIAL"]);
                        lbdate.Content = Convert.ToString(reader_buscar["ULTIMA_FECHA_ENTREGA"]);
                        ClaseTecnico.nombre = Convert.ToString(lbname.Content);
                        ClaseTecnico.cedula = Convert.ToString(txtnumdoc.Text);
                    }
                    reader_buscar.Close();
                    MessageBox.Show("Base de datos actualizada");
                    rbmaterial.IsEnabled = true;
                    rbcpe.IsEnabled = true;
                    cbx1.IsEnabled = false;
                    materiales.Visibility = Visibility.Hidden;
                    Mcpes.Visibility = Visibility.Hidden;
                    cbx1.SelectedIndex = 0;
                    rdasig.IsChecked = false;
                    rdrein.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                rbmaterial.IsEnabled = false;
                rbcpe.IsEnabled = false;
                MessageBox.Show("Usuario no registrado." + ex.ToString());
                lbname.Content = "";
                lbname.Content = "";
                gvtablatecnicos.ItemsSource = null;
                materiales.Visibility = Visibility.Hidden;
                Mcpes.Visibility = Visibility.Hidden;
                cbx1.IsEnabled = false;
                rdasig.IsChecked = false;
                rdrein.IsChecked = false;
                lbdate.Content = "";
                btnbuscarelemento.IsEnabled = false;
            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            flagt.flagtec = 0;
            this.Close();
        }
        private void btnexportar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnnewcodigo_Click(object sender, RoutedEventArgs e)
        {

            connection.Close();
            connection.Open();

            string nombre;
            string cedula1;
            string movil;
            do
            {
                nombre = Microsoft.VisualBasic.Interaction.InputBox(
                "INGRESE NOMBRE COMPLETO",
                "NOMBRE"
                );
                cedula1 = Microsoft.VisualBasic.Interaction.InputBox(
               "INGRESE CÉDULA, SIN PUNTOS",
               "CÉDULA"
               );
                movil = "00";
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cedula1) || string.IsNullOrEmpty(movil))

                {
                    MessageBox.Show("NINGUN CAMPO PUEDE QUEDAR VACIÓ");
                }

            } while (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cedula1) || string.IsNullOrEmpty(movil));

            string btn = Convert.ToString(MessageBox.Show("¿Esta seguro que desea creal el usuario " + cedula1 + "?", "CREAR USUARIO", MessageBoxButtons.YesNoCancel));

            if (btn == "Yes")
            {
                try
                {
                    string sql = "CREATE TABLE tabla" + cedula1 + "(ITEM nvarchar(255), MATERIAL nvarchar(255), CANTIDAD float, ULTIMA_FECHA_ENTREGA nvarchar(255), ESTADO nvarchar(255));";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Creada");


                    string cadena = "INSERT into tabla" + cedula1 + "(ITEM, MATERIAL, CANTIDAD) VALUES (@item, @material, @cantidad)";
                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                    sqlCommand.Parameters.AddWithValue("@item", cedula1);
                    sqlCommand.Parameters.AddWithValue("@material", nombre);
                    sqlCommand.Parameters.AddWithValue("@cantidad", movil);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show("El usuario ya existe, no se puede crear");
                }

            }
            else if (btn == "No" || btn == "Cancel")
            {
                MessageBox.Show("Creación cancelada");
            }
            connection.Close();

        }
        private void btntrl_Click(object sender, RoutedEventArgs e)
        {
            Inversa inv = new Inversa();
            inv.ShowDialog();
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            flagt.flagtec = 0;
            this.Close();
        }
        private void cbx1_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbx1.IsDropDownOpen = true;
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
                    SqlCommand command_buscar = new SqlCommand("Select * from tbBodega_ppal Where ITEM = @item", connection); //CONSULTA SQL
                    command_buscar.Parameters.AddWithValue("@item", itemscod[cbx1.SelectedIndex].ToString());
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        lb1.Content = Convert.ToString(reader_buscar["MATERIAL"]);
                        btnasignar.IsEnabled = true;
                    }
                    else
                    {
                        lb1.Content = "**CÓDIDGO INEXISTENTE**";
                        btnasignar.IsEnabled = false;
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                lb1.Content = "**CÓDIDGO INEXISTENTE**";
                btnasignar.IsEnabled = false;
            }
        }
        private void btnasignar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
                //try
                //{

                if (string.IsNullOrEmpty(txtcan1.Text))
                {
                    MessageBox.Show("Por favor introduzca cantidad.");
                }
                else
                {
                    connection.Close();
                    connection.Open();
                    //BUSCAR EL ITEM PARA COMPARARLO EN EL SIGUIENTE IF
                    string cedula = txtnumdoc.Text;
                    string busqueda_item = "";
                    int cantidad_item = 0;
                    SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " Where ITEM = @item", connection);
                    command_buscar.Parameters.AddWithValue("@item", Convert.ToString(cbx1.Text));
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        busqueda_item = Convert.ToString(reader_buscar["ITEM"]); //Almacena ese item EN una variable
                        cantidad_item = Convert.ToInt32(reader_buscar["CANTIDAD"]);//Obtiene cantidad de bdppal 
                        string namemat = Convert.ToString(reader_buscar["MATERIAL"]);


                        if (cbx1.Text == busqueda_item)
                        {
                            reader_buscar.Close();
                            var fecha = Convert.ToString(dateTime);

                            int busqueda_item_can = 0;
                            SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item2", connection);
                            command_buscar2.Parameters.AddWithValue("@item2", cbx1.Text);
                            SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                            if (reader_buscar2.Read())
                            {
                                int suma_can_nueva = 0;
                                int suma = 0;
                                string op = "";
                                string op1 = "";

                                if (rdasig.IsChecked == true)
                                {
                                    busqueda_item_can = Convert.ToInt32(reader_buscar2["CANTIDAD"]); //Obtiene cantidad de bdppal                                 
                                    suma_can_nueva = busqueda_item_can - Convert.ToInt32(txtcan1.Text);
                                    suma = cantidad_item + Convert.ToInt32(txtcan1.Text);
                                    movimientos = "ASIGNACION";
                                    op = "+";
                                    op1 = "-";
                                }
                                if (rdrein.IsChecked == true)
                                {
                                    busqueda_item_can = Convert.ToInt32(reader_buscar2["CANTIDAD"]);
                                    //int txt = Convert.ToInt32(txtcant1.Text);
                                    suma_can_nueva = busqueda_item_can + Convert.ToInt32(txtcan1.Text);
                                    suma = cantidad_item - Convert.ToInt32(txtcan1.Text);
                                    movimientos = "REINTEGRO";
                                    op = "-";
                                    op1 = "+";
                                }
                                if (suma_can_nueva >= 0 && suma >= 0)
                                {
                                    //Resta cantidad en BD_ppal
                                    reader_buscar2.Close();
                                    string query2 = "UPDATE tbbodega_ppal SET CANTIDAD = CANTIDAD "+op1+" "+ Convert.ToInt32(txtcan1.Text) +" Where ITEM ='" + cbx1.Text + "'";
                                    SqlCommand command_update2 = new SqlCommand(query2, connection);
                                    command_update2.Parameters.AddWithValue("ITEM", "CANTIDAD " + op1 + " " + Convert.ToInt32(txtcan1.Text) + "");
                                    command_update2.ExecuteNonQuery();

                                    //Suma cantidad a tecnico                          


                                    string queryup = "UPDATE tabla" + cedula + " SET CANTIDAD = CANTIDAD " + op + " " + Convert.ToInt32(txtcan1.Text) + ", ULTIMA_FECHA_ENTREGA = '" + fecha + " | " + ClaseCompartida.nombre + "', ESTADO = NULL Where ITEM ='" + cbx1.Text + "'";
                                    SqlCommand command_updateup = new SqlCommand(queryup, connection);
                                    command_updateup.Parameters.AddWithValue("ITEM", "CANTIDAD " + op + " " + Convert.ToInt32(txtcan1.Text) + "");
                                    command_updateup.Parameters.AddWithValue("ULTIMA_FECHA_ENTREGA", fecha);
                                    command_updateup.Parameters.AddWithValue("ESTADO", "NULL");
                                    command_updateup.ExecuteNonQuery();


                                    //envia cantidad al consolidado
                                    int cantidad_item_consolidado = 0;
                                    SqlCommand command_buscar3 = new SqlCommand("Select * from tbbodega_consolidado Where ITEM = @item", connection);
                                    command_buscar3.Parameters.AddWithValue("@item", Convert.ToString(cbx1.Text));
                                    SqlDataReader reader_buscar3 = command_buscar3.ExecuteReader();
                                    if (reader_buscar3.Read())
                                    {
                                        if (rdasig.IsChecked == true)
                                        {
                                            cantidad_item_consolidado = int.Parse(txtcan1.Text) + Convert.ToInt32(reader_buscar3["CANTIDAD"]);
                                        }
                                        else if (rdrein.IsChecked == true)
                                        {
                                            cantidad_item_consolidado = Convert.ToInt32(reader_buscar3["CANTIDAD"]) - int.Parse(txtcan1.Text);
                                        }

                                        reader_buscar3.Close();

                                        String queryup2 = "UPDATE tbbodega_consolidado SET CANTIDAD = CANTIDAD " + op + " " + Convert.ToInt32(txtcan1.Text) + " Where ITEM ='" + cbx1.Text + "'";
                                        SqlCommand command_updateup2 = new SqlCommand(queryup2, connection);
                                        command_updateup2.Parameters.AddWithValue("ITEM", cantidad_item_consolidado);
                                        command_updateup2.ExecuteNonQuery();
                                    }
                                    String queryup4 = "UPDATE tabla" + cedula + " SET ULTIMA_FECHA_ENTREGA = '" + fecha + " | " + ClaseCompartida.nombre + "' Where ITEM ='" + Convert.ToString(txtnumdoc.Text) + "'";
                                    SqlCommand command_updateup4 = new SqlCommand(queryup4, connection);
                                    command_updateup4.Parameters.AddWithValue("ITEM", fecha);
                                    command_updateup4.ExecuteNonQuery();
                                    lbdate.Content = Convert.ToString(dateTime);

                                    string cadena = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                                    sqlCommand.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                    sqlCommand.Parameters.AddWithValue("@tecnico", lbname.Content);
                                    sqlCommand.Parameters.AddWithValue("@item", busqueda_item);
                                    sqlCommand.Parameters.AddWithValue("@material", namemat);
                                    sqlCommand.Parameters.AddWithValue("@cantidad", txtcan1.Text);
                                    sqlCommand.Parameters.AddWithValue("@tipo_movimiento", movimientos);
                                    sqlCommand.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                    sqlCommand.ExecuteNonQuery();



                                    connection.Close();

                                    txtcan1.Clear();



                                    SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " WHERE ITEM LIKE '%-%' AND CANTIDAD >= 1", connection);
                                    SqlDataAdapter adapter = new SqlDataAdapter();
                                    adapter.SelectCommand = command1;
                                    DataTable dataTable = new DataTable();
                                    adapter.Fill(dataTable);
                                    gvtablatecnicos.ItemsSource = dataTable.DefaultView;
                                    cbx1.Text = "";
                                    cbx1.Focus();

                                    if (movimientos == "ASIGNACION")
                                    {
                                        MessageBox.Show("Elemento asignado correctamente");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Elemento reintegrado correctamente");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Stock insuficiente, por favor verifique la cantidad");
                                }
                                connection.Close();
                            }

                        }



                    }
                    else
                    {
                        if (rdasig.IsChecked == true)
                        {
                            reader_buscar.Close();
                            int busqueda_item_can = 0;
                            SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item2", connection);
                            command_buscar2.Parameters.AddWithValue("@item2", cbx1.Text);
                            SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                            if (reader_buscar2.Read())
                            {
                                var fecha = Convert.ToString(dateTime);
                                int suma_can_nueva = 0;
                                busqueda_item_can = Convert.ToInt32(reader_buscar2["CANTIDAD"]);
                                string nombremat = Convert.ToString(reader_buscar2["MATERIAL"]);
                                suma_can_nueva = busqueda_item_can - Convert.ToInt32(txtcan1.Text);
                                if (suma_can_nueva >= 0)
                                {
                                    reader_buscar2.Close();

                                    string cadena = "INSERT into tabla" + cedula + "(ITEM, MATERIAL, CANTIDAD, ULTIMA_FECHA_ENTREGA, ESTADO) VALUES (@item, @material, @cantidad, @fecha, @estado)";
                                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                                    sqlCommand.Parameters.AddWithValue("@item", cbx1.Text);
                                    sqlCommand.Parameters.AddWithValue("@material", nombremat);
                                    sqlCommand.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtcan1.Text));
                                    sqlCommand.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                    sqlCommand.Parameters.AddWithValue("@estado", "NULL");
                                    sqlCommand.ExecuteNonQuery();


                                    String query2 = "UPDATE tbbodega_ppal SET CANTIDAD = CANTIDAD - " + Convert.ToInt32(txtcan1.Text) +" Where ITEM ='" + cbx1.Text + "'";
                                    SqlCommand command_update2 = new SqlCommand(query2, connection);
                                    command_update2.Parameters.AddWithValue("ITEM", "CANTIDAD - " + Convert.ToInt32(txtcan1.Text) + "");
                                    command_update2.ExecuteNonQuery();

                                    //envia cantidad al consolidado
                                    int cantidad_item_consolidado = 0;
                                    SqlCommand command_buscar3 = new SqlCommand("Select * from tbbodega_consolidado Where ITEM = @item", connection);
                                    command_buscar3.Parameters.AddWithValue("@item", Convert.ToString(cbx1.Text));
                                    SqlDataReader reader_buscar3 = command_buscar3.ExecuteReader();
                                    if (reader_buscar3.Read())
                                    {
                                        cantidad_item_consolidado = int.Parse(txtcan1.Text) + Convert.ToInt32(reader_buscar3["CANTIDAD"]);


                                        reader_buscar3.Close();

                                        string queryup2 = "UPDATE tbbodega_consolidado SET CANTIDAD = CANTIDAD + " + Convert.ToInt32(txtcan1.Text) + " Where ITEM ='" + cbx1.Text + "'";
                                        SqlCommand command_updateup2 = new SqlCommand(queryup2, connection);
                                        command_updateup2.Parameters.AddWithValue("ITEM", "CANTIDAD + " + Convert.ToInt32(txtcan1.Text) + "");
                                        command_updateup2.ExecuteNonQuery();
                                        //Inserta nuevo codigo en tecnico
                                        string fecha1 = dateTime.ToString();
                                        string cadena2 = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                        SqlCommand sqlCommand2 = new SqlCommand(cadena2, connection);
                                        sqlCommand2.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                        sqlCommand2.Parameters.AddWithValue("@tecnico", lbname.Content);
                                        sqlCommand2.Parameters.AddWithValue("@item", cbx1.Text);
                                        sqlCommand2.Parameters.AddWithValue("@material", nombremat);
                                        sqlCommand2.Parameters.AddWithValue("@cantidad", txtcan1.Text);
                                        sqlCommand2.Parameters.AddWithValue("@tipo_movimiento", "ASIGNACIÓN");
                                        sqlCommand2.Parameters.AddWithValue("@fecha", fecha1 + " | " + ClaseCompartida.nombre);
                                        sqlCommand2.ExecuteNonQuery();

                                        MessageBox.Show("ITEM nuevo agregado correctamente.");
                                        txtcan1.Clear();

                                    }
                                    else
                                    {
                                        reader_buscar3.Close();
                                        MessageBox.Show("No se agrego a consolidado");
                                    }

                                }
                                else
                                {
                                    reader_buscar2.Close();
                                    MessageBox.Show("Stock insuficiente");
                                }
                            }

                            SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " WHERE ITEM LIKE '%-%' AND CANTIDAD >= 1", connection);
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command1;
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            gvtablatecnicos.ItemsSource = dataTable.DefaultView;
                            cbx1.Text = "";
                            cbx1.Focus();
                            connection.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se peude reintegrar, item en 0");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void btnasignarcpe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt2.Text))
                {
                    MessageBox.Show("Por favor introduzca serial.");
                }
                else
                {
                    txt2.Text.ToUpper();
                    txt2.Text.Trim();
                    connection.Close();
                    connection.Open();
                    //BUSCAR EL ITEM PARA COMPARARLO EN EL SIGUIENTE IF
                    string serial = txt2.Text;
                    SqlCommand command_buscar = new SqlCommand("Select * from tbbodega_cpesppal Where SERIAL = @serial", connection);
                    command_buscar.Parameters.AddWithValue("@serial", Convert.ToString(txt2.Text));
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        string busqueda_item = Convert.ToString(reader_buscar["SERIAL"]); //Almacena ese item EN una variable
                        string estado = Convert.ToString(reader_buscar["ESTADO"]);
                        string id_asignado = Convert.ToString(reader_buscar["ID_LUGAR"]);
                        string descripción = Convert.ToString(reader_buscar["DESCRIPCIÓN"]);
                        string codigo = Convert.ToString(reader_buscar["CODIGO"]);

                        if (txt2.Text == busqueda_item)
                        {
                            reader_buscar.Close();
                            if (estado == "ALMACENADO" || estado == "DISPONIBLE")
                            {
                                var fecha = DateTime.Now.ToString("dd/MM/yyyy");
                                //CAMBIA ESTADO EN BDCPES

                                string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'ASIGNADO', LUGAR_ASIGNACIÓN = '" + lbname.Content + " | " + ClaseCompartida.usuario + "' ,ID_LUGAR = '" + txtnumdoc.Text + "'  Where SERIAL = '" + busqueda_item + "'";
                                SqlCommand command_update = new SqlCommand(query, connection);
                                command_update.Parameters.AddWithValue("@CANTIDAD", busqueda_item);
                                command_update.ExecuteNonQuery();

                                //SUBE DATOS A BD_TECNICO.                            
                                string tabla = "tabla" + Convert.ToString(txtnumdoc.Text);
                                string query2 = "INSERT " + tabla + " (ITEM, MATERIAL, CANTIDAD, ULTIMA_FECHA_ENTREGA, ESTADO) VALUES(@item,@material,@cantidad,@ultima, @estado)";
                                SqlCommand command = new SqlCommand(query2, connection);
                                command.Parameters.AddWithValue("@item", txt2.Text);
                                command.Parameters.AddWithValue("@material", descripción);
                                command.Parameters.AddWithValue("@cantidad", 1);
                                command.Parameters.AddWithValue("@ultima", fecha + " | " + ClaseCompartida.nombre);
                                command.Parameters.AddWithValue("@estado", "ASIGNADO");
                                command.ExecuteNonQuery();

                                MessageBox.Show("ASIGNADO");

                                string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                SqlCommand sqlCommand = new SqlCommand(trasavilidad, connection);

                                sqlCommand.Parameters.AddWithValue("@codigo", codigo);
                                sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                                sqlCommand.Parameters.AddWithValue("@serial", serial);
                                sqlCommand.Parameters.AddWithValue("@estado", "ASIGNADO");
                                sqlCommand.Parameters.AddWithValue("@lugar", lbname.Content);
                                sqlCommand.Parameters.AddWithValue("@idlugar", txtnumdoc.Text);
                                sqlCommand.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                sqlCommand.ExecuteNonQuery();


                                string cedula = Convert.ToString(txtnumdoc.Text);
                                SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " where ESTADO = 'ASIGNADO' OR ESTADO = 'REINTEGRAR' OR ESTADO = 'ACEPTADO'", connection);
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command1;
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                gvtablatecnicos.ItemsSource = dataTable.DefaultView;

                                //FIN
                                txt2.Focus();
                                txt2.Clear();
                            }
                            else
                            {
                                MessageBox.Show("El CPE se encuentra en estado '" + estado + "' en " + id_asignado);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El CPE ingresado no coincide con el registrado " + txt2.Text + " " + busqueda_item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Elemento no encontrado en bodega CPES");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnexportar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string tabla = "tabla" + Convert.ToString(txtnumdoc.Text);
                SqlCommand command1 = new SqlCommand("Select * from " + tabla + "", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command1;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvtablatecnicos.ItemsSource = dt.DefaultView;

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
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            gvtablatecnicos.IsEnabled = true;
            cbx1.IsEnabled = true;
            txtcan1.IsEnabled = true;
            btnasignar.Content = "ASIGNAR MATERIAL";
        }
        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            gvtablatecnicos.IsEnabled = true;
            cbx1.IsEnabled = true;
            txtcan1.IsEnabled = true;
            btnasignar.Content = "REINTEGRAR MATERIAL";
        }
        private void txtcan1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtcan1.Text) >= 0)
                {

                }
                else
                {
                    txtcan1.Text = "";
                }
            }
            catch (Exception)
            {
                txtcan1.Text = "";
            }
        }
        private void btnbuscarelemento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdmochila.IsChecked == true)
                {
                    // MUESTRA DATOS EN GREADVIEW
                    string cedula = Convert.ToString(txtnumdoc.Text);
                    SqlCommand command1 = new SqlCommand("Select * from tabla" + cedula + " where ITEM = '" + cbx1_Copy.Text + "'", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command1;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvtablatecnicos.ItemsSource = dataTable.DefaultView;
                }
                else if (rdtrasavilidad.IsChecked == true)
                {
                    string cedula = Convert.ToString(txtnumdoc.Text);
                    SqlCommand command1 = new SqlCommand("Select * from tbtrasavilidadmaterial where ITEM = '" + cbx1_Copy.Text + "' AND CEDULA = '" + txtnumdoc.Text + "' AND CANTIDAD > 0", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command1;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvtablatecnicos.ItemsSource = dataTable.DefaultView;
                }
                cbx1_Copy.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Algo paso, intenta de nuevo");
            }
        }
        private void cbx1_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnbuscarelemento.IsEnabled = true;
        }
        private void gvtablatecnicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void gvtablatecnicos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (flagt.flagtec != 1)
                {
                    if (flag == 1)
                    {
                        string item = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                        string mat = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[1].ToString());
                        int can = Convert.ToInt32((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[2].ToString());
                        string cedula = Convert.ToString(txtnumdoc.Text);
                        string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                        if (can >= 1)
                        {
                            DialogResult dialogResult = MessageBox.Show("¿Esta seguro que el técnico debe " + can + " unidades de " + mat + "", "Deuda", MessageBoxButtons.YesNo);
                            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                connection.Close();
                                connection.Open();
                                string serial = txt2.Text;
                                SqlCommand command_buscar = new SqlCommand("Select * from Deudas_personal where ID_TECNICO = '" + txtnumdoc.Text + "' AND ITEM = @item", connection);
                                command_buscar.Parameters.AddWithValue("@item", item);
                                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                                if (reader_buscar.Read())
                                {
                                    int id = Convert.ToInt32(reader_buscar["id"]);
                                    int canr = Convert.ToInt32(reader_buscar["CANTIDAD"]) + can;
                                    reader_buscar.Close();


                                    string query = "UPDATE Deudas_personal SET CANTIDAD = '" + canr + "'  Where id = '" + id + "'";
                                    SqlCommand command_update = new SqlCommand(query, connection);
                                    command_update.Parameters.AddWithValue("CANTIDAD", canr);
                                    command_update.ExecuteNonQuery();

                                    string query1 = "UPDATE tabla" + cedula + " SET CANTIDAD = '0'  Where ITEM = '" + item + "'";
                                    SqlCommand command_update1 = new SqlCommand(query1, connection);
                                    command_update1.Parameters.AddWithValue("CANTIDAD", 0);
                                    command_update1.ExecuteNonQuery();



                                    string cadena2 = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                    SqlCommand sqlCommand2 = new SqlCommand(cadena2, connection);
                                    sqlCommand2.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                    sqlCommand2.Parameters.AddWithValue("@tecnico", lbname.Content);
                                    sqlCommand2.Parameters.AddWithValue("@item", item);
                                    sqlCommand2.Parameters.AddWithValue("@material", mat);
                                    sqlCommand2.Parameters.AddWithValue("@cantidad", can);
                                    sqlCommand2.Parameters.AddWithValue("@tipo_movimiento", "DEUDA");
                                    sqlCommand2.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                    sqlCommand2.ExecuteNonQuery();
                                }
                                else
                                {
                                    reader_buscar.Close();
                                    string trasavilidad = "insert Deudas_personal (ID_TECNICO, NOMBRE_TEC, ITEM, DESCRIPCIÓN, CANTIDAD, FECHA_REGISTRO, LOGISTICO) VALUES(@id_tec, @nombre, @codigo, @descripcion, @serial, @estado, @lugar)";
                                    SqlCommand sqlCommand = new SqlCommand(trasavilidad, connection);

                                    sqlCommand.Parameters.AddWithValue("@id_tec", txtnumdoc.Text);
                                    sqlCommand.Parameters.AddWithValue("@nombre", lbname.Content);
                                    sqlCommand.Parameters.AddWithValue("@codigo", item);
                                    sqlCommand.Parameters.AddWithValue("@descripcion", mat);
                                    sqlCommand.Parameters.AddWithValue("@serial", can);
                                    sqlCommand.Parameters.AddWithValue("@estado", fecha);
                                    sqlCommand.Parameters.AddWithValue("@lugar", ClaseCompartida.nombre);
                                    sqlCommand.ExecuteNonQuery();
                                    string query = "UPDATE tabla" + cedula + " SET CANTIDAD = '0'  Where ITEM = '" + item + "'";
                                    SqlCommand command_update = new SqlCommand(query, connection);
                                    command_update.Parameters.AddWithValue("CANTIDAD", 0);
                                    command_update.ExecuteNonQuery();

                                    string cadena2 = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                    SqlCommand sqlCommand2 = new SqlCommand(cadena2, connection);
                                    sqlCommand2.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                    sqlCommand2.Parameters.AddWithValue("@tecnico", lbname.Content);
                                    sqlCommand2.Parameters.AddWithValue("@item", item);
                                    sqlCommand2.Parameters.AddWithValue("@material", mat);
                                    sqlCommand2.Parameters.AddWithValue("@cantidad", can);
                                    sqlCommand2.Parameters.AddWithValue("@tipo_movimiento", "DEUDA");
                                    sqlCommand2.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                    sqlCommand2.ExecuteNonQuery();
                                }
                                MessageBox.Show("Elemento enviado a bodega de deudas");
                                actualizamaterial();
                            }
                            else if (dialogResult == System.Windows.Forms.DialogResult.No)
                            {
                                MessageBox.Show("Cancelado");
                            }
                        }
                    }
                    else if (flag == 0)
                    {
                        try
                        {
                            string id = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                            string item = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[3].ToString());
                            string mat = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[4].ToString());
                            int candeuda = Convert.ToInt32((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[5].ToString());
                            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                            int candevuelta = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Ingrese la cantidad devuelta por el técnico", "Devolución"));
                            DialogResult dialogResult = MessageBox.Show("¿Esta seguro que el técnico devolvio " + candevuelta + " unidades de " + mat + "?", "Devolución", MessageBoxButtons.YesNo);
                            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (candevuelta > candeuda)
                                {
                                    MessageBox.Show("No se peude reintegrar una cantidqad mayor a la que debe el técnico");
                                }
                                else
                                {
                                    int nuevacandu = candeuda - candevuelta;

                                    if (nuevacandu > 0)
                                    {
                                        string query = "UPDATE Deudas_personal SET CANTIDAD = '" + nuevacandu + "'  Where id = " + id + "";
                                        SqlCommand command_update = new SqlCommand(query, connection);
                                        command_update.Parameters.AddWithValue("CANTIDAD", nuevacandu);
                                        command_update.ExecuteNonQuery();

                                        string cadena2 = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                        SqlCommand sqlCommand2 = new SqlCommand(cadena2, connection);
                                        sqlCommand2.Parameters.AddWithValue("@cedula", txtnumdoc.Text);
                                        sqlCommand2.Parameters.AddWithValue("@tecnico", lbname.Content);
                                        sqlCommand2.Parameters.AddWithValue("@item", item);
                                        sqlCommand2.Parameters.AddWithValue("@material", mat);
                                        sqlCommand2.Parameters.AddWithValue("@cantidad", candevuelta);
                                        sqlCommand2.Parameters.AddWithValue("@tipo_movimiento", "PAGO DEUDA");
                                        sqlCommand2.Parameters.AddWithValue("@fecha", fecha + " | " + ClaseCompartida.nombre);
                                        sqlCommand2.ExecuteNonQuery();

                                        SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item2", connection);
                                        command_buscar2.Parameters.AddWithValue("@item2", item);
                                        SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                                        if (reader_buscar2.Read())
                                        {
                                            int cantidad_Bppal = Convert.ToInt32(reader_buscar2["CANTIDAD"]);
                                            int cantidad_nuevappal = cantidad_Bppal + candevuelta;
                                            reader_buscar2.Close();
                                            String query2 = "UPDATE tbbodega_ppal SET CANTIDAD = '" + cantidad_nuevappal + "' Where ITEM ='" + item + "'";
                                            SqlCommand command_update2 = new SqlCommand(query2, connection);
                                            command_update2.Parameters.AddWithValue("ITEM", cantidad_nuevappal);
                                            command_update2.ExecuteNonQuery();
                                        }
                                        rddeudas();
                                        MessageBox.Show("Material devuelto a la Bodega Principal");
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se peude reintegrar una cantidqad mayor a la que debe el técnico");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Cancelado");
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }
                else
                {
                    if (rbcpe.IsChecked == true)
                    {
                        if (Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[4].ToString()) == "ACEPTADO")
                        {
                            flagt.serial = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                            flagt.descripcion = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[1].ToString());
                            flagt.flagtec = 0;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El equipo no esta en estado aceptado, no se puede agregar.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo puede seleccionar equipos.");
                        flagt.flagtec = 1;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            rddeudas();
        }
        public void rddeudas()
        {
            SqlCommand command1 = new SqlCommand("Select * from Deudas_personal where ID_TECNICO = '" + txtnumdoc.Text + "' AND CANTIDAD > 0", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command1;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gvtablatecnicos.ItemsSource = dataTable.DefaultView;
            flag = 0;
        }
        private void gvtablatecnicos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                string c = Convert.ToString((gvtablatecnicos.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                cbx1.Text = c;
            }
            catch (Exception)
            {

            }
        }

        private void btnbodcpes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

