namespace MyWork2
{
    partial class HistoryViewer
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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.UserComboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupsComboBox1 = new System.Windows.Forms.ComboBox();
            this.HistoryListView = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WHO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WHAT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FULLWHAT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SearchDataButton = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.ResetSearchButton = new System.Windows.Forms.Button();
            this.FullWhatTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendar1.Location = new System.Drawing.Point(948, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // UserComboBox1
            // 
            this.UserComboBox1.FormattingEnabled = true;
            this.UserComboBox1.Location = new System.Drawing.Point(5, 19);
            this.UserComboBox1.Name = "UserComboBox1";
            this.UserComboBox1.Size = new System.Drawing.Size(152, 21);
            this.UserComboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Группа доступа";
            // 
            // GroupsComboBox1
            // 
            this.GroupsComboBox1.FormattingEnabled = true;
            this.GroupsComboBox1.Location = new System.Drawing.Point(5, 59);
            this.GroupsComboBox1.Name = "GroupsComboBox1";
            this.GroupsComboBox1.Size = new System.Drawing.Size(152, 21);
            this.GroupsComboBox1.TabIndex = 3;
            // 
            // HistoryListView
            // 
            this.HistoryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.WHO,
            this.WHAT,
            this.FULLWHAT,
            this.Data});
            this.HistoryListView.FullRowSelect = true;
            this.HistoryListView.GridLines = true;
            this.HistoryListView.Location = new System.Drawing.Point(1, 1);
            this.HistoryListView.MultiSelect = false;
            this.HistoryListView.Name = "HistoryListView";
            this.HistoryListView.Size = new System.Drawing.Size(937, 501);
            this.HistoryListView.TabIndex = 1;
            this.HistoryListView.UseCompatibleStateImageBehavior = false;
            this.HistoryListView.View = System.Windows.Forms.View.Details;
            this.HistoryListView.VirtualMode = true;
            this.HistoryListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ClientsListView_RetrieveVirtualItem);
            this.HistoryListView.SelectedIndexChanged += new System.EventHandler(this.HistoryListView_SelectedIndexChanged);
            this.HistoryListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HistoryListView_MouseDoubleClick);
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 40;
            // 
            // WHO
            // 
            this.WHO.Text = "Кто менял";
            this.WHO.Width = 200;
            // 
            // WHAT
            // 
            this.WHAT.Text = "Что менял";
            this.WHAT.Width = 150;
            // 
            // FULLWHAT
            // 
            this.FULLWHAT.Text = "Полный список изменений";
            this.FULLWHAT.Width = 381;
            // 
            // Data
            // 
            this.Data.Text = "Дата";
            this.Data.Width = 160;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.GroupsComboBox1);
            this.panel1.Controls.Add(this.UserComboBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(948, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 144);
            this.panel1.TabIndex = 17;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(5, 95);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(152, 23);
            this.SearchButton.TabIndex = 5;
            this.SearchButton.Text = "Поиск";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SearchDataButton);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(948, 333);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 140);
            this.panel2.TabIndex = 18;
            // 
            // SearchDataButton
            // 
            this.SearchDataButton.Location = new System.Drawing.Point(5, 108);
            this.SearchDataButton.Name = "SearchDataButton";
            this.SearchDataButton.Size = new System.Drawing.Size(152, 23);
            this.SearchDataButton.TabIndex = 35;
            this.SearchDataButton.Text = "Поиск";
            this.SearchDataButton.UseVisualStyleBackColor = true;
            this.SearchDataButton.Click += new System.EventHandler(this.SearchDataButton_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker2.Location = new System.Drawing.Point(5, 73);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(152, 20);
            this.dateTimePicker2.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "по";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Выберите период:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(5, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(152, 20);
            this.dateTimePicker1.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "с";
            // 
            // ResetSearchButton
            // 
            this.ResetSearchButton.Location = new System.Drawing.Point(955, 481);
            this.ResetSearchButton.Name = "ResetSearchButton";
            this.ResetSearchButton.Size = new System.Drawing.Size(152, 23);
            this.ResetSearchButton.TabIndex = 19;
            this.ResetSearchButton.Text = "Сброс поиска";
            this.ResetSearchButton.UseVisualStyleBackColor = true;
            this.ResetSearchButton.Click += new System.EventHandler(this.ResetSearchButton_Click);
            // 
            // FullWhatTextBox
            // 
            this.FullWhatTextBox.Location = new System.Drawing.Point(1, 508);
            this.FullWhatTextBox.Multiline = true;
            this.FullWhatTextBox.Name = "FullWhatTextBox";
            this.FullWhatTextBox.Size = new System.Drawing.Size(1111, 112);
            this.FullWhatTextBox.TabIndex = 20;
            // 
            // HistoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 621);
            this.Controls.Add(this.FullWhatTextBox);
            this.Controls.Add(this.ResetSearchButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HistoryListView);
            this.Controls.Add(this.monthCalendar1);
            this.Name = "HistoryViewer";
            this.Text = "HistoryViewer";
            this.Load += new System.EventHandler(this.HistoryViewer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox UserComboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox GroupsComboBox1;
        public System.Windows.Forms.ListView HistoryListView;
        private System.Windows.Forms.ColumnHeader id;
        public System.Windows.Forms.ColumnHeader WHO;
        public System.Windows.Forms.ColumnHeader WHAT;
        public System.Windows.Forms.ColumnHeader FULLWHAT;
        public System.Windows.Forms.ColumnHeader Data;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SearchDataButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ResetSearchButton;
        private System.Windows.Forms.TextBox FullWhatTextBox;
    }
}