using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodSothiaPanel : UIGamePanel {
	
	public void OnManClick() {
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEBUILD);
	}

	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}
}
