using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class MetroMapEventer: IslandClickMapEventer {

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_METRO);

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach (long island in islands) {
				if (Sh.In.GameContext.GetLong("/map/islands/owners/[{0}]", island) == Sh.GameState.currentUser)
					allowedIslands.Add(island);
			}
		}

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send("build Metro on " + island); //TODO слоты уничтожаемых зданий
		mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}