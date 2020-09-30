using IBBAV.Entidades;
using System;
using System.Data;

namespace IBBAV
{
	public class ErrorIBS : EntidadBase
	{
		private long _EIB_ErrorId;

		private int _EIB_Codigo;

		private string _EIB_MensajeSistema;

		private string _EIB_MensajeCliente;

		private string _SIB_Nemotecnico;

		public int EIB_Codigo
		{
			get
			{
				return this._EIB_Codigo;
			}
			set
			{
				this._EIB_Codigo = value;
			}
		}

		public long EIB_ErrorId
		{
			get
			{
				return this._EIB_ErrorId;
			}
			set
			{
				this._EIB_ErrorId = value;
			}
		}

		public string EIB_MensajeCliente
		{
			get
			{
				return this._EIB_MensajeCliente;
			}
			set
			{
				this._EIB_MensajeCliente = value;
			}
		}

		public string EIB_MensajeSistema
		{
			get
			{
				return this._EIB_MensajeSistema;
			}
			set
			{
				this._EIB_MensajeSistema = value;
			}
		}

		public string SIB_Nemotecnico
		{
			get
			{
				return this._SIB_Nemotecnico;
			}
			set
			{
				this._SIB_Nemotecnico = value;
			}
		}

		public ErrorIBS()
		{
		}

		public static ErrorIBS getNewErrorIBS(DataRow dr)
		{
			return new ErrorIBS()
			{
				EIB_ErrorId = EntidadBase.IsLong(dr, "EIB_ErrorId"),
				EIB_Codigo = EntidadBase.IsInt(dr, "EIB_Codigo"),
				EIB_MensajeSistema = EntidadBase.IsString(dr, "EIB_MensajeSistema"),
				EIB_MensajeCliente = EntidadBase.IsString(dr, "EIB_MensajeCliente"),
				SIB_Nemotecnico = EntidadBase.IsString(dr, "SIB_Nemotecnico")
			};
		}
	}
}