using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game.Client;

public class UIBuyCardPanel : UIGamePanel {

	#region ViewWidgets
	public UISprite CardSprite;
	public UILabel CardText;
	public List<GameObject> PriceElements;
	[HideInInspector]public int slot;
	#endregion

	private string card;
	public string Card {
		get { return card; }
		set {
			card = value;
			CardSprite.spriteName =  "card-" + UIConsts.cardIconSprites[card];
			CardText.text = (UIConsts.cardIconSprites[card] == "empty" ? card : "");
		}
	}

	#region ViewWidgetsSet
	public void SetPrice(int price) {
		for (int i = 0; i < PriceElements.Capacity; ++i)
			PriceElements[i].SetActive(i < price);
	}
	#endregion

	#region Events
	public void OnPressOKButton() {
		Sh.Out.Send(Messanges.BuyCard(slot));
		Hide();
	}

	public void OnPressCancelButton() {
		Hide();
	}
	#endregion

	override protected void OnPanelClose() {
		(parentPanel as UICardPanel).SetHighlightCardIcon(slot, false);
	}

	override protected void OnPanelOpen() {
		(parentPanel as UICardPanel).SetHighlightCardIcon(slot, true);
	}
}
