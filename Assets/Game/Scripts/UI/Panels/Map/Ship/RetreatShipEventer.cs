using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class RetreatShipEventer: SeaClickMapEventer {
	
	public GridPosition lastSeaCell;
	public UIBattlePanel battlePanel;

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_RETREAT_SHIP);

		CalculateAllowedCellsTo();
		HighlightSeaCells(true);

		battlePanel.SetShowContextVisible(false);
	}

	public override void Deactivate() {
		base.Deactivate();
		battlePanel.SetShowContextVisible(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		Sh.Out.Send("retreat ships to cell" + cell);
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion
	
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
