using UnityEngine;
using System.Collections;

public class UIGodMarsPanel : UIGamePanel {

	public void OnBuyUnitClick() {
		Debug.Log("buy unit");
	}

	public void OnMoveUnitClick() {
	}

	public void OnBuildClick() {
		Sh.GameState.MapPanel.SetEventerType(MapEventerType.BUILD);
	}

	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
