using UnityEngine;
using System.Collections;

public class UIMapGridCellsLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapGridLayerElement[MapController.XSize, MapController.YSize];

		for (int x = 0; x < MapController.XSize; ++x) {
			for (int y = 0; y < MapController.YSize; ++y) {
				GridPosition pos = new GridPosition(x, y);
				if (MapController.IsCellPossible(pos))
					CreateElement<UIMapGridLayerElement>(pos);
			}
		}
	}

}
