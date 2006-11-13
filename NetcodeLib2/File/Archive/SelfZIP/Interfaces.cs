
#region Интерфейс IAcedReader
/*
--------------------------------------------------------------------------------
public interface IAcedReader

  Описывает минимальную функциональность, необходимую для эффективного чтения
  данных из бинарного потока. Интерфейс реализуется классами AcedMemoryReader,
  AcedStreamReader.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

long Length { get; }

  Возвращает длину бинарного потока в байтах.

long Position { get; }

  Возвращает текущую позиция в бинарном потоке относительно его начала.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

void Reset()

  Устанавливает текущую позицию на начало бинарного потока. После этого можно
  повторно считать данные из того же потока.

void Skip(int count)

  Пропускает во входном бинарном потоке count байт. Свойство Position
  увеличивается при этом на величину count.

bool ReadBoolean()

  Считывает значение типа System.Boolean из бинарного потока.

byte ReadByte()

  Считывает значение типа System.Byte из бинарного потока.

byte[] ReadByteArray()

  Считывает значение типа System.Byte[] из бинарного потока.

char ReadChar()

  Считывает значение типа System.Char из бинарного потока.

DateTime ReadDateTime()

  Считывает значение типа System.DateTime из бинарного потока.

decimal ReadDecimal()

  Считывает значение типа System.Decimal из бинарного потока.

float ReadSingle()

  Считывает значение типа System.Single из бинарного потока.

double ReadDouble()

  Считывает значение типа System.Double из бинарного потока.

Guid ReadGuid()

  Считывает значение типа System.Guid из бинарного потока.

short ReadInt16()

  Считывает значение типа System.Int16 из бинарного потока.

int ReadInt32()

  Считывает значение типа System.Int32 из бинарного потока.

long ReadInt64()

  Считывает значение типа System.Int64 из бинарного потока.

string ReadString()

  Считывает значение типа System.String из бинарного потока.

TimeSpan ReadTimeSpan()

  Считывает значение типа System.TimeSpan из бинарного потока.

void Read(bool[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Boolean и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(byte[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Byte и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(char[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Char и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(DateTime[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.DateTime и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(decimal[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Decimal и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(float[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Single и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(double[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Double и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(Guid[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Guid и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(short[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int16 и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(int[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int32 и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(long[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.Int64 и помещает
  их в массив values, начиная с элемента с индексом index.

void Read(TimeSpan[] values, int index, int count)

  Считывает из бинарного потока count значений типа System.TimeSpan и помещает
  их в массив values, начиная с элемента с индексом index.

int StreamRead(byte[] buffer, int offset, int count)

  Считывает count байт из бинарного потока и сохраняет их в массиве buffer,
  начиная со смещения offset. Если число байт, оставшееся в потоке меньше
  значения count, считывается столько байт, сколько есть. Функция возвращает
  число байт, фактически прочитанное из бинарного потока.

int StreamReadByte()

  Считывает следующий байт из бинарного потока. Если достигнут конец потока,
  т.е. все байты уже прочитаны, функция возвращает значение -1.

void Close()

  Закрывает бинарный поток и освобождает связанные с ним unmanaged-ресурсы.

--------------------------------------------------------------------------------
*/
#endregion

#region Интерфейс IAcedWriter
/*
--------------------------------------------------------------------------------
public interface IAcedWriter

  Описывает минимальную функциональность, необходимую для эффективной записи
  данных в бинарный поток. Интерфейс реализуется классами AcedMemoryWriter,
  AcedStreamWriter.

--------------------------------------------------------------------------------
Свойство
--------------------------------------------------------------------------------

long Length { get; }

  Возвращает общее число байт, помещенное в выходной бинарный поток.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

void Flush()

  Сбрасывает несохраненные данные из буфера в выходной бинарный поток.

void Reset()

  Очищает выходной бинарный поток, т.е. сбрасывает в ноль его длину. После
  этого данные можно повторно записать в тот же поток.

void WriteBoolean(bool value)

  Сохраняет в бинарном потоке значение типа System.Boolean.

void WriteByte(byte value)

  Сохраняет в бинарном потоке значение типа System.Byte.

void WriteByteArray(byte[] value)

  Сохраняет в бинарном потоке значение типа System.Byte[].

void WriteChar(char value)

  Сохраняет в бинарном потоке значение типа System.Char.

void WriteDateTime(DateTime value)

  Сохраняет в бинарном потоке значение типа System.DateTime.

void WriteDecimal(decimal value)

  Сохраняет в бинарном потоке значение типа System.Decimal.

void WriteSingle(float value)

  Сохраняет в бинарном потоке значение типа System.Single.

void WriteDouble(double value)

  Сохраняет в бинарном потоке значение типа System.Double.

void WriteGuid(Guid value)

  Сохраняет в бинарном потоке значение типа System.Guid.

void WriteInt16(short value)

  Сохраняет в бинарном потоке значение типа System.Int16.

void WriteInt32(int value)

  Сохраняет в бинарном потоке значение типа System.Int32.

void WriteInt64(long value)

  Сохраняет в бинарном потоке значение типа System.Int64.

void WriteString(string value)

  Сохраняет в бинарном потоке значение типа System.String.

void WriteTimeSpan(TimeSpan value)

  Сохраняет в бинарном потоке значение типа System.TimeSpan.

void Write(bool[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Boolean
  из массива values, начиная с элемента с индексом index.

void Write(byte[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Byte
  из массива values, начиная с элемента с индексом index.

void Write(char[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Char
  из массива values, начиная с элемента с индексом index.

void Write(DateTime[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.DateTime
  из массива values, начиная с элемента с индексом index.

void Write(decimal[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Decimal
  из массива values, начиная с элемента с индексом index.

void Write(float[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Single
  из массива values, начиная с элемента с индексом index.

void Write(double[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Double
  из массива values, начиная с элемента с индексом index.

void Write(Guid[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Guid
  из массива values, начиная с элемента с индексом index.

void Write(short[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int16
  из массива values, начиная с элемента с индексом index.

void Write(int[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int32
  из массива values, начиная с элемента с индексом index.

void Write(long[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.Int64
  из массива values, начиная с элемента с индексом index.

void Write(TimeSpan[] values, int index, int count)

  Помещает в выходной бинарный поток count значений типа System.TimeSpan
  из массива values, начиная с элемента с индексом index.

void Close()

  Сбрасывает несохраненные данные из буфера в выходной поток и закрывает
  поток, освобождая связанные с ним unmanaged-ресурсы.

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
