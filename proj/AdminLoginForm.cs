﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            if (loginBox.Text == "admin" && passwordBox.Text == "0000")
            {
                Form1 form1 = new Form1();
                form1.Show();
                form1.AllowEditTable();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login or Password not Correct!");
            }
        }
    }
}
