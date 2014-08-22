using UnityEngine;
using System.Collections;

abstract class SeaClickMapEventer : MapEventer {
	#region Abstract
	abstract protected bool IsPossibleSeaCell(GridPosition cell);
	
	abstract protected void OnClickSeaCell(GridPosition cell);
	#endregion
	
	#region Events
	override public void Deactivate() {
		//TODO тоже надо не забыть "разметить ячейку"
	}
	
	override public void OnClickCell(GridPosition cell) {
		if (IsPossibleSeaCell(cell))
			OnClickSeaCell(cell);
	}
	
	override public void OnHoverCell(GridPosition cell) {
		if (IsPossibleSeaCell(cell))
			HighlightSeaCell(cell, true);
	}
	
	override public void OnHoverOutCell(GridPosition cell) {
		HighlightSeaCell(cell, false);
	}
	
	override public void OnMapCancel() {
		mapStates.SetType(MapEventerType.DEFAULT); //? возможно такая реакция по-умолчанию вредна?
	}
	#endregion
	
	protected void HighlightSeaCell(GridPosition cell, bool active) {
		UIMapGridCellsLayer l = mapStates.MapController.GetLayer<UIMapGridCellsLayer>(GridLayerType.GRID);
		l.HiglightCell(cell, active);
	}
}
