namespace MyWork2
{
    partial class SmsRassilka
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
            this.label1 = new System.Windows.Forms.Label();
            this.SmsReadyTextBox = new System.Windows.Forms.TextBox();
            this.BrandComboBox = new System.Windows.Forms.ComboBox();
            this.What_remont_combo_box = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ServiceAdressComboBox = new System.Windows.Forms.ComboBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.VipRabotTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valCounter = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(-1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Текст сообщения для рассылки";
            // 
            // SmsReadyTextBox
            // 
            this.SmsReadyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsReadyTextBox.Location = new System.Drawing.Point(2, 18);
            this.SmsReadyTextBox.Multiline = true;
            this.SmsReadyTextBox.Name = "SmsReadyTextBox";
            this.SmsReadyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SmsReadyTextBox.Size = new System.Drawing.Size(390, 191);
            this.SmsReadyTextBox.TabIndex = 4;
            // 
            // BrandComboBox
            // 
            this.BrandComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "4PARTS",
            "ACER",
            "ADVOCAM",
            "AMD",
            "APACER",
            "APPLE",
            "ASUS",
            "ASX",
            "BBK",
            "BEELINE",
            "BEKO",
            "BENQ",
            "BLACKBERRY",
            "BLACKVIEW",
            "BQ",
            "COMPAQ",
            "COOLER MASTER",
            "CREATIVE",
            "DAEWOO",
            "DELL",
            "DEXP",
            "DIGMA",
            "DNS",
            "ELENBERG",
            "EMACHINES",
            "EPLUTUS",
            "EPSON",
            "EPSON",
            "ETULINE",
            "EXPLAY",
            "FLY",
            "GARMIN",
            "GIGABYTE",
            "HAIER",
            "HEWLETT PACKARD",
            "HIGHSCREEN",
            "HTC",
            "HUAWEI",
            "ICONBIT",
            "IMPRESSION",
            "INTEGO",
            "INVIN",
            "INWIN",
            "IRBIS",
            "IRU",
            "IRULU",
            "JINGA",
            "JVC",
            "LENOVO",
            "LENTEL",
            "LG",
            "MAXVI",
            "MEGAFON",
            "MERSEDES-BENZ",
            "MI",
            "MICROLAB",
            "MICROMAX",
            "MICROSOFT",
            "MSI",
            "MYSTERY",
            "NOKIA",
            "ORIEL",
            "OUSTERS",
            "PACKARD BELL",
            "PANASONIC",
            "PHILIPS",
            "POLAR",
            "PRESTIGIO",
            "QUMO",
            "REKAM",
            "RITMIX",
            "SAMSUNG",
            "SATELLITE",
            "SC",
            "SHARP+Environment.NewLineDELTA",
            "SIEMENS",
            "SILICON POWER",
            "SMARTBUY",
            "SONY",
            "STARK",
            "STRIKE",
            "SUPRA",
            "TESLA",
            "TESLER",
            "TEXET",
            "TEXET",
            "THOMPSON",
            "TOSHIBA",
            "TREELOGIC",
            "TURBOPAD",
            "TWOCHI",
            "UNITED",
            "VALEO",
            "WEXLER",
            "XEROX",
            "XIAOMI",
            "YOTAPHONE",
            "ZIFRO",
            "ZTE",
            "БЕЗ МАРКИ",
            "БИЛАЙН",
            "КЕЙ",
            "МТС",
            "СПЛАЙН"});
            this.BrandComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.BrandComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BrandComboBox.FormattingEnabled = true;
            this.BrandComboBox.Location = new System.Drawing.Point(228, 341);
            this.BrandComboBox.Name = "BrandComboBox";
            this.BrandComboBox.Size = new System.Drawing.Size(164, 21);
            this.BrandComboBox.TabIndex = 149;
            // 
            // What_remont_combo_box
            // 
            this.What_remont_combo_box.AutoCompleteCustomSource.AddRange(new string[] {
            "DVD ПРИСТАВКА",
            "FLASH НАКОПИТЕЛЬ",
            "MACBOOK",
            "MP3 ПЛЕЕР",
            "TV",
            "АВТОМАГНИТОЛА",
            "АККУМУЛЯТОРНАЯ БАТАРЕЯ",
            "БЛОК ПИТАНИЯ",
            "ВИДЕОКАРТА",
            "ВИДЕОРЕГИСТРАТОР",
            "ДЖОЙСТИК",
            "ДЫМОВАЯ МАШИНА",
            "ЖЕСТКИЙ ДИСК",
            "ЗАРЯДНОЕ УСТРОЙСТВО",
            "ИГРОВАЯ КОНСОЛЬ",
            "МАГНИТОФОН",
            "МИКРОВОЛНОВАЯ ПЕЧЬ",
            "МОНИТОР",
            "МОНИТОР",
            "МОНОБЛОК",
            "МУЗЫКАЛЬНЫЙ ЦЕНТР",
            "МФУ",
            "НАВИГАТОР",
            "НАУШНИКИ",
            "НЕТБУК",
            "НЕТБУК",
            "НОУТБУК",
            "ПЛАНШЕТ",
            "ПЛАТА",
            "ПОРТАТИВНАЯ КОЛОНКА",
            "ПРИНТЕР",
            "ПРОЕКТОР",
            "РАДИОПРИЕМНИК",
            "РАДИОУПРАВЛЯЕМЫЙ ВЕРТОЛЕТ",
            "РОУТЕР",
            "СИСТЕМНЫЙ БЛОК",
            "ТЕЛЕВИЗЕОННАЯ ПРИСТАВКА",
            "ТЕЛЕФОН",
            "УСИЛИТЕЛЬ",
            "ФОТОРАМКА",
            "ЭЛЕКТРОННАЯ КНИГА"});
            this.What_remont_combo_box.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.What_remont_combo_box.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.What_remont_combo_box.FormattingEnabled = true;
            this.What_remont_combo_box.Location = new System.Drawing.Point(228, 284);
            this.What_remont_combo_box.Name = "What_remont_combo_box";
            this.What_remont_combo_box.Size = new System.Drawing.Size(164, 21);
            this.What_remont_combo_box.TabIndex = 148;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(271, 268);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 151;
            this.label14.Text = "Тип устройства";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(261, 314);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 13);
            this.label15.TabIndex = 150;
            this.label15.Text = "Название бренда";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 268);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 147;
            this.label12.Text = "Адресс СЦ";
            // 
            // ServiceAdressComboBox
            // 
            this.ServiceAdressComboBox.FormattingEnabled = true;
            this.ServiceAdressComboBox.Items.AddRange(new object[] {
            ""});
            this.ServiceAdressComboBox.Location = new System.Drawing.Point(2, 284);
            this.ServiceAdressComboBox.Name = "ServiceAdressComboBox";
            this.ServiceAdressComboBox.Size = new System.Drawing.Size(164, 21);
            this.ServiceAdressComboBox.TabIndex = 146;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(229, 245);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(164, 20);
            this.dateTimePicker2.TabIndex = 145;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(2, 245);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(164, 20);
            this.dateTimePicker1.TabIndex = 144;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(1, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(392, 25);
            this.button1.TabIndex = 143;
            this.button1.Text = "Посчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 140;
            this.label3.Text = "по";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 139;
            this.label2.Text = "с";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 138;
            this.label5.Text = "Выберите период:";
            // 
            // VipRabotTextBox
            // 
            this.VipRabotTextBox.Location = new System.Drawing.Point(2, 342);
            this.VipRabotTextBox.Name = "VipRabotTextBox";
            this.VipRabotTextBox.Size = new System.Drawing.Size(164, 20);
            this.VipRabotTextBox.TabIndex = 152;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 31);
            this.label4.TabIndex = 153;
            this.label4.Text = "Поле выполненных работ содержит фразу";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(398, 18);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(440, 344);
            this.listView1.TabIndex = 154;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ФИО";
            this.columnHeader1.Width = 183;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Номер";
            this.columnHeader2.Width = 181;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Выполненные работы";
            this.columnHeader3.Width = 159;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Тип устройства";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Бренд";
            this.columnHeader5.Width = 100;
            // 
            // valCounter
            // 
            this.valCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.valCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.valCounter.Location = new System.Drawing.Point(581, 371);
            this.valCounter.Name = "valCounter";
            this.valCounter.Size = new System.Drawing.Size(257, 23);
            this.valCounter.TabIndex = 155;
            this.valCounter.Text = "Количество подходщих записей: ";
            this.valCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(398, 369);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 156;
            this.button2.Text = "Разослать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(2, 399);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(836, 65);
            this.textBox1.TabIndex = 157;
            // 
            // SmsRassilka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 467);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.valCounter);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.VipRabotTextBox);
            this.Controls.Add(this.BrandComboBox);
            this.Controls.Add(this.What_remont_combo_box);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ServiceAdressComboBox);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SmsReadyTextBox);
            this.Name = "SmsRassilka";
            this.Text = "SmsRassilka";
            this.Load += new System.EventHandler(this.SmsRassilka_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SmsReadyTextBox;
        public System.Windows.Forms.ComboBox BrandComboBox;
        public System.Windows.Forms.ComboBox What_remont_combo_box;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox ServiceAdressComboBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox VipRabotTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label valCounter;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
    }
}