using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Netcode.Security.GOST_28147_89;

namespace WindowsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string original = "This is a much longer string of data than a public/private key algorithm will accept.";
            //string roundtrip;
            //ASCIIEncoding textConverter = new ASCIIEncoding();
            GostManaged myGost = new GostManaged();
            byte[] fromEncrypt;
            byte[] encrypted;
            byte[] toEncrypt;
            byte[] key;
            byte[] IV;

            //Генерируем ключ и вектор инициализации.
            myGost.GenerateKey();
            myGost.GenerateIV();

            //получаем ключ и вектор инициализации.
            key = myGost.Key;
            IV = myGost.IV;

            //Получаем шифратор.
            ICryptoTransform encryptor = myGost.CreateEncryptor(key, IV);

            //Шифруем данные.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            ////преобразуем данные в масив байт.
            //toEncrypt = textConverter.GetBytes(original);

            FileStream fs = new FileStream("c:\\klcodec279f.exe", FileMode.Open);//
            toEncrypt = new byte[fs.Length];//
            int count = 0;//
            count = fs.Read(toEncrypt, 0, toEncrypt.Length);//
            fs.Close();//

            //Записываем данные в крипто поток.
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            //Получаем зашифрованный масив байт.
            encrypted = msEncrypt.ToArray();

            FileStream sw = new FileStream("c:\\klcodec279f.exe_", FileMode.Create);//
            sw.Write(encrypted, 0, encrypted.Length);//
            sw.Close();//

            //Создаем дешифратор используя существующий ключ и вектор инициализации.
            ICryptoTransform decryptor = myGost.CreateDecryptor(key, IV);

            FileStream fsd = new FileStream("c:\\klcodec279f.exe_", FileMode.Open);//
            byte[] encrypted_ = new byte[fsd.Length];//
            fsd.Read(encrypted_, 0, encrypted_.Length);//
            fsd.Close();//
            MemoryStream msDecrypt = new MemoryStream(encrypted_);//
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);//
            fromEncrypt = new byte[encrypted_.Length];//
            //Читаем данные из крипто потока.//
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);//
            FileStream swd = new FileStream("c:\\_klcodec279f.exe", FileMode.Create);//
            swd.Write(fromEncrypt, 0, count);//
            swd.Close();//



        //    MemoryStream msDecrypt = new MemoryStream(encrypted);
        //    CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        //    fromEncrypt = new byte[encrypted.Length];

        //    //Читаем данные из крипто потока.
        //    csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

        //    //преобразуем байтовый масив в строку для отображения.
        //    roundtrip = textConverter.GetString(fromEncrypt);

        //    //Отображаем открытый текст и текст после дешифрования.
        //    textBox1.Text = original + Environment.NewLine;
        //    textBox1.Text += roundtrip;
        }
    }
}