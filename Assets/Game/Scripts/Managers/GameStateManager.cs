using UnityEngine;
using System.Collections;

/*содержит информацию о том, в какие состояния при этом должны быть совершены*/
public class GameStateManager : Manager<GameStateManager> {

	public UIGamePanelTabs AuctionTabsPanel;

	public string auctionState;
	public UIGamePanelTabs CardsTabsPanel;

	public bool cardState;
	public UIGamePanel BattlePanel;

	bool oldBattleState;
	public bool battleState;
	
	public MapEventerType mapEventerType;
	public UIMapStates mapStates;

	public void SetAuctionState(string currentGod) {
		switch (currentGod) {
			case Cyclades.Game.Constants.godNone: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_INFO); break;
			case Cyclades.Game.Constants.godPoseidon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_POSEIDON); break;
			case Cyclades.Game.Constants.godMars: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_MARS); break;
			case Cyclades.Game.Constants.godSophia: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_SOTHIA); break;
			case Cyclades.Game.Constants.godZeus: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_ZEUS); break;
			case Cyclades.Game.Constants.godAppolon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_APPOLON); break;
		}			
	}

	public void SetCardState(bool isConcreteCard) {
		CardsTabsPanel.SetTab( isConcreteCard ? PanelType.CARD_TAB_INFO : PanelType.CARD_TAB_CARDS );		
	}

	public void SetBattleState(bool isBattle) {
		if (isBattle != oldBattleState) {
			oldBattleState = isBattle;
			if (isBattle) {
				BattlePanel.LateInit();
				BattlePanel.Show ();
			} else {
				BattlePanel.Hide ();
			}
		}
	}

	public void SetMapEventorType(MapEventerType mapEventerType) {
		mapStates.SetType(mapEventerType);
	}

	public void UpdateMapEventorType(MapEventerType mapEventerType) {
		this.mapEventerType = mapEventerType;
	}

	void Update () {
		SetAuctionState(auctionState);
		SetCardState(cardState);
		SetBattleState(battleState);
		SetMapEventorType(mapEventerType);
	}
}
