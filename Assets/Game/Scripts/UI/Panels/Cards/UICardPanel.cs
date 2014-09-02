using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UICardPanel : UIGamePanel {
	public List<GameObject> cards;
	List<UIImageButtonSh> icons = new List<UIImageButtonSh>();
	List<UIImageButtonSh> prices = new List<UIImageButtonSh>();

	override protected void Init() {
		if(cards.Count != 3)
			Debug.LogError("не заданы объекты иконок карт");

		foreach(GameObject go in cards) {
			UIImageButtonSh[] imbs = go.GetComponents<UIImageButtonSh>();
			icons.Add (imbs[0]);
			prices.Add(imbs[1]);
		}
	}

	public void GameContext_UpdateData() {
		for(int i = 0; i < cards.Count; ++i) {
			string c = GetSlotCard(i);
			int price = (c == Constants.cardNone ? 0 : GetSlotPrice(i));

			UIImageButtonSh b = icons[i];
			UIImageButtonSh p = prices[i];

			b.normalSprite = "card-button-" + UIConsts.cardIconSprites[c] + (UIConsts.cardIconSprites[c] == "empty" ? "" : "1");
			b.hoverSprite = "card-button-" +  UIConsts.cardIconSprites[c] + (UIConsts.cardIconSprites[c] == "empty" ? "" : "2");
			b.pressedSprite = b.hoverSprite;
			b.disabledSprite = b.normalSprite;
				

			p.normalSprite = "price-card" + price + "a";
			p.hoverSprite = "price-card" + price + "b";
			p.pressedSprite = p.hoverSprite;
			p.disabledSprite = p.normalSprite;

			b.target.spriteName = b.normalSprite;	
			p.target.spriteName = p.normalSprite;	
		}
	}

	public void SetHighlightCardIcon(int slot, bool on) {
		icons[slot].isHighlight = on;
		prices[slot].isHighlight = on;
	}

	#region Events
	public void OnCardClick(int slot) {
		string card = GetSlotCard(slot);
		if (card == Constants.cardNone)
			return;

		UIBuyCardPanel panel = UIGamePanel.GetPanel<UIBuyCardPanel>(PanelType.BUY_CARD_PANEL);

		panel.slot = slot;
		panel.Card = card;
		panel.SetPrice(GetSlotPrice(slot));

		UIGamePanel.ShowPanel(PanelType.BUY_CARD_PANEL, this);
	}

	public void OnCard1Click() {
		OnCardClick(0);
	}
	public void OnCard2Click() {
		OnCardClick(1);
	}
	public void OnCard3Click() {
		OnCardClick(2);
	}
	#endregion

	int GetSlotPrice(int slot) {
		return (int)Library.Card_GoldForSlot(Sh.In.GameContext, slot, Sh.GameState.currentUser);
	}

	string GetSlotCard(int slot) {
		List<object> _cards = Sh.In.GameContext.GetList("/cards/open");
		if (slot >= _cards.Count)
			return Constants.cardNone;
		else
			return _cards[slot] as string;
	}
}
