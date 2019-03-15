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
using System.Configuration;

namespace ProjectA
{
    
    public partial class AssignAdvior : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public AssignAdvior()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                 try
                 {
                sqlCon.Open();
            SqlCommand sqlCmd4 = new SqlCommand("SELECT Id FROM  Project WHERE Title='" + comboBox2.Text + "'", sqlCon);
            int input = Convert.ToInt32(sqlCmd4.ExecuteScalar());
            SqlCommand sqlCmd5 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value ='" + comboBox1.Text + "'", sqlCon);
            int input1 = Convert.ToInt32(sqlCmd5.ExecuteScalar());
            SqlCommand sqlCmd3 = new SqlCommand("SELECT COUNT(ProjectAdvisor.AdvisorId) FROM  ProjectAdvisor  WHERE  ProjectAdvisor.ProjectId='" + input + "' AND ProjectAdvisor.AdvisorId='" + input1 + "' ", sqlCon);
            int count = Convert.ToInt32(sqlCmd3.ExecuteScalar());
              if (comboBox3.Text == "" && comboBox2.Text == ""&&comboBox1.Text=="")
               {
                   lblAdvisor.Text = "Advisor is required!";
                   lblAR.Text = "AdvisorRole is required!";
                   lblProject.Text = "Project is required!";

               }
              else if (count > 0)
               {
                lblAdvisor.Text = "Already Assigned!";
              }
               else
                    {
                        if (comboBox1.Text == "")
                        {
                            lblAdvisor.Text = "Advisor is required!";
                        }
                        if (comboBox2.Text == "")
                        {
                            lblProject.Text = "Project is required!";
                        }
                        if (comboBox3.Text == "")
                        {
                            lblAR.Text = "AdvisorRole is required!";
                        }

                    }
                  if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" &&count==0 )
                  {
                        SqlCommand sqlCmd = new SqlCommand("INSERT INTO ProjectAdvisor(AdvisorId,ProjectId,AdvisorRole,AssignmentDate) " +
                            "VALUES((SELECT Id From Lookup WHERE  Value ='" + comboBox1.Text + "')," +
                            " (SELECT Id From Project WHERE  Title ='" + comboBox2.Text + "')," +
                            "(SELECT Id From Lookup WHERE  Lookup.Value ='" + comboBox3.Text + "')," +
                            "'" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);

                        sqlCmd.ExecuteNonQuery();

                        MessageBox.Show("Advisor has been Assigned");

                  }
                    sqlCon.Close();
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }




                }
        void FillComboBox1()
        {


            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Value  FROM Lookup INNER JOIN Advisor ON Lookup.Id=Advisor.Id" , sqlCon))
            {

                DataTable dt = new DataTable();
                sda.Fill(dt);

                comboBox1.DataSource = dt;

                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Value";
                comboBox1.Enabled = true;
                this.comboBox1.SelectedIndex = -1;
            }
        }
        void FillComboBox2()
        {


            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Title FROM Project ORDER BY Title", sqlCon))
            {
                
                DataTable dt = new DataTable();
                sda.Fill(dt);
         
                comboBox2.DataSource = dt;
               
                comboBox2.DisplayMember = "Title";
                comboBox2.ValueMember = "Title";
                comboBox2.Enabled = true;
                this.comboBox2.SelectedIndex = -1;
            }
        }
        void FillComboBox3()
        {


            using (SqlDataAdapter sda1 = new SqlDataAdapter("SELECT Lookup.Value FROM Lookup WHERE Category='ADVISOR_ROLE'", sqlCon))
            {
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                comboBox3.DataSource = dt1;
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Value";
                comboBox3.Enabled = true;
                this.comboBox3.SelectedIndex = -1;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void AssignAdvior_Load(object sender, EventArgs e)
        {
            FillComboBox1();
            FillComboBox2();
            FillComboBox3();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FinalYearProject_Managment fyp = new FinalYearProject_Managment();
            this.Hide();
            fyp.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AssignAdvior aad = new AssignAdvior();
            this.Hide();
            aad.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblAdvisor.Text = "Please Select from List";

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            lblAdvisor.Text = "";
        }
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblProject.Text = "Please Select from List";

        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            lblProject.Text = "";
        }
        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblAR.Text = "Please Select from List";

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            lblAR.Text = "";
        }
    }
}
