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
        private Boolean update, delete;

        public Supplier()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);
            update = true;
            delete = true;
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
                    if (txtSupplierName.Text.Equals("") || txtAddress.Text.Equals("") || txtContact.Text.Equals(""))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }
                    else
                    {
                        if (!update)
                        {
                            cmd.CommandText = "UPDATE tblSupplier SET supplierName = '" + txtSupplierName.Text + "', address = '" + txtAddress.Text + "', contactNum = " + txtContact.Text + " WHERE supplierId = " + txtId.Text + "";
                            MessageBox.Show("Supplier Updated!");
                            txtId.Clear();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO tblSupplier(supplierName, address, contactNum) VALUES ('" + txtSupplierName.Text + "', '" + txtAddress.Text + "', " + txtContact.Text + ")";
                            MessageBox.Show("Success Query!");
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                delete = false;
                panel5.Visible = true;
            }
            else
            {
                return;
            }
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
                        cmd.CommandText = "DELETE FROM tblSupplier WHERE supplierId = " + txtId.Text + "";
                        MessageBox.Show("Supplier Deleted!");
                        cmd.ExecuteNonQuery();
                        delete = true;
                        showSupplier();
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