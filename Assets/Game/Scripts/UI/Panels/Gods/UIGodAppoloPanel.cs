using UnityEngine;
using System.Collections;

public class UIGodAppoloPanel : UIGamePanel {

	public void OnPlaceHorn() {
		Debug.Log("buy unit");
	}

	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
