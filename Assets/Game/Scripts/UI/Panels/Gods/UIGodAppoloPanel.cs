using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodAppoloPanel : UIGamePanel {

	public void OnPlaceHorn() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEHORN);
	}

	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}
}
