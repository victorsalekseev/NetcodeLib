
#region Класс AcedMemoryWriter
/*
--------------------------------------------------------------------------------
public class AcedMemoryWriter : IAcedWriter

  Предназначен для помещения данных в бинарный поток, т.е, в данном случае,
  в массив байт, размер которого динамически увеличивается по мере добавления
  новых данных. При этом данные могут быть сжаты методом LZ+Huffman, подобным
  используемому в библиотеке zlib, зашифрованы с применением алгоритма CAST-128
  в режиме CFB (64 бита) и защищены значением цифровой сигнатуры RipeMD-160.

--------------------------------------------------------------------------------
ctor's
--------------------------------------------------------------------------------

public AcedMemoryWriter()

  Создает новый бинарный поток с начальным размером 4096 байт.

public AcedMemoryWriter(int capacity)

  Создает бинарный поток с начальным размером capacity байт.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

public int Length { get; }

  Возвращает общее число байт, помещенное в выходной бинарный поток. Значение
  этого свойства соответствует индексу следующего сохраняемого байта в массиве,
  ссылка на который возвращается функцией GetBuffer(). Сжатие и шифрование
  данных выполняется в отдельном массиве после того, как все данные помещены
  в бинарный поток и вызван метод ToArray().
  
public int Capacity { get; set; }

  Возвращает или устанавливает размер внутреннего массива, ссылка на который
  возвращается функцией GetBuffer(), используемого для хранения данных
  выходного потока. При превышении этого размера в процессе добавления новых
  данных происходит перераспределение памяти под внутренний массив.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public byte[] GetBuffer()

  Возвращает ссылку на внутренний массив, в который помещаются данные,
  сохраняемые в бинарном потоке. Значение этой ссылки изменяется при каждом
  перераспределении памяти.

public byte[] ToArray(AcedCompressionLevel compressionLevel)

  Возвращает массив байт, содержащий данные, помещеннные в бинарный поток
  с добавлением контрольной суммы Адлера. Параметр compressionLevel выбирает
  режим сжатия выходного потока. Для сжатия используется общий экземпляр
  класса AcedDeflator, который возвращается свойством AcedDeflator.Instance.

public byte[] ToArray(AcedCompressionLevel compressionLevel,
  AcedDeflator deflator)

  Возвращает массив байт, содержащий данные, помещеннные в бинарный поток
  с добавлением контрольной суммы Адлера. Параметр compressionLevel выбирает
  режим сжатия выходного потока. Для сжатия используется экземпляр класса
  AcedDeflator, передаваемый параметром deflator.

public byte[] ToArray(AcedCompressionLevel compressionLevel, Guid keyGuid)

  Возвращает массив байт, содержащий данные, помещеннные в бинарный поток
  с добавлением контрольной суммы Адлера. Параметр compressionLevel выбирает
  режим сжатия выходного потока. Для сжатия используется общий экземпляр
  класса AcedDeflator, который возвращается свойством AcedDeflator.Instance.
  Если параметр keyGuid отличен от Guid.Empty, поток данных шифруется методом
  CAST-128 в режиме CFB (64 бита) с ключом keyGuid и защищается значением
  односторонней хэш-функции RipeMD-160.

public byte[] ToArray(AcedCompressionLevel compressionLevel,
  AcedDeflator deflator, Guid keyGuid)

  Возвращает массив байт, содержащий данные, помещеннные в бинарный поток
  с добавлением контрольной суммы Адлера. Параметр compressionLevel выбирает
  режим сжатия выходного потока. Для сжатия используется экземпляр класса
  AcedDeflator, передаваемый параметром deflator. Если параметр keyGuid
  отличен от Guid.Empty, поток данных шифруется методом CAST-128 в режиме
  CFB (64 бита) с ключом keyGuid и защищается значением односторонней
  хэш-функции RipeMD-160.

public void Reset()

  Сбрасывает в ноль длину бинарного потока. Свойство Capacity при этом
  не изменяется и память под внутренний массив не перераспределяется.
  После этого можно повторно использовать экземпляр класса AcedWriter.

public void Skip(int count)

  Пропускает в выходном потоке count байт. Свойство Length увеличивается
  при этом на величину count.

public void WriteBoolean(bool value)

  Сохраняет в бинарном потоке значение типа System.Boolean.

public void WriteByte(byte value)

  Сохраняет в бинарном потоке значение типа System.Byte.

public void WriteByteArray(byte[] value)

  Сохраняет в бинарном потоке значение типа System.Byte[].

public void WriteChar(char value)

  Сохраняет в бинарном потоке значение типа System.Char.

public void WriteDateTime(DateTime value)

  Сохраняет в бинарном потоке значение типа System.DateTime.

public void WriteDecimal(decimal value)

  Сохраняет в бинарном потоке значение типа System.Decimal.

public void WriteSingle(float value)

  Сохраняет в бинарном потоке значение типа System.Single.

public void WriteDouble(double value)

  Сохраняет в бинарном потоке значение типа System.Double.

public void WriteGuid(Guid value)

  Сохраняет в бинарном потоке значение типа System.Guid.

public void WriteInt16(short value)

  Сохраняет в бинарном потоке значение типа System.Int16.

public void WriteInt32(int value)

  Сохраняет в бинарном потоке значение типа System.Int32.

public void WriteInt64(long value)

  Сохраняет в бинарном потоке значение типа System.Int64.

public void WriteString(string value)

  Сохраняет в бинарном потоке значение типа System.String.

public void WriteTimeSpan(TimeSpan value)

  Сохраняет в бинарном потоке значение типа System.TimeSpan.

public void WriteUInt16(ushort value)

  Сохраняет в бинарном потоке значение типа System.UInt16.

public void WriteUInt32(uint value)

  Сохраняет в бинарном потоке значение типа System.UInt32.

public void WriteUInt64(ulong value)

  Сохраняет в бинарном потоке значение типа System.UInt64.

public void WriteSByte(sbyte value)

  Сохраняет в бинарном потоке значение типа System.SByte.

public void Write(bool[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Boolean
  из массива values, начиная с элемента с индексом index.

public void Write(byte[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Byte
  из массива values, начиная с элемента с индексом index.

public void Write(char[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Char
  из массива values, начиная с элемента с индексом index.

public void Write(DateTime[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.DateTime
  из массива values, начиная с элемента с индексом index.

public void Write(decimal[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Decimal
  из массива values, начиная с элемента с индексом index.

public void Write(float[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Single
  из массива values, начиная с элемента с индексом index.

public void Write(double[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Double
  из массива values, начиная с элемента с индексом index.

public void Write(Guid[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Guid
  из массива values, начиная с элемента с индексом index.

public void Write(short[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int16
  из массива values, начиная с элемента с индексом index.

public void Write(int[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int32
  из массива values, начиная с элемента с индексом index.

public void Write(long[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int64
  из массива values, начиная с элемента с индексом index.

public void Write(TimeSpan[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.TimeSpan
  из массива values, начиная с элемента с индексом index.

public void Write(ushort[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.UInt16
  из массива values, начиная с элемента с индексом index.

public void Write(uint[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.UInt32
  из массива values, начиная с элемента с индексом index.

public void Write(ulong[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.UInt64
  из массива values, начиная с элемента с индексом index.

public void Write(sbyte[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.SByte
  из массива values, начиная с элемента с индексом index.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedMemoryWriter class

    public class AcedMemoryWriter : IAcedWriter
    {
        private int _length;
        private int _capacity;
        private byte[] _buffer;

        public AcedMemoryWriter()
            : this(4096)
        {
        }

        public AcedMemoryWriter(int capacity)
        {
            _capacity = capacity;
            _buffer = new byte[capacity];
        }

        long IAcedWriter.Length
        {
            get { return _length; }
        }

        public int Length
        {
            get { return _length; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value >= _length && value != _capacity)
                    SetCapacity(value, _length);
            }
        }

        private unsafe void SetCapacity(int newCapacity, int count)
        {
            byte[] newBytes = new byte[newCapacity];
            if (count > 0)
                Buffer.BlockCopy(_buffer, 0, newBytes, 0, count);
            _buffer = newBytes;
            _capacity = newCapacity;
        }

        private void EnlargeCapacity(int count)
        {
            int newCapacity = AcedConsts.EnlargeCapacity(_capacity);
            if (newCapacity < _length)
                newCapacity = (((_length + 10) >> 12) + 1) << 12;
            SetCapacity(newCapacity, count);
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        public byte[] ToArray(AcedCompressionLevel compressionLevel)
        {
            return ToArray(compressionLevel, null, Guid.Empty);
        }

        public byte[] ToArray(AcedCompressionLevel compressionLevel, AcedDeflator deflator)
        {
            return ToArray(compressionLevel, deflator, Guid.Empty);
        }

        public byte[] ToArray(AcedCompressionLevel compressionLevel, Guid keyGuid)
        {
            return ToArray(compressionLevel, null, keyGuid);
        }

        public unsafe byte[] ToArray(AcedCompressionLevel compressionLevel, AcedDeflator deflator, Guid keyGuid)
        {
            byte[] result;
            if (deflator == null)
                deflator = AcedDeflator.Instance;
            if (keyGuid.Equals(Guid.Empty))
                result = deflator.Compress(_buffer, 0, _length, compressionLevel, 4, 0);
            else
            {
                result = deflator.Compress(_buffer, 0, _length, compressionLevel, 24, 0);
                AcedRipeMD.Copy(AcedCast5.Encrypt(keyGuid.ToByteArray(), result, 24, result.Length - 24), result, 4);
            }
            int x = AcedBinary.Adler32(result, 4, result.Length - 4);
            fixed (byte* pResult = &result[0])
                *((int*)pResult) = x;
            return result;
        }

        void IAcedWriter.Flush()
        {
        }

        public void Reset()
        {
            _length = 0;
        }

        public void Skip(int count)
        {
            int n = _length;
            _length = n + count;
            if (_length > _capacity)
                EnlargeCapacity(n);
        }

        public void WriteBoolean(bool value)
        {
            int n = _length;
            _length = n + 1;
            if (_length > _capacity)
                EnlargeCapacity(n);
            _buffer[n] = value ? (byte)1 : (byte)0;
        }

        public void WriteByte(byte value)
        {
            int n = _length;
            _length = n + 1;
            if (_length > _capacity)
                EnlargeCapacity(n);
            _buffer[n] = value;
        }

        public unsafe void WriteByteArray(byte[] value)
        {
            if (value == null)
            {
                WriteUInt16((ushort)AcedConsts.NullValue);
                return;
            }
            int byteCount = value.Length;
            if (byteCount == 0)
            {
                WriteUInt16(0);
                return;
            }
            int n = _length;
            _length = n + byteCount + 2;
            if (byteCount < AcedConsts.LongLength)
            {
                if (_length > _capacity)
                    EnlargeCapacity(n);
                _buffer[n] = (byte)byteCount;
                _buffer[n + 1] = (byte)(byteCount >> 8);
                n += 2;
            }
            else
            {
                _length += 4;
                if (_length > _capacity)
                    EnlargeCapacity(n);
                fixed (byte* pBuffer = &_buffer[n])
                {
                    byte* p = pBuffer;
                    *((ushort*)p) = (ushort)AcedConsts.LongLength;
                    *((int*)(p + 2)) = byteCount;
                    n += 6;
                }
            }
            Buffer.BlockCopy(value, 0, _buffer, n, byteCount);
        }

        public void WriteChar(char value)
        {
            int n = _length;
            _length = n + 2;
            if (_length > _capacity)
                EnlargeCapacity(n);
            uint x = (uint)value;
            _buffer[n] = (byte)x;
            _buffer[n + 1] = (byte)(x >> 8);
        }

        public unsafe void WriteDateTime(DateTime value)
        {
            int n = _length;
            _length = n + 8;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((long*)pBuffer) = value.Ticks;
        }

        public unsafe void WriteDecimal(decimal value)
        {
            int n = _length;
            _length = n + 16;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((decimal*)pBuffer) = value;
        }

        public unsafe void WriteSingle(float value)
        {
            int n = _length;
            _length = n + 4;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((float*)pBuffer) = value;
        }

        public unsafe void WriteDouble(double value)
        {
            int n = _length;
            _length = n + 8;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((double*)pBuffer) = value;
        }

        public unsafe void WriteGuid(Guid value)
        {
            int n = _length;
            _length = n + 16;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((Guid*)pBuffer) = value;
        }

        public void WriteInt16(short value)
        {
            int n = _length;
            _length = n + 2;
            if (_length > _capacity)
                EnlargeCapacity(n);
            uint x = (uint)((ushort)value);
            _buffer[n] = (byte)x;
            _buffer[n + 1] = (byte)(x >> 8);
        }

        public unsafe void WriteInt32(int value)
        {
            int n = _length;
            _length = n + 4;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((int*)pBuffer) = value;
        }

        public unsafe void WriteInt64(long value)
        {
            int n = _length;
            _length = n + 8;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((long*)pBuffer) = value;
        }

        public unsafe void WriteString(string value)
        {
            if (value == null)
            {
                WriteUInt16((ushort)AcedConsts.NullValue);
                return;
            }
            int charCount = value.Length;
            if (charCount == 0)
            {
                WriteUInt16(0);
                return;
            }
            int n = _length;
            _length = (charCount << 1) + n + 2;
            bool big = false;
            if (charCount >= AcedConsts.LongLength)
            {
                _length += 4;
                big = true;
            }
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
            {
                byte* p = pBuffer;
                if (!big)
                {
                    *((ushort*)p) = (ushort)charCount;
                    p += 2;
                }
                else
                {
                    *((ushort*)p) = (ushort)AcedConsts.LongLength;
                    *((int*)(p + 2)) = charCount;
                    p += 6;
                }
                fixed (char* pValue = value)
                    AcedBinary.CopyBytes((byte*)pValue, p, charCount << 1);
            }
        }

        public unsafe void WriteTimeSpan(TimeSpan value)
        {
            int n = _length;
            _length = n + 8;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((long*)pBuffer) = value.Ticks;
        }

        [CLSCompliant(false)]
        public void WriteUInt16(ushort value)
        {
            int n = _length;
            _length = n + 2;
            if (_length > _capacity)
                EnlargeCapacity(n);
            _buffer[n] = (byte)value;
            _buffer[n + 1] = (byte)(value >> 8);
        }

        [CLSCompliant(false)]
        public unsafe void WriteUInt32(uint value)
        {
            int n = _length;
            _length = n + 4;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((uint*)pBuffer) = value;
        }

        [CLSCompliant(false)]
        public unsafe void WriteUInt64(ulong value)
        {
            int n = _length;
            _length = n + 8;
            if (_length > _capacity)
                EnlargeCapacity(n);
            fixed (byte* pBuffer = &_buffer[n])
                *((ulong*)pBuffer) = value;
        }

        [CLSCompliant(false)]
        public void WriteSByte(sbyte value)
        {
            int n = _length;
            _length = n + 1;
            if (_length > _capacity)
                EnlargeCapacity(n);
            _buffer[n] = (byte)value;
        }

        private void IntWriteArray(System.Array values, int index, int count)
        {
            if (count > 0)
            {
                int n = _length;
                _length = n + count;
                if (_length > _capacity)
                    EnlargeCapacity(n);
                Buffer.BlockCopy(values, index, _buffer, n, count);
            }
        }

        public void Write(bool[] values, int index, int count)
        {
            IntWriteArray(values, index, count);
        }

        public void Write(byte[] values, int index, int count)
        {
            IntWriteArray(values, index, count);
        }

        public void Write(char[] values, int index, int count)
        {
            IntWriteArray(values, index << 1, count << 1);
        }

        public void Write(DateTime[] values, int index, int count)
        {
            IntWriteArray(values, index << 3, count << 3);
        }

        public void Write(decimal[] values, int index, int count)
        {
            IntWriteArray(values, index << 4, count << 4);
        }

        public void Write(float[] values, int index, int count)
        {
            IntWriteArray(values, index << 2, count << 2);
        }

        public void Write(double[] values, int index, int count)
        {
            IntWriteArray(values, index << 3, count << 3);
        }

        public void Write(Guid[] values, int index, int count)
        {
            IntWriteArray(values, index << 4, count << 4);
        }

        public void Write(short[] values, int index, int count)
        {
            IntWriteArray(values, index << 1, count << 1);
        }

        public void Write(int[] values, int index, int count)
        {
            IntWriteArray(values, index << 2, count << 2);
        }

        public void Write(long[] values, int index, int count)
        {
            IntWriteArray(values, index << 3, count << 3);
        }

        public void Write(TimeSpan[] values, int index, int count)
        {
            IntWriteArray(values, index << 3, count << 3);
        }

        [CLSCompliant(false)]
        public void Write(ushort[] values, int index, int count)
        {
            IntWriteArray(values, index << 1, count << 1);
        }

        [CLSCompliant(false)]
        public void Write(uint[] values, int index, int count)
        {
            IntWriteArray(values, index << 2, count << 2);
        }

        [CLSCompliant(false)]
        public void Write(ulong[] values, int index, int count)
        {
            IntWriteArray(values, index << 3, count << 3);
        }

        [CLSCompliant(false)]
        public void Write(sbyte[] values, int index, int count)
        {
            IntWriteArray(values, index, count);
        }

        void IAcedWriter.Close()
        {
        }
    }
}
