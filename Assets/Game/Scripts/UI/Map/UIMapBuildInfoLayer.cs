using UnityEngine;
using System.Collections;

public class UIMapBuildInfoLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapBuildInfoLayerElement[MapController.XSize, MapController.YSize];
		CreateIsland(new GridPosition(4, 4), 3) ;
	}

	void CreateIsland(GridPosition cell, int slots_count) {
		UIMapBuildInfoLayerElement el = CreateElement<UIMapBuildInfoLayerElement>(cell);
		el.SetMetro(false);
		el.SetSlotsCount(slots_count);
		for (int i = 0; i < slots_count; ++i)
			el.SetBuildInSlot(i, Cyclades.Game.Constants.buildNone);
	}
}