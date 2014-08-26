using System;
using System.Collections;

namespace Shmipl.Base
{
	#region базовый класс
	public class ShmiplException: Exception
	{
		public ShmiplException (string msg) :base(msg)
		{
		}

		//TODO
		/*public ShmiplException(params object[] param) : base()
		{
		}*/

		public ShmiplException(Hashtable hash) : base(Shmipl.Base.json.dumps(hash))
		{
		}

		public override string ToString()
		{
			string res = "";

			//res = Shmipl.Base.json.dumps( ... ); //TODO
			res = this.Message;

			return res;
		}
	}

	public class ShmiplUnspecifiedException: ShmiplException //TODO или лучше :Exception ???
	{
		public ShmiplUnspecifiedException (string msg) :base(msg)
		{
		}
	}
	#endregion 

	public class HasNoFSMWithThisNameException : ShmiplException
	{
		public HasNoFSMWithThisNameException(string fsm_name)
			: base("не найден FSM с именем " + fsm_name)
		{
		}
	}

	public class UnknownActionException : ShmiplException
	{
		public UnknownActionException(string action_name)
			: base("неизвестный запрос диспетчеру " + action_name)
		{
		}
	}

	public class UnknownMessageTypeException : ShmiplException
	{
		public UnknownMessageTypeException(string type)
			: base(String.Format("Неизвестный тип сигнала: {0}", type))
		{
		}
	}

	public class TestHasNotBeenPassedException : ShmiplException
	{
		public TestHasNotBeenPassedException(string reason)
			: base(reason)
		{
		}
		public TestHasNotBeenPassedException(string state, string test_name, string reason)
			: base(String.Format("В состоянии {0} не пройден тест {1} по причине: {2}", state, test_name, reason))
		{
		}
	}

	public class TestMethodHasNotFoundException : ShmiplException
	{
		public TestMethodHasNotFoundException(string state, string test_name)
			: base(String.Format("В состоянии {0} не найден тест: {1}", state, test_name))
		{
		}
	}

	public class ActionMehodHasNotFoundException : ShmiplException
	{
		public ActionMehodHasNotFoundException(string state, string action_name)
					: base(String.Format("В состоянии {0} не найден метод перехода: {1}", state, action_name))
		{
		}
	}

	public class SignalNameHasNotFoundException : ShmiplException
	{
		public SignalNameHasNotFoundException(string state, string signal_name)
							: base(String.Format("В состоянии {0} не обнаружен сигнал с именем: {1}", state, signal_name))
		{
		}
	}
}

