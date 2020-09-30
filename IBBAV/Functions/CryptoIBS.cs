using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace IBBAV.Functions
{
	public class CryptoIBS
	{
		private static byte[] IV_192;

		private static char filler;

		public static UnicodeEncoding encoding;

		static CryptoIBS()
		{
			CryptoIBS.IV_192 = new byte[8];
			CryptoIBS.filler = 'F';
			CryptoIBS.encoding = new UnicodeEncoding();
		}

		public CryptoIBS()
		{
		}

		private static string byteArrayToHexString(byte[] b)
		{
			StringBuilder stringBuilder = new StringBuilder((int)b.Length * 2);
			for (int i = 0; i < (int)b.Length; i++)
			{
				int num = b[i] & 255;
				stringBuilder.Append(string.Format("{0:X2}", num));
			}
			return stringBuilder.ToString().ToUpper();
		}

		public static string DesEncriptar(string message, string key)
		{
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = CryptoIBS.getprovider(key);
			byte[] byteArray = CryptoIBS.hexStringToByteArray(message);
			byte[] numArray = tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(byteArray, 0, (int)byteArray.Length);
			return CryptoIBS.byteArrayToHexString(numArray);
		}

		public static string Encriptar(string message, string key)
		{
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = CryptoIBS.getprovider(key);
			byte[] byteArray = CryptoIBS.hexStringToByteArray(message.PadRight(16, CryptoIBS.filler));
			byte[] numArray = tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(byteArray, 0, (int)byteArray.Length);
			return CryptoIBS.byteArrayToHexString(numArray);
		}

		public static TripleDESCryptoServiceProvider getprovider(string key)
		{
			return new TripleDESCryptoServiceProvider()
			{
				KeySize = 128,
				Key = CryptoIBS.hexStringToByteArray(key),
				Mode = CipherMode.CBC,
				Padding = PaddingMode.Zeros,
				IV = CryptoIBS.IV_192
			};
		}

		private static byte[] hexStringToByteArray(string s)
		{
			byte[] numArray = new byte[s.Length / 2];
			int num = 0;
			for (int i = 0; i < (int)numArray.Length; i++)
			{
				string str = s.Substring(num, 2);
				numArray[i] = (byte)int.Parse(str, NumberStyles.HexNumber);
				num += 2;
			}
			return numArray;
		}
	}
}