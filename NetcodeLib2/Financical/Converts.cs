using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Netcode.Financical
{
    #region ����� �������� � �.�.

    public class RusNumber
    {
        private static string[] hunds =
        {
            "", "��� ", "������ ", "������ ", "��������� ",
            "������� ", "�������� ", "������� ", "��������� ", "��������� "
        };

        private static string[] tens =
        {
            "", "������ ", "�������� ", "�������� ", "����� ", "��������� ",
            "���������� ", "��������� ", "����������� ", "��������� "
        };

        public static string Str(int val, bool male, string one, string two, string five)
        {
            string[] frac20 =
            {
                "", "���� ", "��� ", "��� ", "������ ", "���� ", "����� ",
                "���� ", "������ ", "������ ", "������ ", "����������� ",
                "���������� ", "���������� ", "������������ ", "���������� ",
                "����������� ", "���������� ", "������������ ", "������������ "
            };

            int num = val % 1000;
            if (0 == num) return "";
            if (num < 0) throw new ArgumentOutOfRangeException("val", "�������� �� ����� ���� �������������");
            if (!male)
            {
                frac20[1] = "���� ";
                frac20[2] = "��� ";
            }

            StringBuilder r = new StringBuilder(hunds[num / 100]);

            if (num % 100 < 20)
            {
                r.Append(frac20[num % 100]);
            }
            else
            {
                r.Append(tens[num % 100 / 10]);
                r.Append(frac20[num % 10]);
            }

            r.Append(Case(num, one, two, five));

            if (r.Length != 0) r.Append(" ");
            return r.ToString();
        }

        public static string Case(int val, string one, string two, string five)
        {
            int t = (val % 100 > 20) ? val % 10 : val % 20;

            switch (t)
            {
                case 1: return one;
                case 2:
                case 3:
                case 4: return two;
                default: return five;
            }
        }
    };

    struct CurrencyInfo
    {
        public bool male;
        public string seniorOne, seniorTwo, seniorFive;
        public string juniorOne, juniorTwo, juniorFive;
    };

    public class RusCurrencySectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            foreach (XmlNode curr in section.ChildNodes)
            {
                if (curr.Name == "currency")
                {
                    XmlNode senior = curr["senior"];
                    XmlNode junior = curr["junior"];
                    RusCurrency.Register(
                        curr.Attributes["code"].InnerText,
                        (curr.Attributes["male"].InnerText == "1"),
                        senior.Attributes["one"].InnerText,
                        senior.Attributes["two"].InnerText,
                        senior.Attributes["five"].InnerText,
                        junior.Attributes["one"].InnerText,
                        junior.Attributes["two"].InnerText,
                        junior.Attributes["five"].InnerText);
                }
            }
            return null;
        }
    };

    /// <summary>
    /// ��������, ��� ������� ����� RusCurrency.Str(summa, "RUR");
    /// </summary>
    public class RusCurrency
    {
        private static HybridDictionary currencies = new HybridDictionary();

        static RusCurrency()
        {
            Register("RUR", true, "�����", "�����", "������", "�������", "�������", "������");
            Register("EUR", true, "����", "����", "����", "��������", "���������", "����������");
            Register("USD", true, "������", "�������", "��������", "����", "�����", "������");
            ConfigurationSettings.GetConfig("currency-names");
        }

        public static void Register(string currency, bool male,
            string seniorOne, string seniorTwo, string seniorFive,
            string juniorOne, string juniorTwo, string juniorFive)
        {
            CurrencyInfo info;
            info.male = male;
            info.seniorOne = seniorOne; info.seniorTwo = seniorTwo; info.seniorFive = seniorFive;
            info.juniorOne = juniorOne; info.juniorTwo = juniorTwo; info.juniorFive = juniorFive;
            currencies.Add(currency, info);
        }

        public static string Str(double val)
        {
            return Str(val, "RUR");
        }

        public static string Str(double val, string currency)
        {
            if (!currencies.Contains(currency))
                throw new ArgumentOutOfRangeException("currency", "������ \"" + currency + "\" �� ����������������");

            CurrencyInfo info = (CurrencyInfo)currencies[currency];
            return Str(val, info.male,
                info.seniorOne, info.seniorTwo, info.seniorFive,
                info.juniorOne, info.juniorTwo, info.juniorFive);
        }

        public static string Str(double val, bool male,
            string seniorOne, string seniorTwo, string seniorFive,
            string juniorOne, string juniorTwo, string juniorFive)
        {
            bool minus = false;
            if (val < 0) { val = -val; minus = true; }

            int n = (int)val;
            int remainder = (int)((val - n + 0.005) * 100);

            StringBuilder r = new StringBuilder();

            if (0 == n) r.Append("0 ");
            if (n % 1000 != 0)
                r.Append(RusNumber.Str(n, male, seniorOne, seniorTwo, seniorFive));
            else
                r.Append(seniorFive);

            n /= 1000;

            r.Insert(0, RusNumber.Str(n, false, "������", "������", "�����"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "�������", "��������", "���������"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "��������", "���������", "����������"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "��������", "���������", "����������"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "���������", "����������", "�����������"));
            if (minus) r.Insert(0, "����� ");

            r.Append(remainder.ToString(" 00 "));
            r.Append(RusNumber.Case(remainder, juniorOne, juniorTwo, juniorFive));

            //������ ������ ����� ���������
            r[0] = char.ToUpper(r[0]);

            return r.ToString();
        }
    };

    public class Converts
    {
        public string cast(string cena)
        {
            string result = "";
            Regex r = new Regex("([.,])"); // Split on hyphens.
            if (r.IsMatch(cena))
            {
                string[] s = r.Split(cena);
                if ((s[2].ToString().Length == 0))
                {
                    result = s[2].ToString() + "00";
                }
                if ((s[2].ToString().Length == 1))
                {
                    result = s[2].ToString() + "0";
                }
                if ((s[2].ToString().Length == 2))
                {
                    result = s[2].ToString();
                }
                if ((s[2].ToString().Length > 2))
                {
                    result = s[2].Remove(2);
                }
                //return s[0].ToString() + "," + result;
                decimal round = 0;
                try
                {
                    round = Math.Round(Convert.ToDecimal(s[0].ToString() + "," + result), 2);
                }
                catch (FormatException)
                {
                    round = Math.Round(Convert.ToDecimal(s[0].ToString() + "." + result), 2);
                }

                return round.ToString();
            }
            else
            {
                return cena + ",00";
            }

        }
    }

    #endregion

}
