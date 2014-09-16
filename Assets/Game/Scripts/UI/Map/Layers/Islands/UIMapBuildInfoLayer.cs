using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapBuildInfoLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapBuildInfoLayerElement[MapController.XSize, MapController.YSize];
		List<object> islands = Sh.In.GameContext.GetList ("/map/islands/coords");
		for(int i = 0; i < islands.Count; ++i) {
			CreateIsland(i);
		}
	}

	public void GameContext_UpdateData() {
		List<object> islands = Sh.In.GameContext.GetList ("/map/islands/coords");
		for(int i = 0; i < islands.Count; ++i) {
			UpdateIsland(i);
		}
	}

	void CreateIsland(int island) {
		int slots_count = Sh.In.GameContext.GetList("/map/islands/buildings/[{0}]", island).Count;
		List<object> coords = Sh.In.GameContext.GetList ("/map/islands/coords/[{0}]/[0]", island);
		GridPosition cell = new GridPosition(coords);

		UIMapBuildInfoLayerElement el = CreateElement<UIMapBuildInfoLayerElement>(cell);
		el.SetSlotsCount(slots_count);
	}

	void UpdateIsland(int island) {
		List<object> slots = Sh.In.GameContext.GetList("/map/islands/buildings/[{0}]", island);
		List<object> coords = Sh.In.GameContext.GetList ("/map/islands/coords/[{0}]/[0]", island);
		bool isMetro = Sh.In.GameContext.GetBool("/map/islands/is_metro/[{0}]", island);
		GridPosition cell = new GridPosition(coords);
		
		UIMapBuildInfoLayerElement el = elements[cell.x, cell.y] as UIMapBuildInfoLayerElement;
		el.SetMetro(isMetro, Library.Map_IslandMetroSize(Sh.In.GameContext, island) );
		for (int i = 0; i < slots.Count; ++i)
			el.SetBuildInSlot(i, slots[i] as string);
	}
}