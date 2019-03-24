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
    public partial class AssignedAdvisors : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public AssignedAdvisors()
        {
            InitializeComponent();
        }
        void Clear()
        {
            textBox1.Text=textBox2.Text=comboBox3.Text=dataGridView1.Text= "";
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }
        void FillGridView()
        {


          DataGridViewButtonColumn buttonColView = new DataGridViewButtonColumn();
            buttonColView.Name = "View";
            buttonColView.Text = "View";
            buttonColView.UseColumnTextForButtonValue = true;
            





            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                   
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT L1.Value AS Advisor,L2.Title AS ProjectTitle ,L3.Value AS AdviorRole,ProjectAdvisor.AssignmentDate AS AssignmentDate FROM((( ProjectAdvisor INNER JOIN Lookup AS L1 ON  ProjectAdvisor.AdvisorId=L1.Id)INNER JOIN  Project  AS L2 ON ProjectAdvisor.ProjectId=L2.Id)INNER JOIN Lookup AS L3 ON ProjectAdvisor.AdvisorRole=L3.Id )", sqlCon);

                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 4;
                        dataGridView1.Columns[0].Name = "Advisor";
                        dataGridView1.Columns[0].HeaderText = "Advisor";
                        dataGridView1.Columns[0].DataPropertyName = "Advisor";
                       
                       // dataGridView1.Columns[0].Visible = false;s

                        dataGridView1.Columns[1].Name = "ProjectTitle";
                        dataGridView1.Columns[1].HeaderText = "ProjectTitle";
                        dataGridView1.Columns[1].DataPropertyName = "ProjectTitle";



                        dataGridView1.Columns[2].Name = "AdviorRole";
                        dataGridView1.Columns[2].HeaderText = "AdviorRole";
                        dataGridView1.Columns[2].DataPropertyName = "AdviorRole";


                        dataGridView1.Columns[3].Name = "AssignmentDate";
                        dataGridView1.Columns[3].HeaderText = "AssignmentDate";
                        dataGridView1.Columns[3].DataPropertyName = "AssignmentDate";

                        


                        dataGridView1.Columns.Add(buttonColView);




                    }

                    sqlCon.Close();
                    dataGridView1.DataSource = dtbl;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AssignedAdvisors_Load(object sender, EventArgs e)
        {
            FillGridView();
           
            FillComboBox3();
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
            AssignedAdvisors aa = new AssignedAdvisors();
            this.Hide();
            aa.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd4 = new SqlCommand("SELECT Id FROM  Project WHERE Title='" + textBox2.Text + "'", sqlCon);
                    int input = Convert.ToInt32(sqlCmd4.ExecuteScalar());
                    SqlCommand sqlCmd5 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value ='" + textBox1.Text + "'", sqlCon);
                    int input1 = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                    SqlCommand sqlCmd7 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value ='" + comboBox3.Text + "'", sqlCon);
                    int input2 = Convert.ToInt32(sqlCmd7.ExecuteScalar());
                   
                    if (comboBox3.Text == "" && textBox1.Text == "" && textBox2.Text == "")
                    {
                        lblAdvisor.Text = "Advisor is required!";
                        lblAR.Text = "AdvisorRole is required!";
                        lblProject.Text = "Project is required!";

                    }
                    
                    else
                    {
                        if (textBox1.Text == "")
                        {
                            lblAdvisor.Text = "Advisor is required!";
                        }
                        if (textBox2.Text == "")
                        {
                            lblProject.Text = "Project is required!";
                        }
                        if (comboBox3.Text == "")
                        {
                            lblAR.Text = "AdvisorRole is required!";
                        }

                    }
                    if (textBox1.Text != "" && textBox2.Text != "" && comboBox3.Text != "" )
                    {
                        SqlCommand sqlCmd = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + textBox1.Text + "'", sqlCon);
                        int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + comboBox3.Text + "'", sqlCon);
                        int id1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                        SqlCommand sqlCmd2 = new SqlCommand("SELECT Id FROM  Project WHERE  Title= '" + textBox2.Text + "'", sqlCon);
                        int id2 = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                        string query1 = "UPDATE  ProjectAdvisor SET AdvisorId='"+id+"',ProjectId='"+ id2+"',AdvisorRole='"+ id1+"',AssignmentDate=@AssignmentDate WHERE ProjectId='"+ id2 +"' AND AdvisorId='"+ id+"'";
                        SqlCommand sqlCmd6 = new SqlCommand(query1, sqlCon);
                        sqlCmd6.Parameters.AddWithValue("@AssignmentDate", Convert.ToDateTime(dateTimePicker1.Value));
                       


                        sqlCmd6.ExecuteNonQuery();
                        Clear();



                        MessageBox.Show("Information has been Updated!");

                    }
                    sqlCon.Close();
                    dataGridView1.DataSource = null;
                    FillGridView();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
               // rowNo = index;
                if (e.ColumnIndex == 4 && e.RowIndex == index)
                {
                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                   textBox2.Text = selectedRow.Cells[1].Value.ToString();
                    comboBox3.Text = selectedRow.Cells[2].Value.ToString();
                   

                    dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells[3].Value.ToString());

                   


                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
