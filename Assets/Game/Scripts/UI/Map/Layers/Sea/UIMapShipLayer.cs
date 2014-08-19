using UnityEngine;
using System.Collections;

public class UIMapShipLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapShipElement[MapController.XSize, MapController.YSize];
		UIMapShipElement el = CreateShip(new GridPosition(0, 0));
		el.SetOwner(3);
		el.SetCount(1);

		el = CreateShip(new GridPosition(3, 2));
		el.SetOwner(4);
		el.SetCount(2);
	}

	public UIMapShipElement CreateShip(GridPosition pos) {
		UIMapShipElement el = elements[pos.x, pos.y] as UIMapShipElement;
		if (elements[pos.x, pos.y] == null) {
			el = CreateElement<UIMapShipElement>(pos);
		}

		return el;
	}
}