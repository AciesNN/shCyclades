using UnityEngine;
using System.Collections;

public class UIMapIslandsLayer: UIMapGridLayer {

	public override void CreateGridElements() {

		elements = new UIMapIslandElement[MapController.XSize, MapController.YSize];

		CreateElement<UIMapIslandElement>(new GridPosition(4, 4));

	}

}
