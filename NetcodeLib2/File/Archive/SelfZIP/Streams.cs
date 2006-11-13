
#region ����� AcedReaderStream
/*
--------------------------------------------------------------------------------
public class AcedReaderStream : Stream

  ������������� ��������� ������, ������ ��� AcedMemoryReader ���
  AcedStreamReader, ������������ ��������� IAcedReader, � ������������ ���
  � ���� �������, ������������ �� ������ System.IO.Stream.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedReaderStream(IAcedReader reader)

  ������� ��������� ������ AcedReaderStream �� ������ �������, ������������
  ��������� IAcedReader, ������������� ���������� reader.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public IAcedReader Reader { get; }

  ���������� ������ �� ������, ����������� ��������� IAcedReader, �������
  ��������������� ������ ����������� ������ AcedReaderStream.

public override bool CanRead { get; }

  ���������� �������� True, �.�. ����� ������������ ������.

public override bool CanWrite { get; }

  ���������� �������� False, �.�. ����� �� ������������ ������.

public override bool CanSeek { get; }

  ���������� �������� False, �.�. ����� �� ������������ ������������
  ����������������.

public override long Length { get; }

  ���������� ����� �������� ��������� ������ � ������.

public override long Position { get; set; }

  ���������� ������� ������� � �������� ������. ��� ������� ��������� ��������
  ����� �������� ��������� ����������, �.�. ����� �� ������������ ������������
  ����������������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public override int Read(byte[] buffer, int offset, int count)

  ��������� count ���� �� �������� ��������� ������ � ��������� �����������
  ������ � ������� buffer, ������� �� �������� offset. ����������� �����
  ����������� ���� ����� ���� ������ �������� count, ���� � �������� ������
  ����������� ����������� ���������� ������. ������� ���������� ����� ����,
  ���������� ����������� �� ��������� ������.

public override int ReadByte()

  ������ ��������� ���� �� ��������� ������ � ���������� ��� ��� ���������
  �������. ���� ��������� ����� ������, ������� ���������� �������� -1.

public override long Seek(long offset, SeekOrigin origin)

  � ������ AcedReaderStream ����� ������ ������� �������� � �������������
  ����������.

public override void SetLength(long value)

  � ������ AcedReaderStream ����� ������ ������� �������� � �������������
  ����������.

public override void Write(byte[] buffer, int offset, int count)

  � ������ AcedReaderStream ����� ������� ������ �������� � �������������
  ����������.

public override void WriteByte(byte value)

  � ������ AcedReaderStream ����� ������� ������ �������� � �������������
  ����������.

public override void Flush()

  � ������ AcedReaderStream ������ ����� ������ �� ������.

public override void Close()

  ��������� �������� ����� � ����������� ��������� � ��� unmanaged-�������.

--------------------------------------------------------------------------------
*/
#endregion

#region ����� AcedWriterStream
/*
--------------------------------------------------------------------------------
public class AcedWriterStream : Stream

  ������������� ��������� ������, ������������ ��������� IAcedWriter, ������
  ��� AcedMemoryWriter ��� AcedStreamWriter, � ������������ ��� � ���� �������,
  ������������ �� ������ System.IO.Stream.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedWriterStream(IAcedWriter writer)

  ������� ��������� ������ AcedWriterStream �� ������ �������, ������������
  ��������� IAcedWriter, ������������� ���������� writer.

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public IAcedWriter Writer { get; }

  ���������� ������ �� ������, ��������������� ������� AcedWriterStream,
  ����������� ��������� IAcedWriter.

public override bool CanRead { get; }

  ���������� �������� False, �.�. ����� �� ������������ ������.

public override bool CanWrite { get; }

  ���������� �������� True, �.�. ����� ������������ ������.

public override bool CanSeek { get; }

  ���������� �������� False, �.�. ����� �� ������������ ������������
  ����������������.

public override long Length { get; }

  ���������� ���������� ����, ���������� � �������� �������� �����. 

public override long Position { get; set; }

  � ������ AcedWriterStream ��� �������� ���������� ��������, �����������
  �� ��������� �������� Length. �������� Position �������� ������ ��� ������,
  �.�. ����� �� ������������ ������������ ����������������. ��� �������
  ��������� �������� ����� �������� ��������� ����������.

--------------------------------------------------------------------------------
������
--------------------------------------------------------------------------------

public override int Read(byte[] buffer, int offset, int count)

  � ������ AcedWriterStream ����� ������ ������� �������� � �������������
  ����������.

public override int ReadByte()

  � ������ AcedWriterStream ����� ������ ������� �������� � �������������
  ����������.

public override long Seek(long offset, SeekOrigin origin)

  � ������ AcedWriterStream ����� ������ ������� �������� � �������������
  ����������.

public override void SetLength(long value)

  � ������ AcedWriterStream ����� ������ ������� �������� � �������������
  ����������.

public override void Write(byte[] buffer, int offset, int count)

  ��������� � �������� ������ �������� ������� buffer, ������� �� ��������
  offset, ������ count ����.

public override void WriteByte(byte value)

  ��������� � �������� ������ �������� ���� ����, ���������� ���������� value.

public override void Flush()

  ���������� ������������� ������ �� ������ � �������� �������� �����.

public override void Close()

  ���������� ������������� ������ �� ������ � �������� ����� � ���������
  �����, ���������� ��������� � ��� unmanaged-�������.

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
