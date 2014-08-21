class BuildMapEventer : MapEventer {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_BUILD_CHOOSE_PLACE);
	}
	
	override public void OnClickCell(GridPosition cell) {
		int island = GetIsland(cell);
		if (island > 0 && true) { //TODO если это остров и он наш...

			UIMapBuildPanel p = UIGamePanel.GetPanel<UIMapBuildPanel>(PanelType.MAP_TAB_BUILD);
			p.SetActiveIsland(island);
			mapStates.Panel.SetTab(PanelType.MAP_TAB_BUILD);
		}

		//parentObject.BuildOnIsland();
	}

	override public void OnHoverCell(GridPosition cell) {
		if (cell.x == 4 && cell.y == 4)
			mapStates.HighlightIsland(true);
	}

	override public void OnHoverOutCell(GridPosition cell) {
		mapStates.HighlightIsland(false);
	}

	override public void OnMapCancel() {
		mapStates.SetType(MapEventerType.DEFAULT);
	}
	#endregion

	int GetIsland(GridPosition cell) {
		return 1; //todo
	}
}