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
    public partial class Sales : UserControl
    {
        public Sales()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(180, 0, 0, 0);
            Login.con = "Server=localhost;Database=dbsales;User=root;Password=root;";
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    showSales();
                }
                catch
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }

        public void showSales()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.con))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select receiptId as 'RECEIPT ID', receiptDate as 'DATE', item_id as 'ITEM ID', quantity as 'QUANTITY', total_amount as 'TOTAL' From tblSales";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dataSales.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Problem!");
                }
            }
        }
    }
}