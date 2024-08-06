using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SISTEMA_DE_GESTIÓN_LCCA.CALIDAD
{
    /// <summary>
    /// Lógica de interacción para Reagendamiento.xaml
    /// </summary>
    public partial class Reagendamiento : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenallamadas);
        SqlConnection connectiontec = new SqlConnection(conexion.cadena);
        List<listec> _listec = new List<listec>();
        List<listsup> _listsup = new List<listsup>();
        int cedulaselect = 0;
        public Reagendamiento()
        {
            InitializeComponent();
            lbagenda.Content = "REAGENDAMIENTO VISITA " + Agenda.id_visita;
            cargatec(_listec, _listsup);
        }
        public void cargatec(List<listec> _listec, List<listsup> _listsup)
        {
            cbxsupervisores.Items.Clear();
            cbxtecnico.Items.Clear();
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
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                lbagenda.Content = "AGENDAMIENTO VISITA " + Agenda.id_visita;
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

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            int s = Convert.ToInt32(Agenda.id_visita);
            connection.Close();
            connection.Open();
            string cadena = "UPDATE Agendamiento SET SUPERVISOR = '" + cbxsupervisores.Text + "' where ID_VISITA = " + s + "";
            SqlCommand comando = new SqlCommand(cadena, connection);
            comando.Parameters.AddWithValue("ID_VISITA", s);
            comando.ExecuteNonQuery();

            string cadena1 = "UPDATE Agendamiento SET ID_TECNICO = '" + cedulaselect + "' where ID_VISITA = " + s + "";
            SqlCommand comando1 = new SqlCommand(cadena1, connection);
            comando1.Parameters.AddWithValue("ID_VISITA", s);
            comando1.ExecuteNonQuery();

            string cadena2 = "UPDATE Agendamiento SET TECNICO = '" + cbxtecnico.Text + "' where ID_VISITA = " + s + "";
            SqlCommand comando2 = new SqlCommand(cadena2, connection);
            comando2.Parameters.AddWithValue("ID_VISITA", s);
            comando2.ExecuteNonQuery();

            string cadena3 = "UPDATE Agendamiento SET TECNICO = '" + cbxtecnico.Text + "' where ID_VISITA = " + s + "";
            SqlCommand comande3 = new SqlCommand(cadena3, connection);
            comande3.Parameters.AddWithValue("ID_VISITA", s);
            comande3.ExecuteNonQuery();

            string cadena4 = "UPDATE Agendamiento SET FECHA_AGENDA = '" + pickfechavisita.Text + "' where ID_VISITA = " + s + "";
            SqlCommand comande4 = new SqlCommand(cadena4, connection);
            comande4.Parameters.AddWithValue("ID_VISITA", s);
            comande4.ExecuteNonQuery();

            string cadena5 = "UPDATE Agendamiento SET FRANJA_VISITA = '" + cbxfranjavisitainicial.Text + " a " + cbxfranjavisitafinal.Text + "' where ID_VISITA = " + s + "";
            SqlCommand comande5 = new SqlCommand(cadena5, connection);
            comande5.Parameters.AddWithValue("ID_VISITA", s);
            comande5.ExecuteNonQuery();

            AutoClosingMessageBox.Show("Reagendado", "Evento Reagendado", 3000);

            this.Close();
        }
        private void cbxfranjavisitainicial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxfranjavisitafinal.SelectedIndex = cbxfranjavisitainicial.SelectedIndex + 2;
        }
    }
}

