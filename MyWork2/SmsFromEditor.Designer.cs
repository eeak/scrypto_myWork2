namespace MyWork2
{
    partial class SmsFromEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmsFromEditor));
            this.SmsTextBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.PhoneListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SmsReadyButton = new System.Windows.Forms.Button();
            this.SmsSoglasovanButton = new System.Windows.Forms.Button();
            this.SmsShablonButton = new System.Windows.Forms.Button();
            this.SmsPrivetButton = new System.Windows.Forms.Button();
            this.ShublonSetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SmsTextBox
            // 
            this.SmsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SmsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsTextBox.Location = new System.Drawing.Point(0, 2);
            this.SmsTextBox.Multiline = true;
            this.SmsTextBox.Name = "SmsTextBox";
            this.SmsTextBox.Size = new System.Drawing.Size(554, 339);
            this.SmsTextBox.TabIndex = 0;
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendButton.Image = ((System.Drawing.Image)(resources.GetObject("SendButton.Image")));
            this.SendButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SendButton.Location = new System.Drawing.Point(559, 319);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(198, 23);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // PhoneListView
            // 
            this.PhoneListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.PhoneListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhoneListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.PhoneListView.FullRowSelect = true;
            this.PhoneListView.GridLines = true;
            this.PhoneListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PhoneListView.HideSelection = false;
            this.PhoneListView.HotTracking = true;
            this.PhoneListView.HoverSelection = true;
            this.PhoneListView.Location = new System.Drawing.Point(560, 2);
            this.PhoneListView.Name = "PhoneListView";
            this.PhoneListView.Size = new System.Drawing.Size(197, 313);
            this.PhoneListView.TabIndex = 3;
            this.PhoneListView.UseCompatibleStateImageBehavior = false;
            this.PhoneListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Номера клиента";
            this.columnHeader1.Width = 193;
            // 
            // SmsReadyButton
            // 
            this.SmsReadyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SmsReadyButton.Location = new System.Drawing.Point(4, 344);
            this.SmsReadyButton.Name = "SmsReadyButton";
            this.SmsReadyButton.Size = new System.Drawing.Size(145, 36);
            this.SmsReadyButton.TabIndex = 4;
            this.SmsReadyButton.Text = "СМС ПО ГОТОВНОСТИ";
            this.SmsReadyButton.UseVisualStyleBackColor = true;
            this.SmsReadyButton.Click += new System.EventHandler(this.SmsReadyButton_Click);
            // 
            // SmsSoglasovanButton
            // 
            this.SmsSoglasovanButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SmsSoglasovanButton.Location = new System.Drawing.Point(155, 344);
            this.SmsSoglasovanButton.Name = "SmsSoglasovanButton";
            this.SmsSoglasovanButton.Size = new System.Drawing.Size(145, 36);
            this.SmsSoglasovanButton.TabIndex = 5;
            this.SmsSoglasovanButton.Text = "СМС ПО СОГЛАСОВАНИЮ";
            this.SmsSoglasovanButton.UseVisualStyleBackColor = true;
            this.SmsSoglasovanButton.Click += new System.EventHandler(this.SmsSoglasovanButton_Click);
            // 
            // SmsShablonButton
            // 
            this.SmsShablonButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SmsShablonButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SmsShablonButton.Location = new System.Drawing.Point(306, 344);
            this.SmsShablonButton.Name = "SmsShablonButton";
            this.SmsShablonButton.Size = new System.Drawing.Size(145, 36);
            this.SmsShablonButton.TabIndex = 6;
            this.SmsShablonButton.Text = "СМС ПО ВАШЕМУ ШАБЛОНУ";
            this.SmsShablonButton.UseVisualStyleBackColor = false;
            this.SmsShablonButton.Click += new System.EventHandler(this.SmsShablonButton_Click);
            // 
            // SmsPrivetButton
            // 
            this.SmsPrivetButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SmsPrivetButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SmsPrivetButton.Location = new System.Drawing.Point(457, 344);
            this.SmsPrivetButton.Name = "SmsPrivetButton";
            this.SmsPrivetButton.Size = new System.Drawing.Size(145, 36);
            this.SmsPrivetButton.TabIndex = 7;
            this.SmsPrivetButton.Text = "СМС C ПРИВЕТСТВИЕМ";
            this.SmsPrivetButton.UseVisualStyleBackColor = false;
            this.SmsPrivetButton.Click += new System.EventHandler(this.SmsPrivetButton_Click);
            // 
            // ShublonSetButton
            // 
            this.ShublonSetButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ShublonSetButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ShublonSetButton.Location = new System.Drawing.Point(608, 344);
            this.ShublonSetButton.Name = "ShublonSetButton";
            this.ShublonSetButton.Size = new System.Drawing.Size(145, 36);
            this.ShublonSetButton.TabIndex = 8;
            this.ShublonSetButton.Text = "НАСТРОИТЬ ШАБЛОНЫ СМС";
            this.ShublonSetButton.UseVisualStyleBackColor = false;
            this.ShublonSetButton.Click += new System.EventHandler(this.ShublonSetButton_Click);
            // 
            // SmsFromEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(759, 382);
            this.Controls.Add(this.ShublonSetButton);
            this.Controls.Add(this.SmsPrivetButton);
            this.Controls.Add(this.SmsShablonButton);
            this.Controls.Add(this.SmsSoglasovanButton);
            this.Controls.Add(this.SmsReadyButton);
            this.Controls.Add(this.PhoneListView);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SmsTextBox);
            this.Name = "SmsFromEditor";
            this.Text = " Отправка SMS";
            this.Load += new System.EventHandler(this.SmsFromEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SmsTextBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ListView PhoneListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button SmsReadyButton;
        private System.Windows.Forms.Button SmsSoglasovanButton;
        private System.Windows.Forms.Button SmsShablonButton;
        private System.Windows.Forms.Button SmsPrivetButton;
        private System.Windows.Forms.Button ShublonSetButton;
    }
}