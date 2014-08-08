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
			UnitsElements[i].SetActive(i+1 < unitCount);
	}

	public void SetBuildingsCount(int buildingsCount) {
		for (int i = 0; i < BuildingsElements.Capacity; ++i)
			BuildingsElements[i].SetActive(i+1 < buildingsCount);
	}
	#endregion

	#region Events
	public void OnRetreamentClick() {
		Sh.Out.Send("Retreate in battle");
	}

	public void OnMoveButtleOnClick() {
		Sh.Out.Send("Move on battle");
	}
	#endregion
}
