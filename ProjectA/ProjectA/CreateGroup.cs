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
    public partial class CreateGroup : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
       

        public CreateGroup()
        {
            InitializeComponent();

            textBox1.Visible = false;

        }

        private void CreateGroup_Load(object sender, EventArgs e)
        {

            FillGridView();






        }






         

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

              
            if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
            
            
            SqlCommand sqlCmdd = new SqlCommand("INSERT INTO [Group](Created_On) VALUES('" + Convert.ToDateTime(dateTimePicker1.Value) + "')", sqlCon);
            sqlCmdd.ExecuteNonQuery();

            SqlCommand sqlCmd = new SqlCommand("SELECT IDENT_CURRENT('Group') ", sqlCon);
            int id = Convert.ToInt32(sqlCmd.ExecuteScalar());

            MessageBox.Show("Group has been Created And Group Id is:  " + id);

          

            sqlCon.Close();
            dataGridView1.DataSource = null;

            FillGridView();



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
            this .Hide();
            fyp.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateGroup cg = new CreateGroup();
            this.Hide();
            cg.Show();
        }

       
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT Id,Created_On FROM [Group] ORDER BY Id", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 2;
                        dataGridView1.Columns[0].Name = "Id";
                        dataGridView1.Columns[0].HeaderText = "Id";
                        dataGridView1.Columns[0].DataPropertyName = "Id";
                        

                        dataGridView1.Columns[1].Name = "Created_On";
                        dataGridView1.Columns[1].HeaderText = "Created_On";
                        dataGridView1.Columns[1].DataPropertyName = "Created_On";



                        


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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                //   rowNo = index;
                if (e.ColumnIndex == 2 && e.RowIndex == index)
                {
                   
                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells[1].Value.ToString());



                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                   button1.Enabled = false;
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

                    SqlCommand sqlCmd1 = new SqlCommand("UPDATE GroupStudent SET GroupId=@id WHERE GroupId=@Id", sqlCon);
                    sqlCmd1.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox1.Text);
                    sqlCmd1.ExecuteNonQuery();

                    SqlCommand sqlCmd2 = new SqlCommand("UPDATE GroupProject SET GroupId=@id WHERE GroupId=@Id", sqlCon);
                    sqlCmd2.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox1.Text);
                    sqlCmd2.ExecuteNonQuery();

                    SqlCommand sqlCmd3 = new SqlCommand("UPDATE GroupEvaluation SET GroupId=@id WHERE GroupId=@Id", sqlCon);
                    sqlCmd3.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox1.Text);
                    sqlCmd3.ExecuteNonQuery();

                    SqlCommand sqlCmd4 = new SqlCommand("UPDATE [Group] SET  Created_On=@Created  WHERE  Id=@Id", sqlCon);

                    sqlCmd4.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd4.Parameters.AddWithValue("@Created", Convert.ToDateTime(dateTimePicker1.Text));

                    sqlCmd4.ExecuteNonQuery();


                    MessageBox.Show("Information has been Edited");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    button1.Enabled = true;

                    

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
            textBox1.Text = "";
            dateTimePicker1.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd4 = new SqlCommand("DELETE FROM GroupStudent WHERE GroupId=@Id", sqlCon);
                    sqlCmd4.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd4.ExecuteNonQuery();

                    SqlCommand sqlCmd6 = new SqlCommand("DELETE FROM GroupProject WHERE GroupId=@Id", sqlCon);
                    sqlCmd6.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd6.ExecuteNonQuery();

                    SqlCommand sqlCmd7 = new SqlCommand("DELETE FROM GroupEvaluation WHERE GroupId=@Id", sqlCon);
                    sqlCmd7.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd7.ExecuteNonQuery();

                    SqlCommand sqlCmd5 = new SqlCommand("DELETE FROM [Group] WHERE Id=@Id", sqlCon);
                    sqlCmd5.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd5.ExecuteNonQuery();

                    MessageBox.Show("Group has been Deleted");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    button1.Enabled = true;
                    
                    sqlCon.Close();
                    dataGridView1.DataSource = null;

                    FillGridView();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
