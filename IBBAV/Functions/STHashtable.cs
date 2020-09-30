using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IBBAV.Functions
{
	public class STHashtable
	{
		public List<KeyValueTrio<string, string, DateTime>> tabla;

		private static STHashtable instance;

		protected STHashtable()
		{
			this.tabla = new List<KeyValueTrio<string, string, DateTime>>();
		}

		public void addHash(string key, string value, DateTime date)
		{
			this.tabla.Add(new KeyValueTrio<string, string, DateTime>(key, value, date));
		}

		public string findValue(string key)
		{
			string str;
			KeyValueTrio<string, string, DateTime> keyValueTrio = this.tabla.Find((KeyValueTrio<string, string, DateTime> p) => p.Key.Equals(key));
			str = (!keyValueTrio.Equals(null) ? keyValueTrio.Value1 : string.Empty);
			return str;
		}

		public static STHashtable getInstance()
		{
			if (STHashtable.instance == null)
			{
				STHashtable.instance = new STHashtable();
			}
			return STHashtable.instance;
		}

		public void removeValue(string key)
		{
			KeyValueTrio<string, string, DateTime> keyValueTrio = this.tabla.Find((KeyValueTrio<string, string, DateTime> p) => p.Key.Equals(key));
			if (!keyValueTrio.Equals(null))
			{
				this.tabla.Remove(keyValueTrio);
			}
		}
	}
}