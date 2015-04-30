using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

public class UIAuctionInfo : UIGamePanel {

	#region ViewWidgets
	public UITable Table;
	public GameObject UIAuctionGodPrefab;
	public List<UIAuctionGod> uiAuctionGods;
	public UIAuctionGod uiAuctionGodApollo;
	#endregion

	private long players_number { get { return Sh.In.GameContext.GetLong("/players_number"); } }

	override protected void Init () {
	}

	override public void GameContext_LateInit() {

		for (int i = 0; i < uiAuctionGods.Count; ++i) {
			if (i < players_number - 1) {
				uiAuctionGods[i].AuctionInfoPanel = this;
				uiAuctionGods[i].MyNumber = i;
			} else {
				uiAuctionGods[i].gameObject.SetActive(false);
			}
		}

		int j = (int)players_number - 1;
		uiAuctionGods[j] = uiAuctionGodApollo;
		uiAuctionGods[j].AuctionInfoPanel = this;
		uiAuctionGods[j].MyNumber = j;
		uiAuctionGods[j].gameObject.SetActive(true);
	}

	#region UpdateData
	override protected void GameContext_UpdateData_Panel(bool deserialize) {

		TabloidPanel.inst.SetText("Время приносить жертвы богам!");

		for (int i = 0; i < players_number; ++i) {
			
			UIAuctionGod w = uiAuctionGods[i];
			
			w.SetUser(Library.Aiction_GetCurrentBetPlayerForGod(Sh.In.GameContext, i));
			string god = Sh.In.GameContext.Get<string>("/auction/gods_order/[{0}]", i);
			w.SetGod(god);
			int player = Library.Aiction_GetCurrentBetPlayerForGod(Sh.In.GameContext, i);
			if (player == -1)
				w.SetBet(0);
			else {
				if (god == Cyclades.Game.Constants.godAppolon) {
					List<int> bets = Library.Auction_GetAllOrderBetPlayersForGod(Sh.In.GameContext, i);
					w.SetApolloBets(bets);
				} else {
					int bet = (int)Library.Auction_GetCurrentBetForPlayer(Sh.In.GameContext, player);
					if (bet < 0)
						w.SetBet(0);
					else
						w.SetBet(bet);
				}
			}
		}
	}
	#endregion

	#region Events
	public void OnBetPanelClose(ModelPanelCloseResult res) {
		UIAuctionPanel panel = UIGamePanel.GetPanel<UIAuctionPanel>(PanelType.AUCTION_PANEL);
		if (res == ModelPanelCloseResult.OK)
			Sh.Out.Send(Messanges.MakeBet(panel.CurrentBet, panel.GodName));
	}

	public void OnConreteAuctionGodClick(int number) {
		if (number == Library.Auction_GetCurrentGodBetForPlayer(Sh.In.GameContext, Sh.GameState.currentUser))
			return;

		UIAuctionPanel panel = UIGamePanel.GetPanel<UIAuctionPanel>(PanelType.AUCTION_PANEL);

		if (number < players_number-1) { // not appolon
			int player = Library.Aiction_GetCurrentBetPlayerForGod(Sh.In.GameContext, number);
			if (player >= 0)
				panel.MinBet = Sh.In.GameContext.GetInt("/auction/bets/[{0}]/[{1}]", number, player) + 1;
			else
				panel.MinBet = 1;
			panel.MaxBet = Sh.In.GameContext.GetInt("/markers/gold/[{0}]", Sh.GameState.currentUser) + Sh.In.GameContext.GetInt("/markers/priest/[{0}]", Sh.GameState.currentUser);
		} else { //Appolo
			int bet = Library.Auction_GetAllOrderBetPlayersForGod(Sh.In.GameContext, number).Count + 1;
			panel.MinBet = bet;
			panel.MaxBet = bet;
		}

		if (panel.MaxBet >= panel.MinBet) {
			panel.GodName = Sh.In.GameContext.Get<string>("/auction/gods_order/[{0}]", number);
			panel.CurrentBet = panel.MinBet;

			UIGamePanel.ShowPanel(PanelType.AUCTION_PANEL, this);
		}
	}
	#endregion
}
