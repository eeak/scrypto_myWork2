using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class ClientsEditor : Form
    {
        Form1 mainForm;
        ItemComparerClients itemComparerClients;
        public List<KlientBase> ClientsList = new List<KlientBase>();
        public ClientsEditor(Form1 mf)
        {
            InitializeComponent();
            mainForm = mf;
            try
            {

                //lview сортировка
                itemComparerClients = new ItemComparerClients(this);
                ClientsListView.ListViewItemSorter = itemComparerClients;
                ClientsListView.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ServiceAdressComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Указываем сортируемую колонку
                itemComparerClients.ColumnIndex = e.Column;
                //   MainListView.Items.Clear();
                ClientsListView.VirtualListSize = ClientsList.Count;
                //Да, каждый раз через жопу, не баг а фича!!! Пока не разобрался, как заставить перерисовывать лист вью в виртуальном режиме, 
                //если количество итемов не поменялось.
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize - 1;
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize + 1;
                //  MainListView.RedrawItems(TemporaryBase.startIndex, TemporaryBase.endIndex, false);


            }
            catch
            {
                MessageBox.Show("Нечего сортировать");
            }
        }

        private void ClientsEditor_Load(object sender, EventArgs e)
        {
            AddZip.Enabled = (TemporaryBase.clientAdd == "1") ? true : false;
            DeleteClientButton.Enabled = (TemporaryBase.clientDel == "1") ? true : false;
            ClitenToClientButton.Enabled = (TemporaryBase.clientConcat == "1") ? true : false;
            ClientEditorStartLoad();
        }

        private void ClientEditorStartLoad()
        {
            ClientsListView.Items.Clear();
            ClientsList.Clear();
            DataTable dt2 = mainForm.basa.ClientsAllMapGiver();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ClientsList.Add(new KlientBase(dt2.Rows[i].ItemArray[0].ToString(),
                    dt2.Rows[i].ItemArray[1].ToString(),
                    dt2.Rows[i].ItemArray[2].ToString(),
                    dt2.Rows[i].ItemArray[3].ToString(),
                    dt2.Rows[i].ItemArray[4].ToString(),
                    KlientBlistDecoder(dt2.Rows[i].ItemArray[5].ToString()),
                    dt2.Rows[i].ItemArray[6].ToString(),
                    dt2.Rows[i].ItemArray[7].ToString()
                    ));
            }
            ClientsListView.VirtualListSize = ClientsList.Count;

            if (ClientsListView.VirtualListSize > 0)
            {
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize - 1;
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize + 1;
            }
        }

        private string KlientBlistDecoder(string blist)
        {
            if (blist == "0") return "Не проблемный";
            else if (blist == "1") return "Проблемный";
            else return "";

        }
        private void ClientsListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                // добавляем новый элемент в коллекцию
                e.Item = new ListViewItem(ClientsList[e.ItemIndex].id);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].FIO);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].Phone);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].Adress);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].AboutUs);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].Blist);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].Primechanie);
                e.Item.SubItems.Add(ClientsList[e.ItemIndex].Date);

                // Если индекс в границах нашей коллекции
                if (e.ItemIndex >= 0 && e.ItemIndex < ClientsList.Count)
                {
                    //Если клиент в чёрном списке
                    if (ClientsList[e.ItemIndex].Blist == "Проблемный")
                    {
                        if (TemporaryBase.BlistColor != "")
                        {
                            e.Item.BackColor = Color.FromArgb(int.Parse(TemporaryBase.BlistColor));
                        }

                        else
                            e.Item.BackColor = Color.Yellow;
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

        private void AddZip_Click(object sender, EventArgs e)
        {
            ClientAddForm caf1 = new ClientAddForm(mainForm, this);
            caf1.Show(mainForm);
        }

        private void DeleteClientButton_Click(object sender, EventArgs e)
        {
            if (ClientsListView.SelectedIndices.Count > 0)
                if (MessageBox.Show("Вы действительно хотите удалить клиента" + Environment.NewLine + ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[1].Text + Environment.NewLine +
                    "Вместе с клиентом будут удалены все записи, о ремонтированной им технике", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.ClientsMapDelete(ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text);
                    mainForm.basa.ClientsMapZapisiDelete(ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text);
                    ClientEditorStartLoad();
                    TemporaryBase.SearchFULLBegin();
                }
        }

        private void ClientFIOSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchClient();
        }

        public void SearchClient()
        {
            ClientsListView.Items.Clear();
            ClientsList.Clear();
            DataTable dt2 = mainForm.basa.ClientsFIOPhoneSearch(ClientFIOSearchTextBox.Text, ClientPhoneSearchTextBox.Text);

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ClientsList.Add(new KlientBase(dt2.Rows[i].ItemArray[0].ToString(),
                    dt2.Rows[i].ItemArray[1].ToString(),
                    dt2.Rows[i].ItemArray[2].ToString(),
                    dt2.Rows[i].ItemArray[3].ToString(),
                    dt2.Rows[i].ItemArray[4].ToString(),
                    KlientBlistDecoder(dt2.Rows[i].ItemArray[5].ToString()),
                    dt2.Rows[i].ItemArray[6].ToString(),
                    dt2.Rows[i].ItemArray[7].ToString()
                    ));
            }
            ClientsListView.VirtualListSize = ClientsList.Count;

            if (ClientsListView.VirtualListSize > 0)
            {
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize - 1;
                ClientsListView.VirtualListSize = ClientsListView.VirtualListSize + 1;
            }
        }

        private void ClientPhoneSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchClient();
        }
        int firstClient = -1;
        int secondClient = -1;
        private void ClitenToClientButton_Click(object sender, EventArgs e)
        {
            if (ClientsListView.SelectedIndices.Count > 0)
            {

                if (firstClient == -1)
                {
                    if (MessageBox.Show("Теперь выберите второго клиента, записи певрого клиента, станут записями второго клиента" + Environment.NewLine + "После выбора клиента, еще раз нажмите эту кнопку, для объединения", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        firstClient = int.Parse(ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text);
                    }

                }
                else
                {
                    secondClient = int.Parse(ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text);
                    if (MessageBox.Show("Вы действительно хотите объединить клиентов? " + Environment.NewLine +
                       "Первый клиент " + firstClient.ToString() + Environment.NewLine +
                       "Второй клиент " + secondClient.ToString(), "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        mainForm.basa.ClientsMapDelete(firstClient.ToString());
                        mainForm.basa.ClientsToClitens(firstClient.ToString(), secondClient.ToString());
                        SearchClient();
                        TemporaryBase.SearchFULLBegin();
                        firstClient = -1;
                        secondClient = -1;
                    }
                }

            }
            else
                MessageBox.Show("Для начала выберите клиента, которого хотите объединить, все записи этого клиента перейдут ко второму клиенту");

        }

        private void ClientsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClientsListView.SelectedIndices.Count > 0)
            {
                this.Text = "В данный момент выбран клиент " + ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[1].Text;
            }
            ClientsHistoryMaker();
        }
        public void MyMethod(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            Editor ed1 = new Editor(mainForm, lb.Tag.ToString());
            ed1.Show(mainForm);
        }
        // История ремонта
        public void ClientsHistoryMaker()
        {
            if (ClientsListView.SelectedIndices.Count > 0)
            {
                DataTable dtHistory = mainForm.basa.ClientsShowHistory(ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text);
                int left = 3;
                int top = 3;
                int width = panel1.Width - left * 2 - 2;
                List<Label> labelList1 = new List<Label>();

                for (int i = 0; i < dtHistory.Rows.Count; i++)
                {

                    string StatusZapisi = "";
                    Label statusLable = new Label();
                    if (dtHistory.Rows[i].ItemArray[20].ToString() == "Выдан")
                        StatusZapisi = "Выдан " + dtHistory.Rows[i].ItemArray[2].ToString();
                    else
                        StatusZapisi = dtHistory.Rows[i].ItemArray[20].ToString();
                    statusLable.Text = dtHistory.Rows[i].ItemArray[1].ToString() + Environment.NewLine + dtHistory.Rows[i].ItemArray[7].ToString() + Environment.NewLine + dtHistory.Rows[i].ItemArray[8].ToString() +
                        Environment.NewLine + dtHistory.Rows[i].ItemArray[9].ToString() + Environment.NewLine + "Цена ремонта: " + dtHistory.Rows[i].ItemArray[18].ToString() +
                        Environment.NewLine + "Скидка: " + dtHistory.Rows[i].ItemArray[19].ToString() + Environment.NewLine + StatusZapisi;
                    statusLable.BorderStyle = BorderStyle.FixedSingle;
                    statusLable.Location = new Point(left, top);
                    statusLable.BackColor = Color.White;
                    statusLable.AutoSize = true;
                    statusLable.MinimumSize = new System.Drawing.Size(width, 107);
                    top = top + 110;
                    statusLable.Font = new Font("Arial", 9, FontStyle.Bold);
                    statusLable.Tag = dtHistory.Rows[i].ItemArray[0].ToString();
                    labelList1.Add(statusLable);
                }
                foreach (Label sLabel in labelList1)
                {
                    if (top > panel1.Height)
                    {
                        sLabel.MinimumSize = new System.Drawing.Size(width - 18, 107);
                    }
                }
                panel1.Controls.Clear();
                ToolTip t = new ToolTip();
                t.AutoPopDelay = 30000;
                int x = 0;
                foreach (Label label in labelList1)
                {
                    label.MouseClick += MyMethod;
                    this.panel1.Controls.Add(label);
                    t.SetToolTip(label, "Адрес СЦ: " + dtHistory.Rows[x].ItemArray[27].ToString() + Environment.NewLine +
                        "Выполненные работы: " + dtHistory.Rows[x].ItemArray[22].ToString() + Environment.NewLine +
                         "Мастер: " + dtHistory.Rows[x].ItemArray[21].ToString() + Environment.NewLine +
                         "Поломка: " + dtHistory.Rows[x].ItemArray[13].ToString() + Environment.NewLine +
                         "Комментарий: " + dtHistory.Rows[x].ItemArray[14].ToString() + Environment.NewLine);
                    x++;
                }

            }

        }

        private void ClientsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ClientsListView.SelectedIndices.Count > 0)
            {

                ClientEditorTrue cet1 = new ClientEditorTrue(mainForm, ClientsListView.Items[ClientsListView.SelectedIndices[0]].SubItems[0].Text, this);
                cet1.Show(mainForm);
            }
        }
    }
}
