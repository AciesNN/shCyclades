using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapIslandsLayer: UIMapGridLayer {

	Dictionary<int, GridPosition[]> islandsCells = new Dictionary<int, GridPosition[]>();

	public override void CreateGridElements() {

		elements = new UIMapIslandElement[MapController.XSize, MapController.YSize];
		List<object> islands = Sh.In.GameContext.GetList ("/map/islands/coords");
		for(int i = 0; i < islands.Count; ++i) {
			List<List<int>> coords = Library.Map_GetIslandCoords(Sh.In.GameContext, i);
			GridPosition[] cells = new GridPosition[coords.Count];
			for(int t = 0; t < coords.Count; ++t) {
				cells[t] = new GridPosition(coords[t][0], coords[t][1]);
			}
			islandsCells[i] = cells;
			CreateIsland(i);
		}

	}

	void CreateIsland(int island) {

		GridPosition[] points = islandsCells[island];
		foreach(GridPosition pos in points) {
			UIMapIslandElement islandElement = CreateElement<UIMapIslandElement>(pos);
			//тут можно что-нибудь сделать с конкретным элементом, типа настроить картинку
		}

	}

	public void GameContext_UpdateData(bool deserialize) {

		List<object> islands = Sh.In.GameContext.GetList ("/map/islands/coords");
		for(int i = 0; i < islands.Count; ++i) {

			GridPosition[] points = islandsCells[i];
			int owner = Sh.In.GameContext.GetInt("/map/islands/owners/[{0}]", i);

			foreach(GridPosition pos in points) {
				UIMapIslandElement islandElement = elements[pos.x, pos.y] as UIMapIslandElement;
				islandElement.SetOwner(owner);
			}
		}

	}

	public void HiglightIsland(int island, bool isHighlight) {
		if (island != -1 && !islandsCells.ContainsKey(island)) 
			return;
		if (island == -1) {//TODO спорный момент - не очень красиво передавать -1, если надо сбросить выделение со всех
			foreach(int i in islandsCells.Keys)
				HiglightIsland(i, isHighlight);
		} else {
			GridPosition[] points = islandsCells[island];
			foreach(GridPosition pos in points) {
				GetElement<UIMapIslandElement>(pos).SetHighlight(isHighlight);
			}
		}
	}
}
