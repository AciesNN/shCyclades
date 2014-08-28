using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapUnitLayer : UIMapGridLayer {
	
	public override void CreateGridElements() {
		elements = new UIMapUnitElement[MapController.XSize, MapController.YSize];
	}

	public void GameContext_UpdateData() {
		List<object> islands = Sh.In.GameContext.GetList ("/map/islands/coords");
		for(int i = 0; i < islands.Count; ++i) {
			CreateUnit(i);
		}
	}

	public UIMapUnitElement CreateUnit(int island) {
		List<object> coords = Sh.In.GameContext.GetList ("/map/islands/coords/[{0}]/[0]", island);
		GridPosition cell = new GridPosition((int)(long)coords[0], (int)(long)coords[1]);
		int count = Sh.In.GameContext.GetInt ("/map/islands/army/[{0}]", island);
		int owner = Sh.In.GameContext.GetInt("/map/islands/owners/[{0}]", island);

		UIMapUnitElement el = elements[cell.x, cell.y] as UIMapUnitElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapUnitElement>(cell);
		}

		el.SetOwner(owner);
		el.SetCount(count);
		return el;
	}

}