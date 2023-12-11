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
    public partial class Items : UserControl
    {
        private Boolean update, delete;

        public Items()
        {
            InitializeComponent();
            update = true;
            delete = true;
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showItems();
                    showSupplierId();
                    showNameAndId();
                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showItems()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select itemId, itemName, supplierName, categoryName, base_price from tblItems inner join tblSupplier on tblSupplier.supplierId = tblItems.supplier_id inner join tblItemCategory on tblItemCategory.CategoryId = tblItems.category_id order by itemId DESC;";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataItems.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        private void showNameAndId()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM tblItemCategory;";

                        using (MySqlDataAdapter adap = new MySqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adap.Fill(table);

                            DataRow dr = table.NewRow();
                            table.Rows.InsertAt(dr, 0);

                            cbCatId.DataSource = table;
                            cbCatId.DisplayMember = "categoryName";
                            //cbCatId.ValueMember = "categoryName";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void showSupplierId()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM tblSupplier;";

                        using (MySqlDataAdapter adap = new MySqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adap.Fill(table);

                            DataRow dr = table.NewRow();
                            table.Rows.InsertAt(dr, 0);

                            cbSupId.DataSource = table;
                            cbSupId.DisplayMember = "supplierName";
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
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            if (pbItemImg.Image != null)
            {
                pbItemImg.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            byte[] img_arr = new byte[ms.Length];
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            ms.Read(img_arr, 0, img_arr.Length);

            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;

                try
                {
                    if (string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                    {
                        MessageBox.Show("Please Input Fields!");
                        return;
                    }

                    if (!update)
                    {
                        cmd.CommandText = "UPDATE tblItems SET itemName = '" + txtItemName.Text + "', supplier_id = '" + cbSupId.Text + "', category_id = " + cbCatId.Text + ", base_price = " + txtPrice.Text + ", itemImg = @image WHERE itemId = " + txtItemID.Text + "";
                        MessageBox.Show("Item Updated!");
                        txtItemID.Clear();
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO tblItems(itemName, supplier_id, category_id, base_price, itemImg) VALUES ('" + txtItemName.Text + "', (select supplierId from tblSupplier where supplierName = '" + cbSupId.Text + "'), (select categoryId from tblItemCategory where categoryName = '" + cbCatId.Text + "'), " + txtPrice.Text + ", @image)";
                        MessageBox.Show("Success Query!");
                    }

                    cmd.Parameters.AddWithValue("@image", img_arr);
                    cmd.ExecuteNonQuery();

                    txtItemName.Clear();
                    cbSupId.SelectedIndex = -1;
                    cbCatId.SelectedIndex = -1;
                    txtPrice.Value = 0;
                    txtItemID.Focus();

                    showItems();

                    update = true;
                    delete = true;
                    showItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnInsertImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbItemImg.Image = new System.Drawing.Bitmap(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
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
                        cmd.CommandText = "DELETE FROM tblItems WHERE itemId = " + txtItemID.Text + "";
                        MessageBox.Show("Item Deleted!");
                        cmd.ExecuteNonQuery();
                        delete = true;
                        showItems();
                    }
                    catch (Exception z)
                    {
                        MessageBox.Show(z.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            update = true;
            delete = true;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            update = false;
        }
    }
}