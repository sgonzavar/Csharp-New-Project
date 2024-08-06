using System;
using System.Windows;

namespace SISTEMA_DE_GESTIÓN_LCCA
{

    public static class ClaseTecnico
    {
        public static string cedula;
        public static string nombre;
    }
    public class Listado
    {
        public string serial { get; set; }
    }
    public class lisdesc
    {
        public string descripcion { get; set; }
        public int cantidad { get; set; }
    }   
    public static class conexion
    {
        ////PRUEBAS
        //public static string cadena = "Data Source=190.145.38.162;Initial Catalog=BDbodega_ppal_pruebas;User ID=analista;Password=claro4n4l1sta";
        //public static string cadenacalidad = "";
        //public static string cadenallamadas = "";


        ////PRODUCCIÓN
        public static string cadena = "Data Source=190.145.38.162;Initial Catalog=BDbodega_ppal;User ID=analista;Password=claro4n4l1sta";
        public static string cadenacalidad = "Data Source=190.145.38.162;Initial Catalog=BDbodega_calidad;User ID=analista;Password=claro4n4l1sta";
        public static string cadenallamadas = "Data Source=190.145.38.162;Initial Catalog=BDllamadas_calidad;User ID=analista;Password=claro4n4l1sta";

    }
    public static class ClaseCompartida
    {
        public static string usuario;
        public static string nombre;
        public static string nivel;
        public static string logistica;
        public static string calidad;
        public static string administrativo;
        public static string uploadmetasop;
        public static string formacion;
        public static string sst;
        public static string dotacion;
        public static string operacion;
        public static string personal;
        public static string up;
        public static string bancopruebas;
        public static string proyecto;
        public static string consecutivos;
    }
    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
    public static class flagt
    {
        public static string ced;
        public static int flagtec;
        public static string serial;
        public static string descripcion;
    }

}
