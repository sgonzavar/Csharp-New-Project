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
    /// Lógica de interacción para ConsumosDigi.xaml
    /// </summary>
    public partial class ConsumosDigi : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        DataTable dt = new DataTable();
        public static string id_con;
        public static string cedula;
        public static string nombre;
        public static string ot;
        public static string elemento;
        public static string fechaact;
        private int t_divicion;
        public string filtro;
        public ConsumosDigi()
        {
            InitializeComponent();
            contadores();
        }
        public void iniciar()
        {
            try
            {
                lstomadas.Items.Clear();
                lsnametec(Convert.ToDateTime(pfechaact.Text).ToString("dd/MM/yyyy"));
                loandtomadas(Convert.ToDateTime(pfechaact.Text).ToString("dd/MM/yyyy"));
                contadores();
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
        public void loandtomadas(string date)
        {
            string date1 = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select DISTINCT OT from tbregistroconsumosterreno where ESTADO_REGISTRO = '" + filtro + "'  AND TOMADO_POR = '" + ClaseCompartida.nombre + "' AND FECHA_ACTIVIDAD = '" + date1 + "'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            foreach (DataRow row in dataTable2.Rows)
            {
                lstomadas.Items.Add(row["OT"]);
            }
            
        }
        public void lsnametec(string date)
        {
            cedula = "0";
            string date1 = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            lstecnicos.Items.Clear();
            lspendientes.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select DISTINCT NOMBRE, TOMADO_POR from tbregistroconsumosterreno where ESTADO_REGISTRO = '" + filtro + "' AND FECHA_ACTIVIDAD = '"+date1+ "'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            foreach (DataRow row in dataTable2.Rows)
            {
                if (Convert.ToString(row["TOMADO_POR"]) == ClaseCompartida.nombre || Convert.ToString(row["TOMADO_POR"]) == "")
                {
                    lstecnicos.Items.Add(Convert.ToString(row["NOMBRE"]));
                }
            }
            lbcontfecha.Content = "(" + dataTable2.Rows.Count + ")";
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnregistrarot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                string ot = lspendientes.SelectedItem.ToString();
                int pos = lspendientes.SelectedIndex;
                SqlCommand command2 = new SqlCommand("Select DISTINCT OT, TOMADO_POR from tbregistroconsumosterreno WHERE OT = '" + ot + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    if (Convert.ToString(row["TOMADO_POR"]) == "")
                    {
                        lstomadas.Items.Add(row["OT"]);
                        lspendientes.Items.RemoveAt(pos);
                        string cadena = "UPDATE tbregistroconsumosterreno SET TOMADO_POR = '" + ClaseCompartida.nombre + "' WHERE OT = '" + ot + "'";
                        SqlCommand comando = new SqlCommand(cadena, connection);
                        comando.Parameters.AddWithValue("OT", ClaseCompartida.nombre);
                        comando.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("La OT ya fue tomada por " + Convert.ToString(row["TOMADO_POR"]));
                        lspendientes.Items.RemoveAt(pos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void lstomadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecpendientes();
        }
        private void gvBodegaConsumible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[2].ToString());
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToString(row["ESTADO_REGISTRO"]) != "CONSUMO CONFIRMADO" || Convert.ToString(row["ESTADO_REGISTRO"]) != "RECHAZADO")
                    {
                        string cedula = Convert.ToString(row["CÉDULA"]);
                        string sf = Convert.ToString(row["ITEM"]);
                        if (Convert.ToString(row["ELEMENTO"]) == "MATERIAL")//CONSUMO MATERIAL
                        {
                            connection.Close();
                            connection.Open();
                            string busqueda_item = "";
                            int cantidad_item_tbtecnico = 0;
                            SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " WHERE ITEM = @item", connection);
                            command_buscar.Parameters.AddWithValue("@item", Convert.ToString(row["ITEM"]));
                            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                            if (reader_buscar.Read())
                            {
                                busqueda_item = Convert.ToString(reader_buscar["ITEM"]); //Almacena ese item EN una variable
                                cantidad_item_tbtecnico = Convert.ToInt32(reader_buscar["CANTIDAD"]);
                                reader_buscar.Close();
                                if (Convert.ToString(row["ITEM"]) == busqueda_item)
                                {
                                    int cantidad_item_consoli = 0;
                                    SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_consolidado WHERE ITEM = @item2", connection);
                                    command_buscar2.Parameters.AddWithValue("@item2", Convert.ToString(row["ITEM"]));
                                    SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                                    if (reader_buscar2.Read())
                                    {
                                        cantidad_item_consoli = Convert.ToInt32(reader_buscar2["CANTIDAD"]);
                                        int nueva_can_consoli = cantidad_item_consoli - Convert.ToInt32(row["CANTIDAD"]);
                                        int nueva_can_tecni = cantidad_item_tbtecnico - Convert.ToInt32(row["CANTIDAD"]);
                                        if (nueva_can_consoli >= 0 && nueva_can_tecni >= 0)
                                        {
                                            //Resta cantidad en BD_Consolidado
                                            reader_buscar2.Close();
                                            string query2 = "UPDATE tbbodega_consolidado SET CANTIDAD = CANTIDAD - "+Convert.ToInt32(row["CANTIDAD"])+" WHERE ITEM ='" + Convert.ToString(row["ITEM"]) + "'";
                                            SqlCommand command_update2 = new SqlCommand(query2, connection);
                                            command_update2.Parameters.AddWithValue("ITEM", "CANTIDAD - " + Convert.ToInt32(row["CANTIDAD"]) + "");
                                            command_update2.ExecuteNonQuery();


                                            //Resta cantidad en bodega tecnico
                                            string queryup = "UPDATE tabla" + cedula + " SET CANTIDAD = CANTIDAD - " + Convert.ToInt32(row["CANTIDAD"]) + " WHERE ITEM ='" + Convert.ToString(row["ITEM"]) + "'";
                                            SqlCommand command_updateup = new SqlCommand(queryup, connection);
                                            command_updateup.Parameters.AddWithValue("ITEM", nueva_can_tecni);
                                            command_updateup.ExecuteNonQuery();


                                            //Registra material en consumible
                                            string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
                                            string query = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD, FECHA_REGISTRO, DIGITADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad, @fecha_registro, @digitador_por)";
                                            SqlCommand command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@cédula", cedula);
                                            command.Parameters.AddWithValue("@nombre", Convert.ToString(row["NOMBRE"]));
                                            command.Parameters.AddWithValue("@cuenta_consumo", Convert.ToString(row["CUENTA"]));
                                            command.Parameters.AddWithValue("@ot", Convert.ToString(row["OT"]));
                                            command.Parameters.AddWithValue("@nodo", Convert.ToString(row["NODO"]));
                                            command.Parameters.AddWithValue("@tipo_trabajo", Convert.ToString(row["TIPO_OT"]));
                                            command.Parameters.AddWithValue("@item", Convert.ToString(row["ITEM"]));
                                            command.Parameters.AddWithValue("@material", Convert.ToString(row["DESCRIPCIÓN"]));
                                            command.Parameters.AddWithValue("@cantidad", Convert.ToString(row["CANTIDAD"]));
                                            command.Parameters.AddWithValue("@fecha_actividad", Convert.ToDateTime(row["FECHA_ACTIVIDAD"]).ToString("dd/MM/yyyy"));
                                            command.Parameters.AddWithValue("@fecha_registro", dateTime);
                                            command.Parameters.AddWithValue("@digitador_por", ClaseCompartida.nombre);
                                            command.ExecuteNonQuery();

                                            string cadena = "INSERT into tbtrasavilidadmaterial (CEDULA, TECNICO, ITEM, MATERIAL, CANTIDAD, TIPO_MOVIMIENTO, fecha) VALUES (@cedula, @tecnico, @item, @material, @cantidad, @tipo_movimiento, @fecha)";
                                            SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                                            sqlCommand.Parameters.AddWithValue("@cedula", cedula);
                                            sqlCommand.Parameters.AddWithValue("@tecnico", Convert.ToString(row["NOMBRE"]));
                                            sqlCommand.Parameters.AddWithValue("@item", busqueda_item);
                                            sqlCommand.Parameters.AddWithValue("@material", Convert.ToString(row["DESCRIPCIÓN"]));
                                            sqlCommand.Parameters.AddWithValue("@cantidad", Convert.ToString(row["CANTIDAD"]));
                                            sqlCommand.Parameters.AddWithValue("@tipo_movimiento", "CONSUMO");
                                            sqlCommand.Parameters.AddWithValue("@fecha", Convert.ToDateTime(row["FECHA_ACTIVIDAD"]).ToString("dd/MM/yyyy") + " | " + ClaseCompartida.nombre);
                                            sqlCommand.ExecuteNonQuery();

                                            string ot = lstomadas.SelectedItem.ToString();

                                            string S = "UPDATE tbregistroconsumosterreno SET ESTADO_REGISTRO = 'CONSUMO CONFIRMADO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                            SqlCommand comando = new SqlCommand(S, connection);
                                            comando.Parameters.AddWithValue("OT", lstomadas.SelectedItem.ToString());
                                            comando.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            reader_buscar2.Close();
                                            string ot = lstomadas.SelectedItem.ToString();
                                            string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'STOCK DEL TÉC. INSUFICIENTE' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                            SqlCommand comando = new SqlCommand(cadena, connection);
                                            comando.Parameters.AddWithValue("OT", "STOCK DEL TÉC. INSUFICIENTE");
                                            comando.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        reader_buscar2.Close();
                                        string ot = lstomadas.SelectedItem.ToString();
                                        string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'ERROR EN CONSOLIDADO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                        SqlCommand comando = new SqlCommand(cadena, connection);
                                        comando.Parameters.AddWithValue("OT", "ERROR EN CONSOLIDADO");
                                        comando.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    string ot = lstomadas.SelectedItem.ToString();
                                    string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'ERROR DESCONOCIDO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                    SqlCommand comando = new SqlCommand(cadena, connection);
                                    comando.Parameters.AddWithValue("OT", "ERROR DESCONOCIDO");
                                    comando.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string ot = lstomadas.SelectedItem.ToString();
                                reader_buscar.Close();
                                string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'CÓDIGO SIN REGISTRO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                SqlCommand comando = new SqlCommand(cadena, connection);
                                comando.Parameters.AddWithValue("OT", "CÓDIGO SIN REGISTRO");
                                comando.ExecuteNonQuery();
                            }
                        }
                        else// CONSUMO EQUIPOS
                        {
                            connection.Close();
                            connection.Open();
                            string busqueda_item = "";
                            string descripcion = "";
                            string estado = "";
                            string serial1 = Convert.ToString(row["ITEM"]).Trim();
                            SqlCommand command_buscar = new SqlCommand("Select * from tabla" + cedula + " WHERE ITEM = @item", connection);
                            command_buscar.Parameters.AddWithValue("@item", serial1);
                            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                            if (reader_buscar.Read())
                            {
                                estado = Convert.ToString(reader_buscar["ESTADO"]);
                                if (estado == "ACEPTADO" || estado == "CONSUMIDO")
                                {
                                    busqueda_item = Convert.ToString(reader_buscar["ITEM"]);
                                    descripcion = Convert.ToString(reader_buscar["MATERIAL"]);
                                    reader_buscar.Close();
                                    string tabla = "tabla" + cedula;
                                    string serial = Convert.ToString(row["ITEM"]);
                                    string cadena = "DELETE from " + tabla + " WHERE ITEM = @itemD";
                                    SqlCommand comando = new SqlCommand(cadena, connection);
                                    comando.Parameters.AddWithValue("@itemD", serial);
                                    comando.ExecuteNonQuery();

                                    //CAMBIA ESTADO EN BDCPES
                                    string query = "UPDATE tbbodega_cpesppal SET ESTADO = 'CONSUMIDO', LUGAR_ASIGNACIÓN = '" + Convert.ToString(row["CUENTA"]) + "' ,ID_LUGAR = '" + Convert.ToString(row["OT"]) + "'  WHERE SERIAL = '" + busqueda_item + "'";
                                    SqlCommand command_update = new SqlCommand(query, connection);
                                    command_update.Parameters.AddWithValue("@CANTIDAD", busqueda_item);
                                    command_update.ExecuteNonQuery();


                                    string dateTime = DateTime.Now.ToString("dd/MM/yyyy");

                                    string queryIN = "INSERT tbbodega_consumo(CÉDULA, NOMBRE, CUENTA_CONSUMO, OT, NODO, TIPO_TRABAJO, ITEM, MATERIAL, CANTIDAD, FECHA_ACTIVIDAD, FECHA_REGISTRO, DIGITADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad, @fecha_registro, @digitador_por)";
                                    SqlCommand command = new SqlCommand(queryIN, connection);
                                    command.Parameters.AddWithValue("@cédula", cedula);
                                    command.Parameters.AddWithValue("@nombre", Convert.ToString(row["NOMBRE"]));
                                    command.Parameters.AddWithValue("@cuenta_consumo", Convert.ToString(row["CUENTA"]));
                                    command.Parameters.AddWithValue("@ot", Convert.ToString(row["OT"]));
                                    command.Parameters.AddWithValue("@nodo", Convert.ToString(row["NODO"]));
                                    command.Parameters.AddWithValue("@tipo_trabajo", Convert.ToString(row["TIPO_OT"]));
                                    command.Parameters.AddWithValue("@item", Convert.ToString(row["ITEM"]));
                                    command.Parameters.AddWithValue("@material", Convert.ToString(row["DESCRIPCIÓN"]));
                                    command.Parameters.AddWithValue("@cantidad", Convert.ToString(row["CANTIDAD"]));
                                    command.Parameters.AddWithValue("@fecha_actividad", Convert.ToDateTime(row["FECHA_ACTIVIDAD"]).ToString("dd/MM/yyyy"));
                                    command.Parameters.AddWithValue("@fecha_registro", dateTime);
                                    command.Parameters.AddWithValue("@digitador_por", ClaseCompartida.nombre);
                                    command.ExecuteNonQuery();

                                    string descripción = Convert.ToString(row["DESCRIPCIÓN"]);
                                    string codigo = Convert.ToString(row["ITEM"]);
                                    string trasavilidad = "insert tbtrasavilidadcpe (CODIGO, DESCRIPCION, SERIAL, ESTADO, LUGAR_ASIGNACIÓN, ID_LUGAR, FECHA_TRANSACCIÓN) VALUES(@codigo, @descripcion, @serial, @estado, @lugar, @idlugar, @fecha)";
                                    SqlCommand sqlCommand = new SqlCommand(trasavilidad, connection);
                                    sqlCommand.Parameters.AddWithValue("@codigo", codigo);
                                    sqlCommand.Parameters.AddWithValue("@descripcion", descripción);
                                    sqlCommand.Parameters.AddWithValue("@serial", Convert.ToString(row["ITEM"]));
                                    sqlCommand.Parameters.AddWithValue("@estado", "CONSUMIDO");
                                    sqlCommand.Parameters.AddWithValue("@lugar", Convert.ToString(row["CUENTA"]));
                                    sqlCommand.Parameters.AddWithValue("@idlugar", Convert.ToString(row["OT"]));
                                    sqlCommand.Parameters.AddWithValue("@fecha", Convert.ToDateTime(row["FECHA_ACTIVIDAD"]).ToString("dd/MM/yyyy") + " | " + ClaseCompartida.nombre);
                                    sqlCommand.ExecuteNonQuery();

                                    string ot = lstomadas.SelectedItem.ToString();
                                    string S = "UPDATE tbregistroconsumosterreno SET ESTADO_REGISTRO = 'CONSUMO CONFIRMADO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                    SqlCommand comando1 = new SqlCommand(S, connection);
                                    comando1.Parameters.AddWithValue("OT", lstomadas.SelectedItem.ToString());
                                    comando1.ExecuteNonQuery();
                                }
                                else
                                {
                                    reader_buscar.Close();
                                    string ot = lstomadas.SelectedItem.ToString();
                                    string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'ASIGNACIÓN SIN ACEPTAR' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                    SqlCommand comando = new SqlCommand(cadena, connection);
                                    comando.Parameters.AddWithValue("OT", "ASIGNACIÓN SIN ACEPTAR");
                                    comando.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                reader_buscar.Close();
                                string ot = lstomadas.SelectedItem.ToString();
                                string cadena = "UPDATE tbregistroconsumosterreno SET ERRORES = 'SIN ASIGNNACIÓN AL TÉCNICO' WHERE OT = '" + ot + "' AND ITEM = '" + Convert.ToString(row["ITEM"]) + "'";
                                SqlCommand comando = new SqlCommand(cadena, connection);
                                comando.Parameters.AddWithValue("OT", "SIN ASIGNNACIÓN AL TÉCNICO");
                                comando.ExecuteNonQuery();
                            }
                        }
                    }
                }
                iniciar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lspendientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void gvBodegaConsumible_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                string n = gvBodegaConsumible.CurrentColumn.Header.ToString();
                if (n == "ITEM")
                {
                    if (Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[11].ToString()) == "EQUIPO")
                    {

                        string serial = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[8].ToString());
                        DialogResult dialogResult = MessageBox.Show("¿Desea eliminar este Serial " + serial + " del consumo?", "Delete", MessageBoxButtons.YesNo);
                        if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            connection.Close();
                            connection.Open();
                            string S = "UPDATE tabla" + Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[1].ToString()) + " SET ESTADO = 'ACEPTADO' WHERE ITEM = '" + Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[8].ToString()) + "'";
                            SqlCommand comando1 = new SqlCommand(S, connection);
                            comando1.Parameters.AddWithValue("ID", Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[1].ToString()));
                            comando1.ExecuteNonQuery();

                            string dl = "DELETE FROM tbregistroconsumosterreno WHERE ID =" + Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                            SqlCommand command = new SqlCommand(dl, connection);
                            command.ExecuteNonQuery();
                            selecpendientes();
                        }

                    }
                    else
                    {
                        abrircambios();
                    }
                }
                else
                {

                    abrircambios();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void abrircambios()
        {
            id_con = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
            cedula = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[1].ToString());
            nombre = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[2].ToString());
            ot = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[3].ToString());
            elemento = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[11].ToString());
            fechaact = Convert.ToString((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[7].ToString());
            CambiosConsuDig cambios = new CambiosConsuDig();
            cambios.ShowDialog();
            selecpendientes();
        }
        private void selecpendientes()
        {
            try
            {
                dt.Clear();
                ot = lstomadas.SelectedItem.ToString();
                SqlCommand command1 = new SqlCommand("Select * from tbregistroconsumosterreno WHERE OT ='" + ot + "' AND ESTADO_REGISTRO = '" + filtro + "'", connection);
                command1.Parameters.AddWithValue("ESTADO_REGITRO", filtro);
                SqlDataReader reader_buscar1 = command1.ExecuteReader();
                if (reader_buscar1.Read())
                {
                    cvnotas.Visibility = Visibility.Visible;
                    string notas = Convert.ToString(reader_buscar1["NOTAS_OT"]);
                    cedula = Convert.ToString(reader_buscar1["CÉDULA"]);
                    reader_buscar1.Close();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    gvBodegaConsumible.ItemsSource = dt.DefaultView;
                    try
                    {
                        gvBodegaConsumible.Columns.RemoveAt(15);
                        gvBodegaConsumible.Columns.RemoveAt(14);
                    }
                    catch (Exception)
                    {
                        gvBodegaConsumible.ItemsSource = dt.DefaultView;
                    }
                    if (notas != "" && notas != null)
                    {
                        txtnotas.Text = notas;
                    }
                    else
                    {
                        txtnotas.Text = "EL TÉCNICO NO DEJO NOTAS";
                    }
                }
                else
                {
                    cvnotas.Visibility = Visibility.Hidden;
                    cedula = "0";
                    dt.Clear();
                }
            }
            catch (Exception x)
            {
                cedula = "0";
                dt.Clear();
                gvBodegaConsumible.ItemsSource = dt.DefaultView;
            }
        }
        private void divicionesmat()
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToString(row["ELEMENTO"]) == "MATERIAL")
                    {
                        int id = Convert.ToInt32(row["ID"]);
                        string ced = Convert.ToString(row["CÉDULA"]);
                        string name = Convert.ToString(row["NOMBRE"]);
                        string ot = Convert.ToString(row["OT"]);
                        string cuenta = Convert.ToString(row["CUENTA"]);
                        string nodo = Convert.ToString(row["NODO"]);
                        string tipot = Convert.ToString(row["TIPO_OT"]);
                        string f_actividad = Convert.ToString(row["FECHA_ACTIVIDAD"]);
                        string item = Convert.ToString(row["ITEM"]);
                        string material = Convert.ToString(row["DESCRIPCIÓN"]);
                        int can = Convert.ToInt32(row["CANTIDAD"]);
                        string elemento = "MATERIAL";
                        string estado = Convert.ToString(row["ESTADO_REGISTRO"]);
                        string logis = Convert.ToString(row["TOMADO_POR"]);
                        switch (tipot.Trim())
                        {
                            /*"SI NIEMCA (INSTALACIONES SIN CABLE MENSAJERO) CONSUME MÁS DE 18 MTS DE CABLE 1029784-1 (CABLE COAXIAL RG -6 (BLANCO) ENTONCES EL RESTANTE SE CONSUME CON NDDVNC."
                            "SI NIEMCA (INSTALACIONES SIN CABLE MENSAJERO) CONSUME MÁS DE 18 MTS DE CABLE 1029783-1 (CABLE COAXIAL RG -6 (NEGRO) ENTONCES EL RESTANTE SE CONSUME CON NDDVNC."
                            "SI NIEMCA (INSTALACIONES SIN CABLE MENSAJERO) CONSUME MÁS DE 18 MTS DE CABLE 1029789-1 (CABLE COAXIAL RG -6 CON/MEN) ENTONCES EL RESTANTE SE CONSUME CON NDDVNC."
                            "SI NIEMCA (INSTALACIONES SIN CABLE MENSAJERO) CONSUME MÁS DE 12 MTS DE CABLE 1023462-1 (CONECTOR RG-6) ENTONCES EL RESTANTE SE CONSUME CON NDDVNC."*/
                            case "NIEMCA":

                                if (item == "1029784-1" || item == "1029784-2" || item == "1029783-1" || item == "1029783-2" || item == "1029789-1" || item == "1029789-2")
                                {
                                    if (can > 18)
                                    {
                                        string tipot_divi = "NDDVNC";
                                        t_divicion = 18;
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1023462-1" || item == "1023462-1")
                                {
                                    if (can > 12)
                                    {
                                        string tipot_divi = "NDDVNC";
                                        t_divicion = 12;
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1025507-1" || item == "1025507-2" || item == "1020498-1" || item == "1020498-2" || item == "1025629-1" || item == "1025629-2")
                                {
                                    string tipot_n = "NTODAT";
                                    divicion_tipoot(id, tipot_n);
                                    if (item == "1025507-1" || item == "1025507-2")
                                    {
                                        if (can > 18)
                                        {
                                            string tipot_divi = "NDDVNC";
                                            t_divicion = 18;
                                            divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                        }
                                    }
                                    else if (item == "1020498-1" || item == "1020498-2")
                                    {
                                        if (can > 12)
                                        {
                                            string tipot_divi = "NDDVNC";
                                            t_divicion = 12;
                                            divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                        }
                                    }
                                }
                                break;
                            case "NIEMNC":
                                if (item == "1029784-1" || item == "1029784-2" || item == "1029783-1" || item == "1029783-2" || item == "1029789-1" || item == "1029789-2")
                                {
                                    if (can > 18)
                                    {
                                        t_divicion = 18;
                                        string tipot_divi = "NDDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1023462-1" || item == "1023462-1")
                                {
                                    if (can > 12)
                                    {
                                        t_divicion = 12;
                                        string tipot_divi = "NDDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1025507-1" || item == "1025507-2" || item == "1020498-1" || item == "1020498-2" || item == "1025629-1" || item == "1025629-2")
                                {
                                    string tipot_n = "NTODAT";
                                    divicion_tipoot(id, tipot_n);
                                    if (item == "1025507-1" || item == "1025507-2")
                                    {
                                        if (can > 18)
                                        {
                                            string tipot_divi = "NDDVNC";
                                            t_divicion = 18;
                                            divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                        }
                                    }
                                    else if (item == "1020498-1" || item == "1020498-2")
                                    {
                                        if (can > 12)
                                        {
                                            string tipot_divi = "NDDVNC";
                                            t_divicion = 12;
                                            divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                        }
                                    }
                                }
                                break;

                            case "NTEMCA":
                                if (item == "1029784-1" || item == "1029784-2" || item == "1029783-1" || item == "1029783-2" || item == "1029789-1" || item == "1029789-2")
                                {
                                    if (can > 18)
                                    {
                                        t_divicion = 18;
                                        string tipot_divi = "NTDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1023462-1" || item == "1023462-1")
                                {
                                    if (can > 12)
                                    {
                                        t_divicion = 12;
                                        string tipot_divi = "NTDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                break;
                            case "NTEMNC":
                                if (item == "1029784-1" || item == "1029784-2" || item == "1029783-1" || item == "1029783-2" || item == "1029789-1" || item == "1029789-2")
                                {
                                    if (can > 18)
                                    {
                                        t_divicion = 18;
                                        string tipot_divi = "NTDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                else if (item == "1023462-1" || item == "1023462-1")
                                {
                                    if (can > 12)
                                    {
                                        t_divicion = 12;
                                        string tipot_divi = "NTDVNC";
                                        divicion_can(id, tipot_divi, ced, name, ot, cuenta, nodo, tipot, f_actividad, item, material, can, elemento, estado, logis);
                                    }
                                }
                                break;
                            case "NDDNCP":
                                if (item == "1025507-1" || item == "1025507-2" || item == "1020498-1" || item == "1020498-2" || item == "1025629-1" || item == "1025629-2")
                                {
                                    string tipot_n = "NTODAP";
                                    divicion_tipoot(id, tipot_n);
                                }
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            selecpendientes();
        }
        public void divicion_can(int id, string tipot_divi, string ced, string name, string ot, string cuenta, string nodo, string tipot, string f_actividad, string item, string material, int can, string elemento, string estado, string logis)
        {
            int can_res_divicion = 0;
            if (t_divicion == 18)
            {
                can_res_divicion = can - 18;
            }
            else if (t_divicion == 12)
            {
                can_res_divicion = can - 12;
            }
            connection.Close();
            connection.Open();
            string queryup = "UPDATE tbregistroconsumosterreno SET CANTIDAD = '" + t_divicion + "' Where ID ='" + id + "'";
            SqlCommand command_updateup = new SqlCommand(queryup, connection);
            command_updateup.Parameters.AddWithValue("CANTIDAD", t_divicion);
            command_updateup.ExecuteNonQuery();

            string query = "INSERT tbregistroconsumosterreno (CÉDULA, NOMBRE, CUENTA, OT, NODO, TIPO_OT, ITEM, DESCRIPCIÓN, CANTIDAD, FECHA_ACTIVIDAD, ELEMENTO, ESTADO_REGISTRO, TOMADO_POR) VALUES(@cédula, @nombre, @cuenta_consumo, @ot, @nodo, @tipo_trabajo, @item, @material, @cantidad, @fecha_actividad,  @elemento, @estado, @tomado)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@cédula", ced);
            command.Parameters.AddWithValue("@nombre", name);
            command.Parameters.AddWithValue("@cuenta_consumo", cuenta);
            command.Parameters.AddWithValue("@ot", ot);
            command.Parameters.AddWithValue("@nodo", nodo);
            command.Parameters.AddWithValue("@tipo_trabajo", tipot_divi);
            command.Parameters.AddWithValue("@item", item);
            command.Parameters.AddWithValue("@material", material);
            command.Parameters.AddWithValue("@cantidad", can_res_divicion);
            command.Parameters.AddWithValue("@fecha_actividad", Convert.ToDateTime(f_actividad).ToString("dd/MM/yyyy"));
            command.Parameters.AddWithValue("@elemento", elemento);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@tomado", logis);
            command.ExecuteNonQuery();
        }
        public void divicion_tipoot(int id, string tipoot_n)
        {
            connection.Close();
            connection.Open();
            string queryup = "UPDATE tbregistroconsumosterreno SET TIPO_OT = '" + tipoot_n + "' Where ID ='" + id + "'";
            SqlCommand command_updateup = new SqlCommand(queryup, connection);
            command_updateup.Parameters.AddWithValue("TIPO_OT", tipoot_n);
            command_updateup.ExecuteNonQuery();
        }
        private void btnok_Copy_Click(object sender, RoutedEventArgs e)
        {
            divicionesmat();
        }
        private void gvBodegaConsumible_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show((gvBodegaConsumible.CurrentItem as DataRowView).Row.ItemArray[9].ToString());
        }
        private void btnbodetec_Click(object sender, RoutedEventArgs e)
        {
            if (cedula != "0")
            {
                flagt.flagtec = 1;
                flagt.ced = cedula;
                BodegaTec tec = new BodegaTec();
                tec.ShowDialog();
            }
        }
        private void btnbodcpes_Click(object sender, RoutedEventArgs e)
        {
            BodegaCpes cpe = new BodegaCpes();
            cpe.ShowDialog();
        }
        private void lstecnicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lspendientes.Items.Clear();
                string nametec = Convert.ToString(lstecnicos.SelectedItem);
                SqlCommand command2 = new SqlCommand("Select DISTINCT OT, TOMADO_POR from tbregistroconsumosterreno where ESTADO_REGISTRO = '" + filtro + "' AND NOMBRE = '" + nametec + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    string ot = Convert.ToString(row["OT"]);
                    string tomada_por = Convert.ToString(row["TOMADO_POR"]);
                    int index = lstomadas.Items.IndexOf(ot);
                    if (index < 0)
                    {
                        if (tomada_por == "")
                        {
                            lspendientes.Items.Add(ot);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void btnrechazarot_Click(object sender, RoutedEventArgs e)
        {
            string motivo = Microsoft.VisualBasic.Interaction.InputBox("Ingrese motivos de la devolución de la OT", "CÉDULA");
            if (motivo == null || motivo == "")
            {
                MessageBox.Show("Cancelado");
            }
            else
            {
                string queryup = "UPDATE tbregistroconsumosterreno SET ERRORES = '" + motivo + "', ESTADO_REGISTRO = 'RECHAZADO' Where OT ='" + ot + "'";
                SqlCommand command_updateup = new SqlCommand(queryup, connection);
                command_updateup.Parameters.AddWithValue("ERRORES", motivo);
                command_updateup.Parameters.AddWithValue("ESTADO_REGISTRO", "RECHAZADO");
                command_updateup.ExecuteNonQuery();
                MessageBox.Show("OT rechazada");
                iniciar();
            }
        }
        private void btnporvalidar_Click(object sender, RoutedEventArgs e)
        {
            filtro = "POR VALIDAR";
            btnrechazarot.IsEnabled = true;
            btnrechazarot.Visibility = Visibility.Visible;
            iniciar();
        }
        private void btnrechazadas_Click(object sender, RoutedEventArgs e)
        {
            filtro = "RECHAZADO";
            btnrechazarot.Visibility = Visibility.Hidden;
            iniciar();
        }
        public void contadores()
        {
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select DISTINCT OT from tbregistroconsumosterreno where ESTADO_REGISTRO = 'POR VALIDAR'", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            lbcontpendi.Content = dataTable2.Rows.Count;


            SqlCommand command = new SqlCommand("Select DISTINCT OT from tbregistroconsumosterreno where ESTADO_REGISTRO = 'RECHAZADO'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            lbcontrecha.Content = dataTable.Rows.Count;
        }
        private void lstomadas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string btn = Convert.ToString(MessageBox.Show("¿Desea devolver la OT para ser revisada por otro digitador?", "CREAR USUARIO", MessageBoxButtons.YesNoCancel));
            if (btn == "Yes")
            {
                connection.Close();
                connection.Open();
                string ot = lstomadas.SelectedItem.ToString();
                int pos = lstomadas.SelectedIndex;
                lstomadas.Items.RemoveAt(pos);
                string cadena = "UPDATE tbregistroconsumosterreno SET TOMADO_POR = NULL WHERE OT = '" + ot + "'";
                SqlCommand comando = new SqlCommand(cadena, connection);
                comando.Parameters.AddWithValue("OT", "NULL");
                comando.ExecuteNonQuery();
                lsnametec(Convert.ToDateTime(pfechaact.Text).ToString("dd/MM/yyyy"));
            }
            else
            {
                MessageBox.Show("Cancelado");
            }
        }
        private void pfechaact_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            iniciar();
        }
    }
}


