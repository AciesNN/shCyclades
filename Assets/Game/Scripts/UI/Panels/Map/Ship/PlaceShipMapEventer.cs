class PlaceShipMapEventer: SeaClickMapEventer {

	#region Events
	override public void Activate() {
		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_SHIP);
	}
	#endregion

	#region Abstract
	override protected bool IsPossibleSeaCell(GridPosition cell) {
		return cell.x == 3 && cell.y == 3;
	}
	
	override protected void OnClickSeaCell(GridPosition cell) {
		Sh.Out.Send("place ship on cell " + cell);
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
		HighlightSeaCell(cell, false);
	}
	#endregion

}
