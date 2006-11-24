using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Netcode.Strings.Replacement
{
    public class Rus2Lat
    {
        /// <summary>
        /// Транслитерация
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="Rus2Lat">true: РУС-ЛАТ, false: ЛАТ-РУС</param>
        /// <param name="RegexOptions">RegexOptions</param>
        /// <returns>Выход</returns>
        public string ReplaceItemText(string text, bool Rus2Lat, RegexOptions RegexOptions)
        {
            string[] rus = new string[] { "Ё", "Й", "Ц", "У", "К", "Е", "Н", "Г", "Ш", "Щ", "З", "Х", "Ъ", "Ф", "С", "Ф", "Ы", "В", "А", "П", "Р", "О", "Л", "Д", "Ж", "Э", "Я", "Ч", "С", "М", "И", "Т", "Ь", "Б", "Ю", "ё", "й", "ц", "у", "к", "е", "н", "г", "ш", "щ", "з", "х", "ъ", "ф", "с", "ф", "ы", "в", "а", "п", "р", "о", "л", "д", "ж", "э", "я", "ч", "с", "м", "и", "т", "ь", "б", "ю", " " };
            string[] lat = new string[] { "E", "Y", "C", "U", "K", "E", "N", "G", "SH", "SC", "Z", "H", "", "F", "S", "F", "Y", "V", "A", "P", "R", "O", "L", "D", "G", "E", "YA", "CH", "S", "M", "I", "T", "", "B", "Y", "e", "y", "c", "u", "k", "e", "n", "g", "sh", "sc", "z", "h", "", "f", "s", "f", "y", "v", "a", "p", "r", "o", "l", "d", "g", "e", "ya", "ch", "s", "m", "i", "t", "", "b", "y", "_" };
            //string[] err = new string[] { @"`", @"~", @"!", @"@", @"#", @"$", @"%", @"^", @"&", @"*", @"(", @")", @"-", @"+", @"=", @"[", @"]", @";", @":", @"/", @"<", @">", @"'", @",", @".", @"\", @"|", @"№", @"?", @"*" };
            string rept = string.Empty;
            
            if(Rus2Lat)
            {
                for (int i = 0; i < rus.Length; i++)
                {
                    rept = Regex.Replace(rept, rus[i], lat[i], RegexOptions);
                }
            }
            else
            {
                for (int i = 0; i < rus.Length; i++)
                {
                    rept = Regex.Replace(rept, lat[i], rus[i], RegexOptions);
                }
            }
            return rept;
        }
    }
}
