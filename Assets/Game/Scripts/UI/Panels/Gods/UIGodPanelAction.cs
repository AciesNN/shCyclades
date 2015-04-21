using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodPanelAction: MonoBehaviour {

	public delegate void ActionClick();

	#region ViewWidgets
	public UISprite priceSprite;
	public UISprite mainSprite;
	public UISprite userRing;
	public ActionClick click;
	public GameObject button;

	UIImageButton[] buttons;
	#endregion

	void Awake() {
		buttons = button.GetComponents<UIImageButton>();
	}

	public void SetPrice(long price) {
		if (price <= 0 || price > 4)
			priceSprite.spriteName = "";
		else
			priceSprite.spriteName = "price-card" + price + "a";
	}

	public void SetActionSprite(string spriteName) {
		buttons[1].SetImageButtonSprites(spriteName, "1", "2");
	}

	public void SetPlayer(int player) {
		buttons[0].SetImageButtonSprites("sm-" + UIConsts.userColorsString[player], "1", "2");
	}
}
