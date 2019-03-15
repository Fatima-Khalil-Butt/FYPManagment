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
            textBox2.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd4 = new SqlCommand("SELECT Id FROM Lookup  WHERE  Value='" + comboBox1.Text + "'", sqlCon);
                    int input = Convert.ToInt32(sqlCmd4.ExecuteScalar());
                    SqlCommand sqlCmd3 = new SqlCommand("SELECT COUNT(Advisor.Id) FROM Advisor  WHERE  Id='" + input + "'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd3.ExecuteScalar());
                    if (textBox1.Text == "" && textBox2.Text == "" && comboBox1.Text == "")
                    {
                        lblDesignation.Text = "Designation is required!";
                        lblSalary.Text = "Salary is required!";

                    }
                    else if (count > 0)
                    {
                        lblDesignation.Text = "Already Exist!";
                    }

                    else
                    {
                        if (comboBox1.Text == "")
                        {
                            lblDesignation.Text = "Designation is required!";

                        }
                        if (textBox1.Text == "")
                        {
                            lblSalary.Text = "Salary is required!";
                        }

                    }

                    if (textBox1.Text != "" && comboBox1.Text != ""&&count==0)
                    {
                        SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + comboBox1.Text + "'", sqlCon);
                        int input1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                        SqlCommand sqlCmd2 = new SqlCommand("INSERT INTO Advisor(Id,Designation,Salary) VALUES('" + input1 + "',(SELECT Id From Lookup WHERE  Value ='" + comboBox1.Text + "'), '" + Convert.ToDecimal(textBox1.Text) + "')", sqlCon);

                        sqlCmd2.ExecuteNonQuery();
                       
                        MessageBox.Show("Advisor has been Added");
                        Clear();


                    }
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Advisor_Load(object sender, EventArgs e)
        {
           
        }
        void Clear()
        {
           comboBox1.Text = textBox2.Text = textBox1.Text = "";
           

        }
       


      

      

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Advisor ad = new Advisor();
            this.Hide();
            ad.Show();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FinalYearProject_Managment fyp = new FinalYearProject_Managment();
            this.Hide();
            fyp.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblDesignation.Text = "Please Select From List";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            lblDesignation.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblSalary.Text = "";
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AvailableAdvisors ava = new AvailableAdvisors();
            this.Hide();
            ava.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
