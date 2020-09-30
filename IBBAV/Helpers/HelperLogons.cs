using IBBAV;
using IBBAV.Functions;
using IBBAV.WsLogons;
using System;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperLogons
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsLogons.WsLogons ws
		{
			get
			{
				return new IBBAV.WsLogons.WsLogons();
			}
		}

		public HelperLogons()
		{
		}

		public static bool ActivarUsuario(long AF_Id)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, string.Empty, string.Empty, string.Empty, "RECCLV", 1);
			return flag;
		}

		public static bool ActualizarClaveLogin(long AF_Id, string AF_PasswordOld, string AF_PasswordNew, int AF_DiasPassword, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.PasswordUpdate(AF_Id, AF_PasswordOld, AF_PasswordNew, AF_DiasPassword, FL_IP, "CLVLOGIN", sCod);
			return flag;
		}

		public static bool ActualizarClaveTransacciones(long AF_Id, string AF_PasswordOld, string AF_PasswordNew, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.PasswordUpdate(AF_Id, AF_PasswordOld, AF_PasswordNew, 0, FL_IP, "CLVTRAN", sCod);
			return flag;
		}

		public static bool FallidoIntentoAfiliacion(string tarjeta, string FL_IP)
		{
			bool flag = HelperLogons.IntentosValidate((long)0, tarjeta, string.Empty, FL_IP, "AFILUSU", 0);
			return flag;
		}

		private static bool IntentosValidate(long AF_Id, string AF_Password, string AF_RespuestaPD, string FL_IP, string TI_Nemotecnico, int sCod)
		{
			bool flag = false;
			try
			{
				DataSet dataSet = HelperLogons.ws.IntentosValidate(AF_Id, AF_Password, AF_RespuestaPD, FL_IP, TI_Nemotecnico, sCod);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						flag = true;
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		private static bool PasswordUpdate(long AF_Id, string AF_PasswordOld, string AF_PasswordNew, int AF_DiasPassword, string FL_IP, string TI_Nemotecnico, int sCod)
		{
			bool flag = false;
			try
			{
				string aFPasswordOld = AF_PasswordOld;
				string str = CryptoUtils.EncryptMD5(AF_PasswordNew);
				DataSet dataSet = HelperLogons.ws.PasswordUpdate(AF_Id, aFPasswordOld, str, AF_DiasPassword, FL_IP, TI_Nemotecnico, sCod);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						flag = true;
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static string ValidarAfiliacionIntentos(string tarjeta, string clave, string tipo)
		{
			string empty = string.Empty;
			string str = CryptoUtils.EncryptMD5(clave);
			try
			{
				empty = HelperLogons.ws.ValidarAfiliacionIntentos(tarjeta, str, tipo);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return empty;
		}

		public static bool ValidarClaveLogin(long AF_Id, string AF_Password, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, CryptoUtils.EncryptMD5(AF_Password), string.Empty, FL_IP, "CLVLOGIN", sCod);
			return flag;
		}

		public static bool ValidarClaveTransacciones(long AF_Id, string AF_Password, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, CryptoUtils.EncryptMD5(AF_Password), string.Empty, FL_IP, "CLVTRAN", sCod);
			return flag;
		}

		public static bool ValidarIntentoAfiliacion(string tarjeta, string FL_IP)
		{
			bool flag = HelperLogons.IntentosValidate((long)0, tarjeta, string.Empty, FL_IP, "VALUSU", 0);
			return flag;
		}

		public static bool ValidarIntentoAfiliacion(string tarjeta, string clvesp, string FL_IP)
		{
			bool flag = HelperLogons.IntentosValidate((long)0, tarjeta, clvesp, FL_IP, "VALUSUESP", 0);
			return flag;
		}

		public static bool ValidarIntentoRecuparecionCLV(long AF_Id, string tarjeta, string FL_IP)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, tarjeta, string.Empty, FL_IP, "RECCLV", 0);
			return flag;
		}

		public static bool ValidarIntentoRecuparecionCLVFALLIDO(long AF_Id, string tarjeta, string FL_IP)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, tarjeta, "1", FL_IP, "RECCLV", 0);
			return flag;
		}

		public static bool ValidarRespuestaDesafio(long AF_Id, string AF_RespuestaPD, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, string.Empty, CryptoUtils.EncryptMD5(AF_RespuestaPD), FL_IP, "RESPDESA", sCod);
			return flag;
		}

		public static bool ValidarRespuestaDesafioFavorito(long AF_Id, bool fallido, bool eliminarintento, string FL_IP, int sCod)
		{
			bool flag = HelperLogons.IntentosValidate(AF_Id, (fallido ? "1" : "0"), (eliminarintento ? "1" : "0"), FL_IP, "FAVORITO", sCod);
			return flag;
		}
	}
}