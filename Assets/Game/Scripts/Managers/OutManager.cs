using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutManager : Manager<OutManager> {

	public void Send(Hashtable msg) {
		Debug.Log("send msg: " + Shmipl.Base.json.dumps(msg));

		Shmipl.FrmWrk.Client.DispetcherFSM dsp = null;
		if (Cyclades.Program.isServer) {
			if (Cyclades.Program.clnts.ContainsKey(Sh.GameState.currentUser))
				dsp = Cyclades.Program.clnts[Sh.GameState.currentUser];
		} else {
			dsp = Cyclades.Program.clnt;
		}

		if (dsp == null) {
			Debug.LogError("Не найден клиент юзера " + Sh.GameState.currentUser);
			return;
		} else {
			dsp.conn.SendMsg(msg);
		}

	}

}