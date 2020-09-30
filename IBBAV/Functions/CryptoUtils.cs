using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IBBAV.Functions
{
	public class CryptoUtils
	{
		private static byte[] KEY_192;

		private static byte[] IV_192;

		private static TripleDESCryptoServiceProvider cryptoProvider;

		static CryptoUtils()
		{
			CryptoUtils.KEY_192 = new byte[] { 40, 50, 60, 89, 92, 6, 217, 30, 15, 16, 44, 60, 65, 25, 14, 12, 2, 14, 10, 20, 19, 9, 14, 17 };
			CryptoUtils.IV_192 = new byte[] { 5, 13, 52, 4, 8, 1, 17, 3, 42, 5, 82, 83, 16, 7, 29, 13, 11, 3, 22, 8, 16, 10, 11, 25 };
			CryptoUtils.cryptoProvider = new TripleDESCryptoServiceProvider();
		}

		private CryptoUtils()
		{
		}

		public static string EncryptDES(string value)
		{
			return CryptoUtils.EncryptDES(CryptoUtils.KEY_192, value);
		}

		public static string EncryptDES(byte[] key, string value)
		{
			string base64String = "";
			if (value.Length > 0)
			{
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, CryptoUtils.cryptoProvider.CreateEncryptor(key, CryptoUtils.IV_192), CryptoStreamMode.Write);
					StreamWriter streamWriter = new StreamWriter(cryptoStream);
					streamWriter.Write(value);
					streamWriter.Flush();
					cryptoStream.FlushFinalBlock();
					memoryStream.Flush();
					base64String = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				}
				catch
				{
				}
			}
			return base64String;
		}

		public static string EncryptMD5(string value)
		{
			string str = "";
			if (value.Length > 0)
			{
				try
				{
					MD5 mD5 = MD5.Create();
					byte[] bytes = Encoding.Default.GetBytes(value);
					bytes = mD5.ComputeHash(bytes);
					int length = (int)bytes.Length;
					StringBuilder stringBuilder = new StringBuilder(length);
					for (int i = 0; i < length; i++)
					{
						stringBuilder.AppendFormat("{0:x2}", bytes[i]);
					}
					str = stringBuilder.ToString();
					stringBuilder = null;
				}
				catch
				{
				}
			}
			return str;
		}

		public static string UncryptDES(string value)
		{
			return CryptoUtils.UncryptDES(CryptoUtils.KEY_192, value);
		}

		public static string UncryptDES(byte[] key, string value)
		{
			string end = "";
			if (value.Length > 0)
			{
				try
				{
					MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value));
					CryptoStream cryptoStream = new CryptoStream(memoryStream, CryptoUtils.cryptoProvider.CreateDecryptor(key, CryptoUtils.IV_192), CryptoStreamMode.Read);
					end = (new StreamReader(cryptoStream)).ReadToEnd();
				}
				catch
				{
				}
			}
			return end;
		}
	}
}