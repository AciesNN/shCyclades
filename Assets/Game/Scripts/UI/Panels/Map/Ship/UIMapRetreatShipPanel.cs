using UnityEngine;
using System.Collections;

public class UIMapRetreatShipPanel: UIGamePanel {

	#region Events
	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}

