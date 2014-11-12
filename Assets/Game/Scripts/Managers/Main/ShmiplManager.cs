using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShmiplManager : Manager<ShmiplManager> {

	public long _pl = 0; //TODO
	public string _gm = "Game"; //TODO

	//TODO тут конечно надо пересмотреть все эти фильтры сообщений
	protected override void Init ()	{		
		base.Init ();
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			Application.LoadLevel("Menu");
			return;
		}

		StartCoroutine(Shmipl.Base.ThreadSafeMessenger.ReceiveEvent());

		Shmipl.Base.Messenger<string, object, Hashtable, long>.AddListener("Shmipl.DeserializeContext", OnContextDeserialize);
		Shmipl.Base.Messenger<string, object, Hashtable, long, bool>.AddListener("Shmipl.DoMacros", OnContextChanged);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.Error", OnError);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.AddContext", OnAddContext);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.RemoveContext", OnRemoveContext);

		#if UNITY_WEBPLAYER
		Shmipl.Base.Log.inFile = false;
		Shmipl.Base.Log.inConsole = false;
		#elif UNITY_ANDROID
		Shmipl.Base.Log.inFile = false;
		Shmipl.Base.Log.inConsole = false;
		#else
		Shmipl.Base.Log.PrintDebug = Debug.Log;
		#endif

		Cyclades.Program.GetIniTextFromFileMethod = (string path) => ((TextAsset)Resources.Load(path, typeof(TextAsset))).text;

		NGUIDebug.Log("resolution: " + Screen.width + "/" + Screen.height);
	}

	void OnDestroy() {
		#if UNITY_WEBPLAYER
		#elif UNITY_ANDROID
		#else
		Shmipl.Base.Log.close_all();
		#endif
	}

	private void OnContextChanged(string context_name, object to, Hashtable msg, long counter, bool stable) {
		if (context_name == _gm && (long)msg["to"] == _pl) {
			if (msg.ContainsKey("macros") && msg["macros"] is String && (string)msg["macros"] == "SHOW") {//todo выглядит хардкордно
				Debug.Log("show: " + Shmipl.Base.json.dumps(msg));
				Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable>.Broadcast("UnityShmipl.ShowAnimation", msg));
			} else {
				Debug.Log("change: " + Shmipl.Base.json.dumps(msg));
				Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, counter, stable, false));
			}
		}
	}
	
	private void OnContextDeserialize(string context_name, object to, Hashtable msg, long counter) {
		if (context_name == _gm && (long)msg["to"] == _pl) {
			Debug.Log("load: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, counter, false, true));
		}
	}
	
	private void OnError(object to, Hashtable msg) {
		Shmipl.Base.ThreadSafeMessenger.SendEvent(() => NGUIDebug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg)));
		Debug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg));
	}
	
	private void OnAddContext(object to, string fsm_name) {
		if ((long)to == _pl)
			Debug.Log("+FSM: " + fsm_name);
	}
	
	private void OnRemoveContext(object to, string fsm_name) {
		if ((long)to == _pl)
			Debug.Log("-FSM: " + fsm_name);
	}

	void OnLevelWasLoaded(int level) {

	}
}

