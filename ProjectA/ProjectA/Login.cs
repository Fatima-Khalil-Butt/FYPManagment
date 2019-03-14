using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txt_password.PasswordChar = '*';
           // txt_password.MaxLength = 10;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if(txt_Username.Text==""&& txt_password.Text == "")
            {
                lblUserName.Text = "Please Enter UserName!";
                lblPassword.Text = "Please Enter Password!";
                

            }
           
            else if(txt_Username.Text=="Admin"&&txt_password.Text=="123")
            {
                FinalYearProject_Managment fyp = new FinalYearProject_Managment();
                this.Hide();
                fyp.Show();
            }
            else
            {
                if (txt_Username.Text != "Admin")
                {
                    lblUserName.Text = "UserName is Incorrect!";

                   
                    txt_Username.Text = "";
                   
                }
                if (txt_password.Text != "123")
                {
                    lblPassword.Text = "Password is Incorrect!";
                    txt_password.Text = "";
                }
            }
        }

        private void txt_Username_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUserName.Text = "";
            char chr = e.KeyChar;
            if (char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                lblUserName.Text = "Letters Only";
               
            }
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblPassword.Text = "";
        }
    }
}
