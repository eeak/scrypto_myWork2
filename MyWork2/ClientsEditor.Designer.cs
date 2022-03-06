namespace MyWork2
{
    partial class ClientsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsEditor));
            this.AddZip = new System.Windows.Forms.Button();
            this.ClientsListView = new System.Windows.Forms.ListView();
            this.Art = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PosName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Colour = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Brand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientFIOSearchTextBox = new System.Windows.Forms.TextBox();
            this.ClientPhoneSearchTextBox = new System.Windows.Forms.TextBox();
            this.DeleteClientButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ClitenToClientButton = new System.Windows.Forms.Button();
            this.RepairHistoryLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // AddZip
            // 
            this.AddZip.BackColor = System.Drawing.SystemColors.Window;
            this.AddZip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddZip.Location = new System.Drawing.Point(335, 5);
            this.AddZip.Name = "AddZip";
            this.AddZip.Size = new System.Drawing.Size(76, 47);
            this.AddZip.TabIndex = 3;
            this.AddZip.Text = "Добавить клиента";
            this.AddZip.UseVisualStyleBackColor = false;
            this.AddZip.Click += new System.EventHandler(this.AddZip_Click);
            // 
            // ClientsListView
            // 
            this.ClientsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Art,
            this.PosName,
            this.Colour,
            this.Brand,
            this.Model,
            this.Count,
            this.Price,
            this.date});
            this.ClientsListView.FullRowSelect = true;
            this.ClientsListView.GridLines = true;
            this.ClientsListView.Location = new System.Drawing.Point(3, 57);
            this.ClientsListView.MultiSelect = false;
            this.ClientsListView.Name = "ClientsListView";
            this.ClientsListView.Size = new System.Drawing.Size(965, 612);
            this.ClientsListView.TabIndex = 15;
            this.ClientsListView.UseCompatibleStateImageBehavior = false;
            this.ClientsListView.View = System.Windows.Forms.View.Details;
            this.ClientsListView.VirtualMode = true;
            this.ClientsListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ClientsListView_RetrieveVirtualItem);
            this.ClientsListView.SelectedIndexChanged += new System.EventHandler(this.ClientsListView_SelectedIndexChanged);
            this.ClientsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClientsListView_MouseDoubleClick);
            // 
            // Art
            // 
            this.Art.Text = "id";
            this.Art.Width = 40;
            // 
            // PosName
            // 
            this.PosName.Text = "ФИО";
            this.PosName.Width = 200;
            // 
            // Colour
            // 
            this.Colour.Text = "Телефон";
            this.Colour.Width = 113;
            // 
            // Brand
            // 
            this.Brand.Text = "Адрес";
            this.Brand.Width = 80;
            // 
            // Model
            // 
            this.Model.Text = "Откуда узнал";
            this.Model.Width = 80;
            // 
            // Count
            // 
            this.Count.Text = "Чёрный список";
            this.Count.Width = 100;
            // 
            // Price
            // 
            this.Price.Text = "Примечание";
            this.Price.Width = 226;
            // 
            // date
            // 
            this.date.Text = "Дата прихода";
            this.date.Width = 103;
            // 
            // ClientFIOSearchTextBox
            // 
            this.ClientFIOSearchTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.ClientFIOSearchTextBox.Location = new System.Drawing.Point(26, 5);
            this.ClientFIOSearchTextBox.Name = "ClientFIOSearchTextBox";
            this.ClientFIOSearchTextBox.Size = new System.Drawing.Size(301, 20);
            this.ClientFIOSearchTextBox.TabIndex = 1;
            this.ClientFIOSearchTextBox.TextChanged += new System.EventHandler(this.ClientFIOSearchTextBox_TextChanged);
            // 
            // ClientPhoneSearchTextBox
            // 
            this.ClientPhoneSearchTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.ClientPhoneSearchTextBox.Location = new System.Drawing.Point(26, 31);
            this.ClientPhoneSearchTextBox.Name = "ClientPhoneSearchTextBox";
            this.ClientPhoneSearchTextBox.Size = new System.Drawing.Size(301, 20);
            this.ClientPhoneSearchTextBox.TabIndex = 2;
            this.ClientPhoneSearchTextBox.TextChanged += new System.EventHandler(this.ClientPhoneSearchTextBox_TextChanged);
            // 
            // DeleteClientButton
            // 
            this.DeleteClientButton.BackColor = System.Drawing.SystemColors.Window;
            this.DeleteClientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteClientButton.Location = new System.Drawing.Point(417, 5);
            this.DeleteClientButton.Name = "DeleteClientButton";
            this.DeleteClientButton.Size = new System.Drawing.Size(76, 47);
            this.DeleteClientButton.TabIndex = 4;
            this.DeleteClientButton.Text = "Удалить клиента";
            this.DeleteClientButton.UseVisualStyleBackColor = false;
            this.DeleteClientButton.Click += new System.EventHandler(this.DeleteClientButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 20);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(19, 20);
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // ClitenToClientButton
            // 
            this.ClitenToClientButton.BackColor = System.Drawing.SystemColors.Window;
            this.ClitenToClientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClitenToClientButton.Location = new System.Drawing.Point(499, 5);
            this.ClitenToClientButton.Name = "ClitenToClientButton";
            this.ClitenToClientButton.Size = new System.Drawing.Size(81, 47);
            this.ClitenToClientButton.TabIndex = 5;
            this.ClitenToClientButton.Text = "Объединить клиентов";
            this.ClitenToClientButton.UseVisualStyleBackColor = false;
            this.ClitenToClientButton.Click += new System.EventHandler(this.ClitenToClientButton_Click);
            // 
            // RepairHistoryLabel
            // 
            this.RepairHistoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RepairHistoryLabel.Location = new System.Drawing.Point(1008, 7);
            this.RepairHistoryLabel.Name = "RepairHistoryLabel";
            this.RepairHistoryLabel.Size = new System.Drawing.Size(166, 19);
            this.RepairHistoryLabel.TabIndex = 204;
            this.RepairHistoryLabel.Text = "История клиента";
            this.RepairHistoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(972, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 640);
            this.panel1.TabIndex = 205;
            // 
            // ClientsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 672);
            this.Controls.Add(this.RepairHistoryLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ClitenToClientButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DeleteClientButton);
            this.Controls.Add(this.ClientPhoneSearchTextBox);
            this.Controls.Add(this.AddZip);
            this.Controls.Add(this.ClientsListView);
            this.Controls.Add(this.ClientFIOSearchTextBox);
            this.Name = "ClientsEditor";
            this.Text = "ClientsEditor";
            this.Load += new System.EventHandler(this.ClientsEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button AddZip;
        public System.Windows.Forms.ListView ClientsListView;
        private System.Windows.Forms.ColumnHeader Art;
        public System.Windows.Forms.ColumnHeader PosName;
        public System.Windows.Forms.ColumnHeader Colour;
        public System.Windows.Forms.ColumnHeader Brand;
        public System.Windows.Forms.ColumnHeader Model;
        public System.Windows.Forms.ColumnHeader Count;
        public System.Windows.Forms.ColumnHeader Price;
        public System.Windows.Forms.TextBox ClientFIOSearchTextBox;
        public System.Windows.Forms.TextBox ClientPhoneSearchTextBox;
        private System.Windows.Forms.ColumnHeader date;
        public System.Windows.Forms.Button DeleteClientButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Button ClitenToClientButton;
        private System.Windows.Forms.Label RepairHistoryLabel;
        public System.Windows.Forms.Panel panel1;
    }
}