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
    public partial class Dashboard : Form
    {
        private bool Maincollapse;
        private Inventory inventory = new Inventory();

        public Dashboard()
        {
            InitializeComponent();
            Maincollapse = true;
            btnUser.Cursor = Cursors.Hand;
            btnSupplier.Cursor = Cursors.Hand;
            btnCategory.Cursor = Cursors.Hand;
            btnItems.Cursor = Cursors.Hand;
            inventory.showInventory();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (Maincollapse)
            {
                SubMain.Height += 10;
                if (SubMain.Height == SubMain.MaximumSize.Height)
                {
                    Maincollapse = false;
                    MainTimer.Stop();
                }
            }
            else
            {
                SubMain.Height -= 10;
                if (SubMain.Height == SubMain.MinimumSize.Height)
                {
                    Maincollapse = true;
                    MainTimer.Stop();
                }
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            MainTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS pos = new POS();
            pos.Show();
            this.Close();
        }

        private void addUser(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            itemCategory itmCat = new itemCategory();
            addUser(itmCat);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            Supplier sup = new Supplier();
            addUser(sup);
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            Items itm = new Items();
            addUser(itm);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            User us = new User();
            addUser(us);
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            Delivery delivery = new Delivery();
            addUser(delivery);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            addUser(inventory);
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            addUser(sales);
        }

        public void BackupRestore(String path, String toDo)
        {
            MySqlConnection conn = new MySqlConnection(Login.con);
            MySqlCommand cmd = new MySqlCommand();
            MySqlBackup mb = new MySqlBackup(cmd);
            try
            {
                cmd.Connection = conn;
                conn.Open();
                if (toDo.Equals("Backup"))
                {
                    mb.ExportToFile(path);
                }
                else
                {
                    mb.ImportFromFile(path);
                }
                MessageBox.Show("Database " + toDo + " successfully\n" +
                path);
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string sSelectedFolder, filePathName;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                sSelectedFolder = fbd.SelectedPath;
                filePathName = System.IO.Path.Combine(sSelectedFolder, "ThisIsTheBackUpFile" + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ".bak");
                //flf.BackupRestore(filePathName, "Backup");
            }
            else
            {
                sSelectedFolder = string.Empty;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string filePathName;
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.bak)|*.bak";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                filePathName = choofdlog.FileName;
                //flf.BackupRestore(filePathName, "Restore");
            }
            else
            {
                filePathName = string.Empty;
            }
        }
    }
}