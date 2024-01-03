using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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

        private DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

        public POS()
        {
            InitializeComponent();

            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            lblWelcomeUser.Text = "Welcome! \n" + Login.welcomeUser;
            showAllItems();
            showCart();
            showItmName();
            updateTotal();

            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.HeaderText = "ACTION";

            buttonColumn.Text = "-";

            dataCart.Columns.Add(buttonColumn);
        }

        private void showAllItems()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT itemName as 'NAME', itemImg 'IMAGE', base_price as 'PRICE' FROM tblItems";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataMenu.DataSource = ds.Tables[0].DefaultView;

                    if (dataMenu.Columns["IMAGE"] is DataGridViewImageColumn imageColumn)
                    {
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        /*    public void ShowAllItems()
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
            }*/

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
                            MessageBox.Show("Inputs are empty! / Insufficent quantity!");
                            return;
                        }

                        addToCartCmd.CommandText = "INSERT INTO tblCart(item_id, quantity, basePrice, total_price) " +
                                                  "VALUES (" + cbItmName.SelectedValue.ToString() + ", " + numQuantity.Text + ", " +
                                                  "(SELECT base_price FROM tblItems WHERE itemName = '" + cbItmName.Text + "'), " +
                                                  "(" + numQuantity.Text + " * basePrice))";
                        addToCartCmd.ExecuteNonQuery();

                        int ordernum = generateRandomNum();

                        lblOrderNum.Text = "ORD-" + ordernum;
                    }

                    using (MySqlCommand calculateTotalCmd = connection.CreateCommand())
                    {
                        calculateTotalCmd.CommandText = "SELECT SUM(total_price) AS total_sum FROM tblCart";
                        object result = calculateTotalCmd.ExecuteScalar();

                        MessageBox.Show("Added to Cart!");
                        showCart();

                        updateTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

                        int receiptId = generateRandomNum();
                        foreach (DataGridViewRow row in dataCart.Rows)
                        {
                            string itemName = row.Cells["NAME"].Value.ToString();
                            int quantity = Convert.ToInt32(row.Cells["QUANTITY"].Value.ToString());
                            decimal total_price = Convert.ToDecimal(row.Cells["TOTAL"].Value.ToString());

                            cmd.CommandText = "INSERT INTO tblSales (receiptId, receiptDate, item_id, quantity, total_amount) VALUES (" + receiptId + ", CURDATE(), (SELECT itemId FROM tblItems WHERE itemName = '" + itemName + "'), " + quantity + ", " + total_price + ")";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE tblInventory SET quantity = quantity - " + quantity + " WHERE item_id = (SELECT itemId FROM tblItems WHERE itemName = '" + itemName + "')";

                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "DELETE FROM tblCart";
                        cmd.ExecuteNonQuery();
                        showCart();
                        lblTotal.Text = string.Empty;
                        MessageBox.Show("Order Placed!");
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            /* List<object> rowData = new List<object>();
             rowData.Add(row.Cells["id"].Value);
             rowData.Add(row.Cells["itemName"].Value);
             rowData.Add(row.Cells["basePrice"].Value);
             rowData.Add(row.Cells["total_price"].Value);
             rowData.Add(row.Cells["quantity"].Value);
             allValues.Add(rowData);*/

            /* List<List<object>> allValues = new List<List<object>>();

             foreach (DataGridViewRow row in dataCart.Rows)
             {
                 List<object> rowData = new List<object>();

                 for (int i = 1; i <= 5; i++)
                 {
                     rowData.Add(row.Cells[i].Value);
                 }

                 allValues.Add(rowData);
             }

             // Format the values into a string
             string message = "Extracted Values:\n";

             foreach (List<object> rowData in allValues)
             {
                 message += string.Join(", ", rowData) + "\n";
             }

             MessageBox.Show(message, "Extracted Values", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
        }

        private int generateRandomNum()
        {
            Random random = new Random();
            int randomNum = random.Next(100, 1000);
            return randomNum;
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
                            cbItmName.ValueMember = "itemId";
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
                    cmd.CommandText = "SELECT id as 'ID', itemName as 'NAME', basePrice as 'PRICE', total_price as 'TOTAL', quantity as 'QUANTITY' FROM tblCart inner join tblItems on tblItems.itemId = tblCart.item_id;";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);

                    dataCart.DataSource = ds.Tables[0].DefaultView;
                    if (dataCart.Rows.Count == 0)
                    {
                        lblTotal.Text = string.Empty;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void dataCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == buttonColumn.Index)
            {
                DataGridViewTextBoxCell quantityCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["QUANTITY"];
                int currentQuantity = Convert.ToInt32(quantityCell.Value);

                DataGridViewTextBoxCell totaPriceCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["TOTAL"];
                decimal currentTotalPrice = Convert.ToDecimal(totaPriceCell.Value);

                if (currentQuantity > 1)
                {
                    currentQuantity--;

                    quantityCell.Value = currentQuantity.ToString();

                    decimal unitPrice = Convert.ToDecimal(dataCart.Rows[e.RowIndex].Cells["PRICE"].Value);

                    decimal newTotalPrice = unitPrice * currentQuantity;

                    totaPriceCell.Value = newTotalPrice.ToString();
                    UpdateDatabase(e.RowIndex, currentQuantity);
                }
                else
                {
                    using (MySqlConnection connection = new MySqlConnection(Login.con))
                    {
                        connection.Open();
                        MySqlCommand cmd = connection.CreateCommand();
                        int productId = Convert.ToInt32(dataCart.Rows[e.RowIndex].Cells["ID"].Value);

                        cmd.CommandText = "DELETE FROM tblCart where id = " + productId + "";
                        cmd.ExecuteNonQuery();
                    }
                }

                showCart();
                updateTotal();
            }
        }

        private void UpdateDatabase(int rowIndex, int newQuantity)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                int productId = Convert.ToInt32(dataCart.Rows[rowIndex].Cells["ID"].Value);

                decimal unitPrice = Convert.ToDecimal(dataCart.Rows[rowIndex].Cells["PRICE"].Value);

                decimal newTotalPrice = unitPrice * newQuantity;

                cmd.CommandText = "UPDATE tblCart SET quantity = " + newQuantity + ", total_price = " + newTotalPrice + " WHERE id = " + productId + "";

                cmd.ExecuteNonQuery();
            }
        }

        private void updateTotal()
        {
            decimal total_amount = 0;
            foreach (DataGridViewRow row in dataCart.Rows)
            {
                if (decimal.TryParse(row.Cells["TOTAL"].Value.ToString(), out decimal value))
                {
                    total_amount += value;
                    lblTotal.Text = total_amount.ToString();
                }
            }
        }

        /*private void showuser()
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
                           lblWelcomeUser.Text = "Welcome!\n" + reader["userName"].ToString();
                       }
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
           }
       }*/
    }
}