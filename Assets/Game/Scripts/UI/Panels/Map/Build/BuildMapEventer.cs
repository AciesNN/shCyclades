class BuildMapEventer: IslandClickMapEventer {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_BUILD);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleIsland(int island) {
		return island >= 0;
	}
	
	override protected void OnClickIsland(int island) {
		UIMapSlotBuildPanel p = UIGamePanel.GetPanel<UIMapSlotBuildPanel>(PanelType.MAP_TAB_SLOT_BUILD);
		p.SetActiveIsland(island);
		mapStates.Panel.SetTab(PanelType.MAP_TAB_SLOT_BUILD);
	}
	#endregion

}