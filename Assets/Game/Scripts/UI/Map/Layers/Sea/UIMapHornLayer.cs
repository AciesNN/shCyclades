using UnityEngine;
using System.Collections;

public class UIMapHornLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapGridLayerElement[MapController.XSize, MapController.YSize];
		CreateElement<UIMapGridLayerElement>(new GridPosition(0, 0));
		CreateElement<UIMapGridLayerElement>(new GridPosition(5, 0));
	}

}