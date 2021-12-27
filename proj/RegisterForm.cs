using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginBox.Text))
            {
                MessageBox.Show("Create login!");
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                MessageBox.Show("Create password!");
                return;
            }

            if (DataBaseFunc.IsUserExits(loginBox.Text))
            {
                MessageBox.Show("Login is busy!");
                return;
            }

            if (DataBaseFunc.AddUser(loginBox.Text, passwordBox.Text))
            {
                MessageBox.Show("Signed Up!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        public void DarkTheme()
        {
            this.BackColor = Color.Black;
            loginLabel.ForeColor = Color.White;
            passwordLabel.ForeColor = Color.White;
        }
    }
}
