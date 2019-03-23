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
    public partial class AvailableAdvisors : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public AvailableAdvisors()
        {
            InitializeComponent();
            textBox1.Visible = false;
        }

        private void AvailableAdvisors_Load(object sender, EventArgs e)
        {
            FillGridView();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

        }
        void Clear()
        {
            textBox3.Text = textBox2.Text = textBox1.Text = "";
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT Advisor.Id,Lookup.Value,Advisor.Salary FROM Advisor INNER JOIN Lookup ON Advisor.Designation=Lookup.Id", sqlCon);
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

                        dataGridView1.Columns[1].Name = "Value";
                        dataGridView1.Columns[1].HeaderText = "Designation";
                        dataGridView1.Columns[1].DataPropertyName = "Value";



                        dataGridView1.Columns[2].Name = "Salary";
                        dataGridView1.Columns[2].HeaderText = "Salary";
                        dataGridView1.Columns[2].DataPropertyName = "Salary";


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                //   rowNo = index;
                if (e.ColumnIndex == 3 && e.RowIndex == index)
                {
                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                    textBox2.Text = selectedRow.Cells[1].Value.ToString();
                    textBox3.Text = selectedRow.Cells[2].Value.ToString();


                    lblDesignation.Text = "*";
                    lblSalary.Text = "*";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    textBox2.ReadOnly = true;


                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                try
                {
                    sqlCon.Open();
                    if (textBox2.Text == "" && textBox3.Text == "")
                    {
                        lblDesignation.Text = "Designation is required!";
                        lblSalary.Text = "Salary is required!";

                    }

                    else
                    {
                        if (textBox2.Text == "")
                        {
                            lblDesignation.Text = "Designation is required!";

                        }
                        if (textBox3.Text == "")
                        {
                            lblSalary.Text = "Salary is required!";
                        }

                    }
                    if (textBox2.Text != "" && textBox3.Text != "")
                    {

                        SqlCommand sqlCmd4 = new SqlCommand("UPDATE Advisor SET Salary=@Salary WHERE Advisor.Id=@Id", sqlCon);

                        sqlCmd4.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                        sqlCmd4.Parameters.AddWithValue("@Salary", textBox3.Text);

                        sqlCmd4.ExecuteNonQuery();


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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblSalary.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd4 = new SqlCommand("DELETE FROM ProjectAdvisor WHERE AdvisorId=@Id", sqlCon);
                    sqlCmd4.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd4.ExecuteNonQuery();

                    SqlCommand sqlCmd5 = new SqlCommand("DELETE FROM Advisor WHERE Id=@Id", sqlCon);
                    sqlCmd5.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd5.ExecuteNonQuery();

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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AvailableAdvisors ava = new AvailableAdvisors();
            this.Hide();
            ava.Show();
        }
    }
}
