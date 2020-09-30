using System;
using System.Net;

namespace IBBAV.Functions
{
	public class DNSUtility
	{
		private string _strHostName;

		public string strHostName
		{
			get
			{
				return this._strHostName;
			}
			set
			{
				this._strHostName = value;
			}
		}

		public DNSUtility() : this(string.Empty)
		{
		}

		public DNSUtility(string strHostNameIn)
		{
			if (strHostNameIn.Length == 0)
			{
				this._strHostName = Dns.GetHostName();
				Console.WriteLine(string.Concat("Local Machine's Host Name: ", this._strHostName));
			}
			else
			{
				this._strHostName = strHostNameIn;
			}
			IPAddress[] addressList = Dns.GetHostByName(this.strHostName).AddressList;
			for (int i = 0; i < (int)addressList.Length; i++)
			{
				Console.WriteLine("IP Address {0}: {1} ", i, addressList[i].ToString());
			}
		}
	}
}