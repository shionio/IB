using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.WsAfiliados;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperAfiliado
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsAfiliados.WsAfiliados ws
		{
			get
			{
				return new IBBAV.WsAfiliados.WsAfiliados()
				{
					Timeout = 10000
				};
			}
		}

		public HelperAfiliado()
		{
		}

		public bool ActualizaLoginTransacc(string login)
		{
			bool flag = true;
			try
			{
				flag = HelperAfiliado.ws.AfiliadosActualizaLoginTransaccUPD(login) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool AfiliadosCHKCard(string tarjeta)
		{
			bool flag = false;
			try
			{
				DataSet dataSet = HelperAfiliado.ws.AfiliadosCHKCard(tarjeta);
				if ((dataSet == null ? false : dataSet.Tables.Count != 0))
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						foreach (DataRow row in dataSet.Tables[0].Rows)
						{
							if ((long)row["AF_ID"] != (long)0)
							{
								flag = true;
							}
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static Afiliado AfiliadosGet(long valor, EnumTipoCodigo tipovalor)
		{
			Afiliado newAfiliado = null;
			if (valor > (long)0)
			{
				try
				{
					DataSet dataSet = null;
					dataSet = (tipovalor != EnumTipoCodigo.AF_ID ? HelperAfiliado.ws.SA_AfiliadoGetByAF_CodCliente(valor) : HelperAfiliado.ws.AfiliadosGet(valor));
					if ((dataSet == null ? false : dataSet.Tables.Count != 0))
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
								newAfiliado = Afiliado.getNewAfiliado(row);
							}
						}
					}
				}
				catch (WebException webException)
				{
					throw new IBException(webException.Message, "SQLIB");
				}
			}
			return newAfiliado;
		}

		public static List<Afiliado> AfiliadosGet(string strNumTarjeta, string strCedula)
		{
			List<Afiliado> afiliados = new List<Afiliado>();
			if (!string.IsNullOrEmpty(strNumTarjeta))
			{
				try
				{
					DataSet dataSet = HelperAfiliado.ws.AfiliadosGetByTarjetaCedula(strNumTarjeta, strCedula);
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
								Afiliado newAfiliado = Afiliado.getNewAfiliado(row);
								string sAFRif = newAfiliado.sAF_Rif;
								newAfiliado.tCedRIF = sAFRif.Substring(0, 1);
								newAfiliado.sAF_Rif = sAFRif.ToString();
								afiliados.Add(newAfiliado);
							}
						}
					}
				}
				catch (WebException webException)
				{
					throw new IBException(webException.Message, "SQLIB");
				}
			}
			return afiliados;
		}

        public static List<Afiliado> AfiliadosGet(long valor)
        {
            List<Afiliado> afiliados = new List<Afiliado>();
            if (valor != 0)
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    dataSet = HelperAfiliado.ws.SA_AfiliadoGetByAF_CodCliente(valor);
                   
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
                                Afiliado newAfiliado = Afiliado.getNewAfiliado(row);
                                string sAFRif = newAfiliado.sAF_Rif;
                                newAfiliado.tCedRIF = sAFRif.Substring(0, 1);
                                newAfiliado.sAF_Rif = sAFRif.ToString();
                                afiliados.Add(newAfiliado);
                            }
                        }
                    }
                }
                catch (WebException webException)
                {
                    throw new IBException(webException.Message, "SQLIB");
                }
            }
            return afiliados;
        }

        public static List<Afiliado> AfiliadosGet(string valor, EnumTipoValor tipoValor)
		{
			List<Afiliado> afiliados = new List<Afiliado>();
			if (!string.IsNullOrEmpty(valor))
			{
				try
				{
					DataSet dataSet = new DataSet();
					if (tipoValor == EnumTipoValor.CedulaAfiliado)
					{
						dataSet = HelperAfiliado.ws.AfiliadosGetByCedula(valor);
					}
					if (tipoValor == EnumTipoValor.NombreAfiliado)
					{
						dataSet = HelperAfiliado.ws.AfiliadosGetByNombre(valor);
					}
					if (tipoValor == EnumTipoValor.Session)
					{
						dataSet = HelperAfiliado.ws.SA_AfiliadoGetBySession(valor);
					}
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
								Afiliado newAfiliado = Afiliado.getNewAfiliado(row);
								string sAFRif = newAfiliado.sAF_Rif;
								newAfiliado.tCedRIF = sAFRif.Substring(0, 1);
								newAfiliado.sAF_Rif = sAFRif.ToString();
								afiliados.Add(newAfiliado);
							}
						}
					}
				}
				catch (WebException webException)
				{
					throw new IBException(webException.Message, "SQLIB");
				}
			}
			return afiliados;
		}

		public static Afiliado AfiliadosLogin(string login, string password, string fl_ip)
		{
			Afiliado newAfiliado = null;
			try
			{
				DataSet dataSet = HelperAfiliado.ws.AfiliadosLogin(login, password, fl_ip);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						newAfiliado = Afiliado.getNewAfiliado(item.Rows[0]);
						if (newAfiliado.nFI_Codigo != null)
						{
							newAfiliado.nFI_Codigo = newAfiliado.nFI_Codigo.Trim();
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return newAfiliado;
		}

		public static Afiliado AutenticarAfiliado(string login, string password, string fl_ip)
		{
			Afiliado newAfiliado = null;
			string str = CryptoUtils.EncryptMD5(password);
			try
			{
				DataSet dataSet = HelperAfiliado.ws.AfiliadosLogin(login, str, fl_ip);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						newAfiliado = Afiliado.getNewAfiliado(item.Rows[0]);
						newAfiliado.nFI_Codigo = "E";
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException("1003", "SQLIB");
			}
			return newAfiliado;
		}

		public bool CambioEstatusAfiliado(string sAF_IdListIn)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.CambioEstatus(sAF_IdListIn) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool chkIntentosLogin(string sTarjeta, string sIP, string userName, EnumAccionChkIntentosLogin accion)
		{
			bool flag = false;
			try
			{
				string str = HelperAfiliado.ws.chkErrorAfiliacion(sTarjeta, (sIP != null ? sIP : ""), userName, int.Parse(accion.ToString("d")));
				if (int.Parse(str.Split(new char[] { '|' })[0]) > 0)
				{
					flag = true;
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool chkIntentosTransaccion(string sUserName, string sRespuestaClave, string sIP, string sTipoFalla, string sTransaccion, int iClaveBuena)
		{
			bool flag = false;
			try
			{
				HelperAfiliado.ws.AfiliadoschkIntentosTransaccion(sUserName, sRespuestaClave, sIP, sTipoFalla, sTransaccion, iClaveBuena);
				flag = true;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool chkRespuestaDesafio(string login, EnumRespuestaDesafio accion)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.AfiliadosChkRespDesafio(login, short.Parse(accion.ToString("d"))) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static string CrearClaveTransaccionTemporal(long nAF_Id)
		{
			string str = "";
			try
			{
				str = HelperAfiliado.ws.SMSPasswordUpdate(nAF_Id);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return str;
		}

		public static bool DeFailedLogons(string login)
		{
			bool flag = true;
			try
			{
				flag = HelperAfiliado.ws.EliminarFailedLogons(login) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool EliminarUsuarioSecundario(string sAF_Rif, string sAF_IdListIn)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.DelUsuSecund(sAF_Rif, sAF_IdListIn) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool ExisteAfiliado(string sAF_RifIn, string sAF_CedulaIn, string sAF_NombreUsuarioIn)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.ExisteAfiliado(sAF_RifIn, sAF_CedulaIn, sAF_NombreUsuarioIn);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static Afiliado GetAfiliado(string valor, EnumTipoValor tipoValor)
		{
			Afiliado array = null;
			List<Afiliado> afiliados = new List<Afiliado>();
			if (!string.IsNullOrEmpty(valor))
			{
				afiliados = HelperAfiliado.AfiliadosGet(valor, tipoValor);
			}
			if (afiliados.Count > 0)
			{
				array = afiliados.ToArray()[0];
			}
			return array;
		}

		public static bool GuardarAfiliado(Afiliado afi, Contactos contacto, string sCodMenuListIn, string sCtasListIn, List<KeyValuePair<short, string>> listaPR)
		{
			bool flag = false;
			DataSet dataSet = new DataSet();
			try
			{
				if (afi.nAF_Id == (long)0)
				{
					dataSet = HelperAfiliado.ws.AfilAddUpd(sCodMenuListIn, sCtasListIn, contacto.nCO_Codigo, contacto.sCO_Nombres, contacto.sCO_Apellidos, contacto.sCO_DocId, contacto.nCO_TipoDocId, contacto.sCO_Email, contacto.sCO_Telefono, contacto.sCO_EdificioCasa, contacto.sCO_Calle, contacto.sCO_Urbanizacion, contacto.sCO_CodigoPostal, contacto.sCO_Ciudad, contacto.sCO_Estado, contacto.sCO_Pais, contacto.sCO_Empresa, contacto.nCA_Id, contacto.sCO_Email2, contacto.sCO_Telefono2, contacto.sCO_Celular, contacto.sCO_Rif, contacto.nCO_id, afi.sAF_NombreUsuario, afi.sAF_Password, afi.nTI_Id, afi.nES_Id, afi.nCO_Id, afi.sAF_PasswordTransacciones, afi.sAF_PreguntaDesafio, afi.sAF_RespuestaPD, afi.nST_Id, afi.sTarjeta, afi.sCedula, afi.sClPhoenix, afi.sAF_Rif, afi.nFI_Id, afi.nAF_Tipo, afi.nAF_ApruebaTr, afi.nAF_IniciaTr, afi.sIP, afi.AF_CodCliente, afi.AF_CodOficina, afi.nAF_Id, afi.AF_DiasPassword);
					if (dataSet.Tables.Count != 0)
					{
						DataTable item = dataSet.Tables[0];
						if (item.Rows.Count != 0)
						{
							if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
							{                                
                                throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
							}
							long num = long.Parse(item.Rows[0][0].ToString());
							if (afi.nAF_Id > (long)0)
							{
								afi.nAF_Id = num;
							}
							if (listaPR != null)
							{
								HelperAfiliado.PreguntasAfiliadoAdd(num, listaPR);
							}
						}
					}
				}
				flag = true;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool GuardarAfiliado(Afiliado afi, Contactos contacto, string sCodMenuListIn, string sCtasListIn)
		{
			return HelperAfiliado.GuardarAfiliado(afi, contacto, sCodMenuListIn, sCtasListIn, null);
		}

		public static bool GuardarNuevaClave(long nAF_Id, string nuevaClave)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.AfiliadosUpdCLEsp(nAF_Id, nuevaClave) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool GuardarUsuarioSecundario(Afiliado afi, Contactos contacto)
		{
			bool flag = false;
			try
			{
				afi.nAF_Id = HelperAfiliado.ws.UsuarioSecundarioAddUpd(contacto.sCO_Nombres, contacto.sCO_Apellidos, contacto.sCO_DocId, contacto.nCO_TipoDocId, contacto.sCO_Email, contacto.nCA_Id, contacto.sCO_Email, contacto.sCO_Rif, contacto.nCO_id, afi.sAF_NombreUsuario, afi.sAF_Password, afi.nTI_Id, afi.nES_Id, afi.nCO_Id, afi.sAF_PasswordTransacciones, afi.nST_Id, afi.sAF_Rif, afi.nFI_Id, afi.nAF_Tipo, afi.nAF_ApruebaTr, afi.nAF_IniciaTr, afi.nCO_Id);
				flag = true;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static void PreguntasAfiliadoAdd(long af_id, List<KeyValuePair<short, string>> pr)
		{
			short key = 0;
			short num = 0;
			short key1 = 0;
			short num1 = 0;
			short key2 = 0;
			string value = "";
			string str = "";
			string value1 = "";
			string str1 = "";
			string value2 = "";
			switch (pr.Count)
			{
				case 1:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					break;
				}
				case 2:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					break;
				}
				case 3:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					break;
				}
				case 4:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					num1 = pr[3].Key;
					str1 = pr[3].Value;
					break;
				}
				case 5:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					num1 = pr[3].Key;
					str1 = pr[3].Value;
					key2 = pr[4].Key;
					value2 = pr[4].Value;
					break;
				}
			}
			try
			{
				HelperAfiliado.ws.PreguntasAfiliadoAdd(af_id, key, value, num, str, key1, value1, num1, str1, key2, value2);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
		}

		public bool PreguntasAfiliadoAddRes(long af_id, List<KeyValuePair<short, string>> pr)
		{
			short key = 0;
			short num = 0;
			short key1 = 0;
			short num1 = 0;
			short key2 = 0;
			string value = "";
			string str = "";
			string value1 = "";
			string str1 = "";
			string value2 = "";
			switch (pr.Count)
			{
				case 1:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					break;
				}
				case 2:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					break;
				}
				case 3:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					break;
				}
				case 4:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					num1 = pr[3].Key;
					str1 = pr[3].Value;
					break;
				}
				case 5:
				{
					key = pr[0].Key;
					value = pr[0].Value;
					num = pr[1].Key;
					str = pr[1].Value;
					key1 = pr[2].Key;
					value1 = pr[2].Value;
					num1 = pr[3].Key;
					str1 = pr[3].Value;
					key2 = pr[4].Key;
					value2 = pr[4].Value;
					break;
				}
			}
			try
			{
				HelperAfiliado.ws.PreguntasAfiliadoAdd(af_id, key, value, num, str, key1, value1, num1, str1, key2, value2);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return true;
		}

		public static List<KeyValuePair<short, string>> PreguntasAfiliadoGet(long af_id)
		{
			List<KeyValuePair<short, string>> keyValuePairs = new List<KeyValuePair<short, string>>();
			try
			{
				foreach (DataRow row in HelperAfiliado.ws.PreguntasAfiliadoGet(af_id).Tables[0].Rows)
				{
					keyValuePairs.Add(new KeyValuePair<short, string>(Convert.ToInt16(row["TPS_Id"]), row["TPS_Pregunta"].ToString()));
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return keyValuePairs;
		}

		public static bool PreguntasAfiliadoValidate(long af_id, short id1, short id2, string r1, string r2)
		{
			bool flag = false;
			try
			{
				DataSet dataSet = HelperAfiliado.ws.PreguntasAfiliadoValidate(af_id, id1, id2, r1, r2);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						long num = Convert.ToInt64(item.Rows[0][0]);
						flag = num > (long)0;
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static List<KeyValuePair<short, string>> PreguntasGet(short grupo)
		{
			List<KeyValuePair<short, string>> keyValuePairs = new List<KeyValuePair<short, string>>();
			try
			{
				foreach (DataRow row in HelperAfiliado.ws.PreguntasGet(grupo).Tables[0].Rows)
				{
					keyValuePairs.Add(new KeyValuePair<short, string>(Convert.ToInt16(row["TPS_Id"]), row["TPS_Pregunta"].ToString()));
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return keyValuePairs;
		}

		public static bool RecuperarClave(Afiliado afi)
		{
			bool flag = false;
			try
			{
				long num = HelperAfiliado.ws.AfiliadosAddUpd(afi.sAF_NombreUsuario, afi.sAF_Password, afi.nTI_Id, afi.nES_Id, afi.nCO_Id, afi.sAF_PasswordTransacciones, afi.sAF_PreguntaDesafio, afi.sAF_RespuestaPD, afi.nST_Id, afi.sTarjeta, afi.sCedula, afi.sClPhoenix, afi.sAF_Rif, afi.nFI_Id, afi.nAF_Tipo, afi.nAF_ApruebaTr, afi.nAF_IniciaTr, afi.sIP, afi.nAF_Id, afi.AF_CodCliente, afi.AF_CodOficina);
				flag = num > (long)0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public bool RestaurarClaveAfiliado(long nAF_Id, string clave)
		{
			bool flag = false;
			try
			{
				flag = HelperAfiliado.ws.AfiliadosRestPassw(nAF_Id, clave) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool SMSContactoCelularUpdate(long af_id, string correo, string celular)
		{
			bool flag = true;
			try
			{
				flag = HelperAfiliado.ws.SMSContactoCelularUpdate(af_id, correo, celular) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}
	}
}