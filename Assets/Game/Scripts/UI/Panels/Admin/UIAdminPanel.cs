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
		//TODO
		/*UIToggle t = UIToggle.GetActiveToggle(toggleGroupe); //волшебное число - номер группы радиобаттаннов "текущий юзверь"
		if (t && t.name != "" + Sh.GameState.currentUser) 
			return;
		if (Sh.GameState.currentUser != curUser) {
			Sh.GameState.currentUser = curUser;
		}*/
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
			Sh.In._LoadContextFromCash(loadFileName.text);
		#else
		string path = "Assets\\Game\\Data\\test\\" + loadFileName.text + ".txt";
			string text = System.IO.File.ReadAllText(path).Replace("\n", " ");
			Sh.In._LoadContextFromText(text);
			//NGUIDebug.Log("" + Shmipl.Base.json.dumps( Cyclades.Program.srv.GetContext("Game").data));
		#endif
	}

	public void OnDataSave() {
		string text = Sh.In.GameContext.ToString();
		#if UNITY_WEBPLAYER
			Sh.In._testDataCash[loadFileName.text] = text;
		#else
			string path = "Assets\\Game\\Data\\test\\" + loadFileName.text + ".txt";
			System.IO.File.WriteAllText(path, text);
			Debug.Log (text);
		#endif
	}

	public void OnUpdateData() {
		Sh.GameState.GameContext_UpdateData(true);
	}
	#endregion
}
