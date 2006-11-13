
#region ������ ����������, ������������ � AcedUtils.NET
/*
--------------------------------------------------------------------------------
public class AcedException : Exception

  ������� ����� ����������. �������� ������� ���� ������ ������� ����������,
  ������������ � ������ ����������.

public class AcedDataCorruptedException : AcedException

  ����������, ������� ��������� � ������� AcedMemoryReader, AcedStreamReader,
  ���� ���������� ������������ ����������� ����� ������� ����, ���������������
  ������ ��������� ������, � ����������� � ������ ��������� ����������� �����.

public class AcedWrongDecryptionKeyException : AcedDataCorruptedException

  ����������, ������� ��������� � ������� AcedMemoryReader, AcedStreamReader,
  ���� ��� ������������ ������ ����������� �������� ���� � �������� ��������
  ��������� RipeMD-160, ������������ ��� ��������������� ������� ����,
  �� ��������� �� ���������, ����������� � �������� ������.

public class AcedHashDataFinalizedException : AcedException

  ����������, ����������� ��� ������� �������� ��������� ������ hashData
  � ���� �� ������� ������ AcedRipeMD ����� ����, ��� ���� ��������� ������
  ��� ������ ������� ������ AcedRipeMD.Finalize().

public class AcedNoPlaceToStoreCompressedDataException : AcedException

  ����������, �����������, ����� � ������� ����������� ������, �����������
  �������� AcedDeflator.Compress(), ������������ �����.

public class AcedNoPlaceToStoreDecompressedDataException : AcedException

  ����������, �����������, ����� � ������� ����������� ������, �������������
  �������� AcedInflator.Decompress(), ������������ �����.

public class AcedReadingNotSupportedException : AcedException

  ����������, ����������� ��� ������� ��������� ������ �� ��������� ������,
  ������� ������������ ������ ������.

public class AcedWritingNotSupportedException : AcedException

  ����������, ����������� ��� ������� �������� ������ � �������� �����,
  ������� ������������ ������ ������.

public class AcedSeekingNotSupportedException : AcedException

  ����������, ����������� ��� ������� ���������� ������� ������� �� ������
  ��������� ������, ���� ����� �� ������������ ����������������.

public class AcedReadBeyondTheEndException : AcedException

  ����������, ����������� ��� ������� ������ ������ �� ��������� ���������
  ������ �������� AcedInflator, AcedMemoryReader, AcedStreamReader.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedException exception

    public class AcedException : Exception
    {
        public AcedException(string message)
            : base(message)
        {
        }
    }

    // AcedDataCorruptedException exception

    public class AcedDataCorruptedException : AcedException
    {
        public AcedDataCorruptedException()
            : base(AcedConsts.DataCorruptedMessage)
        {
        }

        public AcedDataCorruptedException(string message)
            : base(message)
        {
        }

        internal static void Throw()
        {
            throw new AcedDataCorruptedException();
        }
    }

    // AcedWrongDecryptionKeyException exception

    public class AcedWrongDecryptionKeyException : AcedDataCorruptedException
    {
        public AcedWrongDecryptionKeyException()
            : base(AcedConsts.WrongDecryptionKeyMessage)
        {
        }

        internal new static void Throw()
        {
            throw new AcedWrongDecryptionKeyException();
        }
    }

    // AcedHashDataFinalizedException exception

    public class AcedHashDataFinalizedException : AcedException
    {
        public AcedHashDataFinalizedException()
            : base(AcedConsts.HashDataFinalizedMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedHashDataFinalizedException();
        }
    }

    // AcedNoPlaceToStoreCompressedDataException exception

    public class AcedNoPlaceToStoreCompressedDataException : AcedException
    {
        public AcedNoPlaceToStoreCompressedDataException()
            : base(AcedConsts.NoPlaceToStoreCompressedDataMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedNoPlaceToStoreCompressedDataException();
        }
    }

    // AcedNoPlaceToStoreDecompressedDataException exception

    public class AcedNoPlaceToStoreDecompressedDataException : AcedException
    {
        public AcedNoPlaceToStoreDecompressedDataException()
            : base(AcedConsts.NoPlaceToStoreDecompressedDataMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedNoPlaceToStoreDecompressedDataException();
        }
    }

    // AcedReadingNotSupportedException exception

    public class AcedReadingNotSupportedException : AcedException
    {
        public AcedReadingNotSupportedException()
            : base(AcedConsts.ReadingNotSupportedMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedReadingNotSupportedException();
        }
    }

    // AcedWritingNotSupportedException exception

    public class AcedWritingNotSupportedException : AcedException
    {
        public AcedWritingNotSupportedException()
            : base(AcedConsts.WritingNotSupportedMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedWritingNotSupportedException();
        }
    }

    // AcedSeekingNotSupportedException exception

    public class AcedSeekingNotSupportedException : AcedException
    {
        public AcedSeekingNotSupportedException()
            : base(AcedConsts.SeekingNotSupportedMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedSeekingNotSupportedException();
        }
    }

    // AcedReadBeyondTheEndException exception

    public class AcedReadBeyondTheEndException : AcedException
    {
        public AcedReadBeyondTheEndException()
            : base(AcedConsts.ReadBeyondTheEndMessage)
        {
        }

        internal static void Throw()
        {
            throw new AcedReadBeyondTheEndException();
        }
    }
}
