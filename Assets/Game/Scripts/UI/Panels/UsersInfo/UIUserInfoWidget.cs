using UnityEngine;
using System.Collections;

public class UIUserInfoWidget : MonoBehaviour {

	#region VewWidgets
	public UILabel UserNumberLabel;
	public UISprite UserColorSprite;
	public UILabel IncomeLabel;
	public UILabel PhilosothersLabel;
	public GameObject IsMetro;
	public GameObject CurrenUserFlag;
	public UISprite GodSprite;
	#endregion

	#region ViewWidgetsSet
	public void SetUser(int userNumber) {
		UserNumberLabel.text = "" + userNumber;
		UserColorSprite.color = UIConsts.userColors[userNumber];
	}

	public void SetGod(string godName) {
		if (godName == "") {
			GodSprite.gameObject.SetActive(false);
		} else {
			GodSprite.gameObject.SetActive(true);
			GodSprite.spriteName = UIConsts.godSprites[godName];
		}	
	}

	public void SetUserIncome(int income, int gold) {
		IncomeLabel.text = "+" + income;
		if (gold != -1)
			IncomeLabel.text += " / " + gold;
	}

	public void SetPhilosothsNumber(int phNumber) {
		PhilosothersLabel.text = "" + phNumber;
	}

	public void SetIsMetro(bool isMetro) {
		IsMetro.SetActive(isMetro);
	}

	public void SetIsCurrentUser(bool isCurrent) {
		CurrenUserFlag.SetActive(isCurrent);
	}
	#endregion
}