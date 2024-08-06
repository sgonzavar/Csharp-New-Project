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

namespace exampleo01.view
{
    public partial class see_Teach : Form
    {
        string[] teachers = new string[] { "juancita - JAVA", "pepito - Base de Datos", "peranito - C#", "carlitos - HTML" };
        string name, time, hour, teach, note;
        ArrayList dateList = new ArrayList();
        rules.Person person = new rules.Person();        

        private void fullComb()
        {            
            cmbTeach.Items.AddRange(teachers);
        }

        public see_Teach()
        {
            InitializeComponent();
        }

        private void see_Teach_Load(object sender, EventArgs e)
        {
            fullComb();
            name = person.Name;
            time = person.Time;
            hour = person.Hour;
            teach = person.Teach;
            note = person.Notes;

            dateList.Add(hour);
            dateList.Add(teach);

            dgvTeach.DataSource = null;
            dgvTeach.DataSource = dateList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                
        }
    }
}
