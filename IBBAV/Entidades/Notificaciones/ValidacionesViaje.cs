using IBBAV.Helpers;
using System;

namespace IBBAV.Entidades.Notificaciones
{
    public class ValidacionesViaje
    {
        public ValidacionesViaje()
        {
        }

        public static bool AgregarNotificacion(long afiliado, string inicio, string fin, string paisDestino, string TipoIns,string NumIns)
        {
            bool flag = false;
            try
            {
                if (!HelperNotificacionIBP.VerificaNotificacion(afiliado))
                {
                    flag = HelperNotificacionIBP.NotificacionAdd(afiliado, inicio, fin, paisDestino,TipoIns,NumIns);
                }

            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        public static bool AgregarDestino(long afiliado, DateTime inicio, DateTime fin, string paisDestino)
        {
            bool flag = false;
            try
            {
                if (HelperNotificacionIBP.ValidaDestinos(afiliado))
                {
                    flag = HelperNotificacionIBP.DestinosAdd(afiliado, inicio, fin, paisDestino);
                }
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        public bool AfiliadoRestricc(long afiliado, string dirIP, string decIP, string TipoIns, string NumIns)
        {
            bool flag = false;
            try
            {
                string restriccion = HelperNotificacionIBP.RestriccionVerify(afiliado);

                if (restriccion.Equals("N"))
                {
                    HelperNotificacionIBP.ResticcionAdd(afiliado, dirIP, decIP, TipoIns, NumIns);
                    flag = true;
                }
            }
            catch (IBException bException)
            {
            }

            return flag;
        }

    }
}