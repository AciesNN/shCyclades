using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour
{
	[RPC]
	void PhotonNetworkRPC_ClientToServer(string msg)
	{ 
		Debug.Log("PhotonNetworkRPC_ClientToServer" + msg);
	}

	[RPC]
	void PhotonNetworkRPC_ServerToClient(string msg)
	{ 
		Debug.Log("PhotonNetworkRPC_ServerToClient" + msg);
	}
}

