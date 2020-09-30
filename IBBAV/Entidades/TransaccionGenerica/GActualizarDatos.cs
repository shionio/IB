using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Runtime.CompilerServices;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GActualizarDatos : GBase
	{
		public string Celular
		{
			get;
			set;
		}

		public string Correo
		{
			get;
			set;
		}

		public GActualizarDatos(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			string empty = string.Empty;
			HelperAfiliado.SMSContactoCelularUpdate(base.Afiliado.nAF_Id, this.Correo, this.Celular);
			return empty;
		}
	}
}