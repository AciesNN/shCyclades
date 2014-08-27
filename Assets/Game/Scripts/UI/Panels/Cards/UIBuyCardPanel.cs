using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBuyCardPanel : UIGamePanel {

	#region ViewWidgets
	public UISprite CardSprite;
	public UILabel CardText;
	public List<GameObject> PriceElements;
	public int slot;
	#endregion

	private string card;
	public string Card {
		get { return card; }
		set {
			card = value;
			//CardSprite.spriteName = "";
			//CardText.text = "";
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
		Sh.Out.Send("buy card from slot " + slot);
		UIGamePanel.CloseActivePanel();
	}

	public void OnPressCancelButton() {
		UIGamePanel.CloseActivePanel();
	}
	#endregion
}
