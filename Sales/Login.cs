using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales
{
    public partial class Login : Form
    {
        public static String con, welcomeUser;

        public Login()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void Connect()
        {
            con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            welcomeUser = txtUser.Text;
            using (MySqlConnection connection = new MySqlConnection(con))
            {
                try
                {
                    connection.Open();
                    string statement = "select userName, userPass from tblUser where userName = @Username AND userPass = @Password";

                    using (MySqlCommand command = new MySqlCommand(statement, connection))
                    {
                        command.Parameters.AddWithValue("@Username", txtUser.Text);
                        command.Parameters.AddWithValue("@Password", txtPass.Text);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Login Successful!", "Success");
                                Dashboard dashboard = new Dashboard();
                                dashboard.Show();
                                Reset();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Credentials!", "Error");
                                Reset();
                                return;
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error in Connection: " + ex.Message, "Error");
                }
            }
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPass.Checked)
            {
                txtPass.PasswordChar = '\0';
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }

        private void Reset()
        {
            txtUser.Clear();
            txtPass.Clear();
            txtUser.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Connect();
            /*Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();*/
        }
    }
}