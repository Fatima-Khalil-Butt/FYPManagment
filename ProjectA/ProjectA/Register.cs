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
    public partial class Register : Form
    {
        public static int gender;
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4NQFIN1\FATIMAKHALIL;Initial Catalog=ProjectA;Integrated Security=true;");
        public Register()
        {
            InitializeComponent();
            

        }

        private void Register_Load(object sender, EventArgs e)
        {
           


        }
       
        public void Clear()
        {
            
           txt_FirstName.Text = txt_LastName.Text = txt_Contact.Text=txt_Email.Text=cmb_gender.Text=txt_RegisterationNo.Text="";
           

        }

        private void txt_FirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblFirstName.Text = "";
            char chr = e.KeyChar;
            if (!char.IsLetter(chr) && chr != 8)
            {
                e.Handled = true;
                lblFirstName.Text = " Characters Only!";
            }

        }

        private void txt_LastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblLastName.Text = "";
            char chr1 = e.KeyChar;
            if (!char.IsLetter(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblLastName.Text = "Characters Only!";
            }

        }

        private void txt_Contact_TextChanged(object sender, EventArgs e)
        {


        }

        private void txt_Contact_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblContact.Text = "";
            char chr1 = e.KeyChar;
            if (char.IsLetter(chr1) && chr1 != 8)
            {
                e.Handled = true;
                lblContact.Text = "Numeric Value Only!";
            }

        }

        private void txt_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblEmail.Text = "";


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_FirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            bool found;
            string et= txt_Email.Text;
            string ct = txt_Contact.Text;
            if (sqlCon.State == ConnectionState.Closed)

                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd3 = new SqlCommand("SELECT COUNT(Student.Id) FROM Student  WHERE  RegistrationNo='" + txt_RegisterationNo.Text + "'", sqlCon);
                    int count = Convert.ToInt32(sqlCmd3.ExecuteScalar());

                    if (txt_FirstName.Text == "" && txt_LastName.Text == "" && txt_Contact.Text == ""
                        && txt_Email.Text == "" && cmb_gender.Text == "" && txt_RegisterationNo.Text == "")
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
                        lblRegistrationNo.Text = "RegistrationNumber Already Exist!";
                    }
                    else
                    {
                        if (txt_FirstName.Text == "")
                        {
                            lblFirstName.Text = "FirstName is required!";
                        }
                        if (txt_LastName.Text == "")
                        {
                            lblLastName.Text = "LastName is required!";
                        }
                        if (txt_Contact.Text == "")
                        {
                            lblContact.Text = "Contact is required!";
                        }
                       if (txt_Email.Text == "")
                        {
                            lblEmail.Text = "Email is required!";
                        }
                        if (cmb_gender.Text == "" && cmb_gender.Text != "Male" && cmb_gender.Text != "Female")
                        {
                            lblGender.Text = "Gender is required!";
                        }
                        if (txt_RegisterationNo.Text == "")
                        {
                            lblRegistrationNo.Text = "RegistrationNo. is required!";
                        }
                        if (et.EndsWith("@gmail.com") == false )
                        {
                            lblEmail.Text = "Valid Email is required!";
                        }
                        if (txt_Email.TextLength<11)
                        {
                            lblEmail.Text = "Valid Email is required!";
                        }
                        if (txt_Contact.TextLength != 16)
                        {
                            lblContact.Text = "Valid Contact is required!";
                        }
                        if (ct.StartsWith("+92-")==false)
                        {
                            lblContact.Text = "Valid Contact is required!";
                        }
                    }
                    if (txt_FirstName.Text != "" && txt_LastName.Text != ""  &&txt_Email.TextLength>11
                        && cmb_gender.Text != "" && txt_RegisterationNo.Text != "" && count == 0 
                       && et.EndsWith("@gmail.com") == true &&txt_Contact.TextLength==16&& ct.StartsWith("+92-") == true)
                    {
                        found = true;
                     

                    }
                    else
                    {
                        found = false;
                    }
                    if(found==true)
                    {
                        SqlCommand sqlCmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) VALUES('" + txt_FirstName.Text + "','" + txt_LastName.Text + "','" + txt_Contact.Text + "','" + txt_Email.Text + "' ,'" + Convert.ToDateTime(dateTimePicker1.Value) + "',(SELECT Id From Lookup WHERE  Value = '" + cmb_gender.Text + "')) ", sqlCon);

                        sqlCmd.ExecuteNonQuery();

                        SqlCommand sqlCmd1 = new SqlCommand("SELECT IDENT_CURRENT('Person') ", sqlCon);
                        int id = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                        SqlCommand sqlCmd2 = new SqlCommand("INSERT INTO Student(Id,RegistrationNo) Values('" + id + "','" + txt_RegisterationNo.Text + "')", sqlCon);
                        sqlCmd2.ExecuteNonQuery();
                        MessageBox.Show("Added Successfully");
                        Clear();
                    }
                    sqlCon.Close();
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FinalYearProject_Managment fyp = new FinalYearProject_Managment();
            this.Hide();
            fyp.Show();

        }

        private void cmb_gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_FirstName_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_LastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmb_gender_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            lblGender.Text = "Please Select From List";
            
        }

        private void txt_RegisterationNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblRegistrationNo.Text = "";
        }

        private void cmb_gender_SelectedValueChanged(object sender, EventArgs e)
        {
            
            lblGender.Text = "";
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();


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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
