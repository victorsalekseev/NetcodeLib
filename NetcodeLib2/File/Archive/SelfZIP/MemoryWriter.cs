
#region ����� AcedMemoryWriter
/*
--------------------------------------------------------------------------------
public class AcedMemoryWriter : IAcedWriter

  ������������ ��� ��������� ������ � �������� �����, �.�, � ������ ������,
  � ������ ����, ������ �������� ����������� ������������� �� ���� ����������
  ����� ������. ��� ���� ������ ����� ���� ����� ������� LZ+Huffman, ��������
  ������������� � ���������� zlib, ����������� � ����������� ��������� CAST-128
  � ������ CFB (64 ����) � �������� ��������� �������� ��������� RipeMD-160.

--------------------------------------------------------------------------------
ctor's
--------------------------------------------------------------------------------

public AcedMemoryWriter()

  ������� ����� �������� ����� � ��������� �������� 4096 ����.

public AcedMemoryWriter(int capacity)

  ������� �������� ����� � ��������� �������� capacity ����.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public int Length { get; }

  ���������� ����� ����� ����, ���������� � �������� �������� �����. ��������
  ����� �������� ������������� ������� ���������� ������������ ����� � �������,
  ������ �� ������� ������������ �������� GetBuffer(). ������ � ����������
  ������ ����������� � ��������� ������� ����� ����, ��� ��� ������ ��������
  � �������� ����� � ������ ����� ToArray().
  
public int Capacity { get; set; }

  ���������� ��� ������������� ������ ����������� �������, ������ �� �������
  ������������ �������� GetBuffer(), ������������� ��� �������� ������
  ��������� ������. ��� ���������� ����� ������� � �������� ���������� �����
  ������ ���������� ����������������� ������ ��� ���������� ������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public byte[] GetBuffer()

  ���������� ������ �� ���������� ������, � ������� ���������� ������,
  ����������� � �������� ������. �������� ���� ������ ���������� ��� ������
  ����������������� ������.

public byte[] ToArray(AcedCompressionLevel compressionLevel)

  ���������� ������ ����, ���������� ������, ����������� � �������� �����
  � ����������� ����������� ����� ������. �������� compressionLevel ��������
  ����� ������ ��������� ������. ��� ������ ������������ ����� ���������
  ������ AcedDeflator, ������� ������������ ��������� AcedDeflator.Instance.

public byte[] ToArray(AcedCompressionLevel compressionLevel,
  AcedDeflator deflator)

  ���������� ������ ����, ���������� ������, ����������� � �������� �����
  � ����������� ����������� ����� ������. �������� compressionLevel ��������
  ����� ������ ��������� ������. ��� ������ ������������ ��������� ������
  AcedDeflator, ������������ ���������� deflator.

public byte[] ToArray(AcedCompressionLevel compressionLevel, Guid keyGuid)

  ���������� ������ ����, ���������� ������, ����������� � �������� �����
  � ����������� ����������� ����� ������. �������� compressionLevel ��������
  ����� ������ ��������� ������. ��� ������ ������������ ����� ���������
  ������ AcedDeflator, ������� ������������ ��������� AcedDeflator.Instance.
  ���� �������� keyGuid ������� �� Guid.Empty, ����� ������ ��������� �������
  CAST-128 � ������ CFB (64 ����) � ������ keyGuid � ���������� ���������
  ������������� ���-������� RipeMD-160.

public byte[] ToArray(AcedCompressionLevel compressionLevel,
  AcedDeflator deflator, Guid keyGuid)

  ���������� ������ ����, ���������� ������, ����������� � �������� �����
  � ����������� ����������� ����� ������. �������� compressionLevel ��������
  ����� ������ ��������� ������. ��� ������ ������������ ��������� ������
  AcedDeflator, ������������ ���������� deflator. ���� �������� keyGuid
  ������� �� Guid.Empty, ����� ������ ��������� ������� CAST-128 � ������
  CFB (64 ����) � ������ keyGuid � ���������� ��������� �������������
  ���-������� RipeMD-160.

public void Reset()

  ���������� � ���� ����� ��������� ������. �������� Capacity ��� ����
  �� ���������� � ������ ��� ���������� ������ �� ������������������.
  ����� ����� ����� �������� ������������ ��������� ������ AcedWriter.

public void Skip(int count)

  ���������� � �������� ������ count ����. �������� Length �������������
  ��� ���� �� �������� count.

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
