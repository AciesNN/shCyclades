class MoveShipEventer: SeaClickMapEventer {
	
	GridPosition lastSeaCell;
	UIMapMoveShipPanel panel;

	#region Events
	override public void Activate() {
		lastSeaCell = new GridPosition(-1, -1);

		panel = UIMapMoveShipPanel.GetPanel<UIMapMoveShipPanel>(PanelType.MAP_TAB_MOVE_SHIP);
		panel.ReInit();
		panel.SetDescription(lastSeaCell);
		panel.SetUnitsVisible(false);

		mapStates.Panel.SetTab(PanelType.MAP_TAB_MOVE_SHIP);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleSeaCell(GridPosition cell) {
		return cell.x == 3 && cell.y == 3;
	}
	
	override protected void OnClickSeaCell(GridPosition cell) {
		panel.SetInfoPosition(cell);
		if (lastSeaCell.x == -1 && lastSeaCell.y == -1) {
			lastSeaCell = cell;
			panel.SetDescription(lastSeaCell);
			panel.SetUnitsVisible(true);
			panel.SetUnitMaxCount(3);
			panel.SetUnitActiveCount(1);
			panel.CountOfMovement = 3;
		} else {
			Sh.Out.Send("move unit from island (" + lastSeaCell + ") -> (" + cell + ")            " + panel.activeUnitCount + " units");
			lastSeaCell = cell;
			--panel.CountOfMovement;
			if (panel.CountOfMovement == 0) {
				Sh.Out.Send ("ships movements end");
				Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
			}
		}
	}
	#endregion
}
