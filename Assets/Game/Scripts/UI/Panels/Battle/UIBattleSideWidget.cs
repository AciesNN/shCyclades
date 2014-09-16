using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBattleSideWidget : MonoBehaviour {

	public UIBattlePanel parentPanel;
	public bool isAttack;

	#region ViewWidgets
	public UILabel UserNumberLabel;
	public UISprite UserColorSprite;
	public List<GameObject> UnitsElements;
	public List<GameObject> BuildingsElements;
	#endregion

	#region ViewWidgetsSet
	public void SetUser(int userNumber) {
		UserNumberLabel.text = "" + userNumber;
		UserColorSprite.color = UIConsts.userColors[userNumber];
	}

	public void SetUnitsCount(int unitCount) {
		for (int i = 0; i < UnitsElements.Capacity; ++i)
			UnitsElements[i].SetActive(i < unitCount);
	}

	public void SetBuildingsCount(int buildingsCount) {
		for (int i = 0; i < BuildingsElements.Capacity; ++i)
			BuildingsElements[i].SetActive(i < buildingsCount);
	}
	#endregion

	#region Events
	public void OnRetreamentClick() {
		parentPanel.OnRetreatClick(isAttack);
	}

	public void OnMoveButtleOnClick() {
		parentPanel.OnMoveButtleOnClick(isAttack);
	}
	#endregion
}
