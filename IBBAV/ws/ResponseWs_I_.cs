using System;
using System.Runtime.CompilerServices;

namespace IBBAV.ws
{
	public class ResponseWs<I>
	{
		public int CodError
		{
			get;
			set;
		}

		public I Data
		{
			get;
			set;
		}

		public string Resultado
		{
			get;
			set;
		}

		public ResponseWs()
		{
		}
	}
}