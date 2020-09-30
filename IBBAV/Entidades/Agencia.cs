using System;
using System.Data;

namespace IBBAV.Entidades
{
	public class Agencia
	{
		private int _nAG_ID;

		private int _nAG_AgenciaId;

		private string _sAG_Descripcion;

		private string _sAG_Direccion;

		private string _sAG_Tlf;

		public int nAG_AgenciaId
		{
			get
			{
				return this._nAG_AgenciaId;
			}
			set
			{
				this._nAG_AgenciaId = value;
			}
		}

		public int nAG_ID
		{
			get
			{
				return this._nAG_ID;
			}
			set
			{
				this._nAG_ID = value;
			}
		}

		public string sAG_Descripcion
		{
			get
			{
				return this._sAG_Descripcion;
			}
			set
			{
				this._sAG_Descripcion = value;
			}
		}

		public string sAG_Direccion
		{
			get
			{
				return this._sAG_Direccion;
			}
			set
			{
				this._sAG_Direccion = value;
			}
		}

		public string sAG_Tlf
		{
			get
			{
				return this._sAG_Tlf;
			}
			set
			{
				this._sAG_Tlf = value;
			}
		}

		public Agencia()
		{
		}

		public static Agencia getNewAgencia(DataRow dr)
		{
			int num;
			int num1;
			string empty;
			string str;
			string empty1;
			Agencia agencium = new Agencia();
			Agencia agencium1 = agencium;
			if (dr.Table.Columns.Contains("AG_ID"))
			{
				num = (dr.IsNull("AG_ID") ? 0 : int.Parse(dr["AG_ID"].ToString()));
			}
			else
			{
				num = 0;
			}
			agencium1.nAG_ID = num;
			Agencia agencium2 = agencium;
			if (dr.Table.Columns.Contains("AG_AgenciaId"))
			{
				num1 = (dr.IsNull("AG_AgenciaId") ? 0 : int.Parse(dr["AG_AgenciaId"].ToString()));
			}
			else
			{
				num1 = 0;
			}
			agencium2.nAG_AgenciaId = num1;
			Agencia agencium3 = agencium;
			if (dr.Table.Columns.Contains("AG_Descripcion"))
			{
				empty = (dr.IsNull("AG_Descripcion") ? string.Empty : dr["AG_Descripcion"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			agencium3.sAG_Descripcion = empty;
			Agencia agencium4 = agencium;
			if (dr.Table.Columns.Contains("AG_Direccion"))
			{
				str = (dr.IsNull("AG_Direccion") ? string.Empty : dr["AG_Direccion"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			agencium4.sAG_Direccion = str;
			Agencia agencium5 = agencium;
			if (dr.Table.Columns.Contains("AG_Tlf"))
			{
				empty1 = (dr.IsNull("AG_Tlf") ? string.Empty : dr["AG_Tlf"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			agencium5.sAG_Tlf = empty1;
			return agencium;
		}
	}
}