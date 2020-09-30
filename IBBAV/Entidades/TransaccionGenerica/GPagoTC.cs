using IBBAV.Entidades;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GPagoTC : GBase
	{
		private string tarjeta;

		private string monto;

		private string cuentadebito;

		public string Cuentadebito
		{
			get
			{
				return this.cuentadebito;
			}
			set
			{
				this.cuentadebito = value;
			}
		}

		public string Monto
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

		public GPagoTC(IBBAV.Entidades.Afiliado afi, int sCod)
		{
			base.Afiliado = afi;
			base.sCod = sCod;
		}

		public override string EjecutarAccion()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}