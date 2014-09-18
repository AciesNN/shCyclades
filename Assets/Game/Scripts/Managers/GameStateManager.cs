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
	
	public UIMapStates mapStates;

	protected override void Init() {
		base.Init ();

		currentUser = 0;
	}

	void SetAuctionState() {
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

	void SetCardState(bool isConcreteCard) {
		CardsTabsPanel.SetTab( isConcreteCard ? PanelType.CARD_TAB_INFO : PanelType.CARD_TAB_CARDS );		
	}

	void SetBattleState() {
		if (Sh.In.GameContext.GetBool("/fight/army/fight") || Sh.In.GameContext.GetBool("/fight/navy/fight")) {
			BattlePanel.Show();		
		} else {
			if (BattlePanel.IsActive()) {
				BattlePanel.Hide();
			}
		}
	}

	void SetMapEventorType() {
		string cur_state = Sh.In.GameContext.GetStr("/cur_state");
		switch (cur_state) {
			case "Turn.MoveNavy": {
				SetMapEventorType(MapEventerType.MOVESHIP);
				break;
			}
			case "Turn.PlaceMetroPhilosopher": {
				SetMapEventorType(MapEventerType.PLACEMETRO);
				break;
			}
			case "Turn.PlaceMetroBuilding": {
				SetMapEventorType(MapEventerType.PLACEMETRO);
				break;
			}

			case "Turn.Card.Use": {
				string card = Sh.In.GameContext.GetStr("/cards/open/[{0}]", Sh.In.GameContext.GetLong("/cards/open_card_number"));
				MapEventerType type = UIConsts.cardsMapEventors[card];
				SetMapEventorType(type);
				break;
			}

			default: {
				SetMapEventorType(MapEventerType.DEFAULT);
				break;
			}
		}
	}

	public void GameContext_UpdateData () {
		SetAuctionState();
		SetCardState(false);
		SetBattleState();

		SetMapEventorType();
	}

	void SetMapEventorType(MapEventerType type) {
		if (mapStates.GetEventorType() == type) {
			mapStates.ReActivate();
			return;
		}
		if (mapStates.GetEventorType() != MapEventerType.DEFAULT)
			mapStates.SetEventorType(MapEventerType.DEFAULT); //todo связано с тем, что новый евентер не будет иначе принят (защита от других кнопок, которую следует удалить)
		if(type != MapEventerType.DEFAULT)
			mapStates.SetEventorType(type);
	}
}
