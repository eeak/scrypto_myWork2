using System;
using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class Registration : Form
    {
        IniFile INIF = new IniFile("Config.ini");
        Form1 mform;
        public Registration(Form1 mf)
        {
            InitializeComponent();
            TopMost = true;
            mform = mf;
        }



        public static string getHDD()
        {
            string crpt = Crypt(GetModelFromDrive(Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1))).Replace("[", "");
            crpt = crpt.Replace("]", "");
            crpt = crpt.Replace("=", "");
            return crpt;
        }
        public static string GetModelFromDrive(string driveLetter)
        {

            if (driveLetter.Length != 1)
                return "ZALUPE";
            else driveLetter = driveLetter + ":";

            try
            {
                using (var partitions = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + driveLetter +
                                                 "'} WHERE ResultClass=Win32_DiskPartition"))
                {
                    foreach (var partition in partitions.Get())
                    {
                        using (var drives = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" +
                                                             partition["DeviceID"] +
                                                             "'} WHERE ResultClass=Win32_DiskDrive"))
                        {
                            foreach (var drive in drives.Get())
                            {
                                string serial = "NULE";
                                string model = "AHULE";
                                try { serial = (string)drive["SerialNumber"]; } catch { }
                                try { model = (string)drive["Model"]; } catch { }

                                return (serial + model).Trim().Replace(" ", "");
                            }
                        }
                    }
                }
            }
            catch
            {
                return "<unknown>";
            }

            // Not Found
            return "<unknown>";
        }
        public static string Crypt(string text)
        {
            string rtnStr = string.Empty;
            foreach (char c in text) // Цикл, которым мы и криптуем "текст"
            {
                rtnStr += (char)((int)c ^ 1); //Число можно взять любое.
            }
            return rtnStr; //Возвращаем уже закриптованную строку. 
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        // Verify a hash against a string.
        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string sha1(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }


        public static bool deHash(string pass, string val)
        {
            MD5 md5h = MD5.Create();
            string salt = "antivzlom89";
            string hash = sha1(sha1(Registration.GetMd5Hash(md5h, val) + salt));
            return (pass == hash);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://vk.com/scrypto");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (deHash(textBox2.Text, textBox1.Text))
            {
                mform.Enabled = true;
                // Пишем ключи активации в ини файл, чтобы потом не любить мозги пользователям при обновлении
                INIF.WriteINI("ACTIVATION", textBox1.Text, textBox2.Text);
                this.Close();
            }

            else
            {
                MessageBox.Show("Не верный ключ авторизации");
            }
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!deHash(textBox2.Text, textBox1.Text))
            {
                mform.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://vk.com/scrypto");
        }

        private void Registration_Load_1(object sender, EventArgs e)
        {

            textBox1.Text = TemporaryBase.UserKey;

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void Registration_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (mform.Enabled == false)
                Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
