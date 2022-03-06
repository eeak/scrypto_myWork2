namespace MyWork2
{
    public class PhoneExport
    {
        public string Phone;
        public string FIO;
        public PhoneExport(string phone, string fio)
        {
            Phone = phone;
            FIO = fio;
        }
        //Перегрузка для корректной сортировки и удаления повторов в Export.cs
        public override bool Equals(object obj)
        {
            return ((PhoneExport)obj).Phone == Phone;
        }
        public override int GetHashCode()
        {
            return Phone.GetHashCode();
        }
    }
}
