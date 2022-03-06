using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class StockEditor : Form
    {
        Form1 mainForm;
        string idOfZip = "0";
        // Пути к фото
        string photoPath = "";
        string photoPath2 = "";
        string photoPath3 = "";
        int photoShow = 0;
        int maxPhoto = 0;
        string idInMainBd = "-1";
        Stock stockForm;
        ZIP zip;
        Editor editorForm = null;
        string clientId = "";
        public StockEditor(Form1 mf, string idOfZip, Stock st1, string idInMainBd = "-1", Editor ed1 = null, string clientId = "")
        {
            InitializeComponent();
            mainForm = mf;
            this.idOfZip = idOfZip;
            stockForm = st1;
            this.idInMainBd = idInMainBd;
            editorForm = ed1;
            this.clientId = clientId;
        }

        private void StockEditor_Load(object sender, EventArgs e)
        {
            SaveStockButton.Enabled = (TemporaryBase.stockEdit == "1") ? true : false;
            DeleteStockButton1.Enabled = (TemporaryBase.stockDel == "1") ? true : false;
            if (idInMainBd != "-1" && clientId != "")
            {
                FIOLabel.Text = "ФИО клента: " + mainForm.basa.ClientsReadOne("FIO", clientId);
                //добавить функцию для проверки, использовалась ли запчасть клиентом
                if (!mainForm.basa.BdStockMapZIPUsedCheck(idInMainBd, idOfZip))
                {
                    CancelZIPButton.Enabled = false;
                }
                // Обновляем надпись с кол-вом использующихся запчастей
                UsedZIPCounterLabelUpdate();
            }

            else
                CancelZIPButton.Enabled = false;
            StockEdShower();
            stockForm.Enabled = false;
            this.Text = "Склад: Редактирование запчисти Арт. " + idOfZip;
        }

        //Поиск по складу
        private void StockEdShower()
        {
            try
            {

                DataTable dtStock1;
                dtStock1 = mainForm.basa.BdStockEditor(idOfZip);
                int i = 0;
                zip = new ZIP(dtStock1.Rows[i].ItemArray[1].ToString(), dtStock1.Rows[i].ItemArray[4].ToString(), dtStock1.Rows[i].ItemArray[5].ToString(),
                   dtStock1.Rows[i].ItemArray[6].ToString(), dtStock1.Rows[i].ItemArray[7].ToString(), dtStock1.Rows[i].ItemArray[8].ToString(), dtStock1.Rows[i].ItemArray[0].ToString(), dtStock1.Rows[i].ItemArray[10].ToString(),
                   dtStock1.Rows[i].ItemArray[2].ToString(), dtStock1.Rows[i].ItemArray[3].ToString(), dtStock1.Rows[i].ItemArray[9].ToString(), dtStock1.Rows[i].ItemArray[11].ToString(), dtStock1.Rows[i].ItemArray[12].ToString(), dtStock1.Rows[i].ItemArray[13].ToString());

                if (zip.photo != "")
                {
                    if (File.Exists(zip.photo))
                    {
                        FileStream fs = new FileStream(zip.photo, FileMode.Open, FileAccess.Read);
                        pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                        pictureBox1.Invalidate();
                        photoShow = 1;
                        fs.Close();
                        StockEditorPhotoEditButton1.Text = zip.photo;
                        photoPath = zip.photo;
                    }

                }
                if (zip.photo2 != "")
                {
                    if (File.Exists(zip.photo2))
                    {
                        StockEditorPhotoEditButton2.Text = zip.photo2;
                        photoPath2 = zip.photo2;
                        //Нужно, если не загружено первое фото
                        if (!File.Exists(zip.photo))
                        {
                            FileStream fs = new FileStream(zip.photo2, FileMode.Open, FileAccess.Read);
                            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                            pictureBox1.Invalidate();
                            fs.Close();
                        }
                    }
                }
                if (zip.photo3 != "")
                {
                    if (File.Exists(zip.photo3))
                    {
                        StockEditorPhotoEditButton3.Text = zip.photo3;
                        photoPath3 = zip.photo3;
                        if (!File.Exists(zip.photo) && !File.Exists(zip.photo2))
                        {
                            FileStream fs = new FileStream(zip.photo3, FileMode.Open, FileAccess.Read);
                            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                            pictureBox1.Invalidate();
                            fs.Close();
                        }
                    }
                }
                PrimechanieTextBox.Text = zip.primechanie;
                KategoryComboBox.Text = zip.kategoriya;
                PodKategoryComboBox.Text = zip.podkategoriya;
                BrandComboBox.Text = zip.brand;
                ModelTextBox.Text = zip.model;
                ColourComboBox.Text = zip.colour;
                PriceTextBox.Text = zip.price;
                CountOfTextBox.Text = zip.countOf;
                NapominanieTextBox.Text = zip.napominanie;



            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        private void StockEditorPhotoEditButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                StockEditorPhotoEditButton1.Text = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Invalidate();
                string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath = pPath;
            }
            if (StockEditorPhotoEditButton1.Text == "openFileDialog1")
            {
                StockEditorPhotoEditButton1.Text = "Не загружено";
                photoPath = "";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }

        private void SaveStockButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить запись Арт. " + idOfZip + " в складe?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdStockEdit(NaimenovanieMaker(), KategoryComboBox.Text, PodKategoryComboBox.Text, ColourComboBox.Text, BrandComboBox.Text, ModelTextBox.Text, CountOfTextBox.Text, NapominanieTextBox.Text,
                PriceTextBox.Text, photoPath, idOfZip, PrimechanieTextBox.Text, photoPath2, photoPath3);
                this.Close();
            }

        }
        // Создаёт строку наименования складывая остальные данные через пробел
        private string NaimenovanieMaker()
        {
            if (PrimechanieTextBox.Text != "")
                return KategoryComboBox.Text + " " + PodKategoryComboBox.Text + " " + BrandComboBox.Text + " " + ModelTextBox.Text + " " + ColourComboBox.Text + " (" + PrimechanieTextBox.Text + ")";
            else return KategoryComboBox.Text + " " + PodKategoryComboBox.Text + " " + BrandComboBox.Text + " " + ModelTextBox.Text + " " + ColourComboBox.Text + " " + PrimechanieTextBox.Text;
        }

        private void DeleteStockButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить запись Арт. " + idOfZip + " из склада?", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mainForm.basa.BdStockDelete(idOfZip);
                this.Close();
            }
        }

        private void StockEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            stockForm.Enabled = true;
            stockForm.StockFullSearch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                StockEditorPhotoEditButton2.Text = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Invalidate();
                string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath2 = pPath;
            }
            if (StockEditorPhotoEditButton2.Text == "openFileDialog1")
            {
                StockEditorPhotoEditButton2.Text = "Не загружено";
                photoPath2 = "";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                StockEditorPhotoEditButton3.Text = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Invalidate();
                string rashrenie = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4);
                string pPath = "";
                pPath = @"settings\Stock\Photos\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + rashrenie;
                File.Copy(openFileDialog1.FileName, pPath);
                photoPath3 = pPath;
            }
            if (StockEditorPhotoEditButton3.Text == "openFileDialog1")
            {
                StockEditorPhotoEditButton3.Text = "Не загружено";
                photoPath3 = "";
            }
            openFileDialog1.FileName = "openFileDialog1";
        }
        //Чтобы сразу после первого клика переключалась на вторую картинку
        bool firstclick = true;
        // Чтобы листались фотки в окне просмотра
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (photoPath != "") { maxPhoto = 1; }
                if (photoPath2 != "") { maxPhoto = 2; }
                if (photoPath3 != "") { maxPhoto = 3; }
                if (maxPhoto > 0) { ++photoShow; }
                if (photoShow != 0)
                {
                    if (photoShow > maxPhoto)
                    {
                        photoShow = 1;
                    }
                    if (firstclick)
                    {
                        //Чтобы и картинки листались, и удалялось нормально, так как Photoshow еще и в инициализации первой картинки участвует
                        photoShow = 1;
                        ++photoShow;
                        firstclick = false;
                    }
                    if (photoShow == 1 && photoPath != "" && File.Exists(photoPath))
                    {
                        FileStream fs = new FileStream(photoPath, FileMode.Open, FileAccess.Read);
                        pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                        pictureBox1.Invalidate();
                        fs.Close();

                    }
                    else if (photoShow == 2 && photoPath2 != "" && File.Exists(photoPath2))
                    {
                        FileStream fs = new FileStream(photoPath2, FileMode.Open, FileAccess.Read);
                        pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                        pictureBox1.Invalidate();
                        fs.Close();

                    }
                    else if (photoShow == 3 && photoPath3 != "" && File.Exists(photoPath3))
                    {
                        FileStream fs = new FileStream(photoPath3, FileMode.Open, FileAccess.Read);
                        pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                        pictureBox1.Invalidate();
                        fs.Close();
                    }

                    else
                    {
                        ++photoShow;
                        if (photoShow == 1 && photoPath != "" && File.Exists(photoPath))
                        {
                            FileStream fs = new FileStream(photoPath, FileMode.Open, FileAccess.Read);
                            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                            pictureBox1.Invalidate();
                            fs.Close();

                        }
                        else if (photoShow == 2 && photoPath2 != "" && File.Exists(photoPath2))
                        {
                            FileStream fs = new FileStream(photoPath2, FileMode.Open, FileAccess.Read);
                            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                            pictureBox1.Invalidate();
                            fs.Close();

                        }
                        else if (photoShow == 3 && photoPath3 != "" && File.Exists(photoPath3))
                        {
                            FileStream fs = new FileStream(photoPath3, FileMode.Open, FileAccess.Read);
                            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                            pictureBox1.Invalidate();
                            fs.Close();
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Для нормального пролистывания фото, нужно соблюдать очередность при добавлении фото, сначала 1, потом 2, потом 3");
            }

        }
        //Удаляем данные о рисунке
        private void DeletePhotoButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить фото 1? Так же будет удалён файл фото с компьютера", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                StockEditorPhotoEditButton1.Text = "Загрузить фото 1";
                if (photoShow == 1)
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
                if (File.Exists(photoPath))
                {
                    File.Delete(photoPath);
                }
                photoPath = "";
            }
        }

        private void DeletePhotoButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить фото 2? Так же будет удалён файл фото с компьютера", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                StockEditorPhotoEditButton2.Text = "Загрузить фото 2";
                if (photoShow == 2)
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
                if (File.Exists(photoPath2))
                {
                    File.Delete(photoPath2);
                }
                photoPath2 = "";
            }
        }

        private void DeletePhotoButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить фото 3? Так же будет удалён файл фото с компьютера", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                StockEditorPhotoEditButton3.Text = "Загрузить фото 3";
                if (photoShow == 3)
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
                if (File.Exists(photoPath3))
                {
                    File.Delete(photoPath3);
                }
                photoPath3 = "";
            }
        }
        private void UsedZIPCounterLabelUpdate()
        {
            if (decimal.Parse(idInMainBd) > -1)
                UsedZipCounterLabel.Text = string.Format("Запчастей с арт. {0} использовано на клиента {1}: {2} Шт. ", idOfZip, mainForm.basa.ClientsReadOne("FIO", clientId), mainForm.basa.BdStockMapZIPUsedCoutner(idInMainBd, idOfZip));
        }
        private void UseZipButton_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(idInMainBd) > 0)
            {
                if (CountOfZIPNumericUpDown.Value > 0)
                {
                    if (decimal.Parse(CountOfTextBox.Text) >= CountOfZIPNumericUpDown.Value)
                    {
                        if (MessageBox.Show("Вы действительно хотите использовать запчать  Арт. " + idOfZip + " для клиента: " + mainForm.basa.ClientsReadOne("FIO", clientId) + " В количестве " + CountOfZIPNumericUpDown.Value.ToString() + "Шт.", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            if (editorForm != null)
                            {
                                // В редакторе меняем поле затрат
                                editorForm.ZatratyTextBox.Text = ((decimal.Parse(editorForm.ZatratyTextBox.Text) + decimal.Parse(PriceTextBox.Text) * CountOfZIPNumericUpDown.Value)).ToString();
                                //Убираем со склада, столько, сколько использовали
                                string counerZIP = (decimal.Parse(CountOfTextBox.Text) - CountOfZIPNumericUpDown.Value).ToString();
                                CountOfTextBox.Text = counerZIP;
                                // Записываем базу данных, что использовалось.
                                mainForm.basa.BdStockMapWrite(idInMainBd, idOfZip, CountOfZIPNumericUpDown.Value.ToString(), PriceTextBox.Text);
                                //Изменяем значение в складе
                                mainForm.basa.BdStockEditOne("CountOf", counerZIP, idOfZip);
                                // Состояние кнопки отмены
                                if (mainForm.basa.BdStockMapZIPUsedCheck(idInMainBd, idOfZip))
                                {
                                    CancelZIPButton.Enabled = true;
                                }
                                // Пишем в основную базу, чуть блядь не забыл про это
                                mainForm.basa.BdEditOne("Zatrati", editorForm.ZatratyTextBox.Text, idInMainBd);

                                // Обновляем надпись с кол-вом использующихся запчастей
                                UsedZIPCounterLabelUpdate();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("На скалде нет столько " + NaimenovanieMaker() + ", сколько вы указали в затраты");
                        return;
                    }

                }
                else
                    MessageBox.Show("Вы не можете добавить к заказу 0 запчастей! Минимум 1");
            }
            else
                MessageBox.Show("Клиент не выбран");

        }

        private void CancelZIPButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("1. Удалятся все использования данной запчасти клиентом " + mainForm.basa.ClientsReadOne("FIO", clientId) + Environment.NewLine + "2. В складе прибавится количество " + NaimenovanieMaker() + ", которое было потрачено на этого клиента" + Environment.NewLine + "3. Все затраты, сделанные клиентом на эту запчасть, отменятся, изменения сохранятся только для данного клиента ", "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    decimal counterOfUsedZIP = 0;
                    decimal counterOfSpendedMoney = 0;
                    DataTable dtStockMap1;
                    dtStockMap1 = mainForm.basa.BdStockMapZIPDeleteCounter(idInMainBd, idOfZip);
                    for (int i = 0; i < dtStockMap1.Rows.Count; i++)
                    {
                        counterOfUsedZIP += decimal.Parse(dtStockMap1.Rows[i].ItemArray[3].ToString());
                        counterOfSpendedMoney += decimal.Parse(dtStockMap1.Rows[i].ItemArray[3].ToString()) * decimal.Parse(dtStockMap1.Rows[i].ItemArray[4].ToString());
                    }
                    string zatratiTBox = (decimal.Parse(editorForm.ZatratyTextBox.Text) - counterOfSpendedMoney).ToString();
                    editorForm.ZatratyTextBox.Text = zatratiTBox;
                    //Так же меняем в бд
                    mainForm.basa.BdEditOne("Zatrati", zatratiTBox, idInMainBd);
                    //Так же добавляем обратно в склад
                    CountOfTextBox.Text = (decimal.Parse(CountOfTextBox.Text) + counterOfUsedZIP).ToString();
                    mainForm.basa.BdStockEditOne("CountOf", CountOfTextBox.Text, idOfZip);

                    // Удаляем данные из бд
                    mainForm.basa.BdStockMapDeleteZIP(idInMainBd, idOfZip);
                    // Обновляем надпись с кол-вом использующихся запчастей
                    UsedZIPCounterLabelUpdate();
                    MessageBox.Show("Было восстановлено " + counterOfUsedZIP.ToString() + " Запчастей" + Environment.NewLine + "Было списано из затрат " + counterOfSpendedMoney + " Денег");

                }
                if (!mainForm.basa.BdStockMapZIPUsedCheck(idInMainBd, idOfZip))
                {
                    CancelZIPButton.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }

        }
    }
}
