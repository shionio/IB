using System;
using System.Data;

namespace IBBAV.Entidades.Notificaciones
{
    [Serializable]
    public class Paises
    {
        private long _Id_Pais;
        private string _Nombre;
        private string key;

        public long sId_Pais
        {
            get
            {
                return this._Id_Pais;
            }
            set
            {
                this._Id_Pais = value;
            }
        }
        public string sNombre
        {
            get
            {
                return this._Nombre;
            }
            set
            {
                this._Nombre = value;
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

        public Paises()
        {
        }

        public static Paises getNewPais(DataRow dr)
        {
            long num;
            string str;
            Paises pais = new Paises();
            Paises pais1 = pais;
            if (dr.Table.Columns.Contains("Id_Pais"))
            {
                num = (dr.IsNull("Id_Pais") ? (long)0 : long.Parse(dr["Id_Pais"].ToString()));
            }
            else
            {
                num = (long)0;
            }
            pais1._Id_Pais = num;
            Paises pais2 = pais;
           
            if (dr.Table.Columns.Contains("Nombre"))
            {
                str = (dr.IsNull("Nombre") ? string.Empty : dr["Nombre"].ToString());
            }
            else
            {
                str = string.Empty;
            }
            pais2._Nombre = str;
            return pais;
        }
    }
}