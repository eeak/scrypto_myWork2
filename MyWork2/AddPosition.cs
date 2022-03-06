using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class AddPosition : Form
    {
        Form1 mainForm;
        IniFile INIF = new IniFile("Config.ini");
        // Если клиент уже с нами
        bool clientIsAlreadyWithUs = false;
        string clientIdInBase = "";
        string FILL_FIO_AUTO = "";
        string FILL_PHONE_AUTO = "";
        public AddPosition(Form1 mf)
        {
            mainForm = mf;
            InitializeComponent();
            // Дабы не прокручивать колёсиком мышки случайно, пишем этот код
            this.What_remont_combo_box.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.What_remont_combo_box_MouseWheel);
            this.BrandComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.BrandComboBox_MouseWheel);
            this.DeviceColourComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.DeviceColourComboBox_MouseWheel);
            this.ServiceAdressComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ServiceAdressComboBox_MouseWheel);
            this.AboutUsComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.AboutUsComboBox_MouseWheel);
            this.MasterComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MasterComboBox_MouseWheel);
            this.StatusComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.StatusComboBox_MouseWheel);
            this.SostoyaniePriemaComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.SostoyaniePriemaComboBox_MouseWheel);
            this.KomplektonstComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.KomplektonstComboBox_MouseWheel);
            this.PolomkaComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PolomkaComboBox_MouseWheel);

            //Прописываем true в значение переменной, чтобы можно было контроллировать открытие закрытие дочерних форм
            mainForm.adPos = true;

        }
        // Начало функций обработки колёскика мышки
        private void What_remont_combo_box_MouseWheel(object sender, MouseEventArgs e)
        {

            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void BrandComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void DeviceColourComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void ServiceAdressComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void AboutUsComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void MasterComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void StatusComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void SostoyaniePriemaComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void KomplektonstComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void PolomkaComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        //Конец обработки колёсика мышки
        private void AddButton_Click(object sender, EventArgs e)
        {
            // Обработка обязательных полей
            bool yerror = false;
            string obPolya = "";
            if (SurnameTextBox.Text == "")
            {
                obPolya += " Введите ФИО " + Environment.NewLine;
                yerror = true;
            }
            if (phoneTextBox.Text == "")
            {
                obPolya += " Введите номер телефона " + Environment.NewLine;
                yerror = true;
            }
            if (!TemporaryBase.Nessesary)
            {
                if (What_remont_combo_box.Text == "")
                {
                    obPolya += " Введите тип устройства " + Environment.NewLine;
                    yerror = true;
                }
                if (BrandComboBox.Text == "")
                {
                    obPolya += " Введите бренд устройства " + Environment.NewLine;
                    yerror = true;
                }
                if (ModelTextBox.Text == "")
                {
                    obPolya += " Введите модель " + Environment.NewLine;
                    yerror = true;
                }
                if (SerialTextBox.Text == "")
                {
                    obPolya += " Введите серийный номер " + Environment.NewLine;
                    yerror = true;
                }
                if (SostoyanieTextBox.Text == "")
                {
                    obPolya += " Не указано состояние приема " + Environment.NewLine;
                    yerror = true;
                }
                if (KomplektnostTextBox.Text == "")
                {
                    obPolya += " Не указана комплектность " + Environment.NewLine;
                    yerror = true;
                }
                if (PolomkaTextBox.Text == "")
                {
                    obPolya += " Не указана неисправность " + Environment.NewLine;
                    yerror = true;
                }
                if (AboutUsComboBox.Text == "")
                {
                    obPolya += " Не указано откуда узнали о нас " + Environment.NewLine;
                    yerror = true;
                }

            }
            if (yerror)
            {
                MessageBox.Show(obPolya, "Не заполнены обязательные поля");
            }
            if (!yerror)
            {
                if (MessageBox.Show("Сохранить все изменения и напечатать акт приема?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //Добавляем текст пароля в комментарий
                    if (UserPassTextBox.Text != "")
                    {
                        kommentarijTextBox.AppendText(Environment.NewLine + "________________________________________" + Environment.NewLine + "Пароль: " + Environment.NewLine + UserPassTextBox.Text + Environment.NewLine + "‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾" + Environment.NewLine);
                    }
                    if (FILL_FIO_AUTO != "" && FILL_PHONE_AUTO != "")
                    {
                        if (FILL_FIO_AUTO != SurnameTextBox.Text || FILL_PHONE_AUTO != phoneTextBox.Text)
                        {
                            var mboxResult = MessageBox.Show("Ввёденные данные о клиенте (ФИО или телефон) изменились" + Environment.NewLine +
                                "Вы действитльно хотитие сохранить их в таком виде, это приведёт к изменению данных клента во всех его заказах. C" + Environment.NewLine +
                                FILL_FIO_AUTO + " " + FILL_PHONE_AUTO + " На" + Environment.NewLine +
                                SurnameTextBox.Text + " " + phoneTextBox.Text + Environment.NewLine + Environment.NewLine +
                                "Нажав Да вы подтвердите эти изменения" + Environment.NewLine +
                                "Нажав Нет вы создате нового клиента, с  данными: " + Environment.NewLine +
                                 SurnameTextBox.Text + " " + phoneTextBox.Text + Environment.NewLine +
                                "Нажав Отмена, вы вернётесь в окно добавления записи" + Environment.NewLine +
                                "Данное уведомление показано так как, вы изначально ввели данные одного клиента, а в процессе редактирования записи их изменили", "Вы уверены?", MessageBoxButtons.YesNoCancel);
                            if (mboxResult == DialogResult.Yes)
                            {
                                // Обрабатываем инфу клиента
                                ClientOfficer();
                                // Обрабатываем инфу о записи
                                mainForm.basa.BdWrite(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "", PredoplataDate(PredoplataTextBox.Text), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text,
                                ModelTextBox.Text, SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text), EmptyStringToZeroMaker(PredoplataTextBox.Text),
                                "0", "0", "0", StatusComboBox.Text, MasterComboBox.Text, "", "", NeedZakaz(), "", "", ServiceAdressComboBox.Text.ToUpper().Trim(), DeviceColourComboBox.Text.ToUpper().Trim(), clientIdInBase);


                                string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                                mainForm.StatusStripLabel.Text = "Запись номрер " + topBaseZapis + " добавлена";

                                TemporaryBase.SearchFULLBegin();
                                mainForm.basa.StatesMapWrite(topBaseZapis, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлен статус" + Environment.NewLine + StatusComboBox.Text);
                                if (TemporaryBase.baseR == true)
                                {
                                    Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(topBaseZapis), mainForm, TemporaryBase.valuta);
                                    actPriema1.Show();
                                }
                                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Добавление новой записи", "", topBaseZapis);
                                if (TemporaryBase.openClientFolder)
                                {
                                    string path = "ClientFiles\\" + clientIdInBase + "\\" + topBaseZapis;
                                    if (!Directory.Exists(path))
                                        System.IO.Directory.CreateDirectory(path);

                                    System.Diagnostics.Process.Start("explorer", "" + path);
                                }
                                this.Close();
                            }
                            else if (mboxResult == DialogResult.No)
                            {
                                clientIsAlreadyWithUs = false;
                                clientIdInBase = "";
                                // Обрабатываем инфу клиента
                                ClientOfficer();
                                // Обрабатываем инфу о записи
                                mainForm.basa.BdWrite(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "", PredoplataDate(PredoplataTextBox.Text), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text,
                                ModelTextBox.Text, SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text), EmptyStringToZeroMaker(PredoplataTextBox.Text),
                                "0", "0", "0", StatusComboBox.Text, MasterComboBox.Text, "", "", NeedZakaz(), "", "", ServiceAdressComboBox.Text.ToUpper().Trim(), DeviceColourComboBox.Text.ToUpper().Trim(), clientIdInBase);


                                string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                                mainForm.StatusStripLabel.Text = "Запись номрер " + topBaseZapis + " добавлена";

                                TemporaryBase.SearchFULLBegin();
                                mainForm.basa.StatesMapWrite(topBaseZapis, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлен статус" + Environment.NewLine + StatusComboBox.Text);
                                if (TemporaryBase.baseR == true)
                                {
                                    Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(topBaseZapis), mainForm, TemporaryBase.valuta);
                                    actPriema1.Show();
                                }
                                if (TemporaryBase.openClientFolder)
                                {
                                    string path = "ClientFiles\\" + clientIdInBase + "\\" + topBaseZapis;
                                    if (!Directory.Exists(path))
                                        System.IO.Directory.CreateDirectory(path);

                                    System.Diagnostics.Process.Start("explorer", "" + path);
                                }
                                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Добавление новой записи", "", topBaseZapis);
                                this.Close();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            // Обрабатываем инфу клиента
                            ClientOfficer();
                            // Обрабатываем инфу о записи
                            mainForm.basa.BdWrite(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "", PredoplataDate(PredoplataTextBox.Text), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text,
                            ModelTextBox.Text, SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text), EmptyStringToZeroMaker(PredoplataTextBox.Text),
                            "0", "0", "0", StatusComboBox.Text, MasterComboBox.Text, "", "", NeedZakaz(), "", "", ServiceAdressComboBox.Text.ToUpper().Trim(), DeviceColourComboBox.Text.ToUpper().Trim(), clientIdInBase);


                            string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                            mainForm.StatusStripLabel.Text = "Запись номер " + topBaseZapis + " добавлена";

                            TemporaryBase.SearchFULLBegin();
                            mainForm.basa.StatesMapWrite(topBaseZapis, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлен статус" + Environment.NewLine + StatusComboBox.Text);
                            if (TemporaryBase.baseR == true)
                            {
                                Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(topBaseZapis), mainForm, TemporaryBase.valuta);
                                actPriema1.Show();
                            }
                            if (TemporaryBase.openClientFolder)
                            {
                                string path = "ClientFiles\\" + clientIdInBase + "\\" + topBaseZapis;
                                if (!Directory.Exists(path))
                                    System.IO.Directory.CreateDirectory(path);

                                System.Diagnostics.Process.Start("explorer", "" + path);
                            }
                            mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Добавление новой записи", "", topBaseZapis);
                            this.Close();
                        }
                    }

                    else
                    {
                        // Обрабатываем инфу клиента
                        ClientOfficer();
                        // Обрабатываем инфу о записи
                        mainForm.basa.BdWrite(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "", PredoplataDate(PredoplataTextBox.Text), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text,
                        ModelTextBox.Text, SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text), EmptyStringToZeroMaker(PredoplataTextBox.Text),
                        "0", "0", "0", StatusComboBox.Text, MasterComboBox.Text, "", "", NeedZakaz(), "", "", ServiceAdressComboBox.Text.ToUpper().Trim(), DeviceColourComboBox.Text.ToUpper().Trim(), clientIdInBase);


                        string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                        mainForm.StatusStripLabel.Text = "Запись номрер " + topBaseZapis + " добавлена";

                        TemporaryBase.SearchFULLBegin();
                        mainForm.basa.StatesMapWrite(topBaseZapis, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлен статус" + Environment.NewLine + StatusComboBox.Text);
                        if (TemporaryBase.baseR == true)
                        {
                            Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(topBaseZapis), mainForm, TemporaryBase.valuta);
                            actPriema1.Show();
                        }

                        if (TemporaryBase.openClientFolder)
                        {
                            string path = "ClientFiles\\" + clientIdInBase + "\\" + topBaseZapis;
                            if (!Directory.Exists(path))
                                System.IO.Directory.CreateDirectory(path);

                            System.Diagnostics.Process.Start("explorer", "" + path);
                        }
                        mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Добавление новой записи", "", topBaseZapis);
                        this.Close();
                    }

                }
            }
        }
        public void MyMethod(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            string lbTag = lb.Tag.ToString();
            string shablon = @"(.*)\n(.*)\n(.*)\n(.*)\n";
            Regex regex = new Regex(shablon);
            Match match = regex.Match(lbTag);
            if (match.Success)
            {
                What_remont_combo_box.Text = match.Groups[1].ToString();
                BrandComboBox.Text = match.Groups[2].ToString();
                ModelTextBox.Text = match.Groups[3].ToString();
                SerialTextBox.Text = match.Groups[4].ToString();
            }
        }
        // История ремонта
        public void ClientsHistoryMaker()
        {

            DataTable dtHistory = mainForm.basa.ClientsShowHistory(clientIdInBase);
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

                label.Click += MyMethod;
                label.Tag = dtHistory.Rows[x].ItemArray[7].ToString() + Environment.NewLine +
                    dtHistory.Rows[x].ItemArray[8].ToString() + Environment.NewLine +
                    dtHistory.Rows[x].ItemArray[9].ToString() + Environment.NewLine +
                    dtHistory.Rows[x].ItemArray[10].ToString() + Environment.NewLine;
                this.panel1.Controls.Add(label);
                t.SetToolTip(label, "Адрес СЦ: " + dtHistory.Rows[x].ItemArray[27].ToString() + Environment.NewLine +
                    "Выполненные работы: " + dtHistory.Rows[x].ItemArray[22].ToString() + Environment.NewLine +
                     "Мастер: " + dtHistory.Rows[x].ItemArray[21].ToString() + Environment.NewLine +
                     "Поломка: " + dtHistory.Rows[x].ItemArray[13].ToString() + Environment.NewLine +
                     "Серийний №: " + dtHistory.Rows[x].ItemArray[10].ToString() + Environment.NewLine +
                     "Комментарий: " + dtHistory.Rows[x].ItemArray[14].ToString() + Environment.NewLine);
                x++;
            }
        }
        private void notifyFormShower(int showThis)
        {

        }






        // Обработка клиентов
        private void ClientOfficer()
        {
            if (clientIsAlreadyWithUs)
            {
                if (clientIdInBase != "")
                {
                    mainForm.basa.ClientsMapEditAll(SurnameTextBox.Text, phoneTextBox.Text.Replace(" ", ""), AdressKlientTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), ClientFirstDate(), AboutUsComboBox.Text, clientIdInBase);
                }
                else
                {
                    mainForm.basa.ClientsMapWrite(SurnameTextBox.Text, phoneTextBox.Text.Replace(" ", ""), AdressKlientTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), DateTime.Now.ToString("dd-MM-yyyy HH:mm"), AboutUsComboBox.Text);
                    clientIdInBase = mainForm.basa.ClientReadId(SurnameTextBox.Text, phoneTextBox.Text);
                }

            }
            else
            {
                mainForm.basa.ClientsMapWrite(SurnameTextBox.Text, phoneTextBox.Text.Replace(" ", ""), AdressKlientTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), DateTime.Now.ToString("dd-MM-yyyy HH:mm"), AboutUsComboBox.Text);
                clientIdInBase = mainForm.basa.ClientReadId(SurnameTextBox.Text, phoneTextBox.Text);
            }

        }
        //Дата прихода клиента
        private string ClientFirstDate()
        {
            return mainForm.basa.ClientReadDate(clientIdInBase);
        }

        //Блэк лист клентов
        private string BlistOfClients()
        {
            if (BlackListComboBox.Text == "Не проблемный")
                return "0";
            else if (BlackListComboBox.Text == "Проблемный")
                return "1";
            else return "-1";
        }
        // В зависимости от того, стоит ли глачка "Требует заказа" выдает нужное значение
        private string NeedZakaz()
        {
            if (checkBox3.Checked)
                return "Заказать";
            else
                return "";
        }
        // В зависимости от того, есть ли предоплата добавляет дату
        private string PredoplataDate(string str1)
        {
            if (str1 != "" && str1 != "0")
            {
                if (checkBox3.Checked)
                {
                    return DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                }
                else
                    return "";
            }
            else
                return "";
        }
        //Функция для окрашивания заголовков обязательных полей
        private void coloredLables()
        {
            if (SurnameTextBox.Text != "") label3.ForeColor = Color.Black;
            if (phoneTextBox.Text != "") label10.ForeColor = Color.Black;
            if (What_remont_combo_box.Text != "") label2.ForeColor = Color.Black;
            if (BrandComboBox.Text != "") label7.ForeColor = Color.Black;
            if (ModelTextBox.Text != "") label18.ForeColor = Color.Black;
            if (SerialTextBox.Text != "") label19.ForeColor = Color.Black;
            if (SostoyanieTextBox.Text != "") label14.ForeColor = Color.Black;
            if (KomplektnostTextBox.Text != "") label20.ForeColor = Color.Black;
            if (PolomkaTextBox.Text != "") label4.ForeColor = Color.Black;
            if (AboutUsComboBox.Text != "") label8.ForeColor = Color.Black;
            if (UserPassTextBox.Text != "") label16.ForeColor = Color.Black;

            if (SurnameTextBox.Text == "") label3.ForeColor = Color.RoyalBlue;
            if (phoneTextBox.Text == "") label10.ForeColor = Color.RoyalBlue;
            if (!TemporaryBase.Nessesary)
            {
                if (What_remont_combo_box.Text == "") label2.ForeColor = Color.RoyalBlue;
                if (BrandComboBox.Text == "") label7.ForeColor = Color.RoyalBlue;
                if (ModelTextBox.Text == "") label18.ForeColor = Color.RoyalBlue;
                if (SerialTextBox.Text == "") label19.ForeColor = Color.RoyalBlue;
                if (SostoyanieTextBox.Text == "") label14.ForeColor = Color.RoyalBlue;
                if (KomplektnostTextBox.Text == "") label20.ForeColor = Color.RoyalBlue;
                if (PolomkaTextBox.Text == "") label4.ForeColor = Color.RoyalBlue;
                if (AboutUsComboBox.Text == "") label8.ForeColor = Color.RoyalBlue;
                if (UserPassTextBox.Text == "") label16.ForeColor = Color.RoyalBlue;
            }


        }
        private void AddPosition_MouseDown(object sender, MouseEventArgs e)
        {
            //Для перетаскивания за форму
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewOrderButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистить содержимое полей?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                SurnameTextBox.Text = "";
                phoneTextBox.Text = "";
                AboutUsComboBox.Text = "";
                What_remont_combo_box.Text = "";
                BrandComboBox.Text = "";
                ModelTextBox.Text = "";
                SerialTextBox.Text = "";
                SostoyaniePriemaComboBox.SelectedIndex = -1;
                SostoyanieTextBox.Text = "";
                KomplektonstComboBox.SelectedIndex = -1;
                KomplektnostTextBox.Text = "";
                PolomkaComboBox.SelectedIndex = -1;
                PolomkaTextBox.Text = "";
                kommentarijTextBox.Text = "";
                PredvaritelnayaStoimostTextBox.Text = "";
                PredoplataTextBox.Text = "";
                StatusComboBox.SelectedIndex = 0;
                MasterComboBox.Text = "";
                clientIsAlreadyWithUs = false;
                clientIdInBase = "";
                PrimechanieTextBox.Text = "";
                BlackListComboBox.Text = "";
                panel1.Controls.Clear();
            }
        }

        private void label20_MouseDown(object sender, MouseEventArgs e)
        {
            label20.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label21_MouseDown(object sender, MouseEventArgs e)
        {
            label21.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void PredvaritelnayaStoimostTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                //Разрешаем ввод только цифр
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
              if (PredvaritelnayaStoimostTextBox.Text == "" && e.KeyChar == (char)'0')
            {
                e.Handled = true;
            }
        }

        private void PredoplataTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;

                }

            }
            else
            {
                //Нужно, чтобы ставилась галочка, при вводе значения в поле предоплаты.
                if (PredoplataTextBox.Text == "" && e.KeyChar == (char)'0')
                {
                    checkBox3.Checked = false;
                    e.Handled = true;
                }
                else
                    checkBox3.Checked = true;
            }
        }

        private void PredoplataTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (PredoplataTextBox.Text == "" || decimal.Parse(PredoplataTextBox.Text) == 0)
            {
                checkBox3.Checked = false;
            }
        }

        private string EmptyStringToZeroMaker(string str1)
        {
            if (str1 == "")
            {
                return 0.ToString();
            }
            else
                return str1;
        }

        private void SurnameTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void phoneTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            coloredLables();
        }

        private void What_remont_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

            coloredLables();
        }

        private void BrandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void ModelTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void SerialTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
            SerialTextBox.Text = SerialTextBox.Text.Replace('а', 'f');
            SerialTextBox.Text = SerialTextBox.Text.Replace('в', 'd');
            SerialTextBox.Text = SerialTextBox.Text.Replace('г', 'u');
            SerialTextBox.Text = SerialTextBox.Text.Replace('д', 'l');
            SerialTextBox.Text = SerialTextBox.Text.Replace('е', 't');
            SerialTextBox.Text = SerialTextBox.Text.Replace('з', 'p');
            SerialTextBox.Text = SerialTextBox.Text.Replace('и', 'b');
            SerialTextBox.Text = SerialTextBox.Text.Replace('к', 'r');
            SerialTextBox.Text = SerialTextBox.Text.Replace('л', 'k');
            SerialTextBox.Text = SerialTextBox.Text.Replace('м', 'v');
            SerialTextBox.Text = SerialTextBox.Text.Replace('н', 'y');
            SerialTextBox.Text = SerialTextBox.Text.Replace('о', 'j');
            SerialTextBox.Text = SerialTextBox.Text.Replace('п', 'g');
            SerialTextBox.Text = SerialTextBox.Text.Replace('р', 'h');
            SerialTextBox.Text = SerialTextBox.Text.Replace('с', 'c');
            SerialTextBox.Text = SerialTextBox.Text.Replace('т', 'n');
            SerialTextBox.Text = SerialTextBox.Text.Replace('у', 'e');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ф', 'a');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ц', 'w');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ч', 'x');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ш', 'i');
            SerialTextBox.Text = SerialTextBox.Text.Replace('щ', 'o');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ы', 's');
            SerialTextBox.Text = SerialTextBox.Text.Replace('ь', 'm');
            SerialTextBox.Text = SerialTextBox.Text.Replace('я', 'z');
            SerialTextBox.Text = SerialTextBox.Text.Replace('А', 'F');
            SerialTextBox.Text = SerialTextBox.Text.Replace('В', 'D');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Г', 'U');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Д', 'L');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Е', 'T');
            SerialTextBox.Text = SerialTextBox.Text.Replace('З', 'P');
            SerialTextBox.Text = SerialTextBox.Text.Replace('И', 'B');
            SerialTextBox.Text = SerialTextBox.Text.Replace('К', 'R');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Л', 'K');
            SerialTextBox.Text = SerialTextBox.Text.Replace('М', 'V');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Н', 'Y');
            SerialTextBox.Text = SerialTextBox.Text.Replace('О', 'J');
            SerialTextBox.Text = SerialTextBox.Text.Replace('П', 'G');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Р', 'H');
            SerialTextBox.Text = SerialTextBox.Text.Replace('С', 'C');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Т', 'N');
            SerialTextBox.Text = SerialTextBox.Text.Replace('У', 'E');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ф', 'A');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ц', 'W');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ч', 'X');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ш', 'I');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Щ', 'O');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ы', 'S');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Ь', 'M');
            SerialTextBox.Text = SerialTextBox.Text.Replace('Я', 'Z');
            SerialTextBox.SelectionStart = SerialTextBox.Text.Length;
        }

        private void SostoyanieTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void KomplektnostTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void PolomkaTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void What_remont_combo_box_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void BrandComboBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void phoneTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();

        }

        private void AddPosition_Load(object sender, EventArgs e)
        {
            if (TemporaryBase.Nessesary)
            {
                if (What_remont_combo_box.Text == "") label2.ForeColor = Color.Black;
                if (BrandComboBox.Text == "") label7.ForeColor = Color.Black;
                if (ModelTextBox.Text == "") label18.ForeColor = Color.Black;
                if (SerialTextBox.Text == "") label19.ForeColor = Color.Black;
                if (SostoyanieTextBox.Text == "") label14.ForeColor = Color.Black;
                if (KomplektnostTextBox.Text == "") label20.ForeColor = Color.Black;
                if (PolomkaTextBox.Text == "") label4.ForeColor = Color.Black;
                if (AboutUsComboBox.Text == "") label8.ForeColor = Color.Black;
                if (UserPassTextBox.Text == "") label16.ForeColor = Color.Black;
            }
            foreach (string strCombo in TemporaryBase.SortirovkaMasters)
            {
                MasterComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaSostoyanie)
            {
                SostoyaniePriemaComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaNeispravnost)
            {
                PolomkaComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaColour)
            {
                DeviceColourComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaKomplektnost)
            {
                KomplektonstComboBox.Items.Add(strCombo);
            }
            foreach (string strCombo in TemporaryBase.SortirovkaAdressSc)
            {
                ServiceAdressComboBox.Items.Add(strCombo);
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaBrands)
            {
                BrandComboBox.Items.Add(ssp.SortObj);
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaUstrojstvo)
            {
                What_remont_combo_box.Items.Add(ssp.SortObj);
            }
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaAboutUs)
            {
                AboutUsComboBox.Items.Add(ssp.SortObj);
            }
            //Восстанавливаем значения Адреса СЦ 
            //Восстанавливаем значения Адреса СЦ 
            if (TemporaryBase.AdressSCDefault.ToString() != "")
            {
                if (ServiceAdressComboBox.Items.Count > int.Parse(TemporaryBase.AdressSCDefault.ToString()))
                {
                    ServiceAdressComboBox.SelectedIndex = int.Parse(TemporaryBase.AdressSCDefault.ToString());
                }

            }

            // Восстанавлиеваем мастера по умолчанию
            if (TemporaryBase.MasterDefault != "")
                MasterComboBox.Text = TemporaryBase.MasterDefault;
            // Подгружает список фамилий для выпадающиего списка
            appendTextSurname();
            if (What_remont_combo_box.Items.Count < 1)
            {
                MessageBox.Show("В программе используются умные выпадающие списки, введите в поля тип устройства, бренд и откуда о нас узнали, любые данные, и программа их запомнит, и в следующий раз п" +
                    "редложит эти данные в выпдающем списке. Так же она будет сортировать их по количеству повторений, чем это число больше, чем выше данные будут в списке.  " + Environment.NewLine + "" +
                    "Это сообщение будет показываться, пока не появится хотя бы одна запись в базе");
            }
        }
        //Функция заполнения коллекции автозаполнения в surnametextbox
        private void appendTextSurname()
        {

            SurnameTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SurnameTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection SurnameAutoColl = new AutoCompleteStringCollection();
            SurnameAutoColl = mainForm.basa.AddCollectionFIO();
            SurnameTextBox.AutoCompleteCustomSource = SurnameAutoColl;
        }
        private void SurnameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (phoneTextBox.Text.Length < 3)
                    phoneTextBox.Text = SurnamePhoneAdder(SurnameTextBox.Text);
            }
        }
        //Функция добавления телефона при наличии записи в бд
        public string SurnamePhoneAdder(string fio)
        {
            string phoneNum = "";
            if (fio != "")
            {
                try
                {

                    DataTable dTable = mainForm.basa.BdReadFIOPhone(fio);


                    if (dTable.Rows.Count > 0)
                    {
                        string adress = "";
                        for (int i = 0; i < dTable.Rows.Count; i++)
                        {

                            if (dTable.Rows[i].ItemArray[1].ToString() != "")
                                phoneTextBox.Text = phoneNum = dTable.Rows[i].ItemArray[1].ToString();
                            if (dTable.Rows[i].ItemArray[2].ToString() != "")
                                AdressKlientTextBox.Text = adress = dTable.Rows[i].ItemArray[2].ToString();
                            if (dTable.Rows[i].ItemArray[3].ToString() != "")
                                PrimechanieTextBox.Text = dTable.Rows[i].ItemArray[3].ToString();
                            if (dTable.Rows[i].ItemArray[6].ToString() != "")
                                AboutUsComboBox.Text = dTable.Rows[i].ItemArray[6].ToString();
                            if (dTable.Rows[i].ItemArray[4].ToString() != "")
                                BlackListComboBox.Text = BlackLIstComboWriter(dTable.Rows[i].ItemArray[4].ToString());

                            if (dTable.Rows.Count > 1)
                            {
                                // Добавляем значение id клиента из базы
                                if (MessageBox.Show("Найдено более одного клиента с таким ФИО, исходя из номера телефона, можете определить это тот клиент, который вам нужен? ", "Совпадение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    break;
                                }
                            }


                        }
                        FILL_PHONE_AUTO = phoneNum;
                        FILL_FIO_AUTO = SurnameTextBox.Text;
                        clientIdInBase = mainForm.basa.ClientReadId(SurnameTextBox.Text, phoneNum);
                        ClientsHistoryMaker();
                        clientIsAlreadyWithUs = true;
                        return phoneNum;

                    }

                    return phoneNum;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return phoneNum;
            }
            return phoneNum;
        }
        private string BlackLIstComboWriter(string SomeNumber)
        {
            if (SomeNumber == "0")
                return "Не проблемный";
            else if (SomeNumber == "1")
                return "Проблемный";
            else
                return "";
        }

        private void SurnameTextBox_Click(object sender, EventArgs e)
        {
            if (phoneTextBox.Text.Length < 3)
                phoneTextBox.Text = SurnamePhoneAdder(SurnameTextBox.Text);
        }

        private void SostoyaniePriemaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SostoyaniePriemaComboBox.SelectedIndex != -1)
                SostoyanieTextBox.AppendText(SostoyaniePriemaComboBox.Items[SostoyaniePriemaComboBox.SelectedIndex].ToString() + ", ");
        }

        private void KomplektonstComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KomplektonstComboBox.SelectedIndex != -1)
                KomplektnostTextBox.AppendText(KomplektonstComboBox.Items[KomplektonstComboBox.SelectedIndex].ToString());
        }

        private void PolomkaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PolomkaComboBox.SelectedIndex != -1)
                PolomkaTextBox.AppendText(PolomkaComboBox.Items[PolomkaComboBox.SelectedIndex].ToString() + "/ ");
        }

        private void phoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void AddPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Чтобы можно было снова открыть окошко
            mainForm.adPos = false;
            Thread tr1 = new Thread(new ThreadStart(mainForm.VipadSpiskiObnovitPotok));
            tr1.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void zzz(object sender, EventArgs e)
        {

        }

        private void zz(object sender, EventArgs e)
        {

        }

        private void z(object sender, EventArgs e)
        {

        }

        private void BlackListComboBox_TextChanged(object sender, EventArgs e)
        {
            if (BlackListComboBox.Text == "Проблемный")
            {
                if (TemporaryBase.BlistColor != "")
                {
                    BlackListComboBox.BackColor = Color.FromArgb(int.Parse(TemporaryBase.BlistColor));
                }

                else
                    BlackListComboBox.BackColor = Color.White;
            }

        }

        private void SerialTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void PredvaritelnayaStoimostTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.Text.Replace(",", ".");
        }

        private void AboutUsComboBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void UserPassTextBox_TextChanged(object sender, EventArgs e)
        {
            coloredLables();
        }

        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            //Ставим пробелы в телефонный номер при вводе
            if (Char.IsDigit(key) || (key == (char)44) || key == (char)8 || key == (char)32 || key == (char)43)
            {
                if (key != (char)8)
                {
                    if (phoneTextBox.Text.Length > 0 && phoneTextBox.Text.Length < 2)
                    {
                        phoneTextBox.AppendText(" ");
                    }
                    if (phoneTextBox.Text.Length > 4 && phoneTextBox.Text.Length < 6)
                    {
                        phoneTextBox.AppendText(" ");
                    }
                    if (phoneTextBox.Text.Length > 8 && phoneTextBox.Text.Length < 10)
                    {
                        phoneTextBox.AppendText(" ");
                    }
                    if (phoneTextBox.Text.Length > 13 && phoneTextBox.Text.Length < 15)
                    {
                        //  phoneTextBox.AppendText(" ");
                    }
                }

            }
            else e.Handled = true;
        }

        private void SendPrivetSmsButton_Click(object sender, EventArgs e)
        {
            if (phoneTextBox.Text.Length < 3) MessageBox.Show("Введите номер телефона");
            else
            {
                string topBaseZapis = mainForm.basa.BdReadAdvertsDataTop().ToString();
                SmsFromEditor sfe = new SmsFromEditor(SurnameTextBox.Text, phoneTextBox.Text, StatusComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
               "0", "0", PredoplataTextBox.Text, PredvaritelnayaStoimostTextBox.Text, mainForm, (decimal.Parse(topBaseZapis) + 1).ToString());
                sfe.Show(this);

            }
        }



        private void GoogleSearchButton_Click(object sender, EventArgs e)
        {
            if (PolomkaTextBox.Text.Split('/')[0] != "")
            {
                contextMenuStrip1.Items.Clear();
                foreach (string neispravnost in PolomkaTextBox.Text.Split('/'))
                {
                    contextMenuStrip1.Items.Add(neispravnost, null, pasteMenuItem_Click);
                }
                contextMenuStrip1.Show(GoogleSearchButton, new Point(0, GoogleSearchButton.Height));

            }
        }
        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(String.Format("https://www.google.ru/search?&rls=ru&q={0}+{1}+{2}", BrandComboBox.Text, ModelTextBox.Text, sender.ToString()));
        }
    }
}
