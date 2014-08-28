using UnityEngine;
using System.Collections;

public class UIGodMarsPanel : UIGamePanel {
	
	#region ViewWidgets
	public GameObject BuyButtonsStrip;
	#endregion

	Vector3 pricePos;

	void Awake() {
		pricePos = BuyButtonsStrip.transform.localPosition;
	}

	#region Events
	public void OnBuyUnitClick() {
		OpenCloseBuyButton(false);
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

	public void OnBuyUnitsClick() {
		OpenCloseBuyButton(!BuyButtonsStrip.activeSelf);
	}

	void OpenCloseBuyButton(bool open) {
		BuyButtonsStrip.SetActive(open);
		AnimatePricePosition(open);
	}

	public void OnSelect() {
		Debug.Log ("...");
	}
	#endregion

	void AnimatePricePosition (bool on)
	{
		Vector3 target;
		Vector3 start;

		if (on) {
			target = pricePos;
			start = transform.localPosition;
		} else {
			target = transform.localPosition;
			start = pricePos;
		}

		BuyButtonsStrip.transform.localPosition = start;
		
		GameObject go = BuyButtonsStrip.gameObject;
		TweenPosition.Begin(go, 0.15f, target).method = UITweener.Method.EaseOut;
	}
}
