using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Netcode.Settings;
using Netcode.Crypt;
using Netcode.Controls;
using Netcode.Messages;
using System.Diagnostics;

namespace Necode.Crypt.Action
{
    public partial class FormAction : Form
    {
        public FormAction()
        {
            InitializeComponent();
        }

        string _src_fullname = string.Empty;
        string _dst_path = string.Empty;
        string _dst_file_name = string.Empty;
        string _prefix = FileCrypt.default_prefix;
        string _key_word = FileCrypt.default_key;
        UInt16 _key_size = (UInt16)FileCrypt.key_size.K256;
        string _key_word_filename = FileCrypt.default_key;
        string _time_stamp = string.Empty;
        Stopwatch sw = new Stopwatch();

        bool _IsEncrypt = true;

        /// <summary>
        /// Конструктор формы де/шифрования файла. Предпреждений перезаписи нет!
        /// </summary>
        /// <param name="src_fullname">Исходный файл</param>
        /// <param name="dst_path">Папку, в которую надо сохранять</param>
        /// <param name="dst_file_name">Имя файла, с которым его следует сохранить (работает только при расшифровании; если исходное - пустое)</param>
        /// <param name="time_stamp">Штамп времени в тиках, не шифруется, добавляется в начало имени файла (м.б. пустым)</param>
        /// <param name="IsEncrypt">Шифрация ли</param>
        /// <param name="prefix">Префикс "0." шифруемых файлов</param>
        /// <param name="key_word">Ключ-пароль для шифрования файлов</param>
        /// <param name="key_size">Длина ключа шифрования файлов (256 192 128)</param>
        /// <param name="key_word_filename">Ключ для шифрования имен файлов</param>
        public FormAction(string src_fullname, string dst_path, string dst_file_name, string time_stamp, bool IsEncrypt, string prefix, string key_word, UInt16 key_size, string key_word_filename)
        {
            InitializeComponent();

            dst_path = ManageSetting.FixDrivePath(dst_path);

            _src_fullname = src_fullname;
            _dst_path = dst_path;
            _dst_file_name = dst_file_name;
            _IsEncrypt = IsEncrypt;

            _prefix = prefix;
            _key_word = key_word;
            _key_size = key_size;
            _key_word_filename = key_word_filename;
            if (!string.IsNullOrEmpty(time_stamp))
            {
                _time_stamp = time_stamp + FileTreeExplorer.delimiter;
            }
            else
            {
                _time_stamp = string.Empty;
            }
            sw.Reset();
        }

        void cr_CryptFileTail(double tail)
        {
            string title = "Дешифрование";
            
            if (_IsEncrypt)
            {
                title = "Шифрование";
            }

            if (!sw.IsRunning)
            {
                sw.Start();
            }

            string second_to_end = string.Empty;
            if (tail > 0.01)
            {
                second_to_end = ". Осталось " + string.Format("{0:0.00}", ((sw.ElapsedMilliseconds*((1 / tail) - 1)) / 1000)) + "с.";
            }

            this.Text = title + " -- " + string.Format("{0:0.00}", tail * 100.00) + "%" + second_to_end;
        }

        private void FormAction_Shown(object sender, EventArgs e)
        {
            //Заокмментировано, потому что проверка пароля для входа не используется пока
            //if (PrefSettings.pwd == null)
            //{
            //    FormPwd fp = new FormPwd();
            //    if (fp.ShowDialog() == DialogResult.OK)
            //    {
            //        FileTransfer(_src_file_path, _dst_path, _IsEncrypt);
            //    }
            //}
            //else
            //{
            FileTransfer(_src_fullname, _dst_path, _IsEncrypt);
            //}
        }

        /// <summary>
        /// Простая функция (де-)шифровани файлов
        /// </summary>
        /// <param name="src_fullname">Исходное полное имя файла</param>
        /// <param name="dst_path">Каталог назначения (без имени файла!)</param>
        /// <param name="isEncrypt">Шифрование?</param>
        private void FileTransfer(string src_fullname, string dst_path, bool isEncrypt)
        {
            Application.DoEvents();
            FileCrypt cr = new FileCrypt();
            cr.CryptFileTail += new FileCrypt.OnCryptFileTail(cr_CryptFileTail);

            string dst_fullname;
            string _enc_file_name = string.Empty;
            string _dec_file_name = string.Empty;
            textBox_src_file.Text = src_fullname;

            try
            {
                if (!Directory.Exists(dst_path))
                {
                    Directory.CreateDirectory(dst_path);
                }

                if (isEncrypt)
                {
                    #region Шифрование
                    this.Text = "Шифрование";
                    listBox_log.Items.Add("Шифрование начато...");
                    //EncryptFName - вызов из флат-эксплорера по хорошему
                    //Здесь результативное имя файла всегда постоянное
                    _enc_file_name = cr.EncryptFName(Path.GetFileName(src_fullname), _key_word_filename, _prefix);
                    dst_fullname = Path.Combine(dst_path, _time_stamp+_enc_file_name);
                    textBox_dst_file.Text = dst_fullname;

                    if (dst_path.Length < 3)
                    {
                        new CriticalErrors().PrintError("S2", "В эту папку сохранять нельзя"); 
                    }
                    else
                    {
                        cr.crypt_file(true, _key_word, _key_size, src_fullname, dst_fullname, progressBar_crypt);
                    }
                    #endregion
                }
                else
                {
                    #region Дешифрование
                    this.Text = "Дешифрование";
                    listBox_log.Items.Add("Дешифрование начато...");
                    _dec_file_name = _dst_file_name;
                    if (string.IsNullOrEmpty(_dst_file_name))
                    {
                        _dec_file_name = cr.DecryptFName(Path.GetFileName(src_fullname), _key_word_filename, _prefix);
                    }
                    dst_fullname = Path.Combine(dst_path, _dec_file_name);
                    textBox_dst_file.Text = dst_fullname;

                    if (dst_path.Length < 3)
                    {
                        new CriticalErrors().PrintError("S2", "В эту папку сохранять нельзя"); 
                    }
                    else
                    {
                         cr.crypt_file(false, _key_word, _key_size, src_fullname, dst_fullname, progressBar_crypt);
                     }
                    #endregion
                }
                listBox_log.Items.Add("OK");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("E1", ex.Message + " | " + ex.TargetSite); 
                listBox_log.Items.Add(ex.Message);
                listBox_log.Items.Add("ERROR...");
            }
            finally
            {
                button_ok.Enabled = true;
            }
        }



        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void FormAction_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!button_ok.Enabled)
            //{

            //}
        }

    }
}