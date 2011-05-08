using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using Netcode.Settings;
using Netcode.Messages;

namespace Netcode.Crypt
{
    public class FileCrypt
    {
        public static string not_crypted_mess = " :: (не зашифрован)";
        public static string crypted_mess = " :: (зашифрован)";
        public static string default_key = "ZXCVBNMfghfghfggh67890$%^+_)(*&?><-GERTYUIOP123%^+_)"+
                                            "(*&?>YUIfghsda67890$%^+_)(*&?><-GER&?><-GERTYUIOP123%^+_)"+
                                            "(*af&?>YUIOP1RgfgjghkWsdfdfYUIOP12jjfghddfsfds67890$%^+_)"+
                                            "(*dfhg565547dfvghg?>YUIOP12345678dfghytfER?><-GEfgh123456"+
                                            "78dfghytfER&678dfghytf6dfghytf78";
        public enum key_size { K256=256, K192=192, K128=128 };
        public static string default_prefix = "0";

        
        public delegate void OnCryptFileTail(double f);
        public event OnCryptFileTail CryptFileTail;

        public static bool CheckKeyFNameLen(string key)
        {
            return true;
        }

        public static bool isKeyFNameLen(string f_name)
        {
            return true;
        }

        /// <summary>
        /// Шифование имени файла
        /// </summary>
        /// <param name="d_str">Имя файла</param>
        /// <param name="key_word_filename">Ключ</param>
        /// <param name="prefix">Добавляемый вперед нешифруемый префикс</param>
        /// <returns>[PREFIX].ENCRYPTSRTING</returns>
        public string EncryptFName(string d_str, string key_word_filename, string prefix)
        {
            if (string.IsNullOrEmpty(key_word_filename)) { key_word_filename = default_key; }
            string e_str = string.Empty;
            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word_filename, null); //класс, позволяющий генерировать ключи на базе паролей
                pdb.HashName = "SHA512"; //будем использовать SHA512

                SymmetricAlgorithm alg = new TripleDESCryptoServiceProvider();
                byte[] iv = new Byte[alg.BlockSize >> 3];
                byte[] key = pdb.GetBytes(alg.KeySize >> 3);
                // Шифрование
                ICryptoTransform transform = alg.CreateEncryptor(key, iv);
                byte[] passwordByte = Encoding.GetEncoding(1251).GetBytes(d_str);
                byte[] encryptedPassword = transform.TransformFinalBlock(passwordByte, 0, passwordByte.Length);

                foreach (byte enc in encryptedPassword)
                {
                    string dd = enc.ToString("X2");
                    e_str += dd;// "." + enc.ToString();
                }
                e_str = prefix + "." + e_str;
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("E2", ex.Message + " | " + ex.TargetSite);
                e_str = string.Empty;
            }
            return e_str;
        }

        /// <summary>
        /// Дешифование имени файла
        /// </summary>
        /// <param name="d_str">Имя файла [PREFIX].ENCRYPTSRTING</param>
        /// <param name="key_word_filename">Ключ</param>
        /// <param name="prefix">Ожидаемый впереди нешифруемый префикс</param>
        /// <returns>DECRYPTSRTING</returns>
        public string DecryptFName(string e_str, string key_word_filename, string prefix)
        {
            if (string.IsNullOrEmpty(key_word_filename)) { key_word_filename = default_key; }
            string d_str = string.Empty;
            try
            {
                string[] arr_e_str = Regex.Split(e_str, "\\.");
                if (arr_e_str.Length > 1)
                {
                    int rr;
                    if (arr_e_str[0] == prefix && Int32.TryParse(string.Format("{0}", arr_e_str[1].Length / 2), out rr))
                    {
                        d_str = "Имя файла не расшифровано ";
                        int count_bytes = (int)arr_e_str[1].Length / 2;
                        byte[] passwordByte = new byte[count_bytes];
                        for (int i = 0; i < count_bytes; i++)
                        {
                            passwordByte[i] = Convert.ToByte(int.Parse(arr_e_str[1].Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
                        }

                        SymmetricAlgorithm alg = new TripleDESCryptoServiceProvider();

                        PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word_filename, null); //класс, позволяющий генерировать ключи на базе паролей
                        pdb.HashName = "SHA512"; //будем использовать SHA512
                        byte[] iv = new Byte[alg.BlockSize >> 3];
                        byte[] key = pdb.GetBytes(alg.KeySize >> 3);
                        // ДеШифрование
                        ICryptoTransform transform = alg.CreateDecryptor(key, iv);

                        byte[] decryptedPassword = transform.TransformFinalBlock(passwordByte, 0, passwordByte.Length);
                        d_str = Encoding.GetEncoding(1251).GetString(decryptedPassword);
                    }
                    else
                    {
                        d_str = e_str + not_crypted_mess;
                    }
                }
                else
                {
                    d_str = e_str + not_crypted_mess;
                }
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("E2", ex.Message + " | " + ex.TargetSite);
                DateTime dt;
                dt = DateTime.Now;
                string time = dt.Hour.ToString() + "-" + dt.Minute.ToString() + "-" + dt.Second.ToString() + " (" + dt.Millisecond.ToString() + ")";
                d_str += time;
            }
            return d_str;
        }

        /// <summary>
        /// Шифрование/Дешифрование файла
        /// </summary>
        /// <param name="isEncrypting">Шифровать?</param>
        /// <param name="key_word">Пароль, из которого генерируется ключ</param>
        /// <param name="key_size">Длина ключа</param>
        /// <param name="src_file">Входной файл</param>
        /// <param name="dst_file">Выходной файл (путь д.б. уже создан)</param>
        /// <param name="pr_b">Прогресс бар</param>
        public void crypt_file(bool isEncrypting, string key_word, int key_size, string src_file, string dst_file, ProgressBar pr_b)
        {
            if (string.IsNullOrEmpty(key_word)) { key_word = FileCrypt.default_key; }
            SymmetricAlgorithm alg;
            alg = (SymmetricAlgorithm)RijndaelManaged.Create(); //пример создания класса RijndaelManaged

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word, null); //класс, позволяющий генерировать ключи на базе паролей
            pdb.HashName = "SHA512"; //будем использовать SHA512
            int keylen = key_size; //получаем размер ключа из ComboBox’а
            alg.KeySize = keylen; //устанавливаем размер ключа
            alg.Key = pdb.GetBytes(keylen >> 3); //получаем ключ из пароля
            alg.Mode = CipherMode.CBC; //используем режим CBC
            alg.IV = new Byte[alg.BlockSize >> 3]; //и пустой инициализационный вектор
            ICryptoTransform tr;
            if (isEncrypting)
            {
                tr = alg.CreateEncryptor(); //создаем encryptor
            }
            else
            {
                tr = alg.CreateDecryptor(); //создаем decryptor

            }

            FileStream instream = new FileStream(src_file, FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream outstream = new FileStream(dst_file, FileMode.Create, FileAccess.Write, FileShare.None);
            int buflen = ((2 << 16) / alg.BlockSize) * alg.BlockSize;
            byte[] inbuf = new byte[buflen];
            byte[] outbuf = new byte[buflen];
            int len;
            long input_size = instream.Length;
            long c_position = 0;
            pr_b.Maximum = int.MaxValue;

            while ((len = instream.Read(inbuf, 0, buflen)) == buflen)
            {
                int enclen = tr.TransformBlock(inbuf, 0, buflen, outbuf, 0); //собственно шифруем
                outstream.Write(outbuf, 0, enclen);
                c_position += enclen;
                pr_b.Value = (int)Math.Ceiling((double)int.MaxValue * ((double)c_position / (double)input_size));
                if (CryptFileTail != null)
                {
                    CryptFileTail.Invoke(((double)c_position / (double)input_size));
                }
                Application.DoEvents();
            }
            instream.Close();
            outbuf = tr.TransformFinalBlock(inbuf, 0, len); //шифруем финальный блок
            outstream.Write(outbuf, 0, outbuf.Length);
            pr_b.Value = int.MaxValue;
            outstream.Close();
            alg.Clear(); //осуществляем зачистку
        }

        //public void decrypt_file(string key_word, int key_size, string src_enc_file, string dst_dec_file, ProgressBar pr_b)
        //{
        //    if (string.IsNullOrEmpty(key_word)) { key_word = FileCrypt.default_key; }
        //    SymmetricAlgorithm alg;
        //    alg = (SymmetricAlgorithm)RijndaelManaged.Create(); //пример создания класса RijndaelManaged

        //    PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word, null); //класс, позволяющий генерировать ключи на базе паролей
        //    pdb.HashName = "SHA512"; //будем использовать SHA512
        //    int keylen = key_size; //получаем размер ключа из ComboBox’а
        //    alg.KeySize = keylen; //устанавливаем размер ключа
        //    alg.Key = pdb.GetBytes(keylen >> 3); //получаем ключ из пароля
        //    alg.Mode = CipherMode.CBC; //используем режим CBC
        //    alg.IV = new Byte[alg.BlockSize >> 3]; //и пустой инициализационный вектор
        //    ICryptoTransform tr = alg.CreateDecryptor(); //создаем decryptor

        //    FileStream instream = new FileStream(src_enc_file, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    FileStream outstream = new FileStream(dst_dec_file, FileMode.Create, FileAccess.Write, FileShare.None);
        //    int buflen = ((2 << 16) / alg.BlockSize) * alg.BlockSize;
        //    byte[] inbuf = new byte[buflen];
        //    byte[] outbuf = new byte[buflen];
        //    int len;
        //    long input_size = instream.Length;
        //    long c_position = 0;
        //    pr_b.Maximum = int.MaxValue;
        //    while ((len = instream.Read(inbuf, 0, buflen)) == buflen)
        //    {
        //        int enclen = tr.TransformBlock(inbuf, 0, buflen, outbuf, 0); //собственно расшифровываем
        //        outstream.Write(outbuf, 0, enclen);
        //        c_position += enclen;
        //        pr_b.Value = (int)Math.Ceiling((double)int.MaxValue * ((double)c_position / (double)input_size));
        //        Application.DoEvents();
        //    }
        //    instream.Close();
        //    outbuf = tr.TransformFinalBlock(inbuf, 0, len); //расшифровываем финальный блок
        //    outstream.Write(outbuf, 0, outbuf.Length);
        //    pr_b.Value = int.MaxValue;
        //    outstream.Close();
        //    alg.Clear(); //осуществляем зачистку
        //}

        public byte[] decrypt_file_to_byte(string key_word, int key_size, string src_enc_file, ProgressBar pr_b)
        {

            if (string.IsNullOrEmpty(key_word)) { key_word = FileCrypt.default_key; }
            SymmetricAlgorithm alg;
            alg = (SymmetricAlgorithm)RijndaelManaged.Create(); //пример создания класса RijndaelManaged

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word, null); //класс, позволяющий генерировать ключи на базе паролей
            pdb.HashName = "SHA512"; //будем использовать SHA512
            int keylen = key_size; //получаем размер ключа из ComboBox’а
            alg.KeySize = keylen; //устанавливаем размер ключа
            alg.Key = pdb.GetBytes(keylen >> 3); //получаем ключ из пароля
            alg.Mode = CipherMode.CBC; //используем режим CBC
            alg.IV = new Byte[alg.BlockSize >> 3]; //и пустой инициализационный вектор
            ICryptoTransform tr = alg.CreateDecryptor(); //создаем decryptor

            FileStream instream = new FileStream(src_enc_file, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] decr_text = new byte[instream.Length];
            MemoryStream outstream = new MemoryStream(decr_text, true);// FileStream(dst_dec_file, FileMode.Create, FileAccess.Write, FileShare.None);
            int buflen = ((2 << 16) / alg.BlockSize) * alg.BlockSize;
            byte[] inbuf = new byte[buflen];
            byte[] outbuf = new byte[buflen];
            int len;
            long input_size = instream.Length;
            long c_position = 0;
            pr_b.Maximum = int.MaxValue;
            while ((len = instream.Read(inbuf, 0, buflen)) == buflen)
            {
                int enclen = tr.TransformBlock(inbuf, 0, buflen, outbuf, 0); //собственно расшифровываем
                outstream.Write(outbuf, 0, enclen);
                c_position += enclen;
                pr_b.Value = (int)Math.Ceiling((double)int.MaxValue * ((double)c_position / (double)input_size));
                Application.DoEvents();
            }
            instream.Close();
            outbuf = tr.TransformFinalBlock(inbuf, 0, len); //расшифровываем финальный блок
            outstream.Write(outbuf, 0, outbuf.Length);
            pr_b.Value = int.MaxValue;
            outstream.Close();
            alg.Clear(); //осуществляем зачистку

            return decr_text;
        }

        public void encryt_byte_to_file(string key_word, int key_size, byte[] text_bytes, string dst_enc_file, ProgressBar pr_b)
        {
            if (string.IsNullOrEmpty(key_word)) { key_word = FileCrypt.default_key; }
            SymmetricAlgorithm alg;
            alg = (SymmetricAlgorithm)RijndaelManaged.Create(); //пример создания класса RijndaelManaged

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key_word, null); //класс, позволяющий генерировать ключи на базе паролей
            pdb.HashName = "SHA512"; //будем использовать SHA512
            int keylen = key_size; //получаем размер ключа из ComboBox’а
            alg.KeySize = keylen; //устанавливаем размер ключа
            alg.Key = pdb.GetBytes(keylen >> 3); //получаем ключ из пароля
            alg.Mode = CipherMode.CBC; //используем режим CBC
            alg.IV = new Byte[alg.BlockSize >> 3]; //и пустой инициализационный вектор
            ICryptoTransform tr = alg.CreateEncryptor(); //создаем encryptor

            MemoryStream instream = new MemoryStream(text_bytes, false);
            FileStream outstream = new FileStream(dst_enc_file, FileMode.Create, FileAccess.Write, FileShare.None);
            int buflen = ((2 << 16) / alg.BlockSize) * alg.BlockSize;
            byte[] inbuf = new byte[buflen];
            byte[] outbuf = new byte[buflen];
            int len;
            long input_size = instream.Length;
            long c_position = 0;
            pr_b.Maximum = int.MaxValue;

            while ((len = instream.Read(inbuf, 0, buflen)) == buflen)
            {
                int enclen = tr.TransformBlock(inbuf, 0, buflen, outbuf, 0); //собственно шифруем
                outstream.Write(outbuf, 0, enclen);
                c_position += enclen;
                pr_b.Value = (int)Math.Ceiling((double)int.MaxValue * ((double)c_position / (double)input_size));
                Application.DoEvents();
            }
            instream.Close();
            outbuf = tr.TransformFinalBlock(inbuf, 0, len); //шифруем финальный блок
            outstream.Write(outbuf, 0, outbuf.Length);
            pr_b.Value = int.MaxValue;
            outstream.Close();
            alg.Clear(); //осуществляем зачистку
        }
    }
}
