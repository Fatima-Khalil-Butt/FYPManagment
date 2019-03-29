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
    public partial class UpdateGroupEvaluation : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");

        public UpdateGroupEvaluation()
        {
            InitializeComponent();
        }

        private void UpdateGroupEvaluation_Load(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
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
            this.Hide();
            fyp.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GroupEvaluation cg = new GroupEvaluation();
            this.Hide();
            cg.Show();
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            label10.Text = "";
            char chr1 = e.KeyChar;
            if (char.IsLetter(chr1) && chr1 != 8)
            {
                e.Handled = true;
                label10.Text = "Numeric Value Only!";
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label7.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT GroupEvaluation.GroupId,GroupEvaluation.EvaluationId,GroupEvaluation.ObtainedMarks,GroupEvaluation.EvaluationDate FROM  GroupEvaluation ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                       dataGridView1.ColumnCount = 4;
                        dataGridView1.Columns[0].Name = "GroupId";
                        dataGridView1.Columns[0].HeaderText = "GroupId";
                        dataGridView1.Columns[0].DataPropertyName = "GroupId";


                        dataGridView1.Columns[1].Name = "EvaluationId";
                        dataGridView1.Columns[1].HeaderText = "EvaluationId";
                        dataGridView1.Columns[1].DataPropertyName = "EvaluationId";

                        dataGridView1.Columns[2].Name = "ObtainedMarks";
                        dataGridView1.Columns[2].HeaderText = "ObtainedMarks";
                        dataGridView1.Columns[2].DataPropertyName = "ObtainedMarks";

                        dataGridView1.Columns[3].Name = "EvaluationDate";
                        dataGridView1.Columns[3].HeaderText = "EvaluationDate";
                        dataGridView1.Columns[3].DataPropertyName = "EvaluationDate";

                       



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
                if (e.ColumnIndex == 4 && e.RowIndex == index)
                {

                   textBox2.Text = selectedRow.Cells[0].Value.ToString();
                    textBox3.Text = selectedRow.Cells[1].Value.ToString();
                    textBox1.Text = selectedRow.Cells[2].Value.ToString();
                   


                    dateTimePicker2.Value = DateTime.Parse(selectedRow.Cells[3].Value.ToString());



                   btnEdit.Enabled = true;
                   btnDelete.Enabled = true;

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



                    if (textBox1.Text == "" && textBox3.Text == "" && textBox1.Text == "")
                    {

                        label7.Text = "Group Id Required!";
                        label14.Text = "Project Id Required!";
                        label10.Text = "Marks Required!";

                    }

                    else
                    {
                        if (textBox1.Text == "")
                        {
                            label7.Text = "Group Id Required!";


                        }
                        if (textBox3.Text == "")
                        {
                            label14.Text = "Project Id Required!";

                        }
                        if (textBox1.Text == "")
                        {
                            label10.Text = "Marks Required!";

                        }

                    }
                    if (textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != "")
                    {

                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE GroupEvaluation SET ObtainedMarks=@ObtainedMarks,EvaluationDate=@EvaluationDate WHERE GroupId=@GroupId AND EvaluationId=@EvaluationId", sqlCon);
                        sqlCmd3.Parameters.Add("@GroupId", SqlDbType.VarChar).Value = Convert.ToInt32(textBox2.Text);
                        sqlCmd3.Parameters.Add("@EvaluationId", SqlDbType.VarChar).Value = Convert.ToInt32(textBox3.Text);
                        sqlCmd3.Parameters.AddWithValue("@ObtainedMarks", Convert.ToInt32(textBox1.Text));

                        sqlCmd3.Parameters.AddWithValue("@EvaluationDate", Convert.ToDateTime(dateTimePicker2.Text));

                        sqlCmd3.ExecuteNonQuery();

                        MessageBox.Show("Updated");

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
        void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = dateTimePicker2.Text = "";
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateGroupEvaluation upd = new UpdateGroupEvaluation();
            this.Hide();
            upd.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();
                   

                    SqlCommand sqlCmd3 = new SqlCommand("DELETE FROM GroupEvaluation WHERE GroupId='" + Convert.ToInt32(textBox2.Text) + "'AND  EvaluationId='" + Convert.ToInt32(textBox3.Text) + "'", sqlCon);


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
    }
}
