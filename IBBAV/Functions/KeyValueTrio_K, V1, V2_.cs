using System;

namespace IBBAV.Functions
{
	public class KeyValueTrio<K, V1, V2>
	{
		private K key;

		private V1 value1;

		private V2 value2;

		public K Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		public V1 Value1
		{
			get
			{
				return this.value1;
			}
			set
			{
				this.value1 = value;
			}
		}

		public V2 Value2
		{
			get
			{
				return this.value2;
			}
			set
			{
				this.value2 = value;
			}
		}

		public KeyValueTrio(K key, V1 value1, V2 value2)
		{
			this.key = key;
			this.value1 = value1;
			this.value2 = value2;
		}
	}
}