using System;
using System.Data;
using System.Web;

namespace IBBAV.Entidades
{
	[Serializable]
	public class Afiliado : EntidadBase
	{
		private long _nAF_Id;

		private string _sAF_NombreUsuario;

		private string _sAF_Password;

		private int _nTI_Id;

		private long _nES_Id;

		private long _nCO_Id;

		private string _sAF_PasswordTransacciones;

		private string _sAF_PreguntaDesafio;

		private string _sAF_RespuestaPD;

		private long _nST_Id;

		private string _sAF_Rif;

		private string _sTarjeta;

		private string _sCedula;

		private string _sClPhoenix;

		private int _nFI_Id;

		private int _nAF_IniciaTr;

		private int _nAF_ApruebaTr;

		private int _nAF_Tipo;

		private string _sCO_Nombres;

		private string _sCO_Apellidos;

		private string _nFI_Codigo;

		private string _cedRIF;

		private string _tCedRIF;

		private string _sAF_CLAsig;

		private string _sAF_FecConst;

		private string _sIP;

		private long _nST_PassTransacciones;

		private long _AF_CodCliente;

		private int _AF_CodOficina;

		private DateTime _AF_FechaPassword;

		private int _AF_DiasPassword;

		private string _CO_Email;

		private string _CO_Celular;

		private SessionAfiliado session;

		public long AF_CodCliente
		{
			get
			{
				return this._AF_CodCliente;
			}
			set
			{
				this._AF_CodCliente = value;
			}
		}

		public int AF_CodOficina
		{
			get
			{
				return this._AF_CodOficina;
			}
			set
			{
				this._AF_CodOficina = value;
			}
		}

		public int AF_DiasPassword
		{
			get
			{
				return this._AF_DiasPassword;
			}
			set
			{
				this._AF_DiasPassword = value;
			}
		}

		public DateTime AF_FechaPassword
		{
			get
			{
				return this._AF_FechaPassword;
			}
			set
			{
				this._AF_FechaPassword = value;
			}
		}

		public string cedRIF
		{
			get
			{
				return this._cedRIF;
			}
			set
			{
				this._cedRIF = value;
			}
		}

		public string CO_Celular
		{
			get
			{
				return this._CO_Celular;
			}
			set
			{
				this._CO_Celular = value;
			}
		}

		public string CO_Email
		{
			get
			{
				return this._CO_Email;
			}
			set
			{
				this._CO_Email = value;
			}
		}

		public int nAF_ApruebaTr
		{
			get
			{
				return this._nAF_ApruebaTr;
			}
			set
			{
				this._nAF_ApruebaTr = value;
			}
		}

		public long nAF_Id
		{
			get
			{
				return this._nAF_Id;
			}
			set
			{
				this._nAF_Id = value;
			}
		}

		public int nAF_IniciaTr
		{
			get
			{
				return this._nAF_IniciaTr;
			}
			set
			{
				this._nAF_IniciaTr = value;
			}
		}

		public int nAF_Tipo
		{
			get
			{
				return this._nAF_Tipo;
			}
			set
			{
				this._nAF_Tipo = value;
			}
		}

		public long nCO_Id
		{
			get
			{
				return this._nCO_Id;
			}
			set
			{
				this._nCO_Id = value;
			}
		}

		public long nES_Id
		{
			get
			{
				return this._nES_Id;
			}
			set
			{
				this._nES_Id = value;
			}
		}

		public string nFI_Codigo
		{
			get
			{
				return this._nFI_Codigo;
			}
			set
			{
				this._nFI_Codigo = value;
			}
		}

		public int nFI_Id
		{
			get
			{
				return this._nFI_Id;
			}
			set
			{
				this._nFI_Id = value;
			}
		}

		public long nST_Id
		{
			get
			{
				return this._nST_Id;
			}
			set
			{
				this._nST_Id = value;
			}
		}

		public long nST_PassTransacciones
		{
			get
			{
				return this._nST_PassTransacciones;
			}
			set
			{
				this._nST_PassTransacciones = value;
			}
		}

		public int nTI_Id
		{
			get
			{
				return this._nTI_Id;
			}
			set
			{
				this._nTI_Id = value;
			}
		}

		public string sAF_CLAsig
		{
			get
			{
				return this._sAF_CLAsig;
			}
			set
			{
				this._sAF_CLAsig = value;
			}
		}

		public string sAF_FecConst
		{
			get
			{
				return this._sAF_FecConst;
			}
			set
			{
				this._sAF_FecConst = value;
			}
		}

		public string sAF_NombreUsuario
		{
			get
			{
				return this._sAF_NombreUsuario;
			}
			set
			{
				this._sAF_NombreUsuario = value;
			}
		}

		public string sAF_Password
		{
			get
			{
				return this._sAF_Password;
			}
			set
			{
				this._sAF_Password = value;
			}
		}

		public string sAF_PasswordTransacciones
		{
			get
			{
				return this._sAF_PasswordTransacciones;
			}
			set
			{
				this._sAF_PasswordTransacciones = value;
			}
		}

		public string sAF_PreguntaDesafio
		{
			get
			{
				return this._sAF_PreguntaDesafio;
			}
			set
			{
				this._sAF_PreguntaDesafio = value;
			}
		}

		public string sAF_RespuestaPD
		{
			get
			{
				return this._sAF_RespuestaPD;
			}
			set
			{
				this._sAF_RespuestaPD = value;
			}
		}

		public string sAF_Rif
		{
			get
			{
				return this._sAF_Rif;
			}
			set
			{
				this._sAF_Rif = value;
			}
		}

		public string sCedula
		{
			get
			{
				return this._sCedula;
			}
			set
			{
				this._sCedula = value;
			}
		}

		public string sClPhoenix
		{
			get
			{
				return this._sClPhoenix;
			}
			set
			{
				this._sClPhoenix = value;
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

		public SessionAfiliado Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		public string sIP
		{
			get
			{
				return this._sIP;
			}
			set
			{
				this._sIP = value;
			}
		}

		public string sTarjeta
		{
			get
			{
				return this._sTarjeta;
			}
			set
			{
				this._sTarjeta = value;
			}
		}

		public string tCedRIF
		{
			get
			{
				return this._tCedRIF;
			}
			set
			{
				this._tCedRIF = value;
			}
		}

		public Afiliado(string sAF_NombreUsuarioIn)
		{
			this._sAF_NombreUsuario = sAF_NombreUsuarioIn;
		}

		public Afiliado(long nAF_IdIn, string sAF_NombreUsuarioIn, string sAF_PasswordIn, int nTI_IdIn, long nES_IdIn, long nCO_IdIn, string sAF_PasswordTransaccionesIn, string sAF_PreguntaDesafioIn, string sAF_RespuestaPDIn, long nST_IdIn, string sAF_RifIn)
		{
			this._nAF_Id = nAF_IdIn;
			this._sAF_NombreUsuario = sAF_NombreUsuarioIn;
			this._sAF_Password = sAF_PasswordIn;
			this._nTI_Id = nTI_IdIn;
			this._nES_Id = nES_IdIn;
			this._nCO_Id = nCO_IdIn;
			this._sAF_PasswordTransacciones = sAF_PasswordTransaccionesIn;
			this._sAF_PreguntaDesafio = sAF_PreguntaDesafioIn;
			this._sAF_RespuestaPD = sAF_RespuestaPDIn;
			this._nST_Id = nST_IdIn;
			this._sAF_Rif = sAF_RifIn;
			this._nFI_Id = this.nFI_Id;
			this._nAF_IniciaTr = 2;
			this._nAF_ApruebaTr = 2;
			this._nST_PassTransacciones = (long)2;
			this._AF_CodCliente = (long)0;
			this._sCO_Nombres = string.Empty;
			this._sCO_Apellidos = string.Empty;
			this._CO_Email = string.Empty;
			SessionAfiliado sessionAfiliado = new SessionAfiliado();
		}

		public Afiliado()
		{
			this._nAF_Id = (long)0;
			this._sAF_NombreUsuario = string.Empty;
			this._sAF_Password = string.Empty;
			this._nTI_Id = 10;
			this._nES_Id = (long)2;
			this._nCO_Id = (long)0;
			this._sAF_PasswordTransacciones = "";
			this._sAF_PreguntaDesafio = "";
			this._sAF_RespuestaPD = "";
			this._sCedula = "";
			this._sTarjeta = "9999999999999999";
			this._sClPhoenix = string.Empty;
			this._nST_Id = (long)2;
            this._sAF_Rif = string.Empty;
			this._nFI_Id = 21;
			this._nAF_Tipo = 2;
			this._nAF_IniciaTr = 2;
			this._nAF_ApruebaTr = 2;
			this._nST_PassTransacciones = (long)2;
			this._AF_CodCliente = (long)0;
			this._CO_Email = string.Empty;
			this._CO_Celular = string.Empty;
			SessionAfiliado sessionAfiliado = new SessionAfiliado();
        }

		public static Afiliado getNewAfiliado(DataRow dr)
		{
			long num;
			string str;
			string str1;
			int num1;
			long num2;
			long num3;
			string str2;
			string str3;
			string str4;
			long num4;
			string str5;
			string str6;
			string str7;
			string str8;
			int num5;
			int num6;
			int num7;
			int num8;
			long num9;
			int num10;
			string str9;
			DateTime dateTime;
			int num11;
			Afiliado afiliado = new Afiliado();
			Afiliado afiliado1 = afiliado;
			if (dr.Table.Columns.Contains("AF_Id"))
			{
				num = (dr.IsNull("AF_Id") ? (long)0 : long.Parse(dr["AF_Id"].ToString()));
			}
			else
			{
				num = (long)0;
			}
			afiliado1.nAF_Id = num;
			Afiliado afiliado2 = afiliado;
			if (dr.Table.Columns.Contains("AF_NombreUsuario"))
			{
				str = (dr.IsNull("AF_NombreUsuario") ? "" : dr["AF_NombreUsuario"].ToString());
			}
			else
			{
				str = "";
			}
			afiliado2.sAF_NombreUsuario = str;
			Afiliado afiliado3 = afiliado;
			if (dr.Table.Columns.Contains("AF_Password"))
			{
				str1 = (dr.IsNull("AF_Password") ? "" : dr["AF_Password"].ToString());
			}
			else
			{
				str1 = "";
			}
			afiliado3.sAF_Password = str1;
			Afiliado afiliado4 = afiliado;
			if (dr.Table.Columns.Contains("TI_Id"))
			{
				num1 = (dr.IsNull("TI_Id") ? 0 : int.Parse(dr["TI_Id"].ToString()));
			}
			else
			{
				num1 = 0;
			}
			afiliado4.nTI_Id = num1;
			Afiliado afiliado5 = afiliado;
			if (dr.Table.Columns.Contains("ES_Id"))
			{
				num2 = (dr.IsNull("ES_Id") ? (long)0 : long.Parse(dr["ES_Id"].ToString()));
			}
			else
			{
				num2 = (long)0;
			}
			afiliado5.nES_Id = num2;
			Afiliado afiliado6 = afiliado;
			if (dr.Table.Columns.Contains("CO_Id"))
			{
				num3 = (dr.IsNull("CO_Id") ? (long)0 : long.Parse(dr["CO_Id"].ToString()));
			}
			else
			{
				num3 = (long)0;
			}
			afiliado6.nCO_Id = num3;
			Afiliado afiliado7 = afiliado;
			if (dr.Table.Columns.Contains("AF_PasswordTransacciones"))
			{
				str2 = (dr.IsNull("AF_PasswordTransacciones") ? "" : dr["AF_PasswordTransacciones"].ToString());
			}
			else
			{
				str2 = "";
			}
			afiliado7.sAF_PasswordTransacciones = str2;
			Afiliado afiliado8 = afiliado;
			if (dr.Table.Columns.Contains("AF_PreguntaDesafio"))
			{
				str3 = (dr.IsNull("AF_PreguntaDesafio") ? "" : dr["AF_PreguntaDesafio"].ToString());
			}
			else
			{
				str3 = "";
			}
			afiliado8.sAF_PreguntaDesafio = str3;
			Afiliado afiliado9 = afiliado;
			if (dr.Table.Columns.Contains("AF_RespuestaPD"))
			{
				str4 = (dr.IsNull("AF_RespuestaPD") ? "" : dr["AF_RespuestaPD"].ToString());
			}
			else
			{
				str4 = "";
			}
			afiliado9.sAF_RespuestaPD = str4;
			Afiliado afiliado10 = afiliado;
			if (dr.Table.Columns.Contains("ST_Id"))
			{
				num4 = (dr.IsNull("ST_Id") ? (long)0 : long.Parse(dr["ST_Id"].ToString()));
			}
			else
			{
				num4 = (long)0;
			}
			afiliado10.nST_Id = num4;
			Afiliado afiliado11 = afiliado;
			if (dr.Table.Columns.Contains("AF_Rif"))
			{
				str5 = (dr.IsNull("AF_Rif") ? "" : dr["AF_Rif"].ToString());
			}
			else
			{
				str5 = "";
			}
			afiliado11.sAF_Rif = str5;
			Afiliado afiliado12 = afiliado;
			if (dr.Table.Columns.Contains("AF_Tarjeta"))
			{
				str6 = (dr.IsNull("AF_Tarjeta") ? "" : dr["AF_Tarjeta"].ToString());
			}
			else
			{
				str6 = "";
			}
			afiliado12.sTarjeta = str6;
			Afiliado afiliado13 = afiliado;
			if (dr.Table.Columns.Contains("AF_Cedula"))
			{
				str7 = (dr.IsNull("AF_Cedula") ? "" : dr["AF_Cedula"].ToString());
			}
			else
			{
				str7 = "";
			}
			afiliado13.sCedula = str7;
			Afiliado afiliado14 = afiliado;
			if (dr.Table.Columns.Contains("AF_Clave"))
			{
				str8 = (dr.IsNull("AF_Clave") ? "" : dr["AF_Clave"].ToString());
			}
			else
			{
				str8 = "";
			}
			afiliado14.sClPhoenix = str8;
			Afiliado afiliado15 = afiliado;
			if (dr.Table.Columns.Contains("FI_Id"))
			{
				num5 = (dr.IsNull("FI_Id") ? 0 : int.Parse(dr["FI_Id"].ToString()));
			}
			else
			{
				num5 = 0;
			}
			afiliado15.nFI_Id = num5;
			Afiliado afiliado16 = afiliado;
			if (dr.Table.Columns.Contains("AF_Tipo"))
			{
				num6 = (dr.IsNull("AF_Tipo") ? 0 : int.Parse(dr["AF_Tipo"].ToString()));
			}
			else
			{
				num6 = 0;
			}
			afiliado16.nAF_Tipo = num6;
			Afiliado afiliado17 = afiliado;
			if (dr.Table.Columns.Contains("AF_ApruebaTr"))
			{
				num7 = (dr.IsNull("AF_ApruebaTr") ? 0 : int.Parse(dr["AF_ApruebaTr"].ToString()));
			}
			else
			{
				num7 = 0;
			}
			afiliado17.nAF_ApruebaTr = num7;
			Afiliado afiliado18 = afiliado;
			if (dr.Table.Columns.Contains("AF_IniciaTr"))
			{
				num8 = (dr.IsNull("AF_IniciaTr") ? 0 : int.Parse(dr["AF_IniciaTr"].ToString()));
			}
			else
			{
				num8 = 0;
			}
			afiliado18.nAF_IniciaTr = num8;
			Afiliado afiliado19 = afiliado;
			if (dr.Table.Columns.Contains("AF_CodCliente"))
			{
				num9 = (dr.IsNull("AF_CodCliente") ? (long)0 : long.Parse(dr["AF_CodCliente"].ToString()));
			}
			else
			{
				num9 = (long)0;
			}
			afiliado19.AF_CodCliente = num9;
			Afiliado afiliado20 = afiliado;
			if (dr.Table.Columns.Contains("AF_CodOficina"))
			{
				num10 = (dr.IsNull("AF_CodOficina") ? 0 : int.Parse(dr["AF_CodOficina"].ToString()));
			}
			else
			{
				num10 = 0;
			}
			afiliado20.AF_CodOficina = num10;
			afiliado.sCO_Nombres = EntidadBase.IsString(dr, "CO_NOMBRES");
			afiliado.sCO_Apellidos = EntidadBase.IsString(dr, "CO_APELLIDOS");
			Afiliado afiliado21 = afiliado;
			if (dr.Table.Columns.Contains("CO_DocId"))
			{
				str9 = (dr.IsNull("CO_DocId") ? "" : dr["CO_DocId"].ToString());
			}
			else
			{
				str9 = "";
			}
			afiliado21.cedRIF = str9;
			afiliado.sIP = HttpContext.Current.Request.UserHostAddress;
			Afiliado afiliado22 = afiliado;
			if (dr.Table.Columns.Contains("AF_FechaPassword"))
			{
				dateTime = (dr.IsNull("AF_FechaPassword") ? new DateTime(2000, 1, 1) : DateTime.Parse(dr["AF_FechaPassword"].ToString()));
			}
			else
			{
				dateTime = new DateTime(2000, 1, 1);
			}
			afiliado22.AF_FechaPassword = dateTime;
			Afiliado afiliado23 = afiliado;
			if (dr.Table.Columns.Contains("AF_DiasPassword"))
			{
				num11 = (dr.IsNull("AF_DiasPassword") ? 0 : int.Parse(dr["AF_DiasPassword"].ToString()));
			}
			else
			{
				num11 = 0;
			}
			afiliado23.AF_DiasPassword = num11;
			afiliado.CO_Email = EntidadBase.IsString(dr, "CO_Email");
			afiliado.CO_Celular = EntidadBase.IsString(dr, "CO_Celular");
			return afiliado;
		}

		public override string ToString()
		{
			return string.Concat(new string[] { "nAF_Id.:", this._nAF_Id.ToString(), " sAF_NombreUsuario.:", this._sAF_NombreUsuario, " sAF_Password.: ", this._sAF_Password, " nTI_Id.: ", this._nTI_Id.ToString(), " nES_Id.: ", this._nES_Id.ToString(), " nCO_Id.: ", this._nCO_Id.ToString(), " _sAF_PasswordTransacciones.: ", this._sAF_PasswordTransacciones, " sAF_PreguntaDesafio.: ", this._sAF_PreguntaDesafio, " sAF_RespuestaPD.: ", this._sAF_RespuestaPD, " nST_Id.: ", this._nST_Id.ToString(), " _sCedula.:", this._sCedula, " _sTarjeta.:", this._sTarjeta, " sAF_Rif:", this._sAF_Rif });
		}
	}
}