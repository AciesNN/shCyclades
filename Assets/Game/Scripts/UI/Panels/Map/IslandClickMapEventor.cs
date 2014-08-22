using UnityEngine;
using System.Collections;

abstract class IslandClickMapEventor : MapEventer {
	#region Abstract
	abstract protected bool IsPossibleIsland(int island);

	abstract protected void OnClickIsland(int island);
	#endregion

	#region Events
	override public void Deactivate() {
		HighlightIsland(-1, false);
	}

	override public void OnClickCell(GridPosition cell) {
		int island = GetIsland(cell);
		if (IsPossibleIsland(island))
			OnClickIsland(island);
	}
	
	override public void OnHoverCell(GridPosition cell) {
		int island = GetIsland(cell);
		if (IsPossibleIsland(island))
			HighlightIsland(island, true);
	}
	
	override public void OnHoverOutCell(GridPosition cell) {
		HighlightIsland(-1, false);
	}
	
	override public void OnMapCancel() {
		mapStates.SetType(MapEventerType.DEFAULT); //? возможно такая реакция по-умолчанию вредна?
	}
	#endregion

	void HighlightIsland(int island, bool active) {
		UIMapIslandsLayer l = mapStates.MapController.GetLayer<UIMapIslandsLayer>(GridLayerType.ISLANDS);
		l.HiglightIsland(l.debugPoints, active);
	}

	int GetIsland(GridPosition cell) {
		return (cell.x == 4 && cell.y == 4 ? 1 : -1); //TODO
	}
}
