using UnityEngine;
using System.Collections;

public class UIGodZeusPanel : UIGamePanel {
	
	public void OnBuildClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.BUILD);
	}
	
	public void OnManClick() {
	}
	
	public void OnCardChangeClick() {
	}
	
	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}