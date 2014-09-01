using UnityEngine;
using System.Collections;

public class UIMapPlaceMetroPanel : UIGamePanel {

	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}

}
