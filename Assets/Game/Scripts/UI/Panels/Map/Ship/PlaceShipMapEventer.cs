using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class PlaceShipMapEventer: SeaClickMapEventer {

	#region Events
	override public void Activate() {
		base.Activate();
		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_SHIP);
		
		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach(long island in islands) {

				List<Shmipl.FrmWrk.Library.Coords> seaCoords = Library.Map_GetSeasNearIsland(Sh.In.GameContext, island);

				foreach(Shmipl.FrmWrk.Library.Coords seaCoord in seaCoords) {
					if (Library.Map_IsPointAccessibleForShip(Sh.In.GameContext, seaCoord.x, seaCoord.y, Sh.GameState.currentUser, false)) {
						GridPosition cell = new GridPosition(seaCoord.x, seaCoord.y);
						if (!allowedCells.Contains(cell))
						    allowedCells.Add(cell);
					}
				}

			}
		}
		
		HighlightSeaCells(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		Sh.Out.Send(Messanges.BuyNavy(cell.x, cell.y));
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}
