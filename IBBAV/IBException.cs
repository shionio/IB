using IBBAV.Helpers;
using System;
using System.Data.SqlClient;

namespace IBBAV
{
	public class IBException : Exception
	{
		private string returnCode;

		private string codigoSistema;

		private string iBMessage;

		public string CodigoSistema
		{
			get
			{
				return this.codigoSistema;
			}
			set
			{
				this.codigoSistema = value;
			}
		}

		public string IBMessage
		{
			get
			{
				return this.iBMessage;
			}
			set
			{
				this.iBMessage = value;
			}
		}

		public bool IsErrorIB
		{
			get
			{
				bool flag;
				if (this.CodigoSistema.Equals("SQLIB"))
				{
					flag = ((this.ReturnCode.Equals("8001") || this.ReturnCode.Equals("8002") || this.ReturnCode.Equals("8003") || this.ReturnCode.Equals("8004") || this.ReturnCode.Equals("8005") || this.ReturnCode.Equals("8006") || this.ReturnCode.Equals("8007") || this.ReturnCode.Equals("9997") ? false : !this.ReturnCode.Equals("9998")) ? this.ReturnCode.Equals("9999") : true);
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		public string ReturnCode
		{
			get
			{
				return this.returnCode;
			}
			set
			{
				this.returnCode = value;
			}
		}

		public IBException()
		{
		}

		public IBException(Exception ex) : base(ex.Message)
		{
		}

		public IBException(SqlException ex) : base(ex.Message)
		{
		}

		public IBException(string mensaje) : base(mensaje)
		{
			this.IBMessage = mensaje;
		}

		public IBException(string returnCode, string codigoSistema)
		{   
            int num = 0;
			if (int.TryParse(returnCode, out num))
			{
                this.init(num, codigoSistema);
			}
			else if ((returnCode.Contains("403") || returnCode.Contains("404") || returnCode.Contains("500") ? true : returnCode.Contains("501")))
			{
				this.init(9997, "SQLIB");
			}
			else
			{
				this.init(9999, "SQLIB");
			}
		}

		public IBException(int returnCode, string codigoSistema)
		{
			this.init(returnCode, codigoSistema);
		}

		public void init(int returnCode, string codigoSistema)
		{
			ErrorIBS errorIB = new ErrorIBS();
			this.returnCode = returnCode.ToString();
			this.codigoSistema = codigoSistema;
			try
			{
				errorIB = HelperErrores.ErrorPhoenixGet(returnCode, codigoSistema);
				this.iBMessage = (string.IsNullOrEmpty(errorIB.EIB_MensajeCliente) ? HelperErrores.ErrorPhoenixGet(9999, "SQLIB").EIB_MensajeCliente : errorIB.EIB_MensajeCliente);
				if (this.iBMessage.Contains("9997"))
				{
					this.iBMessage = this.iBMessage.Replace("9997", this.returnCode);
				}
				if (this.iBMessage.Contains("9999"))
				{
					this.iBMessage = this.iBMessage.Replace("9999", this.returnCode);
				}
			}
			catch (Exception exception)
			{
				this.iBMessage = exception.Message;
			}
			errorIB = null;
		}
	}
}