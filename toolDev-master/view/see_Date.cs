using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exampleo01
{
    public partial class see_Date : Form
    {
        private bool isFound = false;
        string name, time, hour, teach, notes;
        scheduling scheduling = new scheduling();
        ArrayList dateList = new ArrayList();

        public see_Date()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!isFound) 
            {
                MessageBox.Show("No hay citas programadas para la fecha seleccionada", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void see_Date_Load(object sender, EventArgs e)
        {
            rules.Operations operations = new rules.Operations();
            dateList.Add(operations.information(name, notes, time, hour, teach));

            dgvDate.DataSource = null;
            dgvDate.DataSource = dateList;

        }
    }
}
