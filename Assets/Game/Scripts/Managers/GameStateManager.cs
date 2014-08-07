using UnityEngine;
using System.Collections;

/*содержит информацию о том, в какие состояния при этом должны быть совершены*/
public class GameStateManager : Manager<GameStateManager> {

	public UIGamePanelTabs AuctionTabsPanel;
	public string auctionState;
	public UIGamePanelTabs CardsTabsPanel;
	public bool cardState;
	
	private void SetAuctionState(string currentGod) {
		switch (currentGod) {
			case Cyclades.Game.Constants.godNone: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_INFO); break;
			case Cyclades.Game.Constants.godPoseidon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_POSEIDON); break;
			case Cyclades.Game.Constants.godMars: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_MARS); break;
			case Cyclades.Game.Constants.godSophia: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_SOTHIA); break;
			case Cyclades.Game.Constants.godZeus: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_ZEUS); break;
			case Cyclades.Game.Constants.godAppolon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_APPOLON); break;
		}			
	}

	private void SetCardState(bool isConcreteCard) {
		CardsTabsPanel.SetTab( isConcreteCard ? PanelType.CARD_TAB_INFO : PanelType.CARD_TAB_CARDS );		
	}
	
	void Update () {
		SetAuctionState(auctionState);
		SetCardState(cardState);
	}
}
