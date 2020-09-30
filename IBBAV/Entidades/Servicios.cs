using IBBAV.Functions;
using System;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class Servicios
	{
		private int _SE_ServicioId;

		private string _SE_Nombre;

		private string _SE_Activo;

		private string _SE_OlbId;

		private string _SE_CodTransaccion;

		private string _SE_CodBanco;

		private int _SE_HoraInicio;

		private int _SE_HoraFin;

		private string _SE_Grupo;

		private string _SE_CtaAdministrativa;

		private string _SE_RIF;

		private string _SE_Nemotecnico;

		private string key;

		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		public string SE_Activo
		{
			get
			{
				return this._SE_Activo;
			}
			set
			{
				this._SE_Activo = value;
			}
		}

		public string SE_CodBanco
		{
			get
			{
				return this._SE_CodBanco;
			}
			set
			{
				this._SE_CodBanco = value;
			}
		}

		public string SE_CodTransaccion
		{
			get
			{
				return this._SE_CodTransaccion;
			}
			set
			{
				this._SE_CodTransaccion = value;
			}
		}

		public string SE_CtaAdministrativa
		{
			get
			{
				return this._SE_CtaAdministrativa;
			}
			set
			{
				this._SE_CtaAdministrativa = value;
			}
		}

		public string SE_Grupo
		{
			get
			{
				return this._SE_Grupo;
			}
			set
			{
				this._SE_Grupo = value;
			}
		}

		public int SE_HoraFin
		{
			get
			{
				return this._SE_HoraFin;
			}
			set
			{
				this._SE_HoraFin = value;
			}
		}

		public int SE_HoraInicio
		{
			get
			{
				return this._SE_HoraInicio;
			}
			set
			{
				this._SE_HoraInicio = value;
			}
		}

		public string SE_Nemotecnico
		{
			get
			{
				return this._SE_Nemotecnico;
			}
			set
			{
				this._SE_Nemotecnico = value;
			}
		}

		public string SE_Nombre
		{
			get
			{
				return this._SE_Nombre;
			}
			set
			{
				this._SE_Nombre = value;
			}
		}

		public string SE_OlbId
		{
			get
			{
				return this._SE_OlbId;
			}
			set
			{
				this._SE_OlbId = value;
			}
		}

		public string SE_RIF
		{
			get
			{
				return this._SE_RIF;
			}
			set
			{
				this._SE_RIF = value;
			}
		}

		public int SE_ServicioId
		{
			get
			{
				return this._SE_ServicioId;
			}
			set
			{
				this._SE_ServicioId = value;
			}
		}

		public Servicios()
		{
		}

		public static Servicios getNewTiposServicios(DataRow row)
		{
			int num;
			string empty;
			string str;
			string empty1;
			string str1;
			string empty2;
			int num1;
			int num2;
			string str2;
			string empty3;
			string str3;
			string empty4;
			Servicios servicio = new Servicios();
			Servicios servicio1 = servicio;
			if (row.Table.Columns.Contains("SE_ServicioId"))
			{
				num = (row.IsNull("SE_ServicioId") ? 0 : int.Parse(row["SE_ServicioId"].ToString()));
			}
			else
			{
				num = 0;
			}
			servicio1.SE_ServicioId = num;
			Servicios servicio2 = servicio;
			if (row.Table.Columns.Contains("SE_Activo"))
			{
				empty = (row.IsNull("SE_Activo") ? string.Empty : row["SE_Activo"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			servicio2.SE_Activo = empty;
			Servicios servicio3 = servicio;
			if (row.Table.Columns.Contains("SE_CodBanco"))
			{
				str = (row.IsNull("SE_CodBanco") ? string.Empty : row["SE_CodBanco"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			servicio3.SE_CodBanco = str;
			Servicios servicio4 = servicio;
			if (row.Table.Columns.Contains("SE_CodTransaccion"))
			{
				empty1 = (row.IsNull("SE_CodTransaccion") ? string.Empty : row["SE_CodTransaccion"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			servicio4.SE_CodTransaccion = empty1;
			Servicios servicio5 = servicio;
			if (row.Table.Columns.Contains("SE_CtaAdministrativa"))
			{
				str1 = (row.IsNull("SE_CtaAdministrativa") ? string.Empty : row["SE_CtaAdministrativa"].ToString());
			}
			else
			{
				str1 = string.Empty;
			}
			servicio5.SE_CtaAdministrativa = str1;
			Servicios servicio6 = servicio;
			if (row.Table.Columns.Contains("SE_Grupo"))
			{
				empty2 = (row.IsNull("SE_Grupo") ? string.Empty : row["SE_Grupo"].ToString());
			}
			else
			{
				empty2 = string.Empty;
			}
			servicio6.SE_Grupo = empty2;
			Servicios servicio7 = servicio;
			if (row.Table.Columns.Contains("SE_HoraFin"))
			{
				num1 = (row.IsNull("SE_HoraFin") ? 0 : int.Parse(row["SE_HoraFin"].ToString()));
			}
			else
			{
				num1 = 0;
			}
			servicio7.SE_HoraFin = num1;
			Servicios servicio8 = servicio;
			if (row.Table.Columns.Contains("SE_HoraInicio"))
			{
				num2 = (row.IsNull("SE_HoraInicio") ? 0 : int.Parse(row["SE_HoraInicio"].ToString()));
			}
			else
			{
				num2 = 0;
			}
			servicio8.SE_HoraInicio = num2;
			Servicios servicio9 = servicio;
			if (row.Table.Columns.Contains("SE_Nombre"))
			{
				str2 = (row.IsNull("SE_Nombre") ? string.Empty : row["SE_Nombre"].ToString());
			}
			else
			{
				str2 = string.Empty;
			}
			servicio9.SE_Nombre = str2;
			Servicios servicio10 = servicio;
			if (row.Table.Columns.Contains("SE_OlbId"))
			{
				empty3 = (row.IsNull("SE_OlbId") ? string.Empty : row["SE_OlbId"].ToString());
			}
			else
			{
				empty3 = string.Empty;
			}
			servicio10.SE_OlbId = empty3;
			Servicios servicio11 = servicio;
			if (row.Table.Columns.Contains("SE_RIF"))
			{
				str3 = (row.IsNull("SE_RIF") ? string.Empty : row["SE_RIF"].ToString());
			}
			else
			{
				str3 = string.Empty;
			}
			servicio11.SE_RIF = str3;
			Servicios servicio12 = servicio;
			if (row.Table.Columns.Contains("SE_Nemotecnico"))
			{
				empty4 = (row.IsNull("SE_Nemotecnico") ? string.Empty : row["SE_Nemotecnico"].ToString());
			}
			else
			{
				empty4 = string.Empty;
			}
			servicio12.SE_Nemotecnico = empty4;
			int sEServicioId = servicio.SE_ServicioId;
			servicio.Key = CryptoUtils.EncryptMD5(sEServicioId.ToString());
			return servicio;
		}
	}
}