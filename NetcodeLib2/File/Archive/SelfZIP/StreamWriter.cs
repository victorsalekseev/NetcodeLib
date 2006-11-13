
#region ����� AcedStreamWriter
/*
--------------------------------------------------------------------------------
public class AcedStreamWriter : IDisposable, IAcedWriter

  ������������ ��� ��������� ������ � �������� ����� ���� System.IO.Stream,
  ��������������� � ����������� ������� ������. ��� ���� ������ ����� ����
  ����� �������, �������� ���������� � RFC 1951 � �������������� � zlib,
  ����������� � ����������� ��������� CAST-128 � ������ CFB (64 ����) �
  �������� ��������� �������� ��������� RipeMD-160. ��� �������������
  � ������������� ���������� ���������� ���������������� ��������� � ��������
  Instance ������� ������.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedStreamWriter()

  ������� ��������� ������ AcedStreamWriter. ����� ������� ��� � �������
  ������, � ������� �������������� ��������� �������� ������, ����� �������
  ����� AssignStream. ������ � ��������� ���������� ����� ���� ��������� ������
  AcedStreamWriter, ������� ����� ����������� � ���������� �������� ����
  System.IO.Stream. ��� ���� ���� ������������� ����������� �������� Instance
  ������� ������.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public static AcedStreamWriter Instance { get; }

  ���������� ��������� ������ AcedStreamWriter. ��������� ������� ������
  ���������� � ����������� ���� ������ � ����� �������������� ������ ���,
  ����� ��������� �������� ������ � �������� �����.

public Stream Stream { get; }

  ���������� ������ �� ����� ���� System.IO.Stream, ��������������� � ������
  ����������� ������ AcedStreamWriter.

public AcedCompressionLevel CompressionLevel { get; set; }

  ������ ��� ������������� ��������, ������� ���������� ����� ������ ������
  ��� ������ �� � �������� �����. ���� ��� �������� ����� �������� Store,
  ������, ���������� � �����, �� ���������. ����� Fast �������� �����������
  �� ����������� ��������/������� ������. � ������ Maximum �����������
  ������������ ������� ������, ��������� ��� �������������� ����� ���������.

public AcedDeflator Deflator { get; set; }

  ���������� ��� ������������� ��������� ������ AcedDeflator, �������
  ������������ ��� ������ ������, ���������� � �������� �����. ���� ��������
  ����� �������� ����� null, ������������ ����� ��������� ������ AcedDeflator,
  ������������ ��������� AcedDeflator.Instance.

public long Length { get; }

  ���������� ����� ����� ����, ���������� � �������� �������� �����.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public void Dispose()

  ���� � ����������� ������ AcedStreamReader ������������ ����� ����
  System.IO.Stream, ���������� ����������� ������ ������������ � �����,
  � ����� ����� ����������� ������� ������ Close().

public void AssignStream(Stream stream, AcedCompressionLevel compressionLevel)

  ��������� � ����������� ������ AcedStreamWriter ����� ���� System.IO.Stream,
  ������������ ���������� stream. ���� � ������ ����������� ������ ������
  �����, ���� ����� �����������. �������� compressionLevel, ���� �� �� �����
  �������� Store, ������ ����� ������ ������, ���������� � �����. �����
  AssignStream � ����� ����������� ����� ������������, ���� ������������
  � ����� ������ �� ������ �����������. ����� ������� ������ AssignStream()
  ����� ������ ���� ��������������� �� �������������� ����� ������ ���������
  ������.

public void AssignStream(Stream stream, AcedCompressionLevel compressionLevel,
  Guid keyGuid)

  ��������� � ����������� ������ AcedStreamWriter ����� ���� System.IO.Stream,
  ������������ ���������� stream. ���� � ������ ����������� ������ ������
  �����, ���� ����� �����������. �������� compressionLevel, ���� �� �� �����
  �������� Store, ������ ����� ������ ������, ���������� � �����. ��������
  keyGuid, ���� �� ������� �� Guid.Empty, ������ ���� ��� ���������� ������.
  ��� ������������� ������ ���������� � ������� ��������� ������ �����������
  �������� ��������� RipeMD-160 ��� �������� �����������. ����� ������� ������
  AssignStream() ����� ������ ���� ��������������� �� �������������� �����
  ������ ��������� ������.
  
public void Close(bool closeStream)

  ���� � ������ ����������� ������ AcedStreamWriter ������������ ����� ����
  System.IO.Stream, ���������� ����������� ������ ������������ � �����, �����
  ���� �� ����������� �� AcedStreamWriter. ���� �������� closeStream �����
  True, �� �����, ����� ����, ����������� ������� ������ Close() ������.

public void Flush()

  ���������� ������������� ������ �� ������ � �������� �����, ���������������
  � ������ ����������� ������ AcedStreamWriter.

public void Reset()

  ������������� ������� ������� � �������� ������ �� ������ �����������
  ��������� ������. ���� ����� ����� �������� ������ ���� ����� ����
  System.IO.Stream, ��������������� � ������ ����������� ������
  AcedStreamWriter, ��������� ������������ ����������������. ����� ������
  ������ Reset ������ ����� ���� �������� �������� � ��� �� �����.

public void WriteBoolean(bool value)

  ��������� � �������� ������ �������� ���� System.Boolean.

public void WriteByte(byte value)

  ��������� � �������� ������ �������� ���� System.Byte.

public void WriteByteArray(byte[] value)

  ��������� � �������� ������ �������� ���� System.Byte[].

public void WriteChar(char value)

  ��������� � �������� ������ �������� ���� System.Char.

public void WriteDateTime(DateTime value)

  ��������� � �������� ������ �������� ���� System.DateTime.

public void WriteDecimal(decimal value)

  ��������� � �������� ������ �������� ���� System.Decimal.

public void WriteSingle(float value)

  ��������� � �������� ������ �������� ���� System.Single.

public void WriteDouble(double value)

  ��������� � �������� ������ �������� ���� System.Double.

public void WriteGuid(Guid value)

  ��������� � �������� ������ �������� ���� System.Guid.

public void WriteInt16(short value)

  ��������� � �������� ������ �������� ���� System.Int16.

public void WriteInt32(int value)

  ��������� � �������� ������ �������� ���� System.Int32.

public void WriteInt64(long value)

  ��������� � �������� ������ �������� ���� System.Int64.

public void WriteString(string value)

  ��������� � �������� ������ �������� ���� System.String.

public void WriteTimeSpan(TimeSpan value)

  ��������� � �������� ������ �������� ���� System.TimeSpan.

public void WriteUInt16(ushort value)

  ��������� � �������� ������ �������� ���� System.UInt16.

public void WriteUInt32(uint value)

  ��������� � �������� ������ �������� ���� System.UInt32.

public void WriteUInt64(ulong value)

  ��������� � �������� ������ �������� ���� System.UInt64.

public void WriteSByte(sbyte value)

  ��������� � �������� ������ �������� ���� System.SByte.

public void Write(bool[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Boolean
  �� ������� values, ������� � �������� � �������� index.

public void Write(byte[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Byte
  �� ������� values, ������� � �������� � �������� index.

public void Write(char[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Char
  �� ������� values, ������� � �������� � �������� index.

public void Write(DateTime[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.DateTime
  �� ������� values, ������� � �������� � �������� index.

public void Write(decimal[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Decimal
  �� ������� values, ������� � �������� � �������� index.

public void Write(float[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Single
  �� ������� values, ������� � �������� � �������� index.

public void Write(double[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Double
  �� ������� values, ������� � �������� � �������� index.

public void Write(Guid[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Guid
  �� ������� values, ������� � �������� � �������� index.

public void Write(short[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int16
  �� ������� values, ������� � �������� � �������� index.

public void Write(int[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int32
  �� ������� values, ������� � �������� � �������� index.

public void Write(long[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int64
  �� ������� values, ������� � �������� � �������� index.

public void Write(TimeSpan[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.TimeSpan
  �� ������� values, ������� � �������� � �������� index.

public void Write(ushort[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.UInt16
  �� ������� values, ������� � �������� � �������� index.

public void Write(uint[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.UInt32
  �� ������� values, ������� � �������� � �������� index.

public void Write(ulong[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.UInt64
  �� ������� values, ������� � �������� � �������� index.

public void Write(sbyte[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.SByte
  �� ������� values, ������� � �������� � �������� index.

public static void Release()

  ����������� ���������� ������ �� ��������� ������ AcedStreamWriter. �����,
  ��� ���������� ������ ������ �� ������ ���������, �� ����� ���� ���������
  ��������� ������, � ���������� �� ������ ���������� �������.

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
