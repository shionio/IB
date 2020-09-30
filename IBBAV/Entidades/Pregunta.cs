using System;
using System.Collections.Generic;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class Pregunta
	{
		private string _PRO_Nemotecnico;

		private string _PRO_Titulo;

		private string _PRO_Status;

		private List<PreguntaCampos> _ListaPreguntaCampos;

		public List<PreguntaCampos> ListaPreguntaCampos
		{
			get
			{
				return this._ListaPreguntaCampos;
			}
			set
			{
				this._ListaPreguntaCampos = value;
			}
		}

		public string PRO_Nemotecnico
		{
			get
			{
				return this._PRO_Nemotecnico;
			}
			set
			{
				this._PRO_Nemotecnico = value;
			}
		}

		public string PRO_Status
		{
			get
			{
				return this._PRO_Status;
			}
			set
			{
				this._PRO_Status = value;
			}
		}

		public string PRO_Titulo
		{
			get
			{
				return this._PRO_Titulo;
			}
			set
			{
				this._PRO_Titulo = value;
			}
		}

		public Pregunta()
		{
			this.ListaPreguntaCampos = new List<PreguntaCampos>();
		}

		public static Pregunta getNewPregunta(DataRow row)
		{
			string empty;
			string str;
			string empty1;
			Pregunta preguntum = new Pregunta();
			Pregunta preguntum1 = preguntum;
			if (row.Table.Columns.Contains("PRO_Nemotecnico"))
			{
				empty = (row.IsNull("PRO_Nemotecnico") ? string.Empty : row["PRO_Nemotecnico"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			preguntum1.PRO_Nemotecnico = empty;
			Pregunta preguntum2 = preguntum;
			if (row.Table.Columns.Contains("PRO_Titulo"))
			{
				str = (row.IsNull("PRO_Titulo") ? string.Empty : row["PRO_Titulo"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			preguntum2.PRO_Titulo = str;
			Pregunta preguntum3 = preguntum;
			if (row.Table.Columns.Contains("PRO_Status"))
			{
				empty1 = (row.IsNull("PRO_Status") ? string.Empty : row["PRO_Status"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			preguntum3.PRO_Status = empty1;
			return preguntum;
		}
	}
}