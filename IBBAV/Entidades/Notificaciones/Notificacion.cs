using System;
using IBBAV.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IBBAV.Entidades.Notificaciones
{
    [Serializable]
    public class Notificacion : EntidadBase
    {
        private long _NotificacionId;
        private long _DestinoId;
        private long _PaisId;
        private string _PaisNombre;
        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        private string key;

        public long NotificacionId
        {
            get
            {
                return this._NotificacionId;
            }
            set
            {
                this._NotificacionId = value;
            }
        }

        public long DestinoId
        {
            get
            {
                return this._DestinoId;
            }
            set
            {
                this._DestinoId = value;
            }
        }

        public long PaisId
        {
            get
            {
                return this._PaisId;
            }
            set
            {
                this._PaisId = value;
            }
        }

        public string PaisNombre
        {
            get
            {
                return this._PaisNombre;
            }
            set
            {
                this._PaisNombre = value;
            }
        }

        public string FechaInicio
        {
            get
            {
                return this._FechaInicio.ToString("dd - MMM - yyyy");
            }
            set
            {
                this._FechaInicio = Convert.ToDateTime(value);
            }
        }

        public string FechaFin
        {
            get
            {
                return this._FechaFin.ToString("dd - MMM - yyyy");
            }
            set
            {
                this._FechaFin = Convert.ToDateTime(value);
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


        public Notificacion(long NotificacionId,long DestinoId, long PaisId,string PaisNombre, string FechaInicio, string FechaFin)
        {
            this._NotificacionId = NotificacionId;
            this._DestinoId = DestinoId;
            this._PaisId = PaisId;
            this._PaisNombre = PaisNombre;
            this._FechaInicio = Convert.ToDateTime(FechaInicio);
            this._FechaFin = Convert.ToDateTime(FechaFin);
        }

        public Notificacion()
        {
            this._NotificacionId = (long)0;
            this._DestinoId = (long)0;
            this._PaisId = (long)0;
            this._PaisNombre = string.Empty;
            this._FechaInicio = DateTime.Now;
            this._FechaFin = DateTime.Now;
        }

        public static DataTable NuevaNotificacion(long sAF_Id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NotificacionId");
            dt.Columns.Add("DestinoId");
            dt.Columns.Add("PaisId");
            dt.Columns.Add("País");
            dt.Columns.Add("Fecha Salida");
            dt.Columns.Add("Fecha Retorno");

            foreach (DataRow row in HelperNotificacionIBP.DestinoByNotificacion(sAF_Id).Tables[0].Rows)
            {
                dt.Rows.Add(HelperNotificacionIBP.DestinoByNotificacion(sAF_Id).Tables[0].Rows);
            }
            return dt;
        }

        public static Notificacion UltimoDestino (long sAF_Id)
        {
            long num;
            long num1;
            long num2;
            string str;
            DateTime dateTime1;
            DateTime dateTime2;

            Notificacion notificacion = new Notificacion();
            Notificacion notificacion1 = notificacion;

            DataSet ds = HelperNotificacionIBP.DestinoByNotificacion(sAF_Id);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[dt.Rows.Count - 1];

            if (dr.Table.Columns.Contains("NotificacionId"))
            {
                num = (dr.IsNull("NotificacionId") ? (long)0 : long.Parse(dr["NotificacionId"].ToString()));
            }
            else
            {
                num = (long)0;
            }
            notificacion1.NotificacionId = num;
            Notificacion notificacion2 = notificacion;
            if (dr.Table.Columns.Contains("DestinoId"))
            {
                num1 = (dr.IsNull("DestinoId") ? (long)0 : long.Parse(dr["DestinoId"].ToString()));
            }
            else
            {
                num1 = (long)0;
            }
            notificacion2.DestinoId = num1;
            Notificacion notificacion3 = notificacion;
            if (dr.Table.Columns.Contains("PaisId"))
            {
                num2 = (dr.IsNull("PaisId") ? (long)0 : long.Parse(dr["PaisId"].ToString()));
            }
            else
            {
                num2 = (long)0;
            }
            notificacion3.PaisId = num2;
            Notificacion notificacion4 = notificacion;
            if (dr.Table.Columns.Contains("PaisNombre"))
            {
                str = (dr.IsNull("PaisNombre") ? "" : dr["PaisNombre"].ToString());
            }
            else
            {
                str = "";
            }
            notificacion4.PaisNombre = str;
            Notificacion notificacion5 = notificacion;
            if (dr.Table.Columns.Contains("FechaInicio"))
            {
                dateTime1 = (dr.IsNull("FechaInicio") ? DateTime.Now : DateTime.Parse(dr["FechaInicio"].ToString()));
            }
            else
            {
                dateTime1 = DateTime.Now;
            }
            notificacion5.FechaInicio = dateTime1.ToString("dd - MMM - yyyy");
            Notificacion notificacion6 = notificacion;
            if (dr.Table.Columns.Contains("FechaFin"))
            {
                dateTime2 = (dr.IsNull("FechaFin") ? DateTime.Now : DateTime.Parse(dr["FechaFin"].ToString()));
            }
            else
            {
                dateTime2 = DateTime.Now;
            }
            notificacion6.FechaFin = dateTime2.ToString("dd - MMM - yyyy");

            return notificacion;
        }

        public static Notificacion PrimerDestino(long sAF_Id)
        {
            long num;
            long num1;
            long num2;
            string str;
            DateTime dateTime1;
            DateTime dateTime2;

            Notificacion notificacion = new Notificacion();
            Notificacion notificacion1 = notificacion;

            DataSet ds = HelperNotificacionIBP.DestinoByNotificacion(sAF_Id);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            if (dr.Table.Columns.Contains("NotificacionId"))
            {
                num = (dr.IsNull("NotificacionId") ? (long)0 : long.Parse(dr["NotificacionId"].ToString()));
            }
            else
            {
                num = (long)0;
            }
            notificacion1.NotificacionId = num;
            Notificacion notificacion2 = notificacion;
            if (dr.Table.Columns.Contains("DestinoId"))
            {
                num1 = (dr.IsNull("DestinoId") ? (long)0 : long.Parse(dr["DestinoId"].ToString()));
            }
            else
            {
                num1 = (long)0;
            }
            notificacion2.DestinoId = num1;
            Notificacion notificacion3 = notificacion;
            if (dr.Table.Columns.Contains("PaisId"))
            {
                num2 = (dr.IsNull("PaisId") ? (long)0 : long.Parse(dr["PaisId"].ToString()));
            }
            else
            {
                num2 = (long)0;
            }
            notificacion3.PaisId = num2;
            Notificacion notificacion4 = notificacion;
            if (dr.Table.Columns.Contains("PaisNombre"))
            {
                str = (dr.IsNull("PaisNombre") ? "" : dr["PaisNombre"].ToString());
            }
            else
            {
                str = "";
            }
            notificacion4.PaisNombre = str;
            Notificacion notificacion5 = notificacion;
            if (dr.Table.Columns.Contains("FechaInicio"))
            {
                dateTime1 = (dr.IsNull("FechaInicio") ? DateTime.Now : DateTime.Parse(dr["FechaInicio"].ToString()));
            }
            else
            {
                dateTime1 = DateTime.Now;
            }
            notificacion5.FechaInicio = dateTime1.ToString("dd - MMM - yyyy");
            Notificacion notificacion6 = notificacion;
            if (dr.Table.Columns.Contains("FechaFin"))
            {
                dateTime2 = (dr.IsNull("FechaFin") ? DateTime.Now : DateTime.Parse(dr["FechaFin"].ToString()));
            }
            else
            {
                dateTime2 = DateTime.Now;
            }
            notificacion6.FechaFin = dateTime2.ToString("dd - MMM - yyyy");

            return notificacion;
        }

        public static Notificacion getNotificacion(long AF_Id)
        {
            long num;
            long num1;
            long num2;
            string str;
            DateTime dateTime1;
            DateTime dateTime2;
            
            Notificacion notificacion = new Notificacion();
            Notificacion notificacion1 = notificacion;

            DataSet ds = HelperNotificacionIBP.DestinoByNotificacion(AF_Id);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            if (dr.Table.Columns.Contains("NotificacionId"))
            {
                num = (dr.IsNull("NotificacionId") ? (long)0 : long.Parse(dr["NotificacionId"].ToString()));
            }
            else
            {
                num = (long)0;
            }
            notificacion1.NotificacionId = num;
            Notificacion notificacion2 = notificacion;
            if (dr.Table.Columns.Contains("DestinoId"))
            {
                num1 = (dr.IsNull("DestinoId") ? (long)0 : long.Parse(dr["DestinoId"].ToString()));
            }
            else
            {
                num1 = (long)0;
            }
            notificacion2.DestinoId = num1;
            Notificacion notificacion3 = notificacion;
            if (dr.Table.Columns.Contains("PaisId"))
            {
                num2 = (dr.IsNull("PaisId") ? (long)0 : long.Parse(dr["PaisId"].ToString()));
            }
            else
            {
                num2 = (long)0;
            }
            notificacion3.PaisId = num2;
            Notificacion notificacion4 = notificacion;
            if (dr.Table.Columns.Contains("PaisNombre"))
            {
                str = (dr.IsNull("PaisNombre") ? "" : dr["PaisNombre"].ToString());
            }
            else
            {
                str = "";
            }
            notificacion4.PaisNombre = str;
            Notificacion notificacion5 = notificacion;
            if (dr.Table.Columns.Contains("FechaInicio"))
            {
                dateTime1 = (dr.IsNull("FechaInicio") ? DateTime.Now : DateTime.Parse(dr["FechaInicio"].ToString()));
            }
            else
            {
                dateTime1 = DateTime.Now;
            }
            notificacion5.FechaInicio = dateTime1.ToString("dd - MMM - yyyy");
            Notificacion notificacion6 = notificacion;
            if (dr.Table.Columns.Contains("FechaFin"))
            {
                dateTime2 = (dr.IsNull("FechaFin") ? DateTime.Now : DateTime.Parse(dr["FechaFin"].ToString()));
            }
            else
            {
                dateTime2 = DateTime.Now;
            }
            notificacion6.FechaFin = dateTime2.ToString("dd - MMM - yyyy");

            return notificacion;
        }

        public static implicit operator DataRowView(Notificacion v)
        {
            throw new NotImplementedException();
        }        
    }
}