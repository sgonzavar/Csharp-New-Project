using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para Inversa.xaml
    /// </summary>
    public partial class Inversa : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        public Inversa()
        {
            InitializeComponent();
            SqlCommand command2 = new SqlCommand("Select * from baseinversa", connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = command2;
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (DataRow row in dataTable2.Rows)
            {
                collection.Add(Convert.ToString(row["DESCRIPCIÓN"]));
            }
            //CBX1
            cbxdescripcion.ItemsSource = collection;
            connection.Close();
        }
        private void btnloanddata_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            connection.Close();
            connection.Open();
            SqlCommand command_buscar = new SqlCommand("Select * from baseinversa Where DESCRIPCIÓN = @id", connection); //CONSULTA SQL
            command_buscar.Parameters.AddWithValue("@id", cbxdescripcion.Text);
            SqlDataReader reader_buscar = command_buscar.ExecuteReader();
            if (reader_buscar.Read())
            {
                txtcodigo.Text = Convert.ToString(reader_buscar["CODIGO_SAP"]); //Almacena ese itemuna variable no definida            

            }
            connection.Close();
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtserial.Text))
            {
                MessageBox.Show("Por favor introduzca serial.");
            }
            else
            {
                connection.Close();
                connection.Open();
                SqlCommand command_buscar = new SqlCommand("Select * from tbinversa Where SERIAL = @id", connection); //CONSULTA SQL
                command_buscar.Parameters.AddWithValue("@id", txtserial.Text);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    MessageBox.Show("EL SERIAL YA ESTA REGISTRADO");
                }
                else
                {
                    string dateTime = DateTime.Now.ToString("dd/MM/yyyy");
                    string nametec = ClaseTecnico.nombre;
                    string cedtec = ClaseTecnico.cedula;

                    reader_buscar.Close();
                    string query2 = "INSERT tbinversa (CODIGO, DESCRIPCION, SERIAL, ESTADO, CEDULA, NOMBRE, FECHA_INGRESO) VALUES(@codigo,@descripción,@serial,@estado,@cedula,@nombre,@fecha)";
                    SqlCommand command = new SqlCommand(query2, connection);
                    command.Parameters.AddWithValue("@codigo", txtcodigo.Text);
                    command.Parameters.AddWithValue("@descripción", cbxdescripcion.Text);
                    command.Parameters.AddWithValue("@serial", txtserial.Text);
                    command.Parameters.AddWithValue("@estado", "ALMACENADO");
                    command.Parameters.AddWithValue("@cedula", cedtec);
                    command.Parameters.AddWithValue("@nombre", nametec);
                    command.Parameters.AddWithValue("@fecha", dateTime + " | " + ClaseCompartida.nombre);
                    command.ExecuteNonQuery();
                    txtserial.Clear();
                    MessageBox.Show("REGISTRADO");

                    //FIN
                    connection.Close();
                }
                connection.Close();
            }
        }
    }
}
