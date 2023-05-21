using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserPass
{
    public partial class frmAddUser : Form
    {
        private OleDbConnection con;
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxUser.Text))
            {
                MessageBox.Show("Username field must not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(txtBoxPass.Text))
            {
                MessageBox.Show("Password field must not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Desktop\dev\2NDYEAR\2nd SEM\appsdev\USERLIBSYS-main\UserPass\PASSWORD.mdb; Persist Security Info = False; ";
                con = new OleDbConnection(connection);
                con.Open();

                try
                {
                    OleDbCommand command = new OleDbCommand("INSERT INTO [Password] ([Username], [Password]) VALUES (@username, @password)", con);
                    command.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                    command.Parameters.AddWithValue("@Password", txtBoxPass.Text);
                    command.ExecuteNonQuery();
                    DialogResult result = MessageBox.Show("User is added!", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("User is not added", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }
    }
}
