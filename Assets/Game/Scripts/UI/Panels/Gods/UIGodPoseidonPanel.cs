using UnityEngine;
using System.Collections;

public class UIGodPoseidonPanel : UIGamePanel {

	public void OnBuyUnitClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.PLACESHIP);
	}

	public void OnMoveUnitClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.MOVESHIP);
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.PLACEBUILD);
	}
	
	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
