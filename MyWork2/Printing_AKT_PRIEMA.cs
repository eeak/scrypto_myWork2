using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Printing_AKT_PRIEMA : Form
    {
        Form1 mainForm;
        string id_bd;
        string valutaMain = "";
        DataTable dTable = new DataTable();
        DataTable dt2 = new DataTable();
        private string MonthInText(string month)
        {
            if (decimal.Parse(month) == 1)
            {
                return " Января ";
            }
            else if (decimal.Parse(month) == 2)
            {
                return " Февраля ";
            }
            else if (decimal.Parse(month) == 3)
            {
                return " Марта ";
            }
            else if (decimal.Parse(month) == 4)
            {
                return " Апреля ";
            }
            else if (decimal.Parse(month) == 5)
            {
                return " Мая ";
            }
            else if (decimal.Parse(month) == 6)
            {
                return " Июня ";
            }
            else if (decimal.Parse(month) == 7)
            {
                return " Июля ";
            }
            else if (decimal.Parse(month) == 8)
            {
                return " Августа ";
            }
            else if (decimal.Parse(month) == 9)
            {
                return " Сентября ";
            }
            else if (decimal.Parse(month) == 10)
            {
                return " Октября ";
            }
            else if (decimal.Parse(month) == 11)
            {
                return " Ноября ";
            }
            else if (decimal.Parse(month) == 12)
            {
                return " Декабря ";
            }
            else
                return "Месяц не указан";
        }
        public Printing_AKT_PRIEMA(DataTable dtabe, Form1 mf, string valuta)
        {
            this.valutaMain = valuta;



            InitializeComponent();
            dTable = dtabe;
            mainForm = mf;
            if (dTable.Rows.Count > 0)
            {
                //Подгружаем данные о клиенте
                dt2 = mainForm.basa.ClientsMapGiver(dTable.Rows[0].ItemArray[29].ToString());

                int i = 0;
                string str = File.ReadAllText(@"Shablon_Act_Priema.htm");
                string datP1 = DateTime.Parse(dTable.Rows[i].ItemArray[1].ToString()).ToString("dd");
                string datP2Month = DateTime.Parse(dTable.Rows[i].ItemArray[1].ToString()).ToString("MM");
                string datP3 = DateTime.Parse(dTable.Rows[i].ItemArray[1].ToString()).ToString("yyyy");
                str = str.Replace("NUMZAKAZPRINT", dTable.Rows[i].ItemArray[0].ToString() + "   | Дата выдачи акта: " + datP1 + MonthInText(datP2Month) + datP3);
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
                str = str.Replace("PREDOPLATAPRINT", dtabe.Rows[i].ItemArray[16].ToString() + " " + valutaMain);
                if (dtabe.Rows[i].ItemArray[15].ToString() != " " && dtabe.Rows[i].ItemArray[15].ToString() != "0")
                    str = str.Replace("PREDSTOIMOSTPRINT", dtabe.Rows[i].ItemArray[15].ToString() + " " + valutaMain);
                else
                    str = str.Replace("PREDSTOIMOSTPRINT", "Не оговорена");

                str = str.Replace("FIRMNAMEPRINT", File.ReadAllText("Settings/Akts/FirmName.txt"));
                str = str.Replace("FIRMTELPRINT", File.ReadAllText("Settings/Akts/Phone.txt"));
                str = str.Replace("DANNIEOFIRMEPRINT", File.ReadAllText("Settings/Akts/DannieOFirme.txt"));
                str = str.Replace("URDANNIEPRINT", File.ReadAllText("Settings/Akts/URDannie.txt"));

                str = str.Replace("DOGOVORPRIEMPRINT", File.ReadAllText("Settings/Akts/DogovorTextPriem.txt"));
                if (dt2.Rows[0].ItemArray[3].ToString() == "")
                {
                    str = str.Replace("ADRESSCLIENTAIFNOTNULLPRINT", "Не указан");
                }
                else
                {
                    str = str.Replace("ADRESSCLIENTAIFNOTNULLPRINT", dt2.Rows[0].ItemArray[3].ToString());
                }
                File.WriteAllText("Act_priema", str);
                webBrowser1.Navigate(Application.StartupPath + @"\Act_priema");

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
                // wait zakaz 24, adress clienta 25, image key 26, adress sc 27, device colour 28

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
        private void PrintButton_Click(object sender, EventArgs e)
        {
            //Чтобы не было колонтитулов сверху и снизу страницы
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\PageSetup", "header", "");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\PageSetup", "footer", "");
            webBrowser1.ShowPrintDialog();

            mainForm.StatusStripLabel.Text = "Печать акта приёма " + id_bd;

        }

        private void ReAktor_Click(object sender, EventArgs e)
        {
            RedaktorAktov ReAkt1 = new RedaktorAktov(mainForm);
            ReAkt1.Show();
        }

        private void SetPrintButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPageSetupDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Чтобы не было колонтитулов сверху и снизу страницы
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\PageSetup", "header", "");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\PageSetup", "footer", "");
            webBrowser1.ShowPrintPreviewDialog();

            mainForm.StatusStripLabel.Text = "Печать акта приёма " + id_bd;
        }

        private void Printing_AKT_PRIEMA_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Чтобы не было колонтитулов сверху и снизу страницы
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\PageSetup", "header", "");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\PageSetup", "footer", "");

            mainForm.StatusStripLabel.Text = "Печать акта приёма " + id_bd;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            PrinterSettings.StringCollection sc = PrinterSettings.InstalledPrinters;
            sc[0].ToString();

            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = sc[0].ToString();
                pd.PrintPage += new PrintPageEventHandler
                       (this.prnDocument_PrintPage);
                if (pd.PrinterSettings.IsValid)
                    pd.Print();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void prnDocument_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font ft = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 9);
            if (dt2.Rows.Count > 0)
            {
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

                string FIO = FirstLetterToUpper(dt2.Rows[0].ItemArray[1].ToString());
                string PHONE = phoneNumber;
                string NUMBER = dTable.Rows[0].ItemArray[0].ToString();
                ev.Graphics.DrawImage(new Bitmap("Barcode.gif"), new Point(0, 0));
                ev.Graphics.DrawString(FIO, ft, Brushes.Black, new PointF(0, 41));
                ev.Graphics.DrawString(PHONE, ft, Brushes.Black, new PointF(0, 53));
                ev.Graphics.DrawString("Номер заказа: " + NUMBER, ft, Brushes.Black, new PointF(0, 65));
                ev.HasMorePages = false;
            }

        }
    }
}
