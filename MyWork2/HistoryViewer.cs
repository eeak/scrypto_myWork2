using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class HistoryViewer : Form
    {
        ItemComparerHistory itemComparerHistory;
        public List<HistoryViewerListViewLoader> HistoryList = new List<HistoryViewerListViewLoader>();
        Form1 mainForm;
        bool dataSearcherRetriview = false;
        public HistoryViewer(Form1 mf)
        {
            mainForm = mf;
            InitializeComponent();
            itemComparerHistory = new ItemComparerHistory(this);
            try
            {

                //lview сортировка
                //   itemComparerClients = new ItemComparerClients(this);
                HistoryListView.ListViewItemSorter = itemComparerHistory;
                HistoryListView.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Указываем сортируемую колонку
                itemComparerHistory.ColumnIndex = e.Column;
                //   MainListView.Items.Clear();
                HistoryListView.VirtualListSize = HistoryList.Count;
                //Да, каждый раз через жопу, не баг а фича!!! Пока не разобрался, как заставить перерисовывать лист вью в виртуальном режиме, 
                //если количество итемов не поменялось.
                HistoryListView.VirtualListSize = HistoryListView.VirtualListSize - 1;
                HistoryListView.VirtualListSize = HistoryListView.VirtualListSize + 1;
                //  MainListView.RedrawItems(TemporaryBase.startIndex, TemporaryBase.endIndex, false);


            }
            catch
            {
                MessageBox.Show("Нечего сортировать");
            }
        }
        private void HistoryViewer_Load(object sender, EventArgs e)
        {
            HistorySearcher();
            comboboxUsersMaker(UserComboBox1);
            comboboxGroupsMaker(GroupsComboBox1);

        }

        private void HistorySearcher(string fio = "", string date = "", string WHO = "", string WHAT = "")
        {
            HistoryListView.Items.Clear();
            HistoryList.Clear();
            DataTable dt2 = mainForm.basa.HISTORYSearchFIO(fio, date, WHO, WHAT);

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                // Если поиск по дипазону дат
                if (dataSearcherRetriview)
                {
                    if (DateTime.Parse(dt2.Rows[i].ItemArray[4].ToString()) >= dateTimePicker1.Value && DateTime.Parse(dt2.Rows[i].ItemArray[4].ToString()) <= dateTimePicker2.Value)
                    {
                        // добавляем новый элемент в коллекцию
                        HistoryList.Add(new HistoryViewerListViewLoader(dt2.Rows[i].ItemArray[0].ToString(),
                          dt2.Rows[i].ItemArray[1].ToString(),
                          dt2.Rows[i].ItemArray[2].ToString(),
                          dt2.Rows[i].ItemArray[3].ToString(),
                          dt2.Rows[i].ItemArray[4].ToString()));

                    }
                }

                else
                {
                    HistoryList.Add(new HistoryViewerListViewLoader(dt2.Rows[i].ItemArray[0].ToString(),
                dt2.Rows[i].ItemArray[1].ToString(),
                dt2.Rows[i].ItemArray[2].ToString(),
                dt2.Rows[i].ItemArray[3].ToString(),
                dt2.Rows[i].ItemArray[4].ToString()));
                }

            }
            dataSearcherRetriview = false;
            HistoryListView.VirtualListSize = HistoryList.Count;

            if (HistoryListView.VirtualListSize > 0)
            {
                HistoryListView.VirtualListSize = HistoryListView.VirtualListSize - 1;
                HistoryListView.VirtualListSize = HistoryListView.VirtualListSize + 1;
            }
        }

        private void ClientsListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {

                // добавляем новый элемент в коллекцию
                e.Item = new ListViewItem(HistoryList[e.ItemIndex].id);
                e.Item.SubItems.Add(HistoryList[e.ItemIndex].WHO);
                e.Item.SubItems.Add(HistoryList[e.ItemIndex].WHAT);
                e.Item.SubItems.Add(HistoryList[e.ItemIndex].FULLWHAT);
                e.Item.SubItems.Add(HistoryList[e.ItemIndex].data.ToString("dd-MM-yyyy HH:mm"));

                // Если индекс в границах нашей коллекции
                if (e.ItemIndex >= 0 && e.ItemIndex < HistoryList.Count)
                {
                    //Черезстрочная 
                    if (e.ItemIndex % 2 == 0)
                        e.Item.BackColor = Color.FromArgb(240, 240, 240);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
            dataSearcherRetriview = false;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            HistorySearcher("", monthCalendar1.SelectionStart.ToString("dd-MM-yyyy"));
        }

        private void comboboxGroupsMaker(ComboBox cmbox)
        {
            cmbox.Items.Clear();
            DataTable dTable = mainForm.basa.GroupDostupBdRead();
            if (dTable.Rows.Count > 0)
            {
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    cmbox.Items.Add(dTable.Rows[i].ItemArray[1].ToString());
                }
            }
        }

        private void comboboxUsersMaker(ComboBox cmbox)
        {
            cmbox.Items.Clear();
            DataTable dTable = mainForm.basa.UsersBdRead();
            if (dTable.Rows.Count > 0)
            {
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    cmbox.Items.Add(dTable.Rows[i].ItemArray[2].ToString());
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string WHO = UserComboBox1.Text + " " + GroupsComboBox1.Text;

            HistorySearcher(WHO);
        }

        private void SearchDataButton_Click(object sender, EventArgs e)
        {
            dataSearcherRetriview = true;
            HistorySearcher();
        }

        private void ResetSearchButton_Click(object sender, EventArgs e)
        {
            dataSearcherRetriview = false;
            HistorySearcher();
        }

        private void HistoryListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void HistoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HistoryListView.SelectedIndices.Count > 0)
                FullWhatTextBox.Text = HistoryListView.Items[HistoryListView.SelectedIndices[0]].SubItems[3].Text;
        }
    }

    public class HistoryViewerListViewLoader
    {
        public string id = "";
        public string WHO = "";
        public string WHAT = "";
        public string FULLWHAT = "";
        public DateTime data = new DateTime();
        public HistoryViewerListViewLoader(string id, string WHO, string WHAT, string FULLWHAT, string data)
        {
            this.id = id;
            this.WHO = WHO;
            this.WHAT = WHAT;
            this.FULLWHAT = FULLWHAT;
            this.data = DateTime.Parse(data);
        }
    }


    public class ItemComparerHistory : System.Collections.IComparer
    {
        int columnIndex = 0;
        bool sortAscending = false;
        HistoryViewer hView;
        public ItemComparerHistory(HistoryViewer hv)
        {
            hView = hv;
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
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return decimal.Parse(vc2.id).CompareTo(decimal.Parse(vc1.id));
                            });
                        }
                        else
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return decimal.Parse(vc1.id).CompareTo(decimal.Parse(vc2.id));
                            });
                        }

                    }
                    else if (columnIndex == 1)
                    {
                        if (sortAscending)
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc2.WHO.CompareTo(vc1.WHO);
                            });
                        }
                        else
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc1.WHO.CompareTo(vc2.WHO);
                            });
                        }
                    }
                    else if (columnIndex == 2)
                    {
                        if (sortAscending)
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc2.WHAT.CompareTo(vc1.WHAT);
                            });
                        }
                        else
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc1.WHAT.CompareTo(vc2.WHO);
                            });
                        }
                    }
                    else if (columnIndex == 3)
                    {
                        if (sortAscending)
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc2.FULLWHAT.CompareTo(vc1.FULLWHAT);
                            });
                        }
                        else
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc1.FULLWHAT.CompareTo(vc2.FULLWHAT);
                            });
                        }
                    }
                    else if (columnIndex == 4)
                    {
                        if (sortAscending)
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc2.data.CompareTo(vc1.data);
                            });
                        }
                        else
                        {
                            hView.HistoryList.Sort(delegate (HistoryViewerListViewLoader vc1, HistoryViewerListViewLoader vc2)
                            {
                                return vc1.data.CompareTo(vc2.data);
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
            HistoryViewerListViewLoader zx = (HistoryViewerListViewLoader)x;
            HistoryViewerListViewLoader zy = (HistoryViewerListViewLoader)y;
            if (decimal.Parse(zx.id) < decimal.Parse(zy.id))
                return -1;
            else if (decimal.Parse(zx.id) > decimal.Parse(zy.id))
                return 1;
            else
                return 0;
        }
    }

}
