using UnityEngine;
using System.Collections;

public class UI_DEBUG_MapPanel : UIGamePanel {

	#region Eventers
	class MapEventer {

		protected UI_DEBUG_MapPanel parentObject;
		public MapEventer(UI_DEBUG_MapPanel parentObject) {
			this.parentObject = parentObject;
		}

		virtual public void OnUnitClick() {
		}

		virtual public void OnIslandClick() {
		}

		virtual public void OnIslandHover() {
		}

		virtual public void OnIslandHoverOut() {
			parentObject.HighlightIsland(false);
		}

		virtual public void OnMapCancel(){
			parentObject.SetEventer(MapEventerType.DEFAULT);
		}
		
	}

	class BuildMapEventer : MapEventer {

		public BuildMapEventer(UI_DEBUG_MapPanel parentObject): base(parentObject) {
		}

		override public void OnIslandClick() {
			parentObject.BuildOnIsland();
		}
		
		override public void OnIslandHover() {
			parentObject.HighlightIsland(true);
		}
		
	}

	class PlaceUnitMapEventer : MapEventer {

		public PlaceUnitMapEventer(UI_DEBUG_MapPanel parentObject): base(parentObject) {
		}
		
		override public void OnUnitClick() {
		}
		
		override public void OnIslandClick() {
		}
		
		override public void OnIslandHover() {
		}
		
	}

	class MoveUnitMapEventer : MapEventer {

		public MoveUnitMapEventer(UI_DEBUG_MapPanel parentObject): base(parentObject) {
		}
		
		override public void OnUnitClick() {
		}
		
		override public void OnIslandClick() {
		}
		
		override public void OnIslandHover() {
		}
		
	}
	#endregion

	#region VewWidgets
	public UISprite IslandSprite;
	#endregion

	MapEventer mapEventer;

	void Awake() {
		SetEventer(MapEventerType.DEFAULT);
	}

	public void SetEventer(MapEventerType type) {
		switch(type) {
		case MapEventerType.DEFAULT: 	mapEventer = new MapEventer(this); break;
		case MapEventerType.BUILD: 	mapEventer = new BuildMapEventer(this); break;
		case MapEventerType.PLACEUNIT: 	mapEventer = new PlaceUnitMapEventer(this); break;
		case MapEventerType.MOVEUNIT: 	mapEventer = new MoveUnitMapEventer(this); break;
		}
	}

	#region ViewWidgetsSet
	public void BuildOnIsland() {
		Sh.Out.Send("build");
		SetEventer(MapEventerType.DEFAULT);
	}

	public void HighlightIsland(bool active) {
		IslandSprite.color = (active ? Color.green : Color.white );
	}
	#endregion

	#region Events
	public void OnUnitClick() {
		mapEventer.OnUnitClick();
	}

	public void OnIslandClick() {
		mapEventer.OnIslandClick();
	}

	public void OnIslandHover() {
		mapEventer.OnIslandHover();
	}

	public void OnIslandHoverOut() {
		mapEventer.OnIslandHoverOut();
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
