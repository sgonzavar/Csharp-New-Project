using System;
using System.Data.SqlClient;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA.LOGISTICA
{
    /// <summary>
    /// Lógica de interacción para Newcodigo.xaml
    /// </summary>
    public partial class Newcodigo : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadena);
        public Newcodigo()
        {
            InitializeComponent();
        }
        private void btnguardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtitem.Text) || string.IsNullOrEmpty(txtmaterial.Text) || string.IsNullOrEmpty(txtubicacion1.Text) || string.IsNullOrEmpty(txtubicacion2.Text))
            {
                MessageBox.Show("Ningun campo puede quedar vacio");
            }
            else
            {
                connection.Close();
                connection.Open();
                //INTRODUCE CODIGO VALORADO

                string item1 = txtitem.Text + "-1";
                SqlCommand command_buscar2 = new SqlCommand("Select * from tbbodega_ppal Where ITEM = @item2", connection);
                command_buscar2.Parameters.AddWithValue("@item2", item1);
                SqlDataReader reader_buscar2 = command_buscar2.ExecuteReader();
                if (reader_buscar2.Read())
                {
                    MessageBox.Show("El item ya existe en bodega, no se puede agregar");
                    txtmaterial.Text = Convert.ToString(reader_buscar2["MATERIAL"]);
                    txtubicacion1.Text = Convert.ToString(reader_buscar2["UBICACIÓN"]);
                    reader_buscar2.Close();
                }
                else
                {

                    reader_buscar2.Close();
                    //INTRODUCE CODIGO VALORADO
                    string item = txtitem.Text + "-1";
                    string cadena = "INSERT into tbbodega_ppal (ITEM, MATERIAL, UBICACIÓN, CANTIDAD, LOTE) VALUES (@item, @material, @ubicación, @cantidad, @lote)";
                    SqlCommand sqlCommand = new SqlCommand(cadena, connection);
                    sqlCommand.Parameters.AddWithValue("@item", item);
                    sqlCommand.Parameters.AddWithValue("@material", txtmaterial.Text);
                    sqlCommand.Parameters.AddWithValue("@ubicación", txtubicacion1.Text);
                    sqlCommand.Parameters.AddWithValue("@cantidad", 0);
                    sqlCommand.Parameters.AddWithValue("@lote", "VALORADO");
                    sqlCommand.ExecuteNonQuery();


                    //INTRODUCE CODIGO VALORADO

                    string item2 = txtitem.Text + "-2";
                    string cadena1 = "INSERT into tbbodega_ppal (ITEM, MATERIAL, UBICACIÓN, CANTIDAD, LOTE) VALUES (@item, @material, @ubicación, @cantidad, @lote)";
                    SqlCommand sqlCommand1 = new SqlCommand(cadena1, connection);
                    sqlCommand1.Parameters.AddWithValue("@item", item2);
                    sqlCommand1.Parameters.AddWithValue("@material", txtmaterial.Text);
                    sqlCommand1.Parameters.AddWithValue("@ubicación", txtubicacion2.Text);
                    sqlCommand1.Parameters.AddWithValue("@cantidad", 0);
                    sqlCommand1.Parameters.AddWithValue("@lote", "NOVALORADO");
                    sqlCommand1.ExecuteNonQuery();
                    // connection.Close();
                    MessageBox.Show("Item creado actualice Bodega ppal");


                    reader_buscar2.Close();
                    //INTRODUCE CODIGO VALORADO

                    string cadena2 = "INSERT into tbbodega_consolidado (ITEM, MATERIAL, UBICACIÓN, CANTIDAD) VALUES (@item, @material, @ubicación, @cantidad)";
                    SqlCommand sqlCommand2 = new SqlCommand(cadena2, connection);
                    sqlCommand2.Parameters.AddWithValue("@item", item);
                    sqlCommand2.Parameters.AddWithValue("@material", txtmaterial.Text);
                    sqlCommand2.Parameters.AddWithValue("@ubicación", txtubicacion1.Text);
                    sqlCommand2.Parameters.AddWithValue("@cantidad", 0);
                    // sqlCommand2.Parameters.AddWithValue("@lote", "VALORADO");
                    sqlCommand2.ExecuteNonQuery();


                    //INTRODUCE CODIGO VALORADO


                    string cadena3 = "INSERT into tbbodega_consolidado (ITEM, MATERIAL, UBICACIÓN, CANTIDAD) VALUES (@item, @material, @ubicación, @cantidad)";
                    SqlCommand sqlCommand3 = new SqlCommand(cadena3, connection);
                    sqlCommand3.Parameters.AddWithValue("@item", item2);
                    sqlCommand3.Parameters.AddWithValue("@material", txtmaterial.Text);
                    sqlCommand3.Parameters.AddWithValue("@ubicación", txtubicacion2.Text);
                    sqlCommand3.Parameters.AddWithValue("@cantidad", 0);
                    //sqlCommand3.Parameters.AddWithValue("@lote", "NOVALORADO");
                    sqlCommand3.ExecuteNonQuery();
                    // connection.Close();
                }
            }
        }

        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }
    }
}
