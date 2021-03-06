using UnityEngine;
using System.Collections;

using Cyclades.Game;

/*содержит информацию о том, в какие состояния при этом должны быть совершены*/
public class GameStateManager : Manager<GameStateManager> {
	public void GameContext_NewData ()
	{
		throw new System.NotImplementedException ();
	}

	private long _currentUser;
	public long currentUser {
		get { return _currentUser; }
		set {
			//NGUIDebug.Log ("current user: " + value);
			_currentUser = value;
		}
	}

	public UIMapController mapController;

	public UIGamePanelTabs AuctionTabsPanel;
	public UIGodPanel AuctionGodPanel;

	public UIGamePanelTabs CardsTabsPanel;

	public UIGamePanel BattlePanel;
	
	public UIMapStates mapStates;

	public GameObject rootUI;

	protected override void Init() {
		base.Init();
	}

	void SetAuctionState() {
		Cyclades.Game.Phase phase = Library.GetPhase(Sh.In.GameContext);
		//TODO есть еще странная фаза
		if (phase == Cyclades.Game.Phase.AuctionPhase) {
			AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_INFO);
		} else {
			AuctionTabsPanel.SetTab(PanelType.AUCTION_TAB_GOD);
			int currentPlayer = Sh.In.GameContext.GetInt("/turn/current_player");
			AuctionGodPanel.SetPlayer(currentPlayer);
			AuctionGodPanel.Reset();
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
				if (type == MapEventerType.DEFAULT)
					Debug.LogError("Для карты " + card + " не определен Map eventor");

				SetMapEventorType(type);
				break;
			}

			default: {
				//SetMapEventorType(MapEventerType.DEFAULT);
				break;
			}
		}
	}

	public void GameContext_UpdateData (bool deserialize) {
		SetAuctionState();
		SetCardState(false);
		SetBattleState();

		SetMapEventorType();

		rootUI.BroadcastMessage("GameContext_UpdateData", deserialize, SendMessageOptions.DontRequireReceiver);
	}

	public void GameContext_ShowAnimation (Hashtable msg) {
		rootUI.BroadcastMessage("GameContext_ShowAnimation", msg, SendMessageOptions.DontRequireReceiver);
	}

	public void GameContext_LateInit () {
		rootUI.BroadcastMessage("GameContext_LateInit", SendMessageOptions.DontRequireReceiver);
	}

	public void GameContext_Init() {
		rootUI.BroadcastMessage("GameContext_Init", SendMessageOptions.DontRequireReceiver);
	}

	void SetMapEventorType(MapEventerType type) {
		if(type != MapEventerType.DEFAULT)
			mapStates.SetEventorType(type);
	}
}
