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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Desktop\dev\2NDYEAR\2nd SEM\appsdev\USERLIBSYS-main\UserPass\PASSWORD.mdb";
            OleDbConnection connect = new OleDbConnection(connection);
            connect.Open();

            string query = "SELECT * FROM [Password]";
            OleDbCommand access = new OleDbCommand(query, connect);

            OleDbDataReader reader = access.ExecuteReader();

            bool isAuthenticated = false;
            while (reader.Read())
            {
                string Username = reader.GetString(0);
                string Password = reader.GetString(1);
                if (Username == txtBoxUser.Text && Password == txtBoxPass.Text)
                {
                    isAuthenticated = true;
                    break;
                }
            }
                connect.Close();
                if (isAuthenticated)
                {
                    frmMenu frmM = new frmMenu();
                    frmM.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login Failed!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void txtBoxUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
