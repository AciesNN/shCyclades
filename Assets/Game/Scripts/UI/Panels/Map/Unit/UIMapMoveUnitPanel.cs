using UnityEngine;
using System.Collections;

public class UIMapMoveUnitPanel: UIGamePanel {

	#region ViewWidgets	
	public GameObject unitsRoot;
	public UISprite[] units;
	public UILabel DesriptionLabel;
	#endregion

	[HideInInspector]public int activeUnitCount;

	#region ViewWidgetsSet
	public void SetDescription(int island) {
		DesriptionLabel.text = (island == -1 ? "Выберите свой остров для перемещения": "Выберите остров, куда хотите переместиться (подоректируйте кол-во солдат, ели нужно)");
	}

	public void SetUnitMaxCount(int maxCount) {
		for(int i = 0; i < units.Length; ++i) {
			units[i].gameObject.SetActive(i < maxCount);
		}
	}

	public void SetUnitCount(int count) {

		activeUnitCount = count;
		for(int i = 0; i < units.Length; ++i) {
			units[i].color = (i < activeUnitCount ? Color.white : Color.black);
			//units[i].alpha = (i < activeUnitCount ? 0 : 0.7f);
		}

	}

	public void SetUnitsVisible(bool visible) {
		unitsRoot.SetActive(visible);
	}
	#endregion

	#region Events
	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}


	//TODO бред с отдельными ф-циями надо прекращать
	public void OnUnitCount1Click() {
		SetUnitCount(1);
	}

	public void OnUnitCount2Click() {
		SetUnitCount(2);
	}

	public void OnUnitCount3Click() {
		SetUnitCount(3);
	}

	public void OnUnitCount4Click() {
		SetUnitCount(4);
	}

	public void OnUnitCount5Click() {
		SetUnitCount(5);
	}

	public void OnUnitCount6Click() {
		SetUnitCount(6);
	}

	public void OnUnitCount7Click() {
		SetUnitCount(7);
	}

	public void OnUnitCount8Click() {
		SetUnitCount(8);
	}
	#endregion
}

