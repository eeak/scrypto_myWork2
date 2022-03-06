namespace MyWork2
{
    partial class Stock
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
            this.StockSearchTextBox = new System.Windows.Forms.TextBox();
            this.KategoryComboBox = new System.Windows.Forms.ComboBox();
            this.PodKategoryComboBox = new System.Windows.Forms.ComboBox();
            this.StockListView = new System.Windows.Forms.ListView();
            this.Art = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PosName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Colour = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Brand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddZip = new System.Windows.Forms.Button();
            this.BrandComboBox = new System.Windows.Forms.ComboBox();
            this.ColourComboBox = new System.Windows.Forms.ComboBox();
            this.ModelTextBox = new System.Windows.Forms.TextBox();
            this.BuyCheckBox = new System.Windows.Forms.CheckBox();
            this.ClientUsedCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // StockSearchTextBox
            // 
            this.StockSearchTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.StockSearchTextBox.Location = new System.Drawing.Point(12, 12);
            this.StockSearchTextBox.Name = "StockSearchTextBox";
            this.StockSearchTextBox.Size = new System.Drawing.Size(326, 20);
            this.StockSearchTextBox.TabIndex = 0;
            this.StockSearchTextBox.TextChanged += new System.EventHandler(this.StockSearchTextBox_TextChanged);
            // 
            // KategoryComboBox
            // 
            this.KategoryComboBox.FormattingEnabled = true;
            this.KategoryComboBox.Location = new System.Drawing.Point(358, 12);
            this.KategoryComboBox.Name = "KategoryComboBox";
            this.KategoryComboBox.Size = new System.Drawing.Size(179, 21);
            this.KategoryComboBox.TabIndex = 1;
            this.KategoryComboBox.TextChanged += new System.EventHandler(this.KategoryComboBox_TextChanged);
            // 
            // PodKategoryComboBox
            // 
            this.PodKategoryComboBox.FormattingEnabled = true;
            this.PodKategoryComboBox.Location = new System.Drawing.Point(558, 12);
            this.PodKategoryComboBox.Name = "PodKategoryComboBox";
            this.PodKategoryComboBox.Size = new System.Drawing.Size(179, 21);
            this.PodKategoryComboBox.TabIndex = 2;
            this.PodKategoryComboBox.TextChanged += new System.EventHandler(this.PodKategoryComboBox_TextChanged);
            // 
            // StockListView
            // 
            this.StockListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StockListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Art,
            this.PosName,
            this.Colour,
            this.Brand,
            this.Model,
            this.Count,
            this.Price});
            this.StockListView.FullRowSelect = true;
            this.StockListView.GridLines = true;
            this.StockListView.Location = new System.Drawing.Point(12, 68);
            this.StockListView.MultiSelect = false;
            this.StockListView.Name = "StockListView";
            this.StockListView.Size = new System.Drawing.Size(922, 483);
            this.StockListView.TabIndex = 3;
            this.StockListView.UseCompatibleStateImageBehavior = false;
            this.StockListView.View = System.Windows.Forms.View.Details;
            this.StockListView.VirtualMode = true;
            this.StockListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.StockListView_ColumnClick);
            this.StockListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.StockListView_RetrieveVirtualItem);
            this.StockListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.StockListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StockListView_MouseDoubleClick);
            // 
            // Art
            // 
            this.Art.Text = "Арт.";
            this.Art.Width = 40;
            // 
            // PosName
            // 
            this.PosName.Text = "Наименование";
            this.PosName.Width = 540;
            // 
            // Colour
            // 
            this.Colour.Text = "Цвет";
            // 
            // Brand
            // 
            this.Brand.Text = "Бренд";
            this.Brand.Width = 80;
            // 
            // Model
            // 
            this.Model.Text = "Модель";
            this.Model.Width = 80;
            // 
            // Count
            // 
            this.Count.Text = "Количество";
            this.Count.Width = 70;
            // 
            // Price
            // 
            this.Price.Text = "Цена";
            // 
            // AddZip
            // 
            this.AddZip.Location = new System.Drawing.Point(12, 38);
            this.AddZip.Name = "AddZip";
            this.AddZip.Size = new System.Drawing.Size(326, 23);
            this.AddZip.TabIndex = 4;
            this.AddZip.Text = "Добавить ЗИП";
            this.AddZip.UseVisualStyleBackColor = true;
            this.AddZip.Click += new System.EventHandler(this.AddZip_Click);
            // 
            // BrandComboBox
            // 
            this.BrandComboBox.FormattingEnabled = true;
            this.BrandComboBox.Location = new System.Drawing.Point(755, 12);
            this.BrandComboBox.Name = "BrandComboBox";
            this.BrandComboBox.Size = new System.Drawing.Size(179, 21);
            this.BrandComboBox.TabIndex = 5;
            this.BrandComboBox.TextChanged += new System.EventHandler(this.BrandComboBox_TextChanged);
            // 
            // ColourComboBox
            // 
            this.ColourComboBox.FormattingEnabled = true;
            this.ColourComboBox.Location = new System.Drawing.Point(558, 38);
            this.ColourComboBox.Name = "ColourComboBox";
            this.ColourComboBox.Size = new System.Drawing.Size(179, 21);
            this.ColourComboBox.TabIndex = 7;
            this.ColourComboBox.TextChanged += new System.EventHandler(this.ColourComboBox_TextChanged);
            // 
            // ModelTextBox
            // 
            this.ModelTextBox.Location = new System.Drawing.Point(358, 39);
            this.ModelTextBox.Name = "ModelTextBox";
            this.ModelTextBox.Size = new System.Drawing.Size(179, 20);
            this.ModelTextBox.TabIndex = 9;
            this.ModelTextBox.TextChanged += new System.EventHandler(this.ModelTextBox_TextChanged);
            // 
            // BuyCheckBox
            // 
            this.BuyCheckBox.AutoSize = true;
            this.BuyCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.BuyCheckBox.Location = new System.Drawing.Point(859, 41);
            this.BuyCheckBox.Name = "BuyCheckBox";
            this.BuyCheckBox.Size = new System.Drawing.Size(75, 17);
            this.BuyCheckBox.TabIndex = 10;
            this.BuyCheckBox.Text = "Докупить";
            this.BuyCheckBox.UseVisualStyleBackColor = false;
            this.BuyCheckBox.CheckedChanged += new System.EventHandler(this.BuyCheckBox_CheckedChanged);
            // 
            // ClientUsedCheckBox
            // 
            this.ClientUsedCheckBox.AutoSize = true;
            this.ClientUsedCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.ClientUsedCheckBox.Location = new System.Drawing.Point(755, 41);
            this.ClientUsedCheckBox.Name = "ClientUsedCheckBox";
            this.ClientUsedCheckBox.Size = new System.Drawing.Size(101, 17);
            this.ClientUsedCheckBox.TabIndex = 11;
            this.ClientUsedCheckBox.Text = "Исп. клиентом";
            this.ClientUsedCheckBox.UseVisualStyleBackColor = false;
            this.ClientUsedCheckBox.CheckedChanged += new System.EventHandler(this.ClientUsedCheckBox_CheckedChanged);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 563);
            this.Controls.Add(this.ClientUsedCheckBox);
            this.Controls.Add(this.BuyCheckBox);
            this.Controls.Add(this.ModelTextBox);
            this.Controls.Add(this.ColourComboBox);
            this.Controls.Add(this.BrandComboBox);
            this.Controls.Add(this.AddZip);
            this.Controls.Add(this.StockListView);
            this.Controls.Add(this.PodKategoryComboBox);
            this.Controls.Add(this.KategoryComboBox);
            this.Controls.Add(this.StockSearchTextBox);
            this.Name = "Stock";
            this.Text = "Stock";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Stock_FormClosed);
            this.Load += new System.EventHandler(this.Stock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox StockSearchTextBox;
        public System.Windows.Forms.ComboBox KategoryComboBox;
        public System.Windows.Forms.ComboBox PodKategoryComboBox;
        public System.Windows.Forms.ListView StockListView;
        public System.Windows.Forms.ColumnHeader PosName;
        public System.Windows.Forms.ColumnHeader Colour;
        public System.Windows.Forms.ColumnHeader Count;
        public System.Windows.Forms.ColumnHeader Price;
        public System.Windows.Forms.ColumnHeader Brand;
        public System.Windows.Forms.ColumnHeader Model;
        public System.Windows.Forms.Button AddZip;
        public System.Windows.Forms.ComboBox BrandComboBox;
        public System.Windows.Forms.ComboBox ColourComboBox;
        public System.Windows.Forms.TextBox ModelTextBox;
        private System.Windows.Forms.ColumnHeader Art;
        private System.Windows.Forms.CheckBox BuyCheckBox;
        private System.Windows.Forms.CheckBox ClientUsedCheckBox;
    }
}