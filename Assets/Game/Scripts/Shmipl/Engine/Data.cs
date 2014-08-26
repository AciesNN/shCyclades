using System;
using System.Collections;
using System.Collections.Generic;

namespace Shmipl.Base
{
	public class Pair<A, B>
	{
		public A a { get; private set; }
		public B b { get; private set; }

		public Pair(A a, B b)
		{
			this.a = a;
			this.b = b;
		}
	}

	public static class Clone
	{
		public static object Deep(object obj)
		{
			/*if (!(obj is object)) {

				return obj;

			} else*/ if (obj is Hashtable) {

				Hashtable res = new Hashtable ();
				foreach (object key in ((Hashtable)obj).Keys) {
					res [key] = Deep (((Hashtable)obj) [key]);
				}
				return res;

			} else if (obj is List<object>) {

				List<object> res = new List<object> ();

				foreach (object elem in (List<object>)obj)
					res.Add( Deep (elem) );

				return res;

			} else {

				return obj;
				//throw new Shmipl.Base.ShmiplUnspecifiedException ("Попытка глубокого копирования неучтенного типа данных: " + obj.ToString());

			}
		}
	}
}

