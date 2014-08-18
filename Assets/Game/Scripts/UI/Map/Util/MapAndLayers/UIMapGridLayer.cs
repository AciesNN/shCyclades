using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapGridLayer:  UIMapLayer {

	protected new UIMapGridLayerElement [,] elements;

	void Awake() {
		Register();
	}

	public override void LateInit() {
		CreateGridElements();
	}

	public virtual void CreateGridElements() {
		elements = new UIMapGridLayerElement[MapController.XSize, MapController.YSize];

		for (int x = 0; x < MapController.XSize; ++x) {
			for (int y = 0; y < MapController.YSize; ++y) {
				GridPosition pos = new GridPosition(x, y);
				if (MapController.IsCellPossible(pos))
					CreateElement<UIMapGridLayerElement>(pos);
			}
		}
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

	public virtual T GetElement<T> (GridPosition pos) where T: UIMapGridLayerElement {
		return elements[pos.x, pos.y] as T;
	}
}

