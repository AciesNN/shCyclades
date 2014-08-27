using UnityEngine;
using System.Collections;

public class UICardPanel : UIGamePanel {

	public void OnCardClick(int cardSlot) {
		UIBuyCardPanel panel = UIGamePanel.GetPanel<UIBuyCardPanel>(PanelType.BUY_CARD_PANEL);

		panel.slot = cardSlot;
		panel.Card = Cyclades.Game.Constants.cardChimera;
		panel.SetPrice(4-cardSlot);

		UIGamePanel.ShowPanel(PanelType.BUY_CARD_PANEL);
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

}
