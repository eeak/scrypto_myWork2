using System;
using System.IO;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class StockAddPosition : Form
    {
        Form1 mainForm;
        Stock stockForm;
        string photoPath = "";
        string photoPath2 = "";
        string photoPath3 = "";
        public StockAddPosition(Form1 mf, Stock st1)
        {
            InitializeComponent();
            mainForm = mf;
            stockForm = st1;
        }

        private void StockAddPosition_Load(object sender, EventArgs e)
        {

            foreach (string strCombo in TemporaryBase.SortirovkaStockUstrojstvo)
            {
                What_remont_combo_box.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockPodkategory)
            {
                PodKategoryComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockBrands)
            {
                BrandComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockDeviceColour)
            {
                ColourComboBox.Items.Add(strCombo);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Добавить " + NaimenovanieMaker() + " в склад?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdStockWrite(NaimenovanieMaker(), What_remont_combo_box.Text, PodKategoryComboBox.Text, ColourComboBox.Text, BrandComboBox.Text, ModelTextBox.Text, CountOfTextBox.Text, NapominanieTextBox.Text, PriceTextBox.Text, PrimechanieTextBox.Text, photoPath, photoPath2, photoPath3);
                photoPath = "";
                button2.Text = "Загрузить фото";
                button4.Text = "Загрузите фото 2";
                button3.Text = "Загрузите фото 3";
                stockForm.StockFullSearch();
            }
        }
        // Создаёт строку наименования складывая остальные данные через пробел
        private string NaimenovanieMaker()
        {
            if (PrimechanieTextBox.Text != "")
                return What_remont_combo_box.Text + " " + PodKategoryComboBox.Text + " " + BrandComboBox.Text + " " + ModelTextBox.Text + " " + ColourComboBox.Text + " (" + PrimechanieTextBox.Text + ")";
            else return What_remont_combo_box.Text + " " + PodKategoryComboBox.Text + " " + BrandComboBox.Text + " " + ModelTextBox.Text + " " + ColourComboBox.Text + " " + PrimechanieTextBox.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            button2.Text = openFileDialog1.FileName;
            string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
            if (File.Exists(openFileDialog1.FileName))
            {
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath = pPath;
            }
            if (button2.Text == "openFileDialog1")
            {
                button2.Text = "Не загружено";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }

        private void ResetFields_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Данное действие очистить все поля, в которых введены данные", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                PrimechanieTextBox.Text = "";
                What_remont_combo_box.Text = "";
                PodKategoryComboBox.Text = "";
                BrandComboBox.Text = "";
                ModelTextBox.Text = "";
                ColourComboBox.Text = "";
                PriceTextBox.Text = "";
                CountOfTextBox.Text = "";
                NapominanieTextBox.Text = "";
                button2.Text = "Загрузить фото";

                photoPath = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            button4.Text = openFileDialog1.FileName;
            string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
            if (File.Exists(openFileDialog1.FileName))
            {
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath2 = pPath;
            }
            if (button4.Text == "openFileDialog1")
            {
                button4.Text = "Не загружено";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            button3.Text = openFileDialog1.FileName;
            string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
            if (File.Exists(openFileDialog1.FileName))
            {
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath3 = pPath;
            }
            if (button3.Text == "openFileDialog1")
            {
                button3.Text = "Не загружено";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }
    }
}
