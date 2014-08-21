﻿using UnityEngine;

class MapEventer : MonoBehaviour {

	protected UIMapStates mapStates;
	public MapEventerType type;

	void Awake() {
		mapStates = GetComponent<UIMapStates>();
	}

	#region Events
	virtual public void Activate() {
	}

	virtual public void OnClickCell(GridPosition cell) {
	}

	virtual public void OnHoverCell(GridPosition cell) {
	}

	virtual public void OnHoverOutCell(GridPosition cell) {
	}

	virtual public void OnMapCancel() {
	}
	#endregion
}

public enum MapEventerType {
	DEFAULT,
	BUILD,
	PLACEUNIT,
	MOVEUNIT
}