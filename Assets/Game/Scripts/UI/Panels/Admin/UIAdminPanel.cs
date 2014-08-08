using UnityEngine;
using System.Collections;

public class UIAdminPanel : UIGamePanel {

	public int curUser = -2;

	#region ViewWidgets
	public GameObject MainContext;
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
	#endregion
}
