class BuildMapEventer : MapEventer {

	int activeIsland;

	public BuildMapEventer(UIMapPanel parentObject)
		: base(parentObject) {
	}

	#region Events
	override public void OnClickCell(GridPosition cell) {
		int island = GetIsland(cell);
		if (island > 0 && true) { //TODO если это остров и он наш...
			activeIsland = island;
			parentObject.tabs.SetTab(PanelType.MAP_TAB_BUILD);
		}

		//parentObject.BuildOnIsland();
	}

	override public void OnHoverCell(GridPosition cell) {
		if (cell.x == 4 && cell.y == 4)
			parentObject.HighlightIsland(true);
	}

	override public void OnHoverOutCell(GridPosition cell) {
		parentObject.HighlightIsland(false);
	}

	override public void OnMapCancel() {
		parentObject.SetEventerType(MapEventerType.DEFAULT);
	}
	#endregion

	int GetIsland(GridPosition cell) {
		return 1; //todo
	}
}