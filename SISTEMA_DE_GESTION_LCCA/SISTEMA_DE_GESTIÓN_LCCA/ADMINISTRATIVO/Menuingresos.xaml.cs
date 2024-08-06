using System;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA.ADMINISTRATIVO
{
    /// <summary>
    /// Lógica de interacción para Menuingresos.xaml
    /// </summary>
    public partial class Menuingresos : Window
    {
        public Menuingresos()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void btnexcelenciamod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.formacion == "TRUE")
                {
                    Formacion formacion = new Formacion();
                    formacion.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnmetasmod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.sst == "TRUE")
                {
                    SST sst = new SST();
                    sst.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btncall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.dotacion == "TRUE")
                {
                    Dotacion dot = new Dotacion();
                    dot.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.operacion == "TRUE")
                {
                    Operacion dot = new Operacion();
                    dot.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.administrativo == "TRUE")
                {
                    Personal per = new Personal();
                    per.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaseCompartida.up == "TRUE")
                {
                    upempleados per = new upempleados();
                    per.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sin permisos para ingresar");
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnmotos_Click(object sender, RoutedEventArgs e)
        {
            Cuentascobro n = new Cuentascobro();
            n.Show();
            this.Close();
        }
    }
}
