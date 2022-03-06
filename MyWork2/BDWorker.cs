using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyWork2
{
    public class BDWorker
    {
        Form1 mainForm;
        public String dbFileName = "baza.sqlite";
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;

        public BDWorker(Form1 fm)
        {
            mainForm = fm;
        }
        //Создание базы данных
        public void CreateBd()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            if (!File.Exists(dbFileName))
            {
                // С какого номера начинать инкременацию
                BaseLineNumber bln1 = new BaseLineNumber(mainForm);
                bln1.Show();

                SQLiteConnection.CreateFile(dbFileName);
            }


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, Data_priema TEXT, Data_vidachi TEXT, Data_predoplaty TEXT, surname TEXT,phone TEXT, AboutUs TEXT, WhatRemont TEXT, brand TEXT, model TEXT, SerialNumber TEXT, sostoyanie TEXT, komplektonst TEXT, polomka TEXT,kommentarij  TEXT, predvaritelnaya_stoimost TEXT, Predoplata TEXT, Zatrati TEXT, okonchatelnaya_stoimost_remonta TEXT,Skidka TEXT, Status_remonta TEXT,master TEXT, vipolnenie_raboti TEXT,Garanty TEXT, wait_zakaz TEXT,Adress TEXT, Image_key TEXT, AdressSC TEXT, DeviceColour TEXT, ClientId TEXT, Barcode TEXT)";
                m_sqlCmd.ExecuteNonQuery();
                //Добавляет нужные столбцы, если их нет
                SovmestimostBDTester();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Создание базы данных
        public void CreateBd(string incr_auto_number)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            if (incr_auto_number != "1" || incr_auto_number != "0" || incr_auto_number != "")
            {
                try
                {
                    m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                    m_dbConn.Open();
                    m_sqlCmd.Connection = m_dbConn;

                    m_sqlCmd.CommandText = string.Format("UPDATE sqlite_sequence SET seq = {0} WHERE name = 'Catalog'", incr_auto_number);
                    m_sqlCmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
                }
            }

        }


        // Проверяет наличие столбцов от новой версии Adress и Image_key
        private void SovmestimostBDTester()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
            }
            try
            {
                //Создаём базу данных склада
                CreateStock();
                CreateStockMap();
                HistoryBDTable_Create();
                //  UsersTable_Create(); // Дублирую в form1.load, чтобы не было ошбок
                GroupDostupTable_Create();
                sqlQuery = "pragma table_info(Catalog)";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                // Создаём таблицу клиентов
                mainForm.basa.ClientsMapTable_Create();
                // Ибо обратная совместимость важна
                if (dTable.Rows.Count > 0)
                {
                    if (dTable.Rows.Count < 27)
                    {
                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN Adress";
                        m_sqlCmd.ExecuteNonQuery();

                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN Image_key";
                        m_sqlCmd.ExecuteNonQuery();
                    }
                    if (dTable.Rows.Count < 29)
                    {
                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN AdressSC";
                        m_sqlCmd.ExecuteNonQuery();

                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN DeviceColour";
                        m_sqlCmd.ExecuteNonQuery();
                        // Дабы не было пустых полей в новых записях
                        mainForm.basa.BdNoNull("Image_key");
                        mainForm.basa.BdNoNull("Adress");
                        mainForm.basa.BdNoNull("wait_zakaz");
                        mainForm.basa.BdNoNull("AdressSC");
                        mainForm.basa.BdNoNull("DeviceColour");
                    }
                    // Конвертируем базу в новый формат
                    if (dTable.Rows.Count < 30)
                    {
                        if (!Directory.Exists("settings/backup"))
                        {
                            Directory.CreateDirectory("settings/backup");
                        }
                        //Бэкапим старую базу
                        if (File.Exists(dbFileName))
                            File.Copy(dbFileName, "settings/backup/baza.sqlite_backup" + DateTime.Now.ToString("dd-MM-yyyy HH-mm"));
                        mainForm.basa.CreateStock();
                        mainForm.basa.CreateStockMap();

                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN ClientId";
                        m_sqlCmd.ExecuteNonQuery();
                        DataTable dt1 = mainForm.basa.BdReadAll();
                        List<VirtualClient> vc1List = new List<VirtualClient>();
                        List<VirtualClient> vc2List = new List<VirtualClient>();
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                vc1List.Add(new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                               dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                               dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                               dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                               dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                               dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), true, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString()));
                            }

                        }
                        // Составляем список уникальных
                        for (int i = 0; i < vc1List.Count; i++)
                        {
                            bool povtor = false;
                            for (int j = 0; j < vc2List.Count; j++)
                            {

                                if (vc1List[i].Surname == vc2List[j].Surname && vc1List[i].Phone == vc2List[j].Phone && vc1List[i].Id != vc2List[j].Id)
                                {
                                    povtor = true;
                                }

                            }
                            if (!povtor)
                            {
                                vc2List.Add(vc1List[i]);
                            }
                        }
                        //Begin Loop
                        m_sqlCmd = new SQLiteCommand("begin", m_sqlCmd.Connection);
                        m_sqlCmd.ExecuteNonQuery();
                        // Записываем номера клиентов к номеру записи
                        foreach (VirtualClient vc1 in vc1List)
                        {
                            for (int i = 0; i < vc2List.Count; i++)
                            {
                                if (vc1.Surname == vc2List[i].Surname && vc1.Phone == vc2List[i].Phone)
                                {
                                    if (m_dbConn.State != ConnectionState.Open)
                                    {
                                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                                        return;
                                    }

                                    try
                                    {
                                        m_sqlCmd.CommandText = "UPDATE Catalog SET " + "ClientId" + " ='" + (i + 1).ToString() + "' WHERE ID = " + vc1.Id;
                                        m_sqlCmd.ExecuteNonQuery();

                                    }
                                    catch (SQLiteException ex)
                                    {
                                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
                                    }
                                }
                            }
                        }
                        //---END LOOP
                        m_sqlCmd = new SQLiteCommand("end", m_sqlCmd.Connection);
                        m_sqlCmd.ExecuteNonQuery();

                        try
                        {
                            if (m_dbConn.State != ConnectionState.Open)
                            {
                                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                                return;
                            }
                            if (vc2List.Count == 1)
                            {
                                foreach (VirtualClient VC in vc2List)
                                {
                                    string query = "INSERT INTO ClientsMap ('FIO', 'Phone','Adress','Primechanie','Blist','Date','aboutUs') values ('" +
                                          VC.Surname.Trim().ToUpper() + "' , '" +
                                          VC.Phone.Trim() + "', '" +
                                          VC.Adress.Trim() + "', '" +
                                          "" + "', '" +
                                          "0" + "', '" +
                                          VC.Data_priema + "', '" +
                                          VC.AboutUs.Trim() + "')";
                                    m_sqlCmd = new SQLiteCommand(query, m_sqlCmd.Connection);
                                    m_sqlCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                m_sqlCmd = new SQLiteCommand("begin", m_sqlCmd.Connection);
                                m_sqlCmd.ExecuteNonQuery();
                                //---INSIDE LOOP
                                foreach (VirtualClient VC in vc2List)
                                {
                                    string query = "INSERT INTO ClientsMap ('FIO', 'Phone','Adress','Primechanie','Blist','Date','aboutUs') values ('" +
                                          VC.Surname.Trim().ToUpper() + "' , '" +
                                          VC.Phone.Trim() + "', '" +
                                          VC.Adress.Trim() + "', '" +
                                          "" + "', '" +
                                          "0" + "', '" +
                                          VC.Data_priema + "', '" +
                                          VC.AboutUs.Trim() + "')";
                                    m_sqlCmd = new SQLiteCommand(query, m_sqlCmd.Connection);
                                    m_sqlCmd.ExecuteNonQuery();
                                }
                                //---END LOOP
                                m_sqlCmd = new SQLiteCommand("end", m_sqlCmd.Connection);
                                m_sqlCmd.ExecuteNonQuery();
                            }
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
                        }


                        MessageBox.Show(String.Format("В базе найдено {0} уникальных клиентов{2} Всего записей в базе {1} {2} ", vc2List.Count(), vc1List.Count(), Environment.NewLine), "База данных обновилась", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    }
                    //Для сканера штрихкодов
                    if (dTable.Rows.Count < 31)
                    {
                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN Barcode";
                        m_sqlCmd.ExecuteNonQuery();
                        mainForm.basa.BdNoNull("Barcode");
                        mainForm.basa.BdNoNullRename("Status_remonta", "Ждёт запчасть", "Ждет ЗИП");
                    }
                    //Для удаления записей
                    if (dTable.Rows.Count < 32)
                    {
                        m_sqlCmd.CommandText = "ALTER TABLE Catalog ADD COLUMN Deleted";
                        m_sqlCmd.ExecuteNonQuery();
                        mainForm.basa.BdNoNull("Deleted");
                    }


                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении id записи из базы данных " + ex.ToString() + Environment.NewLine);
            }


        }

        // получает ID последней записи в базе
        public int BdReadAdvertsDataTop()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            int lastId = 0;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return lastId;
            }
            try
            {
                sqlQuery = "SELECT id FROM Catalog ORDER BY id DESC LIMIT 1";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return int.Parse(dTable.Rows[0].ItemArray[0].ToString());
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": База данных пуста" + Environment.NewLine);

                return lastId;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении id записи из базы данных " + ex.ToString() + Environment.NewLine);
                return lastId;
            }

        }
        // ПРоверяет есть ли такая запись
        public int CatlogIDExists(string Catalog_id)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            int lastId = 0;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return lastId;
            }
            try
            {
                sqlQuery = String.Format("SELECT EXISTS(SELECT * FROM Catalog WHERE id = '{0}' LIMIT 1)", Catalog_id);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return int.Parse(dTable.Rows[0].ItemArray[0].ToString());
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": База данных пуста" + Environment.NewLine);

                return lastId;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении id записи из базы данных " + ex.ToString() + Environment.NewLine);
                return lastId;
            }

        }
        // получает ID первой записи в базе
        public int BdReadAdvertsDataFirt()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            int lastId = 0;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return lastId;
            }
            try
            {
                sqlQuery = "SELECT id FROM Catalog LIMIT 1";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return int.Parse(dTable.Rows[0].ItemArray[0].ToString());
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": База данных пуста" + Environment.NewLine);

                return lastId;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении id записи из базы данных " + ex.ToString() + Environment.NewLine);
                return lastId;
            }

        }

        // Чтение из базы данных
        public DataTable BdRead(bool trfl)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                //bool Для сортировке с датой выдачи и без (false = выдан)
                if (!trfl)
                {
                    sqlQuery = "SELECT * FROM Catalog WHERE Data_Vidachi != ''";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                    adapter.Fill(dTable);

                    return dTable;
                }
                else
                {
                    sqlQuery = "SELECT * FROM Catalog WHERE Data_Vidachi == ''";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                    adapter.Fill(dTable);

                    return dTable;
                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Чтение всех записей из базы данных
        public DataTable BdReadAll()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = "SELECT * FROM Catalog";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }


        // Чтение из базы данных
        public DataTable BdReadOneEditor(string id_bd)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = "SELECT * FROM Catalog WHERE id = " + id_bd;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Чтение из базы, для Графиков
        public DataSet BdReadGraf(string calendar1, string calendar2, string master)
        {
            DataSet dTable = new DataSet();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = string.Format("SELECT * FROM Catalog WHERE( [{0}] BETWEEN '{1}' AND '{2}') AND Data_vidachi !='' AND master LIKE'%{3}%'", "Data_vidachi",
                    calendar1, calendar2, master.ToUpper());

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Чтение из базы, для Графиков с новым времянным форматом
        public List<VirtualClient> BdReadGrafList(string calendar1, string calendar2, string master, string AdressSCINBD, string whatRemont, string brand, string aboutUs)
        {
            List<VirtualClient> vClientList = new List<VirtualClient>();
            DataTable dt1 = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return vClientList;
            }
            try
            {

                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.Data_Vidachi != '' AND c.master LIKE'%{0}%' AND c.AdressSC LIKE '%{1}%' AND c.WhatRemont LIKE '%{2}%' AND c.brand LIKE '%{3}%' AND cm.AboutUs LIKE '%{4}%'", master.ToUpper().Trim(), AdressSCINBD.ToUpper().Trim(), whatRemont.ToUpper().Trim(), brand.ToUpper().Trim(), aboutUs);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        //Если дата в нужном дипапазоне, то добавляем запись в список
                        if (DateTime.Parse(DateTime.Parse(dt1.Rows[i].ItemArray[2].ToString()).ToShortDateString()) >= DateTime.Parse(calendar1) && DateTime.Parse(DateTime.Parse(dt1.Rows[i].ItemArray[2].ToString()).ToShortDateString()) <= DateTime.Parse(calendar2))
                        {
                            VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                                     dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                                     dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                                 dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                                 dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                                 dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), false, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString());
                            vClientList.Add(vc);
                        }
                    }
                    return vClientList;
                }
                return vClientList;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return vClientList;

        }
        // Чтение из базы, для Графиков с новым времянным форматом
        public List<VirtualClient> BdReadSmsList(string calendar1, string calendar2, string AdressSCINBD, string whatRemont, string brand)
        {
            List<VirtualClient> vClientList = new List<VirtualClient>();
            DataTable dt1 = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return vClientList;
            }
            try
            {

                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.Data_Vidachi != '' AND c.AdressSC LIKE '%{0}%' AND c.WhatRemont LIKE '%{1}%' AND c.brand LIKE '%{2}%'", AdressSCINBD.ToUpper().Trim(), whatRemont.ToUpper().Trim(), brand.ToUpper().Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        //Если дата в нужном дипапазоне, то добавляем запись в список
                        if (DateTime.Parse(DateTime.Parse(dt1.Rows[i].ItemArray[2].ToString()).ToShortDateString()) >= DateTime.Parse(calendar1) && DateTime.Parse(DateTime.Parse(dt1.Rows[i].ItemArray[2].ToString()).ToShortDateString()) <= DateTime.Parse(calendar2))
                        {
                            VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                                     dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                                     dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                                 dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                                 dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                                 dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), false, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString());
                            vClientList.Add(vc);
                        }
                    }
                    return vClientList;
                }
                return vClientList;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return vClientList;

        }
        // Чтение из базы, для выпадающих списков
        public List<VirtualClient> BdReadListTechnics(string adressSC)
        {
            // Следующая строчка затычка )
            List<VirtualClient> vClientList1 = new List<VirtualClient>();

            List<VirtualClient> vClientList = new List<VirtualClient>();
            DataTable dt1 = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return vClientList;
            }
            try
            {

                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.AdressSC LIKE '%{0}%'", adressSC.ToUpper().Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                                 dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                                 dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                             dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                             dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                             dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), false, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString());
                        vClientList.Add(vc);

                    }
                    return vClientList;
                }
                return vClientList;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return vClientList;
        }

        //Выдёргиваем номера телефонов для последующей передачи в экспорт
        public List<VirtualClient> ExportPhonesVCList(string from, string to)
        {
            List<VirtualClient> vClientList = new List<VirtualClient>();
            DataTable dt1 = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return vClientList;
            }
            try
            {

                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.id >= {0} AND c.id <={1}", from, to);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt1);


                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {


                        VirtualClient vc = new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                                 dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                                 dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                             dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                             dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                             dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), false, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString());
                        vClientList.Add(vc);

                    }
                    return vClientList;
                }
                return vClientList;


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return vClientList;

        }

        // Чтение из базы данных
        public string BdReadBarcode(string barcode)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            string ReadData = "";

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return ReadData;
            }
            try
            {
                sqlQuery = string.Format("SELECT id FROM Catalog Where Barcode = '{0}'", barcode);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу прочитать данные записи со штрихкодом  " + barcode + Environment.NewLine);

                return ReadData;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении записи из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return ReadData;

        }
        // Чтение из базы данных
        public string BdReadOne(string readWhat, string id_bd)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            string ReadData = "";

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return ReadData;
            }
            try
            {
                sqlQuery = "SELECT " + readWhat + " FROM Catalog Where id =" + id_bd;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу прочитать даты записи номер " + id_bd + Environment.NewLine);

                return ReadData;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении записи из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return ReadData;

        }

        //Запись в базу данных
        public void BdWrite(string Data_priema, string Data_vidachi, string Data_predoplaty, string surname, string phone, string AboutUs, string WhatRemont, string brand,
            string model, string SerialNumber, string sostoyanie, string komplektonst, string polomka, string kommentarij, string predvaritelnaya_stoimost, string Predoplata,
            string Zatrati, string okonchatelnaya_stoimost_remonta, string Skidka, string Status_remonta, string master, string vipolnenie_raboti, string Garanty,
            string wait_zakaz, string Adress, string Image_key, string AdressSC, string DeviceColour, string ClientId)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }
            // Добавляем штрихкод в формате UPC-A
            string barCODE = DateTime.Now.ToString("ddMMHHmmss");
            string lastDigOfYear = DateTime.Now.ToString("yyyy");
            lastDigOfYear = lastDigOfYear.Substring(3);
            barCODE += lastDigOfYear;
            barCODE = barcodeLastDigit(barCODE);
            string del = "";
            try
            {
                if (Data_priema != "")
                {
                    Data_priema = DateTime.Parse(Data_priema).ToString("dd-MM-yyyy HH:mm");
                }
                m_sqlCmd.CommandText = "INSERT INTO Catalog ('Data_priema', 'Data_vidachi','Data_predoplaty','surname','phone','AboutUs','WhatRemont','brand','model','SerialNumber','sostoyanie','komplektonst','polomka','kommentarij','predvaritelnaya_stoimost','Predoplata','Zatrati','okonchatelnaya_stoimost_remonta','Skidka','Status_remonta','master','vipolnenie_raboti','Garanty','wait_zakaz', 'Adress', 'Image_key', 'AdressSC', 'DeviceColour','ClientId','Barcode','Deleted') values ('" +
                           Data_priema + "' , '" +
                           Data_vidachi + "', '" +
                           Data_predoplaty + "', '" +
                           surname.Trim().ToUpper() + "', '" +
                           phone.Trim() + "', '" +
                           AboutUs.Trim() + "', '" +
                           WhatRemont.Trim().ToUpper() + "', '" +
                           brand.Trim().ToUpper() + "', '" +
                           model.Trim().ToUpper() + "', '" +
                           SerialNumber.Trim().ToUpper() + "', '" +
                           sostoyanie.Trim() + "', '" +
                           komplektonst.Trim() + "', '" +
                           polomka.Trim() + "', '" +
                           kommentarij.Trim() + "', '" +
                           predvaritelnaya_stoimost.Trim() + "', '" +
                           Predoplata.Trim() + "', '" +
                           Zatrati.Trim() + "', '" +
                           okonchatelnaya_stoimost_remonta.Trim() + "', '" +
                           Skidka + "', '" +
                           Status_remonta.Trim() + "', '" +
                           master.Trim().ToUpper() + "', '" +
                           vipolnenie_raboti.Trim() + "', '" +
                           Garanty.Trim() + "', '" +
                           wait_zakaz + "', '" +
                           Adress + "', '" +
                           Image_key + "', '" +
                           AdressSC + "', '" +
                           DeviceColour + "', '" +
                           ClientId + "', '" +
                           barCODE + "', '" +
                           del + "')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Изменение в базе данных
        public void BdEdit(string data_predoplaty, string data_vidachi, string surname, string phone, string AboutUs, string WhatRemont, string brand,
            string model, string SerialNumber, string sostoyanie, string komplektonst, string polomka, string kommentarij, string predvaritelnaya_stoimost,
            string Predoplata, string Zatrati, string okonchatelnaya_stoimost_remonta, string Skidka, string Status_remonta, string master, string vipolnenie_raboti,
            string Garanty, string wait_zakaz, string Adress, string Image_key, string id_bd, string AdressSC, string DeviceColour)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET surname ='" + surname.Trim().ToUpper()
                            + "',Data_predoplaty ='" + data_predoplaty
                            + "',Data_vidachi ='" + data_vidachi
                            + "',phone ='" + phone.Trim()
                            + "',AboutUs ='" + AboutUs.Trim()
                            + "',WhatRemont ='" + WhatRemont.ToUpper()
                            + "',brand ='" + brand.Trim().ToUpper()
                            + "',model ='" + model.Trim().ToUpper()
                            + "',SerialNumber ='" + SerialNumber.Trim().ToUpper()
                            + "',sostoyanie ='" + sostoyanie.Trim()
                            + "',komplektonst ='" + komplektonst.Trim()
                            + "',polomka ='" + polomka.Trim()
                            + "',kommentarij ='" + kommentarij.Trim()
                            + "',predvaritelnaya_stoimost ='" + predvaritelnaya_stoimost.Trim()
                            + "',Predoplata ='" + Predoplata.Trim()
                            + "',Zatrati ='" + Zatrati.Trim()
                            + "',okonchatelnaya_stoimost_remonta ='" + okonchatelnaya_stoimost_remonta.Trim()
                            + "',Skidka ='" + Skidka
                            + "',Status_remonta ='" + Status_remonta
                            + "',master ='" + master.Trim().ToUpper()
                            + "',vipolnenie_raboti ='" + vipolnenie_raboti.Trim()
                            + "',Garanty ='" + Garanty
                            + "',wait_zakaz ='" + wait_zakaz
                            + "',Adress ='" + Adress
                            + "',Image_key ='" + Image_key
                            + "',AdressSC ='" + AdressSC.ToUpper().Trim()
                            + "',DeviceColour ='" + DeviceColour.ToUpper().Trim()
                            + "' WHERE ID = " + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }


        //Изменение в базе данных
        public void BdEditVidachiPoGarantii(string data_predoplaty, string data_vidachi, string surname, string phone, string AboutUs, string WhatRemont, string brand,
            string model, string SerialNumber, string sostoyanie, string komplektonst, string polomka, string kommentarij, string predvaritelnaya_stoimost,
            string Predoplata, string Zatrati, string okonchatelnaya_stoimost_remonta, string Skidka, string Status_remonta, string master, string vipolnenie_raboti,
            string Garanty, string wait_zakaz, string Adress, string Image_key, string id_bd, string AdressSC, string DeviceColour)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET surname ='" + surname.Trim().ToUpper()
                            + "',Data_predoplaty ='" + data_predoplaty
                            + "',phone ='" + phone.Trim()
                            + "',AboutUs ='" + AboutUs.Trim()
                            + "',WhatRemont ='" + WhatRemont.ToUpper()
                            + "',brand ='" + brand.Trim().ToUpper()
                            + "',model ='" + model.Trim().ToUpper()
                            + "',SerialNumber ='" + SerialNumber.Trim().ToUpper()
                            + "',sostoyanie ='" + sostoyanie.Trim()
                            + "',komplektonst ='" + komplektonst.Trim()
                            + "',polomka ='" + polomka.Trim()
                            + "',kommentarij ='" + kommentarij.Trim()
                            + "',predvaritelnaya_stoimost ='" + predvaritelnaya_stoimost.Trim()
                            + "',Predoplata ='" + Predoplata.Trim()
                            + "',Zatrati ='" + Zatrati.Trim()
                            + "',okonchatelnaya_stoimost_remonta ='" + okonchatelnaya_stoimost_remonta.Trim()
                            + "',Skidka ='" + Skidka
                            + "',Status_remonta ='" + Status_remonta
                            + "',master ='" + master.Trim().ToUpper()
                            + "',vipolnenie_raboti ='" + vipolnenie_raboti.Trim()
                            + "',Garanty ='" + Garanty
                            + "',wait_zakaz ='" + wait_zakaz
                            + "',Adress ='" + Adress
                            + "',Image_key ='" + Image_key
                            + "',AdressSC ='" + AdressSC.ToUpper().Trim()
                            + "',DeviceColour ='" + DeviceColour.ToUpper().Trim()
                            + "' WHERE ID = " + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Изменение в базе данных
        public void BdEditOne(string EditWhat, string EditThis, string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET " + EditWhat + " ='" + EditThis + "' WHERE ID = " + id_bd;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        //Изменение в базе данных Убираем NULL
        public void BdNoNull(string WhatToDo)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET " + WhatToDo + " ='" + "" + "' WHERE " + WhatToDo + " is null";
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Изменение в базе данных Убираем NULL
        public void BdNoNullRename(string WhatToDo, string DoThis, string WithThis)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Catalog SET " + WhatToDo + " ='" + DoThis.Trim() + "' WHERE " + WhatToDo + "='" + WithThis + "'";
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        //Удаление из бд
        //Изменение в базе данных
        public void BdDelete(string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM Catalog WHERE id =" + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Поиск по базе
        //Поиск по ФИО
        public DataTable SearchFIO(string FIO, bool Chek)
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            String sqlQuery = "";
            DataTable dt = new DataTable();
            try
            {
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                    return dt;
                }

                if (Chek) // Сортировка по дате выдачи ORDER BY
                    sqlQuery = String.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.Data_Vidachi != '' AND cm.FIO LIKE '%{0}%'", FIO.ToUpper());
                if (!Chek)
                    sqlQuery = String.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.Data_Vidachi = ''AND cm.FIO LIKE '%{0}%'", FIO.ToUpper());

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так при проведении поиска" + Environment.NewLine + ex.ToString());
            }


            return dt;
        }

        // Полный поиск по базе
        public DataTable BdReadFullSearch(string FIO, string phone, string TypeOf, string brand, string model, string status, string master, string zakaz, bool trfl, string idInBd, string serialImei, string AdressSC, string garanty = "", string soglasovat = "")
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try // id like 9, id == 9 вот чем строки отличаются
            {
                if (garanty == "garanty")
                {
                    if (idInBd != "")
                    {
                        sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%'  AND c.Status_remonta LIKE'%{5}%' AND c.master LIKE'%{6}%' AND c.wait_zakaz LIKE'%{7}%' AND c.Image_key LIKE '%{8}%' AND c.id == '{9}' AND c.SerialNumber LIKE '%{10}%' AND c.AdressSC LIKE '%{11}%' AND c.Deleted != '1'",
                                               FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), status, master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                        adapter.Fill(dTable);
                    }
                    else
                    {
                        sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%'  AND c.Status_remonta LIKE'%{5}%' AND c.master LIKE'%{6}%' AND c.wait_zakaz LIKE'%{7}%' AND c.Image_key LIKE '%{8}%' AND c.id LIKE '%{9}%' AND c.SerialNumber LIKE '%{10}%' AND c.AdressSC LIKE '%{11}%' AND c.Deleted != '1'",
                                                 FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), status.Trim(), master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                        adapter.Fill(dTable);
                    }

                    return dTable;
                }
                else
                {
                    //bool Для сортировки с датой выдачи и без (true = выдан)
                    if (!trfl)
                    {
                        if (idInBd != "")
                        {
                            sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.Data_vidachi != '{5}' AND c.Status_remonta LIKE'%{6}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id == '{10}' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Deleted != '1'",
                            FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), "", status, master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                            adapter.Fill(dTable);
                        }
                        else
                        {
                            sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.Data_vidachi != '{5}' AND c.Status_remonta LIKE'%{6}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id LIKE '%{10}%' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Deleted != '1'",
                                                        FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), "", status, master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                            adapter.Fill(dTable);
                        }
                        return dTable;
                    }
                    else
                    {
                        if (idInBd != "")
                        {
                            sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.Data_vidachi != '{5}' AND c.Status_remonta LIKE'%{6}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id == '{10}' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Deleted != '1'",
                            FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), "", status, master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                            adapter.Fill(dTable);
                        }
                        else
                        {
                            sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.Data_vidachi == '{5}' AND c.Status_remonta LIKE'%{6}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id LIKE '%{10}%' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Status_remonta != 'Выдан' AND c.Deleted != '1' OR (c.Status_remonta == 'Принят по гарантии' AND cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id LIKE '%{10}%' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Deleted != '1')",
                            FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), "", status, master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                            adapter.Fill(dTable);
                        }
                        return dTable;
                    }
                }


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        //Перегрузка для поиска в выданном
        public DataTable BdReadFullSearch(string FIO, string phone, string TypeOf, string brand, string model, string status, string master, string zakaz, bool trfl, string idInBd, string serialImei, string AdressSC, string garanty = "", string soglasovat = "", bool vidannoe = false)
        {
            //bl для того, чтобы отработала эта перегрузка
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try // id like 9, id == 9 вот чем строки отличаются
            {

                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE cm.FIO LIKE'%{0}%' AND c.brand LIKE'%{1}%' AND cm.Phone LIKE'%{2}%' AND c.WhatRemont LIKE'%{3}%' AND c.model LIKE'%{4}%' AND c.Data_vidachi != '{5}' AND c.Status_remonta LIKE'%{6}%' AND c.master LIKE'%{7}%' AND c.wait_zakaz LIKE'%{8}%' AND c.Image_key LIKE '%{9}%' AND c.id LIKE '%{10}%' AND c.SerialNumber LIKE '%{11}%' AND c.AdressSC LIKE '%{12}%' AND c.Deleted != '1'",
                                                       FIO.ToUpper().Trim(), brand.ToUpper().Trim(), phone.Trim(), TypeOf.ToUpper().Trim(), model.ToUpper().Trim(), "", "", master.ToUpper().Trim(), zakaz, soglasovat, idInBd, serialImei.ToUpper(), AdressSC.ToUpper());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        //Поиск тех, кому нужно отзвониться
        public DataTable BdSearchPhoneWaiting()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT c.id,c.Data_priema,c.Data_vidachi,c.Data_predoplaty,cm.FIO,cm.Phone,cm.AboutUs,c.WhatRemont,c.brand,c.model,c.SerialNumber,c.Sostoyanie,c.komplektonst,c.polomka,c.kommentarij,c.predvaritelnaya_stoimost,c.Predoplata,c.Zatrati, c.okonchatelnaya_stoimost_remonta,c.Skidka,c.Status_remonta,c.master,c.vipolnenie_raboti,c.Garanty,c.wait_zakaz,cm.Adress,c.Image_key, c.AdressSC, c.DeviceColour, c.ClientId,c.Barcode FROM Catalog c JOIN ClientsMap cm ON c.clientID = cm.id WHERE c.Image_key == '{0}'", "1");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

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

        //Корректировка дат в верный формат










        //////////////////////////////////////////////////////// СКЛАД /////////////////////////////////////////////////////////////////////////
        //Создание таблицы для склада
        public void CreateStock()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Stock (id INTEGER PRIMARY KEY AUTOINCREMENT, Naimenovanie TEXT, Kategoriya TEXT, Podkategoriya TEXT, Colour TEXT, Brand TEXT, Model TEXT,CountOf TEXT, Price TEXT, Napominanie TEXT, Photo TEXT, Primechanie TEXT, Photo2 TEXT, Photo3 TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }
        // Создание таблицы со значениями использованных деталий и записей, в которых они были использованы

        public void CreateStockMap()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS StockMap (id INTEGER PRIMARY KEY AUTOINCREMENT, clientId TEXT,ZIPId TEXT ,countOfZIP TEXT, priceOfZIP TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Запись в базу данных
        public void BdStockWrite(string Naimenovanie, string Kategoriya, string Podkategoriya, string Colour, string Brand, string Model, string CountOf, string Napominanie, string Price, string Primechanie, string Photo = "", string Photo2 = "", string Photo3 = "")
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "INSERT INTO Stock ('Naimenovanie', 'Kategoriya','Podkategoriya','Colour','Brand','Model','CountOf','Napominanie','Price','Photo','Primechanie','Photo2','Photo3') values ('" +
                           Naimenovanie.Trim().ToUpper() + "' , '" +
                           Kategoriya.Trim().ToUpper() + "', '" +
                           Podkategoriya.Trim().ToUpper() + "', '" +
                           Colour.Trim().ToUpper() + "', '" +
                           Brand.Trim().ToUpper() + "', '" +
                           Model.Trim().ToUpper() + "', '" +
                           CountOf.Trim().ToUpper() + "', '" +
                           Napominanie.Trim().ToUpper() + "','" +
                           Price.Trim().ToUpper() + "','" +
                           Photo.Trim() + "','" +
                           Primechanie.Trim().ToUpper() + "','" +
                           Photo2.Trim() + "','" +
                           Photo3.Trim() + "')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Изменение в базе данных
        public void BdStockEdit(string Naimenovanie, string Kategoriya, string Podkategoriya, string Colour, string Brand, string Model, string CountOf, string Napominanie, string Price, string Photo, string id_bd, string Primechanie, string Photo2, string Photo3)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }
            try
            {
                m_sqlCmd.CommandText = "UPDATE Stock SET Naimenovanie ='" + Naimenovanie.Trim().ToUpper()
                            + "',Kategoriya ='" + Kategoriya.Trim().ToUpper()
                            + "',Podkategoriya ='" + Podkategoriya.Trim().ToUpper()
                            + "',Colour ='" + Colour.Trim().ToUpper()
                            + "',Brand ='" + Brand.Trim().ToUpper()
                            + "',Model ='" + Model.Trim().ToUpper()
                            + "',CountOf ='" + CountOf.Trim().ToUpper()
                            + "',Napominanie ='" + Napominanie.Trim().ToUpper()
                            + "',Price ='" + Price.Trim().ToUpper()
                            + "',Photo ='" + Photo.Trim()
                            + "',Primechanie ='" + Primechanie.Trim().ToUpper()
                            + "',Photo2 ='" + Photo2.Trim()
                            + "',Photo3 ='" + Photo3.Trim()
                            + "' WHERE id = " + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        //Изменение в базе данных
        public void BdStockEditOne(string EditWhat, string EditThis, string idOfZIP)

        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE Stock SET " + EditWhat + " ='" + EditThis + "' WHERE ID = " + idOfZIP;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }

        // Полный поиск по базе
        public DataTable BdStockFullSearch(string Naimenovanie, string Kategoriya, string Podkategoriya, string Colour, string Brand, string Model, string CountOf, string Napominanie)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM Stock WHERE Naimenovanie LIKE'%{0}%' AND Kategoriya LIKE'%{1}%' AND Podkategoriya LIKE'%{2}%' AND Colour LIKE'%{3}%' AND Brand LIKE'%{4}%'  AND Model LIKE'%{5}%'",
                                          Naimenovanie.ToUpper().Trim(), Kategoriya.ToUpper().Trim(), Podkategoriya.Trim().ToUpper(), Colour.ToUpper().Trim(), Brand.ToUpper().Trim(), Model.ToUpper().Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        // StockEditor
        public DataTable BdStockEditor(string id)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM Stock WHERE id ={0}", id.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        //Удаление из бд
        public void BdStockDelete(string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM Stock WHERE id =" + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Запись в базу Map данных  clientId TEXT,ZIPId TEXT ,countOfZIP TEXT
        public void BdStockMapWrite(string clientId, string ZIPId, string countOfZIP, string priceOfZIP)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "INSERT INTO StockMap ('clientId', 'ZIPId','countOfZIP','priceOfZIP') values ('" +
                           clientId.Trim() + "' , '" +
                           ZIPId.Trim() + "', '" +
                           countOfZIP.Trim() + "', '" +
                           priceOfZIP.Trim() + "')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Удаление stock map, всех записей о клиенте из бд
        public void BdStockMapDelete(string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM StockMap WHERE clientId =" + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        // Отмена затрат
        public DataTable BdStockMapZIPDeleteCounter(string idClient, string idZIP)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM StockMap WHERE clientId = '{0}' AND ZIPId = '{1}'", idClient.Trim(), idZIP.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        // Удаление по критериям из StockMap
        public void BdStockMapDeleteZIP(string id_bd, string idZIP)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM StockMap WHERE clientId =" + id_bd + " AND ZIPId =" + idZIP;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        // Проверка на наличие использованных запчастей
        public bool BdStockMapZIPUsedCheck(string id_bd, string idZIP)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return false;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM StockMap WHERE clientId = '{0}' AND ZIPId = '{1}'", id_bd.Trim(), idZIP.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return true;
                }
                else return false;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
                return false;
            }

        }

        // Проверка на наличие использованных запчастей оптимизация
        public DataTable BdStockMapZIPUsedCheckOptimised()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM StockMap");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
                return dTable;
            }
        }

        // Проверка на наличие использованных запчастей
        public string BdStockMapZIPUsedCoutner(string id_bd, string idZIP)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "0";
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM StockMap WHERE clientId = '{0}' AND ZIPId = '{1}'", id_bd.Trim(), idZIP.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                int zipCount = 0;
                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        zipCount += int.Parse(dTable.Rows[i].ItemArray[3].ToString());
                    }
                    return zipCount.ToString();
                }
                else return "0";

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
                return "0";
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////-------------------------------Статусы
        // Создание таблицы

        public void StatesMapTable_Create()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS StatesMap (id INTEGER PRIMARY KEY AUTOINCREMENT, clientId TEXT, State TEXT ,date TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Удаление из бд
        public void StatesMapDelete(string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM StatesMap WHERE id =" + id_bd;

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Запись в базу Map данных  
        public void StatesMapWrite(string clientId, string state, string date)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "INSERT INTO StatesMap ('clientId', 'State','date') values ('" +
                           clientId.Trim() + "' , '" +
                           state.Trim() + "', '" +
                           date.Trim() + "')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        // Получить все статусы записи
        public DataTable StatesMapGiver(string idClient)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM StatesMap WHERE clientId = '{0}'", idClient.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        //Изменение в базе данных
        public void StatesMapEdit(string EditWhat, string EditThis, string id_map_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE StatesMap SET " + EditWhat + " ='" + EditThis + "' WHERE ID = " + id_map_bd;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////-------------------------------Клиенты
        // Создание таблицы

        public void ClientsMapTable_Create()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS ClientsMap (id INTEGER PRIMARY KEY AUTOINCREMENT, FIO TEXT, Phone TEXT, Adress Text, Primechanie TEXT, Blist TEXT ,date TEXT, aboutUs TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Удаление из бд
        public void ClientsMapDelete(string id_client)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "DELETE FROM ClientsMap WHERE id =" + id_client;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        //Удаление из бд записей клиента
        public void ClientsMapZapisiDelete(string id_client)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = String.Format("DELETE FROM Catalog WHERE ClientId = '{0}'", id_client);
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        //Запись в базу Map данных  
        public void ClientsMapWrite(string FIO, string Phone, string Adress, string Primechanie, string Blist, string Date, string aboutUs)
        {

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "INSERT INTO ClientsMap ('FIO', 'Phone','Adress','Primechanie','Blist','Date','aboutUs') values ('" +
                           FIO.Trim().ToUpper() + "' , '" +
                           Phone.Trim() + "', '" +
                           Adress.Trim() + "', '" +
                           Primechanie.Trim() + "', '" +
                           Blist.Trim() + "', '" +
                           Date.Trim() + "', '" +
                           aboutUs.Trim() + "')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

        // Получить данные о клиенте
        public DataTable ClientsMapGiver(string idClient)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM ClientsMap WHERE id = '{0}'", idClient.Trim());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Получить данные о клиентах
        public DataTable ClientsAllMapGiver()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM ClientsMap");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Поиск по фамилии и номеру
        public DataTable ClientsFIOPhoneSearch(string fio, string phone)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT * FROM ClientsMap WHERE FIO LIKE '%{0}%' AND Phone LIKE'%{1}%'", fio.Trim().ToUpper(), phone.Trim().Replace(" ", ""));
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        //Изменение в базе данных
        public void ClientsMapEditOne(string EditWhat, string EditThis, string id_map_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE ClientsMap SET " + EditWhat + " ='" + EditThis + "' WHERE ID = " + id_map_bd;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Изменение в базе данных, объединение клиентов
        public void ClientsToClitens(string firstClient, string secondClient)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = String.Format("UPDATE Catalog SET ClientId= '{1}' WHERE ClientId == '{0}'", firstClient, secondClient);
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }
        //Изменение в базе данных
        public void ClientsMapEditAll(string FIO, string Phone, string Adress, string Primechanie, string Blist, string date, string aboutUs, string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "UPDATE ClientsMap SET FIO ='" + FIO.Trim().ToUpper()
                             + "',Phone ='" + Phone.Trim()
                             + "',Adress ='" + Adress.Trim()
                             + "',Primechanie ='" + Primechanie.Trim()
                             + "',Blist ='" + Blist.Trim()
                             + "',date ='" + date.Trim()
                             + "',aboutUs ='" + aboutUs.Trim()
                             + "' WHERE id = " + id_bd;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }
        //Изменение в базе данных
        public void ClientsMapEditWithoutDate(string FIO, string Phone, string Adress, string Primechanie, string Blist, string aboutUs, string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "UPDATE ClientsMap SET FIO ='" + FIO.Trim().ToUpper()
                             + "',Phone ='" + Phone.Trim()
                             + "',Adress ='" + Adress.Trim()
                             + "',Primechanie ='" + Primechanie.Trim()
                             + "',Blist ='" + Blist.Trim()
                             + "',aboutUs ='" + aboutUs.Trim()
                             + "' WHERE id = " + id_bd;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }
        //Изменение в базе данных
        public void ClientsMapEditInEditor(string FIO, string Phone, string Adress, string aboutUs, string id_bd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "UPDATE ClientsMap SET FIO ='" + FIO.Trim().ToUpper()
                             + "',Phone ='" + Phone.Trim()
                             + "',Adress ='" + Adress.Trim()
                             + "',aboutUs ='" + aboutUs.Trim()
                             + "' WHERE id = " + id_bd;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }
        // Чтение из базы данных
        public string ClientReadId(string FIO, string Phone)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "";
            }
            try
            {

                sqlQuery = String.Format("SELECT id FROM ClientsMap WHERE FIO = '{0}' AND Phone = '{1}'", FIO.Trim().ToUpper(), Phone.Trim().Replace(" ", ""));
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "";

        }
        // Чтение из базы данных
        public string ClientReadDate(string id)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "";
            }
            try
            {

                sqlQuery = String.Format("SELECT date FROM ClientsMap WHERE id = '{0}'", id);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "";

        }

        //Перегрузка функции для подгрузки фамилий
        public AutoCompleteStringCollection AddCollectionFIO()
        {
            AutoCompleteStringCollection SurnameAutoColl = new AutoCompleteStringCollection();

            try
            {
                DataTable dTable = new DataTable();
                String sqlQuery;

                m_dbConn = new SQLiteConnection();
                m_sqlCmd = new SQLiteCommand();

                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                }

                sqlQuery = string.Format("SELECT FIO FROM ClientsMap");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //Последующие циклы и bool нужны, чтобы не дублировались записи при подгрузке значинй в коллекцию surname
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        SurnameAutoColl.Add(FirstLetterToUpper(dTable.Rows[i].ItemArray[0].ToString()));
                    }
                    return SurnameAutoColl;
                }

                return SurnameAutoColl;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return SurnameAutoColl;
        }

        // Поиск по базы данных ФИО (AddPos)
        public DataTable BdReadFIOPhone(string fio)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {
                sqlQuery = string.Format("SELECT FIO, Phone, Adress, Primechanie, Blist, Date, aboutUs  FROM ClientsMap WHERE FIO = '{0}'", fio.ToUpper());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;



            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        // Поиск по базе, просмотр записей для клиента
        public DataTable ClientsShowHistory(string clientId)
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            String sqlQuery = "";
            DataTable dt = new DataTable();
            try
            {
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                    return dt;
                }
                sqlQuery = string.Format("SELECT * FROM Catalog WHERE ClientId = '{0}'", clientId.Trim());

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так при проведении поиска" + Environment.NewLine + ex.ToString());
            }


            return dt;
        }
        // Чтение из базы данных
        public string ClientsReadOne(string readWhat, string Clientid)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            string ReadData = "";

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return ReadData;
            }
            try
            {
                sqlQuery = "SELECT " + readWhat + " FROM ClientsMap Where id =" + Clientid;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                else
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу прочитать даты записи номер " + Clientid + Environment.NewLine);

                return ReadData;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при получении записи из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return ReadData;

        }


        //Поиск по ФИО
        public DataTable ClientsSearchFIO(string FIO)
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            String sqlQuery = "";
            DataTable dt = new DataTable();
            try
            {
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                    return dt;
                }

                sqlQuery = string.Format("SELECT * FROM ClientsMap WHERE FIO LIKE'%{0}%'", FIO.ToUpper());

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так при проведении поиска" + Environment.NewLine + ex.ToString());
            }


            return dt;
        }
        // ШТРИХКОД
        public string barcodeLastDigit(string barcode11)
        {
            string RawDataHolder = barcode11.Substring(0, 11);
            string barcode12 = "";
            //calculate check digit
            int even = 0;
            int odd = 0;

            for (int i = 0; i < RawDataHolder.Length; i++)
            {
                if (i % 2 == 0)
                    odd += Int32.Parse(RawDataHolder.Substring(i, 1)) * 3;
                else
                    even += Int32.Parse(RawDataHolder.Substring(i, 1));
            }//for

            int total = even + odd;
            int cs = total % 10;
            cs = 10 - cs;
            if (cs == 10)
                cs = 0;

            barcode12 = RawDataHolder + cs.ToString()[0];
            return barcode12;
        }

        public void bdBarcodeAllGenerator()
        {
            DataTable dt1 = mainForm.basa.BdReadAll();
            List<VirtualClient> vc1List = new List<VirtualClient>();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    vc1List.Add(new VirtualClient(dt1.Rows[i].ItemArray[0].ToString(), dt1.Rows[i].ItemArray[1].ToString(), dt1.Rows[i].ItemArray[2].ToString(), dt1.Rows[i].ItemArray[3].ToString(),
                   dt1.Rows[i].ItemArray[4].ToString(), dt1.Rows[i].ItemArray[5].ToString(), dt1.Rows[i].ItemArray[6].ToString(), dt1.Rows[i].ItemArray[7].ToString(), dt1.Rows[i].ItemArray[8].ToString(),
                   dt1.Rows[i].ItemArray[9].ToString(), dt1.Rows[i].ItemArray[10].ToString(), dt1.Rows[i].ItemArray[11].ToString(), dt1.Rows[i].ItemArray[12].ToString(), dt1.Rows[i].ItemArray[13].ToString(),
                   dt1.Rows[i].ItemArray[14].ToString(), dt1.Rows[i].ItemArray[15].ToString(), dt1.Rows[i].ItemArray[16].ToString(), dt1.Rows[i].ItemArray[17].ToString(),
                   dt1.Rows[i].ItemArray[18].ToString(), dt1.Rows[i].ItemArray[19].ToString(), dt1.Rows[i].ItemArray[20].ToString(), dt1.Rows[i].ItemArray[21].ToString(), dt1.Rows[i].ItemArray[22].ToString(),
                   dt1.Rows[i].ItemArray[23].ToString(), dt1.Rows[i].ItemArray[24].ToString(), dt1.Rows[i].ItemArray[25].ToString(), dt1.Rows[i].ItemArray[26].ToString(), true, dt1.Rows[i].ItemArray[27].ToString(), dt1.Rows[i].ItemArray[28].ToString(), -1, dt1.Rows[i].ItemArray[30].ToString()));
                }

            }


            //Begin Loop
            m_sqlCmd = new SQLiteCommand("begin", m_sqlCmd.Connection);
            m_sqlCmd.ExecuteNonQuery();
            // Записываем номера клиентов к номеру записи

            Random random1 = new Random();
            Random random2 = new Random();
            Random random3 = new Random();

            foreach (VirtualClient vc1 in vc1List)
            {

                if (vc1.Barcode == "")
                {
                    if (m_dbConn.State != ConnectionState.Open)
                    {
                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                        return;
                    }

                    try
                    {
                        string bcode = "";
                        string bcode1 = random1.Next(1111, 9999).ToString();
                        string bcode2 = random2.Next(1111, 9999).ToString();
                        string bcode3 = random3.Next(333, 999).ToString();
                        bcode = bcode1 + bcode2 + bcode3;
                        bcode = barcodeLastDigit(bcode);
                        m_sqlCmd.CommandText = String.Format("UPDATE Catalog SET Barcode = '{0}' WHERE ID = {1}", bcode, vc1.Id);
                        m_sqlCmd.ExecuteNonQuery();

                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
                    }
                }

            }
            //---END LOOP
            m_sqlCmd = new SQLiteCommand("end", m_sqlCmd.Connection);
            m_sqlCmd.ExecuteNonQuery();
        }
        // ------------------------------------------------------------------------------------Создаём таблицу истории
        public void HistoryBDTable_Create()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS HistoryBD(id INTEGER PRIMARY KEY AUTOINCREMENT, WHO TEXT, WHAT TEXT, FULLWHAT TEXT, DATA TEXT, IDINCATALOG TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Поиск по ФИО и остальным параметрам
        public DataTable HISTORYSearchFIO(string FIO = "", string Date = "", string WHAT_HistoryBD = "", string WHO_HistoryBD = "")
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            String sqlQuery = "";
            DataTable dt = new DataTable();
            try
            {
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                    return dt;
                }

                sqlQuery = string.Format("SELECT hbd.*,clmap.FIO, cat.* FROM Catalog cat JOIN ClientsMap clmap ON cat.clientID = clmap.id JOIN HistoryBD hbd ON cat.id = hbd.IDINCATALOG WHERE hbd.WHO LIKE'%{0}%' AND hbd.WHO LIKE'%{1}%' AND hbd.WHAT LIKE'%{2}%' AND hbd.DATA LIKE'%{3}%'", FIO.ToUpper().Trim(), WHO_HistoryBD.ToUpper(), WHAT_HistoryBD.ToUpper(), Date);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так при проведении поиска" + Environment.NewLine + ex.ToString());
            }


            return dt;
        }

        public void HistoryBDWrite(string WHO, string WHAT, string FULLWHAT, string IDINCATALOG, string DATA = "")
        {

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            //id INTEGER PRIMARY KEY AUTOINCREMENT, WHO TEXT, WHAT TEXT, FULLWHAT TEXT, DATA TEXT, IDINCATALOG TEXT)";
            try
            {
                string data = (DATA == "") ? DateTime.Now.ToString("dd-MM-yyyy HH:mm") : DATA;
                m_sqlCmd.CommandText = "INSERT INTO HistoryBD ('WHO', 'WHAT','FULLWHAT','IDINCATALOG','DATA') values ('" +
                           WHO.Trim().ToUpper() + "' , '" +
                           WHAT.Trim().ToUpper() + "', '" +
                           FULLWHAT.Trim() + "', '" +
                           IDINCATALOG.Trim() + "', '" +
                           data + "')";


                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        // ------------------------------------------------------------------------------- база пользователей (управление)
        public void UsersTable_Create()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Users(id INTEGER PRIMARY KEY AUTOINCREMENT, type TEXT, name TEXT, id_gruppi_dostupa, user_pwd TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        public void UsersBDWrite(string type, string name, string id_gruppi_dostupa, string user_pwd)
        {

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "INSERT INTO Users ('type', 'name','id_gruppi_dostupa','user_pwd') values ('" +
                           type.Trim() + "' , '" +
                           name.Trim() + "', '" +
                           id_gruppi_dostupa + "', '" +
                           user_pwd + "')";


                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }

        }
        //Изменение в базе данных
        public void UsersBdEditPassword(string EditWhat, string EditThis, string name)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = string.Format("UPDATE Users SET " + EditWhat + " ='" + EditThis.Trim() + "' WHERE name = '{0}'", name);
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }

        // Чтение из базы данных
        public DataTable UsersBdRead()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = "SELECT * FROM users";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Чтение из базы данных
        public DataTable UsersBdRead(string name)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = string.Format("SELECT * FROM users WHERE name = '{0}'", name);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        //Удаление из бд записей клиента
        public void UserDelete(string userName)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = String.Format("DELETE FROM users WHERE name = '{0}'", userName);
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        public void UserBdEditAll(string type, string name, string id_gruppi_dostupa)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "UPDATE users SET type ='" + type
                          + "',name ='" + name
                          + "',id_gruppi_dostupa ='" + id_gruppi_dostupa
                          + "' WHERE name = '" + name + "'";
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        // Чтение из базы данных
        public string UsersGetPass(string name)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "-1";
            }
            try
            {

                sqlQuery = string.Format("SELECT user_pwd FROM users WHERE name == '{0}'", name);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                return "-1";
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "-1";

        }
        // Чтение из базы данных
        public string UsersGetGroupIdByUserName(string name)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "-1";
            }
            try
            {

                sqlQuery = string.Format("SELECT id_gruppi_dostupa FROM users WHERE name == '{0}'", name);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                return "-1";
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "-1";

        }
        // ------------------------------------------------------------------------------- база групп доступа (управление)
        public void GroupDostupTable_Create()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();


            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS GroupDostup(id INTEGER PRIMARY KEY AUTOINCREMENT, grName TEXT, delZapis TEXT, addZapis  TEXT, saveZapis TEXT, graf TEXT, sms TEXT, stock TEXT, clients TEXT, stockAdd TEXT, stockDel TEXT, stockEdit TEXT, clientAdd TEXT, clientDel TEXT, clientConcat TEXT, settings TEXT, dates TEXT, editDates TEXT)";
                m_sqlCmd.ExecuteNonQuery();


            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Не могу установить соединение с базой данных " + ex.ToString() + Environment.NewLine);
            }
        }

        public void GroupDostupBDWrite(string grName, string delZapis, string addZapis, string saveZapis, string graf, string sms, string stock, string clients,
            string stockAdd, string stockDel, string stockEdit, string clientAdd, string clientDel, string clientConcat, string settings, string dates, string editDates)
        {

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "INSERT INTO GroupDostup ('grName', 'delZapis','addZapis','saveZapis','graf','sms','stock','clients','stockAdd','stockDel','stockEdit','clientAdd','clientDel','clientConcat','settings', 'dates', 'editDates') values ('" +
                           grName + "' , '" +
                           delZapis + "', '" +
                           addZapis + "', '" +
                           saveZapis + "', '" +
                           graf + "', '" +
                           sms + "', '" +
                           stock + "', '" +
                           clients + "', '" +
                           stockAdd + "', '" +
                           stockDel + "', '" +
                           stockEdit + "', '" +
                           clientAdd + "', '" +
                           clientDel + "', '" +
                           clientConcat + "', '" +
                           settings + "', '" +
                           dates + "', '" +
                           editDates + "')";


                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }
        }

        //Изменение в базе данных
        public void GroupDostupBdEditOne(string EditWhat, string EditThis, string id_in_Usersbd)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {

                m_sqlCmd.CommandText = "UPDATE GroupDostup SET " + EditWhat + " ='" + EditThis + "' WHERE ID = " + id_in_Usersbd;
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        //Изменение в базе данных
        public void GroupDostupBdEditAll(string grName, string delZapis, string addZapis, string saveZapis, string graf, string sms, string stock, string clients,
            string stockAdd, string stockDel, string stockEdit, string clientAdd, string clientDel, string clientConcat, string settings, string dates, string editDates)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = "UPDATE GroupDostup SET delZapis ='" + delZapis
                          + "',addZapis ='" + addZapis
                          + "',saveZapis ='" + saveZapis
                          + "',graf ='" + graf
                          + "',sms ='" + sms
                          + "',stock ='" + stock
                          + "',clients ='" + clients
                          + "',stockAdd ='" + stockAdd
                          + "',stockDel ='" + stockDel
                          + "',stockEdit ='" + stockEdit
                          + "',clientAdd ='" + clientAdd
                          + "',clientDel ='" + clientDel
                          + "',clientConcat ='" + clientConcat
                          + "',settings ='" + settings
                          + "',dates ='" + dates
                          + "',editDates ='" + editDates
                          + "' WHERE grName = '" + grName + "'";
                m_sqlCmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }
        // Чтение из базы данных
        public DataTable GroupDostupBdRead()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = "SELECT * FROM GroupDostup";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }
        // Чтение из базы данных
        public DataTable GroupDostupBdRead(string grName)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return dTable;
            }
            try
            {

                sqlQuery = string.Format("SELECT * FROM GroupDostup WHERE grName == '{0}'", grName);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                return dTable;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return dTable;

        }

        // Чтение из базы данных
        public string GroupDostupGetIdByGrNameBdRead(string grName)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "-1";
            }
            try
            {

                sqlQuery = string.Format("SELECT id FROM GroupDostup WHERE grName == '{0}'", grName);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                return "-1";
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "-1";

        }
        // Чтение из базы данных
        public string GroupDostupGetgrNameByIdBdRead(string id)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return "-1";
            }
            try
            {

                sqlQuery = string.Format("SELECT grName FROM GroupDostup WHERE id == '{0}'", id);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);
                if (dTable.Rows.Count > 0)
                {
                    return dTable.Rows[0].ItemArray[0].ToString();
                }
                return "-1";
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при чтении из базы данных " + ex.ToString() + Environment.NewLine);
            }
            return "-1";

        }
        //Удаление из бд записей клиента
        public void GroupDostupDelete(string grName)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Соединение с базой данных потеряно, установите соединение" + Environment.NewLine);
                return;
            }


            try
            {
                m_sqlCmd.CommandText = String.Format("DELETE FROM GroupDostup WHERE grName = '{0}'", grName);
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(DateTime.Now.ToShortTimeString() + ": Что-то пошло не так, при записи в базу данных " + ex.ToString() + Environment.NewLine);
            }


        }

    }


}
