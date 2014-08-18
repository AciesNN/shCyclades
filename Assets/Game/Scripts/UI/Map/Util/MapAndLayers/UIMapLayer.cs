using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapLayer: MonoBehaviour, IUpdateble {

	public GameObject ObjectPrefab; 
	protected UIMapController MapController;

	protected List<GameObject> elements = new List<GameObject>();

	public GridLayerType type;
	public int depth {
		get { return MapLayersDepths.GetDepth(type); } 
	}

	protected void Register() {
		if (type == GridLayerType.ERROR)
			Debug.Log("Слою не задан тип");
		
		MapController = NGUITools.FindInParents<UIMapController>(gameObject);
		MapController.RegisterGridLayer(this);
	}

	void Awake() {
		Register();
	}

	protected virtual GameObject CreateElement(Vector3 pos) {

		GameObject go = NGUITools.AddChild(gameObject, ObjectPrefab);
		elements.Add(go);
		go.transform.localPosition = pos;

		UIWidget[] ws = go.GetComponentsInChildren<UIWidget>();
		foreach (UIWidget w in ws)
			w.depth += depth;

		return go;
	}

	public virtual void LateInit() {
		//elements = new UIMapLayerElement[]{};
	}
}

#region изменяется в конкретном проекте
public enum GridLayerType {
	//ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ

	ERROR,
	GRID,
	ISLANDS,
	SKY

	//ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ
}

static public class MapLayersDepths {
	static Dictionary<GridLayerType, int> depths = new Dictionary<GridLayerType, int>
	{

		//{GridLayerType.BACKGROUND, 100},

		{GridLayerType.ISLANDS, 200},

		{GridLayerType.GRID, 1000},

		{GridLayerType.SKY, 2000}

	};
	
	static public int GetDepth(GridLayerType type) {
		return depths[type];
	}
}
#endregion