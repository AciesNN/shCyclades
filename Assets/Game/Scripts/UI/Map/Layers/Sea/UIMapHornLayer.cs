using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapHornLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapGridLayerElement[MapController.XSize, MapController.YSize];
		List<object> horns = Sh.In.GameContext.GetList ("/map/seas/horns");
		foreach(List<object> coord in horns) {
			CreateHorn(new GridPosition((long)coord[0], (long)coord[1]));
		}
	}

	public UIMapGridLayerElement CreateHorn(GridPosition cell) {
		UIMapGridLayerElement el = elements[cell.x, cell.y] as UIMapGridLayerElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapGridLayerElement>(cell);
		}		
		return el;
	}
}