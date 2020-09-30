using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GAvanceEfectivo : GBase
	{
		private string cuentaOrigen;

		private string cuentaDestino;

		private decimal monto;

		private string tipoTarjeta;

		private string tarjeta;

		private string fechaVencimiento;

		private short codigoSeguridad;

		private string mensaje;

		private string numBanco;

		private string beneficiario;

		private string cedulaBeneficiario;

		public string Beneficiario
		{
			get
			{
				return this.beneficiario;
			}
			set
			{
				this.beneficiario = value;
			}
		}

		public string CedulaBeneficiario
		{
			get
			{
				return this.cedulaBeneficiario;
			}
			set
			{
				this.cedulaBeneficiario = value;
			}
		}

		public short CodigoSeguridad
		{
			get
			{
				return this.codigoSeguridad;
			}
			set
			{
				this.codigoSeguridad = value;
			}
		}

		public string CuentaDestino
		{
			get
			{
				return this.cuentaDestino;
			}
			set
			{
				this.cuentaDestino = value;
			}
		}

		public string CuentaOrigen
		{
			get
			{
				return this.cuentaOrigen;
			}
			set
			{
				this.cuentaOrigen = value;
			}
		}

		public string FechaVencimiento
		{
			get
			{
				return this.fechaVencimiento;
			}
			set
			{
				this.fechaVencimiento = value;
			}
		}

		public string Mensaje
		{
			get
			{
				return this.mensaje;
			}
			set
			{
				this.mensaje = value;
			}
		}

		public decimal Monto
		{
			get
			{
				return this.monto;
			}
			set
			{
				this.monto = value;
			}
		}

		public string NumBanco
		{
			get
			{
				return this.numBanco;
			}
			set
			{
				this.numBanco = value;
			}
		}

		public string Tarjeta
		{
			get
			{
				return this.tarjeta;
			}
			set
			{
				this.tarjeta = value;
			}
		}

		public string TipoTarjeta
		{
			get
			{
				return this.tipoTarjeta;
			}
			set
			{
				this.tipoTarjeta = value;
			}
		}

		public GAvanceEfectivo(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			string empty = string.Empty;
			long aFCodCliente = base.Afiliado.AF_CodCliente;
			string sAFRif = base.Afiliado.sAF_Rif;
			string cuentaOrigen = this.CuentaOrigen;
			string cuentaDestino = this.CuentaDestino;
			decimal monto = this.Monto;
			string tarjeta = this.Tarjeta;
			short codigoSeguridad = this.CodigoSeguridad;
			RespuestaAvancedsjv respuestaAvancedsjv = HelperIbs.ibsAvanceEfectivo(aFCodCliente, sAFRif, cuentaOrigen, cuentaDestino, monto, tarjeta, codigoSeguridad.ToString(), this.FechaVencimiento);
			if (!string.IsNullOrEmpty(respuestaAvancedsjv.avancedsjv.EErrores.SVectorCod))
			{
				throw new IBException(respuestaAvancedsjv.avancedsjv.EErrores.SVectorCod);
			}
			empty = respuestaAvancedsjv.avancedsjv.SReferencia;
			if (!string.IsNullOrEmpty(empty))
			{
				this.LogAvance();
			}
			empty = string.Concat("IB", empty.Trim().PadLeft(10, '0'));
			HelperEnvioCorreo.BuscarCamposCorreo(base.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, this.Monto, this.Tarjeta, "", empty, "", this.CuentaDestino, this.TipoTarjeta, "", "", "", "", "", "", "", "");
			return empty;
		}

		protected string LogAvance()
		{
			string empty = string.Empty;
			DataLog dataLog = new DataLog()
			{
				NAF_Id = base.Afiliado.nAF_Id,
				SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
				DtFecha_Trans = DateTime.Now.Date,
				STime_Trans = DateTime.Now.ToLongTimeString(),
				SCod_Trans = "TAVEF",
				SAF_IP = (string.IsNullOrEmpty(base.Afiliado.sIP) ? string.Empty : base.Afiliado.sIP),
				SBanco = (string.IsNullOrEmpty(this.NumBanco) ? string.Empty : this.NumBanco),
				SCuenta_Origen = (string.IsNullOrEmpty(this.Tarjeta) ? string.Empty : this.Tarjeta),
				SCuenta_Destino = (string.IsNullOrEmpty(this.CuentaDestino) ? string.Empty : this.CuentaDestino),
				SMonto = this.Monto.ToString(),
				STipo_Tarjeta = (string.IsNullOrEmpty(this.TipoTarjeta) ? string.Empty : this.TipoTarjeta),
				SBeneficiario = (string.IsNullOrEmpty(this.Beneficiario) ? string.Empty : this.Beneficiario),
				SCedula_Id_B = (string.IsNullOrEmpty(this.CedulaBeneficiario) ? string.Empty : this.CedulaBeneficiario),
				SSerial_Chequera = string.Empty,
				SCheques = (string.IsNullOrEmpty(this.Tarjeta) ? string.Empty : this.Tarjeta),
				STitular = (string.IsNullOrEmpty(base.Afiliado.sCO_Nombres) ? string.Empty : base.Afiliado.sCO_Nombres),
				ICantidad = 0,
				SReferencia = (string.IsNullOrEmpty(this.Mensaje) ? string.Empty : this.Mensaje),
				SConcepto = string.Empty,
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
	}
}