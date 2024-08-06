using SISTEMA_DE_GESTIÓN_LCCA.CALIDAD;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Operación.xaml
    /// </summary>
    public partial class Operación : Window
    {
        public Operación()
        {
            InitializeComponent();
        }

        private void btnexcelenciamod_Click(object sender, RoutedEventArgs e)
        {
            Calidad calidad = new Calidad();
            calidad.Show();
            this.Close();
        }

        private void btnmetasmod_Click(object sender, RoutedEventArgs e)
        {
            Metasoperación metasoperación = new Metasoperación();
            metasoperación.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btncall_Click(object sender, RoutedEventArgs e)
        {
            LlamadasSevicio llamadasSevicio = new LlamadasSevicio();
            llamadasSevicio.Show();
            this.Close();
        }
    }
}
