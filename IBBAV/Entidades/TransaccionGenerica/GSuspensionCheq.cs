using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;
using System.Collections.Generic;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GSuspensionCheq : GBase
	{
		private string SNroCuenta;

		private string sSerialChq;

		private string sSerialDesde;

		private string sMotivoSusp;

		private DateTime dtFechaSusp;

		private List<string> lCheques;

		private string sMonto;

		private long codCliente;

		private string rif;

		private EnumTransaccion tipoTransaccion;

		private string nroChequeInicial;

		private string nroChequeFinal;

		public List<string> Cheques
		{
			get
			{
				return this.lCheques;
			}
			set
			{
				this.lCheques = value;
			}
		}

		public long CodCliente
		{
			get
			{
				return this.codCliente;
			}
			set
			{
				this.codCliente = value;
			}
		}

		public DateTime FechaSusp
		{
			get
			{
				return this.dtFechaSusp;
			}
			set
			{
				this.dtFechaSusp = value;
			}
		}

		public string Monto
		{
			get
			{
				return this.sMonto;
			}
			set
			{
				this.sMonto = value;
			}
		}

		public string MotivoSusp
		{
			get
			{
				return this.sMotivoSusp;
			}
			set
			{
				this.sMotivoSusp = value;
			}
		}

		public string NroChequeFinal
		{
			get
			{
				return this.nroChequeFinal;
			}
			set
			{
				this.nroChequeFinal = value;
			}
		}

		public string NroChequeInicial
		{
			get
			{
				return this.nroChequeInicial;
			}
			set
			{
				this.nroChequeInicial = value;
			}
		}

		public string NroCuenta
		{
			get
			{
				return this.SNroCuenta;
			}
			set
			{
				this.SNroCuenta = value;
			}
		}

		public string Rif
		{
			get
			{
				return this.rif;
			}
			set
			{
				this.rif = value;
			}
		}

		public string SerialChq
		{
			get
			{
				return this.sSerialChq;
			}
			set
			{
				this.sSerialChq = value;
			}
		}

		public string SerialDesde
		{
			get
			{
				return this.sSerialDesde;
			}
			set
			{
				this.sSerialDesde = value;
			}
		}

		public EnumTransaccion TipoTransaccion
		{
			get
			{
				return this.tipoTransaccion;
			}
			set
			{
				this.tipoTransaccion = value;
			}
		}

		public GSuspensionCheq(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			bool flag = false;
			string str = "";
			RespuestaIfcsuspchqdsjv respuestaIfcsuspchqdsjv = new RespuestaIfcsuspchqdsjv();
			if (this.tipoTransaccion == EnumTransaccion.SuspensionChequera)
			{
				respuestaIfcsuspchqdsjv = HelperIbs.ibsSuspCheqrs(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.SNroCuenta, this.SerialChq, this.MotivoSusp, this.FechaSusp, this.NroChequeInicial, this.NroChequeFinal, this.Monto);
				flag = (!string.IsNullOrEmpty(respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores.SVectorCod) ? false : true);
			}
			else if (this.tipoTransaccion == EnumTransaccion.SuspensionCheques)
			{
				for (int i = 0; i < this.Cheques.Count; i++)
				{
					respuestaIfcsuspchqdsjv = HelperIbs.ibsSuspCheqrs(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.SNroCuenta, this.SerialChq, this.MotivoSusp, this.FechaSusp, this.Cheques[i], this.Cheques[i], this.Monto);
					flag = (!string.IsNullOrEmpty(respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores.SVectorCod) ? false : true);
				}
			}
			if (!flag)
			{
				this.LogSuspension("Fallido");
				throw new IBException(respuestaIfcsuspchqdsjv.ifcsuspchqdsjv.EErrores.SVectorCod.Trim());
			}
			str = "Se realizó exitosamente";
			this.LogSuspension("Exitoso");
			return str;
		}

		protected string LogSuspension(string result)
		{
			string empty = string.Empty;
			DataLog dataLog = new DataLog()
			{
				NAF_Id = base.Afiliado.nAF_Id,
				SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
				DtFecha_Trans = DateTime.Now.Date,
				STime_Trans = DateTime.Now.ToLongTimeString(),
				SAF_IP = base.Afiliado.sIP,
				SBanco = string.Empty,
				SCuenta_Origen = this.SNroCuenta,
				SCuenta_Destino = string.Empty,
				SMonto = this.Monto,
				STipo_Tarjeta = string.Empty,
				SBeneficiario = string.Empty,
				SCedula_Id_B = string.Empty,
				SSerial_Chequera = this.SerialChq,
				SCheques = string.Join(",", this.Cheques.ToArray())
			};
			if (dataLog.SCheques.Equals("0000"))
			{
				dataLog.SCod_Trans = "SUCHE";
			}
			else
			{
				dataLog.SCod_Trans = "SUCHQ";
			}
			dataLog.STitular = base.Afiliado.sCO_Nombres;
			dataLog.ICantidad = 0;
			dataLog.SReferencia = string.Empty;
			dataLog.SConcepto = string.Concat("Suspensión de Chequeras / Cheques: ", result);
			dataLog.SMotivo_Suspension = this.MotivoSusp;
			dataLog.SDir_Envio_Chequera = string.Empty;
			try
			{
				HelperGlobal.LogTransAdd(dataLog);
			}
			catch (IBException bException)
			{
				empty = "Error al Grabar en LogTran";
			}
			return empty;
		}
	}
}