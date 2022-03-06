namespace MyWork2
{
    partial class BaseLineNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseLineNumber));
            this.label1 = new System.Windows.Forms.Label();
            this.IncrementValueUpDown = new System.Windows.Forms.NumericUpDown();
            this.NextButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.IncrementValueUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 131);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IncrementValueUpDown
            // 
            this.IncrementValueUpDown.Location = new System.Drawing.Point(15, 163);
            this.IncrementValueUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.IncrementValueUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IncrementValueUpDown.Name = "IncrementValueUpDown";
            this.IncrementValueUpDown.Size = new System.Drawing.Size(214, 20);
            this.IncrementValueUpDown.TabIndex = 1;
            this.IncrementValueUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(235, 161);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(210, 23);
            this.NextButton.TabIndex = 2;
            this.NextButton.Text = "Далее";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BaseLineNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 204);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.IncrementValueUpDown);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BaseLineNumber";
            this.Text = "BaseLineNumber";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaseLineNumber_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.IncrementValueUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown IncrementValueUpDown;
        private System.Windows.Forms.Button NextButton;
    }
}