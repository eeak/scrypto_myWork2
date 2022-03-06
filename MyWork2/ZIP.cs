using System;

namespace MyWork2
{
    public class ZIP : IComparable<ZIP>
    {
        public string idOf = "";
        public string naimenovanie = "";
        public string colour = "";
        public string brand = "";
        public string model = "";
        public string photo = "";
        public string kategoriya = "";
        public string podkategoriya = "";
        public string countOf = "0";  // Возможно будут непонятки из-за этого
        public string price = "0";
        public string napominanie = "0";
        public string primechanie = "";
        public string photo2 = "";
        public string photo3 = "";

        public ZIP(string naimenovanie, string colour, string brand, string model, string countOf, string price, string idOf, string photo, string kategoriya, string podkategoriya, string napominanie, string primechanie, string photo2, string photo3)
        {
            try
            {
                if (naimenovanie != null)
                    this.naimenovanie = naimenovanie;
                if (colour != null)
                    this.colour = colour;
                if (brand != null)
                    this.brand = brand;
                if (model != null)
                    this.model = model;
                if (countOf != null && countOf != "")
                    this.countOf = countOf;
                if (price != null && price != "")
                    this.price = price;
                if (idOf != null)
                    this.idOf = idOf;
                if (photo != null)
                    this.photo = photo;
                if (kategoriya != null)
                    this.kategoriya = kategoriya;
                if (podkategoriya != null)
                    this.podkategoriya = podkategoriya;
                if (napominanie != null && napominanie != "")
                    this.napominanie = napominanie;
                if (primechanie != null)
                    this.primechanie = primechanie;
                if (photo2 != null)
                    this.photo2 = photo2;
                if (photo3 != null)
                    this.photo3 = photo3;
            }
            catch (Exception ex)
            {
                ex.ToString(); // нужно вести лог
            }
        }

        public int CompareTo(ZIP ZipToCompare)
        {
            if (decimal.Parse(this.countOf) > decimal.Parse(ZipToCompare.countOf))
                return 1;
            else if (decimal.Parse(this.countOf) < decimal.Parse(ZipToCompare.countOf))
                return -1;
            else
                return 0;
        }
    }
}
