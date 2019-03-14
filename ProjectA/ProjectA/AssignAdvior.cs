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
using System.Configuration;

namespace ProjectA
{
    
    public partial class AssignAdvior : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public AssignAdvior()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)

                sqlCon.Open();
           
            SqlCommand sqlCmd = new SqlCommand("INSERT INTO ProjectAdvisor(AdvisorId,ProjectId,AdvisorRole,AssignmentDate) VALUES((SELECT Id From Lookup WHERE  Value ='" + comboBox3.Text + "'), (SELECT Id From Project WHERE  Title ='" + comboBox2.Text + "'),(SELECT Id From Lookup WHERE  Value ='" + comboBox3.Text + "'),'"+Convert.ToDateTime(dateTimePicker1.Text)+"')", sqlCon);

            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            MessageBox.Show("Assigned");

            //AssignAdvior assign = new AssignAdvior();
           // this.Hide();
           // assign.Show();




        }
        void FillComboBox2()
        {


            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Title FROM Project", sqlCon))
            {
                //Fill the DataTable with records from Table.
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Insert the Default Item to DataTable.
                DataRow row = dt.NewRow();
                row[0] ="Please Select";
               // row[1] = "Please select";
                dt.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "Title";
                comboBox2.ValueMember = "Title";
            }
        }
        void FillComboBox3()
        {


            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Lookup.Value FROM Lookup INNER JOIN Advisor ON Advisor.Designation=Lookup.Id;" , sqlCon))
            {
                //Fill the DataTable with records from Table.
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Insert the Default Item to DataTable.
                DataRow row = dt.NewRow();
                row[0] = "Please Select";
               // row[1] = "Please select"s;
                dt.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Value";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void AssignAdvior_Load(object sender, EventArgs e)
        {
            FillComboBox2();
            FillComboBox3();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
