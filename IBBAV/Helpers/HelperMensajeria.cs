using IBBAV.WsMensajeria;
using System;
using static IBBAV.WsMensajeria.SendMsgCompletedEventArgs;

namespace IBBAV.Helpers
{
    public class HelperMensajeria
    {
        public HelperMensajeria()
        {
        }

        public static bool SendMsg(string destino, string mensaje)
        {
            bool flag = false;
            try
            {
                using (Service service = new Service())
                {
                    service.SendMsg(mensaje, destino, "NATURAL");
                }
                flag = true;
            }
            catch (Exception exception)
            {
            }
            return flag;
        }
    }
}