namespace MyWork2
{
    public class KlientBase
    {

        public string id;
        public string FIO;
        public string Phone;
        public string Adress;
        public string Primechanie;
        public string Blist;
        public string Date;
        public string AboutUs;

        public KlientBase(string id, string FIO, string Phone, string Adress, string Primechanie, string Blist, string Date, string AboutUs)
        {
            this.id = id;
            this.FIO = FIO;
            this.Phone = Phone;
            this.Adress = Adress;
            this.Primechanie = Primechanie;
            this.Blist = Blist;
            this.Date = Date;
            this.AboutUs = AboutUs;
        }
    }
}
