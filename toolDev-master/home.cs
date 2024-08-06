using System;
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
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void home_Load(object sender, EventArgs e)
        {


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnScheTeach_Click(object sender, EventArgs e)
        {
            scheduling scheduling = new scheduling();
            scheduling.Show();
        }

        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            see_Date see_Date = new see_Date();
            see_Date.Show();
        }

        private void btnSearchTeach_Click(object sender, EventArgs e)
        {
            view.see_Teach see_Teach = new view.see_Teach();
            see_Teach.Show();

        }
    }
}
