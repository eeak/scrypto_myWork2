using System;
using System.IO;
using System.Windows.Forms;
/*
Редактирование актов
Редактирование происходит по обоим актам сразу (данные о фирме, номер и т.п.)
Подгружается только содержимое тегов id которых без цифры 1, содержимое тех, что с цифрой 1, должно быть идентичным своему собрату без цифры.
Замена происходить через String.Replace(), поэтому менятся содержимое, как тегов с цифрой, так и без. 
*/
namespace MyWork2
{
    public partial class RedaktorAktov : Form
    {
        Form1 mainForm;
        HtmlWorker htmw1 = new HtmlWorker();
        HtmlWorker htmw2 = new HtmlWorker();
        public RedaktorAktov(Form1 fm1)
        {
            mainForm = fm1;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Сохранить все изменения в актах выдачи и приёма?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    File.WriteAllText("Settings/Akts/FirmName.txt", FirmNameTextBox.Text); // FIRMNAMEPRINT
                    File.WriteAllText("Settings/Akts/Phone.txt", PhoneNumberTextBox.Text); // FIRMTELPRINT
                    File.WriteAllText("Settings/Akts/DannieOFirme.txt", DannieOFirmeTextBox.Text); // DANNIEOFIRMEPRINT
                    File.WriteAllText("Settings/Akts/URDannie.txt", DannieURLitsaTextBox.Text); // URDANNIEPRINT
                    File.WriteAllText("Settings/Akts/DogovorTextPriem.txt", RulesAktPriema.Text); // DOGOVORPRIEMPRINT
                    File.WriteAllText("Settings/Akts/DogovorTextVidacha.txt", RulesAktVidachi.Text); //DOGOVORVIDACHAPRINT


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void RedaktorAktov_Load(object sender, EventArgs e)
        {
            try
            {
                FirmNameTextBox.Text = File.ReadAllText("Settings/Akts/FirmName.txt");
                PhoneNumberTextBox.Text = File.ReadAllText("Settings/Akts/Phone.txt");
                DannieOFirmeTextBox.Text = File.ReadAllText("Settings/Akts/DannieOFirme.txt");
                DannieURLitsaTextBox.Text = File.ReadAllText("Settings/Akts/URDannie.txt");
                RulesAktPriema.Text = File.ReadAllText("Settings/Akts/DogovorTextPriem.txt");
                RulesAktVidachi.Text = File.ReadAllText("Settings/Akts/DogovorTextVidacha.txt");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void RulesAktPriema_TextChanged(object sender, EventArgs e)
        {

        }

        private void AktPiemaButton_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                if (mainForm.basa.BdReadAdvertsDataTop() > 0)
                {
                    Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(mainForm.basa.BdReadAdvertsDataTop().ToString()), mainForm, TemporaryBase.valuta);
                    actPriema1.Show();
                }
                else
                {
                    MessageBox.Show("Нужна хотябы одна запись в базе, чтобы посмотреть акты");
                }

            }

            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");
        }

        private void AktVidachiButton_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                if (mainForm.basa.BdReadAdvertsDataTop() > 0)
                {
                    Printing_AKT_VIDACHI actPriema1 = new Printing_AKT_VIDACHI(mainForm.basa.BdReadOneEditor(mainForm.basa.BdReadAdvertsDataTop().ToString()), mainForm, TemporaryBase.valuta);
                    actPriema1.Show();
                }
                else
                {
                    MessageBox.Show("Нужна хотябы одна запись в базе, чтобы посмотреть акты");
                }

            }

            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");
        }
    }
}
