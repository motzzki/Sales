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
        Boolean update, delete;
        public itemCategory()
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
                    showCategory();

                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }
        void showCategory()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select categoryId as 'CATEGORY ID', categoryName as 'CATEGORY NAME' From tblItemCategory";
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
                    else
                    {

                        if (!update)
                        {
                            cmd.CommandText = "UPDATE tblItemCategory SET categoryName = '" + txtCatName.Text + "' WHERE categoryName = '" + txtNameCat.Text + "'";
                            MessageBox.Show("Category Updated!");
                            txtNameCat.Clear();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO tblItemCategory(categoryName) VALUES ('" + txtCatName.Text + "')";
                            MessageBox.Show("Success Query!");
                        }
                        cmd.ExecuteNonQuery();
                        txtCatName.Clear();
                        txtNameCat.Clear();
                        txtCatName.Focus();
                        showCategory();
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
                        
                        cmd.CommandText = "DELETE FROM tblItemCategory WHERE categoryName = '" + txtNameCat.Text + "'";
                        MessageBox.Show("User Deleted!");
                        cmd.ExecuteNonQuery();
                        delete = true;
                        showCategory();
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
