using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;

namespace IBBAV.Entidades
{
	public class Correo
	{
		private string server;

		private int port;

		private string userName;

		private string userPassword;

		private string @from;

		private string to;

		private string subject;

		private string body;

		private bool isHTML;

		private List<HttpPostedFile> archivosAdjuntos;

		private List<MailAddress> cC;

		private List<MailAddress> bCC;

		public List<HttpPostedFile> ArchivosAdjuntos
		{
			get
			{
				return this.archivosAdjuntos;
			}
			set
			{
				this.archivosAdjuntos = value;
			}
		}

		public List<MailAddress> BCC
		{
			get
			{
				return this.bCC;
			}
			set
			{
				this.bCC = value;
			}
		}

		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		public List<MailAddress> CC
		{
			get
			{
				return this.cC;
			}
			set
			{
				this.cC = value;
			}
		}

		public string From
		{
			get
			{
				return this.@from;
			}
			set
			{
				this.@from = value;
			}
		}

		public bool IsHTML
		{
			get
			{
				return this.isHTML;
			}
			set
			{
				this.isHTML = value;
			}
		}

		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		public string Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		public string UserPassword
		{
			get
			{
				return this.userPassword;
			}
			set
			{
				this.userPassword = value;
			}
		}

		public Correo()
		{
			this.server = string.Empty;
			this.port = 0;
			this.userName = string.Empty;
			this.userPassword = string.Empty;
			this.@from = string.Empty;
			this.to = string.Empty;
			this.subject = string.Empty;
			this.body = string.Empty;
			this.isHTML = false;
			this.archivosAdjuntos = new List<HttpPostedFile>();
			this.cC = new List<MailAddress>();
			this.bCC = new List<MailAddress>();
		}

		public void AddBCC(string email)
		{
			this.bCC.Add(new MailAddress(email));
		}

		public void AddCC(string email)
		{
			this.cC.Add(new MailAddress(email));
		}
	}
}