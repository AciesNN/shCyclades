using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIUsersInfo : UIGamePanel {

	public UIGrid Grid;
	public GameObject UserInfoWidgetPrefab;
	public List<UIUserInfoWidget> userInfoWidgets;

	private int players_number { get {return 5;} } //debug

	override protected void Init () {
		LateInit();
	}

	override public void LateInit() {
		userInfoWidgets = new List<UIUserInfoWidget>();

		for (int i = 0; i < players_number; ++i) {
			AddUserInfoWidget("" + i + ". ");
		}

		Grid.Reposition();

		SetCurrentUserNumber(1);
	}

	#region UpdateData
	void Update() {
		UpdateData();
	}

	public void UpdateData() {

		for (int i = 0; i < players_number; ++i) {

			UIUserInfoWidget w = userInfoWidgets[i];

			w.SetUser(i);
			w.SetGod("");
			w.SetUserIncome(i);
			w.SetPhilosothsNumber(i);
			w.SetIsMetro(i == 2);
		}

		SetCurrentUserNumber(1);
	}
	#endregion

	private void AddUserInfoWidget(string name_prefix) {
		GameObject userInfoWidget_go = (GameObject) NGUITools.AddChild(Grid.gameObject, UserInfoWidgetPrefab);
		userInfoWidget_go.name = name_prefix + userInfoWidget_go.name;
		UIUserInfoWidget userInfoWidget = userInfoWidget_go.GetComponent<UIUserInfoWidget>();
		userInfoWidgets.Add (userInfoWidget);
	}
	
	#region Widgets
	public void SetCurrentUserNumber(int currentUserNumber) {
		foreach(UIUserInfoWidget uiw in userInfoWidgets)
			uiw.SetIsCurrentUser(false);
		userInfoWidgets[currentUserNumber].SetIsCurrentUser(true);
	}
	#endregion
}
