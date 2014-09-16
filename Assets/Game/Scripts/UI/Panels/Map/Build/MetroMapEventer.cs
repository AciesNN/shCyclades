using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class MetroMapEventer: IslandClickMapEventer {

	bool Metro4Buildings;

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_METRO);

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach (long island in islands) {
				//if (Sh.In.GameContext.GetLong("/map/islands/owners/[{0}]", island) == Sh.GameState.currentUser)
					allowedIslands.Add(island);
			}
		}

		Metro4Buildings = (Sh.In.GameContext.GetStr("/cur_state") == "Turn.PlaceMetroBuilding");

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		if (Metro4Buildings)
			Sh.Out.Send( Messanges.PlaceMetro4Buildings(island, new List<object>())); //TODO слоты уничтожаемых зданий 
		else 
			Sh.Out.Send( Messanges.PlaceMetro4Philosopher(island) );
		mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}