using UnityEngine;
using System.Collections;

public class UIMapMoveShipPanel: UIGamePanel {

	#region ViewWidgets	
	public GameObject unitsRoot;
	public UISprite[] units;
	public bool[] isActiveUnit;
	public UILabel DesriptionLabel;
	#endregion

	[HideInInspector]public int activeUnitCount;

	#region ViewWidgetsSet
	public void ReInit() {
		activeUnitCount = 0;
		isActiveUnit = new bool[units.Length]; //по умолчанию же все false?
	}

	public void SetDescription(GridPosition cell) {
		DesriptionLabel.text = (cell.x == -1 && cell.y == -1 ? "Выберите свой корабль для перемещения": "Выберите соседнюю клетку моря, куда хотите переместиться (подоректируйте кол-во кораблей, ели нужно)");
	}

	public void SetUnitMaxCount(int maxCount) {
		for(int i = 0; i < units.Length; ++i) {
			units[i].gameObject.SetActive(i < maxCount);
		}
	}

	public void SetUnitActive(int number, bool active) {

		if (isActiveUnit[number] != active)
			activeUnitCount += (active ? +1 : -1);
		isActiveUnit[number] = active;
		units[number].color = (active ? Color.white : Color.black);

	}

	void SetUnitActive(int number) {
		int i = number -1;
		SetUnitActive(i, !isActiveUnit[i]);
	}

	public void SetUnitActiveCount(int count) {
		for(int i = 0; i < units.Length; ++i) {
			SetUnitActive(i, i<count);
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
		SetUnitActive(1);
	}

	public void OnUnitCount2Click() {
		SetUnitActive(2);
	}

	public void OnUnitCount3Click() {
		SetUnitActive(3);
	}

	public void OnUnitCount4Click() {
		SetUnitActive(4);
	}

	public void OnUnitCount5Click() {
		SetUnitActive(5);
	}

	public void OnUnitCount6Click() {
		SetUnitActive(6);
	}

	public void OnUnitCount7Click() {
		SetUnitActive(7);
	}

	public void OnUnitCount8Click() {
		SetUnitActive(8);
	}
	#endregion
}

