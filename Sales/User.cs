using MySqlConnector;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sales
{
    public partial class User : UserControl
    {
        private bool update = false, delete = false;
        private int indexRow;
        public int selectedUser;

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
                    cmd.CommandText = "Select userId as ID, userName as USERNAME, userPass as PASSWORD From tblUser order by userId desc";
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
                        selectedUser = GetSelectedUserId();

                        cmd.CommandText = "UPDATE tblUser SET userName = '" + txtUser.Text + "', userPass = '" + txtPass.Text + "' WHERE userId = " + selectedUser + "";
                        MessageBox.Show("User Updated!");
                        resetUpdate();
                    }

                    cmd.ExecuteNonQuery();

                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();

                    showUser();

                    update = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int GetSelectedUserId()
        {
            if (dataUser.SelectedRows.Count >= 0)
            {
                return Convert.ToInt32(dataUser.SelectedRows[0].Cells["ID"].Value);
            }
            else
            {
                return -1;
            }
        }

        private void resetUpdate()
        {
            dataUser.Enabled = false;
            update = false;
            delete = false;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating User, Please Select Desired Row", "Message");
            update = true;
            dataUser.Enabled = true;
            btnDelete.Enabled = false;
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand del = connection.CreateCommand())
                    {
                        del.CommandText = "DELETE FROM tblUser WHERE userId = " + userId + "";

                        del.ExecuteNonQuery();

                        MessageBox.Show("User Deleted!");
                        showUser();
                        delete = false;
                        dataUser.Enabled = false;
                        btnUpdate.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataUser_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            selectedUser = GetSelectedUserId();
            indexRow = e.RowIndex;
            DataGridViewRow row = dataUser.Rows[indexRow];
            if (update)
            {
                txtUser.Text = row.Cells[1].Value.ToString();
                txtPass.Text = row.Cells[2].Value.ToString();
            }

            if (delete)
            {
                DialogResult result = MessageBox.Show("Confirm Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteUser(selectedUser);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to delete? Please Select Desired Row", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                dataUser.Enabled = true;
                delete = true;
                update = false;
            }
        }
    }
}