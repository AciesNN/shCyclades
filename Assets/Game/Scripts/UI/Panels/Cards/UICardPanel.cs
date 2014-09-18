using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UICardPanel : UIGamePanel {

	#region ViewWidgets
	public UIGrid grid;
	public GameObject cardWidgetPrefub;
	List<CardButtonWidget> cardWidgets;
	#endregion

	override protected void Init() {
		cardWidgets = new List<CardButtonWidget>();
		for (int i = 0; i < 3; ++i)
			AddCardButtonWidget(i);
		grid.Reposition();
	}

	private CardButtonWidget AddCardButtonWidget(int w_number) {
		GameObject w_go = (GameObject) NGUITools.AddChild(grid.gameObject, cardWidgetPrefub);
		w_go.name = "" + w_number + ". " + w_go.name;
		CardButtonWidget w = w_go.GetComponent<CardButtonWidget>();
		
		w.parentPanel = this;
		w.number = w_number;
		w.LateInit();

		cardWidgets.Add (w);
		return w;
	}

	#region UpdateData
	override protected void GameContext_UpdateData_Panel() {

		for(int i = 0; i < 3; ++i) {
			string card = GetSlotCard(i);
			int price = (card == Constants.cardNone ? 0 : GetSlotPrice(i));

			cardWidgets[i].SetIcon(card);
			cardWidgets[i].SetPrice(price);
		}
	}

	public void SetHighlightCardIcon(int slot, bool on) {
		cardWidgets[slot].SetHighlight(on);
	}
	#endregion

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
