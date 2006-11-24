using System;
using System.Security.Cryptography;

namespace Netcode.Security.GOST_28147_89
{
	/// <summary>
	///	Класс реализующий шифрование по алгоритму ГОСТ 28147-89
	/// </summary>
	internal class GostBase
	{
		//размер блока шифрования
		static private readonly int	BLOCK_SIZE = 64;

		//направление шифрования
		public enum EncryptionDirection
		{
			Encrypting,
			Decrypting
		}

		public GostBase()
		{

		}

		//ключ
		protected uint []k = { 0, 1, 2, 3, 4, 5, 6, 7};

		//вектор инициализации
		protected uint[] IV = {0,0};

		//режим шифрования
		protected CipherMode cipherMode = CipherMode.CBC;

		//с-блоки используемые при подстановках
		protected byte []k87 = new byte[256];
		protected byte []k65 = new byte[256];
		protected byte []k43 = new byte[256];
		protected byte []k21 = new byte[256];

		//размер входящего блока в байтах
		protected int inputBlockSize = BLOCK_SIZE/8;

		//размер возвращаемого блока в байтах
		protected int outputBlockSize = BLOCK_SIZE/8;

		//инициализация с-блоков
		protected void kboxinit()
		{
			uint i = 0;

			byte []k8 = {14,  4, 13,  1, 2, 15,  1,  8,  3, 10,  6, 12,  5,  9,  0, 7 };
			byte []k7 = {15,  1,  8, 14, 6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10};
			byte []k6 = {10,  0,  9, 14, 6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8};
			byte []k5 = { 7, 13, 14,  3, 0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15};
			byte []k4 = { 2, 12,  4,  1, 7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9};
			byte []k3 = {12,  1, 10, 15, 9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11};
			byte []k2 = { 4, 11,  2, 14,15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1};
			byte []k1 = {13,  2,  8,  4, 6, 15, 11,  1, 10,  9,  3, 14,  5,  0,  12, 7};

			for( i = 0; i < 256; ++i)
			{
				k87[i] = (byte)(k8[i >> 4] << 4 | k7[i & 15]);
				k65[i] = (byte)(k6[i >> 4] << 4 | k5[i & 15]);
				k43[i] = (byte)(k4[i >> 4] << 4 | k3[i & 15]);
				k21[i] = (byte)(k2[i >> 4] << 4 | k1[i & 15]);
			}
		}

		//функция f
		private uint f(uint x)
		{
			x = (uint)(k87[x>>24 & 255] << 24 | k65[x>>16 & 255] << 16 | k43[x>>8 & 255] << 8 | k21[x & 255]);
			return x << 11 | x >> (32-11);
		}

		//шифрование одного блока данных
		protected void gostcrypt(ref uint[] d)
		{
			uint n1;
			uint n2;

			n1 = d[0];
			n2 = d[1];

			if( cipherMode == CipherMode.CBC )
			{
				n1 ^= IV[0];
				n2 ^= IV[1];
			}

			n2 ^= f((uint)(n1+k[0])); n1 ^= f((uint)(n2+k[1]));
			n2 ^= f((uint)(n1+k[2])); n1 ^= f((uint)(n2+k[3]));
			n2 ^= f((uint)(n1+k[4])); n1 ^= f((uint)(n2+k[5]));
			n2 ^= f((uint)(n1+k[6])); n1 ^= f((uint)(n2+k[7]));

			n2 ^= f((uint)(n1+k[0])); n1 ^= f((uint)(n2+k[1]));
			n2 ^= f((uint)(n1+k[2])); n1 ^= f((uint)(n2+k[3]));
			n2 ^= f((uint)(n1+k[4])); n1 ^= f((uint)(n2+k[5]));
			n2 ^= f((uint)(n1+k[6])); n1 ^= f((uint)(n2+k[7]));

			n2 ^= f((uint)(n1+k[0])); n1 ^= f((uint)(n2+k[1]));
			n2 ^= f((uint)(n1+k[2])); n1 ^= f((uint)(n2+k[3]));
			n2 ^= f((uint)(n1+k[4])); n1 ^= f((uint)(n2+k[5]));
			n2 ^= f((uint)(n1+k[6])); n1 ^= f((uint)(n2+k[7]));

			n2 ^= f((uint)(n1+k[7])); n1 ^= f((uint)(n2+k[6]));
			n2 ^= f((uint)(n1+k[5])); n1 ^= f((uint)(n2+k[4]));
			n2 ^= f((uint)(n1+k[3])); n1 ^= f((uint)(n2+k[2]));
			n2 ^= f((uint)(n1+k[1])); n1 ^= f((uint)(n2+k[0]));

			d[0] = n2; d[1] = n1;

			if( cipherMode == CipherMode.CBC )
			{
				IV[0] = d[0];
				IV[1] = d[1];
			}
		}

		//расшифровка одного блока данных
		protected void gostdecrypt(ref uint[] d)
		{
			uint n1;
			uint n2;

			n1 = d[0];
			n2 = d[1];

			uint[] xtemp = new uint[2];

			if (cipherMode == CipherMode.CBC)
				d.CopyTo(xtemp,0);

			n2 ^= f((uint)(n1+k[0])); n1 ^= f((uint)(n2+k[1]));
			n2 ^= f((uint)(n1+k[2])); n1 ^= f((uint)(n2+k[3]));
			n2 ^= f((uint)(n1+k[4])); n1 ^= f((uint)(n2+k[5]));
			n2 ^= f((uint)(n1+k[6])); n1 ^= f((uint)(n2+k[7]));

			n2 ^= f((uint)(n1+k[7])); n1 ^= f((uint)(n2+k[6]));
			n2 ^= f((uint)(n1+k[5])); n1 ^= f((uint)(n2+k[4]));
			n2 ^= f((uint)(n1+k[3])); n1 ^= f((uint)(n2+k[2]));
			n2 ^= f((uint)(n1+k[1])); n1 ^= f((uint)(n2+k[0]));

			n2 ^= f((uint)(n1+k[7])); n1 ^= f((uint)(n2+k[6]));
			n2 ^= f((uint)(n1+k[5])); n1 ^= f((uint)(n2+k[4]));
			n2 ^= f((uint)(n1+k[3])); n1 ^= f((uint)(n2+k[2]));
			n2 ^= f((uint)(n1+k[1])); n1 ^= f((uint)(n2+k[0]));

			n2 ^= f((uint)(n1+k[7])); n1 ^= f((uint)(n2+k[6]));
			n2 ^= f((uint)(n1+k[5])); n1 ^= f((uint)(n2+k[4]));
			n2 ^= f((uint)(n1+k[3])); n1 ^= f((uint)(n2+k[2]));
			n2 ^= f((uint)(n1+k[1])); n1 ^= f((uint)(n2+k[0]));

			d[0] = n2; d[1] = n1;

			if (cipherMode == CipherMode.CBC)
			{
				d[0] ^= IV[0];
				d[1] ^= IV[1];
				IV[0] = xtemp[0]; 
				IV[1] = xtemp[1];
			}
		}

		//обнуление ключа
		protected void Destroy()
		{
			for( int i = 0; i < k.Length; ++i)
				k[i] = 0;
		}

		// вспомогательные функции возвращающие соответствующий байт
		protected static byte b0(uint x)
		{
			return (byte)(x );//& 0xFF);
		}
		
		protected static byte b1(uint x)
		{
			return (byte)((x >> 8));// & (0xFF));
		}
		
		protected static byte b2(uint x)
		{
			return (byte)((x >> 16));// & (0xFF));
		}
		
		protected static byte b3(uint x)
		{
			return (byte)((x >> 24));// & (0xFF));
		}

	}
}
