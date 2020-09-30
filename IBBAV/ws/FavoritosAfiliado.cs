using IBBAV.Entidades;
using IBBAV.Functions;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace IBBAV.ws
{
	public class FavoritosAfiliado : EntidadBase
	{
		public string Activo
		{
			get;
			set;
		}

		public int AF_ID
		{
			get;
			set;
		}

		public string BANK_ID
		{
			get;
			set;
		}

		public string Beneficiario
		{
			get;
			set;
		}

		public string CedulaRif
		{
			get;
			set;
		}

		public string dCompDate
		{
			get;
			set;
		}

		public string Descripcion
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string Key
		{
			get;
			set;
		}

		public string NumeroInstrumento
		{
			get;
			set;
		}

		public string TipoCedulaRif
		{
			get;
			set;
		}

		public string TipoDescripcion
		{
			get;
			set;
		}

		public int TipoFavoritoID
		{
			get;
			set;
		}

		public string TipoTarjetaCredito
		{
			get;
			set;
		}

		public FavoritosAfiliado()
		{
		}

		public static FavoritosAfiliado getNewFavoritosAfiliado(DataRow dr)
		{
			FavoritosAfiliado favoritosAfiliado = new FavoritosAfiliado()
			{
				AF_ID = EntidadBase.IsInt(dr, "AF_ID"),
				TipoFavoritoID = EntidadBase.IsInt(dr, "TipoFavoritoID"),
				NumeroInstrumento = EntidadBase.IsString(dr, "NumeroInstrumento"),
				TipoCedulaRif = EntidadBase.IsString(dr, "TipoCedulaRif"),
				CedulaRif = EntidadBase.IsString(dr, "CedulaRif"),
				BANK_ID = EntidadBase.IsString(dr, "BANK_ID"),
				Beneficiario = EntidadBase.IsString(dr, "Beneficiario"),
				Descripcion = EntidadBase.IsString(dr, "Descripcion"),
				TipoTarjetaCredito = EntidadBase.IsString(dr, "TipoTarjetaCredito"),
				Email = EntidadBase.IsString(dr, "Email")
			};
			DateTime dateTime = EntidadBase.IsDateTime(dr, "dCompDate");
			favoritosAfiliado.dCompDate = dateTime.ToString("dd/MM/yyyy");
			favoritosAfiliado.Activo = EntidadBase.IsString(dr, "Activo");
			favoritosAfiliado.TipoDescripcion = EntidadBase.IsString(dr, "TipoDescripcion");
			int tipoFavoritoID = favoritosAfiliado.TipoFavoritoID;
			favoritosAfiliado.Key = CryptoUtils.EncryptMD5(tipoFavoritoID.ToString());
			return favoritosAfiliado;
		}
	}
}