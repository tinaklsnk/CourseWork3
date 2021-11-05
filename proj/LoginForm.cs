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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            String loginUser = LoginBox.Text;
            String passwordUser = PasswordBox.Text;

            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection= new SqlConnection(ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE login = @usLog AND password = @usPass", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@usPass", MySqlDbType.VarChar).Value = passwordUser;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            if(dataTable.Rows.Count>0)
            {
                MessageBox.Show("Authorized!");
            }
            else
                MessageBox.Show("User not found!");
        }
    }
}
