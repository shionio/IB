using IBBAV.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace IBBAV.Functions
{
	public class MailManager
	{
		public MailManager()
		{
		}

		private static void EmbedImage(MailMessage message, string ContentId, string ruta)
		{
			AlternateView alternateView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
			string str = HttpContext.Current.Server.MapPath(ruta);
			LinkedResource linkedResource = new LinkedResource(str, "image/jpeg")
			{
				ContentId = ContentId
			};
			alternateView.LinkedResources.Add(linkedResource);
			message.AlternateViews.Add(alternateView);
		}

		public static void SendMail(Correo correo)
		{
			MailManager.SendMail(correo.Server, correo.Port, correo.UserName, correo.UserPassword, correo.From, correo.To, correo.Subject, correo.Body, correo.IsHTML, correo.ArchivosAdjuntos, correo.CC, correo.BCC);
		}

		public static void SendMail(string to, string subject, string body, bool isHtml, List<HttpPostedFile> attachments)
		{
			MailManager.SendMail(IBBAVConfiguration.MailServer, int.Parse(IBBAVConfiguration.MailPort), IBBAVConfiguration.MailUserName, IBBAVConfiguration.MailPassword, IBBAVConfiguration.MailRemitente, to, subject, body, isHtml, attachments, null, null);
		}

		public static void SendMail(string to, string subject, string body, bool isHtml)
		{
			MailManager.SendMail(IBBAVConfiguration.MailServer, int.Parse(IBBAVConfiguration.MailPort), IBBAVConfiguration.MailUserName, IBBAVConfiguration.MailPassword, IBBAVConfiguration.MailRemitente, to, subject, body, isHtml, null, null, null);
		}

		public static void SendMail(string server, int port, string user, string password, string from, string to, string subject, string body, bool isHtml, List<HttpPostedFile> attachments, List<MailAddress> CC, List<MailAddress> BCC)
		{
			try
			{
				MailMessage mailMessage = new MailMessage()
				{
					From = new MailAddress(from)
				};
				mailMessage.To.Add(to);
				mailMessage.Subject = subject;
				mailMessage.Body = body;
				mailMessage.IsBodyHtml = isHtml;
				mailMessage.Priority = MailPriority.Normal;
				if (CC != null)
				{
					foreach (MailAddress cC in CC)
					{
						mailMessage.CC.Add(cC);
					}
				}
				if (BCC != null)
				{
					foreach (MailAddress bCC in BCC)
					{
						mailMessage.Bcc.Add(bCC);
					}
				}
				if (attachments != null)
				{
					foreach (HttpPostedFile attachment in attachments)
					{
						FileInfo fileInfo = new FileInfo(attachment.FileName);
						mailMessage.Attachments.Add(new Attachment(attachment.InputStream, fileInfo.Name));
					}
				}
				(new SmtpClient(server)
				{
					Credentials = new NetworkCredential(user, password),
					Port = port
				}).Send(mailMessage);
			}
			catch (Exception exception)
			{
				string message = exception.Message;
			}
		}
	}
}