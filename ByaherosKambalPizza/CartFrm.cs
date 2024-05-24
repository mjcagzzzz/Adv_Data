using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByaherosKambalPizza
{
    public partial class CartFrm : Form
    {
        public static int quantity = 0;
        createorder createOrder;
        public delegate void ProductAddedEventHandler(string productCode);
        public event ProductAddedEventHandler ProductAdded;
        string Code;
        public CartFrm(createorder createOrder, string productName, string productCode)
        {
            InitializeComponent();
            this.createOrder = createOrder;
            Code = productCode;
            label1.Text = productName;
        } 

        private void CartFrm_Load(object sender, EventArgs e)
        {
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            quantity = Convert.ToInt32(textBox1.Text);
            ProductAdded?.Invoke(Code);

            DataGridViewRow selectedrow = null;
            foreach(DataGridViewRow row in createOrder.dataGridView3.Rows)
            {
               if (row.Cells["productcode"].Value.ToString() == Code)
                {
                    selectedrow = row;
                    break;
                }
            }

            this.Hide();
        }

        private void CartForm_ProductAdded(string productCode)
        {
/*            DataGridView selectedRow = null;
            foreach (DataGridView row in createOrder.dataGridView3.Rows)
            {
                if (row.Cells["productcode"].Value.ToString() == Code)
                {
                    selectedRow = row;
                    break;
                }
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                quantity = Convert.ToInt32(textBox1.Text);
                ProductAdded?.Invoke(Code);

                DataGridViewRow selectedrow = null;
                foreach (DataGridViewRow row in createOrder.dataGridView3.Rows)
                {
                    if (row.Cells["productcode"].Value.ToString() == Code)
                    {
                        selectedrow = row;
                        break;
                    }
                }

                this.Hide();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
