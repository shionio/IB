using IBBAV;
using IBBAV.Entidades;
using IBBAV.WsMenu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperMenu
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsMenu.WsMenu ws
		{
			get
			{
				return new IBBAV.WsMenu.WsMenu();
			}
		}

		public HelperMenu()
		{
		}

		public static TransMaxMin getMaxMinTransaccion(int sCod, short sType)
		{
			TransMaxMin transMaxMin = new TransMaxMin();
			try
			{
				DataSet maxMinTransaccion = HelperMenu.ws.getMaxMinTransaccion(sCod, sType);
				if (maxMinTransaccion.Tables.Count != 0)
				{
					DataTable item = maxMinTransaccion.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						transMaxMin = TransMaxMin.getNewTransMaxMin(item.Rows[0]);
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return transMaxMin;
		}

		public static List<BAVMenuOpcion> getMenuTest()
		{
			List<BAVMenuOpcion> bAVMenuOpcions = new List<BAVMenuOpcion>();
			DataSet dataSet = new DataSet();
			try
			{
				dataSet = HelperMenu.ws.MenuDinamicoNatural();
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						DataRow[] dataRowArray = dataSet.Tables[0].Select("MD_Cod_Padre = 0", "MD_Pos_Mostrar");
						DataRow[] dataRowArray1 = dataRowArray;
						for (int i = 0; i < (int)dataRowArray1.Length; i++)
						{
							BAVMenuOpcion newOpcion = BAVMenuOpcion.getNewOpcion(dataRowArray1[i]);
							List<BAVMenuOpcion> opciones = newOpcion.Opciones;
							DataTable dataTable = dataSet.Tables[0];
							int codigo = newOpcion.Codigo;
							opciones.AddRange(HelperMenu.recursivo(dataTable, codigo.ToString()));
							bAVMenuOpcions.Add(newOpcion);
						}
						dataSet = null;
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return bAVMenuOpcions;
		}

		private static List<BAVMenuOpcion> recursivo(DataTable dt, string codigo)
		{
			List<BAVMenuOpcion> bAVMenuOpcions = new List<BAVMenuOpcion>();
			DataRow[] dataRowArray = dt.Select(string.Concat("MD_Cod_Padre=", codigo), "MD_Pos_Mostrar");
			for (int i = 0; i < (int)dataRowArray.Length; i++)
			{
				BAVMenuOpcion newOpcion = BAVMenuOpcion.getNewOpcion(dataRowArray[i]);
				List<BAVMenuOpcion> opciones = newOpcion.Opciones;
				int num = newOpcion.Codigo;
				opciones.AddRange(HelperMenu.recursivo(dt, num.ToString()));
				bAVMenuOpcions.Add(newOpcion);
			}
			return bAVMenuOpcions;
		}
	}
}