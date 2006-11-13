
#region ����� AcedStreamReader
/*
--------------------------------------------------------------------------------
public class AcedStreamReader : IDisposable, IAcedReader

  ������������ ��� ������ ������ �� ��������� ������, ������� ���� ��������
  � ����� ����������� ������ AcedStreamWriter. ������ ���������� �����������
  ������ ������. ����� ����, ��� ����� ���� �����, ����������� � ��������
  ��������� �������� ��������� RipeMD-160. ��� ������������� � �������������
  ���������� ���������� ���������������� ��������� � �������� Instance
  ������� ������.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedStreamReader()

  ������� ��������� ������ AcedStreamReader. ����� ������� ��� � �������
  ������, �� �������� �������� ����������, ����� ������� ����� AssignStream.
  ������ � ��������� ���������� ����� ���� ��������� ������ AcedStreamReader,
  ������� ����� ����������� � ���������� �������� ���� System.IO.Stream.
  ��� ���� ���� ������������� ����������� �������� Instance ������� ������.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public static AcedStreamReader Instance { get; }

  ���������� ��������� ������ AcedStreamReader. ��������� ������� ������
  ���������� � ����������� ���� ������ � ����� �������������� ������ ���,
  ����� ��������� ��������� ������ �� ��������� ������.

public Stream Stream { get; }

  ���������� ������ �� ����� ���� System.IO.Stream, ��������������� � ������
  ����������� ������ AcedStreamReader.

public AcedInflator Inflator { get; set; }
  
  ���������� ��� ������������� ��������� ������ AcedInflator, �������
  ������������ ��� ���������� ������ ��������� ������. ���� �������� �����
  �������� ����� null, ������������ ����� ��������� ������ AcedInflator,
  ������������ ��������� AcedInflator.Instance.
 
public long Length { get; }

  ���������� ����� ��������� ������, ����������� � �������� �����, �.�. �����
  ����, ������� ����� ���� ��������� �� ������. ��� �������� ����� �������
  �������� -1, ���� ��� ������ � ����� ���� AcedStreamWriter ���������������
  � ��� ����� System.IO.Stream �� ����������� ������������ ����������������,
  �.�. �������� CanSeek ������ System.IO.Stream ������� �������� False.

public long Position { get; }

  ���������� ������� ������� � ����������� ��������� ������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public void Dispose()

  ���� � ����������� ������ AcedStreamReader ������������ ����� ����
  System.IO.Stream, ���� ����� ����������� ������� ������ Close().

public void AssignStream(Stream stream)

  ��������� � ����������� ������ AcedStreamReader ����� ���� System.IO.Stream,
  ������������ ���������� stream. ���� � ������ ����������� ������ ������
  �����, ���� ����� �����������. ����� AssignStream � ����� ���������� �����
  ������������, ���� �������� ������, ���������� � �����, �� ��� ����������.
  ����� ������� ������ AssignStream() ����� ������ ���� ��������������� ��
  ������ ��������� ��������� ������.

public void AssignStream(Stream stream, Guid keyGuid)

  ��������� � ����������� ������ AcedStreamReader ����� ���� System.IO.Stream,
  ������������ ���������� stream. ���� � ������ ����������� ������ ������
  �����, ���� ����� �����������. �� ������ ��������� keyGuid ����������
  ��������, ������� ������������ � �������� ����� ��� ������������ ������
  ��������� ������. ���� � ���� ��������� �������� �������� Guid.Empty, ���
  ��������, ��� ������ � ������ �� �����������. ����� ������� ������
  AssignStream() ����� ������ ���� ��������������� �� ������ ���������
  ��������� ������.

public void Close(bool closeStream)

  ���� � ������ ����������� ������ AcedStreamReader ������������ ����� ����
  System.IO.Stream, �� ����������� �� AcedStreamReader. ���� ��������
  closeStream ����� True, �� �����, ����� ����, ����������� ������� ������
  Close() ������.

public void Reset()

  ������������� ������� ������� � �������� ������ �� ������ ��������� ���������
  ������. ���� ����� ����� �������� ������ ���� ����� ���� System.IO.Stream,
  ��������������� � ������ ����������� ������ AcedStreamReader, ���������
  ������������ ����������������. ����� ������ ������ Reset ������ ����� ����
  �������� ��������� �� ���� �� ������.

public void Skip(int count)

  ���������� count ���� �� ������� �������� ������.

public bool ReadBoolean()

  ��������� �������� ���� System.Boolean �� ��������� ������.

public byte ReadByte()

  ��������� �������� ���� System.Byte �� ��������� ������.

public byte[] ReadByteArray()

  ��������� �������� ���� System.Byte[] �� ��������� ������.

public char ReadChar()

  ��������� �������� ���� System.Char �� ��������� ������.

public DateTime ReadDateTime()

  ��������� �������� ���� System.DateTime �� ��������� ������.

public decimal ReadDecimal()

  ��������� �������� ���� System.Decimal �� ��������� ������.

public float ReadSingle()

  ��������� �������� ���� System.Single �� ��������� ������.

public double ReadDouble()

  ��������� �������� ���� System.Double �� ��������� ������.

public Guid ReadGuid()

  ��������� �������� ���� System.Guid �� ��������� ������.

public short ReadInt16()

  ��������� �������� ���� System.Int16 �� ��������� ������.

public int ReadInt32()

  ��������� �������� ���� System.Int32 �� ��������� ������.

public long ReadInt64()

  ��������� �������� ���� System.Int64 �� ��������� ������.

public string ReadString()

  ��������� �������� ���� System.String �� ��������� ������.

public TimeSpan ReadTimeSpan()

  ��������� �������� ���� System.TimeSpan �� ��������� ������.

public ushort ReadUInt16()

  ��������� �������� ���� System.UInt16 �� ��������� ������.

public uint ReadUInt32()

  ��������� �������� ���� System.UInt32 �� ��������� ������.

public ulong ReadUInt64()

  ��������� �������� ���� System.UInt64 �� ��������� ������.

public sbyte ReadSByte()

  ��������� �������� ���� System.SByte �� ��������� ������.

public void Read(bool[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Boolean � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(byte[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Byte � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(char[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Char � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(DateTime[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.DateTime � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(decimal[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Decimal � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(float[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Single � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(double[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Double � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(Guid[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Guid � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(short[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int16 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(int[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int32 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(long[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int64 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(TimeSpan[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.TimeSpan � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(ushort[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.UInt16 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(uint[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.UInt32 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(ulong[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.UInt64 � ��������
  �� � ������ values, ������� � �������� � �������� index.

public void Read(sbyte[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.SByte � ��������
  �� � ������ values, ������� � �������� � �������� index.

public int StreamRead(byte[] buffer, int offset, int count)

  ��������� count ���� �� ��������� ������ � ��������� �� � ������� buffer,
  ������� �� �������� offset. ���� ����� ����, ���������� � ������ ������
  �������� count, ����������� ������� ����, ������� ����. ������� ����������
  ����� ����, ���������� ����������� �� ��������� ������.

public int StreamReadByte()

  ��������� ��������� ���� �� ��������� ������. ���� ��������� ����� ������,
  �.�. ��� ����� ��� ���������, ������� ���������� �������� -1.
  
public static void Release()

  ����������� ���������� ������ �� ��������� ������ AcedStreamReader. �����,
  ��� ���������� ������ ������ �� ������ ���������, �� ����� ���� ���������
  ��������� ������, � ���������� �� ������ ���������� �������.

--------------------------------------------------------------------------------
*/
#endregion

using System;
using System.IO;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedStreamReader class

    public class AcedStreamReader : IDisposable, IAcedReader
    {
        private static AcedStreamReader _instance;

        private int _position;
        private int _bufferLength;
        private int _offset;
        private byte[] _buffer;
        private byte[] _decompressionBuffer;
        private long _length;
        private long _streamStartPosition;
        private long _basePosition;
        private long _iv;
        private int _nextBufferLength;
        private Stream _stream;
        private AcedInflator _inflator;
        private int[] _key;
        private int[] _hashData;
        private int[] _hash;
        private char[] _chars;

        public static AcedStreamReader Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new AcedStreamReader();
                return _instance;
            }
        }

        public AcedStreamReader()
        {
            _buffer = new byte[AcedConsts.BufferSize];
            _decompressionBuffer = new byte[AcedConsts.BufferSize];
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

        public AcedInflator Inflator
        {
            get { return _inflator; }
            set { _inflator = value; }
        }

        public long Length
        {
            get
            {
                if (_stream == null)
                    AcedConsts.ThrowStreamClosedException();
                return _length;
            }
        }

        public long Position
        {
            get
            {
                if (_stream == null)
                    AcedConsts.ThrowStreamClosedException();
                return _basePosition - _offset + _position;
            }
        }

        public void AssignStream(Stream stream)
        {
            AssignStream(stream, Guid.Empty);
        }

        public unsafe void AssignStream(Stream stream, Guid keyGuid)
        {
            Close(true);
            if (stream == null)
                AcedConsts.ThrowArgumentNullException("stream");
            if (!stream.CanRead)
                AcedReadingNotSupportedException.Throw();
            _stream = stream;
            if (!keyGuid.Equals(Guid.Empty))
            {
                _key = AcedCast5.ScheduleKey(keyGuid.ToByteArray());
                _iv = AcedCast5.GetOrdinaryIV(_key);
                _hash = null;
            }
            else
                _key = null;
            _streamStartPosition = stream.Position;
            stream.Read(_buffer, 0, 12);
            fixed (byte* pBuffer = &_buffer[0])
            {
                _length = *((long*)pBuffer);
                _nextBufferLength = *((int*)(pBuffer + 8));
            }
            _basePosition = 0;
            _offset = 0;
            _bufferLength = 0;
            _position = 0;
        }

        public void Close(bool closeStream)
        {
            if (_stream != null)
            {
                if (_key != null)
                {
                    AcedCast5.ClearKey(_key);
                    _iv = 0;
                    _hash = null;
                }
                if (closeStream)
                    _stream.Close();
                _stream = null;
            }
        }

        private unsafe void LoadBuffer()
        {
            if (_nextBufferLength == 0)
                AcedReadBeyondTheEndException.Throw();
            _basePosition += _bufferLength;
            int length = _nextBufferLength;
            _stream.Read(_buffer, 0, length + 4);
            int offset;
            fixed (byte* pBuffer = &_buffer[0])
            {
                int* p = (int*)pBuffer;
                _nextBufferLength = *((int*)((byte*)p + length));
                length -= 4;
                if (length < 0 || AcedBinary.Adler32(_buffer, 4, length) != p[0])
                    AcedDataCorruptedException.Throw();
                if (_key == null)
                {
                    length = p[1];
                    offset = 4;
                }
                else
                {
                    length -= 20;
                    if (length < 0)
                        AcedDataCorruptedException.Throw();
                    _iv = AcedCast5.Decrypt(_key, _buffer, 24, length, _iv);
                    if (_hash != null)
                    {
                        AcedRipeMD.Initialize(_hashData, _hash);
                        AcedRipeMD.Update(_hashData, _buffer, 24, length);
                        AcedRipeMD.Finalize(_hashData, _hash);
                    }
                    else
                    {
                        AcedRipeMD.Initialize(_hashData);
                        AcedRipeMD.Update(_hashData, _buffer, 24, length);
                        _hash = AcedRipeMD.Finalize(_hashData);
                    }
                    fixed (int* pHash = &_hash[0])
                    {
                        int* h = pHash;
                        if (h[0] != p[1] || h[1] != p[2] || h[2] != p[3] || h[3] != p[4] || h[4] != p[5])
                            AcedWrongDecryptionKeyException.Throw();
                    }
                    length = p[6];
                    offset = 24;
                }
                if (length <= 0)
                {
                    length = -length;
                    offset += 4;
                }
                else
                {
                    AcedInflator inflator = _inflator;
                    if (inflator == null)
                        inflator = AcedInflator.Instance;
                    length = inflator.Decompress(_buffer, offset, _decompressionBuffer, 0);
                    byte[] temp = _decompressionBuffer;
                    _decompressionBuffer = _buffer;
                    _buffer = temp;
                    offset = 0;
                }
            }
            _offset = offset;
            _bufferLength = length + offset;
            _position = offset;
        }

        public unsafe void Reset()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (!_stream.CanSeek)
                AcedSeekingNotSupportedException.Throw();
            if (_key != null)
                _iv = AcedCast5.GetOrdinaryIV(_key);
            _hash = null;
            _stream.Seek(_streamStartPosition + 8, SeekOrigin.Begin);
            _stream.Read(_buffer, 0, 4);
            fixed (byte* pBuffer = &_buffer[0])
                _nextBufferLength = *((int*)pBuffer);
            _basePosition = 0;
            _offset = 0;
            _bufferLength = 0;
            _position = 0;
        }

        public void Skip(int count)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            while (count > 0)
            {
                int n = _bufferLength - _position;
                if (n == 0)
                {
                    LoadBuffer();
                    n = _bufferLength - _position;
                }
                if (count < n)
                    n = count;
                _position += n;
                count -= n;
            }
        }

        private unsafe void IntReadValue(byte* p, int count)
        {
            int n = _bufferLength - _position;
            fixed (byte* pBuffer = &_buffer[_position])
                AcedBinary.CopyBytes(pBuffer, p, n);
            p += n;
            n = count - n;
            LoadBuffer();
            fixed (byte* pBuffer = &_buffer[0])
                AcedBinary.CopyBytes(pBuffer, p, n);
            _position = n;
        }

        public bool ReadBoolean()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            _position = n + 1;
            if (_buffer[n] == 0)
                return false;
            return true;
        }

        public byte ReadByte()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            _position = n + 1;
            return _buffer[n];
        }

        public byte[] ReadByteArray()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            int byteCount = ReadUInt16();
            if (byteCount == AcedConsts.NullValue)
                return null;
            if (byteCount == AcedConsts.LongLength)
                byteCount = ReadInt32();
            byte[] result = new byte[byteCount];
            int index = 0;
            while (byteCount > 0)
            {
                int n = _bufferLength - _position;
                if (n == 0)
                {
                    LoadBuffer();
                    n = _bufferLength - _position;
                }
                if (byteCount < n)
                    n = byteCount;
                Buffer.BlockCopy(_buffer, _position, result, index, n);
                _position += n;
                index += n;
                byteCount -= n;
            }
            return result;
        }

        public unsafe char ReadChar()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 2 <= _bufferLength)
            {
                _position = n + 2;
                return (char)(_buffer[n] + (_buffer[n + 1] << 8));
            }
            char result;
            IntReadValue((byte*)&result, 2);
            return result;
        }

        public unsafe DateTime ReadDateTime()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 8 <= _bufferLength)
            {
                _position = n + 8;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((DateTime*)pBuffer);
            }
            DateTime result;
            IntReadValue((byte*)&result, 8);
            return result;
        }

        public unsafe decimal ReadDecimal()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 16 <= _bufferLength)
            {
                _position = n + 16;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((decimal*)pBuffer);
            }
            decimal result;
            IntReadValue((byte*)&result, 16);
            return result;
        }

        public unsafe float ReadSingle()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 4 <= _bufferLength)
            {
                _position = n + 4;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((float*)pBuffer);
            }
            float result;
            IntReadValue((byte*)&result, 4);
            return result;
        }

        public unsafe double ReadDouble()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 8 <= _bufferLength)
            {
                _position = n + 8;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((double*)pBuffer);
            }
            double result;
            IntReadValue((byte*)&result, 8);
            return result;
        }

        public unsafe Guid ReadGuid()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 16 <= _bufferLength)
            {
                _position = n + 16;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((Guid*)pBuffer);
            }
            Guid result;
            IntReadValue((byte*)&result, 16);
            return result;
        }

        public unsafe short ReadInt16()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 2 <= _bufferLength)
            {
                _position = n + 2;
                return (short)((ushort)(_buffer[n] + (_buffer[n + 1] << 8)));
            }
            short result;
            IntReadValue((byte*)&result, 2);
            return result;
        }

        public unsafe int ReadInt32()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 4 <= _bufferLength)
            {
                _position = n + 4;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((int*)pBuffer);
            }
            int result;
            IntReadValue((byte*)&result, 4);
            return result;
        }

        public unsafe long ReadInt64()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 8 <= _bufferLength)
            {
                _position = n + 8;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((long*)pBuffer);
            }
            long result;
            IntReadValue((byte*)&result, 8);
            return result;
        }

        public string ReadString()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            int charCount = ReadUInt16();
            if (charCount == AcedConsts.NullValue)
                return null;
            if (charCount == 0)
                return String.Empty;
            if (charCount == AcedConsts.LongLength)
                charCount = ReadInt32();
            if (_chars == null || _chars.Length < charCount)
                _chars = new char[charCount > 200 ? charCount + 50 : 200];
            int byteCount = charCount << 1;
            int index = 0;
            while (byteCount > 0)
            {
                int n = _bufferLength - _position;
                if (n == 0)
                {
                    LoadBuffer();
                    n = _bufferLength - _position;
                }
                if (byteCount < n)
                    n = byteCount;
                Buffer.BlockCopy(_buffer, _position, _chars, index, n);
                _position += n;
                index += n;
                byteCount -= n;
            }
            return new string(_chars, 0, charCount);
        }

        public unsafe TimeSpan ReadTimeSpan()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 8 <= _bufferLength)
            {
                _position = n + 8;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((TimeSpan*)pBuffer);
            }
            TimeSpan result;
            IntReadValue((byte*)&result, 8);
            return result;
        }

        [CLSCompliant(false)]
        public unsafe ushort ReadUInt16()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 2 <= _bufferLength)
            {
                _position = n + 2;
                return (ushort)(_buffer[n] + (_buffer[n + 1] << 8));
            }
            ushort result;
            IntReadValue((byte*)&result, 2);
            return result;
        }

        [CLSCompliant(false)]
        public unsafe uint ReadUInt32()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 4 <= _bufferLength)
            {
                _position = n + 4;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((uint*)pBuffer);
            }
            uint result;
            IntReadValue((byte*)&result, 4);
            return result;
        }

        [CLSCompliant(false)]
        public unsafe ulong ReadUInt64()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            if (n + 8 <= _bufferLength)
            {
                _position = n + 8;
                fixed (byte* pBuffer = &_buffer[n])
                    return *((ulong*)pBuffer);
            }
            ulong result;
            IntReadValue((byte*)&result, 8);
            return result;
        }

        [CLSCompliant(false)]
        public sbyte ReadSByte()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
                LoadBuffer();
            int n = _position;
            _position = n + 1;
            return (sbyte)_buffer[n];
        }

        private void IntReadArray(System.Array values, int index, int count)
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            while (count > 0)
            {
                int n = _bufferLength - _position;
                if (n == 0)
                {
                    LoadBuffer();
                    n = _bufferLength - _position;
                }
                if (count < n)
                    n = count;
                Buffer.BlockCopy(_buffer, _position, values, index, n);
                _position += n;
                index += n;
                count -= n;
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
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            int newIndex = offset;
            while (count > 0)
            {
                int n = _bufferLength - _position;
                if (n == 0)
                {
                    if (_nextBufferLength == 0)
                        break;
                    LoadBuffer();
                    n = _bufferLength - _position;
                }
                if (count < n)
                    n = count;
                Buffer.BlockCopy(_buffer, _position, buffer, newIndex, n);
                newIndex += n;
                _position += n;
                count -= n;
            }
            return newIndex - offset;
        }

        public int StreamReadByte()
        {
            if (_stream == null)
                AcedConsts.ThrowStreamClosedException();
            if (_position == _bufferLength)
            {
                if (_nextBufferLength == 0)
                    return -1;
                LoadBuffer();
            }
            int n = _position;
            _position = n + 1;
            return _buffer[n];
        }

        public static void Release()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        void IAcedReader.Close()
        {
            Close(true);
        }
    }
}
