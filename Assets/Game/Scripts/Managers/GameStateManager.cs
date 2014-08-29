using UnityEngine;
using System.Collections;

using Cyclades.Game;

/*содержит информацию о том, в какие состояния при этом должны быть совершены*/
public class GameStateManager : Manager<GameStateManager> {

	int CurrentUser;
	public int currentUser {
		get { return CurrentUser; }
		set {
			CurrentUser = value;
			NGUIDebug.Log ("current user: " + CurrentUser);
			//UIToggle.GetActiveToggle(UIAdminPanel.toggleGroupe). = CurrentUser;
		}
	}

	public UIMapController mapController;
	public UIGamePanelTabs AuctionTabsPanel;

	public UIGamePanelTabs CardsTabsPanel;

	public bool cardState;
	public UIGamePanel BattlePanel;

	bool oldBattleState;
	public bool battleState;
	
	public MapEventerType mapEventerType;
	public UIMapStates mapStates;

	protected override void Init() {
		currentUser = -1;
	}

	public void SetAuctionState() {
		Cyclades.Game.Phase phase = Library.GetPhase(Sh.In.GameContext);
		//TODO есть еще странная фаза
		if (phase == Cyclades.Game.Phase.AuctionPhase) {
			AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_INFO);
		} else {
			string currentGod = Sh.In.GameContext.GetStr("/turn/current_god");
			switch (currentGod) {
				case Cyclades.Game.Constants.godNone: 		 break;
				case Cyclades.Game.Constants.godPoseidon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_POSEIDON); break;
				case Cyclades.Game.Constants.godMars: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_MARS); break;
				case Cyclades.Game.Constants.godSophia: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_SOTHIA); break;
				case Cyclades.Game.Constants.godZeus: 		AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_ZEUS); break;
				case Cyclades.Game.Constants.godAppolon: 	AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD_APPOLON); break;
			}
		}
	}

	public void SetCardState(bool isConcreteCard) {
		CardsTabsPanel.SetTab( isConcreteCard ? PanelType.CARD_TAB_INFO : PanelType.CARD_TAB_CARDS );		
	}

	public void SetBattleState(bool isBattle) {
		if (isBattle != oldBattleState) {
			oldBattleState = isBattle;
			if (isBattle) {
				//BattlePanel.Init();
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

	public void GameContext_UpdateData () {
		SetAuctionState();
		SetCardState(cardState);
		SetBattleState(battleState);
		SetMapEventorType(mapEventerType);
	}
}
