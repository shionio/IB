using System;
using System.Data;

namespace IBBAV.Entidades
{
	public class Contactos
	{
		private long _nCO_id;

		private long _nCO_Codigo;

		private string _sCO_Nombres;

		private string _sCO_Apellidos;

		private int _nTI_id;

		private long _nES_id;

		private string _sCO_DocId;

		private int _nCO_TipoDocId;

		private string _sCO_Email;

		private string _sCO_Telefono;

		private string _sCO_EdificioCasa;

		private string _sCO_Calle;

		private string _sCO_Urbanizacion;

		private string _sCO_CodigoPostal;

		private string _sCO_Ciudad;

		private string _sCO_Estado;

		private string _sCO_Pais;

		private string _sCO_Empresa;

		private int _nCA_Id;

		private string _sCO_Email2;

		private string _sCO_Telefono2;

		private string _sCO_Celular;

		private string _sCO_Rif;

		public int nCA_Id
		{
			get
			{
				return this._nCA_Id;
			}
			set
			{
				this._nCA_Id = value;
			}
		}

		public long nCO_Codigo
		{
			get
			{
				return this._nCO_Codigo;
			}
			set
			{
				this._nCO_Codigo = value;
			}
		}

		public long nCO_id
		{
			get
			{
				return this._nCO_id;
			}
			set
			{
				this._nCO_id = value;
			}
		}

		public int nCO_TipoDocId
		{
			get
			{
				return this._nCO_TipoDocId;
			}
			set
			{
				this._nCO_TipoDocId = value;
			}
		}

		public long nES_id
		{
			get
			{
				return this._nES_id;
			}
			set
			{
				this._nES_id = value;
			}
		}

		public int nTI_id
		{
			get
			{
				return this._nTI_id;
			}
			set
			{
				this._nTI_id = value;
			}
		}

		public string sCO_Apellidos
		{
			get
			{
				return this._sCO_Apellidos;
			}
			set
			{
				this._sCO_Apellidos = value;
			}
		}

		public string sCO_Calle
		{
			get
			{
				return this._sCO_Calle;
			}
			set
			{
				this._sCO_Calle = value;
			}
		}

		public string sCO_Celular
		{
			get
			{
				return this._sCO_Celular;
			}
			set
			{
				this._sCO_Celular = value;
			}
		}

		public string sCO_Ciudad
		{
			get
			{
				return this._sCO_Ciudad;
			}
			set
			{
				this._sCO_Ciudad = value;
			}
		}

		public string sCO_CodigoPostal
		{
			get
			{
				return this._sCO_CodigoPostal;
			}
			set
			{
				this._sCO_CodigoPostal = value;
			}
		}

		public string sCO_DocId
		{
			get
			{
				return this._sCO_DocId;
			}
			set
			{
				this._sCO_DocId = value;
			}
		}

		public string sCO_EdificioCasa
		{
			get
			{
				return this._sCO_EdificioCasa;
			}
			set
			{
				this._sCO_EdificioCasa = value;
			}
		}

		public string sCO_Email
		{
			get
			{
				return this._sCO_Email;
			}
			set
			{
				this._sCO_Email = value;
			}
		}

		public string sCO_Email2
		{
			get
			{
				return this._sCO_Email2;
			}
			set
			{
				this._sCO_Email2 = value;
			}
		}

		public string sCO_Empresa
		{
			get
			{
				return this._sCO_Empresa;
			}
			set
			{
				this._sCO_Empresa = value;
			}
		}

		public string sCO_Estado
		{
			get
			{
				return this._sCO_Estado;
			}
			set
			{
				this._sCO_Estado = value;
			}
		}

		public string sCO_Nombres
		{
			get
			{
				return this._sCO_Nombres;
			}
			set
			{
				this._sCO_Nombres = value;
			}
		}

		public string sCO_Pais
		{
			get
			{
				return this._sCO_Pais;
			}
			set
			{
				this._sCO_Pais = value;
			}
		}

		public string sCO_Rif
		{
			get
			{
				return this._sCO_Rif;
			}
			set
			{
				this._sCO_Rif = value;
			}
		}

		public string sCO_Telefono
		{
			get
			{
				return this._sCO_Telefono;
			}
			set
			{
				this._sCO_Telefono = value;
			}
		}

		public string sCO_Telefono2
		{
			get
			{
				return this._sCO_Telefono2;
			}
			set
			{
				this._sCO_Telefono2 = value;
			}
		}

		public string sCO_Urbanizacion
		{
			get
			{
				return this._sCO_Urbanizacion;
			}
			set
			{
				this._sCO_Urbanizacion = value;
			}
		}

		public Contactos()
		{
			this._nCO_id = (long)0;
			this._nCO_Codigo = (long)0;
			this._sCO_Nombres = "";
			this._sCO_Apellidos = "";
			this._nTI_id = 0;
			this._nES_id = (long)1;
			this._sCO_DocId = "";
			this._nCO_TipoDocId = 0;
			this._sCO_Email = "";
			this._sCO_Telefono = "";
			this._sCO_EdificioCasa = "";
			this._sCO_Calle = "";
			this._sCO_Urbanizacion = "";
			this._sCO_CodigoPostal = "";
			this._sCO_Ciudad = "";
			this._sCO_Estado = "";
			this._sCO_Pais = "";
			this._sCO_Empresa = "";
			this._nCA_Id = 0;
			this._sCO_Email2 = "";
			this._sCO_Telefono2 = "";
			this._sCO_Celular = "";
			this._sCO_Rif = "";
		}

		public Contactos(long nCO_idIn, long nCO_CodigoIn, string sCO_NombresIn, string sCO_ApellidosIn, int nTI_idIn, long nES_idIn, string sCO_DocIdIn, int nCO_TipoDocIdIn, string sCO_EmailIn, string sCO_TelefonoIn, string sCO_EdificioCasaIn, string sCO_CalleIn, string sCO_UrbanizacionIn, string sCO_CodigoPostalIn, string sCO_CiudadIn, string sCO_EstadoIn, string sCO_PaisIn, string sCO_EmpresaIn, int nCA_IdIn, string sCO_Email2In, string sCO_Telefono2In, string sCO_CelularIn, string sCO_RifIn)
		{
			this._nCO_id = nCO_idIn;
			this._nCO_Codigo = nCO_CodigoIn;
			this._sCO_Nombres = sCO_NombresIn;
			this._sCO_Apellidos = sCO_ApellidosIn;
			this._nTI_id = nTI_idIn;
			this._nES_id = nES_idIn;
			this._sCO_DocId = sCO_DocIdIn;
			this._nCO_TipoDocId = nCO_TipoDocIdIn;
			this._sCO_Email = sCO_EmailIn;
			this._sCO_Telefono = sCO_TelefonoIn;
			this._sCO_EdificioCasa = sCO_EdificioCasaIn;
			this._sCO_Calle = sCO_CalleIn;
			this._sCO_Urbanizacion = sCO_UrbanizacionIn;
			this._sCO_CodigoPostal = sCO_CodigoPostalIn;
			this._sCO_Ciudad = sCO_CiudadIn;
			this._sCO_Estado = sCO_EstadoIn;
			this._sCO_Pais = sCO_PaisIn;
			this._sCO_Empresa = sCO_EmpresaIn;
			this._nCA_Id = nCA_IdIn;
			this._sCO_Email2 = sCO_Email2In;
			this._sCO_Telefono2 = sCO_Telefono2In;
			this._sCO_Celular = sCO_CelularIn;
			this._sCO_Rif = sCO_RifIn;
		}

		public static Contactos getNewContactos(DataRow dr)
		{
			long num;
			long num1;
			string str;
			string str1;
			int num2;
			long num3;
			string str2;
			int num4;
			string str3;
			string str4;
			string str5;
			string str6;
			string str7;
			string str8;
			string str9;
			string str10;
			string str11;
			string str12;
			string str13;
			int num5;
			string str14;
			string str15;
			string str16;
			Contactos contacto = new Contactos();
			Contactos contacto1 = contacto;
			if (!dr.Table.Columns.Contains("CO_id"))
			{
				num = (dr.IsNull("CO_id") ? (long)0 : long.Parse(dr["CO_id"].ToString()));
			}
			else
			{
				num = (long)0;
			}
			contacto1.nCO_id = num;
			Contactos contacto2 = contacto;
			if (!dr.Table.Columns.Contains("CO_Codigo"))
			{
				num1 = (dr.IsNull("CO_Codigo") ? (long)0 : long.Parse(dr["CO_Codigo"].ToString()));
			}
			else
			{
				num1 = (long)0;
			}
			contacto2.nCO_Codigo = num1;
			Contactos contacto3 = contacto;
			if (!dr.Table.Columns.Contains("CO_Nombres"))
			{
				str = (dr.IsNull("CO_Nombres") ? "" : dr["CO_Nombres"].ToString());
			}
			else
			{
				str = "";
			}
			contacto3.sCO_Nombres = str;
			Contactos contacto4 = contacto;
			if (!dr.Table.Columns.Contains("CO_Apellidos"))
			{
				str1 = (dr.IsNull("CO_Apellidos") ? "" : dr["CO_Apellidos"].ToString());
			}
			else
			{
				str1 = "";
			}
			contacto4.sCO_Apellidos = str1;
			Contactos contacto5 = contacto;
			if (!dr.Table.Columns.Contains("TI_Id"))
			{
				num2 = (dr.IsNull("TI_Id") ? 0 : int.Parse(dr["TI_Id"].ToString()));
			}
			else
			{
				num2 = 0;
			}
			contacto5.nTI_id = num2;
			Contactos contacto6 = contacto;
			if (!dr.Table.Columns.Contains("ES_Id"))
			{
				num3 = (dr.IsNull("ES_Id") ? (long)0 : long.Parse(dr["ES_Id"].ToString()));
			}
			else
			{
				num3 = (long)0;
			}
			contacto6.nES_id = num3;
			Contactos contacto7 = contacto;
			if (!dr.Table.Columns.Contains("CO_DocId"))
			{
				str2 = (dr.IsNull("CO_DocId") ? "" : dr["CO_DocId"].ToString());
			}
			else
			{
				str2 = "";
			}
			contacto7.sCO_DocId = str2;
			Contactos contacto8 = contacto;
			if (!dr.Table.Columns.Contains("CO_TipoDocId"))
			{
				num4 = (dr.IsNull("CO_TipoDocId") ? 0 : int.Parse(dr["CO_TipoDocId"].ToString()));
			}
			else
			{
				num4 = 0;
			}
			contacto8.nCO_TipoDocId = num4;
			Contactos contacto9 = contacto;
			if (!dr.Table.Columns.Contains("CO_Email"))
			{
				str3 = (dr.IsNull("CO_Email") ? "" : dr["CO_Email"].ToString());
			}
			else
			{
				str3 = "";
			}
			contacto9.sCO_Email = str3;
			Contactos contacto10 = contacto;
			if (!dr.Table.Columns.Contains("CO_Telefono"))
			{
				str4 = (dr.IsNull("CO_Telefono") ? "" : dr["CO_Telefono"].ToString());
			}
			else
			{
				str4 = "";
			}
			contacto10.sCO_Telefono = str4;
			Contactos contacto11 = contacto;
			if (!dr.Table.Columns.Contains("CO_Email2"))
			{
				str5 = (dr.IsNull("CO_Email2") ? "" : dr["CO_Email2"].ToString());
			}
			else
			{
				str5 = "";
			}
			contacto11.sCO_Email2 = str5;
			Contactos contacto12 = contacto;
			if (!dr.Table.Columns.Contains("CO_EdificioCasa"))
			{
				str6 = (dr.IsNull("CO_EdificioCasa") ? "" : dr["CO_EdificioCasa"].ToString());
			}
			else
			{
				str6 = "";
			}
			contacto12.sCO_EdificioCasa = str6;
			Contactos contacto13 = contacto;
			if (!dr.Table.Columns.Contains("CO_Calle"))
			{
				str7 = (dr.IsNull("CO_Calle") ? "" : dr["CO_Calle"].ToString());
			}
			else
			{
				str7 = "";
			}
			contacto13.sCO_Calle = str7;
			Contactos contacto14 = contacto;
			if (!dr.Table.Columns.Contains("CO_Urbanizacion"))
			{
				str8 = (dr.IsNull("CO_Urbanizacion") ? "" : dr["CO_Urbanizacion"].ToString());
			}
			else
			{
				str8 = "";
			}
			contacto14.sCO_Urbanizacion = str8;
			Contactos contacto15 = contacto;
			if (!dr.Table.Columns.Contains("CO_CodigoPostal"))
			{
				str9 = (dr.IsNull("CO_CodigoPostal") ? "" : dr["CO_CodigoPostal"].ToString());
			}
			else
			{
				str9 = "";
			}
			contacto15.sCO_CodigoPostal = str9;
			Contactos contacto16 = contacto;
			if (!dr.Table.Columns.Contains("CO_Ciudad"))
			{
				str10 = (dr.IsNull("CO_Ciudad") ? "" : dr["CO_Ciudad"].ToString());
			}
			else
			{
				str10 = "";
			}
			contacto16.sCO_Ciudad = str10;
			Contactos contacto17 = contacto;
			if (!dr.Table.Columns.Contains("CO_Estado"))
			{
				str11 = (dr.IsNull("CO_Estado") ? "" : dr["CO_Estado"].ToString());
			}
			else
			{
				str11 = "";
			}
			contacto17.sCO_Estado = str11;
			Contactos contacto18 = contacto;
			if (!dr.Table.Columns.Contains("CO_Pais"))
			{
				str12 = (dr.IsNull("CO_Pais") ? "" : dr["CO_Pais"].ToString());
			}
			else
			{
				str12 = "";
			}
			contacto18.sCO_Pais = str12;
			Contactos contacto19 = contacto;
			if (!dr.Table.Columns.Contains("CO_Empresa"))
			{
				str13 = (dr.IsNull("CO_Empresa") ? "" : dr["CO_Empresa"].ToString());
			}
			else
			{
				str13 = "";
			}
			contacto19.sCO_Empresa = str13;
			Contactos contacto20 = contacto;
			if (!dr.Table.Columns.Contains("CA_Id"))
			{
				num5 = (dr.IsNull("CA_Id") ? 0 : int.Parse(dr["CA_Id"].ToString()));
			}
			else
			{
				num5 = 0;
			}
			contacto20.nCA_Id = num5;
			Contactos contacto21 = contacto;
			if (!dr.Table.Columns.Contains("CO_Telefono2"))
			{
				str14 = (dr.IsNull("CO_Telefono2") ? "" : dr["CO_Telefono2"].ToString());
			}
			else
			{
				str14 = "";
			}
			contacto21.sCO_Telefono2 = str14;
			Contactos contacto22 = contacto;
			if (!dr.Table.Columns.Contains("CO_Celular"))
			{
				str15 = (dr.IsNull("CO_Celular") ? "" : dr["CO_Celular"].ToString());
			}
			else
			{
				str15 = "";
			}
			contacto22.sCO_Celular = str15;
			Contactos contacto23 = contacto;
			if (!dr.Table.Columns.Contains("CO_Rif"))
			{
				str16 = (dr.IsNull("CO_Rif") ? "" : dr["CO_Rif"].ToString());
			}
			else
			{
				str16 = "";
			}
			contacto23.sCO_Rif = str16;
			return contacto;
		}

		public string sTostring()
		{
			return string.Concat(new string[] { "CO_id.:", this._nCO_id.ToString(), " CO_Codigo.:", this._nCO_Codigo.ToString(), " CO_Nombres.:", this._sCO_Nombres, " CO_Apellidos.: ", this._sCO_Apellidos });
		}
	}
}