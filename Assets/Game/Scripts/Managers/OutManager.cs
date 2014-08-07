using UnityEngine;
using System.Collections;

public class OutManager : Manager<OutManager> {

	public void Send(string msg) {
		Debug.Log("send msg: " + msg);
	}
		
}
