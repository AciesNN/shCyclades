class MoveUnitEventor : IslandClickMapEventor {
	
	int fromIsland;
	UIMapMoveUnitPanel panel;

	#region Events
	override public void Activate() {
		fromIsland = -1;

		panel = UIGamePanel.GetPanel<UIMapMoveUnitPanel>(PanelType.MAP_TAB_MOVE_UNIT);
		panel.SetDescription(fromIsland);

		mapStates.Panel.SetTab(PanelType.MAP_TAB_MOVE_UNIT);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleIsland(int island) {
		return island > 0;
	}
	
	override protected void OnClickIsland(int island) {
		if (fromIsland == -1) {
			fromIsland = island;
			panel.SetDescription(fromIsland);
		} else {
			Sh.Out.Send("move unit from island " + fromIsland + " to " + island);
			Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
		}
	}
	#endregion

}
