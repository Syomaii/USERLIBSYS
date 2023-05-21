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
    public partial class frmUser : Form
    {
        OleDbConnection connection;
        OleDbCommand command = new OleDbCommand();
        DataView dv = new DataView();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        DataTable table = new DataTable();

        public frmUser()
        {
            InitializeComponent();
            connection = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\User\\Desktop\\dev\\2NDYEAR\\2nd SEM\\appsdev\\USERLIBSYS-main\\UserPass\\PASSWORD.mdb");
            ViewData();
        }

        public void ViewData()
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from [Password]", connection);
                cmd.ExecuteNonQuery();
                adapter = new OleDbDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                Grid1.DataSource = table;
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow select = Grid1.Rows[e.RowIndex];
            txtBoxUser.Text = select.Cells["Username"].Value.ToString();
            txtBoxPass.Text = select.Cells["Password"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
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
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("INSERT INTO [Password] ([User], [Pass]) VALUES (@username, @password)", connection);
                    command.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                    command.Parameters.AddWithValue("@Password", txtBoxPass.Text);
                    command.ExecuteNonQuery();
                    DialogResult result = MessageBox.Show("User is added!", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("User is not added", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { connection.Close(); }
            }
            ViewData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                try
                {
                    // Check if the user exists in the database
                    OleDbCommand checkCommand = new OleDbCommand("SELECT COUNT(*) FROM [Password] WHERE [User] = @Username AND [Pass] = @Password", connection);
                    checkCommand.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                    checkCommand.Parameters.AddWithValue("@Password", txtBoxPass.Text);
                    connection.Open();
                    int userCount = (int)checkCommand.ExecuteScalar();
                    if (userCount == 0)
                    {
                        MessageBox.Show("User does not exist in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("Are you sure you want to remove this?", "Confirm Remove",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        OleDbCommand com = new OleDbCommand("DELETE FROM [PASSWORD] WHERE [User] = @Username AND [Pass] = @Password", connection);
                        com.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                        com.Parameters.AddWithValue("@Password", txtBoxPass.Text);
                        com.ExecuteNonQuery();

                        MessageBox.Show("Data removed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBoxUser.Clear();
                        txtBoxPass.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                { 
                    connection.Close(); 
                }
            }
            ViewData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
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
                try
                {
                    connection.Open();
                    OleDbCommand checkCommand = new OleDbCommand("SELECT COUNT(*) FROM [Password] WHERE [User] = @Username", connection);
                    checkCommand.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                    
                    int userCount = (int)checkCommand.ExecuteScalar();
                    if (userCount == 0)
                    {
                        MessageBox.Show("User does not exist in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("Are you sure you want to Edit this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        
                        OleDbCommand com = new OleDbCommand("Update [Password] SET Pass= '"+txtBoxPass.Text+"' where User = @Username",connection);
                        com.Parameters.AddWithValue("@Username", txtBoxUser.Text);
                        int rowsAffected = com.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Succesfully Edited!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Account is not Edited", "Edit Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while updating the password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            ViewData();
        }
    }
}
