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
    public partial class addproduct : Form
    {
        static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public addproduct()
        {
            InitializeComponent();
            productClass.Items.Add("Pizza Dough");
            productClass.Items.Add("Sauce");
            productClass.Items.Add("Box");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conzx = new MySqlConnection(con);
            MySqlCommand cmd = new MySqlCommand("update addnewproduct set productname=@name,productprice=@price,productclass=@class WHERE productcode=@id", conzx);
            conzx.Open();
  
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@price", textBox3.Text);
            cmd.Parameters.AddWithValue("@class", productClass.Text);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Record Updated Successfully");
            views();
            conzx.Close();

        }

        private void views()
        {
            MySqlConnection connm = new MySqlConnection(con);
            connm.Open();
            MySqlCommand cmdm = new MySqlCommand(" SELECT productcode as 'ProductCode',productname as 'ProductName',productprice as 'Price', productclass as 'Class' FROM addnewproduct ", connm);
            MySqlDataReader readerm = cmdm.ExecuteReader();
            DataTable dtm = new DataTable();
            dtm.Load(readerm);
            guna2DataGridView1.DataSource = dtm;

            /*string one = "Price";*/

        }


        private void addproduct_Load(object sender, EventArgs e)
        {
            views();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MySqlConnection conn1 = new MySqlConnection(con);
            MySqlCommand cmd;
            conn1.Open();
            try
            {
                cmd = conn1.CreateCommand();
                cmd.CommandText = "Insert INTO addnewproduct(productcode,productname,productprice,productclass)VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + productClass.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("SAVED");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                productClass.Text = "";



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn1.Close();
            views();
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value !=null)
            {
                try
                {
                    textBox1.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    textBox2.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    textBox3.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    productClass.Text = (guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
