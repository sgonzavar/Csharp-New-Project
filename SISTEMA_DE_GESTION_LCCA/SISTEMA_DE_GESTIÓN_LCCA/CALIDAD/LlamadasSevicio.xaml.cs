using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SISTEMA_DE_GESTIÓN_LCCA.CALIDAD
{
    /// <summary>
    /// Lógica de interacción para LlamadasSevicio.xaml
    /// </summary>
    public partial class LlamadasSevicio : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        SqlConnection connectiontec = new SqlConnection(conexion.cadena);
        List<listec> _listec = new List<listec>();
        List<listsup> _listsup = new List<listsup>();
        int cedulaselect = 0;
        int id_llamada = 0;
        int id_visita = 0;
        int vezvisita = 1;
        public LlamadasSevicio()
        {
            InitializeComponent();
            iniciar();
        }
        public void iniciar()
        {
            txtvezllamada.IsEnabled = false;
            VISITA.Visibility = Visibility.Hidden;
            txtcontacto.IsEnabled = false;
            txtdireccion.IsEnabled = false;
            pickfechaevento.IsEnabled = false;
            txtactividad.IsEnabled = false;
            //btn_.IsEnabled = false;
            //btn__Copy.IsEnabled = false;
            txtnotasllamada.IsEnabled = false;
            rdyes.IsEnabled = false;
            rdno.IsEnabled = false;
            cbxtipollamada.IsEnabled = false;
            cbxtecnico_Copy.IsEnabled = false;
            txtcuenta.IsEnabled = false;
            txtnamecliente.IsEnabled = false;
            cargatec(_listec, _listsup);
            idvisita();
        }
        public void desbloquear()
        {
            txtcontacto.IsEnabled = true;
            txtdireccion.IsEnabled = true;
            pickfechaevento.IsEnabled = true;
            txtactividad.IsEnabled = true;
            //btn_.IsEnabled = true;
            //btn__Copy.IsEnabled = true;
            txtnotasllamada.IsEnabled = true;
            rdyes.IsEnabled = true;
            rdno.IsEnabled = true;
            cbxtipollamada.IsEnabled = true;
            cbxtecnico.IsEnabled = true;
            cbxtecnico_Copy.IsEnabled = true;
            txtcuenta.IsEnabled = true;
            txtnamecliente.IsEnabled = true;
        }
        public void cargatec(List<listec> _listec, List<listsup> _listsup)
        {
            cbxsupervisores.Items.Clear();
            cbxtecnico.Items.Clear();
            cbxtecnico_Copy.Items.Clear();
            connectiontec.Close();
            connectiontec.Open();
            SqlCommand command = new SqlCommand("Select * from tbloginstecnicos", connectiontec);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                if (Convert.ToString(row["cargo"]) != "SUPERVISOR")
                {
                    listec listec = new listec();
                    listec.NOMBRE = Convert.ToString(row["nombre"]);
                    listec.CEDULA = Convert.ToInt32(row["usuario"]);
                    _listec.Add(listec);
                    cbxtecnico.Items.Add(Convert.ToString(row["nombre"]));
                    cbxtecnico_Copy.Items.Add(Convert.ToString(row["nombre"]));
                }
                else if (Convert.ToString(row["cargo"]) == "SUPERVISOR")
                {
                    listsup listsup = new listsup();
                    listsup.NOMBRE = Convert.ToString(row["nombre"]);
                    listsup.CEDULA = Convert.ToInt32(row["usuario"]);
                    _listsup.Add(listsup);
                    cbxsupervisores.Items.Add(Convert.ToString(row["nombre"]));
                }
            }

        }
        private void txtvezllamada_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void cbxsupervisores_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbxsupervisores.IsDropDownOpen = true;
        }
        private void cbxtecnico_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbxtecnico.IsDropDownOpen = true;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            VISITA.Visibility = Visibility.Hidden;
        }
        private void txtcontacto_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
        private void btn__Click(object sender, RoutedEventArgs e)
        {
            if (txtvezllamada.Text == null || txtvezllamada.Text == "")
            {
                int s = 0;
                s += 1;
                txtvezllamada.Text = Convert.ToString(s);
            }
            else
            {
                int s = Convert.ToInt32(txtvezllamada.Text);
                s += 1;
                txtvezllamada.Text = Convert.ToString(s);
            }
        }
        private void btn__Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtvezllamada.Text) >= 1)
                {
                    int s = Convert.ToInt32(txtvezllamada.Text);
                    s += -1;
                    txtvezllamada.Text = Convert.ToString(s);
                }
            }
            catch (Exception)
            {
                int s = 0;
                s += 1;
                txtvezllamada.Text = Convert.ToString(s);
            }
        }
        private void searchidorden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand command2 = new SqlCommand("Select * from Llamadas WHERE NUMERO_ORDEN = '" + txtidorden.Text + "'", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = command2;
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                int count = dataTable2.Rows.Count;
                if (count >= 1)
                {
                    int vezllamada = count;

                    SqlCommand sqlCommand1 = new SqlCommand("Select * from Llamadas WHERE NUMERO_ORDEN = '" + txtidorden.Text + "'", connection);
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    if (dataReader1.Read())
                    {
                        txtcuenta.Text = Convert.ToString(dataReader1["CUENTA"]);
                        txtnamecliente.Text = Convert.ToString(dataReader1["NOMBRE_CLIENTE"]);
                        txtcontacto.Text = Convert.ToString(dataReader1["NUM_CONTACTO"]);
                        txtdireccion.Text = Convert.ToString(dataReader1["DIRECCIÓN"]);
                        pickfechaevento.Text = Convert.ToString(dataReader1["FECHA_EVENTO"]);
                        txtactividad.Text = Convert.ToString(dataReader1["TIPO_ACTIVIDAD"]);
                        txtvezllamada.Text = Convert.ToString(vezllamada);
                        txtnotasllamada.Text = Convert.ToString(dataReader1["NOTA"]);
                    }
                    dataReader1.Close();
                    desbloquear();
                }
                else
                {
                    MessageBox.Show("Orden sin llamadas", "Sin registro");
                    txtvezllamada.Text = "0";
                    desbloquear();
                }

                connection.Close();
                connection.Open();
                SqlCommand command22 = new SqlCommand("Select * from Agendamiento WHERE NUMERO_ORDEN = '" + txtidorden.Text + "'", connection);
                SqlDataAdapter adapter22 = new SqlDataAdapter();
                adapter22.SelectCommand = command2;
                DataTable dataTable22 = new DataTable();
                adapter22.Fill(dataTable22);
                int count1 = dataTable22.Rows.Count;
                if (count1 >= 1)
                {
                    vezvisita = count;
                }
            }
            catch (Exception)
            {

            }
        }
        private void rdyes_Checked(object sender, RoutedEventArgs e)
        {
            VISITA.Visibility = Visibility.Visible;
        }
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            connection.Open();
            idvisita();
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            string Dateh = DateTime.Now.ToString("hh:mm:tt");
            if (rdyes.IsChecked == true)
            {
                try
                {

                    //INSERTAMOS DATOS EN TABLA LLAMADAS//
                    string query = "INSERT Llamadas (ID_LLAMADA, CUENTA, NUMERO_ORDEN, NOMBRE_CLIENTE, NUM_CONTACTO, DIRECCIÓN, TECNICO, FECHA_EVENTO, TIPO_ACTIVIDAD, VEZ_LLAMADA, NOTA, HORA_REGISTRO, FECHA_REGISTRO,TIPO_LLAMADA, VISITA, ID_VISITA) VALUES(@id_llamada, @cuenta, @numero_orden, @nombre_cliente, @num_contacto, @dirección, @tecnico, @fecha_evento, @tipo_actividad, @vez_llamada, @nota, @horar, @fechar, @tipo_llamada, @visita, @id_visita)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id_llamada", id_llamada + 1);
                    command.Parameters.AddWithValue("@cuenta", txtcuenta.Text);
                    command.Parameters.AddWithValue("@numero_orden", txtidorden.Text);
                    command.Parameters.AddWithValue("@nombre_cliente", txtnamecliente.Text);
                    command.Parameters.AddWithValue("@num_contacto", txtcontacto.Text);
                    command.Parameters.AddWithValue("@dirección", txtdireccion.Text);
                    command.Parameters.AddWithValue("@tecnico", cbxtecnico_Copy.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@fecha_evento", pickfechaevento.Text);
                    command.Parameters.AddWithValue("@tipo_actividad", txtactividad.Text);
                    command.Parameters.AddWithValue("@vez_llamada", Convert.ToInt32(txtvezllamada.Text) + 1);
                    command.Parameters.AddWithValue("@nota", txtnotasllamada.Text);
                    command.Parameters.AddWithValue("@horar", Dateh);
                    command.Parameters.AddWithValue("@fechar", Date);
                    command.Parameters.AddWithValue("@tipo_llamada", cbxtipollamada.Text);

                    command.Parameters.AddWithValue("@visita", "TRUE");
                    command.Parameters.AddWithValue("@id_visita", id_visita + 1);

                    string updateidllamada = "UPDATE ids SET ID_LLAMADAS = " + (id_llamada + 1) + " WHERE ID_LLAMADAS = " + id_llamada + "";
                    SqlCommand command_updateup = new SqlCommand(updateidllamada, connection);
                    command_updateup.Parameters.AddWithValue("ID_LLAMADAS", id_llamada + 1);

                    string query2 = "INSERT Agendamiento (ID_VISITA, ID_LLAMADA, SUPERVISOR, ID_TECNICO, TECNICO, HORA_NOTIFICACION, FECHA_AGENDA, FRANJA_VISITA, NOTAS_VISITA, VEZ_VISITA, ESTADO_VISITA) VALUES(@id_visita, @id_llamada, @supervisor, @id_tecnico, @tecnico, @hora_notificacion, @fecha_agenda, @franja_visita, @notas_visita, @vez_visita, @estado_visita)";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@id_visita", id_visita + 1);
                    command2.Parameters.AddWithValue("@id_llamada", id_llamada + 1);
                    command2.Parameters.AddWithValue("@supervisor", cbxsupervisores.Text);
                    command2.Parameters.AddWithValue("@id_tecnico", cedulaselect);
                    command2.Parameters.AddWithValue("@tecnico", cbxtecnico.Text);
                    command2.Parameters.AddWithValue("@hora_notificacion", txthora.Text + ":" + txtminutos.Text + " " + cbxfranja.Text);
                    command2.Parameters.AddWithValue("@fecha_agenda", pickfechavisita.Text);
                    command2.Parameters.AddWithValue("@franja_visita", cbxfranjavisitainicial.Text + " a " + cbxfranjavisitafinal.Text);
                    command2.Parameters.AddWithValue("@notas_visita", txtnotasvisita.Text);
                    command2.Parameters.AddWithValue("@vez_visita", vezvisita);
                    command2.Parameters.AddWithValue("@estado_visita", "AGENDADO");

                    string updatevisitas = "UPDATE ids SET ID_VISITAS = " + (id_visita + 1) + " WHERE ID_VISITAS = " + id_visita + "";
                    SqlCommand command_updateup2 = new SqlCommand(updatevisitas, connection);
                    command_updateup2.Parameters.AddWithValue("ID_VISITAS", id_visita + 1);

                    command2.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    command_updateup.ExecuteNonQuery();
                    command_updateup2.ExecuteNonQuery();
                    clear();
                    iniciar();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(Convert.ToString(ex));
                }
            }
            else if (rdno.IsChecked == true)
            {
                try
                {
                    string query = "INSERT Llamadas (ID_LLAMADA, CUENTA, NUMERO_ORDEN, NOMBRE_CLIENTE, NUM_CONTACTO, DIRECCIÓN, TECNICO, FECHA_EVENTO, TIPO_ACTIVIDAD, VEZ_LLAMADA, NOTA, HORA_REGISTRO, FECHA_REGISTRO,TIPO_LLAMADA, VISITA, ID_VISITA) VALUES(@id_llamada, @cuenta, @numero_orden, @nombre_cliente, @num_contacto, @dirección, @tecnico, @fecha_evento, @tipo_actividad, @vez_llamada, @nota, @horar, @fechar, @tipo_llamada, @visita, @id_visita)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id_llamada", id_llamada + 1);
                    command.Parameters.AddWithValue("@cuenta", txtcuenta.Text);
                    command.Parameters.AddWithValue("@numero_orden", txtidorden.Text);
                    command.Parameters.AddWithValue("@nombre_cliente", txtnamecliente.Text);
                    command.Parameters.AddWithValue("@num_contacto", txtcontacto.Text);
                    command.Parameters.AddWithValue("@dirección", txtdireccion.Text);
                    command.Parameters.AddWithValue("@tecnico", cbxtecnico_Copy.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@fecha_evento", pickfechaevento.Text);
                    command.Parameters.AddWithValue("@tipo_actividad", txtactividad.Text);
                    command.Parameters.AddWithValue("@vez_llamada", Convert.ToInt32(txtvezllamada.Text) + 1);
                    command.Parameters.AddWithValue("@nota", txtnotasllamada.Text);
                    command.Parameters.AddWithValue("@horar", Dateh);
                    command.Parameters.AddWithValue("@fechar", Date);
                    command.Parameters.AddWithValue("@tipo_llamada", cbxtipollamada.Text);
                    command.Parameters.AddWithValue("@visita", "FALSE");
                    command.Parameters.AddWithValue("@id_visita", 0);

                    string updateidllamada = "UPDATE ids SET ID_LLAMADAS = " + (id_llamada + 1) + " WHERE ID_LLAMADAS = " + id_llamada + "";
                    SqlCommand command_updateup = new SqlCommand(updateidllamada, connection);
                    command_updateup.Parameters.AddWithValue("ID_LLAMADAS", id_llamada + 1);


                    command.ExecuteNonQuery();
                    command_updateup.ExecuteNonQuery();

                    iniciar();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
            else
            {
                MessageBox.Show("¿Requiere visita?");
            }
            lb_id_.Content = "ID LLAMADA " + id_llamada;
        }
        public void idvisita()
        {
            connection.Close();
            connection.Open();
            SqlCommand sqlCommand1 = new SqlCommand("Select * from ids", connection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            if (dataReader1.Read())
            {
                id_llamada = Convert.ToInt32(dataReader1["ID_LLAMADAS"]);
                id_visita = Convert.ToInt32(dataReader1["ID_VISITAS"]);
                lb_id_.Content = "ID LLAMADA: " + id_llamada;
            }
            dataReader1.Close();
        }
        private void cbxtecnico_Copy_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbxtecnico_Copy.IsDropDownOpen = true;
        }
        private void cbxtecnico_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string name = cbxtecnico.SelectedItem.ToString();
                var cedula = (from i in _listec
                              where i.NOMBRE == name
                              select i).ToList();
                cedulaagg(cedula);
                lbagenda.Content = "AGENDAMIENTO VISITA " + cedulaselect;
                btnok.IsEnabled = true;
            }
            catch (Exception)
            {
                btnok.IsEnabled = false;
            }
        }
        public void cedulaagg(List<listec> _listec)
        {
            Console.WriteLine("");
            foreach (var item in _listec)
            {
                cedulaselect = Convert.ToInt32(item.CEDULA);
            }
        }
        private void cbxtecnico_Copy_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            cbxtecnico.SelectedIndex = cbxtecnico_Copy.SelectedIndex;
            if (cbxtecnico_Copy.SelectedItem != null)
            {
                btnok.IsEnabled = true;
            }
            else
            {
                btnok.IsEnabled = false;
            }
        }
        public void clear()
        {
            txtidorden.Clear();
            txtcuenta.Clear();
            txtnamecliente.Clear();
            txtcontacto.Clear();
            txtdireccion.Clear();
            txtdireccion.Clear();
            cbxtecnico_Copy.SelectedIndex = -1;
            txtactividad.Clear();
            txtvezllamada.Clear();
            cbxtipollamada.SelectedIndex = -1;
            txtnotasllamada.Clear();
            rdyes.IsChecked = false;
            rdno.IsChecked = false;
            cbxsupervisores.SelectedIndex = -1;
            cbxtecnico.SelectedIndex = -1;
            txtminutos.Clear();
            cbxfranjavisitafinal.SelectedIndex = -1;
            cbxfranjavisitainicial.SelectedIndex = -1;
            txtnotasvisita.Clear();
            VISITA.Visibility = Visibility.Hidden;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Agenda agenda = new Agenda();
            agenda.Show();
        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Operación op = new Operación();
            op.Show();
            this.Close();
        }
        private void cbxfranjavisitainicial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxfranjavisitafinal.SelectedIndex = cbxfranjavisitainicial.SelectedIndex + 2;
        }
        private void btncall_Click(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
        private void btncall_Click_1(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void txtidorden_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cadena = txtidorden.Text;
            txtidorden.Text = cadena.Replace(" ", "");
        }

        private void txtcuenta_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cadena = txtcuenta.Text;
            txtcuenta.Text = cadena.Replace(" ", "");
        }

        private void txtcontacto_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cadena = txtcontacto.Text;
            txtcontacto.Text = cadena.Replace(" ", "");
        }

        private void cbxsupervisores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
