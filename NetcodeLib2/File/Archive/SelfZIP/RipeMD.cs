
#region Класс AcedRipeMD
/*
--------------------------------------------------------------------------------
public sealed class AcedRipeMD

  Используется для вычисления значения односторонней хэш-функции RipeMD-160
  бинарных данных или строки символов. Кроме того, включает методы для
  копирования и сравнения цифровой сигнатуры, преобразования сигнатуры
  в значение типа Guid, очистки соответствующего массива.

--------------------------------------------------------------------------------
Методы для поточного расчета цифровой сигнатуры RipeMD-160
--------------------------------------------------------------------------------

public static int[] Initialize()

  Инициализирует расчет цифровой сигнатуры RipeMD-160. Функция возвращает
  служебный массив, который затем передается в качестве параметра hashData
  в другие функции данного класса.

public static void Initialize(int[] hashData)

  Инициализирует расчет цифровой сигнатуры RipeMD-160. В качестве параметра
  hashData функция принимает служебный массив, который, возможно, использовался
  раньше для расчета сигнатуры RipeMD-160. Это позволяет избежать лишнего
  перераспределения памяти. После инициализации данный массив может
  передаваться в качестве параметра hashData в другие функции данного класса.

public static void Initialize(int[] hashData, int[] baseHash)

  Инициализирует расчет цифровой сигнатуры RipeMD-160. В качестве параметра
  функция принимает служебный массив hashData, который, возможно, использовался
  раньше для расчета сигнатуры RipeMD-160. Это позволяет избежать лишнего
  перераспределения памяти. Кроме того, метод принимает в качестве параметра
  значение цифровой сигнатуры baseHase, которое используется в качестве
  начального вектора при инициализации служебного массива. После инициализации
  массив hashData может передаваться в качестве одноименного параметра в другие
  функции данного класса.

public static void Update(int[] hashData, byte* bytes, int length)

  Вычисляет цифровую сигнатуру RipeMD-160 для массива байт, адресуемого
  параметром bytes, длиной length байт. При этом изменяется состояние
  служебного массива, который передается параметром hashData. Чтобы получить
  готовое значение цифровой сигнатуры, следует обратиться к методу Finalize.

public static void Update(int[] hashData, byte[] bytes, int offset, int length)

  Вычисляет цифровую сигнатуру RipeMD-160 для фрагмента массива байт bytes,
  начиная со смещения offset, длиной length байт. При этом изменяется состояние
  служебного массива, который передается параметром hashData. Чтобы получить
  готовое значение цифровой сигнатуры, следует обратиться к методу Finalize.

public static void Update(int[] hashData, string s)

  Вычисляет цифровую сигнатуру RipeMD-160 для строки s. При этом изменяется
  состояние служебного массива hashData. Чтобы получить готовое значение
  цифровой сигнатуры, следует обратиться к методу Finalize.

public static int[] Finalize(int[] hashData)

  Возвращает значение цифровой сигнатуры RipeMD-160 в виде массива из
  5 элементов типа System.Int32, соответствующее текущему состоянию служебного
  массива, передаваемого параметром hashData. Сам служебный массив при этом
  обнуляется.

public static void Finalize(int[] hashData, int[] resultHash)

  Возвращает значение цифровой сигнатуры RipeMD-160 в параметре resultHash,
  который представляет собой ссылку на массив из 5 элементов типа System.Int32.
  Значение цифровой сигнатуры соответствует текущему состоянию служебного
  массива, передаваемого параметром hashData. Сам служебный массив при этом
  обнуляется.

--------------------------------------------------------------------------------
Методы для расчета сигнатуры RipeMD-160 для одного фрагмента данных
--------------------------------------------------------------------------------

public static int[] NoDataHash()
  
  Возвращает значение хэш-функции RipeMD-160 для пустого массива данных.

public static int[] Compute(byte* bytes, int length)

  Вычисляет цифровую сигнатуру RipeMD-160 для массива байт, адресуемого
  параметром bytes длиной length байт. Рассчитанная сигнатура возвращается
  в виде массива из 5 элементов типа System.Int32.

public static int[] Compute(byte[] bytes, int offset, int length)

  Вычисляет цифровую сигнатуру RipeMD-160 для фрагмента массива байт bytes,
  начиная с индекса offset, длиной length байт. Рассчитанная сигнатура
  возвращается в виде массива из 5 элементов типа System.Int32.

public static int[] Compute(string s)

  Возвращает 20-байтную цифровую сигнатуру RipeMD-160 для строки s в виде
  массива из 5 элементов типа System.Int32.

--------------------------------------------------------------------------------
Прочие методы
--------------------------------------------------------------------------------

public static Guid ToGuid(int[] hash)

  Возвращает значение типа System.Guid, соответствующее цифровой сигнатуре
  из 5 элементов типа System.Int32, переданной параметром hash.

public static void Copy(int[] sourceHash, byte[] bytes, int offset)

  Копирует 20-байтную цифровую сигнатуру из массива sourceHash, состоящего из
  5 элементов типа System.Int32, в массив байт bytes, начиная с индекса offset.

public static void Copy(byte[] bytes, int offset, int[] destinationHash)

  Копирует 20-байтную цифровую сигнатуру из массива байт bytes, начиная
  с индекса offset, в массив destinationHash, состоящего из 5 элементов типа
  System.Int32.

public static void Copy(int[] sourceHash, int[] destinationHash)

  Копирует 20-байтную цифровую сигнатуру из массива sourceHash в массив
  destinationHash. Оба массива должны состоять из 5 элементов типа
  System.Int32.

public static bool Equals(int[] hash, byte[] bytes, int offset)

  Сравнивает по байтам цифровую сигнатуру, находящуюся в массиве hash,
  состоящем из 5 элементов типа System.Int32, с фрагментом массива байт bytes,
  начиная с индекса offset. Функция возвращает True, если первые 20 байт
  обоих фрагментов равны, иначе возвращает False.

public static bool Equals(int[] hash1, int[] hash2)

  Сравнивает по байтам цифровую сигнатуру, находящуюся в массиве hash1,
  с цифровой сигнатуров в массиве hash2. Предполагается, что оба массива
  состоят из 5 элементов типа System.Int32. Функция возвращает True, если
  элементы обоих массивов равны, иначе возвращает False.

public static void Clear(int[] hash)

  Очищает цифровую сигнатуру в массиве hash, который состоит из 5 элементов
  типа System.Int32.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedRipeMD class

    public sealed class AcedRipeMD
    {
        private static unsafe void TransformBlock(uint* hash, uint* x)
        {
            uint a = hash[0];
            uint b = hash[1];
            uint c = hash[2];
            uint d = hash[3];
            uint e = hash[4];

            #region left line
            a += (b ^ c ^ d) + x[0];
            a = ((a << 11) | (a >> 21)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[1];
            e = ((e << 14) | (e >> 18)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[2];
            d = ((d << 15) | (d >> 17)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[3];
            c = ((c << 12) | (c >> 20)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[4];
            b = ((b << 5) | (b >> 27)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[5];
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[6];
            e = ((e << 7) | (e >> 25)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[7];
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[8];
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[9];
            b = ((b << 13) | (b >> 19)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[10];
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[11];
            e = ((e << 15) | (e >> 17)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[12];
            d = ((d << 6) | (d >> 26)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[13];
            c = ((c << 7) | (c >> 25)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[14];
            b = ((b << 9) | (b >> 23)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[15];
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[7] + 0x5a827999u;
            e = ((e << 7) | (e >> 25)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[4] + 0x5a827999u;
            d = ((d << 6) | (d >> 26)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[13] + 0x5a827999u;
            c = ((c << 8) | (c >> 24)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[1] + 0x5a827999u;
            b = ((b << 13) | (b >> 19)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[10] + 0x5a827999u;
            a = ((a << 11) | (a >> 21)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[6] + 0x5a827999u;
            e = ((e << 9) | (e >> 23)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[15] + 0x5a827999u;
            d = ((d << 7) | (d >> 25)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[3] + 0x5a827999u;
            c = ((c << 15) | (c >> 17)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[12] + 0x5a827999u;
            b = ((b << 7) | (b >> 25)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[0] + 0x5a827999u;
            a = ((a << 12) | (a >> 20)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[9] + 0x5a827999u;
            e = ((e << 15) | (e >> 17)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[5] + 0x5a827999u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[2] + 0x5a827999u;
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[14] + 0x5a827999u;
            b = ((b << 7) | (b >> 25)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[11] + 0x5a827999u;
            a = ((a << 13) | (a >> 19)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[8] + 0x5a827999u;
            e = ((e << 12) | (e >> 20)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[3] + 0x6ed9eba1u;
            d = ((d << 11) | (d >> 21)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[10] + 0x6ed9eba1u;
            c = ((c << 13) | (c >> 19)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[14] + 0x6ed9eba1u;
            b = ((b << 6) | (b >> 26)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[4] + 0x6ed9eba1u;
            a = ((a << 7) | (a >> 25)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[9] + 0x6ed9eba1u;
            e = ((e << 14) | (e >> 18)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[15] + 0x6ed9eba1u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[8] + 0x6ed9eba1u;
            c = ((c << 13) | (c >> 19)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[1] + 0x6ed9eba1u;
            b = ((b << 15) | (b >> 17)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[2] + 0x6ed9eba1u;
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[7] + 0x6ed9eba1u;
            e = ((e << 8) | (e >> 24)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[0] + 0x6ed9eba1u;
            d = ((d << 13) | (d >> 19)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[6] + 0x6ed9eba1u;
            c = ((c << 6) | (c >> 26)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[13] + 0x6ed9eba1u;
            b = ((b << 5) | (b >> 27)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[11] + 0x6ed9eba1u;
            a = ((a << 12) | (a >> 20)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[5] + 0x6ed9eba1u;
            e = ((e << 7) | (e >> 25)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[12] + 0x6ed9eba1u;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[1] + 0x8f1bbcdcu;
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[9] + 0x8f1bbcdcu;
            b = ((b << 12) | (b >> 20)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[11] + 0x8f1bbcdcu;
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[10] + 0x8f1bbcdcu;
            e = ((e << 15) | (e >> 17)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[0] + 0x8f1bbcdcu;
            d = ((d << 14) | (d >> 18)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[8] + 0x8f1bbcdcu;
            c = ((c << 15) | (c >> 17)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[12] + 0x8f1bbcdcu;
            b = ((b << 9) | (b >> 23)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[4] + 0x8f1bbcdcu;
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[13] + 0x8f1bbcdcu;
            e = ((e << 9) | (e >> 23)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[3] + 0x8f1bbcdcu;
            d = ((d << 14) | (d >> 18)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[7] + 0x8f1bbcdcu;
            c = ((c << 5) | (c >> 27)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[15] + 0x8f1bbcdcu;
            b = ((b << 6) | (b >> 26)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[14] + 0x8f1bbcdcu;
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[5] + 0x8f1bbcdcu;
            e = ((e << 6) | (e >> 26)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[6] + 0x8f1bbcdcu;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[2] + 0x8f1bbcdcu;
            c = ((c << 12) | (c >> 20)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[4] + 0xa953fd4eu;
            b = ((b << 9) | (b >> 23)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[0] + 0xa953fd4eu;
            a = ((a << 15) | (a >> 17)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[5] + 0xa953fd4eu;
            e = ((e << 5) | (e >> 27)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[9] + 0xa953fd4eu;
            d = ((d << 11) | (d >> 21)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[7] + 0xa953fd4eu;
            c = ((c << 6) | (c >> 26)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[12] + 0xa953fd4eu;
            b = ((b << 8) | (b >> 24)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[2] + 0xa953fd4eu;
            a = ((a << 13) | (a >> 19)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[10] + 0xa953fd4eu;
            e = ((e << 12) | (e >> 20)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[14] + 0xa953fd4eu;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[1] + 0xa953fd4eu;
            c = ((c << 12) | (c >> 20)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[3] + 0xa953fd4eu;
            b = ((b << 13) | (b >> 19)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[8] + 0xa953fd4eu;
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[11] + 0xa953fd4eu;
            e = ((e << 11) | (e >> 21)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[6] + 0xa953fd4eu;
            d = ((d << 8) | (d >> 24)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[15] + 0xa953fd4eu;
            c = ((c << 5) | (c >> 27)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[13] + 0xa953fd4eu;
            b = ((b << 6) | (b >> 26)) + a;
            d = (d << 10) | (d >> 22);
            #endregion

            uint wa = a;
            uint wb = b;
            uint wc = c;
            uint wd = d;
            uint we = e;

            a = hash[0];
            b = hash[1];
            c = hash[2];
            d = hash[3];
            e = hash[4];

            #region right line
            a += (b ^ (c | ~d)) + x[5] + 0x50a28be6u;
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[14] + 0x50a28be6u;
            e = ((e << 9) | (e >> 23)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[7] + 0x50a28be6u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[0] + 0x50a28be6u;
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[9] + 0x50a28be6u;
            b = ((b << 13) | (b >> 19)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[2] + 0x50a28be6u;
            a = ((a << 15) | (a >> 17)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[11] + 0x50a28be6u;
            e = ((e << 15) | (e >> 17)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[4] + 0x50a28be6u;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[13] + 0x50a28be6u;
            c = ((c << 7) | (c >> 25)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[6] + 0x50a28be6u;
            b = ((b << 7) | (b >> 25)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[15] + 0x50a28be6u;
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ (b | ~c)) + x[8] + 0x50a28be6u;
            e = ((e << 11) | (e >> 21)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ (a | ~b)) + x[1] + 0x50a28be6u;
            d = ((d << 14) | (d >> 18)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ (e | ~a)) + x[10] + 0x50a28be6u;
            c = ((c << 14) | (c >> 18)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ (d | ~e)) + x[3] + 0x50a28be6u;
            b = ((b << 12) | (b >> 20)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ (c | ~d)) + x[12] + 0x50a28be6u;
            a = ((a << 6) | (a >> 26)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[6] + 0x5c4dd124u;
            e = ((e << 9) | (e >> 23)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[11] + 0x5c4dd124u;
            d = ((d << 13) | (d >> 19)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[3] + 0x5c4dd124u;
            c = ((c << 15) | (c >> 17)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[7] + 0x5c4dd124u;
            b = ((b << 7) | (b >> 25)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[0] + 0x5c4dd124u;
            a = ((a << 12) | (a >> 20)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[13] + 0x5c4dd124u;
            e = ((e << 8) | (e >> 24)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[5] + 0x5c4dd124u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[10] + 0x5c4dd124u;
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[14] + 0x5c4dd124u;
            b = ((b << 7) | (b >> 25)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[15] + 0x5c4dd124u;
            a = ((a << 7) | (a >> 25)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[8] + 0x5c4dd124u;
            e = ((e << 12) | (e >> 20)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & b) | (a & ~b)) + x[12] + 0x5c4dd124u;
            d = ((d << 7) | (d >> 25)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & a) | (e & ~a)) + x[4] + 0x5c4dd124u;
            c = ((c << 6) | (c >> 26)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & e) | (d & ~e)) + x[9] + 0x5c4dd124u;
            b = ((b << 15) | (b >> 17)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & d) | (c & ~d)) + x[1] + 0x5c4dd124u;
            a = ((a << 13) | (a >> 19)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & c) | (b & ~c)) + x[2] + 0x5c4dd124u;
            e = ((e << 11) | (e >> 21)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[15] + 0x6d703ef3u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[5] + 0x6d703ef3u;
            c = ((c << 7) | (c >> 25)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[1] + 0x6d703ef3u;
            b = ((b << 15) | (b >> 17)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[3] + 0x6d703ef3u;
            a = ((a << 11) | (a >> 21)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[7] + 0x6d703ef3u;
            e = ((e << 8) | (e >> 24)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[14] + 0x6d703ef3u;
            d = ((d << 6) | (d >> 26)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[6] + 0x6d703ef3u;
            c = ((c << 6) | (c >> 26)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[9] + 0x6d703ef3u;
            b = ((b << 14) | (b >> 18)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[11] + 0x6d703ef3u;
            a = ((a << 12) | (a >> 20)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[8] + 0x6d703ef3u;
            e = ((e << 13) | (e >> 19)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[12] + 0x6d703ef3u;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d | ~e) ^ a) + x[2] + 0x6d703ef3u;
            c = ((c << 14) | (c >> 18)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c | ~d) ^ e) + x[10] + 0x6d703ef3u;
            b = ((b << 13) | (b >> 19)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b | ~c) ^ d) + x[0] + 0x6d703ef3u;
            a = ((a << 13) | (a >> 19)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a | ~b) ^ c) + x[4] + 0x6d703ef3u;
            e = ((e << 7) | (e >> 25)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e | ~a) ^ b) + x[13] + 0x6d703ef3u;
            d = ((d << 5) | (d >> 27)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[8] + 0x7a6d76e9u;
            c = ((c << 15) | (c >> 17)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[6] + 0x7a6d76e9u;
            b = ((b << 5) | (b >> 27)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[4] + 0x7a6d76e9u;
            a = ((a << 8) | (a >> 24)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[1] + 0x7a6d76e9u;
            e = ((e << 11) | (e >> 21)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[3] + 0x7a6d76e9u;
            d = ((d << 14) | (d >> 18)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[11] + 0x7a6d76e9u;
            c = ((c << 14) | (c >> 18)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[15] + 0x7a6d76e9u;
            b = ((b << 6) | (b >> 26)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[0] + 0x7a6d76e9u;
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[5] + 0x7a6d76e9u;
            e = ((e << 6) | (e >> 26)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[12] + 0x7a6d76e9u;
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[2] + 0x7a6d76e9u;
            c = ((c << 12) | (c >> 20)) + b;
            e = (e << 10) | (e >> 22);
            b += ((c & d) | (~c & e)) + x[13] + 0x7a6d76e9u;
            b = ((b << 9) | (b >> 23)) + a;
            d = (d << 10) | (d >> 22);
            a += ((b & c) | (~b & d)) + x[9] + 0x7a6d76e9u;
            a = ((a << 12) | (a >> 20)) + e;
            c = (c << 10) | (c >> 22);
            e += ((a & b) | (~a & c)) + x[7] + 0x7a6d76e9u;
            e = ((e << 5) | (e >> 27)) + d;
            b = (b << 10) | (b >> 22);
            d += ((e & a) | (~e & b)) + x[10] + 0x7a6d76e9u;
            d = ((d << 15) | (d >> 17)) + c;
            a = (a << 10) | (a >> 22);
            c += ((d & e) | (~d & a)) + x[14] + 0x7a6d76e9u;
            c = ((c << 8) | (c >> 24)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[12];
            b = ((b << 8) | (b >> 24)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[15];
            a = ((a << 5) | (a >> 27)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[10];
            e = ((e << 12) | (e >> 20)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[4];
            d = ((d << 9) | (d >> 23)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[1];
            c = ((c << 12) | (c >> 20)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[5];
            b = ((b << 5) | (b >> 27)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[8];
            a = ((a << 14) | (a >> 18)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[7];
            e = ((e << 6) | (e >> 26)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[6];
            d = ((d << 8) | (d >> 24)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[2];
            c = ((c << 13) | (c >> 19)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[13];
            b = ((b << 6) | (b >> 26)) + a;
            d = (d << 10) | (d >> 22);
            a += (b ^ c ^ d) + x[14];
            a = ((a << 5) | (a >> 27)) + e;
            c = (c << 10) | (c >> 22);
            e += (a ^ b ^ c) + x[0];
            e = ((e << 15) | (e >> 17)) + d;
            b = (b << 10) | (b >> 22);
            d += (e ^ a ^ b) + x[3];
            d = ((d << 13) | (d >> 19)) + c;
            a = (a << 10) | (a >> 22);
            c += (d ^ e ^ a) + x[9];
            c = ((c << 11) | (c >> 21)) + b;
            e = (e << 10) | (e >> 22);
            b += (c ^ d ^ e) + x[11];
            b = ((b << 11) | (b >> 21)) + a;
            d = (d << 10) | (d >> 22);
            #endregion

            c += wb + hash[0];
            hash[0] = hash[1] + wc + d;
            hash[1] = hash[2] + wd + e;
            hash[2] = hash[3] + we + a;
            hash[3] = hash[4] + wa + b;
            hash[4] = c;
        }

        private static unsafe void LoadBlock(uint* p, uint* x)
        {
            x[0] = p[0];
            x[1] = p[1];
            x[2] = p[2];
            x[3] = p[3];
            x[4] = p[4];
            x[5] = p[5];
            x[6] = p[6];
            x[7] = p[7];
            x[8] = p[8];
            x[9] = p[9];
            x[10] = p[10];
            x[11] = p[11];
            x[12] = p[12];
            x[13] = p[13];
            x[14] = p[14];
            x[15] = p[15];
        }

        private static unsafe void LoadPartialBlock(byte* p, uint* x, int length)
        {
            int n = 8 - ((length + 7) >> 3);
            while (length >= 8)
            {
                x[0] = *((uint*)p);
                x[1] = *((uint*)(p + 4));
                p += 8;
                x += 2;
                length -= 8;
            }
            switch (length)
            {
                case 0:
                    x -= 2;
                    break;
                case 1:
                    x[0] = p[0];
                    x[1] = 0;
                    break;
                case 2:
                    x[0] = (uint)p[0] | ((uint)p[1] << 8);
                    x[1] = 0;
                    break;
                case 3:
                    x[0] = (uint)p[0] | ((uint)p[1] << 8) | ((uint)p[2] << 16);
                    x[1] = 0;
                    break;
                case 4:
                    x[0] = *((uint*)p);
                    x[1] = 0;
                    break;
                case 5:
                    x[0] = *((uint*)p);
                    x[1] = p[4];
                    break;
                case 6:
                    x[0] = *((uint*)p);
                    x[1] = (uint)p[4] | ((uint)p[5] << 8);
                    break;
                case 7:
                    x[0] = *((uint*)p);
                    x[1] = (uint)p[4] | ((uint)p[5] << 8) | ((uint)p[6] << 16);
                    break;
            }
            x += 2;
            while (n > 0)
            {
                x[0] = 0;
                x[1] = 0;
                x += 2;
                n--;
            }
        }

        public static unsafe int[] Initialize()
        {
            int[] hashData = new int[23];
            fixed (int* pData = &hashData[0])
            {
                uint* d = (uint*)pData;
                d[0] = 0x67452301u;
                d[1] = 0xefcdab89u;
                d[2] = 0x98badcfeu;
                d[3] = 0x10325476u;
                d[4] = 0xc3d2e1f0u;
            }
            return hashData;
        }

        public static unsafe void Initialize(int[] hashData)
        {
            fixed (int* pData = &hashData[0])
            {
                uint* d = (uint*)pData;
                d[0] = 0x67452301u;
                d[1] = 0xefcdab89u;
                d[2] = 0x98badcfeu;
                d[3] = 0x10325476u;
                d[4] = 0xc3d2e1f0u;
                AcedBinary.Fill(0, (int*)(d + 5), 18);
            }
        }

        public static unsafe void Initialize(int[] hashData, int[] baseHash)
        {
            fixed (int* pData = &hashData[0], pHash = &baseHash[0])
            {
                uint* d = (uint*)pData;
                uint* h = (uint*)pHash;
                d[0] = h[0];
                d[1] = h[1];
                d[2] = h[2];
                d[3] = h[3];
                d[4] = h[4];
                AcedBinary.Fill(0, (int*)(d + 5), 18);
            }
        }

        [CLSCompliant(false)]
        public static unsafe void Update(int[] hashData, byte* bytes, int length)
        {
            fixed (int* pHash = &hashData[0])
            {
                uint* h = (uint*)pHash;
                if (h[0] == 0 && h[5] == 0)
                    AcedHashDataFinalizedException.Throw();
                if (length <= 0)
                    return;
                h[5] += (uint)length;
                uint* x = h + 7;
                if (h[6] + length < 64)
                {
                    AcedBinary.CopyBytes(bytes, (byte*)x + h[6], length);
                    h[6] += (uint)length;
                    return;
                }
                int n = 64 - (int)h[6];
                AcedBinary.CopyBytes(bytes, (byte*)x + h[6], n);
                TransformBlock(h, x);
                length -= n;
                bytes += n;
                while (length >= 64)
                {
                    LoadBlock((uint*)bytes, x);
                    TransformBlock(h, x);
                    length -= 64;
                    bytes += 64;
                }
                LoadPartialBlock(bytes, x, length);
                h[6] = (uint)length;
            }
        }

        public static unsafe void Update(int[] hashData, byte[] bytes, int offset, int length)
        {
            if (length > 0)
                fixed (byte* pBytes = &bytes[offset])
                    Update(hashData, pBytes, length);
        }

        public static unsafe void Update(int[] hashData, string s)
        {
            int n;
            if (s != null && (n = s.Length) > 0)
                fixed (char* pS = s)
                    Update(hashData, (byte*)pS, n << 1);
        }

        public static unsafe int[] Finalize(int[] hashData)
        {
            int[] result = new int[5];
            Finalize(hashData, result);
            return result;
        }

        public static unsafe void Finalize(int[] hashData, int[] resultHash)
        {
            fixed (int* pHash = &hashData[0])
            {
                uint* h = (uint*)pHash;
                int length = (int)h[5];
                if (h[0] == 0 && length == 0)
                    AcedHashDataFinalizedException.Throw();
                uint* x = h + 7;
                x[(length >> 2) & 15] ^= 1u << (((length & 3) << 3) + 7);
                if ((length & 63) > 55)
                {
                    TransformBlock(h, x);
                    AcedBinary.Fill(0, (int*)x, 16);
                }
                x[14] = (uint)length << 3;
                x[15] = (uint)length >> 29;
                TransformBlock(h, x);
                fixed (int* pResult = &resultHash[0])
                {
                    x = (uint*)pResult;
                    x[0] = h[0];
                    x[1] = h[1];
                    x[2] = h[2];
                    x[3] = h[3];
                    x[4] = h[4];
                }
                AcedBinary.Fill(0, (int*)h, 23);
            }
        }

        public static int[] NoDataHash()
        {
            return new int[] { -1518005860, 1425861061, -1761073055, 1224075390, 831333810 };
        }

        [CLSCompliant(false)]
        public static unsafe int[] Compute(byte* bytes, int length)
        {
            int[] result = new int[5];
            int[] xx = new int[16];
            fixed (int* pHash = &result[0], pX = &xx[0])
            {
                uint* h = (uint*)pHash;
                uint* x = (uint*)pX;
                h[0] = 0x67452301u;
                h[1] = 0xefcdab89u;
                h[2] = 0x98badcfeu;
                h[3] = 0x10325476u;
                h[4] = 0xc3d2e1f0u;
                int count = length;
                while (count >= 64)
                {
                    LoadBlock((uint*)bytes, x);
                    TransformBlock(h, x);
                    count -= 64;
                    bytes += 64;
                }
                LoadPartialBlock(bytes, x, count);
                x[(length >> 2) & 15] ^= 1u << (((length & 3) << 3) + 7);
                if ((length & 63) > 55)
                {
                    TransformBlock(h, x);
                    AcedBinary.Fill(0, (int*)x, 16);
                }
                x[14] = (uint)length << 3;
                x[15] = (uint)length >> 29;
                TransformBlock(h, x);
                AcedBinary.Fill(0, (int*)x, 16);
            }
            return result;
        }

        public static unsafe int[] Compute(byte[] bytes, int offset, int length)
        {
            if (length > 0)
                fixed (byte* pBytes = &bytes[offset])
                    return Compute(pBytes, length);
            return NoDataHash();
        }

        public static unsafe int[] Compute(string s)
        {
            int n;
            if (s != null && (n = s.Length) > 0)
                fixed (char* pS = s)
                    return Compute((byte*)pS, n << 1);
            return NoDataHash();
        }

        public static unsafe Guid ToGuid(int[] hash)
        {
            fixed (int* pHash = &hash[0])
            {
                uint* h = (uint*)pHash;
                return new Guid(h[0] ^ h[4], (ushort)h[1], (ushort)(h[1] >> 16),
                    (byte)h[2], (byte)(h[2] >> 8), (byte)(h[2] >> 16), (byte)(h[2] >> 24),
                    (byte)h[3], (byte)(h[3] >> 8), (byte)(h[3] >> 16), (byte)(h[3] >> 24));
            }
        }

        public static unsafe void Copy(int[] sourceHash, byte[] bytes, int offset)
        {
            fixed (int* pHash = &sourceHash[0])
            fixed (byte* pBytes = &bytes[offset])
            {
                int* h = pHash;
                int* p = (int*)pBytes;
                p[0] = h[0];
                p[1] = h[1];
                p[2] = h[2];
                p[3] = h[3];
                p[4] = h[4];
            }
        }

        public static unsafe void Copy(byte[] bytes, int offset, int[] destinationHash)
        {
            fixed (byte* pBytes = &bytes[offset])
            fixed (int* pHash = &destinationHash[0])
            {
                int* p = (int*)pBytes;
                int* h = pHash;
                h[0] = p[0];
                h[1] = p[1];
                h[2] = p[2];
                h[3] = p[3];
                h[4] = p[4];
            }
        }

        public static unsafe void Copy(int[] sourceHash, int[] destinationHash)
        {
            fixed (int* pSrc = &sourceHash[0], pDst = &destinationHash[0])
            {
                int* s = pSrc;
                int* d = pDst;
                d[0] = s[0];
                d[1] = s[1];
                d[2] = s[2];
                d[3] = s[3];
                d[4] = s[4];
            }
        }

        public static unsafe bool Equals(int[] hash, byte[] bytes, int offset)
        {
            fixed (int* pHash = &hash[0])
            fixed (byte* pBytes = &bytes[offset])
            {
                int* h = pHash;
                int* p = (int*)pBytes;
                if (h[0] == p[0] && h[1] == p[1] && h[2] == p[2] && h[3] == p[3] && h[4] == p[4])
                    return true;
                return false;
            }
        }

        public static unsafe bool Equals(int[] hash1, int[] hash2)
        {
            fixed (int* pHash1 = &hash1[0], pHash2 = &hash2[0])
            {
                int* h1 = pHash1;
                int* h2 = pHash2;
                if (h1[0] == h2[0] && h1[1] == h2[1] && h1[2] == h2[2] && h1[3] == h2[3] && h1[4] == h2[4])
                    return true;
                return false;
            }
        }

        public static unsafe void Clear(int[] hash)
        {
            fixed (int* pHash = &hash[0])
            {
                int* h = pHash;
                h[0] = 0;
                h[1] = 0;
                h[2] = 0;
                h[3] = 0;
                h[4] = 0;
            }
        }

        private AcedRipeMD() { }
    }
}
