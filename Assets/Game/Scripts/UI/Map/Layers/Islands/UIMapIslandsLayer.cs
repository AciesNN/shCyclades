using UnityEngine;
using System.Collections;

public class UIMapIslandsLayer: UIMapGridLayer {

	public GridPosition[] debugPoints = new GridPosition [] { 
		new GridPosition(4, 4),
		new GridPosition(4, 5),
		new GridPosition(5, 5)
	};
	
	public override void CreateGridElements() {

		elements = new UIMapIslandElement[MapController.XSize, MapController.YSize];
		CreateIsland(debugPoints, 1);
	}

	void CreateIsland(GridPosition[] points, int owner) {
		foreach(GridPosition pos in points) {
			UIMapIslandElement islandElement = CreateElement<UIMapIslandElement>(pos);
			islandElement.SetOwner(owner);
			//тут можно что-нибудь сделать с конкретным элементом, типа настроить картинку
		}
	}

	public void HiglightIsland(GridPosition[] points, bool isHighlight) {
		foreach(GridPosition pos in points) {
			GetElement<UIMapIslandElement>(pos).SetHighlight(isHighlight);
		}
	}
}
