using UnityEngine;
using System.Collections;

public class UIGodMarsPanel : UIGamePanel {

	public void OnBuyUnitClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.PLACEUNIT);
	}

	public void OnMoveUnitClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.MOVEUNIT);
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.PLACEBUILD);
	}

	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
