using IBBAV.Helpers;
using System;
namespace IBBAV.Entidades.Notificaciones
{
    [Serializable]
    public class VerificaNotificacion
    {
        private string _AF_Viaje;
        HelperNotificacionIBP helper = new HelperNotificacionIBP();

        public string AF_Viaje
        {
            get
            {
                return this._AF_Viaje;
            }
            set
            {
                this._AF_Viaje = value;
            }
        }


        public VerificaNotificacion()
        {
            this._AF_Viaje = string.Empty;
        }

        public bool NotificacionActiva(string af_Viaje)
        {
            bool activa;

            if (af_Viaje == "S")
            {
                activa = true;
            }
            else
            {
                activa = false;
            }
            return activa;
        }

        public static string getAfiliadoNotifi(string af_Viaje)
        {
            string str = "";
            VerificaNotificacion verifica = new VerificaNotificacion();
            if ((af_Viaje != "") && (af_Viaje != null))
            {
                str = af_Viaje;
            }
            verifica.AF_Viaje = str;
            return str;
        }
    }
}