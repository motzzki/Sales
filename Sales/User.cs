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
    public partial class User : UserControl
    {
        Boolean update, delete;
        public User()
        {
            InitializeComponent();
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            panel4.BackColor = Color.FromArgb(180,0,0,0);
            showUser();
            update = true;
            delete = true;
        }

        void showUser()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select userName as USERNAME, userPass as PASSWORD From tblUser";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataUser.DataSource = ds.Tables[0].DefaultView;


                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!" + e.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;

                try
                {
                    if ((txtUser.Text.Equals("") || txtPass.Text.Equals("")))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }
                    else
                    {
                        if (!update)
                        {
                            cmd.CommandText = "UPDATE tblUser SET userName = '"+txtUser.Text+"', userPass = '"+txtPass.Text+"' WHERE userName = '"+txtUname.Text +"'";
                            MessageBox.Show("User Updated!");
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO tblUser(userName, userPass) VALUES ('" + txtUser.Text + "','" + txtPass.Text + "')";
                            MessageBox.Show("Success Query!");
                        } 
                        cmd.ExecuteNonQuery();
                        txtUser.Clear();
                        txtPass.Clear();
                        txtUser.Focus();
                        showUser();
                        update = true;
                        delete = true;
                    }

                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            update = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete = false;
            panel5.Visible=true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            update = true;
            delete = true;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            if (!delete)
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.Connection = connection;
                    try
                    {
                        
                        cmd.CommandText = "DELETE FROM tblUser WHERE userName = '" + txtUname.Text + "'";
                        MessageBox.Show("User Deleted!");
                        cmd.ExecuteNonQuery();
                        delete = true;
                        showUser();
                    }
                    catch (Exception z)
                    {
                        MessageBox.Show(z.Message);
                    }
                }
                    
            }
        }
    }
}
