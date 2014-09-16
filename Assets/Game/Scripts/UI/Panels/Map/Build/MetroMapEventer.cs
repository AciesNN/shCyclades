using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
				if (!Sh.In.GameContext.GetBool("/map/islands/is_metro/[{0}]", island))
					allowedIslands.Add(island);
			}
		}

		Metro4Buildings = (Sh.In.GameContext.GetStr("/cur_state") == "Turn.PlaceMetroBuilding");

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		if (Metro4Buildings) {
			List<object> slots_and_islands = GetSlotsForMetro();
			Sh.Out.Send( Messanges.PlaceMetro4Buildings(island, slots_and_islands)); 
		}
		else 
			Sh.Out.Send( Messanges.PlaceMetro4Philosopher(island) );
		mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

	List<object> GetSlotsForMetro() {
		//TODO тут конечно нужно как-то дать выбор

		List<object> slots_and_islands = new List<object>();
		List<string> buildings = new List<string>();

		List<object> owners = Sh.In.GameContext.GetList ("/map/islands/owners");
		for(int island = 0; island < buildings.Count; ++island) {
			if ((int)(long)owners[island] == Sh.GameState.currentUser) {
				List<object> slots = Sh.In.GameContext.GetList ("/map/islands/buildings/[{0}]", island);
				for(int s = 0; s < slots.Count; ++s) {
					string slot = slots[s] as string;
					if (slot != Cyclades.Game.Constants.buildNone && buildings.IndexOf(slot) >= 0) {
						buildings.Add (slot);
						slots_and_islands.Add ( new List<object> {island, s} );
					}
				}
			}
		}		

		return slots_and_islands;
	}
}