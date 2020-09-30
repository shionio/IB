using System;
using System.Web.UI.WebControls;

namespace IBBAV.Functions
{
	public class Calendario
	{
		public const int Domingo = 0;

		public const int Lunes = 1;

		public const int Martes = 2;

		public const int Miercoles = 3;

		public const int Jueves = 4;

		public const int Viernes = 5;

		public const int Sabado = 6;

		private int _anioactual;

		public int anioactual
		{
			get
			{
				return this._anioactual;
			}
			set
			{
				this._anioactual = value;
			}
		}

		public Calendario()
		{
			this._anioactual = DateTime.Now.Year;
		}

		public static bool FillDropDownListDias(DropDownList dDl, int nMesIn, int nYearIn)
		{
			int num = Calendario.MonthDays(nMesIn, nYearIn);
			dDl.Items.Clear();
			dDl.Items.Insert(0, "<--- Select --->");
			for (int i = 1; i <= num; i++)
			{
				dDl.Items.Add(i.ToString());
			}
			return true;
		}

		public static bool FillDropDownListMeses(DropDownList dDl)
		{
			dDl.Items.Add(new ListItem("Enero", "1"));
			dDl.Items.Add(new ListItem("Febrero", "2"));
			dDl.Items.Add(new ListItem("Marzo", "3"));
			dDl.Items.Add(new ListItem("Abril", "4"));
			dDl.Items.Add(new ListItem("Mayo", "5"));
			dDl.Items.Add(new ListItem("Junio", "6"));
			dDl.Items.Add(new ListItem("Julio", "7"));
			dDl.Items.Add(new ListItem("Agosto", "8"));
			dDl.Items.Add(new ListItem("Septiembre", "9"));
			dDl.Items.Add(new ListItem("Octubre", "10"));
			dDl.Items.Add(new ListItem("Noviembre", "11"));
			dDl.Items.Add(new ListItem("Diciembre", "12"));
			return true;
		}

		public static int FirstDayOfMonth(int month, int year)
		{
			int i;
			int num = 1;
			for (i = 1900; i < year; i++)
			{
				num = (num + 365) % 7;
				if (Calendario.IsLeapYear(i))
				{
					num = (num + 1) % 7;
				}
			}
			for (i = 1; i < month; i++)
			{
				num = (num + Calendario.MonthDays(i, year)) % 7;
			}
			return num;
		}

		public static bool IsLeapYear(int year)
		{
			bool flag;
			flag = ((year % 4 != 0 ? true : year % 100 == 0) ? year % 400 == 0 : true);
			return flag;
		}

		public static int MonthDays(int month, int year)
		{
			int num1;
			int num = month;
			switch (num)
			{
				case 2:
				{
					if (!Calendario.IsLeapYear(year))
					{
						num1 = 28;
						break;
					}
					else
					{
						num1 = 29;
						break;
					}
				}
				case 3:
				case 5:
				{
					num1 = 31;
					break;
				}
				case 4:
				case 6:
				{
					num1 = 30;
					break;
				}
				default:
				{
					switch (num)
					{
						case 9:
						case 11:
						{
							num1 = 30;
							break;
						}
						case 10:
						{
							num1 = 31;
							break;
						}
						default:
						{
							num1 = 31;
							break;
						}
					}
					break;
				}
			}
			return num1;
		}

		public static string MonthName(int month)
		{
			string str;
			switch (month)
			{
				case 1:
				{
					str = "Enero";
					break;
				}
				case 2:
				{
					str = "Febrero";
					break;
				}
				case 3:
				{
					str = "Marzo";
					break;
				}
				case 4:
				{
					str = "Abril";
					break;
				}
				case 5:
				{
					str = "Mayo";
					break;
				}
				case 6:
				{
					str = "Junio";
					break;
				}
				case 7:
				{
					str = "Julio";
					break;
				}
				case 8:
				{
					str = "Agosto";
					break;
				}
				case 9:
				{
					str = "Septiembre";
					break;
				}
				case 10:
				{
					str = "Octubre";
					break;
				}
				case 11:
				{
					str = "Noviembre";
					break;
				}
				case 12:
				{
					str = "Diciembre";
					break;
				}
				default:
				{
					str = "Mes Invalido";
					break;
				}
			}
			return str;
		}
	}
}