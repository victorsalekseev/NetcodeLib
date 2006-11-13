using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    // AcedConsts internal class

    internal sealed class AcedConsts
    {
        internal const string
            NoPlaceToStoreCompressedDataMessage = "Destination byte array is not enough to store compressed data.",
            NoPlaceToStoreDecompressedDataMessage = "Destination byte array is not enough to store decompressed data.",
            ReadingNotSupportedMessage = "The stream does not support reading.",
            WritingNotSupportedMessage = "The stream does not support writing.",
            SeekingNotSupportedMessage = "The stream does not support seeking.",
            ReadBeyondTheEndMessage = "An attempt to read beyond the end of the binary stream.",
            DataCorruptedMessage = "Data corrupted in the binary stream.",
            HashDataFinalizedMessage = "The \"hashData\" parameter references finalized data.",
            WrongDecryptionKeyMessage = "Wrong key used to decrypt the binary stream.",
            StreamClosedMessage = "The stream is closed.";

        internal const int
            NullValue = 0xFFFF,
            LongLength = 0xFFFE,
            BufferSize = 0x100000,
            ChunkShift = 17,
            ChunkCapacity = 32768,
            BlockSize = 8192,
            MaxLength = 17010,
            MaxDistance = 524288,
            InitPosValue = -(MaxDistance + 1),
            CharCount = 320,
            FirstLengthChar = 256,
            FirstCharWithExBit = 272,
            CharTreeSize = CharCount * 2,
            DistCount = 64,
            FirstDistWithExBit = 5,
            DistTreeSize = DistCount * 2,
            MaxBits = 14,
            ChLenCount = 20,
            ChLenTreeSize = ChLenCount * 2,
            MaxChLenBits = 7;

        internal static readonly int[] CharExBitLength = new int[]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 6, 8, 14
		};

        internal static readonly int[] CharExBitBase = new int[]
		{
			19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 43, 45, 47, 49, 51, 55, 59, 63,
			67, 71, 75, 79, 83, 87, 91, 95, 99, 103, 107, 111, 115, 123, 131, 139, 147, 155,
			163, 171, 179, 195, 211, 227, 243, 307, 371, 627
		};

        internal static readonly int[] DistExBitLength = new int[]
		{
			0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5,
			6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 11, 11, 11, 11, 12,
			12, 12, 12, 14, 14, 15, 15, 16, 16, 17, 17
		};

        internal static readonly int[] DistExBitBase = new int[]
		{
			0, 0, 0, 1, 2, 3, 5, 7, 9, 11, 13, 15, 17, 21, 25, 29, 33, 41, 49, 57, 65, 81, 97, 113,
			129, 161, 193, 225, 257, 321, 385, 449, 513, 641, 769, 897, 1025, 1281, 1537, 1793,
			2049, 2561, 3073, 3585, 4097, 5121, 6145, 7169, 8193, 10241, 12289, 14337, 16385, 20481,
			24577, 28673, 32769, 49153, 65537, 98305, 131073, 196609, 262145, 393217
		};

        internal static readonly int[] ChLenExBitLength = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 7
		};

        internal static void ThrowArgumentNullException(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }

        internal static void ThrowStreamClosedException()
        {
            throw new ObjectDisposedException(StreamClosedMessage);
        }

        internal static int EnlargeCapacity(int capacity)
        {
            if (capacity < 1014)
                return capacity < 246 ? (capacity < 54 ? (capacity < 6 ? 16 : 64) : 256) : 1024;
            int k = 10;
            capacity = (capacity + 10) >> 10;
            while (capacity > 1)
            {
                capacity >>= 1;
                k++;
            }
            return 1 << (k + 1);
        }

        private AcedConsts() { }
    }
}
