using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.Extraefectivo;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GTransferenciasPagos : GBase
	{
		private string _ctaDebitar;

		private string _CedulaBeneficiario;

		private string _ctaAcreditar;

		private decimal _monto;

		private string _concepto;

		private string _email;

		private string _codBanco = string.Empty;

		private string _tipoTarj;

		private string _nroTarjeta;

		private string _beneficiario;

		private int _sCod;

		private string _numBanco;

		private string _numContrato;

		private EnumTipoFavorito _tipoTransaccion;

		private string mensaje = string.Empty;

		private string SCod_Trans;

		private string _fechaVencimiento;

		private string _codigoSeguridad;

		private string _SCodBco;

		private string _instrumento;

		private int rdoSelected;

		private string tiposervicio;

		private string servicioSel;

        //Liliana Guerra 06 Nov 2019 Parametros Extra Efectivo

        private string _montoSolic;

        private string _cantCuotas;

        private string _montoCuota;

        public string montoSolic
        {
            get
            {
                return this._montoSolic;
            }
            set
            {
                this._montoSolic = value;
            }
        }

        public string cantCuotas
        {
            get
            {
                return this._cantCuotas;
            }
            set
            {
                this._cantCuotas = value;
            }
        }

        public string montoCuota
        {
            get
            {
                return this._montoCuota;
            }
            set
            {
                this._montoCuota = value;
            }
        }

        public string Beneficiario
		{
			get
			{
				return this._beneficiario;
			}
			set
			{
				this._beneficiario = value;
			}
		}

		public string CedulaBeneficiario
		{
			get
			{
				return this._CedulaBeneficiario;
			}
			set
			{
				this._CedulaBeneficiario = value;
			}
		}

		public string CodBanco
		{
			get
			{
				return this._codBanco;
			}
			set
			{
				this._codBanco = value;
			}
		}

		public string CodigoSeguridad
		{
			get
			{
				return this._codigoSeguridad;
			}
			set
			{
				this._codigoSeguridad = value;
			}
		}

		public string Concepto
		{
			get
			{
				return this._concepto;
			}
			set
			{
				this._concepto = value;
			}
		}

		public string CtaAcreditar
		{
			get
			{
				return this._ctaAcreditar;
			}
			set
			{
				this._ctaAcreditar = value;
			}
		}

		public string CtaDebitar
		{
			get
			{
				return this._ctaDebitar;
			}
			set
			{
				this._ctaDebitar = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}

		public string FechaVencimiento
		{
			get
			{
				return this._fechaVencimiento;
			}
			set
			{
				this._fechaVencimiento = value;
			}
		}

		public string Instrumento
		{
			get
			{
				return this._instrumento;
			}
			set
			{
				this._instrumento = value;
			}
		}

		public decimal Monto
		{
			get
			{
				return this._monto;
			}
			set
			{
				this._monto = value;
			}
		}

		public string NroTarjeta
		{
			get
			{
				return this._nroTarjeta;
			}
			set
			{
				this._nroTarjeta = value;
			}
		}

		public string NumBanco
		{
			get
			{
				return this._numBanco;
			}
			set
			{
				this._numBanco = value;
			}
		}

		public string numContrato
		{
			get
			{
				return this._numContrato;
			}
			set
			{
				this._numContrato = value;
			}
		}

		public int RdoSelected
		{
			get
			{
				return this.rdoSelected;
			}
			set
			{
				this.rdoSelected = value;
			}
		}

		public new int sCod
		{
			get
			{
				return this._sCod;
			}
			set
			{
				this._sCod = value;
			}
		}

		public string SCodBco
		{
			get
			{
				return this._SCodBco;
			}
			set
			{
				this._SCodBco = value;
			}
		}

		public string ServicioSel
		{
			get
			{
				return this.servicioSel;
			}
			set
			{
				this.servicioSel = value;
			}
		}

		public string Tiposervicio
		{
			get
			{
				return this.tiposervicio;
			}
			set
			{
				this.tiposervicio = value;
			}
		}

		public string TipoTarj
		{
			get
			{
				return this._tipoTarj;
			}
			set
			{
				this._tipoTarj = value;
			}
		}

		public EnumTipoFavorito TipoTransaccion
		{
			get
			{
				return this._tipoTransaccion;
			}
			set
			{
				this._tipoTransaccion = value;
			}
		}

		public GTransferenciasPagos(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			this.sCod = sCod;
		}

        public Boolean EjecutarAccionEETDC()
        {
            this.SCod_Trans = "EETDC";
            //this.LogDiario(0);
            RespuestaIntrfdsjv respuestaIntrfdsjv3 = HelperIbs.ibsTransfPgoBAV(01, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, this.Monto, 0, DateTime.Now, 0, string.Empty, 0);

            this.mensaje = string.Concat("IB", " OK");
            //this.LogTransferenciasPagos();

            return true;
        }
        
        public override string EjecutarAccion()
		{
			DateTime now;
			decimal monto = this.Monto;
			EnumTipoFavorito tipoTransaccion = this.TipoTransaccion;
            // Liliana Guerra 08 Nov 2019 EXTRA EFECTIVO
            if (tipoTransaccion == EnumTipoFavorito.ExtraEfectivoTDC)
            {
                this.SCod_Trans = "EETDC";
                //this.LogDiario(0);
                RespuestaIntrfdsjv respuestaIntrfdsjv3 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Transferencia, string.Empty, TipoServicio.NoAplica);
                //LineaExtracredito solicitud = HelperExtracredito.solicAprobacion(CedulaBeneficiario, CtaAcreditar, CtaDebitar, montoSolic, cantCuotas, montoCuota);
                
                this.mensaje = string.Concat("IB", " OK");
                this.LogTransferenciasPagos();
                
            }

            else if (tipoTransaccion <= EnumTipoFavorito.TransferenciaOtrosBancosTerceros)
			{
				switch (tipoTransaccion)
				{
					case EnumTipoFavorito.PagoServicioElectricidadCaracas:
					{
						this.Instrumento = this.numContrato;
						this.SCod_Trans = "PAEDC";
						if (this.RdoSelected == 1)
						{
							this.PagoServicios(monto, TipoServicio.EDC1);
							break;
						}
						else if (this.RdoSelected == 2)
						{
							this.PagoServicios(monto, TipoServicio.EDC2);
							break;
						}
						else if (this.RdoSelected == 3)
						{
							this.PagoServicios(monto, TipoServicio.EDC3);
							break;
						}
						else
						{
							break;
						}
					}
					case EnumTipoFavorito.PagoServicioCANTV:
					{
						this.Instrumento = this.numContrato;
						this.SCod_Trans = "PAGSE";
						this.PagoServicios(monto, TipoServicio.CANTV);
						break;
					}
					case EnumTipoFavorito.PagoServicioElectricidadCaracas | EnumTipoFavorito.PagoServicioCANTV:
					{
						break;
					}
					case EnumTipoFavorito.PagoServicioCANTVNET:
					{
						this.Instrumento = this.numContrato;
						this.SCod_Trans = "PACNN";
						this.PagoServicios(monto, TipoServicio.CantvNET);
						break;
					}
					case EnumTipoFavorito.PagoServicioMovilnet:
					{
						this.Instrumento = this.numContrato;
						this.SCod_Trans = "PAMOV";
						this.PagoServicios(monto, TipoServicio.Movilnet);
						break;
					}
					default:
					{
						EnumTipoFavorito enumTipoFavorito = tipoTransaccion;
                        if (enumTipoFavorito == EnumTipoFavorito.TransferenciaMismoTitularBAV)
						{
							this.SCod_Trans = "TBAMT";
							this.LogDiario(0);
							RespuestaIntrfdsjv respuestaIntrfdsjv3 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Transferencia, string.Empty, TipoServicio.NoAplica);
							if ((respuestaIntrfdsjv3.intrfdsjv.EErrores.SVectorCod == string.Empty ? false : !respuestaIntrfdsjv3.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
							{
								throw new IBException(respuestaIntrfdsjv3.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
							}
							this.mensaje = respuestaIntrfdsjv3.intrfdsjv.SReferencia;
							if (!string.IsNullOrEmpty(this.mensaje))
							{
								this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
								this.LogTransferenciasPagos();
							}
							else
							{
								goto Label0;
							}
						}
						else if (enumTipoFavorito == EnumTipoFavorito.TransferenciaTercerosBAV)
						{
							this.SCod_Trans = "TBTER";
							this.LogDiario(0);
							RespuestaIntrfdsjv respuestaIntrfdsjv4 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, this.CedulaBeneficiario, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.TransferenciaTer, string.Empty, TipoServicio.NoAplica);
							if ((respuestaIntrfdsjv4.intrfdsjv.EErrores.SVectorCod == string.Empty ? false : !respuestaIntrfdsjv4.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
							{
								throw new IBException(respuestaIntrfdsjv4.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
							}
							this.mensaje = respuestaIntrfdsjv4.intrfdsjv.SReferencia;
							if (!string.IsNullOrEmpty(this.mensaje))
							{
								this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
								this.LogTransferenciasPagos();
								string cOCelular3 = base.Afiliado.CO_Celular;
								object[] objArray2 = new object[] { "BAV informa Transferencia por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la cuenta ", this.CtaAcreditar.Substring(10, 10), " en fecha ", null, null };
								now = DateTime.Now;
								objArray2[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
								objArray2[8] = ". Si la desconoce llame al 0500-288.00.01";
								HelperTedexis.sendSMS(cOCelular3, string.Concat(objArray2));
							}
							else
							{
								goto Label0;
							}
						}
						else
						{
							EnumTipoFavorito enumTipoFavorito1 = tipoTransaccion;
							if (enumTipoFavorito1 == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular)
							{
								this.SCod_Trans = "TBOMT";
								this.LogDiario(0);
								RespuestaInextdsjv respuestaInextdsjv2 = HelperIbs.ibsTransfPgoOtros(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, DateTime.Now, monto, this.CtaDebitar, base.Afiliado.sCO_Nombres.Trim(), this.CtaAcreditar, this.Beneficiario.Trim(), TransferenciaTipoVia.TransferenciaExterna, base.Afiliado.sAF_Rif, this.CedulaBeneficiario, "220", this.SCodBco);
								if ((respuestaInextdsjv2.inextdsjv.EErrores.SVectorCod == string.Empty ? false : !respuestaInextdsjv2.inextdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
								{
									throw new IBException(respuestaInextdsjv2.inextdsjv.EErrores.SVectorCod.Trim(), "IBSX");
								}
								this.mensaje = respuestaInextdsjv2.inextdsjv.SReferencia;
								if (!string.IsNullOrEmpty(this.mensaje))
								{
									this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
									this.LogTransferenciasPagos();
									string str3 = base.Afiliado.CO_Celular;
									object[] objArray3 = new object[] { "BAV informa Transferencia por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la cuenta ", this.CtaAcreditar.Substring(10, 10), " en fecha ", null, null };
									now = DateTime.Now;
									objArray3[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
									objArray3[8] = ". Si la desconoce llame al 0500-288.00.01";
									HelperTedexis.sendSMS(str3, string.Concat(objArray3));
								}
								else
								{
									goto Label0;
								}
							}
							else if (enumTipoFavorito1 == EnumTipoFavorito.TransferenciaOtrosBancosTerceros)
							{
								this.SCod_Trans = "TBOTE";
								this.LogDiario(0);
								RespuestaInextdsjv respuestaInextdsjv3 = HelperIbs.ibsTransfPgoOtros(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, DateTime.Now, monto, this.CtaDebitar, base.Afiliado.sCO_Nombres, this.CtaAcreditar, this.Beneficiario, TransferenciaTipoVia.TransferenciaExternaTer, base.Afiliado.sAF_Rif, this.CedulaBeneficiario, "220", this.SCodBco);
								if ((respuestaInextdsjv3.inextdsjv.EErrores.SVectorCod == string.Empty ? false : !respuestaInextdsjv3.inextdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
								{
									throw new IBException(respuestaInextdsjv3.inextdsjv.EErrores.SVectorCod.Trim(), "IBSX");
								}
								this.mensaje = respuestaInextdsjv3.inextdsjv.SReferencia;
								if (!string.IsNullOrEmpty(this.mensaje))
								{
									this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
									this.LogTransferenciasPagos();
									string cOCelular4 = base.Afiliado.CO_Celular;
									object[] str4 = new object[] { "BAV informa Transferencia por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la cuenta ", this.CtaAcreditar.Substring(10, 10), " en fecha ", null, null };
									now = DateTime.Now;
									str4[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
									str4[8] = ". Si la desconoce llame al 0500-288.00.01";
									HelperTedexis.sendSMS(cOCelular4, string.Concat(str4));
								}
								else
								{
									goto Label0;
								}
							}
						}
						break;
					}
				}
			}
			else
			{
				switch (tipoTransaccion)
				{
					case EnumTipoFavorito.AdelantodeEfectivoTDC:
					{
						this.SCod_Trans = "TAVEF";
						this.LogDiario(1);
						RespuestaIntrfdsjv respuestaIntrfdsjv = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Avance, string.Empty, TipoServicio.NoAplica);
						if ((respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod == null ? false : !respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
						{
							throw new IBException(respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
						}
						this.mensaje = respuestaIntrfdsjv.intrfdsjv.SReferencia;
						if (!string.IsNullOrEmpty(this.mensaje))
						{
							this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
							this.LogTransferenciasPagos();
							break;
						}
						else
						{
							break;
						}
					}
					case EnumTipoFavorito.PagoTarjetaCreditoMismoTitularBAV:
					{
						this.SCod_Trans = "PTBMT";
						this.LogDiario(0);
						RespuestaIntrfdsjv respuestaIntrfdsjv1 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Pago, this.Instrumento, TipoServicio.NoAplica);
						if ((respuestaIntrfdsjv1.intrfdsjv.EErrores.SVectorCod == string.Empty ? false : !respuestaIntrfdsjv1.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
						{
							throw new IBException(respuestaIntrfdsjv1.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
						}
						this.mensaje = respuestaIntrfdsjv1.intrfdsjv.SReferencia;
						if (!string.IsNullOrEmpty(this.mensaje))
						{
							this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
							this.CtaAcreditar = this.Instrumento;
							this.LogTransferenciasPagos();
							break;
						}
						else
						{
							break;
						}
					}
					case EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV:
					{
						this.SCod_Trans = "PTBTE";
						this.LogDiario(0);
						RespuestaIntrfdsjv respuestaIntrfdsjv2 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, this.CedulaBeneficiario, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Pago, this.Instrumento, TipoServicio.NoAplica);
						if ((respuestaIntrfdsjv2.intrfdsjv.EErrores.SVectorCod == null ? false : !respuestaIntrfdsjv2.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
						{
							throw new IBException(respuestaIntrfdsjv2.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
						}
						this.mensaje = respuestaIntrfdsjv2.intrfdsjv.SReferencia;
						if (!string.IsNullOrEmpty(this.mensaje))
						{
							this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
							this.CtaAcreditar = this.Instrumento;
							this.LogTransferenciasPagos();
							string cOCelular = base.Afiliado.CO_Celular;
							object[] str = new object[] { "BAV informa PagoTDC por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la tarjeta ", this.CtaAcreditar.Substring(8, 8), " en fecha ", null, null };
							now = DateTime.Now;
							str[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
							str[8] = ". Si la desconoce llame al 0500-288.00.01";
							HelperTedexis.sendSMS(cOCelular, string.Concat(str));
							break;
						}
						else
						{
							break;
						}
					}
					default:
					{
						if (tipoTransaccion == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular)
						{
							string str1 = string.Concat(this.SCodBco, this.CtaAcreditar);
							if (this.CtaAcreditar.Length < 16)
							{
								str1 = string.Concat(this.SCodBco, this.CtaAcreditar.Substring(0, this.CtaAcreditar.Length - 9).PadLeft(7, '0'), this.CtaAcreditar.Substring(this.CtaAcreditar.Length - 9, 9));
							}
							this.SCod_Trans = "PTOMT";
							this.LogDiario(0);
							RespuestaInextdsjv respuestaInextdsjv = HelperIbs.ibsTransfPgoOtros(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, DateTime.Now, monto, this.CtaDebitar, base.Afiliado.sCO_Nombres.Trim(), str1, this.Beneficiario.Trim(), TransferenciaTipoVia.PagoTDC, base.Afiliado.sAF_Rif, this.CedulaBeneficiario, "225", this.SCodBco);
							if ((respuestaInextdsjv.inextdsjv.EErrores.SVectorCod == null ? false : !respuestaInextdsjv.inextdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
							{
								throw new IBException(respuestaInextdsjv.inextdsjv.EErrores.SVectorCod.Trim(), "IBSX");
							}
							this.mensaje = respuestaInextdsjv.inextdsjv.SReferencia;
							if (!string.IsNullOrEmpty(this.mensaje))
							{
								this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
								this.LogTransferenciasPagos();
								string cOCelular1 = base.Afiliado.CO_Celular;
								object[] objArray = new object[] { "BAV informa PagoTDC por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la tarjeta ", str1.Substring(8, 8), " en fecha ", null, null };
								now = DateTime.Now;
								objArray[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
								objArray[8] = ". Si la desconoce llame al 0500-288.00.01";
								HelperTedexis.sendSMS(cOCelular1, string.Concat(objArray));
								break;
							}
							else
							{
								break;
							}
						}
						else if (tipoTransaccion != EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros)
						{
							break;
						}
						else
						{
							this.SCod_Trans = "PTOTE";
							this.LogDiario(0);
							string str2 = string.Concat(this.SCodBco, this.CtaAcreditar);
							if (this.CtaAcreditar.Length < 16)
							{
								str2 = string.Concat(this.SCodBco, this.CtaAcreditar.Substring(0, this.CtaAcreditar.Length - 9).PadLeft(7, '0'), this.CtaAcreditar.Substring(this.CtaAcreditar.Length - 9, 9));
							}
							RespuestaInextdsjv respuestaInextdsjv1 = HelperIbs.ibsTransfPgoOtros(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, DateTime.Now, monto, this.CtaDebitar, base.Afiliado.sCO_Nombres, str2, this.Beneficiario, TransferenciaTipoVia.PagoTDC, base.Afiliado.sAF_Rif, this.CedulaBeneficiario, "225", this.SCodBco);
							if ((respuestaInextdsjv1.inextdsjv.EErrores.SVectorCod == null ? false : !respuestaInextdsjv1.inextdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
							{
								throw new IBException(respuestaInextdsjv1.inextdsjv.EErrores.SVectorCod.Trim(), "IBSX");
							}
							this.mensaje = respuestaInextdsjv1.inextdsjv.SReferencia;
							if (!string.IsNullOrEmpty(this.mensaje))
							{
								this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
								this.LogTransferenciasPagos();
								string cOCelular2 = base.Afiliado.CO_Celular;
								object[] objArray1 = new object[] { "BAV informa PagoTDC por Bs. ", monto, " de su cuenta ", this.CtaDebitar.Substring(10, 10), " a la tarjeta ", str2.Substring(8, 8), " en fecha ", null, null };
								now = DateTime.Now;
								objArray1[7] = now.ToString("dd/MM/yyyy hh:mm:ss");
								objArray1[8] = ". Si la desconoce llame al 0500-288.00.01";
								HelperTedexis.sendSMS(cOCelular2, string.Concat(objArray1));
								break;
							}
							else
							{
								break;
							}
						}
					}
				}
			}
		Label0:
			HelperTransaccion.AcumuladorTransVerifyUpdate(false, base.Afiliado.nAF_Id, this.Monto, this.sCod, this.SCod_Trans);
			HelperEnvioCorreo.BuscarCamposCorreo(this.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, this.Monto, (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), this.mensaje, (string.IsNullOrEmpty(this.Beneficiario) ? string.Empty : this.Beneficiario), (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), (string.IsNullOrEmpty(this.TipoTarj) ? string.Empty : this.TipoTarj), (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar), string.Empty, string.Empty, (string.IsNullOrEmpty(this.Concepto) ? string.Empty : this.Concepto), (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar), (string.IsNullOrEmpty(base.Afiliado.sCO_Nombres) ? string.Empty : base.Afiliado.sCO_Nombres), (string.IsNullOrEmpty(this.NumBanco) ? string.Empty : this.NumBanco), (string.IsNullOrEmpty(this.Email) ? string.Empty : this.Email));
			return this.mensaje;
		}

		protected string LogDiario(int iTipo)
		{
			string empty = string.Empty;
			try
			{
				string str = "";
				if (iTipo == 0)
				{
					str = (string.IsNullOrEmpty(this.Instrumento) ? this.CtaAcreditar : this.Instrumento);
				}
				else
				{
					str = (string.IsNullOrEmpty(this.Instrumento) ? string.Empty : this.Instrumento);
				}
				HelperGlobal.LogTransDiarioAdd(base.Afiliado.nAF_Id, DateTime.Now.Date, DateTime.Now.ToLongTimeString(), (string.IsNullOrEmpty(this.SCod_Trans) ? string.Empty : this.SCod_Trans), (string.IsNullOrEmpty(this.mensaje) ? string.Empty : this.mensaje), (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar), str, Formatos.formatoMonto(this.Monto));
			}
			catch (IBException bException)
			{
				empty = "Error al Grabar en LogDiario";
			}
			return empty;
		}

		protected string LogPagosServicios()
		{
			string empty = string.Empty;
			DataLog dataLog = new DataLog()
			{
				NAF_Id = base.Afiliado.nAF_Id,
				SAF_NombreUsuario = (string.IsNullOrEmpty(base.Afiliado.sAF_NombreUsuario) ? string.Empty : base.Afiliado.sAF_NombreUsuario),
				DtFecha_Trans = DateTime.Now.Date,
				STime_Trans = DateTime.Now.ToLongTimeString(),
				SCod_Trans = (string.IsNullOrEmpty(this.SCod_Trans) ? string.Empty : this.SCod_Trans),
				SAF_IP = (string.IsNullOrEmpty(base.Afiliado.sIP) ? string.Empty : base.Afiliado.sIP),
				SBanco = "BAV",
				SCuenta_Origen = (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar),
				SCuenta_Destino = (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar),
				SMonto = Formatos.formatoMonto(this.Monto),
				STipo_Tarjeta = (string.IsNullOrEmpty(this.TipoTarj) ? string.Empty : this.TipoTarj),
				SBeneficiario = this.TipoTransaccion.ToString(),
				SCedula_Id_B = (string.IsNullOrEmpty(this.Instrumento) ? string.Empty : this.Instrumento),
				SSerial_Chequera = string.Empty,
				SCheques = string.Empty,
				STitular = (string.IsNullOrEmpty(base.Afiliado.sCO_Nombres) ? string.Empty : base.Afiliado.sCO_Nombres),
				ICantidad = 0,
				SReferencia = (string.IsNullOrEmpty(this.mensaje) ? string.Empty : this.mensaje),
				SConcepto = (string.IsNullOrEmpty(this.Concepto) ? string.Empty : this.Concepto),
				SMotivo_Suspension = string.Empty,
				SDir_Envio_Chequera = string.Empty
			};
			try
			{
				HelperGlobal.LogTransAdd(dataLog);
			}
			catch (IBException bException)
			{
				empty = "Error al Grabar en LogTran";
			}
			catch (Exception exception)
			{
				empty = "Error al Grabar en LogTran";
			}
			return empty;
		}

		protected string LogTransferenciasPagos()
		{
			string empty = string.Empty;
			DataLog dataLog = new DataLog()
			{
				NAF_Id = base.Afiliado.nAF_Id,
				SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
				DtFecha_Trans = DateTime.Now.Date,
				STime_Trans = DateTime.Now.ToLongTimeString(),
				SCod_Trans = this.SCod_Trans,
				SAF_IP = base.Afiliado.sIP,
				SBanco = (string.IsNullOrEmpty(this.NumBanco) ? string.Empty : this.NumBanco),
				SCuenta_Origen = this.CtaDebitar,
				SCuenta_Destino = this.CtaAcreditar,
				SMonto = Formatos.formatoMonto(this.Monto),
				STipo_Tarjeta = (string.IsNullOrEmpty(this.TipoTarj) ? string.Empty : this.TipoTarj),
				SBeneficiario = (string.IsNullOrEmpty(this.Beneficiario) ? string.Empty : this.Beneficiario),
				SCedula_Id_B = (string.IsNullOrEmpty(this.CedulaBeneficiario) ? string.Empty : this.CedulaBeneficiario),
				SSerial_Chequera = string.Empty,
				SCheques = string.Empty,
				STitular = base.Afiliado.sCO_Nombres,
				ICantidad = 0,
				SReferencia = this.mensaje,
				SConcepto = this.Concepto,
				SMotivo_Suspension = string.Empty,
				SDir_Envio_Chequera = string.Empty
			};
			try
			{
				HelperGlobal.LogTransAdd(dataLog);
			}
			catch (IBException bException)
			{
				empty = "Error al Grabar en LogTran";
			}
			catch (Exception exception)
			{
				empty = "Error al Grabar en LogTran";
			}
			return empty;
		}

		protected void PagoServicios(decimal montoIbs, TipoServicio sTipoServicio)
		{
			this.LogDiario(1);
			RespuestaIntrfdsjv respuestaIntrfdsjv = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, montoIbs, 0, DateTime.Now, TransferenciaTipoOperacion.Servicio, this.Instrumento, sTipoServicio);
			if ((respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod == null ? false : !respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod.Trim().Equals(string.Empty)))
			{
				throw new IBException(respuestaIntrfdsjv.intrfdsjv.EErrores.SVectorCod.Trim(), "IBSX");
			}
			this.mensaje = respuestaIntrfdsjv.intrfdsjv.SReferencia;
			if (!string.IsNullOrEmpty(this.mensaje))
			{
				this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
				if (sTipoServicio == TipoServicio.CANTV)
				{
					this.SCod_Trans = "PAGSE";
				}
				else if (sTipoServicio == TipoServicio.Movilnet)
				{
					this.SCod_Trans = "PAMOV";
				}
				else if (sTipoServicio == TipoServicio.CantvNET)
				{
					this.SCod_Trans = "PACNN";
				}
				else if (sTipoServicio == TipoServicio.EDC)
				{
					this.SCod_Trans = "PAEDC";
				}
				this.LogPagosServicios();
			}
		}

		private enum Sms
		{
			NATURAL,
			JURIDICO
		}
	}
}