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
    public partial class Evaluation : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public Evaluation()
        {
            InitializeComponent();
            textBox1.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(Evaluation.Id) FROM Evaluation WHERE  Name='" + txtName.Text + "'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());


                    if (txtName.Text == "" && txtmarks.Text == "" && txtWeightage.Text == "")
                    {
                        lblName.Text = "TaskName is required!";
                        lblTotalMarks.Text = "TotalMarks is required!";
                        lblTotalWeight.Text = "TotalWeightage is required!";

                    }
                    else if(count>0)
                    {
                        lblName.Text = "Already exist!";
                    }
                    else
                    {
                        if (txtName.Text == "")
                        {
                            lblName.Text = "TaskName is required!";
                        }
                        if (txtmarks.Text == "")
                        {
                            lblTotalMarks.Text = "TotalMarks is required!";
                        }
                        if (txtWeightage.Text == "")
                        {
                            lblTotalWeight.Text = "TotalWeightage is required!";
                        }

                    }
                    if (txtmarks.Text != "" && txtmarks.Text != "" && txtWeightage.Text != "" && count==0)
                    {
                        SqlCommand sqlCmd1 = new SqlCommand("INSERT INTO Evaluation(Name,TotalMarks,TotalWeightage) VALUES('" + txtName.Text + "','" + Convert.ToInt32(txtmarks.Text) + "','" + Convert.ToInt32(txtWeightage.Text) + "')", sqlCon);

                        sqlCmd1.ExecuteNonQuery();
                        
                        MessageBox.Show("Data has been Added");
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

        private void Evaluation_Load(object sender, EventArgs e)
        {
            FillGridView();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        void Clear()
        {
            txtWeightage.Text = txtmarks.Text = txtName.Text = textBox1.Text="";
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT Evaluation.Id,Evaluation.Name, Evaluation.TotalMarks,Evaluation.TotalWeightage FROM Evaluation ORDER BY Name", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 4;
                        dataGridView1.Columns[0].Name = "Id";
                        dataGridView1.Columns[0].HeaderText = "Id";
                        dataGridView1.Columns[0].DataPropertyName = "Id";
                        dataGridView1.Columns[0].Visible = false;

                        dataGridView1.Columns[1].Name = "Name";
                        dataGridView1.Columns[1].HeaderText = "Name";
                        dataGridView1.Columns[1].DataPropertyName = "Name";



                        dataGridView1.Columns[2].Name = "TotalMarks";
                        dataGridView1.Columns[2].HeaderText = "TotalMarks";
                        dataGridView1.Columns[2].DataPropertyName = "TotalMarks";

                        dataGridView1.Columns[3].Name = "TotalWeightage";
                        dataGridView1.Columns[3].HeaderText = "TotalWeightage";
                        dataGridView1.Columns[3].DataPropertyName = "TotalWeightage";


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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd7= new SqlCommand("SELECT COUNT(Evaluation.Id) FROM Evaluation  WHERE  Name='" + txtName.Text + "' AND Id!='" + textBox1.Text + "'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd7.ExecuteScalar());
                    if (txtName.Text == "" && txtmarks.Text == "" && txtWeightage.Text == "")
                    {
                        lblName.Text = "TaskName is required!";
                        lblTotalMarks.Text = "TotalMarks is required!";
                        lblTotalWeight.Text = "TotalWeightage is required!";

                    }
                    else if(count>0)
                    {
                        lblName.Text = "Name Already Exist!";

                    }
                    else
                    {
                        if (txtName.Text == "")
                        {
                            lblName.Text = "TaskName is required!";
                        }
                        if (txtmarks.Text == "")
                        {
                            lblTotalMarks.Text = "TotalMarks is required!";
                        }
                        if (txtWeightage.Text == "")
                        {
                            lblTotalWeight.Text = "TotalWeightage is required!";
                        }

                    }
                    if (txtName.Text != "" && txtmarks.Text != "" && txtWeightage.Text != "")
                    {


                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE Evaluation SET  Name=@Name,TotalMarks=@TotalMarks,TotalWeightage=@TotalWeightage  WHERE  Id=@Id", sqlCon);

                        sqlCmd3.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                        sqlCmd3.Parameters.AddWithValue("@Name", txtName.Text);
                        sqlCmd3.Parameters.AddWithValue("@TotalMarks", Convert.ToInt32(txtmarks.Text));
                        sqlCmd3.Parameters.AddWithValue("@TotalWeightage", Convert.ToInt32(txtWeightage.Text));
                        sqlCmd3.ExecuteNonQuery();


                        MessageBox.Show("Information has been Edited");
                        Clear();
                        btnDelete.Enabled = false;
                        btnEdit.Enabled = false;
                        btnAdd.Enabled = true;
                       


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
                if (e.ColumnIndex == 4 && e.RowIndex == index)
                {
                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                    txtName.Text = selectedRow.Cells[1].Value.ToString();
                    txtmarks.Text = selectedRow.Cells[2].Value.ToString();
                    txtWeightage.Text = selectedRow.Cells[3].Value.ToString();


                    lblName.Text = "*";
                    lblTotalMarks.Text = "*";
                    lblTotalWeight.Text = "*";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = false;
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



                    SqlCommand sqlCmd5 = new SqlCommand("DELETE FROM Evaluation WHERE Id=@Id", sqlCon);
                    sqlCmd5.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
                    sqlCmd5.ExecuteNonQuery();

                    MessageBox.Show("Information has been Deleted");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    btnAdd.Enabled = true;

                   
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
            Evaluation ev = new Evaluation();
            this.Hide();
            ev.Show();
        }

        private void txtmarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTotalMarks.Text = "";
            char chr1 = e.KeyChar;
            if (!char.IsDigit(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblTotalMarks.Text = "Numeric Value Only!";
            }
        }

        private void txtWeightage_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTotalWeight.Text = "";
            char chr1 = e.KeyChar;
            if (!char.IsDigit(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblTotalWeight.Text = "Numeric Value Only!";
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblName.Text = "";
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalMarks_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
