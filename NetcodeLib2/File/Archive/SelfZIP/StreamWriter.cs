
#region Класс AcedStreamWriter
/*
--------------------------------------------------------------------------------
public class AcedStreamWriter : IDisposable, IAcedWriter

  Предназначен для помещения данных в бинарный поток типа System.IO.Stream,
  ассоциированный с экземпляром данного класса. При этом данные могут быть
  сжаты методом, подобным описанному в RFC 1951 и реализованному в zlib,
  зашифрованы с применением алгоритма CAST-128 в режиме CFB (64 бита) и
  защищены значением цифровой сигнатуры RipeMD-160. При использовании
  в многопоточном приложении необходимо синхронизировать обращение к свойству
  Instance данного класса.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedStreamWriter()

  Создает экземпляр класса AcedStreamWriter. Чтобы связать его с потоком
  данных, в который предполагается поместить фрагмент данных, нужно вызвать
  метод AssignStream. Обычно в программе достаточно иметь один экземпляр класса
  AcedStreamWriter, который может связываться с различными потоками типа
  System.IO.Stream. Для этой цели предназначено статическое свойство Instance
  данного класса.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

public static AcedStreamWriter Instance { get; }

  Возвращает экземпляр класса AcedStreamWriter. Экземпляр данного класса
  кэшируется в статическом поле класса и может использоваться всякий раз,
  когда требуется записать данные в бинарный поток.

public Stream Stream { get; }

  Возвращает ссылку на поток типа System.IO.Stream, ассоциированный с данным
  экземпляром класса AcedStreamWriter.

public AcedCompressionLevel CompressionLevel { get; set; }

  Читает или устанавливает значение, которое определяет режим сжатия данных
  при записи их в бинарный поток. Если это свойство равно значению Store,
  данные, помещаемые в поток, не сжимаются. Режим Fast является оптимальным
  по соотношению скорость/степень сжатия. В режиме Maximum достигается
  максимальная степень сжатия, доступная для реализованного здесь алгоритма.

public AcedDeflator Deflator { get; set; }

  Возвращает или устанавливает экземпляр класса AcedDeflator, который
  используется при сжатии данных, помещаемых в бинарный поток. Если значение
  этого свойства равно null, используется общий экземпляр класса AcedDeflator,
  возвращаемый свойством AcedDeflator.Instance.

public long Length { get; }

  Возвращает общее число байт, помещенное в выходной бинарный поток.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public void Dispose()

  Если с экземпляром класса AcedStreamReader ассоциирован поток типа
  System.IO.Stream, содержимое внутреннего буфера сбрасывается в поток,
  а затем поток закрывается вызовом метода Close().

public void AssignStream(Stream stream, AcedCompressionLevel compressionLevel)

  Связывает с экземпляром класса AcedStreamWriter поток типа System.IO.Stream,
  передаваемый параметром stream. Если с данным экземпляром связан другой
  поток, этот поток закрывается. Параметр compressionLevel, если он не равен
  значению Store, задает режим сжатия данных, помещаемых в поток. Метод
  AssignStream с двумя параметрами можно использовать, если записываемые
  в поток данные не должны шифроваться. Перед вызовом метода AssignStream()
  поток должен быть спозиционирован на предполагаемое место записи фрагмента
  данных.

public void AssignStream(Stream stream, AcedCompressionLevel compressionLevel,
  Guid keyGuid)

  Связывает с экземпляром класса AcedStreamWriter поток типа System.IO.Stream,
  передаваемый параметром stream. Если с данным экземпляром связан другой
  поток, этот поток закрывается. Параметр compressionLevel, если он не равен
  значению Store, задает режим сжатия данных, помещаемых в поток. Параметр
  keyGuid, если он отличен от Guid.Empty, задает ключ для шифрования данных.
  При использовании режима шифрования к каждому фрагменту данных добавляется
  цифровая сигнатура RipeMD-160 для контроля целостности. Перед вызовом метода
  AssignStream() поток должен быть спозиционирован на предполагаемое место
  записи фрагмента данных.
  
public void Close(bool closeStream)

  Если с данным экземпляром класса AcedStreamWriter ассоциирован поток типа
  System.IO.Stream, содержимое внутреннего буфера сбрасывается в поток, после
  чего он отключается от AcedStreamWriter. Если параметр closeStream равен
  True, то поток, кроме того, закрывается вызовом метода Close() потока.

public void Flush()

  Сбрасывает несохраненные данные из буфера в выходной поток, ассоциированный
  с данным экземпляром класса AcedStreamWriter.

public void Reset()

  Устанавливает текущую позицию в бинарном потоке на начало записанного
  фрагмента данных. Этот метод можно вызывать только если поток типа
  System.IO.Stream, ассоциированный с данным экземпляром класса
  AcedStreamWriter, допускает произвольное позиционирование. После вызова
  метода Reset данные могут быть повторно записаны в тот же поток.

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

public static void Release()

  Освобождает внутреннюю ссылку на экземпляр класса AcedStreamWriter. Тогда,
  при отсутствии других ссылок на данный экземпляр, он может быть уничтожен
  сборщиком мусора, а занимаемая им память возвращена системе.

--------------------------------------------------------------------------------
*/
#endregion

using System;
using System.IO;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedStreamWriter class

    public class AcedStreamWriter : IDisposable, IAcedWriter
    {
        private static AcedStreamWriter _instance;

        private int _position;
        private byte[] _buffer;
        private byte[] _compressionBuffer;
        private long _streamStartPosition;
        private long _basePosition;
        private long _iv;
        private Stream _stream;
        private AcedCompressionLevel _compressionLevel;
        private AcedDeflator _deflator;
        private int[] _key;
        private int[] _hashData;
        private int[] _hash;

        public static AcedStreamWriter Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new AcedStreamWriter();
                return _instance;
            }
        }

        public AcedStreamWriter()
        {
            _buffer = new byte[AcedConsts.BufferSize];
            _compressionBuffer = new byte[AcedConsts.BufferSize];
            _hashData = AcedRipeMD.Initialize();
        }

        public void Dispose()
        {
            Close(true);
        }

        public Stream Stream
        {
            get { return _stream; }
        }

        public AcedCompressionLevel CompressionLevel
        {
            get { return _compressionLevel; }
            set { _compressionLevel = value; }
        }

        public AcedDeflator Deflator
        {
            get { return _deflator; }
            set { _deflator = value; }
        }

        public long Length
        {
            get
            {
                if (_stream == null)
                    AcedConsts.ThrowStreamClosedException();
                return _basePosition + _position - 32;
            }
        }

        public void AssignStream(Stream stream, AcedCompressionLevel compressionLevel)
        {
            AssignStream(stream, compressionLevel, Guid.Empty);
        }

        public unsafe void AssignStream(Stream stream, AcedCompressionLevel compressionLevel, Guid keyGuid)
        {
            Close(true);
            if (stream == null)
                AcedConsts.ThrowArgumentNullException("stream");
            if (!stream.CanWrite)
                AcedWritingNotSupportedException.Throw();
            _stream = stream;
            _compressionLevel = compressionLevel;
            if (!keyGuid.Equals(Guid.Empty))
            {
                _key = AcedCast5.ScheduleKey(keyGuid.ToByteArray());
                _iv = AcedCast5.GetOrdinaryIV(_key);
                _hash = null;
            }
            else
                _key = null;
            _streamStartPosition = stream.Position;
            fixed (byte* pBuffer = &_buffer[0])
                *((long*)pBuffer) = -1L;
            stream.Write(_buffer, 0, 8);
            _basePosition = 0;
            _position = 32;
        }

        public unsafe void Close(bool closeStream)
        {
            if (_stream == null)
                return;
            Flush();
            fixed (byte* pBuffer = &_buffer[0])
            {
                *((int*)pBuffer) = 0;
                _stream.Write(_buffer, 0, 4);
                if (_stream.CanSeek)
                {
                    long currentPosition = _stream.Position;
                    _stream.Seek(_streamStartPosition, SeekOrigin.Begin);
                    *((long*)pBuffer) = _basePosition;
                    _stream.Write(_buffer, 0, 8);
                    _stream.Seek(currentPosition, SeekOrigin.Begin);
                }
            }
            if (_key != null)
            {
                AcedCast5.ClearKey(_key);
                _iv = 0;
                _hash = null;
            }
            if (closeStream)
            {
                if (_stream.CanSeek)
                    _stream.SetLength(_stream.Position);
                _stream.Close();
            }
            _stream = null;
        }

        public unsafe void Flush()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            int length = _position - 32;
            if (length == 0)
                return;
            _basePosition += length;
            if (_compressionLevel == AcedCompressionLevel.Store)
            {
                fixed (byte* pBuffer = &_buffer[28])
                    *((int*)pBuffer) = -length;
            }
            else
            {
                AcedDeflator deflator = _deflator;
                if (deflator == null)
                    deflator = AcedDeflator.Instance;
                length = deflator.Compress(_buffer, 32, length, _compressionLevel, _compressionBuffer, 28);
                byte[] temp = _compressionBuffer;
                _compressionBuffer = _buffer;
                _buffer = temp;
            }
            length += 4;
            int offset = 28;
            if (_key != null)
            {
                if (_hash != null)
                {
                    AcedRipeMD.Initialize(_hashData, _hash);
                    AcedRipeMD.Update(_hashData, _buffer, 28, length);
                    AcedRipeMD.Finalize(_hashData, _hash);
                }
                else
                {
                    AcedRipeMD.Initialize(_hashData);
                    AcedRipeMD.Update(_hashData, _buffer, 28, length);
                    _hash = AcedRipeMD.Finalize(_hashData);
                }
                _iv = AcedCast5.Encrypt(_key, _buffer, 28, length, _iv);
                AcedRipeMD.Copy(_hash, _buffer, 8);
                length += 20;
                offset = 8;
            }
            fixed (byte* pBuffer = &_buffer[offset - 8])
            {
                int* p = (int*)pBuffer;
                p[0] = length + 4;
                p[1] = AcedBinary.Adler32(_buffer, offset, length);
            }
            _stream.Write(_buffer, offset - 8, length + 8);
            _position = 32;
        }

        public void Reset()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (!_stream.CanSeek)
                AcedSeekingNotSupportedException.Throw();
            if (_key != null)
                _iv = AcedCast5.GetOrdinaryIV(_key);
            _hash = null;
            _stream.Seek(_streamStartPosition + 8, SeekOrigin.Begin);
            _basePosition = 0;
            _position = 32;
        }

        public void WriteBoolean(bool value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == AcedConsts.BufferSize)
                Flush();
            _buffer[_position] = value ? (byte)1 : (byte)0;
            _position++;
        }

        public void WriteByte(byte value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == AcedConsts.BufferSize)
                Flush();
            _buffer[_position] = value;
            _position++;
        }

        public void WriteByteArray(byte[] value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (value == null)
            {
                WriteUInt16((ushort)AcedConsts.NullValue);
                return;
            }
            int byteCount = value.Length;
            if (byteCount < AcedConsts.LongLength)
                WriteUInt16((ushort)byteCount);
            else
            {
                WriteUInt16((ushort)AcedConsts.LongLength);
                WriteInt32(byteCount);
            }
            int index = 0;
            while (byteCount > 0)
            {
                int n = AcedConsts.BufferSize - _position;
                if (n == 0)
                {
                    Flush();
                    n = AcedConsts.BufferSize - _position;
                }
                if (byteCount < n)
                    n = byteCount;
                Buffer.BlockCopy(value, index, _buffer, _position, n);
                _position += n;
                byteCount -= n;
                index += n;
            }
        }

        public void WriteChar(char value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 2)
                Flush();
            uint x = (uint)value;
            _buffer[_position] = (byte)x;
            _buffer[_position + 1] = (byte)(x >> 8);
            _position += 2;
        }

        public unsafe void WriteDateTime(DateTime value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 8)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((long*)pBuffer) = value.Ticks;
            _position += 8;
        }

        public unsafe void WriteDecimal(decimal value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 16)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((decimal*)pBuffer) = value;
            _position += 16;
        }

        public unsafe void WriteSingle(float value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 4)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((float*)pBuffer) = value;
            _position += 4;
        }

        public unsafe void WriteDouble(double value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 8)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((double*)pBuffer) = value;
            _position += 8;
        }

        public unsafe void WriteGuid(Guid value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 16)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((Guid*)pBuffer) = value;
            _position += 16;
        }

        public void WriteInt16(short value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 2)
                Flush();
            uint x = (uint)((ushort)value);
            _buffer[_position] = (byte)x;
            _buffer[_position + 1] = (byte)(x >> 8);
            _position += 2;
        }

        public unsafe void WriteInt32(int value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 4)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((int*)pBuffer) = value;
            _position += 4;
        }

        public unsafe void WriteInt64(long value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 8)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((long*)pBuffer) = value;
            _position += 8;
        }

        public unsafe void WriteString(string value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (value == null)
            {
                WriteUInt16((ushort)AcedConsts.NullValue);
                return;
            }
            int charCount = value.Length;
            if (charCount < AcedConsts.LongLength)
                WriteUInt16((ushort)charCount);
            else
            {
                WriteUInt16((ushort)AcedConsts.LongLength);
                WriteInt32(charCount);
            }
            charCount <<= 1;
            int index = 0;
            while (charCount > 0)
            {
                int n = AcedConsts.BufferSize - _position;
                if (n == 0)
                {
                    Flush();
                    n = AcedConsts.BufferSize - _position;
                }
                if (charCount < n)
                    n = charCount;
                fixed (char* pValue = value)
                fixed (byte* p = &_buffer[_position])
                    AcedBinary.CopyBytes((byte*)pValue + index, p, n);
                _position += n;
                charCount -= n;
                index += n;
            }
        }

        public unsafe void WriteTimeSpan(TimeSpan value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 8)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((long*)pBuffer) = value.Ticks;
            _position += 8;
        }

        [CLSCompliant(false)]
        public void WriteUInt16(ushort value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 2)
                Flush();
            _buffer[_position] = (byte)value;
            _buffer[_position + 1] = (byte)(value >> 8);
            _position += 2;
        }

        [CLSCompliant(false)]
        public unsafe void WriteUInt32(uint value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 4)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((uint*)pBuffer) = value;
            _position += 4;
        }

        [CLSCompliant(false)]
        public unsafe void WriteUInt64(ulong value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position > AcedConsts.BufferSize - 8)
                Flush();
            fixed (byte* pBuffer = &_buffer[_position])
                *((ulong*)pBuffer) = value;
            _position += 8;
        }

        [CLSCompliant(false)]
        public void WriteSByte(sbyte value)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == AcedConsts.BufferSize)
                Flush();
            _buffer[_position] = (byte)value;
            _position++;
        }

        private void IntWriteArray(System.Array values, int index, int count)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            while (count > 0)
            {
                int n = AcedConsts.BufferSize - _position;
                if (n == 0)
                {
                    Flush();
                    n = AcedConsts.BufferSize - _position;
                }
                if (count < n)
                    n = count;
                Buffer.BlockCopy(values, index, _buffer, _position, n);
                _position += n;
                index += n;
                count -= n;
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

        public static void Release()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        void IAcedWriter.Close()
        {
            Close(true);
        }
    }
}
