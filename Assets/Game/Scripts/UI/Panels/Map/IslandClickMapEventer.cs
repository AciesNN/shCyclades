using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

abstract class IslandClickMapEventer : MapEventer {

	protected List<long> allowedIslands;
	protected UIMapIslandsLayer l;
	#region Abstract
	abstract protected void OnClickIsland(int island);
	#endregion

	#region Events
	override public void Activate() {
		allowedIslands = new List<long>();
		l = mapStates.MapController.GetLayer<UIMapIslandsLayer>(GridLayerType.ISLANDS);
	}

	override public void Deactivate() {
		DeHighlightAllIslands();
	}

	override public void OnClickCell(GridPosition cell) {
		int island = GetIsland(cell);
		if (IsPossibleIsland(island))
			OnClickIsland(island);
	}
	
	override public void OnHoverCell(GridPosition cell) {
		int island = GetIsland(cell);
		OnHoverIsland(island, true);
	}
	
	override public void OnHoverOutCell(GridPosition cell) {
		int island = GetIsland(cell);
		OnHoverIsland(island, false);
	}
	
	override public void OnMapCancel() {
		mapStates.SetEventorType(MapEventerType.DEFAULT); //todo возможно такая реакция по-умолчанию вредна?
	}
	#endregion

	protected void HighlightIslands(bool active) {
		foreach (long island in allowedIslands) 
			HighlightIsland((int)island, active);
	}

	protected void DeHighlightAllIslands() {
		HighlightIsland(-1, false);
	}

	void HighlightIsland(int island, bool active) {
		l.HiglightIsland(island, active);
	}

	protected void OnHoverIsland(int island, bool onHover) {
		l.OnHoverIsland(island, onHover);
	}

	int GetIsland(GridPosition cell) {
		return Library.Map_GetIslandByPoint(Sh.In.GameContext, (long)cell.x, (long)cell.y);
	}

	protected bool IsPossibleIsland(int island) {
		return allowedIslands.Contains((long)island);
	}
}
