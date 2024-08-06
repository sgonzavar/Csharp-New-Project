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
    public partial class scheduling : Form
    {
        string[] hours = new string[] {"08:00", "10:00", "12:00", "14:00", "16:00", "18:00" };
        string[] teachers = new string[] { "juancita - JAVA", "pepito - Base de Datos", "peranito - C#", "carlitos - HTML" };
        string name, time, hour, teach, note;
        ArrayList scheList = new ArrayList();

        private void fullComb()
        {
            cmbHours.Items.AddRange(hours);
            cmbTeach.Items.AddRange(teachers);
        }
        public scheduling()
        {
            InitializeComponent();
        }

        private void scheduling_Load(object sender, EventArgs e)
        {
            fullComb();
        }

        private void btnScheduling_Click(object sender, EventArgs e)
        {
            //rules.Person person = new rules.Person();
            //rules.Operations operations = new rules.Operations();
            try
            {
                name = Convert.ToString(txtNameStudent.Text);
                time = Convert.ToString(dtpDate.Value.ToString("dd-MM-yyyy"));
                hour = Convert.ToString(cmbHours.SelectedItem);
                teach = Convert.ToString(cmbTeach.SelectedItem);
                note = Convert.ToString(txtTheme.Text);
                rules.Person person = new rules.Person(name, time, hour, teach, note);

                scheList.Add(person);

                dgvSche.DataSource = null;
                dgvSche.DataSource = scheList;

                if (cmbHours.SelectedItem == null)
                {
                    MessageBox.Show(cmbHours.SelectedItem.ToString());
                }
               
            }
            catch ( Exception ex )
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }
    }
}
