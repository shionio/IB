using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;

namespace IBBAV.Helpers
{
	public class HelperHidrocapital
	{
		public HelperHidrocapital()
		{
		}

		public static HelperHidrocapital.Resultado ConsultarRecibos(string contrato)
		{
			HelperHidrocapital.Resultado resultado = new HelperHidrocapital.Resultado();
			WebRequest webRequest = null;
			try
			{
				webRequest = WebRequest.Create("http://inorca.es:28085/geasws/rest/cobranza/pago");
				webRequest.Method = "POST";
				webRequest.ContentType = "application/json";
				StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				streamWriter.WriteLine(javaScriptSerializer.Serialize(new HelperHidrocapital.Operacion()
				{
					OPERACION = "1",
					CANAL = "I",
					CONTRATO = contrato,
					BANCO = "BAV",
					CAJERO = "BAV01",
					BCID = "1"
				}));
				streamWriter.Close();
				using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
				{
					resultado = javaScriptSerializer.Deserialize<HelperHidrocapital.Resultado>(streamReader.ReadToEnd());
				}
			}
			catch
			{
			}
			return resultado;
		}

		[Serializable]
		public class Avisos
		{
			public string CLIENTE
			{
				get;
				set;
			}

			public string CONTRATO
			{
				get;
				set;
			}

			public string DIRECCION
			{
				get;
				set;
			}

			public string IMPMIN
			{
				get;
				set;
			}

			public string IMPMORA
			{
				get;
				set;
			}

			public string IMPTOTAL
			{
				get;
				set;
			}

			public string MONEDA
			{
				get;
				set;
			}

			public string NOMBRE
			{
				get;
				set;
			}

			public HelperHidrocapital.Recibos[] RECIBOS
			{
				get;
				set;
			}

			public string REFPAGO
			{
				get;
				set;
			}

			public string TIPOAVISO
			{
				get;
				set;
			}

			public string TIPOIMP
			{
				get;
				set;
			}

			public Avisos()
			{
			}
		}

		[Serializable]
		public class Lineas_Detalle
		{
			public string CANTIDAD
			{
				get;
				set;
			}

			public string DESCRIPCION
			{
				get;
				set;
			}

			public string IMPORTE
			{
				get;
				set;
			}

			public string UNIDMED
			{
				get;
				set;
			}

			public Lineas_Detalle()
			{
			}
		}

		[Serializable]
		public class Operacion
		{
			public string BANCO
			{
				get;
				set;
			}

			public string BCID
			{
				get;
				set;
			}

			public string CAJERO
			{
				get;
				set;
			}

			public string CANAL
			{
				get;
				set;
			}

			public string CONTRATO
			{
				get;
				set;
			}

			public string OPERACION
			{
				get;
				set;
			}

			public Operacion()
			{
			}
		}

		[Serializable]
		public class Recibos
		{
			public string CODIGOFACT
			{
				get;
				set;
			}

			public string CONSUMO
			{
				get;
				set;
			}

			public string CONTRATO
			{
				get;
				set;
			}

			public string EJERCICIO
			{
				get;
				set;
			}

			public string FECHAFACT
			{
				get;
				set;
			}

			public string FECHAVTO
			{
				get;
				set;
			}

			public string IMPMORA
			{
				get;
				set;
			}

			public string IMPTOTAL
			{
				get;
				set;
			}

			public HelperHidrocapital.Lineas_Detalle[] LINEAS_DETALLE
			{
				get;
				set;
			}

			public string MONEDA
			{
				get;
				set;
			}

			public string PERIODO
			{
				get;
				set;
			}

			public string TIPOIMP
			{
				get;
				set;
			}

			public Recibos()
			{
			}
		}

		[Serializable]
		public class Resultado
		{
			public HelperHidrocapital.Avisos[] AVISOS
			{
				get;
				set;
			}

			public string BANCO
			{
				get;
				set;
			}

			public string BCID
			{
				get;
				set;
			}

			public string CAJERO
			{
				get;
				set;
			}

			public string CANAL
			{
				get;
				set;
			}

			public string CLIENTE
			{
				get;
				set;
			}

			public string CODIGO_ERROR
			{
				get;
				set;
			}

			public string NOMBRE
			{
				get;
				set;
			}

			public Resultado()
			{
			}
		}
	}
}