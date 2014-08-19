using UnityEngine;
using System.Collections;

public class UIMapPanel : UIGamePanel {
		
	#region VewWidgets

	#endregion

	public UIMapController MapController;
	public UIGamePanelTabs tabs;

	MapEventer mapEventer;
	MapEventerType type;

	void Awake() {
		tabs = gameObject.GetComponent<UIGamePanelTabs>();
		SetEventer (MapEventerType.DEFAULT);
	}

	private void SetEventer(MapEventerType type) {
		this.type = type;

		switch(type) {
			case MapEventerType.DEFAULT: 	mapEventer = new MapEventer(this); break;
			case MapEventerType.BUILD: 		mapEventer = new BuildMapEventer(this); break;
			case MapEventerType.PLACEUNIT: 	mapEventer = new PlaceUnitMapEventer(this); break;
			case MapEventerType.MOVEUNIT: 	mapEventer = new MoveUnitMapEventer(this); break;
		}
			
		if (type == MapEventerType.DEFAULT)
			this.Hide();
		else
			this.Show();
	}

	public void SetEventerType(MapEventerType type) {

		if (this.type == type)
			return;
		SetEventer(type);
		Sh.GameState.UpdateMapEventorType(type);

	}

	public MapEventerType GetEventerType() {
		return type;
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

public enum MapEventerType {
	DEFAULT,
	BUILD,
	PLACEUNIT,
	MOVEUNIT
}
