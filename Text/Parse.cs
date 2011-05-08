using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Netcode.Common.Text
{
    public class Parse
    {
        /// <summary>
        /// Удаление спецсимволов из текста запроса в базу данных
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveSpecDBsymbols(string text)
        {
            return Regex.Replace(text, "[\"'\\[\\]\\?\\(\\)\\+]", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
}
