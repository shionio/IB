using IBBAV;
using System.Data;
using IBBAV.Entidades.Notificaciones;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
    [Serializable]
    public class GAddNotificacion : GBase
    {
        private IBBAV.Entidades.Notificaciones.Notificacion _Notificacion;

        public string Destino
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

        public string TipoInstrumento
        {
            get;
            set;
        }

        public string NumInstrumento
        {
            get;
            set;
        }

        public string Opcion
       {
            get;
            set;
       }

        public GAddNotificacion(IBBAV.Entidades.Afiliado afi, int sCod)
        {
            base.Afiliado = afi;
            base.sCod = sCod;
        }

        public override string EjecutarAccion()
        {
            string empty = string.Empty;

            if (Opcion.Equals("Notificacion"))
            {
                if (ValidacionesViaje.AgregarNotificacion(base.Afiliado.nAF_Id, this.FechaInicio, this.FechaFin, this.Destino,this.TipoInstrumento,this.NumInstrumento))
                {
                    this.LogNotificacion();
                }
                else
                {
                    throw new IBException("Ya existe una notificación activa.");
                }
            }
            else if (Opcion.Equals("Destino"))
            {
                if (ValidacionesViaje.AgregarDestino(base.Afiliado.nAF_Id, Convert.ToDateTime(this.FechaInicio),Convert.ToDateTime(this.FechaFin), this.Destino))
                {
                    this.LogNotificacion();
                }
                else
                {
                    throw new IBException("No cumple los requisitos para agregar un nuevo destino");
                }
            }
            else if (Opcion.Equals("EETDC"))
            {                
                    this.LogNotificacion();
            }

            return empty;
        }

        private bool LogNotificacion()
        {
            _Notificacion = Notificacion.getNotificacion(base.Afiliado.nAF_Id);
            HelperEnvioCorreo.BuscarCamposCorreo(base.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, new decimal(0), string.Empty, this._Notificacion.FechaInicio, string.Empty, string.Empty, string.Empty, this._Notificacion.PaisNombre, string.Empty, this._Notificacion.FechaFin, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            bool flag = false;
            try
            {
                DataLog dataLog = new DataLog()
                {
                    NAF_Id = base.Afiliado.nAF_Id,
                    SAF_NombreUsuario = (string.IsNullOrEmpty(base.Afiliado.sAF_NombreUsuario) ? string.Empty : base.Afiliado.sAF_NombreUsuario),
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString()
                };

                if (Opcion.Equals("Notificacion"))
                {
                    dataLog.SCod_Trans = "ANOVA";
                    dataLog.SConcepto = string.Concat("Agregar Notificación de Viaje");
                }
                else if (Opcion.Equals("Destino"))
                {
                    dataLog.SCod_Trans = "ANODA";
                    dataLog.SConcepto = string.Concat("Agregar Destino");
                }
                else if (Opcion.Equals("EETDC"))
                {
                    throw new IBException("SE EJECUTO LA ACCION");
                }

                dataLog.SAF_IP = (string.IsNullOrEmpty(base.Afiliado.sIP) ? string.Empty : base.Afiliado.sIP);
                dataLog.SBanco = string.Concat("Salida: ", _Notificacion.FechaInicio); ;
                dataLog.SCuenta_Origen = string.Concat("Notificación ID: ", _Notificacion.NotificacionId);
                dataLog.SCuenta_Destino = string.Concat("Destino ID: ", _Notificacion.DestinoId);
                dataLog.SMonto = string.Empty;
                dataLog.STipo_Tarjeta = string.Concat("Retorno: ", _Notificacion.FechaFin);
                dataLog.SBeneficiario = string.Empty;
                dataLog.SCedula_Id_B = string.Empty;
                dataLog.SSerial_Chequera = string.Empty;
                dataLog.SCheques = string.Empty;
                dataLog.STitular = string.Empty;
                dataLog.ICantidad = 0;
                dataLog.SReferencia = string.Empty;
                dataLog.SMotivo_Suspension = string.Empty;
                dataLog.SDir_Envio_Chequera = string.Empty;
                flag = HelperGlobal.LogTransAdd(dataLog);
            }
            catch (IBException bException)
            {
            }
            return flag;
        }
    }
}