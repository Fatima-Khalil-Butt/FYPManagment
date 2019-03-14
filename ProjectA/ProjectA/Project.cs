using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectA
{
    public partial class Project : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public Project()
        {
            InitializeComponent();
        }

        private void Project_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            
            SqlCommand sqlCmd4 = new SqlCommand("INSERT INTO Project(Description,Title) VALUES('" + textBox1.Text+ "','" + textBox2.Text + "')", sqlCon);

            sqlCmd4.ExecuteNonQuery();
            sqlCon.Close();
            MessageBox.Show("Added");
            Advisor ad = new Advisor();
            this.Hide();
            ad.Show();
        }
    }
}
