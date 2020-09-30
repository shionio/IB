using IBBAV;
using IBBAV.Functions;
using IBBAV.WsIbsService;
using IBBAV.WsExtraefectivo;
using System;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;

namespace IBBAV.Helpers
{
	public class HelperIbs
	{
		private const string CODSISTEMA = "IBS";

		private const string CODSISTEMAIB = "SQLIB";

		public HelperIbs()
		{
		}

        //RespuestaAfiljudsjv
		public static RespuestaAfiljudsjv ibsAfiliarJuridico(long SUserId, string SCedRif, string SClaveEnc, DateTime SFechConst)
		{
			AfiljudsjvIn afiljudsjvIn = new AfiljudsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SFechConst = SFechConst.ToString("ddMMyy"),
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaAfiljudsjv respuestaAfiljudsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaAfiljudsjv = ibsServiceService.ibaAfiliarJuridico(afiljudsjvIn);
					if (respuestaAfiljudsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaAfiljudsjv.SError))
					{
						throw new IBException(respuestaAfiljudsjv.SError, "SQLIB");
					}
					if (respuestaAfiljudsjv.afiljudsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaAfiljudsjv.afiljudsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaAfiljudsjv.afiljudsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaAfiljudsjv.afiljudsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaAfiljudsjv;
		}

		public static RespuestaAfilpedsjv ibsAfiliarNatural(long SUserId, string SCedRif, string cta, string SNroTjtaDbto)
		{
			AfilpedsjvIn afilpedsjvIn = new AfilpedsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SCodCtaUlt = cta,
				SNroTjtaDbto = SNroTjtaDbto,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaAfilpedsjv respuestaAfilpedsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaAfilpedsjv = ibsServiceService.ibaAfiliarNatural(afilpedsjvIn);
					if (respuestaAfilpedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaAfilpedsjv.SError))
					{
						throw new IBException(respuestaAfilpedsjv.SError, "SQLIB");
					}
					if (respuestaAfilpedsjv.afilpedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaAfilpedsjv.afilpedsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaAfilpedsjv.afilpedsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaAfilpedsjv.afilpedsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaAfilpedsjv;
		}

		public static RespuestaAvancedsjv ibsAvanceEfectivo(long SUserId, string SCedRif, string SCtaOrgn, string SCtaDest, decimal SMto, string SInstrmto, string SCodSeg, string SFchVcto)
		{
			RespuestaAvancedsjv respuestaAvancedsjv = null;
			AvancedsjvIn avancedsjvIn = new AvancedsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SCtaOrgn = SCtaOrgn,
				SCtaDest = SCtaDest,
				SMto = SMto.ToString("#0.00").Replace(",", "").Replace(".", ""),
				STpOper = "A",
				SInstrmto = SInstrmto,
				SCodSeg = SCodSeg,
				SFchVcto = SFchVcto,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaAvancedsjv = ibsServiceService.ibaAvanceEfectivo(avancedsjvIn);
					if (respuestaAvancedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaAvancedsjv.SError))
					{
						throw new IBException(respuestaAvancedsjv.SError, "SQLIB");
					}
					if (respuestaAvancedsjv.avancedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaAvancedsjv.avancedsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaAvancedsjv.avancedsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaAvancedsjv.avancedsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaAvancedsjv;
		}

		public static RespuestaBlotdddsjv ibsBloqueoTd(long SUserId, string SCedRif, string STjta, string SMotivo)
		{
			BlotdddsjvIn blotdddsjvIn = new BlotdddsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = STjta,
				SMotivBloq = SMotivo,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaBlotdddsjv respuestaBlotdddsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaBlotdddsjv = ibsServiceService.ibaBloqueoTd(blotdddsjvIn);
					if (respuestaBlotdddsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaBlotdddsjv.SError))
					{
						throw new IBException(respuestaBlotdddsjv.SError, "SQLIB");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaBlotdddsjv;
		}

		public static RespuestaCmesdidsjv ibsConMesDisp(long SUserId, string SCedRif, string SNroCta)
		{
			CmesdidsjvIn cmesdidsjvIn = new CmesdidsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta
			};
			RespuestaCmesdidsjv respuestaCmesdidsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 15000;
				try
				{
					respuestaCmesdidsjv = ibsServiceService.ibaConMesDisp(cmesdidsjvIn);
					if (respuestaCmesdidsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaCmesdidsjv.SError))
					{
						throw new IBException(respuestaCmesdidsjv.SError, "SQLIB");
					}
					if (respuestaCmesdidsjv.cmesdidsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaCmesdidsjv.cmesdidsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaCmesdidsjv.cmesdidsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaCmesdidsjv.cmesdidsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaCmesdidsjv;
		}

		public static RespuestaContdcdsjv ibsConSaldoTdc(long SUserId, string SCedRif, string SNroCuenta)
		{
			ContdcdsjvIn contdcdsjvIn = new ContdcdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCuenta,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaContdcdsjv respuestaContdcdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 30000;
				try
				{
					respuestaContdcdsjv = ibsServiceService.ibaConSaldoTdc(contdcdsjvIn);
					if (respuestaContdcdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaContdcdsjv.SError))
					{
						throw new IBException(respuestaContdcdsjv.SError, "SQLIB");
					}
					if (respuestaContdcdsjv.contdcdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaContdcdsjv.contdcdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaContdcdsjv.contdcdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaContdcdsjv.contdcdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaContdcdsjv;
		}

		public static RespuestaChequedsjv ibsConsultaChq(long SUserId, string SCedRif, string SNroCta)
		{
			ChequedsjvIn chequedsjvIn = new ChequedsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaChequedsjv respuestaChequedsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaChequedsjv = ibsServiceService.ibaConsultaChq(chequedsjvIn);
					if (respuestaChequedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaChequedsjv.SError))
					{
						throw new IBException(respuestaChequedsjv.SError, "SQLIB");
					}
					if (respuestaChequedsjv.chequedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaChequedsjv.chequedsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaChequedsjv.chequedsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaChequedsjv.chequedsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaChequedsjv;
		}

		public static RespuestaIbaCons ibsConsultaCtas(string UserId, string CedRif, TipoConsultaCuentasIBS SIndCons)
		{
			IbaConsIn ibaConsIn = new IbaConsIn()
			{
				SUserId = UserId,
				SCedRif = CedRif,
				SIndCons = SIndCons.ToString("d"),
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaIbaCons respuestaIbaCon = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 20000;
				try
				{
					respuestaIbaCon = ibsServiceService.ibaConsolidaCtas(ibaConsIn);
					if (respuestaIbaCon == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaIbaCon.SError))
					{
						throw new IBException(respuestaIbaCon.SError, "SQLIB");
					}
					if (respuestaIbaCon.sdjvCuentas == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaIbaCon.sdjvCuentas.EErrores == null ? false : !string.IsNullOrEmpty(respuestaIbaCon.sdjvCuentas.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaIbaCon.sdjvCuentas.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaIbaCon;
		}

		public static RespuestaStmrdsjv ibsConsultaMovimientos(long SUserId, string SCedRif, string SNroCuenta, ConsultaMovimientoTipoConsulta TipoConsulta, DateTime SFechaDesde, DateTime SFechaHasta, int cantMov)
		{
			StmrdsjvIn stmrdsjvIn = new StmrdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCuenta = SNroCuenta,
				STipoConsulta = TipoConsulta.ToString("d"),
				SFechaDesde = SFechaDesde.ToString("ddMMyy"),
				SFechaHasta = SFechaHasta.ToString("ddMMyy"),
				SChqRefDesde = string.Empty,
				SChqRefHasta = string.Empty,
				SMontoDesde = string.Empty,
				SMontoHasta = string.Empty,
				SCantMov = (TipoConsulta == ConsultaMovimientoTipoConsulta.UltMovimientos ? string.Concat(cantMov) : "0"),
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaStmrdsjv respuestaStmrdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 60000;
				try
				{
					respuestaStmrdsjv = ibsServiceService.ibaConsultaMovmientos(stmrdsjvIn);
					if (respuestaStmrdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaStmrdsjv.SError))
					{
						throw new IBException(respuestaStmrdsjv.SError, "SQLIB");
					}
					if (respuestaStmrdsjv.stmjvCuentas == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaStmrdsjv.stmjvCuentas.EErrores == null ? false : !string.IsNullOrEmpty(respuestaStmrdsjv.stmjvCuentas.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaStmrdsjv.stmjvCuentas.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaStmrdsjv;
		}

		public static RespuestaConsuldsjv ibsConsultaSaldos(long SUserId, string SCedRif, string SNroCuenta)
		{
			ConsuldsjvIn consuldsjvIn = new ConsuldsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCuenta,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaConsuldsjv respuestaConsuldsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaConsuldsjv = ibsServiceService.ibaConsultaSaldos(consuldsjvIn);
					if (respuestaConsuldsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaConsuldsjv.SError))
					{
						throw new IBException(respuestaConsuldsjv.SError, "SQLIB");
					}
					if (respuestaConsuldsjv.consuldsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaConsuldsjv.consuldsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaConsuldsjv.consuldsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaConsuldsjv.consuldsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaConsuldsjv;
		}

		public static RespuestaDetchedsjv ibsDetalleChq(long SUserId, string SCedRif, string SNroCta, string SSerial, string SCheqIni)
		{
			DetchedsjvIn detchedsjvIn = new DetchedsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta,
				SSerial = SSerial,
				SCheqIni = SCheqIni,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaDetchedsjv respuestaDetchedsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaDetchedsjv = ibsServiceService.ibaDetalleChq(detchedsjvIn);
					if (respuestaDetchedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaDetchedsjv.SError))
					{
						throw new IBException(respuestaDetchedsjv.SError, "SQLIB");
					}
					if (respuestaDetchedsjv.detchedsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaDetchedsjv.detchedsjv.EErrores == null ? false : respuestaDetchedsjv.detchedsjv.EErrores.SVectorCod != string.Empty))
					{
						throw new IBException(respuestaDetchedsjv.detchedsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaDetchedsjv;
		}

		public static RespuestaIfcedoctadsjv ibsEstadoCta(long SUserId, string SCedRif, string SNroCta, string SFecha)
		{
			IfcedoctadsjvIn ifcedoctadsjvIn = new IfcedoctadsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta,
				SFecha = SFecha,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaIfcedoctadsjv respuestaIfcedoctadsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 60000;
				try
				{
					respuestaIfcedoctadsjv = ibsServiceService.ibaEstadoCta(ifcedoctadsjvIn);
					if (respuestaIfcedoctadsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaIfcedoctadsjv.SError))
					{
						throw new IBException(respuestaIfcedoctadsjv.SError, "SQLIB");
					}
					if (respuestaIfcedoctadsjv.ifcedoctadsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaIfcedoctadsjv.ifcedoctadsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaIfcedoctadsjv.ifcedoctadsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaIfcedoctadsjv.ifcedoctadsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaIfcedoctadsjv;
		}

		public static RespuestaConfavdsjv ibsFavoritoPreg(long SUserId, string SCedRif)
		{
			ConfavdsjvIn confavdsjvIn = new ConfavdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaConfavdsjv respuestaConfavdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaConfavdsjv = ibsServiceService.ibaFavoritoPreg(confavdsjvIn);
					if (respuestaConfavdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaConfavdsjv.SError))
					{
						throw new IBException(respuestaConfavdsjv.SError, "SQLIB");
					}
					if (respuestaConfavdsjv.confavdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaConfavdsjv.confavdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaConfavdsjv.confavdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaConfavdsjv.confavdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaConfavdsjv;
		}

		public static bool ibsFavoritoResp(long SUserId, string SCedRif, string SCodProd, int SNroPrgta, string SRespParam1, string SRespParam2, string SSubProd, string STipoProd)
		{
			bool flag = true;
			ValfavdsjvIn valfavdsjvIn = new ValfavdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SCodProd = SCodProd,
				SNroPrgta = SNroPrgta.ToString(),
				SRespParam1 = SRespParam1,
				SRespParam2 = SRespParam2,
				SSubProd = SSubProd,
				STipoProd = STipoProd,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaValfavdsjv respuestaValfavdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaValfavdsjv = ibsServiceService.ibaFavoritoResp(valfavdsjvIn);
					if (respuestaValfavdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaValfavdsjv.SError))
					{
						throw new IBException(respuestaValfavdsjv.SError, "SQLIB");
					}
					if (respuestaValfavdsjv.valfavdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaValfavdsjv.valfavdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaValfavdsjv.valfavdsjv.EErrores.SVectorCod)))
					{
						flag = false;
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return flag;
		}

		public static RespuestaReferbdsjv ibsReferenBcria(long SUserId, string SCedRif, string SNroCta)
		{
			ReferbdsjvIn referbdsjvIn = new ReferbdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaReferbdsjv respuestaReferbdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaReferbdsjv = ibsServiceService.ibaReferenBcria(referbdsjvIn);
					if (respuestaReferbdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaReferbdsjv.SError))
					{
						throw new IBException(respuestaReferbdsjv.SError, "SQLIB");
					}
					if (respuestaReferbdsjv.referbdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaReferbdsjv.referbdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaReferbdsjv.referbdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaReferbdsjv.referbdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaReferbdsjv;
		}

		public static RespuestaIfcsuspchqdsjv ibsSuspCheqrs(long SUserId, string SCedRif, string SNroCta, string Sserial, string SmotivSus, DateTime DtfechaSusp, string SCheqDesde, string SCheqHasta, string SMonto)
		{
			IfcsuspchqdsjvIn ifcsuspchqdsjvIn = new IfcsuspchqdsjvIn()
			{
				SPrefijo = string.Empty,
				SFiller = string.Empty,
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SNroCta = SNroCta,
				SSerial = Sserial,
				SMotivoSuspen = SmotivSus,
				SFechaSuspen = DtfechaSusp.ToString("ddMMyy"),
				SCheqFrom = SCheqDesde,
				SCheqDesde = SCheqHasta,
				SMtoCheq = SMonto
			};
			RespuestaIfcsuspchqdsjv respuestaIfcsuspchqdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaIfcsuspchqdsjv = ibsServiceService.ibaSuspCheqrs(ifcsuspchqdsjvIn);
					if (respuestaIfcsuspchqdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaIfcsuspchqdsjv.SError))
					{
						throw new IBException(respuestaIfcsuspchqdsjv.SError, "SQLIB");
					}
					if (respuestaIfcsuspchqdsjv.ifcsuspchqdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaIfcsuspchqdsjv;
		}

		public static RespuestaIntrfdsjv ibsTransfPgoBAV(long SUserId, string SCedRif, string SCtaOrigen, string SCedBenef, string SCtaDest, decimal SMto, int SCodCvcTdc, DateTime SFecVtoTdc, TransferenciaTipoOperacion STipOper, string SInstrumento, TipoServicio sTipoServicio)
		{
			IntrfdsjvIn intrfdsjvIn = new IntrfdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SCtaOrigen = SCtaOrigen,
				SCtaDest = SCtaDest,
				SMto = SMto.ToString("#0.00").Replace(",", "").Replace(".", ""),
				SInstrumento = SInstrumento,
				SCedBenef = SCedBenef,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			string str = "";
			switch (STipOper)
			{
				case TransferenciaTipoOperacion.Transferencia:
				{
					str = "T";
					break;
				}
				case TransferenciaTipoOperacion.TransferenciaTer:
				{
					str = "T";
					break;
				}
				case TransferenciaTipoOperacion.Avance:
				{
					str = "A";
					break;
				}
				case TransferenciaTipoOperacion.Pago:
				{
					str = "P";
					break;
				}
				case TransferenciaTipoOperacion.Servicio:
				{
					str = "C";
					if (sTipoServicio != TipoServicio.NoAplica)
					{
						intrfdsjvIn.SCodServ = sTipoServicio.ToString("d");
						break;
					}
					else
					{
						break;
					}
				}
			}
			intrfdsjvIn.STipOper = str;
			RespuestaIntrfdsjv respuestaIntrfdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 30000;
				try
				{
					respuestaIntrfdsjv = ibsServiceService.ibaTransfPgoBav(intrfdsjvIn);
					if (respuestaIntrfdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaIntrfdsjv.SError))
					{
						throw new IBException(respuestaIntrfdsjv.SError, "SQLIB");
					}
					if (respuestaIntrfdsjv.intrfdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaIntrfdsjv.intrfdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaIntrfdsjv;
		}

		public static RespuestaInextdsjv ibsTransfPgoOtros(long SUserId, string SCedRif, DateTime SFechaValor, decimal SMonto, string SCtaOrigen, string SNomEmisor, string SCtaDest, string SNomBenef, TransferenciaTipoVia SViaPago, string SCedulaEmisor, string SCedulaBenef, string SSubtipo, string SSCodBco)
		{
			InextdsjvIn inextdsjvIn = new InextdsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SFechaValor = SFechaValor.ToString("ddMMyyyy"),
				SMto = SMonto.ToString("#0.00").Replace(",", "").Replace(".", ""),
				SMoneda = "VEB",
				SCtaOrigen = SCtaOrigen.Substring(10, 10)
			};
			string str = Formatos.FormatCadena(SNomEmisor.Trim());
			if (str.Length > 35)
			{
				str = str.Substring(0, 35);
			}
			inextdsjvIn.SNomEmisor = str;
			inextdsjvIn.SCtaDest = SCtaDest;
			str = Formatos.FormatCadena(SNomBenef.Trim());
			if (str.Length > 35)
			{
				str = str.Substring(0, 35);
			}
			inextdsjvIn.SNomBenef = str;
			inextdsjvIn.SCodBco = SCtaOrigen.Substring(0, 4);
			inextdsjvIn.SSubtipo = SSubtipo;
			inextdsjvIn.SCodBco = SSCodBco;
			string str1 = "";
			switch (SViaPago)
			{
				case TransferenciaTipoVia.Switf:
				{
					str1 = "S";
					break;
				}
				case TransferenciaTipoVia.TransferenciaExterna:
				{
					str1 = "O";
					break;
				}
				case TransferenciaTipoVia.TransferenciaExternaTer:
				{
					str1 = "U";
					break;
				}
				case TransferenciaTipoVia.PagoTDC:
				{
					str1 = "P";
					break;
				}
			}
			inextdsjvIn.SViaPago = str1;
			inextdsjvIn.SCedulaEmisor = SCedulaEmisor;
			inextdsjvIn.SCedulaBenef = SCedulaBenef;
			inextdsjvIn.SPrefijo = string.Empty;
			inextdsjvIn.SFiller = string.Empty;
			RespuestaInextdsjv respuestaInextdsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaInextdsjv = ibsServiceService.ibaTransfPgoOtros(inextdsjvIn);
					if (respuestaInextdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaInextdsjv.SError))
					{
						throw new IBException(respuestaInextdsjv.SError, "SQLIB");
					}
					if (respuestaInextdsjv.inextdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaInextdsjv.inextdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaInextdsjv.inextdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaInextdsjv.inextdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaInextdsjv;
		}

		public static RespuestaValctadsjv ibsValCtaBAV(string SNroCta)
		{
			ValctadsjvIn valctadsjvIn = new ValctadsjvIn()
			{
				SNroCta = SNroCta
			};
			RespuestaValctadsjv respuestaValctadsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaValctadsjv = ibsServiceService.ibaValCtaBav(valctadsjvIn);
					if (respuestaValctadsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaValctadsjv.SError))
					{
						throw new IBException(respuestaValctadsjv.SError, "SQLIB");
					}
					if (respuestaValctadsjv.validarctabavdsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaValctadsjv.validarctabavdsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaValctadsjv.validarctabavdsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaValctadsjv.validarctabavdsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaValctadsjv;
		}

		public static RespuestaValusudsjv ibsValidarUsrJdco(long SUserId, string SCedRif, string SCedAut)
		{
			ValusudsjvIn valusudsjvIn = new ValusudsjvIn()
			{
				SUserId = SUserId.ToString(),
				SCedRif = SCedRif,
				SCedAut = SCedAut,
				SPrefijo = string.Empty,
				SFiller = string.Empty
			};
			RespuestaValusudsjv respuestaValusudsjv = null;
			using (IbsServiceService ibsServiceService = new IbsServiceService())
			{
				ibsServiceService.Timeout = 10000;
				try
				{
					respuestaValusudsjv = ibsServiceService.ibaValidarUsrJdco(valusudsjvIn);
					if (respuestaValusudsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if (!string.IsNullOrEmpty(respuestaValusudsjv.SError))
					{
						throw new IBException(respuestaValusudsjv.SError, "SQLIB");
					}
					if (respuestaValusudsjv.valusudsjv == null)
					{
						throw new IBException(9998, "SQLIB");
					}
					if ((respuestaValusudsjv.valusudsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaValusudsjv.valusudsjv.EErrores.SVectorCod)))
					{
						throw new IBException(respuestaValusudsjv.valusudsjv.EErrores.SVectorCod, "IBS");
					}
				}
				catch (WebException webException)
				{
					throw new IBException(9997, "SQLIB");
				}
				catch (SoapException soapException)
				{
					throw new IBException(9997, "SQLIB");
				}
			}
			return respuestaValusudsjv;
		}

        public static Tarjeta[] ConsultaLineaCredito2(string identificacion)
        {
          Tarjeta[] lineaCredito2 = new Tarjeta[0];
            using (ExtraCreditoService extraEfectivo = new ExtraCreditoService())
            {
                try
                {
                    lineaCredito2 = extraEfectivo.tarjetasL2(identificacion);

                    if (extraEfectivo == null)
                    {
                        throw new IBException(9998, "SQLIB");
                    }
                    //if (!string.IsNullOrEmpty(extraEfectivo.SError))
                    //{
                    //    throw new IBException(extraEfectivo.SError, "SQLIB");
                    //}
                    //if (extraEfectivo.valusudsjv == null)
                    //{
                    //    throw new IBException(9998, "SQLIB");
                    //}
                    //if ((extraEfectivo.valusudsjv.EErrores == null ? false : !string.IsNullOrEmpty(respuestaValusudsjv.valusudsjv.EErrores.SVectorCod)))
                    //{
                    //    throw new IBException(extraEfectivo.valusudsjv.EErrores.SVectorCod, "IBS");
                    //}
                }
                catch (WebException webException)
                {
                    throw new IBException(9997, "SQLIB");
                }
                catch (SoapException soapException)
                {
                    throw new IBException(9997, "SQLIB");
                }
            }
            return lineaCredito2;
        }

        public static void writelog(string m, string metodo)
		{
			using (StreamWriter streamWriter = new StreamWriter(string.Concat("\\\\", IBBAVConfiguration.GetHostName, "\\gestionempresarial\\ErrorHelperIBS.txt"), true))
			{
				streamWriter.WriteLine("[{0}] - [{1}]: {2}", DateTime.Now, metodo, m);
				streamWriter.Flush();
			}
		}
	}
}