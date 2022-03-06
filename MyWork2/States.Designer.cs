namespace MyWork2
{
    partial class States
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
            this.StockListView = new System.Windows.Forms.ListView();
            this.Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumInBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DeleteStateButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StockListView
            // 
            this.StockListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StockListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Number,
            this.Date,
            this.State,
            this.NumInBase});
            this.StockListView.FullRowSelect = true;
            this.StockListView.GridLines = true;
            this.StockListView.Location = new System.Drawing.Point(12, 12);
            this.StockListView.MultiSelect = false;
            this.StockListView.Name = "StockListView";
            this.StockListView.Size = new System.Drawing.Size(540, 213);
            this.StockListView.TabIndex = 4;
            this.StockListView.UseCompatibleStateImageBehavior = false;
            this.StockListView.View = System.Windows.Forms.View.Details;
            // 
            // Number
            // 
            this.Number.Text = "Номер";
            // 
            // Date
            // 
            this.Date.Text = "Дата";
            this.Date.Width = 160;
            // 
            // State
            // 
            this.State.Text = "Статус";
            this.State.Width = 315;
            // 
            // NumInBase
            // 
            this.NumInBase.Text = "Номер в базе";
            this.NumInBase.Width = 0;
            // 
            // DeleteStateButton1
            // 
            this.DeleteStateButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteStateButton1.Location = new System.Drawing.Point(440, 231);
            this.DeleteStateButton1.Name = "DeleteStateButton1";
            this.DeleteStateButton1.Size = new System.Drawing.Size(112, 23);
            this.DeleteStateButton1.TabIndex = 5;
            this.DeleteStateButton1.Text = "Удалить";
            this.DeleteStateButton1.UseVisualStyleBackColor = true;
            this.DeleteStateButton1.Click += new System.EventHandler(this.DeleteStateButton1_Click);
            // 
            // States
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 261);
            this.Controls.Add(this.DeleteStateButton1);
            this.Controls.Add(this.StockListView);
            this.Name = "States";
            this.Text = "States";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.States_FormClosed);
            this.Load += new System.EventHandler(this.States_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView StockListView;
        private System.Windows.Forms.ColumnHeader Number;
        public System.Windows.Forms.ColumnHeader Date;
        public System.Windows.Forms.ColumnHeader State;
        private System.Windows.Forms.ColumnHeader NumInBase;
        private System.Windows.Forms.Button DeleteStateButton1;
    }
}