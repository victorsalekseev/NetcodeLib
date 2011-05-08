using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Hashing
{
    public class MD5Wrapper
    {
        public MD5Wrapper()
        {
        }

        /// <summary>
        /// Расчет хеш-сигнатуры
        /// </summary>
        /// <param name="inByte">Массив байт</param>
        /// <returns>Хеш-сигнатура</returns>
        public string GetMd5Hash(byte[] inByte)
        {
            Netcode.Hashing.MD5 md = new Netcode.Hashing.MD5();
            md.ValueAsByte = inByte;
            return md.FingerPrint;
        }

        /// <summary>
        /// Анализ файла
        /// </summary>
        /// <param name="inByte">Массив байт</param>
        /// <returns>Хеш-сигнатура</returns>
        public string Analysing(byte[] inByte)
        {
            return GetMd5Hash(inByte);
        }
    }
}
