using UnityEngine;
using System.Collections;

public class UIGodPoseidonPanel : UIGamePanel {

	#region ViewWidgets
	public GameObject BuyButtonsStrip;
	#endregion
	
	#region Events
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

	public void OnBuyUnitsClick() {
		OpenCloseBuyButton(!BuyButtonsStrip.activeSelf);
	}

	void OpenCloseBuyButton(bool open) {
		BuyButtonsStrip.SetActive(open);
	}
	#endregion
}
