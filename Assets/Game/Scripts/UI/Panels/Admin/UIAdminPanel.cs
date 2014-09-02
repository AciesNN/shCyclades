using UnityEngine;
using System.Collections;

public class UIAdminPanel : UIGamePanel {

	static public readonly int toggleGroupe = 2;

	#region ViewWidgets
	public GameObject MainContext;
	public UILabel loadFileName;
	#endregion

	#region Events
	public void OnOpenCloseButtonClick() {
		MainContext.SetActive(!MainContext.activeSelf);
	}

	void OnChangeActiveUser(int curUser) {
		UIToggle t = UIToggle.GetActiveToggle(toggleGroupe); //волшебное число - номер группы радиобаттаннов "текущий юзверь"
		if (t && t.name != "" + Sh.GameState.currentUser) 
			return;
		if (Sh.GameState.currentUser != curUser) {
			Sh.GameState.currentUser = curUser;
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
	public void OnDataLoad() {
		#if UNITY_WEBPLAYER
		Sh.In._LoadContextFromCash(text);
		#else
		string path = "Assets\\Game\\Data\\test\\" + loadFileName.text + ".txt";
		string text = System.IO.File.ReadAllText(path).Replace("\n", " ");
		Sh.In._LoadContextFromText(text);
		#endif
	}
	#endregion
}
