using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GAfiliacionFavorito : GBase
	{
		private IBBAV.Entidades.AfiliadoFavorito _AfiliadoFavorito;

		private EnumAccionAddUpdateAfiliadoFavoritos _Accion;

		public EnumAccionAddUpdateAfiliadoFavoritos Accion
		{
			get
			{
				return this._Accion;
			}
			set
			{
				this._Accion = value;
			}
		}

		public IBBAV.Entidades.AfiliadoFavorito AfiliadoFavorito
		{
			get
			{
				return this._AfiliadoFavorito;
			}
			set
			{
				this._AfiliadoFavorito = value;
				IBBAV.Entidades.AfiliadoFavorito afiliadoFavorito = this._AfiliadoFavorito;
				int tipoFavoritoId = this._AfiliadoFavorito.TipoFavoritoId;
				afiliadoFavorito.Key = CryptoUtils.EncryptMD5(tipoFavoritoId.ToString());
			}
		}

		public GAfiliacionFavorito(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			string empty = string.Empty;
			if (!HelperFavorito.AfiliadoFavoritosAddUpdate(this.Accion, this.AfiliadoFavorito))
			{
				if (this.AfiliadoFavorito.IsServicio)
				{
					throw new IBException("Ya tienes Afiliado este Número de Teléfono / Contrato");
				}
				throw new IBException("Ya tienes Afiliado este Número de Cuenta / Tarjeta");
			}
			this.LogFavorito(this.AfiliadoFavorito.TipoDescripcion, this.AfiliadoFavorito.NumeroInstrumento, this.AfiliadoFavorito.BankId, this.AfiliadoFavorito.Beneficiario, this.AfiliadoFavorito.CedulaRif);
			HelperEnvioCorreo.BuscarCamposCorreo(base.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, new decimal(0), string.Empty, string.Empty, string.Empty, this.AfiliadoFavorito.Beneficiario, this.AfiliadoFavorito.NumeroInstrumento, this.AfiliadoFavorito.TipoTarjetaCredito, this.AfiliadoFavorito.NumeroInstrumento, "", "", this.AfiliadoFavorito.TipoDescripcion, "", "", this.AfiliadoFavorito.BankId, "");
			return empty;
		}

		private bool LogFavorito(string tipoaf, string numins, string bc, string benef, string ced)
		{
			bool flag = false;
			try
			{
				DataLog dataLog = new DataLog()
				{
					NAF_Id = base.Afiliado.nAF_Id,
					SAF_NombreUsuario = (string.IsNullOrEmpty(base.Afiliado.sAF_NombreUsuario) ? string.Empty : base.Afiliado.sAF_NombreUsuario),
					DtFecha_Trans = DateTime.Now.Date,
					STime_Trans = DateTime.Now.ToLongTimeString()
				};
				if (this._Accion == EnumAccionAddUpdateAfiliadoFavoritos.Insert)
				{
					dataLog.SCod_Trans = "AFBCA";
					dataLog.SConcepto = string.Concat("Afiliación de Favorito: Tipo Favorito: ", tipoaf, " Instrumento ", numins);
				}
				else
				{
					dataLog.SCod_Trans = "AFBCM";
					dataLog.SConcepto = string.Concat("Modificación de Favorito: Tipo Favorito: ", tipoaf, " Instrumento ", numins);
				}
				dataLog.SAF_IP = (string.IsNullOrEmpty(base.Afiliado.sIP) ? string.Empty : base.Afiliado.sIP);
				dataLog.SBanco = (string.IsNullOrEmpty(bc) ? string.Empty : bc);
				dataLog.SCuenta_Origen = string.Empty;
				dataLog.SCuenta_Destino = string.Empty;
				dataLog.SMonto = string.Empty;
				dataLog.STipo_Tarjeta = string.Empty;
				dataLog.SBeneficiario = (string.IsNullOrEmpty(benef) ? string.Empty : benef);
				dataLog.SCedula_Id_B = (string.IsNullOrEmpty(ced) ? string.Empty : ced);
				dataLog.SSerial_Chequera = string.Empty;
				dataLog.SCheques = string.Empty;
				dataLog.STitular = (string.IsNullOrEmpty(base.Afiliado.sCO_Nombres) ? string.Empty : base.Afiliado.sCO_Nombres);
				dataLog.ICantidad = 0;
				dataLog.SReferencia = string.Empty;
				dataLog.SMotivo_Suspension = string.Empty;
				dataLog.SDir_Envio_Chequera = string.Empty;
				flag = HelperGlobal.LogTransAdd(dataLog);
			}
			catch (IBException bException)
			{
			}
			return flag;
		}
	}
}