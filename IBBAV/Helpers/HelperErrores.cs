using IBBAV;
using IBBAV.WsErrores;
using System;
using System.Collections;
using System.Data;
using System.Net;
using System.Web.Services.Protocols;

namespace IBBAV.Helpers
{
	public class HelperErrores
	{
		private const string CODSISTEMA = "SQLIB";

		public HelperErrores()
		{
		}

		public static ErrorIBS ErrorPhoenixGet(int codError, string sistema)
		{
			ErrorIBS errorIB = new ErrorIBS();
			try
			{
				using (IBBAV.WsErrores.WsErrores wsErrore = new IBBAV.WsErrores.WsErrores())
				{
					wsErrore.Timeout = 10000;
					DataSet dataSet = wsErrore.BDI_ErrorGetByCodigo_Nemo(codError, sistema);
					if (dataSet.Tables.Count != 0)
					{
						DataTable item = dataSet.Tables[0];
						if (item.Rows.Count != 0)
						{
							foreach (DataRow row in item.Rows)
							{
								errorIB = ErrorIBS.getNewErrorIBS(row);
							}
						}
						else
						{
							Exception exception = new Exception("No existe error Parametrizado");
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new Exception("Error con la conexión del servicio de errores, verificar el webservice WsErrores");
			}
			catch (Exception exception1)
			{
				throw new Exception("Error con la conexión del servicio de errores, verificar el webservice WsErrores");
			}
			return errorIB;
		}
	}
}