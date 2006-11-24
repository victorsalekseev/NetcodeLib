using System;
using System.Security.Cryptography;

namespace Netcode.Security.GOST_28147_89
{
	/// <summary>
	///  ласс расшир€ющий базовую функциональность и реализующий интерфейс ICryptoTransform.
	/// </summary>
	internal class GostTransform : GostBase, ICryptoTransform
	{
		public GostTransform( ref byte[] key, ref byte[] iv, CipherMode cMode, EncryptionDirection direction)
		{
			for (int i=0;i<key.Length/4;i++)
			{
				k[i] = (uint)( key[i*4+3]<<24) | (uint)(key[i*4+2] << 16) | (uint)(key[i*4+1] << 8) | (uint)(key[i*4+0]);
			}

			cipherMode = cMode;

			if (cipherMode == CipherMode.CBC)
			{
				for (int i=0;i<2;i++)
				{
					IV[i] = (uint)( iv[i*4+3]<<24) | (uint)(iv[i*4+2] << 16) | (uint)(iv[i*4+1] << 8) | (uint)(iv[i*4+0]);
				}
			}

			encryptionDirection = direction;
			kboxinit();
		}

		private bool canReuseTransform = true;
		private bool canTransformMultipleBlocks = false;
		private EncryptionDirection encryptionDirection;

		public bool CanReuseTransform
		{
			get
			{
				return canReuseTransform;
			}
		}

		public bool CanTransformMultipleBlocks
		{
			get
			{
				return canTransformMultipleBlocks;
			}
		}

		public int InputBlockSize
		{
			get
			{
				return inputBlockSize;
			}
		}

		public int OutputBlockSize
		{
			get
			{
				return outputBlockSize;
			}
		}

		//очистка
		public void Dispose()
		{
			Destroy();
		}

		public int TransformBlock(
			byte[] inputBuffer,
			int inputOffset,
			int inputCount,
			byte[] outputBuffer,
			int outputOffset
			)
		{
			uint[] x = new uint[2];

			for (int i=0; i<2; ++i)
			{
				x[i]= (uint)(inputBuffer[i*4+3+inputOffset]<<24) | (uint)(inputBuffer[i*4+2+inputOffset] << 16) | 
					(uint)(inputBuffer[i*4+1+inputOffset] << 8) | (uint)(inputBuffer[i*4+0+inputOffset]);
			}

			if (encryptionDirection == EncryptionDirection.Encrypting)
			{
				gostcrypt(ref x);
			}
			else
			{
				gostdecrypt(ref x);
			}

			for (int i=0; i<2; ++i)
			{
				outputBuffer[i*4+0+outputOffset] = b0(x[i]);
				outputBuffer[i*4+1+outputOffset] = b1(x[i]);
				outputBuffer[i*4+2+outputOffset] = b2(x[i]);
				outputBuffer[i*4+3+outputOffset] = b3(x[i]);
			}

			return inputCount;
		}

		public byte[] TransformFinalBlock(
			byte[] inputBuffer,
			int inputOffset,
			int inputCount
			)
		{
			byte[] outputBuffer;
			
			if (inputCount>0)
			{
				outputBuffer = new byte[8];
				uint[] x = new uint[2];

				for (int i=0;i<2;i++)
				{
					x[i]= (uint)(inputBuffer[i*4+3+inputOffset]<<24) | (uint)(inputBuffer[i*4+2+inputOffset] << 16) | 
						(uint)(inputBuffer[i*4+1+inputOffset] << 8) | (uint)(inputBuffer[i*4+0+inputOffset]);

				}

				if (encryptionDirection == EncryptionDirection.Encrypting)
				{
					gostcrypt(ref x);
				}
				else
				{
					gostdecrypt(ref x);
				}

				for (int i=0; i<2; ++i)
				{
					outputBuffer[i*4+0] = b0(x[i]);
					outputBuffer[i*4+1] = b1(x[i]);
					outputBuffer[i*4+2] = b2(x[i]);
					outputBuffer[i*4+3] = b3(x[i]);
				}
			}
			else
			{
				outputBuffer = new byte[0];
			}
			
			return outputBuffer;
		}
	}
}
