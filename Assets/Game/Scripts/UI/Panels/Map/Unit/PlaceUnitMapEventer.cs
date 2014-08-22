class PlaceUnitMapEventer : IslandClickMapEventer {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_UNIT);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleIsland(int island) {
		return island > 0;
	}
	
	override protected void OnClickIsland(int island) {
		Sh.Out.Send("place unit on island " + island);
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}
	#endregion

}