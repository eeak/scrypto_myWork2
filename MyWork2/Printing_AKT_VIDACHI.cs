using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace MyWork2
{
    public partial class Printing_AKT_VIDACHI : Form
    {
        Form1 mainForm;
        string id_bd;
        string valutaMain = "";
        public Printing_AKT_VIDACHI(DataTable dtabe, Form1 mf, string valuta)
        {
            valutaMain = valuta;
            InitializeComponent();
            mainForm = mf;
            DataTable dTable = dtabe;
            if (dTable.Rows.Count > 0)
            {
                //Подгружаем данные о клиенте
                DataTable dt2 = mainForm.basa.ClientsMapGiver(dTable.Rows[0].ItemArray[29].ToString());

                int i = 0;
                string str = File.ReadAllText(@"Shablon_Act_vidachi.htm");
                str = str.Replace("NUMZAKAZPRINT", dTable.Rows[i].ItemArray[0].ToString());
                id_bd = dTable.Rows[i].ItemArray[0].ToString();
                //Штрихкод
                if (dTable.Rows[i].ItemArray[30].ToString().Length == 12)
                    BarcodeMacker(dTable.Rows[i].ItemArray[30].ToString());
                str = str.Replace("DATAPRIEMAPRINT", dTable.Rows[i].ItemArray[1].ToString());
                str = str.Replace("FIOPRINT", FirstLetterToUpper(dt2.Rows[0].ItemArray[1].ToString()));
                //Преобразовываем номер телефона в читабельный формат
                string phoneNumber = dt2.Rows[0].ItemArray[2].ToString();

                //Ставим пробелы в телефонный номер при вводе
                if (phoneNumber.Length == 5)
                {
                    phoneNumber = phoneNumber.Insert(2, " ");
                }

                else if (phoneNumber.Length == 6)
                {
                    phoneNumber = phoneNumber.Insert(3, " ");
                }

                else if (phoneNumber.Length == 7)
                {
                    phoneNumber = phoneNumber.Insert(3, " ");
                    phoneNumber = phoneNumber.Insert(6, " ");
                }
                else if (phoneNumber.Length > 9)
                {
                    phoneNumber = phoneNumber.Insert(1, " ");
                    phoneNumber = phoneNumber.Insert(5, " ");
                    phoneNumber = phoneNumber.Insert(9, " ");
                }

                str = str.Replace("PHONEPRINT", phoneNumber);
                string typeustr = "";
                typeustr = dTable.Rows[i].ItemArray[7].ToString() + " ";
                typeustr += dTable.Rows[i].ItemArray[8].ToString() + " ";
                str = str.Replace("TYPEUSTRPRINT", FirstLetterToUpper(typeustr) + " " + dTable.Rows[i].ItemArray[9].ToString());
                str = str.Replace("SERIALNPIRNT", dTable.Rows[i].ItemArray[10].ToString());
                str = str.Replace("KOMPLEKTNOSTPRINT", dTable.Rows[i].ItemArray[12].ToString());
                str = str.Replace("VNESHVIPRINT", dTable.Rows[i].ItemArray[11].ToString());
                str = str.Replace("NEISPRAVNOSTPRINT", dTable.Rows[i].ItemArray[13].ToString());
                str = str.Replace("DATAVIDACHIPRINT", dTable.Rows[i].ItemArray[2].ToString());
                str = str.Replace("GARANTYPRINT", dTable.Rows[i].ItemArray[23].ToString());
                str = str.Replace("VIPRABOTPRINT", dTable.Rows[i].ItemArray[22].ToString());
                int fullPrice = 0;
                //трайкятч нужен, чтобы в редакторе актов не возникало ошибок при пробной печати
                try
                {
                    if (decimal.Parse(dTable.Rows[i].ItemArray[19].ToString()) == 0)
                        str = str.Replace("SKIDKAPRINT", "");
                    else
                        str = str.Replace("SKIDKAPRINT", " Скидка: " + dTable.Rows[i].ItemArray[19].ToString() + " " + valutaMain);
                    //Мудотня с выводом цены и предоплаты
                    fullPrice = int.Parse(dTable.Rows[i].ItemArray[18].ToString()) - int.Parse(dTable.Rows[i].ItemArray[16].ToString());
                    str = str.Replace("PRICEFULLPRINT", string.Format(" Сумма предоплаты: {1}<br>К оплате: {0}" + " " + valutaMain, fullPrice.ToString(), decimal.Parse(dTable.Rows[i].ItemArray[16].ToString())));

                }
                catch
                {

                }
                str = str.Replace("FIRMNAMEPRINT", File.ReadAllText("Settings/Akts/FirmName.txt"));
                str = str.Replace("FIRMTELPRINT", File.ReadAllText("Settings/Akts/Phone.txt"));
                str = str.Replace("DANNIEOFIRMEPRINT", File.ReadAllText("Settings/Akts/DannieOFirme.txt"));
                str = str.Replace("URDANNIEPRINT", File.ReadAllText("Settings/Akts/URDannie.txt"));

                str = str.Replace("DOGOVORVIDACHAPRINT", File.ReadAllText("Settings/Akts/DogovorTextVidacha.txt"));
                File.WriteAllText("Act_vidachi", str);
                webBrowser1.Navigate(Application.StartupPath + @"\Act_vidachi");

                //  NomerZakazaLabel.Text = dTable.Rows[i].ItemArray[0].ToString();
                //  DataLabel.Text = dTable.Rows[i].ItemArray[1].ToString();
                // data_vidachi_bd = dTable.Rows[i].ItemArray[2].ToString();
                //   data_predoplaty_bd = dTable.Rows[i].ItemArray[3].ToString();
                //  FIOLabel.Text= dTable.Rows[i].ItemArray[4].ToString();
                //  PhoneLabel.Text = dTable.Rows[i].ItemArray[5].ToString();
                //  AboutUsComboBox.Text = dTable.Rows[i].ItemArray[6].ToString();
                //   TipUstrojstvaLabel.Text= dTable.Rows[i].ItemArray[7].ToString()+" ";
                //    TipUstrojstvaLabel.Text += dTable.Rows[i].ItemArray[8].ToString()+" ";
                //   TipUstrojstvaLabel.Text += dTable.Rows[i].ItemArray[9].ToString()+" ";
                //    SerialNLabel.Text = dTable.Rows[i].ItemArray[10].ToString();
                //    VidLabel.Text = dTable.Rows[i].ItemArray[11].ToString();
                //    KomplektonstLabel.Text = dTable.Rows[i].ItemArray[12].ToString();
                //   PolomkaLabel.Text = dTable.Rows[i].ItemArray[13].ToString();
                //  kommentarijTextBox.Text = dTable.Rows[i].ItemArray[14].ToString();
                //  PredvaritelnayaStoimostTextBox.Text = dTable.Rows[i].ItemArray[15].ToString();
                //  PredoplataTextBox.Text = dTable.Rows[i].ItemArray[16].ToString();
                //  ZatratyTextBox.Text = dTable.Rows[i].ItemArray[17].ToString();
                //  PriceTextBox.Text = dTable.Rows[i].ItemArray[18].ToString();
                //   MoneyTextBox.Text = dTable.Rows[i].ItemArray[19].ToString();
                //   MoneySkidka = decimal.Parse(dTable.Rows[i].ItemArray[19].ToString());
                //   StatusComboBox.Text = dTable.Rows[i].ItemArray[20].ToString();


                //    MasterComboBox.Text = dTable.Rows[i].ItemArray[21].ToString();
                //     VipolnenieRabotiComboBox.Text = "";
                //     VipolnenieRabotiTextBox.Text = dTable.Rows[i].ItemArray[22].ToString();
                //      GarantyComboBox.Text = dTable.Rows[i].ItemArray[23].ToString();


            }

        }

        //BarcodeMaker
        private void BarcodeMacker(string id_base)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeLib.TYPE type = BarcodeLib.TYPE.UPCA;

            // b.IncludeLabel = true;
            // b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
            // barcode.BackgroundImage = 
            b.Encode(type, id_base, Color.Black, Color.White, TemporaryBase.barcodeW, TemporaryBase.barcodeH);
            //   barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);

            //Сохранение картинки
            BarcodeLib.SaveTypes savetype = BarcodeLib.SaveTypes.GIF;
            b.SaveImage("barcode.gif", savetype);
        }
        private void PrintButton_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\PageSetup", "header", "");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\PageSetup", "footer", "");

            webBrowser1.ShowPrintDialog();
            mainForm.StatusStripLabel.Text = "Печать акта выдачи " + id_bd;

        }
        //Функция заглавной буквы в начале каждого слова
        string FirstLetterToUpper(string krolik)
        {
            string lookup = " \r\n\t";
            var sb = new StringBuilder(krolik.ToLower());

            if (sb.Length > 0 && char.IsLetter(sb[0]))
                sb[0] = char.ToUpper(sb[0]);

            for (int z = 1; z < sb.Length; z++)
            {
                char ch = sb[z];
                if (lookup.Contains(sb[z - 1]) && char.IsLetter(ch))
                    sb[z] = char.ToUpper(ch);
            }
            return sb.ToString();
        }
        private void ReAktor_Click(object sender, EventArgs e)
        {
            RedaktorAktov ReAkt1 = new RedaktorAktov(mainForm);
            ReAkt1.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPageSetupDialog();
            //      PrintDocument PD = new PrintDocument();
            //   string PrinterName = "Canon MF2540";
            //  PrinterSettings PS = new PrinterSettings();
            //    PS.PrinterName = PrinterName;
            //   PD.PrinterSettings = PS;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\PageSetup", "header", "");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\PageSetup", "footer", "");

            webBrowser1.ShowPrintPreviewDialog();
            mainForm.StatusStripLabel.Text = "Печать акта выдачи " + id_bd;
        }
    }
}
