using System;
using System.Security.Cryptography;
using System.Text;

namespace IBBAV
{
	public class cMD5
	{
		public cMD5()
		{
		}

		public static string ObtenerMd5(string pass)
		{
			MD5 mD5 = MD5.Create();
			byte[] numArray = mD5.ComputeHash(Encoding.Default.GetBytes(pass));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < (int)numArray.Length; i++)
			{
				stringBuilder.AppendFormat("{0:x2}", numArray[i]);
			}
			return stringBuilder.ToString();
		}
	}
}