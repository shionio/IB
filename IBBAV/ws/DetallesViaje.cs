using IBBAV.Entidades;
using IBBAV.Functions;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace IBBAV.ws
{
    public class DetallesViaje : EntidadBase
    {

        public long NotificacionId
        {
            get;
            set;
        }

        public long DestinoId
        {
            get;
            set;
        }

        public long PaisId
        {
            get;
            set;
        }

        public string País
        {
            get;
            set;
        }

        public string FechaInicio
        {
            get;
            set;
        }

        public string FechaFin
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public DetallesViaje()
        {
        }

        public static DetallesViaje getDetalles(DataRow dr)
        {
            DetallesViaje detalles = new DetallesViaje()
            {
                NotificacionId = EntidadBase.IsLong(dr,"NotificacionId"),
                DestinoId = EntidadBase.IsLong(dr,"DestinoId"),
                PaisId = EntidadBase.IsLong(dr,"PaisId"),
                País = EntidadBase.IsString(dr, "PaisNombre"),
                FechaInicio = EntidadBase.IsString(dr,"FechaInicio"),
                FechaFin = EntidadBase.IsString(dr,"FechaFin")
            };           
            long destino = detalles.DestinoId;
            detalles.Key = CryptoUtils.EncryptMD5(destino.ToString());
            return detalles;
        }
    }
}
    