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
    public partial class itemCategory : UserControl
    {
        private bool update = false, delete = false;
        private int indexRow;
        private int selectedUser;

        public itemCategory()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);

            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";

            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showCategory();
                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showCategory()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select categoryId as 'CATEGORY ID', categoryName as 'CATEGORY NAME' From tblItemCategory ORDER by categoryId DESC";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataCategory.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
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
                    if (string.IsNullOrEmpty(txtCatName.Text))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }

                    if (update == false)
                    {
                        cmd.CommandText = "INSERT INTO tblItemCategory(categoryName) VALUES ('" + txtCatName.Text + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Insert Successfull!");
                    }
                    else
                    {
                        selectedUser = GetSelectedUserId();
                        cmd.CommandText = "UPDATE tblItemCategory SET categoryName = '" + txtCatName.Text + "' WHERE categoryId = " + selectedUser + "";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category Updated!");
                        resetUpdate();
                    }

                    txtCatName.Clear();
                    txtCatName.Focus();
                    showCategory();
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating Category, Please Select Desired Row", "Message");
            update = true;
            btnDelete.Enabled = false;
            dataCategory.Enabled = true;
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
                dataCategory.Enabled = true;
                delete = true;
                update = false;
            }
        }

        public int GetSelectedUserId()
        {
            if (dataCategory.SelectedRows.Count >= 0)
            {
                return Convert.ToInt32(dataCategory.SelectedRows[0].Cells["CATEGORY ID"].Value);
            }
            else
            {
                return -1;
            }
        }

        private void resetUpdate()
        {
            dataCategory.Enabled = false;
            update = false;
            delete = false;
            btnDelete.Enabled = true;
        }

        private void dataCategory_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedUser = GetSelectedUserId();
            indexRow = e.RowIndex;
            DataGridViewRow row = dataCategory.Rows[indexRow];

            if (update)
            {
                txtCatName.Text = row.Cells[1].Value.ToString();
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

        private void DeleteUser(int catId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand del = connection.CreateCommand())
                    {
                        del.CommandText = "DELETE FROM tblItemCategory WHERE categoryId = " + catId + "";

                        del.ExecuteNonQuery();

                        MessageBox.Show("Category Deleted!");
                        showCategory();
                        delete = false;
                        dataCategory.Enabled = false;
                        btnUpdate.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}