using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWork2
{

    public partial class Settings : Form
    {
        string delZapis = "1";
        string addZapis = "1";
        string saveZapis = "1";
        string graf = "1";
        string sms = "1";
        string stock = "1";
        string clients = "1";
        string stockAdd = "1";
        string stockDel = "1";
        string stockEdit = "1";
        string clientAdd = "1";
        string clientDel = "1";
        string clientConcat = "1";
        string settings = "1";
        string dates = "1";
        string editDates = "1";
        Form1 mainForm;
        IniFile INIF = new IniFile("Config.ini");
        string color1 = "-8323328";
        public Settings(Form1 fm1)
        {
            mainForm = fm1;
            InitializeComponent();
            //Прописываем true в значение переменной, чтобы можно было контроллировать открытие закрытие дочерних форм
            mainForm.setBool = true;

            //Заморочка с цветом строк, где дата диагностики меньше текущей на n дней

            colorDialog1.FullOpen = true;
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "daysDiagnostik"))
            {
                NumericDiagnosikDays.Value = -1 * (decimal.Parse(INIF.ReadINI("PROGRAMM_SETTINGS", "daysDiagnostik")));
            }
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorDiagnostik"))
            {
                colorDialog1.Color = Color.FromArgb(int.Parse(INIF.ReadINI("PROGRAMM_SETTINGS", "colorDiagnostik")));
            }
            else
                colorDialog1.Color = Color.YellowGreen;
            ColorButton.BackColor = colorDialog1.Color;
            //Восстанавливаем значение галочки цвета и дней
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "colorCheckBox"))
            {
                if (INIF.ReadINI("PROGRAMM_SETTINGS", "colorCheckBox") == "Checked")
                {
                    ColorDiagnosticCheckBox.Checked = true;
                }
                else
                {
                    ColorDiagnosticCheckBox.Checked = false;
                }

            }
            //Восстанавливаем валюту
            if (INIF.KeyExists("PROGRAMM_SETTINGS", "valuta"))
            {
                ValutaTextBox.Text = INIF.ReadINI("PROGRAMM_SETTINGS", "valuta");
            }
            else
            {
                ValutaTextBox.Text = "Рублей";
                TemporaryBase.valuta = ValutaTextBox.Text;
                INIF.WriteINI("PROGRAMM_SETTINGS", "valuta", ValutaTextBox.Text);
            }


        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            //Задаем цвет кнопки и строки
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            color1 = colorDialog1.Color.ToArgb().ToString();
            ColorButton.BackColor = colorDialog1.Color;

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить настройки?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //Сохраняем настройки
                INIF.WriteINI("PROGRAMM_SETTINGS", "daysDiagnostik", (NumericDiagnosikDays.Value * -1).ToString());
                INIF.WriteINI("PROGRAMM_SETTINGS", "colorDiagnostik", color1);
                try { TemporaryBase.backOfColour = Color.FromArgb(int.Parse(color1)); } catch { }
                INIF.WriteINI("PROGRAMM_SETTINGS", "colorCheckBox", ColorDiagnosticCheckBox.CheckState.ToString());
                INIF.WriteINI("PROGRAMM_SETTINGS", "NessesaryCHB", NessesaryCheckBox.CheckState.ToString());
                if (NessesaryCheckBox.Checked)
                {
                    TemporaryBase.Nessesary = true;
                }
                else
                    TemporaryBase.Nessesary = false;
                INIF.WriteINI("PROGRAMM_SETTINGS", "valuta", ValutaTextBox.Text);
                INIF.WriteINI("PROGRAMM_SETTINGS", "Mpersent", MpersentTbox.Text.Trim());
                INIF.WriteINI("CHECKBOX", "garantyDefault", GarantyDefaultCheckBox.CheckState.ToString());
                if (GarantyDefaultCheckBox.Checked) TemporaryBase.EditorGarantyComboboxVal = "Без Гарантии"; else TemporaryBase.EditorGarantyComboboxVal = "30 дней";
                INIF.WriteINI("CHECKBOX", "EveryDayBackup", EveryDayBackupCheckBox.CheckState.ToString());
                INIF.WriteINI("SMSSEND", "Token", TokenTextBox.Text);
                INIF.WriteINI("SMSSEND", "PhoneId", phoneIdTextBox.Text);
                INIF.WriteINI("SMSSEND", "KodCountry", KodCountry.Text.Replace(" ", ""));
                INIF.WriteINI("SMSSEND", "KodCountryZamena", KodCountryZamena.Text.Replace(" ", ""));
                TemporaryBase.smsToken = TokenTextBox.Text;
                TemporaryBase.smsPhoneId = phoneIdTextBox.Text;
                TemporaryBase.everyDayBackup = EveryDayBackupCheckBox.CheckState.ToString();
                TemporaryBase.smsPhone = KodCountry.Text.Replace(" ", "");
                TemporaryBase.smsPhonePref = KodCountryZamena.Text.Replace(" ", "");
                if (ServiceAdressComboBox.SelectedIndex >= 0)
                {
                    //  AdressSCDefault Адрес сервисного центра по умолчанию
                    TemporaryBase.AdressSCDefault = ServiceAdressComboBox.SelectedIndex.ToString();
                    INIF.WriteINI(TemporaryBase.UserKey, "AdressSCDefault", ServiceAdressComboBox.SelectedIndex.ToString());
                }
                //  AdressSCDefault Адрес сервисного центра по умолчанию
                TemporaryBase.MasterDefault = MasterComboBox.Text;
                INIF.WriteINI(TemporaryBase.UserKey, "MasterDefault", MasterComboBox.Text);

                TemporaryBase.valuta = ValutaTextBox.Text;
                TemporaryBase.Mpersent = MpersentTbox.Text.Trim();

                //Штрихкод
                if (BarcodeHTextBox.Text != "" && BarcodeWTextBox.Text != "")
                {
                    INIF.WriteINI("ACTS", "BarcodeH", BarcodeHTextBox.Text);
                    INIF.WriteINI("ACTS", "BarcodeW", BarcodeWTextBox.Text);

                    TemporaryBase.barcodeH = int.Parse(BarcodeHTextBox.Text);
                    TemporaryBase.barcodeW = int.Parse(BarcodeWTextBox.Text);
                }

                mainForm.StatusStripLabel.Text = "Настройки программы сохранены";
                TemporaryBase.SearchFULLBegin();
                this.Close();
            }
        }

        private void ReAktor_Click(object sender, EventArgs e)
        {
            RedaktorAktov ReAkt1 = new RedaktorAktov(mainForm);
            ReAkt1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.Text += "Версия программы: " + TemporaryBase.ProgrammVersion;
            KodCountry.Text = TemporaryBase.smsPhone;
            KodCountryZamena.Text = TemporaryBase.smsPhonePref;
            if (TemporaryBase.baseR)
            {
                this.Height = 484;
            }
            else
            {
                this.Height = 582;
            }
            TokenTextBox.Text = TemporaryBase.smsToken;
            if (TemporaryBase.baseR) // Чтобы было доступно только для зарегистрированнных
                phoneIdTextBox.Text = TemporaryBase.smsPhoneId;
            BarcodeHTextBox.Text = TemporaryBase.barcodeH.ToString();
            BarcodeWTextBox.Text = TemporaryBase.barcodeW.ToString();
            //Обязательные поля
            if (TemporaryBase.Nessesary)
            {
                NessesaryCheckBox.Checked = true;
            }
            else
                NessesaryCheckBox.Checked = false;
            foreach (string strCombo in TemporaryBase.SortirovkaMasters)
            {
                MasterComboBox.Items.Add(strCombo);
            }

            foreach (string strCombo in TemporaryBase.SortirovkaAdressSc)
            {
                ServiceAdressComboBox.Items.Add(strCombo);
            }
            //Восстанавливаем значения Адреса СЦ 
            if (TemporaryBase.AdressSCDefault.ToString() != "")
            {
                if (ServiceAdressComboBox.Items.Count > decimal.Parse(TemporaryBase.AdressSCDefault.ToString()))
                {
                    ServiceAdressComboBox.SelectedIndex = int.Parse(TemporaryBase.AdressSCDefault.ToString());
                }

            }

            // Восстанавливливаем мастера по умолчанию
            {
                if (TemporaryBase.MasterDefault.ToString() != "")
                {
                    MasterComboBox.Text = TemporaryBase.MasterDefault;
                }
            }
            //Восстанавливаем гарантию по умолчанию
            if (INIF.KeyExists("CHECKBOX", "garantyDefault"))
            {
                if (INIF.ReadINI("CHECKBOX", "garantyDefault") == "Checked")
                {
                    GarantyDefaultCheckBox.Checked = true;
                }
            }
            // Ежедневное сохранение базы данных
            if (TemporaryBase.everyDayBackup != "Checked")
            {
                EveryDayBackupCheckBox.Checked = false;
            }
            else
            {
                EveryDayBackupCheckBox.Checked = true;
            }

            ToolTip t = new ToolTip();
            t.SetToolTip(GarantyDefaultCheckBox, "Если галочка не нажата, то по умолчанию гарантия будет ставиться 30 дней, если нажата, то без гарантии");
            t.SetToolTip(ShowBackupsButton, "Бэкапы сохраняются в момент закрытия программы");

            if (TemporaryBase.BlistColor != "")
                BlistButtonColor.BackColor = Color.FromArgb(int.Parse(TemporaryBase.BlistColor));
            else
                BlistButtonColor.BackColor = Color.Yellow;

            // Poloski
            if (TemporaryBase.Poloski)
            {
                PoloskiCheckBox.Checked = true;
            }
            else
                PoloskiCheckBox.Checked = false;
            // Автооткрытие папки клиента
            if (TemporaryBase.openClientFolder) openClientFilesCheckBox.Checked = true;
            // Добавляем данные о группах
            comboboxGroupsMaker(GroupDostupComboBox);
            comboboxGroupsMaker(UsersGroupDostupComboboxAdd);
            comboboxGroupsMaker(UsersComboBoxGroupEdit);
            comboboxUsersMaker(UserComboBox1);
            if (System.IO.Directory.Exists(TemporaryBase.pathtoSaveBD))
            {
                folderBrowserDialog1.SelectedPath = TemporaryBase.pathtoSaveBD;
            }
            MpersentTbox.Text = TemporaryBase.Mpersent;

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/clubremontuchet");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://vk.com/scrypto");
        }

        private void PasswordBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void PhoneExportButton_Click(object sender, EventArgs e)
        {
            Export ex1 = new Export(mainForm);
            ex1.Show(mainForm);
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Чтобы можно было снова открыть окошко
            mainForm.setBool = false;
        }

        private void FallDownListSetButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "settings");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://semysms.net");
        }


        async public Task<string> WebSend(string token, string device, string phone, string msg)
        {
            string url = String.Format("https://semysms.net/api/3/sms.php?token={0}&&device={1}&phone={2}&msg={3}", token, device, phone, msg);
            HttpWebRequest SemiSmsRequest = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse semiSmsResponse = (HttpWebResponse)await SemiSmsRequest.GetResponseAsync())
            using (Stream semiSmsStream = semiSmsResponse.GetResponseStream())
            using (StreamReader semiSmsStreamReader = new StreamReader(semiSmsStream))
            {
                return await semiSmsStreamReader.ReadToEndAsync();
            }
        }

        private void SmsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowBackupsButton_Click(object sender, EventArgs e)
        {
            Process PrFolder = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            string file = TemporaryBase.pathtoSaveBD;
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Normal;
            psi.FileName = "explorer";
            psi.Arguments = @file;
            PrFolder.StartInfo = psi;
            PrFolder.Start();
        }

        private void BlistButtonColor_Click(object sender, EventArgs e)
        {
            //Задаем цвет кнопки и строки
            if (colorDialogBlist.ShowDialog() == DialogResult.OK)
            {
                TemporaryBase.BlistColor = colorDialogBlist.Color.ToArgb().ToString();
                BlistButtonColor.BackColor = colorDialogBlist.Color;
                INIF.WriteINI(TemporaryBase.UserKey, "BlistColor", colorDialogBlist.Color.ToArgb().ToString());
            }
        }

        private void PoloskiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            INIF.WriteINI(TemporaryBase.UserKey, "Poloski", PoloskiCheckBox.CheckState.ToString());
            if (PoloskiCheckBox.Checked == false)
            {
                TemporaryBase.Poloski = false;
            }
            else
                TemporaryBase.Poloski = true;
        }

        private void BarcodeWTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;

                }

            }
        }

        private void BarcodeHTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;

                }

            }
        }

        private void StolbButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mainForm.MainListView.Columns.Count; i++)
            {
                if (mainForm.MainListView.Columns[i].Width < 3)
                    mainForm.MainListView.Columns[i].Width = 30;
            }
        }

        private void openClientFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            INIF.WriteINI(TemporaryBase.UserKey, "openClientFolder", openClientFilesCheckBox.CheckState.ToString());
            if (openClientFilesCheckBox.Checked == false)
            {
                TemporaryBase.openClientFolder = false;
            }
            else
                TemporaryBase.openClientFolder = true;
        }

        private void AddGroupDostupButton_Click(object sender, EventArgs e)
        {
            if (GroupDostupNameTextBox.Text != "")
            {
                if (MessageBox.Show("Добавить новую группу " + GroupDostupNameTextBox.Text + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    dostupRulesMaker();
                    mainForm.basa.GroupDostupBDWrite(GroupDostupNameTextBox.Text, delZapis, addZapis, saveZapis, graf, sms, stock, clients, stockAdd, stockDel, stockEdit, clientAdd, clientDel, clientConcat, settings, dates, editDates);
                    comboboxGroupsMaker(GroupDostupComboBox);
                    comboboxGroupsMaker(UsersGroupDostupComboboxAdd);
                    comboboxGroupsMaker(UsersComboBoxGroupEdit);
                    GroupDostupNameTextBox.Text = "";
                }
            }
            else
                MessageBox.Show("Введите название группы");
        }

        private void AllCheckBoxTrue()
        {
            delZapisCheckBox.Checked = true;
            addZapisCheckBox.Checked = true;
            saveZapisCheckBox.Checked = true;
            grafCheckBox.Checked = true;
            smsCheckBox.Checked = true;
            stockCheckBox.Checked = true;
            clientsCheckBox.Checked = true;
            stockAddCheckBox.Checked = true;
            stockDelCheckBox.Checked = true;
            stockEditCheckBox.Checked = true;
            clientAddCheckBox.Checked = true;
            clientDelCheckBox.Checked = true;
            clientConcatCheckBox.Checked = true;
            settingsCheckBox.Checked = true;
            datesCheckBox.Checked = true;
            editDatesCheckBox.Checked = true;
        }

        private void dostupRulesMaker()
        {
            delZapis = (delZapisCheckBox.Checked) ? "1" : "0";
            addZapis = (addZapisCheckBox.Checked) ? "1" : "0";
            saveZapis = (saveZapisCheckBox.Checked) ? "1" : "0";
            graf = (grafCheckBox.Checked) ? "1" : "0";
            sms = (smsCheckBox.Checked) ? "1" : "0";
            stock = (stockCheckBox.Checked) ? "1" : "0";
            clients = (clientsCheckBox.Checked) ? "1" : "0";
            stockAdd = (stockAddCheckBox.Checked) ? "1" : "0";
            stockDel = (stockDelCheckBox.Checked) ? "1" : "0";
            stockEdit = (stockEditCheckBox.Checked) ? "1" : "0";
            clientAdd = (clientAddCheckBox.Checked) ? "1" : "0";
            clientDel = (clientDelCheckBox.Checked) ? "1" : "0";
            clientConcat = (clientConcatCheckBox.Checked) ? "1" : "0";
            settings = (settingsCheckBox.Checked) ? "1" : "0";
            dates = (datesCheckBox.Checked) ? "1" : "0";
            editDates = (editDatesCheckBox.Checked) ? "1" : "0";
        }

        private void comboboxGroupsMaker(ComboBox cmbox)
        {
            cmbox.Items.Clear();
            AllCheckBoxTrue();
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

        private void GroupDostupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxGroupsMaker(GroupDostupComboBox.SelectedItem.ToString());
        }

        private void checkBoxGroupsMaker(string grName)
        {
            DataTable dTable = mainForm.basa.GroupDostupBdRead(grName);
            if (dTable.Rows.Count > 0)
            {
                int i = 0;
                delZapisCheckBox.Checked = (dTable.Rows[i].ItemArray[2].ToString() == "1") ? true : false;
                addZapisCheckBox.Checked = (dTable.Rows[i].ItemArray[3].ToString() == "1") ? true : false;
                saveZapisCheckBox.Checked = (dTable.Rows[i].ItemArray[4].ToString() == "1") ? true : false;
                grafCheckBox.Checked = (dTable.Rows[i].ItemArray[5].ToString() == "1") ? true : false;
                smsCheckBox.Checked = (dTable.Rows[i].ItemArray[6].ToString() == "1") ? true : false;
                stockCheckBox.Checked = (dTable.Rows[i].ItemArray[7].ToString() == "1") ? true : false;
                clientsCheckBox.Checked = (dTable.Rows[i].ItemArray[8].ToString() == "1") ? true : false;
                stockAddCheckBox.Checked = (dTable.Rows[i].ItemArray[9].ToString() == "1") ? true : false;
                stockDelCheckBox.Checked = (dTable.Rows[i].ItemArray[10].ToString() == "1") ? true : false;
                stockEditCheckBox.Checked = (dTable.Rows[i].ItemArray[11].ToString() == "1") ? true : false;
                clientAddCheckBox.Checked = (dTable.Rows[i].ItemArray[12].ToString() == "1") ? true : false;
                clientDelCheckBox.Checked = (dTable.Rows[i].ItemArray[13].ToString() == "1") ? true : false;
                clientConcatCheckBox.Checked = (dTable.Rows[i].ItemArray[14].ToString() == "1") ? true : false;
                settingsCheckBox.Checked = (dTable.Rows[i].ItemArray[15].ToString() == "1") ? true : false;
                datesCheckBox.Checked = (dTable.Rows[i].ItemArray[16].ToString() == "1") ? true : false;
                editDatesCheckBox.Checked = (dTable.Rows[i].ItemArray[17].ToString() == "1") ? true : false;

            }
        }

        private void SaveGroupDostupButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Применить права доступа к " + GroupDostupComboBox.SelectedItem.ToString() + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dostupRulesMaker();
                mainForm.basa.GroupDostupBdEditAll(GroupDostupComboBox.SelectedItem.ToString(), delZapis, addZapis, saveZapis, graf, sms, stock, clients, stockAdd, stockDel, stockEdit, clientAdd, clientDel, clientConcat, settings, dates, editDates);
            }
        }

        private void DeleteGroupButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить группу  " + GroupDostupComboBox.SelectedItem.ToString() + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.GroupDostupDelete(GroupDostupComboBox.SelectedItem.ToString());
                comboboxGroupsMaker(GroupDostupComboBox);
                comboboxGroupsMaker(UsersGroupDostupComboboxAdd);
                comboboxGroupsMaker(UsersComboBoxGroupEdit);
            }
        }

        private void UserDostupAddButton_Click(object sender, EventArgs e)
        {
            if (UserNameTextBox.Text != "" && UserPasswordTextBox.Text != "" && UsersGroupDostupComboboxAdd.Text != "")
            {
                if (MessageBox.Show("Добавить пользователя  " + UserNameTextBox.Text + " в группу " + UsersGroupDostupComboboxAdd.Text + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.UsersBDWrite(UsersGroupDostupComboboxAdd.Text, UserNameTextBox.Text, mainForm.basa.GroupDostupGetIdByGrNameBdRead(UsersGroupDostupComboboxAdd.Text), Registration.sha1(UserPasswordTextBox.Text));
                    comboboxUsersMaker(UserComboBox1);
                }
            }
            else
                MessageBox.Show("Одно из полей: ИМЯ, ПАРОЛЬ или ГРУППА ДОСТУПА пустое", "Заполните все поля");

        }

        private void UserDeleteButton_Click(object sender, EventArgs e)
        {
            UsersComboBoxGroupEdit.Text = "";
            if (UserComboBox1.Text != "")
            {
                if (MessageBox.Show("Удалить пользователя: " + UserComboBox1.Text + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.UserDelete(UserComboBox1.Text);
                    comboboxUsersMaker(UserComboBox1);
                    comboboxGroupsMaker(UsersComboBoxGroupEdit);
                }
            }
            else MessageBox.Show("Выберите пользователя, для удаления");
        }
        private void UserChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (UserComboBox1.Text != "" && UserChangePasswordTextBox.Text != "")
            {
                if (MessageBox.Show("Сменить пароль пользователю: " + UserComboBox1.Text + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.UsersBdEditPassword("user_pwd", Registration.sha1(UserChangePasswordTextBox.Text), UserComboBox1.Text);
                }
            }
            else MessageBox.Show("Заполните поля новый пароль и выберите пользователя");
        }

        private void ReselectUserRulesButton_Click(object sender, EventArgs e)
        {
            if (UserComboBox1.Text != "" && UsersComboBoxGroupEdit.Text != "")
            {
                if (MessageBox.Show("Сменить права пользователя : " + UserComboBox1.Text + " на " + UsersComboBoxGroupEdit.Text + "?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.UserBdEditAll(UsersComboBoxGroupEdit.Text, UserComboBox1.Text, mainForm.basa.GroupDostupGetIdByGrNameBdRead(UsersComboBoxGroupEdit.Text));
                }
            }
            else MessageBox.Show("Заполните поля имени пользователя и прав доступа");

        }

        private void UserComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dTable = mainForm.basa.UsersBdRead(UserComboBox1.Text);
            if (dTable.Rows.Count > 0)
            {
                UsersComboBoxGroupEdit.Text = dTable.Rows[0].ItemArray[1].ToString();
            }
        }

        private void ShowHistoryButton_Click(object sender, EventArgs e)
        {
            HistoryViewer hv1 = new HistoryViewer(mainForm);
            hv1.Show(mainForm);
        }

        private void pathbutton1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TemporaryBase.pathtoSaveBD = folderBrowserDialog1.SelectedPath;
                INIF.WriteINI(TemporaryBase.UserKey, "backupPath", TemporaryBase.pathtoSaveBD);
                pathbutton1.Text = TemporaryBase.pathtoSaveBD;
            }
        }

        private void TokenTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ColorDiagnosticCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void phoneIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!TemporaryBase.baseR)
            {
                phoneIdTextBox.Text = "";
                MessageBox.Show("СМС доступны только в полной версии");
            }
        }

        private void NessesaryCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;

                }

            }
        }

        private void GarantyDefaultCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}