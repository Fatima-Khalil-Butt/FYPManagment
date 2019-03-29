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
    public partial class UpdateGroupProject : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public UpdateGroupProject()
        {
            InitializeComponent();
        }

        private void UpdateGroupProject_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            FillComboBox1();
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

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            label14.Text = "";

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            label14.Text = "Select From List";
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter(" SELECT GroupProject.ProjectId,GroupProject.GroupId,GroupProject.AssignmentDate FROM  GroupProject ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    foreach (DataRow item in dtbl.Rows)
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnCount = 3;
                        dataGridView1.Columns[0].Name = "ProjectId";
                        dataGridView1.Columns[0].HeaderText = "ProjectId";
                        dataGridView1.Columns[0].DataPropertyName = "ProjectId";

                        dataGridView1.Columns[1].Name = "GroupId";
                        dataGridView1.Columns[1].HeaderText = "GroupId";
                        dataGridView1.Columns[1].DataPropertyName = "GroupId";

                        dataGridView1.Columns[2].Name = "AssignmentDate";
                        dataGridView1.Columns[2].HeaderText = "AssignmentDate";
                        dataGridView1.Columns[2].DataPropertyName = "AssignmentDate";







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
        void Clear()
        {
            textBox1.Text = comboBox3.Text = dateTimePicker1.Text = "";
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

                    comboBox3.Text = selectedRow.Cells[0].Value.ToString();
                    textBox1.Text = selectedRow.Cells[1].Value.ToString();

                    dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells[2].Value.ToString());



                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateGroupProject upd = new UpdateGroupProject();
            this.Hide();
            upd.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                try
                {

                    sqlCon.Open();



                    if (comboBox3.Text == "")
                    {

                       
                        label14.Text = "Project Id Required!";
                        

                    }

                    else
                    {
                        if (textBox1.Text == "")
                        {
                            label7.Text = "Group Id Required!";


                        }
                        if (comboBox3.Text == "")
                        {
                            label14.Text = "Project Id Required!";

                        }
                       

                    }
                    if (textBox1.Text != "" && comboBox3.Text != "")
                    {

                        SqlCommand sqlCmd3 = new SqlCommand("UPDATE GroupProject SET ProjectId=@ProjectId,AssignmentDate=@AssignmentDate WHERE GroupId=@GroupId", sqlCon);
                        sqlCmd3.Parameters.Add("@GroupId", SqlDbType.VarChar).Value = Convert.ToInt32(textBox1.Text);
                        sqlCmd3.Parameters.Add("@ProjectId", SqlDbType.VarChar).Value = Convert.ToInt32(comboBox3.Text);
                        

                        sqlCmd3.Parameters.AddWithValue("@AssignmentDate", Convert.ToDateTime(dateTimePicker1.Text));

                        sqlCmd3.ExecuteNonQuery();

                        MessageBox.Show("Updated");

                        Clear();
                        
                        FillComboBox1();

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
            if (sqlCon.State == ConnectionState.Closed)
                try
                {
                    sqlCon.Open();


                    SqlCommand sqlCmd3 = new SqlCommand("DELETE FROM GroupProject WHERE GroupId='" + Convert.ToInt32(textBox1.Text) + "'AND  ProjectId='" + Convert.ToInt32(comboBox3.Text) + "'", sqlCon);


                    sqlCmd3.ExecuteNonQuery();



                    MessageBox.Show("Information has been Deleted");
                    Clear();
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;

                    FillComboBox1();

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
