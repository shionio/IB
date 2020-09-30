using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class TipoFavorito
	{
		private int _TipoFavoritoID;

		private string _descripcion;

		private string _titulo;

		private string _msgError;

		private string _Activo;

		private string key;

		private IBBAV.Helpers.EnumTipoFavorito _EnumTipoFavorito;

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

		public IBBAV.Helpers.EnumTipoFavorito EnumTipoFavorito
		{
			get
			{
				return this._EnumTipoFavorito;
			}
			set
			{
				this._EnumTipoFavorito = value;
			}
		}

		public bool IsServicio
		{
			get
			{
				string str = this.TipoFavoritoID.ToString();
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

		public string MsgError
		{
			get
			{
				return this._msgError;
			}
			set
			{
				this._msgError = value;
			}
		}

		public int TipoFavoritoID
		{
			get
			{
				return this._TipoFavoritoID;
			}
			set
			{
				this._TipoFavoritoID = value;
			}
		}

		public string Titulo
		{
			get
			{
				return this._titulo;
			}
			set
			{
				this._titulo = value;
			}
		}

		public TipoFavorito()
		{
		}

		public static TipoFavorito getNewTipoFavorito(DataRow dr)
		{
			int num;
			string str;
			string str1;
			string str2;
			string str3;
			TipoFavorito tipoFavorito = new TipoFavorito();
			TipoFavorito tipoFavorito1 = tipoFavorito;
			if (dr.Table.Columns.Contains("TipoFavoritoID"))
			{
				num = (dr.IsNull("TipoFavoritoID") ? 0 : int.Parse(dr["TipoFavoritoID"].ToString()));
			}
			else
			{
				num = 0;
			}
			tipoFavorito1.TipoFavoritoID = num;
			Type type = typeof(IBBAV.Helpers.EnumTipoFavorito);
			int tipoFavoritoID = tipoFavorito.TipoFavoritoID;
			tipoFavorito.EnumTipoFavorito = (IBBAV.Helpers.EnumTipoFavorito)Enum.Parse(type, tipoFavoritoID.ToString());
			TipoFavorito tipoFavorito2 = tipoFavorito;
			if (dr.Table.Columns.Contains("Descripcion"))
			{
				str = (dr.IsNull("Descripcion") ? "" : dr["Descripcion"].ToString());
			}
			else
			{
				str = "";
			}
			tipoFavorito2.Descripcion = str;
			TipoFavorito tipoFavorito3 = tipoFavorito;
			if (dr.Table.Columns.Contains("Titulo"))
			{
				str1 = (dr.IsNull("Titulo") ? "" : dr["Titulo"].ToString());
			}
			else
			{
				str1 = "";
			}
			tipoFavorito3.Titulo = str1;
			TipoFavorito tipoFavorito4 = tipoFavorito;
			if (dr.Table.Columns.Contains("MsgError"))
			{
				str2 = (dr.IsNull("MsgError") ? "" : dr["MsgError"].ToString());
			}
			else
			{
				str2 = "";
			}
			tipoFavorito4.MsgError = str2;
			TipoFavorito tipoFavorito5 = tipoFavorito;
			if (dr.Table.Columns.Contains("Activo"))
			{
				str3 = (dr.IsNull("Activo") ? "" : dr["Activo"].ToString());
			}
			else
			{
				str3 = "";
			}
			tipoFavorito5.Activo = str3;
			int tipoFavoritoID1 = tipoFavorito.TipoFavoritoID;
			tipoFavorito.Key = CryptoUtils.EncryptMD5(tipoFavoritoID1.ToString());
			return tipoFavorito;
		}
	}
}