using UnityEngine;
using System.Collections;

public class UIMapPanel : UIGamePanel {
	
	#region Eventers
	class MapEventer {
		
		protected UIMapPanel parentObject;
		public MapEventer(UIMapPanel parentObject) {
			this.parentObject = parentObject;
		}
		
		virtual public void OnClickCell(GridPosition cell) {
		}
		
		virtual public void OnHoverCell(GridPosition cell) {
		}
		
		virtual public void OnHoverOutCell(GridPosition cell) {
			parentObject.HighlightIsland(false);
		}
		
		virtual public void OnMapCancel() {
			parentObject.SetEventer(MapEventerType.DEFAULT);
		}
		
	}
	
	class BuildMapEventer : MapEventer {
		
		public BuildMapEventer(UIMapPanel parentObject): base(parentObject) {
		}
		
		override public void OnClickCell(GridPosition cell) {
			parentObject.BuildOnIsland();
		}
		
		override public void OnHoverCell(GridPosition cell) {
			if (cell.x == 4 && cell.y == 4)
				parentObject.HighlightIsland(true);
		}

		override public void OnHoverOutCell(GridPosition cell) {
			parentObject.HighlightIsland(false);
		}
	}
	
	class PlaceUnitMapEventer : MapEventer {
		
		public PlaceUnitMapEventer(UIMapPanel parentObject): base(parentObject) {
		}
		
	}
	
	class MoveUnitMapEventer : MapEventer {
		
		public MoveUnitMapEventer(UIMapPanel parentObject): base(parentObject) {
		}
		
	}
	#endregion
	
	#region VewWidgets

	#endregion

	public UIMapController MapController;

	MapEventer mapEventer;
	public MapEventerType type;

	void Awake() {
		SetEventer (MapEventerType.DEFAULT);
	}
	
	public void SetEventer(MapEventerType type) {

		if (this.type == type)
			return;

		switch(type) {
			case MapEventerType.DEFAULT: 	mapEventer = new MapEventer(this); break;
			case MapEventerType.BUILD: 	mapEventer = new BuildMapEventer(this); break;
			case MapEventerType.PLACEUNIT: 	mapEventer = new PlaceUnitMapEventer(this); break;
			case MapEventerType.MOVEUNIT: 	mapEventer = new MoveUnitMapEventer(this); break;
		}


		if (type == MapEventerType.DEFAULT)
			this.Hide();
		else
			this.Show();

	}
	
	#region ViewWidgetsSet
	public void BuildOnIsland() {
		Sh.Out.Send("build");
		SetEventer (MapEventerType.DEFAULT); 
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
