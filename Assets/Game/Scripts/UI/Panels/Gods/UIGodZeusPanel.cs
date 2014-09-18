using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodZeusPanel : UIGamePanel {
	
	public void OnBuildClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEBUILD);
	}
	
	public void OnManClick() {
		Sh.Out.Send( Messanges.BuyPriest() );
	}
	
	public void OnCardChangeClick() {
		Sh.Out.Send( Messanges.ChangeCard() );
	}
	
	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}
}