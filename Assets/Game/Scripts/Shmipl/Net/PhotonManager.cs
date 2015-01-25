using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour
{
	PhotonView photonView;
	delegate void LogMethod(string msg);
	LogMethod log = Debug.Log; 

	[RPC]
	void PhotonNetworkRPC_ClientToServer(string name, string msg)
	{
		Cyclades.Program.srv.conn_pull.ReceiveMsg(name, msg);
		Debug.Log("PhotonNetworkRPC_ClientToServer" + msg);
	}

	[RPC]
	void PhotonNetworkRPC_ServerToClient(string msg)
	{ 
		Cyclades.Program.clnt.conn.ReceiveMsg(msg);
		Debug.Log("PhotonNetworkRPC_ServerToClient" + msg);
	}

	void Awake () {
		photonView = gameObject.GetComponent<PhotonView>();
	}

	// We have two options here: we either joined(by title, list or random) or created a room.
	public void OnJoinedRoom()
	{
		log("OnJoinedRoom");
	}
	
	public void OnPhotonCreateRoomFailed()
	{
		log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}
	
	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
		try {
			log("\t" + codeAndMsg[0] + "/" + codeAndMsg[1]);
		} finally {}
	}

	public void OnPhotonRandomJoinFailed()
	{
		log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinRandom filter-options can limit available rooms.");
	}
	
	public void OnCreatedRoom()
	{
		log("OnCreatedRoom");
	}
	
	public void OnFailedToConnectToPhoton(object parameters)
	{
		log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}
	
	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		log("OnMasterClientSwitched: " + player);
		
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
		log("OnLeftRoom (local)");
	}
	
	public void OnLeftLobby()
	{
		log("OnLeftLobby (local)");
	}
	
	public void OnDisconnectedFromPhoton()
	{
		log("OnDisconnectedFromPhoton");
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		log("OnPhotonPlayerConnected: " + player);
		
		Shmipl.FrmWrk.Net.UniversalServerConnection conn = new Shmipl.FrmWrk.Net.UniversalServerConnection(Cyclades.Program.srv.conn_pull);
		conn.send_msg = (string msg) => {
			PhotonNetwork.RPC(photonView, "PhotonNetworkRPC_ServerToClient", player, msg);
		};
		Cyclades.Program.ConnectNetClient(conn, player.name);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		log("OnPlayerDisconneced: " + player);
	}
	
	public void OnJoinedLobby()
	{
		log("OnJoinedLobby (local): " + PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms");
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();
		if (rooms.Length > 0) {
			log("\tthere're " + rooms.Length + " rooms: ");
			foreach(RoomInfo room in rooms) {
				log("\t" + room.name);
			}
		}
	}
	
	public void OnConnectedToMaster(PhotonPlayer player)
	{
		log("OnConnectedToMaster: " + player);
	}
}

