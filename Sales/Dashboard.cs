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
        bool Maincollapse;
        public Dashboard()
        {
            InitializeComponent();
            Maincollapse = true;
        }

       

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if(Maincollapse)
            {
                SubMain.Height += 10;
                if(SubMain.Height == SubMain.MaximumSize.Height)
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
    }
}
