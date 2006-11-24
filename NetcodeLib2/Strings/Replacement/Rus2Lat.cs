using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Netcode.Strings.Replacement
{
    public class Rus2Lat
    {
        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="text">�����</param>
        /// <param name="Rus2Lat">true: ���-���, false: ���-���</param>
        /// <param name="RegexOptions">RegexOptions</param>
        /// <returns>�����</returns>
        public string ReplaceItemText(string text, bool Rus2Lat, RegexOptions RegexOptions)
        {
            string[] rus = new string[] { "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", " " };
            string[] lat = new string[] { "E", "Y", "C", "U", "K", "E", "N", "G", "SH", "SC", "Z", "H", "", "F", "S", "F", "Y", "V", "A", "P", "R", "O", "L", "D", "G", "E", "YA", "CH", "S", "M", "I", "T", "", "B", "Y", "e", "y", "c", "u", "k", "e", "n", "g", "sh", "sc", "z", "h", "", "f", "s", "f", "y", "v", "a", "p", "r", "o", "l", "d", "g", "e", "ya", "ch", "s", "m", "i", "t", "", "b", "y", "_" };
            //string[] err = new string[] { @"`", @"~", @"!", @"@", @"#", @"$", @"%", @"^", @"&", @"*", @"(", @")", @"-", @"+", @"=", @"[", @"]", @";", @":", @"/", @"<", @">", @"'", @",", @".", @"\", @"|", @"�", @"?", @"*" };
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
