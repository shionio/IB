using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.WsGlobal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperGlobal
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsGlobal.WsGlobal ws
		{
			get
			{
				return new IBBAV.WsGlobal.WsGlobal();
			}
		}

		public HelperGlobal()
		{
		}

		public static List<Agencia> AgenciaGet()
		{
			List<Agencia> agencias = new List<Agencia>();
			try
			{
				DataSet dataSet = HelperGlobal.ws.AgenciasGet();
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
							agencias.Add(Agencia.getNewAgencia(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return agencias;
		}

		public static Agencia AgenciaGet(long id)
		{
			Agencia agencium = new Agencia();
			try
			{
				DataSet dataSet = HelperGlobal.ws.AgenciaGetById(id);
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
							agencium = Agencia.getNewAgencia(row);
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return agencium;
		}

		public static List<Banco> BancosGet(bool blnOtrosBancos)
		{
			List<Banco> bancos = new List<Banco>();
			try
			{
				DataSet dataSet = HelperGlobal.ws.BankGet(blnOtrosBancos);
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
							bancos.Add(Banco.getNewBanco(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return bancos;
		}

		public static List<Banco> BankGetByType(bool conBAV, string tipoString)
		{
			List<Banco> bancos = new List<Banco>();
			try
			{
				DataSet dataSet = HelperGlobal.ws.BankGetByType(conBAV, tipoString);
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
							bancos.Add(Banco.getNewBanco(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return bancos;
		}

		public static string BankNameGet(string id)
		{
			string bANKNombre = "";
			try
			{
				DataSet dataSet = HelperGlobal.ws.BankGetById(id);
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
							bANKNombre = Banco.getNewBanco(row).BANK_Nombre;
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return bANKNombre;
		}

		public static List<DatosCorreo> BDI_DatosCorreoGetByDC_CodTrans(string codtrans)
		{
			List<DatosCorreo> datosCorreos = new List<DatosCorreo>();
			try
			{
				DataSet dataSet = HelperGlobal.ws.BDI_DatosCorreoGetByDC_CodTrans(codtrans);
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
							datosCorreos.Add(DatosCorreo.getNewDatosCorreo(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return datosCorreos;
		}

		public bool ChkBineCard(string bine, string banco, string statu)
		{
			bool flag1;
			string str = bine;
			string str1 = banco;
			string str2 = statu;
			bool flag = false;
			try
			{
				DataSet dataSet = HelperGlobal.ws.ChkBineCard(str, str1, str2);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count == 0)
					{
						flag1 = false;
					}
					else
					{
						flag1 = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
					}
					if (flag1)
					{
						throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static DataSet DiasPasswordGet()
		{
			return HelperGlobal.ws.DiasPasswordget();
		}

		public static int GetNumeroOperDiarias(long nAF_Id, string sCuenta_Origen, string sCuenta_Destino, string sMonto)
		{
			int numeroOperDiarias = 0;
			try
			{
				numeroOperDiarias = HelperGlobal.ws.GetNumeroOperDiarias(nAF_Id, sCuenta_Origen, sCuenta_Destino, sMonto);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			return numeroOperDiarias;
		}

		public static string GetNumeroRefeferencia()
		{
			string str = "";
			try
			{
				long numeroReferencia = HelperGlobal.ws.GetNumeroReferencia();
				if (numeroReferencia > (long)0)
				{
					str = numeroReferencia.ToString().PadLeft(6, '0');
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			return str;
		}

		public static string GetNumeroReferenciaCrediCard()
		{
			string str = "";
			try
			{
				long numeroReferenciaCrediCard = HelperGlobal.ws.GetNumeroReferenciaCrediCard();
				if (numeroReferenciaCrediCard > (long)0)
				{
					str = (numeroReferenciaCrediCard <= (long)6 ? numeroReferenciaCrediCard.ToString().PadLeft(6, '0') : numeroReferenciaCrediCard.ToString().PadLeft(6, '0').Substring(numeroReferenciaCrediCard.ToString().Length - 6));
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			return str;
		}

		public DataSet getTipoTarjetas(string statu)
		{
			bool flag;
			string str = statu;
			DataSet dataSet = new DataSet();
			try
			{
				dataSet = HelperGlobal.ws.GetTipoTarjetas(str);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count == 0)
					{
						flag = false;
					}
					else
					{
						flag = (item.TableName == "SqlException" ? true : item.TableName == "Exception");
					}
					if (flag)
					{
						throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return dataSet;
		}

		public static long LogRefBankAdd(long nAF_Id, string sAF_NombreUsuario, string Fecha_Trans, string sTime_Trans, string sAF_IP, string sCod_Trans, string sTipoCuenta_Origen, string sCuenta_Origen, string sMonto, string sDirigidoA, string sFechaAperturaCta, string sSaldoPromediom, string sMesesRef, string sCifras, string sTipoRef, string sStatus)
		{
			long num = (long)0;
			try
			{
				num = HelperGlobal.ws.LogRefBankAdd(nAF_Id, sAF_NombreUsuario, Fecha_Trans, sTime_Trans, sAF_IP, sCod_Trans, sTipoCuenta_Origen, sCuenta_Origen, sMonto, sDirigidoA, sFechaAperturaCta, sSaldoPromediom, sMesesRef, sCifras, sTipoRef, sStatus);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return num;
		}

		public static bool LogRegUsAdd(long nAF_Id, string sAF_NombreUsuario, DateTime Fecha_Trans, string sTime_Trans, string sAF_IP, string sCod_Trans, string sReferencia, string sConcepto)
		{
			bool flag = false;
			try
			{
				flag = HelperGlobal.ws.LogRegUsAdd(nAF_Id, sAF_NombreUsuario, Fecha_Trans, sTime_Trans, sAF_IP, sCod_Trans, sReferencia, sConcepto) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool LogTransAdd(long nAF_Id, string sAF_NombreUsuario, DateTime Fecha_Trans, string sTime_Trans, string sAF_IP, string sCod_Trans, string sReferencia, string sCuenta_Origen, string sCuenta_Destino, string sMonto, string sBanco, string sTipo_Tarjeta, string sBeneficiario, string sCedula_Id_B, string sSerial_Chequera, string sCheques, string sTitular, int iCantidad, string sConcepto, string sMotivo_Suspension, string sDir_Envio_Chequera)
		{
			int num = 0;
			try
			{
				num = HelperGlobal.ws.LogTransAdd(nAF_Id, sAF_NombreUsuario, Fecha_Trans, sTime_Trans, sAF_IP, sCod_Trans, sReferencia.Trim(), sCuenta_Origen, sCuenta_Destino, sMonto, sBanco, sTipo_Tarjeta, sBeneficiario, sCedula_Id_B, sSerial_Chequera, sCheques, sTitular, iCantidad, sConcepto, sMotivo_Suspension, sDir_Envio_Chequera);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return num > 0;
		}

		public static bool LogTransAdd(DataLog log)
		{
			bool flag = HelperGlobal.LogTransAdd(log.NAF_Id, log.SAF_NombreUsuario, log.DtFecha_Trans, log.STime_Trans, WebUtils.GetClientIP(), log.SCod_Trans, log.SReferencia, log.SCuenta_Origen, log.SCuenta_Destino, log.SMonto, log.SBanco, log.STipo_Tarjeta, log.SBeneficiario, log.SCedula_Id_B, log.SSerial_Chequera, log.SCheques, log.STitular, log.ICantidad, log.SConcepto, log.SMotivo_Suspension, log.SDir_Envio_Chequera);
			return flag;
		}

		public static bool LogTransDiarioAdd(long nAF_Id, DateTime Fecha_Trans, string sTime_Trans, string sCod_Trans, string sReferencia, string sCuenta_Origen, string sCuenta_Destino, string sMonto)
		{
			int num = 0;
			try
			{
				num = HelperGlobal.ws.LogTransDiarioAdd(nAF_Id, Fecha_Trans, sTime_Trans, sCod_Trans, sReferencia.Trim(), sCuenta_Origen, sCuenta_Destino, sMonto);
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return num > 0;
		}

		public static void SaveSendMsg(string msg, string destiny, Origen origen, string codigoRetorno, string descripcionRetorno)
		{
			HelperGlobal.ws.SaveSendMsg(msg, destiny, origen, codigoRetorno, descripcionRetorno);
		}
	}
}