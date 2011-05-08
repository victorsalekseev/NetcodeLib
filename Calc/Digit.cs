using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Netcode.Common.Calc
{
    public class Digit
    {
        public Digit()
        {
        }

        /// <summary>
        /// Проверка поля на ввод только чисел, дробного разделителя и знака -
        /// </summary>
        /// <returns></returns>
        public static bool IsRealDigitInput(string tb)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            return Regex.IsMatch(tb, "^[0-9" + ci.NumberFormat.CurrencyDecimalSeparator + "\\-]+$", RegexOptions.Singleline);
        }

        /// <summary>
        /// Проверка поля на ввод только целых чисел и знака -
        /// </summary>
        /// <returns></returns>
        public static bool IsIntegerDigitInput(string tb)
        {
            return Regex.IsMatch(tb, "^[0-9\\-]+$", RegexOptions.Singleline);
        }

        /// <summary>
        /// Вернуть число с правильным дробным разделитем
        /// </summary>
        /// <returns></returns>
        public static string ConvertDigitMitDecimalSeparator(string tb)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            return tb.Replace(",", ci.NumberFormat.CurrencyDecimalSeparator).Replace(".", ci.NumberFormat.CurrencyDecimalSeparator);
        }
    }
}
