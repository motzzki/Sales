namespace Sales
{
    partial class POS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnTransact = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblWelcomeUser = new System.Windows.Forms.Label();
            this.panelCart = new System.Windows.Forms.Panel();
            this.dataCart = new System.Windows.Forms.DataGridView();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRid = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAdd2Cart = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbItmName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelItems = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataCart)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnTransact);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 639);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(9, 533);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(58, 59);
            this.btnBack.TabIndex = 2;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnTransact
            // 
            this.btnTransact.BackColor = System.Drawing.Color.Transparent;
            this.btnTransact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTransact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransact.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransact.FlatAppearance.BorderSize = 0;
            this.btnTransact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(204)))), ((int)(((byte)(160)))));
            this.btnTransact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.btnTransact.Image = ((System.Drawing.Image)(resources.GetObject("btnTransact.Image")));
            this.btnTransact.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransact.Location = new System.Drawing.Point(0, 219);
            this.btnTransact.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransact.Name = "btnTransact";
            this.btnTransact.Size = new System.Drawing.Size(200, 55);
            this.btnTransact.TabIndex = 3;
            this.btnTransact.Text = "   TRANSACTIONS";
            this.btnTransact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransact.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.lblWelcomeUser);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 219);
            this.panel3.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(42, 17);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 110);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblWelcomeUser
            // 
            this.lblWelcomeUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.lblWelcomeUser.Location = new System.Drawing.Point(40, 143);
            this.lblWelcomeUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcomeUser.Name = "lblWelcomeUser";
            this.lblWelcomeUser.Size = new System.Drawing.Size(121, 55);
            this.lblWelcomeUser.TabIndex = 0;
            this.lblWelcomeUser.Text = "\r\n";
            this.lblWelcomeUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcomeUser.UseCompatibleTextRendering = true;
            // 
            // panelCart
            // 
            this.panelCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            this.panelCart.Controls.Add(this.dataCart);
            this.panelCart.Controls.Add(this.btnPlaceOrder);
            this.panelCart.Controls.Add(this.panel6);
            this.panelCart.Controls.Add(this.panel5);
            this.panelCart.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCart.Location = new System.Drawing.Point(806, 0);
            this.panelCart.Margin = new System.Windows.Forms.Padding(2);
            this.panelCart.Name = "panelCart";
            this.panelCart.Size = new System.Drawing.Size(428, 639);
            this.panelCart.TabIndex = 1;
            // 
            // dataCart
            // 
            this.dataCart.AllowUserToAddRows = false;
            this.dataCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataCart.BackgroundColor = System.Drawing.Color.Snow;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataCart.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataCart.Location = new System.Drawing.Point(12, 129);
            this.dataCart.Margin = new System.Windows.Forms.Padding(2);
            this.dataCart.Name = "dataCart";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataCart.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataCart.RowHeadersVisible = false;
            this.dataCart.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataCart.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataCart.RowTemplate.Height = 24;
            this.dataCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataCart.Size = new System.Drawing.Size(405, 360);
            this.dataCart.TabIndex = 56;
            this.dataCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataCart_CellContentClick);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.btnPlaceOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaceOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(212)))), ((int)(((byte)(255)))));
            this.btnPlaceOrder.FlatAppearance.BorderSize = 0;
            this.btnPlaceOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            this.btnPlaceOrder.Location = new System.Drawing.Point(250, 536);
            this.btnPlaceOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(156, 43);
            this.btnPlaceOrder.TabIndex = 5;
            this.btnPlaceOrder.Text = "PLACE ORDER";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblTotal);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Location = new System.Drawing.Point(4, 503);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(230, 78);
            this.panel6.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblTotal.Location = new System.Drawing.Point(82, 46);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(73, 19);
            this.lblTotal.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.label7.Location = new System.Drawing.Point(4, 46);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total: ₱";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.lblRid);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(428, 119);
            this.panel5.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "CART";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRid
            // 
            this.lblRid.AutoSize = true;
            this.lblRid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.lblRid.Location = new System.Drawing.Point(8, 84);
            this.lblRid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRid.Name = "lblRid";
            this.lblRid.Size = new System.Drawing.Size(66, 20);
            this.lblRid.TabIndex = 1;
            this.lblRid.Text = "Order #:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.label3.Location = new System.Drawing.Point(4, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "MENU";
            // 
            // numQuantity
            // 
            this.numQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuantity.Location = new System.Drawing.Point(292, 71);
            this.numQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(129, 23);
            this.numQuantity.TabIndex = 1;
            // 
            // btnAdd2Cart
            // 
            this.btnAdd2Cart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.btnAdd2Cart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd2Cart.FlatAppearance.BorderSize = 0;
            this.btnAdd2Cart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd2Cart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd2Cart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(36)))), ((int)(((byte)(12)))));
            this.btnAdd2Cart.Location = new System.Drawing.Point(284, 112);
            this.btnAdd2Cart.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd2Cart.Name = "btnAdd2Cart";
            this.btnAdd2Cart.Size = new System.Drawing.Size(145, 37);
            this.btnAdd2Cart.TabIndex = 2;
            this.btnAdd2Cart.Text = "ADD TO CART";
            this.btnAdd2Cart.UseVisualStyleBackColor = false;
            this.btnAdd2Cart.Click += new System.EventHandler(this.btnAdd2Cart_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cbItmName);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.btnAdd2Cart);
            this.panel7.Controls.Add(this.numQuantity);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(606, 183);
            this.panel7.TabIndex = 4;
            // 
            // cbItmName
            // 
            this.cbItmName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbItmName.FormattingEnabled = true;
            this.cbItmName.Location = new System.Drawing.Point(292, 33);
            this.cbItmName.Margin = new System.Windows.Forms.Padding(2);
            this.cbItmName.Name = "cbItmName";
            this.cbItmName.Size = new System.Drawing.Size(130, 25);
            this.cbItmName.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.label9.Location = new System.Drawing.Point(189, 72);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "Quantity:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(143)))), ((int)(((byte)(69)))));
            this.label8.Location = new System.Drawing.Point(178, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Item Name:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(200, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(606, 230);
            this.panel4.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanelItems);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(200, 230);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(606, 409);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanelItems
            // 
            this.tableLayoutPanelItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelItems.ColumnCount = 3;
            this.tableLayoutPanelItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.flowLayoutPanel1.SetFlowBreak(this.tableLayoutPanelItems, true);
            this.tableLayoutPanelItems.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanelItems.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelItems.Name = "tableLayoutPanelItems";
            this.tableLayoutPanelItems.RowCount = 2;
            this.tableLayoutPanelItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelItems.Size = new System.Drawing.Size(600, 407);
            this.tableLayoutPanelItems.TabIndex = 0;
            // 
            // POS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(41)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1234, 639);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelCart);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "POS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataCart)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnTransact;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWelcomeUser;
        private System.Windows.Forms.Label lblRid;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnAdd2Cart;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelItems;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ComboBox cbItmName;
        private System.Windows.Forms.DataGridView dataCart;
    }
}