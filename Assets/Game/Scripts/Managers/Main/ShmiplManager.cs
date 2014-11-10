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

	#region Events
	public void OnServerCreateClick() {

		PhotonNetwork.CreateRoom("test",true,true,5);
		Cyclades.Program.CreateServer();

	}

	public void OnNetClientCreateClick() {
		PhotonNetwork.JoinRoom("test");
	}

	public void OnHotSeatClientCreateClick() {
		Cyclades.Program.CreateHotSeatClient(Cyclades.Program.srv.conn_pull);
	}

	public void OnGameStartClick() {
		try {
			Cyclades.Program.StartServer((int)System.DateTime.Now.Ticks, true);
		} catch (Exception ex) {
			NGUIDebug.Log("ERROR: " + ex);
		}
		Application.LoadLevel("Main");
	}
	#endregion

	// We have two options here: we either joined(by title, list or random) or created a room.
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		Debug.Log ( "P c r = " + PhotonNetwork.countOfRooms );
	}
	
	public void OnPhotonCreateRoomFailed()
	{
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}
	
	public void OnPhotonJoinRoomFailed()
	{
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}
	public void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}
	
	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		//PhotonNetwork.LoadLevel(SceneNameGame);
	}

	public void OnFailedToConnectToPhoton(object parameters)
	{
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}

	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		
		/*string message;
		InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message
		
		if (chatComponent != null)
		{
			// to check if this client is the new master...
			if (player.isLocal)
			{
				message = "You are Master Client now.";
			}
			else
			{
				message = player.name + " is Master Client now.";
			}
			
			
			chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
		}*/
	}
	
	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		
		// back to main menu        
		Application.LoadLevel("Menu");
	}

	public void OnLeftLobby()
	{
		Debug.Log("OnLeftLobby (local)");
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		
		// back to main menu        
		Application.LoadLevel("Menu");
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}

	public void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby (local)");
	}

	public void OnConnectedToMaster(PhotonPlayer player)
	{
		Debug.Log("OnConnectedToMaster: " + player);
	}
}

