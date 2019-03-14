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
    public partial class Advisor : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public Advisor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                sqlCon.Open();
            SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + comboBox1.Text+"'", sqlCon);
            int input = Convert.ToInt32(sqlCmd1.ExecuteScalar());
            SqlCommand sqlCmd4 = new SqlCommand("INSERT INTO Advisor(Id,Designation,Salary) VALUES('" + input+ "',(SELECT Id From Lookup WHERE  Value ='" + comboBox1.Text + "'), '" + Convert.ToDecimal(textBox1.Text) + "')",sqlCon);
            
            sqlCmd4.ExecuteNonQuery();
            sqlCon.Close();
            MessageBox.Show("Added");

            AssignAdvior assign= new AssignAdvior();
            this.Hide();
            assign.Show();

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Advisor_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}
