using UnityEngine;
using System.Collections;

public class UIMapUnitLayer : UIMapGridLayer {
	
	public override void CreateGridElements() {
		elements = new UIMapUnitElement[MapController.XSize, MapController.YSize];
		UIMapUnitElement el = CreateUnit(new GridPosition(4, 4));
		el.SetOwner(1);
		el.SetCount(2);
	}
	
	public UIMapUnitElement CreateUnit(GridPosition pos) {
		UIMapUnitElement el = elements[pos.x, pos.y] as UIMapUnitElement;
		if (elements[pos.x, pos.y] == null) {
			el = CreateElement<UIMapUnitElement>(pos);
		}
		
		return el;
	}

}