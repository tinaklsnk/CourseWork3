using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;

namespace proj
{
   static class DataBaseClass
    {
        private static SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString);
        public static bool IsUserExits(string login)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE login = @usLog", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = login;
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool AddUser(string login, string password)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[User] (login, password) VALUES (@usLog, @usPass)", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@usPass", MySqlDbType.VarChar).Value = password;
            sqlConnection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                sqlConnection.Close();
                return true;
            }
            else
            {
                sqlConnection.Close();
                return false;
            }
        }
        public static bool RemoveUser(string login)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[User] WHERE login = @usLog", sqlConnection);
            command.Parameters.Add("@usLog", MySqlDbType.VarChar).Value = login;
            sqlConnection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                sqlConnection.Close();
                return true;
            }
            else
            {
                sqlConnection.Close();
                return false;
            }
        }
    }
}
