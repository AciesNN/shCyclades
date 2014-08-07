using UnityEngine;
using System.Collections;

public class UIGodZeusPanel : UIGamePanel {
	
	public void OnBuyUnitClick() {
		Debug.Log("buy unit");
	}
	
	public void OnManClick() {
	}
	
	public void OnCardChangeClick() {
	}
	
	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}