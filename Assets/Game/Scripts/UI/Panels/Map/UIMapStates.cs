using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapStates : MonoBehaviour {

	public UIGamePanelTabs Panel;
	public UIMapController MapController;
	static public UIMapStates inst {
		get;
		private set;
	}

	Dictionary<MapEventerType, MapEventer> mapEventers = new Dictionary<MapEventerType, MapEventer>();
	MapEventerType type = MapEventerType.DEFAULT;
	public MapEventer eventer {
		get {
			MapEventer res = null;
			try {
				res = mapEventers[type];
			} catch {
				Debug.LogError("unk type: " + type); //TODO
			}
			
			return res; 
		}
	}

	void Awake() {
		inst = this;
		if (!MapController)
			MapController = GetComponent<UIMapController>();
		if (!MapController)
			Debug.LogError("Не указан компонент UIMapController");
		RegisterEventers();
	}

	void RegisterEventers() {
		MapEventer[] es = GetComponents<MapEventer>();
		foreach(MapEventer e in es) {
			mapEventers[e.type] = e;
		}
		_TestMapEventors();
	}

	public void SetEventorType(MapEventerType type) {
		if (this.type != MapEventerType.DEFAULT && type != MapEventerType.DEFAULT) //возможно, не самое красивое решение - оно не дает всем кнопкам подряд ставить свой тип евентора, когда другой уже работает
			return;
		if (this.type == type)
			return;
		if(eventer)
			eventer.Deactivate();
		this.type = type;
		eventer.Activate();

	}

	public MapEventerType GetEventorType() {
		return type;
	}

	public MapEventer GetEventorByType(MapEventerType type) {
		return mapEventers[type];
	}

	public void ReActivate() {
		eventer.ReActivate();
	}
	#region ViewWidgetsSet

	#endregion
	
	#region Events
	GridPosition oldCell = GridPosition.LessThanZero(); //TODO некрасиво

	public void OnClickCell(GridPosition cell) {
		//NGUIDebug.Log("press cell: " + cell);
		eventer.OnClickCell(cell);
	}
	
	public void OnHoverCell(GridPosition cell) {
		if (oldCell != cell) {
			OnHoverOutCell(oldCell);
			oldCell = new GridPosition(cell.x, cell.y);
		}
		eventer.OnHoverCell(cell);
	}
	
	public void OnHoverOutCell(GridPosition cell) {
		eventer.OnHoverOutCell(cell);
	}
	
	public void OnMapCancel() {
		eventer.OnMapCancel();
	}
	#endregion

	void _TestMapEventors() {
		MapEventerType[] eventers = System.Enum.GetValues(typeof(MapEventerType)) as MapEventerType[];
		foreach(MapEventerType e in eventers) {
			if (!mapEventers.ContainsKey(e))
				Debug.LogError("Не зарегестрирован Map eventor: " + e);
		}
	}
}
