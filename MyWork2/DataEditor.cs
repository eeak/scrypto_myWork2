using System;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class DataEditor : Form
    {
        Form1 mainForm;
        Editor ed1;
        string id_bd;
        public DataEditor(Form1 mf, string id_bd, Editor ed)
        {
            mainForm = mf;
            this.id_bd = id_bd;
            ed1 = ed;
            InitializeComponent();
        }

        private void SaveDataPriemaButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить дату приёма?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdEditOne("Data_priema", DataPriemaCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm"), id_bd);
                DataPriemaLabel.Text = DataPriemaCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                mainForm.StatusStripLabel.Text = "Дата приема записи номер " + id_bd + " изменена на " + DataPriemaCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                TemporaryBase.SearchFULLBegin();
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Изменение даты приёма", "", id_bd);
            }
        }
        private void SaveDataVidButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить дату выдачи?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdEditOne("Data_vidachi", DataVidachiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm"), id_bd);
                DataVidachiLabel.Text = DataVidachiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                mainForm.StatusStripLabel.Text = "Дата выдачи записи номер " + id_bd + " изменена на " + DataVidachiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                //   mainForm.MainListViewUpdate(id_bd
                TemporaryBase.SearchFULLBegin();
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Изменение даты выдачи", "", id_bd);
            }
        }
        private void DataVidDeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить дату выдачи?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdEditOne("Data_vidachi", "", id_bd);
                DataVidachiLabel.Text = "Не указана";
                mainForm.StatusStripLabel.Text = "Дата выдачи записи номер " + id_bd + " удалена";
                //mainForm.MainListViewUpdate(id_bd);
                TemporaryBase.SearchFULLBegin();
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Удаление даты выдачи", "", id_bd);
            }
        }


        private void SaveDataPredButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить дату предоплаты?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdEditOne("Data_predoplaty", DataPredoplatiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm"), id_bd);
                DataPredoplatiLabel.Text = DataPredoplatiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                mainForm.StatusStripLabel.Text = "Дата предоплаты записи номер " + id_bd + " изменена на " + DataPredoplatiCalendar.SelectionStart.ToString("dd-MM-yyyy HH:mm");
                // mainForm.MainListViewUpdate(id_bd);
                TemporaryBase.SearchFULLBegin();
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Сохранение даты предоплаты", "", id_bd);
            }
        }

        private void DeleteDataPredButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить дату предоплаты?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdEditOne("Data_predoplaty", "", id_bd);
                DataPredoplatiLabel.Text = "Не указана";
                mainForm.StatusStripLabel.Text = "Дата предоплаты записи номер " + id_bd + " удалена";
                // mainForm.MainListViewUpdate(id_bd);
                TemporaryBase.SearchFULLBegin();
                mainForm.basa.HistoryBDWrite(TemporaryBase.USER_SESSION, "Удаление даты предоплаты", "", id_bd);
            }
        }
        private void DataEditor_MouseDown(object sender, MouseEventArgs e)
        {
            //Для перетаскивания за форму
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataEditor_Load(object sender, EventArgs e)
        {
            SaveDataPriemaButton.Enabled = (TemporaryBase.editDates == "1") ? true : false;
            SaveDataPredButton.Enabled = (TemporaryBase.editDates == "1") ? true : false;
            DeleteDataPredButton.Enabled = (TemporaryBase.editDates == "1") ? true : false;
            SaveDataVidButton.Enabled = (TemporaryBase.editDates == "1") ? true : false;
            DataVidDeleteButton.Enabled = (TemporaryBase.editDates == "1") ? true : false;
            DataPriemaLabel.Text = labelDataWorker(mainForm.basa.BdReadOne("Data_priema", id_bd));
            DataPredoplatiLabel.Text = labelDataWorker(mainForm.basa.BdReadOne("Data_predoplaty", id_bd));
            DataVidachiLabel.Text = labelDataWorker(mainForm.basa.BdReadOne("Data_vidachi", id_bd));

            // Подгружаем даты в календари
            DateTime dtPriema = DateTime.Parse(calendarDateWorker(mainForm.basa.BdReadOne("Data_priema", id_bd)));
            DateTime dtPredoplati = DateTime.Parse(calendarDateWorker(mainForm.basa.BdReadOne("Data_predoplaty", id_bd)));
            DateTime dtVidachi = DateTime.Parse(calendarDateWorker(mainForm.basa.BdReadOne("Data_vidachi", id_bd)));
            DataPriemaCalendar.SelectionStart = dtPriema;
            DataPriemaCalendar.SelectionEnd = dtPriema;

            DataPredoplatiCalendar.SelectionStart = dtPredoplati;
            DataPredoplatiCalendar.SelectionEnd = dtPredoplati;

            DataVidachiCalendar.SelectionStart = dtVidachi;
            DataVidachiCalendar.SelectionEnd = dtVidachi;
        }

        private string labelDataWorker(string date)
        {
            if (date != "")
            {
                return date;
            }
            else
                return "Не указана";
        }
        private string calendarDateWorker(string date)
        {
            if (date != "")
            {
                return date;
            }
            else
                return DateTime.Now.ToString();
        }

        private void DataEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ed1.Enabled = true;
        }
    }
}
