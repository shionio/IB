using IBBAV.WsErrores;
using System;

namespace IBBAV.Entidades
{
	[Serializable]
	public class SessionAfiliado : EntidadBase
	{
		private long aF_Id;

		private string sesion;

		private DateTime tiempoInicio;

		private DateTime tiempoFin;

		private string sES_CodStatus;

		public long AF_Id
		{
			get
			{
				return this.aF_Id;
			}
			set
			{
				this.aF_Id = value;
			}
		}

		public string SES_CodStatus
		{
			get
			{
				return this.sES_CodStatus;
			}
			set
			{
				this.sES_CodStatus = value;
			}
		}

		public string Sesion
		{
			get
			{
				return this.sesion;
			}
			set
			{
				this.sesion = value;
			}
		}

		public DateTime TiempoFin
		{
			get
			{
				return this.tiempoFin;
			}
			set
			{
				this.tiempoFin = value;
			}
		}

		public DateTime TiempoInicio
		{
			get
			{
				return this.tiempoInicio;
			}
			set
			{
				this.tiempoInicio = value;
			}
		}

		public SessionAfiliado() : this((long)0, string.Empty, DateTime.Now, DateTime.Now, string.Empty)
		{
		}

		public SessionAfiliado(long aF_Id, string sesion, DateTime tiempoInicio, DateTime tiempoFin, string sES_CodStatus)
		{
			this.aF_Id = aF_Id;
			this.sesion = sesion;
			this.tiempoInicio = tiempoInicio;
			this.tiempoFin = tiempoFin;
			this.sES_CodStatus = sES_CodStatus;
		}

		public SessionAfiliado(IBBAV.WsSession.SessionAfiliado wssa)
		{
			if (wssa != null)
			{
				this.AF_Id = wssa.AF_Id;
				this.sesion = wssa.Sesion;
				this.tiempoInicio = wssa.TiempoInicio;
				this.tiempoFin = wssa.TiempoFin;
				this.sES_CodStatus = wssa.SES_CodStatus;
			}
		}

		public static IBBAV.WsSession.SessionAfiliado getNewWSSessionAfiliado(IBBAV.Entidades.SessionAfiliado sa)
		{
			return new IBBAV.WsSession.SessionAfiliado()
			{
				AF_Id = sa.AF_Id,
				Sesion = sa.Sesion,
				TiempoInicio = sa.TiempoInicio,
				TiempoFin = sa.TiempoFin,
				SES_CodStatus = sa.SES_CodStatus
			};
		}
	}
}