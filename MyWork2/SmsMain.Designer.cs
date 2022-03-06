namespace MyWork2
{
    partial class SmsMain
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
            this.SmsReadyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SmsSoglasovanTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SmsShablonTextBox = new System.Windows.Forms.TextBox();
            this.infoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SmsPrivetTextBox = new System.Windows.Forms.TextBox();
            this.SmsRassilkaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SmsReadyTextBox
            // 
            this.SmsReadyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsReadyTextBox.Location = new System.Drawing.Point(12, 14);
            this.SmsReadyTextBox.Multiline = true;
            this.SmsReadyTextBox.Name = "SmsReadyTextBox";
            this.SmsReadyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SmsReadyTextBox.Size = new System.Drawing.Size(683, 141);
            this.SmsReadyTextBox.TabIndex = 2;
            this.SmsReadyTextBox.Text = "Здравствуйте {ФИО}, Ваш {ТИП} {БРЕНД} {МОДЕЛЬ} готов. Стоимость ремонта {ЦЕНА}, с" +
    " учётом скидки {СКИДКА}, можете забрать в любое удобное для Вас время";
            this.SmsReadyTextBox.TextChanged += new System.EventHandler(this.SmsReadyTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Смс по Готовности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Смс на согласование с клиентом";
            // 
            // SmsSoglasovanTextBox
            // 
            this.SmsSoglasovanTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsSoglasovanTextBox.Location = new System.Drawing.Point(12, 170);
            this.SmsSoglasovanTextBox.Multiline = true;
            this.SmsSoglasovanTextBox.Name = "SmsSoglasovanTextBox";
            this.SmsSoglasovanTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SmsSoglasovanTextBox.Size = new System.Drawing.Size(683, 141);
            this.SmsSoglasovanTextBox.TabIndex = 4;
            this.SmsSoglasovanTextBox.TextChanged += new System.EventHandler(this.SmsSoglasovanTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Свой шаблон смс";
            // 
            // SmsShablonTextBox
            // 
            this.SmsShablonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsShablonTextBox.Location = new System.Drawing.Point(11, 325);
            this.SmsShablonTextBox.Multiline = true;
            this.SmsShablonTextBox.Name = "SmsShablonTextBox";
            this.SmsShablonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SmsShablonTextBox.Size = new System.Drawing.Size(683, 141);
            this.SmsShablonTextBox.TabIndex = 6;
            this.SmsShablonTextBox.TextChanged += new System.EventHandler(this.SmsShablonTextBox_TextChanged);
            // 
            // infoButton
            // 
            this.infoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoButton.Location = new System.Drawing.Point(366, 629);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(332, 23);
            this.infoButton.TabIndex = 8;
            this.infoButton.Text = "Информация";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(9, 468);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Приветственное смс, при сдаче техники в сервис";
            // 
            // SmsPrivetTextBox
            // 
            this.SmsPrivetTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsPrivetTextBox.Location = new System.Drawing.Point(12, 482);
            this.SmsPrivetTextBox.Multiline = true;
            this.SmsPrivetTextBox.Name = "SmsPrivetTextBox";
            this.SmsPrivetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SmsPrivetTextBox.Size = new System.Drawing.Size(683, 141);
            this.SmsPrivetTextBox.TabIndex = 9;
            // 
            // SmsRassilkaButton
            // 
            this.SmsRassilkaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SmsRassilkaButton.Location = new System.Drawing.Point(11, 629);
            this.SmsRassilkaButton.Name = "SmsRassilkaButton";
            this.SmsRassilkaButton.Size = new System.Drawing.Size(332, 23);
            this.SmsRassilkaButton.TabIndex = 11;
            this.SmsRassilkaButton.Text = "Смс Рассылка";
            this.SmsRassilkaButton.UseVisualStyleBackColor = true;
            this.SmsRassilkaButton.Click += new System.EventHandler(this.SmsRassilkaButton_Click);
            // 
            // SmsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(707, 656);
            this.Controls.Add(this.SmsRassilkaButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SmsPrivetTextBox);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SmsShablonTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SmsSoglasovanTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SmsReadyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SmsMain";
            this.Text = "SmsMain";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SmsMain_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmsMain_FormClosing);
            this.Load += new System.EventHandler(this.SmsMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SmsReadyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SmsSoglasovanTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SmsShablonTextBox;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SmsPrivetTextBox;
        private System.Windows.Forms.Button SmsRassilkaButton;
    }
}