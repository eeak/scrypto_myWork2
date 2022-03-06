using System;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class BaseLineNumber : Form
    {
        Form1 mainForm;
        public BaseLineNumber(Form1 mf)
        {
            mainForm = mf;
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BaseLineNumber_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Хер знает зачем эта функция, но скорее всего выдают последнюю запись 
            if (IncrementValueUpDown.Value != 1)
            {
                mainForm.basa.BdWrite(DateTime.Now.ToShortDateString(), "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "");
                string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                mainForm.basa.BdDelete(topBaseZapis);
                mainForm.basa.CreateBd((IncrementValueUpDown.Value - 1).ToString());
            }

        }
    }
}
