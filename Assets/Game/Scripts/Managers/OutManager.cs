using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutManager : Manager<OutManager> {

	public void Send(Hashtable msg) {
		Debug.Log("send msg: " + Shmipl.Base.json.dumps(msg));

		if (!Cyclades.Program.clnts.ContainsKey(Sh.GameState.currentUser)) {
			Debug.LogError("Не найден клиент юзера " + Sh.GameState.currentUser);
			return;
		}

		Cyclades.Program.clnts[Sh.GameState.currentUser].send(msg);
	}

}