
#region  лассы исключений, используемые в AcedUtils.NET
/*
--------------------------------------------------------------------------------
public class AcedException : Exception

  Ѕазовый класс исключений. явл€етс€ предком всех других классов исключений,
  используемой в данной библиотеке.

public class AcedDataCorruptedException : AcedException

  »сключение, которое возникает в классах AcedMemoryReader, AcedStreamReader,
  если обнаружено несовпадение контрольной суммы массива байт, представл€ющего
  данные бинарного потока, с сохраненным в потоке значением контрольной суммы.

public class AcedWrongDecryptionKeyException : AcedDataCorruptedException

  »сключение, которое возникает в классах AcedMemoryReader, AcedStreamReader,
  если при дешифровании данных использован неверный ключ и значение цифровой
  сигнатуры RipeMD-160, рассчитанное дл€ расшифрованного массива байт,
  не совпадает со значением, сохраненным в бинарном потоке.

public class AcedHashDataFinalizedException : AcedException

  »сключение, возникающее при попытке передать служебный массив hashData
  в один из методов класса AcedRipeMD после того, как этот служебный массив
  был очищен вызовом метода AcedRipeMD.Finalize().

public class AcedNoPlaceToStoreCompressedDataException : AcedException

  »сключение, возникающее, когда в массиве принимающем данные, упакованные
  функцией AcedDeflator.Compress(), недостаточно места.

public class AcedNoPlaceToStoreDecompressedDataException : AcedException

  »сключение, возникающее, когда в массиве принимающем данные, распакованные
  функцией AcedInflator.Decompress(), недостаточно места.

public class AcedReadingNotSupportedException : AcedException

  »сключение, возникающее при попытке прочитать данные из бинарного потока,
  который поддерживает только запись.

public class AcedWritingNotSupportedException : AcedException

  »сключение, возникающее при попытке записать данные в бинарный поток,
  который поддерживает только чтение.

public class AcedSeekingNotSupportedException : AcedException

  »сключение, возникающее при попытке возвратить текущую позицию на начало
  бинарного потока, если поток не поддерживает позиционирование.

public class AcedReadBeyondTheEndException : AcedException

  »сключение, возникающее при попытке чтени€ данных за пределами бинарного
  потока классами AcedInflator, AcedMemoryReader, AcedStreamReader.

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
