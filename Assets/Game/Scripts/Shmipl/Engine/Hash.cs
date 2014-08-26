using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Shmipl.Base
{
	public class HasNotThisKeyException: Exception
	{
	}

	public class Hash
	{
		private Hashtable data = new Hashtable();

		public T Get<T>(string index) 
		{
			if (data.ContainsKey (index))
				throw new HasNotThisKeyException ();
			return (T)data [index];
		}
		
		public Hash (string data)
		{
			this.data = Base.json.loads (data);
		}

		public Hash()
		{
		}

		public object this[string index]
		{
			get {
				return Get<object>(index);
			}

			set {
				data [index] = value;
			}
		}

		/*public operator<string>=(string new_data)
		{

		}*/

		public override string ToString()
		{
			return Base.json.dumps (data);
		}
	}

	/*public static class util 
	{
		public static T Get<T>(this Hashtable h, string z)
		{
			return default(T);
		}

		/*public static T Get<T>(this Hashtable h, string z)
		{
			return default(T);
			//return ( h.Contains(key) ? (T)h[key] : default_value );
		}

		/*
		public static T GetVal<T>(this Hashtable h, string key)
		{
			return ( h.Contains(key) ? (T)h[key] : default(T) );
		}*/
	//}
}

