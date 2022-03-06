using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class ClientAddForm : Form
    {
        Form1 mainForm;
        ClientsEditor clientsForm;
        public ClientAddForm(Form1 mf, ClientsEditor ce)
        {
            InitializeComponent();
            mainForm = mf;
            clientsForm = ce;
        }

        private void AddClientButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Добавить клиента " + ClientFioTextBox.Text + " ?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.ClientsMapWrite(ClientFioTextBox.Text, ClientPhoneTextBox.Text.Replace(" ", ""), ClientAdressTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ClientAboutUsComboBox.Text);
                AllItemsClear();
                clientsForm.SearchClient();
            }
            //   mainForm.basa.ClientsMapWrite(ClientFioTextBox.Text,ClientPhoneTextBox.Text.Replace(" ",""),ClientAdressTextBox.Text, PrimechanieTextBox.Text, BlistOfClients(), DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ClientAboutUsComboBox.Text);
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

        // Очитска полей
        private void AllItemsClear()
        {
            ClientFioTextBox.Text = "";
            ClientPhoneTextBox.Text = "";
            ClientAdressTextBox.Text = "";
            PrimechanieTextBox.Text = "";
            BlackListComboBox.Text = "Не проблемный";
            ClientAboutUsComboBox.Text = "";
        }

        private void label30_MouseLeave(object sender, EventArgs e)
        {
            label30.BorderStyle = BorderStyle.Fixed3D;
        }

        private void label30_Click(object sender, EventArgs e)
        {
            AllItemsClear();
        }

        private void label30_MouseEnter(object sender, EventArgs e)
        {
            label30.BorderStyle = BorderStyle.FixedSingle;
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

        private void ClientAddForm_Load(object sender, EventArgs e)
        {
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaAboutUs)
            {
                ClientAboutUsComboBox.Items.Add(ssp.SortObj);
            }
        }

    }
}
