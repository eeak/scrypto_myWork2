using System;
using System.Collections;
namespace MyWork2
{
    public class itemComparerStock : IComparer
    {
        int columnIndex = 0;
        Stock mainForm;
        bool sortAscending = false;
        public itemComparerStock(Stock fm)
        {
            this.mainForm = fm;
        }

        //Это свойство инициализируется при каждом клике на column header'e
        public int ColumnIndex
        {
            set
            {
                //предыдущий клик был на этой же колонке?
                if (columnIndex == value)
                    //да - меняем направление сортировки
                    sortAscending = !sortAscending;
                else
                {
                    columnIndex = value;
                    sortAscending = true;
                }

                try
                {
                    if (columnIndex == 0)
                    {


                    }
                    else if (columnIndex == 1)
                    {
                        if (sortAscending)
                        {

                        }
                        else
                        {

                        }
                    }
                    else if (columnIndex == 2)
                    {


                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка при сортировке " + Environment.NewLine + ex.ToString() + Environment.NewLine);
                }


            }
        }

        public int Compare(object x, object y)
        {
            return 0;
        }
    }
}

