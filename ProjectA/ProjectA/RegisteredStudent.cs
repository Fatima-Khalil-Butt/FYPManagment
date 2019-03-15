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
    public partial class RegisteredStudent : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public static int rowNo;
        public RegisteredStudent()
        {
            InitializeComponent();
            textBox1.Visible = false;
           
        }
        void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = comboBox1.Text = "";
            btnDelete.Enabled = false;
            btn_Edit.Enabled = false;
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT Person.Id,Person.FirstName," +
                        "Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Lookup.Value," +
                        "Student.RegistrationNo FROM (( Person INNER JOIN  Lookup ON  Lookup.Id=Person.Gender ) " +
                        "INNER JOIN  Student ON Person.Id=Student.Id)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 8;
                        dataGridView1.Columns[0].Name = "Id";
                        dataGridView1.Columns[0].HeaderText = "Id";
                        dataGridView1.Columns[0].DataPropertyName = "Id";
                        dataGridView1.Columns[0].Visible = false;

                        dataGridView1.Columns[1].Name = "FirstName";
                        dataGridView1.Columns[1].HeaderText = "FirstName";
                        dataGridView1.Columns[1].DataPropertyName = "FirstName";



                        dataGridView1.Columns[2].Name = "LastName";
                        dataGridView1.Columns[2].HeaderText = "LastName";
                        dataGridView1.Columns[2].DataPropertyName = "LastName";


                        dataGridView1.Columns[3].Name = "Contact";
                        dataGridView1.Columns[3].HeaderText = "Contact";
                        dataGridView1.Columns[3].DataPropertyName = "Contact";


                        dataGridView1.Columns[4].Name = "Email";
                        dataGridView1.Columns[4].HeaderText = "Email";
                        dataGridView1.Columns[4].DataPropertyName = "Email";


                        dataGridView1.Columns[5].Name = "DateOfBirth";
                        dataGridView1.Columns[5].HeaderText = "DateOfBirth";
                        dataGridView1.Columns[5].DataPropertyName = "DateOfBirth";


                        dataGridView1.Columns[6].Name = "Value";
                        dataGridView1.Columns[6].HeaderText = "Gender";
                        dataGridView1.Columns[6].DataPropertyName = "Value";


                        dataGridView1.Columns[7].Name = "RegistrationNo";
                        dataGridView1.Columns[7].HeaderText = "RegistrationNo";
                        dataGridView1.Columns[7].DataPropertyName = "RegistrationNo";



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

        private void RegisteredStudent_Load(object sender, EventArgs e)
        {
           
           
            FillGridView();
            btnDelete.Enabled = false;
            btn_Edit.Enabled = false;
           

           



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            rowNo = index;
            if (e.ColumnIndex == 8 && e.RowIndex == index)
            {
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox2.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
                textBox4.Text = selectedRow.Cells[3].Value.ToString();
                textBox5.Text = selectedRow.Cells[4].Value.ToString();
                
               dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells[5].Value.ToString());
           
                comboBox1.Text = selectedRow.Cells[6].Value.ToString();
                textBox6.Text = selectedRow.Cells[7].Value.ToString();

                lblFirstName.Text = "*";
                lblLastName.Text = "*";
                lblContact.Text = "*";
                lblEmail.Text = "*";
                lblGender.Text = "*";
                lblRegistrationNo.Text = "*";

                btn_Edit.Enabled = true;
                btnDelete.Enabled = true;

            }
          
         
            
               
              
            

        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        

        private void dataGridView1_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {

        }

        private void dataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CancelRowEdit(object sender, QuestionEventArgs e)
        {
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        

        private void RegisteredStudent_Click(object sender, EventArgs e)
        {

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(Student.Id) FROM Student  WHERE  RegistrationNo='" + textBox6.Text + "' AND Id!='"+textBox1.Text+"'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == ""
                        && textBox4.Text == "" && comboBox1.Text == "" && textBox6.Text == "")
                    {
                        lblFirstName.Text = "FirstName is required!";
                        lblLastName.Text = "LastName is required!";
                        lblContact.Text = "Contact is required!";
                        lblEmail.Text = "Email is required!";
                        lblGender.Text = "Gender is required!";
                        lblRegistrationNo.Text = "RegistrationNo. is required!";
                    }
                    else if (count > 0)
                    {
                        lblRegistrationNo.Text = "Already Exist!";
                    }
                    else
                    {
                        if (textBox2.Text == "")
                        {
                            lblFirstName.Text = "FirstName is required!";
                        }
                        if (textBox3.Text == "")
                        {
                            lblLastName.Text = "LastName is required!";
                        }
                        if (textBox4.Text == "")
                        {
                            lblContact.Text = "Contact is required!";
                        }
                        if (textBox5.Text == "")
                        {
                            lblEmail.Text = "Email is required!";
                        }
                        if (comboBox1.Text == "" && comboBox1.Text != "Male" && comboBox1.Text != "Female")
                        {
                            lblGender.Text = "Gender is required!";
                        }
                        if (textBox6.Text == "")
                        {
                            lblRegistrationNo.Text = "RegistrationNo. is required!";
                        }
                    }
                   
                    if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != ""
                       && textBox5.Text != "" && comboBox1.Text != "" && textBox6.Text != "" && count==0)
                    {

                        SqlCommand sqlCmd1 = new SqlCommand("SELECT Id FROM  Lookup WHERE Value= '" + comboBox1.Text + "'", sqlCon);
                        int input = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                        string query1 = "UPDATE  Person SET FirstName=@FirstName,LastName=@LastName,Contact=@Contact," +
                            "Email=@Email,DateOfBirth=@DateOfBirth,Gender='" + input + "' WHERE Person.Id=@Id";
                        SqlCommand sqlCmd2 = new SqlCommand(query1, sqlCon);

                        sqlCmd2.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                        sqlCmd2.Parameters.AddWithValue("@FirstName", textBox2.Text);
                        sqlCmd2.Parameters.AddWithValue("@LastName", textBox3.Text);
                        sqlCmd2.Parameters.AddWithValue("@Contact", textBox4.Text);
                        sqlCmd2.Parameters.AddWithValue("@Email", textBox5.Text);
                        sqlCmd2.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateTimePicker1.Value));
                        // sqlCmd1.Parameters.AddWithValue("@Gender", selectedRow.Cells[6].Value);



                        sqlCmd2.ExecuteNonQuery();


                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE Student SET RegistrationNo=@RegistrationNo WHERE Student.Id=@Id", sqlCon);
                        sqlCmd3.Parameters.Add("@RegistrationNo", SqlDbType.VarChar).Value = textBox6.Text;
                        sqlCmd3.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(textBox1.Text);
                        sqlCmd3.ExecuteNonQuery();

                        MessageBox.Show("Information has been Edited");
                        Clear();
                       
                        btnDelete.Enabled = false;
                        btn_Edit.Enabled = false;
                       
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            try
            {
                SqlCommand sqlCmd1 = new SqlCommand("DELETE FROM Student WHERE Id= '" + Convert.ToInt32(textBox1.Text) + "'", sqlCon);
                sqlCmd1.ExecuteNonQuery();
                string query1 = "DELETE FROM Person WHERE Id='" + Convert.ToInt32(textBox1.Text) + "'";
                SqlCommand sqlCmd2 = new SqlCommand(query1, sqlCon);


                sqlCmd2.ExecuteNonQuery();
                MessageBox.Show("Information has been Deleted");
                Clear();
                btnDelete.Enabled = false;
                btn_Edit.Enabled = false;
                
                sqlCon.Close();
                dataGridView1.DataSource = null;

                FillGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void lblLastName_Click(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void lblContact_Click(object sender, EventArgs e)
        {

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
            Register reg = new Register();
            this.Hide();
            reg.Show();

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisteredStudent regstudent = new RegisteredStudent();
            this.Hide();
            regstudent.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            lblFirstName.Text = "";
            char chr = e.KeyChar;
            if (!char.IsLetter(chr) && chr != 8)
            {
                e.Handled = true;
                lblFirstName.Text = " Characters Only!";
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblLastName.Text = "";
            char chr1 = e.KeyChar;
            if (!char.IsLetter(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblLastName.Text = "Characters Only!";
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblContact.Text = "";
            char chr1 = e.KeyChar;
            if (!char.IsDigit(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblContact.Text = "Numeric Value Only!";
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblEmail.Text = "";

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblGender.Text = "Please Select From List";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            lblGender.Text = "";
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblRegistrationNo.Text = "";
        }
    }
    }

