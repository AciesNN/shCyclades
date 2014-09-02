using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIMapMoveShipPanel: UIGamePanel {

	#region ViewWidgets	
	public UIMapController mapController; //TODO возможно стоит вынести в одиночку
	public GameObject MapLayerObjects;

	public GameObject unitsRoot;
	public UISprite[] units;
	public bool[] isActiveUnit;
	public UILabel DesriptionLabel;

	public UISprite cancelButton;
	public UILabel okButton;
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

	private int countOfMovement;
	public int CountOfMovement {
		get { return countOfMovement; }
		set {
			countOfMovement = value;
			if (countOfMovement == 3) {
				cancelButton.enabled = true;
				okButton.enabled = false;
			} else {
				cancelButton.enabled = false;
				okButton.enabled = true;
				okButton.text = "" + countOfMovement;
			}
		}
	}

	public void SetInfoPosition(GridPosition cell) {
		MapLayerObjects.transform.localPosition = mapController.CellToWorldPosition(cell);
	}
	#endregion

	public override void Show (){
		MapLayerObjects.SetActive(true);
		base.Show();
	}  
	
	public override void Hide() {
		MapLayerObjects.SetActive(false);
		base.Hide ();
	}

	#region Events
	public void OnCancelButtonClick() {
		if (countOfMovement < 3)
			Sh.Out.Send (Messanges.CancelMoveNavy());
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
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

