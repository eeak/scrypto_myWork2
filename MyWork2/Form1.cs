using MyWork2.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace MyWork2
{
    public partial class Form1 : Form
    {
        // Для виртуального listView
        public List<VirtualClient> VCList = new List<VirtualClient>();
        //Ини файл
        IniFile INIF = new IniFile("Config.ini");


        // Для картинок в listbox
        ImageList imageListSmall = new ImageList();

        //Класс работы с бд
        public BDWorker basa;

        //Работа с сортировкой в MainListView
        ItemComparer itemComparer;
        //Число  дней, для выделения строки в listView при просрочке диагностики

        int daysDiagnostik = -4;
        //Оптимизация, выносим из цикла определение цвета записи.

        string ServArd = "";



        //В данный момент выделенная строка окна listview
        public int lviewSelectedIndex = 1;
        public void lviewReturnSelectedIndex()
        {
            MainListView.Items[lviewSelectedIndex].Selected = true;
            MainListView.Select();
        }
        public Form1()
        {
            TemporaryBase.UserKey = Registration.getHDD();
            basa = new BDWorker(this);
            InitializeComponent();
            ServiceAdressComboBox.ComboBox.MouseWheel += new MouseEventHandler(ToolStripComboBox_MouseWheel);


            MainListView.KeyDown += new KeyEventHandler(Program_KeyDown); // привязываем обработчик нажатий на кнопки
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorDiagnostik"))
                TemporaryBase.backOfColour = Color.FromArgb(int.Parse(INIF.ReadINI("PROGRAMM_SETTINGS", "colorDiagnostik")));
            // Чтобы нельзя было случайно пролистнуть комбобокс
            //this.ServiceAdressComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ServiceAdressComboBox_MouseWheel);
            // Восстанавливаем ширину колонок
            for (int i = 0; i < MainListView.Columns.Count; i++)
            {
                if (INIF.KeyExists(TemporaryBase.UserKey, i.ToString()))
                {
                    try { MainListView.Columns[i].Width = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, i.ToString())); }
                    catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
                }
            }
            //Задаем интервал дней до критичекской диагностики
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "daysDiagnostik"))
            {
                daysDiagnostik = int.Parse(INIF.ReadINI("PROGRAMM_SETTINGS", "daysDiagnostik"));
            }

            // Восстанавливаем расположение колонок
            MainListViewColumnIndexWriter();

            //Выводит окно регистрации, если программа не зарегистрированна
            if (TemporaryBase.baseR == true)
            {
                if (!Registration.deHash(INIF.ReadINI("ACTIVATION", TemporaryBase.UserKey), TemporaryBase.UserKey))
                {
                    this.Enabled = false;
                    Registration reg = new Registration(this);
                    reg.Show();
                }
            }

            try
            {
                // Добавляем картинки в коллекцию mainListView
                imageListSmall.Images.Add(Properties.Resources.phone);
                //     imageListSmall.Images.Add(Image.FromFile(Application.StartupPath.ToString() + "\\pic\\" + "Solglasovano.png"));
                //     imageListSmall.Images.Add(Bitmap.FromFile(Application.StartupPath.ToString() + "\\pic\\" + "Gotovo.png"));
                MainListView.SmallImageList = imageListSmall;
                //lview сортировка
                itemComparer = new ItemComparer(this);
                MainListView.ListViewItemSorter = itemComparer;
                MainListView.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToolStripComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            //Cast the MouseEventArgs to HandledMouseEventArgs
            HandledMouseEventArgs mwe = (HandledMouseEventArgs)e;

            //Indicate that this event was handled
            //(prevents the event from being sent to its parent control)
            if (!this.ServiceAdressComboBox.DroppedDown)
                mwe.Handled = true;
        }
        //Горячие клавиши
        void Program_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F12:
                    {
                        AddPosition adp = new AddPosition(this);
                        adp.Show(this);
                        break;
                    }

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
                itemComparer.ColumnIndex = e.Column;
                //   MainListView.Items.Clear();
                MainListView.VirtualListSize = VCList.Count;
                //Да, каждый раз через жопу, не баг а фича!!! Пока не разобрался, как заставить перерисовывать лист вью в виртуальном режиме, 
                //если количество итемов не поменялось.
                MainListView.VirtualListSize = MainListView.VirtualListSize - 1;
                MainListView.VirtualListSize = MainListView.VirtualListSize + 1;
                //  MainListView.RedrawItems(TemporaryBase.startIndex, TemporaryBase.endIndex, false);


            }
            catch
            {
                MessageBox.Show("Нечего сортировать");
            }
        }

        //Добавление данных по поиску в темббейз
        public void TempBaseUpdateSearch(string FIO, bool SearchInOld, string Phone = "", string TypeOf = "", string Brand = "",
            string Model = "", string Status = "", string Master = "", string NeedZakaz = "")
        {
            TemporaryBase.FIO = FIO;
            TemporaryBase.SearchInOld = SearchInOld;
            TemporaryBase.Phone = Phone;
            TemporaryBase.TypeOf = TypeOf;
            TemporaryBase.Brand = Brand;
            TemporaryBase.Model = Model;
            TemporaryBase.Status = Status;
            TemporaryBase.Master = Master;
            TemporaryBase.NeedZakaz = NeedZakaz;
        }
        // Поиск из основного окна

        // Переменная создания формы добавления новой записи (чтобы не открывалась вторая)
        public bool adPos = false;
        private void AddPositionButton_Click(object sender, EventArgs e)
        {

            if (adPos)
                return;

            else
            {
                AddPosition ap1;
                ap1 = new AddPosition(this);
                ap1.Show(this);
            }

        }

        private void MainListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            MainListView.Sorting = SortOrder.Descending;
        }

        private void MainListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            int x = MainListView.Columns[e.ColumnIndex].Width;
            INIF.WriteINI(TemporaryBase.UserKey, e.ColumnIndex.ToString(), x.ToString());
        }

        private void SearchFullButton_Click(object sender, EventArgs e)
        {

            ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);

            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;

            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            FullSearchBrand.Text = "";
            FullSearchMaster.Text = "";
            FullSearchModel.Text = "";
            FullSearchPhone.Text = "";
            FullSearchSerial.Text = "";
            FullSearchType.Text = "";

            if (toolStrip3.Visible == false)
            {
                toolStrip3.Visible = true;
                MainListView.Location = new Point(0, 79);

            }
            else
            {
                MainListView.Location = new Point(0, 54);
                toolStrip3.Visible = false;
            }

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedItems.Count > 0)
            {
                string id_zapisi = MainListView.SelectedItems[0].Text;
                StatusStripLabel.Text = "Редактирование записи номер: " + id_zapisi;
                Editor ed1 = new Editor(this, id_zapisi);
                ed1.Show();
            }
            else
            {
                MessageBox.Show("Не выбрана запись для редактирования");
            }
        }

        //Вбиваем данные во все комбобоксы
        public void ComboboxMaker()
        {
            try
            {
                StreamReader sr = new StreamReader(@"settings/AdresSC.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaAdressSc.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaAdressSc)
            {
                ServiceAdressComboBox.ComboBox.Invoke(new Action(() => { ServiceAdressComboBox.Items.Add(strCombo); }));
            }
            if (TemporaryBase.AdressSCDefault.ToString() != "")
            {
                if (ServiceAdressComboBox.Items.Count > decimal.Parse(TemporaryBase.AdressSCDefault.ToString()))
                {
                    ServiceAdressComboBox.ComboBox.Invoke(new Action(() => { ServiceAdressComboBox.ComboBox.SelectedIndex = int.Parse(TemporaryBase.AdressSCDefault.ToString()); }));
                }

            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/sostoyaniePriema.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaSostoyanie.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/komplektonst.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaKomplektnost.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/neispravnost.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaNeispravnost.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/masters.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaMasters.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }

            try
            {
                StreamReader sr = new StreamReader(@"settings/DeviceColour.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaColour.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader("settings/vipolnRaboti.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaVipolnRaboti.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/Garanty.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaGaranty.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/Stock/ustrojstvo.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaStockUstrojstvo.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/Stock/TypeOvZIP.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaStockPodkategory.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/Stock/brands.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaStockBrands.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(@"settings/Stock/DeviceColour.txt", Encoding.Default);
                String line = sr.ReadLine();

                while (line != null)
                {
                    TemporaryBase.SortirovkaStockDeviceColour.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сортировки списков", ex.Message);
            }

            foreach (string strCombo in TemporaryBase.SortirovkaMasters)
            {
                FullSearchMaster.ComboBox.Invoke(new Action(() => { FullSearchMaster.ComboBox.Items.Add(strCombo); }));
            }

            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaBrands)
            {
                FullSearchBrand.ComboBox.Invoke(new Action(() => { FullSearchBrand.Items.Add(ssp.SortObj); }));
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaUstrojstvo)
            {
                FullSearchType.ComboBox.Invoke(new Action(() => { FullSearchType.Items.Add(ssp.SortObj); }));
            }



        }
        private void Form1_Load(object sender, EventArgs e)
        {
            basa.CreateBd();
            basa.UsersTable_Create(); //Если нет, то создаём
            DataTable usersDt = basa.UsersBdRead();
            if (usersDt.Rows.Count > 0)
            {
                this.Enabled = false;
                Authorisation au1 = new Authorisation(this);
                au1.Show(this);
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "backupPath"))
            {
                string ptobackup = INIF.ReadINI(TemporaryBase.UserKey, "backupPath");
                if (System.IO.Directory.Exists(ptobackup))
                {
                    TemporaryBase.pathtoSaveBD = ptobackup;
                }
            }
            //подгружаем путь сохранения бэкапов бд

            //Настройка цвета кнопок фильтра поиска
            ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);
            AllOrdersButton.BackColor = Color.FromArgb(179, 215, 243);
            //Штрихкод
            if (INIF.KeyExists("ACTS", "BarcodeH") && INIF.KeyExists("ACTS", "BarcodeW"))
            {
                TemporaryBase.barcodeH = int.Parse(INIF.ReadINI("ACTS", "BarcodeH"));
                TemporaryBase.barcodeW = int.Parse(INIF.ReadINI("ACTS", "BarcodeW"));
            }
            if (INIF.KeyExists("CHECKBOX", "EveryDayBackup"))
            {
                TemporaryBase.everyDayBackup = INIF.ReadINI("CHECKBOX", "EveryDayBackup");
            }
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "Mpersent"))
            {
                TemporaryBase.Mpersent = INIF.ReadINI("PROGRAMM_SETTINGS", "Mpersent");
            }

            if (INIF.KeyExists(TemporaryBase.UserKey, "BlistColor"))
                TemporaryBase.BlistColor = INIF.ReadINI(TemporaryBase.UserKey, "BlistColor");
            if (INIF.KeyExists(TemporaryBase.UserKey, "AdressSCDefault"))
                TemporaryBase.AdressSCDefault = INIF.ReadINI(TemporaryBase.UserKey, "AdressSCDefault");

            if (INIF.KeyExists(TemporaryBase.UserKey, "MasterDefault"))
                TemporaryBase.MasterDefault = INIF.ReadINI(TemporaryBase.UserKey, "MasterDefault");
            //Передаем ссылку на эту форму в статический класс
            TemporaryBase.mainForm = this;

            //Обязательные поля
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "NessesaryCHB"))
            {
                if (INIF.ReadINI("PROGRAMM_SETTINGS", "NessesaryCHB") == "Unchecked")
                {
                    TemporaryBase.Nessesary = false;
                }
                else
                    TemporaryBase.Nessesary = true;
            }


            //смс
            if (INIF.KeyExists("SMSSEND", "Token"))
                TemporaryBase.smsToken = INIF.ReadINI("SMSSEND", "Token");
            if (INIF.KeyExists("SMSSEND", "PhoneId"))
                TemporaryBase.smsPhoneId = INIF.ReadINI("SMSSEND", "PhoneId");
            if (INIF.KeyExists("SMSSEND", "ReadyText"))
                TemporaryBase.smsTextGotov = INIF.ReadINI("SMSSEND", "ReadyText");
            if (INIF.KeyExists("SMSSEND", "SoglasovanText"))
                TemporaryBase.smsTextSoglasovat = INIF.ReadINI("SMSSEND", "SoglasovanText");
            if (INIF.KeyExists("SMSSEND", "ShablonText"))
                TemporaryBase.smsTextShablon = INIF.ReadINI("SMSSEND", "ShablonText");
            if (INIF.KeyExists("SMSSEND", "PrivetText"))
                TemporaryBase.smsTextPrivet = INIF.ReadINI("SMSSEND", "PrivetText");
            if (INIF.KeyExists("SMSSEND", "KodCountry"))
                TemporaryBase.smsPhone = INIF.ReadINI("SMSSEND", "KodCountry");
            if (INIF.KeyExists("SMSSEND", "KodCountryZamena"))
                TemporaryBase.smsPhonePref = INIF.ReadINI("SMSSEND", "KodCountryZamena");

            FilesExistsOrNot();
            // Всплывающие подсказки
            ToolTip t = new ToolTip();
            t.SetToolTip(SearchFIOTextBox, "Нажмите Enter для поиска");
            Thread treadCombo = new Thread(new ThreadStart(ComboboxMaker));
            treadCombo.Start();
            //Подгружаем значение в comboBox
            //  ComboboxMaker();

            VipadSpiskiObnovit();

            //Валюта
            //Восстанавливаем валюту
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "valuta"))
            {
                TemporaryBase.valuta = INIF.ReadINI("PROGRAMM_SETTINGS", "valuta");
            }
            else
            {
                TemporaryBase.valuta = "Рублей";
                INIF.WriteINI("PROGRAMM_SETTINGS", "valuta", "Рублей");
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "MainFormPosition"))
            {

                try
                {
                    this.Width = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "MfWidth"));
                    this.Height = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "MfHeight"));
                    this.Left = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "MfLeft"));
                    this.Top = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "MfTop"));

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
            //Генератор штрихкодов
            if (!INIF.KeyExists("BARCODE", "Generator"))
            {
                basa.bdBarcodeAllGenerator();
                INIF.WriteINI("BARCODE", "Generator", "DONE");
            }

            if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorCheckBox"))
            {
                if (INIF.ReadINI("PROGRAMM_SETTINGS", "colorCheckBox") == "Checked")
                {
                    if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorDiagnostik"))
                    {
                        TemporaryBase.diagnostika = true;
                    }
                }
            }
            // poloski
            if (INIF.KeyExists(TemporaryBase.UserKey, "Poloski"))
            {
                if (INIF.ReadINI(TemporaryBase.UserKey, "Poloski") == "Unchecked")
                {
                    TemporaryBase.Poloski = false;
                }
                else
                    TemporaryBase.Poloski = true;
            }

            if (INIF.KeyExists(TemporaryBase.UserKey, "openClientFolder"))
            {
                TemporaryBase.openClientFolder = (INIF.ReadINI(TemporaryBase.UserKey, "openClientFolder") == "Unchecked") ? false : true;
            }
            TemporaryBase.SearchFULLBegin();
            basa.StatesMapTable_Create();
            //Проверяем обновления
            if (TemporaryBase.baseR)
            {
                Thread tr1 = new Thread(new ThreadStart(CheckUpdates));
                tr1.Start();
            }

            // Имя фирмы в заголовок
            this.Text += File.ReadAllText("Settings/Akts/FirmName.txt").Replace("<br>", "");
            if (!Directory.Exists("ClientFiles"))
                System.IO.Directory.CreateDirectory("ClientFiles");

            // Адрес сервиса для обновления списков
            ServArd = ServiceAdressComboBox.Text;

            if (INIF.KeyExists("CHECKBOX", "garantyDefault"))
            {
                if (INIF.ReadINI("CHECKBOX", "garantyDefault") == "Checked")
                    TemporaryBase.EditorGarantyComboboxVal = "Без гарантии";
            }
        }

        public void VipadSpiskiObnovit()
        {
            //Сортировка брендов
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServiceAdressComboBox.Text);
                List<string> brandS = new List<string>();
                List<string> brndsSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].Brand != "")
                        {
                            brandS.Add(vClientList[i].Brand);
                        }

                    }
                    for (int v = 0; v < brandS.Count; v++)
                    {
                        bool addMe = true;
                        if (brndsSVariants.Count == 0)
                        {
                            brndsSVariants.Add(brandS[v]);
                        }
                        for (int z = 0; z < brndsSVariants.Count; z++)
                        {
                            if (brndsSVariants[z] == brandS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            brndsSVariants.Add(brandS[v]);
                        }
                    }

                }
                int[] arr = new int[brndsSVariants.Count];
                for (int zi = 0; zi < brndsSVariants.Count; zi++)
                {
                    for (int i = 0; i < brandS.Count; i++)
                    {
                        if (brndsSVariants[zi] == brandS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < brndsSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], brndsSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaBrands = vals;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Сортировка типа устройств
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServiceAdressComboBox.Text);
                List<string> typeS = new List<string>();
                List<string> typesSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].WhatRemont != "")
                        {
                            typeS.Add(vClientList[i].WhatRemont);
                        }

                    }
                    for (int v = 0; v < typeS.Count; v++)
                    {
                        bool addMe = true;
                        if (typesSVariants.Count == 0)
                        {
                            typesSVariants.Add(typeS[v]);
                        }
                        for (int z = 0; z < typesSVariants.Count; z++)
                        {
                            if (typesSVariants[z] == typeS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            typesSVariants.Add(typeS[v]);
                        }
                    }

                }
                int[] arr = new int[typesSVariants.Count];
                for (int zi = 0; zi < typesSVariants.Count; zi++)
                {
                    for (int i = 0; i < typeS.Count; i++)
                    {
                        if (typesSVariants[zi] == typeS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < typesSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], typesSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaUstrojstvo = vals;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Сортировка откуда о нас узнали
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServiceAdressComboBox.Text);
                List<string> aboutuS = new List<string>();
                List<string> aboutsSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].AboutUs != "")
                        {
                            aboutuS.Add(vClientList[i].AboutUs);
                        }

                    }
                    for (int v = 0; v < aboutuS.Count; v++)
                    {
                        bool addMe = true;
                        if (aboutsSVariants.Count == 0)
                        {
                            aboutsSVariants.Add(aboutuS[v]);
                        }
                        for (int z = 0; z < aboutsSVariants.Count; z++)
                        {
                            if (aboutsSVariants[z] == aboutuS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            aboutsSVariants.Add(aboutuS[v]);
                        }
                    }

                }
                int[] arr = new int[aboutsSVariants.Count];
                for (int zi = 0; zi < aboutsSVariants.Count; zi++)
                {
                    for (int i = 0; i < aboutuS.Count; i++)
                    {
                        if (aboutsSVariants[zi] == aboutuS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < aboutsSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], aboutsSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaAboutUs = vals;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void VipadSpiskiObnovitPotok()
        {
            //Сортировка брендов
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServArd);
                List<string> brandS = new List<string>();
                List<string> brndsSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].Brand != "")
                        {
                            brandS.Add(vClientList[i].Brand);
                        }

                    }
                    for (int v = 0; v < brandS.Count; v++)
                    {
                        bool addMe = true;
                        if (brndsSVariants.Count == 0)
                        {
                            brndsSVariants.Add(brandS[v]);
                        }
                        for (int z = 0; z < brndsSVariants.Count; z++)
                        {
                            if (brndsSVariants[z] == brandS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            brndsSVariants.Add(brandS[v]);
                        }
                    }

                }
                int[] arr = new int[brndsSVariants.Count];
                for (int zi = 0; zi < brndsSVariants.Count; zi++)
                {
                    for (int i = 0; i < brandS.Count; i++)
                    {
                        if (brndsSVariants[zi] == brandS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < brndsSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], brndsSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaBrands = vals;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Сортировка типа устройств
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServArd);
                List<string> typeS = new List<string>();
                List<string> typesSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].WhatRemont != "")
                        {
                            typeS.Add(vClientList[i].WhatRemont);
                        }

                    }
                    for (int v = 0; v < typeS.Count; v++)
                    {
                        bool addMe = true;
                        if (typesSVariants.Count == 0)
                        {
                            typesSVariants.Add(typeS[v]);
                        }
                        for (int z = 0; z < typesSVariants.Count; z++)
                        {
                            if (typesSVariants[z] == typeS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            typesSVariants.Add(typeS[v]);
                        }
                    }

                }
                int[] arr = new int[typesSVariants.Count];
                for (int zi = 0; zi < typesSVariants.Count; zi++)
                {
                    for (int i = 0; i < typeS.Count; i++)
                    {
                        if (typesSVariants[zi] == typeS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < typesSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], typesSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaUstrojstvo = vals;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Сортировка откуда о нас узнали
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = basa.BdReadListTechnics(ServArd);
                List<string> aboutuS = new List<string>();
                List<string> aboutsSVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].AboutUs != "")
                        {
                            aboutuS.Add(vClientList[i].AboutUs);
                        }

                    }
                    for (int v = 0; v < aboutuS.Count; v++)
                    {
                        bool addMe = true;
                        if (aboutsSVariants.Count == 0)
                        {
                            aboutsSVariants.Add(aboutuS[v]);
                        }
                        for (int z = 0; z < aboutsSVariants.Count; z++)
                        {
                            if (aboutsSVariants[z] == aboutuS[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            aboutsSVariants.Add(aboutuS[v]);
                        }
                    }

                }
                int[] arr = new int[aboutsSVariants.Count];
                for (int zi = 0; zi < aboutsSVariants.Count; zi++)
                {
                    for (int i = 0; i < aboutuS.Count; i++)
                    {
                        if (aboutsSVariants[zi] == aboutuS[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                List<SortirovkaSpiskov> vals = new List<SortirovkaSpiskov>();
                for (int i = 0; i < aboutsSVariants.Count; i++)
                {
                    SortirovkaSpiskov spsort = new SortirovkaSpiskov(arr[i], aboutsSVariants[i]);
                    vals.Add(spsort);
                }
                //Сортировка финальная
                vals.Sort((x, y) => y.CompareTo(x));
                List<string> valOne = new List<string>();
                //Делаем, чтобы записи у которых значение счета меньше 3, были в алфавитном порядке
                foreach (SortirovkaSpiskov SorSp in vals)
                {
                    if (SorSp.count <= 3)
                    {
                        valOne.Add(SorSp.SortObj);
                    }
                }
                for (int i = 0; i < vals.Count; i++)
                {
                    if (vals[i].count <= 3)
                    {
                        vals.Remove(vals[i]);
                        i--;
                    }
                }
                valOne.Sort();
                foreach (string stVals in valOne)
                {
                    SortirovkaSpiskov sspOne = new SortirovkaSpiskov(1, stVals);
                    vals.Add(sspOne);
                }
                TemporaryBase.SortirovkaAboutUs = vals;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void RulesMackerMainWindow()
        {
            AddPositionButton.Enabled = (TemporaryBase.addZapis == "1") ? true : false;
            SettingsButton.Enabled = (TemporaryBase.settings == "1") ? true : false;
            SmsStripButton.Enabled = (TemporaryBase.sms == "1") ? true : false;
            toolStripButton2.Enabled = (TemporaryBase.graf == "1") ? true : false;
            StockButton.Enabled = (TemporaryBase.stock == "1") ? true : false;
            toolStripButton3.Enabled = (TemporaryBase.clients == "1") ? true : false;
        }

        //Восстанавливает файлы программы, если их нет
        private static void FilesExistsOrNot()
        {

            //Пути к файлам
            FileInfo
                aboutUs = new FileInfo(@"settings/aboutUs.txt"),
                AdresSC = new FileInfo(@"settings/AdresSC.txt"),
                brands = new FileInfo(@"settings/brands.txt"),
                DeviceColour = new FileInfo(@"settings/DeviceColour.txt"),
                komplektonst = new FileInfo(@"settings/komplektonst.txt"),
                masters = new FileInfo(@"settings/masters.txt"),
                neispravnost = new FileInfo(@"settings/neispravnost.txt"),
                sostoyaniePriema = new FileInfo(@"settings/sostoyaniePriema.txt"),
                ustrojstvo = new FileInfo(@"settings/ustrojstvo.txt"),
                vipolnRaboti = new FileInfo(@"settings/vipolnRaboti.txt"),
                DannieOFirme = new FileInfo(@"settings/Akts/DannieOFirme.txt"),
                DogovorTextPriem = new FileInfo(@"settings/Akts/DogovorTextPriem.txt"),
                DogovorTextVidacha = new FileInfo(@"settings/Akts/DogovorTextVidacha.txt"),
                FirmName = new FileInfo(@"settings/Akts/FirmName.txt"),
                Phone = new FileInfo(@"settings/Akts/Phone.txt"),
                URDannie = new FileInfo(@"settings/Akts/URDannie.txt");

            //Проверяем наличие папки
            if (!Directory.Exists("settings"))
            {
                Directory.CreateDirectory("settings");
            }
            if (!Directory.Exists("settings/Akts"))
            {
                Directory.CreateDirectory("settings/Akts");
            }
            if (!Directory.Exists("reports"))
            {
                Directory.CreateDirectory("reports");
            }
            if (!Directory.Exists("settings/Stock"))
            {
                Directory.CreateDirectory("settings/Stock");
            }
            if (!Directory.Exists("settings/Stock/Photos"))
            {
                Directory.CreateDirectory("settings/Stock/Photos");
            }
            if (!Directory.Exists("settings/backup"))
            {
                Directory.CreateDirectory("settings/backup");
            }

            //Проверяем наличие файлов
            if (!aboutUs.Exists)
            {
                FileStream file = new FileStream(aboutUs.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Наружняя реклама");
                sw.WriteLine("Интернет");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/aboutUs.txt");
                sw.Close();
            }

            if (!AdresSC.Exists)
            {
                FileStream file = new FileStream(AdresSC.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Улица 3й ноги");
                sw.WriteLine("Переулок 2го уха");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/AdresSC.txt");
                sw.Close();
            }

            if (!brands.Exists)
            {
                FileStream file = new FileStream(brands.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("ACER");
                sw.WriteLine("ASUS");
                sw.WriteLine("APPLE");
                sw.WriteLine("LENOVO");
                sw.WriteLine("SAMSUNG");
                sw.WriteLine("HEWLETT PACKARD");
                sw.WriteLine("DELL");
                sw.WriteLine("MSI");
                sw.WriteLine("DIGMA");
                sw.WriteLine("BENQ");
                sw.WriteLine("BBK");
                sw.Close();
            }

            if (!DeviceColour.Exists)
            {
                FileStream file = new FileStream(DeviceColour.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Белый");
                sw.WriteLine("Чёрный");
                sw.WriteLine("Серебристый");
                sw.WriteLine("Золотоистый");
                sw.WriteLine("Синий");
                sw.Close();
            }
            if (!komplektonst.Exists)
            {
                FileStream file = new FileStream(komplektonst.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Аппарат");
                sw.WriteLine("АКБ");
                sw.WriteLine("Зарядное устройство");
                sw.WriteLine("Чехол");
                sw.WriteLine("Блок питания");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/komplektonst.txt");
                sw.Close();
            }
            if (!masters.Exists)
            {
                FileStream file = new FileStream(masters.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Мастер1");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/masters.txt");
                sw.Close();
            }
            if (!neispravnost.Exists)
            {
                FileStream file = new FileStream(neispravnost.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Что-то сломано");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/neispravnost.txt");
                sw.Close();
            }
            if (!sostoyaniePriema.Exists)
            {
                FileStream file = new FileStream(sostoyaniePriema.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Не бит, не крашен");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/sostoyaniePriema.txt");
                sw.Close();
            }
            if (!ustrojstvo.Exists)
            {
                FileStream file = new FileStream(ustrojstvo.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Ноутбук");
                sw.WriteLine("Телефон");
                sw.WriteLine("Патифон");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/ustrojstvo.txt");
                sw.Close();
            }
            if (!vipolnRaboti.Exists)
            {
                FileStream file = new FileStream(vipolnRaboti.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding(1251));
                sw.WriteLine("Замена чего-нибудь");
                sw.WriteLine("Диагностика");
                sw.WriteLine("Этот текст можно");
                sw.WriteLine("поменять в файле");
                sw.WriteLine("settings/vipolnRaboti.txt");
                sw.Close();
            }


            if (!DannieOFirme.Exists)
            {
                FileStream file = new FileStream(DannieOFirme.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("Режим работы: пн-пт: 10-19, сб: 10-16, вс: выходной <br>г.Петрозаводск, уд.Древлянка д.18, 2 этаж(ТЦ Находка)");
                sw.Close();
            }
            if (!DogovorTextPriem.Exists)
            {
                FileStream file = new FileStream(DogovorTextPriem.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(@"        1. Стоимость услуг определяется сервис-инженером только после проведения диагностики оборудования в соответствии с прайс-листом.<br>

        2.Сроки ремонта устанавливаются  в зависимости от наличия запчастей и сложности выполнения работ.<br>

        3.Аппараты принимаются на ремонт / диагностику без SIM карт и карт памяти, а также зарядных устройств, гарнитур, кабелей и других аксессуаров, кроме тех случаев, когда это необходимо для диагностики.Такой случай фиксируется в квитанции дополнительно.Исполнитель не несет ответственности за сохранность перечисленных устройств, при отсутствии записи о них в квитанции.<br>

        4.Оборудование с согласия клиента принято без разборки и проверки неисправностей.Клиент согласен, что все неисправности и внутренние повреждения, которые могут быть обнаружены в оборудовании при техническом обслуживании, возникли до приема оборудования по данной квитанции.<br>

        5.Заказчик согласен на обработку персональных данных, а также несет ответственность за достоверность предоставленной информации.Сервисный центр не несет ответственности за сохранность данных, хранящихся в памяти(носителе памяти) оборудования сданного в ремонт.<br>

       6.Исполнитель предоставляет гарантию на ремонт узлов оборудования до 14 дней на установленные комплектующие в соответствии с гарантийным талоном.При этом гарантия Исполнителя распространяется только на те узлы или комплектующие, которые подвергались ремонту или замене Исполнителем.<br>

        7.Заказчик обязан проверить работоспособность оборудования или настроенного программного обеспечения в присутствии сервис - инженера.<br>

        8.Установленные узлы или расходные материалы возврату не подлежат.<br>

        9.В случае утери квитанции выдача аппарата производится при предъявлении паспорта лица сдававшего аппарат и письменного заявления.<br>

       10.Сданный в ремонт или на диагностику аппарат должен быть получен в течении 30 дней с момента извещения(в случае недоступности отправляется SMS на номер телефона).При невыполнении этого требования взимается пени в размере 10 рублей за каждый день просрочки.Аппараты, невостребованные в течении 90 дней, могут быть реализованы в установленном законом порядке для погашения задолженности Заказчика перед Исполнителем. * Правила бытового обслуживания населения в РФ, глава IV, пункт 15.<br>

        &nbsp;<br>
        С комплектацией, описанием неисправностей и повреждений, условиями ремонта оборудования ознакомлен(а) и согласен(а).
        <br> ");
                sw.Close();
            }
            if (!DogovorTextVidacha.Exists)
            {
                FileStream file = new FileStream(DogovorTextVidacha.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(@"   1. Гарантийный ремонт производится в срок от 1 до 7 дней после поступления запчастей.<br>

        2.Выход устройства из строя в результате действий пользователя или заражения вирусами гарантийным случаем не является.<br>

        3.Исполнитель предоставляет гарантию на ремонт в соответствии с гарантийным талоном.При этом гарантия Исполнителя распространяется только на те узлы или комплектующие, которые подвергались ремонту или замене Исполнителем.<br>

        4.Гарантийное обслуживание производится по адресу, указанному в Акте и только при наличии у Заказчика Акта сдачи - приемки работ, подписанных обеими сторонами.<br>

       5.Исполнитель несет ответственность только за услуги, оказанные в соответствии с данным Договором.<br>

       6.Ремонт и обслуживание оборудования осуществляются в соответствии с требованиями нормативных документов, в том числе ГОСТ 12.2006 - 87 п.9.1, ГОСТР 50377 - 92 п.2.1.4, ГОСТР 50936 - 96, ГОСТР50938 - 96, и согласно Федеральному Закону «О защите прав потребителей».<br>

       7.Исполнитель не несет гарантийных обязательств в случаях отсутствия или повреждения гарантийной пломбы Исполнителя, внесения каких - либо изменений в конфигурацию оборудования, в том числе программное обеспечение устройства, в случае замены узлов, комплектующих или расходных материалов, в случае установки или настройки программного обеспечения, в случае монтажных работ, работ по администрированию без присутствия представителя Исполнителя.<br>

        8.Требования по устранению недостатков оказанных услуг принимаются Исполнителем только в письменном виде и при условии выполнения установленных производителем правил эксплуатации оборудования.<br>

        9.Установленные узлы или расходные материалы возврату не подлежат.<br>

       &nbsp;<br>
        Подтверждаю, что работа была выполнена в полном объеме, претензий к Исполнителю не имею.
        <br> ");
                sw.Close();
            }
            if (!FirmName.Exists)
            {
                FileStream file = new FileStream(FirmName.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("Название Вашей Фирмы");
                sw.Close();
            }
            if (!Phone.Exists)
            {
                FileStream file = new FileStream(Phone.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("тел.: Вашей фирмы");
                sw.Close();
            }
            if (!URDannie.Exists)
            {
                FileStream file = new FileStream(URDannie.ToString(), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("ИП Какойктото В.Е., ОГРНИП 315100234567334 от 19.05.2015 г., ИНН 112301774509");
                sw.Close();
            }
        }

        // Обновление программы
        //Обновление
        public void CheckUpdates()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://mywork2.ru/version.xml");

                var remoteVersion = new Version(doc.GetElementsByTagName("version")[0].InnerText);
                var localVersion = new Version(Application.ProductVersion);
                TemporaryBase.ProgrammVersion = localVersion.ToString();
                if (localVersion < remoteVersion)
                {
                    if (MessageBox.Show("Обнаружено обновление, скачать?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Process.Start("http://mywork2.ru/update.html");
                    }
                }
            }
            catch (Exception) { }
        }



        //Функция заглавной буквы в начале каждого слова
        string FirstLetterToUpper(string krolik)
        {
            string lookup = " \r\n\t";
            var sb = new StringBuilder(krolik.ToLower());

            if (sb.Length > 0 && char.IsLetter(sb[0]))
                sb[0] = char.ToUpper(sb[0]);

            for (int z = 1; z < sb.Length; z++)
            {
                char ch = sb[z];
                if (lookup.Contains(sb[z - 1]) && char.IsLetter(ch))
                    sb[z] = char.ToUpper(ch);
            }
            return sb.ToString();
        }


        private void MainListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (VCList.Count > 0)
            {
                lviewSelectedIndex = MainListView.SelectedIndices[0];
                string id_zapisi = MainListView.Items[lviewSelectedIndex].SubItems[0].Text;
                StatusStripLabel.Text = "Открыт заказ номер: " + id_zapisi;
                Editor ed1 = new Editor(this, id_zapisi);
                ed1.Show(this);
            }
        }
        // Переменная создания формы настроек (чтобы не открывалась вторая)
        public bool setBool = false;
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (setBool) return;
            else
            {
                Settings set1 = new Settings(this);
                set1.Show(this);
            }

        }
        // Считываем текущее значение столбцов
        private void MainListViewColumnIndexReader()
        {
            INIF.WriteINI(TemporaryBase.UserKey, "id", MainListView.Columns[0].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Data_priema", MainListView.Columns[1].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Data_vidachi", MainListView.Columns[2].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Data_predoplaty", MainListView.Columns[3].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "surname", MainListView.Columns[4].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "phone", MainListView.Columns[5].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "AboutUs", MainListView.Columns[6].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "WhatRemont", MainListView.Columns[7].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "brand", MainListView.Columns[8].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "model", MainListView.Columns[9].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "SerialNumber", MainListView.Columns[10].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "sostoyanie", MainListView.Columns[11].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "komplektonst", MainListView.Columns[12].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "polomka", MainListView.Columns[13].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "kommentarij", MainListView.Columns[14].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "predvaritelnaya_stoimost", MainListView.Columns[15].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Predoplata", MainListView.Columns[16].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Zatrati", MainListView.Columns[17].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "okonchatelnaya_stoimost_remonta", MainListView.Columns[18].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Skidka", MainListView.Columns[19].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Status_remonta", MainListView.Columns[20].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "master", MainListView.Columns[21].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "vipolnenie_raboti", MainListView.Columns[22].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Garanty", MainListView.Columns[23].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "wait_zakaz", MainListView.Columns[24].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "Adress", MainListView.Columns[25].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "AdressSC", MainListView.Columns[26].DisplayIndex.ToString());
            INIF.WriteINI(TemporaryBase.UserKey, "DeviceColour", MainListView.Columns[27].DisplayIndex.ToString());

        }

        // Записываем новые значения столбцов
        private void MainListViewColumnIndexWriter()
        {
            if (INIF.KeyExists(TemporaryBase.UserKey, "id"))
            {
                try { MainListView.Columns[0].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "id")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Data_priema"))
            {
                try { MainListView.Columns[1].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Data_priema")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Data_vidachi"))
            {
                try { MainListView.Columns[2].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Data_vidachi")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Data_predoplaty"))
            {
                try { MainListView.Columns[3].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Data_predoplaty")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "surname"))
            {
                try { MainListView.Columns[4].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "surname")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "phone"))
            {
                try { MainListView.Columns[5].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "phone")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "AboutUs"))
            {
                try { MainListView.Columns[6].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "AboutUs")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "WhatRemont"))
            {
                try { MainListView.Columns[7].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "WhatRemont")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "brand"))
            {
                try { MainListView.Columns[8].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "brand")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "model"))
            {
                try { MainListView.Columns[9].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "model")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "SerialNumber"))
            {
                try { MainListView.Columns[10].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "SerialNumber")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "sostoyanie"))
            {
                try { MainListView.Columns[11].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "sostoyanie")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "komplektonst"))
            {
                try { MainListView.Columns[12].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "komplektonst")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "polomka"))
            {
                try { MainListView.Columns[13].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "polomka")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "kommentarij"))
            {
                try { MainListView.Columns[14].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "kommentarij")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "predvaritelnaya_stoimost"))
            {
                try { MainListView.Columns[15].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "predvaritelnaya_stoimost")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Predoplata"))
            {
                try { MainListView.Columns[16].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Predoplata")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Zatrati"))
            {
                try { MainListView.Columns[17].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Zatrati")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "okonchatelnaya_stoimost_remonta"))
            {
                try { MainListView.Columns[18].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "okonchatelnaya_stoimost_remonta")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Skidka"))
            {
                try { MainListView.Columns[19].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Skidka")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Status_remonta"))
            {
                try { MainListView.Columns[20].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Status_remonta")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "master"))
            {
                try { MainListView.Columns[21].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "master")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "vipolnenie_raboti"))
            {
                try { MainListView.Columns[22].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "vipolnenie_raboti")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Garanty"))
            {
                try { MainListView.Columns[23].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Garanty")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "wait_zakaz"))
            {
                try { MainListView.Columns[24].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "wait_zakaz")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "Adress"))
            {
                try { MainListView.Columns[25].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "Adress")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "AdressSC"))
            {
                try { MainListView.Columns[26].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "AdressSC")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
            if (INIF.KeyExists(TemporaryBase.UserKey, "DeviceColour"))
            {
                try { MainListView.Columns[27].DisplayIndex = int.Parse(INIF.ReadINI(TemporaryBase.UserKey, "DeviceColour")); }
                catch (Exception ex) { MessageBox.Show((DateTime.Now.ToString() + " Что-то с шириной колонок нет так " + ex.ToString() + Environment.NewLine)); }
            }
        }

        private void MainListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            //Считываем расположение столбцов
            MainListViewColumnIndexReader();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Считываем расположение столбцов
            MainListViewColumnIndexReader();
            //Пишем текущие значения размеров и положения
            if (this.Left > -10000 && this.Top > -10000)
            {
                INIF.WriteINI(TemporaryBase.UserKey, "MainFormPosition", "1");
                INIF.WriteINI(TemporaryBase.UserKey, "MfLeft", this.Left.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "MfTop", this.Top.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "MfWidth", this.Width.ToString());
                INIF.WriteINI(TemporaryBase.UserKey, "MfHeight", this.Height.ToString());
            }
            if (TemporaryBase.everyDayBackup == "Checked")
            {
                if (!File.Exists(TemporaryBase.pathtoSaveBD + "/Backup_" + DateTime.Now.ToString("dd-MM-yyyy HH") + ".sqlite"))
                {
                    File.Copy(basa.dbFileName, TemporaryBase.pathtoSaveBD + "/Backup_" + DateTime.Now.ToString("dd-MM-yyyy HH") + ".sqlite");
                }
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void SearchFIOTextBox_Click(object sender, EventArgs e)
        {

        }


        private void SearchFIOButton_Click(object sender, EventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            //Запускаем поиск
            //Проверка на скрытое окно
            if (SearchFIOTextBox.Text == "QWERTY777")
            {
                SURPRISE sup1 = new SURPRISE(this);
                sup1.Show();
            }
            else
                TemporaryBase.SearchFULLBegin();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            StatusButtonColorer();
            SearchFIOTextBox.Text = "";
            if (Properties.Settings.Default["AdressSCDefault"].ToString() != "")
            {
                if (ServiceAdressComboBox.Items.Count > int.Parse(Properties.Settings.Default["AdressSCDefault"].ToString()))
                {
                    ServiceAdressComboBox.SelectedIndex = int.Parse(Properties.Settings.Default["AdressSCDefault"].ToString());
                }

            }
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;

            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            //Расширенный поиск
            FullSearchBrand.Text = "";
            FullSearchMaster.Text = "";
            FullSearchModel.Text = "";
            FullSearchPhone.Text = "";
            FullSearchSerial.Text = "";
            FullSearchType.Text = "";


            ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);
            ReadyFilterCheckBox.Image = Properties.Resources.check_circle_outline_16;
            TemporaryBase.SearchCleaner();
            TemporaryBase.SearchFULLBegin();
            AllOrdersButton.BackColor = Color.FromArgb(179, 215, 243);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                Graf gf1 = new Graf(this);
                gf1.Show(this);
            }
            else
                MessageBox.Show("Графики и отчеты доступны только в полной версии https://vk.com/clubremontuchet");
        }

        private void ShowPhoneWaitingButton_Click(object sender, EventArgs e)
        {
            TemporaryBase.IskatVseVidannoe = false;
            StatusButtonColorer();
            ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);
            if (ShowPhoneWaitingButton.Checked == false)
            {
                ShowPhoneWaitingButton.Image = Properties.Resources.Check1;
                ShowPhoneWaitingButton.Checked = true;

                TemporaryBase.SearchCleaner();
                TemporaryBase.soglasovat = "1";
                TemporaryBase.Status = " "; // Чтобы искало и в выданном тоже
                TemporaryBase.SearchFULLBegin("SearchInOldToo");

            }
            else
            {
                if (AllOrdersButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   DiagnosticksButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   SoglasovanieSKlientomButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   SoglasovanoButton1.BackColor != Color.FromArgb(179, 215, 243) &&
                   InWorkButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   PartWaitingButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   ReadyStatButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   PrinyatPoGarantiiButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   OutOfSCButton.BackColor != Color.FromArgb(179, 215, 243))
                {
                    AllOrdersButton.BackColor = Color.FromArgb(179, 215, 243);

                    TemporaryBase.ColumnIndex = 0;
                    TemporaryBase.SortAscending = false;
                    TemporaryBase.SearchInOld = true;


                    ShowPhoneWaitingButton.Checked = false;
                    WaitZakazButton.Checked = false;

                    ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                    WaitZakazButton.Image = Properties.Resources.chip;

                    // Чтобы не было зависисмотстей от всякой сторонней херни, типо значка телефона;
                    TemporaryBase.soglasovat = "";
                    TemporaryBase.NeedZakaz = "";


                    TemporaryBase.Status = "";
                }
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                ShowPhoneWaitingButton.Checked = false;
                TemporaryBase.SearchCleaner();
                TemporaryBase.SearchFULLBegin();

            }
            WaitZakazButton.Checked = false;
            WaitZakazButton.Image = Properties.Resources.chip;

        }

        //Производит поиск по базе: выводит те, по которым нужно отзвониться
        private void SearchPhone()
        {
            try
            {
                MainListView.Items.Clear();
                VCList.Clear();
                DataTable dt1 = basa.BdSearchPhoneWaiting();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                             dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                              dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                              dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                              dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                              dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), TemporaryBase.diagnostika, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString());
                    VCList.Add(vc);
                }
                MainListView.VirtualListSize = VCList.Count;

                CountListViewLabel.Text = "Найдено записей: " + dt1.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DiagnostikSearchButton_Click(object sender, EventArgs e)
        {
        }


        private void WaitZakazButton_Click(object sender, EventArgs e)
        {
            TemporaryBase.IskatVseVidannoe = false;
            StatusButtonColorer();
            ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);
            if (WaitZakazButton.Checked == false)
            {
                WaitZakazButton.Image = Properties.Resources.Check1;
                WaitZakazButton.Checked = true;
                TemporaryBase.SearchCleaner();
                TemporaryBase.Status = " "; //Чтобы искало везде
                TemporaryBase.NeedZakaz = "Заказать";
                TemporaryBase.SearchFULLBegin();

            }
            else
            {
                if (AllOrdersButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   DiagnosticksButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   SoglasovanieSKlientomButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   SoglasovanoButton1.BackColor != Color.FromArgb(179, 215, 243) &&
                   InWorkButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   PartWaitingButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   ReadyStatButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   PrinyatPoGarantiiButton.BackColor != Color.FromArgb(179, 215, 243) &&
                   OutOfSCButton.BackColor != Color.FromArgb(179, 215, 243))
                {
                    AllOrdersButton.BackColor = Color.FromArgb(179, 215, 243);

                    TemporaryBase.ColumnIndex = 0;
                    TemporaryBase.SortAscending = false;
                    TemporaryBase.SearchInOld = true;


                    ShowPhoneWaitingButton.Checked = false;
                    WaitZakazButton.Checked = false;

                    ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                    WaitZakazButton.Image = Properties.Resources.chip;

                    // Чтобы не было зависисмотстей от всякой сторонней херни, типо значка телефона;
                    TemporaryBase.soglasovat = "";
                    TemporaryBase.NeedZakaz = "";


                    TemporaryBase.Status = "";
                }
                WaitZakazButton.Image = Properties.Resources.chip;
                WaitZakazButton.Checked = false;
                TemporaryBase.SearchCleaner();
                TemporaryBase.SearchFULLBegin();

            }
            ShowPhoneWaitingButton.Checked = false;


            ShowPhoneWaitingButton.Image = Properties.Resources.phone;

        }

        private void MainListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // Если индекс в границах нашей коллекции
            if (e.ItemIndex >= 0 && e.ItemIndex < VCList.Count)
            {
                // добавляем новый элемент в коллекцию
                e.Item = new ListViewItem(VCList[e.ItemIndex].Id);
                //Проверка на рисунок
                if (VCList[e.ItemIndex].Image_key == "1")
                {
                    // Вставляем рисунок
                    e.Item.ImageIndex = 0;
                }

                // Проверочка на критичность даты диагностики
                if (TemporaryBase.Poloski)
                {
                    //Черезстрочная 
                    if (e.ItemIndex % 2 == 0)
                        e.Item.BackColor = Color.FromArgb(240, 240, 240);
                }
                if (TemporaryBase.backOfColour != null && VCList[e.ItemIndex].Data_priema != "")
                {
                    bool dateOfCriticalDiagnosik = ((DateTime.Parse(VCList[e.ItemIndex].Data_priema)) < DateTime.Today.AddDays(daysDiagnostik));

                    if (VCList[e.ItemIndex].Data_vidachi == "" && dateOfCriticalDiagnosik && VCList[e.ItemIndex].Status_remonta == "Диагностика")
                    {
                        if (VCList[e.ItemIndex].Diagnosik)
                        {
                            e.Item.BackColor = TemporaryBase.backOfColour;
                        }

                    }

                }
                if (VCList[e.ItemIndex].Status_remonta == "Готов")
                {
                    e.Item.BackColor = Color.FromArgb(126, 187, 126);
                }

                e.Item.SubItems.Add(VCList[e.ItemIndex].Data_priema);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Data_vidachi);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Data_predoplaty);
                e.Item.SubItems.Add(FirstLetterToUpper(VCList[e.ItemIndex].Surname));
                e.Item.SubItems.Add(VCList[e.ItemIndex].Phone);
                e.Item.SubItems.Add(VCList[e.ItemIndex].AboutUs);
                e.Item.SubItems.Add(FirstLetterToUpper(VCList[e.ItemIndex].WhatRemont));
                e.Item.SubItems.Add(VCList[e.ItemIndex].Brand);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Model);
                e.Item.SubItems.Add(VCList[e.ItemIndex].SerialNumber);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Sostoyanie);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Komplektonst);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Polomka);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Kommentarij);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Predvaritelnaya_stoimost);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Predoplata);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Zatrati);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Okonchatelnaya_stoimost_remonta);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Skidka);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Status_remonta);
                e.Item.SubItems.Add(FirstLetterToUpper(VCList[e.ItemIndex].Master));
                e.Item.SubItems.Add(VCList[e.ItemIndex].Vipolnenie_raboti);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Garanty);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Wait_zakaz);
                e.Item.SubItems.Add(VCList[e.ItemIndex].Adress);
                e.Item.SubItems.Add(VCList[e.ItemIndex].AdressSC);
                e.Item.SubItems.Add(VCList[e.ItemIndex].DeviceColour);
            }
        }

        private void MainListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainListView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            // для отрисовки лист бокса при сортировке
            TemporaryBase.startIndex = e.StartIndex;
            TemporaryBase.endIndex = e.EndIndex;
        }

        private void SearchFIOTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (AllOrdersButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    DiagnosticksButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    SoglasovanieSKlientomButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    SoglasovanoButton1.BackColor != Color.FromArgb(179, 215, 243) &&
                    InWorkButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    PartWaitingButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    ReadyStatButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    PrinyatPoGarantiiButton.BackColor != Color.FromArgb(179, 215, 243) &&
                    OutOfSCButton.BackColor != Color.FromArgb(179, 215, 243))
                {
                    AllOrdersButton.BackColor = Color.FromArgb(179, 215, 243);

                    TemporaryBase.ColumnIndex = 0;
                    TemporaryBase.SortAscending = false;
                    TemporaryBase.SearchInOld = true;


                    ShowPhoneWaitingButton.Checked = false;
                    WaitZakazButton.Checked = false;

                    ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                    WaitZakazButton.Image = Properties.Resources.chip;

                    // Чтобы не было зависисмотстей от всякой сторонней херни, типо значка телефона;
                    TemporaryBase.soglasovat = "";
                    TemporaryBase.NeedZakaz = "";


                    TemporaryBase.Status = "";
                }
                int numCvit;
                if (int.TryParse(SearchFIOTextBox.Text, out numCvit))
                {
                    // Защита от не существующих строк

                    int Exists = 0;
                    Exists = basa.CatlogIDExists(SearchFIOTextBox.Text);
                    if (Exists != 0)
                    {
                        Editor ed = new Editor(this, numCvit.ToString());
                        ed.Show(this);
                    }
                    else
                    {
                        MessageBox.Show("Запись с данным номером в базе не найдена");
                    }

                    SearchFIOTextBox.Text = "";

                }
                else
                {
                    ShowPhoneWaitingButton.Checked = false;
                    WaitZakazButton.Checked = false; ;
                    ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                    WaitZakazButton.Image = Properties.Resources.chip;
                    //Запускаем поиск
                    TemporaryBase.SearchFULLBegin();
                }

            }
        }

        private void ReadyFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ServiceAdressComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            TemporaryBase.SearchFULLBegin();
        }

        private void ReadyFilterCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            TemporaryBase.SearchFULLBegin();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SearchFIOTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchFIOTextBox.Text.Length == 12)
            {
                long trprs;
                if (long.TryParse(SearchFIOTextBox.Text, out trprs))
                {
                    string idInCatalog = "";
                    idInCatalog = basa.BdReadBarcode(SearchFIOTextBox.Text.Trim());
                    SearchFIOTextBox.Text = "";
                    if (idInCatalog != "")
                    {
                        Editor ed = new Editor(this, idInCatalog);
                        ed.Show(this);
                    }

                }
                else
                    TemporaryBase.FIO = SearchFIOTextBox.Text.ToUpper();
            }
            else
                TemporaryBase.FIO = SearchFIOTextBox.Text.ToUpper();
        }

        private void StockButton_Click(object sender, EventArgs e)
        {
            basa.CreateStock();
            basa.CreateStockMap();
            if (TemporaryBase.stockR)
            {
                if (!TemporaryBase.baseR)
                {
                    MessageBox.Show("Склад доступен только в полной версии, все вопросы vk.com/scrypto");
                }
                else
                {
                    Stock st1 = new Stock(this);
                    st1.Show(this);
                }
            }
            else

                MessageBox.Show("Склад доступен только в полной версии, все вопросы: vk.com/scrypto");
        }

        private void ReadyFilterCheckBox_Click(object sender, EventArgs e)
        {
            if (ReadyFilterCheckBox.BackColor == Color.FromArgb(179, 215, 243))
            {
                ReadyFilterCheckBox.BackColor = Color.FromArgb(240, 240, 240);
                ReadyFilterCheckBox.Image = Resources.check_circle_outline_blank_16;
            }
            else
            {
                ReadyFilterCheckBox.BackColor = Color.FromArgb(179, 215, 243);
                ReadyFilterCheckBox.Image = Resources.check_circle_outline_16;
            }
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            TemporaryBase.SearchFULLBegin();

        }

        private void ServiceAdressComboBox_TextChanged(object sender, EventArgs e)
        {
            ServArd = ServiceAdressComboBox.Text;
            TemporaryBase.SearchFULLBegin();
        }

        private void SmsStripButton_Click(object sender, EventArgs e)
        {
            SmsMain smsM = new SmsMain(this);
            smsM.Show(this);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClientsEditor ce1 = new ClientsEditor(this);
            ce1.Show(this);
        }

        private void SoglasovanoButton_Click(object sender, EventArgs e)
        {

        }

        private void AllOrdersButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void DiagnosticksButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void SoglasovanieSKlientomButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void SoglasovanoButton1_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void InWorkButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void PartWaitingButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void ReadyStatButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void OutOfSCButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }

        private void PrinyatPoGarantiiButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ts1 = (ToolStripButton)sender;
            if (ts1.BackColor == Color.FromArgb(179, 215, 243))
            {
                ButtonTool(AllOrdersButton);
            }
            else
                ButtonTool((ToolStripButton)sender);
        }
        private void ButtonTool(ToolStripButton tsb)
        {
            StatusButtonColorer();

            tsb.BackColor = Color.FromArgb(179, 215, 243);

            if (tsb.Tag == null || tsb.Tag.ToString() == "")
            {
                tsb.Tag = "";
                TemporaryBase.ColumnIndex = 0;
                TemporaryBase.SortAscending = false;
                TemporaryBase.SearchInOld = true;
                TemporaryBase.IskatVseVidannoe = false;
            }
            else if (tsb.Tag.ToString() == "Выдан")
            {
                TemporaryBase.ColumnIndex = 2;
                TemporaryBase.SortAscending = false;
                TemporaryBase.SearchInOld = false;
                TemporaryBase.IskatVseVidannoe = true;
            }
            else
            {
                TemporaryBase.ColumnIndex = 0;
                TemporaryBase.SortAscending = false;
                TemporaryBase.SearchInOld = false;
                TemporaryBase.IskatVseVidannoe = false;
            }

            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false;

            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;

            // Чтобы не было зависисмотстей от всякой сторонней херни, типо значка телефона;
            TemporaryBase.soglasovat = "";
            TemporaryBase.NeedZakaz = "";


            TemporaryBase.Status = tsb.Tag.ToString();
            TemporaryBase.SearchFULLBegin();

        }

        private void StatusButtonColorer()
        {
            AllOrdersButton.BackColor = Color.FromArgb(240, 240, 240);
            DiagnosticksButton.BackColor = Color.FromArgb(240, 240, 240);
            SoglasovanieSKlientomButton.BackColor = Color.FromArgb(240, 240, 240);
            SoglasovanoButton1.BackColor = Color.FromArgb(240, 240, 240);
            InWorkButton.BackColor = Color.FromArgb(240, 240, 240);
            PartWaitingButton.BackColor = Color.FromArgb(240, 240, 240);
            ReadyStatButton.BackColor = Color.FromArgb(240, 240, 240);
            OutOfSCButton.BackColor = Color.FromArgb(240, 240, 240);
            PrinyatPoGarantiiButton.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void FullSearchType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchBrand_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.Brand = FullSearchBrand.Text.ToUpper().Trim();
        }

        private void FullSearchModel_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.Model = FullSearchModel.Text.ToUpper().Trim();
        }

        private void FullSearchSerial_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.SerialImei = FullSearchSerial.Text.ToUpper().Trim();
        }

        private void FullSearchMaster_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.Master = FullSearchMaster.Text.Trim();
        }

        private void FullSearchType_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.TypeOf = FullSearchType.Text.ToUpper().Trim();
        }

        private void FullSearchPhone_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.Phone = FullSearchPhone.Text.Trim().Replace(" ", "");
        }

        private void FullSearchPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowPhoneWaitingButton.Checked = false;
                WaitZakazButton.Checked = false; ;
                ShowPhoneWaitingButton.Image = Properties.Resources.phone;
                WaitZakazButton.Image = Properties.Resources.chip;
                //Запускаем поиск
                TemporaryBase.SearchFULLBegin();
            }
        }

        private void FullSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false; ;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            //Запускаем поиск
            TemporaryBase.SearchFULLBegin();
        }

        private void FullSearchBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false; ;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            //Запускаем поиск
            TemporaryBase.SearchFULLBegin();
        }

        private void FullSearchMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPhoneWaitingButton.Checked = false;
            WaitZakazButton.Checked = false; ;
            ShowPhoneWaitingButton.Image = Properties.Resources.phone;
            WaitZakazButton.Image = Properties.Resources.chip;
            //Запускаем поиск
            TemporaryBase.SearchFULLBegin();
        }

        private void MainListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (VCList.Count > 0)
                {
                    lviewSelectedIndex = MainListView.SelectedIndices[0];
                    string id_zapisi = MainListView.Items[lviewSelectedIndex].SubItems[0].Text;
                    StatusStripLabel.Text = "Открыт заказ номер: " + id_zapisi;
                    Editor ed1 = new Editor(this, id_zapisi);
                    ed1.Show(this);
                }
            }
        }

        private void MainListView_Click(object sender, EventArgs e)
        {

        }

        private void MainListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ColumnsEdtitor CE1 = new ColumnsEdtitor(this);
                CE1.Show(this);
            }
        }

        private void ServiceAdressComboBox_Click(object sender, EventArgs e)
        {

        }
    }
}
