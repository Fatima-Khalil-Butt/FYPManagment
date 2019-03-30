using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            StudentProjectReport sr = new StudentProjectReport();
            this.Hide();
            sr.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StudentEvaluationReport er = new StudentEvaluationReport();
            this.Hide();
            er.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjectReport p = new ProjectReport();
            this.Hide();
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EvaluationReport ev = new EvaluationReport();
            this.Hide();
            ev.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FinalYearProject_Managment fyp = new FinalYearProject_Managment();
            this.Hide();
            fyp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }
    }
}
