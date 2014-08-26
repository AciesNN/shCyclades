using UnityEngine;
using System.Collections;

public class UIAdminPanel : UIGamePanel {

	public int curUser = -2;

	#region ViewWidgets
	public GameObject MainContext;
	public UILabel loadFileName;
	#endregion

	#region Events
	public void OnOpenCloseButtonClick() {
		MainContext.SetActive(!MainContext.activeSelf);
	}

	void OnChangeActiveUser(int curUser) {
		UIToggle t = UIToggle.GetActiveToggle(2); //волшебное число - номер группы радиобаттаннов "текущий юзверь"
		if (t && t.name != "" + curUser) 
			return;
		if (this.curUser != curUser) {
			this.curUser = curUser;
			Debug.Log("cur user set: " + curUser);
		}
	}

	public void OnChangeActiveUser0() {
		OnChangeActiveUser(0);
	}
	public void OnChangeActiveUser1() {
		OnChangeActiveUser(1);
	}
	public void OnChangeActiveUser2() {
		OnChangeActiveUser(2);
	}
	public void OnChangeActiveUser3() {
		OnChangeActiveUser(3);
	}
	public void OnChangeActiveUser4() {
		OnChangeActiveUser(4);
	}
	public void OnChangeActiveUserNull() {
		OnChangeActiveUser(-1);
	}

	/*
	TAB MODES
	*/
	public void SetAutionGodMode() {
		switch (Sh.GameState.auctionState) {
			case Cyclades.Game.Constants.godNone: Sh.GameState.auctionState = Cyclades.Game.Constants.godMars; break;
			case Cyclades.Game.Constants.godMars: Sh.GameState.auctionState = Cyclades.Game.Constants.godPoseidon; break;
			case Cyclades.Game.Constants.godPoseidon: Sh.GameState.auctionState = Cyclades.Game.Constants.godZeus; break;
			case Cyclades.Game.Constants.godZeus: Sh.GameState.auctionState = Cyclades.Game.Constants.godSophia; break;
			case Cyclades.Game.Constants.godSophia: Sh.GameState.auctionState = Cyclades.Game.Constants.godAppolon; break;
			case Cyclades.Game.Constants.godAppolon: Sh.GameState.auctionState = Cyclades.Game.Constants.godNone; break;
		}
	}

	public void SetCardsMode() {
		Sh.GameState.cardState = !Sh.GameState.cardState;
	}

	public void OpenBattlePanel() {
		Sh.GameState.battleState = !Sh.GameState.battleState;
	}

	public void OnDataLoad() {
		string path = "Assets\\Game\\Data\\test\\" + loadFileName.text + ".txt";
		string text = System.IO.File.ReadAllText(path).Replace("\n", " ");
		Sh.In._LoadContextFromText(text);
	}
	#endregion
}
