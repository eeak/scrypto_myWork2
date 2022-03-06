using System;
using System.Data;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Authorisation : Form
    {
        IniFile INIF = new IniFile("Config.ini");
        Form1 mainForm;
        public Authorisation(Form1 fm)
        {
            InitializeComponent();
            mainForm = fm;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PasswordBox.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            PasswordBox.UseSystemPasswordChar = true;
        }

        private void Authorisation_Load(object sender, EventArgs e)
        {
            comboboxUsersMaker(LoginComboBox);
            PasswordBox.UseSystemPasswordChar = true;
            if (INIF.KeyExists(TemporaryBase.UserKey, "LastUser"))
            {
                for (int i = 0; i < LoginComboBox.Items.Count; i++)
                {
                    string loginName = LoginComboBox.Items[i].ToString();
                    if (loginName == INIF.ReadINI(TemporaryBase.UserKey, "LastUser"))
                        LoginComboBox.SelectedIndex = i;
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

        private void EnterButton_Click(object sender, EventArgs e)
        {
            VhodVMy();

        }

        private void VhodVMy()
        {
            if (LoginComboBox.Text != "")
            {
                if (Registration.sha1(PasswordBox.Text) == mainForm.basa.UsersGetPass(LoginComboBox.Text))
                {
                    RulesMaker(mainForm.basa.GroupDostupGetgrNameByIdBdRead(mainForm.basa.UsersGetGroupIdByUserName(LoginComboBox.Text)));
                    mainForm.RulesMackerMainWindow();
                    mainForm.Enabled = true;
                    TemporaryBase.USER_SESSION = LoginComboBox.Text + " " + mainForm.basa.GroupDostupGetgrNameByIdBdRead(mainForm.basa.UsersGetGroupIdByUserName(LoginComboBox.Text));
                    INIF.WriteINI(TemporaryBase.UserKey, "LastUser", LoginComboBox.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Неверный логин-пароль");
            }
            else
                MessageBox.Show("Выберите пользователя");
        }

        private void Authorisation_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Authorisation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm.Enabled == false)
                Application.Exit();
        }
        private void RulesMaker(string grName)
        {
            DataTable dTable = mainForm.basa.GroupDostupBdRead(grName);
            if (dTable.Rows.Count > 0)
            {
                int i = 0;
                TemporaryBase.delZapis = (dTable.Rows[i].ItemArray[2].ToString() == "1") ? "1" : "0";
                TemporaryBase.addZapis = (dTable.Rows[i].ItemArray[3].ToString() == "1") ? "1" : "0";
                TemporaryBase.saveZapis = (dTable.Rows[i].ItemArray[4].ToString() == "1") ? "1" : "0";
                TemporaryBase.graf = (dTable.Rows[i].ItemArray[5].ToString() == "1") ? "1" : "0";
                TemporaryBase.sms = (dTable.Rows[i].ItemArray[6].ToString() == "1") ? "1" : "0";
                TemporaryBase.stock = (dTable.Rows[i].ItemArray[7].ToString() == "1") ? "1" : "0";
                TemporaryBase.clients = (dTable.Rows[i].ItemArray[8].ToString() == "1") ? "1" : "0";
                TemporaryBase.stockAdd = (dTable.Rows[i].ItemArray[9].ToString() == "1") ? "1" : "0";
                TemporaryBase.stockDel = (dTable.Rows[i].ItemArray[10].ToString() == "1") ? "1" : "0";
                TemporaryBase.stockEdit = (dTable.Rows[i].ItemArray[11].ToString() == "1") ? "1" : "0";
                TemporaryBase.clientAdd = (dTable.Rows[i].ItemArray[12].ToString() == "1") ? "1" : "0";
                TemporaryBase.clientDel = (dTable.Rows[i].ItemArray[13].ToString() == "1") ? "1" : "0";
                TemporaryBase.clientConcat = (dTable.Rows[i].ItemArray[14].ToString() == "1") ? "1" : "0";
                TemporaryBase.settings = (dTable.Rows[i].ItemArray[15].ToString() == "1") ? "1" : "0";
                TemporaryBase.dates = (dTable.Rows[i].ItemArray[16].ToString() == "1") ? "1" : "0";
                TemporaryBase.editDates = (dTable.Rows[i].ItemArray[17].ToString() == "1") ? "1" : "0";

            }
        }

        private void LoginComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "Авторизация " + mainForm.basa.GroupDostupGetgrNameByIdBdRead(mainForm.basa.UsersGetGroupIdByUserName(LoginComboBox.Text));
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                VhodVMy();
        }
    }
}
