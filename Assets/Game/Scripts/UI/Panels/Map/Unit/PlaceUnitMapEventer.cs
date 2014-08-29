using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class PlaceUnitMapEventer : IslandClickMapEventer {

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_UNIT);
		
		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			allowedIslands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
		}
		
		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send("place unit on island " + island);
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}
	#endregion

}