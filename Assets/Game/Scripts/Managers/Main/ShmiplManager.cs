using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShmiplManager : Manager<ShmiplManager> {

	bool _messanges_subscribed = false;

	public object _pl = 0L; //TODO
	public string _gm = "Game"; //TODO
	public string _room_name = "test"; //TODO

	public PhotonView photonView;

	//TODO тут конечно надо пересмотреть все эти фильтры сообщений
	protected override void Init ()	{		
		base.Init ();

		// the following line checks if this client was just created (and not yet online). if so, we connect
		StartCoroutine(Shmipl.Base.ThreadSafeMessenger.ReceiveEvent());

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

		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		//PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}

		//PhotonNetwork.logLevel = PhotonLogLevel.Informational;

		//Debug.Log ( "P c r = "  +PhotonNetwork.countOfRooms );
		//NGUIDebug.Log("resolution: " + Screen.width + "/" + Screen.height);
	}

	void Start() {
		Shmipl.Base.Messenger<string, object, Hashtable, long>.AddListener("Shmipl.DeserializeContext", OnContextDeserialize);
		Shmipl.Base.Messenger<string, object, Hashtable>.AddListener("Shmipl.Init", OnContextInit);
		Shmipl.Base.Messenger<string, object, Hashtable, long, bool>.AddListener("Shmipl.DoMacros", OnContextChanged);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.Error", OnError);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.AddContext", OnAddContext);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.RemoveContext", OnRemoveContext);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.DeserializeConnections", OnDeserializeConnections);
		Shmipl.Base.Messenger<object>.AddListener("Shmipl.Server.ConnectionRegister", OnServerConnectionRegister);
		_messanges_subscribed = true;

		//отрисуем то, что есть
		//TODO выглядит отвратительно
		long counter = -1;
		if (Cyclades.Program.clnts.ContainsKey(_pl)) {
			Shmipl.FrmWrk.Client.DispetcherFSM dsp = Cyclades.Program.clnts[_pl];
			if (dsp != null && dsp.history_tree.ContainsKey(_gm)) {
				Shmipl.FrmWrk.ContextHistory ch = dsp.history_tree[_gm];
				long[] counters = ch.GetCounters();
				
				foreach (long c in counters) {
					if (c > counter)
						counter = c;
				}
			}
		}
		
		if (counter == -1) {
			NGUIDebug.Log("counter == -1");
		} else {
			NGUIDebug.Log("drow context " + counter);
			Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", null, counter, false, true);
		}
	}

	void OnDestroy() {
		#if UNITY_WEBPLAYER
		#elif UNITY_ANDROID
		#else
		Shmipl.Base.Log.close_all();
		#endif

		if (_messanges_subscribed) {
			Shmipl.Base.Messenger<string, object, Hashtable, long>.RemoveListener("Shmipl.DeserializeContext", OnContextDeserialize);
			Shmipl.Base.Messenger<string, object, Hashtable>.RemoveListener("Shmipl.Init", OnContextInit);
			Shmipl.Base.Messenger<string, object, Hashtable, long, bool>.RemoveListener("Shmipl.DoMacros", OnContextChanged);
			Shmipl.Base.Messenger<object, Hashtable>.RemoveListener("Shmipl.Error", OnError);
			Shmipl.Base.Messenger<object, string>.RemoveListener("Shmipl.AddContext", OnAddContext);
			Shmipl.Base.Messenger<object, string>.RemoveListener("Shmipl.RemoveContext", OnRemoveContext);
			Shmipl.Base.Messenger<object, Hashtable>.RemoveListener("Shmipl.DeserializeConnections", OnDeserializeConnections);
			Shmipl.Base.Messenger<object>.RemoveListener("Shmipl.Server.ConnectionRegister", OnServerConnectionRegister);
		}
	}

	#region Shmipl.Events
	bool TestAdress(object to) {
		if (Cyclades.Program.isServer) {
			return (long)to == (long)_pl;
		} else {
			return true;
		}
	}

	void OnServerConnectionRegister(object name) {
		Debug.Log ("ServerConnectionRegister: " + name);
	}
	
	void OnDeserializeConnections(object name, Hashtable data) {
		Debug.Log ("OnDeserializeConnections: " + name + ", " + Shmipl.Base.json.dumps(data));
		/*try {
			object z = Cyclades.Program.clnts[_pl].GetRootName();
			Debug.Log ("player " + _pl + " is " + z);
		} catch (Exception ex) {
			Debug.Log ("err: " + ex);
		}*/
	}

	private void OnContextChanged(string context_name, object to, Hashtable msg, long counter, bool stable) {
		if (context_name == _gm && TestAdress(msg["to"])) {
			Debug.Log("change: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, counter, stable, false));
		}
	}

	private void OnContextInit(string context_name, object to, Hashtable msg) {
		if (context_name == _gm && TestAdress(msg["to"])) {
			Debug.Log("init: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, 1, false, true));
		}
	}

	private void OnContextDeserialize(string context_name, object to, Hashtable msg, long counter) {
		if (context_name == _gm && TestAdress(msg["to"])) {
			Debug.Log("load: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, counter, false, true));
		}
	}
	
	private void OnError(object to, Hashtable msg) {
		Shmipl.Base.ThreadSafeMessenger.SendEvent(() => NGUIDebug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg)));
		Debug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg));
	}
	
	private void OnAddContext(object to, string fsm_name) {
		/*if (to == _pl)
			Debug.Log("+FSM: " + fsm_name);*/
	}
	
	private void OnRemoveContext(object to, string fsm_name) {
		/*if (to == _pl)
			Debug.Log("-FSM: " + fsm_name);*/
	}
	#endregion
}

