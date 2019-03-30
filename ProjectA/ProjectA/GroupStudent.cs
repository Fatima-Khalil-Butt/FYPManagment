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
    public partial class GroupStudent : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public GroupStudent()
        {
            InitializeComponent();
            listBox1.Visible = false;
        }

        private void GroupStudent_Load(object sender, EventArgs e)
        {
            FillComboBox1();
            FillComboBox2();


        }
        void select()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Student.Id,Student.RegistrationNo FROM Student WHERE Student.Id NOT IN (SELECT StudentId FROM GroupStudent)", sqlCon))
            {

                DataTable dt = new DataTable();
                sda.Fill(dt);



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (listBox2.GetSelected(i) == true)
                    {
                        listBox1.Items.Add(dt.Rows[i][1].ToString());

                    }
                }


            }
        }
        void Remove()
        {
            int c = listBox1.Items.Count;
            for (int i = c - 1; i >= 0; --i)
            {
                listBox1.Items.Remove(listBox1.Items[i]);

            }



        }

        void FillComboBox1()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Student.Id,Student.RegistrationNo FROM Student WHERE Student.Id NOT IN (SELECT StudentId FROM GroupStudent) ", sqlCon))
            {

                DataTable dt = new DataTable();
                sda.Fill(dt);
                listBox2.DataSource = dt;
                listBox2.DisplayMember = "RegitrationNo";
                listBox2.ValueMember = "RegistrationNo";

                listBox2.Enabled = true;
                this.listBox2.SelectedIndex = -1;
            }


        }
        void FillComboBox2()
        {
            using (SqlDataAdapter s = new SqlDataAdapter("SELECT Id FROM [Group] ORDER BY Id", sqlCon))
            {

                DataTable dt1 = new DataTable();
                s.Fill(dt1);
               comboBox2.DataSource = dt1;
               comboBox2.DisplayMember = "Id";
               comboBox2.ValueMember = "Id";

                comboBox2.Enabled = true;
                this.comboBox2.SelectedIndex = -1;
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
            GroupStudent cg = new GroupStudent();
            this.Hide();
            cg.Show();
        }
        private void cmbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblStatus.Text = " Select From List";

        }
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            label6.Text = " Select From List";

        }

        private void cmbstatus_SelectedValueChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            label6.Text = "";
        }
        void Clear()
        {
            cmbstatus.Text = comboBox2.Text = dateTimePicker1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                    select();




                    SqlCommand sqlCmd4 = new SqlCommand("SELECT Id FROM Lookup WHERE Value='" + cmbstatus.Text + "'", sqlCon);
                    int id4 = Convert.ToInt32(sqlCmd4.ExecuteScalar());

                    SqlCommand sqlCmd14 = new SqlCommand("SELECT COUNT(GroupId) FROM GroupStudent WHERE GroupId='" + comboBox2.Text + "'", sqlCon);
                    int countId = Convert.ToInt32(sqlCmd14.ExecuteScalar());

                    int count = listBox1.Items.Count;
                    if (cmbstatus.Text == "" && comboBox2.Text == "")
                    {
                        lblStatus.Text = "Status Required!";
                        label6.Text = "Group Id Required!";
                        Remove();
                    }

                    else if (count > 3)
                    {

                        MessageBox.Show("You can Select Maximum 3 Students!");
                        Remove();

                    }
                    else if (count == 0)
                    {
                        MessageBox.Show("You have to Select atleast one Student!");

                    }
                    else if (countId == 3)
                    {
                        MessageBox.Show("Maximum 3 Students Can be Added ....No Space For More Students!");
                        Remove();
                    }
                    else
                    {
                        if (cmbstatus.Text == "")
                        {
                            lblStatus.Text = "Status Required!";
                            Remove();

                        }
                        if (comboBox2.Text == "")
                        {
                            label6.Text = "Group Id Required!";
                            Remove();
                        }

                    }
                    if (cmbstatus.Text != "" && comboBox2.Text != "" && count > 0 && count <= 3 && countId >= 0 && countId < 3)
                    {
                        if (count == 3 && countId == 0)
                        {

                            SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM  Student WHERE RegistrationNo='" + listBox1.Items[0].ToString() + "'", sqlCon);
                            int id1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                            SqlCommand sqlCmd2 = new SqlCommand("SELECT Id FROM Student WHERE RegistrationNo='" + listBox1.Items[1].ToString() + "'", sqlCon);
                            int id2 = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                            SqlCommand sqlCmd3 = new SqlCommand("SELECT Id FROM Student WHERE RegistrationNo='" + listBox1.Items[2].ToString() + "'", sqlCon);
                            int id3 = Convert.ToInt32(sqlCmd3.ExecuteScalar());


                            SqlCommand sqlCmd5 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id1 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd5.ExecuteNonQuery();
                            SqlCommand sqlCmd6 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id2 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd6.ExecuteNonQuery();
                            SqlCommand sqlCmd7 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id3 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd7.ExecuteNonQuery();
                            MessageBox.Show("  " + count + " Students  has been added to this Group");
                            Remove();
                            Clear();

                            FillComboBox1();
                        }
                        else if (count == 2 && countId >= 0 && countId <= 1)
                        {
                            SqlCommand sqlCmd8 = new SqlCommand("SELECT Id FROM  Student WHERE RegistrationNo='" + listBox1.Items[0].ToString() + "'", sqlCon);
                            int id8 = Convert.ToInt32(sqlCmd8.ExecuteScalar());
                            SqlCommand sqlCmd9 = new SqlCommand("SELECT Id FROM Student WHERE RegistrationNo='" + listBox1.Items[1].ToString() + "'", sqlCon);
                            int id9 = Convert.ToInt32(sqlCmd9.ExecuteScalar());


                            SqlCommand sqlCmd10 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id8 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd10.ExecuteNonQuery();
                            SqlCommand sqlCmd11 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id9 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd11.ExecuteNonQuery();
                            MessageBox.Show(" " + count + " Students has been added to this Group");
                            Remove();
                            Clear();

                            FillComboBox1();
                        }
                        else if (count == 1 && countId >= 0 && countId <= 2)
                        {
                            SqlCommand sqlCmd12 = new SqlCommand("SELECT Id FROM  Student WHERE RegistrationNo='" + listBox1.Items[0].ToString() + "'", sqlCon);
                            int id12 = Convert.ToInt32(sqlCmd12.ExecuteScalar());


                            SqlCommand sqlCmd13 = new SqlCommand("INSERT INTO GroupStudent(GroupId,StudentId,Status,AssignmentDate) VALUES('" + Convert.ToInt32(comboBox2.Text) + "','" + id12 + "','" + id4 + "','" + Convert.ToDateTime(dateTimePicker1.Text) + "')", sqlCon);
                            sqlCmd13.ExecuteNonQuery();
                            MessageBox.Show(" " + count + " Students  has been added to this Group");
                            Remove();
                            Clear();

                            FillComboBox1();
                        }
                        else
                        {
                            MessageBox.Show("No Of Selected Students Exceeding the Limit of Students In Groups");
                            Remove();
                        }
                    }
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }




        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateGroupStudent ugs = new UpdateGroupStudent();
            this.Hide();
            ugs.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
