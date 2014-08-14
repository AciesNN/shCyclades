using UnityEngine;
using System.Collections;

public class UIMapLayerElement : MonoBehaviour {

	public GameObject context;

	void Awake() {
		if (!context)
			Debug.Log("У объекта отсутствует контекст");
	}

	public virtual void SetDepth(int depth) {
		UIWidget[] ws = context.GetComponentsInChildren<UIWidget>();
		foreach(UIWidget w in ws)
			w.depth += depth;
	}

}
