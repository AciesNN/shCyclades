using UnityEngine;
using System.Collections;

public class MapManager : Manager<MapManager> {

	#region Events
	public void OnCellClick(GridPosition pos) {
		Debug.Log ("map click " + pos);
	}
	
	public void OnMapCancel() {

	}
	#endregion
}
