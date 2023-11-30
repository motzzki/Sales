using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales
{
    public partial class POS : Form
    {
        public POS()
        {
            InitializeComponent();
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            ShowAllItems();
            showtblCart();
            showNameAndId();
        }

        public void ShowAllItems()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                try
                {
                    // Replace "tblItems" with your actual table name
                    cmd.CommandText = "SELECT itemName, itemImg, base_price FROM tblItems";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing controls from the tableLayoutPanelItems
                        tableLayoutPanelItems.Controls.Clear();

                        // Set the number of columns based on your layout requirements
                        tableLayoutPanelItems.ColumnCount = 3;

                        while (reader.Read())
                        {
                            String itemName = reader.GetString("itemName");
                            Decimal baseprice = reader.GetDecimal("base_price");
                            byte[] imgData = (byte[])reader["itemImg"];

                            // Create a PictureBox for each item
                            PictureBox pictureBox = new PictureBox
                            {
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Size = new Size(100, 100), // Adjust the size as needed
                                Tag = itemName // Store the itemId in the Tag property for reference
                            };

                            // Load the image into the PictureBox
                            pictureBox.Image = ByteArrayToImage(imgData);

                            // Create a label to display the item ID
                            Label label = new Label
                            {
                                Text = itemName,
                                TextAlign = ContentAlignment.TopLeft,
                                Location = new Point(100, 0),
                                Font = new Font("Century Gothic", 12),
                                ForeColor = Color.FromArgb(228, 143, 69),
                                AutoSize = false,
                                Size = new Size(100,68)
                            };
                            

                            Label bp = new Label
                            {
                                Text = "₱" + baseprice.ToString(),
                                TextAlign = ContentAlignment.BottomLeft,
                                Location = new Point(100, 75),
                                Font = new Font("Century Gothic", 10),
                                ForeColor = Color.FromArgb(228, 143, 69)

                            };

                            // Create a container (e.g., Panel) to hold both the PictureBox and the Label
                            Panel container = new Panel();
                            container.Controls.Add(pictureBox);
                            container.Controls.Add(label);
                            container.Controls.Add(bp);

                           label.BringToFront();
                            bp.BringToFront();
                            // Add the container to the tableLayoutPanelItems
                            tableLayoutPanelItems.Controls.Add(container);

                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private static Image ByteArrayToImage(byte[] imgData)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imgData))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting byte array to image: " + ex.Message);
                return null;
            }
        }


        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void btnAdd2Cart_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {

           
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT quantity FROM tblInventory WHERE item_id = '" + cbItemId.Text + "'";
                    int checkQuantity = Convert.ToInt32(cmd.ExecuteScalar());

              
                    if(checkQuantity < Convert.ToInt32(numQuantity.Text))
                    {
                        MessageBox.Show("Not Enough Stock!");
                        return;
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO tblCart(item_name,quantity, basePrice, total_price) VALUES ('" + txtPName.Text + "', " + numQuantity.Text + ", (select base_price from tblItems where itemName = '" + txtPName.Text + "'), (" + numQuantity.Text + " * basePrice))";
                        cmd.ExecuteNonQuery();
                    }
                    
                    cmd.CommandText = "SELECT SUM(total_price) AS total_sum FROM tblCart";
                    object result = cmd.ExecuteScalar();
                    MessageBox.Show("Success Query!");
                    showtblCart();
                    lblTotal.Text = "₱" + Convert.ToDecimal(result).ToString();
                }
                catch(Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }

        public void showtblCart()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                try
                {
                    // Replace "tblItems" with your actual table name
                    cmd.CommandText = "SELECT item_name,quantity, basePrice, total_price FROM tblCart";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing controls from the tableLayoutPanelItems
                        tableCart.Controls.Clear();

                        // Set the number of columns based on your layout requirements
                        tableCart.ColumnCount = 1;

                        while (reader.Read())
                        {
                            String itemName = reader.GetString("item_name");
                            Decimal baseprice = reader.GetDecimal("basePrice");
                            int quantity = reader.GetInt32("quantity");
                            Decimal totalprice = reader.GetDecimal("total_price");

                            // Create a label to display the item ID
                            Label label = new Label
                            {
                                Text = itemName,
                                TextAlign = ContentAlignment.TopLeft,
                                Location = new Point(10, 0),
                                Font = new Font("Century Gothic", 12),
                                ForeColor = Color.FromArgb(228, 143, 69),
                                AutoSize = false,
                                Size = new Size(100, 68)
                            };


                            Label bp = new Label
                            {
                                Text = "₱" + baseprice.ToString(),
                                TextAlign = ContentAlignment.BottomLeft,
                                Location = new Point(10, 75),
                                Font = new Font("Century Gothic", 10),
                                ForeColor = Color.FromArgb(228, 143, 69)

                            };
                            Label qtt = new Label
                            {
                                Text = "Quantity: " + quantity.ToString(),
                                TextAlign = ContentAlignment.BottomLeft,
                                Location = new Point(10, 50),
                                Font = new Font("Century Gothic", 10),
                                ForeColor = Color.FromArgb(228, 143, 69)
                            };
                            Label tp = new Label
                            {
                                Text = "₱" + totalprice.ToString(),
                                TextAlign = ContentAlignment.BottomLeft,
                                Location = new Point(120, 75),
                                Font = new Font("Century Gothic", 10),
                                ForeColor = Color.FromArgb(228, 143, 69)

                            };

                            // Create a container (e.g., Panel) to hold both the PictureBox and the Label
                            Panel container = new Panel();
                            //container.Controls.Add(pictureBox);
                            container.Controls.Add(label);
                            container.Controls.Add(bp);
                            container.Controls.Add(qtt);
                            container.Controls.Add(tp);

                            label.BringToFront();
                            bp.BringToFront();
                            qtt.BringToFront();
                            tp.BringToFront();
                            // Add the container to the tableLayoutPanelItems
                            tableCart.Controls.Add(container);


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM tblCart;";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Placed!");
                    lblTotal.Text = "";
                    showtblCart();
                    cmd.CommandText = "UPDATE tblInventory SET quantity = quantity - " + numQuantity.Text + " WHERE item_id = '" + cbItemId.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
        }

        void showNameAndId()
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

                            // Insert an empty row at the beginning
                            DataRow dr = table.NewRow();
                            table.Rows.InsertAt(dr, 0);

                            // Set the DataSource and configure display and value members
                            cbItemId.DataSource = table;
                            cbItemId.DisplayMember = "itemId";
                            cbItemId.ValueMember = "itemName";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cbItemId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedRow = (DataRowView)cbItemId.SelectedItem;
            if (selectedRow != null)
            {
                txtPName.Text = selectedRow["itemName"].ToString();
            }
        }
    }
}
