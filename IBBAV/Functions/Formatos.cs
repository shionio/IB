using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace IBBAV.Functions
{
	public class Formatos
	{
		public Formatos()
		{
		}

		public static string FormatCadena(string s)
		{
			s = s.Replace("ñ", "n").Replace("Ñ", "N").Replace("'", " ").Replace("º", " ").Replace("¢", " ");
			s = s.Replace("!", " ").Replace("#", " ").Replace("$", " ").Replace("%", " ");
			s = s.Replace("&", " ").Replace("/", " ").Replace("(", " ").Replace(")", " ").Replace("=", " ");
			s = s.Replace("?", " ").Replace("¡", " ").Replace("[", " ").Replace("]", " ").Replace("_", " ");
			s = s.Replace(":", " ").Replace(";", " ").Replace(",", " ").Replace(".", " ").Replace("-", " ");
			s = s.Replace("{", " ").Replace("}", " ").Replace("¿", " ").Replace("'", " ").Replace("'", " ");
			s = s.Replace("´", " ").Replace("¨", " ").Replace("*", " ").Replace("^", " ").Replace("°", " ").Replace("¬", " ").Replace("´", " ").Replace("~", " ").Replace("|", " ").Replace("¦", " ").Replace("Â", " ");
			s = s.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");
			s = s.Replace("à", "a").Replace("è", "e").Replace("ì", "i").Replace("ò", "o").Replace("ù", "u").Replace("À", "A").Replace("È", "E").Replace("Ì", "I").Replace("Ò", "O").Replace("Ù", "U");
			s = s.Replace("ä", "a").Replace("ë", "e").Replace("ï", "i").Replace("ö", "o").Replace("ü", "u").Replace("Ä", "A").Replace("Ë", "E").Replace("Ï", "I").Replace("Ö", "O").Replace("Ü", "U");
			return s;
		}

		public static string formatocedula(string cedula)
		{
			string empty = string.Empty;
			string str = cedula.Substring(0, 1);
			cedula = cedula.Substring(1, cedula.Length - 1);
			if ("JGOFP".IndexOf(str) > 0)
			{
				string str1 = cedula.Substring(cedula.Length - 1, 1);
				cedula = cedula.Substring(1, cedula.Length - 1);
				string[] strArrays = new string[] { str, "-", null, null, null };
				decimal num1 = Formatos.ISOToDecimal(cedula);
				strArrays[2] = num1.ToString("#,##0");
				strArrays[3] = "-";
				strArrays[4] = str1;
				empty = string.Concat(strArrays);
			}
			else
			{
				decimal num = Formatos.ISOToDecimal(cedula);
				empty = string.Concat(str, "-", num.ToString("#,##0"));
			}
			return empty;
		}

		public static string formatoCuenta(string valor)
		{
			valor = valor.Trim();
			valor = valor.Replace("-", "");
			string str = valor;
			if (str != "")
			{
				string str1 = valor.Substring(0, 4);
				int length = valor.Length;
				string str2 = valor.Substring(length - 4, 4);
				length -= 8;
				StringBuilder stringBuilder = new StringBuilder(512);
				stringBuilder.Append(str1);
				stringBuilder.Append(new string('*', length));
				stringBuilder.Append(str2);
				str = stringBuilder.ToString();
			}
			return str;
		}

		public static string formatoCuentaVisible(string p)
		{
			return string.Concat(new string[] { p.Substring(0, 4), "-", p.Substring(4, 4), "-", p.Substring(8, 2), "-", p.Substring(10, 10) });
		}

		public static string formatoEmail(string valor)
		{
			string str = valor.Trim();
			string str1 = valor.Substring(0, valor.IndexOf("@"));
			str1 = string.Concat(str1.Substring(0, str1.Length - 4), "****");
			str = string.Concat(str1, valor.Substring(valor.IndexOf("@"), valor.Length - str1.Length));
			return str;
		}

		public static DateTime FormatoFechaCorrecta(string fecha)
		{
			CultureInfo cultureInfo = new CultureInfo("es-VE");
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			DateTime dateTime = DateTime.Parse(fecha);
			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentCulture;
			return dateTime;
		}

        public static string formatoMonto(string valor)
		{
			string str = valor;
			if (str != "")
			{
				valor = valor.Replace(".", ",");
				Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
                str = Formatos.formatoMonto(double.Parse(valor)); 
			}
			return str;
		}

		public static string formatoMonto(decimal monto)
		{
			return monto.ToString("#,##0.00");
		}

        // Agregado 21/07/2018 por Liliana Guerra, para validar operación mismo titular (sCod == 6) y mostrar mensaje de comision en %
		
        public static string formatoMonto3Decimales(decimal monto)
        {
            return monto.ToString("#,##0.000");
        }

        //Agregado 12/09/2018 por Liliana Guerra, Formato saldo PETROS
        public static string formatoSaldoPetro(decimal monto)
        {
            return monto.ToString("#,##0.0000000");
        }

        public static string formatoMonto(double monto)
		{
			return monto.ToString("#,##0.00");
		}

        public static string formatoMontoAgrDecimal(string monto)
        {
            int nroNum2 = Convert.ToInt32(monto);
            monto = (nroNum2 / 100m).ToString("N2");

            return monto;
        }

        public static string formatoTarjeta(string valor)
		{
			valor = valor.Trim();
			string str = valor.Trim();
			if (str != "")
			{
				string str1 = valor.Substring(0, 4);
				int length = valor.Length;
				string str2 = valor.Substring(length - 4, 4);
				length -= 8;
				StringBuilder stringBuilder = new StringBuilder(512);
				stringBuilder.Append(str1);
				for (int i = 0; i < length; i++)
				{
					stringBuilder.Append("*");
				}
				stringBuilder.Append(str2);
				str = stringBuilder.ToString();
			}
			return str;
		}

		public static bool IsFormatoFechaCorrecta(string fecha)
		{
			DateTime dateTime;
			bool flag = false;
			CultureInfo cultureInfo = new CultureInfo("es-VE");
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			flag = DateTime.TryParse(fecha, out dateTime);
			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentCulture;
			return flag;
		}

		public static decimal ISOToDecimal(string monto)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			decimal num = decimal.Parse(monto);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
			return num;
		}


		public static double ISOToDouble(string monto)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			double num = double.Parse(monto);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
			return num;
		}

		public static DateTime ISOToFecha(string Fecha)
		{
			int num = 0;
			int num1 = (int.Parse(Fecha.Substring(0, 2)) == 0 ? 1 : int.Parse(Fecha.Substring(0, 2)));
			int num2 = (int.Parse(Fecha.Substring(2, 2)) == 0 ? 1 : int.Parse(Fecha.Substring(2, 2)));
			if (Fecha.Length == 6)
			{
				num = int.Parse(Fecha.Substring(4, 2)) + 2000;
			}
			if (Fecha.Length == 8)
			{
				num = (int.Parse(Fecha.Substring(4, 4)) == 0 ? 2000 : int.Parse(Fecha.Substring(4, 4)));
			}
			return new DateTime(num, num2, num1);
		}

		public static DateTime ISOToFecha(string Fecha, string format)
		{
			int num = 0;
			int num1 = format.ToUpper().IndexOf('D');
			int num2 = format.ToUpper().IndexOf('M');
			int num3 = format.ToUpper().IndexOf('Y');
			int num4 = (int.Parse(Fecha.Substring(num1, 2)) == 0 ? 1 : int.Parse(Fecha.Substring(num1, 2)));
			int num5 = (int.Parse(Fecha.Substring(num2, 2)) == 0 ? 1 : int.Parse(Fecha.Substring(num2, 2)));
			if (format.Length == 6)
			{
				num = int.Parse(Fecha.Substring(num3, 2)) + 2000;
			}
			if (format.Length == 8)
			{
				num = (int.Parse(Fecha.Substring(num3, 4)) == 0 ? 2000 : int.Parse(Fecha.Substring(num3, 4)));
			}
			return new DateTime(num, num5, num4);
		}
	}
}