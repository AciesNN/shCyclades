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
	#endregion

	private long players_number { get { return Sh.In.GameContext.GetLong("/players_number"); } }

	override protected void Init () {
	}

	override public void GameContext_LateInit() {
		uiAuctionGods = new List<UIAuctionGod>();

		for (int i = 0; i < players_number; ++i) {
			AddGodInfoWidget(i);
		}
		
		Table.Reposition();	
	}

	#region UpdateData
	public void GameContext_UpdateData() {
		for (int i = 0; i < players_number; ++i) {
			
			UIAuctionGod w = uiAuctionGods[i];
			
			w.SetUser(Library.Aiction_GetCurrentBetPlayerForGod(Sh.In.GameContext, i));
			w.SetGod(Sh.In.GameContext.Get<string>("/auction/gods_order/[{0}]", i));
			int player = Library.Aiction_GetCurrentBetPlayerForGod(Sh.In.GameContext, i);
			if (player == -1)
				w.SetBet(0);
			else
				w.SetBet((int)Library.Auction_GetCurrentBetForPlayer(Sh.In.GameContext, player));
		}
	}
	#endregion

	private UIAuctionGod AddGodInfoWidget(int w_number) {
		GameObject w_go = (GameObject) NGUITools.AddChild(Table.gameObject, UIAuctionGodPrefab);
		w_go.name = "" + w_number + ". " + w_go.name;
		UIAuctionGod w = w_go.GetComponent<UIAuctionGod>();

		w.AuctionInfoPanel = this;
		w.MyNumber = w_number;

		uiAuctionGods.Add (w);
		return w;
	}

	#region Events
	public void OnBetPanelClose(ModelPanelCloseResult res) {
		UIAuctionPanel panel = UIGamePanel.GetPanel<UIAuctionPanel>(PanelType.AUCTION_PANEL);
		if (res == ModelPanelCloseResult.OK)
			Sh.Out.Send(Messanges.MakeBet(panel.CurrentBet, panel.GodName));
	}

	public void OnConreteAuctionGodClick(int number) {
		UIAuctionPanel panel = UIGamePanel.GetPanel<UIAuctionPanel>(PanelType.AUCTION_PANEL);

		panel.MinBet = 0;
		panel.MaxBet = 10;
		panel.GodName = Cyclades.Game.Constants.godMars;
		panel.CurrentBet = number;

		UIGamePanel.ShowPanel(PanelType.AUCTION_PANEL, this);
	}
	#endregion
}
