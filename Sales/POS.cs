using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Sales
{
    public partial class POS : Form
    {
        private Dashboard dashboard = new Dashboard();
        private List<int> qtt = new List<int>();
        private List<int> itemid = new List<int>();
        private List<int> total = new List<int>();
        private int rip = 0;

        //private int cartId;
        private DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

        public POS()
        {
            InitializeComponent();
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            ShowAllItems();
            showCart();
            //showtblCart();
            showItmName();
            showuser();

            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.HeaderText = "Action";

            buttonColumn.Text = "-";

            dataCart.Columns.Add(buttonColumn);
        }

        public void ShowAllItems()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = "SELECT itemName, itemImg, base_price FROM tblItems";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        tableLayoutPanelItems.Controls.Clear();

                        tableLayoutPanelItems.ColumnCount = 3;

                        while (reader.Read())
                        {
                            String itemName = reader.GetString("itemName");
                            Decimal baseprice = reader.GetDecimal("base_price");
                            byte[] imgData = (byte[])reader["itemImg"];

                            PictureBox pictureBox = new PictureBox
                            {
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Size = new Size(100, 100),
                                Tag = itemName
                            };

                            pictureBox.Image = ByteArrayToImage(imgData);

                            Label label = new Label
                            {
                                Text = itemName,
                                TextAlign = ContentAlignment.TopLeft,
                                Location = new Point(100, 0),
                                Font = new Font("Century Gothic", 12),
                                ForeColor = Color.FromArgb(228, 143, 69),
                                AutoSize = false,
                                Size = new Size(100, 68)
                            };

                            Label bp = new Label
                            {
                                Text = "₱" + baseprice.ToString(),
                                TextAlign = ContentAlignment.BottomLeft,
                                Location = new Point(100, 75),
                                Font = new Font("Century Gothic", 10),
                                ForeColor = Color.FromArgb(228, 143, 69)
                            };

                            Panel container = new Panel();
                            container.Controls.Add(pictureBox);
                            container.Controls.Add(label);
                            container.Controls.Add(bp);

                            label.BringToFront();
                            bp.BringToFront();

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
            dashboard.Show();
        }

        private void btnAdd2Cart_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand checkQuantityCmd = connection.CreateCommand())
                    {
                        checkQuantityCmd.CommandText = "SELECT quantity FROM tblInventory inner join tblItems on tblInventory.item_id = tblItems.itemId where itemName = '" + cbItmName.Text + "'";
                        int checkQuantity = Convert.ToInt32(checkQuantityCmd.ExecuteScalar());

                        if (checkQuantity < Convert.ToInt32(numQuantity.Text))
                        {
                            MessageBox.Show("Not Enough Stock!");
                            return;
                        }
                    }

                    using (MySqlCommand addToCartCmd = connection.CreateCommand())
                    {
                        if (cbItmName.SelectedIndex == -1 || numQuantity.Value == 0)
                        {
                            MessageBox.Show("Inputs are empty");
                            return;
                        }

                        addToCartCmd.CommandText = "INSERT INTO tblCart(item_id, quantity, basePrice, total_price) " +
                                                  "VALUES ((SELECT itemId FROM tblItems WHERE itemName = '" + cbItmName.Text + "'), " + numQuantity.Text + ", " +
                                                  "(SELECT base_price FROM tblItems WHERE itemName = '" + cbItmName.Text + "'), " +
                                                  "(" + numQuantity.Text + " * basePrice))";
                        addToCartCmd.ExecuteNonQuery();

                        addToCartCmd.CommandText = "SELECT MAX(receiptId) from tblSales";
                        rip = Convert.ToInt32(addToCartCmd.ExecuteScalar());

                        lblRid.Text = "Order #: " + (rip + 1);
                        //rip = Convert.ToInt32(addToCartCmd.ExecuteScalar());
                        //addToCartCmd.CommandText = "select max(id) from tblCart";
                        //int id = Convert.ToInt32(addToCartCmd.ExecuteScalar());
                        //MessageBox.Show(id.ToString());
                        //cartId = id;
                    }

                    using (MySqlCommand calculateTotalCmd = connection.CreateCommand())
                    {
                        calculateTotalCmd.CommandText = "SELECT SUM(total_price) AS total_sum FROM tblCart";
                        object result = calculateTotalCmd.ExecuteScalar();

                        MessageBox.Show("Success!");
                        showCart();
                        // para sa list of cart
                        calculateTotalCmd.CommandText = "SELECT itemId FROM tblItems WHERE itemName = '" + cbItmName.Text + "'";
                        int itmid = Convert.ToInt32(calculateTotalCmd.ExecuteScalar());

                        itemid.Add(itmid);
                        qtt.Add(Int32.Parse(numQuantity.Text));
                        lblTotal.Text = Convert.ToDecimal(result).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* public void showtblCart()
         {
             using (MySqlConnection connection = new MySqlConnection(Login.con))
             {
                 connection.Open();
                 MySqlCommand cmd = connection.CreateCommand();

                 try
                 {
                     cmd.CommandText = "SELECT itemName, basePrice, total_price, quantity FROM tblCart inner join tblItems on tblItems.itemId = tblCart.item_id;";

                     using (MySqlDataReader reader = cmd.ExecuteReader())
                     {
                         tableCart.Controls.Clear();

                         tableCart.ColumnCount = 1;

                         while (reader.Read())
                         {
                             String itemName = reader.GetString("itemName");
                             Decimal baseprice = reader.GetDecimal("basePrice");
                             int quantity = reader.GetInt32("quantity");
                             Decimal totalprice = reader.GetDecimal("total_price");

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
                                 Location = new Point(10, 60),
                                 Font = new Font("Century Gothic", 10),
                                 ForeColor = Color.FromArgb(228, 143, 69)
                             };
                             Label qtt = new Label
                             {
                                 Text = "Quantity: " + quantity.ToString(),
                                 TextAlign = ContentAlignment.BottomLeft,
                                 Location = new Point(10, 30),
                                 Font = new Font("Century Gothic", 10),
                                 ForeColor = Color.FromArgb(228, 143, 69)
                             };
                             Label tp = new Label
                             {
                                 Text = "₱" + totalprice.ToString(),
                                 TextAlign = ContentAlignment.BottomLeft,
                                 Location = new Point(120, 60),
                                 Font = new Font("Century Gothic", 10),
                                 ForeColor = Color.FromArgb(228, 143, 69)
                             };

                             *//*  Button btnCancel = new Button
                               {
                                   Name = "delet" + cartId.ToString(),
                                   Location = new Point(155, 55),
                                   Size = new Size(20, 20),
                                   BackgroundImage = Image.FromFile("C:/Users/tizon/Desktop/img/icons8-minus-24.png"),
                                   BackgroundImageLayout = ImageLayout.Zoom,
                                   Cursor = Cursors.Hand,
                                   FlatStyle = FlatStyle.Flat,
                               };*//*

                             Panel container = new Panel();

                             container.Controls.Add(label);
                             container.Controls.Add(bp);
                             container.Controls.Add(qtt);
                             container.Controls.Add(tp);
                             //container.Controls.Add(btnCancel);

                             //btnCancel.Click += btnCancel_Click;

                             label.BringToFront();
                             bp.BringToFront();
                             qtt.BringToFront();
                             tp.BringToFront();
                             //btnCancel.BringToFront();

                             tableCart.Controls.Add(container);
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Error: " + ex.Message);
                 }
             }
         }*/

        /* private void btnCancel_Click(object sender, EventArgs e)
         {
             using (MySqlConnection connection = new MySqlConnection(Login.con))
             {
                 try
                 {
                     connection.Open();
                     using (MySqlCommand cmd = connection.CreateCommand())
                     {
                         MessageBox.Show(cartId.ToString());
                         cmd.CommandText = "Delete from tblCart where id = " + cartId + "";
                         cmd.ExecuteNonQuery();
                         MessageBox.Show("DELETED");
                     }
                     //showtblCart();
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
         }*/

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM tblCart";
                        int cartItemCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (cartItemCount == 0)
                        {
                            MessageBox.Show("Cart is empty. Please add items to the cart before placing an order.");
                            return;
                        }

                        cmd.CommandText = "DELETE FROM tblCart";
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Order Placed!");
                        showCart();

                        cmd.CommandText = "SELECT MAX(receiptId) from tblSales";
                        rip = Convert.ToInt32(cmd.ExecuteScalar());

                        for (int i = 0; i < cartItemCount; i++)
                        {
                            cmd.CommandText = "UPDATE tblInventory SET quantity = quantity - " + qtt[i] + " WHERE item_id = " + itemid[i];
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO tblSales (receiptId, receiptDate, item_id, quantity, total_amount) VALUES( " + rip.ToString() + " + 1, CURDATE(), " + itemid[i] + ", " + qtt[i] + ", " + qtt[i] + " * (SELECT base_price from tblItems where itemId = " + itemid[i] + "))";
                            cmd.ExecuteNonQuery();
                        }

                        //lblRid.Text = "Order #: ";
                        //cmd.CommandText = "UPDATE tblInventory SET quantity = quantity - " + numQuantity.Text + " WHERE item_id = " + cbItemId.Text;

                        //cmd.ExecuteNonQuery();

                        cbItmName.SelectedIndex = -1;
                        numQuantity.Value = 0;
                        lblTotal.Text = string.Empty;
                        cbItmName.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showItmName()
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

                            cbItmName.DataSource = table;
                            cbItmName.DisplayMember = "itemName";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void showCart()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT id, itemName, basePrice, total_price, quantity FROM tblCart inner join tblItems on tblItems.itemId = tblCart.item_id;";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);

                    dataCart.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showuser()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    string user = Login.welcomeUser;
                    cmd.CommandText = "SELECT userName FROM tblUser WHERE userName = '" + user + "'";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblWelcomeUser.Text = "Welcome! " + reader["userName"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == buttonColumn.Index)
            {
                DataGridViewTextBoxCell quantityCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["quantity"];
                int currentQuantity = Convert.ToInt32(quantityCell.Value);

                DataGridViewTextBoxCell totaPriceCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["total_price"];
                decimal currentTotalPrice = Convert.ToDecimal(totaPriceCell.Value);

                if (currentQuantity > 0)
                {
                    currentQuantity--;

                    quantityCell.Value = currentQuantity.ToString();

                    decimal unitPrice = Convert.ToDecimal(dataCart.Rows[e.RowIndex].Cells["basePrice"].Value);

                    decimal newTotalPrice = unitPrice * currentQuantity;

                    totaPriceCell.Value = newTotalPrice.ToString();

                    UpdateDatabase(e.RowIndex, currentQuantity);
                }
                else
                {
                    MessageBox.Show("Quantity cannot be negative.");
                }
            }
        }

        private void UpdateDatabase(int rowIndex, int newQuantity)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                int productId = Convert.ToInt32(dataCart.Rows[rowIndex].Cells["id"].Value);

                decimal unitPrice = Convert.ToDecimal(dataCart.Rows[rowIndex].Cells["basePrice"].Value);

                decimal newTotalPrice = unitPrice * newQuantity;

                cmd.CommandText = "UPDATE tblCart SET quantity = " + newQuantity + ", total_price = " + newTotalPrice + " WHERE id = " + productId + "";

                cmd.ExecuteNonQuery();
            }
        }
    }
}