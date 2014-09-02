using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodAppoloPanel : UIGamePanel {

	public void OnPlaceHorn() {
		Debug.Log("buy horn");
	}

	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}
}
