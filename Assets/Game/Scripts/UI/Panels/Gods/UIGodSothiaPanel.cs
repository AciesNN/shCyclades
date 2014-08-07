using UnityEngine;
using System.Collections;

public class UIGodSothiaPanel : UIGamePanel {
	
	public void OnBuyUnitClick() {
		Debug.Log("buy unit");
	}
	
	public void OnManClick() {
	}
	
	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
