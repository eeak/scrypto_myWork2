namespace MyWork2
{
    partial class StockEditor
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
            this.PicturePanel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.UseZipButton = new System.Windows.Forms.Button();
            this.FIOLabel = new System.Windows.Forms.Label();
            this.CountOfZIPNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ZIPUSEPANEL = new System.Windows.Forms.Panel();
            this.CancelZIPButton = new System.Windows.Forms.Button();
            this.ZIPPANEL = new System.Windows.Forms.Panel();
            this.DeletePhotoButton3 = new System.Windows.Forms.Button();
            this.DeletePhotoButton2 = new System.Windows.Forms.Button();
            this.DeletePhotoButton1 = new System.Windows.Forms.Button();
            this.StockEditorPhotoEditButton3 = new System.Windows.Forms.Button();
            this.StockEditorPhotoEditButton2 = new System.Windows.Forms.Button();
            this.DeleteStockButton1 = new System.Windows.Forms.Button();
            this.SaveStockButton = new System.Windows.Forms.Button();
            this.StockEditorPhotoEditButton1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NapominanieTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CountOfTextBox = new System.Windows.Forms.TextBox();
            this.ModelTextBox = new System.Windows.Forms.TextBox();
            this.ColourComboBox = new System.Windows.Forms.ComboBox();
            this.BrandComboBox = new System.Windows.Forms.ComboBox();
            this.PodKategoryComboBox = new System.Windows.Forms.ComboBox();
            this.KategoryComboBox = new System.Windows.Forms.ComboBox();
            this.PrimechanieTextBox = new System.Windows.Forms.TextBox();
            this.UsedZipCounterLabel = new System.Windows.Forms.Label();
            this.PicturePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfZIPNumericUpDown)).BeginInit();
            this.ZIPUSEPANEL.SuspendLayout();
            this.ZIPPANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicturePanel1
            // 
            this.PicturePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PicturePanel1.AutoScroll = true;
            this.PicturePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicturePanel1.Controls.Add(this.pictureBox1);
            this.PicturePanel1.Location = new System.Drawing.Point(451, 11);
            this.PicturePanel1.Name = "PicturePanel1";
            this.PicturePanel1.Size = new System.Drawing.Size(482, 552);
            this.PicturePanel1.TabIndex = 49;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(474, 544);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // UseZipButton
            // 
            this.UseZipButton.Location = new System.Drawing.Point(7, 75);
            this.UseZipButton.Name = "UseZipButton";
            this.UseZipButton.Size = new System.Drawing.Size(410, 23);
            this.UseZipButton.TabIndex = 58;
            this.UseZipButton.Text = "Использовать запчасть(и)";
            this.UseZipButton.UseVisualStyleBackColor = true;
            this.UseZipButton.Click += new System.EventHandler(this.UseZipButton_Click);
            // 
            // FIOLabel
            // 
            this.FIOLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FIOLabel.Location = new System.Drawing.Point(11, 13);
            this.FIOLabel.Name = "FIOLabel";
            this.FIOLabel.Size = new System.Drawing.Size(406, 13);
            this.FIOLabel.TabIndex = 59;
            this.FIOLabel.Text = "Клиент не выбран";
            this.FIOLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CountOfZIPNumericUpDown
            // 
            this.CountOfZIPNumericUpDown.Location = new System.Drawing.Point(322, 43);
            this.CountOfZIPNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.CountOfZIPNumericUpDown.Name = "CountOfZIPNumericUpDown";
            this.CountOfZIPNumericUpDown.Size = new System.Drawing.Size(95, 20);
            this.CountOfZIPNumericUpDown.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(72, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(244, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Количество использованных запчастей";
            // 
            // ZIPUSEPANEL
            // 
            this.ZIPUSEPANEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZIPUSEPANEL.Controls.Add(this.CancelZIPButton);
            this.ZIPUSEPANEL.Controls.Add(this.FIOLabel);
            this.ZIPUSEPANEL.Controls.Add(this.label11);
            this.ZIPUSEPANEL.Controls.Add(this.CountOfZIPNumericUpDown);
            this.ZIPUSEPANEL.Controls.Add(this.UseZipButton);
            this.ZIPUSEPANEL.Location = new System.Drawing.Point(10, 348);
            this.ZIPUSEPANEL.Name = "ZIPUSEPANEL";
            this.ZIPUSEPANEL.Size = new System.Drawing.Size(432, 136);
            this.ZIPUSEPANEL.TabIndex = 62;
            // 
            // CancelZIPButton
            // 
            this.CancelZIPButton.Location = new System.Drawing.Point(7, 104);
            this.CancelZIPButton.Name = "CancelZIPButton";
            this.CancelZIPButton.Size = new System.Drawing.Size(410, 23);
            this.CancelZIPButton.TabIndex = 62;
            this.CancelZIPButton.Text = "Отменить использования запчасти данным клиентом";
            this.CancelZIPButton.UseVisualStyleBackColor = true;
            this.CancelZIPButton.Click += new System.EventHandler(this.CancelZIPButton_Click);
            // 
            // ZIPPANEL
            // 
            this.ZIPPANEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZIPPANEL.Controls.Add(this.DeletePhotoButton3);
            this.ZIPPANEL.Controls.Add(this.DeletePhotoButton2);
            this.ZIPPANEL.Controls.Add(this.DeletePhotoButton1);
            this.ZIPPANEL.Controls.Add(this.StockEditorPhotoEditButton3);
            this.ZIPPANEL.Controls.Add(this.StockEditorPhotoEditButton2);
            this.ZIPPANEL.Controls.Add(this.DeleteStockButton1);
            this.ZIPPANEL.Controls.Add(this.SaveStockButton);
            this.ZIPPANEL.Controls.Add(this.StockEditorPhotoEditButton1);
            this.ZIPPANEL.Controls.Add(this.label9);
            this.ZIPPANEL.Controls.Add(this.PriceTextBox);
            this.ZIPPANEL.Controls.Add(this.label8);
            this.ZIPPANEL.Controls.Add(this.label7);
            this.ZIPPANEL.Controls.Add(this.label6);
            this.ZIPPANEL.Controls.Add(this.label5);
            this.ZIPPANEL.Controls.Add(this.label4);
            this.ZIPPANEL.Controls.Add(this.label3);
            this.ZIPPANEL.Controls.Add(this.label2);
            this.ZIPPANEL.Controls.Add(this.NapominanieTextBox);
            this.ZIPPANEL.Controls.Add(this.label1);
            this.ZIPPANEL.Controls.Add(this.CountOfTextBox);
            this.ZIPPANEL.Controls.Add(this.ModelTextBox);
            this.ZIPPANEL.Controls.Add(this.ColourComboBox);
            this.ZIPPANEL.Controls.Add(this.BrandComboBox);
            this.ZIPPANEL.Controls.Add(this.PodKategoryComboBox);
            this.ZIPPANEL.Controls.Add(this.KategoryComboBox);
            this.ZIPPANEL.Controls.Add(this.PrimechanieTextBox);
            this.ZIPPANEL.Location = new System.Drawing.Point(10, 11);
            this.ZIPPANEL.Name = "ZIPPANEL";
            this.ZIPPANEL.Size = new System.Drawing.Size(432, 328);
            this.ZIPPANEL.TabIndex = 63;
            // 
            // DeletePhotoButton3
            // 
            this.DeletePhotoButton3.Location = new System.Drawing.Point(135, 171);
            this.DeletePhotoButton3.Name = "DeletePhotoButton3";
            this.DeletePhotoButton3.Size = new System.Drawing.Size(58, 22);
            this.DeletePhotoButton3.TabIndex = 82;
            this.DeletePhotoButton3.Text = "Удалить";
            this.DeletePhotoButton3.UseVisualStyleBackColor = true;
            this.DeletePhotoButton3.Click += new System.EventHandler(this.DeletePhotoButton3_Click);
            // 
            // DeletePhotoButton2
            // 
            this.DeletePhotoButton2.Location = new System.Drawing.Point(135, 143);
            this.DeletePhotoButton2.Name = "DeletePhotoButton2";
            this.DeletePhotoButton2.Size = new System.Drawing.Size(58, 22);
            this.DeletePhotoButton2.TabIndex = 81;
            this.DeletePhotoButton2.Text = "Удалить";
            this.DeletePhotoButton2.UseVisualStyleBackColor = true;
            this.DeletePhotoButton2.Click += new System.EventHandler(this.DeletePhotoButton2_Click);
            // 
            // DeletePhotoButton1
            // 
            this.DeletePhotoButton1.Location = new System.Drawing.Point(135, 114);
            this.DeletePhotoButton1.Name = "DeletePhotoButton1";
            this.DeletePhotoButton1.Size = new System.Drawing.Size(58, 23);
            this.DeletePhotoButton1.TabIndex = 80;
            this.DeletePhotoButton1.Text = "Удалить";
            this.DeletePhotoButton1.UseVisualStyleBackColor = true;
            this.DeletePhotoButton1.Click += new System.EventHandler(this.DeletePhotoButton1_Click);
            // 
            // StockEditorPhotoEditButton3
            // 
            this.StockEditorPhotoEditButton3.Location = new System.Drawing.Point(11, 171);
            this.StockEditorPhotoEditButton3.Name = "StockEditorPhotoEditButton3";
            this.StockEditorPhotoEditButton3.Size = new System.Drawing.Size(118, 22);
            this.StockEditorPhotoEditButton3.TabIndex = 79;
            this.StockEditorPhotoEditButton3.Text = "Загрузить фото 3";
            this.StockEditorPhotoEditButton3.UseVisualStyleBackColor = true;
            this.StockEditorPhotoEditButton3.Click += new System.EventHandler(this.button2_Click);
            // 
            // StockEditorPhotoEditButton2
            // 
            this.StockEditorPhotoEditButton2.Location = new System.Drawing.Point(11, 143);
            this.StockEditorPhotoEditButton2.Name = "StockEditorPhotoEditButton2";
            this.StockEditorPhotoEditButton2.Size = new System.Drawing.Size(118, 22);
            this.StockEditorPhotoEditButton2.TabIndex = 78;
            this.StockEditorPhotoEditButton2.Text = "Загрузить фото 2";
            this.StockEditorPhotoEditButton2.UseVisualStyleBackColor = true;
            this.StockEditorPhotoEditButton2.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeleteStockButton1
            // 
            this.DeleteStockButton1.Location = new System.Drawing.Point(9, 284);
            this.DeleteStockButton1.Name = "DeleteStockButton1";
            this.DeleteStockButton1.Size = new System.Drawing.Size(410, 24);
            this.DeleteStockButton1.TabIndex = 77;
            this.DeleteStockButton1.Text = "Удалить";
            this.DeleteStockButton1.UseVisualStyleBackColor = true;
            this.DeleteStockButton1.Click += new System.EventHandler(this.DeleteStockButton1_Click);
            // 
            // SaveStockButton
            // 
            this.SaveStockButton.Location = new System.Drawing.Point(9, 253);
            this.SaveStockButton.Name = "SaveStockButton";
            this.SaveStockButton.Size = new System.Drawing.Size(410, 25);
            this.SaveStockButton.TabIndex = 76;
            this.SaveStockButton.Text = "Сохранить";
            this.SaveStockButton.UseVisualStyleBackColor = true;
            this.SaveStockButton.Click += new System.EventHandler(this.SaveStockButton_Click);
            // 
            // StockEditorPhotoEditButton1
            // 
            this.StockEditorPhotoEditButton1.Location = new System.Drawing.Point(11, 114);
            this.StockEditorPhotoEditButton1.Name = "StockEditorPhotoEditButton1";
            this.StockEditorPhotoEditButton1.Size = new System.Drawing.Size(118, 23);
            this.StockEditorPhotoEditButton1.TabIndex = 66;
            this.StockEditorPhotoEditButton1.Text = "Загрузить фото 1";
            this.StockEditorPhotoEditButton1.UseVisualStyleBackColor = true;
            this.StockEditorPhotoEditButton1.Click += new System.EventHandler(this.StockEditorPhotoEditButton_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(55, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(210, 13);
            this.label9.TabIndex = 75;
            this.label9.Text = "Цена: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(271, 175);
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.Size = new System.Drawing.Size(148, 20);
            this.PriceTextBox.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(15, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Примечание: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(55, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 13);
            this.label7.TabIndex = 73;
            this.label7.Text = "Напомнить, если количество менее: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(55, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Количество: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(55, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "Цвет: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(55, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Модель: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(49, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Бренд: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(52, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Тип запчасти (ЗИП): ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NapominanieTextBox
            // 
            this.NapominanieTextBox.Location = new System.Drawing.Point(271, 227);
            this.NapominanieTextBox.Name = "NapominanieTextBox";
            this.NapominanieTextBox.Size = new System.Drawing.Size(148, 20);
            this.NapominanieTextBox.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(49, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Тип устройства: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CountOfTextBox
            // 
            this.CountOfTextBox.Location = new System.Drawing.Point(271, 201);
            this.CountOfTextBox.Name = "CountOfTextBox";
            this.CountOfTextBox.Size = new System.Drawing.Size(148, 20);
            this.CountOfTextBox.TabIndex = 64;
            // 
            // ModelTextBox
            // 
            this.ModelTextBox.Location = new System.Drawing.Point(271, 122);
            this.ModelTextBox.Name = "ModelTextBox";
            this.ModelTextBox.Size = new System.Drawing.Size(148, 20);
            this.ModelTextBox.TabIndex = 61;
            // 
            // ColourComboBox
            // 
            this.ColourComboBox.FormattingEnabled = true;
            this.ColourComboBox.Location = new System.Drawing.Point(271, 148);
            this.ColourComboBox.Name = "ColourComboBox";
            this.ColourComboBox.Size = new System.Drawing.Size(148, 21);
            this.ColourComboBox.TabIndex = 62;
            // 
            // BrandComboBox
            // 
            this.BrandComboBox.FormattingEnabled = true;
            this.BrandComboBox.Location = new System.Drawing.Point(271, 95);
            this.BrandComboBox.Name = "BrandComboBox";
            this.BrandComboBox.Size = new System.Drawing.Size(148, 21);
            this.BrandComboBox.TabIndex = 60;
            // 
            // PodKategoryComboBox
            // 
            this.PodKategoryComboBox.FormattingEnabled = true;
            this.PodKategoryComboBox.Location = new System.Drawing.Point(271, 68);
            this.PodKategoryComboBox.Name = "PodKategoryComboBox";
            this.PodKategoryComboBox.Size = new System.Drawing.Size(148, 21);
            this.PodKategoryComboBox.TabIndex = 59;
            // 
            // KategoryComboBox
            // 
            this.KategoryComboBox.FormattingEnabled = true;
            this.KategoryComboBox.Location = new System.Drawing.Point(271, 41);
            this.KategoryComboBox.Name = "KategoryComboBox";
            this.KategoryComboBox.Size = new System.Drawing.Size(148, 21);
            this.KategoryComboBox.TabIndex = 58;
            // 
            // PrimechanieTextBox
            // 
            this.PrimechanieTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.PrimechanieTextBox.Location = new System.Drawing.Point(124, 12);
            this.PrimechanieTextBox.Name = "PrimechanieTextBox";
            this.PrimechanieTextBox.Size = new System.Drawing.Size(295, 20);
            this.PrimechanieTextBox.TabIndex = 57;
            // 
            // UsedZipCounterLabel
            // 
            this.UsedZipCounterLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsedZipCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UsedZipCounterLabel.Location = new System.Drawing.Point(10, 494);
            this.UsedZipCounterLabel.Name = "UsedZipCounterLabel";
            this.UsedZipCounterLabel.Size = new System.Drawing.Size(432, 69);
            this.UsedZipCounterLabel.TabIndex = 64;
            this.UsedZipCounterLabel.Text = "Чтобы узнать, используются ли запчасти клиентом, выберите клиента. \r\n(Чтобы выбра" +
    "ть клиента, нужно зайти в склад, через редактирование записей, кнопочка рядом с " +
    "полем затраты";
            this.UsedZipCounterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StockEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 568);
            this.Controls.Add(this.UsedZipCounterLabel);
            this.Controls.Add(this.ZIPPANEL);
            this.Controls.Add(this.ZIPUSEPANEL);
            this.Controls.Add(this.PicturePanel1);
            this.Name = "StockEditor";
            this.Text = "StockEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StockEditor_FormClosed);
            this.Load += new System.EventHandler(this.StockEditor_Load);
            this.PicturePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfZIPNumericUpDown)).EndInit();
            this.ZIPUSEPANEL.ResumeLayout(false);
            this.ZIPUSEPANEL.PerformLayout();
            this.ZIPPANEL.ResumeLayout(false);
            this.ZIPPANEL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel PicturePanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button UseZipButton;
        private System.Windows.Forms.Label FIOLabel;
        private System.Windows.Forms.NumericUpDown CountOfZIPNumericUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel ZIPUSEPANEL;
        private System.Windows.Forms.Panel ZIPPANEL;
        private System.Windows.Forms.Button DeletePhotoButton3;
        private System.Windows.Forms.Button DeletePhotoButton2;
        private System.Windows.Forms.Button DeletePhotoButton1;
        private System.Windows.Forms.Button StockEditorPhotoEditButton3;
        private System.Windows.Forms.Button StockEditorPhotoEditButton2;
        private System.Windows.Forms.Button DeleteStockButton1;
        private System.Windows.Forms.Button SaveStockButton;
        private System.Windows.Forms.Button StockEditorPhotoEditButton1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NapominanieTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CountOfTextBox;
        private System.Windows.Forms.TextBox ModelTextBox;
        private System.Windows.Forms.ComboBox ColourComboBox;
        private System.Windows.Forms.ComboBox BrandComboBox;
        private System.Windows.Forms.ComboBox PodKategoryComboBox;
        private System.Windows.Forms.ComboBox KategoryComboBox;
        private System.Windows.Forms.TextBox PrimechanieTextBox;
        private System.Windows.Forms.Button CancelZIPButton;
        private System.Windows.Forms.Label UsedZipCounterLabel;
    }
}