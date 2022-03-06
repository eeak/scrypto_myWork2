using System;

namespace MyWork2
{
    public class SortirovkaSpiskov : IComparable<SortirovkaSpiskov>
    {
        public int count = 0;
        public string SortObj = "";

        public SortirovkaSpiskov(int count, string brand)
        {
            this.count = count; this.SortObj = brand;
        }

        public int CompareTo(object obj)
        {
            return count.CompareTo(obj);
        }

        public int CompareTo(SortirovkaSpiskov other)//сортировка по по полю Name
        {
            if (other == null)
                return 1;

            else
                return this.count.CompareTo(other.count);
        }
    }
}
