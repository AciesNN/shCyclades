using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Menu_ShmiplManager : Manager<Menu_ShmiplManager> {

	public object _pl = "Client0"; //TODO
	public string _gm = "Game"; //TODO

	public PhotonView photonView;

	//TODO тут конечно надо пересмотреть все эти фильтры сообщений
	protected override void Init ()	{		
		base.Init ();
		
		StartCoroutine(Shmipl.Base.ThreadSafeMessenger.ReceiveEvent());

		Shmipl.Base.Messenger<string, object, Hashtable, long>.AddListener("Shmipl.DeserializeContext", OnContextDeserialize);
		Shmipl.Base.Messenger<string, object, Hashtable, long, bool>.AddListener("Shmipl.DoMacros", OnContextChanged);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.Error", OnError);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.AddContext", OnAddContext);
		Shmipl.Base.Messenger<object, string>.AddListener("Shmipl.RemoveContext", OnRemoveContext);
		Shmipl.Base.Messenger<object, Hashtable>.AddListener("Shmipl.DeserializeConnections", OnDeserializeConnections);
		Shmipl.Base.Messenger<object>.AddListener("Shmipl.Server.ConnectionRegister", ServerConnectionRegister);

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
		PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}

		//Debug.Log ( "P c r = "  +PhotonNetwork.countOfRooms );
	}

	void ServerConnectionRegister(object name) {
		Debug.Log ("ServerConnectionRegister: " + name);
	}

	void OnDeserializeConnections(object name, Hashtable data) {
		Debug.Log ("OnDeserializeConnections: " + name + ", " + Shmipl.Base.json.dumps(data));
		try {
			object z = Cyclades.Program.clnts[_pl].GetRootName();
			Debug.Log ("player " + _pl + " is " + z);
		} catch (Exception ex) {
			Debug.Log ("err: " + ex);
		}
	}

	void OnDestroy() {
		#if UNITY_WEBPLAYER
		#elif UNITY_ANDROID
		#else
		Shmipl.Base.Log.close_all();
		#endif
	}

	private void OnContextChanged(string context_name, object to, Hashtable msg, long counter, bool stable) {
		if (context_name == _gm && msg["to"] == _pl) {
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
		if (context_name == _gm && msg["to"] == _pl) {
			Debug.Log("load: " + Shmipl.Base.json.dumps(msg));
			Shmipl.Base.ThreadSafeMessenger.SendEvent(() => Shmipl.Base.Messenger<Hashtable, long, bool, bool>.Broadcast("UnityShmipl.UpdateView", msg, counter, false, true));
		}
	}
	
	private void OnError(object to, Hashtable msg) {
		Shmipl.Base.ThreadSafeMessenger.SendEvent(() => NGUIDebug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg)));
		Debug.Log("\tERROR: " + Shmipl.Base.json.dumps(msg));
	}
	
	private void OnAddContext(object to, string fsm_name) {
		if (to == _pl)
			Debug.Log("+FSM: " + fsm_name);
	}
	
	private void OnRemoveContext(object to, string fsm_name) {
		if (to == _pl)
			Debug.Log("-FSM: " + fsm_name);
	}

	/*void OnLevelWasLoaded(int level) {

	}*/

	#region Events
	public void OnServerCreateClick() {

		PhotonNetwork.CreateRoom("test",true,true,20);
		Cyclades.Program.CreateServer();

	}

	public void OnNetClientCreateClick() {

		PhotonNetwork.playerName = "NetClient" + UnityEngine.Random.Range(100, 1000);
		PhotonNetwork.JoinRoom("test");

		Shmipl.FrmWrk.Net.UniversalClientConnection conn = new Shmipl.FrmWrk.Net.UniversalClientConnection();
		conn.send_msg = (string msg) => {
			PhotonNetwork.RPC(photonView, "PhotonNetworkRPC_ClientToServer", PhotonTargets.MasterClient, PhotonNetwork.playerName, msg);
		};
		Cyclades.Program.CreateNetClient(conn, PhotonNetwork.playerName);
		conn.msgs = Cyclades.Program.clnt.msgs;

	}

	public void OnHotSeatClientCreateClick() {
		Cyclades.Program.CreateHotSeatClient(Cyclades.Program.srv.conn_pull);
	}

	public void OnGameStartClick() {
		Shmipl.Base.Messenger<string, object, Hashtable, long>.RemoveListener("Shmipl.DeserializeContext", OnContextDeserialize);
		Shmipl.Base.Messenger<string, object, Hashtable, long, bool>.RemoveListener("Shmipl.DoMacros", OnContextChanged);
		Shmipl.Base.Messenger<object, Hashtable>.RemoveListener("Shmipl.Error", OnError);
		Shmipl.Base.Messenger<object, string>.RemoveListener("Shmipl.AddContext", OnAddContext);
		Shmipl.Base.Messenger<object, string>.RemoveListener("Shmipl.RemoveContext", OnRemoveContext);
		Shmipl.Base.Messenger<object, Hashtable>.RemoveListener("Shmipl.DeserializeConnections", OnDeserializeConnections);
		Shmipl.Base.Messenger<object>.RemoveListener("Shmipl.Server.ConnectionRegister", ServerConnectionRegister);

		try {
			Cyclades.Program.StartServer((int)System.DateTime.Now.Ticks, true);
		} catch (Exception ex) {
			NGUIDebug.Log("ERROR: " + ex);
		}
		Application.LoadLevel("Main");
	}
	#endregion


}

