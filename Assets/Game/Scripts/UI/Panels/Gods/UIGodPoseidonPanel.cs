using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodPoseidonPanel : UIGamePanel {

	#region ViewWidgets
	public GameObject BuyButtonsStrip;
	#endregion
	
	#region Events
	public void OnBuyUnitClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACESHIP);
	}

	public void OnMoveUnitClick() {
		Sh.Out.Send(Messanges.StartMoveNavy());
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEBUILD);
	}
	
	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}

	public void OnBuyUnitsClick() {
		OpenCloseBuyButton(!BuyButtonsStrip.activeSelf);
	}

	void OpenCloseBuyButton(bool open) {
		BuyButtonsStrip.SetActive(open);
	}
	#endregion
}
