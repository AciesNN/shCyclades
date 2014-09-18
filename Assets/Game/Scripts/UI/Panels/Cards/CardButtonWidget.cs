using UnityEngine;
using System.Collections;

public class CardButtonWidget : MonoBehaviour {

	#region V`iewWidgets
	UIImageButtonSh iconW;
	UIImageButtonSh priceW;
	public GameObject root;

	public UILabel _text;
	#endregion

	[HideInInspector]public UICardPanel parentPanel;
	[HideInInspector]public int number;

	public void LateInit() {
		UIImageButtonSh[] imbs = root.GetComponents<UIImageButtonSh>();
		iconW = imbs[0];
		priceW = imbs[1];
	}

	#region ViewWidgetsSet
	public void SetIcon(string card) {
		string cs = UIConsts.cardIconSprites[card];
		iconW.normalSprite = "card-button-" + cs + (cs == "empty" ? "" : "1");
		iconW.hoverSprite = "card-button-" +  cs + (cs == "empty" ? "" : "2");
		iconW.pressedSprite = iconW.hoverSprite;
		iconW.disabledSprite = iconW.normalSprite;

		iconW.target.spriteName = iconW.normalSprite;	

		_text.text = (cs == "empty" ? card : "");
	}

	public void SetPrice(int price) {
		priceW.normalSprite = "price-card" + price + "a";
		priceW.hoverSprite = "price-card" + price + "b";
		priceW.pressedSprite = priceW.hoverSprite;
		priceW.disabledSprite = priceW.normalSprite;
				
		priceW.target.spriteName = priceW.normalSprite;	
	}

	public void SetHighlight(bool on) {
		iconW.isHighlight = on;
		priceW.isHighlight = on;
	}
	#endregion

	#region Events
	public void OnCardClick() {
		parentPanel.OnCardClick(number);
	}
	#endregion
}
