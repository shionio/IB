using System;
using System.Data;

namespace IBBAV.Entidades
{
    public class TransMaxMin : EntidadBase
    {
        private string _MD_Desc_Trans;

        private string _MD_Cta_Administrativa1;

        private string _MD_Cta_administrativa2;

        private string _MD_Cod_Trans;

        private decimal _MD_Mto_Min_Bco;

        private decimal _MD_Mto_Max_Bco;

        private decimal _MD_Mto_Limite_Diario;

        private decimal _MD_Mto_Limite_Trans;

        private decimal _MD_Mto_Comision;

        private int _MD_Tipo_Seguridad;

        public string MD_Cod_Trans
        {
            get
            {
                return this._MD_Cod_Trans;
            }
            set
            {
                this._MD_Cod_Trans = value;
            }
        }

        public string MD_Cta_Administrativa1
        {
            get
            {
                return this._MD_Cta_Administrativa1;
            }
            set
            {
                this._MD_Cta_Administrativa1 = value;
            }
        }

        public string MD_Cta_administrativa2
        {
            get
            {
                return this._MD_Cta_administrativa2;
            }
            set
            {
                this._MD_Cta_administrativa2 = value;
            }
        }

        public string MD_Desc_Trans
        {
            get
            {
                return this._MD_Desc_Trans;
            }
            set
            {
                this._MD_Desc_Trans = value;
            }
        }

        public decimal MD_Mto_Comision
        {
            get
            {
                return this._MD_Mto_Comision;
            }
            set
            {
                this._MD_Mto_Comision = value;
            }
        }

        public decimal MD_Mto_Limite_Diario
        {
            get
            {
                return this._MD_Mto_Limite_Diario;
            }
            set
            {
                this._MD_Mto_Limite_Diario = value;
            }
        }

        public decimal MD_Mto_Limite_Trans
        {
            get
            {
                return this._MD_Mto_Limite_Trans;
            }
            set
            {
                this._MD_Mto_Limite_Trans = value;
            }
        }

        public decimal MD_Mto_Max_Bco
        {
            get
            {
                return this._MD_Mto_Max_Bco;
            }
            set
            {
                this._MD_Mto_Max_Bco = value;
            }
        }

        public decimal MD_Mto_Min_Bco
        {
            get
            {
                return this._MD_Mto_Min_Bco;
            }
            set
            {
                this._MD_Mto_Min_Bco = value;
            }
        }

        public int MD_Tipo_Seguridad
        {
            get
            {
                return this._MD_Tipo_Seguridad;
            }
            set
            {
                this._MD_Tipo_Seguridad = value;
            }
        }

        public TransMaxMin()
        {
        }

        public static TransMaxMin getNewTransMaxMin(DataRow dr)
        {
            string str;
            string str1;
            string str2;
            string str3;
            decimal num;
            decimal num1;
            decimal num2;
            decimal num3;
            decimal num4;
            TransMaxMin transMaxMin = new TransMaxMin();
            TransMaxMin transMaxMin1 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Desc_Trans"))
            {
                str = (dr.IsNull("MD_Desc_Trans") ? "" : dr["MD_Desc_Trans"].ToString());
            }
            else
            {
                str = "";
            }
            transMaxMin1.MD_Desc_Trans = str;
            TransMaxMin transMaxMin2 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Cta_Administrativa1"))
            {
                str1 = (dr.IsNull("MD_Cta_Administrativa1") ? "" : dr["MD_Cta_Administrativa1"].ToString());
            }
            else
            {
                str1 = "";
            }
            transMaxMin2.MD_Cta_Administrativa1 = str1;
            TransMaxMin transMaxMin3 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Cta_administrativa2"))
            {
                str2 = (dr.IsNull("MD_Cta_administrativa2") ? "" : dr["MD_Cta_administrativa2"].ToString());
            }
            else
            {
                str2 = "";
            }
            transMaxMin3.MD_Cta_administrativa2 = str2;
            TransMaxMin transMaxMin4 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Cod_Trans"))
            {
                str3 = (dr.IsNull("MD_Cod_Trans") ? "" : dr["MD_Cod_Trans"].ToString());
            }
            else
            {
                str3 = "";
            }
            transMaxMin4.MD_Cod_Trans = str3;
            TransMaxMin transMaxMin5 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Mto_Min_Bco"))
            {
                num = (dr.IsNull("MD_Mto_Min_Bco") ? new decimal(0) : decimal.Parse(dr["MD_Mto_Min_Bco"].ToString()));
            }
            else
            {
                num = new decimal(0);
            }
            transMaxMin5.MD_Mto_Min_Bco = num;
            TransMaxMin transMaxMin6 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Mto_Max_Bco"))
            {
                num1 = (dr.IsNull("MD_Mto_Max_Bco") ? new decimal(0) : decimal.Parse(dr["MD_Mto_Max_Bco"].ToString()));
            }
            else
            {
                num1 = new decimal(0);
            }
            transMaxMin6.MD_Mto_Max_Bco = num1;
            TransMaxMin transMaxMin7 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Mto_Limite_Diario"))
            {
                num2 = (dr.IsNull("MD_Mto_Limite_Diario") ? new decimal(0) : decimal.Parse(dr["MD_Mto_Limite_Diario"].ToString()));
            }
            else
            {
                num2 = new decimal(0);
            }
            transMaxMin7.MD_Mto_Limite_Diario = num2;
            TransMaxMin transMaxMin8 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Mto_Limite_Trans"))
            {
                num3 = (dr.IsNull("MD_Mto_Limite_Trans") ? new decimal(0) : decimal.Parse(dr["MD_Mto_Limite_Trans"].ToString()));
            }
            else
            {
                num3 = new decimal(0);
            }
            transMaxMin8.MD_Mto_Limite_Trans = num3;
            TransMaxMin transMaxMin9 = transMaxMin;
            if (dr.Table.Columns.Contains("MD_Comision"))
            {
                num4 = (dr.IsNull("MD_Comision") ? new decimal(0) : decimal.Parse(dr["MD_Comision"].ToString()));
            }
            else
            {
                num4 = new decimal(0);
            }
            transMaxMin9.MD_Mto_Comision = num4;
            transMaxMin.MD_Tipo_Seguridad = EntidadBase.IsInt(dr, "MD_Tipo_Seguridad");
            return transMaxMin;
        }      
	}
}