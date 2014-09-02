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
			Cyclades.Game.Client.Messanges.cur_player = CurrentUser;
			//UIToggle.GetActiveToggle(UIAdminPanel.toggleGroupe). = CurrentUser;
		}
	}

	public UIMapController mapController;
	public UIGamePanelTabs AuctionTabsPanel;

	public UIGamePanelTabs CardsTabsPanel;

	public UIGamePanel BattlePanel;
	
	public MapEventerType mapEventerType;
	public UIMapStates mapStates;

	protected override void Init() {
		base.Init ();

		currentUser = 0;
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

	public void SetBattleState() {
		if (Sh.In.GameContext.GetBool("/fight/army/fight") || Sh.In.GameContext.GetBool("/fight/navy/fight")) {
			BattlePanel.Show();		
		} else {
			if (BattlePanel.IsActive()) {
				BattlePanel.Hide();
			}
		}
	}

	public void SetMetroBuildState() {
		string cur_state = Sh.In.GameContext.GetStr("/cur_state");
		if (cur_state == "Turn.PlaceMetroPhilosopher" || cur_state == "Turn.PlaceMetroBuilding") {
			mapStates.SetEventorType(MapEventerType.PLACEMETRO);
		} else {
			if (mapStates.GetEventorType() == MapEventerType.PLACEMETRO) {
				mapStates.SetEventorType(MapEventerType.DEFAULT);
			}
		}
	}

	public void GameContext_UpdateData () {
		SetAuctionState();
		SetCardState(false);
		SetBattleState();
		SetMetroBuildState();
	}
}
