using System.Data;

namespace IBBAV.Entidades.Notificaciones
{
    public class IP_Nacional
    {
        protected string _red;
        protected string _cidr;
        protected string _rango;

        public string vRed
        {
            get
            {
                return this._red;
            }
            set
            {
                this._red = value;
            }
        }

        public string vCidr
        {
            get
            {
                return this._cidr;
            }

            set
            {
                this._cidr = value;
            }
        }

        public string vRango
        {
            get
            {
                return this._rango;
            }
            set
            {
                this._rango = value;
            }
        }

        public IP_Nacional()
        {

        }

        public static IP_Nacional GetIP_Nacional(DataRow dr)
        {
            string str;
            string str1;
            string str2;
            IP_Nacional ipS = new IP_Nacional();
            IP_Nacional ipS1 = ipS;
            if (dr.Table.Columns.Contains("DirecionRed"))
            {
                str = (dr.IsNull("DirecionRed") ? string.Empty :dr["DirecionRed"].ToString());
            }
            else
            {
                str = string.Empty;
            }
            ipS1._red = str;
            IP_Nacional ipS2 = ipS;
            if (dr.Table.Columns.Contains("Prefijo"))
            {
                str1 = (dr.IsNull("Prefijo") ? string.Empty : dr["Prefijo"].ToString());
            }
            else
            {
                str1 = string.Empty;
            }
            ipS2._cidr = str1;
            IP_Nacional ipS3 = ipS;
            if ((str != "") && (str1 != ""))
            {
                str2 = str + str1;
            }
            else
            {
                str2 = string.Empty;
            }
            ipS3._rango = str2;
            return ipS;
        }
    }
}