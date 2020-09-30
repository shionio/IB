using System;

namespace IBBAV.Entidades
{
	public class DataLog
	{
		private long nAF_Id;

		private string sAF_NombreUsuario;

		private DateTime dtFecha_Trans;

		private string sTime_Trans;

		private string sAF_IP;

		private string sCod_Trans;

		private string sReferencia;

		private string sCuenta_Origen;

		private string sCuenta_Destino;

		private string sMonto;

		private string sBanco;

		private string sTipo_Tarjeta;

		private string sBeneficiario;

		private string sCedula_Id_B;

		private string sSerial_Chequera;

		private string sCheques;

		private string sTitular;

		private int iCantidad;

		private string sConcepto;

		private string sMotivo_Suspension;

		private string sDir_Envio_Chequera;

        private string sNotificacion;

        private string sDestino;

		public DateTime DtFecha_Trans
		{
			get
			{
				return this.dtFecha_Trans;
			}
			set
			{
				this.dtFecha_Trans = value;
			}
		}

		public int ICantidad
		{
			get
			{
				return this.iCantidad;
			}
			set
			{
				this.iCantidad = value;
			}
		}

		public long NAF_Id
		{
			get
			{
				return this.nAF_Id;
			}
			set
			{
				this.nAF_Id = value;
			}
		}

		public string SAF_IP
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sAF_IP) ? this.sAF_IP : string.Empty);
				return str;
			}
			set
			{
				this.sAF_IP = value;
			}
		}

		public string SAF_NombreUsuario
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sAF_NombreUsuario) ? this.sAF_NombreUsuario : string.Empty);
				return str;
			}
			set
			{
				this.sAF_NombreUsuario = value;
			}
		}

		public string SBanco
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sBanco) ? this.sBanco : string.Empty);
				return str;
			}
			set
			{
				this.sBanco = value;
			}
		}

		public string SBeneficiario
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sBeneficiario) ? this.sBeneficiario : string.Empty);
				return str;
			}
			set
			{
				this.sBeneficiario = value;
			}
		}

		public string SCedula_Id_B
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sCedula_Id_B) ? this.sCedula_Id_B : string.Empty);
				return str;
			}
			set
			{
				this.sCedula_Id_B = value;
			}
		}

		public string SCheques
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sCheques) ? this.sCheques : string.Empty);
				return str;
			}
			set
			{
				this.sCheques = value;
			}
		}

		public string SCod_Trans
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sCod_Trans) ? this.sCod_Trans : string.Empty);
				return str;
			}
			set
			{
				this.sCod_Trans = value;
			}
		}

		public string SConcepto
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sConcepto) ? this.sConcepto : string.Empty);
				return str;
			}
			set
			{
				this.sConcepto = value;
			}
		}

		public string SCuenta_Destino
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sCuenta_Destino) ? this.sCuenta_Destino : string.Empty);
				return str;
			}
			set
			{
				this.sCuenta_Destino = value;
			}
		}

		public string SCuenta_Origen
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sCuenta_Origen) ? this.sCuenta_Origen : string.Empty);
				return str;
			}
			set
			{
				this.sCuenta_Origen = value;
			}
		}

		public string SDir_Envio_Chequera
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sDir_Envio_Chequera) ? this.sDir_Envio_Chequera : string.Empty);
				return str;
			}
			set
			{
				this.sDir_Envio_Chequera = value;
			}
		}

		public string SMonto
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sMonto) ? this.sMonto : string.Empty);
				return str;
			}
			set
			{
				this.sMonto = value;
			}
		}

		public string SMotivo_Suspension
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sMotivo_Suspension) ? this.sMotivo_Suspension : string.Empty);
				return str;
			}
			set
			{
				this.sMotivo_Suspension = value;
			}
		}

		public string SReferencia
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sReferencia) ? this.sReferencia : string.Empty);
				return str;
			}
			set
			{
				this.sReferencia = value;
			}
		}

		public string SSerial_Chequera
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sSerial_Chequera) ? this.sSerial_Chequera : string.Empty);
				return str;
			}
			set
			{
				this.sSerial_Chequera = value;
			}
		}

		public string STime_Trans
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sTime_Trans) ? this.sTime_Trans : string.Empty);
				return str;
			}
			set
			{
				this.sTime_Trans = value;
			}
		}

		public string STipo_Tarjeta
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sTipo_Tarjeta) ? this.sTipo_Tarjeta : string.Empty);
				return str;
			}
			set
			{
				this.sTipo_Tarjeta = value;
			}
		}

		public string STitular
		{
			get
			{
				string str;
				str = (!string.IsNullOrEmpty(this.sTitular) ? this.sTitular : string.Empty);
				return str;
			}
			set
			{
				this.sTitular = value;
			}
		}

		public DataLog()
		{
		}
	}
}