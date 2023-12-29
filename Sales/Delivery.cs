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
    public partial class Delivery : UserControl
    {
        public Delivery()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);
            DateTime currentDateTime = DateTime.Now;
            lblDate.Text = currentDateTime.ToString();
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showDelivery();
                    showItemId();
                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showDelivery()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select deliveryId as 'DELIVERY ID', deliveryDate as DATE, itemName as 'NAME', quantity as QUANTITY From tblDelivery inner join tblItems on tblItems.itemId = tblDelivery.item_id order by deliveryId DESC";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataDelivery.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showItemId()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM tblItems;";

                        using (MySqlDataAdapter adap = new MySqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adap.Fill(table);

                            DataRow dr = table.NewRow();
                            table.Rows.InsertAt(dr, 0);

                            cbItemName.DataSource = table;
                            cbItemName.DisplayMember = "itemName";
                            cbItemName.ValueMember = "itemId";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                    //string formattedDate = dateitemDate.Value.ToString("yyyy-MM-dd");

                    cmd.CommandText = "SELECT COUNT(*) FROM tblInventory WHERE item_id = " + cbItemName.SelectedValue.ToString() + "";
                    int existingRecordCount = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = "INSERT INTO tblDelivery(deliveryDate, item_id, quantity) VALUES (CURDATE(), " + cbItemName.SelectedValue.ToString() + ", '" + numQuantity.Text + "')";
                    cmd.ExecuteNonQuery();

                    if (existingRecordCount > 0)
                    {
                        cmd.CommandText = "UPDATE tblInventory SET quantity = quantity + " + numQuantity.Text + " WHERE item_id = " + cbItemName.SelectedValue.ToString() + "";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO tblInventory (item_id, quantity) VALUES ( " + cbItemName.SelectedValue.ToString() + ", '" + numQuantity.Text + "')";
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success Delivery!");
                    showDelivery();
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }
    }
}