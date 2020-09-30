using IBBAV.WsTransaccion;
using System;

namespace IBBAV.Helpers
{
	public class HelperTransaccion
	{
		private static IBBAV.WsTransaccion.WsTransaccion ws
		{
			get
			{
				return new IBBAV.WsTransaccion.WsTransaccion();
			}
		}

		public HelperTransaccion()
		{
		}

		public static bool AcumuladorTransVerifyUpdate(bool verify, long AF_Id, decimal Monto, int sCod, string codtrans)
		{
			bool flag = false;
			try
			{
				flag = (!verify ? HelperTransaccion.ws.AcumuladorTransAddUpdate(AF_Id, Monto, sCod, codtrans) : HelperTransaccion.ws.AcumuladorTransVerify(AF_Id, Monto, sCod, codtrans));
			}
			catch (Exception exception)
			{
			}
			return flag;
		}

        public static string GetValorUnidadCuenta(string tipo)
        {
            string valor = "";
            try
            {
                //valor = (HelperTransaccion.ws.GetValorUnidadCuenta(tipo));
            }
            catch (Exception exception)
            {
            }
            return valor;
        }
    }
}