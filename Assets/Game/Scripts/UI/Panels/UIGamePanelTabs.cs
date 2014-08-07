using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGamePanelTabs : MonoBehaviour {

	Dictionary<PanelType, UIGamePanel> tabs;

	void Awake() {
		UIGamePanel[] ps = gameObject.GetComponentsInChildren<UIGamePanel>();
		tabs = new Dictionary<PanelType, UIGamePanel>();
		foreach(UIGamePanel p in ps) {
			if (p.transform.parent == transform)
				tabs[p.panelType] = p;
		}
	}

	public void SetTab(PanelType type) {
		foreach(KeyValuePair<PanelType, UIGamePanel> tab in tabs) {
			if (tab.Key == type)
				tab.Value.Show();
			else
				tab.Value.Hide();
		}
	}
}