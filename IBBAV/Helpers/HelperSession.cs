using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.WsSession;
using System;
using System.Data;
using System.Net;
using System.Web.Services.Protocols;

namespace IBBAV.Helpers
{
	public class HelperSession
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsSession.WsSession ws
		{
			get
			{
				return new IBBAV.WsSession.WsSession();
			}
		}

		public HelperSession()
		{
		}

		public static IBBAV.Entidades.SessionAfiliado SA_CreateSession(long AF_Id)
		{
			IBBAV.Entidades.SessionAfiliado sessionAfiliado = new IBBAV.Entidades.SessionAfiliado();
			try
			{
				DataSet dataSet = HelperSession.ws.SA_CreateSession(AF_Id);
				if (dataSet.Tables.Count > 0)
				{
					DataTable item = dataSet.Tables[0];
					if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
					{
						throw new IBException(int.Parse(item.Rows[0]["NumeroError"].ToString().Trim()), "SQLIB");
					}
					DataRow dataRow = item.Rows[0];
					sessionAfiliado.AF_Id = long.Parse(dataRow["AF_ID"].ToString());
					sessionAfiliado.Sesion = dataRow["sesion"].ToString();
					sessionAfiliado.SES_CodStatus = dataRow["SES_CodStatus"].ToString();
				}
			}
			catch (WebException webException)
			{
				IBException bException = new IBException(webException.Message, "SQLIB");
			}
			catch (SoapException soapException)
			{
				IBException bException1 = new IBException(soapException.Message);
			}
			return sessionAfiliado;
		}

		public static IBBAV.Entidades.SessionAfiliado SA_GetLastSession(long AF_Id)
		{
			IBBAV.Entidades.SessionAfiliado sessionAfiliado = new IBBAV.Entidades.SessionAfiliado();
			try
			{
				IBBAV.WsSession.SessionAfiliado sessionAfiliado1 = HelperSession.ws.SA_GetLastSession(AF_Id);
				if (sessionAfiliado1 != null)
				{
					sessionAfiliado = new IBBAV.Entidades.SessionAfiliado(sessionAfiliado1);
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return sessionAfiliado;
		}

		public static IBBAV.Entidades.SessionAfiliado SA_GetSession(string sesion)
		{
			IBBAV.Entidades.SessionAfiliado sessionAfiliado = null;
			try
			{
				IBBAV.WsSession.SessionAfiliado sessionAfiliado1 = HelperSession.ws.SA_GetSession(sesion);
				if (sessionAfiliado1 != null)
				{
					sessionAfiliado = new IBBAV.Entidades.SessionAfiliado(sessionAfiliado1);
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return sessionAfiliado;
		}

		public static bool SA_UpdateSession(IBBAV.Entidades.SessionAfiliado sa)
		{
			bool flag = false;
			try
			{
				flag = HelperSession.ws.SA_UpdateSession(IBBAV.Entidades.SessionAfiliado.getNewWSSessionAfiliado(sa));
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static string ValidateSession(string sesion)
		{
			string sESCodStatus = "I";
			if (IBBAVConfiguration.ValidarSesion)
			{
				try
				{
					IBBAV.Entidades.SessionAfiliado now = HelperSession.SA_GetSession(sesion);
					if (now != null)
					{
						sESCodStatus = now.SES_CodStatus;
						if (sESCodStatus == "A")
						{
							long num = DateAndTime.DateDiff(DateInterval.Second, now.TiempoInicio, DateTime.Now);
							string[] strArrays = IBBAVConfiguration.SessionTimeOut.Split(new char[] { ':' });
							if (num > long.Parse(strArrays[0]) * (long)60 + long.Parse(strArrays[1]))
							{
								now.SES_CodStatus = "I";
								now.TiempoFin = DateTime.Now;
							}
							else
							{
								now.TiempoInicio = DateTime.Now;
							}
							HelperSession.SA_UpdateSession(now);
						}
					}
				}
				catch (WebException webException)
				{
					throw new IBException(webException.Message, "SQLIB");
				}
			}
			else
			{
				sESCodStatus = "A";
			}
			return sESCodStatus;
		}
	}
}