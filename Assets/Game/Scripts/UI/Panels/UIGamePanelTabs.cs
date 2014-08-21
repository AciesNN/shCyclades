using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGamePanelTabs : MonoBehaviour {

	bool hide;
	PanelType type;
	Dictionary<PanelType, UIGamePanel> tabs = new Dictionary<PanelType, UIGamePanel>();

	void Awake() {
		UIGamePanel[] ps = gameObject.GetComponentsInChildren<UIGamePanel>(true);
		foreach(UIGamePanel p in ps) {
			if (p.gameObject != gameObject)
				tabs[p.panelType] = p;
			p.TabPanel = this;
		}

		HideAll();
	}

	public void SetTab(PanelType type) {
		if (this.type == type && !hide)
			return;

		this.type = type;

		foreach(KeyValuePair<PanelType, UIGamePanel> tab in tabs) {
			if (tab.Key == type)
				tab.Value.Show();
			else
				tab.Value.Hide();
		}

		hide = false;
	}

	public void HideAll() {
		foreach (KeyValuePair<PanelType, UIGamePanel> tab in tabs) {
			tab.Value.Hide();
		}

		hide = true;
	}
}