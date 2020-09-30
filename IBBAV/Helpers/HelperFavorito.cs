using IBBAV;
using IBBAV.Entidades;
using IBBAV.WsFavoritos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperFavorito
	{
		private const string CODSISTEMA = "SQLIB";

		private static IBBAV.WsFavoritos.WsFavoritos ws
		{
			get
			{
				return new IBBAV.WsFavoritos.WsFavoritos();
			}
		}

		public HelperFavorito()
		{
		}

		public static bool AfiliadoFavoritosAddUpdate(EnumAccionAddUpdateAfiliadoFavoritos accion, AfiliadoFavorito af)
		{
			bool flag = false;
			try
			{
				flag = HelperFavorito.ws.AfiliadoFavoritosAddUpdate(int.Parse(accion.ToString("d")), af.nAF_Id, af.TipoFavoritoId, af.NumeroInstrumento, af.CedulaRif, af.BankId, af.Beneficiario, af.Descripcion, af.TipoTarjetaCredito, af.Email) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static bool AfiliadoFavoritosDelete(long nAF_Id, int tipoServicioId, string numeroInstrumento)
		{
			bool flag = false;
			try
			{
				flag = HelperFavorito.ws.AfiliadoFavoritosDelete(nAF_Id, tipoServicioId, numeroInstrumento) > 0;
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return flag;
		}

		public static AfiliadoFavorito AfiliadoFavoritosGet(long AF_ID, int tipoFavoritoId, string numeroInstrumento)
		{
			AfiliadoFavorito newAfiliadoFavorito = null;
			try
			{
				DataSet dataSet = HelperFavorito.ws.AfiliadoFavoritosGet(AF_ID, tipoFavoritoId, numeroInstrumento);
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
							newAfiliadoFavorito = AfiliadoFavorito.getNewAfiliadoFavorito(row);
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return newAfiliadoFavorito;
		}

		public static DataSet AfiliadoFavoritosGetByAfiliado(long nAF_Id)
		{
			bool flag;
			DataSet dataSet = null;
			try
			{
				dataSet = HelperFavorito.ws.AfiliadoFavoritosGetByAfiliado(nAF_Id);
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

		public static List<AfiliadoFavorito> AfiliadoFavoritosGrupoGetByAfiliado(long nAF_Id, EnumTipoFavorito tipofavorito)
		{
			List<AfiliadoFavorito> afiliadoFavoritos = new List<AfiliadoFavorito>();
			try
			{
				int num = int.Parse(tipofavorito.ToString("d"));
				DataSet dataSet = HelperFavorito.ws.AfiliadoFavoritosGrupoGetByAfiliado(nAF_Id, num);
				if (dataSet.Tables.Count != 0)
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
							afiliadoFavoritos.Add(AfiliadoFavorito.getNewAfiliadoFavorito(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return afiliadoFavoritos;
		}

		public static List<Pregunta> PreguntaIBSByPRE_PreguntaIBSId(int PRE_PreguntaIBSId)
		{
			List<Pregunta> preguntas = new List<Pregunta>();
			try
			{
				DataSet dataSet = HelperFavorito.ws.PreguntaIBSByPRE_PreguntaIBSId(PRE_PreguntaIBSId);
				if (dataSet.Tables.Count != 0)
				{
					DataTable item = dataSet.Tables[0];
					if (item.Rows.Count != 0)
					{
						if ((item.TableName == "SqlException" ? true : item.TableName == "Exception"))
						{
							throw new IBException(item.Rows[0]["NumeroError"].ToString().Trim(), "SQLIB");
						}
						string pROTitulo = "";
						foreach (DataRow row in item.Rows)
						{
							Pregunta newPregunta = Pregunta.getNewPregunta(row);
							if (pROTitulo != newPregunta.PRO_Titulo)
							{
								pROTitulo = newPregunta.PRO_Titulo;
								DataRow[] dataRowArray = item.Select(string.Concat("PRO_Titulo = '", pROTitulo, "'"));
								DataRow[] dataRowArray1 = dataRowArray;
								for (int i = 0; i < (int)dataRowArray1.Length; i++)
								{
									DataRow dataRow = dataRowArray1[i];
									newPregunta.ListaPreguntaCampos.Add(PreguntaCampos.getNewPreguntaCampos(dataRow));
								}
								preguntas.Add(newPregunta);
							}
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return preguntas;
		}

		public static List<TipoFavorito> TipoFavoritoGetOne(int tipoFavoritoId)
		{
			List<TipoFavorito> tipoFavoritos = new List<TipoFavorito>();
			try
			{
				DataSet dataSet = null;
				dataSet = HelperFavorito.ws.TipoFavoritoGetOne(tipoFavoritoId);
				if (dataSet.Tables.Count != 0)
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
							tipoFavoritos.Add(TipoFavorito.getNewTipoFavorito(row));
						}
					}
				}
			}
			catch (WebException webException)
			{
				throw new IBException(webException.Message, "SQLIB");
			}
			return tipoFavoritos;
		}
	}
}