using System;
using System.Data;

namespace IBBAV.Entidades
{
	public class Banco
	{
		private string _BANK_ID;

		private string _BANK_Nombre;

		private string _BANK_Estatus;

		private int _BANK_Tipo;

		public string BANK_Estatus
		{
			get
			{
				return this._BANK_Estatus;
			}
			set
			{
				this._BANK_Estatus = value;
			}
		}

		public string BANK_ID
		{
			get
			{
				return this._BANK_ID;
			}
			set
			{
				this._BANK_ID = value;
			}
		}

		public string BANK_Nombre
		{
			get
			{
				return this._BANK_Nombre;
			}
			set
			{
				this._BANK_Nombre = value;
			}
		}

		public int BANK_Tipo
		{
			get
			{
				return this._BANK_Tipo;
			}
			set
			{
				this._BANK_Tipo = value;
			}
		}

		public Banco()
		{
		}

		public static Banco getNewBanco(DataRow dr)
		{
			string str;
			string str1;
			string str2;
			int num;
			Banco banco = new Banco();
			Banco banco1 = banco;
			if (dr.Table.Columns.Contains("BANK_ID"))
			{
				str = (dr.IsNull("BANK_ID") ? "" : dr["BANK_ID"].ToString());
			}
			else
			{
				str = "";
			}
			banco1.BANK_ID = str;
			Banco banco2 = banco;
			if (dr.Table.Columns.Contains("BANK_Nombre"))
			{
				str1 = (dr.IsNull("BANK_Nombre") ? "" : dr["BANK_Nombre"].ToString());
			}
			else
			{
				str1 = "";
			}
			banco2.BANK_Nombre = str1;
			Banco banco3 = banco;
			if (dr.Table.Columns.Contains("BANK_Estatus"))
			{
				str2 = (dr.IsNull("BANK_Estatus") ? "" : dr["BANK_Estatus"].ToString());
			}
			else
			{
				str2 = "";
			}
			banco3.BANK_Estatus = str2;
			Banco banco4 = banco;
			if (dr.Table.Columns.Contains("BANK_Tipo"))
			{
				num = (dr.IsNull("BANK_Tipo") ? 0 : int.Parse(dr["BANK_Tipo"].ToString()));
			}
			else
			{
				num = 0;
			}
			banco4.BANK_Tipo = num;
			return banco;
		}
	}
}