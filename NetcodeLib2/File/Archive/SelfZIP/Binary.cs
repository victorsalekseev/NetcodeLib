
#region  ласс AcedBinary
/*
--------------------------------------------------------------------------------
public sealed class AcedBinary

  —татические методы дл€ работы с бинарными данными.

--------------------------------------------------------------------------------
ћетоды
--------------------------------------------------------------------------------

public static uint SwapBytes(uint n)

  ќбращает пор€док следовани€ байт в значении n типа System.UInt32.

public static uint ReverseBits(uint n, int bits)

  ќбращает пор€док следовани€ битов в значении n типа System.UInt32, размер
  которого в битах задаетс€ параметром bits (значение от 1 до 32).

public static int Adler32(byte[] bytes, int offset, int length)

  ¬ычисл€ет контрольную сумму јдлера в соответствии с RFC 1950 дл€ фрагмента
  массива bytes, начина€ с индекса offset, длиной length байт.

public static void CopyBytes(byte* ps, byte* pd, int length)

   опирует length байт из массива, адресуемого указателем ps, в массив,
  адресуемый указателем pd. ћассивы могут перекрыватьс€, но копирование
  всегда выполн€етс€ от младших байт к старшим. 

public static void Fill(int value, int* p, int length)

  «аполн€ет length элементов типа Int32 массива, адресуемого указателем p,
  значением value.

public static void Copy(int* ps, int* pd, int length)

   опирует length элементов типа Int32 из массива, адресуемого указателем ps,
  в массив, адресуемый указателем pd.  опирование выполн€етс€ от первых
  элементов массива к последним.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedBinary class

    public sealed class AcedBinary
    {
        [CLSCompliant(false)]
        public static uint SwapBytes(uint n)
        {
            return ((n & 0xFF) << 24) | ((n & 0xFF00) << 8) | ((n & 0xFF0000) >> 8) | (n >> 24);
        }

        [CLSCompliant(false)]
        public static uint ReverseBits(uint n, int bits)
        {
            n = ((n & 0x0000FFFF) << 16) | (n >> 16);
            n = ((n & 0x00FF00FF) << 8) | ((n & 0xFF00FF00u) >> 8);
            n = ((n & 0x0F0F0F0F) << 4) | ((n & 0xF0F0F0F0u) >> 4);
            n = ((n & 0x33333333) << 2) | ((n & 0xCCCCCCCCu) >> 2);
            n = ((n & 0x55555555) << 1) | ((n & 0xAAAAAAAAu) >> 1);
            return n >> (32 - bits);
        }

        public static unsafe int Adler32(byte[] bytes, int offset, int length)
        {
            if (bytes == null || length == 0)
                return 1;
            uint s1 = 1;
            uint s2 = 0;
            fixed (byte* pBytes = &bytes[offset])
            {
                byte* p = pBytes;
                while (length > 0)
                {
                    int k = length < 5552 ? length : 5552;
                    length -= k;
                    while (k >= 16)
                    {
                        s1 += *p; s2 += s1;
                        s1 += *(p + 1); s2 += s1;
                        s1 += *(p + 2); s2 += s1;
                        s1 += *(p + 3); s2 += s1;
                        s1 += *(p + 4); s2 += s1;
                        s1 += *(p + 5); s2 += s1;
                        s1 += *(p + 6); s2 += s1;
                        s1 += *(p + 7); s2 += s1;
                        s1 += *(p + 8); s2 += s1;
                        s1 += *(p + 9); s2 += s1;
                        s1 += *(p + 10); s2 += s1;
                        s1 += *(p + 11); s2 += s1;
                        s1 += *(p + 12); s2 += s1;
                        s1 += *(p + 13); s2 += s1;
                        s1 += *(p + 14); s2 += s1;
                        s1 += *(p + 15); s2 += s1;
                        p += 16;
                        k -= 16;
                    }
                    while (k > 0)
                    {
                        s1 += *p; s2 += s1;
                        k--;
                        p++;
                    }
                    s1 %= 65521u;
                    s2 %= 65521u;
                }
            }
            return (int)(s1 | (s2 << 16));
        }

        [CLSCompliant(false)]
        public static unsafe void CopyBytes(byte* ps, byte* pd, int length)
        {
            while (length >= 16)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
                pd[3] = ps[3];
                pd[4] = ps[4];
                pd[5] = ps[5];
                pd[6] = ps[6];
                pd[7] = ps[7];
                pd[8] = ps[8];
                pd[9] = ps[9];
                pd[10] = ps[10];
                pd[11] = ps[11];
                pd[12] = ps[12];
                pd[13] = ps[13];
                pd[14] = ps[14];
                pd[15] = ps[15];
                length -= 16;
                ps += 16;
                pd += 16;
            }
            while (length >= 4)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
                pd[3] = ps[3];
                length -= 4;
                ps += 4;
                pd += 4;
            }
            if (length < 2)
            {
                if (length == 1)
                    pd[0] = ps[0];
            }
            else if (length == 2)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
            }
            else
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
            }
        }

        [CLSCompliant(false)]
        public static unsafe void Fill(int value, int* p, int length)
        {
            while (length >= 16)
            {
                p[0] = value;
                p[1] = value;
                p[2] = value;
                p[3] = value;
                p[4] = value;
                p[5] = value;
                p[6] = value;
                p[7] = value;
                p[8] = value;
                p[9] = value;
                p[10] = value;
                p[11] = value;
                p[12] = value;
                p[13] = value;
                p[14] = value;
                p[15] = value;
                length -= 16;
                p += 16;
            }
            while (length >= 4)
            {
                p[0] = value;
                p[1] = value;
                p[2] = value;
                p[3] = value;
                length -= 4;
                p += 4;
            }
            if (length < 2)
            {
                if (length == 1)
                    p[0] = value;
            }
            else if (length == 2)
            {
                p[0] = value;
                p[1] = value;
            }
            else
            {
                p[0] = value;
                p[1] = value;
                p[2] = value;
            }
        }

        [CLSCompliant(false)]
        public static unsafe void Copy(int* ps, int* pd, int length)
        {
            while (length >= 16)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
                pd[3] = ps[3];
                pd[4] = ps[4];
                pd[5] = ps[5];
                pd[6] = ps[6];
                pd[7] = ps[7];
                pd[8] = ps[8];
                pd[9] = ps[9];
                pd[10] = ps[10];
                pd[11] = ps[11];
                pd[12] = ps[12];
                pd[13] = ps[13];
                pd[14] = ps[14];
                pd[15] = ps[15];
                length -= 16;
                ps += 16;
                pd += 16;
            }
            while (length >= 4)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
                pd[3] = ps[3];
                length -= 4;
                ps += 4;
                pd += 4;
            }
            if (length < 2)
            {
                if (length == 1)
                    pd[0] = ps[0];
            }
            else if (length == 2)
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
            }
            else
            {
                pd[0] = ps[0];
                pd[1] = ps[1];
                pd[2] = ps[2];
            }
        }

        private AcedBinary() { }
    }
}
