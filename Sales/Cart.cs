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
    public partial class Cart : Form
    {
        private DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

        public Cart()
        {
            InitializeComponent();
            showCart();
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

                    buttonColumn.UseColumnTextForButtonValue = true;
                    buttonColumn.HeaderText = "Action";

                    buttonColumn.Text = "-";

                    dataCart.Columns.Add(buttonColumn);
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
                DataGridViewTextBoxCell quantityCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["quantity"];
                int currentQuantity = Convert.ToInt32(quantityCell.Value);

                DataGridViewTextBoxCell totaPriceCell = (DataGridViewTextBoxCell)dataCart.Rows[e.RowIndex].Cells["total_price"];
                decimal currentTotalPrice = Convert.ToInt32(totaPriceCell.Value);

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

                // Update the quantity and totalPrice in the tblCart table
                cmd.CommandText = "UPDATE tblCart SET quantity = " + newQuantity + ", total_price = " + newTotalPrice + " WHERE id = " + productId + "";

                cmd.ExecuteNonQuery();
            }
        }
    }
}