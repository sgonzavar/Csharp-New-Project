using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Personal.xaml
    /// </summary>
    public partial class Personal : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        SqlConnection cn = new SqlConnection("Data Source=192.168.4.55;Initial Catalog=LINEACOM;User ID=USERLCCA;Password=exUs3rLcc4.2021*+");
        public Personal()
        {
            InitializeComponent();
            actualizarpersonal();
            inicio();
            estadistica();
        }
        public void inicio()
        {
            gv.Visibility = Visibility.Hidden;
            cbxname.Items.Clear();
            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select IDENTIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO, NOMBRE_PILA from EMPLEADOS_PPAL  WHERE ESTADO_EMPLEADO = 'ACT'", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            dgviewfile.ItemsSource = dataTable22.DefaultView;
            foreach (DataRow row in dataTable22.Rows)
            {
                string nombrecompleto = Convert.ToString(row["NOMBRE_PILA"]) + " " + Convert.ToString(row["PRIMER_APELLIDO"]) + " " + Convert.ToString(row["SEGUNDO_APELLIDO"]);
                cbxname.Items.Add(nombrecompleto);
                cbxced.Items.Add(Convert.ToString(row["IDENTIFICACION"]));
            }
        }
        public void actualizarpersonal()
        {
            try
            {
#pragma warning disable CS0219 // La variable 'cont' está asignada pero su valor nunca se usa
                int cont = 0;
#pragma warning restore CS0219 // La variable 'cont' está asignada pero su valor nunca se usa
                connection.Close();
                connection.Open();
                cn.Close();
                cn.Open();
                SqlCommand command22 = new SqlCommand("Select *  from LINEACOM.EMPLEADO WHERE DEPARTAMENTO = 'CLARO'", cn);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command22;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                foreach (DataRow row in dataTable22.Rows)
                {
                    SqlCommand command_buscar = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE IDENTIFICACION = @id", connection);
                    command_buscar.Parameters.AddWithValue("@id", Convert.ToString(row["IDENTIFICACION"]));
                    SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                    if (reader_buscar.Read())
                    {
                        if (Convert.ToString(row["ESTADO_EMPLEADO"]) != Convert.ToString(reader_buscar["ESTADO_EMPLEADO"]))
                        {
                            reader_buscar.Close();
                            string query = "UPDATE EMPLEADOS_PPAL SET ESTADO_EMPLEADO = '" + Convert.ToString(row["ESTADO_EMPLEADO"]) + "'  WHERE IDENTIFICACION = '" + Convert.ToString(row["IDENTIFICACION"]) + "'";
                            SqlCommand command_update2 = new SqlCommand(query, connection);
                            command_update2.Parameters.AddWithValue("IDENTIFICACION", Convert.ToString(row["IDENTIFICACION"]));
                            command_update2.ExecuteNonQuery();

                        }
                        if (Convert.ToString(row["PUESTO"]) != Convert.ToString(row["PUESTO"]))
                        {
                            reader_buscar.Close();
                            string query = "UPDATE EMPLEADOS_PPAL SET PUESTO = '" + Convert.ToString(row["PUESTO"]) + "'  WHERE IDENTIFICACION = '" + Convert.ToString(row["PUESTO"]) + "'";
                            SqlCommand command_update2 = new SqlCommand(query, connection);
                            command_update2.Parameters.AddWithValue("IDENTIFICACION", Convert.ToString(row["IDENTIFICACION"]));
                            command_update2.ExecuteNonQuery();
                        }
                        reader_buscar.Close();
                    }
                    else
                    {
                        reader_buscar.Close();

                        byte[] datos = new byte[0];
                        datos = (byte[])row["FOTOGRAFIA"];

                        string query = "INSERT EMPLEADOS_PPAL (IDENTIFICACION, NOMBRE, ESTADO_EMPLEADO, FECHA_INGRESO, PUESTO, ESTADO_PROYECTO, DIRECCION_HAB, TELEFONO1, FOTOGRAFIA, PRIMER_APELLIDO, SEGUNDO_APELLIDO, NOMBRE_PILA, E_MAIL, SUPERVISOR, U_TALLA_CAMISA, U_TALLA_PANTALON, U_TALLA_ZAPATOS, U_TALLA_CHALECO, U_TALLA_DELANTAL) values(@identificacion, @nombre, @estado_empleado, @fecha_ingreso, @puesto, @estado_proyecto, @direccion_hab, @telefono1, @fotografia, @primer_apellido, @segundo_apellido, @nombre_pila, @e_mail, @supervisor, @u_talla_camisa, @u_talla_pantalon, @u_talla_zapatos, @u_talla_chaleco, @u_talla_delantal)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@identificacion", Convert.ToString(row["IDENTIFICACION"]));
                        command.Parameters.AddWithValue("@nombre", Convert.ToString(row["NOMBRE"]));
                        command.Parameters.AddWithValue("@estado_empleado", Convert.ToString(row["ESTADO_EMPLEADO"]));
                        command.Parameters.AddWithValue("@fecha_ingreso", Convert.ToString(row["FECHA_INGRESO"]));
                        command.Parameters.AddWithValue("@puesto", Convert.ToString(row["PUESTO"]));
                        command.Parameters.AddWithValue("@estado_proyecto", "NUEVO INGRESO");
                        command.Parameters.AddWithValue("@direccion_hab", Convert.ToString(row["DIRECCION_HAB"]));
                        command.Parameters.AddWithValue("@telefono1", Convert.ToString(row["TELEFONO1"]));
                        // command.Parameters.AddWithValue("@fotografia", datos);
                        command.Parameters.AddWithValue("@primer_apellido", Convert.ToString(row["PRIMER_APELLIDO"]));
                        command.Parameters.AddWithValue("@segundo_apellido", Convert.ToString(row["SEGUNDO_APELLIDO"]));
                        command.Parameters.AddWithValue("@nombre_pila", Convert.ToString(row["NOMBRE_PILA"]));
                        command.Parameters.AddWithValue("@e_mail", Convert.ToString(row["E_MAIL"]));
                        command.Parameters.AddWithValue("@supervisor", "POR ASIGNAR");
                        command.Parameters.AddWithValue("@u_talla_camisa", Convert.ToString(row["U_TALLA_CAMISA"]));
                        command.Parameters.AddWithValue("@u_talla_pantalon", Convert.ToString(row["U_TALLA_PANTALON"]));
                        command.Parameters.AddWithValue("@u_talla_zapatos", Convert.ToString(row["U_TALLA_ZAPATOS"]));
                        command.Parameters.AddWithValue("@u_talla_chaleco", Convert.ToString(row["U_TALLA_CHALECO"]));
                        command.Parameters.AddWithValue("@u_talla_delantal", Convert.ToString(row["U_TALLA_DELANTAL"]));


                        command.Parameters.Add("@fotografia", System.Data.SqlDbType.Image);
                        command.Parameters["@fotografia"].Value = datos;

                        command.ExecuteNonQuery();

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base de datos no actualizada, debe conectar la VPN para estar al día" + Convert.ToString(ex), "Failed");
            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void estadistica()
        {
            int personalac = 0;
            int personalop = 0;
            int tec = 0;
            int aux = 0;
            int nuevos = 0;
            int formacion = 0;
            int alturas = 0;
            int dotacion = 0;
            int retiros = 0;
            int retirostec = 0;
            int retirosaux = 0;
            int retirossup = 0;

            connection.Close();
            connection.Open();
            SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL", connection);
            SqlDataAdapter adapter22 = new SqlDataAdapter();
            adapter22.SelectCommand = command22;
            DataTable dataTable22 = new DataTable();
            adapter22.Fill(dataTable22);
            foreach (DataRow row in dataTable22.Rows)
            {
                string estado = Convert.ToString(row["ESTADO_EMPLEADO"]);
                string puesto = Convert.ToString(row["PUESTO"]);
                string estadopr = Convert.ToString(row["ESTADO_PROYECTO"]);
                if (estado == "ACT")
                {
                    personalac += 1;
                    if (puesto == "TEC1,2Y3" || puesto == "TEC INSB" || puesto == "TECN POS" || puesto == "TEC MSB")
                    {
                        tec += 1;
                        personalop += 1;
                    }
                    else if (puesto == "AUX TEC" || puesto == "AUX COND")
                    {
                        aux += 1;
                        personalop += 1;
                    }

                    if (estadopr == "NUEVO INGRESO")
                    {
                        nuevos += 1;
                    }
                    else if (estadopr == "EN ALTURAS" || estadopr == "CERTIFICADO")
                    {
                        alturas += 1;
                    }
                    else if (estadopr == "ENTREGA DOTACIÓN")
                    {
                        dotacion += 1;
                    }
                }
                else
                {
                    retiros += 1;
                    if (puesto == "TEC1,2Y3" || puesto == "TEC INSB" || puesto == "TECN POS" || puesto == "TEC MSB")
                    {
                        retirostec += 1;
                    }
                    else if (puesto == "AUX TEC" || puesto == "AUX COND")
                    {
                        retirosaux += 1;
                    }
                    else if (puesto == "SUP HFC")
                    {
                        retirossup += 1;
                    }
                }

            }
            lbtotalpersonal.Content = "TOTAL PERSONAL: " + personalac;
            lbtotaloperativo.Content = "TOTAL OPERATIVOS: " + personalop;
            lbtotaltec.Content = "TOTAL TÉCNICOS: " + tec;
            lbtotaltec_Copy.Content = "TOTAL AUXILIARES: " + aux;
            lbretiros_Copy.Content = "TOTAL EN INGRESO: " + nuevos;
            lbretirostec_Copy.Content = "TOTAL FORMACIÓN: " + formacion;
            lbretirosaux_Copy.Content = "TOTAL EN ALTURAS: " + alturas;
            lbretirossup_Copy.Content = "TOTAL DOTACIÓN: " + dotacion;
            lbretiros.Content = "TOTAL RETIRADOS: " + retiros;
            lbretirostec.Content = "TOTAL RETIROS TEC: " + retirostec;
            lbretirosaux.Content = "TOTAL RETIROS AUX: " + retirosaux;
            lbretirossup.Content = "TOTAL RETIROS SUP: " + retirossup;

        }
        private void cbxname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbxced.SelectedIndex = cbxname.SelectedIndex;
                string ced = cbxced.SelectedItem.ToString();
                int cedu = Convert.ToInt32(ced);
                connection.Close();
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE IDENTIFICACION = " + cedu + "", connection);
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                if (dataReader1.Read())
                {
                    lbcelular.Content = Convert.ToString(dataReader1["TELEFONO1"]);
                    lbdireccion.Content = Convert.ToString(dataReader1["DIRECCION_HAB"]);
                    lbemail.Content = Convert.ToString(dataReader1["E_MAIL"]);
                    lbfecha.Content = Convert.ToString(dataReader1["FECHA_INGRESO"]);
                    lbid.Content = Convert.ToString(dataReader1["IDENTIFICACION"]);
                    lbsuper.Content = Convert.ToString(dataReader1["SUPERVISOR"]);
                    lbpuesto.Content = Convert.ToString(dataReader1["PUESTO"]);
                    lbcamisa.Content = Convert.ToString(dataReader1["U_TALLA_CAMISA"]);
                    lbpantalon.Content = Convert.ToString(dataReader1["U_TALLA_PANTALON"]);
                    lbzapatos.Content = Convert.ToString(dataReader1["U_TALLA_ZAPATOS"]);
                    lbchaleco.Content = Convert.ToString(dataReader1["U_TALLA_CHALECO"]);
                    lbestado.Content = Convert.ToString(dataReader1["ESTADO_EMPLEADO"]);
                    lbprocesoac.Content = Convert.ToString(dataReader1["ESTADO_PROYECTO"]);
                }
                dataReader1.Close();
                connection.Close();
            }
            catch (Exception)
            {

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btnllego_Click(object sender, RoutedEventArgs e)
        {


            lbcelular.Content = "";
            lbdireccion.Content = "";
            lbemail.Content = "";
            lbfecha.Content = "";
            lbid.Content = "";
            lbsuper.Content = "";
            lbpuesto.Content = "";
            lbcamisa.Content = "";
            lbpantalon.Content = "";
            lbzapatos.Content = "";
            lbchaleco.Content = "";
            lbestado.Content = "";
            lbprocesoac.Content = "";
            connection.Close();
            connection.Open();
            cbxname.Items.Clear();
            cbxced.Items.Clear();
            if (cbxestado.Text.ToString() == "ACTIVOS")
            {
                inicio();
            }
            else if (cbxestado.Text.ToString() == "RETIRADOS")
            {
                SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_EMPLEADO = 'INAC'", connection);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command22;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                foreach (DataRow row in dataTable22.Rows)
                {

                    if (Convert.ToString(row["ESTADO_EMPLEADO"]) == "INAC")
                    {
                        string nombrecompleto = Convert.ToString(row["NOMBRE_PILA"]) + " " + Convert.ToString(row["PRIMER_APELLIDO"]) + " " + Convert.ToString(row["SEGUNDO_APELLIDO"]);
                        cbxname.Items.Add(nombrecompleto);
                        cbxced.Items.Add(Convert.ToString(row["IDENTIFICACION"]));
                    }
                }
            }
            else
            {
                SqlCommand command22 = new SqlCommand("Select * from EMPLEADOS_PPAL WHERE ESTADO_PROYECTO = '" + cbxestado.Text.ToString() + "'", connection);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command22;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                foreach (DataRow row in dataTable22.Rows)
                {

                    if (Convert.ToString(row["ESTADO_EMPLEADO"]) == "ACT")
                    {
                        string nombrecompleto = Convert.ToString(row["NOMBRE_PILA"]) + " " + Convert.ToString(row["PRIMER_APELLIDO"]) + " " + Convert.ToString(row["SEGUNDO_APELLIDO"]);
                        cbxname.Items.Add(nombrecompleto);
                        cbxced.Items.Add(Convert.ToString(row["IDENTIFICACION"]));
                    }

                }
            }
        }

        private void btnexportar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                SqlCommand command1 = new SqlCommand("Select * from EMPLEADOS_PPAL", connection);
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
    }
}
