
#region Класс AcedReaderStream
/*
--------------------------------------------------------------------------------
public class AcedReaderStream : Stream

  Инкапсулирует экземпляр класса, такого как AcedMemoryReader или
  AcedStreamReader, реализующего интерфейс IAcedReader, и представляет его
  в виде объекта, производного от класса System.IO.Stream.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedReaderStream(IAcedReader reader)

  Создает экземпляр класса AcedReaderStream на основе объекта, реализующего
  интерфейс IAcedReader, передаваемого параметром reader.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

public IAcedReader Reader { get; }

  Возвращает ссылку на объект, реализующий интерфейс IAcedReader, который
  инкапсулируется данным экземпляром класса AcedReaderStream.

public override bool CanRead { get; }

  Возвращает значение True, т.к. поток поддерживает чтение.

public override bool CanWrite { get; }

  Возвращает значение False, т.к. поток не поддерживает запись.

public override bool CanSeek { get; }

  Возвращает значение False, т.к. поток не поддерживает произвольное
  позиционирование.

public override long Length { get; }

  Возвращает длину входного бинарного потока в байтах.

public override long Position { get; set; }

  Возвращает текущую позицию в бинарном потоке. При попытке присвоить значение
  этому свойству возникает исключение, т.к. поток не поддерживает произвольное
  позиционирование.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public override int Read(byte[] buffer, int offset, int count)

  Считывает count байт из входного бинарного потока и сохраняет прочитанные
  данные в массиве buffer, начиная со смещения offset. Фактическое число
  прочитанных байт может быть меньше значения count, если в бинарном потоке
  отсутствует необходимое количество данных. Функция возвращает число байт,
  фактически прочитанное из бинарного потока.

public override int ReadByte()

  Читает следующий байт из бинарного потока и возвращает его как результат
  функции. Если достигнут конец потока, функция возвращает значение -1.

public override long Seek(long offset, SeekOrigin origin)

  В классе AcedReaderStream вызов данной функции приводит к возникновению
  исключения.

public override void SetLength(long value)

  В классе AcedReaderStream вызов данной функции приводит к возникновению
  исключения.

public override void Write(byte[] buffer, int offset, int count)

  В классе AcedReaderStream вызов данного метода приводит к возникновению
  исключения.

public override void WriteByte(byte value)

  В классе AcedReaderStream вызов данного метода приводит к возникновению
  исключения.

public override void Flush()

  В классе AcedReaderStream данный метод ничего не делает.

public override void Close()

  Закрывает бинарный поток и освобождает связанные с ним unmanaged-ресурсы.

--------------------------------------------------------------------------------
*/
#endregion

#region Класс AcedWriterStream
/*
--------------------------------------------------------------------------------
public class AcedWriterStream : Stream

  Инкапсулирует экземпляр класса, реализующего интерфейс IAcedWriter, такого
  как AcedMemoryWriter или AcedStreamWriter, и представляет его в виде объекта,
  производного от класса System.IO.Stream.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedWriterStream(IAcedWriter writer)

  Создает экземпляр класса AcedWriterStream на основе объекта, реализующего
  интерфейс IAcedWriter, передаваемого параметром writer.

--------------------------------------------------------------------------------
Свойства
--------------------------------------------------------------------------------

public IAcedWriter Writer { get; }

  Возвращает ссылку на объект, инкапсулируемый классом AcedWriterStream,
  реализующий интерфейс IAcedWriter.

public override bool CanRead { get; }

  Возвращает значение False, т.к. поток не поддерживает чтение.

public override bool CanWrite { get; }

  Возвращает значение True, т.к. поток поддерживает запись.

public override bool CanSeek { get; }

  Возвращает значение False, т.к. поток не поддерживает произвольное
  позиционирование.

public override long Length { get; }

  Возвращает количество байт, помещенное в выходной бинарный поток. 

public override long Position { get; set; }

  В классе AcedWriterStream это свойство возвращает значение, совпадающее
  со значением свойства Length. Свойство Position доступно только для чтения,
  т.к. поток не поддерживает произвольное позиционирование. При попытке
  присвоить значение этому свойству возникает исключение.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public override int Read(byte[] buffer, int offset, int count)

  В классе AcedWriterStream вызов данной функции приводит к возникновению
  исключения.

public override int ReadByte()

  В классе AcedWriterStream вызов данной функции приводит к возникновению
  исключения.

public override long Seek(long offset, SeekOrigin origin)

  В классе AcedWriterStream вызов данной функции приводит к возникновению
  исключения.

public override void SetLength(long value)

  В классе AcedWriterStream вызов данной функции приводит к возникновению
  исключения.

public override void Write(byte[] buffer, int offset, int count)

  Сохраняет в бинарном потоке фрагмент массива buffer, начиная со смещения
  offset, длиной count байт.

public override void WriteByte(byte value)

  Сохраняет в бинарном потоке значение типа байт, переданное параметром value.

public override void Flush()

  Сбрасывает несохраненные данные из буфера в выходной бинарный поток.

public override void Close()

  Сбрасывает несохраненные данные из буфера в выходной поток и закрывает
  поток, освобождая связанные с ним unmanaged-ресурсы.

--------------------------------------------------------------------------------
*/
#endregion

using System;
using System.IO;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedReaderStream class

    public class AcedReaderStream : Stream
    {
        private IAcedReader _reader;

        public AcedReaderStream(IAcedReader reader)
        {
            _reader = reader;
        }

        public IAcedReader Reader
        {
            get { return _reader; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Length
        {
            get
            {
                if (_reader == null)
                    AcedConsts.ThrowStreamClosedException();
                return _reader.Length;
            }
        }

        public override long Position
        {
            get
            {
                if (_reader == null)
                    AcedConsts.ThrowStreamClosedException();
                return _reader.Position;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_reader == null)
                AcedConsts.ThrowStreamClosedException();
            return _reader.StreamRead(buffer, offset, count);
        }

        public override int ReadByte()
        {
            if (_reader == null)
                AcedConsts.ThrowStreamClosedException();
            return _reader.StreamReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
        }

        public override void Close()
        {
            if (_reader != null)
            {
                _reader.Close();
                _reader = null;
            }
        }
    }

    // AcedWriterStream class

    public class AcedWriterStream : Stream
    {
        private IAcedWriter _writer;

        public AcedWriterStream(IAcedWriter writer)
        {
            _writer = writer;
        }

        public IAcedWriter Writer
        {
            get { return _writer; }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Length
        {
            get
            {
                if (_writer == null)
                    AcedConsts.ThrowStreamClosedException();
                return _writer.Length;
            }
        }

        public override long Position
        {
            get
            {
                if (_writer == null)
                    AcedConsts.ThrowStreamClosedException();
                return _writer.Length;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override int ReadByte()
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_writer == null)
                AcedConsts.ThrowStreamClosedException();
            _writer.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            if (_writer == null)
                AcedConsts.ThrowStreamClosedException();
            _writer.WriteByte(value);
        }

        public override void Flush()
        {
            if (_writer == null)
                AcedConsts.ThrowStreamClosedException();
            _writer.Flush();
        }

        public override void Close()
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }
        }
    }
}
