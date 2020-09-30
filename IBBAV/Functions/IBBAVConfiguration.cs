using System;
using System.Collections.Specialized;
using System.Configuration;

namespace IBBAV.Functions
{
	public class IBBAVConfiguration
	{
		public static string ConfirmarTransaccion
		{
			get
			{
				return ConfigurationManager.AppSettings["ConfirmarTransaccion"];
			}
		}

		public static string ConnectionString
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["ConnectionString"]);
			}
		}

		public static string CorreoAtencionCliente
		{
			get
			{
				return ConfigurationManager.AppSettings["CorreoAtencionCliente"];
			}
		}

		public static string CurrentBankCode
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["CodigoBancoActual"]);
			}
		}

		public static string CurrentBankName
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["NombreBancoActual"]);
			}
		}

		public static string GetHostName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		public static string GetMsgBolivarFuerte
		{
			get
			{
				return "Los montos reflejados en los Movimientos, solicitados por el cliente a través del Servicio InternetBanking de BAV, correspondientes a los periodos anteriores al mes de Enero del año 2008, estan indicados en Bolivares no re-expresados, dentro del marco del Decreto con Rango Valor y Fuerza de Ley de Reconversión Monetaria y la normativa dictada por el Banco Central de Venezuela.";
			}
		}

		public static string KeyIBS
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["KeyIBS"]);
			}
		}

		public static string MailNameSpace
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["ConfigNameSpace"]);
			}
		}

		public static string MailPassword
		{
			get
			{
				return ConfigurationManager.AppSettings["SendPassword"];
			}
		}

		public static string MailPort
		{
			get
			{
				return ConfigurationManager.AppSettings["SmtpServerPort"];
			}
		}

		public static string MailRemitente
		{
			get
			{
				return ConfigurationManager.AppSettings["Remitente"];
			}
		}

		public static string MailSendUsing
		{
			get
			{
				return ConfigurationManager.AppSettings["SendUsing"];
			}
		}

		public static string MailServer
		{
			get
			{
				return ConfigurationManager.AppSettings["SmtpServer"];
			}
		}

		public static string MailSMTPAuthenticate
		{
			get
			{
				return ConfigurationManager.AppSettings["SmtpAuthenticate"];
			}
		}

		public static string MailUserName
		{
			get
			{
				return ConfigurationManager.AppSettings["SendUserName"];
			}
		}

		public static int MaxDiasConsulta
		{
			get
			{
				int num = int.Parse(CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["MaxDiasConsulta"]));
				return num;
			}
		}

		public static int MaxPreguntas
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings["MaxPreguntas"]);
			}
		}

		public static string MensajeErrorLogin
		{
			get
			{
				return "Usuario y/o clave personal no válidos. Por favor intente de nuevo. \nRecuerda que al ingresar la clave errada en tres (3) intentos consecutivos, el acceso será bloqueado por medidas de seguridad.\nSi el problema persiste, comunícate con el Centro de Atención Telefónica desde Venezuela al teléfono 0-500-597.22.22 ó desde el Exterior al teléfono (+58-212) 279.92.81";
			}
		}

		public static string MensajeErrorRespuesta
		{
			get
			{
				return "Uno o varios de los datos ingresados no son válidos, por favor, intenta de nuevo. \nRecuerda que al responder tres (3) preguntas de forma incorrecta, el acceso será bloqueado por medidas de seguridad.";
			}
		}

		public static string MensajeExcepcion
		{
			get
			{
				return ConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public static string MensajeSinFavoritos
		{
			get
			{
				return "No existen Favoritos para este tipo de transacción. Para crearlos, ingresa a la opción del menú Registro de Favoritos.";
			}
		}

		public static string PaginaUrl
		{
			get
			{
				return ConfigurationManager.AppSettings["PaginaUrl"];
			}
		}

		public static string PAGO_TARJETA_OTROS_BANCOS
		{
			get
			{
				return "1,3";
			}
		}

		public static string PAGO_TARJETAS
		{
			get
			{
				return "PAGO_TARJETAS";
			}
		}

		public static string PAGO_TARJETAS_DATA
		{
			get
			{
				return "PAGO_TARJETAS_DATA";
			}
		}

		public static string PAGO_TARJETAS_OTROS
		{
			get
			{
				return "PAGO_TARJETAS_OTROS";
			}
		}

		public static string PAGO_TARJETAS_OTROS_DATA
		{
			get
			{
				return "PAGO_TARJETAS_OTROS_DATA";
			}
		}

		public static string PAGO_TARJETAS_TERCEROS
		{
			get
			{
				return "PAGO_TARJETAS_TERCEROS";
			}
		}

		public static string PAGO_TARJETAS_TERCEROS_DATA
		{
			get
			{
				return "PAGO_TARJETAS_TERCEROS_DATA";
			}
		}

		public static string PathReferencia
		{
			get
			{
				return ConfigurationManager.AppSettings["PathReferencia"];
			}
		}

		public static string PeriodoEstadoCuentaNuevo
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["PeriodoEstadoCuentaNuevo"]);
			}
		}

		public static bool ReconversionMonetaria
		{
			get
			{
				bool flag = bool.Parse(ConfigurationManager.AppSettings["Reconversion"].ToUpper());
				return flag;
			}
		}

		public static string SessionTimeOut
		{
			get
			{
				return ConfigurationManager.AppSettings["SessionTimeOut"];
			}
		}

		public static long SessionTimeOutMiliseconds
		{
			get
			{
				string[] strArrays = IBBAVConfiguration.SessionTimeOut.Split(new char[] { ':' });
				long num = long.Parse(strArrays[0]) * (long)60 + long.Parse(strArrays[1]);
				return num * (long)60000;
			}
		}

		public static string TCManageAccountMaster
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["TCManageAccountMaster"]);
			}
		}

		public static string TCManageAccountVisa
		{
			get
			{
				return CryptoUtils.UncryptDES(ConfigurationManager.AppSettings["TCManageAccountVisa"]);
			}
		}

		public static string TedexisPassport
		{
			get
			{
				return ConfigurationManager.AppSettings["TedexisPassport"];
			}
		}

		public static string TedexisPassword
		{
			get
			{
				return ConfigurationManager.AppSettings["TedexisPassword"];
			}
		}

		public static string TextSessionAlert
		{
			get
			{
				return ConfigurationManager.AppSettings["TextSessionAlert"];
			}
		}

		public static string TextSessionCurrent
		{
			get
			{
				return ConfigurationManager.AppSettings["TextSessionCurrent"];
			}
		}

		public static string TextSessionFinished
		{
			get
			{
				return ConfigurationManager.AppSettings["TextSessionFinished"];
			}
		}

		public static string TRANSFERENCIA_OTROS_BANCOS
		{
			get
			{
				return "2,3";
			}
		}

		public static string TRANSFERENCIAS
		{
			get
			{
				return "TRANSFERENCIAS";
			}
		}

		public static string TRANSFERENCIAS_DATA
		{
			get
			{
				return "TRANSFERENCIAS_DATA";
			}
		}

		public static string TRANSFERENCIAS_OTROS
		{
			get
			{
				return "TRANSFERENCIAS_OTROS";
			}
		}

		public static string TRANSFERENCIAS_OTROS_MISMO_TITULAR
		{
			get
			{
				return "TRANSFERENCIAS_OTROS_MISMO_TITULAR";
			}
		}

		public static string TRANSFERENCIAS_OTROS_MISMO_TITULAR_DATA
		{
			get
			{
				return "TRANSFERENCIAS_MISMO_TITULAR_DATA";
			}
		}

		public static string TRANSFERENCIAS_OTROS_TERCEROS
		{
			get
			{
				return "TRANSFERENCIAS_OTROS_TERCEROS";
			}
		}

		public static string TRANSFERENCIAS_OTROS_TERCEROS_DATA
		{
			get
			{
				return "TRANSFERENCIAS_OTROS_TERCEROS_DATA";
			}
		}

		public static string TRANSFERENCIAS_TERCEROS
		{
			get
			{
				return "TRANSFERENCIAS_TERCEROS";
			}
		}

		public static string TRANSFERENCIAS_TERCEROS_DATA
		{
			get
			{
				return "TRANSFERENCIAS_TERCEROS_DATA";
			}
		}

		public static bool UsingEVERTEC
		{
			get
			{
				bool flag = bool.Parse(ConfigurationManager.AppSettings["UseEVERTEC"].ToUpper());
				return flag;
			}
		}

		public static bool ValidarSesion
		{
			get
			{
				return bool.Parse(ConfigurationManager.AppSettings["ValidarSesion"]);
			}
		}

		private IBBAVConfiguration()
		{
		}
	}
}