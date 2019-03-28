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
    public partial class UpdateGroupStudent : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");

        public UpdateGroupStudent()
        {
            InitializeComponent();
        }



        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateGroupStudent_Load(object sender, EventArgs e)
        {
            FillGridView();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
          

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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT GroupStudent.StudentId,Student.RegistrationNo,GroupStudent.GroupId,GroupStudent.AssignmentDate As Date,Lookup.Value FROM ((GroupStudent INNER JOIN Student ON GroupStudent.StudentId=Student.Id)INNER JOIN Lookup ON GroupStudent.Status=Lookup.Id)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 5;
                        dataGridView1.Columns[0].Name = "StudentId";
                        dataGridView1.Columns[0].HeaderText = "StudentId";
                        dataGridView1.Columns[0].DataPropertyName = "StudentId";


                        dataGridView1.Columns[1].Name = "RegistrationNo";
                        dataGridView1.Columns[1].HeaderText = "RegistrationNo";
                        dataGridView1.Columns[1].DataPropertyName = "RegistrationNo";

                        dataGridView1.Columns[2].Name = "GroupId";
                        dataGridView1.Columns[2].HeaderText = "GroupId";
                        dataGridView1.Columns[2].DataPropertyName = "GroupId";

                        dataGridView1.Columns[3].Name = "Date";
                        dataGridView1.Columns[3].HeaderText = "AssginmentDate";
                        dataGridView1.Columns[3].DataPropertyName = "Date";

                        dataGridView1.Columns[4].Name = "Value";
                        dataGridView1.Columns[4].HeaderText = "Status";
                        dataGridView1.Columns[4].DataPropertyName = "Value";

                      

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
            CreateGroup cg = new CreateGroup();
            this.Hide();
            cg.Show();
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateGroupStudent ugs = new UpdateGroupStudent();
            this.Hide();
            ugs.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (sqlCon.State == ConnectionState.Closed)
                try
            {
                sqlCon.Open();
               



                SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM Lookup WHERE Value='" + cmbstatus.Text + "'", sqlCon);
                int id1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());

                

                    if (cmbstatus.Text == "" )
                    {
                        lblStatus.Text = "Status Required!";
                       

                    }
                    else
                    {
                        if (cmbstatus.Text == "")
                        {
                            lblStatus.Text = "Status Required!";

                        }
                      

                    }
                if (cmbstatus.Text != "")
                {
                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE GroupStudent SET Status='"+id1+"',AssignmentDate=@date WHERE GroupId=@Id AND StudentId=@sId", sqlCon);
                        sqlCmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox3.Text);
                        sqlCmd3.Parameters.AddWithValue("@sId", Convert.ToInt32(textBox2.Text));
                       
                        sqlCmd3.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Text));

                        sqlCmd3.ExecuteNonQuery();


                        MessageBox.Show("Information has been Edited");
                        Clear();
                        btnDelete.Enabled = false;
                        btnEdit.Enabled = false;
                       
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
        void Clear()
        {
            textBox2.Text = textBox1.Text = cmbstatus.Text = textBox3.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                //   rowNo = index;
                if (e.ColumnIndex == 5 && e.RowIndex == index)
                {

                    textBox2.Text = selectedRow.Cells[0].Value.ToString();
                    textBox1.Text = selectedRow.Cells[1].Value.ToString();
                   textBox3.Text = selectedRow.Cells[2].Value.ToString();
                   cmbstatus.Text = selectedRow.Cells[4].Value.ToString();


                    dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells[3].Value.ToString());



                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbstatus_SelectedValueChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void cmbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            cmbstatus.Text = "Select from list";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + cmbstatus.Text + "'", sqlCon);
                    int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                   
                    SqlCommand sqlCmd3 = new SqlCommand("DELETE FROM GroupStudent WHERE GroupId='" + Convert.ToInt32(textBox3.Text)+ "'AND StudentId='" + Convert.ToInt32(textBox2.Text) + "'", sqlCon);


                    sqlCmd3.ExecuteNonQuery();



                    MessageBox.Show("Information has been Deleted");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;



                    sqlCon.Close();
                    dataGridView1.DataSource = null;

                    FillGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}