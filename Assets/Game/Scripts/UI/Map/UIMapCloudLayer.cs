using UnityEngine;
using System.Collections;

public class UIMapCloudLayer : UIMapLayer {

	public GameObject GeneratePoint;

	void Awake() {
		StartCoroutine("GenerateClouds");
	}

	IEnumerator GenerateClouds() {
		while (true) {
			GameObject go = CreateElement(GeneratePoint.transform.localPosition);

			//random
			go.transform.localPosition += new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), 0);
			float rsc = Random.Range(-1.33f, 1.33f);
			go.transform.localScale += new Vector3(rsc, rsc, 0);
			UIWidget[] ws = go.GetComponentsInChildren<UIWidget>();
			foreach (UIWidget w in ws)
				w.alpha += Random.Range(-0.2f, 0.2f);

			GameObject.DestroyObject(go, 50f);
			yield return new WaitForSeconds(3f);
		}
	}

}
