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
        OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Z:\QQ129\Putol, Christian Jay\PASSWORD.mdb");
        OleDbCommand command = new OleDbCommand();
        DataView dv = new DataView();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        DataTable table = new DataTable();
        
        public frmUser()
        {
            InitializeComponent();
            ViewData();
        }

        public void ViewData()
        {
            connection.Open();
            table = new DataTable();
            adapter = new OleDbDataAdapter("Select * from [Password]", connection);
            adapter.Fill(table);
            Grid1.DataSource = table;
            connection.Close();


        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow select = Grid1.Rows[e.RowIndex];
            string number = select.Cells["Username"].Value.ToString();
            string tile = select.Cells["Password"].Value.ToString();
        }
    }
}
