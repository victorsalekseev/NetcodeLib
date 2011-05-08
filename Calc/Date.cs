using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Common.Calc
{
    public class Date
    {
        public Date()
        {
        }

        public static int GetKvartal(int month)
        {
            int kv = 0;
            switch (month)
            {
                case 1:
                    {
                        kv = 1;
                    }
                    break;
                case 2:
                    {
                        kv = 1;
                    }
                    break;
                case 3:
                    {
                        kv = 1;
                    }
                    break;
                case 4:
                    {
                        kv = 2;
                    }
                    break;
                case 5:
                    {
                        kv = 2;
                    }
                    break;
                case 6:
                    {
                        kv = 2;
                    }
                    break;
                case 7:
                    {
                        kv = 3;
                    }
                    break;
                case 8:
                    {
                        kv = 3;
                    }
                    break;
                case 9:
                    {
                        kv = 3;
                    }
                    break;
                case 10:
                    {
                        kv = 4;
                    }
                    break;
                case 11:
                    {
                        kv = 4;
                    }
                    break;
                case 12:
                    {
                        kv = 4;
                    }
                    break;
                default:
                    break;
            }
            return kv;
        }
    }
}
