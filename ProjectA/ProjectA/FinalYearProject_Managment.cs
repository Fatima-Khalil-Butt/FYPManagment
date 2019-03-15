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
    public partial class FinalYearProject_Managment : Form
    {
        public FinalYearProject_Managment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Project p = new Project();
            this.Hide();
            p.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Advisor ad = new Advisor();
            this.Hide();
            ad.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Evaluation eva=new Evaluation();
            this.Hide();
            eva.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignAdvior aadvisr = new AssignAdvior();
            this.Hide();
            aadvisr.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }
    }
}
