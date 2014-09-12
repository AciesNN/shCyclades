using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class MoveShipEventer: SeaClickMapEventer {

	UIMapMoveShipPanel panel;
	bool isFirstCall;
	GridPosition firstCell;
	int lastCount;

	GridPosition _lastSeaCell {
		get {	
			GridPosition cell = new GridPosition( Sh.In.GameContext.GetList("/fight/navy/last_coords") );
			if (cell.IsLessThanZero()) {
				return firstCell;
			} else {
				return cell;
			}
		}
	}

	#region Events
	override public void Activate() {
		base.Activate();
		isFirstCall = true;
		lastCount = 1;
		firstCell = GridPosition.LessThanZero();
		ReInit();
	}

	override public void ReActivate() {
		ReInit ();
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		if (isFirstCall) {
			isFirstCall = false;
			firstCell = cell;
			ReInit();
		} else {
			GridPosition lastSeaCell = _lastSeaCell;
			Sh.Out.Send(Messanges.MoveNavy(lastSeaCell.x, lastSeaCell.y, cell.x, cell.y, panel.activeUnitCount));
			lastCount = panel.activeUnitCount;
		}
	}
	#endregion

	void ReInit() {

		GridPosition lastSeaCell = _lastSeaCell;
		panel = UIMapMoveShipPanel.GetPanel<UIMapMoveShipPanel>(PanelType.MAP_TAB_MOVE_SHIP);

		if (!lastSeaCell.IsLessThanZero()) {
			panel.ReInit();
			int max_units_count = Library.Map_GetShipCountByPoint(Sh.In.GameContext, lastSeaCell.x, lastSeaCell.y);
			panel.SetUnitMaxCount(max_units_count);
			
			for (int i = 0; i < max_units_count; ++i)
				panel.SetUnitActive(i, i < lastCount);
		}

		panel.CountOfMovement = Sh.In.GameContext.GetInt("/fight/navy/move");
		panel.SetDescription(lastSeaCell);
		panel.SetUnitsVisible(!isFirstCall);

		mapStates.Panel.SetTab(PanelType.MAP_TAB_MOVE_SHIP);

		HighlightSeaCells(false);
		if (lastSeaCell.IsLessThanZero()) {
			CalculateAllowedCellsFrom(); 
		} else {
			CalculateAllowedCellsTo();
		}
		HighlightSeaCells(true);

		panel.SetInfoPosition(lastSeaCell);

	}

	void CalculateAllowedCellsFrom() {
		List<object> owners = Sh.In.GameContext.GetList ("/map/seas/ships/owners");

		for(int i = 0; i < owners.Count; ++i) {
			if ((long)owners[i] == Sh.GameState.currentUser) {
				GridPosition cell = new GridPosition( Sh.In.GameContext.GetList ("/map/seas/ships/coords/[{0}]", i) );
				if(!allowedCells.Contains(cell))
					allowedCells.Add(cell);
			}
		}
	}
	
	void CalculateAllowedCellsTo() {
		GridPosition lastSeaCell = _lastSeaCell;

		allowedCells = new List<GridPosition>();

		List<Shmipl.FrmWrk.Library.Coords> seaCoords = Library.Map_GetPointNeighbors(Sh.In.GameContext, lastSeaCell.x, lastSeaCell.y);
		foreach(Shmipl.FrmWrk.Library.Coords seaCoord in seaCoords) {
			if (Library.Map_IsPointAccessibleForShip(Sh.In.GameContext, seaCoord.x, seaCoord.y, Sh.GameState.currentUser)) {
				allowedCells.Add(new GridPosition(seaCoord.x, seaCoord.y));
			}
		}

	}
}
