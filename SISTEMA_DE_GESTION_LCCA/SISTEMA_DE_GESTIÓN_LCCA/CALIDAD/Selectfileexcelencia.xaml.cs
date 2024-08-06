using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using DataTable = System.Data.DataTable;

namespace SISTEMA_DE_GESTIÓN_LCCA
{
    /// <summary>
    /// Lógica de interacción para Selectfileexcelencia.xaml
    /// </summary>
    public partial class Selectfileexcelencia : System.Windows.Window
    {
        private DataSet dtsTablas = new DataSet();
        SqlConnection connection = new SqlConnection(conexion.cadena);
        public Selectfileexcelencia()
        {
            InitializeComponent();
            loandmes();
            dgv.Visibility = Visibility.Hidden;
        }
        private void loandmes()
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
            cbxmes.Items.Add("OCTUBRE");
            cbxmes.Items.Add("NOVIEMBRE");
            cbxmes.Items.Add("DICIEMBRE");

        }
        private void cbxmes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SqlCommand command1 = new SqlCommand("Select * from excelencia" + cbxmes.SelectedItem.ToString() + "", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command1;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgv.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception)
            {
                MessageBox.Show("No existe registro");
            }
        }
        //Exporta excel
        private void btndownloandexc_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int k = 1, h = 1;
            GetDataGridRows(dgv);
            var rows = GetDataGridRows(dgv);
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelBook;
            Microsoft.Office.Interop.Excel._Worksheet ExcelSheet;
            ExcelBook = (Microsoft.Office.Interop.Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Microsoft.Office.Interop.Excel._Worksheet)ExcelBook.ActiveSheet;
            for (i = 1; i <= dgv.Columns.Count; i++)
            {
                ExcelSheet.Cells[1, i] = dgv.Columns[i - 1].Header.ToString();
            }
            foreach (DataGridRow r in rows)
            {
                DataRowView rv = (DataRowView)r.Item;
                foreach (DataGridColumn column in dgv.Columns)
                {
                    if (column.GetCellContent(r) is TextBlock)
                    {
                        TextBlock cellContent = column.GetCellContent(r) as TextBlock;
                        ExcelSheet.Cells[h + 1, k] = cellContent.Text.Trim();
                        k++;
                    }
                }
                k = 1;
                h++;
            }
            ExcelApp.Visible = true;
            this.Close();
            ExcelSheet = null;
            ExcelBook = null;
            ExcelApp = null;

        }
        public IEnumerable<DataGridRow> GetDataGridRows(System.Windows.Controls.DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
        //Exporta excel
    }
}
