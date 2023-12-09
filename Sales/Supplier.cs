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
    public partial class Supplier : UserControl
    {
        private bool update = false, delete = false;
        private int selectedUser;
        private int indexRow;

        public Supplier()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);

            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showSupplier();
                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showSupplier()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select supplierId as 'SUPPLIER ID', supplierName as 'SUPPLIER NAME', address as ADDRESS, contactnum as CONTANCT From tblSupplier";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataSupplier.DataSource = ds.Tables[0].DefaultView;
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
                    if (string.IsNullOrEmpty(txtSupplierName.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtContact.Text))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }
                    if (update == false)
                    {
                        cmd.CommandText = "INSERT INTO tblSupplier(supplierName, address, contactNum) VALUES ('" + txtSupplierName.Text + "', '" + txtAddress.Text + "', " + txtContact.Text + ")";
                        MessageBox.Show("Success Query!");
                    }
                    else
                    {
                        selectedUser = GetSelectedUserId();
                        cmd.CommandText = "UPDATE tblSupplier SET supplierName = '" + txtSupplierName.Text + "', address = '" + txtAddress.Text + "', contactNum =" + txtContact.Text + " WHERE userId = " + selectedUser + "";
                        MessageBox.Show("User Updated!");
                        dataSupplier.Enabled = false;
                        btnDelete.Enabled = true;
                    }
                    cmd.ExecuteNonQuery();
                    txtSupplierName.Clear();
                    txtAddress.Clear();
                    txtContact.Clear();
                    txtSupplierName.Focus();
                    showSupplier();
                    update = true;
                    delete = true;
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating User, Please Select Desired Row", "Message");
            update = true;
            dataSupplier.Enabled = true;
            btnDelete.Enabled = false;
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
                dataSupplier.Enabled = true;
                delete = true;
                update = false;
            }
        }

        public int GetSelectedUserId()
        {
            if (dataSupplier.SelectedRows.Count >= 0)
            {
                return Convert.ToInt32(dataSupplier.SelectedRows[0].Cells["SUPPLIER ID"].Value);
            }
            else
            {
                return -1;
            }
        }

        private void dataSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedUser = GetSelectedUserId();
            DataGridViewRow row = dataSupplier.Rows[indexRow];
            indexRow = e.RowIndex;
            if (update)
            {
                txtSupplierName.Text = row.Cells[1].Value.ToString();
                txtAddress.Text = row.Cells[2].Value.ToString();
                txtContact.Text = row.Cells[3].Value.ToString();
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

        private void DeleteUser(int supplierId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand del = connection.CreateCommand())
                    {
                        del.CommandText = "DELETE FROM tblItemCategory WHERE userId = " + supplierId + "";

                        del.ExecuteNonQuery();

                        MessageBox.Show("Supplier Deleted!");
                        showSupplier();
                        delete = false;
                        dataSupplier.Enabled = false;
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