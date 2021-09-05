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
    public partial class frmLogin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8R2IN4I;Initial Catalog=myPhonebookDB;Integrated Security=True");
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void checkbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '*';

            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Registration WHERE userName='" + txtUsername.Text + "' and password='" + txtPassword.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    frmInput input = new frmInput();
                    input.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
                
            }
            catch 
            {

                MessageBox.Show("User Name or Password can't be empty", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            frmRegistration registration = new frmRegistration();
            registration.Show();
            this.Hide();
        }
    }
}
