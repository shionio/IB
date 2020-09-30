using System;
using System.Runtime.CompilerServices;

namespace IBBAV.pages.Consultas.Cuentas
{
	public class MovimientosR
	{
		public string SChqRef
		{
			get;
			set;
		}

		public string SDesctrans
		{
			get;
			set;
		}

		public DateTime SFechaEfect
		{
			get;
			set;
		}

		public DateTime SFechaProc
		{
			get;
			set;
		}

		public string SIndDebCre
		{
			get;
			set;
		}

		public decimal SMonto
		{
			get;
			set;
		}

		public MovimientosR()
		{
		}
	}
}