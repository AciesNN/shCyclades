using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAuctionInfo : UIGamePanel {

	public UITable Table;
	public GameObject UIAuctionGodPrefab;
	public List<UIAuctionGod> uiAuctionGods;

	private int players_number { get {return 5;} } //debug

	override protected void Init () {
		LateInit();
	}

	override protected void LateInit() {
		uiAuctionGods = new List<UIAuctionGod>();

		for (int i = 0; i < players_number; ++i) {
			AddGodInfoWidget(i);
		}
		
		Table.Reposition();	
	}

	#region UpdateData
	void Update() {
		UpdateData();
	}

	public void UpdateData() {
		for (int i = 0; i < players_number; ++i) {
			
			UIAuctionGod w = uiAuctionGods[i];
			
			w.SetUser(i);
			w.SetGod("");
			w.SetBet(i);
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
			Sh.Out.Send("make bet " + panel.CurrentBet + "; god: <" + panel.GodName + ">");
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
