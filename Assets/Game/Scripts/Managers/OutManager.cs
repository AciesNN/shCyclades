using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutManager : Manager<OutManager> {

	public void Send(Hashtable msg) {
		Debug.Log("send msg: " + Shmipl.Base.json.dumps(msg));
		//NGUIDebug.Log(msg);
		Cyclades.Program.SendToSrv(msg);
	}
		
}
