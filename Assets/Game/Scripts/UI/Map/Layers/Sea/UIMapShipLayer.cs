using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapShipLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapShipElement[MapController.XSize, MapController.YSize];
	}

	public void GameContext_UpdateData() {
		for(int x = 0; x < MapController.XSize; ++x) {
			for(int y = 0; y < MapController.YSize; ++y) {
				GridPosition cell = new GridPosition(x, y);
				if(MapController.IsCellPossible(cell) && Library.Map_GetIslandByPoint(Sh.In.GameContext, x, y) == -1) {
					CreateShip(cell);
				}
			}
		}
	}

	public UIMapShipElement CreateShip(GridPosition cell) {
		UIMapShipElement el = elements[cell.x, cell.y] as UIMapShipElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapShipElement>(cell);
		}

		int owner = (int)Library.Map_GetPointOwner(Sh.In.GameContext, cell.x, cell.y);
		int count = Library.Map_GetShipCountByPoint(Sh.In.GameContext, cell.x, cell.y);

		el.SetCount(count);
		el.SetOwner(owner);

		return el;
	}
}