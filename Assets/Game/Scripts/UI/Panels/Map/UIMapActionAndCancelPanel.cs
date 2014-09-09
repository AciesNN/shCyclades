using UnityEngine;
using System.Collections;

public class UIMapActionAndCancelPanel: UIGamePanel {

	#region Widgets
	public GameObject cancelButtonGameObject;
	public UILabel Description;
	#endregion

	#region WidgetsSet
	public void SetCancelButtonEnabled(bool on) {
		cancelButtonGameObject.SetActive(on);
	}

	public void SetDescription(string text) {
		Description.text = text;
	}
	#endregion

	#region Events
	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}

