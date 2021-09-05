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
    public partial class frmData : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\myPhonebookDB.mdf;Integrated Security=True");
        public frmData()
        {
            InitializeComponent();
        }

        private void data_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbSearchBy.SelectedIndex = 0;
           
        }

        private void LoadData()
        {
            string comdtext = "SET CONCAT_NULL_YIELDS_NULL OFF SELECT M.firstName + ' ' + M.middleName + ' ' + M.lastName AS[NAME],M.mobileNo AS CONTACT,M.email EMAIL, M.dateOfBirth DOB, G.gender GENDER, M.groups AS[GROUP]  FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID SET CONCAT_NULL_YIELDS_NULL ON";
            SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEntryForm_Click(object sender, EventArgs e)
        {
            frmInput fin = new frmInput();
            fin.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchContacts();
        }

        private void SearchContacts()
        {
            string comdtext = "SET CONCAT_NULL_YIELDS_NULL OFF SELECT M.firstName + ' ' + M.middleName + ' ' + M.lastName AS[NAME],M.mobileNo AS CONTACT,M.email EMAIL, M.dateOfBirth DOB, G.gender GENDER, M.groups AS[GROUP]  FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID where M." + cmbSearchBy.SelectedItem + " like '" + txtSearch.Text + "%' SET CONCAT_NULL_YIELDS_NULL ON";
            SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchContacts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblMain WHERE mobileNo=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", txtSearch.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
