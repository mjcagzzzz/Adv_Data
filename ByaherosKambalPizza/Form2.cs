﻿using System;
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
    public partial class Form2 : Form
    {
        static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Pan_Content.Controls.Clear();
            createorder ctd = new createorder { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Pan_Content.Controls.Add(ctd);
            ctd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Pan_Content.Controls.Clear();
            orderhistory ctd = new orderhistory { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Pan_Content.Controls.Add(ctd);
            ctd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Pan_Content.Controls.Clear();
            addproduct ctd = new addproduct { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Pan_Content.Controls.Add(ctd);
            ctd.Show();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void logo_img_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
