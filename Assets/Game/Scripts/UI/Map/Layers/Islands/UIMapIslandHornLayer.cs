using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapIslandHornLayer : UIMapGridLayer {
	
	public override void CreateGridElements() {
		elements = new UIMapIslandHornElement[MapController.XSize, MapController.YSize];
	}

	public void GameContext_UpdateData() {
		List<object> horns = Sh.In.GameContext.GetList ("/map/islands/horn");
		for(int i = 0; i < horns.Count; ++i) {
			int count = (int)(long)horns[i];
			if(count > 0) 
				CreateHorn(i, count);
		}
	}

	public UIMapIslandHornElement CreateHorn(int island, int count) {
		List<object> coords = Sh.In.GameContext.GetList ("/map/islands/coords/[{0}]/[0]", island);
		GridPosition cell = new GridPosition((int)(long)coords[0], (int)(long)coords[1]);

		UIMapIslandHornElement el = elements[cell.x, cell.y] as UIMapIslandHornElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapIslandHornElement>(cell);
		}

		el.SetCount(count);
		return el;
	}

}