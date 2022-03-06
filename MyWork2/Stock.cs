using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace MyWork2
{
    public partial class Stock : Form
    {

        Form1 mainForm;
        itemComparerStock itcStock;
        List<ZIP> ZIP1 = new List<ZIP>();
        IniFile INIF = new IniFile("Config.ini");
        string idInMainBd = "-1";
        Editor editorForm = null;
        Color backOfColour = Color.Green;
        DataTable dtMap1 = null;
        string client_id_base = "";
        public Stock(Form1 mf, string idInMainBd = "-1", Editor ed1 = null, string Client_id_inBase = "")
        {
            InitializeComponent();
            mainForm = mf;
            //lview сортировка
            itcStock = new itemComparerStock(this);
            StockListView.ListViewItemSorter = itcStock;
            StockListView.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
            this.idInMainBd = idInMainBd;
            editorForm = ed1;
            // Подгружаем данные о установках запчасти кленту
            dtMap1 = mainForm.basa.BdStockMapZIPUsedCheckOptimised();
            //Данные о клиенте
            client_id_base = Client_id_inBase;
            //Оптимизация
            //Зарание вычитываем цвет записи
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorDiagnostik"))
                backOfColour = Color.FromArgb(int.Parse(INIF.ReadINI("PROGRAMM_SETTINGS", "colorDiagnostik")));
        }

        void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Указываем сортируемую колонку
                itcStock.ColumnIndex = e.Column;
                //   MainListView.Items.Clear();
                StockListView.VirtualListSize = ZIP1.Count;
                //Да, каждый раз через жопу, не баг а фича!!! Пока не разобрался, как заставить перерисовывать лист вью в виртуальном режиме, 
                //если количество итемов не поменялось.
                StockListView.VirtualListSize = StockListView.VirtualListSize - 1;
                StockListView.VirtualListSize = StockListView.VirtualListSize + 1;
                //  MainListView.RedrawItems(TemporaryBase.startIndex, TemporaryBase.endIndex, false);
            }
            catch
            {
                MessageBox.Show("Нечего сортировать");
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            AddZip.Enabled = (TemporaryBase.stockAdd == "1") ? true : false;
            if (INIF.KeyExists(TemporaryBase.UserKey, "StockPosition"))
            {

                try
                {
                    this.Width = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "STfWidth"));
                    this.Height = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "STfHeight"));
                    this.Left = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "STfLeft"));
                    this.Top = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "STfTop"));

                    if (this.Left < -10000)
                    {
                        this.Left = 0;
                        this.Top = 0;
                        this.Width = 600;
                        this.Height = 600;
                    }
                }
                catch { }
            }
            //Создаём базу данных склада
            mainForm.basa.CreateStock();
            mainForm.basa.CreateStockMap();
            //Добавляем поисковые значения в комбобоксы
            foreach (string strCombo in TemporaryBase.SortirovkaStockUstrojstvo)
            {
                KategoryComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockPodkategory)
            {
                PodKategoryComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockBrands)
            {
                BrandComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaStockDeviceColour)
            {
                ColourComboBox.Items.Add(strCombo);
            }
            StockFullSearch();
            ToolTip t = new ToolTip();
            t.SetToolTip(KategoryComboBox, "Тип устройства");
            t.SetToolTip(PodKategoryComboBox, "Тип запчасти");
            t.SetToolTip(BrandComboBox, "Фирма производитель");
            t.SetToolTip(ModelTextBox, "Модель");
            t.SetToolTip(ColourComboBox, "Цвет");
            t.SetToolTip(StockSearchTextBox, "Поиск по наименованию");
            t.SetToolTip(BuyCheckBox, "Показать записи, количество которых меньше нормы " + Environment.NewLine + "Данная галочка не учитывает остальные поля поиска");
            t.SetToolTip(ClientUsedCheckBox, "Показать записи, которые используются данным клиентом " + Environment.NewLine + "Данная галочка не учитывает остальные поля поиска");
            if (editorForm != null)
                editorForm.Enabled = false;
            if (decimal.Parse(idInMainBd) > -1)
                this.Text = "Склад. Клиент: " + mainForm.basa.BdReadOne("surname", idInMainBd);


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddZip_Click(object sender, EventArgs e)
        {
            StockAddPosition sad1 = new StockAddPosition(mainForm, this);
            sad1.Show(mainForm);
        }

        private void StockListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            StockListView.Sorting = SortOrder.Descending;
        }

        private void StockListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                // добавляем новый элемент в коллекцию
                e.Item = new ListViewItem(ZIP1[e.ItemIndex].idOf);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].naimenovanie);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].colour);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].brand);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].model);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].countOf);
                e.Item.SubItems.Add(ZIP1[e.ItemIndex].price);

                // Если индекс в границах нашей коллекции
                if (e.ItemIndex >= 0 && e.ItemIndex < ZIP1.Count)
                {
                    //Красим в цвет записи, которые используются клиентом
                    if (dtableSearchConcidence(idInMainBd, ZIP1[e.ItemIndex].idOf))
                    {
                        e.Item.BackColor = backOfColour;
                    }
                    else if (TemporaryBase.Poloski)
                    {
                        //Черезстрочная 
                        if (e.ItemIndex % 2 == 0)
                            e.Item.BackColor = Color.FromArgb(240, 240, 240);
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }
        // Оптимизация, чтоыбы каждый раз не дёргать базу, проверят в заранее подругженной дататаблице, была ли запись или нет (ставили ли запчасть клиенту)
        private bool dtableSearchConcidence(string idInMainBd, string idOfZIP)
        {
            if (dtMap1 != null)
            {
                for (int i = 0; i < dtMap1.Rows.Count; i++)
                {
                    if (dtMap1.Rows[i].ItemArray[1].ToString() == idInMainBd && dtMap1.Rows[i].ItemArray[2].ToString() == idOfZIP)
                        return true;
                }
                return false;
            }
            else
                return false;

        }

        private void SearchInStock_Click(object sender, EventArgs e)
        {
            StockFullSearch();


        }
        //Поиск по складу
        public void StockFullSearch()
        {
            try
            {
                dtMap1 = mainForm.basa.BdStockMapZIPUsedCheckOptimised();
                StockListView.Items.Clear();
                ZIP1.Clear();

                DataTable dtStock1;
                dtStock1 = mainForm.basa.BdStockFullSearch(StockSearchTextBox.Text, KategoryComboBox.Text, PodKategoryComboBox.Text, ColourComboBox.Text, BrandComboBox.Text, ModelTextBox.Text, "", "");
                for (int i = 0; i < dtStock1.Rows.Count; i++)
                {
                    ZIP zip = new ZIP(dtStock1.Rows[i].ItemArray[1].ToString(), dtStock1.Rows[i].ItemArray[4].ToString(), dtStock1.Rows[i].ItemArray[5].ToString(),
                        dtStock1.Rows[i].ItemArray[6].ToString(), dtStock1.Rows[i].ItemArray[7].ToString(), dtStock1.Rows[i].ItemArray[8].ToString(), dtStock1.Rows[i].ItemArray[0].ToString(), dtStock1.Rows[i].ItemArray[10].ToString(),
                        dtStock1.Rows[i].ItemArray[2].ToString(), dtStock1.Rows[i].ItemArray[3].ToString(), dtStock1.Rows[i].ItemArray[9].ToString(), dtStock1.Rows[i].ItemArray[11].ToString(), dtStock1.Rows[i].ItemArray[12].ToString(), dtStock1.Rows[i].ItemArray[13].ToString());
                    ZIP1.Add(zip); // Добавляем в сиписок
                }
                StockListView.VirtualListSize = ZIP1.Count;

                if (StockListView.VirtualListSize > 0)
                {
                    StockListView.VirtualListSize = StockListView.VirtualListSize - 1;
                    StockListView.VirtualListSize = StockListView.VirtualListSize + 1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        //Поиск по складу
        private void StockFullSearch(string buy)
        {
            try
            {
                dtMap1 = mainForm.basa.BdStockMapZIPUsedCheckOptimised();
                StockListView.Items.Clear();
                ZIP1.Clear();

                DataTable dtStock1;
                dtStock1 = mainForm.basa.BdStockFullSearch("", "", "", "", "", "", "", "");
                for (int i = 0; i < dtStock1.Rows.Count; i++)
                {
                    ZIP zip = new ZIP(dtStock1.Rows[i].ItemArray[1].ToString(), dtStock1.Rows[i].ItemArray[4].ToString(), dtStock1.Rows[i].ItemArray[5].ToString(),
                        dtStock1.Rows[i].ItemArray[6].ToString(), dtStock1.Rows[i].ItemArray[7].ToString(), dtStock1.Rows[i].ItemArray[8].ToString(), dtStock1.Rows[i].ItemArray[0].ToString(), dtStock1.Rows[i].ItemArray[10].ToString(),
                        dtStock1.Rows[i].ItemArray[2].ToString(), dtStock1.Rows[i].ItemArray[3].ToString(), dtStock1.Rows[i].ItemArray[9].ToString(), dtStock1.Rows[i].ItemArray[11].ToString(), dtStock1.Rows[i].ItemArray[12].ToString(), dtStock1.Rows[i].ItemArray[13].ToString());
                    if (zip.napominanie != "0")
                    {
                        if (decimal.Parse(zip.countOf) < decimal.Parse(zip.napominanie))
                        {
                            ZIP1.Add(zip); // Добавляем в сиписок
                        }
                    }

                }
                StockListView.VirtualListSize = ZIP1.Count;

                if (StockListView.VirtualListSize > 0)
                {
                    StockListView.VirtualListSize = StockListView.VirtualListSize - 1;
                    StockListView.VirtualListSize = StockListView.VirtualListSize + 1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        //Поиск по складу
        private void StockFullSearchClientUsed()
        {
            try
            {
                dtMap1 = mainForm.basa.BdStockMapZIPUsedCheckOptimised();
                StockListView.Items.Clear();
                ZIP1.Clear();

                DataTable dtStock1;
                dtStock1 = mainForm.basa.BdStockFullSearch("", "", "", "", "", "", "", "");
                for (int i = 0; i < dtStock1.Rows.Count; i++)
                {
                    ZIP zip = new ZIP(dtStock1.Rows[i].ItemArray[1].ToString(), dtStock1.Rows[i].ItemArray[4].ToString(), dtStock1.Rows[i].ItemArray[5].ToString(),
                        dtStock1.Rows[i].ItemArray[6].ToString(), dtStock1.Rows[i].ItemArray[7].ToString(), dtStock1.Rows[i].ItemArray[8].ToString(), dtStock1.Rows[i].ItemArray[0].ToString(), dtStock1.Rows[i].ItemArray[10].ToString(),
                        dtStock1.Rows[i].ItemArray[2].ToString(), dtStock1.Rows[i].ItemArray[3].ToString(), dtStock1.Rows[i].ItemArray[9].ToString(), dtStock1.Rows[i].ItemArray[11].ToString(), dtStock1.Rows[i].ItemArray[12].ToString(), dtStock1.Rows[i].ItemArray[13].ToString());
                    if (dtableSearchConcidence(idInMainBd, zip.idOf))
                    {
                        ZIP1.Add(zip); // Добавляем в сиписок
                    }

                }
                StockListView.VirtualListSize = ZIP1.Count;

                if (StockListView.VirtualListSize > 0)
                {
                    StockListView.VirtualListSize = StockListView.VirtualListSize - 1;
                    StockListView.VirtualListSize = StockListView.VirtualListSize + 1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }


        private void StockSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void StockListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Открытаваем редактор записи
            if (ZIP1.Count > 0)
            {
                string id_zapisi = StockListView.Items[StockListView.SelectedIndices[0]].SubItems[0].Text;
                StockEditor stEd = new StockEditor(mainForm, id_zapisi, this, idInMainBd, editorForm, client_id_base);
                stEd.Show(mainForm);
            }
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {

        }

        private void BuyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ClientUsedCheckBox.Checked = false;
            if (BuyCheckBox.Checked)
            {
                // Запускаем перегрузку функции StockFullSearch, для поиска по тем записям, которых осталось мало
                StockFullSearch("buy");
            }
            else
                StockFullSearch();

        }

        private void Stock_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Left > -10000 && this.Top > -10000)
            {
                INIF.WriteINI(TemporaryBase.UserKey, "StockPosition", "1");
                INIF.WriteINI(TemporaryBase.UserKey, "STfLeft", this.Left.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "STfTop", this.Top.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "STfWidth", this.Width.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "STfHeight", this.Height.ToString());
            }
            if (editorForm != null)
            {
                editorForm.Enabled = true;
                editorForm.WindowState = FormWindowState.Normal;
            }



        }

        private void KategoryComboBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void ModelTextBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void PodKategoryComboBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void ColourComboBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void BrandComboBox_TextChanged(object sender, EventArgs e)
        {
            StockFullSearch();
        }

        private void ClientUsedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BuyCheckBox.Checked = false;
            if (ClientUsedCheckBox.Checked)
            {
                // Запускаем перегрузку функции StockFullSearch, для поиска по тем записям, которых осталось мало
                StockFullSearchClientUsed();
            }
            else StockFullSearch();

        }
    }
}
