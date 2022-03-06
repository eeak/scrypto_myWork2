using System;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class SURPRISE : Form
    {
        Form1 mainForm;
        public SURPRISE(Form1 fm)
        {
            mainForm = fm;
            InitializeComponent();
            foreach (string strCombo in TemporaryBase.SortirovkaVipolnRaboti)
            {
                WhatToRenameServiceAdressComboBox.Items.Add(strCombo);
            }

            foreach (string strCombo in TemporaryBase.SortirovkaAdressSc)
            {
                ServiceAdressComboBox.Items.Add(strCombo);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNull("Image_key");
        }

        private void Adress_NoNull_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNull("Adress");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNull("wait_zakaz");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNull("AdressSC");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNull("DeviceColour");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNullRename("AdressSC", ServiceAdressComboBox.Text, WhatToRenameServiceAdressComboBox.Text);
        }


        private void SURPRISE_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainForm.basa.BdNoNullRename("Status_remonta", "Ждёт запчасть", "Ждет ЗИП");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mainForm.basa.bdBarcodeAllGenerator();
        }
    }
}
