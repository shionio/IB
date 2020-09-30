using System;
using System.Data;

namespace IBBAV.Entidades
{
	[Serializable]
	public class PreguntaCampos
	{
		private int _PRE_PreguntaId;

		private int _PRE_PreguntaIBSId;

		private int _PRO_ProductoId;

		private string _PRE_TituloCampo;

		private string _PRE_TipoCampo;

		private string _PRE_CampoId;

		private int _PRE_Size;

		private bool _PRE_Mask;

		private int _PRE_Order;

		private string _PRE_MsgValidation;

		private string _PRE_Status;

		private string _PRE_ParametroIBS;

		private bool _PRE_Encriptado;

		public string PRE_CampoId
		{
			get
			{
				return this._PRE_CampoId;
			}
			set
			{
				this._PRE_CampoId = value;
			}
		}

		public bool PRE_Encriptado
		{
			get
			{
				return this._PRE_Encriptado;
			}
			set
			{
				this._PRE_Encriptado = value;
			}
		}

		public bool PRE_Mask
		{
			get
			{
				return this._PRE_Mask;
			}
			set
			{
				this._PRE_Mask = value;
			}
		}

		public string PRE_MsgValidation
		{
			get
			{
				return this._PRE_MsgValidation;
			}
			set
			{
				this._PRE_MsgValidation = value;
			}
		}

		public int PRE_Order
		{
			get
			{
				return this._PRE_Order;
			}
			set
			{
				this._PRE_Order = value;
			}
		}

		public string PRE_ParametroIBS
		{
			get
			{
				return this._PRE_ParametroIBS;
			}
			set
			{
				this._PRE_ParametroIBS = value;
			}
		}

		public int PRE_PreguntaIBSId
		{
			get
			{
				return this._PRE_PreguntaIBSId;
			}
			set
			{
				this._PRE_PreguntaIBSId = value;
			}
		}

		public int PRE_PreguntaId
		{
			get
			{
				return this._PRE_PreguntaId;
			}
			set
			{
				this._PRE_PreguntaId = value;
			}
		}

		public int PRE_Size
		{
			get
			{
				return this._PRE_Size;
			}
			set
			{
				this._PRE_Size = value;
			}
		}

		public string PRE_Status
		{
			get
			{
				return this._PRE_Status;
			}
			set
			{
				this._PRE_Status = value;
			}
		}

		public string PRE_TipoCampo
		{
			get
			{
				return this._PRE_TipoCampo;
			}
			set
			{
				this._PRE_TipoCampo = value;
			}
		}

		public string PRE_TituloCampo
		{
			get
			{
				return this._PRE_TituloCampo;
			}
			set
			{
				this._PRE_TituloCampo = value;
			}
		}

		public int PRO_ProductoId
		{
			get
			{
				return this._PRO_ProductoId;
			}
			set
			{
				this._PRO_ProductoId = value;
			}
		}

		public PreguntaCampos()
		{
		}

		public static PreguntaCampos getNewPreguntaCampos(DataRow row)
		{
			int num;
			int num1;
			int num2;
			string empty;
			string str;
			string empty1;
			int num3;
			bool flag;
			int num4;
			string str1;
			string empty2;
			string str2;
			bool flag1;
			PreguntaCampos preguntaCampo = new PreguntaCampos();
			PreguntaCampos preguntaCampo1 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_PreguntaId"))
			{
				num = (row.IsNull("PRE_PreguntaId") ? 0 : int.Parse(row["PRE_PreguntaId"].ToString()));
			}
			else
			{
				num = 0;
			}
			preguntaCampo1.PRE_PreguntaId = num;
			PreguntaCampos preguntaCampo2 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_PreguntaIBSId"))
			{
				num1 = (row.IsNull("PRE_PreguntaIBSId") ? 0 : int.Parse(row["PRE_PreguntaIBSId"].ToString()));
			}
			else
			{
				num1 = 0;
			}
			preguntaCampo2.PRE_PreguntaIBSId = num1;
			PreguntaCampos preguntaCampo3 = preguntaCampo;
			if (row.Table.Columns.Contains("PRO_ProductoId"))
			{
				num2 = (row.IsNull("PRO_ProductoId") ? 0 : int.Parse(row["PRO_ProductoId"].ToString()));
			}
			else
			{
				num2 = 0;
			}
			preguntaCampo3.PRO_ProductoId = num2;
			PreguntaCampos preguntaCampo4 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_TituloCampo"))
			{
				empty = (row.IsNull("PRE_TituloCampo") ? string.Empty : row["PRE_TituloCampo"].ToString());
			}
			else
			{
				empty = string.Empty;
			}
			preguntaCampo4.PRE_TituloCampo = empty;
			PreguntaCampos preguntaCampo5 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_TipoCampo"))
			{
				str = (row.IsNull("PRE_TipoCampo") ? string.Empty : row["PRE_TipoCampo"].ToString());
			}
			else
			{
				str = string.Empty;
			}
			preguntaCampo5.PRE_TipoCampo = str;
			PreguntaCampos preguntaCampo6 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_CampoId"))
			{
				empty1 = (row.IsNull("PRE_CampoId") ? string.Empty : row["PRE_CampoId"].ToString());
			}
			else
			{
				empty1 = string.Empty;
			}
			preguntaCampo6.PRE_CampoId = empty1;
			PreguntaCampos preguntaCampo7 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_Size"))
			{
				num3 = (row.IsNull("PRE_Size") ? 0 : int.Parse(row["PRE_Size"].ToString()));
			}
			else
			{
				num3 = 0;
			}
			preguntaCampo7.PRE_Size = num3;
			PreguntaCampos preguntaCampo8 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_Mask"))
			{
				flag = (row.IsNull("PRE_Mask") ? false : row["PRE_Mask"].ToString().ToUpper().Equals("S"));
			}
			else
			{
				flag = false;
			}
			preguntaCampo8.PRE_Mask = flag;
			PreguntaCampos preguntaCampo9 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_Order"))
			{
				num4 = (row.IsNull("PRE_Order") ? 0 : int.Parse(row["PRE_Order"].ToString()));
			}
			else
			{
				num4 = 0;
			}
			preguntaCampo9.PRE_Order = num4;
			PreguntaCampos preguntaCampo10 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_MsgValidation"))
			{
				str1 = (row.IsNull("PRE_MsgValidation") ? string.Empty : row["PRE_MsgValidation"].ToString());
			}
			else
			{
				str1 = string.Empty;
			}
			preguntaCampo10.PRE_MsgValidation = str1;
			PreguntaCampos preguntaCampo11 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_Status"))
			{
				empty2 = (row.IsNull("PRE_Status") ? string.Empty : row["PRE_Status"].ToString());
			}
			else
			{
				empty2 = string.Empty;
			}
			preguntaCampo11.PRE_Status = empty2;
			PreguntaCampos preguntaCampo12 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_ParametroIBS"))
			{
				str2 = (row.IsNull("PRE_ParametroIBS") ? string.Empty : row["PRE_ParametroIBS"].ToString());
			}
			else
			{
				str2 = string.Empty;
			}
			preguntaCampo12.PRE_ParametroIBS = str2;
			PreguntaCampos preguntaCampo13 = preguntaCampo;
			if (row.Table.Columns.Contains("PRE_Encriptado"))
			{
				flag1 = (row.IsNull("PRE_Encriptado") ? false : row["PRE_Encriptado"].ToString().ToUpper().Equals("S"));
			}
			else
			{
				flag1 = false;
			}
			preguntaCampo13.PRE_Encriptado = flag1;
			return preguntaCampo;
		}
	}
}