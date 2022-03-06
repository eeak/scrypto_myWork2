namespace MyWork2
{
    partial class ClientEditorTrue
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
            this.SaveClientButton = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.BlackListComboBox = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.PrimechanieTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ClientFioTextBox = new System.Windows.Forms.TextBox();
            this.ClientAdressTextBox = new System.Windows.Forms.TextBox();
            this.ClientAboutUsComboBox = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.ClientPhoneTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SaveClientButton
            // 
            this.SaveClientButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveClientButton.BackgroundImage = global::MyWork2.Properties.Resources.save;
            this.SaveClientButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SaveClientButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveClientButton.Location = new System.Drawing.Point(13, 646);
            this.SaveClientButton.Name = "SaveClientButton";
            this.SaveClientButton.Size = new System.Drawing.Size(340, 28);
            this.SaveClientButton.TabIndex = 221;
            this.SaveClientButton.Text = "Сохранить";
            this.SaveClientButton.UseVisualStyleBackColor = true;
            this.SaveClientButton.Click += new System.EventHandler(this.SaveClientButton_Click);
            // 
            // label30
            // 
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(12, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(340, 46);
            this.label30.TabIndex = 220;
            this.label30.Text = "Редактирование клиента номер";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(12, 599);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(340, 12);
            this.label28.TabIndex = 219;
            this.label28.Text = "Тип клиента";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BlackListComboBox
            // 
            this.BlackListComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlackListComboBox.FormattingEnabled = true;
            this.BlackListComboBox.Items.AddRange(new object[] {
            "Не проблемный",
            "Проблемный"});
            this.BlackListComboBox.Location = new System.Drawing.Point(12, 614);
            this.BlackListComboBox.Name = "BlackListComboBox";
            this.BlackListComboBox.Size = new System.Drawing.Size(340, 21);
            this.BlackListComboBox.TabIndex = 218;
            this.BlackListComboBox.Text = "Не проблемный";
            this.BlackListComboBox.TextChanged += new System.EventHandler(this.BlackListComboBox_TextChanged);
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(12, 232);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(340, 14);
            this.label29.TabIndex = 217;
            this.label29.Text = "Заметка о клиенте (клиенту не видна)";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrimechanieTextBox
            // 
            this.PrimechanieTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrimechanieTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrimechanieTextBox.Location = new System.Drawing.Point(12, 248);
            this.PrimechanieTextBox.Multiline = true;
            this.PrimechanieTextBox.Name = "PrimechanieTextBox";
            this.PrimechanieTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PrimechanieTextBox.Size = new System.Drawing.Size(340, 344);
            this.PrimechanieTextBox.TabIndex = 216;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(12, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(340, 13);
            this.label17.TabIndex = 212;
            this.label17.Text = "ФИО";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClientFioTextBox
            // 
            this.ClientFioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientFioTextBox.Location = new System.Drawing.Point(12, 72);
            this.ClientFioTextBox.Name = "ClientFioTextBox";
            this.ClientFioTextBox.Size = new System.Drawing.Size(340, 22);
            this.ClientFioTextBox.TabIndex = 208;
            // 
            // ClientAdressTextBox
            // 
            this.ClientAdressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientAdressTextBox.Location = new System.Drawing.Point(12, 189);
            this.ClientAdressTextBox.Multiline = true;
            this.ClientAdressTextBox.Name = "ClientAdressTextBox";
            this.ClientAdressTextBox.Size = new System.Drawing.Size(340, 41);
            this.ClientAdressTextBox.TabIndex = 211;
            // 
            // ClientAboutUsComboBox
            // 
            this.ClientAboutUsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientAboutUsComboBox.FormattingEnabled = true;
            this.ClientAboutUsComboBox.Location = new System.Drawing.Point(12, 147);
            this.ClientAboutUsComboBox.Name = "ClientAboutUsComboBox";
            this.ClientAboutUsComboBox.Size = new System.Drawing.Size(340, 24);
            this.ClientAboutUsComboBox.TabIndex = 210;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(15, 173);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(337, 13);
            this.label25.TabIndex = 215;
            this.label25.Text = "Адрес клиента";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(12, 131);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(340, 13);
            this.label26.TabIndex = 214;
            this.label26.Text = "Откуда о нас узнали";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClientPhoneTextBox
            // 
            this.ClientPhoneTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientPhoneTextBox.Location = new System.Drawing.Point(12, 109);
            this.ClientPhoneTextBox.Name = "ClientPhoneTextBox";
            this.ClientPhoneTextBox.Size = new System.Drawing.Size(340, 22);
            this.ClientPhoneTextBox.TabIndex = 209;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(12, 93);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(340, 13);
            this.label27.TabIndex = 213;
            this.label27.Text = "Телефон";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClientEditorTrue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 683);
            this.Controls.Add(this.SaveClientButton);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.BlackListComboBox);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.PrimechanieTextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.ClientFioTextBox);
            this.Controls.Add(this.ClientAdressTextBox);
            this.Controls.Add(this.ClientAboutUsComboBox);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.ClientPhoneTextBox);
            this.Controls.Add(this.label27);
            this.Name = "ClientEditorTrue";
            this.Text = "ClientEditorTrue";
            this.Load += new System.EventHandler(this.ClientEditorTrue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button SaveClientButton;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.ComboBox BlackListComboBox;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox PrimechanieTextBox;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox ClientFioTextBox;
        public System.Windows.Forms.TextBox ClientAdressTextBox;
        public System.Windows.Forms.ComboBox ClientAboutUsComboBox;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.MaskedTextBox ClientPhoneTextBox;
        public System.Windows.Forms.Label label27;
    }
}