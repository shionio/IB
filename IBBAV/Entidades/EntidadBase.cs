using System;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class EntidadBase
	{
		public EntidadBase()
		{
		}

		public static bool IsBool(DataRow dr, string campo)
		{
			bool flag1;
			bool flag = false;
			if (dr.Table.Columns.Contains(campo))
			{
				bool.TryParse(dr[campo].ToString(), out flag);
				flag1 = flag;
			}
			else
			{
				flag1 = flag;
			}
			return flag1;
		}

		public static char IsChar(DataRow dr, string campo)
		{
			char chr1;
			char chr = ' ';
			if (dr.Table.Columns.Contains(campo))
			{
				char.TryParse(dr[campo].ToString(), out chr);
				chr1 = chr;
			}
			else
			{
				chr1 = chr;
			}
			return chr1;
		}

		public static DateTime IsDateTime(DataRow dr, string campo)
		{
			DateTime dateTime1;
			DateTime dateTime = new DateTime(2000, 1, 1);
			if (!dr.Table.Columns.Contains(campo))
			{
				dateTime1 = dateTime;
			}
			else if (!DateTime.TryParse(dr[campo].ToString(), out dateTime))
			{
				dateTime = new DateTime(2000, 1, 1);
				dateTime1 = dateTime;
			}
			else
			{
				dateTime1 = dateTime;
			}
			return dateTime1;
		}

		public static decimal IsDecimal(DataRow dr, string campo)
		{
			decimal num1;
			decimal num = new decimal(0);
			if (dr.Table.Columns.Contains(campo))
			{
				decimal.TryParse(dr[campo].ToString(), out num);
				num1 = num;
			}
			else
			{
				num1 = num;
			}
			return num1;
		}

		public static double IsDouble(DataRow dr, string campo)
		{
			double num1;
			double num = 0;
			if (dr.Table.Columns.Contains(campo))
			{
				double.TryParse(dr[campo].ToString(), out num);
				num1 = num;
			}
			else
			{
				num1 = num;
			}
			return num1;
		}

		public static int IsInt(DataRow dr, string campo)
		{
			int num1;
			int num = 0;
			if (dr.Table.Columns.Contains(campo))
			{
				int.TryParse(dr[campo].ToString(), out num);
				num1 = num;
			}
			else
			{
				num1 = num;
			}
			return num1;
		}

		public static long IsLong(DataRow dr, string campo)
		{
			long num1;
			long num = (long)0;
			if (dr.Table.Columns.Contains(campo))
			{
				long.TryParse(dr[campo].ToString(), out num);
				num1 = num;
			}
			else
			{
				num1 = num;
			}
			return num1;
		}

		public static string IsString(DataRow dr, string campo)
		{
			string empty;
			if (dr.Table.Columns.Contains(campo))
			{
				empty = (!dr.IsNull(campo) ? dr[campo].ToString() : string.Empty);
			}
			else
			{
				empty = string.Empty;
			}
			return empty;
		}
	}
}