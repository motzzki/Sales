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
        private bool update = false, delete = false;
        private int selectedUser;
        private int indexRow;

        public Items()
        {
            InitializeComponent();
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";

            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showItems();
                    showSupplierName();
                    showCatName();
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
                    cmd.CommandText = "select itemId as 'ID', itemName as 'NAME', supplierName as 'SUPPLIER NAME', categoryName as 'CATEGORY NAME', base_price as 'PRICE', itemImg as 'IMAGE' from tblItems inner join tblSupplier on tblSupplier.supplierId = tblItems.supplier_id inner join tblItemCategory on tblItemCategory.CategoryId = tblItems.category_id order by itemId DESC;";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataItems.DataSource = ds.Tables[0].DefaultView;

                    if (dataItems.Columns["IMAGE"] is DataGridViewImageColumn imageColumn)
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

        private void showCatName()
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

                            cbCatName.DataSource = table;
                            cbCatName.DisplayMember = "categoryName";
                            cbCatName.ValueMember = "categoryId";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void showSupplierName()
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

                            cbSupName.DataSource = table;
                            cbSupName.DisplayMember = "supplierName";
                            cbSupName.ValueMember = "supplierId";
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
            else
            {
                MessageBox.Show("Please select Image.");
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
                    if (update == false)
                    {
                        cmd.CommandText = "INSERT INTO tblItems(itemName, supplier_id, category_id, base_price, itemImg) VALUES ('" + txtItemName.Text + "'," + cbSupName.SelectedValue.ToString() + ", " + cbCatName.SelectedValue.ToString() + ", " + txtPrice.Text + ", @image)";
                        MessageBox.Show("Success Query!");
                    }
                    else
                    {
                        selectedUser = GetSelectedUserId();

                        cmd.CommandText = "UPDATE tblItems SET itemName = '" + txtItemName.Text + "', supplier_id = '" + cbSupName.SelectedValue.ToString() + "', category_id = " + cbCatName.SelectedValue.ToString() + ", base_price = " + txtPrice.Text + ", itemImg = @image WHERE itemId = " + selectedUser + "";
                        MessageBox.Show("Item Updated!");
                        resetUpdate();
                    }

                    cmd.Parameters.AddWithValue("@image", img_arr);
                    cmd.ExecuteNonQuery();

                    txtItemName.Clear();
                    cbSupName.SelectedIndex = -1;
                    cbCatName.SelectedIndex = -1;
                    txtPrice.Value = 0;

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

        public int GetSelectedUserId()
        {
            if (dataItems.SelectedRows.Count >= 0)
            {
                return Convert.ToInt32(dataItems.SelectedRows[0].Cells["ID"].Value);
            }
            else
            {
                return -1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to delete? Please Select Desired Row", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                btnUpdate.Enabled = true;
                return;
            }
            else
            {
                //dataItems.Enabled = true;
                delete = true;
                update = false;
            }
        }

        private void dataItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedUser = GetSelectedUserId();
            indexRow = e.RowIndex;
            DataGridViewRow row = dataItems.Rows[indexRow];
            if (update)
            {
                txtItemName.Text = row.Cells[1].Value.ToString();
                cbSupName.Text = row.Cells[2].Value.ToString();
                cbCatName.Text = row.Cells[3].Value.ToString();
                txtPrice.Text = row.Cells[4].Value.ToString();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating Items, Please Select Desired Row", "Message");
            update = true;

            btnDelete.Enabled = false;
        }

        private void resetUpdate()
        {
            update = false;
            delete = false;
            btnDelete.Enabled = true;
        }

        private void DeleteUser(int itemId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Login.con))
                {
                    connection.Open();

                    using (MySqlCommand del = connection.CreateCommand())
                    {
                        del.CommandText = "DELETE FROM tblItems WHERE itemId = " + itemId + "";

                        del.ExecuteNonQuery();

                        MessageBox.Show("Item Deleted!");
                        showItems();
                        delete = false;
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