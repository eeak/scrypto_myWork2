using System;
using System.Collections;

namespace MyWork2
{
    public class ItemComparerClients : IComparer
    {
        int columnIndex = 0;
        ClientsEditor ClientsForm;
        bool sortAscending = false;
        public ItemComparerClients(ClientsEditor cm)
        {
            this.ClientsForm = cm;
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
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return decimal.Parse(vc2.id).CompareTo(decimal.Parse(vc1.id));
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return decimal.Parse(vc1.id).CompareTo(decimal.Parse(vc2.id));
                            });
                        }

                    }
                    else if (columnIndex == 1)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.FIO.CompareTo(vc1.FIO);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.FIO.CompareTo(vc2.FIO);
                            });
                        }
                    }
                    else if (columnIndex == 2)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.Phone.CompareTo(vc1.Phone);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.Phone.CompareTo(vc2.Phone);
                            });
                        }
                    }
                    else if (columnIndex == 3)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.Adress.CompareTo(vc1.Adress);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.Adress.CompareTo(vc2.Adress);
                            });
                        }
                    }
                    else if (columnIndex == 4)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.AboutUs.CompareTo(vc1.AboutUs);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.AboutUs.CompareTo(vc2.AboutUs);
                            });
                        }
                    }
                    else if (columnIndex == 5)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.Blist.CompareTo(vc1.Blist);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.Blist.CompareTo(vc2.Blist);
                            });
                        }
                    }
                    else if (columnIndex == 6)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc2.Primechanie.CompareTo(vc1.Primechanie);
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                return vc1.Primechanie.CompareTo(vc2.Primechanie);
                            });
                        }
                    }
                    else if (columnIndex == 7)
                    {
                        if (sortAscending)
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Date;
                                val2 = vc2.Date;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val2).CompareTo(DateTime.Parse(val1));
                            });
                        }
                        else
                        {
                            ClientsForm.ClientsList.Sort(delegate (KlientBase vc1, KlientBase vc2)
                            {
                                string val1 = "", val2 = "";
                                val1 = vc1.Date;
                                val2 = vc2.Date;
                                if (val1 == "")
                                    val1 = "01.01.1970";
                                if (val2 == "")
                                    val2 = "01.01.1970";
                                return DateTime.Parse(val1).CompareTo(DateTime.Parse(val2));
                            });
                        }
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
            KlientBase zx = (KlientBase)x;
            KlientBase zy = (KlientBase)y;
            if (decimal.Parse(zx.id) < decimal.Parse(zy.id))
                return -1;
            else if (decimal.Parse(zx.id) > decimal.Parse(zy.id))
                return 1;
            else
                return 0;
        }
    }
}
