using System;
using System.Data.SqlClient;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Consecutivoasis.xaml
    /// </summary>
    public partial class Consecutivoasis : Window
    {
        SqlConnection connection = new SqlConnection("Data Source=192.168.4.178; Initial Catalog = BDcuentascobro;User ID=analista;Password=claro4n4l1sta");
        public Consecutivoasis()
        {
            InitializeComponent();
        }
        private void btnuploanddata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand command_buscar = new SqlCommand("Select * from consecutivo Where id = @id", connection); //CONSULTA SQL
                command_buscar.Parameters.AddWithValue("@id", 2);
                SqlDataReader reader_buscar = command_buscar.ExecuteReader();
                if (reader_buscar.Read())
                {
                    lbconsecutivo.Content = lbconsecutivo_Copy.Content;
                    string consecutivo = Convert.ToString(reader_buscar["Consecutivo"]);
                    reader_buscar.Close();
                    int y = Convert.ToInt32(consecutivo) + 1;
                    string dateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    string query1 = "UPDATE consecutivo SET Consecutivo = '" + y + "' Where id = '2'";
                    SqlCommand command_update1 = new SqlCommand(query1, connection);
                    command_update1.ExecuteNonQuery();

                    lbconsecutivo_Copy.Content = "DSCM-" + y;

                    string cadena = "INSERT into Historico_consecutivo (consecutivo, persona_fecha) VALUES (@consecutivo, @persona)";
                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                    sqlCommand.Parameters.AddWithValue("@consecutivo", "DSCM-" + y);
                    sqlCommand.Parameters.AddWithValue("@persona", ClaseCompartida.nombre + " | " + dateTime);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }

        }
        private void btnhome_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
