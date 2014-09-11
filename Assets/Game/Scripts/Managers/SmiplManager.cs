using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmiplManager : Manager<SmiplManager> {

	//TODO тут конечно надо пересмотреть все эти фильтры сообщений

	protected override void Init ()	{		
		base.Init ();

		StartCoroutine(Shmipl.Base.ThreadSafeMessenger.ReceiveEvent());

		Shmipl.Base.Messenger<string, object, Hashtable>.AddListener("Shmipl.DeserializeContext", OnContextDeserialize);
		Shmipl.Base.Messenger<string, object, Hashtable>.AddListener("Shmipl.DoMacros", OnContextChanged);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.Error", OnError);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.AddContext", OnAddContext);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.RemoveContext", OnRemoveContext);

		#if UNITY_WEBPLAYER
			Shmipl.Base.Log.inFile = false;
			Shmipl.Base.Log.inConsole = false;
		#else
			Shmipl.Base.Log.PrintDebug = Debug.Log;
		#endif
		Cyclades.Program.GetIniTextFromFileMethod = (string path) => ((TextAsset)Resources.Load (path, typeof(TextAsset))).text;
		Cyclades.Program.Start();
	}

	void OnDestroy() {
		Shmipl.Base.Log.close_all();
	}

	private void OnContextChanged(string context_name, object to, Hashtable msg) {
		if (context_name == "Game" && (long)msg["to"] == Cyclades.Game.Client.Messanges.cur_player) {
			Debug.Log("change: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, bool>.Broadcast("UnityShmipl.UpdateView", msg, false));
			//Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger.Broadcast("UnityShmipl.UpdateView"));
		}
	}
	
	private void OnContextDeserialize(string context_name, object to, Hashtable msg) {
		if (context_name == "Game" && (long)msg["to"] == 0)	{
			Debug.Log("load: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, bool>.Broadcast("UnityShmipl.UpdateView", msg, true));
			//Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger.Broadcast("UnityShmipl.UpdateView"));
		}
	}
	
	private void OnError(object to, Hashtable msg) {
		Shmipl.Base.ThreadSafeMessenger.SendEvent(() => NGUIDebug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg)));
	}
	
	private void OnAddContext(object to, string fsm_name) {
		if ((long)to == 0)
			Debug.Log("+FSM: " + fsm_name);
	}
	
	private void OnRemoveContext(object to, string fsm_name) {
		if ((long)to == 0)
			Debug.Log("-FSM: " + fsm_name);
	}
}

