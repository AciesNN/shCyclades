class BuildMapEventer : IslandClickMapEventor {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_BUILD_CHOOSE_PLACE);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleIsland(int island) {
		return island > 0;
	}
	
	override protected void OnClickIsland(int island) {
		UIMapBuildPanel p = UIGamePanel.GetPanel<UIMapBuildPanel>(PanelType.MAP_TAB_BUILD);
		p.SetActiveIsland(island);
		mapStates.Panel.SetTab(PanelType.MAP_TAB_BUILD);
	}
	#endregion

}