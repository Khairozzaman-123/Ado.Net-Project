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

namespace phonebook
{
    public partial class frmRegistration : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8R2IN4I;Initial Catalog=myPhonebookDB;Integrated Security=True");
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUserName.Focus();
        }

        private void checkbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConPassword.PasswordChar = '*';
            }
        }

        private void txtRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Username and Password fields are empty", "Registratrion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text == txtConPassword.Text)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Registration VALUES ('" + txtUserName.Text + "','" + txtPassword.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Your account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtConPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("Password does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Text = "";
                    txtConPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            catch
            {
                MessageBox.Show("This password has already been used", "Registration Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                
            }
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }
    }
}
