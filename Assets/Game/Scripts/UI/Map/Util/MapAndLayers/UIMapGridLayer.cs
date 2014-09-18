using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapGridLayer:  UIMapLayer {

	protected new UIMapGridLayerElement [,] elements;

	void Awake() {
		Register();
	}

	public override void GameContext_LateInit() {
		CreateGridElements();
	}

	public virtual void CreateGridElements() {
	}

	protected virtual T CreateElement<T>(GridPosition pos) where T: UIMapGridLayerElement {
		if (!MapController.IsCellPossible(pos))
			Debug.Log ("Попытка создания объекта за пределами сетки");

		GameObject go = NGUITools.AddChild(gameObject, ObjectPrefab);
		go.name = pos.ToString();

		T el = go.GetComponent<T>();
		if (!el)
			Debug.Log ("У объекта сетки отсутствует компонент UIMapLayerElement");
		elements[pos.x, pos.y] = el;
		el.position = pos;

		go.transform.localPosition = MapController.CellToWorldPosition(pos);
		el.SetDepth(depth);

		return el;
	}

	protected virtual T CreateSingleElement<T>(GridPosition pos) where T: UIMapGridLayerElement {
		GameObject go = NGUITools.AddChild(gameObject, ObjectPrefab);
		go.name = pos.ToString();
		
		T el = go.GetComponent<T>();
		if (!el)
			Debug.Log ("У объекта сетки отсутствует компонент UIMapLayerElement");
		el.position = pos;
		
		MoveSingleElementToPos(el, pos);
		el.SetDepth(depth);
		
		return el;
	}

	protected void MoveSingleElementToPos(UIMapGridLayerElement el, GridPosition pos) {
		el.gameObject.transform.localPosition = MapController.CellToWorldPosition(pos);
	}

	public virtual T GetElement<T> (GridPosition pos) where T: UIMapGridLayerElement {
		return elements[pos.x, pos.y] as T;
	}
}

