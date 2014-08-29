using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class MoveShipEventer: SeaClickMapEventer {
	
	GridPosition lastSeaCell;
	UIMapMoveShipPanel panel;

	#region Events
	override public void Activate() {
		base.Activate();

		lastSeaCell = new GridPosition(-1, -1);

		panel = UIMapMoveShipPanel.GetPanel<UIMapMoveShipPanel>(PanelType.MAP_TAB_MOVE_SHIP);
		panel.CountOfMovement = 3;
		panel.ReInit();
		panel.SetDescription(lastSeaCell);
		panel.SetUnitsVisible(false);

		mapStates.Panel.SetTab(PanelType.MAP_TAB_MOVE_SHIP);
		
		CalculateAllowedCellsFrom();
		HighlightSeaCells(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		panel.SetInfoPosition(cell);
		if (lastSeaCell.x == -1 && lastSeaCell.y == -1) {
			lastSeaCell = cell;
			panel.SetDescription(lastSeaCell);
			panel.SetUnitsVisible(true);
			panel.SetUnitMaxCount(3);
			panel.SetUnitActiveCount(1);

			HighlightSeaCells(false);
			CalculateAllowedCellsTo();
			HighlightSeaCells(true);
		} else {
			Sh.Out.Send("move unit from island (" + lastSeaCell + ") -> (" + cell + ")            " + panel.activeUnitCount + " units");
			lastSeaCell = cell;
			--panel.CountOfMovement;
			if (panel.CountOfMovement == 0) {
				Sh.Out.Send ("ships movements end");
				Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
			} else {
				HighlightSeaCells(false);
				CalculateAllowedCellsTo();
				HighlightSeaCells(true);
			}
		}
	}
	#endregion

	void CalculateAllowedCellsFrom() {
		List<object> owners = Sh.In.GameContext.GetList ("/map/seas/ships/owners");

		for(int i = 0; i < owners.Count; ++i) {
			if ((long)owners[i] == Sh.GameState.currentUser) {
				List<object> coord = Sh.In.GameContext.GetList ("/map/seas/ships/coords/[{0}]", i);
				GridPosition cell = new GridPosition((long)coord[0], (long)coord[1]);
				if(!allowedCells.Contains(cell))
					allowedCells.Add(cell);
			}
		}
	}
	
	void CalculateAllowedCellsTo() {
		allowedCells = new List<GridPosition>();
		List<Shmipl.FrmWrk.Library.Coords> seaCoords = Library.Map_GetPointNeighbors(Sh.In.GameContext, lastSeaCell.x, lastSeaCell.y);
		foreach(Shmipl.FrmWrk.Library.Coords seaCoord in seaCoords) {
			if (Library.Map_IsPointAccessibleForShip(Sh.In.GameContext, seaCoord.x, seaCoord.y, Sh.GameState.currentUser)) {
				allowedCells.Add(new GridPosition(seaCoord.x, seaCoord.y));
			}
		}

	}
}
