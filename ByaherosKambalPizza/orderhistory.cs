using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByaherosKambalPizza
{
    public partial class orderhistory : Form
    {
        static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public orderhistory()
        {
            InitializeComponent();
            update_history_table(name);
            update_customer();
        }

        private void orderhistory_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void update_history_table(string name)
        {
            if (name == "" || name == null)
            {
                name = "*";
            }
            MySqlConnection connm = new MySqlConnection(con);
            connm.Open();
            MySqlCommand cmdm = new MySqlCommand(" SELECT * FROM orderhistory where customerName = '"+name+"'", connm);
            MySqlDataReader readerm = cmdm.ExecuteReader();
            DataTable dtm = new DataTable();
            dtm.Load(readerm);
            orderhistorytable.DataSource = dtm;
        }

        private void update_customer()
        {
            MySqlConnection connm = new MySqlConnection(con);
            connm.Open();
            MySqlCommand cmdm = new MySqlCommand(" SELECT * FROM customers", connm);
            MySqlDataReader readerm = cmdm.ExecuteReader();
            DataTable dtm = new DataTable();
            dtm.Load(readerm);
            customerTable.DataSource = dtm;
        }

        private void customerTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }
        string name = "";
        private void customerTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                try
                {

                    name = customerTable.Rows[e.RowIndex].Cells[0].Value.ToString();
                    update_history_table(name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void customerTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
