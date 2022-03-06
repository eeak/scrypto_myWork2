namespace MyWork2
{
    public class VirtualClient //: IComparable<VirtualClient>
    {
        public string Id;
        public string Data_priema;
        public string Data_vidachi;
        public string Data_predoplaty;
        public string Surname;
        public string Phone;
        public string AboutUs;
        public string WhatRemont;
        public string Brand;
        public string Model;
        public string SerialNumber;
        public string Sostoyanie;
        public string Komplektonst;
        public string Polomka;
        public string Kommentarij;
        public string Predvaritelnaya_stoimost;
        public string Predoplata;
        public string Zatrati;
        public string Okonchatelnaya_stoimost_remonta;
        public string Skidka;
        public string Status_remonta;
        public string Master;
        public string Vipolnenie_raboti;
        public string Garanty;
        public string Wait_zakaz;
        public string Adress;
        public string Image_key;
        public string AdressSC;
        public string DeviceColour;
        public int ClientId;
        public string Barcode;
        //Врод что-то для верной сортировки
        public bool Diagnosik;

        public VirtualClient(string id, string data_priema, string data_vidachi, string data_predoplaty, string surname, string phone, string aboutUs,
            string whatRemont, string brand, string model, string serialNumber, string sostoyanie, string komplektnost, string polomka, string kommentarij,
            string predvaritelnaya_stoimost, string predoplata, string zatrati, string okonchatelnaya_stoimost_remonta, string skidka, string status_remonta,
            string master, string vipolnenie_raboti, string garanty, string wait_zakaz, string adress, string image_key, bool diagnosik, string adressSc, string deviceColour, int clientId = -1, string barcode = "")
        {
            Id = id;
            Data_priema = data_priema;
            Data_vidachi = data_vidachi;
            Data_predoplaty = data_predoplaty;
            Surname = surname;
            Phone = phone;
            AboutUs = aboutUs;
            WhatRemont = whatRemont;
            Brand = brand;
            Model = model;
            SerialNumber = serialNumber;
            Sostoyanie = sostoyanie;
            Komplektonst = komplektnost;
            Polomka = polomka;
            Kommentarij = kommentarij;
            Predvaritelnaya_stoimost = predvaritelnaya_stoimost;
            Predoplata = predoplata;
            Zatrati = zatrati;
            Okonchatelnaya_stoimost_remonta = okonchatelnaya_stoimost_remonta;
            Skidka = skidka;
            Status_remonta = status_remonta;
            Master = master;
            Vipolnenie_raboti = vipolnenie_raboti;
            Garanty = garanty;
            Wait_zakaz = wait_zakaz;
            Adress = adress;
            Image_key = image_key;
            Diagnosik = diagnosik;
            AdressSC = adressSc;
            DeviceColour = deviceColour;
            ClientId = clientId;
            Barcode = barcode;
        }

    }


}
