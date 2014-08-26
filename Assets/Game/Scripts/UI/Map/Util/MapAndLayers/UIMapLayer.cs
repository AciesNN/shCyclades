using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapLayer: MonoBehaviour {

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

	public virtual void GameContext_LateInit() {
		//elements = new UIMapLayerElement[]{};
	}
}