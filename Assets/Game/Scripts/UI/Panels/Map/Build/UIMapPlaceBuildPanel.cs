using UnityEngine;
using System.Collections;

public class UIMapPlaceBuildPanel : UIGamePanel {

	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}

}
