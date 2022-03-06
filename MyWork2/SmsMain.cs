using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class SmsMain : Form
    {
        IniFile INIF = new IniFile("Config.ini");
        Form1 mainForm;
        public SmsMain(Form1 mf)
        {
            InitializeComponent();
            mainForm = mf;
        }

        private void SmsMain_Load(object sender, EventArgs e)
        {
            SmsReadyTextBox.Text = TemporaryBase.smsTextGotov;
            SmsSoglasovanTextBox.Text = TemporaryBase.smsTextSoglasovat;
            SmsShablonTextBox.Text = TemporaryBase.smsTextShablon;
            SmsPrivetTextBox.Text = TemporaryBase.smsTextPrivet;
        }

        private void SmsReadyTextBox_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.smsTextGotov = SmsReadyTextBox.Text;
        }

        private void SmsSoglasovanTextBox_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.smsTextSoglasovat = SmsSoglasovanTextBox.Text;
        }

        private void SmsShablonTextBox_TextChanged(object sender, EventArgs e)
        {
            TemporaryBase.smsTextShablon = SmsShablonTextBox.Text;
        }

        private void SmsMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            TemporaryBase.smsTextGotov = SmsReadyTextBox.Text;
            INIF.WriteINI("SMSSEND", "ReadyText", SmsReadyTextBox.Text);
            TemporaryBase.smsTextSoglasovat = SmsSoglasovanTextBox.Text;
            INIF.WriteINI("SMSSEND", "SoglasovanText", SmsSoglasovanTextBox.Text);
            TemporaryBase.smsTextShablon = SmsShablonTextBox.Text;
            INIF.WriteINI("SMSSEND", "ShablonText", SmsShablonTextBox.Text);
            TemporaryBase.smsTextPrivet = SmsPrivetTextBox.Text;
            INIF.WriteINI("SMSSEND", "PrivetText", SmsPrivetTextBox.Text);

        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"В тексте любого сообщения можно писать любой текст, программа автоматически заполнит ключевые слова на данные из базы. {Environment.NewLine}ОБЯЗАТЕЛЬНО ПИСАТЬ СЛОВА так же как и в шаблоне, с кавычками и с заглавной буквы" +
                $"{Environment.NewLine}" +
                 "{ФИО} - ФИО КЛИЕНТА" + Environment.NewLine +
                 "{ТЕЛЕФОН} - ТЕЛЕФОН КЛИЕНТА" + Environment.NewLine +
                 "{СТАТУС} - СТАТУС ЗАКАЗА КЛИЕНТА" + Environment.NewLine +
                 "{ТИП} - ТИП УСТРОЙСТВА КЛИЕНТА" + Environment.NewLine +
                 "{БРЕНД} - БРЕНД УСТРОЙТСВА КЛИЕНТА" + Environment.NewLine +
                 "{МОДЕЛЬ} - МОДЕЛЬ УСТРОЙТСВА КЛИЕНТА" + Environment.NewLine +
                 "{ЦЕНА} - ЦЕНА РЕМОНТА" + Environment.NewLine +
                 "{ПРЕДОПЛАТА} - ПРЕДОПЛАТА, ЕСЛИ КЛИЕНТ ВНОСИЛ" + Environment.NewLine +
                 "{ПРЕДСТОИМОСТЬ} - ПРЕДВАРИТЕЛЬНАЯ СТОИМОСТЬ РЕМОНТА" + Environment.NewLine +
                 "{ЦЕНАБЕЗПРЕДОПЛАТЫ} - ЦЕНА, ЗА ВЫЧЕТОМ УЖЕ ОПЛАЧЕННОГО" + Environment.NewLine +
                 "{НОМЕР} - НОМЕР ЗАКАЗА" + Environment.NewLine +
                 "{СКИДКА} - СКИДКА" + Environment.NewLine);
        }

        private void SmsMain_HelpButtonClicked(object sender, CancelEventArgs e)
        {

        }

        private void SmsRassilkaButton_Click(object sender, EventArgs e)
        {
            SmsRassilka smrs = new SmsRassilka(mainForm);
            smrs.Show(this);
        }
    }
}
