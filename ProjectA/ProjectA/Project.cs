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
            textBox3.Visible = false;
        }

        private void Project_Load(object sender, EventArgs e)
        {
            FillGridView();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        void Clear()
        {
            textBox3.Text = textBox2.Text =textBox1.Text="";
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT Project.Id,Project.Description,Project.Title FROM Project", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 3;
                        dataGridView1.Columns[0].Name = "Id";
                        dataGridView1.Columns[0].HeaderText = "Id";
                        dataGridView1.Columns[0].DataPropertyName = "Id";
                        dataGridView1.Columns[0].Visible = false;

                        dataGridView1.Columns[1].Name = "Description";
                        dataGridView1.Columns[1].HeaderText = "Description";
                        dataGridView1.Columns[1].DataPropertyName = "Description";



                        dataGridView1.Columns[2].Name = "Title";
                        dataGridView1.Columns[2].HeaderText = "Title";
                        dataGridView1.Columns[2].DataPropertyName = "Title";


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

        private void btnnAdd_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(Project.Id) FROM Project  WHERE  Title='" + textBox2.Text + "'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                    {
                        lblDescription.Text = "Description is required!";
                        lblTitle.Text = "Title is required!";

                    }
                    else if(count>0)
                    {
                        lblTitle.Text = "Already Exist!";
                    }

                    else
                    {
                        if (textBox1.Text == "")
                        {
                            lblDescription.Text = "Description is required!";
                        }
                        if (textBox2.Text == "")
                        {
                            lblTitle.Text = "Title is required!";
                        }

                    }
                    if (textBox1.Text != "" && textBox2.Text != ""&&count==0)
                    {
                        SqlCommand sqlCmd1 = new SqlCommand("INSERT INTO Project(Description,Title) VALUES('" + textBox1.Text + "','" + textBox2.Text + "')", sqlCon);

                        sqlCmd1.ExecuteNonQuery();
                         MessageBox.Show("Project has been Added");
                        Clear();
                        


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd1 = new SqlCommand("SELECT COUNT(Project.Id) FROM Project  WHERE  Title='" + textBox2.Text + "' AND Id!='" + textBox3.Text + "'", sqlCon);
                    int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());

                    if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                    {
                        lblDescription.Text = "Description is required!";
                          lblTitle.Text = "Title is required!";

                    }
                    else if(count1>0)
                    {
                        lblTitle.Text = "Already required!";
                    }

                    else
                    {
                        if (textBox1.Text == "")
                        {
                            lblDescription.Text = "Description is required!";
                        }
                        if (textBox2.Text == "")
                        {
                            lblTitle.Text = "Title is required!";
                        }

                    }
                    if (textBox1.Text != "" && textBox2.Text != "" && count1==0)
                    {

                        SqlCommand sqlCmd2 = new SqlCommand("UPDATE ProjectAdvisor SET ProjectId=@id WHERE ProjectId=@Id", sqlCon);
                        sqlCmd2.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox3.Text);
                        sqlCmd2.ExecuteNonQuery();

                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE Project SET Description=@Description,Title=@Title  WHERE  Id=@Id", sqlCon);

                        sqlCmd3.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox3.Text));
                        sqlCmd3.Parameters.AddWithValue("@Description", textBox1.Text);
                        sqlCmd3.Parameters.AddWithValue("@Title", textBox2.Text);
                        sqlCmd3.ExecuteNonQuery();


                        MessageBox.Show("Information has been Edited");
                        Clear();
                        btnDelete.Enabled = false;
                        btnEdit.Enabled = false;
                        btnnAdd.Enabled = true;
                       

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                //   rowNo = index;
                if (e.ColumnIndex == 3 && e.RowIndex == index)
                {
                    textBox3.Text = selectedRow.Cells[0].Value.ToString();
                    textBox1.Text = selectedRow.Cells[1].Value.ToString();
                    textBox2.Text = selectedRow.Cells[2].Value.ToString();


                    lblTitle.Text = "*";
                    lblDescription.Text = "*";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnnAdd.Enabled = false;
                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd4 = new SqlCommand("DELETE FROM ProjectAdvisor WHERE ProjectId=@Id", sqlCon);
                    sqlCmd4.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox3.Text));
                    sqlCmd4.ExecuteNonQuery();

                    SqlCommand sqlCmd5 = new SqlCommand("DELETE FROM Project WHERE Id=@Id", sqlCon);
                    sqlCmd5.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox3.Text));
                    sqlCmd5.ExecuteNonQuery();

                    MessageBox.Show("Information has been Deleted");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    btnnAdd.Enabled = true;
                   
                    sqlCon.Close();
                    dataGridView1.DataSource = null;

                    FillGridView();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FinalYearProject_Managment fyp = new FinalYearProject_Managment();
            this.Hide();
            fyp.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Project p = new Project();
            this.Hide();
            p.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblDescription.Text = "";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTitle.Text = "";
        }

      
    }
}
