using IBBAV;
using IBBAV.Entidades;
using IBBAV.WsServicios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperServicios
	{
		private const string CODSISTEMA = "SQLIB";

		public const string TIPO_PAGO_TOTAL = "T";

		public const string TIPO_PAGO_VENCIDO = "V";

		public const string TIPO_PAGO_ACTUAL = "A";

		public const string TIPO_PAGO_DESCONOCIDO = " ";

		public HelperServicios()
		{
		}

		public static Hashtable BDI_CodigoAreaGetBySE_ServicioId(int SE_ServicioId)
		{
			Hashtable hashtables = new Hashtable();
			try
			{
				using (IBBAV.WsServicios.WsServicios wsServicio = new IBBAV.WsServicios.WsServicios())
				{
					DataSet dataSet = wsServicio.BDI_CodigoAreaGetBySE_ServicioId(SE_ServicioId);
					if (dataSet.Tables.Count != 0)
					{
						DataTable item = dataSet.Tables[0];
						if (item.Rows.Count != 0)
						{
							if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
							{
								throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
							}
							foreach (DataRow row in item.Rows)
							{
								hashtables.Add(row[1].ToString(), row[1].ToString());
							}
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return hashtables;
		}

		public static List<Servicios> BDI_ServiciosGet(int SE_ServicioId)
		{
			List<Servicios> servicios = new List<Servicios>();
			try
			{
				using (IBBAV.WsServicios.WsServicios wsServicio = new IBBAV.WsServicios.WsServicios())
				{
					DataSet dataSet = wsServicio.BDI_ServiciosGet(SE_ServicioId);
					if (dataSet.Tables.Count != 0)
					{
						DataTable item = dataSet.Tables[0];
						if (item.Rows.Count != 0)
						{
							if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
							{
								throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
							}
							foreach (DataRow row in item.Rows)
							{
								servicios.Add(Servicios.getNewTiposServicios(row));
							}
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return servicios;
		}
	}
}