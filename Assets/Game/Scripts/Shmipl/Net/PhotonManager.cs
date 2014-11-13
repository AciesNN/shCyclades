using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour
{
	PhotonView photonView;

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
	}
	
	public void OnLeftLobby()
	{
		Debug.Log("OnLeftLobby (local)");
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
		
		Shmipl.FrmWrk.Net.UniversalServerConnection conn = new Shmipl.FrmWrk.Net.UniversalServerConnection(Cyclades.Program.srv.conn_pull);
		conn.send_msg = (string msg) => {
			PhotonNetwork.RPC(photonView, "PhotonNetworkRPC_ServerToClient", player, msg);
		};
		Cyclades.Program.ConnectNetClient(conn, player.name);
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

