using UnityEngine;
using System.Collections;

public class UIGodPoseidonPanel : UIGamePanel {

	public void OnBuyUnitClick() {
		Debug.Log("buy unit");
	}

	public void OnMoveUnitClick() {
	}

	public void OnBuildClick() {
		Sh.GameState.MapPanel.SetEventer(MapEventerType.BUILD);
	}
	
	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
