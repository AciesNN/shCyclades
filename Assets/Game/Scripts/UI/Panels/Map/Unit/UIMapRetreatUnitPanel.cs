using UnityEngine;
using System.Collections;

public class UIMapRetreatUnitPanel: UIGamePanel {

	#region Events
	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion
}

