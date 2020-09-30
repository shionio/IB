using System;
using System.Collections;
using System.Collections.Generic;

namespace IBBAV.Functions
{
	public class Utilities<T>
	{
		private static Random rng;

		static Utilities()
		{
			Utilities<T>.rng = new Random();
		}

		public Utilities()
		{
		}

		public static List<T> EnumToList()
		{
			Type type = typeof(T);
			if (type.BaseType != typeof(Enum))
			{
				throw new ArgumentException("T must be of type System.Enum");
			}
			Array values = Enum.GetValues(type);
			List<T> ts = new List<T>(values.Length);
			foreach (int value in values)
			{
				ts.Add((T)Enum.Parse(type, value.ToString()));
			}
			return ts;
		}

		public static List<T> Shuffle(List<T> c)
		{
			T[] tArray = new T[c.Count];
			c.CopyTo(tArray, 0);
			byte[] numArray = new byte[(int)tArray.Length];
			Utilities<T>.rng.NextBytes(numArray);
			Array.Sort<byte, T>(numArray, tArray);
			return new List<T>(tArray);
		}
	}
}