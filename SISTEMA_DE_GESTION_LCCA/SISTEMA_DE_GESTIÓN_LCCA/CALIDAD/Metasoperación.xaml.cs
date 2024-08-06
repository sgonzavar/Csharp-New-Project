using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Metasoperación.xaml
    /// </summary>
    public partial class Metasoperación : Window
    {
        SqlConnection connection = new SqlConnection(conexion.cadenacalidad);
        int meta = 0;
        int flag = 0;
        int flag2 = 0;
        public Metasoperación()
        {
            InitializeComponent();
            loandmes();
            visual.Visibility = Visibility.Hidden;
            visualuploand.Visibility = Visibility.Hidden;
        }
        public void loandmes()
        {
            cbxmes.Items.Clear();
            cbxmes.Items.Add("ENERO");
            cbxmes.Items.Add("FEBRERO");
            cbxmes.Items.Add("MARZO");
            cbxmes.Items.Add("ABRIL");
            cbxmes.Items.Add("MAYO");
            cbxmes.Items.Add("JUNIO");
            cbxmes.Items.Add("JULIO");
            cbxmes.Items.Add("AGOSTO");
            cbxmes.Items.Add("SEPTIEMBRE");
            cbxmes.Items.Add("OCTIBRE");
            cbxmes.Items.Add("NOVIEMBRE");
            cbxmes.Items.Add("DICIEMBRE");

            cbxmetasup.Items.Clear();
            cbxmetasup.Items.Add("ENERO");
            cbxmetasup.Items.Add("FEBRERO");
            cbxmetasup.Items.Add("MARZO");
            cbxmetasup.Items.Add("ABRIL");
            cbxmetasup.Items.Add("MAYO");
            cbxmetasup.Items.Add("JUNIO");
            cbxmetasup.Items.Add("JULIO");
            cbxmetasup.Items.Add("AGOSTO");
            cbxmetasup.Items.Add("SEPTIEMBRE");
            cbxmetasup.Items.Add("OCTIBRE");
            cbxmetasup.Items.Add("NOVIEMBRE");
            cbxmetasup.Items.Add("DICIEMBRE");

        }
        private void cbxmes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                int total = 0;
                string fechaul_re = "";
                for (int i = 1; i < 8; i++)
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * from meta" + cbxmes.SelectedItem.ToString() + " WHERE ID = '" + i + "'", connection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.Read())
                    {
                        switch (i)
                        {
                            case 1:
                                lbcaninstahfc.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                lbmetainstaH.Content = "META HFC Y FTTH: " + Convert.ToString(dataReader["META"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                fechaul_re = Convert.ToString(dataReader["ULTIMA_FECHA"]);
                                break;
                            case 2:
                                lbcantidadinftth.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                // lbmetainstaF.Content = "META: " + Convert.ToString(dataReader["META"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                break;
                            case 3:
                                lbcanmtohfc.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                lbmetamantoH.Content = "META HFC Y FTTH: " + Convert.ToString(dataReader["META"]);
                                break;
                            case 4:
                                lbmtoftth.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                // lbmetamantoF.Content = "META: " + Convert.ToString(dataReader["META"]);
                                break;
                            case 5:
                                lbposhfc.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                lbmetaposH.Content = "META HFC Y FTTH: " + Convert.ToString(dataReader["META"]);
                                break;
                            case 6:
                                lbposftth.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                total += Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
                                // lbmetaposF.Content = "META: " + Convert.ToString(dataReader["META"]);
                                break;
                            case 7:
                                lbventa.Content = "EVENTOS REALIZADOS: " + Convert.ToString(dataReader["CANTIDAD_ACTIVIDADES"]);
                                break;
                        }
                    }
                    dataReader.Close();
                }
                loadmeta();
                visual.Visibility = Visibility.Visible;
                lbtotalactividades.Content = total;
                lbmeta.Content = meta;
                lbactifaltantes.Content = meta - total;
                lbcorte.Content = "Último corte: " + fechaul_re;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No existe registro" + ex, "Failed");

            }
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Operación operación = new Operación();
            operación.Show();
            this.Close();
        }
        private void loadmeta()
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT * from meta" + cbxmes.SelectedItem.ToString() + " WHERE ACTIVIDAD = 'META'", connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.Read())
            {
                meta = Convert.ToInt32(dataReader["CANTIDAD_ACTIVIDADES"]);
            }
        }
        private void btnloanddata_Click_1(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                c();
            }
        }
        private void btnupmetas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flag2 == 0) // Sube total
                {
                    if (string.IsNullOrEmpty(txtfechacorte.Text) || string.IsNullOrEmpty(cbxinstah.Text) || string.IsNullOrEmpty(cbxinstaF.Text) || string.IsNullOrEmpty(cbxmtof.Text) || string.IsNullOrEmpty(cbxmtoh.Text) || string.IsNullOrEmpty(cbxposh.Text) && string.IsNullOrEmpty(cbxposf.Text) || string.IsNullOrEmpty(cbxventa.Text))
                    {
                        MessageBox.Show("Ningún campo puede estar vacio", "Failed");
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        string cadena = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxinstah.Text) + "' where ID = '1'";
                        SqlCommand comando = new SqlCommand(cadena, connection);

                        string cadena1 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxinstaF.Text) + "' where ID = '2'";
                        SqlCommand comando1 = new SqlCommand(cadena1, connection);

                        string cadena2 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxmtoh.Text) + "' where ID = '3'";
                        SqlCommand comando2 = new SqlCommand(cadena2, connection);

                        string cadena3 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " set CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxmtof.Text) + "' where ID = '4'";
                        SqlCommand comando3 = new SqlCommand(cadena3, connection);

                        string cadena4 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxposh.Text) + "' where ID = '5'";
                        SqlCommand comando4 = new SqlCommand(cadena4, connection);

                        string cadena5 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxposf.Text) + "' where ID = '6'";
                        SqlCommand comando5 = new SqlCommand(cadena5, connection);

                        string cadena6 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxventa.Text) + "' where ID = '7'";
                        SqlCommand comando6 = new SqlCommand(cadena6, connection);

                        string cadena7 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET ULTIMA_FECHA = '" + Convert.ToString(txtfechacorte.Text) + "' where ID = '1'";
                        SqlCommand comando7 = new SqlCommand(cadena7, connection);

                        comando.ExecuteNonQuery();
                        comando1.ExecuteNonQuery();
                        comando2.ExecuteNonQuery();
                        comando3.ExecuteNonQuery();
                        comando4.ExecuteNonQuery();
                        comando5.ExecuteNonQuery();
                        comando6.ExecuteNonQuery();
                        comando7.ExecuteNonQuery();
                        c();
                        MessageBox.Show("Datos actualizados", "OKAY");
                    }
                }
                else if (flag2 == 1) //Sube meta
                {
                    if (string.IsNullOrEmpty(cbxinstah.Text) || string.IsNullOrEmpty(cbxmtoh.Text) || string.IsNullOrEmpty(cbxposh.Text) || string.IsNullOrEmpty(cbxventa.Text))
                    {
                        MessageBox.Show("Ningún campo puede estar vacio", "Failed");
                    }
                    else
                    {

                        connection.Close();
                        connection.Open();
                        string cadena = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET META = '" + Convert.ToInt32(cbxinstah.Text) + "' where ID = '1'";
                        SqlCommand comando = new SqlCommand(cadena, connection);



                        string cadena2 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET META = '" + Convert.ToInt32(cbxmtoh.Text) + "' where ID = '3'";
                        SqlCommand comando2 = new SqlCommand(cadena2, connection);



                        string cadena4 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET META = '" + Convert.ToInt32(cbxposh.Text) + "' where ID = '5'";
                        SqlCommand comando4 = new SqlCommand(cadena4, connection);



                        string cadena6 = "UPDATE meta" + cbxmetasup.SelectedItem.ToString() + " SET CANTIDAD_ACTIVIDADES = '" + Convert.ToInt32(cbxventa.Text) + "' where ID = '20'";
                        SqlCommand comando6 = new SqlCommand(cadena6, connection);


                        comando.ExecuteNonQuery();

                        comando2.ExecuteNonQuery();

                        comando4.ExecuteNonQuery();

                        comando6.ExecuteNonQuery();

                        c();
                        MessageBox.Show("Datos actualizados", "OKAY");
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error, intente de nuevo", "Failed");
            }
        }
        private void btnchange_Click(object sender, RoutedEventArgs e)
        {
            if (flag2 == 1)
            {
                flag2 = 0;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("https://i.ibb.co/wMZLWZ3/iconmeta.png");
                bi3.EndInit();
                imgchange.Stretch = Stretch.Uniform;
                imgchange.ImageSource = bi3;

                lbinstaH.Visibility = Visibility.Visible;
                lbinstaF.Visibility = Visibility.Visible;
                lbmtoH.Visibility = Visibility.Visible;
                lbmtoF.Visibility = Visibility.Visible;
                lbposH.Visibility = Visibility.Visible;
                lbposF.Visibility = Visibility.Visible;
                lbventa.Visibility = Visibility.Visible;

                cbxinstah.Visibility = Visibility.Visible;
                cbxinstaF.Visibility = Visibility.Visible;
                cbxmtoh.Visibility = Visibility.Visible;
                cbxmtof.Visibility = Visibility.Visible;
                cbxposh.Visibility = Visibility.Visible;
                cbxposf.Visibility = Visibility.Visible;
                cbxventa.Visibility = Visibility.Visible;
                lbfecha.Visibility = Visibility.Visible;
                txtfechacorte.Visibility = Visibility.Visible;

                lbinstaH.Content = "Valor Instalaciones HFC:";
                lbmtoH.Content = "Valor Mantenimiento HFC:";
                lbposH.Content = "Valor Postventa HFC:";
                lbVenta.Content = "Valor Venta Técnica:";

            }
            else
            {
                flag2 = 1;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("https://i.ibb.co/4Jc7r36/icon.png");
                bi3.EndInit();
                imgchange.Stretch = Stretch.Uniform;
                imgchange.ImageSource = bi3;

                txtfechacorte.Visibility = Visibility.Hidden;
                lbinstaF.Visibility = Visibility.Hidden;
                cbxinstaF.Visibility = Visibility.Hidden;
                lbmtoF.Visibility = Visibility.Hidden;
                cbxmtof.Visibility = Visibility.Hidden;
                lbposF.Visibility = Visibility.Hidden;
                cbxposf.Visibility = Visibility.Hidden;
                lbfecha.Visibility = Visibility.Hidden;

                lbinstaH.Content = "Meta Instalaciones:";
                lbmtoH.Content = "Meta Mantenimiento:";
                lbposH.Content = "Meta Postventa:";
                lbVenta.Content = "Total Metas:";
            }
        }
        private void c()
        {
            if (flag == 0)
            {
                visual.Visibility = Visibility.Hidden;
                visualuploand.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("https://i.ibb.co/tcxq6F0/iconout.png");
                bi3.EndInit();
                imgbtn.Stretch = Stretch.UniformToFill;
                imgbtn.ImageSource = bi3;
                flag = 1;
                lbselec.Visibility = Visibility.Hidden;
                cbxmes.Visibility = Visibility.Hidden;
                lbcorte.Visibility = Visibility.Hidden;
                gridpuntos.Visibility = Visibility.Hidden;
                clear();
            }
            else
            {
                visualuploand.Visibility = Visibility.Hidden;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("https://i.ibb.co/Rp7J4H7/icondatacarga.png");
                bi3.EndInit();
                imgbtn.Stretch = Stretch.UniformToFill;
                imgbtn.ImageSource = bi3;
                lbselec.Visibility = Visibility.Visible;
                cbxmes.Visibility = Visibility.Visible;
                lbcorte.Visibility = Visibility.Visible;
                gridpuntos.Visibility = Visibility.Visible;
                flag = 0;
                clear();
            }
        }
        private void clear()
        {
            cbxinstah.Clear();
            cbxinstaF.Clear();
            cbxmtof.Clear();
            cbxmtoh.Clear();
            cbxposf.Clear();
            cbxposh.Clear();
            cbxventa.Clear();
            txtfechacorte.Clear();
        }
    }
}
