
#region ����� AcedMemoryReader
/*
--------------------------------------------------------------------------------
public class AcedMemoryReader : IAcedReader

  ������������ ��� ������ ������ �� ������� ����, ���������� �����������
  ������ AcedMemoryWriter. ������ � ������� ���������� ����������� ������
  ������. ����� ����, ��� ����� ���� �����, ����������� � �������� ���������
  �������� ��������� RipeMD-160. ��� ������������� � ������������� ����������
  ���������� ���������������� ����� ������� ������� ������.

--------------------------------------------------------------------------------
ctor's
--------------------------------------------------------------------------------

public AcedMemoryReader(byte[] bytes, int offset, int length)

  ������� ��������� ������ AcedMemoryReader �� ��������� ������� bytes,
  ������� � ������� offset, ������ length ����. ��������������, ��� ������
  � ������� �� �����������. ���� ������ ���������, ��� ����������
  ������������ ����� ��������� ������ AcedInflator, ������� ������������
  ��������� AcedInflator.Instance.

public AcedMemoryReader(byte[] bytes, int offset, int length,
  AcedInflator inflator)

  ������� ��������� ������ AcedMemoryReader �� ��������� ������� bytes,
  ������� � ������� offset, ������ length ����. ��������������, ��� ������
  � ������� �� �����������. � ��������� inflator ���������� ��������� ������
  AcedInflator, ������� ����� ����������� ��� ���������� ������ ������.

public AcedMemoryReader(byte[] bytes, int offset, int length, Guid keyGuid)

  ������� ��������� ������ AcedMemoryReader �� ��������� ������� bytes,
  ������� � ������� offset ������ length ����. � ��������� keyGuid ����������
  ���� �����, �������������� ��������� ���� System.Guid. ���� �������� �����
  �� ����������, � ��������� keyGuid ����� ���������� �������� Guid.Empty.
  ���� ������ ���������, ��� ���������� ������������ ����� ��������� ������
  AcedInflator, ������� ������������ ��������� AcedInflator.Instance.

public AcedMemoryReader(byte[] bytes, int offset, int length,
  AcedInflator inflator, Guid keyGuid)

  ������� ��������� ������ AcedMemoryReader �� ��������� ������� bytes,
  ������� � ������� offset ������ length ����. � ��������� inflator ����������
  ��������� ������ AcedInflator, ������� ����� ����������� ��� ����������
  ������ ������. � ��������� keyGuid ���������� ���� �����, ��������������
  ��������� ���� System.Guid. ���� �������� ����� �� ����������, � ���������
  keyGuid ����� ���������� �������� Guid.Empty.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public int Position { get; }

  ���������� ������� ������� � �������� ������, �.�. ������ ����������
  ��������� ����� �� ����������� �������, ������ �� ������� ������������
  �������� GetBuffer().

public int Size { get; }

  ���������� ����� ��������� ������ � ������ ���� �������� Offset. ��� ��������
  ������������� ������������� �������� �������� Position. ��� ������� ������
  ������ �� ��������� ���� ����� ��������� ����������.

public int Offset { get; }

  ���������� ��������� ������� � �������� ������, �.�. ������ �������� ��
  ���������� �������, ������������ �������� GetBuffer(), � �������� ����������
  ������ ������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public byte[] GetBuffer()

  ���������� ������ �� ���������� ������, ���������� �������������� �
  ������������� ������ ��������� ������, �� �������� ������������ ������.

public void Reset()

  ������������� ������� ������� �� ������ ��������� ������. ����� ����� �����
  �������� ������� ������ �� ���� �� ���������� ������ AcedReader.

public void Skip(int count)

  ���������� �� ������� �������� ������ count ����. �������� Position
  ������������� ��� ���� �� �������� count.

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

  �������� ��������� count ���� �� ��������� ������ � ��������� �� � �������
  buffer, ������� �� �������� offset. ���� ����� ����, ���������� � ������
  ������ �������� count, ����������� ������� ����, ������� ����. �������
  ���������� ����� ����, ���������� ����������� �� ��������� ������.

public int StreamReadByte()

  ��������� ��������� ���� �� ��������� ������. ���� ��������� ����� ������,
  �.�. ��� ����� ��� ���������, ������� ���������� �������� -1.

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
