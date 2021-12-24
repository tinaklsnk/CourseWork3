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
    public partial class LoginForm : Form
    {
        private SqlConnection sqlConnection = null;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connected!");
            }
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE login = @usLog AND password = @usPass", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@usPass", MySqlDbType.VarChar).Value = passwordBox.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            if(dataTable.Rows.Count>0)
            {
                //MessageBox.Show("Authorized!");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Login or Password not Correct!");
        }

        private void labelSignUp_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void labelSignUp_MouseEnter(object sender, EventArgs e)
        {
            labelSignUp.ForeColor = Color.White;
        }

        private void labelSignUp_MouseLeave(object sender, EventArgs e)
        {
            labelSignUp.ForeColor = Color.Black;
        }

        private void adminLabel_Click(object sender, EventArgs e)
        {
            AdminLoginForm admin = new AdminLoginForm();
            admin.Show();
            this.Hide();
        }

        private void adminLabel_MouseEnter(object sender, EventArgs e)
        {
            adminLabel.ForeColor = Color.White;
        }

        private void adminLabel_MouseLeave(object sender, EventArgs e)
        {
            adminLabel.ForeColor = Color.Black;
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE login = @usLog AND password = @usPass", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@usPass", MySqlDbType.VarChar).Value = passwordBox.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login or Password not Correct!");
            }
        }
    }
}
