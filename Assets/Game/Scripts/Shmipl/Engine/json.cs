using System;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace Shmipl.Base
{
	public static class json
	{
		#region Интерфейс
		public static Hashtable loads(string json_str)
		{
			Newtonsoft.Json.Linq.JObject jobject = Newtonsoft.Json.Linq.JObject.Parse(json_str);

			Hashtable data = jobject.ToObject<Hashtable>();
			object data_ = data;
			object res = recursivelyJsonLoads(ref data_);		   

			return res as Hashtable;
		}

		public static Hashtable load(string path)
		{
			string file_content = System.IO.File.ReadAllText(path).Replace("\n", " ");
			return json.loads(file_content);
		}

		public static string dumps(Hashtable data)
		{
			return JsonConvert.SerializeObject(data);
		}
		#endregion

		#region Реализация
		private static object recursivelyJsonLoads(ref object data)
		{
			object res;
			if (data is Hashtable) {
				res = new Hashtable(); 
				Hashtable data_ = data as Hashtable;
				foreach (DictionaryEntry elem in data_) {
					object elem_ = elem.Value as object;
					parsePreprocession(ref elem_);
					(res as Hashtable)[elem.Key] = recursivelyJsonLoads(ref elem_);
				}
			}
			else if (data is List<object>)
			{
				res = new List<object>();
				List<object> data_ = data as List<object>;
				foreach (object elem in data_)
				{
					object elem_ = elem;
					parsePreprocession(ref elem_);
					(res as List<object>).Add(recursivelyJsonLoads(ref elem_));
				}
			} else {
				 res = data;
			}
			return res;
		}

		private static void parsePreprocession(ref object elem)
		{
			if (elem is Newtonsoft.Json.Linq.JObject)
			{
				Newtonsoft.Json.Linq.JObject elem_ = elem as Newtonsoft.Json.Linq.JObject;
				elem = elem_.ToObject<Hashtable>() as object;
			} else if (elem is Newtonsoft.Json.Linq.JArray) {
				Newtonsoft.Json.Linq.JArray elem_ = elem as Newtonsoft.Json.Linq.JArray;
				elem = elem_.ToObject<List<object>>() as object;
			}
		}
		#endregion
	}
}
