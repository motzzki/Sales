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
        private bool update = false, delete = false;
        private int indexRow;

        public User()
        {
            InitializeComponent();
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            panel4.BackColor = Color.FromArgb(180, 0, 0, 0);
            showUser();
        }

        private void showUser()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select userId as ID, userName as USERNAME, userPass as PASSWORD From tblUser";
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
                int selectedUser = GetSelectedUserId();

                try
                {
                    if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }

                    if (update == false)
                    {
                        cmd.CommandText = "INSERT INTO tblUser(userName, userPass) VALUES ('" + txtUser.Text + "','" + txtPass.Text + "')";
                        MessageBox.Show("Insert Successfull!");
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE tblUser SET userName = '" + txtUser.Text + "', userPass = '" + txtPass.Text + "' WHERE userId = " + selectedUser + "";
                        MessageBox.Show("User Updated!");
                    }

                    cmd.ExecuteNonQuery();

                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                    dataUser.Enabled = false;

                    showUser();

                    update = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int GetSelectedUserId()
        {
            if (dataUser.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataUser.SelectedRows[0].Cells["ID"].Value);
            }
            else
            {
                return -1;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating User, Please Select Desired Row", "Message");
            update = true;
            dataUser.Enabled = true;
        }

        private void dataUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataUser.Rows[indexRow];

            txtUser.Text = row.Cells[1].Value.ToString();
            txtPass.Text = row.Cells[2].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }
    }
}