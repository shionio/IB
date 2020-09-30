using System;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class AfiliadoFavorito : EntidadBase
	{
		private long _nAF_Id;

		private int _tipoFavoritoId;

		private string _numeroInstrumento;

		private string _cedulaRif;

		private string _sBANK_ID;

		private string _beneficiario;

		private string _descripcion;

		private string _tipoTarjetaCredito;

		private string _email;

		private string _Activo;

		private string _TipoDescripcion;

		private string key;

		public string Activo
		{
			get
			{
				return this._Activo;
			}
			set
			{
				this._Activo = value;
			}
		}

		public string BankId
		{
			get
			{
				return this._sBANK_ID;
			}
			set
			{
				this._sBANK_ID = value;
			}
		}

		public string Beneficiario
		{
			get
			{
				return this._beneficiario;
			}
			set
			{
				this._beneficiario = value;
			}
		}

		public string CedulaRif
		{
			get
			{
				return this._cedulaRif;
			}
			set
			{
				this._cedulaRif = value;
			}
		}

		public string Descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				this._descripcion = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}

		public bool IsServicio
		{
			get
			{
				string str = this.TipoFavoritoId.ToString();
				return "123456".IndexOf(str) != -1;
			}
		}

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

		public string NumeroInstrumento
		{
			get
			{
				return this._numeroInstrumento;
			}
			set
			{
				this._numeroInstrumento = value;
			}
		}

		public string TipoDescripcion
		{
			get
			{
				return this._TipoDescripcion;
			}
			set
			{
				this._TipoDescripcion = value;
			}
		}

		public int TipoFavoritoId
		{
			get
			{
				return this._tipoFavoritoId;
			}
			set
			{
				this._tipoFavoritoId = value;
			}
		}

		public string TipoTarjetaCredito
		{
			get
			{
				return this._tipoTarjetaCredito;
			}
			set
			{
				this._tipoTarjetaCredito = value;
			}
		}

		public AfiliadoFavorito()
		{
		}

		public static AfiliadoFavorito getNewAfiliadoFavorito(DataRow dr)
		{
			long num;
			int num1;
			string empty;
			string str;
			string empty1;
			string str1;
			string empty2;
			string str2;
			string empty3;
			string str3;
			string empty4;
			AfiliadoFavorito afiliadoFavorito = new AfiliadoFavorito();
			AfiliadoFavorito afiliadoFavorito1 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("AF_ID"))
			{
				num = (dr.IsNull("AF_ID") ? (long)0 : long.Parse(dr["AF_ID"].ToString()));
			}
			else
			{
				num = (long)0;
			}
			afiliadoFavorito1.nAF_Id = num;
			AfiliadoFavorito afiliadoFavorito2 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("TipoFavoritoID"))
			{
				num1 = (dr.IsNull("TipoFavoritoID") ? 0 : int.Parse(dr["TipoFavoritoID"].ToString()));
			}
			else
			{
				num1 = 0;
			}
			afiliadoFavorito2.TipoFavoritoId = num1;
			AfiliadoFavorito afiliadoFavorito3 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("NumeroInstrumento"))
			{
				empty = (dr.IsNull("NumeroInstrumento") ? string.Empty : dr["NumeroInstrumento"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			afiliadoFavorito3.NumeroInstrumento = empty;
			AfiliadoFavorito afiliadoFavorito4 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("CedulaRif"))
			{
				str = (dr.IsNull("CedulaRif") ? string.Empty : dr["CedulaRif"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			afiliadoFavorito4.CedulaRif = str;
			AfiliadoFavorito afiliadoFavorito5 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("BANK_ID"))
			{
				empty1 = (dr.IsNull("BANK_ID") ? string.Empty : dr["BANK_ID"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			afiliadoFavorito5.BankId = empty1;
			AfiliadoFavorito afiliadoFavorito6 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("Beneficiario"))
			{
				str1 = (dr.IsNull("Beneficiario") ? string.Empty : dr["Beneficiario"].ToString());
			}
			else
			{
				str1 = string.Empty;
			}
			afiliadoFavorito6.Beneficiario = str1;
			AfiliadoFavorito afiliadoFavorito7 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("Descripcion"))
			{
				empty2 = (dr.IsNull("Descripcion") ? string.Empty : dr["Descripcion"].ToString());
			}
			else
			{
				empty2 = string.Empty;
			}
			afiliadoFavorito7.Descripcion = empty2;
			AfiliadoFavorito afiliadoFavorito8 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("TipoTarjetaCredito"))
			{
				str2 = (dr.IsNull("TipoTarjetaCredito") ? string.Empty : dr["TipoTarjetaCredito"].ToString());
			}
			else
			{
				str2 = string.Empty;
			}
			afiliadoFavorito8.TipoTarjetaCredito = str2;
			AfiliadoFavorito afiliadoFavorito9 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("Email"))
			{
				empty3 = (dr.IsNull("Email") ? string.Empty : dr["Email"].ToString());
			}
			else
			{
				empty3 = string.Empty;
			}
			afiliadoFavorito9.Email = empty3;
			AfiliadoFavorito afiliadoFavorito10 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("Activo"))
			{
				str3 = (dr.IsNull("Activo") ? string.Empty : dr["Activo"].ToString());
			}
			else
			{
				str3 = string.Empty;
			}
			afiliadoFavorito10.Activo = str3;
			AfiliadoFavorito afiliadoFavorito11 = afiliadoFavorito;
			if (dr.Table.Columns.Contains("TipoDescripcion"))
			{
				empty4 = (dr.IsNull("TipoDescripcion") ? string.Empty : dr["TipoDescripcion"].ToString());
			}
			else
			{
				empty4 = string.Empty;
			}
			afiliadoFavorito11.TipoDescripcion = empty4;
			return afiliadoFavorito;
		}
	}
}