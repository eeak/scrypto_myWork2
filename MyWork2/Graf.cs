using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Graf : Form
    {
        Form1 mainForm;
        List<string> models = new List<string>();
        List<string> modelsAndCount = new List<string>();
        public Graf(Form1 mf)
        {
            mainForm = mf;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Обнуляем данные во всех lables
                label5.Text = "Выручка: ";
                label6.Text = "Затраты: ";
                label7.Text = "Итого: ";
                label13.Text = "Оплата мастеру: ";

                label8.Text = "Средняя выручка за 1 заказ: ";
                label9.Text = "Средняя скидка за 1 заказ: ";
                label10.Text = "Всего заказов за выбранный период: ";
                label11.Text = "Из них без ремонта: ";


                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = mainForm.basa.BdReadGrafList(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), comboBox1.Text, ServiceAdressComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text, comboBox3.Text);
                //Сортировка по дате
                //Оператор BETWEEN, соверашает поиск между интервалами дат, в нашем случае между строками, поэтому даты идут в формате YYYY.MM.DD

                //  dataGridView1.DataSource = dSet.Tables[0].DefaultView;

                double summa_viruchki = 0;
                double summ_zatrat = 0;
                double summ_skidki = 0;
                double bez_remonta = 0;

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        summa_viruchki += double.Parse(vClientList[i].Okonchatelnaya_stoimost_remonta);
                        // Не вычитаем скидку из выручки
                        // summa_viruchki -= double.Parse(dSet.Tables[0].Rows[i].ItemArray[19].ToString());
                        summ_zatrat += double.Parse(vClientList[i].Zatrati);
                        summ_skidki += double.Parse(vClientList[i].Skidka);
                        //Два раза без ремонта, так как иногда проставляют с запятой
                        if (vClientList[i].Vipolnenie_raboti.ToUpper() == "Без ремонта,".ToUpper())
                        {
                            bez_remonta += 1;
                        }

                        if (vClientList[i].Vipolnenie_raboti.ToUpper() == "Без ремонта".ToUpper())
                        {
                            bez_remonta += 1;
                        }
                    }
                }


                if (vClientList.Count > 0)
                {
                    label5.Text = "Выручка: " + summa_viruchki + " " + TemporaryBase.valuta;
                    label6.Text = "Затраты: " + summ_zatrat + " " + TemporaryBase.valuta;
                    label7.Text = "Итого: " + (summa_viruchki - summ_zatrat) + " " + TemporaryBase.valuta;

                    label8.Text = "Средняя выручка за 1 заказ: " + (summa_viruchki - summ_zatrat) / (vClientList.Count) + " " + TemporaryBase.valuta;
                    label9.Text = "Средняя скидка за 1 заказ: " + summ_skidki / (vClientList.Count) + " " + TemporaryBase.valuta;
                    label10.Text = "Всего заказов за выбранный период: " + vClientList.Count;
                    label11.Text = "Из них без ремонта: " + bez_remonta;
                    string GrName = String.Format(comboBox1.Text + " " + ServiceAdressComboBox.Text + " " + What_remont_combo_box.Text + " " + BrandComboBox.Text);
                    if (GrName.Trim() == "")
                    {
                        GrName = "Все";
                    }
                    chart1.Series["Выручка"].Points.AddXY(GrName, summa_viruchki);
                    chart1.Series["Затраты"].Points.AddXY(GrName, summ_zatrat);
                    chart1.Series["Итого"].Points.AddXY(GrName, summa_viruchki - summ_zatrat);

                    label13.Text = "Оплата мастеру: " + (summa_viruchki - summ_zatrat) * (int.Parse(TemporaryBase.Mpersent) * 0.01) + " " + TemporaryBase.valuta;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Graf_Load(object sender, EventArgs e)
        {

            foreach (string strCombo in TemporaryBase.SortirovkaMasters)
            {
                comboBox1.Items.Add(strCombo);
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
            ToolTip t = new ToolTip();
            t.SetToolTip(comboBox1, "Если хотите искать по всем мастреам, выберите пустную строку");
            t.SetToolTip(ServiceAdressComboBox, "Если хотите искать по всем СЦ, выберите пустую строку");

            dateTimePicker1.Value = DateTime.Now;
            foreach (SortirovkaSpiskov ssp in TemporaryBase.SortirovkaAboutUs)
            {
                comboBox3.Items.Add(ssp.SortObj);
            }
        }


        private void ReportExcelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Отчёт будет сохранён в папке reports, после чего откроется окно с этой папкой", "Отчёт", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    //Обнуляем данные во всех lables
                    label5.Text = "Выручка: ";
                    label6.Text = "Затраты: ";
                    label7.Text = "Итого: ";

                    label8.Text = "Средняя выручка за 1 заказ: ";
                    label9.Text = "Средняя скидка за 1 заказ: ";
                    label10.Text = "Всего заказов за выбранный период: ";
                    label11.Text = "Из них без ремонта: ";


                    List<VirtualClient> vClientList = new List<VirtualClient>();
                    vClientList = mainForm.basa.BdReadGrafList(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), comboBox1.Text, ServiceAdressComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text, comboBox3.Text);
                    //Сортировка по дате
                    //Оператор BETWEEN, соверашает поиск между интервалами дат, в нашем случае между строками, поэтому даты идут в формате YYYY.MM.DD

                    //  dataGridView1.DataSource = dSet.Tables[0].DefaultView;

                    double summa_viruchki = 0;
                    double summ_zatrat = 0;
                    double summ_skidki = 0;
                    double bez_remonta = 0;

                    if (vClientList.Count > 0)
                    {
                        using (var eP = new ExcelPackage())
                        {

                            eP.Workbook.Properties.Author = "Scrypto";
                            eP.Workbook.Properties.Title = "Отчёт";
                            eP.Workbook.Properties.Company = "MyWork2";

                            var sheet = eP.Workbook.Worksheets.Add("Прайс-лист");
                            var row = 1;
                            var col = 1;
                            //Для excel
                            // шапка
                            sheet.Cells[row, col].Value = "Номер заказа";
                            sheet.Cells[row, col + 1].Value = "ФИО клиента";
                            sheet.Cells[row, col + 2].Value = "Тип устройства";
                            sheet.Cells[row, col + 3].Value = "Фирма устройства";
                            sheet.Cells[row, col + 4].Value = "Модель";
                            sheet.Cells[row, col + 5].Value = "Неисправность";
                            sheet.Cells[row, col + 6].Value = "Выполненные работы";
                            sheet.Cells[row, col + 7].Value = "Стоимость ремонта";
                            sheet.Cells[row, col + 8].Value = "Мастер";
                            sheet.Cells[row, col + 9].Value = "Телефон клиента";
                            sheet.Cells[row, col + 10].Value = "Дата приёма";
                            sheet.Cells[row, col + 11].Value = "Дата выдачи";
                            sheet.Cells[row, col + 12].Value = "Как долго забирали";
                            sheet.Cells[row, col + 13].Value = "Адрес СЦ";
                            sheet.Cells[row, col + 14].Value = "Откуда узнали о нас";
                            sheet.Cells[row, col + 15].Value = "Оплата мастеру";
                            sheet.Cells[row, col + 16].Value = "Затраты";

                            //Добавляем ряд
                            row++;
                            //Цикл, где все происходит ))
                            for (int i = 0; i < vClientList.Count; i++)
                            {
                                summa_viruchki += double.Parse(vClientList[i].Okonchatelnaya_stoimost_remonta);
                                // Не вычитаем скидку из выручки
                                // summa_viruchki -= double.Parse(dSet.Tables[0].Rows[i].ItemArray[19].ToString());
                                summ_zatrat += double.Parse(vClientList[i].Zatrati);
                                summ_skidki += double.Parse(vClientList[i].Skidka);
                                //Два раза без ремонта, так как иногда проставляют с запятой
                                if (vClientList[i].Vipolnenie_raboti.ToUpper() == "Без ремонта,".ToUpper())
                                {
                                    bez_remonta += 1;
                                }

                                if (vClientList[i].Vipolnenie_raboti.ToUpper() == "Без ремонта".ToUpper())
                                {
                                    bez_remonta += 1;
                                }

                                //Обработка для excel
                                sheet.Cells[row, col].Value = vClientList[i].Id;
                                sheet.Cells[row, col + 1].Value = vClientList[i].Surname; //fio
                                sheet.Cells[row, col + 2].Value = vClientList[i].WhatRemont;
                                sheet.Cells[row, col + 3].Value = vClientList[i].Brand;
                                sheet.Cells[row, col + 4].Value = vClientList[i].Model;
                                sheet.Cells[row, col + 5].Value = vClientList[i].Polomka;
                                sheet.Cells[row, col + 6].Value = vClientList[i].Vipolnenie_raboti;
                                sheet.Cells[row, col + 7].Value = vClientList[i].Okonchatelnaya_stoimost_remonta;
                                sheet.Cells[row, col + 8].Value = vClientList[i].Master;
                                sheet.Cells[row, col + 9].Value = vClientList[i].Phone;
                                sheet.Cells[row, col + 10].Value = vClientList[i].Data_priema;
                                sheet.Cells[row, col + 11].Value = vClientList[i].Data_vidachi;
                                sheet.Cells[row, col + 12].Value = String.Format("{0:0}", (DateTime.Parse(vClientList[i].Data_vidachi) - DateTime.Parse(vClientList[i].Data_priema)).TotalDays);
                                sheet.Cells[row, col + 13].Value = vClientList[i].AdressSC;
                                sheet.Cells[row, col + 14].Value = vClientList[i].AboutUs;
                                sheet.Cells[row, col + 15].Value = (double.Parse(vClientList[i].Okonchatelnaya_stoimost_remonta) - double.Parse(vClientList[i].Zatrati)) * (int.Parse(TemporaryBase.Mpersent) * 0.01); //
                                sheet.Cells[row, col + 16].Value = vClientList[i].Zatrati;

                                // указываем что число
                                // sheet.Cells[row, col].Style.Numberformat.Format = @"";
                                row++;
                            }


                            // добави всем ячейкам рамку
                            using (var cells = sheet.Cells[sheet.Dimension.Address])
                            {
                                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                cells.AutoFitColumns();
                            }

                            // сохраняем в файл
                            var bin = eP.GetAsByteArray();
                            string nameOfReport = string.Format("reports/Отчёт от {0} до {1} по мастеру {2}.xlsx", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), (comboBox1.Text == "") ? "Все" : comboBox1.Text);
                            File.WriteAllBytes(nameOfReport, bin);
                            //Открыть файл в папке
                            Process PrFolder = new Process();
                            ProcessStartInfo psi = new ProcessStartInfo();
                            string file = nameOfReport.Replace("/", "\\");
                            psi.CreateNoWindow = true;
                            psi.WindowStyle = ProcessWindowStyle.Normal;
                            psi.FileName = "explorer";
                            psi.Arguments = @"/n, /select, " + file;
                            PrFolder.StartInfo = psi;
                            PrFolder.Start();
                        }


                        if (vClientList.Count > 0)
                        {
                            label5.Text = "Выручка: " + summa_viruchki + " " + TemporaryBase.valuta;
                            label6.Text = "Затраты: " + summ_zatrat + " " + TemporaryBase.valuta;
                            label7.Text = "Итого: " + (summa_viruchki - summ_zatrat) + " " + TemporaryBase.valuta;

                            label8.Text = "Средняя выручка за 1 заказ: " + (summa_viruchki - summ_zatrat) / (vClientList.Count) + " " + TemporaryBase.valuta;
                            label9.Text = "Средняя скидка за 1 заказ: " + summ_skidki / (vClientList.Count) + " " + TemporaryBase.valuta;
                            label10.Text = "Всего заказов за выбранный период: " + vClientList.Count;
                            label11.Text = "Из них без ремонта: " + bez_remonta;
                            string masterName = "Все";
                            if (comboBox1.Text != "")
                            {
                                masterName = comboBox1.Text;
                            }
                            chart1.Series["Выручка"].Points.AddXY(masterName, summa_viruchki);
                            chart1.Series["Затраты"].Points.AddXY(masterName, summ_zatrat);
                            chart1.Series["Итого"].Points.AddXY(masterName, summa_viruchki - summ_zatrat);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void AboutUsStatButton_Click(object sender, EventArgs e)
        {
            try
            {

                List<VirtualClient> vClientList = new List<VirtualClient>();
                vClientList = mainForm.basa.BdReadGrafList(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), comboBox1.Text, ServiceAdressComboBox.Text, What_remont_combo_box.Text, BrandComboBox.Text, comboBox3.Text);
                List<string> aboutus = new List<string>();
                List<string> aboutusVariants = new List<string>();

                if (vClientList.Count > 0)
                {
                    //Цикл, где все происходит ))
                    for (int i = 0; i < vClientList.Count; i++)
                    {
                        if (vClientList[i].AboutUs != "")
                        {
                            aboutus.Add(vClientList[i].AboutUs);
                        }

                    }
                    for (int v = 0; v < aboutus.Count; v++)
                    {
                        bool addMe = true;
                        if (aboutusVariants.Count == 0)
                        {
                            aboutusVariants.Add(aboutus[v]);
                        }
                        for (int z = 0; z < aboutusVariants.Count; z++)
                        {
                            if (aboutusVariants[z] == aboutus[v])
                                addMe = false;
                        }
                        if (addMe)
                        {
                            aboutusVariants.Add(aboutus[v]);
                        }
                    }

                }
                int[] arr = new int[aboutusVariants.Count];
                for (int zi = 0; zi < aboutusVariants.Count; zi++)
                {
                    for (int i = 0; i < aboutus.Count; i++)
                    {
                        if (aboutusVariants[zi] == aboutus[i])
                        {
                            arr[zi] += 1;
                        }
                    }
                }
                string strFull = "";
                for (int i = 0; i < aboutusVariants.Count; i++)
                {
                    strFull += aboutusVariants[i] + ": " + arr[i].ToString() + Environment.NewLine;
                }
                if (strFull != "")
                    MessageBox.Show(strFull);
                else
                    MessageBox.Show("За данные период нечего покаызвать");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                if (comboBox2.SelectedIndex == 0)
                    dateTimePicker1.Value = DateTime.Now;
                else if (comboBox2.SelectedIndex == 1)
                    dateTimePicker1.Value = DateTime.Now.AddDays(-6);
                else if (comboBox2.SelectedIndex == 2)
                    dateTimePicker1.Value = DateTime.Now.AddMonths(-1).AddDays(1);
                else if (comboBox2.SelectedIndex == 3)
                    dateTimePicker1.Value = DateTime.Now.AddYears(-1).AddDays(1);
                else if (comboBox2.SelectedIndex == 4)
                    dateTimePicker1.Value = DateTime.Now.AddYears(-20).AddDays(1);
            }
        }
    }
}
