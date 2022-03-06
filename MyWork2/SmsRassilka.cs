using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class SmsRassilka : Form
    {
        Form1 mainForm;
        List<VirtualClient> vClientListCyr = null;
        public SmsRassilka(Form1 mf)
        {
            InitializeComponent();
            mainForm = mf;
        }

        private void SmsRassilka_Load(object sender, EventArgs e)
        {
            foreach (string strCombo in TemporaryBase.SortirovkaAdressSc)
            {
                ServiceAdressComboBox.Items.Add(strCombo);
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaBrands)
            {
                BrandComboBox.Items.Add(ssp.SortObj);
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaUstrojstvo)
            {
                What_remont_combo_box.Items.Add(ssp.SortObj);
            }
            dateTimePicker1.Value = DateTime.Now.AddYears(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List<VirtualClient> vClientList = new List<VirtualClient>();
            vClientList = mainForm.basa.BdReadSmsList(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), ServiceAdressComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text);
            // так как бд хрен соритрует разные раскладки кириллицы, нужен свой механизм сортировки
            vClientListCyr = new List<VirtualClient>();

            if (vClientList.Count > 0)
            {
                //Цикл, где все происходит ))
                for (int i = 0; i < vClientList.Count; i++)
                {
                    decimal.Parse(vClientList[i].Okonchatelnaya_stoimost_remonta);
                    // Не вычитаем скидку из выручки
                    // summa_viruchki -= decimal.Parse(dSet.Tables[0].Rows[i].ItemArray[19].ToString());
                    decimal.Parse(vClientList[i].Zatrati);
                    decimal.Parse(vClientList[i].Skidka);

                    if (vClientList[i].Vipolnenie_raboti.ToUpper().Contains(VipRabotTextBox.Text.ToUpper()))
                    {
                        try { if (vClientList[i].Phone.Length > 9 && vClientList[i].Phone.Length <= 13) vClientListCyr.Add(vClientList[i]); }
                        catch { }

                    }


                }
            }
            List<VirtualClient> tempVClist = new List<VirtualClient>();
            foreach (VirtualClient vc in vClientListCyr)
            {
                bool vcCheck = true;
                foreach (VirtualClient virt in tempVClist)
                {
                    if (virt.Phone == vc.Phone)
                        vcCheck = false;
                }
                if (vcCheck)
                {
                    tempVClist.Add(vc);
                    ListViewItem lvi = new ListViewItem();
                    // установка названия файла
                    lvi.Text = vc.Surname;
                    lvi.SubItems.Add(vc.Phone);
                    lvi.SubItems.Add(vc.Vipolnenie_raboti);
                    lvi.SubItems.Add(vc.WhatRemont);
                    lvi.SubItems.Add(vc.Brand);
                    //lvi.SubItems.Add(vc.Phone);
                    // добавляем элемент в ListView
                    listView1.Items.Add(lvi);
                }

            }
            valCounter.Text = $"Количество подходщих записей: {tempVClist.Count}";

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            foreach (VirtualClient vc in vClientListCyr)
            {
                string msgText = SmsReadyTextBox.Text.Replace("FIO", vc.Surname);
                string msgPhone = vc.Phone;
                string getWeb;
                getWeb = await WebSend(TemporaryBase.smsToken, TemporaryBase.smsPhoneId, msgPhone, msgText);
                SmsInfo sms = JsonConvert.DeserializeObject<SmsInfo>(getWeb);
                textBox1.AppendText($"{vc.Phone} Сообщение отправлено {vc.Surname} : {sms.code} {Environment.NewLine}");

            }



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
    }
}
