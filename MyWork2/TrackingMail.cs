using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class TrackingMail : Form
    {
        List<Track> trList = new List<Track>();
        public TrackingMail(List<Track> trList)
        {
            InitializeComponent();
            this.trList = trList;
        }

        private void TrackingMail_Load(object sender, EventArgs e)
        {
            foreach (Track tr in trList)
            {
                listBox1.Items.Add($"{tr.trackName} {tr.trackNum}");
            }
        }
        private async Task PostRequestAsync(string track)
        {
            string jpochtra = "";
            WebRequest request = WebRequest.Create("https://www.pochta.ru/tracking?p_p_id=trackingPortlet_WAR_portalportlet&p_p_lifecycle=2&p_p_state=normal&p_p_mode=view&p_p_resource_id=tracking.get-by-barcodes&p_p_cacheability=cacheLevelPage&p_p_col_id=column-1&p_p_col_count=1");
            request.Method = "POST"; // для отправки используется метод Post
                                     // данные для отправки
            string data = $"barcodes={track.ToUpper()}";
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jpochtra = reader.ReadToEnd();
                }
            }
            response.Close();

            Root rt = await Task.Run(() => JsonConverter(jpochtra));
            try
            {
                label1.Text = $"Тип посылки: {rt.response[0].formF22Params.MailTypeText} {Environment.NewLine}" +
              $"Вес {rt.response[0].formF22Params.WeightGr} грамм {Environment.NewLine}" +
              $"Адрес отправителя {rt.response[0].formF22Params.senderAddress} {Environment.NewLine}" +
              $"Трек {rt.response[0].formF22Params.PostId} {Environment.NewLine}" +
              $"Получатель {rt.response[0].trackingItem.recipient} {Environment.NewLine}";
                for (int i = 0; i < rt.response[0].trackingItem.trackingHistoryItemList.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(rt.response[0].trackingItem.trackingHistoryItemList[i].date.ToString("HH:mm dd-MM-yyyy"));
                    lvi.SubItems.Add($"{rt.response[0].trackingItem.trackingHistoryItemList[i].humanStatus} : {rt.response[0].trackingItem.trackingHistoryItemList[i].description}");
                    listView1.Items.Add(lvi);
                }
            }
            catch
            {
                label1.Text = $"Не удалось получить информацию по треку {track.ToUpper()}";
            }

        }
        public Root JsonConverter(string toConvert)
        {
            return JsonConvert.DeserializeObject<Root>(toConvert);
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                listView1.Items.Clear();
                await PostRequestAsync(trList[listBox1.SelectedIndex].trackNum);
            }
        }
    }
}
