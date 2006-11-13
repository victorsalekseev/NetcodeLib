
#region Класс AcedMemoryReader
/*
--------------------------------------------------------------------------------
public class AcedMemoryReader : IAcedReader

  Предназначен для чтения данных из массива байт, созданного экземпляром
  класса AcedMemoryWriter. Данные в массиве защищаются контрольной суммой
  Адлера. Кроме того, они могут быть сжаты, зашифрованы и защищены значением
  цифровой сигнатуры RipeMD-160. При использовании в многопоточном приложении
  необходимо синхронизировать вызов методов данного класса.

--------------------------------------------------------------------------------
ctor's
--------------------------------------------------------------------------------

public AcedMemoryReader(byte[] bytes, int offset, int length)

  Создает экземпляр класса AcedMemoryReader из фрагмента массива bytes,
  начиная с индекса offset, длиной length байт. Предполагается, что данные
  в массиве не зашифрованы. Если данные упакованы, для распаковки
  используется общий экземпляр класса AcedInflator, который возвращается
  свойством AcedInflator.Instance.

public AcedMemoryReader(byte[] bytes, int offset, int length,
  AcedInflator inflator)

  Создает экземпляр класса AcedMemoryReader из фрагмента массива bytes,
  начиная с индекса offset, длиной length байт. Предполагается, что данные
  в массиве не зашифрованы. В параметре inflator передается экземпляр класса
  AcedInflator, который будет использован для распаковки сжатых данных.

public AcedMemoryReader(byte[] bytes, int offset, int length, Guid keyGuid)

  Создает экземпляр класса AcedMemoryReader из фрагмента массива bytes,
  начиная с индекса offset длиной length байт. В параметре keyGuid передается
  ключ шифра, представленный значением типа System.Guid. Если бинарный поток
  не зашифрован, в параметре keyGuid нужно передавать значение Guid.Empty.
  Если данные упакованы, для распаковки используется общий экземпляр класса
  AcedInflator, который возвращается свойством AcedInflator.Instance.

public AcedMemoryReader(byte[] bytes, int offset, int length,
  AcedInflator inflator, Guid keyGuid)

  Создает экземпляр класса AcedMemoryReader из фрагмента массива bytes,
  начиная с индекса offset длиной length байт. В параметре inflator передается
  экземпляр класса AcedInflator, который будет использован для распаковки
  сжатых данных. В параметре keyGuid передается ключ шифра, представленный
  значением типа System.Guid. Если бинарный поток не зашифрован, в параметре
  keyGuid нужно передавать значение Guid.Empty.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

public int Position { get; }

  Возвращает текущую позицию в бинарном потоке, т.е. индекс следующего
  читаемого байта из внутреннего массива, ссылка на который возвращается
  функцией GetBuffer().

public int Size { get; }

  Возвращает длину бинарного потока в байтах плюс смещение Offset. Это значение
  соответствует максимальному значению свойства Position. При попытке чтения
  данных за пределами этой длины возникает исключение.

public int Offset { get; }

  Возвращает начальную позицию в бинарном потоке, т.е. индекс элемента во
  внутреннем массиве, возвращаемом функцией GetBuffer(), с которого начинаются
  данные потока.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public byte[] GetBuffer()

  Возвращает ссылку на внутренний массив, содержащий расшифрованные и
  распакованные данные бинарного потока, из которого производится чтение.

public void Reset()

  Устанавливает текущую позицию на начало бинарного потока. После этого можно
  повторно считать данные из того же экземпляра класса AcedReader.

public void Skip(int count)

  Пропускает во входном бинарном потоке count байт. Свойство Position
  увеличивается при этом на величину count.

public bool ReadBoolean()

  Считывает значение типа System.Boolean из бинарного потока.

public byte ReadByte()

  Считывает значение типа System.Byte из бинарного потока.

public byte[] ReadByteArray()

  Считывает значение типа System.Byte[] из бинарного потока.

public char ReadChar()

  Считывает значение типа System.Char из бинарного потока.

public DateTime ReadDateTime()

  Считывает значение типа System.DateTime из бинарного потока.

public decimal ReadDecimal()

  Считывает значение типа System.Decimal из бинарного потока.

public float ReadSingle()

  Считывает значение типа System.Single из бинарного потока.

public double ReadDouble()

  Считывает значение типа System.Double из бинарного потока.

public Guid ReadGuid()

  Считывает значение типа System.Guid из бинарного потока.

public short ReadInt16()

  Считывает значение типа System.Int16 из бинарного потока.

public int ReadInt32()

  Считывает значение типа System.Int32 из бинарного потока.

public long ReadInt64()

  Считывает значение типа System.Int64 из бинарного потока.

public string ReadString()

  Считывает значение типа System.String из бинарного потока.

public TimeSpan ReadTimeSpan()

  Считывает значение типа System.TimeSpan из бинарного потока.

public ushort ReadUInt16()

  Считывает значение типа System.UInt16 из бинарного потока.

public uint ReadUInt32()

  Считывает значение типа System.UInt32 из бинарного потока.

public ulong ReadUInt64()

  Считывает значение типа System.UInt64 из бинарного потока.

public sbyte ReadSByte()

  Считывает значение типа System.SByte из бинарного потока.

public void Read(bool[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Boolean и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(byte[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Byte и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(char[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Char и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(DateTime[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.DateTime и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(decimal[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Decimal и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(float[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Single и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(double[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Double и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(Guid[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Guid и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(short[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int16 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(int[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int32 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(long[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int64 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(TimeSpan[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.TimeSpan и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(ushort[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.UInt16 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(uint[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.UInt32 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(ulong[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.UInt64 и помещает
  их в массив values, начиная с элемента с индексом index.

public void Read(sbyte[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.SByte и помещает
  их в массив values, начиная с элемента с индексом index.

public int StreamRead(byte[] buffer, int offset, int count)

  Пытается прочитать count байт из бинарного потока и сохранить их в массиве
  buffer, начиная со смещения offset. Если число байт, оставшееся в потоке
  меньше значения count, считывается столько байт, сколько есть. Функция
  возвращает число байт, фактически прочитанное из бинарного потока.

public int StreamReadByte()

  Считывает следующий байт из бинарного потока. Если достигнут конец потока,
  т.е. все байты уже прочитаны, функция возвращает значение -1.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedMemoryReader class

    public class AcedMemoryReader : IAcedReader
    {
        private int _position;
        private int _size;
        private byte[] _buffer;
        private int _offset;
        private char[] _chars;

        public AcedMemoryReader(byte[] bytes, int offset, int length)
            : this(bytes, offset, length, null, Guid.Empty)
        {
        }

        public AcedMemoryReader(byte[] bytes, int offset, int length, AcedInflator inflator)
            : this(bytes, offset, length, inflator, Guid.Empty)
        {
        }

        public AcedMemoryReader(byte[] bytes, int offset, int length, Guid keyGuid)
            : this(bytes, offset, length, null, keyGuid)
        {
        }

        public unsafe AcedMemoryReader(byte[] bytes, int offset, int length, AcedInflator inflator, Guid keyGuid)
        {
            if (bytes == null)
                AcedConsts.ThrowArgumentNullException("bytes");
            length -= 4;
            if (length < 0)
                AcedDataCorruptedException.Throw();
            int x = AcedBinary.Adler32(bytes, offset + 4, length);
            fixed (byte* pBytes = &bytes[offset])
            {
                int* p = (int*)pBytes;
                if (x != p[0])
                    AcedDataCorruptedException.Throw();
                if (keyGuid.Equals(Guid.Empty))
                {
                    x = p[1];
                    offset += 4;
                }
                else
                {
                    length -= 20;
                    if (length < 0)
                        AcedDataCorruptedException.Throw();
                    offset += 24;
                    int[] hash = AcedCast5.Decrypt(keyGuid.ToByteArray(), bytes, offset, length);
                    fixed (int* h = &hash[0])
                        if (h[0] != p[1] || h[1] != p[2] || h[2] != p[3] || h[3] != p[4] || h[4] != p[5])
                            AcedWrongDecryptionKeyException.Throw();
                    x = p[6];
                }
            }
            if (x <= 0)
            {
                _buffer = bytes;
                length = -x;
                offset += 4;
            }
            else
            {
                if (inflator == null)
                    inflator = AcedInflator.Instance;
                _buffer = inflator.Decompress(bytes, offset, 0, 0);
                length = _buffer.Length;
                offset = 0;
            }
            _position = offset;
            _size = offset + length;
            _offset = offset;
        }

        long IAcedReader.Length
        {
            get { return _size - _offset; }
        }

        long IAcedReader.Position
        {
            get { return _position - _offset; }
        }

        public int Position
        {
            get { return _position; }
        }

        public int Size
        {
            get { return _size; }
        }

        public int Offset
        {
            get { return _offset; }
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        public void Reset()
        {
            _position = _offset;
        }

        public void Skip(int count)
        {
            int n = _position + count;
            if (n > _size)
                AcedReadBeyondTheEndException.Throw();
            _position = n;
        }

        public bool ReadBoolean()
        {
            int n = _position;
            _position = n + 1;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            if (_buffer[n] == 0)
                return false;
            return true;
        }

        public byte ReadByte()
        {
            int n = _position;
            _position = n + 1;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            return _buffer[n];
        }

        public unsafe byte[] ReadByteArray()
        {
            int n = _position;
            _position += 2;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            int byteCount = _buffer[n] + (_buffer[n + 1] << 8);
            if (byteCount == AcedConsts.NullValue)
                return null;
            if (byteCount == 0)
                return new byte[0];
            n += 2;
            if (byteCount == AcedConsts.LongLength)
            {
                _position += 4;
                if (_position > _size)
                    AcedReadBeyondTheEndException.Throw();
                fixed (byte* pB = &_buffer[n])
                    byteCount = *((int*)pB);
                n += 4;
            }
            byte[] result = new byte[byteCount];
            _position += byteCount;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            Buffer.BlockCopy(_buffer, n, result, 0, byteCount);
            return result;
        }

        public char ReadChar()
        {
            int n = _position;
            _position = n + 2;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            return (char)(_buffer[n] + (_buffer[n + 1] << 8));
        }

        public unsafe DateTime ReadDateTime()
        {
            int n = _position;
            _position = n + 8;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((DateTime*)pBuffer);
        }

        public unsafe decimal ReadDecimal()
        {
            int n = _position;
            _position = n + 16;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((decimal*)pBuffer);
        }

        public unsafe float ReadSingle()
        {
            int n = _position;
            _position = n + 4;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((float*)pBuffer);
        }

        public unsafe double ReadDouble()
        {
            int n = _position;
            _position = n + 8;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((double*)pBuffer);
        }

        public unsafe Guid ReadGuid()
        {
            int n = _position;
            _position = n + 16;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((Guid*)pBuffer);
        }

        public short ReadInt16()
        {
            int n = _position;
            _position = n + 2;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            return (short)((ushort)(_buffer[n] + (_buffer[n + 1] << 8)));
        }

        public unsafe int ReadInt32()
        {
            int n = _position;
            _position = n + 4;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((int*)pBuffer);
        }

        public unsafe long ReadInt64()
        {
            int n = _position;
            _position = n + 8;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((long*)pBuffer);
        }

        public unsafe string ReadString()
        {
            int n = _position;
            _position += 2;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            int charCount = _buffer[n] + (_buffer[n + 1] << 8);
            if (charCount == AcedConsts.NullValue)
                return null;
            if (charCount == 0)
                return String.Empty;
            n += 2;
            if (charCount == AcedConsts.LongLength)
            {
                _position += 4;
                if (_position > _size)
                    AcedReadBeyondTheEndException.Throw();
                fixed (byte* pB = &_buffer[n])
                    charCount = *((int*)pB);
                n += 4;
            }
            if (_chars == null || _chars.Length < charCount)
                _chars = new char[charCount > 200 ? charCount + 50 : 200];
            _position += charCount << 1;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            Buffer.BlockCopy(_buffer, n, _chars, 0, charCount << 1);
            return new string(_chars, 0, charCount);
        }

        public unsafe TimeSpan ReadTimeSpan()
        {
            int n = _position;
            _position = n + 8;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((TimeSpan*)pBuffer);
        }

        [CLSCompliant(false)]
        public ushort ReadUInt16()
        {
            int n = _position;
            _position = n + 2;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            return (ushort)(_buffer[n] + (_buffer[n + 1] << 8));
        }

        [CLSCompliant(false)]
        public unsafe uint ReadUInt32()
        {
            int n = _position;
            _position = n + 4;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((uint*)pBuffer);
        }

        [CLSCompliant(false)]
        public unsafe ulong ReadUInt64()
        {
            int n = _position;
            _position = n + 8;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            fixed (byte* pBuffer = &_buffer[n])
                return *((ulong*)pBuffer);
        }

        [CLSCompliant(false)]
        public sbyte ReadSByte()
        {
            int n = _position;
            _position = n + 1;
            if (_position > _size)
                AcedReadBeyondTheEndException.Throw();
            return (sbyte)_buffer[n];
        }

        private void IntReadArray(System.Array values, int index, int count)
        {
            if (count > 0)
            {
                int n = _position;
                _position = n + count;
                if (_position <= _size)
                    Buffer.BlockCopy(_buffer, n, values, index, count);
                else
                    AcedReadBeyondTheEndException.Throw();
            }
        }

        public void Read(bool[] values, int index, int count)
        {
            IntReadArray(values, index, count);
        }

        public void Read(byte[] values, int index, int count)
        {
            IntReadArray(values, index, count);
        }

        public void Read(char[] values, int index, int count)
        {
            IntReadArray(values, index << 1, count << 1);
        }

        public void Read(DateTime[] values, int index, int count)
        {
            IntReadArray(values, index << 3, count << 3);
        }

        public void Read(decimal[] values, int index, int count)
        {
            IntReadArray(values, index << 4, count << 4);
        }

        public void Read(float[] values, int index, int count)
        {
            IntReadArray(values, index << 2, count << 2);
        }

        public void Read(double[] values, int index, int count)
        {
            IntReadArray(values, index << 3, count << 3);
        }

        public void Read(Guid[] values, int index, int count)
        {
            IntReadArray(values, index << 4, count << 4);
        }

        public void Read(short[] values, int index, int count)
        {
            IntReadArray(values, index << 1, count << 1);
        }

        public void Read(int[] values, int index, int count)
        {
            IntReadArray(values, index << 2, count << 2);
        }

        public void Read(long[] values, int index, int count)
        {
            IntReadArray(values, index << 3, count << 3);
        }

        public void Read(TimeSpan[] values, int index, int count)
        {
            IntReadArray(values, index << 3, count << 3);
        }

        [CLSCompliant(false)]
        public void Read(ushort[] values, int index, int count)
        {
            IntReadArray(values, index << 1, count << 1);
        }

        [CLSCompliant(false)]
        public void Read(uint[] values, int index, int count)
        {
            IntReadArray(values, index << 2, count << 2);
        }

        [CLSCompliant(false)]
        public void Read(ulong[] values, int index, int count)
        {
            IntReadArray(values, index << 3, count << 3);
        }

        [CLSCompliant(false)]
        public void Read(sbyte[] values, int index, int count)
        {
            IntReadArray(values, index, count);
        }

        public int StreamRead(byte[] buffer, int offset, int count)
        {
            int n = _position;
            _position = n + count;
            if (_position > _size)
            {
                count = _size - n;
                _position = _size;
            }
            Buffer.BlockCopy(_buffer, n, buffer, offset, count);
            return count;
        }

        public int StreamReadByte()
        {
            int n = _position;
            if (n < _size)
            {
                _position = n + 1;
                return _buffer[n];
            }
            return -1;
        }

        void IAcedReader.Close()
        {
        }
    }
}
