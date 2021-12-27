using System;
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
    public partial class RemoveUser : Form
    {
        public RemoveUser()
        {
            InitializeComponent();
        }

        public void DarkTheme()
        {
            this.BackColor = Color.Black;
            loginLabel.ForeColor = Color.White;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginBox.Text))
            {
                MessageBox.Show("Enter login!");
                return;
            }
            if (DataBaseFunc.IsUserExits(loginBox.Text))
            {
                if(DataBaseFunc.RemoveUser(loginBox.Text))
                {
                    MessageBox.Show("User removed!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed!");
                }
            }
            else
            {
                MessageBox.Show("User not found!");
            }
        }
    }
}
