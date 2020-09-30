using Functions;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GReferenciaBancaria : GBase
	{
		private string titular;

		private string literal;

		private string aficedula;

		private string fechainicio;

		private string _base;

		private string tipoCuenta;

		private string sNroCuenta;

		private string dirigido;

		private string referencia;

		private bool isDefaultDirigido;

		public string AfiCedula
		{
			get
			{
				return this.aficedula;
			}
			set
			{
				this.aficedula = value;
			}
		}

		public string Base
		{
			get
			{
				return this._base;
			}
			set
			{
				this._base = value;
			}
		}

		public string Dirigido
		{
			get
			{
				return this.dirigido;
			}
			set
			{
				this.dirigido = value;
			}
		}

		public string FechaInicio
		{
			get
			{
				return this.fechainicio;
			}
			set
			{
				this.fechainicio = value;
			}
		}

		public bool IsDefaultDirigido
		{
			get
			{
				return this.isDefaultDirigido;
			}
			set
			{
				this.isDefaultDirigido = value;
			}
		}

		public string Literal
		{
			get
			{
				return this.literal;
			}
			set
			{
				this.literal = value;
			}
		}

		public string NroCuenta
		{
			get
			{
				return this.sNroCuenta;
			}
			set
			{
				this.sNroCuenta = value;
			}
		}

		public string Referencia
		{
			get
			{
				return this.referencia;
			}
			set
			{
				this.referencia = value;
			}
		}

		public string TipoCuenta
		{
			get
			{
				return this.tipoCuenta;
			}
			set
			{
				this.tipoCuenta = value;
			}
		}

		public string Titular
		{
			get
			{
				return this.titular;
			}
			set
			{
				this.titular = value;
			}
		}

		public GReferenciaBancaria(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			string str = "";
			RespuestaReferbdsjv respuestaReferbdsjv = HelperIbs.ibsReferenBcria(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.NroCuenta);
			if (respuestaReferbdsjv.referbdsjv != null)
			{
				this.aficedula = base.Afiliado.sAF_Rif;
				this.literal = respuestaReferbdsjv.referbdsjv.SLiteral;
				DateTime fecha = Formatos.ISOToFecha(respuestaReferbdsjv.referbdsjv.SFechaInicio);
				this.fechainicio = fecha.ToString("dd/MM/yyyy");
				this._base = respuestaReferbdsjv.referbdsjv.SBase;
				long nAFId = base.Afiliado.nAF_Id;
				string sCONombres = base.Afiliado.sCO_Nombres;
				string str1 = DateTime.Now.ToString("dd/MM/yyyy");
				DateTime now = DateTime.Now;
				string str2 = string.Concat(HelperGlobal.LogRefBankAdd(nAFId, sCONombres, str1, now.ToShortTimeString(), WebUtils.GetClientIP(), "", "", this.NroCuenta, "", this.Dirigido, this.fechainicio, "", "", "", "", ""));
				this.referencia = string.Concat("IBRBC", str2.PadLeft(10, '0'));
				str = this.referencia;
			}
			else
			{
				str = "Fallo referencia";
			}
			try
			{
				HelperEnvioCorreo.BuscarCamposCorreo(base.sCod, base.Afiliado.sCO_Nombres.ToUpper(), base.Afiliado.CO_Email, new decimal(0), "", Formatos.formatoCuenta(this.NroCuenta), this.referencia, base.Afiliado.sCO_Nombres.ToUpper(), "", "", this.NroCuenta, "", "", "", "", "", "", "");
			}
			catch (Exception exception)
			{
				string message = exception.Message;
			}
			return str;
		}
	}
}