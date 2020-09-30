using IBBAV.Functions;
using IBBAV.WsGlobal;
using IBBAV.WSTedexis;
using System;

namespace IBBAV.Helpers
{
	public class HelperTedexis
	{
		public HelperTedexis()
		{
		}

		public static bool sendSMS(string number, string text)
		{
			bool flag = false;
			int num = 0;
			string str = "58";
			try
			{
				if ((number.StartsWith("0414") || number.StartsWith("0424") || number.StartsWith("0416") || number.StartsWith("0426") ? true : number.StartsWith("0412")))
				{
					if (text.Length > 160)
					{
						throw new Exception("El Mensaje de texto supero los 160 caracteres");
					}
					str = string.Concat(str, number.Substring(1));

                    using (WSTedexis.M4WSIntSR m4WSIntSR = new M4WSIntSR())
                    {
                        m4WSIntSR.sendSMS(IBBAVConfiguration.TedexisPassport, IBBAVConfiguration.TedexisPassword, str, text, out num, out flag);
                    }
                }
			}
			catch (Exception exception)
			{
				string.Concat("Error en el envio del Memsaje: ", exception.Message);
			}
			HelperGlobal.SaveSendMsg(text, str, Origen.NATURAL, num.ToString(), "");
			return flag;
		}
	}
}