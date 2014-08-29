using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class PlaceShipMapEventer: SeaClickMapEventer {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_SHIP);

		allowedCells = new List<GridPosition>();
		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach(long island in islands) {
				//Library.Map_GetIslandCoords(
			}
		}
		
		HighlightSeaCells(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		Sh.Out.Send("place ship on cell " + cell);
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}
	#endregion

}
