using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security;

namespace ByaherosKambalPizza
{
    public partial class createorder : Form
    {
        static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public createorder()
        {
            InitializeComponent();
        }

        private void createorder_Load(object sender, EventArgs e)
        {
            views();
            AddButtonColumnToDataGridView();



        }

        private void views()
        {
            MySqlConnection connm = new MySqlConnection(con);
            connm.Open();
            MySqlCommand cmdm = new MySqlCommand(" SELECT * FROM byaheroskambalpizza.addnewproduct", connm);
            MySqlDataReader readerm = cmdm.ExecuteReader();
            DataTable dtm = new DataTable();
            dtm.Load(readerm);
            dataGridView3.DataSource = dtm;
        }

        private void AddButtonColumnToDataGridView()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Add to List",
                UseColumnTextForButtonValue = true
            };

            dataGridView3.Columns.Add(buttonColumn);
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                txtProductCode.Text = row.Cells["productcode"].Value.ToString();
                txtname.Text = row.Cells["productname"].Value.ToString();
                txtPrice.Text = row.Cells["productprice"].Value.ToString();
                txtclass.Text = row.Cells["productclass"].Value.ToString();

                string productCode = Convert.ToString(row.Cells["productcode"].Value);
                string productName = Convert.ToString(row.Cells["productname"].Value);
                string productPrice = Convert.ToString(row.Cells["productprice"].Value);
                string productClass = Convert.ToString(row.Cells["productclass"].Value);

                CartFrm cartform = new CartFrm(this, productName, productCode);
                cartform.Show();
                cartform.ProductAdded += CartForm_ProductAdded;
            }
        }
        public static double total_amount = 0;
        private void CartForm_ProductAdded(string productCode)
        {
            DataGridViewRow selectedRow = null;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells["productcode"].Value.ToString() == productCode)
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow != null)
            {
                int rowIndex = dataGridView1.Rows.Add(); // This line is causing the error
                dataGridView1.Rows[rowIndex].Cells["colProductCode"].Value = selectedRow.Cells["productcode"].Value;
                dataGridView1.Rows[rowIndex].Cells["colProductName"].Value = selectedRow.Cells["productname"].Value;
                dataGridView1.Rows[rowIndex].Cells["colProductPrice"].Value = selectedRow.Cells["productprice"].Value;
                dataGridView1.Rows[rowIndex].Cells["colProductClass"].Value = selectedRow.Cells["productclass"].Value;
                dataGridView1.Rows[rowIndex].Cells["product_quantity"].Value = CartFrm.quantity;
                dataGridView1.Rows[rowIndex].Cells["subTotal"].Value = CartFrm.quantity * Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells["colProductPrice"].Value);
                total_amount += Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells["subTotal"].Value);
            }
            totalAmount.Text = total_amount.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            to_payment window = new to_payment();
            window.ShowDialog();
            record_order_history();
        }

        private void record_order_history()
        {
            int transactionID = set_transactionID();
            MySqlConnection conn1 = new MySqlConnection(con);
            MySqlCommand cmd;
            conn1.Open();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    cmd = conn1.CreateCommand();
                    cmd.CommandText = "Insert INTO orderhistory(customerName,productName,productPrice,productClass, productQuantity, subtotal, totalAmount, orderDate, transactionID )VALUES(@customerName, @productName, @productPrice, @productClass, @productQuantity, @subtotal, @totalAmount, @orderDate, @transactionID)";
                    cmd.Parameters.Add("@productName", MySqlDbType.String).Value = row.Cells["colProductName"].Value.ToString();
                    cmd.Parameters.Add("@customerName", MySqlDbType.String).Value = to_payment.name;
                    cmd.Parameters.Add("@productPrice", MySqlDbType.Double).Value = Convert.ToDouble(row.Cells["colProductPrice"].Value.ToString());
                    cmd.Parameters.Add("@productClass", MySqlDbType.String).Value = (row.Cells["colProductClass"].Value.ToString());
                    cmd.Parameters.Add("@productQuantity", MySqlDbType.Int32).Value = Convert.ToInt32(row.Cells["product_quantity"].Value.ToString());
                    cmd.Parameters.Add("@subtotal", MySqlDbType.Double).Value = Convert.ToInt32(row.Cells["product_quantity"].Value.ToString()) * Convert.ToDouble(row.Cells["colProductPrice"].Value.ToString());
                    cmd.Parameters.Add("@totalAmount", MySqlDbType.Double).Value = Convert.ToDouble(totalAmount.Text);
                    cmd.Parameters.Add("@orderDate", MySqlDbType.Datetime).Value = DateTime.Now;
                    cmd.Parameters.Add("@transactionID", MySqlDbType.Int32).Value = transactionID;
                    addCustomer();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("SAVED");

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            conn1.Close();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private int set_transactionID()
        {
            int id = 0;
            MySqlConnection con1 = new MySqlConnection(con);
            MySqlCommand cmd = new MySqlCommand();
            con1.Open();
            cmd.Connection = con1;
            try
            {
                cmd.CommandText = "SELECT MAX(transactionID) as maxID from orderhistory ";
                cmd.CommandTimeout = 3600;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    id = dr.GetInt32("maxID");

                }
                con1.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id+=1;
        }

        private void addCustomer()
        {
            MySqlConnection conn1 = new MySqlConnection(con);
            MySqlCommand cmd;
            conn1.Open();
            try
            {
                cmd = conn1.CreateCommand();
                cmd.CommandText = "INSERT IGNORE INTO customers (customerName) VALUES (@customerName)";
                cmd.Parameters.Add("@customerName", MySqlDbType.VarChar, 255).Value = to_payment.name;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaction was successful.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn1.Close();
        }
    }
}   