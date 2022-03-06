using System;
using System.Collections;
using System.Windows.Forms;
//Этот класс реализует интерфейс IComparer
namespace MyWork2
{
    class ItemComparer : IComparer
    {
        int columnIndex = 0;

        bool sortAscending = false;

        Form1 mainForm;

        public ItemComparer(Form1 mf)
        {
            mainForm = mf;
        }
        //Это свойство инициализируется при каждом клике на column header'e
        public int ColumnIndex
        {
            set
            {

                //предыдущий клик был на этой же колонке?
                if (columnIndex == value)
                {
                    //да - меняем направление сортировки
                    sortAscending = !sortAscending;
                    TemporaryBase.ColumnIndex = columnIndex;
                    TemporaryBase.SortAscending = sortAscending;
                }

                else
                {
                    columnIndex = value;
                    sortAscending = true;
                    TemporaryBase.ColumnIndex = columnIndex;
                    TemporaryBase.SortAscending = sortAscending;
                }

                try
                {
                    if (columnIndex == 0)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return int.Parse(vc2.Id).CompareTo(int.Parse(vc1.Id));
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return int.Parse(vc1.Id).CompareTo(int.Parse(vc2.Id));
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }

                    // Для верной сортировки по дате приёма
                    else if (columnIndex == 1)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_priema;
                                val2 = vc2.Data_priema;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val2).CompareTo(DateTime.Parse(val1));
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_priema;
                                val2 = vc2.Data_priema;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val1).CompareTo(DateTime.Parse(val2));
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // Для верной сортировки по дате выдачи
                    else if (columnIndex == 2)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_vidachi;
                                val2 = vc2.Data_vidachi;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val1).CompareTo(DateTime.Parse(val2));
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_vidachi;
                                val2 = vc2.Data_vidachi;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val2).CompareTo(DateTime.Parse(val1));
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }

                    // Для верной сортировки по дате предоплаты
                    else if (columnIndex == 3)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_predoplaty;
                                val2 = vc2.Data_predoplaty;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val1).CompareTo(DateTime.Parse(val2));
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Data_predoplaty;
                                val2 = vc2.Data_predoplaty;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val2).CompareTo(DateTime.Parse(val1));
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 4)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Surname.CompareTo(vc1.Surname);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Surname.CompareTo(vc2.Surname);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 5)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Phone.CompareTo(vc1.Phone);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Phone.CompareTo(vc2.Phone);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 6)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.AboutUs.CompareTo(vc1.AboutUs);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.AboutUs.CompareTo(vc2.AboutUs);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 7)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.WhatRemont.CompareTo(vc1.WhatRemont);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.WhatRemont.CompareTo(vc2.WhatRemont);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 8)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Brand.CompareTo(vc1.Brand);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Brand.CompareTo(vc2.Brand);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 9)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Model.CompareTo(vc1.Model);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Model.CompareTo(vc2.Model);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 10)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.SerialNumber.CompareTo(vc1.SerialNumber);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.SerialNumber.CompareTo(vc2.SerialNumber);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 11)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Sostoyanie.CompareTo(vc1.Sostoyanie);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Sostoyanie.CompareTo(vc2.Sostoyanie);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 12)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Komplektonst.CompareTo(vc1.Komplektonst);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Komplektonst.CompareTo(vc2.Komplektonst);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 13)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Polomka.CompareTo(vc1.Polomka);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Polomka.CompareTo(vc2.Polomka);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 14)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Kommentarij.CompareTo(vc1.Kommentarij);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Kommentarij.CompareTo(vc2.Kommentarij);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки чисел
                    else if (columnIndex == 15)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Predvaritelnaya_stoimost == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Predvaritelnaya_stoimost);
                                }

                                if (vc2.Predvaritelnaya_stoimost == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Predvaritelnaya_stoimost);
                                }
                                return val2.CompareTo(val1);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Predvaritelnaya_stoimost == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Predvaritelnaya_stoimost);
                                }

                                if (vc2.Predvaritelnaya_stoimost == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Predvaritelnaya_stoimost);
                                }
                                return val1.CompareTo(val2);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки чисел
                    else if (columnIndex == 16)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Predoplata == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Predoplata);
                                }

                                if (vc2.Predoplata == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Predoplata);
                                }
                                return val2.CompareTo(val1);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Predoplata == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Predoplata);
                                }

                                if (vc2.Predoplata == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Predoplata);
                                }
                                return val1.CompareTo(val2);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки чисел
                    else if (columnIndex == 17)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Zatrati == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Zatrati);
                                }

                                if (vc2.Zatrati == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Zatrati);
                                }
                                return val2.CompareTo(val1);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Zatrati == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Zatrati);
                                }

                                if (vc2.Zatrati == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Zatrati);
                                }
                                return val1.CompareTo(val2);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки чисел
                    else if (columnIndex == 18)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Okonchatelnaya_stoimost_remonta == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Okonchatelnaya_stoimost_remonta);
                                }

                                if (vc2.Okonchatelnaya_stoimost_remonta == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Okonchatelnaya_stoimost_remonta);
                                }
                                return val2.CompareTo(val1);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Okonchatelnaya_stoimost_remonta == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Okonchatelnaya_stoimost_remonta);
                                }

                                if (vc2.Okonchatelnaya_stoimost_remonta == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Okonchatelnaya_stoimost_remonta);
                                }
                                return val1.CompareTo(val2);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки чисел
                    else if (columnIndex == 19)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Skidka == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Skidka);
                                }

                                if (vc2.Skidka == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Skidka);
                                }
                                return val2.CompareTo(val1);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                int val1 = 0;
                                int val2 = 0;
                                if (vc1.Skidka == "")
                                {
                                    val1 = 0;
                                }

                                else
                                {
                                    val1 = int.Parse(vc1.Skidka);
                                }

                                if (vc2.Skidka == "")
                                {
                                    val2 = 0;
                                }
                                else
                                {
                                    val2 = int.Parse(vc2.Skidka);
                                }
                                return val1.CompareTo(val2);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }


                    // для сортировки текста
                    else if (columnIndex == 20)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Status_remonta.CompareTo(vc1.Status_remonta);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Status_remonta.CompareTo(vc2.Status_remonta);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 21)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Master.CompareTo(vc1.Master);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Master.CompareTo(vc2.Master);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 22)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Vipolnenie_raboti.CompareTo(vc1.Vipolnenie_raboti);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Vipolnenie_raboti.CompareTo(vc2.Vipolnenie_raboti);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 23)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Garanty.CompareTo(vc1.Garanty);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Garanty.CompareTo(vc2.Garanty);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 24)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Wait_zakaz.CompareTo(vc1.Wait_zakaz);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Wait_zakaz.CompareTo(vc2.Wait_zakaz);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 25)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Adress.CompareTo(vc1.Adress);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Adress.CompareTo(vc2.Adress);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 26)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Adress.CompareTo(vc1.Adress);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Adress.CompareTo(vc2.Adress);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                    // для сортировки текста
                    else if (columnIndex == 27)
                    {
                        if (sortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc2.Adress.CompareTo(vc1.Adress);
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return vc1.Adress.CompareTo(vc2.Adress);
                            });
                        }
                        TemporaryBase.ColumnIndex = columnIndex;
                        TemporaryBase.SortAscending = sortAscending;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }



            }
        }
        //здесь непосредственно производится сравнение
        //возвращаемые значения:
        // < 0 если x < y
        // 0 если x = y
        // > 0 если x > y
        public int Compare(object x, object y)
        {
            try
            {

                //Для верной сортировки по номеру
                if (columnIndex == 0 || (columnIndex > 14 && columnIndex < 20))
                {
                    int valX, valY;

                    if (((ListViewItem)x).SubItems[columnIndex].Text == "")
                        valX = -1;
                    else
                        valX = int.Parse(((ListViewItem)x).SubItems[columnIndex].Text);

                    if (((ListViewItem)y).SubItems[columnIndex].Text == "")
                        valY = -1;
                    else
                        valY = int.Parse(((ListViewItem)y).SubItems[columnIndex].Text);
                    return (valX > valY ? 1 : -1) * (sortAscending ? 1 : -1);
                }
                // Для верной сортировки по времени
                else if (columnIndex > 0 && columnIndex != 2 && columnIndex < 4)
                {
                    string val1 = ((ListViewItem)x).SubItems[columnIndex].Text;
                    string val2 = ((ListViewItem)y).SubItems[columnIndex].Text;
                    if (val1 == "")
                        val1 = "01.01.1970";
                    if (val2 == "")
                        val2 = "01.01.1970";
                    DateTime valx = DateTime.Parse(val1);
                    DateTime valy = DateTime.Parse(val2);
                    return (valx > valy ? 1 : -1) * (sortAscending ? 1 : -1);
                }

                // Для верной сортировки по времени ВЫАЧИ
                else if (columnIndex == 2)
                {
                    string val1 = ((ListViewItem)x).SubItems[columnIndex].Text;
                    string val2 = ((ListViewItem)y).SubItems[columnIndex].Text;
                    if (val1 == "")
                        val1 = "01.01.1970";
                    if (val2 == "")
                        val2 = "01.01.1970";
                    DateTime valx = DateTime.Parse(val1);
                    DateTime valy = DateTime.Parse(val2);
                    return (valx > valy ? 1 : -1) * (sortAscending ? -1 : 1);
                }

                else
                {
                    string value1 = ((ListViewItem)x).SubItems[columnIndex].Text;
                    string value2 = ((ListViewItem)y).SubItems[columnIndex].Text;
                    return String.Compare(value1, value2) * (sortAscending ? 1 : -1);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
    }
}
