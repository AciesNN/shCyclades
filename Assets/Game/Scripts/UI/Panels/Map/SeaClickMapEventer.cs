﻿using System.Collections;
using System.Collections.Generic;

abstract class SeaClickMapEventer : MapEventer {

	protected List<GridPosition> allowedCells;

	#region Abstract
	abstract protected void OnClickSeaCell(GridPosition cell);
	#endregion
	
	#region Events
	override public void Activate() {
		allowedCells = new List<GridPosition>();
	}

	override public void Deactivate() {
		DeHighlightAllSeaCells();
	}
	
	override public void OnClickCell(GridPosition cell) {
		if (IsPossibleSeaCell(cell))
			OnClickSeaCell(cell);
	}
	
	override public void OnHoverCell(GridPosition cell) {
		/*if (IsPossibleSeaCell(cell))
			HighlightSeaCell(cell, true);*/
	}
	
	override public void OnHoverOutCell(GridPosition cell) {
		/*HighlightSeaCell(cell, false);*/
	}
	
	override public void OnMapCancel() {
		mapStates.SetEventorType(MapEventerType.DEFAULT); //todo возможно такая реакция по-умолчанию вредна?
	}
	#endregion
	
	protected void HighlightSeaCells(bool active) {
		foreach (GridPosition cell in allowedCells) 
			HighlightSeaCell(cell, active);
	}
	
	protected void DeHighlightAllSeaCells() {
		HighlightSeaCell(new GridPosition(-1, -1), false);
	}

	protected void HighlightSeaCell(GridPosition cell, bool active) {
		UIMapGridCellsLayer l = mapStates.MapController.GetLayer<UIMapGridCellsLayer>(GridLayerType.GRID);
		l.HiglightCell(cell, active);
	}

	protected bool IsPossibleSeaCell(GridPosition cell) {
		return allowedCells.Contains(cell);
	}
}
