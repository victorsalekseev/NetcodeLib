
#region Перечисление AcedCompressionLevel
/*
--------------------------------------------------------------------------------
public enum AcedCompressionLevel

  Выбирает соотношение между скоростью и качеством сжатия данных.
  Значение каждого из элементов перечисления:

  Store      - простое копирование данных в выходной массив
  Fastest    - самое быстрое, но и самое слабое сжатие данных
  Fast       - нормальная степень сжатия за минимальное время
  Normal     - хорошая степень сжатия за приемлемое время
  Maximum    - максимальная степень сжатия
		
--------------------------------------------------------------------------------
*/
#endregion

#region Класс AcedDeflator
/*
--------------------------------------------------------------------------------
public class AcedDeflator

  Используется для сжатия массива байт методом LZ+Huffman, подобным описанному
  в RFC 1951. В многопоточном приложении необходимо синхронизировать обращения
  к свойству Instance данного класса.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedDeflator()

  Создает новый экземпляр класса AcedDeflator. Вызывать конструктор напрямую
  из прикладной программы не рекомендуется. За исключением особых случаев,
  лучше пользоваться кэшированным экземпляром класса AcedDeflator, возвращаемым
  статическим свойством Instance.

--------------------------------------------------------------------------------
Свойство
--------------------------------------------------------------------------------

public static AcedDeflator Instance { get; }

  Возвращает экземпляр класса AcedDeflator. Так как для создания и хранения
  экземпляра данного класса требуется значительный объем памяти (более 2МБ),
  единственный экземляр кэшируется в статическом поле класса и используется
  всякий раз при необходимости сжать какие-либо данные.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public byte[] Compress(byte[] sourceBytes, int sourceIndex, int sourceLength,
  AcedCompressionLevel compressionLevel, int beforeGap, int afterGap)

  Упаковывает фрагмент массива байт sourceBytes, начиная с элемента
  sourceIndex, длиной sourceLength байт. Степень сжатия задается параметром
  compressionLevel. При этом выделяется память под массив, достаточный для
  хранения сжатых данных. Кроме того, в начале массива резервируется место под
  beforeGap байт, а в конце - под afterGap байт. Ссылка на этот массив,
  содержащий сжатые данные, возвращается как результат функции.

public int Compress(byte[] sourceBytes, int sourceIndex, int sourceLength,
  AcedCompressionLevel compressionLevel, byte[] destinationBytes,
  int destinationIndex)

  Аналогична предыдущей функции, но память под выходной массив должна быть
  распределена пользователем заранее, до вызова функции. Ссылка на массив,
  предназначенный для хранения упакованных данных передается параметром
  destinationBytes, смещение в этом массиве - параметром destinationIndex.
  Максимальная длина выходных данных составляет (SourceLength + 4) байт.
  Функция возвращает число байт, помещенное в массив destinationBytes. Если
  в параметре destinationBytes передать значение nil, функция выполнит
  "фиктивное" сжатие без сохранения результата. Таким образом можно точно
  определить необходимый размер выходного массива. Следует, однако, учитывать,
  что сжатие "вхолостую" выполняется примерно за то же время, что и сжатие
  в обычном режиме.
  
public static void Release()

  Может использоваться для освобождения внутренней ссылки на экземпляр класса
  AcedDeflator. Тогда при вызове GC.Collect() и отсутствии других ссылок на
  данный экземпляр, выделенная под упаковщик память будет возвращена системе.

--------------------------------------------------------------------------------
*/
#endregion

#region Класс AcedInflator
/*
--------------------------------------------------------------------------------
public class AcedInflator

  Предназначен для распаковки данных, упакованных экземпляром класса
  AcedDeflator. В многопоточном приложении необходимо синхронизировать
  обращения к свойству Instance данного класса.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedInflator()

  Создает новый экземпляр класса AcedInflator. Вызывать конструктор напрямую
  из прикладной программы не рекомендуется. За исключением особых случаев,
  лучше пользоваться кэшированным экземпляром класса AcedInflator, возвращаемым
  статическим свойством Instance.

--------------------------------------------------------------------------------
Свойство
--------------------------------------------------------------------------------

public static AcedInflator Instance { get; }

  Возвращает экземпляр класса AcedInflator. Единственный экземпляр данного
  класса кэшируется в статическом поле класса и используется всякий раз при
  необходимости распаковать данные.

--------------------------------------------------------------------------------
Методы
--------------------------------------------------------------------------------

public static int GetDecompressedLength(byte[] sourceBytes, int sourceIndex)

  Возвращает длину, в байтах, массива, необходимого для распаковки данных,
  находящихся в массиве sourceBytes, начиная с индекса sourceIndex.

public byte[] Decompress(byte[] sourceBytes, int sourceIndex,
  int beforeGap, int afterGap)

  Выполняет распаковку данных, находящихся в массиве байт sourceBytes, начиная
  со смещения sourceIndex. При этом создается новый массив байт, достаточный
  для хранения распакованных данных, а также фрагмента размером BeforeGap байт
  в начале этого массива и фрагмента размером AfterGap байт в конце массива.
  Ссылка на этот массив, содержащий распакованные данные, возвращается как
  результат функции.

public int Decompress(byte[] sourceBytes, int sourceIndex,
  byte[] destinationBytes, int destinationIndex)

  Аналогична предыдущей функции, но память под выходной массив должна быть
  распределена пользователем заранее, до вызова функции. Ссылка на массив,
  предназначенный для хранения распакованных данных, передается параметром
  destinationBytes, смещение в этом массиве - параметром destinationIndex.
  Определить размер данных после их распаковки можно вызовом функции
  GetDecompressedLength. Функция Decompress возвращает число байт, помещенное
  в массив destinationBytes. Если в этом параметре передано значение nil,
  функция работает аналогично GetDecompressedLength, т.е. просто возвращает
  размер массива, необходимого для хранения распакованных данных.

public static void Release()

  Удаляет внутреннюю ссылку на экземпляр класса AcedInflator. Тогда, при
  отсутствии других ссылок на этот экземпляр, он может быть уничтожен
  сборщиком мусора при вызове метода GC.Collect().

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
#region Format of the data block
/*
-------------------------------------------------------------------

  Encoded data block consists of sequences of symbols drawn from
  three conceptually distinct alphabets: either literal bytes,
  from the alphabet of byte values (0..255), or <length, backward
  distance> pairs, where the length is drawn from (3..17010) and
  the distance is drawn from (1..524,288). In fact, the literal
  and length alphabets are merged into a single alphabet (0..319),
  where values 0..255 represent literal bytes, and values
  256..319 represent length codes (possibly in conjunction with
  extra bits following the symbol code) as follows:

               Extra                  Extra
          Code  Bits  Length(s)  Code  Bits  Length(s)
          ----  ----  -------    ----  ----  -------
           256    0      3        288    2    51-54
           257    0      4        289    2    55-58
           258    0      5        290    2    59-62
           259    0      6        291    2    63-66
           260    0      7        292    2    67-70
           261    0      8        293    2    71-74
           262    0      9        294    2    75-78
           263    0     10        295    2    79-82
           264    0     11        296    2    83-86
           265    0     12        297    2    87-90
           266    0     13        298    2    91-94
           267    0     14        299    2    95-98
           268    0     15        300    2    99-102
           269    0     16        301    2   103-106
           270    0     17        302    2   107-110
           271    0     18        303    2   111-114
           272    1    19,20      304    3   115-122
           273    1    21,22      305    3   123-130
           274    1    23,24      306    3   131-138
           275    1    25,26      307    3   139-146
           276    1    27,28      308    3   147-154
           277    1    29,30      309    3   155-162
           278    1    31,32      310    3   163-170
           279    1    33,34      311    3   171-178
           280    1    35,36      312    4   179-194
           281    1    37,38      313    4   195-210
           282    1    39,40      314    4   211-226
           283    1    41,42      315    4   227-242
           284    1    43,44      316    6   243-306
           285    1    45,46      317    6   307-370
           286    1    47,48      318    8   371-626
           287    1    49,50      319   14   627-17010
	 
  Distance codes 0-63 are represented by codes of variable length
  with possible additional bits.

              Extra                    Extra
          Code Bits   Distance    Code  Bits    Distance
          ---- ----   --------    ----  ----    ---------
            0    0       R0        32     7      513-640
            1    0       R1        33     7      641-768
            2    0       R2        34     7      769-896
            3    0        1        35     7      897-1024
            4    0        2        36     8     1025-1280
            5    1       3,4       37     8     1281-1536
            6    1       5,6       38     8     1537-1792
            7    1       7,8       39     8     1793-2048
            8    1       9,10      40     9     2049-2560
            9    1      11,12      41     9     2561-3072
           10    1      13,14      42     9     3073-3584
           11    1      15,16      43     9     3585-4096
           12    2      17-20      44    10     4097-5120
           13    2      21-24      45    10     5121-6144
           14    2      25-28      46    10     6145-7168
           15    2      29-32      47    10     7169-8192
           16    3      33-40      48    11     8193-10240
           17    3      41-48      49    11    10241-12288
           18    3      49-56      50    11    12289-14336
           19    3      57-64      51    11    14337-16384
           20    4      65-80      52    12    16385-20480
           21    4      81-96      53    12    20481-24576
           22    4      97-112     54    12    24577-28672
           23    4     113-128     55    12    28673-32768
           24    5     129-160     56    14    32769-49152
           25    5     161-192     57    14    49153-65536
           26    5     193-224     58    15    65537-98304
           27    5     225-256     59    15    98305-131072
           28    6     257-320     60    16   131073-196608
           29    6     321-384     61    16   196609-262144
           30    6     385-448     62    17   262145-393216
           31    6     449-512     63    17   393217-524288

  The alphabet for code lengths is as follows:

        0 - 14: Represent code lengths of 0 - 14 bits.
          15:   Copy previous code length twice.
          16:   Copy previous code length 3 or 4 times
                (1 bits of length: 0 = 3, 1 = 4).
          17:   Copy previous code length 5 - 8 times
                (2 bits of length: 0 = 5, ..., 3 = 8).
          18:   Copy previous code length 9 - 16 times
                (3 bits of length 0 = 9, ..., 7 = 16).
          19:   Copy previous code length 17 - 144 times
                (7 bits of length).

  We can now define the format of the block:

       1 Bit: X, 1 means using of dynamic Huffman codes
                 0 - non-compressed block of data

       8 Bits (if X = 0): # of Bytes in this block:
              # - 8192, 0 if this block is the last

       if X = 1

       20 x 3 bits: code lengths for the code length alphabet

          These code lengths are interpreted as 3-bit integers
          (0-7); as above, a code length of 0 means the
          corresponding symbol (literal/length or distance code
          length) is not used.

       6 Bits: HLIT, # of Literal/Length codes - 257 (257 - 320)

       HLIT + 256 code lengths for the literal/length alphabet,
          encoded using the code length Huffman code

       6 Bits: HDIST, # of Distance codes - 1 (1 - 64)

       HDIST + 1 code lengths for the distance alphabet,
          encoded using the code length Huffman code

       1..8192..8447 literal/length and distance Huffman codes

-------------------------------------------------------------------
*/
#endregion

    // AcedCompressionLevel enumeration

    public enum AcedCompressionLevel
    {
        Store,
        Fastest,
        Fast,
        Normal,
        Maximum
    }

    // AcedDeflator class (not thread-safe)

    public class AcedDeflator
    {
        private static AcedDeflator _instance;

        private const int
            len3MaxDist = 4096,
            len4MaxDist = 32768,
            diff1Min = 1025,
            diff2Min = 32769,
            lazyLimit = 32,

            hashSizeFastest = 0x1000,
            hashMaskFastest = 0x0FFF,
            prevSizeFastest = 0x2000,
            prevMaskFastest = 0x1FFF,
            chainFastest = 2,
            shiftFastest = 4,

            hashSizeFast = 0x8000,
            hashMaskFast = 0x7FFF,
            prevSizeFast = 0x10000,
            prevMaskFast = 0xFFFF,
            chainFast = 8,
            shiftFast = 5,

            hashSizeNormal = 0x40000,
            hashMaskNormal = 0x3FFFF,
            prevSizeNormal = 0x40000,
            prevMaskNormal = 0x3FFFF,
            chainNormal = 32,
            shiftNormal = 6,

            hashSizeMaximum = 0x40000,
            hashMaskMaximum = 0x3FFFF,
            prevSizeMaximum = 0x40000,
            prevMaskMaximum = 0x3FFFF,
            chainMaximum = 96,
            shiftMaximum = 6;

        private int _hashHead;
        private int _length;
        private int _distance;
        private int _prevSize;
        private int _prevMask;
        private int _shift;
        private int _hashSize;
        private int _hashMask;
        private int _maxChain;
        private int _prevLimit;
        private int _limit;
        private int _breakOffset;
        private int _chain;
        private int _maxCode;
        private int _heapLen;
        private int _r0;
        private int _r1;
        private int _r2;
        private int _prevR0;
        private int _prevR1;
        private int _prevR2;

        private unsafe int* _pHash;
        private unsafe int* _pPrev;
        private unsafe int* _pHeap;
        private unsafe int* _pWorkFreq;
        private unsafe int* _pWorkDad;
        private unsafe int* _pDepth;
        private unsafe int* _pTree;
        private unsafe int* _pNextCode;

        private int[] _hash;
        private int[] _prev;
        private int[] _heap;
        private int[] _depth;
        private int[] _tree;
        private int[] _nextCode;

        private int _dstIndex;
        private int _dstBreak;
        private int _chunkLength;
        private uint[] _chunk;
        private object[] _chunkList;
        private int _bits;
        private uint _hold;

        private unsafe byte* _pSrcBytes;
        private unsafe byte* _pDstBytes;

        private int _exBitCount;
        private int _blockStart;
        private int _blockEndIndex;
        private int _bufLength;

        private unsafe int* _pBuffer;
        private unsafe int* _pLen;
        private unsafe int* _pDist;
        private unsafe int* _pDistExBits;

        private int[] _buffer;
        private int[] _len;
        private int[] _dist;
        private int[] _distExBits;

        private int _charBitLenLen;
        private int _distBitLenLen;
        private int _charLenCount;
        private int _distLenCount;

        private unsafe int* _pCharBitCount;
        private unsafe int* _pCharFreqCode;
        private unsafe int* _pCharDadLen;
        private unsafe int* _pCharBitLen;
        private unsafe int* _pCharBitLenEx;
        private unsafe int* _pDistBitCount;
        private unsafe int* _pDistFreqCode;
        private unsafe int* _pDistDadLen;
        private unsafe int* _pDistBitLen;
        private unsafe int* _pDistBitLenEx;
        private unsafe int* _pChLenBitCount;
        private unsafe int* _pChLenFreqCode;
        private unsafe int* _pChLenDadLen;

        private int[] _charBitCount;
        private int[] _charFreqCode;
        private int[] _charDadLen;
        private int[] _charBitLen;
        private int[] _charBitLenEx;
        private int[] _distBitCount;
        private int[] _distFreqCode;
        private int[] _distDadLen;
        private int[] _distBitLen;
        private int[] _distBitLenEx;
        private int[] _chLenBitCount;
        private int[] _chLenFreqCode;
        private int[] _chLenDadLen;
        private int[] _chLenBitLen;

        private unsafe int* _pCharExBitLength;
        private unsafe int* _pCharExBitBase;
        private unsafe int* _pDistExBitLength;
        private unsafe int* _pDistExBitBase;
        private unsafe int* _pChLenExBitLength;

        private bool _growMode;
        private bool _tryMode;

        private static byte[] _fakeDestinationBytes = new byte[1];

        public static AcedDeflator Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new AcedDeflator();
                return _instance;
            }
        }

        public AcedDeflator()
        {
            _heap = new int[AcedConsts.CharCount + 1];
            _depth = new int[AcedConsts.CharTreeSize];
            _tree = new int[AcedConsts.CharTreeSize];
            _nextCode = new int[AcedConsts.MaxBits + 1];
            _buffer = new int[AcedConsts.BlockSize];
            _len = new int[AcedConsts.BlockSize];
            _dist = new int[AcedConsts.BlockSize];
            _distExBits = new int[AcedConsts.BlockSize];
            _charBitCount = new int[AcedConsts.MaxBits + 1];
            _charFreqCode = new int[AcedConsts.CharTreeSize];
            _charDadLen = new int[AcedConsts.CharTreeSize];
            _charBitLen = new int[AcedConsts.CharCount];
            _charBitLenEx = new int[AcedConsts.CharCount];
            _distBitCount = new int[AcedConsts.MaxBits + 1];
            _distFreqCode = new int[AcedConsts.DistTreeSize];
            _distDadLen = new int[AcedConsts.DistTreeSize];
            _distBitLen = new int[AcedConsts.DistCount];
            _distBitLenEx = new int[AcedConsts.DistCount];
            _chLenBitCount = new int[AcedConsts.MaxChLenBits + 1];
            _chLenFreqCode = new int[AcedConsts.ChLenTreeSize];
            _chLenDadLen = new int[AcedConsts.ChLenTreeSize];
            _chLenBitLen = new int[AcedConsts.ChLenCount];
            _hash = new int[hashSizeMaximum];
            _prev = new int[prevSizeMaximum];
        }

        private void NextChunk()
        {
            if (_chunkList == null)
            {
                _chunkList = new object[16];
                _chunkList[0] = _chunk;
            }
            else
            {
                int index = (_dstIndex >> AcedConsts.ChunkShift) - 1;
                int capacity = _chunkList.Length;
                if (index == capacity)
                {
                    object[] newList = new object[capacity * 2];
                    Array.Copy(_chunkList, 0, newList, 0, capacity);
                    _chunkList = newList;
                }
                _chunkList[index] = _chunk;
            }
            _chunk = new uint[AcedConsts.ChunkCapacity];
            _chunkLength = 0;
        }

        private unsafe bool PutBit(int v)
        {
            if (_bits != 0)
            {
                _hold |= (uint)v << (32 - _bits);
                _bits--;
                return true;
            }
            _dstIndex += 4;
            if (_dstIndex <= _dstBreak)
            {
                if (_growMode)
                {
                    if (_chunkLength == AcedConsts.ChunkCapacity)
                        NextChunk();
                    _chunk[_chunkLength] = _hold;
                    _chunkLength++;
                }
                else if (!_tryMode)
                {
                    *((uint*)_pDstBytes) = _hold;
                    _pDstBytes += 4;
                }
                _hold = (uint)v;
                _bits = 31;
                return true;
            }
            return false;
        }

        private unsafe bool PutNBits(int n, uint v)
        {
            int bits = _bits;
            if (bits >= n)
            {
                _hold |= v << (32 - bits);
                _bits = bits - n;
                return true;
            }
            if (bits != 0)
            {
                _hold |= v << (32 - bits);
                v >>= bits;
            }
            _dstIndex += 4;
            if (_dstIndex <= _dstBreak)
            {
                if (_growMode)
                {
                    if (_chunkLength == AcedConsts.ChunkCapacity)
                        NextChunk();
                    _chunk[_chunkLength] = _hold;
                    _chunkLength++;
                }
                else if (!_tryMode)
                {
                    *((uint*)_pDstBytes) = _hold;
                    _pDstBytes += 4;
                }
                _hold = v;
                _bits = 32 + bits - n;
                return true;
            }
            return false;
        }

        private unsafe void InitHeap(int count)
        {
            int* pH = _pHeap + 1;
            int* pW = _pWorkFreq;
            _heapLen = 0;
            _maxCode = -1;
            int i = 0;
            while (i < count)
            {
                if (*pW != 0)
                {
                    *pH = i;
                    _maxCode = i;
                    _pDepth[i] = 0;
                    _heapLen++;
                    pH++;
                }
                else
                    _pWorkDad[i] = 0;
                i++;
                pW++;
            }
            if (_heapLen > 1)
                return;
            if (_heapLen > 0)
            {
                if (_maxCode == 0)
                    _maxCode = i = 1;
                else
                    i = 0;
                *pH = i;
                _pWorkFreq[i] = 1;
                _pDepth[i] = 0;
            }
            else
            {
                _maxCode = 1;
                pH[0] = 0;
                pH[1] = 1;
                _pWorkFreq[0] = 1;
                _pWorkFreq[1] = 1;
                _pDepth[0] = 0;
                _pDepth[1] = 0;
            }
            _heapLen = 2;
        }

        private unsafe void SortHeap(int L, int R)
        {
            int* pH = _pHeap;
            int* pW = _pWorkFreq;
            int I, J;
            do
            {
                I = L;
                J = R;
                int M = pW[pH[(L + R) >> 1]];
                do
                {
                    while (pW[pH[I]] < M)
                        I++;
                    while (M < pW[pH[J]])
                        J--;
                    if (I <= J)
                    {
                        int T = pH[I];
                        pH[I] = pH[J];
                        pH[J] = T;
                        I++;
                        J--;
                    }
                } while (I <= J);
                if (L < J)
                    SortHeap(L, J);
                L = I;
            } while (I < R);
        }

        private unsafe void PQDownHeap()
        {
            int* pH = _pHeap;
            int v = pH[1];
            int nV = _pWorkFreq[v];
            int k = 1;
            int j = 2;
            while (j <= _heapLen)
            {
                int p0 = pH[j];
                int n0 = _pWorkFreq[p0];
                if (j < _heapLen)
                {
                    int p1 = pH[j + 1];
                    int n1 = _pWorkFreq[p1];
                    if (n1 < n0 || (n1 == n0 && _pDepth[p1] <= _pDepth[p0]))
                    {
                        j++;
                        p0 = p1;
                        n0 = n1;
                    }
                }
                if (nV < n0 || (nV == n0 && _pDepth[v] <= _pDepth[p0]))
                    break;
                pH[k] = pH[j];
                k = j;
                j <<= 1;
            }
            pH[k] = v;
        }

        private unsafe int BuildTree(int nextNode)
        {
            int i = 0;
            int* pHeap1 = _pHeap + 1;
            int* pT = _pTree;
            do
            {
                int n = *pHeap1;
                *pHeap1 = _pHeap[_heapLen];
                _heapLen--;
                PQDownHeap();
                int m = *pHeap1;
                pT[0] = n;
                pT[1] = m;
                i += 2;
                pT += 2;
                _pWorkDad[n] = _pWorkDad[m] = nextNode;
                _pWorkFreq[nextNode] = _pWorkFreq[n] + _pWorkFreq[m];
                n = _pDepth[n];
                m = _pDepth[m];
                _pDepth[nextNode] = (n > m ? n : m) + 1;
                *pHeap1 = nextNode;
                nextNode++;
                PQDownHeap();
            } while (_heapLen > 1);
            _pWorkDad[*pHeap1] = 0;
            return i;
        }

        private unsafe int PrepareCharLengths()
        {
            _pWorkFreq = _pCharFreqCode;
            _pWorkDad = _pCharDadLen;
            InitHeap(AcedConsts.CharCount);
            SortHeap(1, _heapLen);
            int i = BuildTree(AcedConsts.CharCount);
            AcedBinary.Fill(0, _pCharBitCount, AcedConsts.MaxBits + 1);
            int overf = 0;
            int bitAmount = 0;
            int n, m;
            int* pT = _pTree + i;
            do
            {
                pT--;
                n = *pT;
                m = _pCharDadLen[_pCharDadLen[n]] + 1;
                if (m > AcedConsts.MaxBits)
                {
                    m = AcedConsts.MaxBits;
                    overf++;
                }
                _pCharDadLen[n] = m;
                if (n < AcedConsts.CharCount)
                {
                    bitAmount += m * _pCharFreqCode[n];
                    _pCharBitCount[m]++;
                }
                i--;
            } while (i > 0);
            if (overf == 0)
                return bitAmount;
            do
            {
                i = AcedConsts.MaxBits - 1;
                while (_pCharBitCount[i] == 0)
                    i--;
                _pCharBitCount[i]--;
                _pCharBitCount[i + 1] += 2;
                _pCharBitCount[AcedConsts.MaxBits]--;
                overf -= 2;
            } while (overf > 0);
            overf = AcedConsts.MaxBits;
            do
            {
                n = _pCharBitCount[overf];
                while (n != 0)
                {
                    m = *pT;
                    pT++;
                    if (m < AcedConsts.CharCount)
                    {
                        bitAmount += (overf - _pCharDadLen[m]) * _pCharFreqCode[m];
                        _pCharDadLen[m] = overf;
                        n--;
                    }
                }
                overf--;
            } while (overf != 0);
            return bitAmount;
        }

        private unsafe int PrepareDistLengths()
        {
            _pWorkFreq = _pDistFreqCode;
            _pWorkDad = _pDistDadLen;
            InitHeap(AcedConsts.DistCount);
            SortHeap(1, _heapLen);
            int i = BuildTree(AcedConsts.DistCount);
            AcedBinary.Fill(0, _pDistBitCount, AcedConsts.MaxBits + 1);
            int overf = 0;
            int bitAmount = 0;
            int n, m;
            int* pT = _pTree + i;
            do
            {
                pT--;
                n = *pT;
                m = _pDistDadLen[_pDistDadLen[n]] + 1;
                if (m > AcedConsts.MaxBits)
                {
                    m = AcedConsts.MaxBits;
                    overf++;
                }
                _pDistDadLen[n] = m;
                if (n < AcedConsts.DistCount)
                {
                    bitAmount += m * _pDistFreqCode[n];
                    _pDistBitCount[m]++;
                }
                i--;
            } while (i > 0);
            if (overf == 0)
                return bitAmount;
            do
            {
                i = AcedConsts.MaxBits - 1;
                while (_pDistBitCount[i] == 0)
                    i--;
                _pDistBitCount[i]--;
                _pDistBitCount[i + 1] += 2;
                _pDistBitCount[AcedConsts.MaxBits]--;
                overf -= 2;
            } while (overf > 0);
            overf = AcedConsts.MaxBits;
            do
            {
                n = _pDistBitCount[overf];
                while (n != 0)
                {
                    m = *pT;
                    pT++;
                    if (m < AcedConsts.DistCount)
                    {
                        bitAmount += (overf - _pDistDadLen[m]) * _pDistFreqCode[m];
                        _pDistDadLen[m] = overf;
                        n--;
                    }
                }
                overf--;
            } while (overf != 0);
            return bitAmount;
        }

        private unsafe int FillCharBitLengths()
        {
            int* pDad = _pCharDadLen;
            int* pLen = _pCharBitLen;
            _charBitLenLen = 0;
            int lastLen = 0;
            int j, i = 0;
            if (_maxCode > 255)
                _charLenCount = _maxCode + 1;
            else
            {
                _charLenCount = 257;
                _maxCode = 256;
            }
            AcedBinary.Fill(0, _pCharBitLenEx, _charLenCount);
            int breakOffset, result = 0;
            do
            {
                int m = *pDad;
                pDad++;
                if (m != lastLen || m != *pDad)
                {
                    *pLen = lastLen = m;
                    i++;
                }
                else
                {
                    if (i + 144 > _maxCode)
                        breakOffset = _charLenCount;
                    else
                        breakOffset = i + 144;
                    j = i;
                    i += 2;
                    pDad++;
                    while (i < breakOffset && m == *pDad)
                    {
                        pDad++;
                        i++;
                    }
                    j = i - j;
                    if (j == 2)
                        *pLen = 15;
                    else if (j < 5)
                    {
                        *pLen = 16;
                        _pCharBitLenEx[_charBitLenLen] = j - 3;
                        result += 1;
                    }
                    else if (j < 9)
                    {
                        *pLen = 17;
                        _pCharBitLenEx[_charBitLenLen] = j - 5;
                        result += 2;
                    }
                    else if (j < 17)
                    {
                        *pLen = 18;
                        _pCharBitLenEx[_charBitLenLen] = j - 9;
                        result += 3;
                    }
                    else
                    {
                        *pLen = 19;
                        _pCharBitLenEx[_charBitLenLen] = j - 17;
                        result += 7;
                    }
                }
                _charBitLenLen++;
                pLen++;
            } while (i < _maxCode);
            if (i == _maxCode)
            {
                *pLen = *pDad;
                _charBitLenLen++;
            }
            return result;
        }

        private unsafe int FillDistBitLengths()
        {
            int* pDad = _pDistDadLen;
            int* pLen = _pDistBitLen;
            _distBitLenLen = 0;
            int lastLen = 0;
            int j, i = 0;
            _distLenCount = _maxCode + 1;
            AcedBinary.Fill(0, _pDistBitLenEx, _distLenCount);
            int result = 0;
            do
            {
                int m = *pDad;
                pDad++;
                if (m != lastLen || m != *pDad)
                {
                    *pLen = lastLen = m;
                    i++;
                }
                else
                {
                    j = i;
                    i += 2;
                    pDad++;
                    while (i < _distLenCount && m == *pDad)
                    {
                        pDad++;
                        i++;
                    }
                    j = i - j;
                    if (j == 2)
                        *pLen = 15;
                    else if (j < 5)
                    {
                        *pLen = 16;
                        _pDistBitLenEx[_distBitLenLen] = j - 3;
                        result += 1;
                    }
                    else if (j < 9)
                    {
                        *pLen = 17;
                        _pDistBitLenEx[_distBitLenLen] = j - 5;
                        result += 2;
                    }
                    else if (j < 17)
                    {
                        *pLen = 18;
                        _pDistBitLenEx[_distBitLenLen] = j - 9;
                        result += 3;
                    }
                    else
                    {
                        *pLen = 19;
                        _pDistBitLenEx[_distBitLenLen] = j - 17;
                        result += 7;
                    }
                }
                _distBitLenLen++;
                pLen++;
            } while (i < _maxCode);
            if (i == _maxCode)
            {
                *pLen = *pDad;
                _distBitLenLen++;
            }
            return result;
        }

        private unsafe void FillChLenFreqCodes()
        {
            int* pFreq = _pChLenFreqCode;
            AcedBinary.Fill(0, pFreq, AcedConsts.ChLenCount);
            int i;
            int* pLen = _pCharBitLen;
            for (i = 7; i < _charBitLenLen; i += 8)
            {
                pFreq[pLen[0]]++;
                pFreq[pLen[1]]++;
                pFreq[pLen[2]]++;
                pFreq[pLen[3]]++;
                pFreq[pLen[4]]++;
                pFreq[pLen[5]]++;
                pFreq[pLen[6]]++;
                pFreq[pLen[7]]++;
                pLen += 8;
            }
            for (i -= 7; i < _charBitLenLen; i++)
            {
                pFreq[*pLen]++;
                pLen++;
            }
            pLen = _pDistBitLen;
            for (i = 7; i < _distBitLenLen; i += 8)
            {
                pFreq[pLen[0]]++;
                pFreq[pLen[1]]++;
                pFreq[pLen[2]]++;
                pFreq[pLen[3]]++;
                pFreq[pLen[4]]++;
                pFreq[pLen[5]]++;
                pFreq[pLen[6]]++;
                pFreq[pLen[7]]++;
                pLen += 8;
            }
            for (i -= 7; i < _distBitLenLen; i++)
            {
                pFreq[*pLen]++;
                pLen++;
            }
        }

        private unsafe int PrepareChLenLengths()
        {
            _pWorkFreq = _pChLenFreqCode;
            _pWorkDad = _pChLenDadLen;
            InitHeap(AcedConsts.ChLenCount);
            SortHeap(1, _heapLen);
            int i = BuildTree(AcedConsts.ChLenCount);
            AcedBinary.Fill(0, _pChLenBitCount, AcedConsts.MaxChLenBits + 1);
            int overf = 0;
            int bitAmount = 0;
            int n, m;
            int* pT = _pTree + i;
            do
            {
                pT--;
                n = *pT;
                m = _pChLenDadLen[_pChLenDadLen[n]] + 1;
                if (m > AcedConsts.MaxChLenBits)
                {
                    m = AcedConsts.MaxChLenBits;
                    overf++;
                }
                _pChLenDadLen[n] = m;
                if (n < AcedConsts.ChLenCount)
                {
                    bitAmount += m * _pChLenFreqCode[n];
                    _pChLenBitCount[m]++;
                }
                i--;
            } while (i > 0);
            if (overf == 0)
                return bitAmount;
            do
            {
                i = AcedConsts.MaxChLenBits - 1;
                while (_pChLenBitCount[i] == 0)
                    i--;
                _pChLenBitCount[i]--;
                _pChLenBitCount[i + 1] += 2;
                _pChLenBitCount[AcedConsts.MaxChLenBits]--;
                overf -= 2;
            } while (overf > 0);
            overf = AcedConsts.MaxChLenBits;
            do
            {
                n = _pChLenBitCount[overf];
                while (n != 0)
                {
                    m = *pT;
                    pT++;
                    if (m < AcedConsts.ChLenCount)
                    {
                        bitAmount += (overf - _pChLenDadLen[m]) * _pChLenFreqCode[m];
                        _pChLenDadLen[m] = overf;
                        n--;
                    }
                }
                overf--;
            } while (overf != 0);
            return bitAmount;
        }

        private unsafe bool WriteNonCompressedBlock(uint v, int blockStart, int blockEnd)
        {
            if (!PutBit(0))
                return false;
            int bits = _bits;
            while (true)
            {
                if (bits >= 8)
                {
                    _hold |= v << (32 - bits);
                    bits -= 8;
                }
                else
                {
                    if (bits != 0)
                    {
                        _hold |= v << (32 - bits);
                        v >>= bits;
                    }
                    _dstIndex += 4;
                    if (_dstIndex > _dstBreak)
                        return false;
                    if (_growMode)
                    {
                        if (_chunkLength == AcedConsts.ChunkCapacity)
                            NextChunk();
                        _chunk[_chunkLength] = _hold;
                        _chunkLength++;
                    }
                    else if (!_tryMode)
                    {
                        *((uint*)_pDstBytes) = _hold;
                        _pDstBytes += 4;
                    }
                    _hold = v;
                    bits += 24;
                }
                if (blockStart < blockEnd)
                    v = (uint)_pSrcBytes[blockStart];
                else
                    break;
                blockStart++;
            }
            _bits = bits;
            return true;
        }

        private unsafe bool WriteBlockData()
        {
            for (int i = 0; i < _bufLength; i++)
            {
                int c = _pBuffer[i];
                if (!PutNBits(_pCharDadLen[c], (uint)_pCharFreqCode[c]))
                    return false;
                if (c < AcedConsts.FirstLengthChar)
                    continue;
                c -= AcedConsts.FirstCharWithExBit;
                if (c >= 0)
                {
                    if (!PutNBits(_pCharExBitLength[c], (uint)(_pLen[i] - _pCharExBitBase[c])))
                        return false;
                }
                c = _pDist[i];
                int n = _pDistDadLen[c];
                if (c < AcedConsts.FirstDistWithExBit)
                {
                    if (!PutNBits(n, (uint)_pDistFreqCode[c]))
                        return false;
                }
                else if (!PutNBits(n + _pDistExBitLength[c], (uint)(_pDistFreqCode[c] | (_pDistExBits[i] << n))))
                    return false;
            }
            return true;
        }

        private unsafe void GenerateCodes()
        {
            int* p = _pNextCode;
            p[1] = 0;
            int n = _pChLenBitCount[1] << 1;
            p[2] = n;
            p += 3;
            int* pBD = _pChLenBitCount + 2;
            int i, m;
            for (i = AcedConsts.MaxChLenBits - 2; i > 0; i--)
            {
                *p = n = (n + *pBD) << 1;
                pBD++;
                p++;
            }
            p = _pNextCode;
            int* pFC = _pChLenFreqCode;
            pBD = _pChLenDadLen;
            for (i = AcedConsts.ChLenCount; i > 0; i--)
            {
                n = *pBD;
                if (n != 0)
                {
                    m = p[n];
                    *pFC = (int)AcedBinary.ReverseBits((uint)m, n);
                    p[n] = m + 1;
                }
                pBD++;
                pFC++;
            }
            p = _pNextCode;
            p[1] = 0;
            n = _pCharBitCount[1] << 1;
            p[2] = n;
            p += 3;
            pBD = _pCharBitCount + 2;
            for (i = AcedConsts.MaxBits - 2; i > 0; i--)
            {
                *p = n = (n + *pBD) << 1;
                pBD++;
                p++;
            }
            p = _pNextCode;
            pFC = _pCharFreqCode;
            pBD = _pCharDadLen;
            for (i = AcedConsts.CharCount; i > 0; i--)
            {
                n = *pBD;
                if (n != 0)
                {
                    m = p[n];
                    *pFC = (int)AcedBinary.ReverseBits((uint)m, n);
                    p[n] = m + 1;
                }
                pBD++;
                pFC++;
            }
            p = _pNextCode;
            p[1] = 0;
            n = _pDistBitCount[1] << 1;
            p[2] = n;
            p += 3;
            pBD = _pDistBitCount + 2;
            for (i = AcedConsts.MaxBits - 2; i > 0; i--)
            {
                *p = n = (n + *pBD) << 1;
                pBD++;
                p++;
            }
            p = _pNextCode;
            pFC = _pDistFreqCode;
            pBD = _pDistDadLen;
            for (i = AcedConsts.DistCount; i > 0; i--)
            {
                n = *pBD;
                if (n != 0)
                {
                    m = p[n];
                    *pFC = (int)AcedBinary.ReverseBits((uint)m, n);
                    p[n] = m + 1;
                }
                pBD++;
                pFC++;
            }
        }

        private unsafe bool WriteDynamicBlock()
        {
            GenerateCodes();
            if (!PutBit(1))
                return false;
            int i, m, n;
            for (i = 0; i < AcedConsts.ChLenCount; i++)
                if (!PutNBits(3, (uint)_pChLenDadLen[i]))
                    return false;
            if (!PutNBits(6, (uint)(_charLenCount - 257)))
                return false;
            for (i = 0; i < _charBitLenLen; i++)
            {
                m = _pCharBitLen[i];
                n = _pChLenDadLen[m];
                if (!PutNBits(n + _pChLenExBitLength[m], (uint)(_pChLenFreqCode[m] | (_pCharBitLenEx[i] << n))))
                    return false;
            }
            if (!PutNBits(6, (uint)(_distLenCount - 1)))
                return false;
            for (i = 0; i < _distBitLenLen; i++)
            {
                m = _pDistBitLen[i];
                n = _pChLenDadLen[m];
                if (!PutNBits(n + _pChLenExBitLength[m], (uint)(_pChLenFreqCode[m] | (_pDistBitLenEx[i] << n))))
                    return false;
            }
            return WriteBlockData();
        }

        private unsafe bool FlushBuffer()
        {
            int bitAmount = 16;
            bitAmount += PrepareCharLengths();
            bitAmount += FillCharBitLengths();
            bitAmount += PrepareDistLengths();
            bitAmount += FillDistBitLengths();
            FillChLenFreqCodes();
            bitAmount += PrepareChLenLengths() + AcedConsts.ChLenCount * 3;
            bitAmount += _exBitCount;
            int n = _blockEndIndex - _blockStart;
            if (n <= AcedConsts.BlockSize + 255 && (n << 3) + 10 <= bitAmount)
            {
                if (!WriteNonCompressedBlock(n < AcedConsts.BlockSize ? 0u :
                    (uint)(n - AcedConsts.BlockSize), _blockStart, _blockEndIndex))
                    return false;
                _r0 = _prevR0;
                _r1 = _prevR1;
                _r2 = _prevR2;
            }
            else
            {
                if (!WriteDynamicBlock())
                    return false;
                _prevR0 = _r0;
                _prevR1 = _r1;
                _prevR2 = _r2;
            }
            AcedBinary.Fill(0, _pCharFreqCode, AcedConsts.CharCount);
            AcedBinary.Fill(0, _pDistFreqCode, AcedConsts.DistCount);
            _exBitCount = 0;
            _blockStart = _blockEndIndex;
            _bufLength = 0;
            return true;
        }

        private int GetLenChar(int length)
        {
            if (length < 19)
                return length + 253;
            if (length < 115)
            {
                if (length < 51)
                {
                    _exBitCount += 1;
                    return ((length - 19) >> 1) + 272;
                }
                _exBitCount += 2;
                return ((length - 51) >> 2) + 288;
            }
            if (length < 243)
            {
                if (length < 179)
                {
                    _exBitCount += 3;
                    return ((length - 115) >> 3) + 304;
                }
                _exBitCount += 4;
                return ((length - 179) >> 4) + 312;
            }
            if (length < 371)
            {
                _exBitCount += 6;
                return ((length - 243) >> 6) + 316;
            }
            if (length < 627)
            {
                _exBitCount += 8;
                return 318;
            }
            _exBitCount += 14;
            return 319;
        }

        private int SplitDistance(int distance)
        {
            if (distance < 3)
                return distance + 2;
            if (distance == _r0)
                return 0;
            if (distance == _r1)
            {
                _r1 = _r0;
                _r0 = distance;
                return 1;
            }
            if (distance == _r2)
            {
                _r2 = _r0;
                _r0 = distance;
                return 2;
            }
            _r2 = _r1;
            _r1 = _r0;
            _r0 = distance;
            if (distance < 1025)
            {
                if (distance < 65)
                {
                    if (distance < 17)
                        return ((distance - 3) >> 1) + 5;
                    if (distance < 33)
                        return ((distance - 17) >> 2) + 12;
                    return ((distance - 33) >> 3) + 16;
                }
                if (distance < 257)
                {
                    if (distance < 129)
                        return ((distance - 65) >> 4) + 20;
                    return ((distance - 129) >> 5) + 24;
                }
                if (distance < 513)
                    return ((distance - 257) >> 6) + 28;
                return ((distance - 513) >> 7) + 32;
            }
            if (distance < 16385)
            {
                if (distance < 4097)
                {
                    if (distance < 2049)
                        return ((distance - 1025) >> 8) + 36;
                    return ((distance - 2049) >> 9) + 40;
                }
                if (distance < 8193)
                    return ((distance - 4097) >> 10) + 44;
                return ((distance - 8193) >> 11) + 48;
            }
            if (distance < 65537)
            {
                if (distance < 32769)
                    return ((distance - 16385) >> 12) + 52;
                return ((distance - 32769) >> 14) + 56;
            }
            if (distance < 131073)
                return ((distance - 65537) >> 15) + 58;
            if (distance < 262145)
                return ((distance - 131073) >> 16) + 60;
            return ((distance - 262145) >> 17) + 62;
        }

        private unsafe bool PutChar(int c)
        {
            if (_bufLength == AcedConsts.BlockSize)
                if (!FlushBuffer())
                    return false;
            _pCharFreqCode[c]++;
            _pBuffer[_bufLength] = c;
            _bufLength++;
            return true;
        }

        private unsafe bool PutLenDist()
        {
            if (_bufLength == AcedConsts.BlockSize)
                if (!FlushBuffer())
                    return false;
            int bufPos = _bufLength;
            int c = GetLenChar(_length);
            _pCharFreqCode[c]++;
            _pBuffer[bufPos] = c;
            _pLen[bufPos] = _length;
            c = SplitDistance(_distance);
            if (c >= AcedConsts.FirstDistWithExBit)
            {
                _pDistExBits[bufPos] = _distance - _pDistExBitBase[c];
                _exBitCount += _pDistExBitLength[c];
            }
            _pDistFreqCode[c]++;
            _pDist[bufPos] = c;
            _bufLength = bufPos + 1;
            return true;
        }

        private unsafe int CompressCore()
        {
            fixed (int* pPrev = &_prev[0], pHash = &_hash[0], pNextCode = &_nextCode[0],
                       pHeap = &_heap[0], pDepth = &_depth[0], pTree = &_tree[0],
                       pBuffer = &_buffer[0], pLen = &_len[0], pDist = &_dist[0],
                       pDistExBits = &_distExBits[0], pCharBitCount = &_charBitCount[0],
                       pCharFreqCode = &_charFreqCode[0], pCharDadLen = &_charDadLen[0],
                       pCharBitLen = &_charBitLen[0], pCharBitLenEx = &_charBitLenEx[0],
                       pDistBitCount = &_distBitCount[0], pDistFreqCode = &_distFreqCode[0],
                       pDistDadLen = &_distDadLen[0], pDistBitLen = &_distBitLen[0],
                       pDistBitLenEx = &_distBitLenEx[0], pChLenBitCount = &_chLenBitCount[0],
                       pChLenFreqCode = &_chLenFreqCode[0], pChLenDadLen = &_chLenDadLen[0],
                       pCharExBitLength = &AcedConsts.CharExBitLength[0],
                       pCharExBitBase = &AcedConsts.CharExBitBase[0],
                       pDistExBitLength = &AcedConsts.DistExBitLength[0],
                       pDistExBitBase = &AcedConsts.DistExBitBase[0],
                       pChLenExBitLength = &AcedConsts.ChLenExBitLength[0])
            {
                _pPrev = pPrev;
                _pHash = pHash;
                _pNextCode = pNextCode;
                _pHeap = pHeap;
                _pDepth = pDepth;
                _pTree = pTree;
                _pBuffer = pBuffer;
                _pLen = pLen;
                _pDist = pDist;
                _pDistExBits = pDistExBits;
                _pCharBitCount = pCharBitCount;
                _pCharFreqCode = pCharFreqCode;
                _pCharDadLen = pCharDadLen;
                _pCharBitLen = pCharBitLen;
                _pCharBitLenEx = pCharBitLenEx;
                _pDistBitCount = pDistBitCount;
                _pDistFreqCode = pDistFreqCode;
                _pDistDadLen = pDistDadLen;
                _pDistBitLen = pDistBitLen;
                _pDistBitLenEx = pDistBitLenEx;
                _pChLenBitCount = pChLenBitCount;
                _pChLenFreqCode = pChLenFreqCode;
                _pChLenDadLen = pChLenDadLen;
                _pCharExBitLength = pCharExBitLength;
                _pCharExBitBase = pCharExBitBase;
                _pDistExBitLength = pDistExBitLength;
                _pDistExBitBase = pDistExBitBase;
                _pChLenExBitLength = pChLenExBitLength;
                AcedBinary.Fill(AcedConsts.InitPosValue, _pHash, _hashSize);
                AcedBinary.Fill(0, _pCharFreqCode, AcedConsts.CharCount);
                AcedBinary.Fill(0, _pDistFreqCode, AcedConsts.DistCount);
                _blockStart = 0;
                _bufLength = 0;
                _exBitCount = 0;
                _r0 = 0;
                _r1 = 0;
                _r2 = 0;
                _prevR0 = 0;
                _prevR1 = 0;
                _prevR2 = 0;
                _pPrev[0] = AcedConsts.InitPosValue;
                int hv = ((_pSrcBytes[0] << (_shift + _shift)) ^ (_pSrcBytes[1] << _shift) ^ _pSrcBytes[2]) & _hashMask;
                _pHash[hv] = 0;
                _dstIndex = 0;
                PutChar(_pSrcBytes[0]);
                _bits = 32;
                _hold = 0;
                int srcIndex = 1;
                while (srcIndex < _breakOffset - 2)
                {
                    hv = ((hv << _shift) ^ _pSrcBytes[srcIndex + 2]) & _hashMask;
                    _pPrev[srcIndex & _prevMask] = _hashHead = _pHash[hv];
                    _pHash[hv] = srcIndex;
                    _blockEndIndex = srcIndex;
                    _chain = _maxChain;
                    _prevLimit = srcIndex - _prevSize;
                    if (_hashHead < (_limit = srcIndex - AcedConsts.MaxDistance) || !FindFirstFragment(srcIndex))
                    {
                        if (!PutChar(_pSrcBytes[srcIndex]))
                            return -1;
                        srcIndex++;
                    }
                    else
                    {
                        int nextOffset = srcIndex + _length;
                        if (_hashHead > _prevLimit && (_hashHead = _pPrev[_hashHead & _prevMask]) >= _limit &&
                            nextOffset < _breakOffset && _length < AcedConsts.MaxLength)
                        {
                            FindNextFragment(srcIndex);
                            nextOffset = srcIndex + _length;
                        }
                        while (_length < lazyLimit && nextOffset < _breakOffset)
                        {
                            srcIndex++;
                            hv = ((hv << _shift) ^ _pSrcBytes[srcIndex + 2]) & _hashMask;
                            _pPrev[srcIndex & _prevMask] = _hashHead = _pHash[hv];
                            _pHash[hv] = srcIndex;
                            _chain = _maxChain;
                            _prevLimit = srcIndex - _prevSize;
                            if (_hashHead < (_limit = srcIndex - AcedConsts.MaxDistance) || !FindNextFragment(srcIndex))
                                break;
                            if (!PutChar(_pSrcBytes[srcIndex - 1]))
                                return -1;
                            nextOffset = srcIndex + _length;
                            _blockEndIndex = srcIndex;
                        }
                        if (!PutLenDist())
                            return -1;
                        if (nextOffset >= _breakOffset - 2)
                            srcIndex = nextOffset;
                        else
                        {
                            srcIndex++;
                            do
                            {
                                hv = ((hv << _shift) ^ _pSrcBytes[srcIndex + 2]) & _hashMask;
                                _pPrev[srcIndex & _prevMask] = _hashHead = _pHash[hv];
                                _pHash[hv] = srcIndex;
                                srcIndex++;
                            } while (srcIndex < nextOffset);
                        }
                    }
                }
                while (srcIndex < _breakOffset)
                {
                    _blockEndIndex = srcIndex;
                    if (!PutChar(_pSrcBytes[srcIndex]))
                        return -1;
                    srcIndex++;
                }
                _blockEndIndex = srcIndex;
                if (!FlushBitTrail())
                    return -1;
            }
            return _dstIndex;
        }

        private unsafe bool FindFirstFragment(int srcIndex)
        {
            byte* p = _pSrcBytes;
            uint m = (uint)p[srcIndex] | ((uint)p[srcIndex + 1] << 8) | ((uint)p[srcIndex + 2] << 16);
            int head = _hashHead;
            int boff = _breakOffset;
            int xoff = boff - 8;
            int chain = _chain;
            do
            {
                if ((*((uint*)(p + head)) & 0xFFFFFF) == m)
                {
                    int m1 = head + 3;
                    int m2 = srcIndex + 3;
                    if (m2 < boff && p[m1] == p[m2])
                    {
                        m1++; m2++;
                        if (m2 < boff && p[m1] == p[m2])
                        {
                            do
                            { } while (m2 < xoff && p[++m1] == p[++m2] &&
                                p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                                p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                                p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                                p[++m1] == p[++m2]);
                            if (p[m1] == p[m2])
                            {
                                m1++; m2++;
                                while (m2 < boff && p[m1] == p[m2])
                                {
                                    m1++; m2++;
                                }
                            }
                            m2 -= srcIndex;
                            if (m2 < AcedConsts.MaxLength)
                                _length = m2;
                            else
                                _length = AcedConsts.MaxLength;
                            _distance = srcIndex - head;
                            return true;
                        }
                        else if (srcIndex - head <= len4MaxDist)
                        {
                            _length = 4;
                            _distance = srcIndex - head;
                            return true;
                        }
                    }
                    else if (srcIndex - head <= len3MaxDist)
                    {
                        _length = 3;
                        _distance = srcIndex - head;
                        return true;
                    }
                }
                if (head <= _prevLimit)
                    return false;
                _hashHead = head = _pPrev[head & _prevMask];
                if (head < _limit)
                    return false;
                _chain = --chain;
            } while (chain != 0);
            return false;
        }

        private unsafe bool FindNextFragment(int srcIndex)
        {
            byte* p = _pSrcBytes;
            int head = _hashHead;
            int len = _length;
            int boff = _breakOffset;
            int xoff = boff - 8;
            int chain = _chain;
            uint bf = *((uint*)(p + srcIndex + len - 3));
            bool result = false;
            do
            {
                if (*((uint*)(p + head + len - 3)) == bf && p[srcIndex] == p[head] && p[srcIndex + 1] == p[head + 1])
                {
                    int m1 = head + 1;
                    int m2 = srcIndex + 1;
                    if (len > 4)
                    {
                        int n = (len - 1) >> 2;
                        do
                        {
                            if (*((uint*)(p + m1 + 1)) != *((uint*)(p + m2 + 1)))
                                goto SkipThisSequence;
                            m1 += 4;
                            m2 += 4;
                            n--;
                        } while (n > 0);
                    }
                    while (m2 < xoff && p[++m1] == p[++m2] &&
                        p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                        p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                        p[++m1] == p[++m2] && p[++m1] == p[++m2] &&
                        p[++m1] == p[++m2]) ;
                    if (p[m1] == p[m2])
                    {
                        m1++; m2++;
                        while (m2 < boff && p[m1] == p[m2])
                        {
                            m1++; m2++;
                        }
                    }
                    m1 = m2 - srcIndex;
                    srcIndex -= head;
                    if (m1 - len > 2 || (m1 - len == 1 && (srcIndex < diff1Min || Test1Difference(srcIndex, _distance)))
                        || (m1 - len == 2 && (srcIndex < diff2Min || Test2Difference(srcIndex, _distance))))
                    {
                        _length = len = m1;
                        _distance = srcIndex;
                        result = true;
                        if (len >= AcedConsts.MaxLength)
                        {
                            _length = AcedConsts.MaxLength;
                            break;
                        }
                        if (m2 == boff)
                            break;
                        bf = *((uint*)(p + m2 - 3));
                    }
                    srcIndex += head;
                }
            SkipThisSequence:
                if (head <= _prevLimit)
                    break;
                head = _pPrev[head & _prevMask];
                if (head < _limit)
                    break;
            } while (--chain != 0);
            return result;
        }

        private static bool Test1Difference(int newDistance, int oldDistance)
        {
            if (newDistance < 16385)
            {
                if (newDistance < 4097)
                {
                    if (newDistance < 2049)
                    {
                        if (oldDistance < 3)
                            return false;
                    }
                    else if (oldDistance < 17)
                        return false;
                }
                else if (newDistance < 8193)
                {
                    if (oldDistance < 33)
                        return false;
                }
                else if (oldDistance < 65)
                    return false;
            }
            else if (newDistance < 65537)
            {
                if (newDistance < 32769)
                {
                    if (oldDistance < 129)
                        return false;
                }
                else if (oldDistance < 513)
                    return false;
            }
            else if (newDistance < 131073)
            {
                if (oldDistance < 2049)
                    return false;
            }
            else if (newDistance < 262145)
            {
                if (oldDistance < 4097)
                    return false;
            }
            else if (oldDistance < 8193)
                return false;
            return true;
        }

        private static bool Test2Difference(int newDistance, int oldDistance)
        {
            if (newDistance < 131073)
            {
                if (newDistance < 65537)
                {
                    if (oldDistance < 3)
                        return false;
                }
                if (oldDistance < 17)
                    return false;
            }
            if (newDistance < 262145)
            {
                if (oldDistance < 33)
                    return false;
            }
            else if (oldDistance < 65)
                return false;
            return true;
        }

        private unsafe bool FlushBitTrail()
        {
            if (FlushBuffer())
            {
                _dstIndex += 4 - (_bits >> 3);
                if (_dstIndex <= _dstBreak)
                {
                    if (_growMode)
                    {
                        if (_chunkLength == AcedConsts.ChunkCapacity)
                            NextChunk();
                        _chunk[_chunkLength] = _hold;
                        _chunkLength++;
                    }
                    else if (!_tryMode)
                        switch (_bits >> 3)
                        {
                            case 0:
                                *((uint*)_pDstBytes) = _hold;
                                break;
                            case 1:
                                _pDstBytes[0] = (byte)_hold;
                                _pDstBytes[1] = (byte)(_hold >> 8);
                                _pDstBytes[2] = (byte)(_hold >> 16);
                                break;
                            case 2:
                                _pDstBytes[0] = (byte)_hold;
                                _pDstBytes[1] = (byte)(_hold >> 8);
                                break;
                            case 3:
                                *_pDstBytes = (byte)_hold;
                                break;
                        }
                    return true;
                }
            }
            return false;
        }

        public unsafe byte[] Compress(byte[] sourceBytes, int sourceIndex, int sourceLength,
            AcedCompressionLevel compressionLevel, int beforeGap, int afterGap)
        {
            if (sourceLength == 0)
                return new byte[beforeGap + afterGap + 4];
            if (sourceBytes == null)
                AcedConsts.ThrowArgumentNullException("sourceBytes");
            _growMode = true;
            _tryMode = false;
            _chunkList = null;
            _chunk = new uint[AcedConsts.ChunkCapacity];
            _chunkLength = 0;
            _dstBreak = sourceLength;
            _breakOffset = sourceLength;
            _pDstBytes = null;
            byte[] result;
            fixed (byte* pSrcBytes = &sourceBytes[sourceIndex])
            {
                _pSrcBytes = pSrcBytes;
                int outSize = -1;
                if (sourceLength > 3 && compressionLevel != AcedCompressionLevel.Store)
                {
                    SetupCompressionLevel(compressionLevel);
                    outSize = CompressCore();
                }
                if (outSize > 0)
                {
                    result = new byte[beforeGap + 4 + outSize + afterGap];
                    fixed (byte* pResult = &result[beforeGap])
                        *((int*)pResult) = sourceLength;
                    beforeGap += 4;
                    outSize--;
                    int n = outSize >> AcedConsts.ChunkShift;
                    for (int i = 0; i < n; i++)
                    {
                        Buffer.BlockCopy((uint[])_chunkList[i], 0, result, beforeGap, AcedConsts.ChunkCapacity * 4);
                        beforeGap += AcedConsts.ChunkCapacity * 4;
                    }
                    Buffer.BlockCopy(_chunk, 0, result, beforeGap, (outSize & ((AcedConsts.ChunkCapacity * 4) - 1)) + 1);
                    _chunkList = null;
                    _chunk = null;
                }
                else
                {
                    _chunkList = null;
                    _chunk = null;
                    result = new byte[beforeGap + 4 + sourceLength + afterGap];
                    fixed (byte* pResult = &result[beforeGap])
                        *((int*)pResult) = -sourceLength;
                    Buffer.BlockCopy(sourceBytes, sourceIndex, result, beforeGap + 4, sourceLength);
                }
            }
            return result;
        }

        public unsafe int Compress(byte[] sourceBytes, int sourceIndex, int sourceLength,
            AcedCompressionLevel compressionLevel, byte[] destinationBytes, int destinationIndex)
        {
            _tryMode = false;
            if (destinationBytes != null)
                _dstBreak = destinationBytes.Length - destinationIndex - 4;
            else
            {
                _tryMode = true;
                destinationBytes = _fakeDestinationBytes;
                destinationIndex = 0;
                _dstBreak = sourceLength;
            }
            if (sourceLength == 0)
            {
                if (!_tryMode)
                {
                    if (_dstBreak < 0)
                        AcedNoPlaceToStoreCompressedDataException.Throw();
                    fixed (byte* pDstBytes = &destinationBytes[destinationIndex])
                        *((int*)pDstBytes) = 0;
                }
                return 4;
            }
            if (sourceBytes == null)
                AcedConsts.ThrowArgumentNullException("sourceBytes");
            _growMode = false;
            if (sourceLength < _dstBreak)
                _dstBreak = sourceLength;
            _breakOffset = sourceLength;
            fixed (byte* pSrcBytes = &sourceBytes[sourceIndex], pDstBytes = &destinationBytes[destinationIndex])
            {
                _pSrcBytes = pSrcBytes;
                _pDstBytes = pDstBytes;
                int result = -1;
                if (sourceLength > 3 && compressionLevel != AcedCompressionLevel.Store)
                {
                    SetupCompressionLevel(compressionLevel);
                    if (!_tryMode)
                    {
                        if (_dstBreak < 0)
                            AcedNoPlaceToStoreCompressedDataException.Throw();
                        *((int*)_pDstBytes) = sourceLength;
                        _pDstBytes += 4;
                    }
                    result = CompressCore();
                }
                if (result < 0)
                {
                    if (!_tryMode)
                    {
                        if (_dstBreak < sourceLength)
                            AcedNoPlaceToStoreCompressedDataException.Throw();
                        *((int*)pDstBytes) = -sourceLength;
                        Buffer.BlockCopy(sourceBytes, sourceIndex, destinationBytes, destinationIndex + 4, sourceLength);
                    }
                    result = sourceLength;
                }
                return result + 4;
            }
        }

        private void SetupCompressionLevel(AcedCompressionLevel compressionLevel)
        {
            switch (compressionLevel)
            {
                case AcedCompressionLevel.Fastest:
                    _hashSize = hashSizeFastest;
                    _hashMask = hashMaskFastest;
                    _prevSize = prevSizeFastest;
                    _prevMask = prevMaskFastest;
                    _maxChain = chainFastest;
                    _shift = shiftFastest;
                    break;
                case AcedCompressionLevel.Fast:
                    _hashSize = hashSizeFast;
                    _hashMask = hashMaskFast;
                    _prevSize = prevSizeFast;
                    _prevMask = prevMaskFast;
                    _maxChain = chainFast;
                    _shift = shiftFast;
                    break;
                case AcedCompressionLevel.Normal:
                    _hashSize = hashSizeNormal;
                    _hashMask = hashMaskNormal;
                    _prevSize = prevSizeNormal;
                    _prevMask = prevMaskNormal;
                    _maxChain = chainNormal;
                    _shift = shiftNormal;
                    break;
                case AcedCompressionLevel.Maximum:
                    _hashSize = hashSizeMaximum;
                    _hashMask = hashMaskMaximum;
                    _prevSize = prevSizeMaximum;
                    _prevMask = prevMaskMaximum;
                    _maxChain = chainMaximum;
                    _shift = shiftMaximum;
                    break;
            }
        }

        public static void Release()
        {
            _instance = null;
        }
    }

    // AcedInflator class (not thread-safe)

    public class AcedInflator
    {
        private static AcedInflator _instance;

        private int _srcIndex;
        private int _break32Offset;
        private int _breakOffset;
        private int _hold;
        private int _bits;
        private int _outCounter;
        private int _inCounter;
        private int _r0;
        private int _r1;
        private int _r2;

        private unsafe byte* _pSrcBytes;
        private unsafe byte* _pDstBytes;

        private unsafe int* _pCharTree;
        private unsafe int* _pDistTree;
        private unsafe int* _pChLenTree;
        private unsafe int* _pBitLen;
        private unsafe int* _pBitCount;
        private unsafe int* _pNextCode;

        private int[] _charTree;
        private int[] _distTree;
        private int[] _chLenTree;
        private int[] _bitLen;
        private int[] _bitCount;
        private int[] _nextCode;

        private unsafe int* _pCharExBitLength;
        private unsafe int* _pCharExBitBase;
        private unsafe int* _pDistExBitLength;
        private unsafe int* _pDistExBitBase;

        public static AcedInflator Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new AcedInflator();
                return _instance;
            }
        }

        public AcedInflator()
        {
            _charTree = new int[AcedConsts.CharTreeSize];
            _distTree = new int[AcedConsts.DistTreeSize];
            _chLenTree = new int[AcedConsts.ChLenTreeSize];
            _bitLen = new int[AcedConsts.CharCount];
            _bitCount = new int[AcedConsts.MaxBits + 1];
            _nextCode = new int[AcedConsts.MaxBits + 1];
        }

        private unsafe int GetNBits(int n)
        {
            int hold = _hold;
            int bits = _bits;
            while (bits < n)
                if (bits < 8 && _srcIndex < _break32Offset)
                {
                    _srcIndex += 3;
                    hold |= (_pSrcBytes[0] | (_pSrcBytes[1] << 8) | (_pSrcBytes[2] << 16)) << bits;
                    _pSrcBytes += 3;
                    bits += 24;
                }
                else if (_srcIndex < _breakOffset)
                {
                    _srcIndex++;
                    hold |= (*_pSrcBytes) << bits;
                    _pSrcBytes++;
                    bits += 8;
                }
                else
                    AcedReadBeyondTheEndException.Throw();
            _hold = hold >> n;
            _bits = bits - n;
            return hold & ((1 << n) - 1);
        }

        private unsafe int GetBit()
        {
            uint hold = (uint)_hold;
            int bits = _bits;
            if (bits != 0)
                bits--;
            else if (_srcIndex < _break32Offset)
            {
                _srcIndex += 4;
                hold = *((uint*)_pSrcBytes);
                _pSrcBytes += 4;
                bits = 31;
            }
            else if (_srcIndex < _breakOffset)
            {
                _srcIndex++;
                hold = (uint)(*_pSrcBytes);
                _pSrcBytes++;
                bits = 7;
            }
            else
                AcedReadBeyondTheEndException.Throw();
            _hold = (int)(hold >> 1);
            _bits = bits;
            return (int)(hold & 1);
        }

        private unsafe int GetCode(int* tree)
        {
            int code = 1;
            int hold = _hold;
            do
            {
                while (_bits != 0)
                {
                    code = tree[code + (hold & 1)];
                    hold >>= 1;
                    _bits--;
                    if (code <= 0)
                        goto CodeFound;
                }
                if (_srcIndex < _break32Offset)
                {
                    _srcIndex += 4;
                    hold = *((int*)_pSrcBytes);
                    _pSrcBytes += 4;
                    code = tree[code + (hold & 1)];
                    hold = (int)((uint)hold >> 1);
                    _bits = 31;
                }
                else if (_srcIndex < _breakOffset)
                {
                    _srcIndex++;
                    hold = (int)(*_pSrcBytes);
                    _pSrcBytes++;
                    code = tree[code + (hold & 1)];
                    hold >>= 1;
                    _bits = 7;
                }
                else
                    AcedReadBeyondTheEndException.Throw();
            } while (code > 0);
        CodeFound:
            _hold = hold;
            return -code;
        }

        private unsafe void LoadChLenTree()
        {
            int n, m, p, i, code;
            AcedBinary.Fill(0, _pBitCount, AcedConsts.MaxChLenBits + 1);
            for (i = 0; i < AcedConsts.ChLenCount; i++)
            {
                n = GetNBits(3);
                _pBitLen[i] = n;
                _pBitCount[n]++;
            }
            AcedBinary.Fill(0, _pChLenTree, AcedConsts.ChLenTreeSize);
            _pNextCode[1] = 0;
            _pNextCode[2] = n = _pBitCount[1] << 1;
            _pNextCode[3] = n = (n + _pBitCount[2]) << 1;
            _pNextCode[4] = n = (n + _pBitCount[3]) << 1;
            _pNextCode[5] = n = (n + _pBitCount[4]) << 1;
            _pNextCode[6] = n = (n + _pBitCount[5]) << 1;
            _pNextCode[7] = n = (n + _pBitCount[6]) << 1;
            int treeLen = 2;
            for (i = 0; i < AcedConsts.ChLenCount; i++)
            {
                n = _pBitLen[i];
                if (n == 0)
                    continue;
                m = _pNextCode[n];
                code = (int)AcedBinary.ReverseBits((uint)m, n);
                _pNextCode[n] = m + 1;
                p = 1;
                while (true)
                {
                    p += code & 1;
                    code >>= 1;
                    n--;
                    if (n != 0)
                    {
                        m = p;
                        p = _pChLenTree[p];
                        if (p == 0)
                        {
                            p = treeLen + 1;
                            treeLen = p + 1;
                            _pChLenTree[m] = p;
                        }
                    }
                    else
                    {
                        _pChLenTree[p] = -i;
                        break;
                    }
                }
            }
        }

        private unsafe void LoadCharDistLengths(int count)
        {
            int c, lastLen = 0;
            int* p = _pBitLen;
            AcedBinary.Fill(0, _pBitCount, AcedConsts.MaxBits + 1);
            while (count > 0)
            {
                c = GetCode(_pChLenTree);
                if (c < 15)
                {
                    *p = c;
                    _pBitCount[c]++;
                    p++;
                    lastLen = c;
                    count--;
                }
                else
                {
                    if (c < 17)
                    {
                        if (c == 15)
                            c = 2;
                        else
                            c = GetBit() + 3;
                    }
                    else if (c == 17)
                        c = GetNBits(2) + 5;
                    else if (c == 18)
                        c = GetNBits(3) + 9;
                    else
                        c = GetNBits(7) + 17;
                    count -= c;
                    _pBitCount[lastLen] += c;
                    do
                    {
                        c--;
                        *p = lastLen;
                        p++;
                    } while (c != 0);
                }
            }
        }

        private unsafe void LoadCharTree()
        {
            int n, m, p, code;
            AcedBinary.Fill(0, _pBitLen, AcedConsts.CharCount);
            LoadCharDistLengths(GetNBits(6) + 257);
            AcedBinary.Fill(0, _pCharTree, AcedConsts.CharTreeSize);
            _pNextCode[1] = 0;
            _pNextCode[2] = n = _pBitCount[1] << 1;
            _pNextCode[3] = n = (n + _pBitCount[2]) << 1;
            _pNextCode[4] = n = (n + _pBitCount[3]) << 1;
            _pNextCode[5] = n = (n + _pBitCount[4]) << 1;
            _pNextCode[6] = n = (n + _pBitCount[5]) << 1;
            _pNextCode[7] = n = (n + _pBitCount[6]) << 1;
            _pNextCode[8] = n = (n + _pBitCount[7]) << 1;
            _pNextCode[9] = n = (n + _pBitCount[8]) << 1;
            _pNextCode[10] = n = (n + _pBitCount[9]) << 1;
            _pNextCode[11] = n = (n + _pBitCount[10]) << 1;
            _pNextCode[12] = n = (n + _pBitCount[11]) << 1;
            _pNextCode[13] = n = (n + _pBitCount[12]) << 1;
            _pNextCode[14] = n = (n + _pBitCount[13]) << 1;
            int treeLen = 2;
            for (int i = 0; i < AcedConsts.CharCount; i++)
            {
                n = _pBitLen[i];
                if (n == 0)
                    continue;
                m = _pNextCode[n];
                code = (int)AcedBinary.ReverseBits((uint)m, n);
                _pNextCode[n] = m + 1;
                p = 1;
                while (true)
                {
                    p += code & 1;
                    code >>= 1;
                    n--;
                    if (n != 0)
                    {
                        m = p;
                        p = _pCharTree[p];
                        if (p == 0)
                        {
                            p = treeLen + 1;
                            treeLen = p + 1;
                            _pCharTree[m] = p;
                        }
                    }
                    else
                    {
                        _pCharTree[p] = -i;
                        break;
                    }
                }
            }
        }

        private unsafe void LoadDistTree()
        {
            int n, m, p, code;
            AcedBinary.Fill(0, _pBitLen, AcedConsts.DistCount);
            LoadCharDistLengths(GetNBits(6) + 1);
            AcedBinary.Fill(0, _pDistTree, AcedConsts.DistTreeSize);
            _pNextCode[1] = 0;
            _pNextCode[2] = n = _pBitCount[1] << 1;
            _pNextCode[3] = n = (n + _pBitCount[2]) << 1;
            _pNextCode[4] = n = (n + _pBitCount[3]) << 1;
            _pNextCode[5] = n = (n + _pBitCount[4]) << 1;
            _pNextCode[6] = n = (n + _pBitCount[5]) << 1;
            _pNextCode[7] = n = (n + _pBitCount[6]) << 1;
            _pNextCode[8] = n = (n + _pBitCount[7]) << 1;
            _pNextCode[9] = n = (n + _pBitCount[8]) << 1;
            _pNextCode[10] = n = (n + _pBitCount[9]) << 1;
            _pNextCode[11] = n = (n + _pBitCount[10]) << 1;
            _pNextCode[12] = n = (n + _pBitCount[11]) << 1;
            _pNextCode[13] = n = (n + _pBitCount[12]) << 1;
            _pNextCode[14] = n = (n + _pBitCount[13]) << 1;
            int treeLen = 2;
            for (int i = 0; i < AcedConsts.DistCount; i++)
            {
                n = _pBitLen[i];
                if (n == 0)
                    continue;
                m = _pNextCode[n];
                code = (int)AcedBinary.ReverseBits((uint)m, n);
                _pNextCode[n] = m + 1;
                p = 1;
                while (true)
                {
                    p += code & 1;
                    code >>= 1;
                    n--;
                    if (n != 0)
                    {
                        m = p;
                        p = _pDistTree[p];
                        if (p == 0)
                        {
                            p = treeLen + 1;
                            treeLen = p + 1;
                            _pDistTree[m] = p;
                        }
                    }
                    else
                    {
                        _pDistTree[p] = -i;
                        break;
                    }
                }
            }
        }

        private unsafe void ReadBlockHeader()
        {
            _inCounter = AcedConsts.BlockSize;
            if (GetBit() == 0)
                ReadNonCompressedBlock();
            else
            {
                LoadChLenTree();
                LoadCharTree();
                LoadDistTree();
            }
        }

        private unsafe void ReadNonCompressedBlock()
        {
            _inCounter += GetNBits(8);
            int bits = _bits;
            while (_inCounter > 0 && _outCounter > 0)
            {
                int hold = _hold;
                if (bits < 8)
                    if (_srcIndex < _break32Offset)
                    {
                        _srcIndex += 3;
                        hold |= (_pSrcBytes[0] | (_pSrcBytes[1] << 8) | (_pSrcBytes[2] << 16)) << bits;
                        _pSrcBytes += 3;
                        bits += 24;
                    }
                    else if (_srcIndex < _breakOffset)
                    {
                        _srcIndex++;
                        hold |= (*_pSrcBytes) << bits;
                        _pSrcBytes++;
                        bits += 8;
                    }
                    else
                        AcedReadBeyondTheEndException.Throw();
                _hold = hold >> 8;
                bits -= 8;
                *_pDstBytes = (byte)hold;
                _inCounter--;
                _outCounter--;
                _pDstBytes++;
            }
            _bits = bits;
        }

        public static unsafe int GetDecompressedLength(byte[] sourceBytes, int sourceIndex)
        {
            if (sourceBytes == null)
                AcedConsts.ThrowArgumentNullException("sourceBytes");
            fixed (byte* pSrcBytes = &sourceBytes[sourceIndex])
            {
                int result = *((int*)pSrcBytes);
                if (result >= 0)
                    return result;
                return -result;
            }
        }

        public unsafe byte[] Decompress(byte[] sourceBytes, int sourceIndex, int beforeGap, int afterGap)
        {
            if (sourceBytes == null)
                AcedConsts.ThrowArgumentNullException("sourceBytes");
            int byteCount;
            fixed (byte* pSrcBytes = &sourceBytes[sourceIndex])
                byteCount = *((int*)pSrcBytes);
            if (byteCount < 0)
                byteCount = -byteCount;
            byte[] result = new byte[byteCount + beforeGap + afterGap];
            if (byteCount != 0)
                Decompress(sourceBytes, sourceIndex, result, beforeGap);
            return result;
        }

        public unsafe int Decompress(byte[] sourceBytes, int sourceIndex, byte[] destinationBytes, int destinationIndex)
        {
            if (sourceBytes == null)
                AcedConsts.ThrowArgumentNullException("sourceBytes");
            if (destinationBytes == null)
                return GetDecompressedLength(sourceBytes, sourceIndex);
            fixed (byte* pSrcBytes = &sourceBytes[sourceIndex], pDstBytes = &destinationBytes[destinationIndex])
            {
                _pSrcBytes = pSrcBytes;
                _pDstBytes = pDstBytes;
                int byteCount = *((int*)_pSrcBytes);
                if (byteCount <= 0)
                {
                    byteCount = -byteCount;
                    if (destinationBytes.Length - destinationIndex < byteCount)
                        AcedNoPlaceToStoreDecompressedDataException.Throw();
                    if (byteCount > 0)
                        Buffer.BlockCopy(sourceBytes, sourceIndex + 4, destinationBytes, destinationIndex, byteCount);
                    return byteCount;
                }
                if (destinationBytes.Length - destinationIndex < byteCount)
                    AcedNoPlaceToStoreDecompressedDataException.Throw();
                fixed (int* pCharTree = &_charTree[0], pDistTree = &_distTree[0],
                        pChLenTree = &_chLenTree[0], pBitLen = &_bitLen[0],
                        pBitCount = &_bitCount[0], pNextCode = &_nextCode[0],
                        pCharExBitLength = &AcedConsts.CharExBitLength[0],
                        pCharExBitBase = &AcedConsts.CharExBitBase[0],
                        pDistExBitLength = &AcedConsts.DistExBitLength[0],
                        pDistExBitBase = &AcedConsts.DistExBitBase[0])
                {
                    _pCharTree = pCharTree;
                    _pDistTree = pDistTree;
                    _pChLenTree = pChLenTree;
                    _pBitLen = pBitLen;
                    _pBitCount = pBitCount;
                    _pNextCode = pNextCode;
                    _pCharExBitLength = pCharExBitLength;
                    _pCharExBitBase = pCharExBitBase;
                    _pDistExBitLength = pDistExBitLength;
                    _pDistExBitBase = pDistExBitBase;
                    _bits = 0;
                    _hold = 0;
                    _srcIndex = sourceIndex + 4;
                    _pSrcBytes += 4;
                    _breakOffset = sourceBytes.Length;
                    _break32Offset = _breakOffset - 3;
                    int length, distance;
                    _outCounter = byteCount;
                    while (_outCounter > 0)
                    {
                        ReadBlockHeader();
                        while (_inCounter > 0 && _outCounter > 0)
                        {
                            int c = GetCode(_pCharTree);
                            _inCounter--;
                            if (c < AcedConsts.FirstLengthChar)
                            {
                                *_pDstBytes = (byte)c;
                                _outCounter--;
                                _pDstBytes++;
                            }
                            else
                            {
                                c -= AcedConsts.FirstCharWithExBit;
                                if (c < 0)
                                    length = c + 19;
                                else
                                    length = GetNBits(_pCharExBitLength[c]) + _pCharExBitBase[c];
                                c = GetCode(_pDistTree);
                                if (c < 3)
                                {
                                    if (c == 0)
                                        distance = _r0;
                                    else if (c == 1)
                                    {
                                        distance = _r1;
                                        _r1 = _r0;
                                        _r0 = distance;
                                    }
                                    else
                                    {
                                        distance = _r2;
                                        _r2 = _r0;
                                        _r0 = distance;
                                    }
                                }
                                else
                                {
                                    distance = _pDistExBitBase[c];
                                    if (c >= AcedConsts.FirstDistWithExBit)
                                    {
                                        distance += GetNBits(_pDistExBitLength[c]);
                                        _r2 = _r1;
                                        _r1 = _r0;
                                        _r0 = distance;
                                    }
                                }
                                AcedBinary.CopyBytes(_pDstBytes - distance, _pDstBytes, length);
                                _outCounter -= length;
                                _pDstBytes += length;
                            }
                        }
                    }
                }
                return byteCount;
            }
        }

        public static void Release()
        {
            _instance = null;
        }
    }
}
