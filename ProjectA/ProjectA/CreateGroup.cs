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
        public static int x;
        public static int y;

        public CreateGroup()
        {
            InitializeComponent();
           // listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);



        }

        private void CreateGroup_Load(object sender, EventArgs e)
        {
            FillComboBox1();


        }



        /*  
             x = checkedListBox1.SelectedIndex;
                 if (checkedListBox1.GetSelected(x) == true)
                 {
                     label3.Text = checkedListBox1.Text.ToString();

                 }

             */



        /* void select()
         {
            /* checkedListBox1.Sorted = true;
             checkedListBox1.SelectionMode = SelectionMode.MultiExtended;
             int x;
             for (x = 0; x <= checkedListBox1.Items.Count - 1; x++)
             {
                 if (checkedListBox1.GetSelected(x) == true)
                 {
                     checkedListBox1.SetSelected(x, true);
                 }

             }
         }*/

        void FillComboBox1()
        {


            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id,RegistrationNo FROM Student", sqlCon))
            {
                //Fill the DataTable with records from Table.
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Insert the Default Item to DataTable.
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Please select";
                dt.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
            checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "RegitrationNo";
                checkedListBox1.ValueMember = "RegistrationNo";
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void label5_Click(object sender, EventArgs e)
        {
            /*
            y = checkedListBox1.SelectedIndex;
            if (checkedListBox1.GetSelected(y) == true && y != x)
            {
                label5.Text = checkedListBox1.Text.ToString();

            }*/
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
           
           //essageBox.Show(SelectedValues);
        }
    }
}
