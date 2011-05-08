using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Netcode.Messages;

namespace Netcode.AT
{
    public class ATGSM
    {
        public const string terminator = "\r\n";
        public delegate void OnReadBuffer(string buffer);
        public event OnReadBuffer ReadBuffer;
        System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();

        /// <summary>
        /// Статусы SMS, полученные в формате PDU
        /// </summary>
        public enum MsgPduStatus
        {
            REC_UNREAD = 0, //It refers to the message status "received unread".
            REC_READ = 1, //It refers to the message status "received read".
            STO_UNSENT = 2, //It refers to the message status "stored unsent".
            STO_SENT = 3, //It refers to the message status "stored sent".
            ALL = 4//All messages
        }

        /// <summary>
        /// Тип передаваемого номера (городской/международный)
        /// </summary>
        public enum TypeOfNumber
        {
            National = 0x81,
            International = 0x91
        }

        /// <summary>
        /// Кодировка сообщений
        /// </summary>
        public enum DEncProtocol
        {
            SbitDefault = 0x00,// 7-bit 160chr in 140pc bytes 
            UTF7 = 0x08// - BigEndianUnicode (UTF7)
            //#	'F0' for 7bit, immediate display	'7biti'
            //#	'F6' for 8bit, SIM specific			'8bit'
            //#	'F4' for 8bit, immediate display	'8biti'
            //#	'F5' for 8bit, ME specific			'8bitm'
        }

        /// <summary>
        /// Структура, переносящая телефон, смс-центр
        /// </summary>
        struct Contacts
        {

        }

        public ATGSM(string PortName)
        {
            //далее необходимо настроить порт для работы с мобильным телефоном
            port.PortName = PortName;

            //Время ожидания записи и чтения с порта
            port.WriteTimeout = 5000;
            port.ReadTimeout = 5000;

            //Настраиваем скорость обмена данными с телефоном - телефон не может обрабатывать данный на максимальной скорости
            port.BaudRate = 9600;

            //Другие необходимые настройки - подходит для большинства телефонов - но возможно придется настраивать:
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Handshake = Handshake.RequestToSend;
            port.DtrEnable = true;
            port.RtsEnable = true;
            port.NewLine = System.Environment.NewLine;
            OpenPort();
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// Открыть порт
        /// </summary>
        /// <returns></returns>
        public bool OpenPort()
        {
            bool res = false;
            try
            {
                if (!port.IsOpen)
                {
                    //открываем порт
                    port.Open();
                    ATZ();
                    res = true;
                }
                else
                {
                    res = false;
                    throw new ArgumentException("Порт уже открыт");
                }
            }
            catch (IOException ex)
            {
                if (MessageBox.Show(ex.Message, "Порт " + port.PortName + " не существует", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    OpenPort();
                }
                else
                {
                    res = false;
                    new CriticalErrors().PrintError("P1", ex.Message + " | " + ex.TargetSite);
                    //throw new ArgumentException("ERROR: " + ex.Message);
                }
            }
            return res;
        }

        /// <summary>
        /// Закрыть порт
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            bool ret = false;
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                    ret = true;
                }
                else
                {
                    ret = false;
                    throw new ArgumentException("Порт не открыт");
                }
            }
            catch (InvalidOperationException)
            {
                ret = false;
            }
            return ret;
        }

        #region -------- Основные ---------
        /// <summary>
        /// Эта команда восстанавливает исходное состояние модема
        /// в соответствии с параметрами,
        /// хранящимися в энергонезависимой памяти.
        /// </summary>
        /// <returns></returns>
        public string ATZ()
        {
            port.Write("ATZ" + terminator);
            return ReadFromPort();
        }

        public string AT()
        {
            port.Write("AT" + terminator);
            return ReadFromPort();
        }

        /// <summary>
        /// Отправляется перед начлалом отправки в линию звука.
        /// </summary>
        /// <returns></returns>
        public string DLE_ETX()
        {
            byte[] dle_etx = new byte[2];
            dle_etx[0] = 0x10;
            dle_etx[1] = 0x03;
            port.Write(dle_etx, 0, dle_etx.Length);
            return ReadFromPort("VCON");
        }

        /// <summary>
        /// Выполнить AT команду
        /// </summary>
        /// <param name="cmd">команда</param>
        /// <param name="done_str">Ожидаемая строка ответа (ERROR уже есть)</param>
        /// <returns>буфер</returns>
        public string RunAT(string cmd, string done_str)
        {
            port.Write(cmd + terminator);
            return ReadFromPort(done_str);
        }

        /// <summary>
        /// Выполнить AT команду. Предполагается, что строка ответа всегда ОК или ERROR
        /// </summary>
        /// <param name="cmd">команда</param>
        /// <returns>буфер</returns>
        public string RunAT(string cmd)
        {
            port.Write(cmd + terminator);
            return ReadFromPort("OK");
        }

        /// <summary>
        /// Установить текстовый режим
        /// </summary>
        /// <returns></returns>
        public string SetTextMode()
        {
            port.Write("AT+CMGF=1" + terminator);//текстовый режим
            return ReadFromPort();
        }

        #endregion

        #region -------- Кодировки --------
        /// <summary>
        /// Наборы символов
        /// </summary>
        /// <returns></returns>
        public string[] GetArraySupEnc()
        {
            port.Write("AT+CSCS=?" + terminator);//текстовый режим
            //AT+CSCS=?\r\r\n+CSCS: (\"IRA\",\"GSM\",\"UCS2\")\r\n\r\nOK\r\n
            string pat1 = @"CSCS:\s\((?<addr>[(),.\""A-za-z0-9\-]+)\)[\r\n]";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match match1 = reg1.Match(ReadFromPort());

            string s = match1.Groups["addr"].Value;

            string pat2 = @"\""(?<addr>[(),.A-za-z0-9\-]+)\""";
            Regex reg2 = new Regex(pat2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection matches = reg2.Matches(s);
            string[] ret = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                ret[i] = matches[i].Groups["addr"].Value;
            }
            return ret;
        }

        /// <summary>
        /// Текущий набор символов
        /// </summary>
        /// <returns></returns>
        public string GetCurrentEnc()
        {
            port.Write("AT+CSCS?" + terminator);//текстовый режим
            //AT+CSCS?\r\r\n+CSCS: \"IRA\"\r\n\r\nOK\r\n
            string pat1 = @"CSCS:\s\""(?<addr>[(),.A-za-z0-9\-]+)\""[\r\n]";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //string s = ReadFromPort();
            Match match1 = reg1.Match(ReadFromPort());
            return match1.Groups["addr"].Value;
        }

        /// <summary>
        /// Установить набор символов
        /// </summary>
        /// <param name="enc"></param>
        /// <returns></returns>
        public string SetEnc(string enc)
        {
            port.Write("AT+CMGF=1" + terminator);//текстовый режим
            port.Write("AT+CSCS=\"" + enc + "\"");//установить набор
            string s = ReadFromPort();
            return s;
        }
        #endregion

        #region -------- Работа с кодированием --------
        /// <summary>
        /// Получить время прихода принятой SMS на сервер провайдера
        /// </summary>
        /// <param name="SCTS"></param>
        /// <returns></returns>
        public DateTime GetSMSTime(string SCTS)
        {
            string s = SCTS;
            /*
            Code: 
            +-------------------------------------------------------+
            |   1       2       3       4       5       6       7   | 
            | 0   1   2   3   4   5   6   7   8   9   10  11 12  13 |
            +-------+-------+-------+-------+-------+-------+-------+ 
            |  Год  | Месяц |  День |  Час  |  Мин  |  Сек  |Вр.Зона| 
            +-------+-------+-------+-------+-------+-------+-------+ 
            | 2 | 1 | 2 | 1 | 2 | 1 | 2 | 1 | 2 | 1 | 2 | 1 | 2 | 1 | 
            +-------+-------+-------+-------+-------+-------+-------+ 
            Например запись такого вида: 
            +--+--+--+--+--+--+--+ 
            |79|50|12|31|54|33|00| 
            +--+--+--+--+--+--+--+              
            разшифровывается как: 
            21 мая 97 года 13:45:33
             */
            int year = int.Parse(s[1].ToString() + s[0].ToString());
            int mon = int.Parse(s[3].ToString() + s[2].ToString());
            int day = int.Parse(s[5].ToString() + s[4].ToString());
            int hour = int.Parse(s[7].ToString() + s[6].ToString());
            int min = int.Parse(s[9].ToString() + s[8].ToString());
            int sec = int.Parse(s[11].ToString() + s[10].ToString());
            int utc = int.Parse(s[13].ToString() + s[12].ToString());
            DateTime dt = new DateTime(2000 + year, mon, day, hour, min, sec);
            return dt;
        }

        /// <summary>
        /// Получить из номера вида ABCDEFGHIKL BCD-строку BADCFEHGKIFL
        /// </summary>
        /// <param name="number">Номер в принятом формате</param>
        /// <returns></returns>
        public string BCDFixNum(string number)
        {
            string res = string.Empty;
            if (number.Length % 2 > 0)
            {
                number += "F";
            }
            for (int i = 0; i < number.Length; i++)
            {
                res += number[i + 1].ToString() + number[i].ToString();
                i++;
            }
            return res;
        }

        /// <summary>
        /// Получить из строки BCD-вида BADCFEHGKIFL номера ABCDEFGHIKL
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string BCDUnFixNum(string number)
        {
            string res = string.Empty;
            if (number.Length % 2 != 0)
            {
                throw new ArgumentException("PDU telephone nuber must be even");
            }
            for (int i = 0; i < number.Length; i++)
            {
                res += number[i + 1].ToString() + number[i].ToString();
                i++;
            }
            if (res[res.Length - 1].ToString() == "F")
            {
                res = res.Remove(res.Length - 1);
            }
            return res;
        }

        /// <summary>
        /// String to UCS2
        /// </summary>
        /// <param name="s">строка utf</param>
        /// <returns>0-длина байт, 1-данные</returns>
        public string[] StringToUCS2(string s)
        {
            string res = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int k = (char)s[i];
                res = res + IntToHex(k, "4");
            }
            return new string[] { string.Format("{0}", IntToHex(res.Length / 2, "2")), res };
        }

        /// <summary>
        /// UCS2 to String
        /// </summary>
        /// <param name="ucs2_text">UCS2 строка</param>
        /// <returns>Текстовая строка</returns>
        public string USC2toString(string ucs2_text)
        {
            byte[] res = new byte[ucs2_text.Length / 2];
            if (ucs2_text.Length % 4 == 0)
            {
                for (int i = 0; i < ucs2_text.Length / 2; i++)
                {
                    res[i] = Convert.ToByte(ucs2_text.Substring(i * 2, 2), 16);
                }
                return Encoding.BigEndianUnicode.GetString(res);
            }
            else
            {
                throw new ArgumentException("Длина входной строки не кратна 4-м");
            }
        }

        /// <summary>
        /// Unicode 7bit hex to 8bit string (no russian character set)
        /// </summary>
        /// <param name="s">строка лат.</param>
        /// <returns>0-длина байт, 1-данные</returns>
        public string PDU7bToString(string pdu_string)
        {
            string ret_bin = string.Empty;//бит-строка

            //А стоит ли с этим возится?
            if (pdu_string.Length % 2 > 0)
            {
                throw new ArgumentException("Входная строка нечетна");
            }

            //Читаем побайтно строку, конвертируем в 8бит в байте
            for (int i = 0; i < pdu_string.Length; i++)
            {
                //переставляем байты
                ret_bin = ret_bin + HexToBin(pdu_string.Substring(pdu_string.Length - i - 2, 2), 4);
                i++;
            }

            int ful_hex_it = 0;//итераций чтения
            int bit_in_byte = 7;//байт в бите
            double endsb = (double)ret_bin.Length / bit_in_byte;
            double intendsb = Math.Truncate(endsb);
            if (endsb != intendsb)
            {
                ful_hex_it = (int)intendsb + 1;
            }
            else
            {
                ful_hex_it = (int)intendsb;
            }

            byte[] ret = new byte[ful_hex_it];//конечный результат
            for (int hi = 0; hi < ful_hex_it; hi++)
            {
                //начало+7 не больше полной длины
                //здесь берем по 7 чисел bit и переводим в hex
                //начинаем с конца, но слева направо

                //сколько бит копировать
                int est_back = bit_in_byte;

                //смещение назад для получения указателя на 1-й бит
                int bst_back = bit_in_byte * (hi + 1);

                //Если длина не кратна кол-ву бит в байте, 
                //то в конце будет не полный байт
                //тогда считаем, сколько бит осталось
                if (bst_back > ret_bin.Length)
                {
                    bst_back = 0;
                    est_back = ret_bin.Length - (hi * bit_in_byte);
                }
                else
                {
                    //иначе двигаемся влево
                    bst_back = ret_bin.Length - bst_back;
                }
                //2-потому что имеем дело только с полным одним байтом
                ret[hi] = Convert.ToByte(BinToHex(ret_bin.Substring(bst_back, est_back), "2"), 16);
            }

            string s = string.Empty;
            //Иногда в последней итерации 0x00, она не значащая
            if (ret[ful_hex_it - 1] == (byte)0x00)
            {
                byte[] retf = new byte[ful_hex_it - 1];
                for (int i = 0; i < retf.Length; i++)
                {
                    retf[i] = ret[i];
                }
                s = Encoding.ASCII.GetString(retf);
            }
            else
            {
                s = Encoding.ASCII.GetString(ret);
            }
            return s;
        }

        /// <summary>
        /// Unicode 8bit string to 7bit hex (no russian character set)
        /// </summary>
        /// <param name="s">строка лат.</param>
        /// <returns>0-длина байт, 1-данные</returns>
        public string[] StringToPDU7b(string str)
        {
            string ret_bin = string.Empty;//бит-строка
            string ret = string.Empty;//результат

            //Читаем побайтно строку, получаем бит-последовательнось
            for (int i = 0; i < str.Length; i++)
            {
                int symbl_hex_code = (char)str[i];
                string bin_c = Convert.ToString(symbl_hex_code, 2);

                //весь смысл упаковки - если значащих цифр больше 7, то упаковать не сможем
                if (bin_c.Length > 7)
                {
                    throw new ArgumentException("A non printable symbol contained in text.");
                }

                //а если меньше, добавляем до 7
                while (bin_c.Length < 7)
                {
                    bin_c = "0" + bin_c;
                }

                //и прибавляем к строке слева(!)
                ret_bin = bin_c + ret_bin;
            }

            //в ret_bin теперь двоичные данные, которые надо перевести в hex

            //Если остаток от деления есть, то добавляем 1 итерацию для его перекрытия
            int ful_hex_it = 0;
            int bit_in_byte = 8;
            double endsb = (double)ret_bin.Length / bit_in_byte;
            double intendsb = Math.Truncate(endsb);
            if (endsb != intendsb)
            {
                ful_hex_it = (int)intendsb + 1;
            }
            else
            {
                ful_hex_it = (int)intendsb;
            }

            for (int hi = 0; hi < ful_hex_it; hi++)
            {
                //начало+8 не больше полной длины
                //здесь берем по 8 чисел bit и переводим в hex
                //начинаем с конца, но слева направо

                //сколько бит копировать
                int est_back = bit_in_byte;

                //смещение назад для получения указателя на 1-й бит
                int bst_back = bit_in_byte * (hi + 1);

                //Если длина не кратна кол-ву бит в байте, 
                //то в конце будет не полный байт
                //тогда считаем, сколько бит осталось
                if (bst_back > ret_bin.Length)
                {
                    bst_back = 0;
                    est_back = ret_bin.Length - (hi * bit_in_byte);
                }
                else
                {
                    //иначе двигаемся влево
                    bst_back = ret_bin.Length - bst_back;
                }
                //2-потому что имеем дело только с полным одним байтом
                ret += BinToHex(ret_bin.Substring(bst_back, est_back), "2");
            }

            //Иногда (оч. редко) в последней итерации 0x00, она не значащая??

            return new string[] { string.Format("{0}", IntToHex(str.Length, "2")), ret };
        }

        #endregion

        #region -------- Разное системное --------
        /// <summary>
        /// Получить уровень сигнала
        /// </summary>
        /// <returns>Уровень сигнала в db</returns>
        public double GetSignalLevel()
        {
            port.Write("AT+CSQ" + terminator);//уровень сигнала
            //ответ обычно такой:  AT+CSQ\r\r\n+CSQ: 23,99\r\n\r\nOK\r\n
            string pat1 = @"CSQ:\s(?<addr>[0-9]+)\,[0-9]";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);
            string buffer = ReadFromPort();
            double level = 0.0;
            if (!buffer.Contains("ERROR"))
            {
                Match match1 = reg1.Match(buffer);
                string val = match1.Groups["addr"].Value;
                level = (Math.Truncate(Convert.ToDouble(val)) * 2) - 113;
            }
            else
            {
                level = 0;
                throw new ArgumentException("ERROR: " + buffer);
            }
            return level;
        }
        #endregion

        #region -------- Отправка SMS --------

        /// <summary>
        /// Отправка СМС в текстовом виде (латиница макс 160, остальные режутся!)
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <param name="icode">Тип номера</param>
        /// <param name="mess">Сообщение</param>
        /// <returns></returns>
        public string SendSMS(string phone, TypeOfNumber icode, string mess)
        {
            try
            {
                if (mess.Length > 160)
                {
                    mess = mess.Substring(0, 160);
                }
                string message = string.Empty;
                port.Write("AT+CMGF=1\r\n");//текстовый режим
                System.Threading.Thread.Sleep(500);
                port.Write("AT+CMGS=\"" + phone + "\"," + (int)icode + "\r");//подготавливаем смс
                if (WaitBeforeContains(">", out message))
                {
                    port.Write(mess + (char)(26));//текст смс и отправка
                }
                string answ = ReadFromPort();
                //ответ такой:    \r\nTest\r\n+CMGS: 6\r\n\r\nOK\r\n

                return answ;
            }
            catch (Exception ex)
            {
                return "ERROR - НЕ ОТПРАВЛЕНО (" + ex.Message + " | " + ex.TargetSite + ")";
            }
        }

        //Пример отправки SMS с PDU
        public string SendPDUSMS()
        {
            string message = string.Empty;
            port.Write("AT+CMGF=0" + terminator);//текстовый режим выкл
            System.Threading.Thread.Sleep(500);
            port.Write("AT+CMGS=22\r");//подготавливаем смс
            if (WaitBeforeContains(">", out message))
            {
                port.Write("0031000B919730232933F10008AA080422043504410442" + (char)(26));//текст смс и отправка
            }
            string answ = ReadFromPort();
            //ответ такой:    \r\nTest\r\n+CMGS: 6\r\n\r\nOK\r\n

            return answ;
        }

        /// <summary>
        /// Компиляця PDU строки к отправке SMS
        /// </summary>
        /// <param name="smsc_num"></param>
        /// <param name="tsmsc_num"></param>
        /// <param name="da_num"></param>
        /// <param name="tda_num"></param>
        /// <param name="ep"></param>
        /// <param name="TimeLife"></param>
        /// <param name="text"></param>
        /// <param name="pdu_len">Длина PDU, исключая блок SCA</param>
        /// <returns></returns>
        string GetPDUSendStr(string smsc_num, TypeOfNumber tsmsc_num, string da_num, TypeOfNumber tda_num, DEncProtocol ep, string text, out int pdu_len)
        {
            int len_sca = 1;
            SPDUSMS spdusms = new SPDUSMS();
            string res = string.Empty;
            if (smsc_num.Length == 0)
            {
                spdusms.SMSC_num_len = 0x00;
                spdusms.SMSC_type_of_num = 0x00;
                spdusms.SMSC_num = string.Empty;
                res += "00";
            }
            else
            {
                spdusms.SMSC_type_of_num = (byte)tsmsc_num;
                spdusms.SMSC_num = BCDFixNum(smsc_num);
                len_sca += 1 + (spdusms.SMSC_num.Length / 2);
                spdusms.SMSC_num_len = (byte)(len_sca - 1);
                res += string.Format("{0:X2}", spdusms.SMSC_num_len) + string.Format("{0:X2}", spdusms.SMSC_type_of_num) + spdusms.SMSC_num;
            }

            spdusms.TPDU = 0x31;//Отчет о доставке, вр.жизни далее 1 байт
            res += string.Format("{0:X2}", spdusms.TPDU);

            spdusms.MR = 0x00;
            res += string.Format("{0:X2}", spdusms.MR);

            spdusms.DA_ref_len = (byte)da_num.Length;
            res += string.Format("{0:X2}", spdusms.DA_ref_len);

            spdusms.DA_type_of_num = (byte)tda_num;
            res += string.Format("{0:X2}", spdusms.DA_type_of_num);

            spdusms.DA_num = BCDFixNum(da_num);
            res += spdusms.DA_num;

            spdusms.PID = 0x00;
            res += string.Format("{0:X2}", spdusms.PID);

            spdusms.DCS = (byte)ep;
            res += string.Format("{0:X2}", spdusms.DCS);

            spdusms.VP = 0xAA;
            res += string.Format("{0:X2}", spdusms.VP);

            string[] rt = new string[2];
            switch (ep)
            {
                case DEncProtocol.SbitDefault:
                    {
                        rt = StringToPDU7b(text);
                        spdusms.UDL = Convert.ToByte(rt[0], 16);
                        spdusms.UD = rt[1];
                    }
                    break;
                case DEncProtocol.UTF7:
                    {
                        rt = StringToUCS2(text);
                        spdusms.UDL = Convert.ToByte(rt[0], 16);
                        spdusms.UD = rt[1];
                    }
                    break;
                default:
                    break;
            }

            res += rt[0] + rt[1];

            pdu_len = (res.Length / 2) - len_sca;
            return res;
        }

        #endregion

        #region -------- Получение SMS --------
        
        /// <summary>
        /// Чтение конкретной SMS в текстовом виде
        /// </summary>
        /// <returns></returns>
        public string ReadSMSText()
        {
            string message = string.Empty;
            port.Write("AT+CMGF=0" + terminator);//текстовый режим
            //port.Write("AT+CMGL=\"REC READ\""+ terminator);//читаем все смс - для текста
            port.Write("AT+CMGL="+(int)MsgPduStatus.REC_READ+ terminator);//читаем все смс - для pdu
            System.Threading.Thread.Sleep(2000);
            if (WaitBeforeContains("OK", out message))
            {
            }
            //для никса писал код. аналогично
            return message;
        }

        #endregion

        #region --- Чтение PDU-SMS из модема в структуру

        public RPDUSMS[] ReadPDUInboxSMS(string pdu, out int[] index, out int[] message_status, out string[] address_text)
        {
            //RPDUSMS pdusms = new RPDUSMS();
            /*
            +CMGL: 11,0,,44
            07919730071111F1040B919730232933F10000904082904343611C31D98C56B3DD7039582C2693CD66345AAD66B3DD6E385C2E07
            OK
             */
            string pat1 = @"\+CMGL:\s(?<index>[0-9]+),(?<message_status>[0-9]+),(?<address_text>[0-9A-Za-z\-]*),(?<TPDU_length>[0-9]+)(\r\n)(?<SMSC_number_and_TPDU>[0-9A-F]+)[\r\n]";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);
            //string s = ReadFromPort();
            //Match m = reg1.Match(pdu);
            MatchCollection m = reg1.Matches(pdu);
            RPDUSMS[] pdusms = new RPDUSMS[m.Count];
            index = new int[m.Count];
            message_status = new int[m.Count];
            address_text = new string[m.Count];

            for (int i = 0; i < m.Count; i++)
            {
                //ret[i] = m[i].Groups["addr"].Value;
                index[i] = int.Parse(m[i].Groups["index"].Value);
                message_status[i] = int.Parse(m[i].Groups["message_status"].Value);
                address_text[i] = m[i].Groups["address_text"].Value;

                string stpdu = m[i].Groups["SMSC_number_and_TPDU"].Value;

                int cursor = 0;
                pdusms[i].SMSC_num_len = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor = 2;
                if (pdusms[i].SMSC_num_len != 0x00)
                {
                    pdusms[i].SMSC_type_of_num = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                    cursor += 2;
                    int chread = ((int)pdusms[i].SMSC_num_len * 2) - 2;//2-длина SMSC_num_len
                    pdusms[i].SMSC_num = BCDUnFixNum(stpdu.Substring(cursor, chread));
                    cursor += chread;
                }
                else
                {
                    pdusms[i].SMSC_type_of_num = 0x00;
                    pdusms[i].SMSC_num = string.Empty;
                }
                pdusms[i].TPDU = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].OA_ref_num_len = Convert.ToByte(stpdu.Substring(cursor, 2), 16);//TODO не нашел, что делать, но говорят, что мб 0x00
                cursor += 2;

                pdusms[i].OA_type_of_num = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                int OA_num_len = (int)pdusms[i].OA_ref_num_len;
                if (OA_num_len % 2 > 0)
                {
                    OA_num_len++;
                }

                pdusms[i].OA_num = BCDUnFixNum(stpdu.Substring(cursor, OA_num_len));
                cursor += OA_num_len;

                pdusms[i].PID = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].DCS = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].SCTS = GetSMSTime(stpdu.Substring(cursor, 14));
                cursor += 14;

                pdusms[i].UDL = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                switch (pdusms[i].DCS)
                {
                    case (byte)DEncProtocol.SbitDefault:
                        {
                            pdusms[i].UD = PDU7bToString(stpdu.Substring(cursor));
                        }
                        break;
                    case (byte)DEncProtocol.UTF7:
                        {
                            pdusms[i].UD = USC2toString(stpdu.Substring(cursor));
                        }
                        break;
                    default:
                        {
                            pdusms[i].UD = PDU7bToString(stpdu.Substring(cursor));
                        }
                        break;
                }
            }


            #region why
            /*
            +CMGL: index,message_status,[address_text],TPDU_length<CR><LF>SMSC_number_and_TPDU[<CR><LF>+CMGL: ...]

            Before we discuss each of the fields that appear in the +CMGL information response, let's see an example
            that gives you some idea of how an actual +CMGL information response should look like:

            +CMGL: 1,0,,62
            07915892000000F0040B915892214365F700007040213252242331493A283D0795C3F33C88FE06C9CB6132885EC6D341EDF27C1E3E97E7207B3A0C0A5241E377BB1D7693E72E

            The index Field
            The first field of the information response of the +CMGL AT command, index, is an integer that specifies
            the location of the SMS message in the message storage area.

            The message_status Field
            The second field of the information response of the +CMGL AT command, message_status,
            is an integer that indicates the status of the SMS message. It can be one of the following four values:
            0. It refers to the message status "received unread".
            1. It refers to the message status "received read".
            2. It refers to the message status "stored unsent".
            3. It refers to the message status "stored sent".

            The address_text Field
            The third field of the information response of the +CMGL AT command, address_text, is a string that
            contains the text associated to address in the phonebook, where address is the phone number encoded in the
            TPDU of the SMSC_number_and_TPDU field. For example, if the phone number encoded in the TPDU is "91234567"
            and the text "Alice" is associated to the phone number "91234567" in the phonebook, address_text will be
            "Alice". The AT command +CSCS (command name in text: Select TE Character Set) can be used to specify the
            character set for displaying address_text.

            Note that address_text is an optional field. Some GSM/GPRS modems and mobile phones leave this field empty.
            (Examples: Philips 598 supports this field, while most Nokia products, including my Nokia 6021, and 
            Sony Ericsson T68i does not.)

            The TPDU_length Field
            The fourth field of the information response of the +CMGL AT command, TPDU_length, is an integer that
            indicates the length (in octets. 1 octet = 8 bits) of the TPDU contained in the SMSC_number_and_TPDU field.
            In the earlier example command line, the value of the SMSC_number_and_TPDU field is:

            07915892000000F0040B915892214365F700007040213252242331493A283D0795C3F33C88FE06C9CB6132885EC6D341EDF27C1E3E97E7207B3A0C0A5241E377BB1D7693E72E

            It can be divided into two parts. The following part is the TPDU:

                            040B915892214365F700007040213252242331493A283D0795C3F33C88FE06C9CB6132885EC6D341EDF27C1E3E97E7207B3A0C0A5241E377BB1D7693E72E

            The TPDU is coded in hexadecimal format. Each character represents 4 bits, i.e. 1/2 octet.
            The TPDU has 124 characters and so there are totally 62 octets. That's why the value of the
            TPDU_length field is 62.

            The SMSC_number_and_TPDU Field
            The fifth field of the information response of the +CMGL AT command, SMSC_number_and_TPDU, specifies
            the SMSC number and the TPDU (Transfer Protocol Data Unit) in hexadecimal format.

            If the SMS message to be read is an incoming SMS message, the value of the SMSC_number_and_TPDU field
            will be something like this:

            07915892000000F0040B915892214365F700007040213252242331493A283D0795C3F33C88FE06C9CB6132885EC6D341EDF27C1E3E97E7207B3A0C0A5241E377BB1D7693E72E

            The TPDU embedded in the above hexadecimal sequence is of the type SMS-DELIVER. Here is some of the
            information encoded in the hexadecimal sequence:
            SMSC number from which the SMS message was received: +85290000000
            Sender phone number: +85291234567
            Time and date at which the SMSC received the SMS message: 12 April 2007, 23:25:42 GMT+8 hours
            Text message: "It is easy to read text messages via AT commands."
            For the details about how the hexadecimal sequence of an incoming SMS message is coded, 
            please refer to the section titled "Some Explanation about the Decoding of the SMSC_number_and_TPDU Field
            Value of the +CMGR AT Command" of this SMS tutorial.

            If the SMS message to be read is an outgoing SMS message, the value of the SMSC_number_and_TPDU
            field will be something like this:

            07915892000000F001000B915892214365F7000021493A283D0795C3F33C88FE06CDCB6E32885EC6D341EDF27C1E3E97E72E

            The TPDU embedded in the above hexadecimal sequence is of the type SMS-SUBMIT.
            Its format is different from that of an SMS-DELIVER TPDU. Below shows some of the information
            encoded in the hexadecimal sequence above:
            SMSC number to be used if the SMS message is sent out: +85290000000
            Destination phone number: +85291234567
            Text message: "It is easy to send text messages."
            For the details about how the hexadecimal sequence of an outgoing SMS message is coded, 
            please refer to the section titled "Some Explanation about the Coding of the 
            SMSC_number_and_TPDU Parameter Value of the +CMGS AT Command" of this SMS tutorial.
            */
            #endregion

            return pdusms;
        }

        public SPDUSMS[] ReadPDUOutboxSMS(string pdu, out int[] index, out int[] message_status, out string[] address_text)
        {
            //RPDUSMS pdusms = new RPDUSMS();
            /*
            +CMGL: 0,3,,22
            07919772929090F011000B819830232933F10008FF080074006500730074
            OK
             */
            //07 919772929090F0 11 00 0B 81 9830232933F1 00 08 FF 08 0074006500730074
            string pat1 = @"\+CMGL:\s(?<index>[0-9]+),(?<message_status>[0-9]+),(?<address_text>[0-9A-Za-z\-]*),(?<TPDU_length>[0-9]+)(\r\n)(?<SMSC_number_and_TPDU>[0-9A-F]+)[\r\n]";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);

            MatchCollection m = reg1.Matches(pdu);
            SPDUSMS[] pdusms = new SPDUSMS[m.Count];
            index = new int[m.Count];
            message_status = new int[m.Count];
            address_text = new string[m.Count];

            for (int i = 0; i < m.Count; i++)
            {
                //ret[i] = m[i].Groups["addr"].Value;
                index[i] = int.Parse(m[i].Groups["index"].Value);
                message_status[i] = int.Parse(m[i].Groups["message_status"].Value);
                address_text[i] = m[i].Groups["address_text"].Value;

                string stpdu = m[i].Groups["SMSC_number_and_TPDU"].Value;

                int cursor = 0;
                pdusms[i].SMSC_num_len = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor = 2;
                if (pdusms[i].SMSC_num_len != 0x00)
                {
                    pdusms[i].SMSC_type_of_num = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                    cursor += 2;
                    int chread = ((int)pdusms[i].SMSC_num_len * 2) - 2;//2-длина SMSC_num_len
                    pdusms[i].SMSC_num = BCDUnFixNum(stpdu.Substring(cursor, chread));
                    cursor += chread;
                }
                else
                {
                    pdusms[i].SMSC_type_of_num = 0x00;
                    pdusms[i].SMSC_num = string.Empty;
                }
                pdusms[i].TPDU = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].MR = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].DA_ref_len = Convert.ToByte(stpdu.Substring(cursor, 2), 16);//TODO не нашел, что делать, но говорят, что мб 0x00
                cursor += 2;

                pdusms[i].DA_type_of_num = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                int DA_num_len = (int)pdusms[i].DA_ref_len;
                if (DA_num_len % 2 > 0)
                {
                    DA_num_len++;
                }

                pdusms[i].DA_num = BCDUnFixNum(stpdu.Substring(cursor, DA_num_len));
                cursor += DA_num_len;

                pdusms[i].PID = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].DCS = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].VP = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                pdusms[i].UDL = Convert.ToByte(stpdu.Substring(cursor, 2), 16);
                cursor += 2;

                switch (pdusms[i].DCS)
                {
                    case (byte)DEncProtocol.SbitDefault:
                        {
                            pdusms[i].UD = PDU7bToString(stpdu.Substring(cursor));
                        }
                        break;
                    case (byte)DEncProtocol.UTF7:
                        {
                            pdusms[i].UD = USC2toString(stpdu.Substring(cursor));
                        }
                        break;
                    default:
                        {
                            pdusms[i].UD = PDU7bToString(stpdu.Substring(cursor));
                        }
                        break;
                }
            }


            return pdusms;
        }
        #endregion

        #region --- Чтение TXT-SMS из модема в структуру

        public RTXTSMS[] ReadTXTInboxSMS(string txt, out int[] index, out string[] message_status)
        {
            /*
            +CMGL: 23,"REC READ","+79270511075",,"09/12/14,07:59:16+12"
            RESET
            */
            string pat1 = "\\+CMGL:\\s(?<index>[0-9]+),([\"|]*)(?<message_status>[0-9a-zA-Z\\s]+)([\"|]*),\"(?<address_text>.*)\",([\"|]*)(?<rel_addr_book>.*)([\"|]*),\"(?<date_rcv>[0-9//:\\+,\\/]*)\"(\\r\\n)(?<message>.*)(\\r\\n)";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);
            MatchCollection m = reg1.Matches(txt);
            
            RTXTSMS[] txtsms = new RTXTSMS[m.Count];
            index = new int[m.Count];
            message_status = new string[m.Count];

            for (int i = 0; i < m.Count; i++)
            {
                index[i] = int.Parse(m[i].Groups["index"].Value);
                message_status[i] = m[i].Groups["message_status"].Value;
                txtsms[i].address_text = m[i].Groups["address_text"].Value;
                txtsms[i].rel_addr_book = m[i].Groups["rel_addr_book"].Value;
                txtsms[i].date_rcv = Convert.ToDateTime( m[i].Groups["date_rcv"].Value);
                txtsms[i].message = m[i].Groups["message"].Value;
            }
            return txtsms;
        }

        public STXTSMS[] ReadTXTOutboxSMS(string txt, out int[] index, out string[] message_status)
        {
            /*
            +CMGL: 0,"STO SENT","89033292331",,
            0074006500730074
            */
            string pat1 = "\\+CMGL:\\s(?<index>[0-9]+),([\"|]*)(?<message_status>[0-9a-zA-Z\\s]+)([\"|]*),\"(?<address_text>.*)\",([\"|]*)(?<rel_addr_book>.*)([\"|]*),(\\r\\n)(?<message>.*)(\\r\\n)";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);
            MatchCollection m = reg1.Matches(txt);

            STXTSMS[] txtsms = new STXTSMS[m.Count];
            index = new int[m.Count];
            message_status = new string[m.Count];

            for (int i = 0; i < m.Count; i++)
            {
                index[i] = int.Parse(m[i].Groups["index"].Value);
                message_status[i] = m[i].Groups["message_status"].Value;
                txtsms[i].address_text = m[i].Groups["address_text"].Value;
                txtsms[i].rel_addr_book = m[i].Groups["rel_addr_book"].Value;
                txtsms[i].message = m[i].Groups["message"].Value;
            }
            return txtsms;
        }


        #endregion

        #region --- Формат строки PDU сообщений ---

        /// <summary>
        /// Структура принимаемого SMS в PDU-формате
        /// </summary>
        public struct RPDUSMS
        {
            /*
            07 91 9730071111F1 04 0B 91 9730232933F1 00 00 90408290434361 1C 31D98C56B3DD7039582C2693CD66345AAD66B3DD6E385C2E07
            */
            //НАЧ SCA
            public byte SMSC_num_len;//= 0;длина в октетах 2-х след полей  07
            //длина - размер SCA блока в байтах 
            //(невключая байт типа и длинны), если
            //длинна равна 0 то остальная SCA
            //информация отсутствует.
            public byte SMSC_type_of_num;//= (byte)0x91;//см.TypeOfNumber   91
            public string SMSC_num;//= string.Empty;//в формате BCD         9730071111F1
            //КОН SCA

            //НАЧ PDU Type
            public byte TPDU;//= 0x00;//По умолчанию для входящего письма   00
            //КОН PDU Type

            //НАЧ OA
            public byte OA_ref_num_len;//= 0x0B;//референсная длина         0B
            //телефона-источника, обычно 11 симв. CXXXZZZYYAA
            public byte OA_type_of_num;//= (byte)0x91;//см. TypeOfNumber    91
            //телефон-источник
            public string OA_num;//= string.Empty;//в формате BCD           9730232933F1
            //КОН OA

            //НАЧ PID 
            //идентификатор протокола - указывает SMSC,
            //как обрабатывать сообщение
            public byte PID;//= 0x00;//                                     00
            //идентификатор номера протокола, если не равен 0,
            //то должен быть равен 41..47 для того, чтобы замещать
            //сообщения с теми же номерами протокола (как в БИ+!!!)
            //#		41H:	Replace Short Message Type1
            //#		...
            //#		47H:	Replace Short Message Type7
            //#		Can be used to replace previously sent SMS messgaes in the MS (Mobile Station)
            //КОН PID
            //КОН PID

            //НАЧ DCS
            //Указывает какая кодировка применялась при
            //создании этого сообщения (это чтобы мы знали 
            //как его разкодировать) и заодно указывает 
            //класс сообщения.
            public byte DCS;//= 0x00;// 7-bit 160chr in 140pc bytes         00
            //0x08 - BigEndianUnicode (UTF7)
            //#	'F0' for 7bit, immediate display	'7biti'
            //#	'F6' for 8bit, SIM specific			'8bit'
            //#	'F4' for 8bit, immediate display	'8biti'
            //#	'F5' for 8bit, ME specific			'8bitm'

            //КОН DCS

            //НАЧ SCTS
            //Время получения SMS сервером провайдера
            public DateTime SCTS;//= "10101010000000";//                    90408290434361
            //КОН SCTS

            //НАЧ UDL
            //Длина поля UD (сообщения)
            public byte UDL;//= 0x01;//Длина "раскрытого" текста в байтах   1C
            //той кодировки, в которой SMS пришло
            /*
            7BIT 1C 31D98C56B3DD7039582C2693CD66345AAD66B3DD6E385C2E07
                                          1234567890112233445566778899
                                                                       
            UCS2 0C 041F 0440 0438 0432 0435 0442
                       П    р    и    в    е    т
            */
            //КОН UDL

            //НАЧ UD
            public string UD;//= "0C";//Текстовое сообщение                  31D98C56B3DD7039582C2693CD66345AAD66B3DD6E385C2E07
            //КОК UD
        }

        /// <summary>
        /// Структура отправленного SMS в PDU-формате
        /// </summary>
        public struct SPDUSMS
        {
            /*
            07 91 8350000005F1 31 00 0C 91 835010325467 00 08 AA 0С 041F04400438043204350442
            */
            //НАЧ SCA
            public byte SMSC_num_len;//= 0;длина в октетах 2-х след полей   07
            //длина - размер SCA блока в байтах 
            //(невключая байт длины), если
            //длинна равна 0 то остальная SCA
            //информация отсутствует.
            public byte SMSC_type_of_num;//= (byte)0x91;//см.TypeOfNumber   91
            public string SMSC_num;//= string.Empty;//в формате BCD         8350000005F1
            //КОН SCA

            //НАЧ PDU Type
            public byte TPDU;//= 0x01;//По умолчанию для исходящего письма  31
            //0001L - MTI: исходящий SMS плюс сообщение о доставке SMS и 
            //определение времени действия - 0011H
            //см. SetTPDU, также означает, что должны использовать
            //1 байт VP!!!. а было бы 01 - ничего бы не было :)
            //КОН PDU Type

            //НАЧ MR
            public byte MR;//= 0;//0.255 кол-во успешно отправленных SMS    00
            //КОН MR

            //НАЧ DA 
            //идентификатор получателя
            public byte DA_ref_len;//= 0x0B;// длина телефона получателя   0C
            //телефона-источника, обычно 11 симв. CXXXZZZYYAA
            public byte DA_type_of_num;//= (byte)0x91;//см. TypeOfNumber    91
            public string DA_num;//= string.Empty;//в формате BCD           835010325467
            //КОН DA

            //НАЧ PID 
            //идентификатор протокола - указывает SMSC,
            //как обрабатывать сообщение
            public byte PID;//= 0x00;//                                     00
            //идентификатор номера протокола, если не равен 0,
            //то должен быть равен 41..47 для того, чтобы замещать
            //сообщения с теми же номерами протокола (как в БИ+!!!)            
            //#		41H:	Replace Short Message Type1
            //#		...
            //#		47H:	Replace Short Message Type7
            //#		Can be used to replace previously sent SMS messgaes in the MS (Mobile Station)
            //КОН PID

            //НАЧ DCS
            //Указывает какая кодировка применялась при
            //создании этого сообщения (это чтобы мы знали 
            //как его разкодировать) и заодно указывает 
            //класс сообщения.
            public byte DCS;//= 0x00;// 7-bit 160chr in 140pc bytes         08
            //0x08 - BigEndianUnicode (UTF7)            
            //#	'F0' for 7bit, immediate display	'7biti'
            //#	'F6' for 8bit, SIM specific			'8bit'
            //#	'F4' for 8bit, immediate display	'8biti'
            //#	'F5' for 8bit, ME specific			'8bitm'
            //КОН DCS

            //НАЧ VP
            //время действия сообщения
            public byte VP;//= "0,1,7 байт";//                            AA
            //0 0 : TP-VP field not present
            //1 0 : TP-VP field present. Relative format (one octet)
            //0 1 : TP-VP field present. Enhanced format (7 octets)
            //1 1 : TP-VP field present. Absolute format (7 octets)
            //максимальное AA
            //КОН VP

            //НАЧ UDL
            //Длина поля UD (сообщения)
            public byte UDL;//= 0x01;//Длина "раскрытого" текста в байтах   0C
            //той кодировки, в которой SMS пришло
            /*
            7BIT 1C 31D98C56B3DD7039582C2693CD66345AAD66B3DD6E385C2E07
                                          1234567890112233445566778899
                                                                       
            UCS2 0C 041F 0440 0438 0432 0435 0442
                       П    р    и    в    е    т
            */
            //КОН UDL

            //НАЧ UD
            public string UD;//= "0C";//Текстовое сообщение                 041F04400438043204350442
            //КОК UD
        }

        /// <summary>
        /// Получить TPDU
        /// </summary>
        /// <returns></returns>
        private byte SetTPDU(bool isSend)
        {
            /*
            TODO: Вот такая структура, но она нам не нужна пока
            R:
            +------+----+------+-----+-----+----+-----+ 
            | биты |  7 |  6   | 5   | 4 3 | 2  | 1 0 | 
            +------+----+------+-----+-----+----+-----+ 
                   | RP | UDHI | SRI | n/a | MMS| MTI | 
                   +----+------+-----+-----+----+-----+
            
            RP (Reply Path) – 0: Информация об обратном путе отсутствует, 1: Информация об обратном путе присутствует 
            UDHI (User Data Header Information) – 0: В "UD" только сообщение, 1: начало "UD" содержит заголовок к сообщению (об этом мы обсудим в отдельной теме) 
            SRI (Status Report Inf) – требование приемника получить статус сообщения (0: нет запроса, 1: запрос)
            n/a - во входящем сообщении он не используется!
            MMS - количество не отправленных сообщений в SMSC
            MTI (Message Type) - тип сообщения: 00 - входящее, 01 - исходящее
            
            S:
            +------+----+------+-----+-----+----+-----+ 
            | биты |  7 |  6   | 5   | 4 3 | 2  | 1 0 | 
            +------+----+------+-----+-----+----+-----+ 
                   | RP | UDHI | SRR | VPF | RD | MTI | 
                   +----+------+-----+-----+----+-----+
            
            RP (Reply Path) – 0: Информация об обратном путе отсутствует, 1: Информация об обратном путе присутствует 
            UDHI (User Data Header Information) – 0: В "UD" только сообщение, 1: начало "UD" содержит заголовок к сообщению (об этом мы обсудим в отдельной теме) 
            SRR (Status Report Requirement) – 0: нет запроса на статус, 1: запрос на статус 
            VPF (Validity Period Format) - флаг наличия поля VP, во входящем сообщении он не используется!
            RD 	- удалить дубликаты
            MTI (Message Type) - тип сообщения: 00 - входящее, 01 - исходящее
            
            # RP:
	        #	Reply path : 0 = not set / 1 = set
	        # UDHI:
	        #	User data only contains short message : 0
	        #	User data contains a header :			1
	        # SRR:
	        #	Status report requested	:	0 = no / 1 = yes
	        # VPF:
	        #	Validity period field
	        #	0 0	:	Not set
	        #	0 1 :	Reserved
	        #	1 0	:	VP field present : relative (integer)
	        #	1 1 :	VP field present : absolute	(semi-octet)
	        # RD:
	        #	Reject (1)  or accept (0) an SMS in the SMSC with the same MR and DA from the same OA
	        # MTI:
	        #	Message type
	        #	0 0	:	SMS deliver SMSC -> MS
	        #	0 1	:	SMS Submit	MS->SMSC
	        #
	        # We default this field to: 00010001, which means
	        #	Validity period in relative format if $vp
	        #	SMSSubmit type of message
	        #	Accept the same message in the SMSC again
            */
            byte ret = 0x00;//А в Мегафоне 0x04 есть во входящем
            if (isSend)
            {
                ret = 0x01;
            }
            return ret;
        }

        /// <summary>
        /// Получить DCS
        /// </summary>
        /// <param name="isUnicode">Юникод?</param>
        /// <returns></returns>
        private byte SetDCS(bool isUnicode)
        {
            /*
            +------+------------------+---+---------+ 
            | биты |          7 6 5 4 | 3 |   2 1 0 | 
            +------+------------------+---+---------+ 
                   | Группа кодировки | 0 |   X X X | 
                   +------------------+---+---------+
            */
            //Здесь берем две возможности: UTB и 7bit default alphabet
            byte ret = 0x00;
            if (isUnicode)
            {
                ret = 0x08;
            }
            //А вообще так
            /*        
	        ###########################################################################
	        # 5) Get data coding scheme
	        # -------------------------------------------------------------------------
	        # Structure:
	        #	bits	    7 6 5 4      3   2   1   0
	        #           +--------------+---+---+---+---+
	        #			| Coding group | 0 | X | X | X |	
	        #           +--------------+---+---+---+---+	
	        # Examples:
	        #				0 0 0 0      0   0   0   0		: 00H	: 7-bit datacoding, default alphabet
	        #				1 1 1 1      0   1   1   0		: F6H	: 8-bit datacoding Class 2
	        #
	        # Coding group	 |	Alphabet indication
	        # ---------------+---------------------------------------------------------
	        # 	0000         | 0000		Default alphabet
	        #                | 0001		Reserved
	        #                | ...		"    " 
	        #                | 1111		"    "
	        # ---------------+---------------------------------------------------------
	        #   0001-1110    | Reserved coding groups
	        # ---------------+---------------------------------------------------------
	        #   1111		 | bit 3 		: Reserved, always 0
	        #                | bit 2 		: Data Coding
	        #				 |					0	:	Default alphabet (7bit)
	        #				 |					1	:	8 bit encoding INTEL-ASCII
	        #                | bits 1 0 	: Message Class
	        #                |					0 0	:	Class 0, immedidate display
	        #                |                  0 1 :	Class 1, ME specific	(Mobile Equiment)
	        #				 |					1 0 :	Class 2, SIM specific	
	        #				 |					1 1 :	Class 3, TE specific    (Terminate Equipment)
	        # ---------------+---------------------------------------------------------
	        #   We have 2 possible ways of interpreting this for our SMS software
	        #	7 bit default alphabet	:	00000000	111100xx
	        #	8 bit intel-ascii       :	111101xx
	        #		x being a wild card
	        ###########################################################################
            */
            return ret;
        }

        #endregion

        #region --- Формат текстовой строки сообщений ---

        /// <summary>
        /// Структура принимаемого SMS в текст-формате
        /// </summary>
        /// 
        public struct RTXTSMS
        {
            /*
             +CMGL: 25,"REC READ","+79271175932",,"09/12/16,16:07:37+12"
             SHS ERA-3 CALENDAR: OFF
            */
            /// <summary>
            /// Адрес отправителя
            /// </summary>
            public string address_text;//=string.Empty;
            /// <summary>
            /// Информация, связанная с адр. книгой
            /// </summary>
            public string rel_addr_book;//=string.Empty;
            /// <summary>
            /// Время приема
            /// </summary>
            public DateTime date_rcv;
            /// <summary>
            /// Текст сообщения
            /// </summary>
            public string message;
        }

        /// <summary>
        /// Структура отправленного SMS в текст-формате
        /// </summary>
        /// 
        public struct STXTSMS
        {
            /*
            +CMGL: 0,"STO SENT","89033292331",,
            0074006500730074
            */
            /// <summary>
            /// Адрес отправителя
            /// </summary>
            public string address_text;//=string.Empty;
            /// <summary>
            /// Информация, связанная с адр. книгой
            /// </summary>
            public string rel_addr_book;//=string.Empty;
            /// <summary>
            /// Текст сообщения
            /// </summary>
            public string message;
        }

        #endregion

        #region -------- Вспомогательные --------
        /// <summary>
        /// Сбор данных из буфера, пока не встретится последовательность
        /// </summary>
        /// <param name="contains">Последовательность</param>
        /// <param name="reply">Ответ</param>
        /// <returns>Успех/Отказ</returns>
        bool WaitBeforeContains(string contains, out string reply)
        {
            DateTime timeout = DateTime.Now.AddSeconds(4);
            string buffer = "";
            do
            {
                buffer += port.ReadExisting();
                if (DateTime.Now > timeout || buffer.Contains("ERROR\r\n"))
                {
                    reply = buffer;
                    return false;
                }
            } while (!buffer.Contains(contains));
            reply = buffer;

            if (ReadBuffer != null)
            {
                ReadBuffer.Invoke(buffer);
            }
            return true;
        }

        /// <summary>
        /// Обертка над WaitBeforeContains
        /// </summary>
        /// <param name="DoneString">Предпологаемый ответ (OK, ERROR, VCON и др.)</param>
        /// <returns>Присланное сообщение</returns>
        public string ReadFromPort(string DoneString)
        {
            string message = string.Empty;
            WaitBeforeContains(DoneString, out message);
            return message;
        }

        /// <summary>
        /// Обертка над WaitBeforeContains
        /// </summary>
        /// <returns>Присланное сообщение</returns>
        public string ReadFromPort()
        {
            string message = string.Empty;
            WaitBeforeContains("OK", out message);
            return message;
        }

        /// <summary>
        /// Когда данные приходят в UCS2, то телефонный номер надо расшифровать таким образом
        /// </summary>
        /// <param name="number">номер в UCS2</param>
        /// <returns></returns>
        public string GetNumberFromBEU(string number)
        {
            int len = number.Length;
            if (len % 2 > 0)
            {
                throw new ArgumentException("Длина строки не кратна 2-м");
            }
            else
            {
                int len_num = (int)Math.Truncate(Convert.ToDouble( len / 2));
                byte[] numb = new byte[len_num];
                for (int i = 0; i < len_num; i++)
                {
                    string nextb = number.Substring(i * 2, 2);
                    numb[i] = Convert.ToByte(nextb, 16);
                }
                return Encoding.BigEndianUnicode.GetString(numb);
            }
        }

        /// <summary>
        /// Конвертирование даты SMS
        /// </summary>
        /// <param name="time">Дата SMS в текстовом виде 09/04/28,09:42:34+16</param>
        /// <returns>Дата</returns>
        public DateTime GetTimeFromText(string time)
        {
            string pat1 = @"(?<year>[0-9]+)\/(?<mon>[0-9]+)\/(?<day>[0-9]+),(?<hr>[0-9]+):(?<min>[0-9]+):(?<sec>[0-9]+)\+(?<shift>[0-9]+)";
            Regex reg1 = new Regex(pat1, RegexOptions.IgnoreCase);
            //string buffer = "09/04/28,09:42:34+16";//ReadFromPort();

            Match m = reg1.Match(time);
            int year = int.Parse(m.Groups["year"].Value)+2000;
            int mon = int.Parse(m.Groups["mon"].Value);
            int day = int.Parse(m.Groups["day"].Value);
            int hr = int.Parse(m.Groups["hr"].Value);
            int min = int.Parse(m.Groups["min"].Value);
            int sec = int.Parse(m.Groups["sec"].Value);
            int shift = int.Parse(m.Groups["shift"].Value);

            DateTime dt = new DateTime(year,mon,day,hr,min,sec);
            return dt;
        }
        
        #endregion

        #region -------- Математические --------
        public string IntToHex(int number, string cap)
        {
            return String.Format("{0:X" + cap + "}", number);
        }

        public int HexToInt(string hexString)
        {
            return int.Parse(hexString,
                System.Globalization.NumberStyles.HexNumber, null);
        }

        public string BinToHex(string sbin, string cap)
        {
            int res = 0;
            for (int i = 0; i < sbin.Length; i++)
            {
                int m = Convert.ToInt32(Math.Pow(2, sbin.Length - i - 1));
                int cn = int.Parse(Convert.ToString(sbin[i]));
                res += cn * m;
            }
            return IntToHex(res, cap);
        }

        public string HexToBin(string shex, int cap)
        {
            string bin = string.Empty;
            shex = shex.ToUpper();
            string[] bin_array = new string[] {    "0",    "1",   "10",   "11",  "100",  "101",  "110",  "111",
                                                "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };
            for (int i = 0; i < shex.Length; i++)
            {
                string c_bin = bin_array[Convert.ToInt32(shex[shex.Length - i - 1].ToString(), 16)];
                while (c_bin.Length < cap)
                {
                    c_bin = "0" + c_bin;
                }
                bin = c_bin + bin;
            }
            return bin;
        }
        #endregion


    }
}
