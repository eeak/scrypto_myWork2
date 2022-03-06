using System;

namespace MyWork2
{
    class HtmlWorker
    {
        string FirmName = "";
        string FirmPhone = "";
        string FirmDannie = "";
        string FirmUrDannie = "";
        string FirmDogovor = "";
        public void ParseShablon(string Shablon)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument ShablonAktov = new HtmlAgilityPack.HtmlDocument();
                ShablonAktov.LoadHtml(Shablon);

                // Если не получилось в try парсим в catch )
                FirmName = ShablonAktov.GetElementbyId("ServiceName").InnerHtml;
                FirmPhone = ShablonAktov.GetElementbyId("phone").InnerHtml;
                FirmDannie = ShablonAktov.GetElementbyId("Dannie").InnerHtml;
                FirmUrDannie = ShablonAktov.GetElementbyId("UrDannie").InnerHtml;
                FirmDogovor = ShablonAktov.GetElementbyId("Dogovor").InnerHtml;
            }



            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());


            }
        }
        public string firmName
        {
            get
            {
                return FirmName;
            }
            set { }
        }

        public string firmPhone
        {
            get
            {
                return FirmPhone;
            }
            set { }
        }

        public string firmDannie
        {
            get
            {
                return FirmDannie;
            }
            set { }
        }

        public string firmUrDannie
        {
            get
            {
                return FirmUrDannie;
            }
            set { }
        }
        public string firmDogovor
        {
            get
            {
                return FirmDogovor;
            }
            set { }
        }

    }
}
