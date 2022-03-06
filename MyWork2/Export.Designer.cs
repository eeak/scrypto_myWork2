namespace MyWork2
{
    partial class Export
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
            this.ExportToAndroidButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fromNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.toNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ExportToAppleButton = new System.Windows.Forms.Button();
            this.AllExportNumbersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fromNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ExportToAndroidButton
            // 
            this.ExportToAndroidButton.Location = new System.Drawing.Point(39, 168);
            this.ExportToAndroidButton.Name = "ExportToAndroidButton";
            this.ExportToAndroidButton.Size = new System.Drawing.Size(417, 23);
            this.ExportToAndroidButton.TabIndex = 0;
            this.ExportToAndroidButton.Text = "Для переноса в csv формат";
            this.ExportToAndroidButton.UseVisualStyleBackColor = true;
            this.ExportToAndroidButton.Click += new System.EventHandler(this.ExportToAndroidButton_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(486, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Пока еще вы не экспортировали номера телефонов";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Экспортировать телефонные номера клиентов с \r\n";
            // 
            // fromNumericUpDown
            // 
            this.fromNumericUpDown.Location = new System.Drawing.Point(301, 64);
            this.fromNumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.fromNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fromNumericUpDown.Name = "fromNumericUpDown";
            this.fromNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.fromNumericUpDown.TabIndex = 3;
            this.fromNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "по";
            // 
            // toNumericUpDown
            // 
            this.toNumericUpDown.Location = new System.Drawing.Point(301, 93);
            this.toNumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.toNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.toNumericUpDown.Name = "toNumericUpDown";
            this.toNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.toNumericUpDown.TabIndex = 5;
            this.toNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ExportToAppleButton
            // 
            this.ExportToAppleButton.Location = new System.Drawing.Point(39, 139);
            this.ExportToAppleButton.Name = "ExportToAppleButton";
            this.ExportToAppleButton.Size = new System.Drawing.Size(417, 23);
            this.ExportToAppleButton.TabIndex = 6;
            this.ExportToAppleButton.Text = "Для переноса в Android/Iphone/Vcard";
            this.ExportToAppleButton.UseVisualStyleBackColor = true;
            this.ExportToAppleButton.Click += new System.EventHandler(this.ExportToAppleButton_Click);
            // 
            // AllExportNumbersButton
            // 
            this.AllExportNumbersButton.Location = new System.Drawing.Point(427, 63);
            this.AllExportNumbersButton.Name = "AllExportNumbersButton";
            this.AllExportNumbersButton.Size = new System.Drawing.Size(36, 51);
            this.AllExportNumbersButton.TabIndex = 7;
            this.AllExportNumbersButton.Text = "Все";
            this.AllExportNumbersButton.UseVisualStyleBackColor = true;
            this.AllExportNumbersButton.Click += new System.EventHandler(this.AllExportNumbersButton_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 203);
            this.Controls.Add(this.AllExportNumbersButton);
            this.Controls.Add(this.ExportToAppleButton);
            this.Controls.Add(this.toNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fromNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExportToAndroidButton);
            this.Name = "Export";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.Export_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fromNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportToAndroidButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown fromNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown toNumericUpDown;
        private System.Windows.Forms.Button ExportToAppleButton;
        private System.Windows.Forms.Button AllExportNumbersButton;
    }
}