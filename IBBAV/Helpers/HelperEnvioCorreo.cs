using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using System;
using System.Collections.Generic;
using System.Net;

namespace IBBAV.Helpers
{
	public class HelperEnvioCorreo
	{
		public const string CODSISTEMA = "SQLIB";

		public HelperEnvioCorreo()
		{
		}

		public static bool BuscarCamposCorreo(int Codtrans, string NombreTitular, string correodestino, decimal Monto_Operacíon, string Numero_Tarjeta, string Cuenta_Beneficiario, string Numero_Referencia, string Nombre_Beneficiario, string Cuenta_Destino, string Tipo_Tarjeta, string Numero_Cuenta, string Numero_Chequeras, string Numero_Cheques, string Descripcion_Concepto, string Cuenta_Origen, string nombre_Ordenante, string Nombre_Banco, string CorreoBeneficiario)
		{
			bool flag = false;
			try
			{
				foreach (DatosCorreo datosCorreo in HelperGlobal.BDI_DatosCorreoGetByDC_CodTrans(Codtrans.ToString()))
				{
					if (datosCorreo.DC_Destino.Equals("Beneficiario"))
					{
						correodestino = CorreoBeneficiario;
					}
					if ((!datosCorreo.DC_Destino.Equals("Monitoreo") ? false : NombreTitular.Equals("Monitoreo")))
					{
						correodestino = "atencionalcliente@BAV.com.ve";
					}
					string dCContenido = datosCorreo.DC_Contenido;
					dCContenido = dCContenido.Replace("<NombreTitular>", NombreTitular.ToString().Trim().ToUpper());
					DateTime now = DateTime.Now;
					dCContenido = dCContenido.Replace("<FechaHora>", now.ToString("dd/MM/yyyy hh:mm:ss"));
					dCContenido = dCContenido.Replace("<TipoTarjeta>", Tipo_Tarjeta);
					dCContenido = dCContenido.Replace("<NumeroTarjeta>", Formatos.formatoTarjeta(Numero_Tarjeta));
					dCContenido = dCContenido.Replace("<CuentaBeneficiario>", Cuenta_Beneficiario);
					dCContenido = dCContenido.Replace("<NumeroCuenta>", Formatos.formatoCuenta(Numero_Cuenta));
					dCContenido = dCContenido.Replace("<NumeroChequeras>", Numero_Chequeras);
					dCContenido = dCContenido.Replace("<NumeroCheques>", Numero_Cheques);
					dCContenido = dCContenido.Replace("<NumeroReferencia>", Numero_Referencia);
					dCContenido = dCContenido.Replace("<NombreCliente>", NombreTitular.Trim().ToUpper());
					dCContenido = dCContenido.Replace("<NumeroTarjeta Beneficiario>", Numero_Tarjeta);
					dCContenido = dCContenido.Replace("<MontoOperacion>", Formatos.formatoMonto(Monto_Operacíon));
					dCContenido = dCContenido.Replace("<DescripcionConcepto>", Descripcion_Concepto);
					dCContenido = dCContenido.Replace("<NombreBeneficiario>", Nombre_Beneficiario.Trim().ToUpper());
					dCContenido = dCContenido.Replace("<CuentaOrigen>", Formatos.formatoCuenta(Cuenta_Origen));
					dCContenido = dCContenido.Replace("<CuentaDestino>", Formatos.formatoCuenta(Cuenta_Destino));
					dCContenido = dCContenido.Replace("<NombreOrdenante>", "Nombre_Ordenante");
					dCContenido = dCContenido.Replace("<NombreBanco>", Nombre_Banco);
					dCContenido = dCContenido.Replace("<ClienteOrdenante>", "Cliente_Ordenante");
					dCContenido = dCContenido.Replace("<NombreTitularTarjeta>", NombreTitular.Trim().ToUpper());
					dCContenido = dCContenido.Replace("<CorreoAtencionCliente>", IBBAVConfiguration.CorreoAtencionCliente);
					dCContenido = dCContenido.Replace("<PaginaUrl>", IBBAVConfiguration.PaginaUrl);
					MailManager.SendMail(correodestino, datosCorreo.DC_Subject, dCContenido, true);
					flag = true;
				}
			}
			catch (IBException bException)
			{
			}
			catch (WebException webException)
			{
				throw new IBException("1000", "SQLIB");
			}
			catch (Exception exception)
			{
				string.Concat("error al mandar el correo. ", exception.Message);
			}
			return flag;
		}

		public static bool BuscarCamposCorreoSMS(int sCod, string NombreCliente, string CorreoDestino)
		{
			bool flag = false;
			try
			{
				foreach (DatosCorreo datosCorreo in HelperGlobal.BDI_DatosCorreoGetByDC_CodTrans(sCod.ToString()))
				{
					string dCContenido = datosCorreo.DC_Contenido;
					dCContenido = dCContenido.Replace("{NombreCliente}", NombreCliente.Trim());
					MailManager.SendMail(CorreoDestino, datosCorreo.DC_Subject, dCContenido, true);
					flag = true;
				}
			}
			catch (IBException bException)
			{
			}
			catch (WebException webException)
			{
				throw new IBException("1000", "SQLIB");
			}
			catch (Exception exception)
			{
				string.Concat("error al mandar el correo. ", exception.Message);
			}
			return flag;
		}

		public static bool EnviarCorreoClaveTemporal(string NombreTitular, string correodestino, string claveesp)
		{
			bool flag = false;
			try
			{
				foreach (DatosCorreo datosCorreo in HelperGlobal.BDI_DatosCorreoGetByDC_CodTrans("999"))
				{
					string dCContenido = datosCorreo.DC_Contenido;
					dCContenido = dCContenido.Replace("<NombreTitular>", NombreTitular.ToString().Trim().ToUpper());
					DateTime now = DateTime.Now;
					dCContenido = dCContenido.Replace("<FechaHora>", now.ToString("dd/MM/yyyy hh:mm:ss"));
					dCContenido = dCContenido.Replace("<claveesp>", claveesp);
					MailManager.SendMail(correodestino, datosCorreo.DC_Subject, dCContenido, true);
					flag = true;
				}
			}
			catch (IBException bException)
			{
			}
			catch (WebException webException)
			{
				throw new IBException("1000", "SQLIB");
			}
			catch (Exception exception)
			{
				string.Concat("error al mandar el correo. ", exception.Message);
			}
			return flag;
		}
	}
}