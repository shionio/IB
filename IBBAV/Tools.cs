using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV
{
	public class Tools
	{
		public const char SEPARADOR = '~';

		public const char SEPARADOR_PIPE = '|';

		private static string pagesInit;

		public static char[] SEPARADORES;

		static Tools()
		{
			Tools.pagesInit = "logout.aspx";
			Tools.SEPARADORES = new char[] { '~' };
		}

		public Tools()
		{
		}

		public static string formatoCedula(string type, string valor)
		{
			string str = string.Format("{0:0#,###,##0}", Convert.ToInt64(valor));
			string str1 = string.Concat(type, "-", str.Replace(",", "."));
			return str1;
		}

		public static string formatoCuenta(string cuenta)
		{
			string str = cuenta;
			if ((cuenta == null ? false : cuenta.Length == 10))
			{
				str = string.Concat(new string[] { cuenta.Substring(0, 3), "-", cuenta.Substring(3, 6), "-", cuenta.Substring(9) });
			}
			return str;
		}

		public static string formatoCuenta16(string cuenta)
		{
			string str = cuenta;
			if ((cuenta == null ? false : cuenta.Length == 16))
			{
				str = string.Concat(new string[] { cuenta.Substring(0, 4), "-", cuenta.Substring(4, 4), "-", cuenta.Substring(8, 4), "-", cuenta.Substring(12, 4) });
			}
			return str;
		}

		public static string formatoCuenta20(string cuenta)
		{
			string str = cuenta;
			if ((cuenta == null ? false : cuenta.Length == 20))
			{
				str = string.Concat(new string[] { cuenta.Substring(0, 4), "-", cuenta.Substring(4, 4), "-", cuenta.Substring(8, 2), "-", cuenta.Substring(10, 10) });
			}
			return str;
		}

		public static string formatoDouble(double valor)
		{
			string str = string.Format("{0:#,###.#0}", valor);
			int num = str.IndexOf(",");
			if (num > 0)
			{
				string str1 = str.Substring(0, num);
				string str2 = str.Substring(num + 1);
				str = string.Concat(str1.Replace(".", ","), ".", str2);
			}
			if (str.StartsWith(","))
			{
				str = str.Insert(0, "0");
			}
			return str;
		}

		public static string formatoDouble(decimal valor)
		{
			string str = string.Format("{0:#,###.#0}", Convert.ToDouble(valor));
			return str;
		}

		public static string formatoDouble(string valor)
		{
			valor = valor.Trim();
			if (valor.Length == 0)
			{
				valor = "0.00";
			}
			valor = valor.Replace(".", ",");
			double.Parse(valor);
			string str = string.Format("{0:#,###.#0}", double.Parse(valor));
			int num = str.IndexOf(",");
			if (num > 0)
			{
				string str1 = str.Substring(0, num);
				string str2 = str.Substring(num + 1);
				str = string.Concat(str1.Replace(".", ","), ".", str2);
			}
			str = str.Trim();
			if (str.StartsWith(","))
			{
				str = string.Concat("0", str);
			}
			return str;
		}

		public static string formatoDoubleOLB(string valor)
		{
			if (valor.Length < 3)
			{
				valor = string.Concat(valor, "00");
			}
			valor = string.Concat(valor.Substring(0, valor.Length - 2), ",", valor.Substring(valor.Length - 2));
			return Formatos.formatoMonto(valor);
		}

		public static string formatoInstrumento(string valor)
		{
			string str = valor;
			if (valor.Length == 10)
			{
				string str1 = valor.Substring(0, 3);
				string str2 = valor.Substring(3, 6);
				string str3 = valor.Substring(9, 1);
				str = string.Concat(new string[] { str1, "-", str2, "-", str3 });
			}
			return str;
		}

		public static string formatoNumero(string valor)
		{
			string str;
			int num = valor.IndexOf(".");
			if (num <= 0)
			{
				str = string.Concat(valor, ".00");
			}
			else if (num + 1 != valor.Length)
			{
				str = (num != 0 ? valor : "");
			}
			else
			{
				str = string.Concat(valor, "00");
			}
			return str;
		}

		public static string formatoRif(string valor)
		{
			string str = "";
			if (valor != null)
			{
				str = valor.Replace("-", "").Replace(".", "").Trim();
				string str1 = valor.Substring(0, 1);
				string str2 = valor.Substring(1, str.Length - 2);
				string str3 = valor.Substring(str.Length - 1, 1);
				string str4 = string.Format("{0:0#,###,##0}", Convert.ToInt64(str2));
				str = string.Concat(new string[] { str1, "-", str4.Replace(",", "."), "-", str3 });
			}
			return str;
		}

		public static string GetClientIP()
		{
			string item = null;
			item = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (("".Equals(item) ? true : item == null))
			{
				item = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			return item;
		}

		public static TransMaxMin GetCtaDestino(int sCod, short sType)
		{
			TransMaxMin transMaxMin = new TransMaxMin();
			try
			{
				transMaxMin = HelperMenu.getMaxMinTransaccion(sCod, sType);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			return transMaxMin;
		}

		public static string getDateSystem()
		{
			return DateTime.Now.ToString("dd/MM/yyyy");
		}

		public static string getEsIntegerScript()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='javascript' type='text/javascript'>\n");
			stringBuilder.Append("function esInteger(e,valor) {\n");
			stringBuilder.Append("   var charCode\n");
			stringBuilder.Append("   if (navigator.appName == 'Netscape') {\n");
			stringBuilder.Append("      charCode = e.which;\n");
			stringBuilder.Append("   } else { \n");
			stringBuilder.Append("      charCode = e.keyCode;\n");
			stringBuilder.Append("   }\n");
			stringBuilder.Append("   if (charCode == 13) { \n");
			stringBuilder.Append("      return true; \n");
			stringBuilder.Append("   }\n");
			stringBuilder.Append("   //Verificando que solo tenga un punto\n");
			stringBuilder.Append("   var str = valor;\n");
			stringBuilder.Append("   if (charCode==46) {\n");
			stringBuilder.Append("      if (str.indexOf(\".\")>0) {\n");
			stringBuilder.Append("         alert('Introduzca solo numeros');\n");
			stringBuilder.Append("            return false;\n");
			stringBuilder.Append("      }\n");
			stringBuilder.Append("   }\n");
			stringBuilder.Append("   if (charCode > 31 && (charCode < 46 || charCode > 57)) {\n");
			stringBuilder.Append("      alert('Introduzca solo numeros');\n");
			stringBuilder.Append("         return false;\n");
			stringBuilder.Append("   }\n");
			stringBuilder.Append("   if (charCode == 47) {\n");
			stringBuilder.Append("      alert('Introduzca solo numeros');\n");
			stringBuilder.Append("         return false;\n");
			stringBuilder.Append("   }\n");
			stringBuilder.Append("   return true;\n");
			stringBuilder.Append("}\n");
			stringBuilder.Append("</script>\n");
			return stringBuilder.ToString();
		}

		public static string getFormatDate(DateTime dt)
		{
			return dt.ToString("yyyyMMdd");
		}

		public static string getPagesInit()
		{
			return Tools.pagesInit;
		}

		public static string getScriptPrint()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='javascript' type='text/javascript'>\n");
			stringBuilder.Append("     function imprimir() {");
			stringBuilder.Append("          if (document.forms[0].btnPrint) {");
			stringBuilder.Append("               btnPrint.style.visibility='hidden'");
			stringBuilder.Append("          }");
			stringBuilder.Append("          window.print();");
			stringBuilder.Append("          if (document.forms[0].btnPrint) {");
			stringBuilder.Append("               btnPrint.style.visibility='visible'");
			stringBuilder.Append("          }");
			stringBuilder.Append("     }");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		public static string getScriptPrintFrame()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='javascript' type='text/javascript'>\n");
			stringBuilder.Append("     function imprimir2() {");
			stringBuilder.Append("          if (document.forms[0].btnRePrint) {");
			stringBuilder.Append("               btnRePrint.style.visibility='hidden'");
			stringBuilder.Append("          }");
			stringBuilder.Append("          window.parent.frames[1].focus(); window.print();");
			stringBuilder.Append("          if (document.forms[0].btnRePrint) {");
			stringBuilder.Append("               btnRePrint.style.visibility='visible'");
			stringBuilder.Append("          }");
			stringBuilder.Append("     }");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		public static int GetTipoTarjeta(string Tarjeta)
		{
			return (Tarjeta.Substring(0, 4) == "4765" || Tarjeta.Substring(0, 4) == "4108" || Tarjeta.Substring(0, 4) == "4118" ? 1 : 2);
		}

		public static Afiliado GetUserFromSession(Page page)
		{
			Afiliado item = (Afiliado)page.Session["usuario"];
			if (item == null)
			{
				page.Response.Redirect("../redirect.aspx?item=2");
			}
			return item;
		}

		public static string getVal()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='javascript' type='text/javascript'>\n");
			stringBuilder.Append("function valForm(form) {\n");
			stringBuilder.Append("   if (form.debitar.value == \"-1\") {");
			stringBuilder.Append("      alert(\"Seleccione la cuenta a debitar\");");
			stringBuilder.Append("      return false");
			stringBuilder.Append("   }");
			stringBuilder.Append("}");
			stringBuilder.Append("</script>\n");
			return stringBuilder.ToString();
		}

		public static string Inicitipocta(string TipoCta)
		{
			string str;
			str = (TipoCta.Length > 2 ? TipoCta.Substring(0, 3) : TipoCta.Substring(0, 2));
			return str;
		}

		public static bool isNumber(string valor)
		{
			bool flag = false;
			if (valor != null)
			{
				try
				{
					Convert.ToDouble(valor.Replace(".", ","));
					flag = true;
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception.Message);
				}
			}
			return flag;
		}

		public static bool isRightCard(int intTipoTarjeta, string strNumTarjeta)
		{
			bool flag = true;
			if ((intTipoTarjeta != 1 ? false : strNumTarjeta.StartsWith("603216")))
			{
				flag = true;
			}
			else
			{
				flag = (intTipoTarjeta != 1 || !strNumTarjeta.StartsWith("627101") ? false : true);
			}
			if ((intTipoTarjeta != 2 ? false : !strNumTarjeta.StartsWith("603216")))
			{
				flag = false;
			}
			return flag;
		}

		public static string loadDescripcionPantalla(int sCod, short sType)
		{
			string mDDescTrans = "";
			try
			{
				TransMaxMin maxMinTransaccion = HelperMenu.getMaxMinTransaccion(sCod, sType);
				if (maxMinTransaccion != null)
				{
					mDDescTrans = maxMinTransaccion.MD_Desc_Trans;
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			return mDDescTrans;
		}

		public static string OnlyOneComma(string str)
		{
			int num = str.LastIndexOf(",");
			if (num != -1)
			{
				for (int i = str.IndexOf(","); i != num; i = str.IndexOf(","))
				{
					str = str.Remove(i, 1);
					num = str.LastIndexOf(",");
				}
			}
			num = str.LastIndexOf(".");
			if (num != -1)
			{
				for (int j = str.IndexOf("."); j != num; j = str.IndexOf("."))
				{
					str = str.Remove(j, 1);
					num = str.LastIndexOf(".");
				}
			}
			return str;
		}

		public static string[] ordenarFirmas(string uno, string dos, string tres)
		{
			string[] strArrays = new string[3];
			if (uno.CompareTo(dos) > 0)
			{
				if (uno.CompareTo(tres) > 0)
				{
					strArrays[2] = uno;
					if (dos.CompareTo(tres) > 0)
					{
						strArrays[1] = dos;
						strArrays[0] = tres;
					}
					else
					{
						strArrays[0] = dos;
						strArrays[1] = tres;
					}
				}
				else
				{
					strArrays[0] = dos;
					strArrays[1] = uno;
					strArrays[2] = tres;
				}
			}
			else if (dos.CompareTo(tres) > 0)
			{
				strArrays[2] = dos;
				if (uno.CompareTo(tres) > 0)
				{
					strArrays[1] = uno;
					strArrays[0] = tres;
				}
				else
				{
					strArrays[0] = uno;
					strArrays[1] = tres;
				}
			}
			else
			{
				strArrays[0] = uno;
				strArrays[1] = dos;
				strArrays[2] = tres;
			}
			return strArrays;
		}

		public static string parseDescription(string valor)
		{
			string str = valor;
			if (valor != null)
			{
				int num = valor.IndexOf("-");
				if (num > 0)
				{
					str = valor.Substring(0, num);
				}
			}
			return str;
		}

		public static string parseNumero(string valor)
		{
			string str2;
			if ((valor == null ? false : valor.Length >= 3))
			{
				string str = valor.Substring(0, valor.Length - 2);
				string str1 = valor.Substring(valor.Length - 2);
				double num = double.Parse(string.Concat(str, ",", str1));
				str2 = num.ToString();
			}
			else
			{
				str2 = "0.0";
			}
			return str2;
		}

		public static string parseNumero2(string valor)
		{
			string str = "0.0";
			if ((valor == null ? false : valor.Length >= 3))
			{
				string str1 = valor.Substring(0, valor.Length - 2);
				string str2 = valor.Substring(valor.Length - 2);
				str = string.Concat(str1, ",", str2);
			}
			return str;
		}

		public static void PopulateComboCodigoArea(DropDownList ddl, ArrayList al)
		{
			ddl.Items.Clear();
			foreach (string str in al)
			{
				ddl.Items.Add(new ListItem(str, str));
			}
		}

		public static string searchValue(string values, string key)
		{
			string str = "";
			if ((values == null ? false : values.Length > 0))
			{
				int num = 0;
				string[] strArrays = values.Split(new char[] { '|' });
				if (strArrays.Length != 0)
				{
					bool flag = false;
					while (true)
					{
						if ((flag ? true : num >= (int)strArrays.Length))
						{
							break;
						}
						if (strArrays[num].Equals(key))
						{
							str = strArrays[num + 1];
							flag = true;
						}
						else
						{
							num++;
						}
					}
				}
			}
			return str;
		}

		public static bool TestEmailRegex(string sEmailtoValidate)
		{
			Regex regex = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
			Regex regex1 = new Regex("^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$");
			return regex.IsMatch(sEmailtoValidate);
		}

		public static bool validarClaveEspecial(string clave, string tipeada)
		{
			bool flag = false;
			if (clave.Equals(cMD5.ObtenerMd5(tipeada)))
			{
				flag = true;
			}
			return flag;
		}

		public static bool validarMonto(ref string monto, ref string msg, double min)
		{
			bool flag;
			msg = "";
			int num = monto.IndexOf(".");
			if (num >= 0)
			{
				string str = monto.Substring(num + 1);
				if (str.Length > 2)
				{
					msg = "Solo dos decimales";
					monto = string.Concat(monto.Substring(0, num), ".", str.Substring(0, 2));
					flag = false;
					return flag;
				}
				if (str.Length == 0)
				{
					monto = string.Concat(monto.Trim(), "00");
				}
				else if (str.Length == 1)
				{
					monto = string.Concat(monto.Trim(), "0");
				}
			}
			else
			{
				monto = string.Concat(monto.Trim(), ".00");
			}
			string str1 = monto.Replace(".", ",");
			if (str1.Length == 0)
			{
				msg = "Monto inválido";
				flag = false;
			}
			else if (Convert.ToDouble(str1) < min)
			{
				msg = "El monto es menor al mínimo permitido";
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public static bool validarMonto(ref string monto, ref string msg, double min, double max)
		{
			bool flag;
			msg = "";
			int num = monto.IndexOf(".");
			if (num >= 0)
			{
				string str = monto.Substring(num + 1);
				if (str.Length > 2)
				{
					msg = "Solo dos decimales";
					monto = string.Concat(monto.Substring(0, num), ".", str.Substring(0, 2));
					flag = false;
					return flag;
				}
				if (str.Length == 0)
				{
					monto = string.Concat(monto.Trim(), "00");
				}
				else if (str.Length == 1)
				{
					monto = string.Concat(monto.Trim(), "0");
				}
			}
			else
			{
				monto = string.Concat(monto.Trim(), ".00");
			}
			string str1 = monto.Replace(".", ",");
			if (str1.Length != 0)
			{
				double num1 = Convert.ToDouble(str1);
				if (num1 < min)
				{
					msg = string.Concat("El monto es menor al mínimo permitido de Bs. ", min);
					flag = false;
				}
				else if (num1 > max)
				{
					msg = string.Concat("El monto es mayor al máximo permitido de Bs. ", max);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				msg = "Monto inválido";
				flag = false;
			}
			return flag;
		}

		public static bool validarMontoTarjeta(ref string monto, ref string msg, double min, double max)
		{
			bool flag;
			msg = "";
			int num = monto.IndexOf(".");
			if (num >= 0)
			{
				string str = monto.Substring(num + 1);
				if (str.Length > 2)
				{
					msg = "Solo dos decimales";
					monto = string.Concat(monto.Substring(0, num), ".", str.Substring(0, 2));
					flag = false;
					return flag;
				}
				if (str.Length == 0)
				{
					monto = string.Concat(monto.Trim(), "00");
				}
				else if (str.Length == 1)
				{
					monto = string.Concat(monto.Trim(), "0");
				}
			}
			else
			{
				monto = string.Concat(monto.Trim(), ".00");
			}
			string str1 = monto.Replace(".", ",");
			if (str1.Length != 0)
			{
				str1 = Tools.OnlyOneComma(str1);
				double num1 = Convert.ToDouble(str1);
				if (num1 < min)
				{
					msg = string.Concat("El monto es menor al mínimo permitido de Bs. ", min);
					flag = false;
				}
				else if (num1 > max)
				{
					msg = string.Concat("El monto es mayor al máximo permitido de Bs. ", max);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				msg = "Monto inválido";
				flag = false;
			}
			return flag;
		}
	}
}