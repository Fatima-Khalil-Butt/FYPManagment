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
    public partial class GroupProject : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public GroupProject()
        {
            InitializeComponent();
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GroupProject_Load(object sender, EventArgs e)
        {
            FillComboBox1();
            FillComboBox2();
        }
        void FillComboBox1()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Project.Id FROM Project  WHERE Project.Id NOT IN (SELECT ProjectId FROM GroupProject)", sqlCon))
            {

                DataTable dt = new DataTable();
                sda.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "Id";
                comboBox3.ValueMember = "Id";

                comboBox3.Enabled = true;
                this.comboBox3.SelectedIndex = -1;
            }


        }
        void FillComboBox2()
        {
            using (SqlDataAdapter s = new SqlDataAdapter("SELECT  DISTINCT  Id FROM [Group] JOIN  GroupStudent ON  [Group].Id =GroupStudent.GroupId ORDER BY Id", sqlCon))
            {

                DataTable dt1 = new DataTable();
                s.Fill(dt1);
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "Id";
                comboBox1.ValueMember = "Id";

                comboBox1.Enabled = true;
                this.comboBox1.SelectedIndex = -1;
            }


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
            GroupProject cg = new GroupProject();
            this.Hide();
            cg.Show();
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateGroupProject upd = new UpdateGroupProject();
            this.Hide();
            upd.Show();

        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label7.Text = "";
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            label7.Text = "Select From List";

        }
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            label14.Text = "";

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            label14.Text = "Select From List";
        }
        void Clear()
        {
            comboBox1.Text = comboBox3.Text = dateTimePicker2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (sqlCon.State == ConnectionState.Closed)
                try
                {

                    sqlCon.Open();


                    SqlCommand sqlCmd14 = new SqlCommand("SELECT COUNT(GroupId) FROM GroupProject WHERE GroupId='" + Convert.ToInt32(comboBox1.Text) + "'", sqlCon);
                    int countId = Convert.ToInt32(sqlCmd14.ExecuteScalar());
                   

                    if (comboBox1.Text == "" && comboBox3.Text == "" )
                    {

                        label7.Text = "Group Id Required!";
                        label14.Text = "Project Id Required!";
                       

                    }

                    else if (countId > 0)
                    {
                        MessageBox.Show("Project has been Already  Assigned to this group...choose Another Group!");
                    }
                   
                    else
                    {
                        if (comboBox1.Text == "")
                        {
                            label7.Text = "Group Id Required!";


                        }
                        if (comboBox3.Text == "")
                        {
                            label14.Text = "Project Id Required!";

                        }
                       

                    }
                    if (comboBox1.Text != "" && comboBox3.Text != "" &&  countId == 0)
                    {

                        SqlCommand sqlCmd5 = new SqlCommand("INSERT INTO GroupProject(ProjectId,GroupId,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox3.Text) + "','" + Convert.ToInt32(comboBox1.Text) + "','" + Convert.ToDateTime(dateTimePicker2.Text) + "')", sqlCon);

                        sqlCmd5.ExecuteNonQuery();
                        MessageBox.Show("Project Added to Group");
                        Clear();
                        FillComboBox1();


                    }
                    sqlCon.Close();  

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
