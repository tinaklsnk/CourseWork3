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
        private SqlConnection sqlConnection = null;
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            if(loginBox.Text=="")
            {
                MessageBox.Show("Create login!");
                return;
            }
            if (passwordBox.Text == "")
            {
                MessageBox.Show("Create password!");
                return;
            }
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString);
            if (IsUserExits())
                return;
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[User] (login, password) VALUES (@usLog, @usPass)", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = loginBox.Text;
            command.Parameters.Add("@usPass", MySqlDbType.VarChar).Value = passwordBox.Text;
            sqlConnection.Open();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Signed up!");
            else
                MessageBox.Show("Fail!");
            sqlConnection.Close();
        }

        public Boolean IsUserExits()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE login = @usLog", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = loginBox.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Login is busy!");
                return true;
            }
            else
                return false;
        }
    }
}
