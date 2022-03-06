using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Editor : Form
    {
        string Close_surname = "";
        string Close_phone = "";
        string Close_AboutUs = "";
        string Close_What_remont = "";
        string Close_Brand = "";
        string Close_Model = "";
        string Close_Serial = "";
        string Close_Sostoyanie = "";
        string Close_Komplektnost = "";
        string Close_Polomka = "";
        string Close_kommentarij = "";
        string Close_PredvaritelnayaStoimost = "";
        string Close_Predoplata = "";
        string Close_Zatraty = "";
        string Close_Price = "";
        string Close_Skidka = "";
        string Close_Status = "";
        string Close_Master = "";
        string Close_VipolnenieRaboti = "";
        string Close_Garanty = "";
        string Close_Wait_zakaz = "";
        string Close_AdressKlient = "";
        string Close_KlientGalochkaVKurse = "";
        string Close_ServiceAdress = "";
        string Close_DeviceColour = "";

        string ClientId_inBase = "";
        string id_bd;
        string formCaptionText = "";
        bool WhenClosing = false;

        Form1 mainForm;

        bool autoButton = true;
        // Для присваивания даты выдачи при изменении статуса в ручную
        bool autoBool = false;
        bool fuckthemool = true;
        public Editor(Form1 fm1, string id_bd)
        {
            mainForm = fm1;
            this.id_bd = id_bd;
            InitializeComponent();
            // Дабы не прокручивать колёсиком мышки случайно, пишем этот код
            this.What_remont_combo_box.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.What_remont_combo_box_MouseWheel);
            this.BrandComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.BrandComboBox_MouseWheel);
            this.DeviceColourComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.DeviceColourComboBox_MouseWheel);
            this.ServiceAdressComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ServiceAdressComboBox_MouseWheel);
            this.AboutUsComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.AboutUsComboBox_MouseWheel);
            this.MasterComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MasterComboBox_MouseWheel);
            this.GarantyComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GarantyComboBox_MouseWheel);
            this.StatusComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.StatusComboBox_MouseWheel);
            // Для горячей клавиши
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
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
        private void GarantyComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void StatusComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        //Конец обработки колёсика мышки
        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            //Для перетаскивания за форму
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void NewOrderButton_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label20_MouseDown(object sender, MouseEventArgs e)
        {
            label20.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label13_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            // Доделать изображения в ListView
            if (MessageBox.Show("Сохранить запись номер " + id_bd + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                WhenClosing = true;
                autoBool = true;
                fuckthemool = true;
                mainForm.basa.ClientsMapEditInEditor(SurnameTextBox.Text, PhoneToNorm(phoneTextBox.Text), AdressKlientTextBox.Text, AboutUsComboBox.Text, ClientId_inBase);
                mainForm.basa.BdEdit(PredoplataDate(PredoplataTextBox.Text), VidachiDate(), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
                    SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text),
                    EmptyStringToZeroMaker(PredoplataTextBox.Text), EmptyStringToZeroMaker(ZatratyTextBox.Text), EmptyStringToZeroMaker(PriceTextBox.Text), EmptyStringToZeroMaker(SkidkaTextBox.Text),
                    StatusComboBox.Text, MasterComboBox.Text, VipolnenieRabotiTextBox.Text, GarantyComboBox.Text, NeedZakaz(), "", KlinentVKurseTester(), id_bd, ServiceAdressComboBox.Text, DeviceColourComboBox.Text);
                //Запись в базу истории
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Сохранение", editorHistoryMaker(), id_bd);
                mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " сохранена";
                if (mainForm.ShowPhoneWaitingButton.Checked == true)
                {
                    TemporaryBase.SearchFULLBegin("1");
                }
                else
                {
                    TemporaryBase.SearchFULLBegin();
                }

                this.Close();
            }
        }

        private string editorHistoryMaker()
        {
            string history = "";

            if (!(Close_surname == SurnameTextBox.Text)) history += "Изменено: " + Close_surname + " НА: " + SurnameTextBox.Text + Environment.NewLine;
            if (!(Close_phone == phoneTextBox.Text)) history += "Изменено: " + Close_phone + " НА: " + phoneTextBox.Text + Environment.NewLine;
            if (!(Close_AboutUs == AboutUsComboBox.Text)) history += "Изменено: " + Close_AboutUs + " НА: " + AboutUsComboBox.Text + Environment.NewLine;
            if (!(Close_What_remont == What_remont_combo_box.Text)) history += "Изменено: " + Close_What_remont + " НА: " + What_remont_combo_box.Text + Environment.NewLine;
            if (!(Close_Brand == BrandComboBox.Text)) history += "Изменено: " + Close_Brand + " НА: " + BrandComboBox.Text + Environment.NewLine;
            if (!(Close_Model == ModelTextBox.Text)) history += "Изменено: " + Close_Model + " НА: " + ModelTextBox.Text + Environment.NewLine;
            if (!(Close_Serial == SerialTextBox.Text)) history += "Изменено: " + Close_Serial + " НА: " + SerialTextBox.Text + Environment.NewLine;
            if (!(Close_Sostoyanie == SostoyanieTextBox.Text)) history += "Изменено: " + Close_Sostoyanie + " НА: " + SostoyanieTextBox.Text + Environment.NewLine;
            if (!(Close_Komplektnost == KomplektnostTextBox.Text)) history += "Изменено: " + Close_Komplektnost + " НА: " + KomplektnostTextBox.Text + Environment.NewLine;
            if (!(Close_Polomka == PolomkaTextBox.Text)) history += "Изменено: " + Close_Polomka + " НА: " + PolomkaTextBox.Text + Environment.NewLine;
            if (!(Close_kommentarij == kommentarijTextBox.Text)) history += "Изменено: " + Close_kommentarij + " НА: " + kommentarijTextBox.Text + Environment.NewLine;
            if (!(Close_PredvaritelnayaStoimost == PredvaritelnayaStoimostTextBox.Text)) history += "Изменено: " + Close_PredvaritelnayaStoimost + " НА: " + PredvaritelnayaStoimostTextBox.Text + Environment.NewLine;
            if (!(Close_Predoplata == PredoplataTextBox.Text)) history += "Изменено: " + Close_Predoplata + " НА: " + PredoplataTextBox.Text + Environment.NewLine;
            if (!(Close_Zatraty == ZatratyTextBox.Text)) history += "Изменено: " + Close_Zatraty + " НА: " + ZatratyTextBox.Text + Environment.NewLine;
            if (!(Close_Price == PriceTextBox.Text)) history += "Изменено: " + Close_Price + " НА: " + PriceTextBox.Text + Environment.NewLine;
            if (!(Close_Skidka == SkidkaTextBox.Text)) history += "Изменено: " + Close_Skidka + " НА: " + SkidkaTextBox.Text + Environment.NewLine;
            if (!(Close_Status == StatusComboBox.Text)) history += "Изменено: " + Close_Status + " НА: " + StatusComboBox.Text + Environment.NewLine;
            if (!(Close_Master == MasterComboBox.Text)) history += "Изменено: " + Close_Master + " НА: " + MasterComboBox.Text + Environment.NewLine;
            if (!(Close_VipolnenieRaboti == VipolnenieRabotiTextBox.Text)) history += "Изменено: " + Close_VipolnenieRaboti + " НА: " + VipolnenieRabotiTextBox.Text + Environment.NewLine;
            if (!(Close_AdressKlient == AdressKlientTextBox.Text)) history += "Изменено: " + Close_AdressKlient + " НА: " + AdressKlientTextBox.Text + Environment.NewLine;
            if (!(Close_ServiceAdress == ServiceAdressComboBox.Text)) history += "Изменено: " + Close_ServiceAdress + " НА: " + ServiceAdressComboBox.Text + Environment.NewLine;
            if (!(Close_DeviceColour == DeviceColourComboBox.Text)) history += "Изменено: " + Close_DeviceColour + " НА: " + DeviceColourComboBox.Text + Environment.NewLine;

            return history;
        }
        //Преобразовать номер в номер без пробелов и т.п.
        private string PhoneToNorm(string phone)
        {
            return phone.Replace(" ", "");
        }
        //Клиент в курсе
        private string KlinentVKurseTester()
        {
            if (KlientVKurse.Checked == true)
                return "1";
            else
                return "";
        }
        // В зависимости от обстоятельств, ставит дату выдачи
        private string VidachiDate()
        {
            if (mainForm.basa.BdReadOne("Data_vidachi", id_bd) != "")
                return mainForm.basa.BdReadOne("Data_vidachi", id_bd);
            else
                return "";
        }
        // В зависимости от того, есть ли предоплата добавляет дату
        private string PredoplataDate(string str1)
        {
            if (str1 != "" && str1 != "0")
            {
                return mainForm.basa.BdReadOne("Data_predoplaty", id_bd);
            }
            else
                return "";
        }

        // В зависимости от того, стоит ли глачка "Требует заказа" выдает нужное значение
        private string NeedZakaz()
        {
            if (checkBox3.Checked)
                return "Заказать";
            else
                return "";
        }
        private void DataEditorButton_Click(object sender, EventArgs e)
        {
            DataEditor dt1 = new DataEditor(mainForm, id_bd, this);
            this.Enabled = false;
            dt1.Show();
        }
        public void MyMethod(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            Editor ed1 = new Editor(mainForm, lb.Tag.ToString());
            ed1.Show(mainForm);
        }
        // История ремонта
        public void ClientsHistoryMaker(string clientNum)
        {

            DataTable dtHistory = mainForm.basa.ClientsShowHistory(clientNum);
            int left = 3;
            int top = 3;
            int width = panel2.Width - left * 2 - 2;
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
                if (top > panel2.Height)
                {
                    sLabel.MinimumSize = new System.Drawing.Size(width - 18, 107);
                }
            }
            panel2.Controls.Clear();
            ToolTip t = new ToolTip();
            t.AutoPopDelay = 30000;
            int x = 0;
            foreach (Label label in labelList1)
            {
                label.MouseClick += MyMethod;
                this.panel2.Controls.Add(label);
                t.SetToolTip(label, "Адрес СЦ: " + dtHistory.Rows[x].ItemArray[27].ToString() + Environment.NewLine +
                    "Выполненные работы: " + dtHistory.Rows[x].ItemArray[22].ToString() + Environment.NewLine +
                     "Мастер: " + dtHistory.Rows[x].ItemArray[21].ToString() + Environment.NewLine +
                     "Поломка: " + dtHistory.Rows[x].ItemArray[13].ToString() + Environment.NewLine +
                     "Комментарий: " + dtHistory.Rows[x].ItemArray[14].ToString() + Environment.NewLine);
                x++;
            }



        }
        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)       // Ctrl-S Save
            {
                // Do what you want here
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
                this.Close();
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                try
                {
                    int x = int.Parse(ZatratyTextBox.Text);
                    int y = int.Parse(PriceTextBox.Text);
                    y += x;
                    PriceTextBox.Text = y.ToString();
                }
                catch { MessageBox.Show("В полях затрат и окончательной стоимости должны стоять цифры"); }
            }
        }
        private void Editor_Load(object sender, EventArgs e)
        {
            // Чтобы горячие клавиши отрабатывались
            this.KeyPreview = true;
            //Права пользователя
            DeleteButton.Enabled = (TemporaryBase.delZapis == "1") ? true : false;
            SaveButton.Enabled = (TemporaryBase.saveZapis == "1") ? true : false;
            AktPriemaButton.Enabled = (TemporaryBase.saveZapis == "1") ? true : false;
            AktVidachiButton.Enabled = (TemporaryBase.saveZapis == "1") ? true : false;
            AktPriemaGarantiiButton.Enabled = (TemporaryBase.saveZapis == "1") ? true : false;
            AktVidachiGarantiiButton.Enabled = (TemporaryBase.saveZapis == "1") ? true : false;
            DataEditorButton.Enabled = (TemporaryBase.dates == "1") ? true : false;
            GoToTheStockButton.Enabled = (TemporaryBase.stock == "1") ? true : false;
            mainForm.MainListView.Enabled = false;
            DataTable dt1 = mainForm.basa.BdReadOneEditor(id_bd);
            try
            {
                if (dt1.Rows.Count > 0)
                {
                    NumberLabel.Text = "Редактирование записи номер: " + dt1.Rows[0].ItemArray[0].ToString();
                    // Подгружаем данные о клиенте
                    ClientId_inBase = dt1.Rows[0].ItemArray[29].ToString();
                    label30.Text = "Редактирование клиента номер: " + ClientId_inBase;
                    DataTable dt2 = mainForm.basa.ClientsMapGiver(ClientId_inBase);


                    ClientsHistoryMaker(ClientId_inBase); //Добавляем историю клиента
                    Close_What_remont = What_remont_combo_box.Text = dt1.Rows[0].ItemArray[7].ToString();
                    Close_Brand = BrandComboBox.Text = dt1.Rows[0].ItemArray[8].ToString();
                    Close_Model = ModelTextBox.Text = dt1.Rows[0].ItemArray[9].ToString();
                    Close_Serial = SerialTextBox.Text = dt1.Rows[0].ItemArray[10].ToString();
                    Close_Sostoyanie = SostoyanieTextBox.Text = dt1.Rows[0].ItemArray[11].ToString();
                    Close_Komplektnost = KomplektnostTextBox.Text = dt1.Rows[0].ItemArray[12].ToString();
                    Close_Polomka = PolomkaTextBox.Text = dt1.Rows[0].ItemArray[13].ToString();
                    Close_kommentarij = kommentarijTextBox.Text = dt1.Rows[0].ItemArray[14].ToString();
                    Close_PredvaritelnayaStoimost = PredvaritelnayaStoimostTextBox.Text = dt1.Rows[0].ItemArray[15].ToString();
                    Close_Predoplata = PredoplataTextBox.Text = dt1.Rows[0].ItemArray[16].ToString();
                    // Цену и затраты перенес в низ, для того, чтобы корректно отображался текст заголовка окна
                    Close_Skidka = SkidkaTextBox.Text = dt1.Rows[0].ItemArray[19].ToString();
                    Close_Status = StatusComboBox.Text = dt1.Rows[0].ItemArray[20].ToString();
                    Close_Master = MasterComboBox.Text = dt1.Rows[0].ItemArray[21].ToString();
                    Close_VipolnenieRaboti = VipolnenieRabotiTextBox.Text = dt1.Rows[0].ItemArray[22].ToString();


                    if (dt1.Rows[0].ItemArray[2].ToString() != "")
                    {
                        int daysOutOfOffice = 0;
                        DateTime timeVidachi = new DateTime();
                        DateTime timeNow = new DateTime();
                        timeNow = DateTime.Now;
                        timeVidachi = DateTime.Parse(dt1.Rows[0].ItemArray[2].ToString());
                        daysOutOfOffice = (timeNow - timeVidachi).Days;

                        this.Text += " | Дней со дня выдачи прошло: " + daysOutOfOffice;
                    }


                    if (dt1.Rows[0].ItemArray[23].ToString() == "")
                    {
                        GarantyComboBox.Text = TemporaryBase.EditorGarantyComboboxVal;
                    }
                    else
                    {
                        GarantyComboBox.Text = dt1.Rows[0].ItemArray[23].ToString();
                    }
                    // Ждет заказа
                    if (dt1.Rows[0].ItemArray[24].ToString() == "")
                    {
                        checkBox3.Checked = false;
                    }
                    else
                    {
                        checkBox3.Checked = true;
                    }
                    Close_Garanty = dt1.Rows[0].ItemArray[23].ToString();
                    Close_Wait_zakaz = dt1.Rows[0].ItemArray[24].ToString();
                    Close_KlientGalochkaVKurse = dt1.Rows[0].ItemArray[26].ToString();


                    KlientGalochkaVKurse(dt1.Rows[0].ItemArray[26].ToString());
                    Close_ServiceAdress = ServiceAdressComboBox.Text = dt1.Rows[0].ItemArray[27].ToString();
                    Close_DeviceColour = DeviceColourComboBox.Text = dt1.Rows[0].ItemArray[28].ToString();

                    autoButton = false;

                    //Блокируем кнопки приёма и выдачи по гарантии, если нет даты выдачи
                    if (dt1.Rows[0].ItemArray[2].ToString() != "")
                    {
                        AktPriemaGarantiiButton.Enabled = true;
                        AktVidachiGarantiiButton.Enabled = true;
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        ClientFioTextBox.Text = Close_surname = SurnameTextBox.Text = dt2.Rows[0].ItemArray[1].ToString();
                        ClientPhoneTextBox.Text = Close_phone = phoneTextBox.Text = dt2.Rows[0].ItemArray[2].ToString();
                        ClientAdressTextBox.Text = Close_AdressKlient = AdressKlientTextBox.Text = dt2.Rows[0].ItemArray[3].ToString();
                        ClientAboutUsComboBox.Text = Close_AboutUs = AboutUsComboBox.Text = dt2.Rows[0].ItemArray[7].ToString();
                        BlackListComboBox.Text = KlientBlistDecoder(dt2.Rows[0].ItemArray[5].ToString(), "");
                        PrimechanieTextBox.Text = dt2.Rows[0].ItemArray[4].ToString();
                        this.Text += " | " + KlientBlistDecoder(dt2.Rows[0].ItemArray[5].ToString());

                    }
                    else
                        MessageBox.Show("Нет данных о клиенте, обратитесь к разработчику vk.com/scrypto");

                    // Запоминаем данные при загрузке, чтобы при выключении делать запрос


                    formCaptionText = this.Text;
                    Close_Zatraty = ZatratyTextBox.Text = dt1.Rows[0].ItemArray[17].ToString();
                    Close_Price = PriceTextBox.Text = dt1.Rows[0].ItemArray[18].ToString();
                }

                foreach (string strCombo in TemporaryBase.SortirovkaVipolnRaboti)
                {
                    VipolnenieRabotiComboBox.Items.Add(strCombo);
                }
                foreach (string strCombo in TemporaryBase.SortirovkaGaranty)
                {
                    GarantyComboBox.Items.Add(strCombo);
                }
                foreach (string strCombo in TemporaryBase.SortirovkaMasters)
                {
                    MasterComboBox.Items.Add(strCombo);
                }


                foreach (string strCombo in TemporaryBase.SortirovkaColour)
                {
                    DeviceColourComboBox.Items.Add(strCombo);
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
                //История ремонта
                foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaAboutUs)
                {
                    AboutUsComboBox.Items.Add(ssp.SortObj);
                }
                DynamicLabelMaker();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы" + Environment.NewLine + ex.ToString());
            }

        }

        private string KlientBlistDecoder(string blist)
        {
            if (blist == "0") return "Клиент не проблемный";
            else if (blist == "1") return "Клиент проблемный";
            else return "";

        }
        private string KlientBlistDecoder(string blist, string str)
        {
            if (blist == "0") return "Не проблемный";
            else if (blist == "1") return "Проблемный";
            else return "";

        }
        // История ремонта

        public void DynamicLabelMaker()
        {
            DataTable dtStates1 = mainForm.basa.StatesMapGiver(id_bd);
            int left = 3;
            int top = 3;
            int width = panel1.Width - left * 2 - 2;
            List<Label> labelList1 = new List<Label>();
            for (int i = 0; i < dtStates1.Rows.Count; i++)
            {
                Label statusLable = new Label();
                statusLable.Text = dtStates1.Rows[i].ItemArray[2].ToString() + Environment.NewLine + dtStates1.Rows[i].ItemArray[3].ToString();
                statusLable.AutoSize = true;
                statusLable.BorderStyle = BorderStyle.FixedSingle;
                statusLable.Location = new Point(left, top);
                statusLable.BackColor = Color.White;
                top = top + statusLable.Height + 27;
                statusLable.Font = new Font("Arial", 9, FontStyle.Bold);
                labelList1.Add(statusLable);
            }
            panel1.Controls.Clear();
            foreach (Label sLabel in labelList1)
            {
                if (top > panel1.Height)
                {
                    sLabel.MinimumSize = new System.Drawing.Size(width - 18, sLabel.Height);
                }
                else
                    sLabel.MinimumSize = new System.Drawing.Size(width, sLabel.Height);
                this.panel1.Controls.Add(sLabel);
            }

        }


        //Ставит галочку, если клиент в курсе
        private void KlientGalochkaVKurse(string str)
        {
            if (str == "1")
            {
                KlientVKurse.Checked = true;
            }
            else
                KlientVKurse.Checked = false;
        }

        private void NumberLabel_MouseDown(object sender, MouseEventArgs e)
        {
            NumberLabel.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label13_MouseDown_1(object sender, MouseEventArgs e)
        {
            label13.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить запись номер " + id_bd + " ?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //  mainForm.basa.BdDelete(id_bd);
                // помечаем запись, как удаленную
                mainForm.basa.BdEditOne("Deleted", "1", id_bd);
                mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " удалена";
                // Удаляем из склада, все упоминания о записи
                mainForm.basa.BdStockMapDelete(id_bd);
                TemporaryBase.SearchFULLBegin();
                // mainForm.MainListViewUpdate(id_bd);
                //Запись в базу истории
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Удаление", "", id_bd);
                this.Close();
            }
        }

        private void AktVidachiButton_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                // Обработка обязательных полей
                bool yerror = false;
                string obPolya = "";
                if (VipolnenieRabotiTextBox.Text == "")
                {
                    obPolya += "Заполните поле выполненных работ " + Environment.NewLine;
                    yerror = true;
                }
                if (MasterComboBox.Text == "")
                {
                    obPolya += "Выберите Мастера " + Environment.NewLine;
                    yerror = true;
                }
                if (yerror)
                {
                    MessageBox.Show(obPolya, "Не заполнены обязательные поля");
                }
                if (!yerror)
                {

                    if (MessageBox.Show("Сохранить все изменения и напечатать акт выдачи?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        WhenClosing = true;
                        autoBool = true;
                        fuckthemool = true;
                        StatusComboBox.Text = "Выдан";
                        mainForm.basa.ClientsMapEditInEditor(SurnameTextBox.Text, PhoneToNorm(phoneTextBox.Text), AdressKlientTextBox.Text, AboutUsComboBox.Text, ClientId_inBase);
                        mainForm.basa.BdEdit(PredoplataDate(PredoplataTextBox.Text), VidachiDateWhenAktP(), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
                           SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text),
                           EmptyStringToZeroMaker(PredoplataTextBox.Text), EmptyStringToZeroMaker(ZatratyTextBox.Text), EmptyStringToZeroMaker(PriceTextBox.Text), EmptyStringToZeroMaker(SkidkaTextBox.Text), StatusComboBox.Text, MasterComboBox.Text, VipolnenieRabotiTextBox.Text, GarantyComboBox.Text, "", "", "", id_bd, ServiceAdressComboBox.Text, DeviceColourComboBox.Text);

                        mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " сохранена";

                        if (mainForm.ShowPhoneWaitingButton.Checked == true)
                        {
                            TemporaryBase.SearchFULLBegin("1");
                        }
                        else
                        {
                            TemporaryBase.SearchFULLBegin();
                        }
                        Printing_AKT_VIDACHI actVid1 = new Printing_AKT_VIDACHI(mainForm.basa.BdReadOneEditor(id_bd), mainForm, TemporaryBase.valuta);
                        actVid1.Show();
                        //Запись в базу истории
                        mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Печать акта выдачи", editorHistoryMaker(), id_bd);
                        this.Close();
                    }
                }
            }
            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");

        }

        // Еще немного работаем с датой выдачи, чтобы не было переписи дат, при повторном акте выдачи
        private string VidachiDateWhenAktP()
        {
            if (mainForm.basa.BdReadOne("Data_vidachi", id_bd) != "")
            {
                return mainForm.basa.BdReadOne("Data_vidachi", id_bd);
            }
            else
            {
                return DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                if (MessageBox.Show("Сохранить все изменения и напечатать акт приема?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    autoBool = true;
                    fuckthemool = true;
                    WhenClosing = true;
                    mainForm.basa.ClientsMapEditInEditor(SurnameTextBox.Text, PhoneToNorm(phoneTextBox.Text), AdressKlientTextBox.Text, AboutUsComboBox.Text, ClientId_inBase);
                    mainForm.basa.BdEdit(PredoplataDate(PredoplataTextBox.Text), VidachiDate(), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
                         SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text),
                         EmptyStringToZeroMaker(PredoplataTextBox.Text), EmptyStringToZeroMaker(ZatratyTextBox.Text), EmptyStringToZeroMaker(PriceTextBox.Text), EmptyStringToZeroMaker(SkidkaTextBox.Text), StatusComboBox.Text, MasterComboBox.Text, VipolnenieRabotiTextBox.Text, GarantyComboBox.Text, NeedZakaz(), "", KlinentVKurseTester(), id_bd, ServiceAdressComboBox.Text, DeviceColourComboBox.Text);
                    mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " сохранена";

                    // mainForm.MainListViewUpdate(id_bd);
                    if (mainForm.ShowPhoneWaitingButton.Checked == true)
                    {
                        TemporaryBase.SearchFULLBegin("1");
                    }
                    else
                    {
                        TemporaryBase.SearchFULLBegin();
                    }
                    Printing_AKT_PRIEMA actPriema1 = new Printing_AKT_PRIEMA(mainForm.basa.BdReadOneEditor(id_bd), mainForm, TemporaryBase.valuta);
                    actPriema1.Show();
                    //Запись в базу истории
                    mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Печать акта приёма", editorHistoryMaker(), id_bd);
                    this.Close();
                }
            }
            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");
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

        private void ZatratyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
              if (ZatratyTextBox.Text == "" && e.KeyChar == (char)'0')
            {
                e.Handled = true;
            }
        }

        private void PriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    if (e.KeyChar != (char)',')
                        e.Handled = true;
                }
            }
            else if (PriceTextBox.Text == "" && e.KeyChar == (char)'0')
            {
                e.Handled = true;
            }
        }

        private void SkidkaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
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

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.MainListView.Enabled = true;
        }

        private void VipolnenieRabotiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VipolnenieRabotiComboBox.SelectedIndex != -1)
            {
                VipolnenieRabotiTextBox.AppendText(VipolnenieRabotiComboBox.Items[VipolnenieRabotiComboBox.SelectedIndex].ToString() + Environment.NewLine);
                if (VipolnenieRabotiComboBox.SelectedItem.ToString().ToLower().Contains("без ремонта"))
                {
                    GarantyComboBox.SelectedIndex = 0;
                }
            }
        }

        private void SkidkaTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (SkidkaTextBox.Text == "0")
            {
                SkidkaTextBox.Text = "";
            }
        }

        private void PriceTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (PriceTextBox.Text == "0")
            {
                PriceTextBox.Text = "";
            }
        }

        private void PredoplataTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (PredoplataTextBox.Text == "0")
            {
                PredoplataTextBox.Text = "";
            }
        }

        private void PredvaritelnayaStoimostTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (PredvaritelnayaStoimostTextBox.Text == "0")
            {
                PredvaritelnayaStoimostTextBox.Text = "";
            }
        }

        private void ZatratyTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ZatratyTextBox.Text == "0")
            {
                ZatratyTextBox.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                StatusComboBox.Text = "Ждёт запчасть";
            }
            if (autoButton == false)
            {
                if (checkBox3.Checked)
                {
                    mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлена галочка" + Environment.NewLine + "Требует заказа");
                    DynamicLabelMaker();
                }
                else
                {
                    mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Снята галочка" + Environment.NewLine + "Требует заказа");
                    DynamicLabelMaker();
                }
            }
        }

        private void KlientVKurse_CheckedChanged(object sender, EventArgs e)
        {
            //Чтобы не проставлялось при запуске формы
            if (autoButton == false)
            {
                if (KlientVKurse.Checked)
                {
                    mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлена галочка" + Environment.NewLine + "Нужно позвонить");
                    DynamicLabelMaker();
                }

                else
                {
                    mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Снята галочка" + Environment.NewLine + "Нужно позвонить");
                    DynamicLabelMaker();
                }
            }
        }

        private void GoToTheStockButton_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.stockR)
            {
                if (TemporaryBase.baseR == true)
                {
                    this.WindowState = FormWindowState.Minimized;
                    if (ZatratyTextBox.Text == "")
                    {
                        ZatratyTextBox.Text = "0";
                    }
                    Stock stockEd = new Stock(mainForm, id_bd, this, ClientId_inBase);
                    stockEd.Show(mainForm);
                    this.Enabled = false;
                }
                else MessageBox.Show("Доступ к складу только в полной версии, приобрести можно тут vk.com/scrypto");
            }
            else

                MessageBox.Show("Склад продаётся, как отдельный модуль, для полной версии, все вопросы: vk.com/scrypto");
        }

        private void AktPriemaGarantii_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                if (MessageBox.Show("Сохранить все изменения и напечатать акт приема по гарантии?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    WhenClosing = true;
                    autoBool = true;
                    fuckthemool = true;
                    StatusComboBox.Text = "Принят по гарантии";
                    kommentarijTextBox.AppendText(Environment.NewLine + "Устройство было принято на гарантийный ремонт " + DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
                    mainForm.basa.ClientsMapEditInEditor(SurnameTextBox.Text, PhoneToNorm(phoneTextBox.Text), AdressKlientTextBox.Text, AboutUsComboBox.Text, ClientId_inBase);
                    mainForm.basa.BdEditVidachiPoGarantii(PredoplataDate(PredoplataTextBox.Text), VidachiDate(), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
                         SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text),
                         EmptyStringToZeroMaker(PredoplataTextBox.Text), EmptyStringToZeroMaker(ZatratyTextBox.Text), EmptyStringToZeroMaker(PriceTextBox.Text), EmptyStringToZeroMaker(SkidkaTextBox.Text), StatusComboBox.Text, MasterComboBox.Text, VipolnenieRabotiTextBox.Text, GarantyComboBox.Text, NeedZakaz(), "", KlinentVKurseTester(), id_bd, ServiceAdressComboBox.Text, DeviceColourComboBox.Text);
                    mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " сохранена";

                    // mainForm.MainListViewUpdate(id_bd);
                    if (mainForm.ShowPhoneWaitingButton.Checked == true)
                    {
                        TemporaryBase.SearchFULLBegin("1");
                    }
                    else
                    {
                        TemporaryBase.SearchFULLBegin();
                    }
                    ActPriemaPoGarantii actPriema1 = new ActPriemaPoGarantii(mainForm.basa.BdReadOneEditor(id_bd), mainForm, TemporaryBase.valuta);
                    actPriema1.Show();
                    mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Печать акта приёма по гарантии", editorHistoryMaker(), id_bd);
                    this.Close();
                }
            }
            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");
        }

        private void AktVidachiGarantiiButton_Click(object sender, EventArgs e)
        {
            if (TemporaryBase.baseR == true)
            {
                // Обработка обязательных полей
                bool yerror = false;
                string obPolya = "";
                if (VipolnenieRabotiTextBox.Text == "")
                {
                    obPolya += "Заполните поле выполненных работ " + Environment.NewLine;
                    yerror = true;
                }
                if (MasterComboBox.Text == "")
                {
                    obPolya += "Выберите Мастера " + Environment.NewLine;
                    yerror = true;
                }
                if (yerror)
                {
                    MessageBox.Show(obPolya, "Не заполнены обязательные поля");
                }
                if (!yerror)
                {

                    if (MessageBox.Show("Сохранить все изменения и напечатать акт выдачи?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        WhenClosing = true;
                        autoBool = true;
                        fuckthemool = true;
                        StatusComboBox.Text = "Выдан";
                        kommentarijTextBox.AppendText(Environment.NewLine + "Устройство было выдано после гарантийного ремонта " + DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
                        mainForm.basa.ClientsMapEditInEditor(SurnameTextBox.Text, PhoneToNorm(phoneTextBox.Text), AdressKlientTextBox.Text, AboutUsComboBox.Text, ClientId_inBase);
                        mainForm.basa.BdEditVidachiPoGarantii(PredoplataDate(PredoplataTextBox.Text), VidachiDateWhenAktP(), "", "", "", What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text,
                           SerialTextBox.Text, SostoyanieTextBox.Text, KomplektnostTextBox.Text, PolomkaTextBox.Text, kommentarijTextBox.Text, EmptyStringToZeroMaker(PredvaritelnayaStoimostTextBox.Text),
                           EmptyStringToZeroMaker(PredoplataTextBox.Text), EmptyStringToZeroMaker(ZatratyTextBox.Text), EmptyStringToZeroMaker(PriceTextBox.Text), EmptyStringToZeroMaker(SkidkaTextBox.Text), StatusComboBox.Text, MasterComboBox.Text, VipolnenieRabotiTextBox.Text, GarantyComboBox.Text, "", "", "", id_bd, ServiceAdressComboBox.Text, DeviceColourComboBox.Text);

                        mainForm.StatusStripLabel.Text = "Запись номер " + id_bd + " сохранена";

                        if (mainForm.ShowPhoneWaitingButton.Checked == true)
                        {
                            TemporaryBase.SearchFULLBegin("1");
                        }
                        else
                        {
                            TemporaryBase.SearchFULLBegin();
                        }
                        ActVidachiPoGarantii actVid1 = new ActVidachiPoGarantii(mainForm.basa.BdReadOneEditor(id_bd), mainForm, TemporaryBase.valuta);
                        actVid1.Show();
                        mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Печать акта выдачи по гарантии", editorHistoryMaker(), id_bd);
                        this.Close();
                    }
                }
            }
            else MessageBox.Show("Акты приема и выдачи доступны только в полной версии https://vk.com/clubremontuchet");

        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Запрос перед закрытием, если что-то изменилось
            DataTable dt1 = mainForm.basa.BdReadOneEditor(id_bd);
            try
            {
                if (dt1.Rows.Count > 0)
                {

                    if (!WhenClosing)
                    {
                        if (!(Close_surname == SurnameTextBox.Text &&
                            Close_phone == phoneTextBox.Text &&
                            Close_AboutUs == AboutUsComboBox.Text &&
                            Close_What_remont == What_remont_combo_box.Text &&
                            Close_Brand == BrandComboBox.Text &&
                            Close_Model == ModelTextBox.Text &&
                            Close_Serial == SerialTextBox.Text &&
                            Close_Sostoyanie == SostoyanieTextBox.Text &&
                            Close_Komplektnost == KomplektnostTextBox.Text &&
                            Close_Polomka == PolomkaTextBox.Text &&
                            Close_kommentarij == kommentarijTextBox.Text &&
                            Close_PredvaritelnayaStoimost == PredvaritelnayaStoimostTextBox.Text &&
                            Close_Predoplata == PredoplataTextBox.Text &&
                            Close_Zatraty == ZatratyTextBox.Text &&
                            Close_Price == PriceTextBox.Text &&
                            Close_Skidka == SkidkaTextBox.Text &&
                            Close_Status == StatusComboBox.Text &&
                            Close_Master == MasterComboBox.Text &&
                            Close_VipolnenieRaboti == VipolnenieRabotiTextBox.Text &&
                            Close_Garanty == dt1.Rows[0].ItemArray[23].ToString() &&
                            Close_Wait_zakaz == dt1.Rows[0].ItemArray[24].ToString() &&
                            Close_KlientGalochkaVKurse == dt1.Rows[0].ItemArray[26].ToString() &&
                            Close_AdressKlient == AdressKlientTextBox.Text &&
                            Close_ServiceAdress == ServiceAdressComboBox.Text &&
                            Close_DeviceColour == DeviceColourComboBox.Text))
                        {
                            if (MessageBox.Show("Есть несохраненные изменения, вы точно хотитие закрыть окно?",
                            "Выход",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                            {
                                e.Cancel = true;
                            }

                            else
                            {
                                mainForm.lviewReturnSelectedIndex();
                                e.Cancel = false;
                            }

                        }
                        mainForm.lviewReturnSelectedIndex();
                    }
                    mainForm.lviewReturnSelectedIndex();

                }
                mainForm.lviewReturnSelectedIndex();
            }
            catch
            {

            }

        }

        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!autoButton)
            {
                if (!autoBool)
                {

                    if (StatusComboBox.Text == "Выдан" && Close_Status != "Выдан")
                    {
                        if (MessageBox.Show("Вы поставили статус Выдан в ручном режиме, если вы нажмёте OK, база автоматически установит дату выдачи для этого заказа", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            mainForm.basa.BdEditOne("Data_vidachi", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), id_bd);
                            mainForm.basa.BdEditOne("Status_remonta", "Выдан", id_bd);
                            Close_Status = "Выдан";
                            StatusComboBox.Enabled = false;
                            TemporaryBase.SearchFULLBegin();
                        }
                        else
                        {
                            fuckthemool = false;
                            StatusComboBox.Text = Close_Status;
                        }


                    }

                }
            }
            if (!autoButton && fuckthemool)
            {
                mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH-mm"), "Установлен статус " + Environment.NewLine + StatusComboBox.Text);
                DynamicLabelMaker();
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            States st1 = new States(mainForm, id_bd, this);
            st1.Show();
            this.Enabled = false;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                panel2.Visible = false;
                panel1.Visible = true;
                RepairHistoryLabel.Text = "История ремонта";
                this.Width = 1069;

            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                panel2.Visible = true;
                panel1.Visible = false;
                RepairHistoryLabel.Text = "История клиента";
                this.Width = 573;
            }
        }

        private void SaveClientButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить данные о клиенте?" + Environment.NewLine + "Сохраняются только данные о клиенте, окно не закроется, так что информацию о записи можно сохранить во вкладке запись", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.ClientsMapEditWithoutDate(ClientFioTextBox.Text, PhoneToNorm(ClientPhoneTextBox.Text), ClientAdressTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), ClientAboutUsComboBox.Text, ClientId_inBase);
                Close_surname = SurnameTextBox.Text = ClientFioTextBox.Text;
                Close_phone = phoneTextBox.Text = ClientPhoneTextBox.Text;
                Close_AdressKlient = AdressKlientTextBox.Text = ClientAdressTextBox.Text;
                Close_AboutUs = AboutUsComboBox.Text = ClientAboutUsComboBox.Text;
                this.Text = "Редактирование | " + KlientBlistDecoder(BlistOfClients());
                TemporaryBase.SearchFULLBegin();
            }

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

        private void MasterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!autoButton)
            {
                mainForm.basa.StatesMapWrite(id_bd, DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "Назначен мастер " + Environment.NewLine + MasterComboBox.Text);
                DynamicLabelMaker();
            }

        }

        private void klientPage_Click(object sender, EventArgs e)
        {

        }

        private void SerialTextBox_TextChanged(object sender, EventArgs e)
        {
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

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            string path = "ClientFiles\\" + ClientId_inBase + "\\" + id_bd;
            if (!Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            System.Diagnostics.Process.Start("explorer", "" + path);
        }

        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            SummOfRemontCounter();
        }

        private void SummOfRemontCounter()
        {
            try
            {

                if (PriceTextBox.Text != "" && ZatratyTextBox.Text != "" && PriceTextBox.Text != "0")
                    this.Text = formCaptionText + "| Стоимость ремонта: " + (decimal.Parse(PriceTextBox.Text) - decimal.Parse(ZatratyTextBox.Text)) + "| Мастеру: " + (decimal.Parse(PriceTextBox.Text) - decimal.Parse(ZatratyTextBox.Text)) * (decimal.Parse(TemporaryBase.Mpersent) * 0.01M);
                else if (PriceTextBox.Text == "0")
                    this.Text = formCaptionText + "| Стоимость ремонта: Не указана окончательная стоимость";

            }
            catch { MessageBox.Show("Это поле должно содержать только циры, без текста"); }
        }

        private void ZatratyTextBox_TextChanged(object sender, EventArgs e)
        {
            SummOfRemontCounter();
        }

        private void SendSMSButton_Click(object sender, EventArgs e)
        {
            SmsFromEditor sfe = new SmsFromEditor(SurnameTextBox.Text, phoneTextBox.Text, StatusComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text, ModelTextBox.Text, PriceTextBox.Text, SkidkaTextBox.Text, PredoplataTextBox.Text, PredvaritelnayaStoimostTextBox.Text,
                mainForm, id_bd);
            sfe.Show(this);

        }

        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            //Ставим пробелы в телефонный номер при вводе
            if (Char.IsDigit(key) || (key == (char)44) || key == (char)8 || key == (char)32 || key == (char)43) { }
            else e.Handled = true;
        }

        private void phoneTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

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

        private void TrackButton_Click(object sender, EventArgs e)
        {
            TrackingMail tm = new TrackingMail(TrackMaker());
            tm.Show(this);
        }
        public List<Track> TrackMaker()
        {
            string pattern = @"###(\w+)(.*)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(kommentarijTextBox.Text);
            List<Track> tracks = new List<Track>();
            while (match.Success)
            {
                tracks.Add(new Track(match.Groups[1].Value, match.Groups[2].Value));
                match = match.NextMatch();
            }

            return tracks;
        }
    }
}
