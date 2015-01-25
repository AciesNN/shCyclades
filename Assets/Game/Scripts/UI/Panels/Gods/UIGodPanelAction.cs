using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodPanelAction: MonoBehaviour {

	public delegate void ActionClick();

	#region ViewWidgets
	public UISprite priceSprite;
	public UISprite mainSprite;
	public UISprite addSprite;
	public ActionClick click;
	#endregion

	public void SetPrice(long price) {
		if (price <= 0 || price > 4)
			priceSprite.spriteName = "";
		else
			priceSprite.spriteName = "price-card" + price + "a";
	}

	public void SetSprites(string spriteName, string addSpriteName) {
		gameObject.SetActive(spriteName != "");
		mainSprite.spriteName = spriteName;
		addSprite.spriteName = addSpriteName;
	}

}
