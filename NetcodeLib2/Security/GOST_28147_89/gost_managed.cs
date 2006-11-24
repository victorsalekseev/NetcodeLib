using System;
using System.Security.Cryptography;

namespace Netcode.Security.GOST_28147_89
{
	/// <summary>
	/// класс GostManaged реализует управляемую версию алгоритма ГОСТ
	/// в режимах шифрования ЕСВ и СВС.
	/// framework 1.1
	/// </summary>
	public class GostManaged : SymmetricAlgorithm
	{
		public GostManaged()
		{
			this.LegalKeySizesValue = new KeySizes[]{new KeySizes(256,256,0)}; 

			this.LegalBlockSizesValue = new KeySizes[]{new KeySizes(64,64,0)};

			this.BlockSize = 64; // 8 байтов и не может принимать другие значения
			this.KeySize = 256; // размер ключа 256 бит

			this.Padding = PaddingMode.Zeros; //режим дополнения блока
			this.Mode = CipherMode.ECB; // режим шифрования
		}

		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			Key = key; // this appears to make a new copy

			if (Mode == CipherMode.CBC)
				IV = iv;
			
			return new GostTransform(ref KeyValue, ref IVValue, ModeValue, GostBase.EncryptionDirection.Encrypting);
		}

		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			Key = key;

			if (Mode == CipherMode.CBC)
				IV = iv;

			return new GostTransform(ref KeyValue, ref IVValue, ModeValue, GostBase.EncryptionDirection.Decrypting);
		}

		//генерация вектора инициализации 
		public override void GenerateIV()
		{
			IVValue = new byte[8];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(IVValue); 
		}

		//генерация ключа
		public override void GenerateKey()
		{
			KeyValue = new byte[KeySize/8];

			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(KeyValue); 
		}

		//установка режима шифрования
		public override CipherMode Mode
		{
			set
			{
				switch (value)
				{
					case CipherMode.CBC:
						break;
					case CipherMode.ECB:
						break;
					default:
						throw (new CryptographicException("Выбранный режим шифрования не поддерживается"));
				}
				this.ModeValue = value;
			}
		}
	}
}
