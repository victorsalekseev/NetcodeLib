
#region ��������� IAcedReader
/*
--------------------------------------------------------------------------------
public interface IAcedReader

  ��������� ����������� ����������������, ����������� ��� ������������ ������
  ������ �� ��������� ������. ��������� ����������� �������� AcedMemoryReader,
  AcedStreamReader.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

long Length { get; }

  ���������� ����� ��������� ������ � ������.

long Position { get; }

  ���������� ������� ������� � �������� ������ ������������ ��� ������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

void Reset()

  ������������� ������� ������� �� ������ ��������� ������. ����� ����� �����
  �������� ������� ������ �� ���� �� ������.

void Skip(int count)

  ���������� �� ������� �������� ������ count ����. �������� Position
  ������������� ��� ���� �� �������� count.

bool ReadBoolean()

  ��������� �������� ���� System.Boolean �� ��������� ������.

byte ReadByte()

  ��������� �������� ���� System.Byte �� ��������� ������.

byte[] ReadByteArray()

  ��������� �������� ���� System.Byte[] �� ��������� ������.

char ReadChar()

  ��������� �������� ���� System.Char �� ��������� ������.

DateTime ReadDateTime()

  ��������� �������� ���� System.DateTime �� ��������� ������.

decimal ReadDecimal()

  ��������� �������� ���� System.Decimal �� ��������� ������.

float ReadSingle()

  ��������� �������� ���� System.Single �� ��������� ������.

double ReadDouble()

  ��������� �������� ���� System.Double �� ��������� ������.

Guid ReadGuid()

  ��������� �������� ���� System.Guid �� ��������� ������.

short ReadInt16()

  ��������� �������� ���� System.Int16 �� ��������� ������.

int ReadInt32()

  ��������� �������� ���� System.Int32 �� ��������� ������.

long ReadInt64()

  ��������� �������� ���� System.Int64 �� ��������� ������.

string ReadString()

  ��������� �������� ���� System.String �� ��������� ������.

TimeSpan ReadTimeSpan()

  ��������� �������� ���� System.TimeSpan �� ��������� ������.

void Read(bool[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Boolean � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(byte[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Byte � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(char[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Char � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(DateTime[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.DateTime � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(decimal[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Decimal � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(float[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Single � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(double[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Double � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(Guid[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Guid � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(short[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int16 � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(int[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int32 � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(long[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.Int64 � ��������
  �� � ������ values, ������� � �������� � �������� index.

void Read(TimeSpan[] values, int index, int count)

  ��������� �� ��������� ������ count �������� ���� System.TimeSpan � ��������
  �� � ������ values, ������� � �������� � �������� index.

int StreamRead(byte[] buffer, int offset, int count)

  ��������� count ���� �� ��������� ������ � ��������� �� � ������� buffer,
  ������� �� �������� offset. ���� ����� ����, ���������� � ������ ������
  �������� count, ����������� ������� ����, ������� ����. ������� ����������
  ����� ����, ���������� ����������� �� ��������� ������.

int StreamReadByte()

  ��������� ��������� ���� �� ��������� ������. ���� ��������� ����� ������,
  �.�. ��� ����� ��� ���������, ������� ���������� �������� -1.

void Close()

  ��������� �������� ����� � ����������� ��������� � ��� unmanaged-�������.

--------------------------------------------------------------------------------
*/
#endregion

#region ��������� IAcedWriter
/*
--------------------------------------------------------------------------------
public interface IAcedWriter

  ��������� ����������� ����������������, ����������� ��� ����������� ������
  ������ � �������� �����. ��������� ����������� �������� AcedMemoryWriter,
  AcedStreamWriter.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

long Length { get; }

  ���������� ����� ����� ����, ���������� � �������� �������� �����.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

void Flush()

  ���������� ������������� ������ �� ������ � �������� �������� �����.

void Reset()

  ������� �������� �������� �����, �.�. ���������� � ���� ��� �����. �����
  ����� ������ ����� �������� �������� � ��� �� �����.

void WriteBoolean(bool value)

  ��������� � �������� ������ �������� ���� System.Boolean.

void WriteByte(byte value)

  ��������� � �������� ������ �������� ���� System.Byte.

void WriteByteArray(byte[] value)

  ��������� � �������� ������ �������� ���� System.Byte[].

void WriteChar(char value)

  ��������� � �������� ������ �������� ���� System.Char.

void WriteDateTime(DateTime value)

  ��������� � �������� ������ �������� ���� System.DateTime.

void WriteDecimal(decimal value)

  ��������� � �������� ������ �������� ���� System.Decimal.

void WriteSingle(float value)

  ��������� � �������� ������ �������� ���� System.Single.

void WriteDouble(double value)

  ��������� � �������� ������ �������� ���� System.Double.

void WriteGuid(Guid value)

  ��������� � �������� ������ �������� ���� System.Guid.

void WriteInt16(short value)

  ��������� � �������� ������ �������� ���� System.Int16.

void WriteInt32(int value)

  ��������� � �������� ������ �������� ���� System.Int32.

void WriteInt64(long value)

  ��������� � �������� ������ �������� ���� System.Int64.

void WriteString(string value)

  ��������� � �������� ������ �������� ���� System.String.

void WriteTimeSpan(TimeSpan value)

  ��������� � �������� ������ �������� ���� System.TimeSpan.

void Write(bool[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Boolean
  �� ������� values, ������� � �������� � �������� index.

void Write(byte[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Byte
  �� ������� values, ������� � �������� � �������� index.

void Write(char[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Char
  �� ������� values, ������� � �������� � �������� index.

void Write(DateTime[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.DateTime
  �� ������� values, ������� � �������� � �������� index.

void Write(decimal[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Decimal
  �� ������� values, ������� � �������� � �������� index.

void Write(float[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Single
  �� ������� values, ������� � �������� � �������� index.

void Write(double[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Double
  �� ������� values, ������� � �������� � �������� index.

void Write(Guid[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Guid
  �� ������� values, ������� � �������� � �������� index.

void Write(short[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int16
  �� ������� values, ������� � �������� � �������� index.

void Write(int[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int32
  �� ������� values, ������� � �������� � �������� index.

void Write(long[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.Int64
  �� ������� values, ������� � �������� � �������� index.

void Write(TimeSpan[] values, int index, int count)

  �������� � �������� �������� ����� count �������� ���� System.TimeSpan
  �� ������� values, ������� � �������� � �������� index.

void Close()

  ���������� ������������� ������ �� ������ � �������� ����� � ���������
  �����, ���������� ��������� � ��� unmanaged-�������.

--------------------------------------------------------------------------------
*/
#endregion

using System;
using System.IO;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // IAcedReader interface

    public interface IAcedReader
    {
        long Length { get; }
        long Position { get; }
        void Reset();
        void Skip(int count);
        bool ReadBoolean();
        byte ReadByte();
        byte[] ReadByteArray();
        char ReadChar();
        DateTime ReadDateTime();
        decimal ReadDecimal();
        float ReadSingle();
        double ReadDouble();
        Guid ReadGuid();
        short ReadInt16();
        int ReadInt32();
        long ReadInt64();
        string ReadString();
        TimeSpan ReadTimeSpan();
        void Read(bool[] values, int index, int count);
        void Read(byte[] values, int index, int count);
        void Read(char[] values, int index, int count);
        void Read(DateTime[] values, int index, int count);
        void Read(decimal[] values, int index, int count);
        void Read(float[] values, int index, int count);
        void Read(double[] values, int index, int count);
        void Read(Guid[] values, int index, int count);
        void Read(short[] values, int index, int count);
        void Read(int[] values, int index, int count);
        void Read(long[] values, int index, int count);
        void Read(TimeSpan[] values, int index, int count);
        int StreamRead(byte[] buffer, int offset, int count);
        int StreamReadByte();
        void Close();
    }

    // IAcedWriter class

    public interface IAcedWriter
    {
        long Length { get; }
        void Flush();
        void Reset();
        void WriteBoolean(bool value);
        void WriteByte(byte value);
        void WriteByteArray(byte[] value);
        void WriteChar(char value);
        void WriteDateTime(DateTime value);
        void WriteDecimal(decimal value);
        void WriteSingle(float value);
        void WriteDouble(double value);
        void WriteGuid(Guid value);
        void WriteInt16(short value);
        void WriteInt32(int value);
        void WriteInt64(long value);
        void WriteString(string value);
        void WriteTimeSpan(TimeSpan value);
        void Write(bool[] values, int index, int count);
        void Write(byte[] values, int index, int count);
        void Write(char[] values, int index, int count);
        void Write(DateTime[] values, int index, int count);
        void Write(decimal[] values, int index, int count);
        void Write(float[] values, int index, int count);
        void Write(double[] values, int index, int count);
        void Write(Guid[] values, int index, int count);
        void Write(short[] values, int index, int count);
        void Write(int[] values, int index, int count);
        void Write(long[] values, int index, int count);
        void Write(TimeSpan[] values, int index, int count);
        void Close();
    }
}
