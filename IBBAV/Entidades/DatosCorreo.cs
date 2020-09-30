using System;
using System.Data;

namespace IBBAV.Entidades
{
	public class DatosCorreo
	{
		private string _DC_CodTrans;

		private string _DC_Destino;

		private string _DC_Subject;

		private string _DC_Contenido;

		public string DC_CodTrans
		{
			get
			{
				return this._DC_CodTrans;
			}
			set
			{
				this._DC_CodTrans = value;
			}
		}

		public string DC_Contenido
		{
			get
			{
				return this._DC_Contenido;
			}
			set
			{
				this._DC_Contenido = value;
			}
		}

		public string DC_Destino
		{
			get
			{
				return this._DC_Destino;
			}
			set
			{
				this._DC_Destino = value;
			}
		}

		public string DC_Subject
		{
			get
			{
				return this._DC_Subject;
			}
			set
			{
				this._DC_Subject = value;
			}
		}

		public DatosCorreo()
		{
		}

		public static DatosCorreo getNewDatosCorreo(DataRow dr)
		{
			string empty;
			string str;
			string empty1;
			string str1;
			DatosCorreo datosCorreo = new DatosCorreo();
			DatosCorreo datosCorreo1 = datosCorreo;
			if (dr.Table.Columns.Contains("DC_CodTrans"))
			{
				empty = (dr.IsNull("DC_CodTrans") ? string.Empty : dr["DC_CodTrans"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			datosCorreo1.DC_CodTrans = empty;
			DatosCorreo datosCorreo2 = datosCorreo;
			if (dr.Table.Columns.Contains("DC_Destino"))
			{
				str = (dr.IsNull("DC_Destino") ? string.Empty : dr["DC_Destino"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			datosCorreo2.DC_Destino = str;
			DatosCorreo datosCorreo3 = datosCorreo;
			if (dr.Table.Columns.Contains("DC_Subject"))
			{
				empty1 = (dr.IsNull("DC_Subject") ? string.Empty : dr["DC_Subject"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			datosCorreo3.DC_Subject = empty1;
			DatosCorreo datosCorreo4 = datosCorreo;
			if (dr.Table.Columns.Contains("DC_Contenido"))
			{
				str1 = (dr.IsNull("DC_Contenido") ? string.Empty : dr["DC_Contenido"].ToString());
			}
			else
			{
				str1 = string.Empty;
			}
			datosCorreo4.DC_Contenido = str1;
			return datosCorreo;
		}
	}
}