using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapStates : MonoBehaviour {

	public UIGamePanelTabs Panel;
	
	UIMapController MapController;
	Dictionary<MapEventerType, MapEventer> mapEventers = new Dictionary<MapEventerType, MapEventer>();
	MapEventerType type = MapEventerType.DEFAULT;
	MapEventer mapEventer {
		get { return mapEventers[type]; }
	}

	void Awake() {
		MapController = GetComponent<UIMapController>();
		RegisterEventers();
	}

	void RegisterEventers() {
		MapEventer[] es = GetComponents<MapEventer>();
		foreach(MapEventer e in es) {
			mapEventers[e.type] = e;
		}
	}

	public void SetType(MapEventerType type) {

		if (this.type == type)
			return;
		this.type = type;
		mapEventer.Activate();
		Sh.GameState.UpdateMapEventorType(type);

	}
	
	#region ViewWidgetsSet
	public void BuildOnIsland() {
		Sh.Out.Send("build");
		//SetEventerType (MapEventerType.DEFAULT); 
	}
	
	public void HighlightIsland(bool active) {
		UIMapIslandsLayer l = MapController.GetLayer<UIMapIslandsLayer>(GridLayerType.ISLANDS);
		l.HiglightIsland(l.debugPoints, active);
	}
	#endregion
	
	#region Events
	GridPosition oldCell = new GridPosition(-1, -1); //TODO некрасиво

	public void OnClickCell(GridPosition cell) {
		NGUIDebug.Log("press cell: " + cell);
		mapEventer.OnClickCell(cell);
	}
	
	public void OnHoverCell(GridPosition cell) {
		if (oldCell != cell) {
			OnHoverOutCell(oldCell);
			oldCell = new GridPosition(cell.x, cell.y);
		}
		mapEventer.OnHoverCell(cell);
	}
	
	public void OnHoverOutCell(GridPosition cell) {
		mapEventer.OnHoverOutCell(cell);
	}
	
	public void OnMapCancel() {
		mapEventer.OnMapCancel();
	}
	#endregion
}
