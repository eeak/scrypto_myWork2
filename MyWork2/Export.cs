using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Export : Form
    {
        //Ини файл
        IniFile INIF = new IniFile("Config.ini");
        //Главная форма
        Form1 MainForm;
        public Export(Form1 fm1)
        {
            InitializeComponent();
            MainForm = fm1;
        }

        private void Export_Load(object sender, EventArgs e)
        {
            if (INIF.KeyExists("EXPORT", "to"))
            {
                label1.Text = string.Format("В прошлый раз, Вы экспортировали номера с {0} по {1} ", INIF.ReadINI("EXPORT", "from"), INIF.ReadINI("EXPORT", "to"));
                if (decimal.Parse(INIF.ReadINI("EXPORT", "to")) < MainForm.basa.BdReadAdvertsDataTop())
                {
                    // Чтобы в при следующем экспорте диапазон номеров вставлялся автоматически.
                    fromNumericUpDown.Value = decimal.Parse(INIF.ReadINI("EXPORT", "to")) + 1;
                    toNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataTop();
                    label1.Text += Environment.NewLine + "Дипазон номеров сформирован для нового экспорта";
                }
                else
                {
                    toNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataTop();
                    fromNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataFirt();
                    {
                        label1.Text += Environment.NewLine + "Новых номеров не появилось";
                    }
                }

            }
            else
            {
                toNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataTop();
                fromNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataFirt();
            }

        }

        private void ExportToAndroidButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будет создан файл Contacts.csv, содержащий все ваши контакты, он находится в папке с программой", "Экспорт контактов", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Переменные с данными о первой и последней записи в бд
                int FirstKlientID = MainForm.basa.BdReadAdvertsDataFirt();
                int KrajnijKlientID = MainForm.basa.BdReadAdvertsDataTop();
                List<VirtualClient> vcPriveous = new List<VirtualClient>();
                //Проверяем, есть ли в бд записи, помимо выбранных пользователем
                if (FirstKlientID < fromNumericUpDown.Value)
                {
                    // Заносим данные о предыдущих записях в список, чтобы потом сравнить с новыми и убрать повторы
                    vcPriveous = MainForm.basa.ExportPhonesVCList(FirstKlientID.ToString(), (fromNumericUpDown.Value - 1).ToString());

                    string fileName = string.Format("google_{0}.csv", DateTime.Now.ToString("dd_MM_yyyy_hh_mm"));
                    List<VirtualClient> vc1 = new List<VirtualClient>();
                    List<PhoneExport> pe1 = new List<PhoneExport>();
                    // Мудотня для фильтрации повторов телефонов
                    vc1 = MainForm.basa.ExportPhonesVCList(fromNumericUpDown.Value.ToString(), toNumericUpDown.Value.ToString());
                    for (int i = 0; i < vc1.Count; i++)
                    {
                        // Если запись является телефоном
                        if (PhoneValidator(vc1[i].Phone))
                        {
                            bool valPhoneOneMoreTime = true;
                            // Если ни в одной из итераций нет совпадений искомого номера телефона, с предыдущими
                            for (int j = 0; j < vcPriveous.Count; j++)
                            {
                                if (vc1[i].Phone == vcPriveous[j].Phone)
                                {
                                    // Если есть совпадения, то не записываем клиента
                                    valPhoneOneMoreTime = false;
                                    break;
                                }
                            }
                            //Если все круги ада пройдены, и совпадений нет, то пишем клиента в список.
                            if (valPhoneOneMoreTime) pe1.Add(new PhoneExport(vc1[i].Phone, vc1[i].Surname));

                        }

                    }
                    List<PhoneExport> filteredPhoneList = pe1.Distinct().ToList();
                    string csv = "Name,Given Name,Additional Name,Family Name,Yomi Name,Given Name Yomi,Additional Name Yomi,Family Name Yomi,Name Prefix,Name Suffix,Initials,Nickname,Short Name,Maiden Name,Birthday,Gender,Location,Billing Information,Directory Server,Mileage,Occupation,Hobby,Sensitivity,Priority,Subject,Notes,Group Membership,Phone 1 - Type,Phone 1 - Value,Phone 2 - Type,Phone 2 - Value";
                    for (int i = 0; i < filteredPhoneList.Count; i++)
                    {
                        csv += string.Format("{0},{0},,,,,,,,,,,,,,,,,,,,,,,,,* {1},{2},{3},,", FirstLetterToUpper(filteredPhoneList[i].FIO.Replace(",", "")), "My Contacts", "Mobile", filteredPhoneList[i].Phone);
                        csv += Environment.NewLine;
                    }

                    File.WriteAllText(fileName, csv);
                    if (filteredPhoneList.Count == 0)
                    {
                        MessageBox.Show("Новых клиентов не обнаружено");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} новых клиентов добавлено в файл {1}", filteredPhoneList.Count.ToString(), fileName));
                    }
                    //Записываем данные об экспорте, для последующей автоподгрузки
                    INIF.WriteINI("EXPORT", "from", fromNumericUpDown.Value.ToString());
                    INIF.WriteINI("EXPORT", "to", toNumericUpDown.Value.ToString());
                    //Открыть файл в папке
                    Process PrFolder = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    string file = fileName;
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    psi.FileName = "explorer";
                    psi.Arguments = @"/n, /select, " + file;
                    PrFolder.StartInfo = psi;
                    PrFolder.Start();
                }
                else
                {
                    string fileName = string.Format("google_{0}.csv", DateTime.Now.ToString("dd_MM_yyyy_hh_mm"));
                    List<VirtualClient> vc1 = new List<VirtualClient>();
                    List<PhoneExport> pe1 = new List<PhoneExport>();
                    // Мудотня для фильтрации повторов телефонов
                    vc1 = MainForm.basa.ExportPhonesVCList(fromNumericUpDown.Value.ToString(), toNumericUpDown.Value.ToString());
                    for (int i = 0; i < vc1.Count; i++)
                    {
                        if (PhoneValidator(vc1[i].Phone))
                            pe1.Add(new PhoneExport(vc1[i].Phone, vc1[i].Surname));
                    }
                    List<PhoneExport> filteredPhoneList = pe1.Distinct().ToList();
                    string csv = "Name,Given Name,Additional Name,Family Name,Yomi Name,Given Name Yomi,Additional Name Yomi,Family Name Yomi,Name Prefix,Name Suffix,Initials,Nickname,Short Name,Maiden Name,Birthday,Gender,Location,Billing Information,Directory Server,Mileage,Occupation,Hobby,Sensitivity,Priority,Subject,Notes,Group Membership,Phone 1 - Type,Phone 1 - Value,Phone 2 - Type,Phone 2 - Value";
                    for (int i = 0; i < filteredPhoneList.Count; i++)
                    {
                        csv += string.Format("{0},{0},,,,,,,,,,,,,,,,,,,,,,,,,* {1},{2},{3},,", FirstLetterToUpper(filteredPhoneList[i].FIO.Replace(",", "")), "My Contacts", "Mobile", filteredPhoneList[i].Phone);
                        csv += Environment.NewLine;
                    }
                    File.WriteAllText(fileName, csv);
                    if (filteredPhoneList.Count == 0)
                    {
                        MessageBox.Show("Новых клиентов не обнаружено");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} новых клиентов добавлено в файл {1}", filteredPhoneList.Count.ToString(), fileName));
                    }
                    //Записываем данные об экспорте, для последующей автоподгрузки
                    INIF.WriteINI("EXPORT", "from", fromNumericUpDown.Value.ToString());
                    INIF.WriteINI("EXPORT", "to", toNumericUpDown.Value.ToString());
                    //Открыть файл в папке
                    Process PrFolder = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    string file = fileName;
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    psi.FileName = "explorer";
                    psi.Arguments = @"/n, /select, " + file;
                    PrFolder.StartInfo = psi;
                    PrFolder.Start();
                }

            }
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
        private void ExportToAppleButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будет создан файл Vcard.vcf, содержащий все ваши контакты, он находится в папке с программой", "Экспорт контактов", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                // Переменные с данными о первой и последней записи в бд
                int FirstKlientID = MainForm.basa.BdReadAdvertsDataFirt();
                int KrajnijKlientID = MainForm.basa.BdReadAdvertsDataTop();
                List<VirtualClient> vcPriveous = new List<VirtualClient>();
                //Проверяем, есть ли в бд записи, помимо выбранных пользователем
                if (FirstKlientID < fromNumericUpDown.Value)
                {
                    // Заносим данные о предыдущих записях в список, чтобы потом сравнить с новыми и убрать повторы
                    vcPriveous = MainForm.basa.ExportPhonesVCList(FirstKlientID.ToString(), (fromNumericUpDown.Value - 1).ToString());

                    string fileName = string.Format("google_{0}.vcf", DateTime.Now.ToString("dd_MM_yyyy_hh_mm"));
                    List<VirtualClient> vc1 = new List<VirtualClient>();
                    List<PhoneExport> pe1 = new List<PhoneExport>();
                    // Мудотня для фильтрации повторов телефонов
                    vc1 = MainForm.basa.ExportPhonesVCList(fromNumericUpDown.Value.ToString(), toNumericUpDown.Value.ToString());
                    for (int i = 0; i < vc1.Count; i++)
                    {
                        // Если запись является телефоном
                        if (PhoneValidator(vc1[i].Phone))
                        {
                            bool valPhoneOneMoreTime = true;
                            for (int j = 0; j < vcPriveous.Count; j++)
                            {
                                if (vc1[i].Phone == vcPriveous[j].Phone)
                                {
                                    valPhoneOneMoreTime = false;
                                    break;
                                }
                            }
                            //Если все круги ада пройдены, и совпадений нет, то пишем клиента в список.
                            if (valPhoneOneMoreTime) pe1.Add(new PhoneExport(vc1[i].Phone, vc1[i].Surname));

                        }

                    }
                    List<PhoneExport> filteredPhoneList = pe1.Distinct().ToList();
                    string vcf = "";
                    for (int i = 0; i < filteredPhoneList.Count; i++)
                    {
                        vcf += string.Format("{1}{4}{2}{4}FN:{0}{4}N:;{0};;;{4}TEL;TYPE=CELL:{3}{4}{5}", FirstLetterToUpper(filteredPhoneList[i].FIO.Replace(",", "")), "BEGIN:VCARD", "VERSION:3.0", filteredPhoneList[i].Phone, Environment.NewLine, "END:VCARD");
                        vcf += Environment.NewLine;
                    }
                    File.WriteAllText(@fileName, vcf);
                    if (filteredPhoneList.Count == 0)
                    {
                        MessageBox.Show("Новых клиентов не обнаружено");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} новых клиентов добавлено в файл {1}", filteredPhoneList.Count.ToString(), fileName));
                    }
                    //Записываем данные об экспорте, для последующей автоподгрузки
                    INIF.WriteINI("EXPORT", "from", fromNumericUpDown.Value.ToString());
                    INIF.WriteINI("EXPORT", "to", toNumericUpDown.Value.ToString());
                    //Открыть файл в папке
                    Process PrFolder = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    string file = fileName;
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    psi.FileName = "explorer";
                    psi.Arguments = @"/n, /select, " + file;
                    PrFolder.StartInfo = psi;
                    PrFolder.Start();
                }
                else
                {
                    string fileName = string.Format("Vcard_{0}.vcf", DateTime.Now.ToString("dd_MM_yyyy_hh_mm"));
                    List<VirtualClient> vc1 = new List<VirtualClient>();
                    List<PhoneExport> pe1 = new List<PhoneExport>();
                    // Мудотня для фильтрации повторов телефонов
                    vc1 = MainForm.basa.ExportPhonesVCList(fromNumericUpDown.Value.ToString(), toNumericUpDown.Value.ToString());
                    for (int i = 0; i < vc1.Count; i++)
                    {
                        if (PhoneValidator(vc1[i].Phone))
                            pe1.Add(new PhoneExport(vc1[i].Phone, vc1[i].Surname));
                    }
                    List<PhoneExport> filteredPhoneList = pe1.Distinct().ToList();
                    string vcf = "";
                    for (int i = 0; i < filteredPhoneList.Count; i++)
                    {
                        vcf += string.Format("{1}{4}{2}{4}FN:{0}{4}N:;{0};;;{4}TEL;TYPE=CELL:{3}{4}{5}", FirstLetterToUpper(filteredPhoneList[i].FIO.Replace(",", "")), "BEGIN:VCARD", "VERSION:3.0", filteredPhoneList[i].Phone, Environment.NewLine, "END:VCARD");
                        vcf += Environment.NewLine;
                    }
                    File.WriteAllText(@fileName, vcf);
                    if (filteredPhoneList.Count == 0)
                    {
                        MessageBox.Show("Новых клиентов не обнаружено");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} новых клиентов добавлено в файл {1}", filteredPhoneList.Count.ToString(), fileName));
                    }
                    //Записываем данные об экспорте, для последующей автоподгрузки
                    INIF.WriteINI("EXPORT", "from", fromNumericUpDown.Value.ToString());
                    INIF.WriteINI("EXPORT", "to", toNumericUpDown.Value.ToString());
                    //Открыть файл в папке
                    Process PrFolder = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    string file = fileName;
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    psi.FileName = "explorer";
                    psi.Arguments = @"/n, /select, " + file;
                    PrFolder.StartInfo = psi;
                    PrFolder.Start();
                }
            }

        }
        private bool PhoneValidator(string phone)
        {
            if (phone.Length == 11)
                return true;
            else return false;
        }
        private void AllExportNumbersButton_Click(object sender, EventArgs e)
        {
            toNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataTop();
            fromNumericUpDown.Value = MainForm.basa.BdReadAdvertsDataFirt();
        }
    }
}
