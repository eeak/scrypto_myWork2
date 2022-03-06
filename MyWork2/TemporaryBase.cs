using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;




namespace MyWork2
{
    public static class TemporaryBase
    {
        //Внешние
        //Ини файл
        static IniFile INIF = new IniFile("Config.ini");
        //Класс работы с бд
        public static BDWorker basa;
        // Основное окно
        public static Form1 mainForm;

        //Поиск по критериям
        public static string FIO = "";
        public static string Phone = "";
        public static string TypeOf = "";
        public static string Brand = "";
        public static string Model = "";
        public static string Status = "";
        public static string Master = "";
        //true = не стоит галочка поиск у нас
        public static bool SearchInOld = true;
        public static string NeedZakaz = "";
        public static int ColumnIndex = 0;
        public static bool SortAscending = false;
        public static bool FullSearchOpen = false;
        public static string soglasovat = "";
        public static string idinBd = "";
        public static string SerialImei = "";
        public static string AdressSC = "";
        public static bool IskatVseVidannoe = false;
        //sms
        public static string smsToken = "";
        public static string smsPhoneId = "";

        public static string smsTextGotov = "";
        public static string smsTextSoglasovat = "";
        public static string smsTextShablon = "";
        public static string smsTextPrivet = "";
        public static string smsPhonePref = "+7";
        public static string smsPhone = "8";
        // Процент мастера
        public static string Mpersent = "50";
        //Обязательные поля
        public static bool Nessesary = false;
        //Работа с цветом
        public static Color backOfColour;
        //Функция очистки данных

        //Функция EditorGaranty
        public static string EditorGarantyComboboxVal = "30 дней";
        public static void SearchCleaner()
        {
            FIO = "";
            Phone = "";
            TypeOf = "";
            Brand = "";
            Model = "";
            Status = "";
            Master = "";
            SearchInOld = true;
            NeedZakaz = "";
            ColumnIndex = 0;
            SortAscending = false;
            FullSearchOpen = false;
            soglasovat = "";
            SerialImei = "";
            AdressSC = "";

            idinBd = "";
        }

        //Производит поиск по базе
        public static void SearchFULLBegin(string phoneSearchInOld = "")
        {
            try
            {
                mainForm.MainListView.Items.Clear();
                mainForm.VCList.Clear();
                DataTable dt1;
                if (Status == "")
                {
                    dt1 = mainForm.basa.BdReadFullSearch(FIO, Phone, TypeOf, Brand, Model,
                     Status, Master, NeedZakaz, SearchInOld, idinBd, SerialImei, AdressSC, "", soglasovat);
                }
                else if (Status == "Выдан")
                {
                    TemporaryBase.IskatVseVidannoe = false;
                    //Если статус принят по грантии, то ищем и в выданном тоже
                    dt1 = mainForm.basa.BdReadFullSearch(FIO, Phone, TypeOf, Brand, Model,
                    Status, Master, NeedZakaz, SearchInOld, idinBd, SerialImei, AdressSC, "garanty", soglasovat, IskatVseVidannoe);
                }
                else
                {
                    //Если статус принят по грантии, то ищем и в выданном тоже
                    dt1 = mainForm.basa.BdReadFullSearch(FIO, Phone, TypeOf, Brand, Model,
                    Status, Master, NeedZakaz, SearchInOld, idinBd, SerialImei, AdressSC, "garanty", soglasovat);
                }

                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                             dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                              dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                              dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                              dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                              dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), TemporaryBase.diagnostika, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[29].ToString());

                    // Если снята галочка Готово, то не показываем записи со статусом Готово, всё логично ;-)
                    if (mainForm.ReadyFilterCheckBox.BackColor == Color.FromArgb(240, 240, 240) && Status != "Готов")
                    {
                        if (vc.Status_remonta != "Готов" || vc.Data_vidachi != "")
                        {
                            if (mainForm.ServiceAdressComboBox.Text != "")
                            {
                                if (vc.AdressSC == mainForm.ServiceAdressComboBox.Text.ToUpper())
                                {
                                    mainForm.VCList.Add(vc);
                                }
                            }
                            else
                                mainForm.VCList.Add(vc);
                        }
                    }
                    else
                    {
                        if (mainForm.ServiceAdressComboBox.Text != "")
                        {
                            if (vc.AdressSC == mainForm.ServiceAdressComboBox.Text.ToUpper())
                            {
                                mainForm.VCList.Add(vc);
                            }
                        }
                        else
                            mainForm.VCList.Add(vc);
                    }
                }

                try
                {
                    if (ColumnIndex == 0)
                    {
                        if (SortAscending)
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return int.Parse(vc1.Id).CompareTo(int.Parse(vc2.Id));
                            });
                        }
                        else
                        {
                            mainForm.VCList.Sort(delegate (VirtualClient vc1, VirtualClient vc2)
                            {
                                return int.Parse(vc2.Id).CompareTo(int.Parse(vc1.Id));
                            });
                        }

                    }

                    // Для верной сортировки по дате приёма
                    else if (ColumnIndex == 1)
                    {
                        if (SortAscending)
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
                    }
                    // Для верной сортировки по дате выдачи
                    else if (ColumnIndex == 2)
                    {
                        if (SortAscending)
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
                    }

                    // Для верной сортировки по дате предоплаты
                    else if (ColumnIndex == 3)
                    {
                        if (SortAscending)
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
                    }
                    // для сортировки текста
                    else if (ColumnIndex == 4)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 5)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 6)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 7)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 8)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 9)
                    {
                        if (SortAscending)
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
                    }
                    // для сортировки текста
                    else if (ColumnIndex == 10)
                    {
                        if (SortAscending)
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
                    }
                    // для сортировки текста
                    else if (ColumnIndex == 11)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 12)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 13)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 14)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки чисел
                    else if (ColumnIndex == 15)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки чисел
                    else if (ColumnIndex == 16)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки чисел
                    else if (ColumnIndex == 17)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки чисел
                    else if (ColumnIndex == 18)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки чисел
                    else if (ColumnIndex == 19)
                    {
                        if (SortAscending)
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

                    }


                    // для сортировки текста
                    else if (ColumnIndex == 20)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 21)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 22)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 23)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 24)
                    {
                        if (SortAscending)
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

                    }
                    // для сортировки текста
                    else if (ColumnIndex == 25)
                    {
                        if (SortAscending)
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

                    }
                    else if (ColumnIndex == 26)
                    {
                        if (SortAscending)
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

                    }
                    else if (ColumnIndex == 27)
                    {
                        if (SortAscending)
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

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                mainForm.MainListView.VirtualListSize = mainForm.VCList.Count;
                //Да, каждый раз через жопу, не баг а фича!!! Пока не разобрался, как заставить перерисовывать лист вью в виртуальном режиме, 
                //если количество итемов не поменялось.
                if (mainForm.MainListView.VirtualListSize > 0)
                {
                    mainForm.MainListView.VirtualListSize = mainForm.MainListView.VirtualListSize - 1;
                    mainForm.MainListView.VirtualListSize = mainForm.MainListView.VirtualListSize + 1;
                }
                mainForm.CountListViewLabel.Text = "Найдено записей: " + mainForm.VCList.Count;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Для отрисовки listView
        public static int startIndex = 0;
        public static int endIndex = 0;

        //Валюта
        public static string valuta = "";
        //R
        public static bool baseR = true;
        // stock
        public static bool stockR = true;
        // настройки пользователя
        public static string UserKey = "FREEUSER";

        public static string AdressSCDefault = "";

        public static string MasterDefault = "";

        public static bool diagnostika = false;

        public static string everyDayBackup = "Checked";

        public static string BlistColor = "";

        public static bool Poloski = true;


        public static int barcodeW = 180;
        public static int barcodeH = 40;
        // Сортировка выпадающих списков
        public static List<SortirovkaSpiskov> SortirovkaBrands = new List<SortirovkaSpiskov>();
        public static List<SortirovkaSpiskov> SortirovkaUstrojstvo = new List<SortirovkaSpiskov>();
        public static List<SortirovkaSpiskov> SortirovkaAboutUs = new List<SortirovkaSpiskov>();
        public static List<string> SortirovkaAdressSc = new List<string>();
        public static List<string> SortirovkaMasters = new List<string>();
        public static List<string> SortirovkaSostoyanie = new List<string>();
        public static List<string> SortirovkaKomplektnost = new List<string>();
        public static List<string> SortirovkaNeispravnost = new List<string>();
        public static List<string> SortirovkaColour = new List<string>();
        public static List<string> SortirovkaVipolnRaboti = new List<string>();
        public static List<string> SortirovkaGaranty = new List<string>();

        public static List<string> SortirovkaStockUstrojstvo = new List<string>();
        public static List<string> SortirovkaStockPodkategory = new List<string>();
        public static List<string> SortirovkaStockBrands = new List<string>();
        public static List<string> SortirovkaStockDeviceColour = new List<string>();

        public static bool mainWindowsUpdateAfterAllSpiskiReady = false;
        // Пользователь в данный момент
        public static string USER_SESSION = "Admin";

        public static bool openClientFolder = false;
        //Права доступа

        public static string delZapis = "1";
        public static string addZapis = "1";
        public static string saveZapis = "1";
        public static string graf = "1";
        public static string sms = "1";
        public static string stock = "1";
        public static string clients = "1";
        public static string stockAdd = "1";
        public static string stockDel = "1";
        public static string stockEdit = "1";
        public static string clientAdd = "1";
        public static string clientDel = "1";
        public static string clientConcat = "1";
        public static string settings = "1";
        public static string dates = "1";
        public static string editDates = "1";
        public static string pathtoSaveBD = "settings/backup/";

        //Версия программы
        public static string ProgrammVersion = "0";
    }

}
