using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class SmsFromEditor : Form
    {
        string fio = "", phone = "", status = "", type = "", brand = "", model = "", stoimost = "0", skidka = "0", predoplata = "0", predStoimost = "0", id_bd = "-1";
        Form1 mainForm;

        private void ShublonSetButton_Click(object sender, EventArgs e)
        {
            SmsMain smsM = new SmsMain(mainForm);
            smsM.Show(this);
        }

        private void SmsSoglasovanButton_Click(object sender, EventArgs e)
        {
            SmsReadyButton.BackColor = Color.FromName("ControlLight");
            SmsShablonButton.BackColor = Color.FromName("ControlLight");
            SmsSoglasovanButton.BackColor = Color.Aqua;
            SmsPrivetButton.BackColor = Color.FromName("ControlLight");

            SmsTextBox.Text = TemporaryBase.smsTextSoglasovat;
            textReplace();
        }

        private void SmsShablonButton_Click(object sender, EventArgs e)
        {
            SmsReadyButton.BackColor = Color.FromName("ControlLight");
            SmsShablonButton.BackColor = Color.Aqua;
            SmsSoglasovanButton.BackColor = Color.FromName("ControlLight");
            SmsPrivetButton.BackColor = Color.FromName("ControlLight");

            SmsTextBox.Text = TemporaryBase.smsTextShablon;
            textReplace();
        }

        private void SmsPrivetButton_Click(object sender, EventArgs e)
        {
            SmsReadyButton.BackColor = Color.FromName("ControlLight");
            SmsShablonButton.BackColor = Color.FromName("ControlLight");
            SmsSoglasovanButton.BackColor = Color.FromName("ControlLight");
            SmsPrivetButton.BackColor = Color.Aqua;

            SmsTextBox.Text = TemporaryBase.smsTextPrivet;
            textReplace();
        }

        private void SmsReadyButton_Click(object sender, EventArgs e)
        {
            SmsReadyButton.BackColor = Color.Aqua;
            SmsShablonButton.BackColor = Color.FromName("ControlLight");
            SmsSoglasovanButton.BackColor = Color.FromName("ControlLight");
            SmsPrivetButton.BackColor = Color.FromName("ControlLight");

            SmsTextBox.Text = TemporaryBase.smsTextGotov;
            textReplace();
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            if (PhoneListView.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection PhonesCollection = PhoneListView.SelectedItems;
                foreach (ListViewItem item in PhonesCollection)
                {
                    try
                    {
                        string getWeb;
                        getWeb = await WebSend(TemporaryBase.smsToken, TemporaryBase.smsPhoneId, item.Text, SmsTextBox.Text);
                        MessageBox.Show("Сообщение отправлено на сайт SemySms" + Environment.NewLine + getWeb);

                        var sms = JsonConvert.DeserializeObject<SmsInfo>(getWeb);
                        if (id_bd != "-1")
                            mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Сообщение отправлено" + Environment.NewLine + item.Text);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
            else { MessageBox.Show("Выберите номер телефона, по которому отправлять смс"); }


        }
        async public Task<string> WebSend(string token, string device, string phone, string msg)
        {
            string url = String.Format("https://semysms.net/api/3/sms.php?token={0}&&device={1}&phone={2}&msg={3}", token, device, phone, msg);
            HttpWebRequest SemiSmsRequest = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse semiSmsResponse = (HttpWebResponse)await SemiSmsRequest.GetResponseAsync())
            using (Stream semiSmsStream = semiSmsResponse.GetResponseStream())
            using (StreamReader semiSmsStreamReader = new StreamReader(semiSmsStream))
            {
                return await semiSmsStreamReader.ReadToEndAsync();
            }
        }
        public SmsFromEditor(string fio, string phone, string status, string type, string brand, string model,
             string stoimost, string skidka, string predoplata, string predStoimost, Form1 fm, string id_bd)
        {
            this.fio = fio;
            this.phone = phone;
            this.status = status;
            this.type = type;
            this.brand = brand;
            this.model = model;
            this.stoimost = stoimost;
            this.skidka = skidka;
            this.predoplata = predoplata;
            this.predStoimost = predStoimost;
            this.mainForm = fm;
            this.id_bd = id_bd;
            InitializeComponent();
        }

        private void SmsFromEditor_Load(object sender, EventArgs e)
        {



            List<string> phonesList = new List<string>();

            foreach (string phoneNum in phone.Split(','))
            {
                string phoneNumZamena = phoneNum;
                if (phoneNum.Substring(0, TemporaryBase.smsPhone.Length) == TemporaryBase.smsPhone)
                {
                    phoneNumZamena = $"{TemporaryBase.smsPhonePref}" + phoneNum.Substring(TemporaryBase.smsPhone.Length);
                }

                ListViewItem lvi = new ListViewItem();
                // установка названия файла
                lvi.Text = phoneNumZamena;
                // добавляем элемент в ListView
                PhoneListView.Items.Add(lvi);
            }
            if (PhoneListView.Items.Count > 0)
            {
                PhoneListView.Select();
                PhoneListView.Items[0].Selected = true;
            }
            if (status == "Готов") SmsReadyButton_Click(sender, e);
            else if (status == "Диагностика") SmsPrivetButton_Click(sender, e);
            else SmsSoglasovanButton_Click(sender, e);

            this.Text = $"Отправка смс : {FirstLetterToUpper(fio)}";
        }
        void textReplace()
        {
            fio = FirstLetterToUpper(fio);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{ФИО}", fio);

            SmsTextBox.Text = SmsTextBox.Text.Replace("{ТЕЛЕФОН}", phone);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{СТАТУС}", status);

            type = FirstLetterToUpper(type);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{ТИП}", type);
            brand = FirstLetterToUpper(brand);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{БРЕНД}", brand);
            model = FirstLetterToUpper(model);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{МОДЕЛЬ}", model);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{ЦЕНА}", stoimost);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{СКИДКА}", skidka);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{ПРЕДОПЛАТА}", predoplata);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{ПРЕДСТОИМОСТЬ}", predStoimost);
            SmsTextBox.Text = SmsTextBox.Text.Replace("{НОМЕР}", id_bd);
            try
            {
                SmsTextBox.Text = SmsTextBox.Text.Replace("{ЦЕНАБЕЗПРЕДОПЛАТЫ}", (decimal.Parse(stoimost) - decimal.Parse(predoplata)).ToString());
            }
            catch
            {
                SmsTextBox.Text = SmsTextBox.Text.Replace("{ЦЕНАБЕЗПРЕДОПЛАТЫ}", stoimost);
            }

        }
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
    }
}
