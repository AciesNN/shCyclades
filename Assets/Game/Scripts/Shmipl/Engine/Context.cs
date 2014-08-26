using System;
using System.Collections.Generic;
using System.Collections;

namespace Shmipl.Base
{
	public interface IContextGet {
		T Get<T> (string path, params object[] param);

		//** уточнения **//
		bool GetBool(string path, params object[] param);

		string GetStr(string path, params object[] param);

		long GetLong(string path, params object[] param);

		int GetInt(string path, params object[] param);

		List<object> GetList(string path, params object[] param);

		List<T> GetList<T>(string path, params object[] param);

        //List<Shmipl.FrmWrk.Library.Coords> GetListCoords(string path, params object[] param);
		//** уточнения **//
	}

    public class CannotCastGetContext<T>: Exception {
        public CannotCastGetContext(string path, Type T_from, Type T_to)
            : base("Невозможно в контексте привести объект по пути " + path + ", имеющий тип " + T_from.ToString() + " к типу " + T_to.ToString()) {
        }
    }

	public class Context: IContextGet
	{
		public Hashtable data; //TODO надо сделать private, а отдавать только копию каким-нибудь методом get

		public Context()
		{
			this.data = new Hashtable();
		}

        public override string ToString() {
            return json.dumps(data);
        }

		#region Интерфейс
		//1. загрузка
		public void LoadDataFromFile(string path)
		{
            lock (this) {
                this.data = json.load(path);
            }
		}

		public void LoadDataFromText(string text) {
			lock (this) {
				this.data = json.loads(text);
			}
		}

		//2. get/set
		public void Set(string path, object value)
		{
            lock (this) {
                tree.set(this.data, path, value);
            }
		}

		public T Get<T>(string path, params object[] param)
		{
            object res = Get(path, param);
            if (res is T)
                return (T)res;
            else
                throw new CannotCastGetContext<T>(String.Format(path, param), res.GetType(), default(T).GetType()); //todo - default(T).GetType() выглядит довольно тяжеловесно
		}

		//** уточнения **//
		public bool GetBool(string path, params object[] param)
		{
			return Get<bool>(path, param);
		}

		public string GetStr(string path, params object[] param)
		{
            object str = Get(path, param);
            if (str == null)
                return "";
            return (string)str; //TODO связано с непонятным глюком в недрах  Newtonsoft.Json.dll - при некоторых условиях он интерпретирует строку как null, выяснить почему - не удалось
		}

		public long GetLong(string path, params object[] param)
		{
			return Get<long>(path, param);
		}

		public int GetInt(string path, params object[] param) 
		{
			return (int)Get<long>(path, param);
		}
		
		public List<object> GetList(string path, params object[] param)
		{
			return Get<List<object>>(path, param);
		}

		public List<T> GetList<T>(string path, params object[] param)
		{
			return Get<List<object>>(path, param).ConvertAll<T>((o) => (T)o);
		}
		/*
        public List<Shmipl.FrmWrk.Library.Coords> GetListCoords(string path, params object[] param) {
            List<object> olist = Get<List<object>>(path, param);
            List<Shmipl.FrmWrk.Library.Coords> res = new List<FrmWrk.Library.Coords>();
            foreach (List<object> c in olist)
                res.Add(new Shmipl.FrmWrk.Library.Coords((long)c[0], (long)c[1]));
            return res;
        }*/
		//** уточнения **//

		public T DirtyGet<T>(string path, params object[] param)
		{
            object res = DirtyGet(path, param);
            if (res is T)
                return (T)res;
            else
                throw new CannotCastGetContext<T>(String.Format(path, param), res.GetType(), default(T).GetType()); //todo - default(T).GetType() выглядит довольно тяжеловесно
		}

		//3. сериализация/десериализация
		public string Serialize()
		{
            lock (this) {
                return Base.json.dumps((Hashtable)Shmipl.Base.Clone.Deep(this.data));
            }
		}

		public void Deserialize(string data)
		{
            lock (this) {
                this.data = Base.json.loads(data);
            }
		}
		#endregion

		#region Реализация
		private object Get(string path, params object[] param)
		{
            lock (this) {
                string path_ = String.Format(path, param);
                object res = tree.get(this.data, path_);
                object copy_res = Shmipl.Base.Clone.Deep(res);
                return copy_res;
            }
		}

		private object DirtyGet(string path, params object[] param)
		{
            lock (this) {
                string path_ = String.Format(path, param);
                object res = tree.get(this.data, path_);
                return res;
            }
		}
		#endregion
	}
}
