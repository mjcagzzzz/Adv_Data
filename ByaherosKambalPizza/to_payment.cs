using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByaherosKambalPizza
{
    public partial class to_payment : Form
    {
        static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static double payment_amount = 0;
        public static double change_amount = 0;
        public static string name = "";
        public to_payment()
        {
            InitializeComponent();
            total.Text = createorder.total_amount.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            payment_amount = Convert.ToDouble(payment.Text);
            change_amount = Convert.ToDouble(change.Text);
            name = buyer.Text;  
            clear();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void payment_TextChanged(object sender, EventArgs e)
        {
            if (payment.Text.Length > 0)
            {
                change.Text = (Convert.ToDouble(payment.Text) - Convert.ToDouble(total.Text)).ToString();
            }
        }
        private void clear()
        {
            payment.Clear();
            buyer.Clear();
        }

        
    }
}
