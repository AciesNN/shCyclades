using System;
using System.Collections;
using System.Collections.Generic;

namespace Shmipl.Base
{
	public class HasNoThisPathException: Exception
	{
		//TODO нужно использовать в get
		public HasNoThisPathException(string path): base(path)
		{
		}
	}

    public class tree
	{
        /*
        #region уточнения
        public static bool GetBool(Hashtable data, string path, params object[] param) {
			return Get<bool>(data, path, param);
        }

        public static string GetStr(Hashtable data, string path, params object[] param) {
			return Get<string>(data, path, param);
        }

        public static long GetLong(Hashtable data, string path, params object[] param) {
			return Get<long>(data, path, param);
        }

        public static List<object> GetList(Hashtable data, string path, params object[] param) {
			return Get<List<object>>(data, path, param);
        }

        public static List<T> GetList<T>(Hashtable data, string path, params object[] param) {
            return Get<List<object>>(data, path, param).ConvertAll<T>((o) => (T)o);
        }

        public static T Get<T>(Hashtable data, string path, params object[] param) {
            return (T)get(data, String.Format(path, param));
        }
        #endregion
        */

		public static object get(Hashtable data, string path) {
			List<string> path_list = _path_to_list(path);
			try {
				object res = _get(data, path_list, 0);
				return res;
			}
			catch(ShmiplException ex) {
				throw new HasNoThisPathException (path + ":" + ex.Message);
			}
		}

		public static  T get<T>(Hashtable data, string path) {
			return (T) get(data, path);
		}

		public static void set(Hashtable data, string path, object value) {
			List<string> path_list = _path_to_list(path);	
			try {
				object elem_ = _get(data, path_list.GetRange(0, path_list.Count - 1), 0);
				string str_index = path_list[path_list.Count - 1];

				if (elem_ is Hashtable) {
					Hashtable elem = (Hashtable)elem_;
					//_elem = elem[path_list[-1]]	//TODO EXCEPTION
					elem[str_index] = value;
				} else if (elem_ is List<object>) {
					List<object> elem = (List<object>)elem_;
					if (str_index.StartsWith("[") && str_index.EndsWith("]")) {
						int index = Convert.ToInt32 (str_index.Replace ("[", "").Replace ("]", ""));
						elem[index] = value;
					} else {
						throw new HasNoThisPathException ("не указан элемент списка");
					}
				} else {
					throw new HasNoThisPathException ("неизвестный тип узла");
				}
			}
			catch(HasNoThisPathException ex) {
				throw new HasNoThisPathException ("не найден путь <" + path + ">: " + ex.Message);
			}
		}

		public static void addition(Hashtable datatrg, Hashtable datasrc, string path)
		{
			Hashtable data;
			if (path != "")
				data = (Hashtable)get(datatrg, path);
			else 
				data = datatrg;
			foreach (DictionaryEntry elem in datasrc)
				data[elem.Key] = elem.Value;
		}

		public static void addition(Hashtable datatrg, Hashtable datasrc)
		{
			addition (datatrg, datasrc, "");
		}

		public static void intersection(Hashtable data, string path, List<string> interlist) 
		{
			List<string> path_list = _path_to_list(path);
			Hashtable up_data = _get(data, path_list.GetRange(0, path_list.Count - 1), 0) as Hashtable;
			object sub_data = up_data[path_list[path_list.Count - 1]];
			up_data.Remove(path_list[path_list.Count - 1]);
			foreach (string elem in interlist)
				/*if (sub_data is ICloneable)
					up_data[elem] = (sub_data as ICloneable).Clone() as object; //TODO нужно глубокое копирование
				else*/
					up_data[elem] = Shmipl.Base.Clone.Deep (sub_data);

		}

		/*public static void process(data, processRules)
			for (path, rule) in processRules
				set(data, path, _processRule(get(data, path), rule))*/
			
		//-----------------------------------------------------
		[Obsolete]
		static object _get_old(Hashtable data, List<string> path_list) {
			object res = data;
			foreach (string elem in path_list)
			{
				Hashtable h = res as Hashtable;
				if (h == null || !h.ContainsKey(elem))
					return null;
				res = ((Hashtable)res)[elem];
			}
			return res;
		}

		static object _get(object data, List<string> path_list, int path_pos)
		{
			if (path_pos == path_list.Count)
				return data;
			string elem = path_list[path_pos];

			if (data == null)
				throw new HasNoThisPathException(elem);

			if (elem == "")
				throw new Base.ShmiplUnspecifiedException("Пустой элемент пути контекста");

			if (elem == "*")
			{
				Hashtable data_ = data as Hashtable;
				List<object> res = new List<object>(); //тут могут быть разные стратегии - как лист, так и хэш

				foreach (object next_data in data_.Values)
				{
					res.Add(_get(next_data, path_list, path_pos + 1));
				}

				return res;
			}
			else if (elem.StartsWith("[") && elem.EndsWith("]"))
			{
				List<object> data_ = (List<object>)data;
				string str_index = elem.Replace ("[", "").Replace ("]", "");
				if (str_index == "*") {
					List<object> res = new List<object>(); //тут могут быть разные стратегии - как лист, так и хэш

					foreach (object next_data in data_)
					{
						res.Add(_get(next_data, path_list, path_pos + 1));
					}

					return res;

				} else {
					int index = Convert.ToInt32 (str_index);
					if (data_.Count <= index || index < 0)
						throw new HasNoThisPathException (elem);
					return _get (data_ [index], path_list, path_pos + 1);
				}
			}
			else
			{
                if (elem.Contains("["))
                    throw new HasNoThisPathException(elem + " содержит обращение к индексу массива");
				Hashtable data_ = data as Hashtable;
				if(!data_.Contains(elem))
					throw new HasNoThisPathException(elem);
				return _get(data_[elem], path_list, path_pos + 1);
			}
		}

		static List<string> _path_to_list(string path) {
			List<string> res = new List<string>(path.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
			if (res[0] == "")
				res.RemoveAt(0);
			return res;
		}

		/*static void _processRule(elem, rule) {
			if type(elem) == list
				elem = [rule(x) for x in elem]
			return elem
		}*/

    }
}
