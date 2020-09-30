using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GBloqueoTD : GBase
	{
		private string stjtadbto;

		private string smotivo;

		public string Smotivo
		{
			get
			{
				return this.smotivo;
			}
			set
			{
				this.smotivo = value;
			}
		}

		public string Stjtadbto
		{
			get
			{
				return this.stjtadbto;
			}
			set
			{
				this.stjtadbto = value;
			}
		}

		public GBloqueoTD(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			RespuestaBlotdddsjv respuestaBlotdddsjv = HelperIbs.ibsBloqueoTd(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.Stjtadbto, this.Smotivo);
			if (!string.IsNullOrEmpty(respuestaBlotdddsjv.blotdddsjv.EErrores.SVectorCod))
			{
				throw new IBException(respuestaBlotdddsjv.blotdddsjv.EErrores.SVectorCod, "IBS");
			}
			return null;
		}
	}
}